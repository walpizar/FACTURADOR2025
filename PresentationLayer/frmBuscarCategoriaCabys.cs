using BusinessLayer;
using CommonLayer;
using CommonLayer.DTO;
using CommonLayer.Exceptions.DataExceptions;
using EntityLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using static CommonLayer.Enums;

namespace PresentationLayer
{

    public partial class frmBuscarCategoriaCabys : Form
    {
        BCategoriaProducto CatProductIns = new BCategoriaProducto();
        List<tbCategoria9Cabys> lista;

        public delegate void pasaDatos(string  codigo);
        public event pasaDatos pasarDatosEvent;
        private string codigo;
        public frmBuscarCategoriaCabys()
        {
            InitializeComponent();

        }

      

        private void frmBuscarCategoriaCabys_Load(object sender, EventArgs e)
        {
            try
            {
                lista = CatProductIns.getCat9Cabys();
                cargarlistaCabysDB(lista);
                cargarCombo();
            }
            catch (Exception)
            {

                MessageBox.Show("Error al buscar el Bien/Servicio", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        private void cargarCombo()
        {
            //comboBox1.DataSource = Enum.GetValues(typeof(Enums.TiposCabys));
        }

        private void cargarlistaCabysDB(List<tbCategoria9Cabys> lists)
        {
            try
            {
                lstvBienServicios.Items.Clear();

                if (lists != null)
                {
                    foreach (tbCategoria9Cabys p in lists)
                    {
                        ListViewItem item = new ListViewItem();
                        item.Text = p.idCategoria9.Trim();
                        item.SubItems.Add(p.nombre.Trim());
                        item.SubItems.Add(p.impuesto.ToString().Trim());
                        string resultado = p.nombre.ToUpper().IndexOf("SERVICIO", StringComparison.OrdinalIgnoreCase) >= 0
                      ? "Servicios"
                      : "Producto";

                        item.SubItems.Add(resultado);

                        lstvBienServicios.Items.Add(item);

                    }

                }

            }
            catch (ListEntityException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cargarlista(List<CabysDTO> lists)
        {
            try
            {
                lstvBienServicios.Items.Clear();

                if(lists != null)
                {
                    foreach (CabysDTO p in lists)
                    {
                        ListViewItem item = new ListViewItem();
                        item.Text = p.Codigo.Trim();
                        item.SubItems.Add(p.Descripcion.Trim());
                        item.SubItems.Add(p.Impuesto.ToString().Trim());
                        // Reemplaza la línea problemática en el método cargarlista:
                        string resultado = p.Descripcion.ToUpper().IndexOf("SERVICIO", StringComparison.OrdinalIgnoreCase) >= 0
                            ? "Servicios"
                            : "Producto";

               


                        item.SubItems.Add(resultado);
                        lstvBienServicios.Items.Add(item);

                    }

                }
               
            }
            catch (ListEntityException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }



     

        private void lstvBienServicios_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (lstvBienServicios.SelectedItems.Count > 0)
                {
                    codigo = lstvBienServicios.SelectedItems[0].Text;
                    pasarDatosEvent(codigo);
                    this.Dispose();

                }
            }
            catch (LicenseException ex)
            {

                MessageBox.Show(ex.Message);
            }


        }

        private void lstvBienServicios_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        private void btnsalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private async void btnBuscar_Click(object sender, EventArgs e)
        {
            //await BuscarAsync();
            var listaFiltrada = lista.FindAll(c => c.idCategoria9.ToUpper().Contains(txtbusqueda.Text.ToUpper()) || c.nombre.ToUpper().Contains(txtbusqueda.Text.ToUpper()));
            cargarlistaCabysDB(listaFiltrada);

        }

        //private async void txtbusqueda_MouseEnter(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (txtbusqueda.Text != string.Empty)
        //        {

        //            int enumValor = (int)comboBox1.SelectedValue; // código numérico del enum
        //            string textoBusqueda = txtbusqueda.Text.Trim();
           

        //            var listaFiltrada = lista.FindAll(c =>
        //                   c.idCategoria9.Contains(textoBusqueda)              // búsqueda general por texto
        //                || c.nombre.Contains(textoBusqueda)                    // búsqueda en nombre
        //                || c.idCategoria9.StartsWith(enumValor.ToString(), StringComparison.OrdinalIgnoreCase) // empieza con código+letra
        //            ); cargarlistaCabysDB(listaFiltrada);

        //        }
        //    }
        //    catch (Exception)
        //    {

        //        MessageBox.Show("Error al buscar el Bien/Servicio", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}

       

        private async Task BuscarAsync()
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(txtbusqueda.Text))
                {
                    btnBuscar.Enabled = false; // 🔒 Deshabilita el botón

                    // Llamada asíncrona al API
                    var cabys = await Task.Run(() => ConsultasAPI.obtenerCABYS(txtbusqueda.Text.Trim()));

                    // Aquí podrías llenar tu lista
                     //cargarlista(CatProductIns.getCat9CabysByText(txtbusqueda.Text.Trim().ToUpper()));
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error al buscar el Bien/Servicio", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnBuscar.Enabled = true; // 🔓 Rehabilita el botón siempre (haya éxito o error)
            }
        }

        private async void txtbusqueda_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                await BuscarAsync();
                e.Handled = true;
                e.SuppressKeyPress = true; // Evita el beep del Enter
            }
        }
    }
}
