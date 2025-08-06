using EntityLayer;
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
    public partial class frmFechasInicioFinProducto : Form
    {
        public delegate void pasaDatos(DateTime fechaInicio, DateTime fechaFin, string codigoProducto);
        public event pasaDatos pasarDatosEvent;


        private string _codigoProducto { get; set; }

        public frmFechasInicioFinProducto()
        {
            InitializeComponent();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                buscarProducto();
            }
            catch (Exception)
            {

                MessageBox.Show("Error al cargar el producto/Servicio", "Buscar Producto", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void buscarProducto()
        {

            //bool isOK = false;

            frmBuscarProducto buscarProduct = new frmBuscarProducto();

            //Asignamos el metodo a la lista en el evento.
            buscarProduct.recuperarEntidadCant += recuperarEntidad;
            buscarProduct.tipoBusqueda = 3;
            buscarProduct.ShowDialog();

            //if (productoGlo.idProducto == string.Empty)
            //{
            //    isOK = false;
            //}
            //else
            //{
            //    isOK = true;
            //}

            //return isOK;

        }

        private void recuperarEntidad(tbProducto entidad, decimal cant)
        {
            //Cargamos los valores a la entidad.
            if (entidad != null)
            {
                _codigoProducto = entidad.idProducto;
                txtProducto.Text = entidad.idProducto.ToString() + "-" + entidad.nombre.Trim().ToUpper();

            }
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (validar())
            {
                pasarDatosEvent(dtpInicio.Value, dtpFin.Value, _codigoProducto);
                this.Close();

            }
        }
        private bool validar()
        {
            if (_codigoProducto ==null || _codigoProducto==string.Empty)
            {
                MessageBox.Show("Debe indicar un producto/servicio.", "Faltan datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (dtpFin.Value.Date < dtpInicio.Value.Date)
            {
                MessageBox.Show("La fecha de inicio es mayor que la fecha fin.", "Faltan datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
