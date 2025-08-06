using CommonLayer;
using System;
using System.Windows.Forms;

namespace PresentationLayer
{
    public partial class frmCantidad : Form
    {

        public delegate void pasaDatos(decimal peso);
        public event pasaDatos pasarDatosEvent;

        public decimal peso { get; set; }
        public frmCantidad()
        {
            InitializeComponent();
        }

        private void frmCantidad_Load(object sender, EventArgs e)
        {
            txtCantidad.Focus();
            txtCantidad.Text = "1.00";
            txtCantidad.Select();
        }





        private void aceptar()
        {

            if (Utility.isNumeroDecimal(txtCantidad.Text))
            {
                peso = decimal.Parse(txtCantidad.Text);
                pasarDatosEvent(peso);
                this.Close();

            }
            else
            {
                MessageBox.Show("Cantidad incorrecta, verifique", "Error cantidad", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        private void txtCantidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((int)e.KeyChar == (int)Keys.Enter)
            {
                aceptar();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            pasarDatosEvent(decimal.MinValue);
            this.Close();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            aceptar();
        }

        private void frmCantidad_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                pasarDatosEvent(decimal.MinValue);
                this.Close();

            }
    
        }

       
    }
}
