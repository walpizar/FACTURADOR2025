using BusinessLayer;
using CommonLayer;
using CommonLayer.Logs;
using EntityLayer;
using PresentationLayer.Clases;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;


namespace PresentationLayer
{
    public partial class frmDocumentosDetalle : Form
    {
        private tbDocumento _doc;
        BFacturacion facturaIns = new BFacturacion();

        clsDetalleNC _detalleNC;


        public frmDocumentosDetalle()
        {
            InitializeComponent();
        }

        public frmDocumentosDetalle(tbDocumento doc)
        {
            InitializeComponent();
            _doc = doc;
        }

        private void frmFacturaDeshabilitada_Load(object sender, EventArgs e)
        {

            if (_doc.estado == false)
            {
                btnCancelarFactura.Enabled = false;
                btnEnviarCorreo.Enabled = false;
            }
            chkDetalleProforma.Visible = _doc.tipoDocumento == (int)Enums.TipoDocumento.Proforma || _doc.tipoDocumento == (int)Enums.TipoDocumento.ProformaGeneral;
            lblTipoDoc.Text = Enum.GetName(typeof(Enums.TipoDocumento), _doc.tipoDocumento).ToUpper();
            cargarForm();
            cargarTotales();
            cargarOpciones();




        }
        private void cargarOpciones()
        {





            btnReImprimir.Visible = Global.Configuracion.imprime == (int)Enums.EstadoConfig.Si;


            bool factElect = (bool)Global.Usuario.tbEmpresa.tbParametrosEmpresa.First().facturacionElectronica;
            if (!factElect)
            {

                likClave.Enabled = false;
                lblEstadoHacienda.Visible = false;

                txtEstadoHacienda.Visible = false;
            }
            if (Global.Usuario.idRol == (int)Enums.roles.facturador)
            {
                btnCancelarFactura.Enabled = false;
                btnAnularDocumento.Enabled = false;
            }
            else
            {
                bool permiteNC = _doc.tipoDocumento == (int)Enums.TipoDocumento.Factura || _doc.tipoDocumento == (int)Enums.TipoDocumento.FacturaElectronica || _doc.tipoDocumento == (int)Enums.TipoDocumento.TiqueteElectronico;
                btnCancelarFactura.Enabled = permiteNC;
                btnAnularDocumento.Enabled = permiteNC;
                List<tbDocumento> NC = null;
                if (permiteNC)
                {
                    NC = (List<tbDocumento>)facturaIns.getNCByRef(_doc.clave);

                }
                decimal contadorArtNC = 0, contadorArt = 0;
                if (permiteNC)
                {
                    foreach (var item in _doc.tbDetalleDocumento)
                    {
                        contadorArt += item.cantidad;


                    }

                    foreach (var item in NC)
                    {
                        contadorArtNC += item.tbDetalleDocumento.Sum(x => x.cantidad);


                    }
                    if (contadorArt == contadorArtNC)
                    {
                        btnCancelarFactura.Enabled = false;
                        btnAnularDocumento.Enabled = false;

                    }
                    if (contadorArtNC != 0)
                    {
                        btnCancelarFactura.Enabled = false;
                    }

                }


            }
            if (Global.Usuario.idRol == (int)Enums.roles.facturadorSuperMas)
            {
                btnCancelarFactura.Enabled = false;
                btnAnularDocumento.Enabled = false;
            }

        }

        private void cargarTotales()
        {

            decimal total = 0, desc = 0, iva = 0, subtotal = 0, exo = 0;

            foreach (tbDetalleDocumento detalle in _doc.tbDetalleDocumento)
            {
                detalle.totalLinea = (detalle.montoTotal - detalle.montoTotalDesc) + detalle.montoTotalImp - detalle.montoTotalExo;
                total += detalle.totalLinea;
                desc += detalle.montoTotalDesc;
                iva += detalle.montoTotalImp;
                exo += detalle.montoTotalExo;
                subtotal += detalle.montoTotal;

            }
            txtSubtotal.Text = string.Format("{0:N2}", subtotal);
            txtDescuento.Text = desc == 0 ? "0.00" : string.Format("{0:N2}", desc);
            txtIva.Text = iva == 0 ? "0.00" : string.Format("{0:N2}", iva);
            txtTotal.Text = total == 0 ? "0.00" : string.Format("{0:N2}", total);
            txtExoneracion.Text = exo == 0 ? "0.00" : string.Format("{0:N2}", exo);

        }

