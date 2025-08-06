using BusinessLayer;
using EntityLayer;
using System;
using System.Windows.Forms;

namespace PresentationLayer
{
    public partial class frmDetalleLineaCompra : Form
    {
        bTipoMedidas medidaIns = new bTipoMedidas();
        public tbDetalleCompras detalle { get; set; }
        public frmDetalleLineaCompra()
        {
            InitializeComponent();
        }

        private void frmDetalleLineaCompra_Load(object sender, EventArgs e)
        {
            cargarDatos();

        }

        private void cargarDatos()
        {
            txtlinea.Text = detalle.numLinea.ToString();
            txtCodigoProducto.Text = detalle.idProducto.Trim();
            txtIdProductoProveedor.Text = detalle.idProductoProveedor.Trim();
            txtNombeProducto.Text = detalle.nombreProducto.Trim().ToUpper();

            tbTipoMedidas medida = new tbTipoMedidas();
            medida.idTipoMedida = detalle.idMedida;
            medida = medidaIns.GetEnityById(medida);
            txtMedida.Text = medida.nomenclatura.Trim().ToUpper();

            txtCantidad.Text = string.Format("{0:N2}", detalle.cantidad);
            txtPrecio.Text = string.Format("{0:N2}", detalle.precio);
            txtMontoTotal.Text = string.Format("{0:N2}", detalle.montoTotal);
            txtDesc.Text = string.Format("{0:N2}", detalle.montoTotaDesc);
            txtImpEsp.Text = string.Format("{0:N2}", detalle.montoOtroImp);
            txtImp.Text = string.Format("{0:N2}", detalle.montoTotalImp);
            txtExo.Text = string.Format("{0:N2}", detalle.montoTotalExo);
            txtTotalLinea.Text = string.Format("{0:N2}", detalle.montoTotalLinea);





        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
