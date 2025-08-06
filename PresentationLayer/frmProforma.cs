using BusinessLayer;
using CommonLayer;
using EntityLayer;
using PresentationLayer.Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace PresentationLayer
{
    public partial class frmProforma : Form
    {

        public tbDocumento facturaGlobal = new tbDocumento();
        BFacturacion facturacionIns = new BFacturacion();
        public delegate void pasarDatos(tbDocumento documento, bool precio);
        public event pasarDatos recuperarTotal;
        Bcliente clienteB = new Bcliente();
        tbClientes cliente;

        public frmProforma()
        {
            InitializeComponent();
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmProforma_Load(object sender, EventArgs e)
        {
            chkImprimir.Checked = Global.Configuracion.imprime == (int)Enums.EstadoConfig.Si;
            chkImprimir.Visible = Global.Configuracion.imprime == (int)Enums.EstadoConfig.Si;
            chkDuplicar.Enabled = facturaGlobal.tipoDocumento == (int)Enums.TipoDocumento.ProformaGeneral || facturaGlobal.tipoDocumento == (int)Enums.TipoDocumento.Proforma;
            txtDias.Focus();
            cargarDatos();
        }

        private void cargarDatos()
        {
            cliente = clienteB.GetClienteById(facturaGlobal.idCliente, (int)facturaGlobal.tipoIdCliente);

            facturaGlobal.tbClientes = cliente;
            if (facturaGlobal.tipoIdCliente == 1)
            {
                txtCliente.Text = cliente.tbPersona.nombre.Trim().ToUpper() + " " + cliente.tbPersona.apellido1.Trim().ToUpper() + " " + cliente.tbPersona.apellido2.Trim().ToUpper();

            }
            else
            {
                txtCliente.Text = cliente.tbPersona.nombre.Trim().ToUpper();

            }


            txtMonto.Text = facturaGlobal.tbDetalleDocumento.Sum(x => x.totalLinea).ToString();

            txtDias.Text = Global.Usuario.tbEmpresa.tbParametrosEmpresa.First().plazoMaximoProforma.ToString();


        }

        private void btnProcesar_Click(object sender, EventArgs e)
        {

            try
            {
                DialogResult resp = MessageBox.Show($"Esta seguro que desea realizar la PROFORMA por el MONTO: {txtMonto.Text} al CLIENTE: { txtCliente.Text}", "Generar Proforma", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (resp == DialogResult.Yes)
                {

                    if (ValidarCampos())
                    {

                        if (chkDuplicar.Checked)
                        {
                            facturaGlobal.tipoDocumento = (int)Enums.TipoDocumento.Proforma;
                            facturaGlobal.id = 0;
                        }
                        if (chkGeneral.Checked)
                        {
                            facturaGlobal.tipoDocumento = (int)Enums.TipoDocumento.ProformaGeneral;
                        }

                        facturaGlobal.plazo = int.Parse(txtDias.Text);

                        facturaGlobal.notificarCorreo = chkCorreo.Checked;

                        facturacionIns.guadar(facturaGlobal);
                        try
                        {
                            if (chkImprimir.Checked)
                            {
                                facturaGlobal = facturacionIns.getProformasByID(facturaGlobal.id, facturaGlobal.tipoDocumento);
                                clsImpresionFactura imprimir = new clsImpresionFactura(facturaGlobal, cliente, Global.Usuario.tbEmpresa, chkConPrecio.Checked);
                                imprimir.print();
                            }

                        }
                        catch (Exception)
                        {

                            MessageBox.Show("Error al imprimir la proforma", "Proforma", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        }

                        try
                        {
                            if (chkCorreo.Checked)
                            {
                                List<string> correos = new List<string>();
                                if (facturaGlobal.correo1 != null)
                                {
                                    correos.Add(facturaGlobal.correo1);

                                }
                                if (facturaGlobal.correo2 != null)
                                {
                                    correos.Add(facturaGlobal.correo2);

                                }


                                clsDocumentoCorreo docCorreo = new clsDocumentoCorreo(facturaGlobal, correos, true, (int)Enums.tipoAdjunto.factura, chkConPrecio.Checked);

                                CorreoElectronico.enviarCorreo(docCorreo);
                                MessageBox.Show("La proforma se ha enviado correctamente.", "Proforma", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                         
                        }
                        catch (Exception)
                        {

                            MessageBox.Show("No se pudo enviar la proforma por correo electrónico. Favor reintentar desde el módulo de Documentos Emitidos.", "Proforma", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }


                        recuperarTotal(facturaGlobal, chkConPrecio.Checked);
                        this.Close();

                    }

                }

            }
            catch (Exception)
            {

                recuperarTotal(null, false);
            }


        }

        private bool ValidarCampos()
        {

            if (txtDias.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Debe indica la cantidad de días de valides de la proforma", "Faltan datos", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;


            }
            if (!Utility.isNumerInt(txtDias.Text))
            {

                MessageBox.Show("Datos incorrecto", "Faltan datos", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;


            }

            int dias = (int)Global.Usuario.tbEmpresa.tbParametrosEmpresa.First().plazoMaximoProforma;


            if (int.Parse(txtDias.Text) > dias)
            {

                MessageBox.Show($"La cantidad de días máximo de plazo para las proformas es de: {dias}, corrija los datos.", "Datos incorrectos", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;


            }



            return true;
        }
    }
}
