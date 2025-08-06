using BusinessLayer;
using CommonLayer;
using CommonLayer.Exceptions.DataExceptions;
using EntityLayer;
using System;
using System.Windows.Forms;

namespace PresentationLayer
{
    public partial class frmInicioCaja : Form
    {
        BMovimiento movIns = new BMovimiento();
        public frmInicioCaja()
        {
            InitializeComponent();
        }




        public Boolean guardarCajaUsuario()
        {
            tbCajasMovimientos mov = new tbCajasMovimientos();

            try
            {

                if (Utility.isNumeroDecimal(txtTotal.Text))
                {
                    mov.fechaApertura = Utility.getDate();
                    mov.montoApertura = decimal.Parse(txtTotal.Text);
                    mov.caja = Global.Configuracion.caja;
                    mov.sucursal = Global.Configuracion.sucursal;
                    //mov. = Global.Configuracion.sucursal;

                    mov.usuarioApertura = Global.Usuario.nombreUsuario.Trim().ToUpper();
                    



                    Global.Configuracion.mov=movIns.GuardarInicioCaja(mov);
                    MessageBox.Show("Inicio de caja realizado correctamente.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    return true;
                    // BCajaUsuMonedasIns.guardarListaCajaUsuarioMoneda(listaCajaUsuMone);

                }
                else
                {
                    txtTotal.Focus();
                    MessageBox.Show("Datos incorrectos");
                    return false;
                }


            }

            catch (SaveEntityException ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }






        private void txtTotal_Validated(object sender, EventArgs e)
        {
            if (Utility.isNumeroDecimal(txtTotal.Text))
            {
                txtTotal.Text = string.Format("{0:N2}", decimal.Parse(txtTotal.Text));
            }
        }

        private void frmInicioCaja_Load(object sender, EventArgs e)
        {

        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (guardarCajaUsuario())
            {
                this.Close();

            }

        }

        private void frmInicioCaja_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    if (guardarCajaUsuario())
                    {
                        this.Close();

                    }
                }
                catch (Exception)
                {

                    MessageBox.Show("Error al iniciar la caja", "Inicio Caja", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }
    }
}
