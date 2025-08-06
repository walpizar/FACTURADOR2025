using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PresentationLayer
{
    public partial class frmEstadoPromocion : Form
    {
        public delegate void pasaDatos(int estado);
        public event pasaDatos pasarDatosEvent;
        public frmEstadoPromocion()
        {
            InitializeComponent();
        }

        private void frmEstadoPromocion_Load(object sender, EventArgs e)
        {
            cboEstado.SelectedIndex = 0;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            pasarDatosEvent(int.MinValue);
            this.Close();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            

            pasarDatosEvent((int)cboEstado.SelectedIndex);
            this.Close();

           
        }
    }
}
