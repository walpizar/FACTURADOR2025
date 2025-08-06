using BusinessLayer;
using CommonLayer;
using CommonLayer.DTO;
using EntityLayer;
using PresentationLayer.Clases;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PresentationLayer
{
    public partial class frmValidacionDocsHacienda : Form
    {
        BFacturacion facturaIns = new BFacturacion();
        Bcliente clienteIns = new Bcliente();

        IEnumerable<tbReporteHacienda> mensajesLista = new List<tbReporteHacienda>();
        IEnumerable<DocumentoHaciendaDTO> facturasLista = new List<DocumentoHaciendaDTO>();
        IEnumerable<tbCompras> comprasLista = new List<tbCompras>();

        public frmValidacionDocsHacienda()

        {
            InitializeComponent();
        }

        private void frmValidacionMensajesComprasHacienda_Load(object sender, EventArgs e)
        {
            progressBar1.Visible = false;
            formatoGrid();
            cargarDatos();
        }
        private void cargarListas()
        {
            try
            {
                //carga dacumentos emitidos
                facturasLista = (IEnumerable<DocumentoHaciendaDTO>)facturaIns.getListaValidacion(dtpFechaInicio.Value.Date, dtpFechaFin.Value.Date);
               // facturasLista = (IEnumerable<tbDocumento>)facturaIns.listaFacturasFechas(dtpFechaInicio.Value.Date, dtpFechaFin.Value.Date);
     
                ////carga mensajes de compras enviados
                //mensajesLista = facturaIns.listaMensajesCompras();

                ////carga compras simplificadas
                //comprasLista = facturaIns.listaComprasSimplificada();
                //comprasLista = comprasLista.Where(x => x.reporteElectronico == true).ToList();

            }
            catch (Exception ex)
            {

                throw;
            }


        }
        private void cargarDatos()
        {
            try
            {
                //(p.EstadoFacturaHacienda == null || p.EstadoFacturaHacienda == "procesando" || p.EstadoFacturaHacienda == "rechazado")

                cargarListas();


                if (chkInconsistentes.Checked)
                {

                    if (mensajesLista.Count() != 0)
                    {
                        mensajesLista = mensajesLista.Where(x => x.EstadoRespHacienda == null || x.EstadoRespHacienda.Trim().ToUpper() == "RECHAZADO" || x.EstadoRespHacienda.Trim().ToUpper() == "PROCESANDO" || x.reporteAceptaHacienda == false || x.mensajeRespHacienda == false);


                    }


                    if (facturasLista.Count() != 0)
                    {
                        facturasLista = facturasLista.Where(x => x.EstadoFacturaHacienda == null || x.EstadoFacturaHacienda.Trim().ToUpper() == "RECHAZADO" || x.EstadoFacturaHacienda.Trim().ToUpper() == "PROCESANDO" || x.reporteAceptaHacienda == false || x.mensajeRespHacienda == false);


                    }

                    if (comprasLista.Count() != 0)
                    {
                        comprasLista = comprasLista.Where(x => x.EstadoFacturaHacienda == null || x.EstadoFacturaHacienda.Trim().ToUpper() == "RECHAZADO" || x.EstadoFacturaHacienda.Trim().ToUpper() == "PROCESANDO" || x.reporteAceptaHacienda == false || x.mensajeRespHacienda == false);


                    }


                }
                else
                {

                    mensajesLista = mensajesLista.Where(x => x.reporteAceptaHacienda == !chkInconsistentes.Checked && x.mensajeRespHacienda == !chkInconsistentes.Checked && x.EstadoRespHacienda.Trim().ToUpper() == "ACEPTADO");
                    facturasLista = facturasLista.Where(x => x.reporteAceptaHacienda == !chkInconsistentes.Checked && x.mensajeRespHacienda == !chkInconsistentes.Checked && x.EstadoFacturaHacienda.Trim().ToUpper() == "ACEPTADO");
                    comprasLista = comprasLista.Where(x => x.reporteAceptaHacienda == !chkInconsistentes.Checked && x.mensajeRespHacienda == !chkInconsistentes.Checked && x.EstadoFacturaHacienda.Trim().ToUpper() == "ACEPTADO");

                }







                if (chkFechas.Checked)
                {
                    if (dtpFechaInicio.Value.Date > dtpFechaFin.Value.Date)
                    {
                        MessageBox.Show("La fecha de inicio no puede ser mayor que la fecha final", "Datos fechas", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        chkFechas.Checked = false;
                    }
                    else
                    {
                        DateTime fechaInicio = dtpFechaInicio.Value.Date;
                        DateTime fechaFin = dtpFechaFin.Value.Date.AddDays(1);

                        mensajesLista = mensajesLista.Where(x => x.fecha >= fechaInicio && x.fecha <= fechaFin);
                        facturasLista = facturasLista.Where(x => x.fecha >= fechaInicio && x.fecha <= fechaFin);
                        comprasLista = comprasLista.Where(x => x.fecha >= fechaInicio && x.fecha <= fechaFin);

                    }

                }


                if (txtIdRecept.Text != string.Empty)
                {
                    mensajesLista = mensajesLista.Where(x => x.id.ToString() == txtIdRecept.Text.Trim());

                }

                if (txtClaveRecept.Text != string.Empty)
                {
                    mensajesLista = mensajesLista.Where(x => x.claveReceptor == txtClaveRecept.Text.Trim());
                    facturasLista = facturasLista.Where(x => x.clave == txtClaveRecept.Text.Trim());
                    comprasLista = comprasLista.Where(x => x.clave == txtClaveRecept.Text.Trim());
                }


                if (txtConseRecept.Text != string.Empty)
                {
                    mensajesLista = mensajesLista.Where(x => x.consecutivoReceptor == txtConseRecept.Text.Trim());
                    facturasLista = facturasLista.Where(x => x.consecutivo == txtConseRecept.Text.Trim());
                    comprasLista = comprasLista.Where(x => x.consecutivo == txtConseRecept.Text.Trim());

                }


                if (txtIdEmisor.Text != string.Empty)
                {
                    mensajesLista = mensajesLista.Where(x => x.idEmisor == txtIdEmisor.Text.Trim());
                    facturasLista = facturasLista.Where(x => x.idCliente.ToString() == txtIdRecept.Text.Trim());
                    comprasLista = comprasLista.Where(x => x.idProveedor.ToString() == txtIdRecept.Text.Trim());
                }

                if (txtClaveEmisor.Text != string.Empty)
                {
                    mensajesLista = mensajesLista.Where(x => x.claveDocEmisor == txtClaveEmisor.Text.Trim());


                }

                if (txtNombreEmisor.Text != string.Empty)
                {
                    mensajesLista = mensajesLista.Where(x => x.nombreEmisor.Trim().ToUpper().Contains(txtNombreEmisor.Text.Trim().ToUpper()));


                }

                if (txtArchivo.Text != string.Empty)
                {
                    mensajesLista = mensajesLista.Where(x => x.nombreArchivo.Contains(txtArchivo.Text.Trim()));


                }

                cargarGRID(mensajesLista, facturasLista, comprasLista);
            }
            catch (Exception ex)
            {

                MessageBox.Show("Ocurrio un error, intente de nuevo" + ex.Message + "-" + ex.InnerException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void cargarGRID(IEnumerable<tbReporteHacienda> mensajesLista, IEnumerable<DocumentoHaciendaDTO> facturasLista, IEnumerable<tbCompras> comprasLista)
        {
            try
            {
                dtgvDetalleFactura.Rows.Clear();

                foreach (var doc in facturasLista)
                {

                    DataGridViewRow row = (DataGridViewRow)dtgvDetalleFactura.Rows[0].Clone();
                    row.Cells[0].Value = doc.id;
                    row.Cells[1].Value = Enum.GetName(typeof(Enums.TipoDocumento), doc.tipoDocumento).ToUpper();

                    row.Cells[2].Value = doc.fecha.ToString().Trim();

                    row.Cells[3].Value = doc.mensajeReporteHacienda == null ? "SIN ENVIAR" : doc.mensajeReporteHacienda.Trim().ToUpper();
                    row.Cells[4].Value = doc.EstadoFacturaHacienda == null ? "SIN ENVIAR" : doc.EstadoFacturaHacienda.Trim().ToUpper();
                    row.Cells[5].Value = "Ver detalle";
                    row.Cells[6].Value = "Enviar Correo";

                    row.Cells[7].Value = doc.mensajeReporteHacienda == null ? "SIN VALIDAR" : doc.mensajeReporteHacienda.Trim().ToUpper();

                    row.Cells[8].Value = doc.tipoDocumento;
                    dtgvDetalleFactura.Rows.Add(row);
                    // dtgvDetalleFactura.Rows[listaDetalleDocumento.Count-1].Selected=true;
                }

                foreach (var doc in mensajesLista)
                {

                    DataGridViewRow row = (DataGridViewRow)dtgvDetalleFactura.Rows[0].Clone();
                    row.Cells[0].Value = doc.id;
                    row.Cells[1].Value = Enum.GetName(typeof(Enums.TipoDocumento), Enums.TipoDocumento.Compras).ToUpper();
                    row.Cells[2].Value = doc.fecha.ToString().Trim();

                    row.Cells[3].Value = doc.mensajeReporteHacienda == null ? "SIN ENVIAR" : doc.mensajeReporteHacienda.Trim().ToUpper();
                    row.Cells[4].Value = doc.EstadoRespHacienda == null ? "SIN ENVIAR" : doc.EstadoRespHacienda.Trim().ToUpper();
                    row.Cells[5].Value = "Ver detalle";
                    row.Cells[6].Value = "Enviar Correo";

                    row.Cells[7].Value = doc.EstadoRespHacienda == null ? "SIN VALIDAR" : doc.EstadoRespHacienda.Trim().ToUpper();

                    row.Cells[8].Value = (int)Enums.TipoDocumento.Compras;
                    dtgvDetalleFactura.Rows.Add(row);

                    // dtgvDetalleFactura.Rows[listaDetalleDocumento.Count-1].Selected=true;
                }


                foreach (var doc in comprasLista)
                {

                    DataGridViewRow row = (DataGridViewRow)dtgvDetalleFactura.Rows[0].Clone();
                    row.Cells[0].Value = doc.id;
                    row.Cells[1].Value = Enum.GetName(typeof(Enums.TipoDocumento), doc.tipoDoc).ToUpper();
                    row.Cells[2].Value = doc.fecha.ToString().Trim();


                    row.Cells[3].Value = doc.mensajeReporteHacienda == null ? "SIN ENVIAR" : doc.mensajeReporteHacienda.Trim().ToUpper();
                    row.Cells[4].Value = doc.EstadoFacturaHacienda == null ? "SIN ENVIAR" : doc.EstadoFacturaHacienda.Trim().ToUpper();
                    row.Cells[5].Value = "Ver detalle";


                    row.Cells[7].Value = doc.mensajeReporteHacienda == null ? "SIN VALIDAR" : doc.mensajeReporteHacienda.Trim().ToUpper();
                    row.Cells[8].Value = doc.tipoDoc;
                    dtgvDetalleFactura.Rows.Add(row);
                    // dtgvDetalleFactura.Rows[listaDetalleDocumento.Count-1].Selected=true;
                }


            }
            catch (Exception)
            {

                MessageBox.Show("Ocurrio un error, intente de nuevo", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }






        }

        private void formatoGrid()
        {
            dtgvDetalleFactura.BorderStyle = BorderStyle.None;
            dtgvDetalleFactura.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            dtgvDetalleFactura.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dtgvDetalleFactura.DefaultCellStyle.SelectionBackColor = Color.OrangeRed;
            dtgvDetalleFactura.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dtgvDetalleFactura.BackgroundColor = Color.White;



            dtgvDetalleFactura.EnableHeadersVisualStyles = false;
            dtgvDetalleFactura.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dtgvDetalleFactura.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
            dtgvDetalleFactura.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            //  dtgvDetalleFactura.DefaultCellStyle.Font = new Font("Microsoft Sans Serif", 16);


        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            cargarDatos();
        }

        private void btnRefrescar_Click(object sender, EventArgs e)
        {
            cargarDatos();
        }

        private void dtgvDetalleFactura_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {


            try
            {
                int id = int.Parse(dtgvDetalleFactura.Rows[e.RowIndex].Cells[0].Value.ToString());
                string tipoDoc = dtgvDetalleFactura.Rows[e.RowIndex].Cells[1].Value.ToString();
                int tipoDocumento = int.Parse(dtgvDetalleFactura.Rows[e.RowIndex].Cells[8].Value.ToString());


                if (e.ColumnIndex == 5)
                {
                    try
                    {
                        if (tipoDoc == Enum.GetName(typeof(Enums.TipoDocumento), Enums.TipoDocumento.Compras).ToUpper())
                        {
                            frmDetalleMensaje msj = new frmDetalleMensaje();

                            foreach (var item in mensajesLista)
                            {
                                if (item.id == id)
                                {
                                    msj.Reporte = item;
                                    break;
                                }

                            }
                            msj.ShowDialog();

                        }
                        else if (tipoDoc == Enum.GetName(typeof(Enums.TipoDocumento), Enums.TipoDocumento.FacturaElectronica).ToUpper() ||
                            tipoDoc == Enum.GetName(typeof(Enums.TipoDocumento), Enums.TipoDocumento.NotaCreditoElectronica).ToUpper() ||
                            tipoDoc == Enum.GetName(typeof(Enums.TipoDocumento), Enums.TipoDocumento.NotaDebitoElectronica).ToUpper() ||
                            tipoDoc == Enum.GetName(typeof(Enums.TipoDocumento), Enums.TipoDocumento.TiqueteElectronico).ToUpper())
                        {

                            var doc = facturasLista.Where(x => x.id == id && x.tipoDocumento == tipoDocumento).SingleOrDefault();
                            if (doc != null)
                            {
                                var documento = facturaIns.getEntityByKey(doc.id, doc.tipoDocumento);
                                frmDocumentosDetalle form = new frmDocumentosDetalle(documento);
                                form.ShowDialog();
                            }


                        }
                    }
                    catch (Exception ex)
                    {

                        MessageBox.Show("Ocurrio un error obtener entidad, intente de nuevo." + ex.Message + "-" + ex.InnerException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }

                }
                else if (e.ColumnIndex == 6)
                {
                    if (tipoDoc == Enum.GetName(typeof(Enums.TipoDocumento), Enums.TipoDocumento.Compras).ToUpper())
                    {
                        reportarMensajeElectronica(id);

                    }
                    else if (tipoDoc == Enum.GetName(typeof(Enums.TipoDocumento), Enums.TipoDocumento.FacturaElectronica).ToUpper() ||
                       tipoDoc == Enum.GetName(typeof(Enums.TipoDocumento), Enums.TipoDocumento.NotaCreditoElectronica).ToUpper() ||
                       tipoDoc == Enum.GetName(typeof(Enums.TipoDocumento), Enums.TipoDocumento.NotaDebitoElectronica).ToUpper() ||
                       tipoDoc == Enum.GetName(typeof(Enums.TipoDocumento), Enums.TipoDocumento.TiqueteElectronico).ToUpper())
                    {
                        enviarCorreoCorreoDocumentoElectronico(id);


                    }

                }
                else if (e.ColumnIndex == 7)
                {
                    try
                    {
                        if (Utility.AccesoInternet())
                        {
                            if (tipoDoc == Enum.GetName(typeof(Enums.TipoDocumento), Enums.TipoDocumento.Compras).ToUpper())
                            {
                                facturaIns.consultarMensajePorIdFact(id);
                                cargarDatos();

                            }
                            else if (tipoDoc == Enum.GetName(typeof(Enums.TipoDocumento), Enums.TipoDocumento.FacturaElectronica).ToUpper() ||
                               tipoDoc == Enum.GetName(typeof(Enums.TipoDocumento), Enums.TipoDocumento.NotaCreditoElectronica).ToUpper() ||
                               tipoDoc == Enum.GetName(typeof(Enums.TipoDocumento), Enums.TipoDocumento.NotaDebitoElectronica).ToUpper() ||
                               tipoDoc == Enum.GetName(typeof(Enums.TipoDocumento), Enums.TipoDocumento.TiqueteElectronico).ToUpper())
                            {

                                var doc = facturasLista.Where(x => x.id == id && x.tipoDocumento == tipoDocumento).SingleOrDefault();
                                facturaIns.consultarFacturaElectronicaPorClave(doc.clave);


                            }
                            else if (tipoDoc == Enum.GetName(typeof(Enums.TipoDocumento), Enums.TipoDocumento.ComprasSimplificada).ToUpper())
                            {
                                tbCompras doc = comprasLista.Where(x => x.id == id && x.tipoDoc == (int)Enums.TipoDocumento.ComprasSimplificada).SingleOrDefault();
                                //facturaIns.consultarCompraSimplificada(doc);

                            }




                        }
                        else
                        {
                            MessageBox.Show("No hay acceso a internet, no se validarán los documentos", "Sin Internet", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    catch (Exception ex)
                    {

                        MessageBox.Show("Ocurrio un error obtener entidad, intente de nuevo." + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }

                }
                cargarDatos();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Ocurrio un error, intente de nuevo." + ex.Message + "-" + ex.InnerException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }

        private void enviarCorreoCorreoDocumentoElectronico(int id)
        {

            if (Utility.AccesoInternet())
            {
                tbDocumento doc = new tbDocumento();
                doc.id = id;
                doc.tipoDocumento = (int)Enums.TipoDocumento.FacturaElectronica;
                doc = facturaIns.getEntity(doc);
                if (doc != null)
                {
                    DialogResult result = MessageBox.Show("Se enviará por correo electrónico la factura seleccionada, Desea continuar?", "Envio de correo electrónico", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {

                        List<string> correos = new List<string>();


                        if (doc.correo1 != null || doc.correo2 != null)
                        {
                            if (doc.correo1 != null)
                            {
                                correos.Add(doc.correo1);
                            }
                            if (doc.correo2 != null)
                            {
                                correos.Add(doc.correo2);
                            }

                        }
                        else
                        {
                            if (doc.tbClientes.tbPersona.correoElectronico != null)
                            {
                                correos.Add(doc.tbClientes.tbPersona.correoElectronico.Trim());

                            }
                        }

                        if (correos.Count != 0)
                        {
                            enviarCorreo(doc, correos);
                        }
                        else
                        {
                            MessageBox.Show("No hay correo electronicos registrados con este documento", "Correo Electrónico", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        }


                    }
                }

            }
            else
            {
                MessageBox.Show("No hay acceso a internet", "Sin Internet", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void enviarCorreo(tbDocumento doc, List<string> correos)
        {
            try
            {
                bool enviado = false;
                //se solicita respuesta, y se confecciona el correo a enviar

                clsDocumentoCorreo docCorreo = new clsDocumentoCorreo(doc, correos, true, (int)Enums.tipoAdjunto.factura);
                CorreoElectronico.enviarCorreo(docCorreo);

                if (enviado)
                {
                    MessageBox.Show("Se envió correctamente el correo electrónico", "Correo Electrónico", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                else
                {
                    MessageBox.Show("Se produjo un error al enviar el Correo Electrónico", "Correo Electrónico", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }

            }
            catch (Exception)
            {

                MessageBox.Show("Se produjo un error al enviar el Correo Electrónico", "Correo Electrónico", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void reportarMensajeElectronica(int id)
        {
            try
            {

                tbReporteHacienda resp = mensajesLista.Where(x => x.id == id).SingleOrDefault();
                List<tbReporteHacienda> lista = new List<tbReporteHacienda>();
                lista.Add(resp);


                if (resp.reporteAceptaHacienda == false || resp.mensajeRespHacienda == false)
                {
                    facturaIns.reportarMensajesHacienda(lista);
                }



                enviarCorreo(lista);

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

                    clsDocumentoCorreo docCorreo = new clsDocumentoCorreo(item, correos, true, (int)Enums.tipoAdjunto.factura);
                    CorreoElectronico.enviarCorreo(docCorreo);
                }
                MessageBox.Show("Se han enviado los correos electrónicos.", "Envio de correos electrónicos", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception)
            {

                MessageBox.Show("No se pudo enviar los correos, favor de verificar los datos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private Task ProcessData(IProgress<ProgressReport> process)
        {

            int index = 1;

            var ProgressReport = new ProgressReport();

            return Task.Run(async () =>
            {

                try
                {
                    facturasLista = facturasLista.Where(x => x.EstadoFacturaHacienda == null || x.EstadoFacturaHacienda.Trim().ToUpper() == "PROCESANDO" || x.reporteAceptaHacienda == false || x.mensajeRespHacienda == false);
                    comprasLista = comprasLista.Where(x => x.EstadoFacturaHacienda == null || x.EstadoFacturaHacienda.Trim().ToUpper() == "PROCESANDO" || x.reporteAceptaHacienda == false || x.mensajeRespHacienda == false);
                    mensajesLista = mensajesLista.Where(x => x.EstadoRespHacienda == null || x.EstadoRespHacienda.Trim().ToUpper() == "PROCESANDO" || x.reporteAceptaHacienda == false || x.mensajeRespHacienda == false);

                    int totalProcess = facturasLista.Count() + mensajesLista.Count() + comprasLista.Count();

                    foreach (var doc in facturasLista)
                    {

                       // System.Threading.Thread.Sleep(3000);
                        ProgressReport.PorcentComplete = index * 100 / totalProcess;
                        process.Report(ProgressReport);

                        try
                        {
                            if (!doc.reporteAceptaHacienda)
                            {
                                var documento = facturaIns.getEntityByKey(doc.id, doc.tipoDocumento);
                                facturaIns.FacturarElectronicamente(documento);

                            }
                            else
                            {
                                facturaIns.consultarFacturaElectronicaPorClave(doc.clave);
                            }



                        }
                        catch (Exception ex)
                        {


                        }
                        index++;

                    }

                    foreach (var doc in comprasLista)
                    {
                        ProgressReport.PorcentComplete = index * 100 / totalProcess;
                        process.Report(ProgressReport);

                        try
                        {

                            //facturaIns.consultarCompraSimplificada(doc);

                        }
                        catch (Exception ex)
                        {

                        }
                        index++;
                    }

                    foreach (var doc in mensajesLista)
                    {
                        ProgressReport.PorcentComplete = index * 100 / totalProcess;
                        process.Report(ProgressReport);

                        try
                        {

                            facturaIns.consultarMensajePorIdFact(doc.id);

                        }
                        catch (Exception ex)
                        {


                        }
                        index++;
                    }
                }
                catch (Exception ex)
                {

                  
                }

            });

        }


        private async void btnValidarTodos_Click(object sender, EventArgs e)
        {


            if (Utility.AccesoInternet())
            {
                progressBar1.Visible = true;
                try
                {

                    var progress = new System.Progress<ProgressReport>();
                    progress.ProgressChanged += (o, report) =>
                    {

                        progressBar1.Value = report.PorcentComplete;
                        progressBar1.Update();
                    };


                    await ProcessData(progress);


                }
                catch (Exception)
                {

                }

                cargarDatos();
                progressBar1.Visible = false;


            }
            else
            {
                MessageBox.Show("No hay acceso a internet, no se validarán los documentos", "Sin Internet", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            progressBar1.Visible = false;

        }

        private void chkFechas_CheckedChanged(object sender, EventArgs e)
        {

            gbxFechas.Enabled = chkFechas.Checked;

        }

        private void btnsalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
