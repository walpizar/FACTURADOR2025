using BusinessLayer;
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
    public partial class frmBuscarAcompa : Form
    {


        tbAcompanamiento acompaGlobal = new tbAcompanamiento();
        List<tbAcompanamiento> listaAcompa = new List<tbAcompanamiento>();

        BAcompanamiento acompaIns = new BAcompanamiento();


        //Creamos el delegado y el evento
        public delegate void pasarDatos(tbAcompanamiento entity);

        public event pasarDatos recuperamosEntidad;



        public frmBuscarAcompa()
        {
            InitializeComponent();
        }

        private void frmBuscarAcompa_Load(object sender, EventArgs e)
        {
            cargarLista();
        }
        private void cargarLista()
        {

            listaAcompa = acompaIns.getAcompanamiento(1);
            foreach (tbAcompanamiento p in listaAcompa)
            {

                //Creamos un nuevo Item del tipo ListViewItem
                ListViewItem itemNuevo = new ListViewItem();

                itemNuevo.Text = p.id.ToString();
                itemNuevo.SubItems.Add(p.nombre.Trim());

                if (p.estado == true)
                {
                    itemNuevo.SubItems.Add("Activa");

                }
                else
                {
                    itemNuevo.SubItems.Add("Inactiva");
                }

                //Agregamops el itme a la lista.
                lstvAcompa.Items.Add(itemNuevo);


            }


        }

        private void lstvAcompa_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstvAcompa.SelectedItems.Count > 0)
            {

                //Seleccionamos el indice 1 del elemento selccionado.
                int select = Convert.ToInt16(lstvAcompa.SelectedItems[0].Text);
                foreach (tbAcompanamiento p in listaAcompa)
                {

                    if (p.id == select)
                    {
                        acompaGlobal = p;

                    }

                }
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            acompaGlobal = null;

            this.Close();
        }

        private void btnEjecutar_Click(object sender, EventArgs e)
        {
            recuperamosEntidad(acompaGlobal);

            this.Dispose();
        }


    }
}
