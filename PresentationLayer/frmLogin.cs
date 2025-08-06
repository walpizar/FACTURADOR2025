using BusinessLayer;
using CommonLayer;
using CommonLayer.Licencia;
using EntityLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace PresentationLayer
{
    public partial class frmLogin : Form
    {
        //nombramos las variables e instancias necesarias para acceder a los objetos necesarios
        tbUsuarios usuarioB = new tbUsuarios();
        BUsuario insBUsuario = new BUsuario();
        BActividadesEconomicas actINs = new BActividadesEconomicas();
        BEmpresa empresaIns = new BEmpresa();
        int i = 1;
        int intentos = 3;

        //Creo delegados correspondientes
        public delegate void cerrarFormFacturacion();
        public event cerrarFormFacturacion cerrarFact;

        public delegate void cargarPermisos(tbUsuarios usu);
        public event cargarPermisos permisosEvent;

        BActividadesEconomicas actIns = new BActividadesEconomicas();

        public frmLogin()
        {
            InitializeComponent();
        }

        //private void CargarNumCaja()
        //{
        //    BCajas insCaja = new BCajas();
        //    cboNumCaja.DisplayMember = "nombre";
        //    cboNumCaja.ValueMember = "id";  
        //    cboNumCaja.DataSource = insCaja.getListTipoing((int)Enums.EstadoBusqueda.Activo);
        //}

        //Validamos el ingreso de datos
        private bool Validar()
        {
            if (txtUsuario.Text == string.Empty)
            {
                MessageBox.Show("Debe ingresar el nombre de usuario");
                txtUsuario.Focus();
                return false;
            }


            if (txtContraseña.Text == string.Empty)
            {
                MessageBox.Show("Debe ingresar contraseña");
                txtContraseña.Focus();
                return false;
            }


            //if (cboNumCaja.Text == string.Empty)
            //{
            //    MessageBox.Show("Debe ingresar el número de caja");
            //    cboNumCaja.Focus();
            //    return false;
            //}
            return true;
        }

        //El numero de caja lo cargamos dinamicamente desde base de datos
        private void frmLogin_Load(object sender, EventArgs e)
        {

            cargarDatos();
            txtUsuario.Select();
            //ingresar();
        }

        private void cargarDatos()
        {

        }

        //Con este metodo verificamos la auteticidad de que el usuario ya existe
        private void btnEntrar_Click(object sender, EventArgs e)
        {
            try
            {
                ingresar();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Login", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void ingresar()
        {
            if (Validar())
            {
                tbUsuarios login = new tbUsuarios();
                login.nombreUsuario = txtUsuario.Text.ToString();
                login.contraseña = txtContraseña.Text.Trim();
                login = insBUsuario.getLoginUsuario(login);

                int status = 1;
                if (login != null)
                {
                    tbEmpresa empresa = new tbEmpresa();
                    empresa.id = login.idEmpresa;
                    empresa.tipoId = (int)login.idTipoIdEmpresa;
                    login.tbEmpresa = empresaIns.getEntity(empresa);

                    //Licencias.GetLicencia(login.idEmpresa.Trim()); /*heroku*/
            

                    DateTime fechaCaducidad;
                    if (Global.licenciaWeb)
                    {
                        var licenciaServer = Global.licencias.Where(x => x.id.Trim() == empresa.id.Trim()).FirstOrDefault();
                        fechaCaducidad = licenciaServer==null ? DateTime.MinValue : licenciaServer.fechaCaducidad;

                        if (fechaCaducidad == DateTime.MinValue)
                        {
                            fechaCaducidad = (DateTime)login.tbEmpresa.fechaCaducidad;
                            status = 1;
                        }
                        else
                        {
                            if (fechaCaducidad.Date != login.tbEmpresa.fechaCaducidad.Value.Date)
                            {
                                login.tbEmpresa.fechaCaducidad = fechaCaducidad;
                                login.tbEmpresa = empresaIns.ActualizarEmpresa(login.tbEmpresa);
                            }
                            status = licenciaServer.status;
                        }
                    }
                    else
                    {
                        status = 1;
                        fechaCaducidad = (DateTime)login.tbEmpresa.fechaCaducidad;
                    }


                    if (/*fechaCaducidad > Utility.getDate() &&*/ status != 0)
                    {

                        Global.Usuario = insBUsuario.getLoginUsuario(login);
                        //Global.sucursal = int.Parse(Global.Usuario.tbEmpresa.tbParametrosEmpresa.FirstOrDefault().sucursal); 
                        //Global.NumeroCaja = int.Parse(Global.Usuario.tbEmpresa.tbParametrosEmpresa.FirstOrDefault().caja); 

                        List<tbEmpresaActividades> listaAct = actINs.getListaEmpresaActividad(login.idEmpresa, (int)login.idTipoIdEmpresa);
                        if (listaAct.Count == 0)
                        {
                            MessageBox.Show("No existen actividades económicas aplicadas a este usuario");
                            Global.cerrar = true;
                            Application.Exit();

                        }

                        Global.multiActividad = listaAct.Count > 1;
                        Global.actividadEconomic = listaAct.Where(x => x.principal == true).FirstOrDefault();
                        Global.actividadPrincial = listaAct.Where(x => x.principal == true).FirstOrDefault().CodActividad;




                        this.Close();

                    }
                    else
                    {
                        MessageBox.Show("El licenciamiento del producto ha caducado o ha sido bloqueado, favor contactar con la empresa", "Vencimiento licenciamiento", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        Global.cerrar = true;
                        Application.Exit();

                    }

                }
                else
                {
                    //Creamos un ciclo para dar un numero determinado de intentos antes de que se cierre el formulario
                    if (i < intentos)
                    {
                        MessageBox.Show("Usuario o contraseña inválidos", "Autenticación", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        limpiar();
                        txtUsuario.Focus();

                    }
                    else
                    {
                        Global.cerrar = true;
                        Application.Exit();
                        //desahabilitar y cerrar

                    }

                    i += 1;
                }

            }
        }



        //Limpiamos el formulario de login
        private void limpiar()
        {
            txtUsuario.Text = string.Empty;
            txtContraseña.Text = string.Empty;
            //cboNumCaja.ResetText();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Global.cerrar = true;
            Application.Exit();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void txtContraseña_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void txtUsuario_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtContraseña_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((int)e.KeyChar == (int)Keys.Enter)
            {
                ingresar();

            }
        }

        private void cboNumCaja_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
