using BusinessLayer;
using CommonLayer;
using EntityLayer;
using MailKit;
using MailKit.Net.Imap;
using MailKit.Search;
using MimeKit;
using PresentationLayer.Clases;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static CommonLayer.Enums;

namespace PresentationLayer
{
    public partial class frmAceptacionDocumentos : Form
    {
        // Origen de cada fila: distingue lo que ya existe en BD de lo recién
        // cargado desde XML (todavía no persistido).
        private const string ORIGEN_BD = "BD";
        private const string ORIGEN_XML = "XML";

        private readonly BCompras comprasIns = new BCompras();
        private readonly BFacturacion facturaIns = new BFacturacion();
        private readonly BProducto productoIns = new BProducto();

        // Lista de trabajo: cada compra + su origen, para saber si hay que
        // guardarla antes de confirmar (XML nuevo) o no (ya está en BD).
        private List<(tbCompras Compra, string Origen)> listaTrabajo = new List<(tbCompras, string)>();

        private bool actualizandoGrid = false;
        private BackgroundWorker worker;

        public frmAceptacionDocumentos()
        {
            InitializeComponent();
        }

        private void frmAceptacionDocumentos_Load(object sender, EventArgs e)
        {
            // Combo "Estado" de la grilla y el combo de aplicación masiva,
            // ambos alimentados directo del enum: si el día de mañana se
            // agrega "AceptadoParcial = 2" a Enums.Mensajes, aparece solo.
            var estados = Enum.GetValues(typeof(Enums.Mensajes)).Cast<Enums.Mensajes>().ToList();

            colEstado.DataSource = estados;
            colEstado.ValueType = typeof(Enums.Mensajes);

            cboEstadoMasivo.DataSource = estados.ToList();

            worker = new BackgroundWorker();
            worker.DoWork += Worker_DoWork;
            worker.RunWorkerCompleted += Worker_RunWorkerCompleted;

            btnCargarPendientes_Click(this, EventArgs.Empty);
        }

        // ---------- Carga ----------

        private void btnCargarPendientes_Click(object sender, EventArgs e)
        {
            try
            {
                var pendientes = comprasIns.ObtenerPendientesConfirmar();

                // Evita duplicar filas si el usuario aprieta "Cargar pendientes" más de una vez.
                var clavesYaCargadas = new HashSet<string>(
                    listaTrabajo.Select(x => x.Compra.claveEmisor?.Trim()));

                foreach (var compra in pendientes)
                {
                    if (clavesYaCargadas.Contains(compra.claveEmisor?.Trim())) continue;
                    listaTrabajo.Add((compra, ORIGEN_BD));
                }

                RedibujarGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los documentos pendientes: " + ex.Message,
                    "Cargar pendientes", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCargarXml_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() != DialogResult.OK) return;

            int agregados = 0, errores = 0;
            var rechazados = new List<string>();

            foreach (var file in openFileDialog1.FileNames)
            {
                var resultado = AgregarArchivoXml(file);
                switch (resultado.Tipo)
                {
                    case ResultadoAgregarXml.TipoResultado.Agregado:
                        agregados++;
                        break;
                    case ResultadoAgregarXml.TipoResultado.Rechazado:
                        rechazados.Add($"{Path.GetFileName(file)}: {resultado.Mensaje}");
                        break;
                    case ResultadoAgregarXml.TipoResultado.Error:
                        errores++;
                        break;
                        // Duplicado: se ignora en silencio, ya está en la grilla.
                }
            }

            RedibujarGrid();
            MostrarResumenCarga(rechazados, errores);
        }

        /// <summary>
        /// Resultado de intentar agregar un único archivo XML a la lista de
        /// trabajo — usado tanto por "Cargar XML" (diálogo de archivo) como
        /// por "Cargar por correo" (IMAP), para no duplicar la lógica.
        /// </summary>
        private class ResultadoAgregarXml
        {
            public enum TipoResultado { Agregado, Duplicado, Rechazado, Error }
            public TipoResultado Tipo { get; set; }
            public string Mensaje { get; set; }
        }

