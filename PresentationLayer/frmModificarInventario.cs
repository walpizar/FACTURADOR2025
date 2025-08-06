﻿using BusinessLayer;
using CommonLayer;
using EntityLayer;
using System;
using System.Windows.Forms;

namespace PresentationLayer
{





    public partial class frmModificarInventario : Form
    {



        public tbInventario inventarioGlobal = new tbInventario();
        BInventario inveIns = new BInventario();
        public delegate void pasarDatos(tbInventario entity);

        // evento
        public event pasarDatos pasarDatosEvent;


        public frmModificarInventario()
        {
            InitializeComponent();
        }

        private void frmModificar_Load(object sender, EventArgs e)
        {
            cargarInventario();
        }


        public void cargarInventario()
        {
            if (inventarioGlobal != null)
            {
                txtIdentificacion.Text = inventarioGlobal.idProducto.ToString();
                txtNombreP.Text = inventarioGlobal.tbProducto.nombre.ToString();
                txtCantidadP.Text = inventarioGlobal.cantidad.ToString();
                txtCantmax.Text = inventarioGlobal.cant_max.ToString();
                txtCantMin.Text = inventarioGlobal.cant_min.ToString();

            }

        }
        private bool validar()
        {

            if (txtCantidadP.Text == string.Empty)
            {
                MessageBox.Show("Digite valor de cantidad");
                txtCantidadP.Focus();
                return false;
            }
            if (txtCantmax.Text == string.Empty)
            {
                MessageBox.Show("Digite valor de cantidad maxima");
                txtCantmax.Focus();
                return false;
            }
            if (txtCantMin.Text == string.Empty)
            {
                MessageBox.Show("Digite valor de cantidad minima");
                txtCantMin.Focus();
                return false;
            }
            return true;

        }





        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (validar())
            {
                try
                {

                    inventarioGlobal.cantidad = decimal.Parse(txtCantidadP.Text);
                    inventarioGlobal.cant_max = decimal.Parse(txtCantmax.Text);
                    inventarioGlobal.cant_min = decimal.Parse(txtCantMin.Text);
                    inventarioGlobal.fecha_ult_mod = Utility.getDate();
                    inventarioGlobal.usuario_ult_mod = Global.Usuario.nombreUsuario;

                    this.pasarDatosEvent(inventarioGlobal);
                    this.Close();
                }
                catch (Exception)
                {

                    MessageBox.Show("Error al cambiar los datos, verifique los datos y vuelva a intentarlo", "Error de datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }




            }

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (txtNuevoValor.Text != string.Empty && Utility.isNumeroDecimal(txtNuevoValor.Text))
            {
                if (chkAcumular.Checked)
                {
                    txtCantidadP.Text = (inventarioGlobal.cantidad + decimal.Parse(txtNuevoValor.Text)).ToString();
                }
                else
                {
                    txtCantidadP.Text = decimal.Parse(txtNuevoValor.Text).ToString();
                }
            }
            else
            {
                txtNuevoValor.Text = string.Empty;
                txtCantidadP.Text = inventarioGlobal.cantidad.ToString();
            }

        }
    }
}
