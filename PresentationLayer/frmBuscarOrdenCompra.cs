using BusinessLayer;
using CommonLayer;
using CommonLayer.Exceptions.DataExceptions;
using EntityLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace PresentationLayer
{
    public partial class frmBuscarOrdenesCompra : Form
    {

        BOrdenCompra OrdenIns = new BOrdenCompra();
        public static List<tbOrdenCompra> listaCompras = new List<tbOrdenCompra>();
        public static tbOrdenCompra ordenGlo = new tbOrdenCompra();

        public delegate void pasaDatos(tbOrdenCompra orden);
        public event pasaDatos pasarDatosEvent;

        public tbProveedores proveedorGlo { get; set; }


        public frmBuscarOrdenesCompra()
        {
            InitializeComponent();
        }

        private void frmBuscarOrdenesCompra_Load(object sender, EventArgs e)
        {
            listaCompras = OrdenIns.GetListEntities((int)Enums.EstadoOrdenCompra.EnProceso, true);
            cboEstadoOrden.DataSource = Enum.GetValues(typeof(Enums.EstadoOrdenCompra));
            cargarLista(listaCompras);
        }

        private void cargarLista(List<tbOrdenCompra> lista)
        {

            try
            {
                lsvbuscar.Items.Clear();
                lista = lista.OrderByDescending(x => x.fecha).ToList();
                foreach (tbOrdenCompra p in lista)
                {
                    ListViewItem item = new ListViewItem();

                    item.Text = p.id.ToString().Trim();
                    item.SubItems.Add(p.fecha.ToString().Trim());

                    if (p.tipoIdProveedor == 1)
                    {

                        item.SubItems.Add(p.tbProveedores.tbPersona.nombre.Trim() + " " + p.tbProveedores.tbPersona.apellido1.Trim() + " " + p.tbProveedores.tbPersona.apellido2.Trim());

                    }
                    else
                    {
                        item.SubItems.Add(p.tbProveedores.tbPersona.nombre.Trim());
                    }

                    item.SubItems.Add(p.tbTipoPago.nombre.Trim());

                    item.SubItems.Add(p.plazo.ToString());
                    item.SubItems.Add(p.tbDetalleOrdenCompra.Count.ToString());
                    item.SubItems.Add(Enum.GetName(typeof(Enums.EstadoOrdenCompra), p.estadoOrden).ToUpper());


                    lsvbuscar.Items.Add(item);

                }
            }
            catch (ListEntityException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void lsvbuscar_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (lsvbuscar.SelectedItems.Count > 0)
                {
                    string idSelected = lsvbuscar.SelectedItems[0].Text;
                    foreach (tbOrdenCompra compra in listaCompras)
                    {
                        if (idSelected == compra.id.ToString().Trim())
                        {
                            ordenGlo = compra;
                            break;
                        }
                    }
                }
            }
            catch (LicenseException ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void lsvbuscar_MouseDoubleClick(object sender, MouseEventArgs e)
        {

            ordenGlo = OrdenIns.getEntityByID(ordenGlo.id);
            pasarDatosEvent(ordenGlo);
            this.Dispose();
        }

        private void btnBuscarProveedor_Click(object sender, EventArgs e)
        {
            buscarProveedor();
        }
        private void buscarProveedor()
        {
            frmBuscarProveedores buscarProve = new frmBuscarProveedores();
            buscarProve.pasarDatosEvent += datosProveedor;

            buscarProve.ShowDialog();
        }

        private void datosProveedor(tbProveedores proveedor)
        {
            if (proveedor != null)
            {
                proveedorGlo = proveedor;
                if (proveedor.tipoId == (int)Enums.TipoId.Fisica)
                {
                    txtProveedor.Text = proveedor.id.Trim() + "-" + proveedor.tbPersona.nombre.Trim() + " " + proveedor.tbPersona.apellido1.Trim() + " " + proveedor.tbPersona.apellido2.Trim();
                }
                else
                {
                    txtProveedor.Text = proveedor.id.Trim() + "-" + proveedor.tbPersona.nombre.Trim();
                }

            }
            else
            {
                MessageBox.Show("El proveedor del documento no se encuentra registrado en el sistema, Debe incluir el proveedor", "Error de datos");
            }
        }

        private void cboEstadoOrden_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void buscar()
        {

            if (proveedorGlo != null && txtIdOrden.Text != string.Empty)
            {
                if (Utility.isNumeroDecimal(txtIdOrden.Text))
                {
                    listaCompras = OrdenIns.getOrderByParams(int.Parse(txtIdOrden.Text), proveedorGlo.id, proveedorGlo.tipoId, (int)cboEstadoOrden.SelectedValue);

                }
                else
                {
                    MessageBox.Show("El id de la orden no es correcto", "Error de datos");


                }

            }
            else if (proveedorGlo == null && txtIdOrden.Text != string.Empty)
            {
                if (Utility.isNumeroDecimal(txtIdOrden.Text))
                {
                    listaCompras = OrdenIns.getOrderByParams(int.Parse(txtIdOrden.Text), string.Empty, 0, (int)cboEstadoOrden.SelectedValue);

                }
                else
                {
                    MessageBox.Show("El id de la orden no es correcto", "Error de datos");


                }

            }
            else if (proveedorGlo != null && txtIdOrden.Text == string.Empty)
            {
                listaCompras = OrdenIns.getOrderByParams(int.MinValue, proveedorGlo.id, proveedorGlo.tipoId, (int)cboEstadoOrden.SelectedValue);

            }
            else if (proveedorGlo == null && txtIdOrden.Text == string.Empty)
            {
                listaCompras = OrdenIns.getOrderByParams(int.MinValue, string.Empty, 0, (int)cboEstadoOrden.SelectedValue);

            }


            cargarLista(listaCompras);



        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            buscar();
        }
    }
}
