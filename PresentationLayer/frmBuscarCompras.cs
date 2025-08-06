using BusinessLayer;
using CommonLayer;
using CommonLayer.Exceptions.DataExceptions;
using CommonLayer.Logs;
using EntityLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace PresentationLayer
{
    public partial class frmBuscarCompras : Form
    {
        BCompras comprasIns = new BCompras();
        public static List<tbCompras> listaCompras = new List<tbCompras>();
        public static tbCompras compraGlo = new tbCompras();

        public delegate void pasaDatos(tbCompras entity);
        public event pasaDatos pasarDatosEvent;

        public tbProveedores proveedorGlobal { get; private set; }

        public frmBuscarCompras()
        {
            InitializeComponent();
        }

        private void frmBuscarCompras_Load(object sender, EventArgs e)
        {
            try
            {
                compraGlo = null;
                dtpFechaCompra.Checked = false;
                listaCompras = comprasIns.GetListEntities((int)Enums.EstadoBusqueda.Activo);

                cargarLista(listaCompras);
                cargarCombos();
            }
            catch (Exception ex)
            {
                CommonLayer.Logs.clsEvento evento = new clsEvento(ex.Message, "1");
                MessageBox.Show(ex.Message + "-" + ex.InnerException.Message);
            }
        }

        private void cargarCombos()
        {
            cboTipoPago.DataSource = Enum.GetValues(typeof(Enums.TipoPago));
            cboTipoVenta.DataSource = Enum.GetValues(typeof(Enums.tipoVenta));


        }

        private void cargarLista(IEnumerable<tbCompras> listaCompras)
        {
            try
            {
                lsvbuscar.Items.Clear();
                listaCompras = listaCompras.OrderByDescending(x => x.fechaReporte).ToList();
                foreach (tbCompras p in listaCompras)
                {
                    ListViewItem item = new ListViewItem();
                    item.Text = p.numFactura.ToString().Trim();

                    item.SubItems.Add(Enum.GetName(typeof(Enums.TipoDocumento), p.tipoDoc));
                    item.SubItems.Add(p.fechaCompra.ToString().Trim());

                    item.SubItems.Add(p.fechaReporte.ToString().Trim());


                    //if (p.tipoIdProveedor == 1)
                    //{

                    item.SubItems.Add(p.nombreProveedor);

                    //}
                    //else
                    //{
                    //    item.SubItems.Add(p.tbProveedores.tbPersona.nombre.Trim());
                    //}
                    item.SubItems.Add(p.tbTipoVenta.nombre.Trim().ToUpper());
                    item.SubItems.Add(p.tbTipoPago.nombre.Trim().ToUpper());
                    item.SubItems.Add(p.tbDetalleCompras.Sum(x => x.montoTotalLinea).ToString().Trim());


                    lsvbuscar.Items.Add(item);

                }
            }
            catch (ListEntityException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnBuscarProveedor_Click(object sender, EventArgs e)
        {
            frmBuscarProveedores buscarProve = new frmBuscarProveedores();
            buscarProve.pasarDatosEvent += datosProveedor;

            buscarProve.ShowDialog();
        }

        private void datosProveedor(tbProveedores proveedor)
        {
            if (proveedor != null)
            {
                proveedorGlobal = proveedor;
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

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            IEnumerable<tbCompras> listaCompras = comprasIns.GetListEntities((int)Enums.EstadoBusqueda.Activo);

            if (txtNumFact.Text != string.Empty)
            {
                if (Utility.isNumerInt(txtNumFact.Text))
                {
                    listaCompras = listaCompras.Where(x => x.numFactura == txtNumFact.Text.Trim());

                }

            }

            if (dtpFechaCompra.Checked)
            {
                listaCompras = listaCompras.Where(x => x.fechaCompra.Date == dtpFechaCompra.Value.Date);
            }


            if (proveedorGlobal != null)
            {
                listaCompras = listaCompras.Where(x => x.idProveedor == proveedorGlobal.id);
            }


            if (cboTipoVenta.Text != string.Empty)
            {
                listaCompras = listaCompras.Where(x => x.tipoCompra == (int)cboTipoVenta.SelectedValue);

            }
            if (cboTipoPago.Text != string.Empty)
            {
                listaCompras = listaCompras.Where(x => x.tipoPago == (int)cboTipoPago.SelectedValue);

            }

            cargarLista(listaCompras);
        }

        private void lsvbuscar_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (lsvbuscar.SelectedItems.Count > 0)
                {
                    string idSelected = lsvbuscar.SelectedItems[0].Text;
                    foreach (tbCompras compra in listaCompras)
                    {
                        if (idSelected == compra.numFactura.ToString().Trim())
                        {
                            compraGlo = compra;
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

        private void btnseleccionar_Click(object sender, EventArgs e)
        {
            pasarDatosEvent(compraGlo);
            this.Dispose();

        }

        private void cboEstaadoFact_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void lsvbuscar_DoubleClick(object sender, EventArgs e)
        {
            pasarDatosEvent(compraGlo);
            this.Dispose();

        }
    }
}
