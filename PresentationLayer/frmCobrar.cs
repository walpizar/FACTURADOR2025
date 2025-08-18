using BusinessLayer;
using CommonLayer;
using EntityLayer;
using PresentationLayer.Clases;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace PresentationLayer
{
    public partial class frmCobrar : Form
    {


        //public float totalPagar = 0.0f;

        public tbDocumento facturaGlobal { get; set; }
        public clsAbonos abonos { get; set; }

        BFacturacion facturacionIns = new BFacturacion();
        Bcliente clienteB = new Bcliente();

        public delegate void pasarDatos(tbDocumento documento, bool precio);
        public event pasarDatos recuperarTotal;


        public delegate void pasarDatosAbono(clsAbonos abonos);
        public event pasarDatosAbono resultadoAbono;
        int banderaTipoPago;

        bool bandera = false;
        public frmCobrar()
        {
            InitializeComponent();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }



        //Menu forma de pago
        #region Botones Forma de pago





        #endregion

        //Teclado Numerico
        #region Botones numericos.

        private void btnNum0_Click(object sender, EventArgs e)
        {
            if (txtPago.Text == "0")
            {

            }
            else
            {
                txtPago.Text += "0";
            }
            btnBorrar.Enabled = true;
        }

        private void btnNum1_Click(object sender, EventArgs e)
        {
            if (txtPago.Text == "0")
            {
                txtPago.Text = "1";
                btnNum0.Enabled = true;
            }
            else
            {
                txtPago.Text += "1";
            }
            btnBorrar.Enabled = true;
        }

        private void btnNum2_Click(object sender, EventArgs e)
        {
            if (txtPago.Text == "0")
            {
                txtPago.Text = "2";
                btnNum0.Enabled = true;
            }
            else
            {
                txtPago.Text += "2";
            }
            btnBorrar.Enabled = true;
        }

        private void btnNum3_Click(object sender, EventArgs e)
        {
            if (txtPago.Text == "0")
            {
                txtPago.Text = "3";
                btnNum0.Enabled = true;
            }
            else
            {
                txtPago.Text += "3";
            }
            btnBorrar.Enabled = true;
        }

        private void btnNum4_Click(object sender, EventArgs e)
        {
            if (txtPago.Text == "0")
            {
                txtPago.Text = "4";
                btnNum0.Enabled = true;
            }
            else
            {
                txtPago.Text += "4";
            }
            btnBorrar.Enabled = true;
        }

        private void btnNum5_Click(object sender, EventArgs e)
        {

            if (txtPago.Text == "0")
            {
                txtPago.Text = "5";
                btnNum0.Enabled = true;
            }
            else
            {
                txtPago.Text += "5";
            }
            btnBorrar.Enabled = true;
        }

        private void btnNum6_Click(object sender, EventArgs e)
        {

            if (txtPago.Text == "0")
            {
                txtPago.Text = "6";
                btnNum0.Enabled = true;
            }
            else
            {
                txtPago.Text += "6";
            }
            btnBorrar.Enabled = true;
        }

        private void btnNum7_Click(object sender, EventArgs e)
        {

            if (txtPago.Text == "0")
            {
                txtPago.Text = "7";
                btnNum0.Enabled = true;
            }
            else
            {
                txtPago.Text += "7";
            }
            btnBorrar.Enabled = true;
        }

        private void btnNum8_Click(object sender, EventArgs e)
        {

            if (txtPago.Text == "0")
            {
                txtPago.Text = "8";
                btnNum0.Enabled = true;
            }
            else
            {
                txtPago.Text += "8";
            }
            btnBorrar.Enabled = true;
        }

        private void btnNum9_Click(object sender, EventArgs e)
        {

            if (txtPago.Text == "0")
            {
                txtPago.Text = "9";
                btnNum0.Enabled = true;
            }
            else
            {
                txtPago.Text += "9";
            }
            btnBorrar.Enabled = true;

        }

        private void btnNum_Click(object sender, EventArgs e)
        {

            txtPago.Text += ",";
            btnBorrar.Enabled = true;
        }

        private void btnBorrar_Click(object sender, EventArgs e)
        {
            if (txtPago.Text != "")
            {
                txtPago.Text = txtPago.Text.Substring(0, txtPago.Text.Count() - 1);
                if (txtPago.Text == "")
                {
                    btnBorrar.Enabled = false;
                }
            }

            else if (txtPago.Text == "")
            {
                btnBorrar.Enabled = false;
            }

            btnNum0.Enabled = true;
        }

        #endregion


        private void frmCobrar_Load(object sender, EventArgs e)
        {
            groupBox1.Enabled = true;
            groupBox2.Enabled = true;
            gbxBillete.Enabled = true;

            chkImprimir.Checked = Global.Configuracion.imprime == (int)Enums.EstadoConfig.Si;
            chkImprimir.Visible = Global.Configuracion.imprime == (int)Enums.EstadoConfig.Si;

            if (abonos != null)
            {
                btnCredito.Visible = false;
                lblCredito.Visible = false;
            }
            botonesTipoPagoEstado((int)Enums.TipoPago.Efectivo);
            calcularTotal();
            txtPago.Focus();
            txtPago.Select();
        }

        private void calcularTotal()
        {
            decimal total = 0;
            if (facturaGlobal != null)
            {
                if (facturaGlobal.tbPagos != null)
                {
                    total = (decimal)facturaGlobal.tbDetalleDocumento.Sum(x => x.totalLinea) - (decimal)facturaGlobal.tbPagos.Sum(x => x.monto);
                }
                else
                {
                    total = (decimal)facturaGlobal.tbDetalleDocumento.Sum(x => x.totalLinea);
                }

                //foreach (tbDetalleDocumento detalle in facturaGlobal.tbDetalleDocumento)
                //{
                //    total += detalle.totalLinea;

                //}
            }
            else
            {
                total = abonos.montoAbondo;
            }

            txtMontoTotal.Text = Utility.priceFormat( total);
            txtPago.Text = Utility.priceFormat(total);
        }

        private void botonesTipoPagoEstado(int tipoPago)
        {

            if (tipoPago == (int)Enums.TipoPago.Efectivo)
            {
                banderaTipoPago = (int)Enums.TipoPago.Efectivo;
                gbxBillete.Enabled = true;
                btnContado.Enabled = false;
                btnTarjeta.Enabled = true;
                btnCredito.Enabled = true;
                btnTransferencia.Enabled = true;
                btnSinpe.Enabled = true;

                txtCodTarjeta.Enabled = false;
                txtCodTarjeta.Text = string.Empty;
                txtPago.Text = "0";
                txtVuelto.Text = "0";
                txtPago.Enabled = true;
                txtPlazo.Text = string.Empty;
                txtPlazo.Enabled = false;

                btnContado.BackColor = Color.DarkGray;
                btnSinpe.BackColor = Color.White;
                btnTarjeta.BackColor = Color.White;
                btnCredito.BackColor = Color.White;
   
                if (facturaGlobal != null)
                {
                    facturaGlobal.tipoVenta = (int)Enums.tipoVenta.Contado;
                    facturaGlobal.tipoPago = (int)Enums.TipoPago.Efectivo;
                    facturaGlobal.estadoFactura = (int)Enums.EstadoFactura.Cancelada;


                }
                txtTipoPago.Text = "Contado";

                txtPago.Focus();


            }

            if (tipoPago == (int)Enums.TipoPago.sinpe)
            {
                banderaTipoPago = (int)Enums.TipoPago.sinpe;
                gbxBillete.Enabled = true;
                btnContado.Enabled = true;
                btnSinpe.Enabled = false;
                btnTarjeta.Enabled = true;
                btnCredito.Enabled = true;
                btnTransferencia.Enabled = true;
                btnSinpe.Enabled = true;

                txtCodTarjeta.Enabled = false;
                txtCodTarjeta.Text = string.Empty;
                txtPago.Text = "0";
                txtVuelto.Text = "0";
                txtPago.Enabled = true;
                txtPlazo.Text = string.Empty;
                txtPlazo.Enabled = false;

                btnSinpe.BackColor = Color.DarkGray;
                btnContado.BackColor = Color.White;
                btnTarjeta.BackColor = Color.White;
                btnCredito.BackColor = Color.White;

                if (facturaGlobal != null)
                {
                    facturaGlobal.tipoVenta = (int)Enums.tipoVenta.Contado;
                    facturaGlobal.tipoPago = (int)Enums.TipoPago.sinpe;
                    facturaGlobal.estadoFactura = (int)Enums.EstadoFactura.Cancelada;


                }
                txtTipoPago.Text = "SINPE";

                txtPago.Focus();


            }
            else if (tipoPago == (int)Enums.TipoPago.Tarjeta)
            {

                banderaTipoPago = (int)Enums.TipoPago.Tarjeta;
                gbxBillete.Enabled = false;
                btnTarjeta.Enabled = false;
                btnTransferencia.Enabled = true;
                btnContado.Enabled = true;
                btnCredito.Enabled = true;
                txtCodTarjeta.Enabled = true;
                txtPago.Enabled = true;
                txtPago.Text = "0";
                txtVuelto.Text = "0";
                txtCodTarjeta.Focus();
                txtPlazo.Text = string.Empty;
                txtPlazo.Enabled = false;


                btnContado.BackColor = Color.White;
                btnTarjeta.BackColor = Color.DarkGray;
                btnCredito.BackColor = Color.White;
                btnTransferencia.BackColor = Color.White;
                if (facturaGlobal != null)
                {
                    facturaGlobal.tipoVenta = (int)Enums.tipoVenta.Contado;
                    facturaGlobal.tipoPago = (int)Enums.TipoPago.Tarjeta;
                    facturaGlobal.estadoFactura = (int)Enums.EstadoFactura.Cancelada;
                }


                txtTipoPago.Text = "Tarjeta";
            }
            else if (tipoPago == (int)Enums.TipoPago.Transferencia)
            {
                banderaTipoPago = (int)Enums.TipoPago.Transferencia;
                gbxBillete.Enabled = false;
                btnTarjeta.Enabled = true;
                btnContado.Enabled = true;
                btnCredito.Enabled = true;
                btnTransferencia.Enabled = false;
                txtCodTarjeta.Enabled = true;
                txtPago.Enabled = true;
                txtPago.Text = "0";
                txtVuelto.Text = "0";
                txtCodTarjeta.Focus();
                txtPlazo.Text = string.Empty;
                txtPlazo.Enabled = false;


                btnContado.BackColor = Color.White;
                btnTarjeta.BackColor = Color.White;
                btnCredito.BackColor = Color.White;
                btnTransferencia.BackColor = Color.DarkGray;

                if (facturaGlobal != null)
                {
                    facturaGlobal.tipoVenta = (int)Enums.tipoVenta.Contado;
                    facturaGlobal.tipoPago = (int)Enums.TipoPago.Transferencia;
                    facturaGlobal.estadoFactura = (int)Enums.EstadoFactura.Cancelada;
                }


                txtTipoPago.Text = "Transferencia";
            }

            else if (tipoPago == (int)Enums.TipoPago.Otros)
            {
                banderaTipoPago = (int)Enums.TipoPago.Otros;
                gbxBillete.Enabled = false;
                btnCredito.Enabled = false;
                btnTarjeta.Enabled = true;
                btnContado.Enabled = true;
                btnTransferencia.Enabled = true;
                txtCodTarjeta.Enabled = false;
                txtCodTarjeta.Text = string.Empty;
                txtPago.Text = "0";
                txtVuelto.Text = "0";
                txtPago.Enabled = false;

                int plazoEmpresa = (int)Global.Usuario.tbEmpresa.tbParametrosEmpresa.First().plazoMaximoCredito;
                int plazoCliente = 0;
                if (facturaGlobal.idCliente != null)
                {


                    plazoCliente = facturaGlobal.tbClientes.plazoCreditoMax;
                }

                txtPlazo.Text = plazoCliente > plazoEmpresa ? plazoEmpresa.ToString() : plazoCliente.ToString();

                txtPlazo.Enabled = true;


                btnContado.BackColor = Color.White;
                btnTarjeta.BackColor = Color.White;
                btnCredito.BackColor = Color.DarkGray;
                btnTransferencia.BackColor = Color.White;
                btnCredito.Select();
                facturaGlobal.tipoVenta = (int)Enums.tipoVenta.Credito;
                facturaGlobal.tipoPago = (int)Enums.TipoPago.Efectivo;
                facturaGlobal.estadoFactura = (int)Enums.EstadoFactura.Pendiente;

                txtTipoPago.Text = "Crédito";
            }
            calcularTotal();

        }



        private void txtPago_TextChanged(object sender, EventArgs e)
        {
            try
            {
                
                if (txtPago.Text != "")
                {
                    if (decimal.Parse(txtPago.Text) != decimal.Parse("0"))
                    {
                        //calcular vuelto
                        decimal MontoPago = decimal.Parse(txtPago.Text.Trim());
                        txtVuelto.Text = Utility.priceFormat((MontoPago - decimal.Parse(txtMontoTotal.Text)));
                    }
                }

                else if (txtPago.Text == "")
                {
                    btnNum0.Enabled = true;
                }


            }
            catch (Exception)
            {

                txtPago.ResetText();
            }

        }

        private bool ValidarCampos()
        {
            if (!btnContado.Enabled)
            {
                if (txtPago.Text == string.Empty)
                {

                    MessageBox.Show("Digite el monto con que paga", "Monto de Pago necesario", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    txtPago.Focus();
                    return false;
                }

                if (!Utility.isNumeroDecimal(txtPago.Text))
                {

                    MessageBox.Show("Dato incorrecto", "Monto de Pago necesario", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    txtPago.Focus();
                    return false;
                }
                //if (decimal.Parse(txtVuelto.Text) < 0)
                //{
                //    MessageBox.Show("Monto incorrecto", "Error al procesar solicitud", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                //    txtPago.Focus();
                //    return false;
                //}

            }
            if (!btnSinpe.Enabled)
            {
                if (txtPago.Text == string.Empty)
                {

                    MessageBox.Show("Digite el monto SINPE con que paga", "Monto de Pago necesario", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    txtPago.Focus();
                    return false;
                }
               
                if (!Utility.isNumeroDecimal(txtPago.Text))
                {

                    MessageBox.Show("Dato incorrecto", "Monto de Pago necesario", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    txtPago.Focus();
                    return false;
                }
                //if (decimal.Parse(txtVuelto.Text) < 0)
                //{
                //    MessageBox.Show("Monto incorrecto", "Error al procesar solicitud", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                //    txtPago.Focus();
                //    return false;
                //}

            }
            else if (!btnTarjeta.Enabled)
            {
                if (txtPago.Text == string.Empty)


                    if (txtCodTarjeta.Text == string.Empty)
                    {
                        MessageBox.Show("Codigo de tarjeta no digitado o erroneo", "Error al procesar solicitud", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        txtCodTarjeta.Focus();
                        return false;
                    }
            }
            else if (!btnCredito.Enabled)
            {
                if (facturaGlobal.idCliente == null)
                {
                    MessageBox.Show("Al ser una factura al crédito debe indicar un cliente, favor corrija los datos.", "Error al procesar solicitud", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return false;

                }
                if (txtPlazo.Text == string.Empty || !Utility.isNumerInt(txtPlazo.Text))
                {
                    MessageBox.Show("Debe indicar un plazo para del crédito.", "Plazo crédito", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return false;

                }

                if (facturaGlobal.tbClientes.plazoCreditoMax == 0)
                {
                    MessageBox.Show("El cliente no se le puede aplicar crédito.", "Cliente sin crédito", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return false;

                }

                //if (decimal.Parse(txtMontoTotal.Text) > facturaGlobal.tbClientes.creditoMax)
                //{
                //    MessageBox.Show($"El crédito excede el monto máximo indicado al cliente. Máximo crédito permitido: {facturaGlobal.tbClientes.creditoMax}", "Cliente sin crédito", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                //    return false;

                //}

                //

                var deuda = facturacionIns.validarCredito(facturaGlobal.idCliente, facturaGlobal.tipoIdCliente);
                if ((deuda + decimal.Parse(txtMontoTotal.Text))>= facturaGlobal.tbClientes.creditoMax)
                {
                    MessageBox.Show($"El crédito solicitado excede el monto máximo ({Utility.priceFormat(facturaGlobal.tbClientes.creditoMax)}) indicado al cliente.\n\n Crédito actual: {Utility.priceFormat(deuda)}\n Crédito disponible: {(Utility.priceFormat(facturaGlobal.tbClientes.creditoMax- deuda) )}", "Cliente sin crédito", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return false;


                }



            }

            return true;
        }

        /// <summary>
        /// Valida campos antes de guardar datos
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAceptar_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Desea generar la facturación, ¿Desea continuar?", "Facturación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                cobrar();
            }


        }
        public void cobrar()
        {
            try
            {
                
                if (decimal.Parse(txtMontoTotal.Text)!=0)
                {
                    if (facturaGlobal != null)
                    {
                        if (facturaGlobal.tbPagos == null)
                        {

                            facturaGlobal.tbPagos = new List<tbPagos>();
                        }

                        if (ValidarCampos())
                        {
                            decimal totalPagado = (decimal)facturaGlobal.tbPagos.Sum(x => x.monto);
                            decimal totalFactura = (decimal)facturaGlobal.tbDetalleDocumento.Sum(x => x.totalLinea);
                            decimal pago= decimal.Parse(Utility.priceFormat( decimal.Parse(txtPago.Text)));

                           
                            decimal deuda = decimal.Parse(Utility.priceFormat((totalFactura - totalPagado - pago)));

                            tbPagos pagos = new tbPagos();

                            pagos.caja = Global.Configuracion.caja;
                            pagos.sucursal = Global.Configuracion.sucursal;

                            pagos.estado = true;
                            pagos.fecha_crea = Utility.getDate();
                            pagos.fecha_ult_mod = Utility.getDate();
                            pagos.usuario_crea = Global.Usuario.nombreUsuario.Trim().ToUpper();   // Global.Usuario.nombreUsuario;
                            pagos.usuario_ult_mod = Global.Usuario.nombreUsuario.Trim().ToUpper();   // Global.Usuario.nombreUsuario;
                            pagos.tipoPago = banderaTipoPago;                                                                 //pagos.monto = decimal.Parse(txtPago.Text);

                            if (deuda > 0)
                            {
                                pagos.monto = decimal.Parse(txtPago.Text);
                                groupBox1.Enabled = true;
                                groupBox2.Enabled = true;
                                gbxBillete.Enabled = true;
                            }
                            else
                            {
                                deuda = 0;                               
                                pagos.monto = totalFactura - totalPagado;
                         
                            }

                            if (!btnTarjeta.Enabled)
                            {
                                facturaGlobal.refPago = txtCodTarjeta.Text;
                            }
                            else if (!btnCredito.Enabled)
                            {
                                facturaGlobal.plazo = int.Parse(txtPlazo.Text);
                            }
                            
                            if (pagos.tipoPago == (int)Enums.TipoPago.Tarjeta || pagos.tipoPago == (int)Enums.TipoPago.Transferencia)
                            {
                                facturaGlobal.refPago = txtCodTarjeta.Text;
                             
                            }
                          
                            facturaGlobal.tbPagos.Add(pagos);

                            

                            if (deuda<=0)
                            {

                                //valiado si hay pago a credito para cambiar el estado de factura pendiente y tipo venta a credito
                                if (facturaGlobal.tbPagos.Where(x => x.tipoPago == (int)Enums.TipoPago.Otros).ToList().Count > 0)
                                {

                                    facturaGlobal.tipoVenta = (int)Enums.tipoVenta.Credito;
                                    facturaGlobal.estadoFactura = (int)Enums.EstadoFactura.Pendiente;
                                }
                                //asigno unicamente los pagos que no son creditos, los creditos no son pagos.
                                facturaGlobal.tbPagos = facturaGlobal.tbPagos.Where(x => x.tipoPago != (int)Enums.TipoPago.Otros).ToList();


                                bandera = true;
                                facturaGlobal.tbClientes = null;
                                facturaGlobal = facturacionIns.guadar(facturaGlobal);
                                facturaGlobal = facturacionIns.getEntity(facturaGlobal);
                                try
                                {
                                
                                    if (chkImprimir.Checked)
                                    {
                                        if (facturaGlobal.tbPagos.Count > 1 )
                                        {
                                            pago = decimal.Parse(Utility.priceFormat(totalFactura));

                                        }

                                        bool abrirCajon = facturaGlobal.tbPagos.Where(x=>x.tipoPago==(int)Enums.TipoPago.Efectivo).ToList().Count>0?true:false;

                                        if (facturaGlobal.tipoDocumento == (int)Enums.TipoDocumento.TiqueteElectronico)
                                        {

                                            clsImpresionFactura imprimir = new clsImpresionFactura(facturaGlobal, Global.Usuario.tbEmpresa, pago, decimal.Parse(txtVuelto.Text), abrirCajon);
                                            imprimir.print();
                                        }
                                        else
                                        {
                                            clsImpresionFactura imprimir = new clsImpresionFactura(facturaGlobal, Global.Usuario.tbEmpresa, pago, decimal.Parse(txtVuelto.Text), abrirCajon);
                                            imprimir.print();
                                        }
                                    }
                                    else
                                    {
                                    
                                            clsImpresionFactura imprimir = new clsImpresionFactura();
                                           imprimir.abrirCajon();
                                      
                                        //clsPDF.generarPDFFactura(facturaGlobal);
                                    }



                                }
                                catch (Exception)
                                {
                                    MessageBox.Show("No se pudo imprimir el documento o generar el PDF", "Impresión", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                                recuperarTotal(facturaGlobal, false);
                                this.Close();

                            }
                            else
                            {
                                botonesTipoPagoEstado((int)Enums.TipoPago.Efectivo);
                                calcularTotal();
                                txtPago.Focus();
                                txtPago.Select();

                            }
                        }
                    }
                    else//abono
                    {
                        try
                        {
                            bandera = false;
                            int tipopago = btnContado.Enabled == false ? (int)Enums.TipoPago.Efectivo : (int)Enums.TipoPago.Tarjeta;
                            abonos.imprimir = chkImprimir.Checked;

                            foreach (var doc in abonos.abonos)
                            {
                                doc.tbPagos.LastOrDefault().tipoPago = tipopago;
                                if (tipopago == (int)Enums.TipoPago.Tarjeta)
                                {
                                    doc.tbPagos.LastOrDefault().refPago = txtCodTarjeta.Text;
                                }
                            }


                            facturacionIns.guadarFacturaAbonos(abonos);
                            resultadoAbono(abonos);
                            this.Close();
                        }
                        catch (Exception)
                        {

                            resultadoAbono(null);

                        }


                    }



                }

            }
            catch (Exception ex)
            {

                recuperarTotal(null, false);
            }





        }

        /// <summary>
        /// niega el ingreso de valores no numericos al textbox de pago
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeniedNonNumeric(object sender, KeyPressEventArgs e)
        {

            if ((int)e.KeyChar == (int)Keys.Enter)
            {
                cobrar();
            }
            if (Char.IsDigit(e.KeyChar) || e.KeyChar.ToString() == ".")
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }

        }

        /// <summary>
        /// al presionar la tecla retroceso/backspace este llama a la funcion del boton borrar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Backspace(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Back)
            {
                btnBorrar.PerformClick();
            }
        }

        /// <summary>
        ///  al presionar la tecla retroceso/backspace este llama a la funcion del boton borrar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Backspacebtn(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Back))
            {
                btnBorrar.PerformClick();
            }
        }

        private void btnContado_Click(object sender, EventArgs e)
        {
            botonesTipoPagoEstado((int)Enums.TipoPago.Efectivo);

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void btnTarjeta_Click(object sender, EventArgs e)
        {
            botonesTipoPagoEstado((int)Enums.TipoPago.Tarjeta);
        }

        private void btnCredito_Click(object sender, EventArgs e)
        {
            botonesTipoPagoEstado((int)Enums.TipoPago.Otros);
        }

        private void txtPago_Enter(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            txtPago.Text = "1000";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            txtPago.Text = "5000";
        }

        private void btnCompleto_Click(object sender, EventArgs e)
        {
            txtPago.Text = txtMontoTotal.Text;
        }

        private void btn2000_Click(object sender, EventArgs e)
        {
            txtPago.Text = "2000";
        }

        private void btn10000_Click(object sender, EventArgs e)
        {
            txtPago.Text = "10000";
        }

        private void btn15000_Click(object sender, EventArgs e)
        {
            txtPago.Text = "15000";
        }

        private void btn20000_Click(object sender, EventArgs e)
        {
            txtPago.Text = "20000";
        }

        private void btn50000_Click(object sender, EventArgs e)
        {
            txtPago.Text = "50000";
        }

        private void txtPago_DoubleClick(object sender, EventArgs e)
        {
            txtPago.Text = "";
            txtPago.Focus();
        }

        private void btnTransferencia_Click(object sender, EventArgs e)
        {
            botonesTipoPagoEstado((int)Enums.TipoPago.Transferencia);
        }



        private void frmCobrar_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!bandera)
            {
                if (facturaGlobal != null)
                {
                    if (facturaGlobal.tbPagos != null && facturaGlobal.tbPagos.Count > 0)
                    {
                        MessageBox.Show("Al cerrar se eliminará los cobros realizados", "Cerrar cobro", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                }
            }
        }

        private void btnSinpe_Click(object sender, EventArgs e)
        {
            botonesTipoPagoEstado((int)Enums.TipoPago.sinpe);
        }



        //private void frmCobrar_KeyUp(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.Enter)
        //    {
        //        DialogResult result = MessageBox.Show("Desea generar la facturación, ¿Desea continuar?", "Facturación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        //        if (result == DialogResult.Yes)
        //        {
        //            cobrar();
        //        }

        //    }
        //}
    }
}
