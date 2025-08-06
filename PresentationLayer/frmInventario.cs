
using BusinessLayer;
using CommonLayer;
using CommonLayer.Exceptions.DataExceptions;
using EntityLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace PresentationLayer
{
    public partial class frmInventario : Form
    {
        //
        //ESTE ES
        //
        public static int banderalis;
        public static List<tbInventario> listainve = new List<tbInventario>();
        public static tbInventario inveGlo = new tbInventario();
        public BInventario inveIns = new BInventario();
        tbInventario inventarioGlo = new tbInventario();
        BCategoriaProducto CatProductosIns = new BCategoriaProducto();

        public frmInventario()
        {
            InitializeComponent();
        }


        private void frmPuestos_Load(object sender, EventArgs e)
        {

            MenuGenerico.CambioEstadoMenu(ref tlsMenu, (int)EnumMenu.OpcionMenu.Modificar);
            Utility.EnableDisableForm(ref gbxInventario, false);
            tlsBtnBuscar.Enabled = false;
            tlsBtnCancelar.Enabled = false;
            tlsBtnEliminar.Enabled = false;
            tlsBtnSalir.Enabled = true;

        }



        public void cargarLista(List<tbInventario> lista)
        {

            try
            {

                ltvCargar.Items.Clear();

                foreach (tbInventario u in lista)
                {



                    ListViewItem item = new ListViewItem();
                    item.Text = u.idProducto.ToString().Trim();
                    item.SubItems.Add(u.tbProducto.nombre.Trim().ToUpper().ToString());
                    item.SubItems.Add(u.tbProducto.tbTipoMedidas.nomenclatura.Trim().ToUpper().ToString());
                    item.SubItems.Add(u.tbProducto.tbCategoriaProducto.nombre.Trim().ToUpper().ToString());
                    item.SubItems.Add(u.cantidad.ToString());
                    item.SubItems.Add(u.cant_max.ToString());
                    item.SubItems.Add(u.cant_min.ToString());
                    ltvCargar.Items.Add(item);


                }
            }
            catch (ListEntityException ex)
            {
                MessageBox.Show("Ha ocurrido un error. Comuniquese con el administrador " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        public void modificar()
        {

            try
            {
                if (banderalis == 1)
                {
                    inveIns.modificar(listainve);
                    cargarLista(listainve);

                    MessageBox.Show("Los datos han sido actualizados en la base de datos.", "Actualización.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    banderalis = 0;
                }
                else
                {
                    MessageBox.Show("No hay datos que actualizar", "Actualización.", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }
            catch (UpdateEntityException ex)
            {
                MessageBox.Show("Ha ocurrido un error. Comuniquese con el administrador " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void accionMenu(string accion)
        {

            switch (accion)
            {
                case "Guardar":


                    MenuGenerico.CambioEstadoMenu(ref tlsMenu, (int)EnumMenu.OpcionMenu.Modificar);
                    Utility.EnableDisableForm(ref gbxInventario, false);
                    tlsBtnBuscar.Enabled = false;
                    tlsBtnCancelar.Enabled = false;
                    tlsBtnEliminar.Enabled = false;
                    tlsBtnSalir.Enabled = true;
                    modificar();
                    break;

                case "Nuevo":

                    MenuGenerico.CambioEstadoMenu(ref tlsMenu, (int)EnumMenu.OpcionMenu.Guardar);
                    Utility.EnableDisableForm(ref gbxInventario, true);

                    Utility.ResetForm(ref gbxInventario);
                    break;

                case "Modificar":

                    MenuGenerico.CambioEstadoMenu(ref tlsMenu, (int)EnumMenu.OpcionMenu.Guardar);
                    Utility.EnableDisableForm(ref gbxInventario, true);
                    tlsBtnSalir.Enabled = true;
                    //tlsBtnCancelar.Enabled = false;



                    break;
                case "Eliminar":

                    MenuGenerico.CambioEstadoMenu(ref tlsMenu, (int)EnumMenu.OpcionMenu.Guardar);

                    break;
                case "Buscar":

                    MenuGenerico.CambioEstadoMenu(ref tlsMenu, (int)EnumMenu.OpcionMenu.Modificar);
                    Utility.EnableDisableForm(ref gbxInventario, true);

                    break;
                case "Cancelar":
                    listainve.Clear();
                    Utility.ResetForm(ref gbxInventario);
                    MenuGenerico.CambioEstadoMenu(ref tlsMenu, (int)EnumMenu.OpcionMenu.Modificar);
                    Utility.EnableDisableForm(ref gbxInventario, false);
                    tlsBtnBuscar.Enabled = false;
                    tlsBtnCancelar.Enabled = false;
                    tlsBtnEliminar.Enabled = false;
                    tlsBtnSalir.Enabled = true;
                    listainve = inveIns.GetListEntities(1);
                    cargarLista(listainve);

                    break;
                case "Salir":
                    this.Close();
                    break;
            }

        }


        private void tlsMenu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            accionMenu(e.ClickedItem.Text);
        }



        private void ltvCargar_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (ltvCargar.SelectedItems.Count > 0)
            {

                string idSelected = ltvCargar.SelectedItems[0].Text;



                //foreach (tbInventario inve in listainve)
                //{

                //    if (int.Parse(idSelected) == inve.idInventario)
                //    {
                //        inveGlo = inve;

                //    }
                //}

            }

        }





        //private void buscar(int codigo, int busqueda)
        //{
        //    List<tbInventario> listaBuscar = new List<tbInventario>();
        //    //trim elimina espacios en blanco

        //    if (busqueda == 1)
        //    {
        //        if (txtBuscar.Text.Trim() != string.Empty)
        //        {

        //            foreach (tbInventario p in listainve)
        //            {
        //                if (p.tbProducto.nombre.ToUpper().Contains(txtBuscar.Text.ToUpper().Trim()))
        //                {

        //                    listaBuscar.Add(p);
        //                }

        //            }
        //            cargarLista(listaBuscar);
        //        }
        //        else
        //        {
        //            //carga la lista completa
        //            cargarLista(listainve);
        //        }
        //    }
        //    else if (busqueda == 2)
        //    {
        //        if ((int)cboCategoriaProducto.SelectedValue != -1)
        //        {

        //            foreach (tbInventario p in listainve)
        //            {
        //                if (p.tbProducto.id_categoria == codigo)
        //                {

        //                    listaBuscar.Add(p);
        //                }

        //            }
        //            cargarLista(listaBuscar);
        //        }
        //        else
        //        {
        //            //carga la lista completa
        //            cargarLista(listainve);
        //        }


        //    }
        //    else {
        //        if (txtCodigo.Text != string.Empty)
        //        {

        //            foreach (tbInventario p in listainve)
        //            {
        //                if (p.tbProducto.idProducto.Trim().ToUpper().Contains( txtCodigo.Text.ToUpper().Trim()))
        //                {

        //                    listaBuscar.Add(p);
        //                }

        //            }
        //            cargarLista(listaBuscar);
        //        }
        //        else
        //        {
        //            //carga la lista completa
        //            cargarLista(listainve);
        //        }


        //    }




        //}

        private void gbxInventario_Enter(object sender, EventArgs e)
        {

        }
        private void ltvCargar_SelectedIndex(object sender, MouseEventArgs e)
        {

            string idProd = string.Empty;


            if (ltvCargar.SelectedItems.Count > 0)
            {
                idProd = ltvCargar.SelectedItems[0].Text;
                inventarioGlo = listainve.Where(x => x.idProducto.Trim() == idProd.Trim()).SingleOrDefault();
                if (inventarioGlo != null)
                {

                    frmModificarInventario cargar = new frmModificarInventario();
                    cargar.inventarioGlobal = inventarioGlo;
                    cargar.pasarDatosEvent += datosBuscar;
                    cargar.ShowDialog();

                }


            }



            //cargarLista(listainve);


            //ss
        }

        private void datosBuscar(tbInventario inventario)
        {

            listainve.Remove(inventarioGlo);
            listainve.Add(inventario);
            cargarLista(listainve);
            banderalis = 1;

        }





        private void txtCodigo_Click(object sender, EventArgs e)
        {
            txtCodigo.Text = string.Empty;
        }

        private void txtCodigo_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void txtCodigo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((int)e.KeyChar == (int)Keys.Enter)
            {

                buscar();
            }
        }

        private void buscar()
        {
            try
            {
                listainve = inveIns.getInvetarioByQuery(txtCodigo.Text.Trim().ToUpper());
                cargarLista(listainve);
            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}

