﻿using BusinessLayer;
using CommonLayer;
using EntityLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace PresentationLayer
{
    public partial class frmBuscarDocumentos : Form
    {

        private IEnumerable<tbDocumento> lista { get; set; }
        public delegate void pasaDatos(tbDocumento doc);
        public event pasaDatos pasarDatosEvent;

        public bool tipoBusquedaDevolver { get; set; }


        BFacturacion factIns = new BFacturacion();
        public frmBuscarDocumentos()
        {
            tipoBusquedaDevolver = false;
            InitializeComponent();
        }



        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }


        private void btnBuscar_Click(object sender, EventArgs e)
        {
            obtenerAplicarFiltroAsync(2);



        }

        private async System.Threading.Tasks.Task obtenerAplicarFiltroAsync(int tipo)
        {
            bool bandera = true;
            try
            {
                if (tipo==1 || chkFechas.Checked)
                {
                    lista = await factIns.getListAllDocumentosDateBySearchAsync(dtpInicio.Value.Date, dtpFin.Value.Date);
                   
                }
                else
                {
                    lista = await factIns.getListAllDocumentosBySearchAsync();
                }
            

                bool estado = (int)cboEstado.SelectedValue == 0 ? false : true;
                lista = lista.Where(x => x.estado == estado);



                if ((int)cboTipoDoc.SelectedValue != (int)Enums.TipoDocumento.Todos)
                {
                    if (cboTipoDoc.Text != string.Empty)
                    {
                        lista = lista.Where(x => x.tipoDocumento == (int)cboTipoDoc.SelectedValue);

                    }
                }

                if (txtFactura.Text != string.Empty)
                {
                    lista = lista.Where(x => x.id == int.Parse(txtFactura.Text.Trim()));

                }

                if (txtConsecutivo.Text != string.Empty)
                {
                    lista = lista.Where(x => x.consecutivo != null & x.consecutivo == txtConsecutivo.Text.Trim());

                }

                if (txtClave.Text != string.Empty)
                {
                    lista = lista.Where(x => x.clave != null && x.clave == txtClave.Text.Trim());
                }

                if (txtCedula.Text != string.Empty)
                {
                    lista = lista.Where(x => x.idCliente != null && x.idCliente.Trim() == txtCedula.Text.Trim());
                }

                if (txtNombre.Text != string.Empty)
                {
                    lista = lista.Where(x => x.idCliente != null && x.tbClientes.tbPersona.nombre.ToUpper().Contains(txtNombre.Text.ToUpper().Trim()));
                }


                if (txtApell1.Text != string.Empty)
                {
                    lista = lista.Where(x => x.idCliente != null && x.tbClientes.tbPersona.apellido1 != null && x.tbClientes.tbPersona.apellido1.ToUpper().Contains(txtApell1.Text.ToUpper().Trim()));

                }
                if (txtApell2.Text != string.Empty)
                {
                    lista = lista.Where(x => x.idCliente != null && x.tbClientes.tbPersona.apellido2 != null && x.tbClientes.tbPersona.apellido2.ToUpper().Contains(txtApell2.Text.ToUpper().Trim()));

                }


                if (chkFechas.Checked)
                {
                    if (dtpInicio.Value.Date > dtpFin.Value.Date)
                    {
                        MessageBox.Show("La fecha de inicio no puede ser mayor que la fecha final", "Datos fechas", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        bandera = false;
                    }
                    else
                    {
                        DateTime fechaInicio = dtpInicio.Value.Date;
                        DateTime fechaFin = dtpFin.Value.Date.AddDays(1);

                        lista = lista.Where(x => x.fecha.Date >= fechaInicio && x.fecha.Date <= fechaFin);
                    }
                }
                if (bandera)
                {
                    cargarLista(lista);
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show("Error al consultar datos, favor verificar los datos ingresdos.", "Datos errones", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cargarLista(IEnumerable<tbDocumento> documentos)
        {
            lsvFacturas.Items.Clear();
            foreach (tbDocumento item in documentos)
            {

                //Creamos un objeto de tipo ListviewItem
                ListViewItem linea = new ListViewItem();

                linea.Text = item.id.ToString();
                linea.SubItems.Add(Enum.GetName(typeof(Enums.TipoDocumento), item.tipoDocumento).ToUpper());

                if (item.tipoIdCliente == null)
                {
                    if (item.clienteNombre == null)
                    {
                        linea.SubItems.Add("SIN  CLIENTE");
                        linea.SubItems.Add("SIN  CLIENTE");
                    }
                    else
                    {
                        linea.SubItems.Add("SIN  CLIENTE");
                        linea.SubItems.Add(item.clienteNombre.Trim().ToUpper());
                    }

                }
                else if (item.tipoIdCliente == (int)Enums.TipoId.Fisica)
                {
                    linea.SubItems.Add(item.idCliente.ToString().Trim());
                    linea.SubItems.Add(item.tbClientes.tbPersona.nombre.Trim().ToUpper() + " " + item.tbClientes.tbPersona.apellido1.Trim().ToUpper() + " " + item.tbClientes.tbPersona.apellido2.Trim().ToUpper());

                }
                else
                {
                    linea.SubItems.Add(item.idCliente.ToString().Trim());
                    linea.SubItems.Add(item.tbClientes.tbPersona.nombre.Trim().ToUpper());

                }
                linea.SubItems.Add(item.fecha.ToString());
                if ((int)item.tipoDocumento == (int)Enums.TipoDocumento.ProformaGeneral || (int)item.tipoDocumento == (int)Enums.TipoDocumento.Proforma)
                {
                    linea.SubItems.Add(string.Empty);
                    linea.SubItems.Add(string.Empty);
                }
                else
                {
                    linea.SubItems.Add(item.consecutivo.Trim());
                    linea.SubItems.Add(item.clave.Trim());
                }

                linea.SubItems.Add(item.tipoDocumento.ToString());
                //Agregamos el item a la lista.
                lsvFacturas.Items.Add(linea);

            }
        }

        private void frmBuscarFactura_Load(object sender, EventArgs e)
        {
            cboTipoDoc.DataSource = Enum.GetValues(typeof(Enums.TipoDocumento));
            cboEstado.DataSource = Enum.GetValues(typeof(Enums.Estado));
            cboEstado.SelectedIndex = 1;
            chkFechas.Checked = true;
            gbxFechas.Enabled = true;
            obtenerAplicarFiltroAsync(1);
        }

        private void chkFechas_CheckedChanged(object sender, EventArgs e)
        {
            if (chkFechas.Checked)
            {
                gbxFechas.Enabled = true;
            }
            else
            {
                gbxFechas.Enabled = false;
            }

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }



        private void lsvFacturas_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (lsvFacturas.SelectedItems.Count > 0)
                {
                    tbDocumento doc = new tbDocumento();
                    doc.id = int.Parse(lsvFacturas.SelectedItems[0].Text);
                    doc.tipoDocumento = int.Parse(lsvFacturas.SelectedItems[0].SubItems[7].Text);
                    doc = factIns.getEntity(doc, false);
                    if (doc != null)
                    {
                        if (tipoBusquedaDevolver)
                        {

                            pasarDatosEvent(doc);
                            Close();
                        }
                        else
                        {
                            frmDocumentosDetalle frm = new frmDocumentosDetalle(doc);
                            frm.ShowDialog();
                            obtenerAplicarFiltroAsync(1);

                        }
                      

                    }
                    else
                    {
                        MessageBox.Show("No se encontró los datos de la factura seleccionada", "Error al buscar", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }



                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }

        }

        private void lsvFacturas_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnRefrescar_Click(object sender, EventArgs e)
        {
            cboEstado.SelectedIndex = 1;
            chkFechas.Checked = true;
            gbxFechas.Enabled = true;
            obtenerAplicarFiltroAsync(1);
        }

        private void btnsalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
