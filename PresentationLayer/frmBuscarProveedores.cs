﻿using BusinessLayer;
using CommonLayer;
using EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
namespace PresentationLayer
{
    public partial class frmBuscarProveedores : Form
    {
        tbProveedores proveedorGlo = new tbProveedores();
        public static tbPersona personaGlo = new tbPersona();
        List<tbProveedores> listaproveedor = new List<tbProveedores>();
        public BProveedores proveedorIns = new BProveedores();
        bool banderaSeleccionar = false;


        //puntero o delegado
        public delegate void pasarDatos(tbProveedores entity);

        //evento
        public event pasarDatos pasarDatosEvent;


        public frmBuscarProveedores()
        {
            InitializeComponent();
        }



        private void btnSeleccionar_Click(object sender, EventArgs e)
        {
            banderaSeleccionar = true;
            pasarDatosEvent(proveedorGlo);
            this.Dispose();

        }

        private void lstvProveedores_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstvProveedores.SelectedItems.Count > 0)
            {
                string Selected = lstvProveedores.SelectedItems[0].Text;
                foreach (tbProveedores nombrepro in listaproveedor)
                {
                    if (Selected == nombrepro.id)
                    {
                        proveedorGlo = nombrepro;
                        btnSeleccionar.Enabled = true;
                        break;
                    }
                }
            }
        }




        public void cargarLista1(List<tbProveedores> lista)
        {

            // listaproveedor = proveedorIns.getListProveedor((int)Enums.EstadoBusqueda.Todos);
            lstvProveedores.Items.Clear();
            foreach (tbProveedores p in lista)
            {
                ListViewItem item = new ListViewItem();
                item.Text = p.id.ToString();
                if (p.tipoId == (int)Enums.TipoId.Fisica ||
                         p.tipoId == (int)Enums.TipoId.Dimex)
                {
                    item.SubItems.Add(p.tbPersona.nombre.Trim() + " " + p.tbPersona.apellido1.Trim() + " " + p.tbPersona.apellido2.Trim());

                }
                else
                {
                    item.SubItems.Add(p.tbPersona.nombre.Trim());
                }

                lstvProveedores.Items.Add(item);
            }
        }


        private void frmBuscarProveedores_Load(object sender, EventArgs e)
        {
            listaproveedor = proveedorIns.getListProveedor((int)Enums.EstadoBusqueda.Activo);

            cargarLista1(listaproveedor);


        }

        private void lstvProveedores_MouseDoubleClick(object sender, MouseEventArgs e)
        {

            banderaSeleccionar = true;
            pasarDatosEvent(proveedorGlo);
            this.Dispose();



        }

        private void cerraFormBuscar(object sender, FormClosedEventArgs e)
        {
            if (!banderaSeleccionar == true)
            {
                proveedorGlo = null;
                pasarDatosEvent(proveedorGlo);
            }
        }

        private void buscar()
        {

            List<tbProveedores> listaBuscar = new List<tbProveedores>();
            //trim elimina espacios en blanco
            if (txtBuscar.Text.Trim() != string.Empty)
            {

                listaBuscar = listaproveedor.Where(x => x.tbPersona.nombre.Trim().ToUpper().Contains(txtBuscar.Text.Trim().ToUpper()) ||
                x.tbPersona.identificacion.Trim().ToUpper().Contains(txtBuscar.Text.Trim().ToUpper())).ToList();

                cargarLista1(listaBuscar);
            }
            else
            {
                //carga la lista completa
                cargarLista1(listaproveedor);
            }

        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            buscar();
        }
    }
}
