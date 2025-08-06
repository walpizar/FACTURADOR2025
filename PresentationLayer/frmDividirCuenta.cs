using BusinessLayer;
using CommonLayer;
using CommonLayer.Exceptions.BussinessExceptions;
using EntityLayer;
using PresentationLayer.Clases;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace PresentationLayer
{
    public partial class frmDividirCuenta : Form
    {

        public delegate void pasarDatos(tbDocumento documento);
        public event pasarDatos pasarDatosPendientes;

        BFacturacion facturaIns = new BFacturacion();

        List<tbDetalleDocumento> detalleDocumentoParcial = new List<tbDetalleDocumento>();
        Bcliente BCliente = new Bcliente();
        BProducto productoIns = new BProducto();
        public tbDocumento documentoTotal { get; set; }
        tbClientes clienteGlo = null;
        bool exoneracionClie = false;
        bool respuestaAprobaDesc = false;
        decimal porcDesc = 0;
        int tipoDoc = 1;
        bool existeRespuesta = false;
        tbDocumento documentoGlo = null;

        public frmDividirCuenta()
        {
            InitializeComponent();
        }

        private void frmDividirCuenta_Load(object sender, EventArgs e)
        {
            try
            {
                chkTiqueteElectronico.Enabled = false;
                chkTiqueteElectronico.Checked = false;
                chkServicioMesa.Visible = (bool)Global.Usuario.tbEmpresa.tbParametrosEmpresa.FirstOrDefault().servicioMesa;
                if (chkServicioMesa.Visible)
                {
                    chkServicioMesa.Text = "Servicio Mesa(" + Global.Usuario.tbEmpresa.tbParametrosEmpresa.FirstOrDefault().porcServicioMesa + "%)";
                    chkServicioMesa.Checked = documentoTotal.tbDetalleDocumento.Where(x => x.idProducto == "SM").SingleOrDefault() != null;

                }
                cargarListaTotal();
            }
            catch (Exception)
            {

                MessageBox.Show("Error");
            }

        }

        private void cargarListaTotal()
        {
            lstvTotal.Items.Clear();
            foreach (var detalle in documentoTotal.tbDetalleDocumento)
            {
                if (detalle.idProducto != "SM")
                {
                    if (detalle.tbProducto.idMedida == (int)Enums.TipoMedida.kg)
                    {
                        ListViewItem item = new ListViewItem();
                        // Place a check mark next to the item.
                        item.Checked = false;
                        item.SubItems.Add(detalle.idProducto);
                        item.SubItems.Add(detalle.tbProducto.nombre.Trim().ToUpper());
                        item.SubItems.Add(detalle.precio.ToString());
                        lstvTotal.Items.Add(item);
                    }
                    else
                    {
                        for (int i = 0; i < detalle.cantidad; i++)
                        {
                            ListViewItem item = new ListViewItem();
                            // Place a check mark next to the item.
                            item.Checked = false;
                            item.SubItems.Add(detalle.idProducto);
                            item.SubItems.Add(detalle.tbProducto.nombre.Trim().ToUpper());
                            item.SubItems.Add(detalle.precio.ToString());
                            lstvTotal.Items.Add(item);
                        }
                    }
                }


            }
        }


        private void refrescarListas()
        {
            detalleDocumentoParcial = calcularMontosT(detalleDocumentoParcial);
            cargarListaParcial();
            cargarListaTotal();

        }

        private void cargarListaParcial()
        {
            lstvListaParcial.Items.Clear();
            foreach (var detalle in detalleDocumentoParcial)
            {
                if (detalle.idProducto != "SM")
                {
                    ListViewItem item = new ListViewItem();
                    // Place a check mark next to the item.
                    item.Checked = false;
                    item.SubItems.Add(detalle.idProducto);
                    item.SubItems.Add(detalle.tbProducto.nombre.Trim().ToUpper());
                    item.SubItems.Add(detalle.cantidad.ToString().Trim());
                    item.SubItems.Add(detalle.precio.ToString());
                    item.SubItems.Add(detalle.montoTotal.ToString());
                    item.SubItems.Add(detalle.montoTotalDesc.ToString());
                    item.SubItems.Add(detalle.montoTotalImp.ToString());
                    item.SubItems.Add(detalle.totalLinea.ToString());
                    lstvListaParcial.Items.Add(item);
                }
            }
        }

        private void lstvTotal_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            try
            {
                if (e.Item.Checked)
                {
                    string prod = e.Item.SubItems[1].Text;


                    var x = detalleDocumentoParcial.Where(v => v.idProducto.Trim() == prod.Trim()).SingleOrDefault();
                    if (x == null)
                    {
                        tbDetalleDocumento detalleParcial = new tbDetalleDocumento();
                        if (documentoTotal.tbDetalleDocumento.Where(b => b.idProducto.Trim() == prod.Trim()).SingleOrDefault().tbProducto.idMedida == (int)Enums.TipoMedida.kg)
                        {
                            detalleParcial.cantidad = documentoTotal.tbDetalleDocumento.Where(b => b.idProducto.Trim() == prod.Trim()).SingleOrDefault().cantidad;
                        }
                        else
                        {
                            detalleParcial.cantidad = 1;
                        }

                        detalleParcial.idProducto = documentoTotal.tbDetalleDocumento.Where(b => b.idProducto.Trim() == prod.Trim()).SingleOrDefault().idProducto;
                        detalleParcial.precio = documentoTotal.tbDetalleDocumento.Where(b => b.idProducto.Trim() == prod.Trim()).SingleOrDefault().precio;
                        detalleParcial.montoTotal = detalleParcial.cantidad * detalleParcial.precio;


                        detalleParcial.porcExo= documentoTotal.tbDetalleDocumento.Where(b => b.idProducto.Trim() == prod.Trim()).SingleOrDefault().porcExo;
                        detalleParcial.porcImp = documentoTotal.tbDetalleDocumento.Where(b => b.idProducto.Trim() == prod.Trim()).SingleOrDefault().porcImp;
                        detalleParcial.precioCosto= documentoTotal.tbDetalleDocumento.Where(b => b.idProducto.Trim() == prod.Trim()).SingleOrDefault().precioCosto;


                        detalleParcial.tbProducto = documentoTotal.tbDetalleDocumento.Where(b => b.idProducto.Trim() == prod.Trim()).SingleOrDefault().tbProducto;
                        detalleDocumentoParcial.Add(detalleParcial);
                    }
                    else
                    {
                        detalleDocumentoParcial.Where(c => c.idProducto.Trim() == prod.Trim()).SingleOrDefault().cantidad++;
                        detalleDocumentoParcial.Where(c => c.idProducto.Trim() == prod.Trim()).SingleOrDefault().montoTotal = detalleDocumentoParcial.Where(y => y.idProducto.Trim() == prod.Trim()).SingleOrDefault().cantidad *
                            detalleDocumentoParcial.Where(f => f.idProducto.Trim() == prod.Trim()).SingleOrDefault().precio;
                    }
                    if (documentoTotal.tbDetalleDocumento.Where(b => b.idProducto.Trim() == prod.Trim()).SingleOrDefault().tbProducto.idMedida == (int)Enums.TipoMedida.kg)
                    {

                        documentoTotal.tbDetalleDocumento.Where(b => b.idProducto.Trim() == prod.Trim()).SingleOrDefault().cantidad = 0;
                    }
                    else
                    {
                        documentoTotal.tbDetalleDocumento.Where(b => b.idProducto.Trim() == prod.Trim()).SingleOrDefault().cantidad--;
                    }


                    documentoTotal.tbDetalleDocumento.Where(b => b.idProducto.Trim() == prod.Trim()).SingleOrDefault().montoTotal = documentoTotal.tbDetalleDocumento.Where(b => b.idProducto.Trim() == prod.Trim()).SingleOrDefault().precio *
                        documentoTotal.tbDetalleDocumento.Where(b => b.idProducto.Trim() == prod.Trim()).SingleOrDefault().cantidad;
                    var detalle = documentoTotal.tbDetalleDocumento.Where(a => a.idProducto.Trim() == prod.Trim()).SingleOrDefault();
                    if (detalle.cantidad == 0)
                    {
                        documentoTotal.tbDetalleDocumento.Remove(detalle);

                    }


                    refrescarListas();
                }

            }
            catch (Exception)
            {

                MessageBox.Show("Error");
            }
        }

        private void lstvListaParcial_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            try
            {
                if (e.Item.Checked)
                {
                    string prod = e.Item.SubItems[1].Text;
                    var detalle = documentoTotal.tbDetalleDocumento.Where(x => x.idProducto == prod.Trim()).SingleOrDefault();
                    if (detalle == null)
                    {
                        documentoTotal.tbDetalleDocumento.Add(detalleDocumentoParcial.Where(x => x.idProducto == prod).SingleOrDefault());
                    }
                    else
                    {
                        documentoTotal.tbDetalleDocumento.Where(x => x.idProducto == prod.Trim()).SingleOrDefault().cantidad += decimal.Parse(e.Item.SubItems[3].Text);
                        documentoTotal.tbDetalleDocumento.Where(x => x.idProducto == prod.Trim()).SingleOrDefault().montoTotal = documentoTotal.tbDetalleDocumento.Where(x => x.idProducto == prod.Trim()).SingleOrDefault().cantidad *
                            documentoTotal.tbDetalleDocumento.Where(x => x.idProducto == prod.Trim()).SingleOrDefault().precio;
                    }



                    detalleDocumentoParcial.Remove(detalleDocumentoParcial.Where(x => x.idProducto == prod).SingleOrDefault());

                    refrescarListas();
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show("Error");
            }

        }



        private List<tbDetalleDocumento> calcularMontosT(List<tbDetalleDocumento> lista)
        {
            lista = asignarLineasNumero(lista);
            lista = calcularDescuentos(lista);
            lista = calcularImpuestos(lista);
            lista = calcularTotales(lista);
            lista = calcularServicioMesa(lista);
            imprimirTotales(lista);
            return lista;
        }

        private List<tbDetalleDocumento> calcularServicioMesa(List<tbDetalleDocumento> listaDetalleDocumento)
        {
            listaDetalleDocumento.Remove(listaDetalleDocumento.Where(x => x.idProducto == "SM").SingleOrDefault());
            if (chkServicioMesa.Checked)
            {
                tbDetalleDocumento detalle = new tbDetalleDocumento();
                detalle.idProducto = "SM";
                detalle.cantidad = 1;
                detalle.precio = listaDetalleDocumento.Sum(x => x.totalLinea) * ((decimal)Global.Usuario.tbEmpresa.tbParametrosEmpresa.FirstOrDefault().porcServicioMesa / 100);
                detalle.montoTotal = detalle.cantidad * detalle.precio;




                detalle.totalLinea = detalle.cantidad * detalle.montoTotal;
                listaDetalleDocumento.Add(detalle);

            }
            return listaDetalleDocumento;
        }

        private List<tbDetalleDocumento> asignarLineasNumero(List<tbDetalleDocumento> listaDetalleDocumento)
        {
            int linea = 0;
            foreach (tbDetalleDocumento det in listaDetalleDocumento)
            {
                linea++;
                det.numLinea = linea;
            }
            return listaDetalleDocumento;
        }

        private List<tbDetalleDocumento> calcularTotales(List<tbDetalleDocumento> listaDetalleDocumento)
        {

            foreach (tbDetalleDocumento detalle in listaDetalleDocumento)
            {
                if (detalle.idProducto != "SM")
                {
                    detalle.totalLinea = (detalle.montoTotal - detalle.montoTotalDesc) + detalle.montoTotalImp - detalle.montoTotalExo;

                }


            }
            return listaDetalleDocumento;

        }

        private void imprimirTotales(List<tbDetalleDocumento> listaDetalleDocumento)
        {
            decimal total = 0, desc = 0, iva = 0, subtotal = 0, exo = 0, sm = 0;

            foreach (tbDetalleDocumento detalle in listaDetalleDocumento)
            {
                if (detalle.idProducto != "SM")
                {
                    total += detalle.totalLinea;
                    desc += detalle.montoTotalDesc;
                    iva += detalle.montoTotalImp;
                    exo += detalle.montoTotalExo;
                    subtotal += detalle.montoTotal;
                }
            }

            txtSubtotal.Text = string.Format("{0:N2}", subtotal);
            txtDescuento.Text = string.Format("{0:N2}", desc);
            txtIva.Text = string.Format("{0:N2}", iva);
            txtExoneracion.Text = string.Format("{0:N2}", exo);
            txtSub.Text = string.Format("{0:N2}", (subtotal - desc + iva) - exo);
            txtServicioMesa.Text = string.Format("{0:N2}", sm);
            if (listaDetalleDocumento.Where(x => x.idProducto == "SM").SingleOrDefault() != null)
            {
                sm = listaDetalleDocumento.Where(x => x.idProducto == "SM").Sum(x => x.totalLinea);
                txtServicioMesa.Text = string.Format("{0:N2}", sm);
            }
            txtTotal.Text = string.Format("{0:N2}", (total + sm));


            if (txtSubtotal.Text == string.Empty)
            {
                txtSubtotal.Text = "0";

            }
            if (txtTotal.Text == string.Empty)
            {
                txtTotal.Text = "0";

            }

            if (txtPorcetaje.Text == string.Empty)
            {
                txtPorcetaje.Text = "0";

            }
            if (txtDescuento.Text == string.Empty)
            {
                txtDescuento.Text = "0";

            }
            if (txtIva.Text == string.Empty)
            {
                txtIva.Text = "0";

            }
            if (txtExoneracion.Text == string.Empty)
            {
                txtExoneracion.Text = "0";

            }
            if (txtSub.Text == string.Empty)
            {
                txtSub.Text = "0";

            }
            if (txtServicioMesa.Text == string.Empty)
            {
                txtServicioMesa.Text = "0";

            }
        }



        private List<tbDetalleDocumento> calcularImpuestos(List<tbDetalleDocumento> listaDetalleDocumento)
        {

            foreach (tbDetalleDocumento detalle in listaDetalleDocumento)
            {
                if (detalle.idProducto != "SM")
                {
                    //sino es excento el producto
                    if (!detalle.tbProducto.esExento)
                    {


                        detalle.montoTotalImp = (detalle.montoTotal - detalle.montoTotalDesc)
                            * (((decimal)detalle.tbProducto.tbImpuestos.valor) / 100);
                        detalle.porcImp = (int)detalle.tbProducto.tbImpuestos.valor;

                        detalle.montoTotalExo = 0;
                        //aplica exoneracion al cliente
                        if (exoneracionClie && (bool)detalle.tbProducto.aplicaExo)
                        {
                            if (clienteGlo.porcExo >= (decimal)detalle.tbProducto.tbImpuestos.valor)
                            {
                                detalle.montoTotalExo = (detalle.montoTotal - detalle.montoTotalDesc)
                            * (((decimal)detalle.tbProducto.tbImpuestos.valor) / 100);
                                detalle.porcExo = (int)detalle.tbProducto.tbImpuestos.valor;
                            }
                            else
                            {
                                detalle.montoTotalExo = (detalle.montoTotal - detalle.montoTotalDesc)
                               * (((decimal)clienteGlo.porcExo) / 100);
                                detalle.porcExo = (int)clienteGlo.porcExo;

                            }
                        }
                    }
                    else
                    {//no aplica impuesto ya que el producto es excento.
                        detalle.montoTotalImp = 0;
                        detalle.montoTotalExo = 0;
                        detalle.porcExo = 0;
                        detalle.porcImp = 0;

                    }

                }

            }
            return listaDetalleDocumento;
        }

        private List<tbDetalleDocumento> calcularDescuentos(List<tbDetalleDocumento> listaDetalleDocumento)
        {

            decimal descMaxEmp = ((decimal)Global.Usuario.tbEmpresa.tbParametrosEmpresa.First().descuentoBase) / 100;
            bool aprobDescEmp = (bool)Global.Usuario.tbEmpresa.tbParametrosEmpresa.First().aprobacionDescuento;
            bool validaDesc = true;
            decimal porc = 0;

            if (txtPorcetaje.Text != string.Empty)
            {

                porc = decimal.Parse(txtPorcetaje.Text) / 100;

            }
            else
            {

                txtDescuento.Text = "0";
            }
            //verifica si el descuento es mayoral porcentaje permitido en la empresa, de lo contrario que aplique
            if (porc > descMaxEmp)
            {//se debe de solicitar aprobacion en caso que la empresa lo requiera, segun el parametro de la empresa
                if (aprobDescEmp)
                {
                    if (!respuestaAprobaDesc && porcDesc != decimal.Parse(txtPorcetaje.Text.Trim()))
                    {

                        //form para aprobacion de un administrador true aprueba, false no aprueba
                        frmAprobacion aprobacionForm = new frmAprobacion();
                        aprobacionForm.pasarDatosEvent += respuestaAprobacion;
                        aprobacionForm.ShowDialog();
                        validaDesc = respuestaAprobaDesc;
                    }
                    else
                    {
                        validaDesc = true;
                    }
                }
                else
                {

                    validaDesc = true;

                }
            }
            if (validaDesc)
            {

                foreach (tbDetalleDocumento detalle in listaDetalleDocumento)
                {
                    if (detalle.idProducto != "SM")
                    {
                        if ((bool)detalle.tbProducto.aplicaDescuento)
                        {
                            if (porc > ((detalle.tbProducto.descuento_max) / 100))
                            {
                                detalle.descuento = (decimal)detalle.tbProducto.descuento_max;
                                detalle.montoTotalDesc = detalle.montoTotal * ((decimal)detalle.tbProducto.descuento_max / 100);
                            }
                            else
                            {
                                detalle.descuento = (decimal)porc * 100;
                                detalle.montoTotalDesc = detalle.montoTotal * porc;
                            }
                        }
                        else
                        {
                            detalle.descuento = 0;
                            detalle.montoTotalDesc = 0;

                        }
                    }

                }

            }
            else
            {
                porcDesc = 0;
                txtPorcetaje.Text = porcDesc.ToString();
                
            }
            respuestaAprobaDesc = false;
            return listaDetalleDocumento;

        }
   
        private void respuestaAprobacion(bool aprob)
        {
            respuestaAprobaDesc = aprob;
            porcDesc = decimal.Parse(txtPorcetaje.Text.Trim());
        }

        private void txtPorcetaje_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((int)e.KeyChar == (int)Keys.Enter)
            {

                try
                {
                    if (txtPorcetaje.Text == string.Empty)
                    {


                        txtDescuento.Text = "0";
                    }

                    detalleDocumentoParcial = calcularMontosT(detalleDocumentoParcial);
                }
                catch (Exception)
                {


                    MessageBox.Show("Se produjo un error al ingresar el producto, corrija los datos", "Calcular descuento", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnCobrar_Click(object sender, EventArgs e)
        {
            tipoDoc = (int)Enums.TipoDocumento.FacturaElectronica;


            if (detalleDocumentoParcial.Count != 0 && txtTotal.Text != "0")
            {
                if (validarCampos())
                {

                    tbDocumento documento = crearDocumento();
                    frmCobrar form = new frmCobrar();
                    form.recuperarTotal += respuesta;
                    form.facturaGlobal = documento;
                    form.ShowDialog();
                }
            }
            else
            {
                MessageBox.Show("No hay productos o el TOTAL a cobrar es 0.", "Cobrar", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void respuesta(tbDocumento doc, bool precio)
        {
            try
            {

                if (doc != null)
                {
                    existeRespuesta = true;
                    if (doc.reporteElectronic == true)
                    {

                        if (Utility.AccesoInternet())
                        {

                            BackgroundWorker tarea = new BackgroundWorker();
                            documentoGlo = doc;
                            tarea.DoWork += reportarFacturacionElectronica;
                            tarea.RunWorkerAsync();

                            if (doc.id != 0)
                            {
                                MessageBox.Show("El documento ha sido emitido correctamente, el reporte de hacienda se generará en segundo plano", "Documento creado", MessageBoxButtons.OK, MessageBoxIcon.Information);


                            }

                        }
                        else
                        {
                            MessageBox.Show("No hay acceso a internet", "Sin Internet", MessageBoxButtons.OK, MessageBoxIcon.Error);


                        }

                    }
                    else
                    {
                        if (doc.id != 0)
                        {
                            MessageBox.Show("El documento ha sido emitido correctamente", "Documento creado", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            bool enviado = false;
                            //se solicita respuesta, y se confecciona el correo a enviar
                            if ((bool)doc.notificarCorreo)
                            {

                                List<string> correos = new List<string>();
                                if (doc.correo1 != null)
                                {
                                    correos.Add(doc.correo1.Trim());

                                }
                                if (doc.correo2 != null)
                                {
                                    correos.Add(doc.correo2.Trim());

                                }
                                clsDocumentoCorreo docCorreo = new clsDocumentoCorreo(doc, correos, true, (int)Enums.tipoAdjunto.factura, precio);
                                CorreoElectronico.enviarCorreo(docCorreo);
                            }

                        }

                    }
                    limpiar();
                }
                else
                {
                    MessageBox.Show("El documento no se ha guardado, intente nuevamente, de lo contrario contacte con el administrador.", "Error al crear Documento", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }

            catch (Exception ex)
            {

                MessageBox.Show("Error en el sistema, contacte al administrador", "Error general", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void reportarFacturacionElectronica(object sender, DoWorkEventArgs e)
        {
            tbDocumento doc = documentoGlo;
            try
            {
                //envio la factura a hacienda
                doc = facturaIns.FacturarElectronicamente(doc);

                System.Threading.Thread.Sleep(3000);
                //consulto a hacienda el estado de la factura
                try
                {
                    string mensaje = await facturaIns.consultarFacturaElectronicaPorClave(doc.clave);
                }
                catch (Exception)
                {

                    MessageBox.Show("Error al consultar el estado del documento en Hacienda, valida el estado del documento", "Error al consultar el estado del documento", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }

                bool enviado = false;
                //se solicita respuesta, y se confecciona el correo a enviar
                if ((bool)doc.notificarCorreo)
                {
                    List<string> correos = new List<string>();
                    if (doc.correo1 != null)
                    {
                        correos.Add(doc.correo1.Trim());

                    }
                    if (doc.correo2 != null)
                    {
                        correos.Add(doc.correo2.Trim());

                    }
                    clsDocumentoCorreo docCorreo = new clsDocumentoCorreo(doc, correos, true, (int)Enums.tipoAdjunto.factura);
                    CorreoElectronico.enviarCorreo(docCorreo);

                }
            }
            catch (FacturacionElectronicaException ex)
            {
                MessageBox.Show("Error al realizar la facturación electronica", "Factura Electrónica", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (EnvioCorreoException ex)
            {
                MessageBox.Show("Error al enviar la facturación por correo electrónico", "Correo electrónico", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (TokenException ex)
            {
                MessageBox.Show("Error al obtener el Token en Hacienda", "Facturación electrónica", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (ConsultaHaciendaExcpetion ex)
            {
                MessageBox.Show("Error al consultar hacienda la factura electrónica", "Facturación electrónica", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (generarXMLException ex)
            {
                MessageBox.Show("Error al generar el XML de la factura", "Facturación electrónica", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception EX)
            {
                MessageBox.Show("Error general de facturación electrónica", "Facturación electrónica", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void limpiar()
        {
            detalleDocumentoParcial.Clear();

            clienteGlo = null;
            exoneracionClie = false;
            existeRespuesta = false;

            chkEnviar.Checked = false;
            txtCorreo2.Text = string.Empty;

            txtIdCliente.Text = string.Empty;
            txtCliente.Text = string.Empty;

            txtTel.Text = string.Empty;
            txtCorreo.Text = string.Empty;
            txtCorreo2.Text = string.Empty;

            txtSubtotal.Text = "0";
            txtIva.Text = "0";
            txtPorcetaje.Text = "0";
            txtDescuento.Text = "0";
            txtTotal.Text = "0";

            txtExoneracion.Text = "0";

            lstvListaParcial.Items.Clear();
            respuestaAprobaDesc = false;
            porcDesc = 0;

            chkTiqueteElectronico.Enabled = false;
            chkTiqueteElectronico.Checked = false;

        }

        private bool validarCampos()
        {
            bool validaCliente = (bool)Global.Usuario.tbEmpresa.tbParametrosEmpresa.First().clienteObligatorioFact;

            if (validaCliente || tipoDoc == (int)Enums.TipoDocumento.Proforma)
            {
                if (clienteGlo == null)
                {
                    MessageBox.Show("Debe indicar un cliente", "Faltan datos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    btnBuscarCliente.Focus();
                    return false;
                }

            }
            if (chkEnviar.Checked)
            {
                if (txtCorreo.Text == string.Empty)
                {
                    MessageBox.Show("Debe Ingresar un correo electrónico", "Faltan datos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtCorreo.Focus();
                    return false;

                }
                if (!Utility.isValidEmail(txtCorreo.Text))
                {
                    MessageBox.Show("El formato del correo es incorrecto", "Faltan datos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtCorreo.Focus();
                    return false;
                }
                if (txtCorreo2.Text != string.Empty)
                {
                    if (!Utility.isValidEmail(txtCorreo2.Text))
                    {
                        MessageBox.Show("El formato del correo es incorrecto", "Faltan datos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtCorreo2.Focus();
                        return false;
                    }

                }
            }
            return true;
        }


        private tbDocumento crearDocumento()
        {
            tbDocumento documento = new tbDocumento();


            documento.tipoDocumento = (int)Enums.TipoDocumento.FacturaElectronica;
            documento.fecha = Utility.getDate();
            documento.tipoMoneda = (int)Enums.TipoMoneda.CRC;
            documento.tipoCambio = 0;
            documento.reporteElectronic = (bool)Global.Usuario.tbEmpresa.tbParametrosEmpresa.First().facturacionElectronica;
            documento.tipoVenta = (int)Enums.tipoVenta.Contado;
            documento.plazo = 0;
            documento.tipoPago = (int)Enums.TipoPago.Efectivo;
            documento.refPago = string.Empty;
            documento.estadoFactura = (int)Enums.EstadoFactura.Cancelada;
            documento.reporteAceptaHacienda = false;
            documento.notificarCorreo = chkEnviar.Checked;
            //documento.observaciones = txtObservaciones.Text.ToUpper().Trim();
            documento.idEmpresa = Global.Usuario.tbEmpresa.id;
            documento.tipoIdEmpresa = Global.Usuario.tbEmpresa.tipoId;
            documento.codigoActividad = Global.actividadEconomic.CodActividad;

            documento.sucursal = Global.Configuracion.sucursal;
            documento.caja = Global.Configuracion.caja;
            //en caso que no tenga cliente asignado, sera no contribuyente

            //si no marco el check de enviar correo, deja los campos de correo electronico a notificar null
            if ((bool)documento.notificarCorreo)
            {
                documento.correo1 = txtCorreo.Text == string.Empty ? null : txtCorreo.Text.Trim();
                documento.correo2 = txtCorreo2.Text == string.Empty ? null : txtCorreo2.Text.Trim();

            }

            //cliente
            if (clienteGlo != null)
            {
                documento.idCliente = clienteGlo.id;
                documento.tipoIdCliente = clienteGlo.tipoId;
                documento.tbClientes = clienteGlo;

            }

            if (documento.reporteElectronic)
            {
                if ((bool)Global.Usuario.tbEmpresa.regimenSimplificado)
                {
                    if (Global.Usuario.tbEmpresa.tipoFacturacionRegimen == (int)Enums.TipoFacturacionElectRegimenSimplificado.SoloFacturacionConCliente)
                    {
                        if (clienteGlo != null)
                        {
                            documento.tipoDocumento = (int)Enums.TipoDocumento.FacturaElectronica;
                            if (chkTiqueteElectronico.Checked)
                            {
                                documento.tipoDocumento = (int)Enums.TipoDocumento.TiqueteElectronico;
                            }
                        }
                        else
                        {
                            documento.tipoDocumento = (int)Enums.TipoDocumento.Factura;
                            documento.reporteElectronic = false;
                        }
                    }
                    else if (Global.Usuario.tbEmpresa.tipoFacturacionRegimen == (int)Enums.TipoFacturacionElectRegimenSimplificado.Todo)
                    {
                        if (clienteGlo != null)
                        {
                            documento.tipoDocumento = (int)Enums.TipoDocumento.FacturaElectronica;
                            if (chkTiqueteElectronico.Checked)
                            {
                                documento.tipoDocumento = (int)Enums.TipoDocumento.TiqueteElectronico;
                            }
                        }
                        else
                        {
                            documento.tipoDocumento = (int)Enums.TipoDocumento.TiqueteElectronico;
                        }
                    }
                }
                else
                {
                    if (clienteGlo != null)
                    {
                        documento.tipoDocumento = (int)Enums.TipoDocumento.FacturaElectronica;
                        if (chkTiqueteElectronico.Checked)
                        {
                            documento.tipoDocumento = (int)Enums.TipoDocumento.TiqueteElectronico;
                        }
                    }
                    else
                    {
                        documento.tipoDocumento = (int)Enums.TipoDocumento.TiqueteElectronico;

                    }
                }
            }
            else
            {
                documento.tipoDocumento = (int)Enums.TipoDocumento.Factura;
                documento.reporteElectronic = false;
            }

            documento.estado = true;

            //foreach (tbDetalleDocumento detalle in detalleDocumentoParcial)
            //{
            //    detalle.tbProducto = null;
            //}

            documento.tbDetalleDocumento = detalleDocumentoParcial;

            //Atributos de Auditoria

            documento.fecha_crea = Utility.getDate();
            documento.fecha_ult_mod = Utility.getDate();
            documento.usuario_crea = Global.Usuario.nombreUsuario.Trim().ToUpper();   // Global.Usuario.nombreUsuario;
            documento.usuario_ult_mod = Global.Usuario.nombreUsuario.Trim().ToUpper();   // Global.Usuario.nombreUsuario;

            return documento;
        }

        private void btnBuscarCliente_Click(object sender, EventArgs e)
        {
            FrmBuscar buscar = new FrmBuscar();
            buscar.pasarDatosEvent += dataBuscarCliente;
            buscar.ShowDialog();

        }

        private void dataBuscarCliente(tbClientes cliente)
        {
            exoneracionClie = false;
            if (cliente != null)
            {
                chkTiqueteElectronico.Enabled = true;
                chkTiqueteElectronico.Checked = false;
                clienteGlo = cliente;
                if (cliente.idExonercion != null)
                {
                    DialogResult result = MessageBox.Show("El cliente seleccionado aplica para exoneración de impuesto, ¿Desea aplicar la exoneración de impuestos?", "Exoneración de Impuestos", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        exoneracionClie = true;

                    }
                    else
                    {
                        exoneracionClie = false;
                    }

                }
                txtIdCliente.Text = cliente.id.Trim();
                if (cliente.tipoId == 1)
                {
                    txtCliente.Text = cliente.tbPersona.nombre.Trim().ToUpper() + " " + cliente.tbPersona.apellido1.Trim().ToUpper() + " " + cliente.tbPersona.apellido2.Trim().ToUpper();

                }
                else
                {
                    txtCliente.Text = cliente.tbPersona.nombre.Trim().ToUpper();

                }

                txtTel.Text = cliente.tbPersona.telefono.ToString().Trim().ToUpper();
                txtCorreo.Text = cliente.tbPersona.correoElectronico.Trim();


            }
        }

        private void txtIdCliente_TextChanged(object sender, EventArgs e)
        {
            if (txtIdCliente.Text == string.Empty)
            {
                txtIdCliente.Text = string.Empty;
                txtCliente.Text = string.Empty;

                txtTel.Text = string.Empty;
                txtCorreo.Text = string.Empty;
                exoneracionClie = false;

                detalleDocumentoParcial = calcularMontosT(detalleDocumentoParcial);

            }
        }

        private void txtIdCliente_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((int)e.KeyChar == (int)Keys.Enter)
            {
                if (txtIdCliente.Text != string.Empty)
                {
                    try
                    {
                        clienteGlo = BCliente.GetClienteById(txtIdCliente.Text.Trim());
                        if (clienteGlo != null)
                        {
                            dataBuscarCliente(clienteGlo);
                        }
                        else
                        {
                            DialogResult resp = MessageBox.Show($"No se encontró el Cliente con el ID: {txtIdCliente.Text.Trim()}, ¿Desea agregarlo nuevo?", "Buscar Cliente", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                            if (resp == DialogResult.Yes)
                            {
                                try
                                {
                                    frmClientes buscar = new frmClientes();
                                    buscar.id = txtIdCliente.Text;
                                    buscar.pasarDatosEvent += dataBuscarCliente;
                                    buscar.ShowDialog();
                                }
                                catch (Exception)
                                {

                                    MessageBox.Show("Error al buscar el cliente.", "Buscar cliente", MessageBoxButtons.OK, MessageBoxIcon.Question);
                                }
                            }
                            else
                            {
                                clienteGlo = null;
                                txtIdCliente.Text = string.Empty;
                                txtCliente.Text = string.Empty;

                                txtTel.Text = string.Empty;
                                txtCorreo.Text = string.Empty;
                                txtCorreo2.Text = string.Empty;
                            }

                        }
                    }
                    catch (Exception)
                    {
                        txtCliente.Text = string.Empty;
                        MessageBox.Show("No se pudo obtener el Cliente, verifique los datos", "Buscar Cliente", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }
                }
            }
        }

        private void frmDividirCuenta_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {

                foreach (ListViewItem item in lstvListaParcial.Items)
                {

                    string prod = item.SubItems[1].Text;
                    decimal cant = decimal.Parse(item.SubItems[3].Text);

                    var detalle = documentoTotal.tbDetalleDocumento.Where(a => a.idProducto == prod.Trim()).SingleOrDefault();

                    var x = detalleDocumentoParcial.Where(v => v.idProducto == prod.Trim()).SingleOrDefault();
                    if (detalle == null)
                    {
                        tbDetalleDocumento detalleParcial = new tbDetalleDocumento();
                        detalleParcial.cantidad = cant;
                        detalleParcial.idProducto = x.idProducto;
                        detalleParcial.precio = x.precio;
                        detalleParcial.montoTotal = detalleParcial.cantidad * detalleParcial.precio;
                        detalleParcial.porcExo = x.porcExo;
                        detalleParcial.porcImp = x.porcImp;
                        detalleParcial.precioCosto = x.precioCosto;

                        detalleParcial.tbProducto = x.tbProducto;
                        documentoTotal.tbDetalleDocumento.Add(detalleParcial);
                    }
                    else
                    {
                        documentoTotal.tbDetalleDocumento.Where(c => c.idProducto == prod.Trim()).SingleOrDefault().cantidad += cant;
                        documentoTotal.tbDetalleDocumento.Where(c => c.idProducto == prod.Trim()).SingleOrDefault().montoTotal = documentoTotal.tbDetalleDocumento.Where(y => y.idProducto == prod.Trim()).SingleOrDefault().cantidad *
                         documentoTotal.tbDetalleDocumento.Where(f => f.idProducto == prod.Trim()).SingleOrDefault().precio;
                    }
                }
                if (lstvTotal.Items.Count == 0 && lstvListaParcial.Items.Count == 0)
                {

                    pasarDatosPendientes(null);
                }
                else
                {

                    documentoTotal.tbDetalleDocumento = calcularMontosT((List<tbDetalleDocumento>)documentoTotal.tbDetalleDocumento);
                    pasarDatosPendientes(documentoTotal);
                }

            }
            catch (Exception)
            {

                MessageBox.Show("Error al cerrar el formulario", "Cerrando...", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
          
            this.Close();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void chkServicioMesa_CheckedChanged(object sender, EventArgs e)
        {
            try
            {

                documentoTotal.tbDetalleDocumento = calcularMontosT((List<tbDetalleDocumento>)documentoTotal.tbDetalleDocumento);
                detalleDocumentoParcial = calcularMontosT(detalleDocumentoParcial);

                cargarListaTotal();
                cargarListaParcial();

            }
            catch (Exception)
            {

                MessageBox.Show("Error al agregar el servicio de mesa", "Servicio Mesa", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void lstvTotal_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        

        private void imprimirCuenta()
        {
            if (detalleDocumentoParcial.Count != 0 && txtTotal.Text != "0" )
            {
                foreach (var item in detalleDocumentoParcial)
                {
                    item.tbProducto = productoIns.GetEntityById(item.idProducto);
                }

                clsImpresionFactura imprimir = new clsImpresionFactura(detalleDocumentoParcial, "", Global.Usuario.tbEmpresa);
                imprimir.printCuenta();



            }
            else
            {


                MessageBox.Show("No hay cuenta pendiente para imprimir.", "Imprimir cuenta", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void frmDividirCuenta_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F9)
            {
                imprimirCuenta();

            }
        }
    }
}