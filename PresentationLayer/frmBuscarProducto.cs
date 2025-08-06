using BusinessLayer;
using CommonLayer;
using EntityLayer;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace PresentationLayer
{
    public partial class frmBuscarProducto : Form
    {

        BCategoriaProducto CatProductosIns = new BCategoriaProducto();
        //Creamos la instancia global para almacenar al producto bsucado.
        private tbProducto productoiG = new tbProducto();
        private IEnumerable<tbProducto> listaProductos = new List<tbProducto>();
        private List<tbCategoriaProducto> listaCategorias = new List<tbCategoriaProducto>();
        BProducto ProductoIns = new BProducto();
        public int tipoBusqueda { get; set; }

        //Creamos el delegado y evento para pasar la informacion corregida.
        public delegate void pasarDatos(tbProducto entidad);
        //Creamos el delegado y evento para pasar la informacion corregida.
        public delegate void pasarDatosCanti(tbProducto entidad, decimal cant);
        public event pasarDatos recuperarEntidad;

        public event pasarDatosCanti recuperarEntidadCant;

        /// <summary>
        /// Cargamos los productos en la lista.
        /// </summary>
        private void cargarProductos(IEnumerable<tbProducto> listaProductos)
        {
            bool bandera = false;

            //Leemos lista para pasarla al ListView
            foreach (tbProducto item in listaProductos)
            {


                if (Global.busquedaProductos == 0)
                {
                    bandera = true;
                    //Creamos un objeto de tipo ListviewItem
                    ListViewItem p = new ListViewItem();

                    p.Text = item.idProducto.ToString();
                    p.SubItems.Add(item.tbTipoMedidas.nomenclatura.Trim());
                    p.SubItems.Add(item.nombre.Trim());
                    p.SubItems.Add(string.Format("{0:N2}", item.precioVenta1));
                    p.SubItems.Add(string.Format("{0:N2}", item.tbInventario.cantidad));
                    p.SubItems.Add(string.Format("{0:N2}", item.codigoCabys == null ? "No" : "Sí"));
                    //Agregamos el item a la lista.
                    lstvProductos.Items.Add(p);

                }
                else
                {
                    if (item.codigoActividad.Trim() == Global.actividadEconomic.CodActividad.Trim())
                    {
                        bandera = true;
                        //Creamos un objeto de tipo ListviewItem
                        ListViewItem p = new ListViewItem();

                        p.Text = item.idProducto.ToString();
                        p.SubItems.Add(item.tbTipoMedidas.nomenclatura.Trim());
                        p.SubItems.Add(item.nombre.Trim());
                        p.SubItems.Add(string.Format("{0:N2}", item.precioVenta1));
                        p.SubItems.Add(string.Format("{0:N2}", item.tbInventario.cantidad));
                        p.SubItems.Add(string.Format("{0:N2}", item.codigoCabys == null ? "No" : "Sí"));
                        //Agregamos el item a la lista.
                        lstvProductos.Items.Add(p);
                    }


                }


            }

        }


        public frmBuscarProducto()
        {
            InitializeComponent();
        }

        private void frmBuscarProducto_Load(object sender, EventArgs e)
        {

            productoiG = null;

            //Cargamos la lista con los productos.

            cargarCategorias();
            cboCategoriaProducto.SelectedIndex = -1;
            txtNombre.Select();
        }
        private void buscar()
        {

            try
            {
                IEnumerable<tbProducto> productos;
                lstvProductos.Items.Clear();
                if (txtNombre.Text.Trim() == "*")
                {
                    productos = ProductoIns.getProductos((int)Enums.EstadoBusqueda.Activo);

                }
                else
                {
                    productos = ProductoIns.getProductosBusqueda((int)Enums.EstadoBusqueda.Activo, txtNombre.Text.Trim());

                }


                //if (((List<tbProducto>)productos).Count == 0)
                //{

                //   MessageBox.Show("No se encontro el producto.", "Busqueda", MessageBoxButtons.OK, MessageBoxIcon.Information);



                //}

                //if (cboCategoriaProducto.SelectedValue != string.Empty)
                //{

                //    productos = ProductoIns.getListProductoByCategoria(int.Parse(cboCategoriaProducto.SelectedValue.ToString()));

                //}

                cargarProductos(productos);
                listaProductos = productos;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerException.Message);
            }


        }

        private void cargarCategorias()
        {
            if (listaCategorias.Count == 0)
            {
                cboCategoriaProducto.ValueMember = "id";
                cboCategoriaProducto.DisplayMember = "nombre";
                cboCategoriaProducto.DataSource = CatProductosIns.getCategorias((int)Enums.EstadoBusqueda.Activo);
            }

        }

        /// <summary>
        /// Agregamos el producto seleccionado a la entidad global de tipo tbProducto
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSeleccionar_Click(object sender, EventArgs e)
        {
            devolverDatos();


        }

        private void devolverDatos()
        {
            try
            {
                if (lstvProductos.SelectedItems.Count > 0)
                {

                    if (tipoBusqueda == 0)
                    {
                        string id = lstvProductos.SelectedItems[0].Text;
                        productoiG = ProductoIns.GetEntityById(id);
                        recuperarEntidad(productoiG);
                        this.Dispose();
                    }
                    else if (tipoBusqueda == 3)
                    {
                        string id = lstvProductos.SelectedItems[0].Text;
                        productoiG = ProductoIns.GetEntityById(id);
                        recuperarEntidadCant(productoiG, 1);
                        this.Dispose();
                    }
                    else
                    {

                        frmCantidad cantidadfrm = new frmCantidad();
                        cantidadfrm.pasarDatosEvent += cantidadPasarDatos;
                        cantidadfrm.ShowDialog();

                    }


                }
            }
            catch (Exception)
            {

                throw;
            }

        }

        private void cantidadPasarDatos(decimal cant)
        {
            if (cant != decimal.MinValue)
            {
                string id = lstvProductos.SelectedItems[0].Text.Trim();
                productoiG = ProductoIns.GetEntityById(id);
                recuperarEntidadCant(productoiG, cant);
            }

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            productoiG = null;
            this.Close();
        }

        private void txtId_TextChanged(object sender, EventArgs e)
        {
            // buscar();
        }

        private void txtNombre_TextChanged(object sender, EventArgs e)
        {
            ///buscar();
        }

        private void cboCategoriaProducto_SelectedIndexChanged(object sender, EventArgs e)
        {
            buscar();
        }

        private void lstvProductos_MouseDoubleClick(object sender, MouseEventArgs e)
        {

            devolverDatos();
        }

        private void btnLimpiarForm_Click(object sender, EventArgs e)
        {

            txtNombre.Text = string.Empty;
            cboCategoriaProducto.SelectedIndex = -1;
            buscar();
        }

        private void txtNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((int)e.KeyChar == (int)Keys.Enter)
            {

                buscar();
            }
        }

        private void txtId_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((int)e.KeyChar == (int)Keys.Enter)
            {

                buscar();
            }
        }

        private void lblCategoria_Click(object sender, EventArgs e)
        {

        }

        private void cboCategoriaProducto_MouseClick(object sender, MouseEventArgs e)
        {
            cargarCategorias();
        }

      

        private void frmBuscarProducto_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {

                productoiG = null;
                this.Close();

            }
        }
    }
}
