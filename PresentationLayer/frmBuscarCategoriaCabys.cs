using BusinessLayer;
using CommonLayer;
using CommonLayer.DTO;
using CommonLayer.Exceptions.DataExceptions;
using EntityLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PresentationLayer
{

    public partial class frmBuscarCategoriaCabys : Form
    {
        BCategoriaProducto CatProductIns = new BCategoriaProducto();
        //List<tbCategoria1Cabys> cat1;

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
               // cargarCombo();
            }
            catch (Exception)
            {

                MessageBox.Show("Error al buscar el Bien/Servicio", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            //cat1 = CatProductIns.getCat1Cabys();

        }


        private void cargarlista(List<CabysDTO> lists)
        {
            try
            {
                lstvBienServicios.Items.Clear();
                foreach (CabysDTO p in lists)
                {
                    ListViewItem item = new ListViewItem();
                    item.Text = p.Codigo.Trim();
                    item.SubItems.Add(p.Descripcion.Trim());
                    item.SubItems.Add(p.Impuesto.ToString().Trim());

                    lstvBienServicios.Items.Add(item);

                }
            }
            catch (ListEntityException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }



        private void txtbusqueda_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {

                if ((int)e.KeyChar == (int)Keys.Enter)
                {

                    if (txtbusqueda.Text != string.Empty)
                    {
                        var cabys = ConsultasAPI.obtenerCABYS(txtbusqueda.Text);

                       // cargarlista(CatProductIns.getCat9CabysByText(txtbusqueda.Text.Trim().ToUpper()));
                    }
                }
            }
            catch (Exception)
            {

                MessageBox.Show("Error al buscar el Bien/Servicio", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            try
            {
                if (txtbusqueda.Text != string.Empty)
                {

                    List<CabysDTO> lista = await ConsultasAPI.obtenerCABYS(txtbusqueda.Text);
                    cargarlista(lista);

                }
            }
            catch (Exception)
            {

                MessageBox.Show("Error al buscar el Bien/Servicio", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private async void txtbusqueda_MouseEnter(object sender, EventArgs e)
        {
            try
            {
                if (txtbusqueda.Text != string.Empty)
                {

                    List<CabysDTO> lista = await ConsultasAPI.obtenerCABYS(txtbusqueda.Text);
                    cargarlista(lista);

                }
            }
            catch (Exception)
            {

                MessageBox.Show("Error al buscar el Bien/Servicio", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