        /// <summary>
        /// Valida, parsea y agrega un archivo XML a listaTrabajo (sin
        /// redibujar el grid — eso lo hace el llamador una sola vez al
        /// final, para no repintar la grilla archivo por archivo).
        /// </summary>
        private ResultadoAgregarXml AgregarArchivoXml(string rutaArchivo)
        {
            try
            {
                if (!File.Exists(rutaArchivo))
                {
                    return new ResultadoAgregarXml
                    {
                        Tipo = ResultadoAgregarXml.TipoResultado.Error,
                        Mensaje = "El archivo no existe."
                    };
                }

                // Rechaza XML de confirmación (MensajeReceptor) u otros que
                // no sean un comprobante electrónico real.
                if (!CommonLayer.ValidadorXmlComprobante.EsXmlDeFacturaElectronica(rutaArchivo, out string motivoRechazo))
                {
                    return new ResultadoAgregarXml
                    {
                        Tipo = ResultadoAgregarXml.TipoResultado.Rechazado,
                        Mensaje = motivoRechazo
                    };
                }

                var compra = Documentos.ObtenerCompraXmlV44(rutaArchivo);
                compra.tipoDoc = compra.tipoDoc == (int)Enums.TipoDocumento.NotaCredito
                    ? (int)Enums.TipoDocumento.NotaCreditoGasto
                    : (int)Enums.TipoDocumento.Gastos;

                foreach (var item in compra.tbDetalleCompras)
                {
                    item.utilidad = 0;
                }

                bool yaCargado = listaTrabajo.Any(x =>
                    string.Equals(x.Compra.claveEmisor?.Trim(), compra.claveEmisor?.Trim(), StringComparison.OrdinalIgnoreCase));

                if (yaCargado)
                {
                    return new ResultadoAgregarXml { Tipo = ResultadoAgregarXml.TipoResultado.Duplicado };
                }

                listaTrabajo.Add((compra, ORIGEN_XML));
                return new ResultadoAgregarXml { Tipo = ResultadoAgregarXml.TipoResultado.Agregado };
            }
            catch (Exception ex)
            {
                return new ResultadoAgregarXml
                {
                    Tipo = ResultadoAgregarXml.TipoResultado.Error,
                    Mensaje = ex.Message
                };
            }
        }

