using BusinessLayer;
using EntityLayer;
using System;
using System.Windows.Forms;

namespace PresentationLayer
{
    public partial class frmAprobacion : Form
    {

        tbUsuarios usuarioB = new tbUsuarios();
        BUsuario insBUsuario = new BUsuario();

        public delegate void pasarDatosAprob(bool aprob);//DELEGADO
        public event pasarDatosAprob pasarDatosEvent;//EVENTO

        public frmAprobacion()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
        private bool Validar()
        {
            if (txtUsuario.Text == string.Empty)
            {
                MessageBox.Show("Debe ingresar el nombre de usuario");
                txtUsuario.Focus();
                return false;
            }
            if (txtContraseña.Text == string.Empty)
            {
                MessageBox.Show("Debe ingresar contraseña");
                txtContraseña.Focus();
                return false;
            }

            return true;
        }
        private void btnAceptar_Click(object sender, EventArgs e)
        {
            validarDatos();
        }

        private void validarDatos()
        {
            if (Validar())
            {
                tbUsuarios login = new tbUsuarios();
                login.nombreUsuario = txtUsuario.Text.ToString();
                login.contraseña = txtContraseña.Text.Trim();
                login = insBUsuario.getLoginUsuario(login);


                if (login != null)
                {

                    if (login.idRol == 1)
                    {
                        pasarDatosEvent(true);
                    }
                    else
                    {
                        pasarDatosEvent(false);
                    }


                }
                else
                {
                    MessageBox.Show("Usuario o Contraseña incorrectos.","Aprobación", MessageBoxButtons.OK,MessageBoxIcon.Stop);
                    pasarDatosEvent(false);
                }

            }
            this.Close();
        }

        private void btnDenegar_Click(object sender, EventArgs e)
        {
            pasarDatosEvent(false);
            this.Close();
        }

        private void frmAprobacion_Load(object sender, EventArgs e)
        {
            txtUsuario.Focus();
        }

        private void txtContraseña_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((int)e.KeyChar == (int)Keys.Enter)
            {
                validarDatos();
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