        private void cargarForm()
        {
            lblHecha.Text = _doc.usuario_crea;
            txtIdFactura.Text = _doc.id.ToString().Trim();
            likClave.Text = _doc.clave == null ? string.Empty : _doc.clave.ToString().Trim();
            txtConsecutivo.Text = _doc.consecutivo == null ? string.Empty : _doc.consecutivo.ToString().Trim();
            txtFecha.Text = _doc.fecha.ToString().Trim();
            if (_doc.tipoDocumento == (int)Enums.TipoDocumento.Proforma || _doc.tipoVenta == (int)Enums.tipoVenta.Credito
                && _doc.plazo != 0)
            {
                DateTime fechaVence = _doc.fecha.Date.AddDays((double)_doc.plazo);
                txtFechaVence.Text = fechaVence.ToString();
                if (Utility.getDate() > fechaVence)
                {
                    txtFechaVence.ForeColor = Color.Red;

                }
            }



            txtTipoPago.Text = _doc.tipoPago == null ? string.Empty : Enum.GetName(typeof(Enums.TipoPago), _doc.tipoPago).ToUpper();
            txtTipoVenta.Text = _doc.tipoVenta == null ? string.Empty : Enum.GetName(typeof(Enums.tipoVenta), _doc.tipoVenta).ToUpper();
            txtEstado.Text = Enum.GetName(typeof(Enums.Estado), _doc.estado).ToUpper();
            txtEstadoFact.Text = Enum.GetName(typeof(Enums.EstadoFactura), _doc.estadoFactura).ToUpper();
            txtClaveRef.Text = _doc.claveRef;
            chkEnvioCorreo.Checked = _doc.notificarCorreo;

            if (_doc.notificarCorreo)
            {
                lblEstadoCorreo.Visible = true;
                txtEstadoCorreo.Visible = true;
                if (_doc.estadoCorreo==0 )
                {
                    txtEstadoCorreo.Text = "PENDIENTE";
                }
                else
                {
                    txtEstadoCorreo.Text = "ENVIADO";
                }


            }
            else
            {
                lblEstadoCorreo.Visible = false;
                txtEstadoCorreo.Visible = false;
            }
            
            
            if (_doc.EstadoFacturaHacienda != null)
            {
                txtEstadoHacienda.Text = _doc.EstadoFacturaHacienda.ToUpper().Trim();
            }
            else
            {
                txtEstadoHacienda.Text = "SIN ESTADO";
            }



            if (_doc.EstadoFacturaHacienda != null)
            {
                txtEstadoHacienda.Text = _doc.EstadoFacturaHacienda.ToUpper().Trim();
            }
            else
            {
                txtEstadoHacienda.Text = "SIN ESTADO";
            }
            if (_doc.tipoDocumento == (int)Enums.TipoDocumento.FacturaElectronica)
            {
                txtObser.Text = _doc.observaciones;
            }
            else
            {
                txtObser.Text = _doc.razon;
            }

            if (_doc.observaciones != null)
            {
                txtObser.Text = _doc.observaciones;
            }

            if (_doc.razon != null)
            {
                txtObser.Text = _doc.razon;
            }


            if (_doc.tbClientes == null)
            {
                if (_doc.clienteNombre == null)
                {
                    txtIdCliente.Text = "SIN CLIENTE";
                    txtCliente.Text = "SIN CLIENTE";

                }
                else
                {
                    txtCliente.Text = _doc.clienteNombre.Trim().ToUpper();
                    txtIdCliente.Text = "SIN CLIENTE";
                }
                chkEnviarCorreo.Checked = false;
            }
            else if (_doc.tbClientes.tbPersona.tipoId == (int)Enums.TipoId.Fisica)
            {
                txtIdCliente.Text = _doc.tbClientes.tbPersona.identificacion.ToString().Trim();
                txtCliente.Text = _doc.tbClientes.tbPersona.nombre.ToUpper().ToString().Trim() + " " +
                   _doc.tbClientes.tbPersona.apellido1.ToUpper().ToString().Trim() + " " + _doc.tbClientes.tbPersona.apellido2.ToUpper().ToString().Trim();
            }
            else
            {
                txtIdCliente.Text = _doc.tbClientes.tbPersona.identificacion.ToString().Trim();
                txtCliente.Text = _doc.tbClientes.tbPersona.nombre.ToUpper().ToString().Trim();
            }
            if (_doc.tbClientes != null)
            {
                txtTel.Text = _doc.tbClientes.tbPersona.telefono.ToString().Trim();


            }
            txtCorreo.Text = _doc.correo1 == null ? _doc.tbClientes != null ? _doc.tbClientes.tbPersona.correoElectronico.Trim() : string.Empty : _doc.correo1.ToString().Trim();


            txtCorreo2.Text = _doc.correo2 == null ? string.Empty : _doc.correo2.ToString().Trim();

            chkEnviarCorreo.Checked = _doc.notificarCorreo;
            lsvFacturas.Items.Clear();
            _doc.tbDetalleDocumento = _doc.tbDetalleDocumento.OrderBy(x => x.numLinea).ToList();
            foreach (tbDetalleDocumento item in _doc.tbDetalleDocumento)
            {

                //Creamos un objeto de tipo ListviewItem
                ListViewItem linea = new ListViewItem();

                linea.Text = item.numLinea.ToString().ToUpper().Trim();
                linea.SubItems.Add(string.Format("{0:N2}", item.tbProducto.nombre.ToUpper().Trim()));
                linea.SubItems.Add(string.Format("{0:N2}", item.precio));
                linea.SubItems.Add(string.Format("{0:N3}", item.cantidad));
                linea.SubItems.Add(string.Format("{0:N2}", item.montoTotal));
                linea.SubItems.Add(string.Format("{0:N2}", item.montoTotalDesc));
                linea.SubItems.Add(string.Format("{0:N2}", item.montoTotalImp));
                linea.SubItems.Add(string.Format("{0:N2}", item.montoTotalExo));
                linea.SubItems.Add(string.Format("{0:N2}", item.totalLinea));
                //Agregamos el item a la lista.
                lsvFacturas.Items.Add(linea);
            }
        }

