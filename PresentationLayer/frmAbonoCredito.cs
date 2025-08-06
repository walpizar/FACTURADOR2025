using BusinessLayer;
using CommonLayer;
using EntityLayer;
using PresentationLayer.Clases;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;


namespace PresentationLayer
{
    public partial class frmAbonoCredito : Form
    {

        public tbClientes clienteGlobal = new tbClientes();//para buscar clientes
        public BFacturacion facturacionB = new BFacturacion();
        IEnumerable<tbDocumento> docsGlobal;
        List<tbDocumento> docsModificados = new List<tbDocumento>();
        List<tbPagos> pagosModificados = new List<tbPagos>();
        Bcliente clienteB = new Bcliente();



        public frmAbonoCredito()
        {
            InitializeComponent();
        }
        private void frmCredito_Load(object sender, EventArgs e)///load
        {
            cargarOpciones();

        }

        private void cargarOpciones()
        {
            //chkImprimir.Checked = Global.Configuracion.imprime==(int)Enums.EstadoConfig.Si;
            //chkImprimir.Visible = Global.Configuracion.imprime == (int)Enums.EstadoConfig.Si;


        }




        //metodo para buscar clientes
        private void dataBuscar(tbClientes cliente)
        {



            clienteGlobal = cliente;

            if (clienteGlobal != null && clienteGlobal.id.Trim() != null)//antes tenia cero pero como es string se pone null
            {
                txtIdCliente.Text = cliente.id.Trim();
                if (cliente.tipoId == 1)
                {
                    txtCliente.Text = cliente.tbPersona.nombre.Trim().ToUpper() + " " + cliente.tbPersona.apellido1.Trim().ToUpper() + " " + cliente.tbPersona.apellido2.Trim().ToUpper();

                }
                else
                {
                    txtCliente.Text = cliente.tbPersona.nombre.Trim().ToUpper();

                }

                txtDireccion.Text = cliente.tbPersona.otrasSenas.Trim().ToUpper();
                txtTel.Text = cliente.tbPersona.telefono.ToString().Trim().ToUpper();
                txtCorreo.Text = cliente.tbPersona.correoElectronico.Trim();

                cargarCreditos();
            }
            else
            {
                limpiarForm();
            }

        }

