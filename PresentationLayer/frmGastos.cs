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
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;
using static CommonLayer.Enums;

namespace PresentationLayer
{
    public partial class frmGastos : FormBase
    {
        List<tbCompras> listaCompras = new List<tbCompras>();
        BFacturacion facturaIns = new BFacturacion();
        BProducto productoIns = new BProducto();
        BCompras comprasIns = new BCompras();
        private List<string> xmlFilePaths = new List<string>(); // Lista para almacenar rutas de archivos XML



        public frmGastos()
        {
            InitializeComponent();
        }

        private void frmMensajesHacienda_Load(object sender, EventArgs e)
        {
            InitializeOpenFileDialog();

            bool cargaCorreoElect = (Global.actividadEconomic.claveCorreo.Trim() != null && Global.actividadEconomic.claveCorreo.Trim() != string.Empty);

      

            label13.Visible = cargaCorreoElect;
            label14.Visible = cargaCorreoElect;
            dtpIncio.Visible = cargaCorreoElect;
            dtpFin.Visible = cargaCorreoElect;
            btnCorreo.Visible = cargaCorreoElect;
                    
           
           cboEstado.DataSource = Enum.GetValues(typeof(Enums.Mensajes));


        }

        private void InitializeOpenFileDialog()
        {
            // Set the file dialog to filter for graphics files.
            this.openFileDialog1.Filter = "All files (*.XML)|*.XML";

            // Allow the user to select multiple images.
            this.openFileDialog1.Multiselect = true;
            this.openFileDialog1.Title = "Archivos XML de compras";
        }

        private void cargarLista()
        {

            lsvDoc.Items.Clear();
            foreach (var resp in listaCompras)
            {

                ListViewItem item = new ListViewItem();

                item.Text = resp.numFactura;
                item.SubItems.Add(Enum.GetName(typeof(Enums.TipoDocumento), resp.tipoDoc));
                item.SubItems.Add(resp.claveEmisor);
                item.SubItems.Add(Enum.GetName(typeof(Enums.TipoMoneda), resp.tipoMoneda));
                item.SubItems.Add(Enum.GetName(typeof(Enums.TipoId), resp.tipoIdProveedor));
                item.SubItems.Add(resp.nombreProveedor);
                item.SubItems.Add(resp.fechaCompra.ToString());
                item.SubItems.Add(resp.tbDetalleCompras.Sum(x => x.montoTotalImp).ToString());
                item.SubItems.Add(resp.tbDetalleCompras.Sum(x => x.montoTotalLinea).ToString());
                item.SubItems.Add(resp.codigoMensaje == 0 ? "Sin Estado" : Enum.GetName(typeof(Mensajes), resp.codigoMensaje));

                lsvDoc.Items.Add(item);

            }

        }
        private void btnCargar_Click(object sender, EventArgs e)
        {
            //lsvDoc.Items.Clear();
            limpiarDetalle();
            listaCompras.Clear();
            txtMensaje.Text = string.Empty;
            DialogResult dr = this.openFileDialog1.ShowDialog();
            XPathNavigator nav; XPathDocument docNav;

            if (dr == System.Windows.Forms.DialogResult.OK)
            {
                // Read the files
                foreach (String file in openFileDialog1.FileNames)

                {
                    // Create a PictureBox.
                    try
                    {
                        /////
                        ///
                        if (File.Exists(file))
                        {

                            var gasto = Documentos.ObtenerCompraXmlV44(file);
                            gasto.tipoDoc = gasto.tipoDoc== (int)Enums.TipoDocumento.NotaCredito ? (int)Enums.TipoDocumento.NotaCreditoGasto : (int)Enums.TipoDocumento.Gastos;
                            foreach (var item in gasto.tbDetalleCompras)
                            {
                                item.utilidad = 0;
                               
                                
                            }
                        
                            
                          
                            listaCompras.Add(gasto);


                        }

                    }

                    catch (Exception ex)
                    {
                        // Could not load the image - probably related to Windows file system permissions.
                        MessageBox.Show("El documento seleccionado no tiene el formato requerido. " + file.Substring(file.LastIndexOf('\\'))
                           );
                    }
                }

                cargarLista();
            }
        }