        private void btnCancelarFactura_Click(object sender, EventArgs e)
        {
            bool bandera = true;
            try
            {
                DialogResult result = MessageBox.Show("Se generará una NOTA DE CRÉDITO, para anular el documento seleccionado, Desea continuar?", "Anulación de factura", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    if (chkEnviarCorreo.Checked)
                    {

                        bandera = validarCampos();

                    }
                    if (bandera)
                    {
                        _doc.correo1 = txtCorreo.Text.Trim();
                        if (txtCorreo2.Text != String.Empty)
                        {
                            _doc.correo1 = txtCorreo2.Text.Trim();
                        }
                        frmRazon razon = new frmRazon();
                        razon._doc = _doc;
                        razon.pasarDatosEvent += dataBuscar;
                        razon.ShowDialog();
                        if (_detalleNC != null)
                        {
                            var doc = eliminarFactura();
                            _doc = doc;

                            MessageBox.Show("Se ha generado la Nota de Crédito Correctamente.", "Guardar Nota de Crédito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            if (doc != null & doc.reporteElectronic == true)
                            {
                                BackgroundWorker tarea = new BackgroundWorker();

                                tarea.DoWork += reportarFacturacionElectronica;
                                tarea.RunWorkerAsync();


                            }
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Debe indicar la razon de la nota de crédito", "Nota de Crédito", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }

                    }

                }


            }
            catch (Exception)
            {

                MessageBox.Show("No se pudo eliminar la factura, intente más tarde, no se generó la NOTA DE CRÉDITO", "Error al eliminar el documento", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }

        private void reportarFacturacionElectronica(object sender, DoWorkEventArgs e)
        {
            if (Utility.AccesoInternet())
            {
                try
                {
                    _doc = facturaIns.FacturarElectronicamente(_doc);

                    System.Threading.Thread.Sleep(3000);

                    facturaIns.consultarFacturaElectronicaPorClave(_doc.clave);

                    //envio correo electrónico
                    if (_doc.notificarCorreo)
                    {
                        List<string> correos = new List<string>();


                        if (_doc.correo1 != null && _doc.correo1.Trim() != string.Empty)
                        {
                            correos.Add(_doc.correo1);

                        }

                        if (_doc.correo2 != null && _doc.correo2.Trim() != string.Empty)
                        {
                            correos.Add(_doc.correo2);

                        }

                        if (correos.Count != 0)
                        {
                            enviarCorreo(_doc, correos);
                        }

                    }
                }
                catch (Exception ex)
                {

                    MessageBox.Show("No se pudo emitir el documento NOTA CREDITO", "Error al procesar", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
            else
            {
                MessageBox.Show("No hay acceso a internet, No se enviará el documento a Hacienda, validar en la pantalla de validación, para su envio correspodiente.", "Sin Internet", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void dataBuscar(clsDetalleNC detalle)
        {
            _detalleNC = detalle;
        }

        private tbDocumento eliminarFactura()
        {
            try
            {
                tbDocumento notaCredito = new tbDocumento();

                notaCredito.correo1 = _doc.correo1;
                notaCredito.correo2 = _doc.correo2;
                notaCredito.estado = true;
                notaCredito.estadoFactura = (int)Enums.EstadoFactura.Cancelada;
                notaCredito.fecha = Utility.getDate();
                notaCredito.fecha_crea = Utility.getDate();
                notaCredito.fecha_ult_mod = Utility.getDate();
                notaCredito.idCliente = _doc.idCliente;
                notaCredito.reporteAceptaHacienda = false;
                notaCredito.idEmpresa = _doc.idEmpresa;
                notaCredito.reporteElectronic = _doc.reporteElectronic;
                notaCredito.tipoVenta = _doc.tipoVenta;
                notaCredito.tipoPago = _doc.tipoPago;
                notaCredito.tipoMoneda = _doc.tipoMoneda;
                notaCredito.tipoCambio = (int)_doc.tipoCambio;


                notaCredito.sucursal = _doc.sucursal;
                notaCredito.caja = _doc.caja;


                notaCredito.codigoActividad = _doc.codigoActividad;

                notaCredito.plazo = (int)_doc.plazo;

                if (_doc.tipoDocumento == (int)Enums.TipoDocumento.Factura && !_doc.reporteElectronic)
                {
                    notaCredito.tipoDocumento = (int)Enums.TipoDocumento.NotaCredito;

                }
                else
                {
                    notaCredito.tipoDocumento = (int)Enums.TipoDocumento.NotaCreditoElectronica;

                }

                notaCredito.tipoIdCliente = _doc.tipoIdCliente;
                notaCredito.tipoIdEmpresa = _doc.tipoIdEmpresa;
                notaCredito.usuario_crea = _doc.usuario_crea;
                notaCredito.usuario_ult_mod = Global.Usuario.nombreUsuario.Trim().ToUpper();

                //datos de nota de credito
                notaCredito.tipoDocRef = _doc.tipoDocumento;
                notaCredito.claveRef = _doc.clave;
                notaCredito.fechaRef = _doc.fecha;
                notaCredito.codigoRef = (int)Enums.TipoRef.AnulaDocumentoReferencia;

                notaCredito.razon = _detalleNC.razon.Trim().ToUpper();
                notaCredito.observaciones = _detalleNC.razon.Trim().ToUpper();
                notaCredito.tipoPago = _detalleNC.tipoPago;
                notaCredito.refPago = _detalleNC.referencia;


                List<tbDetalleDocumento> lista = new List<tbDetalleDocumento>();
                foreach (var item in _doc.tbDetalleDocumento)
                {
                    tbDetalleDocumento detalle = new tbDetalleDocumento();
                    detalle.precioCosto = item.precioCosto;
                    detalle.cantidad = item.cantidad;
                    detalle.descuento = item.descuento;
                    detalle.idProducto = item.idProducto;
                    detalle.montoTotal = item.montoTotal;
                    detalle.montoTotalDesc = item.montoTotalDesc;
                    detalle.montoTotalExo = item.montoTotalExo;
                    detalle.montoTotalImp = item.montoTotalImp;
                    detalle.numLinea = item.numLinea;
                    detalle.porcExo = item.porcExo;
                    detalle.porcImp = item.porcImp;
                    detalle.precio = item.precio;
                    detalle.totalLinea = item.totalLinea;
                    lista.Add(detalle);

                }
                notaCredito.tbDetalleDocumento = lista;
                notaCredito = facturaIns.guadar(notaCredito);
                return notaCredito;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {


            if (Utility.AccesoInternet())
            {
                DialogResult result = MessageBox.Show("Se enviará por correo electrónico el documento seleccionado, Desea continuar?", "Envio de correo electrónico", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    if (validarCampos())
                    {
                        List<string> correos = new List<string>();
                        correos.Add(txtCorreo.Text.Trim());

                        if (txtCorreo2.Text != String.Empty)
                        {
                            correos.Add(txtCorreo2.Text.Trim());

                        }
                        enviarCorreo(_doc, correos);
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

                clsDocumentoCorreo docCorreo;
                if (doc.tipoDocumento== (int)Enums.TipoDocumento.Proforma || doc.tipoDocumento == (int)Enums.TipoDocumento.ProformaGeneral)
                {
                     docCorreo = new clsDocumentoCorreo(doc, correos, true, (int)Enums.tipoAdjunto.factura,chkDetalleProforma.Checked);

                }
                else
                {
                     docCorreo = new clsDocumentoCorreo(doc, correos, true, (int)Enums.tipoAdjunto.factura);
                }
              
                enviado = CorreoElectronico.enviarCorreo(docCorreo);

                if (enviado)
                {
                    MessageBox.Show("Se envió correctamente el correo electrónico", "Correo Electrónico", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                else
                {
                    
                    MessageBox.Show("Se produjo un error al enviar el Correo Electrónico." , "Correo Electrónico", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }

            }
            catch (Exception ex)
            {
                clsEvento evento = new clsEvento(ex.Message, "1");
                MessageBox.Show("Se produjo un error al enviar el Correo Electrónico", "Correo Electrónico", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private bool validarCampos()
        {
            bool ok = true;
            if (txtCorreo.Text == string.Empty && txtCorreo2.Text == string.Empty)
            {

                MessageBox.Show("Falta indicar al menos 1 correo electrónico para enviar la factura", "Faltan datos", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                txtCorreo.Focus();
                return false;
            }
            if (txtCorreo.Text != string.Empty)
            {
                if (!Utility.isValidEmail(txtCorreo.Text))
                {

                    MessageBox.Show("El formato de los correo es incorrecto, favor verificar", "Faltan datos", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    txtCorreo.Focus();
                    return false;
                }
            }
            if (txtCorreo2.Text != string.Empty)
            {
                if (!Utility.isValidEmail(txtCorreo2.Text))
                {

                    MessageBox.Show("El formato de los correo es incorrecto, favor verificar", "Faltan datos", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    txtCorreo2.Focus();
                    return false;
                }
            }

            return ok;
        }

        private void btnReImprimir_Click(object sender, EventArgs e)
        {

            DialogResult dialogResult = MessageBox.Show("Desea imprimir el documento?", "Imprimir", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {

                if (_doc != null)
                {
                    _doc = facturaIns.getEntity(_doc, true);
                    clsImpresionFactura imprimir;
                    if (_doc.tipoDocumento==(int)Enums.TipoDocumento.Proforma || _doc.tipoDocumento == (int)Enums.TipoDocumento.ProformaGeneral)
                    {
                        imprimir = new clsImpresionFactura(_doc, _doc.tbClientes, Global.Usuario.tbEmpresa, chkDetalleProforma.Checked);
                      

                    }else
                    {
                        imprimir = new clsImpresionFactura(_doc, Global.Usuario.tbEmpresa, false);
                    }
                   
                    imprimir.print();

                }
                else
                {
                    MessageBox.Show("No hay datos que imprimir", "Imprimir", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }

        }

        private void likClave_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmConsultaFacturaElectronica consulta = new frmConsultaFacturaElectronica();
            consulta.clave = likClave.Text;
            consulta.ShowDialog();
        }

        private void btnReporte_Click(object sender, EventArgs e)
        {
            if (_doc.tipoDocumento != (int)Enums.TipoDocumento.Proforma && _doc.tipoDocumento != (int)Enums.TipoDocumento.ProformaGeneral)
            {
                frmReportes reporte = new frmReportes();
                reporte.reporte = (int)Enums.reportes.factura;
                reporte.doc = _doc;
                reporte.ShowDialog();
            }
            else
            {
                if (chkDetalleProforma.Checked)
                {

                    frmReportes reporte = new frmReportes();
                    reporte.reporte = (int)Enums.reportes.factura;
                    reporte.doc = _doc;
                    reporte.ShowDialog();

                }
                else
                {
                    frmReportes reporte = new frmReportes();
                    reporte.reporte = (int)Enums.reportes.proformaSinDetalle;
                    reporte.doc = _doc;
                    reporte.ShowDialog();

                }
            }


        }

        private void btnAnularDocumento_Click(object sender, EventArgs e)
        {
            frmNotaCredito frmNota = new frmNotaCredito();
            frmNota._doc = _doc;
            frmNota.ShowDialog();
        }
    }
}