        private void cargarCreditos()
        {
            try
            {
                lsvFacturas.Items.Clear();
                IEnumerable<tbDocumento> docs = facturacionB.getListDocCreditoPendienteByCliente(clienteGlobal.tipoId, clienteGlobal.id);

                docsGlobal = docs;

                decimal mnontoGeneral = 0;
                decimal adeudadoGeneral = 0;
                decimal adeudadoDoc = 0;
                decimal totalNC = 0;
                IEnumerable<tbDocumento> nc = null;
                foreach (tbDocumento item in docs)
                {
                    totalNC = 0;
                    //Creamos un objeto de tipo ListviewItem
                    ListViewItem linea = new ListViewItem();
                    //CheckBox chk = new CheckBox();
                    //linea.ImageIndext4r+= chk;
                    linea.SubItems.Add(item.id.ToString());
                    linea.SubItems.Add(item.fecha.ToString());
                    linea.SubItems.Add(item.consecutivo.Trim());
                    decimal monto = 0;

                    //MONTO DE LA FACTURA
                    monto = item.tbDetalleDocumento.Sum(x => x.totalLinea);
                    nc = null;
                    //MONTO DE NOTAS DE CREDITO
                    nc = facturacionB.getNCByRef(item.clave);

                    foreach (var notaCredito in nc)
                    {
                        totalNC += (decimal)notaCredito.tbDetalleDocumento.Sum(x => x.totalLinea);

                    }
                    //monto de pagos realizados
                    decimal pagos = (decimal)item.tbPagos.Sum(x => x.monto);

                    adeudadoDoc = monto - totalNC - pagos;

                    if (adeudadoDoc > 0)
                    {
                        mnontoGeneral += (monto - totalNC);
                        adeudadoGeneral += monto - totalNC - pagos;

                        linea.SubItems.Add(Utility.priceFormat (monto - totalNC));
                        linea.SubItems.Add(Utility.priceFormat(adeudadoDoc));

                        //Agregamos el item a la lista.
                        double daysPlazo = double.Parse(item.plazo.ToString());
                        DateTime fechaVenc = item.fecha.AddDays(daysPlazo);

                        linea.SubItems.Add(fechaVenc.ToString());
                        if (fechaVenc < Utility.getDate())
                        {
                            linea.ForeColor = Color.Red;
                        }
                        lsvFacturas.Items.Add(linea);

                    }
                    else
                    {

                    }


                }





                txtAdeudado.Text = Utility.priceFormat(adeudadoGeneral);// string.Format("{0:N2}", adeudadoGeneral);
                txtAbono.Text = Utility.priceFormat(0); ;
                txtFacturado.Text = Utility.priceFormat(mnontoGeneral);
                txtPendiente.Text = Utility.priceFormat(adeudadoGeneral);
            }
            catch (Exception ex)
            {

                MessageBox.Show("El dato al cargar los créditos.", "Cargar Créditos", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }


        }

        //funcion que abre formulario de busqueda y asigna metodos a un evento
        private void buscar()
        {
            FrmBuscar buscar = new FrmBuscar();
            buscar.pasarDatosEvent += dataBuscar;
            buscar.ShowDialog();
        }

        //permiten agregar tablas a las lista y evitar ir a base de datos

        //boton que al clickearlo limpia los listview y va al metodo buscar() que abre form de busqueda personas



        //Metodos para cargar lista de abonos y lista de creditos


        private void btnBuscarCliente_Click(object sender, EventArgs e)
        {
            try
            {
                buscar();
            }
            catch (Exception)
            {
                MessageBox.Show("Error al buscar los datos del cliente.", "Cliente", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }
        private void limpiarForm()
        {

            clienteGlobal = null;
            lsvFacturas.Items.Clear();
            txtIdCliente.Text = string.Empty;
            txtCliente.Text = string.Empty;
            txtDireccion.Text = string.Empty;
            txtTel.Text = string.Empty;
            txtCorreo.Text = string.Empty;
            txtAdeudado.Text = string.Empty;
            txtAbono.Text = string.Empty;
            txtFacturado.Text = string.Empty;
            chkTodos.Checked = false;

            clienteGlobal = null;
            docsGlobal = null;
            docsModificados.Clear();
            pagosModificados.Clear();

            txtPendiente.Text = String.Empty;

            chkEnviarCorreo.Checked = true;
        }
        private void cargarDatosCliente()
        {

            try
            {

                clienteGlobal = clienteB.GetListEntities(1).Where(x => x.id == clienteGlobal.id && x.tipoId == clienteGlobal.tipoId).SingleOrDefault();
                dataBuscar(clienteGlobal);
            }
            catch (Exception)
            {

                throw;
            }


        }



        private void button1_Click(object sender, EventArgs e)
        {

            try
            {
                DialogResult resp = MessageBox.Show($"Esta seguro que desea realizar el abono por el MONTO: {txtAbono.Text} al CLIENTE: { txtIdCliente.Text}-{txtCliente.Text }", "Aplicar abono", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (resp == DialogResult.Yes)
                {

                    docsModificados.Clear();
                    if (Utility.isNumeroDecimal(txtAbono.Text) && txtAbono.Text != string.Empty)
                    {
                        decimal montoAdeudado = decimal.Parse(txtAdeudado.Text);
                        decimal montoPago = decimal.Parse(txtAbono.Text);

                        if ((montoAdeudado - montoPago) >= 0)
                        {
                            foreach (tbDocumento doc in docsGlobal)
                            {
                                pagosModificados.Clear();
                                decimal pago = (decimal)doc.tbPagos.Sum(x => x.monto);
                                decimal totalFact = (decimal)doc.tbDetalleDocumento.Sum(x => x.totalLinea);
                                decimal adeudadoFact = 0;
                                decimal NC = 0;

                                var nc = facturacionB.getNCByRef(doc.clave);
                                foreach (var notaCredito in nc)
                                {
                                    NC += (decimal)notaCredito.tbDetalleDocumento.Sum(x => x.totalLinea);

                                }
                                //calculo el total adeudado actual
                                adeudadoFact = totalFact - pago - NC;


                                tbPagos pagoE = new tbPagos();
                                pagoE.idDoc = doc.id;
                                pagoE.tipoDoc = doc.tipoDocumento;
                                pagoE.estado = true;
                                pagoE.fecha_crea = Utility.getDate();
                                pagoE.fecha_ult_mod = Utility.getDate();
                                pagoE.usuario_crea = Global.Usuario.nombreUsuario.Trim().ToUpper();   // Global.Usuario.nombreUsuario;
                                pagoE.usuario_ult_mod = Global.Usuario.nombreUsuario.Trim().ToUpper();   // Global.Usuario.nombreUsuario;

                                //verifica si el abono es mayor a lo adeudado, para poner en 0 y cancelar la factura
                                if (montoPago >= decimal.Parse( Utility.priceFormat(adeudadoFact)))
                                {

                                    pagoE.monto = adeudadoFact;
                                    doc.estadoFactura = (int)Enums.EstadoFactura.Cancelada;
                                    doc.fecha_ult_mod = Utility.getDate();
                                    doc.usuario_ult_mod = Global.Usuario.nombreUsuario.Trim().ToUpper();   // Global.Usuario.nombreUsuario;
                                }
                                else
                                {
                                    //sino abona lo indicado
                                    pagoE.monto = montoPago;
                                }

                                pagoE.caja = Global.Configuracion.caja;
                                pagoE.sucursal = Global.Configuracion.sucursal;

                                pagosModificados.Add(pagoE);
                                doc.tbPagos.Add(pagoE);
                                docsModificados.Add(doc);

                                montoAdeudado -= (decimal)pagoE.monto;
                                montoPago -= (decimal)pagoE.monto;
                                if (montoPago <= 0)
                                {
                                    break;
                                }
                            }




                            if (docsModificados.Count() != 0)
                            {

                                clsAbonos pago = new clsAbonos();
                                pago.abonos = docsModificados;
                                pago.correo = txtCorreo.Text.Trim();
                                pago.cliente = clienteGlobal;
                                pago.montoAbondo = decimal.Parse(txtAbono.Text);
                                pago.saldoPendiente = decimal.Parse(txtPendiente.Text);
                                pago.enviarCoreo = chkEnviarCorreo.Checked;

                                frmCobrar form = new frmCobrar();
                                //en caso de poner cliente sin que se encuentre registrado en al base datos y es tiquete electronico, s
                                //solo para imprimir

                                form.resultadoAbono += respuesta;
                                form.abonos = pago;
                                form.ShowDialog();



                            }

                        }
                        else
                        {
                            MessageBox.Show("Se esta abonando más de lo adeudado", "Error de monto ", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        }
                    }
                    else
                    {
                        MessageBox.Show("El dato de abono es incorrecto, favor verifique.", "Error de datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }


            }
            catch (Exception ex)
            {

                MessageBox.Show("Se produjo un error al realizar el abono, vuelva a intentarlo.", "Error al realizar el abono", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void respuesta(clsAbonos abonos)
        {
            try
            {
                if (abonos != null)
                {
                    txtAbono.Text = string.Empty;
                    txtFacturado.Text = string.Empty;
                    txtPendiente.Text = string.Empty;
                    chkTodos.Checked = false;
                    cargarDatosCliente();

                    try
                    {
                        if (abonos.imprimir)
                        {

                            clsImpresionFactura imprimir = new clsImpresionFactura(docsModificados, clienteGlobal, Global.Usuario.tbEmpresa, decimal.Parse(txtPendiente.Text));
                            imprimir.print();
                        }


                    }
                    catch (Exception)
                    {

                        MessageBox.Show("Error al imprimir el comprobante del Abono.", "Imprimir comprobante", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }


                    try
                    {
                        if (chkEnviarCorreo.Checked)
                        {
                            enviarCorreoAbono(abonos);
                        }
                    }
                    catch (Exception)
                    {

                        MessageBox.Show("Error al enviar el Correo Eletrónico con el Abono.", "Correo Electrónico", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }


                    MessageBox.Show("Datos guardados correctamente.", "Guardar", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Se produjo un error al realizar el abono, vuelva a intentarlo.", "Error al realizar el abono", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
            catch (Exception)
            {

                MessageBox.Show("Se produjo un error al Imprimir ó enviar el Correo Electrónico, vuelva a intentarlo.", "Abonos", MessageBoxButtons.OK, MessageBoxIcon.Error);


            }

        }
        private void enviarCorreoAbono(clsAbonos abono)
        {
            try
            {
                bool enviado = false;
                //se solicita respuesta, y se confecciona el correo a enviar
                decimal NC = 0;
                List<string> listaCorreo = new List<string>();
                listaCorreo.Add(abono.correo);
                string mensaje = string.Format("Fecha Abono: {1}{0}{2}{0}", Environment.NewLine, DateTime.Now, "--------------------------------------------");
                foreach (var item in abono.abonos)
                {
                    NC = 0;
                    var nc = facturacionB.getNCByRef(item.clave);
                    foreach (var notaCredito in nc)
                    {
                        NC += (decimal)notaCredito.tbDetalleDocumento.Sum(x => x.totalLinea);

                    }


                    mensaje += string.Format("#Factura: {1}{0} Monto Facturado: {5}{0} Saldo Anterior: {6}{0} Monto Abonado: {2}{0} Saldo Actual: {7}{0} Estado: {3}{0}{0}{4}{0}{0}",
                        Environment.NewLine,
                        item.id,
                        string.Format("{0:N2}", item.tbPagos.LastOrDefault().monto),
                        Enum.GetName(typeof(Enums.EstadoFactura), item.estadoFactura),
                        "--------------------------------------------",
                        string.Format("{0:N2}", item.tbDetalleDocumento.Sum(x => x.totalLinea) - NC),
                        string.Format("{0:N2}", item.tbDetalleDocumento.Sum(x => x.totalLinea) - NC - item.tbPagos.Sum(y => y.monto) + item.tbPagos.LastOrDefault().monto),
                        string.Format("{0:N2}", (item.tbDetalleDocumento.Sum(x => x.totalLinea) - NC - item.tbPagos.Sum(y => y.monto) + item.tbPagos.LastOrDefault().monto) - (item.tbPagos.LastOrDefault().monto)));

                }


                mensaje += string.Format("Abono Total:{1}{0} Saldo Pendiente: {2}{0}",
                    Environment.NewLine, string.Format("{0:N2}", abono.montoAbondo), string.Format("{0:N2}", abono.saldoPendiente));

                CorreoElectronicoU correo = new CorreoElectronicoU(mensaje, listaCorreo);
                enviado = correo.enviarCorreo();
                if (!enviado)
                {
                    throw new Exception();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al enviar el correo electrónico.", "Correo Electrónico", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            limpiarForm();
        }



        private void checkList(bool chk)
        {
            foreach (var item in lsvFacturas.Items)
            {

                ((ListViewItem)item).Checked = chk;
            }

        }

        private void chkTodos_CheckedChanged(object sender, EventArgs e)
        {
            checkList(chkTodos.Checked);
        }

        private void lsvFacturas_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            //calcularAbono();

        }

        private void calcularAbono()
        {
            decimal total = 0;
            foreach (var item in lsvFacturas.Items)
            {

                if (((ListViewItem)item).Checked)
                {


                    string itemtext = ((ListViewItem)item).SubItems[5].Text.Trim();
                    if (itemtext != string.Empty)
                    {
                        if (Utility.isNumeroDecimal(itemtext))
                        {

                            total += decimal.Parse(itemtext);
                        }

                    }
                }
            }


            txtAbono.Text = total.ToString();
        }

        private void lsvFacturas_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            calcularAbono();
        }

        private void txtAbono_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtAbono.Text == string.Empty)
                {
                    txtPendiente.Text = "0";
                }
                else
                {
                    txtPendiente.Text = (decimal.Parse(txtAdeudado.Text) - decimal.Parse(txtAbono.Text)).ToString();

                }
            }
            catch (Exception)
            {

                txtAbono.ResetText();
            }
        }

        private void TxtIdCliente_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((int)e.KeyChar == (int)Keys.Enter)
            {
                if (txtIdCliente.Text != string.Empty)
                {
                    try
                    {
                        tbClientes clienteGlo = clienteB.GetClienteById(txtIdCliente.Text.Trim());
                        if (clienteGlo != null)
                        {
                            dataBuscar(clienteGlo);
                        }
                        else
                        {
                            MessageBox.Show($"No se encontró el Cliente con el ID: {txtIdCliente.Text.Trim()}", "Buscar Cliente", MessageBoxButtons.OK, MessageBoxIcon.Error);

                            clienteGlo = null;
                            txtIdCliente.Text = string.Empty;
                            txtCliente.Text = string.Empty;
                            txtDireccion.Text = string.Empty;
                            txtTel.Text = string.Empty;
                            txtCorreo.Text = string.Empty;


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

        private void btnsalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtIdCliente_TextChanged(object sender, EventArgs e)
        {

        }
    }
}