        private bool validarDatos()
        {

            foreach (var item in listaCompras)
            {
                if (item.codigoMensaje != (int)Enums.Mensajes.Aceptado)
                {
                    if (item.razon.Trim() == string.Empty)
                    {

                        MessageBox.Show($"Debe indicar una razón para el documento #{item.numFactura}, Emisor: {item.nombreProveedor}, ya que se encuentra en estado: {Enum.GetName(typeof(Enums.EstadoRespuestaHacienda), item.codigoMensaje).ToUpper()} ", "Datos incompletos", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        return false;

                    }


                }
            }

            return true;
        }



        private void limpiar()
        {
            txtClave.Text = string.Empty;
            txtID.Text = string.Empty;
            txtFecha.Text = string.Empty;
            txtEmisor.Text = string.Empty;

            cboEstado.SelectedIndex = 0;

            txtImp.Text = string.Empty;
            txtTotal.Text = string.Empty;


        }
        private void lsvDoc_SelectedIndexChanged(object sender, EventArgs e)
        {


        }

        //private void cboEstado_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if ((int)cboEstado.SelectedValue == 1)
        //    {
        //        txtRazon.Enabled = false;
        //        txtRazon.Text = string.Empty;

        //    }
        //    else
        //    {

        //        //foreach (var item in listaCompras)
        //        //{
        //        //    if (item.claveDocEmisor.Trim() == txtClave.Text.Trim())
        //        //    {
        //        //        txtRazon.Text = item.razon;
        //        //        break;

        //        //    }
        //        //}
        //        //txtRazon.Enabled = true;

        //    }
        //}

        //private void btnActualizar_Click(object sender, EventArgs e)
        //{
        //    if ((int)cboEstado.SelectedValue != (int)Enums.Mensajes.Aceptado && txtRazon.Text.Trim() == string.Empty)
        //    {
        //        MessageBox.Show($"Debe indicar una razón para el documento #{txtClave.Text}, Emisor:{txtEmisor.Text}, Estado:{cboEstado.Text} ", "Datos incompletos", MessageBoxButtons.OK, MessageBoxIcon.Stop);
        //    }
        //    else
        //    {
        //        foreach (var item in listaCompras)
        //        {
        //            //if (item.claveDocEmisor.Trim() == txtClave.Text.Trim())
        //            //{
        //            //    item.estadoRecibido = (int)cboEstado.SelectedValue;
        //            //    item.razon = txtRazon.Text.Trim().ToUpper();
        //            //    break;

        //            //}
        //        }
        //        MessageBox.Show("Datos actualizados correctamente", "Actulización de datos", MessageBoxButtons.OK, MessageBoxIcon.Information);

        //        cargarLista();
        //        limpiar();


        //    }

        //}

        private void lsvDoc_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {

                if (lsvDoc.SelectedItems.Count > 0)
                {

                    var clave = (lsvDoc.SelectedItems.Count > 0 && lsvDoc.SelectedItems[0].SubItems.Count > 2)
            ? lsvDoc.SelectedItems[0].SubItems[2].Text?.Trim()
            : null;

                if (string.IsNullOrWhiteSpace(clave))
                    return;

                // 2) Buscar la compra (evita foreach)
                var compra = listaCompras.FirstOrDefault(c =>
                    string.Equals(c.claveEmisor?.Trim(), clave, StringComparison.OrdinalIgnoreCase));

                if (compra is null)
                        return;

                    //sino se han guardado cambios 
                    if (txtFactura.Text != "")
                    {

                        var compraAct = listaCompras.FirstOrDefault(c =>
                  string.Equals(c.numFactura?.Trim(), txtFactura.Text.Trim(), StringComparison.OrdinalIgnoreCase));

                        // Obtener valores actuales
                        int nuevoCodigo = (int)cboEstado.SelectedValue;
                        string nuevaRazon = txtRazon.Text?.Trim().ToUpper() ?? string.Empty;
                        string razonActual = compraAct.razon?.Trim().ToUpper() ?? string.Empty;

                        // Verificar cambios antes de actualizar
                        if (compraAct.codigoMensaje != nuevoCodigo || razonActual != nuevaRazon)
                        {
                            compraAct.codigoMensaje = nuevoCodigo;

                            // Solo guardar razón si no es código 1
                            compraAct.razon = (nuevoCodigo != 1) ? nuevaRazon : compraAct.razon;
                        }

                    }







                    // 3) Totales con null-safety (asumiendo decimal; use decimal? si aplica)
                    var totalImp = compra.tbDetalleCompras?.Sum(x => (decimal?)x.montoTotalImp) ?? 0m;
                    var totalLinea = compra.tbDetalleCompras?.Sum(x => (decimal?)x.montoTotalLinea) ?? 0m;

                    // 4) Cultura de Costa Rica para formatos
                    var cr = CultureInfo.GetCultureInfo("es-CR");

                    // 5) Poblar controles
                    txtFactura.Text = compra.numFactura ?? string.Empty;
                    txtTipoDoc.Text = Enum.GetName(typeof(Enums.TipoDocumento), compra.tipoDoc) ?? string.Empty;

                    txtClave.Text = compra.claveEmisor ?? string.Empty;
                    txtFecha.Text = compra.fechaCompra.ToString("dd/MM/yyyy HH:mm", cr);

                    txtID.Text = compra.idProveedor?.Trim() ?? string.Empty;
                    txtEmisor.Text = (compra.nombreProveedor ?? string.Empty).Trim().ToUpperInvariant();

                    txtMoneda.Text = Enum.GetName(typeof(Enums.TipoMoneda), compra.tipoMoneda) ?? string.Empty;
                    txtCambio.Text = (compra.tipoCambio ?? 0m).ToString("N2", cr);
                    txtImp.Text = totalImp.ToString("N2", cr);
                    txtTotal.Text = totalLinea.ToString("N2", cr);

                    cboEstado.SelectedItem = (Enums.Mensajes)compra.codigoMensaje;
                    txtRazon.Text = compra.codigoMensaje == 1 ? string.Empty : compra.razon;


                }
                cargarLista();
            }
            catch (Exception ex)
             {

                MessageBox.Show(ex.Message);
            }

        }

