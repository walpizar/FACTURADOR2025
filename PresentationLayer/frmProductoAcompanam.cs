using BusinessLayer;
using CommonLayer;
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

  

    public partial class frmProductoAcompanam : Form
    {
        tbProducto productoGlobal;

        BAcompanamiento acompaIns = new BAcompanamiento();
        BProducto productoAcompaIns = new BProducto();
        List<tbAcompanamiento> _listaAcomp = new List<tbAcompanamiento>();
        List<tbAcompanamiento> _listaAcompProducto = new List<tbAcompanamiento>();

        public frmProductoAcompanam()
        {
            InitializeComponent();
        }

        private void frmProductoAcompanam_Load(object sender, EventArgs e)
        {
            MenuGenerico.CambioEstadoMenu(ref tlsMenu, (int)EnumMenu.OpcionMenu.Modificar);
            Utility.EnableDisableForm(ref groupBox1, false);
            tlsBtnBuscar.Enabled = false;
            tlsBtnCancelar.Enabled = false;
            tlsBtnEliminar.Enabled = false;
            tlsBtnSalir.Enabled = true;

            _listaAcomp = acompaIns.GetListEntities();
            limpiarForm();
         

        }
        public void limpiarForm()
        {
            txtCodigoProducto.Text = string.Empty;
            txtNombre.Text = string.Empty;
            lstvAcomp.Enabled = false;
            _listaAcompProducto.Clear();
            productoGlobal = null;
            cargarLista();
        }

        private void cargarLista()
        {
            lstvAcomp.Items.Clear();
            foreach (tbAcompanamiento item in _listaAcomp)
            {

                ListViewItem p = new ListViewItem();

                p.Text = item.id.ToString();
                p.SubItems.Add(item.nombre.Trim().ToUpper());            
                lstvAcomp.Items.Add(p);
            }
        }

        private void tlsMenu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            accionMenu(e.ClickedItem.Text);
        }
        private void accionMenu(string accion)
        {

            switch (accion)
            {
                case "Guardar":


                    MenuGenerico.CambioEstadoMenu(ref tlsMenu, (int)EnumMenu.OpcionMenu.Modificar);
                    Utility.EnableDisableForm(ref groupBox1, false);
                    tlsBtnBuscar.Enabled = false;
                    tlsBtnCancelar.Enabled = false;
                    tlsBtnEliminar.Enabled = false;
                    tlsBtnSalir.Enabled = true;
                    modificar();
                    break;

                case "Nuevo":

                    MenuGenerico.CambioEstadoMenu(ref tlsMenu, (int)EnumMenu.OpcionMenu.Guardar);
                    Utility.EnableDisableForm(ref groupBox1, true);

                    Utility.ResetForm(ref groupBox1);
                    break;

                case "Modificar":

                    MenuGenerico.CambioEstadoMenu(ref tlsMenu, (int)EnumMenu.OpcionMenu.Guardar);
                    Utility.EnableDisableForm(ref groupBox1, true);
                    tlsBtnSalir.Enabled = true;
                    //tlsBtnCancelar.Enabled = false;



                    break;
                case "Eliminar":

                    MenuGenerico.CambioEstadoMenu(ref tlsMenu, (int)EnumMenu.OpcionMenu.Guardar);

                    break;
                case "Buscar":

                    MenuGenerico.CambioEstadoMenu(ref tlsMenu, (int)EnumMenu.OpcionMenu.Modificar);
                    Utility.EnableDisableForm(ref groupBox1, true);

                    break;
                case "Cancelar":
                 
                    Utility.ResetForm(ref groupBox1);
                    MenuGenerico.CambioEstadoMenu(ref tlsMenu, (int)EnumMenu.OpcionMenu.Modificar);
                    Utility.EnableDisableForm(ref groupBox1, false);
                    tlsBtnBuscar.Enabled = false;
                    tlsBtnCancelar.Enabled = false;
                    tlsBtnEliminar.Enabled = false;
                    tlsBtnSalir.Enabled = true;
                     limpiarForm();

                    break;
                case "Salir":
                    this.Close();
                    break;
            }

        }

        private void modificar()
        {
            try
            {
                if (txtCodigoProducto.Text!=string.Empty) {
                    productoAcompaIns.guardarProductoAcompa(productoGlobal, _listaAcompProducto);
                    MessageBox.Show("Los datos se guardaron correctamente.", "Guardar", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    limpiarForm();


                }
                else
                {
                    MessageBox.Show("No hay datos para guardar.", "Guardar", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

        
            }
            catch (Exception)
            {

                MessageBox.Show("Error al guardar los datos.","Guardar", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
          
        }

        private void btnBuscarProducto_Click(object sender, EventArgs e)
        {
            frmBuscarProducto buscarProduct = new frmBuscarProducto();
            buscarProduct.recuperarEntidad += datosProducto;
            buscarProduct.ShowDialog();
        }

        private void datosProducto(tbProducto producto)
        {
            if (producto != null)
            {

                productoGlobal = producto;
                txtCodigoProducto.Text = producto.idProducto;
                txtNombre.Text = producto.nombre.Trim().ToUpper();
                _listaAcompProducto = productoAcompaIns.getProductoAcompa(producto.idProducto); 

                cargarAcompaProductos();
                lstvAcomp.Enabled = true;


            }
            else
            {
                MessageBox.Show("No se encontró el producto.", "Producutos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
          
        }

        private void cargarAcompaProductos()
        {
            lstvAcomp.Items.Clear();
            foreach (tbAcompanamiento item in _listaAcomp)
            {

                ListViewItem p = new ListViewItem();

                p.Checked = _listaAcompProducto.Where(x => x.id == item.id).SingleOrDefault() != null;
                p.Text = item.id.ToString();
                p.SubItems.Add(item.nombre.Trim().ToUpper());
                lstvAcomp.Items.Add(p);
            }
        }
       

        private void lstvAcomp_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            if (_listaAcompProducto != null)
            {
                ListViewItem item = e.Item as ListViewItem;

                if (item != null)
                {
                    if (item.Checked)
                    {
                        if(_listaAcompProducto.Where(x => x.id == int.Parse(item.Text)).SingleOrDefault() == null)
                        {
                            _listaAcompProducto.Add(_listaAcomp.Where(x => x.id == int.Parse(item.Text)).SingleOrDefault());
                        }
                       

                    }
                    else
                    {
                        if (_listaAcompProducto.Where(x => x.id == int.Parse(item.Text)).SingleOrDefault() != null)
                        {
                            var acom= _listaAcompProducto.Where(x => x.id == int.Parse(item.Text)).SingleOrDefault();
                            _listaAcompProducto.Remove(acom);
                        }
                            
                    }

                }

            }
          
        }
    }
}
