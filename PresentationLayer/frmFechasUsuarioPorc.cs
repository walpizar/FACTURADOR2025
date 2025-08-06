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
    public partial class frmFechasUsuarioPorc : Form
    {
        public tbUsuarios usuario { get; set; }

        public delegate void pasaDatos(DateTime fechaInicio, DateTime fechaFin, int porcentaje, string usuarios);
        public event pasaDatos pasarDatosEvent;


        public frmFechasUsuarioPorc()
        {
            InitializeComponent();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                buscarUsuario();
            }
            catch (Exception)
            {

                MessageBox.Show("Error al cargar el producto/Servicio", "Buscar Producto", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buscarUsuario()
        {
            frmBuscarUsuario frmBuscar = new frmBuscarUsuario();
            //Asignamos el metodo a la lista en el evento.
            frmBuscar.pasarDatosEvent += recuperarUsuario;
            frmBuscar.ShowDialog();

        }

        private void recuperarUsuario(tbUsuarios entity)
        {
            if (entity != null)
            {
                usuario = entity;
                txtUsuario.Text = $"{ usuario.tbPersona.nombre } { usuario.tbPersona.apellido1 } { usuario.tbPersona.apellido2 }";

            }
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (validar())
            {
                pasarDatosEvent(dtpInicio.Value, dtpFin.Value,int.Parse(txtPorce.Value.ToString()), usuario.nombreUsuario.Trim());
                this.Close();

            }
        }

        private bool validar()
        {
            if (usuario == null )
            {
                MessageBox.Show("Debe indicar el Usuario.", "Faltan datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (txtPorce.Value <= 0)
            {
                MessageBox.Show("El porcentaje debe ser mayor que 0.", "Faltan datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