        public void limpiarDetalle()
        {

            txtFactura.Text = string.Empty;
            txtTipoDoc.Text = string.Empty;

            txtClave.Text = string.Empty;
            txtFecha.Text = string.Empty;

            txtID.Text = string.Empty;
            txtEmisor.Text = string.Empty;

            txtMoneda.Text = string.Empty;
            txtCambio.Text = string.Empty;
            txtImp.Text = string.Empty;
            txtTotal.Text = string.Empty;

        }

        private void btnProcesar_Click(object sender, EventArgs e)
        {
            try
            {

                //ACTUALIZO EL ULTIMO CMBIO ANTES DE GUARDAR
                if (txtFactura.Text != "")
                {

                    var compraAct = listaCompras.FirstOrDefault(c =>
                    string.Equals(c.numFactura?.Trim(), txtFactura.Text.Trim(), StringComparison.OrdinalIgnoreCase));

                    // Obtener valores actuales
                    int nuevoCodigo = (int)cboEstado.SelectedValue;
                    string nuevaRazon = txtRazon.Text?.Trim().ToUpper() ?? string.Empty;
                    string razonActual = compraAct.razon?.Trim().ToUpper() ?? string.Empty;

                    // Verificar cambios antes de actualizar
                    if (compraAct.codigoMensaje != nuevoCodigo || razonActual != nuevaRazon)
                    {
                        compraAct.codigoMensaje = nuevoCodigo;

                        // Solo guardar razón si no es código 1
                        compraAct.razon = (nuevoCodigo != 1) ? nuevaRazon : compraAct.razon;
                    }

                }
                cargarLista();

                DialogResult resul = MessageBox.Show(" ¿Desea registrar los documentos?", "Guardar lista de compras", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (resul == DialogResult.OK)
                {

                    if (!validarDatos())
                    {
                        return;
                    }

                  

                    bool bandera = true;
                    List<tbCompras> comprasListAux = new List<tbCompras>();
                    foreach (var compraGlobal in listaCompras)
                    {
                        bandera = true;
                        try
                        {
                            if (compraGlobal.tipoDoc == (int)Enums.TipoDocumento.Compras)
                            {
                                foreach (var detalle in compraGlobal.tbDetalleCompras)
                                {
                                    tbProducto producto = new tbProducto();
                                    producto.nombre = detalle.nombreProducto;
                                    var pro = productoIns.GetEntityByNombre(producto);
                                    if (pro != null)
                                    {
                                        detalle.idProducto = pro.idProducto;
                                        detalle.utilidad = pro.precioUtilidad1;

                                    }
                                    else
                                    {
                                        detalle.idProducto = "0";
                                        detalle.utilidad = (decimal)Global.Usuario.tbEmpresa.tbParametrosEmpresa.First().utilidadBase;
                                    }

                                }
                            }
                            else
                            {
                                tbCompras compraOriginal = comprasIns.GetEntityComprasByClave(compraGlobal.claveRef);
                                foreach (var detalle in compraGlobal.tbDetalleCompras)
                                {
                                    if (detalle.codigoCabys == null || detalle.codigoCabys == string.Empty)
                                    {
                                        string mens = string.Format("El documento no tiene Cabys. #Factura:{1} No se procesará el documento.{0}", Environment.NewLine, compraGlobal.numFactura);
                                        MessageBox.Show(mens, "Documento incorrecto", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        txtMensaje.Text += mens;

                                        bandera = false;
                                        continue;
                                    }

                                    if (compraOriginal != null)
                                    {

                                        detalle.idProducto = compraOriginal.tbDetalleCompras.Where(x => x.numLinea == detalle.numLinea && x.codigoCabys == detalle.codigoCabys).SingleOrDefault().idProducto;
                                        detalle.utilidad = compraOriginal.tbDetalleCompras.Where(x => x.numLinea == detalle.numLinea && x.codigoCabys == detalle.codigoCabys).SingleOrDefault().utilidad;

                                    }
                                }


                            }

                        }
                        catch (Exception)
                        {

                            MessageBox.Show("El documento no tiene formato correcto. #Factura:" + compraGlobal.numFactura, "Documento incorrecto", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                        if (bandera)
                        {
                            comprasListAux.Add(compraGlobal);
                        }

                    }

                    listaCompras = comprasListAux;

                    txtMensaje.Text += facturaIns.guadarListaCompras(listaCompras);
                    chkCambiarColon.Checked = false;
                    ////   enviarCorreo(lista);
                    //// MessageBox.Show(mensaje, "Procesados...", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    //lsvDoc.Items.Clear();
                    //limpiar();
                    //MessageBox.Show("Se han procesados los archivos, en segundo plano se enviarán los correos electrónicos", "Archivos procesados.", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    BackgroundWorker tarea = new BackgroundWorker();

                    tarea.DoWork += reportarFacturacionElectronica;
                    tarea.RunWorkerAsync();






                }

            }
            catch (Exception)
            {

                MessageBox.Show("Ocurrió un error al procesar los datos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void reportarFacturacionElectronica(object sender, DoWorkEventArgs e)
        {
            try
            {


                facturaIns.reportarMensajesHacienda(listaCompras);

            
 



                //enviarCorreo(lista);

            }
            catch (Exception)
            {

                throw;
            }


        }

        private void enviarCorreo(List<tbReporteHacienda> lista)
        {
            try
            {
                bool enviado = false;
                foreach (var item in lista)
                {
                    List<string> correos = new List<string>();


                    if (item.correoElectronico != null)
                    {
                        correos.Add(item.correoElectronico.Trim());

                    }

                    clsDocumentoCorreo docCorreo = new clsDocumentoCorreo(item, correos, true, (int)Enums.tipoAdjunto.mensaje);
                    CorreoElectronico.enviarCorreo(docCorreo);
                }
                MessageBox.Show("Se han enviado los correos electrónicos.", "Envio de correos electrónicos", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception)
            {

                MessageBox.Show("No se pudo enviar los correos, favor de verificar los datos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void chkCambiarColon_CheckedChanged(object sender, EventArgs e)
        {
            var lista = listaCompras.Where(x => x.tipoMoneda == (int)Enums.TipoMoneda.USD).ToList();

            foreach (var compra in lista)
            {
                if (chkCambiarColon.Checked)
                {
                    foreach (var detalle in compra.tbDetalleCompras)
                    {
                        detalle.precio = detalle.precio * (decimal)compra.tipoCambio;
                        detalle.montoTotal = detalle.precio * detalle.cantidad;

                        detalle.montoTotaDesc = detalle.montoTotaDesc * (decimal)compra.tipoCambio;
                        detalle.montoTotalImp = detalle.montoTotalImp * (decimal)compra.tipoCambio;
                        detalle.montoTotalExo = detalle.montoTotalExo * (decimal)compra.tipoCambio;
                        detalle.montoTotalLinea = detalle.montoTotalLinea * (decimal)compra.tipoCambio;

                    }

                }
                else
                {
                    foreach (var detalle in compra.tbDetalleCompras)
                    {
                        detalle.precio = detalle.precio / (decimal)compra.tipoCambio;
                        detalle.montoTotal = detalle.precio / detalle.cantidad;

                        detalle.montoTotaDesc = detalle.montoTotaDesc / (decimal)compra.tipoCambio;
                        detalle.montoTotalImp = detalle.montoTotalImp / (decimal)compra.tipoCambio;
                        detalle.montoTotalExo = detalle.montoTotalExo / (decimal)compra.tipoCambio;
                        detalle.montoTotalLinea = detalle.montoTotalLinea / (decimal)compra.tipoCambio;

                    }

                }


            }
            limpiarDetalle();
            cargarLista();





        }

        private void btnsalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            

            // Credenciales y ruta de guardado
            string email = Global.actividadEconomic.correoCompras.Trim(); // Tu correo
            string password = Global.actividadEconomic.claveCorreo.Trim(); // Tu contraseña o contraseña de aplicación
            string savePath = CommonLayer.Global.Usuario.tbEmpresa.tbParametrosEmpresa.FirstOrDefault().rutaBackUp.Trim()+"archivoCorreoElectronico";  // Carpeta donde guardar los archivos

            // Obtener fechas del DateTimePicker
            DateTime fechaInicio = dtpIncio.Value.Date;
            DateTime fechaFin = dtpFin.Value.Date.AddDays(1); // Para incluir toda la fecha de fin

            xmlFilePaths.Clear(); // Limpiar la lista antes de procesar nuevos correos

            try
            {
                using (var client = new ImapClient())
                {
                    // Conectar a Gmail
                    await client.ConnectAsync("imap.gmail.com", 993, true);
                    await client.AuthenticateAsync(email, password);

                    // Acceder a la bandeja de entrada
                    var inbox = client.Inbox;
                    await inbox.OpenAsync(FolderAccess.ReadOnly);

                    // Buscar correos entre las fechas dadas
                    var query = SearchQuery.SentSince(fechaInicio).And(SearchQuery.SentBefore(fechaFin));
                    var uids = await inbox.SearchAsync(query);

                    // Configurar el ProgressBar
                    progressBar1.Visible = true;
                    progressBar1.Minimum = 0;
                    progressBar1.Maximum = uids.Count;
                    progressBar1.Value = 0;
                    btnCorreo.Enabled = false;
                    btnProcesar.Enabled = false;
                    btnCargar.Enabled = false;
                    chkCambiarColon.Enabled = false;

                    int emailsProcessed = 0;
                    int counter = 0;

                    foreach (var uid in uids)
                    {
                        // Obtener el mensaje
                        var message = await inbox.GetMessageAsync(uid);

                        // Filtrar correos con al menos 2 archivos adjuntos
                        var attachments = message.Attachments.ToList();
                        if (attachments.Count < 2)
                        {
                            counter++;
                            progressBar1.Value = counter;
                            continue;
                        }

                        // Verificar si hay al menos un PDF y un XML
                        bool hasPdf = attachments.Any(a => a is MimePart pdf && pdf.FileName.EndsWith(".pdf", StringComparison.OrdinalIgnoreCase));
                        bool hasXml = attachments.Any(a => a is MimePart xml && xml.FileName.EndsWith(".xml", StringComparison.OrdinalIgnoreCase));

                        if (hasPdf && hasXml)
                        {
                            // Crear carpeta si no existe
                            Directory.CreateDirectory(savePath);

                            foreach (var attachment in attachments)
                            {

                                if (attachment is MimePart mimePart)
                                {
                                    // Construir la ruta completa donde se guardará el archivo
                                    string filePath = Path.Combine(savePath, mimePart.FileName);

                                    // Verificar si se trata de un XML (ignorando mayúsculas/minúsculas)
                                    if (mimePart.FileName.EndsWith(".xml", StringComparison.OrdinalIgnoreCase))
                                    {
                                        //// Verificar que no se haya procesado ya un XML con el mismo nombre en la lista
                                        //bool yaEnLista = xmlFilePaths.Any(x =>
                                        //    Path.GetFileName(x).Equals(mimePart.FileName, StringComparison.OrdinalIgnoreCase));

                                        //// Verificar también si el archivo ya existe en la carpeta destino
                                        //bool yaEnDisco = File.Exists(filePath);

                                        //if (yaEnLista || yaEnDisco)
                                        //{
                                        //    // Si ya existe un XML con ese nombre, se omite su procesamiento
                                        //    continue;
                                        //}

                                        // Guardar el archivo en disco
                                        using (var stream = File.Create(filePath))
                                        {
                                            mimePart.Content.DecodeTo(stream);
                                        }

                                        // Intentar cargar y validar el contenido del XML
                                        try
                                        {
                                            XDocument xmlDoc = XDocument.Load(filePath);

                                            // Verificar que el nodo raíz sea "FacturaElectronica" usando LocalName para ignorar el namespace
                                            if (xmlDoc.Root != null &&
                                                xmlDoc.Root.Name.LocalName.Equals("FacturaElectronica", StringComparison.OrdinalIgnoreCase))
                                            {
                                                // Es un XML de factura electrónica y no es duplicado, se agrega a la lista
                                                xmlFilePaths.Add(filePath);
                                            }
                                            else
                                            {
                                                // Si el XML no es el esperado, se elimina el archivo
                                                File.Delete(filePath);
                                            }
                                        }
                                        catch (Exception ex)
                                        {
                                            // En caso de error al procesar el XML, se elimina el archivo y se puede registrar el error
                                            File.Delete(filePath);
                                            Console.WriteLine("Error al procesar el XML: " + ex.Message);
                                        }
                                    }
                                    else
                                    {
                                        // Para otros tipos de archivos (por ejemplo, PDF) se procesa normalmente
                                        using (var stream = File.Create(filePath))
                                        {
                                            mimePart.Content.DecodeTo(stream);
                                        }
                                    }
                                }



                            }

                            emailsProcessed++;
                        }

                        // Actualizar el ProgressBar en cada iteración
                        counter++;
                        progressBar1.Value = counter;
                        // Opcional: forzar la actualización de la interfaz
                        progressBar1.Refresh();
                    }
             
                    // Notificar al usuario el resultado del proceso
                    MessageBox.Show(
                        $"Se han procesado {emailsProcessed} correos con PDF y XML adjuntos entre {fechaInicio:dd/MM/yyyy} y {fechaFin.AddDays(-1):dd/MM/yyyy}.",
                        "Proceso completado", MessageBoxButtons.OK, MessageBoxIcon.Information);
               
                    // Mostrar las rutas de los archivos XML guardados
                    if (xmlFilePaths.Count > 0)
                    {
                        string xmlPathsSummary = "Archivos XML descargados:\n" + string.Join("\n", xmlFilePaths);
                        MessageBox.Show(xmlPathsSummary, "Resumen de Archivos XML", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("No se encontraron archivos XML en los correos procesados.", "Sin resultados", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                    // Ocultar el ProgressBar una vez finalizado el proceso (opcional)
                    progressBar1.Visible = false;
                    btnProcesar.Enabled = true;
                    btnCorreo.Enabled = true;
                    btnCargar.Enabled = true;
                    chkCambiarColon.Enabled = true;

                    await client.DisconnectAsync(true);
                }
                cargarDatosCorreo();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnProcesar.Visible = true;
                btnCorreo.Enabled = true;
                btnCargar.Enabled = true;
                chkCambiarColon.Enabled = true;
            }

        }

        private void cargarDatosCorreo()
        {
            foreach (String file in xmlFilePaths)

            {
                // Create a PictureBox.
                try
                {
                    /////
                    ///
                    if (File.Exists(file))
                    {

                        var gasto = Documentos.obtenerCompraXml(file);
                        gasto.tipoDoc = gasto.tipoDoc == (int)Enums.TipoDocumento.NotaCredito ? (int)Enums.TipoDocumento.NotaCreditoGasto : (int)Enums.TipoDocumento.Gastos;
                        foreach (var item in gasto.tbDetalleCompras)
                        {
                            item.utilidad = 0;
                        }

                    

                        listaCompras.Add(gasto);

                      
                    }

                }

                catch (Exception ex)
                {
                    // Could not load the image - probably related to Windows file system permissions.
                    MessageBox.Show("El documento seleccionado no tiene el formato requerido. " + file.Substring(file.LastIndexOf('\\'))
                       );
                }
            }

            cargarLista();
        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void cboEstado_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((int)cboEstado.SelectedValue == 1)
            {
                txtRazon.Enabled = false;
                txtRazon.Text = string.Empty;

            }
            else
            {

                //foreach (var item in listaCompras)
                //{
                //    if (item.claveDocEmisor.Trim() == txtClave.Text.Trim())
                //    {
                //        txtRazon.Text = item.razon;
                //        break;

                //    }
                //}
                txtRazon.Enabled = true;

            }

        }
    }
}
