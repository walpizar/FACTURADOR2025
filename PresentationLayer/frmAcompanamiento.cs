using BusinessLayer;
using CommonLayer;
using CommonLayer.Exceptions.BusisnessExceptions;
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
    public partial class frmAcompanamiento : Form
    {
        int bandera = 1;
        tbAcompanamiento acompanamientoGlo = new tbAcompanamiento();
        BAcompanamiento acompaIns = new BAcompanamiento();

        public frmAcompanamiento()
        {
            InitializeComponent();
        }

        private void frmAcompanamiento_Load(object sender, EventArgs e)
        {
            MenuGenerico.CambioEstadoMenu(ref tlsMenu, (int)EnumMenu.OpcionMenu.Nuevo);
            Utility.EnableDisableForm(ref gboDatos, false);
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

                    if (accionGuardar(bandera))
                    {

                        MenuGenerico.CambioEstadoMenu(ref tlsMenu, (int)EnumMenu.OpcionMenu.Nuevo);
                        Utility.EnableDisableForm(ref gboDatos, false);
                        Utility.ResetForm(ref gboDatos);
                    }
                    break;

                case "Nuevo":
                    bandera = 1;
                    MenuGenerico.CambioEstadoMenu(ref tlsMenu, (int)EnumMenu.OpcionMenu.Guardar);
                    Utility.EnableDisableForm(ref gboDatos, true);
                    txtCodigo.Enabled = false;
                    Utility.ResetForm(ref gboDatos);
                    break;

                case "Modificar":
                    bandera = 2;
                    MenuGenerico.CambioEstadoMenu(ref tlsMenu, (int)EnumMenu.OpcionMenu.Guardar);
                    Utility.EnableDisableForm(ref gboDatos, true);
                    txtCodigo.Enabled = false;
                    break;
                case "Eliminar":
                    bandera = 3;
                    MenuGenerico.CambioEstadoMenu(ref tlsMenu, (int)EnumMenu.OpcionMenu.Guardar);

                    break;
                case "Buscar":

                    buscarAcompanamiento();

                    if (acompanamientoGlo.descripcion != null)
                    {
                        MenuGenerico.CambioEstadoMenu(ref tlsMenu, (int)EnumMenu.OpcionMenu.Modificar);
                        Utility.EnableDisableForm(ref gboDatos, false);
                    }

                    break;
                case "Cancelar":
                    MenuGenerico.CambioEstadoMenu(ref tlsMenu, (int)EnumMenu.OpcionMenu.Nuevo);
                    Utility.EnableDisableForm(ref gboDatos, false);
                    Utility.ResetForm(ref gboDatos);

                    acompanamientoGlo = null;




                    break;
                case "Salir":
                    this.Close();
                    break;
            }

        }
        private bool accionGuardar(int trans)
        {

            bool action = false;

            switch (trans)
            {

                case 1:
                    action = guardar();

                    break;

                case 2:
                    action = actualizar();

                    break;


                case 3:
                    action = eliminar();
                    break;
            }

            return action;


        }
        private bool validarCampos()
        {

            if (txtNombre.Text == String.Empty)
            {
                MessageBox.Show("Debe ingresar el nombre del acompañamiento.", "Datos incompletos", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                txtNombre.Focus();
                return false;
            }


            return true;

        }
        private bool actualizar()
        {
            bool isOK = false;
            try
            {
                if (validarCampos())
                {
                    //Actualizamos la informacion de la entidad y enviamos a la base de datos.
                    acompanamientoGlo.descripcion = txtDescripcion.Text.Trim().ToUpper();
                    acompanamientoGlo.nombre = txtNombre.Text.Trim().ToUpper();
                    acompanamientoGlo.estado = true;


                    acompanamientoGlo.fecha_ult_mod = Utility.getDate();
                    acompanamientoGlo.usuario_ult_mod = Global.Usuario.nombreUsuario;


                    acompanamientoGlo = acompaIns.actualizar(acompanamientoGlo);


                    MessageBox.Show("El acompañamiento ha sido actualizado en la base de datos.", "Actualización.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    isOK = true;

                }


            }

            catch (Exception ex)
            {

                MessageBox.Show("Ha ocurrido un error. Comuniquese con el administrador " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                isOK = false;

            }

            return isOK;

        }
        private bool eliminar()
        {
            bool isOK = false;
            try
            {
                //Actualizamos la informacion de la entidad y enviamos a la base de datos.
                acompanamientoGlo.estado = false;

                acompanamientoGlo.fecha_ult_mod = Utility.getDate();
                acompanamientoGlo.usuario_ult_mod = Global.Usuario.nombreUsuario;


                acompanamientoGlo = acompaIns.actualizar(acompanamientoGlo);


                if (acompanamientoGlo != null)
                {
                    MessageBox.Show("La acompañamiento ha sido elimnado del sistema.", "Eliminación", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    isOK = true;

                }


            }

            catch (Exception ex)
            {

                MessageBox.Show("Ha ocurrido un error. Comuniquese con el administrador " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                isOK = false;
            }

            return isOK;
        }

        private bool guardar()
        {
            bool isOK = false;

            if (validarCampos())
            {
                tbAcompanamiento acompa = new tbAcompanamiento();

                try
                {
                    //Creamos la nueva instancia del objeto tbCategoriaProducto

                    acompa.nombre = txtNombre.Text.Trim().ToUpper();
                    acompa.descripcion = txtDescripcion.Text.Trim().ToUpper();


                    //LLenamos los campos de auditoria.
                    acompa.estado = true;
                    acompa.fecha_crea = Utility.getDate();
                    acompa.fecha_ult_mod = Utility.getDate();
                    acompa.usuario_ult_mod = Global.Usuario.nombreUsuario;
                    acompa.usuario_crea = Global.Usuario.nombreUsuario;

                    acompa = acompaIns.guarda(acompa);


                    MessageBox.Show("El acompañamiento ha sido almacenado en la base de datos.", "Categoria guardada.", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    isOK = true;
                }

                catch (EntityExistException ex)
                {
                    if (MessageBox.Show("La categoria ya existe, ¿Desea actualizarla?", "Existe categoria", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {

                        acompanamientoGlo = acompaIns.Getentity(acompa);

                        actualizar();

                        isOK = true;


                    }
                    else
                    {
                        isOK = false;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ha ocurrido un error. Comuniquese con el administrador " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    isOK = false;
                }
            }
            else
            {
                isOK = false;
            }


            return isOK;
        }

        private void buscarAcompanamiento()
        {

            frmBuscarAcompa buscar = new frmBuscarAcompa();
            buscar.recuperamosEntidad += recuperarEntidadBuscada;
            buscar.ShowDialog();

        }

        private void recuperarEntidadBuscada(tbAcompanamiento entity)
        {



            acompanamientoGlo = entity;

            if (acompanamientoGlo != null)
            {
                txtCodigo.Text = acompanamientoGlo.id.ToString().Trim();
                txtDescripcion.Text = acompanamientoGlo.descripcion.Trim();
                txtNombre.Text = acompanamientoGlo.nombre.Trim();






            }


        }
    }
}