        private void MostrarResumenCarga(List<string> rechazados, int errores)
        {
            if (rechazados.Count > 0)
            {
                string detalle = string.Join(Environment.NewLine, rechazados);
                MessageBox.Show(
                    $"{rechazados.Count} archivo(s) no se cargaron por no ser una factura electrónica válida:" +
                    Environment.NewLine + detalle,
                    "Cargar XML", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            if (errores > 0)
            {
                MessageBox.Show($"{errores} archivo(s) no se pudieron leer (formato incorrecto).",
                    "Cargar XML", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // ---------- Grid ----------

        private void RedibujarGrid()
        {
            actualizandoGrid = true;
            dgvDocumentos.Rows.Clear();

            foreach (var (compra, origen) in listaTrabajo)
            {
                var totalImp = compra.tbDetalleCompras?.Sum(x => (decimal?)x.montoTotalImp) ?? 0m;
                var totalLinea = compra.tbDetalleCompras?.Sum(x => (decimal?)x.montoTotalLinea) ?? 0m;

                Enums.Mensajes estadoActual = compra.codigoMensaje == (int)Enums.Mensajes.Rechazado
                    ? Enums.Mensajes.Rechazado
                    : Enums.Mensajes.Aceptado;

                int idx = dgvDocumentos.Rows.Add(
                    false,
                    origen,
                    compra.numFactura,
                    Enum.GetName(typeof(Enums.TipoDocumento), compra.tipoDoc),
                    compra.claveEmisor,
                    (compra.nombreProveedor ?? string.Empty).Trim().ToUpperInvariant(),
                    compra.fechaCompra.ToString("dd/MM/yyyy HH:mm"),
                    Utility.priceFormat(totalLinea),
                    estadoActual,
                    compra.razon,
                    ObtenerTextoEstadoHacienda(compra)
                );

                dgvDocumentos.Rows[idx].Tag = compra;
            }

            actualizandoGrid = false;
            ActualizarResumen();
        }

        private void ActualizarResumen()
        {
            lblResumen.Text = $"{listaTrabajo.Count} documento(s) cargado(s)";
        }

        /// <summary>
        /// Arma el texto de la columna "Estado en Hacienda" según el estado
        /// real del documento:
        /// - reporteAceptaHacienda = false/null → "Sin enviar" (todavía no
        ///   se le mandó la confirmación a Hacienda).
        /// - reporteAceptaHacienda = true, mensajeRespHacienda = false →
        ///   "Sin respuesta" (ya se envió, pero Hacienda no dio veredicto
        ///   final todavía).
        /// - reporteAceptaHacienda = true, mensajeRespHacienda = true →
        ///   se muestra EstadoFacturaHacienda (el veredicto real:
        ///   aceptado/rechazado), o "Sin respuesta" si por algún motivo
        ///   ese campo quedó vacío a pesar de mensajeRespHacienda = true.
        /// </summary>
        private string ObtenerTextoEstadoHacienda(tbCompras compra)
        {
            if (!compra.reporteAceptaHacienda)
            {
                return "Sin enviar";
            }

            if (!compra.mensajeRespHacienda)
            {
                return "Sin respuesta";
            }

            return string.IsNullOrWhiteSpace(compra.EstadoFacturaHacienda)
                ? "Sin respuesta"
                : compra.EstadoFacturaHacienda;
        }

        private void dgvDocumentos_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dgvDocumentos.CurrentCell is DataGridViewCheckBoxCell && dgvDocumentos.IsCurrentCellDirty)
            {
                dgvDocumentos.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void dgvDocumentos_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (actualizandoGrid || e.RowIndex < 0) return;

            var compra = (tbCompras)dgvDocumentos.Rows[e.RowIndex].Tag;
            string columna = dgvDocumentos.Columns[e.ColumnIndex].Name;

            if (columna == "colEstado")
            {
                var valor = dgvDocumentos.Rows[e.RowIndex].Cells["colEstado"].Value;
                if (valor is Enums.Mensajes estado)
                {
                    compra.codigoMensaje = (int)estado;
                    if (estado == Enums.Mensajes.Aceptado)
                    {
                        // Igual que en frmGastos: aceptado total no necesita razón.
                        dgvDocumentos.Rows[e.RowIndex].Cells["colRazon"].Value = string.Empty;
                        compra.razon = string.Empty;
                    }
                }
            }
            else if (columna == "colRazon")
            {
                compra.razon = dgvDocumentos.Rows[e.RowIndex].Cells["colRazon"].Value?.ToString()?.Trim().ToUpperInvariant();
            }
        }

        // ---------- Marcar / desmarcar / aplicar masivo ----------

        private void btnMarcarTodos_Click(object sender, EventArgs e) => MarcarTodos(true);
        private void btnDesmarcarTodos_Click(object sender, EventArgs e) => MarcarTodos(false);

        private void MarcarTodos(bool valor)
        {
            foreach (DataGridViewRow fila in dgvDocumentos.Rows)
            {
                fila.Cells["colSel"].Value = valor;
            }
        }

        private void btnAplicarAMarcados_Click(object sender, EventArgs e)
        {
            if (cboEstadoMasivo.SelectedItem == null)
            {
                MessageBox.Show("Seleccione un estado para aplicar.", "Aplicar a marcados",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var estado = (Enums.Mensajes)cboEstadoMasivo.SelectedItem;
            string razon = txtRazonMasiva.Text.Trim().ToUpperInvariant();

            if (estado != Enums.Mensajes.Aceptado && razon == string.Empty)
            {
                MessageBox.Show("Debe indicar una razón para aplicar un estado distinto de Aceptado.",
                    "Aplicar a marcados", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int aplicados = 0;

            foreach (DataGridViewRow fila in dgvDocumentos.Rows)
            {
                bool marcada = fila.Cells["colSel"].Value != null && (bool)fila.Cells["colSel"].Value;
                if (!marcada) continue;

                var compra = (tbCompras)fila.Tag;
                compra.codigoMensaje = (int)estado;
                compra.razon = estado == Enums.Mensajes.Aceptado ? string.Empty : razon;

                fila.Cells["colEstado"].Value = estado;
                fila.Cells["colRazon"].Value = compra.razon;

                aplicados++;
            }

            if (aplicados == 0)
            {
                MessageBox.Show("No hay documentos marcados en la columna 'Sel.'.", "Aplicar a marcados",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        // ---------- Procesar ----------

        private void btnProcesar_Click(object sender, EventArgs e)
        {
            var seleccionados = new List<(tbCompras Compra, string Origen)>();

            foreach (DataGridViewRow fila in dgvDocumentos.Rows)
            {
                bool marcada = fila.Cells["colSel"].Value != null && (bool)fila.Cells["colSel"].Value;
                if (!marcada) continue;

                var compra = (tbCompras)fila.Tag;
                string origen = fila.Cells["colOrigen"].Value.ToString();
                seleccionados.Add((compra, origen));
            }

            if (seleccionados.Count == 0)
            {
                MessageBox.Show("No hay documentos marcados para procesar.", "Procesar",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Validación: razón obligatoria cuando el estado no es Aceptado.
            var sinRazon = seleccionados
                .Where(x => x.Compra.codigoMensaje != (int)Enums.Mensajes.Aceptado
                         && string.IsNullOrWhiteSpace(x.Compra.razon))
                .ToList();

            if (sinRazon.Count > 0)
            {
                string detalle = string.Join(Environment.NewLine,
                    sinRazon.Select(x => $"- {x.Compra.numFactura} ({x.Compra.nombreProveedor})"));

                MessageBox.Show(
                    "Debe indicar una razón para los siguientes documentos (estado distinto de Aceptado):" +
                    Environment.NewLine + detalle,
                    "Datos incompletos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirmar = MessageBox.Show(
                $"¿Desea procesar {seleccionados.Count} documento(s) seleccionado(s) y confirmarlos ante Hacienda?",
                "Confirmar procesamiento", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirmar != DialogResult.Yes) return;

            // Bridge defensivo: la grilla y frmGastos trabajan sobre compra.razon,
            // pero el XML del Mensaje Receptor usa compra.DetalleMensaje.
            // Se igualan acá para que lo que el usuario escribió en "Razón"
            // sea justo lo que se manda a Hacienda como DetalleMensaje.
            foreach (var (compra, _) in seleccionados)
            {
                compra.DetalleMensaje = compra.codigoMensaje == (int)Enums.Mensajes.Aceptado
                    ? "Documento recibido y aceptado en su totalidad."
                    : compra.razon;
            }

            btnProcesar.Enabled = false;
            btnCargarPendientes.Enabled = false;
            btnCargarXml.Enabled = false;
            progressBar1.Visible = true;
            progressBar1.Style = ProgressBarStyle.Marquee;

            worker.RunWorkerAsync(seleccionados);
        }

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            var seleccionados = (List<(tbCompras Compra, string Origen)>)e.Argument;

            // 1) Las que vienen de XML todavía no existen en BD: hay que
            //    guardarlas primero (mismo flujo de resolución de producto
            //    que usa frmGastos), antes de poder confirmarlas ante Hacienda.
            var nuevasDeXml = seleccionados
                .Where(x => x.Origen == ORIGEN_XML)
                .Select(x => x.Compra)
                .ToList();

            if (nuevasDeXml.Count > 0)
            {
                PrepararYGuardarNuevas(nuevasDeXml);
            }

            // 2) Confirmar ante Hacienda TODAS las seleccionadas (ya sea que
            //    vinieran de BD o que se acaben de guardar en el paso 1).
            var todasLasCompras = seleccionados.Select(x => x.Compra).ToList();
            facturaIns.reportarMensajesHacienda(todasLasCompras);

            e.Result = seleccionados.Count;
        }

        /// <summary>
        /// Resuelve producto/CABYS para compras nuevas cargadas desde XML,
        /// igual que hace frmGastos.btnProcesar_Click, y las persiste.
        /// </summary>
        private void PrepararYGuardarNuevas(List<tbCompras> nuevas)
        {
            var listaOk = new List<tbCompras>();

            foreach (var compra in nuevas)
            {
                bool ok = true;
                try
                {
                    if (compra.tipoDoc == (int)Enums.TipoDocumento.Compras)
                    {
                        foreach (var detalle in compra.tbDetalleCompras)
                        {
                            tbProducto producto = new tbProducto { nombre = detalle.nombreProducto };
                            var pro = productoIns.GetEntityByNombre(producto);

                            if (pro != null)
                            {
                                detalle.idProducto = pro.idProducto;
                                detalle.utilidad = pro.precioUtilidad1;
                            }
                            else
                            {
                                detalle.idProducto = "0";
                                detalle.utilidad = (decimal)Global.Usuario.tbEmpresa.tbParametrosEmpresa
                                    .First().utilidadBase;
                            }
                        }
                    }
                    else
                    {
                        var compraOriginal = comprasIns.GetEntityComprasByClave(compra.claveRef);

                        foreach (var detalle in compra.tbDetalleCompras)
                        {
                            if (string.IsNullOrEmpty(detalle.codigoCabys))
                            {
                                ok = false;
                                continue;
                            }

                            if (compraOriginal != null)
                            {
                                var detalleOriginal = compraOriginal.tbDetalleCompras
                                    .SingleOrDefault(x => x.numLinea == detalle.numLinea
                                                        && x.codigoCabys == detalle.codigoCabys);

                                if (detalleOriginal != null)
                                {
                                    detalle.idProducto = detalleOriginal.idProducto;
                                    detalle.utilidad = detalleOriginal.utilidad;
                                }
                            }
                        }
                    }
                }
                catch (Exception)
                {
                    ok = false;
                }

                if (ok)
                {
                    listaOk.Add(compra);
                }
            }

            if (listaOk.Count > 0)
            {
                facturaIns.guadarListaCompras(listaOk);
            }
        }

        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            btnProcesar.Enabled = true;
            btnCargarPendientes.Enabled = true;
            btnCargarXml.Enabled = true;
            progressBar1.Visible = false;
            progressBar1.Style = ProgressBarStyle.Blocks;

            if (e.Error != null)
            {
                MessageBox.Show("Ocurrió un error al procesar los documentos: " + e.Error.Message,
                    "Procesar", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            MessageBox.Show("Documentos procesados. Revise el estado de cada uno para confirmar " +
                "que Hacienda los aceptó.", "Procesar", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Recarga desde BD para reflejar el estado real post-envío
            // (reporteAceptaHacienda actualizado por reportarMensajesHacienda).
            listaTrabajo.Clear();
            RedibujarGrid();
            btnCargarPendientes_Click(this, EventArgs.Empty);
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // ---------- Cargar por correo (IMAP) ----------

        private async void btnCargarCorreo_Click(object sender, EventArgs e)
        {
            string email = Global.actividadEconomic.correoCompras?.Trim();
            string password = Global.actividadEconomic.claveCorreo?.Trim();

            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show(
                    "No hay configurado un correo de compras (correoCompras/claveCorreo) para esta empresa.",
                    "Cargar por correo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string savePath = Global.Usuario.tbEmpresa.tbParametrosEmpresa.FirstOrDefault().rutaBackUp.Trim()
                + "archivoCorreoElectronico";

            DateTime fechaInicio = dtpFechaInicio.Value.Date;
            DateTime fechaFin = dtpFechaFin.Value.Date.AddDays(1); // incluye todo el día final

            btnCargarCorreo.Enabled = false;
            btnCargarXml.Enabled = false;
            btnCargarPendientes.Enabled = false;
            btnProcesar.Enabled = false;
            progressBar1.Visible = true;
            progressBar1.Style = ProgressBarStyle.Blocks;
            progressBar1.Minimum = 0;
            progressBar1.Value = 0;

            int agregados = 0, duplicados = 0, errores = 0;
            var rechazados = new List<string>();

            try
            {
                Directory.CreateDirectory(savePath);

                using (var client = new ImapClient())
                {
                    await client.ConnectAsync("imap.gmail.com", 993, true);
                    await client.AuthenticateAsync(email, password);

                    var inbox = client.Inbox;
                    await inbox.OpenAsync(FolderAccess.ReadOnly);

                    var query = SearchQuery.SentSince(fechaInicio).And(SearchQuery.SentBefore(fechaFin));
                    var uids = await inbox.SearchAsync(query);

                    progressBar1.Maximum = Math.Max(uids.Count, 1);

                    int correosConAdjuntos = 0;
                    int counter = 0;

                    foreach (var uid in uids)
                    {
                        var message = await inbox.GetMessageAsync(uid);
                        var attachments = message.Attachments.OfType<MimePart>().ToList();

                        bool hasPdf = attachments.Any(a => a.FileName != null &&
                            a.FileName.EndsWith(".pdf", StringComparison.OrdinalIgnoreCase));
                        bool hasXml = attachments.Any(a => a.FileName != null &&
                            a.FileName.EndsWith(".xml", StringComparison.OrdinalIgnoreCase));

                        if (hasPdf && hasXml)
                        {
                            foreach (var mimePart in attachments)
                            {
                                string filePath = Path.Combine(savePath, mimePart.FileName);

                                using (var stream = File.Create(filePath))
                                {
                                    mimePart.Content.DecodeTo(stream);
                                }

                                if (!mimePart.FileName.EndsWith(".xml", StringComparison.OrdinalIgnoreCase))
                                {
                                    continue; // el PDF se guarda pero no se procesa acá
                                }

                                var resultado = AgregarArchivoXml(filePath);
                                switch (resultado.Tipo)
                                {
                                    case ResultadoAgregarXml.TipoResultado.Agregado:
                                        agregados++;
                                        break;
                                    case ResultadoAgregarXml.TipoResultado.Duplicado:
                                        duplicados++;
                                        break;
                                    case ResultadoAgregarXml.TipoResultado.Rechazado:
                                        rechazados.Add($"{mimePart.FileName}: {resultado.Mensaje}");
                                        // No es un comprobante válido (ej. era un
                                        // MensajeReceptor) — no tiene sentido dejarlo
                                        // en disco mezclado con las facturas reales.
                                        File.Delete(filePath);
                                        break;
                                    case ResultadoAgregarXml.TipoResultado.Error:
                                        errores++;
                                        File.Delete(filePath);
                                        break;
                                }
                            }

                            correosConAdjuntos++;
                        }

                        counter++;
                        progressBar1.Value = Math.Min(counter, progressBar1.Maximum);
                    }

                    await client.DisconnectAsync(true);

                    RedibujarGrid();
                    MostrarResumenCarga(rechazados, errores);

                    MessageBox.Show(
                        $"Se revisaron {uids.Count} correo(s) entre {fechaInicio:dd/MM/yyyy} y {fechaFin.AddDays(-1):dd/MM/yyyy}." +
                        Environment.NewLine +
                        $"Correos con PDF+XML: {correosConAdjuntos}" + Environment.NewLine +
                        $"Facturas agregadas: {agregados}" + Environment.NewLine +
                        $"Ya estaban cargadas: {duplicados}",
                        "Cargar por correo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar correos: " + ex.Message,
                    "Cargar por correo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnCargarCorreo.Enabled = true;
                btnCargarXml.Enabled = true;
                btnCargarPendientes.Enabled = true;
                btnProcesar.Enabled = true;
                progressBar1.Visible = false;
            }
        }

        // ---------- Consultar estado en Hacienda ----------

        private void btnConsultarEstado_Click(object sender, EventArgs e)
        {
            var enviados = listaTrabajo
                .Where(x => x.Compra.reporteAceptaHacienda)
                .Select(x => x.Compra)
                .ToList();

            if (enviados.Count == 0)
            {
                MessageBox.Show(
                    "No hay documentos ya enviados a Hacienda para consultar. " +
                    "Primero use 'Procesar seleccionados'.",
                    "Consultar estado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            btnProcesar.Enabled = false;
            btnCargarPendientes.Enabled = false;
            btnCargarXml.Enabled = false;
            btnConsultarEstado.Enabled = false;
            progressBar1.Visible = true;
            progressBar1.Style = ProgressBarStyle.Marquee;

            var workerConsulta = new BackgroundWorker();
            workerConsulta.DoWork += (s, args) =>
            {
                facturaIns.consultarEstadoConfirmaciones(enviados);
            };
            workerConsulta.RunWorkerCompleted += (s, args) =>
            {
                btnProcesar.Enabled = true;
                btnCargarPendientes.Enabled = true;
                btnCargarXml.Enabled = true;
                btnConsultarEstado.Enabled = true;
                progressBar1.Visible = false;
                progressBar1.Style = ProgressBarStyle.Blocks;

                if (args.Error != null)
                {
                    MessageBox.Show("Error al consultar el estado: " + args.Error.Message,
                        "Consultar estado", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                RedibujarGrid();
                MessageBox.Show("Consulta completada. Revise la columna 'Estado en Hacienda'.",
                    "Consultar estado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            };

            workerConsulta.RunWorkerAsync();
        }
    }
}