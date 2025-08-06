using BusinessLayer;
using CommonLayer;
using EntityLayer;
using PresentationLayer.Clases;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace PresentationLayer
{
    public partial class frmEstadoCaja : Form
    {
        bool aprobacionAdmin = false;
        private DateTime fechaIncio { get; set; }
        private DateTime fechaFin { get; set; }
        private DateTime fechaFinIncioCaja { get; set; }
        private int sucursal { get; set; }
        private int caja { get; set; }
        public tbCajasMovimientos cajaCierre { get; set; }


        BMovimiento movIns = new BMovimiento();
  


        public frmEstadoCaja()
        {
            InitializeComponent();
        }

        private void txtCompras_TextChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void frmEstadoCaja_Load(object sender, EventArgs e)
        {
             
            bool cierreCajaAdmin = (bool)Global.Usuario.tbEmpresa.tbParametrosEmpresa.FirstOrDefault().cierreCajaAdmin;
       

            if (cajaCierre == null && Global.Configuracion.mov!=null)
            {
                cajaCierre = cajaCierre == null ? Global.Configuracion.mov : cajaCierre;

            }


            if (cajaCierre!=null)
            {
               

                fechaIncio = cajaCierre.fechaApertura;
                fechaFin = (DateTime)(cajaCierre.fechaCierre == null ? Utility.getDate() : cajaCierre.fechaCierre);

                fechaFinIncioCaja= (DateTime)(cajaCierre.fechaCierre == null ? DateTime.MinValue : cajaCierre.fechaCierre);


                lblSucursal.Text = cajaCierre.sucursal.ToString();
                lblCaja.Text = cajaCierre.caja.ToString();
                 sucursal = cajaCierre.sucursal;
                caja = cajaCierre.caja;

                txtFecha.Visible = true;
                dtpFecha.Visible = false;

                txtFecha.Text = fechaIncio.ToString();

            }
            else
            {
                fechaIncio = Utility.getDate().Date;
                fechaFin = Utility.getDate().Date.AddDays(1);
                fechaFinIncioCaja = DateTime.MinValue;

                lblSucursal.Text = Global.Configuracion.sucursal.ToString();
                lblCaja.Text = Global.Configuracion.caja.ToString();
                sucursal = Global.Configuracion.sucursal;
                caja = Global.Configuracion.caja;

                txtFecha.Visible = false;
                dtpFecha.Visible = true;
                dtpFecha.Value = fechaIncio;

            }           
          
       

            if (cierreCajaAdmin)
            {

                btnCerrarCaja.Enabled = true;
            }
            else
            {
                btnCerrarCaja.Enabled = cajaCierre == null ? false : (cajaCierre.usuarioApertura.Trim().ToUpper() == Global.Usuario.nombreUsuario.Trim().ToUpper() || Global.Usuario.idRol == (int)Enums.roles.Administracion);

            }

            btnReImprimir.Visible = Global.Configuracion.imprime == 1 && Global.Configuracion.imprimeDoc == 1;

            cargarDatos();
         
        }

        private void cargarDatos()
        {
            try
            {
          
                BFacturacion facturaIns = new BFacturacion();
                BMovimiento bMovimiento = new BMovimiento();
                IEnumerable<tbDocumento> listaDoc = new List<tbDocumento>();
                IEnumerable<tbPagos> listaPagos = new List<tbPagos>();
                IEnumerable<tbMovimientos> listaMov = new List<tbMovimientos>();
          
                decimal inicioCaja = 0, entradaDinero = 0, salidaDinero = 0, notasCredito = 0, notasCreditoContado = 0, notasCreditoTransf = 0, notasCreditoTarjeta = 0,
                    contado = 0, tarjeta = 0, credito = 0, transf = 0;

                #region ELIMINADO
                /*  return list.Where(x => (x.tipoDocumento == (int)Enums.TipoDocumento.Factura || x.tipoDocumento == (int)Enums.TipoDocumento.FacturaElectronica
                      || x.tipoDocumento == (int)Enums.TipoDocumento.TiqueteElectronico) && x.fecha >= fecha);*/
                //if (cajaCierre==null )
                //{


                //    listaDoc = facturaIns.getDocsByNoTrack(sucursal, caja, fechaIncio, fechaFin);


                //    inicioCaja = bMovimiento.getIncioCajaByFecha(fechaIncio, DateTime.MinValue,sucursal,caja).FirstOrDefault().montoApertura;

                //    listaA = facturaIns.getAbonosByFechaAsNotTracking(fechaIncio, fechaFin, sucursal, caja);
                //    listaMov = bMovimiento.getListMovByFechaAsNotTracking(fechaIncio, fechaFin, sucursal, caja);


                //}
                //else
                //{
                //    inicioCaja = bMovimiento.getIncioCajaByFecha(cajaCierre.fechaApertura, (DateTime)cajaCierre.fechaCierre, sucursal, caja).FirstOrDefault().montoApertura;

                //    listaDoc = facturaIns.getDocsByNoTrack(sucursal, caja, cajaCierre.fechaApertura, (DateTime)cajaCierre.fechaCierre);
                //   // inicioCaja = cajaCierre.montoApertura;
                //    listaA = facturaIns.getAbonosByFechaAsNotTracking(cajaCierre.fechaApertura, (DateTime)cajaCierre.fechaCierre, sucursal, caja);
                //    listaMov = bMovimiento.getListMovByFechaAsNotTracking(cajaCierre.fechaApertura, (DateTime)cajaCierre.fechaCierre, sucursal, caja);

                //}
                #endregion

                //inicioCaja = bMovimiento.getIncioCajaByFecha(fechaIncio, fechaFinIncioCaja, sucursal, caja).FirstOrDefault().montoApertura;

                inicioCaja = cajaCierre == null ? 0 : cajaCierre.montoApertura;

                listaDoc = facturaIns.getDocsByNoTrack(sucursal, caja, fechaIncio, fechaFin);             
                listaPagos = facturaIns.getAbonosByFechaAsNotTracking(fechaIncio, fechaFin, sucursal, caja);
                listaMov = bMovimiento.getListMovByFechaAsNotTracking(fechaIncio, fechaFin, sucursal, caja);

                IEnumerable<tbDocumento> lista = listaDoc.Where(x => (x.tipoDocumento == (int)Enums.TipoDocumento.Factura || x.tipoDocumento == (int)Enums.TipoDocumento.FacturaElectronica
                       || x.tipoDocumento == (int)Enums.TipoDocumento.TiqueteElectronico) && x.fecha >= fechaIncio).ToList();
                         
                IEnumerable<tbDocumento> listaNC = listaDoc.Where(x => (x.tipoDocumento == (int)Enums.TipoDocumento.NotaCredito || x.tipoDocumento == (int)Enums.TipoDocumento.NotaCreditoElectronica
                   ) && x.fecha >= fechaIncio).ToList();
                
              
                entradaDinero = listaMov.Where(x => x.idTipoMov == (int)Enums.tipoMovimiento.EntradaDinero).Sum(y => y.total);
                salidaDinero = listaMov.Where(x => x.idTipoMov == (int)Enums.tipoMovimiento.SalidaDinero).Sum(y => y.total);

            
                contado = (decimal)listaPagos.Where(x=> x.tipoPago == (int)Enums.TipoPago.Efectivo).Sum(x => x.monto);
                tarjeta = (decimal)listaPagos.Where(x => x.tipoPago == (int)Enums.TipoPago.Tarjeta).Sum(x => x.monto);
                transf = (decimal)listaPagos.Where(x => x.tipoPago == (int)Enums.TipoPago.Transferencia).Sum(x => x.monto);
          
                //creditos
                foreach (var doc in lista)
                {
                    if (cajaCierre==null)
                    {
                        credito += (decimal)doc.tbDetalleDocumento.Sum(x => x.totalLinea) - (decimal)doc.tbPagos.Where(z => z.fecha_crea >= fechaIncio).Sum(y => y.monto);

                    }
                    else
                    {
                        credito += (decimal)doc.tbDetalleDocumento.Sum(x => x.totalLinea) - (decimal)doc.tbPagos.Where(z => z.fecha_crea >= fechaIncio && z.fecha_crea<=fechaFin).Sum(y => y.monto);
                    }
                }


                //notas de credito

                foreach (var item in listaNC)
                {

                    if (item.tipoPago == (int)Enums.TipoPago.Efectivo)
                    {
                        notasCreditoContado += item.tbDetalleDocumento.Sum(y => y.totalLinea);
                    }
                    else if (item.tipoPago == (int)Enums.TipoPago.Tarjeta)
                    {
                        notasCreditoTarjeta += item.tbDetalleDocumento.Sum(y => y.totalLinea);

                    }
                    else if (item.tipoPago == (int)Enums.TipoPago.Transferencia)
                    {
                        notasCreditoTransf += item.tbDetalleDocumento.Sum(y => y.totalLinea);

                    }

                }

                notasCredito = notasCreditoContado + notasCreditoTransf + notasCreditoTarjeta;

                txtIncioCaja.Text = Utility.priceFormat(inicioCaja);

                txtContado.Text = Utility.priceFormat(contado);
                txtTarjeta.Text = Utility.priceFormat(tarjeta);
                txtCredito.Text = Utility.priceFormat(credito);
                txtTransf.Text = Utility.priceFormat(transf);

                txtNCContado.Text = Utility.priceFormat(notasCreditoContado);
                txtNCTransf.Text = Utility.priceFormat(notasCreditoTransf);
                txtNCTarjeta.Text = Utility.priceFormat(notasCreditoTarjeta);


                txtEntradaDinero.Text = Utility.priceFormat(entradaDinero);
                txtSalidaDinero.Text = Utility.priceFormat(salidaDinero);


                txtTotalVentas.Text = Utility.priceFormat((contado + tarjeta + credito + transf));
                txtTotalNC.Text = Utility.priceFormat(notasCredito);
                txtEntradaSalida.Text = Utility.priceFormat((entradaDinero - salidaDinero));


                txtTotalNeto.Text = Utility.priceFormat(((contado + tarjeta + credito + transf + entradaDinero+ inicioCaja) - (salidaDinero + notasCredito)));
                txtTotalBanco.Text = Utility.priceFormat(((tarjeta + transf) - (notasCreditoTransf + notasCreditoTarjeta)));
                txtTotalCaja.Text = Utility.priceFormat(((contado + entradaDinero+ inicioCaja) - (salidaDinero + notasCreditoContado)));


            }
            catch (Exception ex)
            {

                MessageBox.Show("Error al cargar los datos.");
            }

        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void imprimirCaja(bool abre)
        {
            List<string> lista = new List<string>();
            lista.Add("VENTAS");
            lista.Add("Efectivo: " + txtContado.Text.Trim());
            lista.Add("Tarjeta: " + txtTarjeta.Text.Trim());
            lista.Add("Créditos: " + txtCredito.Text.Trim());
            lista.Add("Transferencia: " + txtTransf.Text.Trim());
            lista.Add("SALTO");
            lista.Add("ENTRADA/SALIDA CAJA");
            lista.Add("Entrada Dinero: " + txtEntradaDinero.Text.Trim());
            lista.Add("Salida Dinero: " + txtSalidaDinero.Text.Trim());
            lista.Add("SALTO");
            lista.Add("NOTAS DE CRÉDITO");
            lista.Add("Efectivo: " + txtNCContado.Text.Trim());
            lista.Add("Tarjeta: " + txtNCTarjeta.Text.Trim());
            lista.Add("Transferencia: " + txtNCTransf.Text.Trim());
            lista.Add("SALTO");
            lista.Add("GENERAL");
            lista.Add("Inicio de Caja: " + txtIncioCaja.Text.Trim());
            lista.Add("Total de Ventas: " + txtTotalVentas.Text.Trim());
            lista.Add("Total Notas de Crédito: " + txtTotalNC.Text.Trim());
            lista.Add("Total Entrada/Salida: " + txtEntradaSalida.Text.Trim());
            lista.Add("Total Neto: " + txtTotalNeto.Text.Trim());
            lista.Add("Total Banco: " + txtTotalBanco.Text.Trim());
            lista.Add("Total Caja: " + txtTotalCaja.Text.Trim());

            clsImpresionFactura imprimir = new clsImpresionFactura(Global.Usuario.tbEmpresa, lista, abre);
            imprimir.print();

        }

        private void btnReImprimir_Click(object sender, EventArgs e)
        {
            imprimirCaja(false);

        }

        private void dtpFecha_ValueChanged(object sender, EventArgs e)
        {
            fechaIncio = dtpFecha.Value.Date;
            cargarDatos();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult result = MessageBox.Show("Desea realizar el cierre de la caja?", "Cierre de Caja", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result== DialogResult.Yes)
                {
                    bool adminCierra = (bool)Global.Usuario.tbEmpresa.tbParametrosEmpresa.FirstOrDefault().cierreCajaAdmin;

                    if (adminCierra)
                    {
                        frmAprobacion aprobacionForm = new frmAprobacion();
                        aprobacionForm.pasarDatosEvent += respuestaAprobacion;
                        aprobacionForm.ShowDialog();


                    }
                    else
                    {
                        aprobacionAdmin = true;
                    }

                    if (aprobacionAdmin)
                    {
                        tbCajasMovimientos movimiento = Global.Configuracion.mov;
                        movimiento.fechaCierre = Utility.getDate();
                        movimiento.montoCierre = decimal.Parse(txtTotalCaja.Text);
                        movimiento.usuarioCierre = Global.Usuario.nombreUsuario.Trim().ToUpper();
                        movIns.GuardarCierreCaja(movimiento);
                        if ((bool)Global.Usuario.tbEmpresa.imprimeDoc && Global.Configuracion.imprimeDoc == 1)
                        {
                            imprimirCaja(true);

                        }

                        if ((bool)Global.Usuario.tbEmpresa.tbParametrosEmpresa.FirstOrDefault().correoCierre)
                        {
                            String mensaje = string.Format("Empresa:{18}{0}Fecha:{17}{0}Sucursal:{19}{0}Caja:{20}{0}Usuario:{21}{0}{0}VENTAS:{0}Contado: {1}{0}Tarjeta: {2}{0}Créditos: {3}{0}Transferencia:{4}{0}{0}ENTRADA/SALIDA CAJA:{0}" +
                                "Entrada Dinero: {5}{0}Salida Dinero: {6}{0}{0}" +
                                "NOTAS DE CRÉDITO:{0}Efectivo: {7}{0}Tarjeta: {8}{0}Transferencia: {9}{0}{0}GENERAL{0}Inicio de Caja: {10}{0}Total de Ventas: {11}{0}" +
                                "Total Notas de Crédito: {12}{0}Total Entrada/Salida: {13}{0}Total Neto: {14}{0}Total Banco: {15}{0}Total Caja: {16}{0}",
                                Environment.NewLine, txtContado.Text.Trim(), txtTarjeta.Text.Trim(), txtCredito.Text.Trim(), txtTransf.Text.Trim()
                                , txtEntradaDinero.Text.Trim(), txtSalidaDinero.Text.Trim(), txtNCContado.Text.Trim(), txtNCTarjeta.Text.Trim(), txtNCTransf.Text.Trim()
                                , txtIncioCaja.Text.Trim(), txtTotalVentas.Text.Trim(), txtTotalNC.Text.Trim(), txtEntradaSalida.Text.Trim(), txtTotalNeto.Text.Trim()
                                , txtTotalBanco.Text.Trim(), txtTotalCaja.Text.Trim(), Utility.getDate(), Global.actividadEconomic.nombreComercial.Trim().ToUpper(),
                                Global.Configuracion.sucursal.ToString(), Global.Configuracion.caja.ToString(), Global.Usuario.nombreUsuario);

                            List<string> correos = new List<string>();
                            correos.Add(Global.actividadEconomic.correoCompras.Trim());
                            //correos.Add("walpizar@hotmail.com");
                            clsDocumentoCorreo _docImp = new clsDocumentoCorreo(correos);
                            string subject = string.Format("ESTADO CIERRE CAJA ({0}) SUCURSAL: {1} CAJA: {2} - ESPARTANO FACTURADOR", Utility.getDate().ToString(), Global.Configuracion.sucursal.ToString(), Global.Configuracion.caja.ToString());

                            CorreoElectronico.enviarCorreoCierreCaja(_docImp, mensaje, subject);

                        }
                        aprobacionAdmin = false;
                        MessageBox.Show("Se realizó correctamente el cierre de caja", "Cierre de Caja", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Global.cerrar = true;
                        Application.Exit();
                       

                    }



                   

                }
                aprobacionAdmin = false;

            }
            catch (Exception)
            {

                MessageBox.Show("Ocurrio un error al cerrar la caja.", "Cierre de Caja", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
          
        }

        private void respuestaAprobacion(bool aprob)
        {
            aprobacionAdmin = aprob;
        }

        private void groupBox4_Enter(object sender, EventArgs e)
        {
            
            
        }

        private void btnsalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
