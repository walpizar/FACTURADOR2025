using CommonLayer;
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
    public partial class frmFechaInicoFinTipoDoc : Form
    {
        public delegate void pasaDatos(DateTime fechaInicio, DateTime fechaFin, int tipo);
        public event pasaDatos pasarDatosEvent;


        public frmFechaInicoFinTipoDoc()
        {
            InitializeComponent();
        }

        private void frmFechaInicoFinTipoDoc_Load(object sender, EventArgs e)
        {
            comboBox1.DataSource = Enum.GetValues(typeof(Enums.TipoReporteHacienda));
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (validar())
            {
                pasarDatosEvent(dtpInicio.Value, dtpFin.Value, (int)comboBox1.SelectedValue);
                this.Close();

            }
        }

        private bool validar()
        {
            
            if (dtpFin.Value.Date < dtpInicio.Value.Date)
            {
                MessageBox.Show("La fecha de inicio es mayor que la fecha fin.", "Faltan datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;

        }
    }
}
