using CommonLayer;
using EntityLayer;

using System;
using System.Windows.Forms;

namespace PresentationLayer
{
    public partial class frmRazon : Form
    {
        public tbDocumento _doc { get; set; }
        public delegate void pasaDatos(clsDetalleNC detalle);
        public event pasaDatos pasarDatosEvent;

        public frmRazon()
        {
            InitializeComponent();
        }

        private void frmRazon_Load(object sender, EventArgs e)
        {
            cboTipoPaga.DataSource = Enum.GetValues(typeof(Enums.TipoPagoDevolucion));
            cboTipoPaga.Text = Enum.GetName(typeof(Enums.TipoPagoDevolucion), _doc.tipoPago);
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (validarDatos())
            {
                clsDetalleNC detalle = new clsDetalleNC();
                detalle.razon = txtRazon.Text.Trim();
                detalle.tipoPago = (int)cboTipoPaga.SelectedValue;
                detalle.referencia = txtReferencia.Text.Trim() == string.Empty ? null : txtReferencia.Text.Trim().ToUpper();

                pasarDatosEvent(detalle);
                this.Close();
            }

        }

        private bool validarDatos()
        {
            if (txtRazon.Text == string.Empty)
            {
                MessageBox.Show("Debe indicar obligatoriamente una razón de la nota de crédito", "Faltan datos", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            if (cboTipoPaga.Text == string.Empty)
            {
                MessageBox.Show("Debe indicar el tipo de pago", "Faltan datos", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }

            return true;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
