using BusinessLayer;
using CommonLayer;
using CommonLayer.Exceptions.BusisnessExceptions;
using CommonLayer.Logs;
using EntityLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace PresentationLayer
{
    public partial class frmCompras : Form
    {
        ComboBox combo;
        Bcliente clienteInst = new Bcliente();
        BImpuestos ImpIns = new BImpuestos();
        BProveedores proveedoresIns = new BProveedores();
        BProducto productoIns = new BProducto();
        bTipoMedidas medidaIns = new bTipoMedidas();
        BExoneraciones exoneraIns = new BExoneraciones();
        BCategoriaProducto cateIns = new BCategoriaProducto();

        bool banderaBusqueda = false;
        tbCompras compraGlobal = new tbCompras();
        List<tbDetalleCompras> detalleDoc = new List<tbDetalleCompras>();
        tbProducto productoGlobal;
        tbReporteHacienda reporteGlobal;
        tbProveedores proveedorGlobal;
        bool facturacionElectronica = false;
        BCompras comprasIns = new BCompras();
        BFacturacion facturacionIns = new BFacturacion();
        private int bandera;
        private List<tbProducto> produtosAgregados = new List<tbProducto>();
        int linea = 0;
        bool manual = true;
        List<tbCategoriaProducto> listaCategorias = new List<tbCategoriaProducto>();
        List<tbCategoriaProducto> listaCategoriasTodos = new List<tbCategoriaProducto>();
        List<tbImpuestos> listaImp = new List<tbImpuestos>();
        public frmCompras()
        {
            InitializeComponent();
        }


        private void CargarCombos()
        {


            ////busquedaProducto
            cboBusquedaProducto.DataSource = Enum.GetValues(typeof(Enums.busquedaProductoCompraXML));
            cboBusquedaProducto.SelectedIndex = 2;

            ////tipoDocRef
            cboTipoDocRef.DataSource = Enum.GetValues(typeof(Enums.TipoDocRef));

            ////tipoDocRef
            cboEstadoRef.DataSource = Enum.GetValues(typeof(Enums.TipoRef));

            ////tipoMoneda
            cboMoneda.DataSource = Enum.GetValues(typeof(Enums.TipoMoneda));

            //tipopago
            cboTipoPago.DataSource = Enum.GetValues(typeof(Enums.TipoPago));
            //tipoveta
            cboTipoVenta.DataSource = Enum.GetValues(typeof(Enums.tipoVenta));

            cboImpuesto.ValueMember = "id";
            cboImpuesto.DisplayMember = "valor";
            cboImpuesto.DataSource = ImpIns.getImpuestos((int)Enums.EstadoBusqueda.Activo);
            cboImpuesto.Text = "13";

            cboMedida.ValueMember = "idTipoMedida";
            cboMedida.DisplayMember = "nomenclatura";
            cboMedida.DataSource = medidaIns.getlistatipomedida();

        }

        private void frmCompras_Load(object sender, EventArgs e)
        {
            //deshabilitamos el formulario al iniciar la carga.
            MenuGenerico.CambioEstadoMenu(ref tlsMenu, (int)EnumMenu.OpcionMenu.Nuevo);
            Utility.EnableDisableForm(ref gbxCompras, false);
            listaCategorias = cateIns.getCategorias();
            listaCategoriasTodos= cateIns.getCategorias((int)Enums.EstadoBusqueda.Todos);
            listaImp = ImpIns.getImpuestos((int)Enums.EstadoBusqueda.Activo); 
            CargarCombos();
            limpiarForm();
            //chkIncluyeInventario.Checked = true;
            //cargarOpciones();
            resetProducto();

            cboTipoDoc.SelectedIndex = 0;

        }

        //private void cargarOpciones()
        //{
        //    facturacionElectronica = (bool)Global.Usuario.tbEmpresa.tbParametrosEmpresa.FirstOrDefault().facturacionElectronica;
        //    if (!facturacionElectronica)
        //    {
        //        chkRegimenSimplificado.Visible = false;
        //        btnBuscarReporteHacienda.Visible = false;

        //    }
        //}

        private void cboTipoVenta_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboTipoVenta.SelectedValue != null)
            {
                if ((int)cboTipoVenta.SelectedValue == (int)Enums.tipoVenta.Credito)
                {
                    mskPlazoCredito.Enabled = true;

                }
                else
                {
                    mskPlazoCredito.Enabled = false;
                    mskPlazoCredito.Text = string.Empty;
                }

            }

        }




        private void btnAgregarProducto_Click(object sender, EventArgs e)
        {
            try
            {
                decimal utilidadPro = 0;
                if (validarCampos())
                {


                    tbDetalleCompras detalle = new tbDetalleCompras();

                    if (txtCodigoProducto.Text.Trim() == string.Empty)
                    {
                        detalle.idProducto = "0";
                    }
                    else
                    {

                        detalle.idProducto = txtCodigoProducto.Text.Trim();
                    }

                    tbProducto pro = productoIns.GetEntityById(txtCodigoProducto.Text.Trim());

                    detalle.nombreProducto = txtNombreProducto.Text.Trim().ToUpper();
                    detalle.idMedida = (int)cboMedida.SelectedValue;
                    detalle.idMedidaVenta = (int)cboMedida.SelectedValue;
                    detalle.precio = decimal.Parse(txtPrecioProducto.Text);
                    detalle.cantidad = decimal.Parse(txtCantidad.Text);
                    detalle.cantidadVenta= decimal.Parse(txtCantidad.Text);
                    detalle.montoTotal = detalle.precio * detalle.cantidad;
                    detalle.utilidad = pro != null ? pro.utilidad1Porc : (decimal)Global.Usuario.tbEmpresa.tbParametrosEmpresa.First().utilidadBase;
                    detalle.montoTotaDesc = decimal.Parse(txtDescuento.Text);
                    detalle.montoOtroImp = 0;
                    detalle.montoTotalImp = (((detalle.montoTotal - detalle.montoTotaDesc) * (1 + decimal.Parse(cboImpuesto.Text) / 100))) - (detalle.montoTotal - detalle.montoTotaDesc);
                    detalle.montoTotalLinea = (detalle.montoTotal - detalle.montoTotaDesc) + detalle.montoTotalImp - detalle.montoTotalExo;
                    detalle.actualizaInvent = chkActualiza.Checked;
                    detalle.actualizaPrecio = chkActualiza.Checked;
                    detalle.codigoCabys = pro != null ? pro.codigoCabys : null;
                    detalle.tarifaImp = decimal.Parse(cboImpuesto.Text);
                    detalle.tarifaImpVenta = decimal.Parse(cboImpuesto.Text);
                    detalle.nomenclatura = cboMedida.Text;
                    detalle.id_categoria = pro != null ? (int)pro.id_categoria : listaCategorias.FirstOrDefault().id;




                    detalleDoc.Add(detalle);
                    actualizarLineas(detalleDoc);
                    cargarTotales(detalleDoc);

                    actualizarGridView(detalleDoc);
                    resetProducto();
                }


            }
            catch (Exception)
            {

                MessageBox.Show("Se produjo un error al agregar el producto", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void actualizarLineas(List<tbDetalleCompras> detalleDoc)
        {
            int conta = 1;
            foreach (var item in detalleDoc)
            {
                item.numLinea = conta;
                conta++;
            }
        }

        private void cargarTotales(IEnumerable<tbDetalleCompras> lista)
        {

            decimal total = 0, desc = 0, iva = 0, subtotal = 0, exo = 0;

            foreach (tbDetalleCompras detalle in lista)
            {
                detalle.montoTotalLinea = (decimal)((detalle.montoTotal - detalle.montoTotaDesc+ detalle.montoOtroImp) + (detalle.montoTotalImp-detalle.montoTotalExo));
                total += (decimal)detalle.montoTotalLinea;
                desc += (decimal)detalle.montoTotaDesc;
                iva += (decimal)detalle.montoTotalImp;
                iva += (decimal)detalle.montoOtroImp;
                subtotal += (decimal)detalle.montoTotal;
                exo += (decimal)detalle.montoTotalExo;

            }


            subtotal = subtotal == 0 ? 0 : subtotal;
            desc = desc == 0 ? 0 : desc;
            iva = iva == 0 ? 0 : iva;
            total = total == 0 ? 0 : total;

            txtSubtotal.Text = string.Format("{0:N2}", subtotal);
            txtDescuentoTotal.Text = string.Format("{0:N2}", desc);
            txtIva.Text = string.Format("{0:N2}", iva);
            txtTotal.Text = string.Format("{0:N2}", total);
            txtExoneracion.Text = string.Format("{0:N2}", exo);

        }

        private void resetProducto()
        {
            txtCodigoProducto.ResetText();
            txtNombreProducto.ResetText();
            cboMedida.SelectedIndex = 0;
            txtPrecioProducto.ResetText();
            txtCantidad.Text = "1";
            txtDescuento.Text = "0";
            cboImpuesto.Text = "13";

        }


        private void actualizarGridView(IEnumerable<tbDetalleCompras> lista)
        {
            dtgvCompras.EndEdit();
            dtgvCompras.Rows.Clear();
            lista = lista.OrderBy(x => x.numLinea).ToList();
            foreach (tbDetalleCompras detalle in lista)
            {
                if (productoGlobal == null)
                {
                    productoGlobal = new tbProducto();
                }
                if (detalle.idProducto != null && detalle.idProducto != "0")
                {
                    productoGlobal.idProducto = detalle.idProducto.Trim();
                    productoGlobal = productoIns.GetEntity(productoGlobal);
                }
                else
                {
                    productoGlobal = null;
                }

                decimal precioActual = productoGlobal == null ? 0 : productoGlobal.precioVenta1;              
                decimal precioCompra = 0;
                //*****************************************************************************************************************
                // actualizar con la tarifa de impuesto 
              
                //ProductoDTO pro1 = null;
                
                
                //precioCompra = pro1.Precio1;

                //Creamos un objeto de tipo ListviewItem
                DataGridViewRow row = (DataGridViewRow)dtgvCompras.Rows[0].Clone();


                row.Cells[0].Value = detalle.actualizaPrecio;     
                row.Cells[1].Value = detalle.actualizaInvent;
                row.Cells[2].Value = detalle.numLinea.ToString().Trim();
                row.Cells[3].Value = detalle.idProducto == null ? "" : detalle.idProducto.ToString().Trim();
                row.Cells[4].Value = detalle.nombreProducto.Trim();
                row.Cells[5].Value = string.Format("{0:N2}", detalle.cantidadVenta);
                row.Cells[6].Value = string.Format("{0:N2}", precioActual);
                decimal precioAntesImp = 0;
                
           
                
                if (detalle.cantidad == detalle.cantidadVenta) {
                    precioAntesImp = (decimal)(((detalle.precio * detalle.cantidad) - detalle.montoTotaDesc + detalle.montoOtroImp) / detalle.cantidad);

                }
                else
                {
                    precioAntesImp = (decimal)(((detalle.precio * detalle.cantidad) - detalle.montoTotaDesc + detalle.montoOtroImp) / detalle.cantidadVenta);

                }
                 row.Cells[7].Value = string.Format("{0:N2}", precioAntesImp);

                //DataGridViewComboBoxCell dgvCmb = (DataGridViewComboBoxCell)row.Cells[8];


                //dgvCmb.DataSource = ImpIns.getCategorias();
                //dgvCmb.DisplayMember = "nombre";
                //dgvCmb.ValueMember = "id";

                decimal imp = 0;
                if (detalle.tarifaImpVenta==detalle.tarifaImp)
                {
                    imp = detalle.tarifaImp;
                }
                else
                {
                    imp = (decimal)detalle.tarifaImpVenta;
                }
                row.Cells[8].Value = Math.Truncate((decimal)imp).ToString(); 



                row.Cells[9].Value = string.Format("{0:N2}", detalle.montoTotalLinea);

                row.Cells[10].Value = string.Format("{0:N5}", Utility.priceFormat( (decimal)detalle.utilidad));
                //precio venta 1
                ProductoDTO pro = null;
               

                if (detalle.cantidad == detalle.cantidadVenta) {
                    pro = ProductosUtility.calcularPrecio(precioAntesImp, (decimal)detalle.utilidad, imp);

                }
                //else if (detalle.p){

                //}
                else
                {
                    pro = ProductosUtility.calcularPrecio((decimal)(precioAntesImp), (decimal)detalle.utilidad, imp);
                }

                precioCompra = pro.Precio1;
                row.Cells[11].Value =Utility.priceFormat(pro.Precio1);

                DataGridViewComboBoxCell dgvCmb = (DataGridViewComboBoxCell)row.Cells[12];


                dgvCmb.DataSource = listaCategoriasTodos;
                dgvCmb.DisplayMember = "nombre";
                dgvCmb.ValueMember = "id";               
               row.Cells[12].Value = detalle.id_categoria;

                dtgvCompras.Rows.Add(row);

                precioCompra = decimal.Parse(string.Format("{0:N5}", precioCompra));
                precioActual = decimal.Parse(string.Format("{0:N5}", precioActual));
                if (productoGlobal == null)
                {
                    row.DefaultCellStyle.BackColor = Color.LightBlue;
                    row.DefaultCellStyle.ForeColor = Color.Black;

                }
                else if (precioActual == precioCompra)
                {
                    row.DefaultCellStyle.BackColor = Color.RoyalBlue;
                    row.DefaultCellStyle.ForeColor = Color.Black;

                }

                else if (precioActual > precioCompra)
                {
                    row.DefaultCellStyle.BackColor = Color.Green;
                    row.DefaultCellStyle.ForeColor = Color.White;
                }
                else
                {
                    row.DefaultCellStyle.BackColor = Color.Red;
                    row.DefaultCellStyle.ForeColor = Color.White;
                }
            }

        }

        private bool validarCampos()
        {
            if (txtCodigoProducto.Text == string.Empty)
            {
                txtCodigoProducto.Focus();
                MessageBox.Show("Debe indicar el código del producto", "Error de datos");
                return false;
            }

            if (detalleDoc.Where(x => x.idProducto == txtCodigoProducto.Text).SingleOrDefault() != null)
            {
                txtCodigoProducto.Focus();
                MessageBox.Show("El producto ya se encuentra en la lista.", "Error de datos");
                return false;
            }


            if (txtNombreProducto.Text == string.Empty)
            {
                txtNombreProducto.Focus();
                MessageBox.Show("Debe indicar el nombre del producto", "Error de datos");
                return false;
            }
            if (cboMedida.Text == string.Empty)
            {
                cboMedida.Focus();
                MessageBox.Show("No se indicó una medida del producto", "Error de datos");
                return false;
            }

            if (txtPrecioProducto.Text == string.Empty)
            {
                txtPrecioProducto.Focus();
                MessageBox.Show("Debe indicar el precio del producto", "Error de datos");
                return false;
            }
            if (txtCantidad.Text == string.Empty)
            {
                txtCantidad.Focus();
                MessageBox.Show("Debe indicar la cantidad del producto", "Error de datos");
                return false;
            }

            if (!Utility.isNumeroDecimal(txtPrecioProducto.Text))
            {
                txtPrecioProducto.Focus();
                MessageBox.Show("El precio indicado tiene formato incorrecto, no es númerico", "Error de datos");
                return false;
            }

            if (txtDescuento.Text == string.Empty)
            {
                txtDescuento.Focus();
                MessageBox.Show("Debe indicar el descuento del producto", "Error de datos");
                return false;
            }
            if (cboImpuesto.Text == string.Empty)
            {
                cboImpuesto.Focus();
                MessageBox.Show("Debe indicar el impuesto del producto", "Error de datos");
                return false;
            }

            return true;
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }





        private bool validarCamposDoc()
        {
            //en caso de nota de credito verificar que exista la compra origen a la cual aplicar la nota de credito
            if (cboTipoDoc.SelectedIndex == 1)
            {
                if (comprasIns.GetEntityComprasByClave(mskClaveRef.Text.Trim()) is null)
                {
                    mskFactura.Focus();
                    MessageBox.Show("No existe ninguna Compra origen para aplicar la nota de crédito indicada.", "Error de datos");
                    return false;
                }

                if (validarProductosNC())
                {
                    MessageBox.Show("Los productos indicados en la Nota de crédito, no corresponde a su documento original", "Error de datos");
                    return false;
                }
            }

            if (mskFactura.Text == string.Empty)
            {
                mskFactura.Focus();
                MessageBox.Show("Debe indicar el #Factura", "Error de datos");
                return false;
            }

            if (txtProveedor.Text == string.Empty)
            {
                txtProveedor.Focus();
                MessageBox.Show("Debe indicar el proveedor", "Error de datos");
                return false;
            }

            if (dtpFechaCompra.Value == null)
            {
                dtpFechaCompra.Focus();
                MessageBox.Show("Debe indicar la fecha de compra", "Error de datos");
                return false;
            }
            //if (dtpFechaReporte.Value == null)
            //{
            //    dtpFechaReporte.Focus();
            //    MessageBox.Show("Debe indicar la fecha de reporte", "Error de datos");
            //    return false;
            //}

            if (cboTipoVenta.Text == string.Empty)
            {
                cboTipoVenta.Focus();
                MessageBox.Show("Debe indicar el tipo de venta", "Error de datos");
                return false;
            }

            if ((int)cboTipoVenta.SelectedValue == (int)Enums.tipoVenta.Credito && mskPlazoCredito.Text == string.Empty)
            {
                mskPlazoCredito.Focus();
                MessageBox.Show("Debe indicar el plazo del crédito", "Error de datos");
                return false;
            }

            if (cboTipoPago.Text == string.Empty)
            {
                cboTipoPago.Focus();
                MessageBox.Show("Debe indicar el tipo de pago", "Error de datos");
                return false;
            }
            if (verificarCodigosProductos())
            {
                MessageBox.Show("Verifique los códigos de los productos.", "Error de datos");
                return false;

            }


            return true;
        }

        private bool validarProductosNC()
        {
            tbCompras compra = comprasIns.GetEntityComprasByClave(mskClaveRef.Text.Trim());
            if (compra != null && compra.tbDetalleCompras.Count > 0)
            {
                foreach (var item in detalleDoc)
                {

                    if (compra.tbDetalleCompras.Where(x => x.idProducto == item.idProducto).SingleOrDefault() is null)
                    {
                        return true;
                    }
                    else
                    {
                        item.actualizaInvent = compra.tbDetalleCompras.Where(x => x.idProducto == item.idProducto).SingleOrDefault().actualizaInvent;
                    }

                }

            }

            return false;
        }
        //REPETIDOS CODIGOS
        private bool verificarCodigosProductos()
        {
            foreach (var item in detalleDoc)
            {
                if (item.idProducto is null || item.idProducto == string.Empty)
                {
                    return true;

                }
                if (item.idProducto != "0")
                {
                    if (detalleDoc.Where(x => x.idProducto == item.idProducto).ToList().Count > 1)
                    {
                        MessageBox.Show("Existe un producto con el mismo código. Código:"+ item.idProducto.Trim());
                        return true;
                    }
                }

            }

            return false;
        }

        private tbCompras crearDocumento()
        {
            try
            {
                tbCompras documento = new tbCompras();

                //documento = compraGlobal;
                documento.reporteElectronico = false;

                documento.tipoDoc = (int)cboTipoDoc.SelectedIndex == 1 ? (int)Enums.TipoDocumento.NotaCredito : (int)Enums.TipoDocumento.Compras;


                // documento.tipoDoc = (int)Enums.TipoDocumento.ComprasSimplificada;
                if (reporteGlobal != null)
                {
                    documento.idReporteHacienda = reporteGlobal.id;
                    documento.reporteElectronico = false;
                }
                documento.reporteElectronico = false;
                documento.claveEmisor = mskClave.Text.Trim();
                documento.consecutivoEmisor = mskConsecutivo.Text.Trim();
                documento.CodActividad = txtActividad.Text.Trim();

                documento.fecha = Utility.getDate();
                documento.numFactura = mskFactura.Text;
                documento.fechaCompra = dtpFechaCompra.Value.Date;
                documento.fechaReporte = Utility.getDate(); ;
                documento.tipoPago = (int)cboTipoPago.SelectedValue;
                documento.tipoCompra = (int)cboTipoVenta.SelectedValue;
                documento.tipoIdProveedor = proveedorGlobal.tipoId;
                documento.idProveedor = proveedorGlobal.id;
                if (documento.tipoIdProveedor == (int)Enums.TipoId.Fisica)
                {
                    documento.nombreProveedor = string.Format("{0} {1} {2}", proveedorGlobal.tbPersona.nombre.Trim().ToUpper(), proveedorGlobal.tbPersona.apellido1.Trim().ToUpper(), proveedorGlobal.tbPersona.apellido2.Trim().ToUpper());
                }
                else
                {
                    documento.nombreProveedor = proveedorGlobal.tbPersona.nombre;
                }
                documento.idProveedor = proveedorGlobal.id;

                documento.plazo = (int)documento.tipoCompra == (int)Enums.tipoVenta.Credito ? int.Parse(mskPlazoCredito.Text) : 0;

                documento.idEmpresa = Global.Usuario.tbEmpresa.id;
                documento.tipoIdEmpresa = Global.Usuario.tbEmpresa.tipoId;

                documento.observaciones = txtObservaciones.Text;
                documento.sucursal = Global.Configuracion.sucursal;
                documento.caja = Global.Configuracion.caja;

                if (cboMoneda.Text == "CRC")
                {
                    documento.tipoMoneda = (int)Enums.TipoMoneda.CRC;
                    documento.cambiarColon = false;
                }
                else
                {
                    documento.tipoMoneda = (int)Enums.TipoMoneda.USD;
                    documento.tipoCambio = decimal.Parse(string.Format("{0:N2}", txtTipoCambio.Text));
                    documento.cambiarColon = chkCambiarMoneda.Checked;
                }

                if ((bool)documento.cambiarColon)
                {
                    documento.tipoMoneda = (int)Enums.TipoMoneda.CRC;

                }


                //en caso de nota de credito
                if (documento.tipoDoc == (int)Enums.TipoDocumento.NotaCredito)
                {
                    documento.tipoDocRef = (int)cboTipoDocRef.SelectedValue;
                    documento.codigoRef = (int)cboEstadoRef.SelectedValue;
                    documento.fechaRef = dtpFechaRef.Value;
                    documento.razon = txtRazonRef.Text.Trim().ToUpper();
                    documento.claveRef = mskClaveRef.Text;

                }

                documento.tbDetalleCompras = detalleDoc;
                documento.estado = true;

                //Atributos de Auditoria

                documento.fecha_crea = Utility.getDate();
                documento.fecha_ult_mod = Utility.getDate();
                documento.usuario_crea = Global.Usuario.nombreUsuario.Trim().ToUpper();   // Global.Usuario.nombreUsuario;
                documento.usuario_ult_mod = Global.Usuario.nombreUsuario.Trim().ToUpper();   // Global.Usuario.nombreUsuario;

                documento.codigoActividadEmpresa = Global.actividadEconomic.CodActividad;



                return documento;
            }
            catch (Exception ex)
            {

                clsEvento evento = new clsEvento(ex.Message, "1");
                throw ex;

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

        private void btnBuscarProducto_Click(object sender, EventArgs e)
        {
            frmBuscarProducto buscarProduct = new frmBuscarProducto();
            buscarProduct.recuperarEntidad += datosProducto;

            buscarProduct.ShowDialog();
        }

        private void datosProducto(tbProducto producto)
        {
            if (producto != null)
            {
                produtosAgregados.Add(producto);
                productoGlobal = producto;
                txtCodigoProducto.Text = producto.idProducto;
                txtNombreProducto.Text = producto.nombre.Trim().ToUpper();
                cboMedida.SelectedValue = producto.idMedida;

            }
            else
            {
                //txtCodigoProducto.Text = string.Empty;
                txtNombreProducto.Text = string.Empty;
                cboMedida.SelectedIndex = 0;
            }
        }

        private void txtCodigoProducto_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((int)e.KeyChar == (int)Keys.Enter)
            {
                if (txtCodigoProducto.Text != string.Empty)
                {
                    tbProducto pro = new tbProducto();
                    pro.idProducto = txtCodigoProducto.Text;
                    pro = productoIns.GetEntity(pro);
                    datosProducto(pro);
                }
            }

        }

        private void gbxDetalleCompra_Enter(object sender, EventArgs e)
        {

        }

        private void btnBuscarReporteHacienda_Click(object sender, EventArgs e)
        {
            frmBuscarComprasHacienda form = new frmBuscarComprasHacienda();
            form.pasarDatosEvent += datosCompra;
            form.ShowDialog();
        }

        private void datosCompra(tbReporteHacienda compra)
        {
            if (compra != null)
            {
                reporteGlobal = compra;

                string consecutivo = compra.claveDocEmisor.Substring(21, 20);
                string numFact = consecutivo.Substring(10, 10);
                mskFactura.Text = numFact.Trim();

                proveedorGlobal = proveedoresIns.getProveedorById(compra.idEmisor.Trim());
                datosProveedor(proveedorGlobal);

                dtpFechaCompra.Value = compra.fechaEmision;

                mskConsecutivo.Text = compra.consecutivoReceptor;
                mskClave.Text = compra.claveReceptor;

            }
        }

        private void txtIdFactura_TextChanged(object sender, EventArgs e)
        {
            if (mskFactura.Text == string.Empty)
            {
                limpiarForm();

            }
            if (reporteGlobal == null)
            {
            }
        }
        public void limpiarForm()
        {
            manual = true;
            btnBuscarProveedor.Enabled = true;
            cboTipoDoc.Enabled = true;
            cboTipoDoc.SelectedIndex = 0;
            banderaBusqueda = false;
            mskFactura.ResetText();
            mskFactura.Enabled = true;
            reporteGlobal = null;
            proveedorGlobal = null;
            compraGlobal = null;
            dtgvCompras.Rows.Clear();
            detalleDoc.Clear();
            tabNotaCredito.Enabled = false;
            chkCambiarMoneda.Checked = false;

            cboMoneda.Enabled = true;
            txtObservaciones.ResetText();
            cboTipoPago.SelectedIndex = 0;
            cboTipoVenta.SelectedIndex = 0;
            cboMoneda.SelectedIndex = 0;
            txtProveedor.Text = string.Empty;
            mskPlazoCredito.ResetText();

            dtpFechaCompra.Refresh();

            mskConsecutivo.ResetText();
            mskClave.ResetText();
            resetProducto();
            txtActividad.ResetText();
            dtgvCompras.Enabled = false;
            //chkIncluyeInventario.Checked = false;


            cboEstadoRef.Enabled = true;
            cboTipoDocRef.Enabled = true;
            txtRazonRef.Enabled = true;
            mskClaveRef.Enabled = true;
            dtpFechaRef.Enabled = true;

            cboEstadoRef.ResetText();
            cboTipoDocRef.SelectedIndex = 0;
            txtRazonRef.ResetText();
            mskClaveRef.ResetText();
            dtpFechaRef.Value = DateTime.Now;


            txtSubtotal.Text = "0";
            txtIva.Text = "0";
            txtDescuento.Text = "0";
            txtTotal.Text = "0";
            txtExoneracion.Text = "0";

            txtTipoCambio.ResetText();





        }

        private void btnLimpiarForm_Click(object sender, EventArgs e)
        {
            limpiarForm();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (detalleDoc.Count != 0 && txtTotal.Text != "0")
                {
                    if (validarCamposDoc())
                    {
                        tbCompras documento = null;
                        if (manual)
                        {
                            documento = crearDocumento();
                        }
                        else
                        {
                            documento = compraGlobal;
                        }


                        facturacionIns.guadarCompra(documento);
                        //if (documento.tipoDoc==(int)Enums.TipoDocumento.ComprasSimplificada && facturacionElectronica)
                        //{
                        //    documento = facturacionIns.GetEntityCompra(documento);
                        //    reportarCompraSimplificadaHacienda(documento);
                        //}
                        MessageBox.Show("La compras se ha guardado correctamente.", "Compras", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        limpiarForm();
                        cargarTotales(documento.tbDetalleCompras);
                    }

                }
                else
                {
                    MessageBox.Show("No hay productos o el TOTAL a cobrar es 0.", "Cobrar", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                clsEvento evento = new clsEvento(ex.Message, "1");

                MessageBox.Show("Error al guardar la compra.", "Compras", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void txtPrecioProducto_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!Utility.isNumeroDecimal(txtPrecioProducto.Text))
                {
                    txtPrecioProducto.Text = txtPrecioProducto.Text.Remove(txtPrecioProducto.Text.Count() - 1);
                }

            }
            catch (Exception)
            {

                txtPrecioProducto.ResetText();
            }
        }

        private void txtDescuento_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!Utility.isNumeroDecimal(txtDescuento.Text, false))
                {
                    txtDescuento.Text = txtDescuento.Text.Remove(txtDescuento.Text.Count() - 1);
                }

            }
            catch (Exception)
            {

                txtDescuento.ResetText();
            }
        }

        private void txtCantidad_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!Utility.isNumeroDecimal(txtCantidad.Text))
                {
                    txtCantidad.Text = txtCantidad.Text.Remove(txtCantidad.Text.Count() - 1);

                }

            }
            catch (Exception)
            {

                txtCantidad.ResetText();
            }
        }

        private void tlsMenu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            dtgvCompras.EndEdit();
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
                        Utility.EnableDisableForm(ref gbxCompras, false);
                        limpiarForm();

                    }
                    break;

                case "Nuevo":
                    bandera = (int)Enums.accionGuardar.Nuevo;
                    MenuGenerico.CambioEstadoMenu(ref tlsMenu, (int)EnumMenu.OpcionMenu.Guardar);
                    Utility.EnableDisableForm(ref gbxCompras, true);



                    limpiarForm();
                    tabNotaCredito.Enabled = true;
                    dtgvCompras.Enabled = true;
                    break;

                case "Modificar":
                    bandera = (int)Enums.accionGuardar.Modificar;
                    MenuGenerico.CambioEstadoMenu(ref tlsMenu, (int)EnumMenu.OpcionMenu.Guardar);
                    Utility.EnableDisableForm(ref gbxCompras, true);
                    txtCodigoProducto.Enabled = false;
                    tabNotaCredito.Enabled = true;
                    dtgvCompras.Enabled = true;

                    break;
                case "Eliminar":
                    bandera = (int)Enums.accionGuardar.Eliminar;
                    MenuGenerico.CambioEstadoMenu(ref tlsMenu, (int)EnumMenu.OpcionMenu.Guardar);

                    break;

                case "Buscar":


                    if (buscarProducto())
                    {

                        MenuGenerico.CambioEstadoMenu(ref tlsMenu, (int)EnumMenu.OpcionMenu.Modificar);
                        //Utility.EnableDisableForm(ref gbxCompras, false);
                        tlsBtnModificar.Enabled = false;
                        tabNotaCredito.Enabled = true;
                        dtgvCompras.Enabled = true;
                    }
                    break;

                case "Cancelar":
                    MenuGenerico.CambioEstadoMenu(ref tlsMenu, (int)EnumMenu.OpcionMenu.Nuevo);
                    Utility.EnableDisableForm(ref gbxCompras, false);
                    limpiarForm();


                    break;

                case "Salir":
                    this.Dispose();
                    break;
            }

        }

        private bool buscarProducto()
        {
            try
            {
                frmBuscarCompras buscar = new frmBuscarCompras();
                buscar.pasarDatosEvent += dataBuscar;
                buscar.ShowDialog();
                return true;
            }
            catch (Exception ex)
            {

                return false;
            }

        }

        private void dataBuscar(tbCompras compra)
        {
            resetProducto();

            if (compra != null)
            {
                compraGlobal = compra;

                mskFactura.Text = compra.numFactura.ToString();


                proveedorGlobal = proveedoresIns.getProveedorById(compra.idProveedor.Trim());
                datosProveedor(proveedorGlobal);

                dtpFechaCompra.Value = compra.fechaCompra;
                mskClave.Text = compra.clave;
                mskConsecutivo.Text = compra.consecutivo;
                mskPlazoCredito.Text = compra.plazo.ToString().Trim();
                txtObservaciones.Text = compra.observaciones;
                cboTipoPago.Text = Enum.GetName(typeof(Enums.TipoPago), compra.tipoPago);
                cboTipoVenta.Text = Enum.GetName(typeof(Enums.tipoVenta), compra.tipoCompra);
                txtActividad.Text = compra.CodActividad.Trim();

                cboMoneda.Text = Enum.GetName(typeof(Enums.TipoMoneda), compra.tipoMoneda);

                var NC = compra.tipoDoc == (int)Enums.TipoDocumento.NotaCredito;
                cboTipoDoc.SelectedIndex = NC ? 1 : 0;
                if (NC)
                {
                    mskClaveRef.Text = compra.claveRef;
                    txtRazonRef.Text = compra.razon;
                    dtpFechaRef.Value = compra.fechaRef.Value;
                    cboTipoDocRef.Text = Enum.GetName(typeof(Enums.TipoDocRef), compra.tipoDocRef);
                    cboEstadoRef.Text = Enum.GetName(typeof(Enums.TipoRef), compra.codigoRef);


                }

                //detalleDoc=compra.tbDetalleCompras;
                actualizarGridView(compra.tbDetalleCompras);
                cargarTotales(compra.tbDetalleCompras);
                dtgvCompras.Enabled = true;
                banderaBusqueda = true;

            }
            else
            {
                MenuGenerico.CambioEstadoMenu(ref tlsMenu, (int)EnumMenu.OpcionMenu.Nuevo);
                Utility.EnableDisableForm(ref gbxCompras, false);
                limpiarForm();
            }



        }

        private bool accionGuardar(int trans)
        {
            bool isOK = false;
            switch (trans)
            {

                case (int)Enums.accionGuardar.Nuevo:
                    isOK = guardarProducto();
                    break;

                //case (int)Enums.accionGuardar.Modificar:
                //    isOK = actualizarProducto();
                //    break;

                case (int)Enums.accionGuardar.Eliminar:
                    isOK = eliminarProducto();
                    break;

            }

            return isOK;

        }

        private bool eliminarProducto()
        {
            //ELIMINAR SOLO MODIFICA EL ESTADO... 
            // Y ACTUALIZA LOS DATOS AUDITORIA....
            bool processOk = false;
            try
            {
                DialogResult result = MessageBox.Show("¿Se encuentra seguro de eliminar los datos?", "Eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                {
                    if (result == DialogResult.Yes)
                    {

                        compraGlobal.estado = false;
                        compraGlobal.fecha_ult_mod = Utility.getDate();
                        compraGlobal.usuario_ult_mod = Global.Usuario.nombreUsuario;
                        compraGlobal = comprasIns.Actualizar(compraGlobal);
                        processOk = true;

                        MessageBox.Show("Los datos han sido eliminados de la base datos.", "Eliminación", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                }
                processOk = true;
                return true;

            }
            catch (Exception ex)
            {
                processOk = false;
                MessageBox.Show("Ha ocurrido un error. Comuniquese con el administrador " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return false;
            }

        }

        private bool guardarProducto()
        {
            tbCompras documento = null;

            try
            {
                if (detalleDoc.Count != 0 && txtTotal.Text != "0")
                {
                    if (validarCamposDoc())
                    {

                         if (manual)
                        {
                            documento = crearDocumento();
                        }
                        else
                        {
                            documento = compraGlobal;
                        }


                        facturacionIns.guadarCompra(documento);
                        //if (documento.tipoDoc == (int)Enums.TipoDocumento.ComprasSimplificada && facturacionElectronica)
                        //{
                        //    documento = facturacionIns.GetEntityCompra(documento);
                        //    reportarCompraSimplificadaHacienda(documento);
                        //}
                        MessageBox.Show("El documento se ha guardado correctamente.", "Compras", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        limpiarForm();
                        cargarTotales(documento.tbDetalleCompras);

                    }
                    else
                    {
                        return false;
                    }
                    return true;

                }
                else
                {

                    MessageBox.Show("No hay productos o el TOTAL a cobrar es 0.", "Cobrar", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }

            catch (EntityExistException ex)
            {

                MessageBox.Show("Error al guardar el documento. Ya se encuentra registrada el documento con ese #Factura: " + documento.numFactura.ToString(), "Compras", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            catch (CompraExistException ex)
            {
                MessageBox.Show("Error al guardar el documento. Ya se encuentra registrada el documento con ese #Factura: " + documento.numFactura.ToString(), "Compras", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            catch (CompraNoEmpresaException ex)
            {
                clsEvento evento = new clsEvento(ex.Message, "1");
                MessageBox.Show("Error al guardar el documento. Compra no pertenece a la empresa.", "Compras", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            catch (Exception ex)
            {
                clsEvento evento = new clsEvento(ex.Message, "1");
                MessageBox.Show("Error al guardar el documento.", "Compras", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }


        }

        private void txtProveedor_TextChanged(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void chkIncluyeInventario_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void btnCargar_Click(object sender, EventArgs e)
        {
            BProducto productoIns = new BProducto();
            limpiarForm();
            tabNotaCredito.Enabled = true;
            cboTipoDoc.Enabled = false;
            dtgvCompras.Enabled = true;
            cboEstadoRef.Enabled = false;
            cboTipoDocRef.Enabled = false;
            txtRazonRef.Enabled = false;
            mskClaveRef.Enabled = false;
            dtpFechaRef.Enabled = false;
            cboMoneda.Enabled = false;
            mskFactura.Enabled = false;

            manual = false;
            detalleDoc.Clear();
            compraGlobal = null;
            try
            {
                DialogResult dr = this.openFileDialog1.ShowDialog();

                if (dr == System.Windows.Forms.DialogResult.OK)
                {
                    // Read the files
                    foreach (String file in openFileDialog1.FileNames)

                    {
                        // Create a PictureBox.
                        try
                        {
                            /////
                            ///
                            if (File.Exists(file))
                            {
                                #region XML 


                                //XmlDocument xDoc = new XmlDocument();
                                //xDoc.Load(file);
                                //bool isNC = xDoc.DocumentElement.Name.ToUpper()== "NOTACREDITOELECTRONICA";


                                //cboTipoDoc.SelectedIndex = isNC?1:0;

                                //if (isNC)
                                //{
                                //    compraOriginal = comprasIns.GetEntityComprasByClave(mskClave.Text);
                                //    if (compraOriginal==null)
                                //    {
                                //        MessageBox.Show("No se puede procesar la nota de crédito seleccionada ya que no existe una factura origen."
                                //        , "Nota de Crédito", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                //        return;
                                //    }
                                //}


                                //var clave = xDoc.GetElementsByTagName("Clave").Item(0).InnerText;
                                //mskClave.Text = clave;



                                //var consec = xDoc.GetElementsByTagName("NumeroConsecutivo").Item(0).InnerText;
                                //mskConsecutivo.Text = consec;

                                //string numFact = consec.Substring(10, 10);
                                //mskFactura.Text = numFact;

                                //var fecha = xDoc.GetElementsByTagName("FechaEmision").Item(0).InnerText;
                                //dtpFechaCompra.Text = fecha;


                                //txtActividad.Text = xDoc.GetElementsByTagName("CodigoActividad").Item(0).InnerText;
                                //var emisor = xDoc.GetElementsByTagName("Emisor");


                                ////

                                //var venta = xDoc.GetElementsByTagName("CondicionVenta").Item(0).InnerText;
                                //cboTipoVenta.Text = Enum.GetName(typeof(Enums.tipoVenta), int.Parse(venta));
                                //if (cboTipoVenta.Text == Enum.GetName(typeof(Enums.tipoVenta), Enums.tipoVenta.Credito))
                                //{
                                //    var plazo = xDoc.GetElementsByTagName("PlazoCredito").Item(0).InnerText;
                                //    mskPlazoCredito.Text = plazo;
                                //}



                                //var pago = xDoc.GetElementsByTagName("MedioPago").Item(0).InnerText;
                                //cboTipoPago.Text = Enum.GetName(typeof(Enums.TipoPago), int.Parse(pago));


                                //var identificacion = ((XmlElement)emisor[0]).GetElementsByTagName("Identificacion");
                                //var correo = ((XmlElement)emisor[0]).GetElementsByTagName("CorreoElectronico").Item(0).InnerText;


                                ////proveddor
                                //var numeroId = ((XmlElement)identificacion[0]).GetElementsByTagName("Numero").Item(0).InnerText;
                                //var tipoId = ((XmlElement)identificacion[0]).GetElementsByTagName("Tipo").Item(0).InnerText;
                                //var nombreEmisor = ((XmlElement)emisor[0]).GetElementsByTagName("Nombre").Item(0).InnerText;
                                //nombreEmisor = numeroId + "-" + nombreEmisor;
                                //proveedorGlobal = proveedoresIns.getProveedorById(numeroId);
                                //if (proveedorGlobal == null)
                                //{
                                //    MessageBox.Show("El proveedor (" + nombreEmisor + ") de la factura no se encuentra en la base datos, debe ingresarlo antes de procesar el documento."
                                //     , "Proveedor", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                //}
                                //else
                                //{
                                //    datosProveedor(proveedorGlobal);
                                //}


                                ////receptor
                                //var recepetor = xDoc.GetElementsByTagName("Receptor");

                                //if (recepetor.Count!=0)
                                //{
                                //    var IdentidicacionRecept = ((XmlElement)recepetor[0]).GetElementsByTagName("Identificacion");

                                //    var nombreRecepetor = ((XmlElement)recepetor[0]).GetElementsByTagName("Nombre").Item(0).InnerText;
                                //    var idRecept = ((XmlElement)IdentidicacionRecept[0]).GetElementsByTagName("Numero").Item(0).InnerText;
                                //    var tipoIdRecept = ((XmlElement)IdentidicacionRecept[0]).GetElementsByTagName("Tipo").Item(0).InnerText;
                                //}



                                //tbTipoMedidas medida = new tbTipoMedidas();

                                //var detalleSer = xDoc.GetElementsByTagName("DetalleServicio").Item(0);
                                //foreach (var item in detalleSer)
                                //{
                                //    try
                                //    {

                                //        tbDetalleCompras detalle = new tbDetalleCompras();
                                //        detalle.actualizaPrecio = chkActualiza.Checked;
                                //        detalle.actualizaInvent = chkActualiza.Checked;


                                //        detalle.numLinea = int.Parse(((XmlElement)item).GetElementsByTagName("NumeroLinea").Item(0).InnerText);
                                //        detalle.nombreProducto = ((XmlElement)item).GetElementsByTagName("Detalle").Item(0).InnerText;

                                //        if (((XmlElement)item).GetElementsByTagName("Codigo").Item(0) != null)
                                //        {
                                //            detalle.codigoCabys = ((XmlElement)item).GetElementsByTagName("Codigo").Item(0).InnerText;
                                //        }

                                //        var codigoComercial= ((XmlElement)item).GetElementsByTagName("CodigoComercial").Item(0);
                                //        if (codigoComercial !=null)
                                //        {

                                //            detalle.idProductoProveedor = ((XmlElement)item).GetElementsByTagName("CodigoComercial").Item(0).InnerText;


                                //        }



                                //        if (detalle.codigoCabys != null)
                                //        {
                                //            tbCategoria9Cabys cat = cateIns.getCat9CabysById(detalle.codigoCabys.Trim());
                                //            if (cat == null)
                                //            {
                                //                detalle.codigoCabys = null;
                                //                //validar si el digo del proveedor es cabys
                                //                if (detalle.idProductoProveedor != null && detalle.idProductoProveedor != string.Empty)
                                //                {
                                //                    cat = cateIns.getCat9CabysById(detalle.idProductoProveedor.Trim());
                                //                    if (cat != null)
                                //                    {
                                //                        detalle.codigoCabys = detalle.idProductoProveedor.Trim();
                                //                        detalle.idProductoProveedor = null;
                                //                    }
                                //                }
                                //            }

                                //        }



                                //        if (detalle.idProductoProveedor != null && detalle.idProductoProveedor != "0" && detalle.idProductoProveedor != string.Empty)
                                //        {
                                //            if (productoGlobal == null)
                                //            {
                                //                productoGlobal = new tbProducto();
                                //            }


                                //            productoGlobal.nombre = detalle.nombreProducto.Trim();
                                //            productoGlobal = productoIns.GetEntityByNombre(productoGlobal);


                                //            if (productoGlobal == null)
                                //            {
                                //                detalle.idProducto = "0";
                                //                detalle.utilidad = (decimal)Global.Usuario.tbEmpresa.tbParametrosEmpresa.First().utilidadBase;
                                //            }
                                //            else
                                //            {
                                //                detalle.idProducto = productoGlobal.idProducto;
                                //                detalle.utilidad = productoGlobal.utilidad1Porc;
                                //            }
                                //        }
                                //        else
                                //        {
                                //            detalle.idProducto = "0";
                                //            detalle.utilidad = (decimal)Global.Usuario.tbEmpresa.tbParametrosEmpresa.First().utilidadBase;
                                //        }


                                //        if (isNC)
                                //        {

                                //            tbDetalleCompras deta = compraOriginal.tbDetalleCompras.Where(x => x.codigoCabys == detalle.codigoCabys && x.numLinea == detalle.numLinea).FirstOrDefault();
                                //            detalle.idProducto = deta.idProducto;

                                //        }



                                //        medida.nomenclatura = ((XmlElement)item).GetElementsByTagName("UnidadMedida").Item(0).InnerText;
                                //        detalle.nomenclatura = medida.nomenclatura;

                                //        medida = medidaIns.GetEnityByNomenclatura(medida);
                                //        if (medida != null)
                                //        {
                                //            detalle.idMedida = medida.idTipoMedida;

                                //        }

                                //        string precio = ((XmlElement)item).GetElementsByTagName("PrecioUnitario").Item(0).InnerText.Trim();

                                //        detalle.precio = decimal.Parse(string.Format(precio, CultureInfo.CurrentCulture));

                                //        var cant = ((XmlElement)item).GetElementsByTagName("Cantidad").Item(0).InnerText.Trim();
                                //        detalle.cantidad = decimal.Parse(cant);

                                //        var montoTotal = ((XmlElement)item).GetElementsByTagName("MontoTotal").Item(0).InnerText.Trim();
                                //        detalle.montoTotal = decimal.Parse(montoTotal);

                                //        var des = ((XmlElement)item).GetElementsByTagName("Descuento").Item(0);
                                //        if (des != null)
                                //        {
                                //            detalle.montoTotaDesc = decimal.Parse(((XmlElement)des).GetElementsByTagName("MontoDescuento").Item(0).InnerText.Trim());

                                //        }


                                //        var impuesto = ((XmlElement)item).GetElementsByTagName("Impuesto").Item(0);
                                //        if (impuesto != null)
                                //        {
                                //            detalle.tarifaImp = decimal.Parse(((XmlElement)impuesto).GetElementsByTagName("Tarifa").Item(0).InnerText.Trim());
                                //            detalle.montoTotalImp = decimal.Parse(((XmlElement)impuesto).GetElementsByTagName("Monto").Item(0).InnerText.Trim());

                                //        }


                                //        var totaLinea = ((XmlElement)item).GetElementsByTagName("MontoTotalLinea").Item(0).InnerText.Trim();
                                //        detalle.montoTotalLinea = decimal.Parse(totaLinea);

                                //        detalle.numLinea = int.Parse( ((XmlElement)item).GetElementsByTagName("NumeroLinea").Item(0).InnerText);

                                //        //if (detalleDoc.Where(x=>x.idProducto==detalle.idProducto).SingleOrDefault()!=null)
                                //        //{
                                //        //    string mensaje = string.Format("La linea #{0} del XML se encuentra repetida({1}). Se eliminó del detalle.", ((XmlElement)item).GetElementsByTagName("NumeroLinea").Item(0).InnerText, detalle.nombreProducto.Trim());

                                //        //    MessageBox.Show(mensaje,"Linea repetida", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                //        //}
                                //        //else
                                //        //{



                                //        detalleDoc.Add(detalle);
                                //        //}



                                //    }
                                //    catch (Exception ex)
                                //    {

                                //        MessageBox.Show("La linea #" + ((XmlElement)item).GetElementsByTagName("NumeroLinea").Item(0).InnerText.Trim() + ", tiene un formato incorrecto y no se pudo procesar."
                                //            , "Linea con error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                //    }


                                //}



                                ////moneda
                                //var resumen = xDoc.GetElementsByTagName("ResumenFactura");

                                //if (resumen.Count != 0)
                                //{
                                //    var codigoMoneda = ((XmlElement)resumen[0]).GetElementsByTagName("CodigoTipoMoneda");                                     
                                //    cboMoneda.SelectedIndex = ((XmlElement)codigoMoneda[0]).GetElementsByTagName("CodigoMoneda").Item(0).InnerText.Trim() == "USD"?1:0;
                                //    txtTipoCambio.Text = ((XmlElement)codigoMoneda[0]).GetElementsByTagName("TipoCambio").Item(0).InnerText.Trim();


                                //}


                                //if (isNC)
                                //{ 


                                //    var tipoDocReF = ((XmlElement)xDoc.GetElementsByTagName("InformacionReferencia")[0]).GetElementsByTagName("TipoDoc").Item(0).InnerText;
                                //    var claveRef = ((XmlElement)xDoc.GetElementsByTagName("InformacionReferencia")[0]).GetElementsByTagName("Numero").Item(0).InnerText;
                                //    var fechaEmisionRef = ((XmlElement)xDoc.GetElementsByTagName("InformacionReferencia")[0]).GetElementsByTagName("FechaEmision").Item(0).InnerText;
                                //    var codigoRef = ((XmlElement)xDoc.GetElementsByTagName("InformacionReferencia")[0]).GetElementsByTagName("Codigo").Item(0).InnerText;
                                //    var razonRef = ((XmlElement)xDoc.GetElementsByTagName("InformacionReferencia")[0]).GetElementsByTagName("Razon").Item(0).InnerText;
                                //    //compraGlobal.tipoDoc = (int)Enums.TipoDocumento.NotaCredito;

                                //    cboTipoDocRef.SelectedText = Enum.GetName(typeof(Enums.TipoDocRef), int.Parse(tipoDocReF));
                                //   // cboTipoDocRef = int.Parse(tipoDocReF);
                                //    mskClaveRef.Text = claveRef;
                                //    dtpFechaRef.Value = DateTime.Parse( fechaEmisionRef);
                                //    cboEstadoRef.SelectedText = Enum.GetName(typeof(Enums.TipoRef), int.Parse(codigoRef));
                                //    txtRazonRef.Text = razonRef;
                                //    tbCompras compras = comprasIns.GetEntityComprasByClave(claveRef);
                                //    if (compras!=null)
                                //    {
                                //        foreach (var item in detalleDoc)
                                //        {
                                //            if (item.idProductoProveedor != "0" && !string.IsNullOrEmpty(item.idProductoProveedor))
                                //            {
                                //                var codigo = compras.tbDetalleCompras.Where(x => x.idProductoProveedor == item.idProductoProveedor).FirstOrDefault().idProducto;
                                //                if (codigo !=null)
                                //                {
                                //                    item.idProducto = codigo;
                                //                }


                                //            }
                                //        }

                                //    }



                                //}

                                #endregion

                                compraGlobal = Documentos.obtenerCompraXml(file);

                                //if (!compraGlobal.idEmpresa.Trim().Equals(Global.actividadEconomic.idEmpresa.Trim()))
                                // {

                                //    MessageBox.Show(string.Format("El documento #Factura[{1}-{2}] a procesar no pertenece a la empresa: {0}.", Global.actividadEconomic.idEmpresa.Trim(), compraGlobal.numFactura, compraGlobal.nombreProveedor.Trim()), "Documento incorrecto", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                //    return;
                                //}


                                if (compraGlobal.tipoDoc == (int)Enums.TipoDocumento.Compras)
                                {
                                    foreach (var detalle in compraGlobal.tbDetalleCompras)
                                    {
                                        detalle.actualizaInvent = true;
                                        detalle.actualizaPrecio = true;
                                        tbProducto producto = null;


                                        if ((int)cboBusquedaProducto.SelectedValue == (int)Enums.busquedaProductoCompraXML.Ambas)
                                        {
                                            //verifica por codigo barra de proveedor
                                            producto = detalle.idProductoProveedor==null? null: productoIns.GetEntityById(detalle.idProductoProveedor.Trim());
                                            //sino encuentra busca por nombre
                                            if (producto == null)
                                            {
                                                producto = new tbProducto();
                                                producto.nombre = detalle.nombreProducto;
                                                producto = productoIns.GetEntityByNombre(producto);
                                            }

                                        }
                                        else if ((int)cboBusquedaProducto.SelectedValue == (int)Enums.busquedaProductoCompraXML.CódigoProducto)
                                        {
                                            //verifica por codigo barra de proveedor
                                            producto = productoIns.GetEntityById(detalle.idProductoProveedor.Trim());


                                        }
                                        else if ((int)cboBusquedaProducto.SelectedValue == (int)Enums.busquedaProductoCompraXML.NombreProducto)
                                        {

                                            producto = new tbProducto();
                                            producto.nombre = detalle.nombreProducto;
                                            producto = productoIns.GetEntityByNombre(producto);
                                        }

                                       
                                       
                                        if (producto != null)
                                        {
                                            detalle.idProducto = producto.idProducto;
                                            detalle.utilidad = producto.utilidad1Porc;
                                            detalle.id_categoria = producto.id_categoria;

                                            detalle.tarifaImpVenta = listaImp.Where(x => x.id == producto.idTipoImpuesto).SingleOrDefault().valor;

                                            detalle.idMedidaVenta = producto.idMedida;
                                            detalle.idMedida = medidaIns.GetEnityByNomenclatura(detalle.nomenclatura.Trim()).idTipoMedida;
                                       

                                        }
                                        else
                                        {
                                            //sino encuentra pone codigo 0 para crearlo de 0
                                            detalle.idProducto = "0";
                                            detalle.utilidad = (decimal)Global.Usuario.tbEmpresa.tbParametrosEmpresa.First().utilidadBase;
                                            detalle.id_categoria = listaCategorias.FirstOrDefault().id;

                                            detalle.idMedida = medidaIns.GetEnityByNomenclatura(detalle.nomenclatura.Trim()).idTipoMedida;
                                            detalle.idMedidaVenta = detalle.idMedida;   

                                        }

                                    }
                                    chkActualiza.Checked = true;
                                    chkActualiza.Enabled = true;
                                }
                                else
                                {
                                    tbCompras compraOriginal = comprasIns.GetEntityComprasByClave(compraGlobal.claveRef);
                                    if (compraOriginal is null)
                                    {
                                        MessageBox.Show("No se ha regitrado la compra correspondiente a la nota de crédito seleccionada. Ingrese la compra primero.", "Falta Compra", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        MenuGenerico.CambioEstadoMenu(ref tlsMenu, (int)EnumMenu.OpcionMenu.Nuevo);
                                        Utility.EnableDisableForm(ref gbxCompras, false);
                                        
                                        
                                        
                                        limpiarForm();
                                        return;
                                    }

                                    foreach (var detalle in compraGlobal.tbDetalleCompras)
                                    {

                                        if (compraOriginal != null)
                                        {
                                            detalle.idProducto = compraOriginal.tbDetalleCompras.Where(x => x.numLinea == detalle.numLinea && x.codigoCabys == detalle.codigoCabys).SingleOrDefault().idProducto;
                                            detalle.utilidad = compraOriginal.tbDetalleCompras.Where(x => x.numLinea == detalle.numLinea && x.codigoCabys == detalle.codigoCabys).SingleOrDefault().utilidad;
                                            detalle.actualizaInvent = compraOriginal.tbDetalleCompras.Where(x => x.numLinea == detalle.numLinea && x.codigoCabys == detalle.codigoCabys).SingleOrDefault().actualizaInvent;
                                            detalle.actualizaPrecio = compraOriginal.tbDetalleCompras.Where(x => x.numLinea == detalle.numLinea && x.codigoCabys == detalle.codigoCabys).SingleOrDefault().actualizaPrecio;
                                        }
                                    }

                                    chkActualiza.Checked = true;
                                    chkActualiza.Enabled = false;
                                }

                                detalleDoc = (List<tbDetalleCompras>)compraGlobal.tbDetalleCompras;
                                cargarEncabezado(compraGlobal);
                                cargarTotales(detalleDoc);
                                //actualizarLineas(detalleDoc);
                                actualizarGridView(detalleDoc);
                                resetProducto();

                          

                            }

                        }

                        catch (Exception ex)
                        {
                            // Could not load the image - probably related to Windows file system permissions.
                            MessageBox.Show("El documento seleccionado no tiene el formato requerido. " + file.Substring(file.LastIndexOf('\\'))
                               );
                        }
                    }
                }
            }
            catch (Exception)
            {

                MessageBox.Show("Error al cargar la factura XML", "Factura XML", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cargarEncabezado(tbCompras compraGlobal)
        {
            btnBuscarProveedor.Enabled = false;
            mskClave.Text = compraGlobal.claveEmisor;
            mskConsecutivo.Text = compraGlobal.consecutivoEmisor;
            mskFactura.Text = compraGlobal.numFactura;
            txtProveedor.Text = compraGlobal.idProveedor + "-" + compraGlobal.nombreProveedor;
            cboTipoDoc.Text = Enum.GetName((typeof(Enums.TipoDocumento)), compraGlobal.tipoDoc);
            dtpFechaCompra.Value = compraGlobal.fechaCompra;
            txtActividad.Text = compraGlobal.CodActividad;
            cboTipoVenta.Text = Enum.GetName(typeof(Enums.tipoVenta), compraGlobal.tipoCompra);
            cboTipoPago.Text = Enum.GetName(typeof(Enums.tipoVenta), compraGlobal.tipoPago);
            if (cboTipoVenta.Text == Enum.GetName(typeof(Enums.tipoVenta), Enums.tipoVenta.Credito))
            {
                mskPlazoCredito.Text = compraGlobal.plazo.ToString();
            }
            cboMoneda.Text = Enum.GetName(typeof(Enums.TipoMoneda), (int)compraGlobal.tipoMoneda);
            if (compraGlobal.tipoMoneda == (int)Enums.TipoMoneda.USD)
            {
                txtTipoCambio.Text = compraGlobal.tipoCambio.ToString();
                chkCambiarMoneda.Visible = true;
                chkCambiarMoneda.Checked = true;
            }

            if (compraGlobal.tipoDoc == (int)Enums.TipoDocumento.NotaCredito)
            {

                cboTipoDocRef.Text = Enum.GetName(typeof(Enums.TipoDocRef), compraGlobal.tipoDocRef);
                mskClaveRef.Text = compraGlobal.claveRef;
                dtpFechaRef.Value = (DateTime)compraGlobal.fechaRef;
                cboEstadoRef.Text = Enum.GetName(typeof(Enums.TipoRef), compraGlobal.codigoRef);
                txtRazonRef.Text = compraGlobal.razon;

            }


        }

        private void chkActualiza_CheckedChanged(object sender, EventArgs e)
        {
            if (compraGlobal != null && compraGlobal.tbDetalleCompras != null)
            {
                foreach (var item in compraGlobal.tbDetalleCompras)
                {
                    item.actualizaPrecio = chkActualiza.Checked;
                   
                }

                actualizarGridView(compraGlobal.tbDetalleCompras);
            }
        }


        private void dtgvCompras_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                

                if (!banderaBusqueda)
                {
                    linea = int.Parse(dtgvCompras.Rows[e.RowIndex].Cells[2].Value.ToString());
                    if (e.ColumnIndex == 14)
                    {
       
                        if (linea != 0)
                        {

                            DialogResult result = MessageBox.Show("¿Desea eliminar el producto " + dtgvCompras.Rows[e.RowIndex].Cells[4].Value.ToString().Trim() + " de la Compra?", "Eliminar linea", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            if (result == DialogResult.Yes)
                            {
                                eliminarLinea(linea);

                            }
                        }
                        cargarTotales(detalleDoc);
                        actualizarGridView(detalleDoc);
                    }
                    else if (e.ColumnIndex == 0)
                    {
                    

                        if (detalleDoc.Where(x => x.numLinea == linea).SingleOrDefault() != null)
                        {
                                           
                            detalleDoc.Where(x => x.numLinea == linea).SingleOrDefault().actualizaPrecio = !detalleDoc.Where(x => x.numLinea == linea).SingleOrDefault().actualizaPrecio;
                           
                        }
                   
                        cargarTotales(detalleDoc);
                        actualizarGridView(detalleDoc);
                    }
                    else if (e.ColumnIndex == 1)
                    {
               
                        if (detalleDoc.Where(x => x.numLinea == linea).SingleOrDefault() != null)                        {
                         
                            detalleDoc.Where(x => x.numLinea == linea).SingleOrDefault().actualizaInvent = !detalleDoc.Where(x => x.numLinea == linea).SingleOrDefault().actualizaInvent;
                        }
                        cargarTotales(detalleDoc);
                        actualizarGridView(detalleDoc);
                    }                  
                    else if (e.ColumnIndex == 13)
                    {
              
                        frmDetalleLineaCompra detalle = new frmDetalleLineaCompra();
                        detalle.detalle = detalleDoc.Where(x => x.numLinea == linea).FirstOrDefault();
                        detalle.ShowDialog();

                    }
                    else if (e.ColumnIndex == 3)
                    {
               
                        frmBuscarProducto buscarProduct = new frmBuscarProducto();


                        buscarProduct.recuperarEntidad += recuperarEntidad;

                        buscarProduct.ShowDialog();
                        linea = 0;

                    }
                    else if (e.ColumnIndex == 10)
                    {
                        //DataGridViewComboBoxCell combo = dtgvCompras.Rows[e.RowIndex].Cells[e.ColumnIndex] as DataGridViewComboBoxCell;

                        //int idCategoria = Convert.ToInt32(combo.Value);
                        //string linea = dtgvCompras.Rows[e.RowIndex].Cells[1].Value.ToString();
                        //detalleDoc.Where(x => x.numLinea == int.Parse(linea)).FirstOrDefault().id_categoria = idCategoria;
                    }
                    else if (e.ColumnIndex == 9)
                    {
                        //DataGridViewComboBoxCell combo = dtgvCompras.Rows[e.RowIndex].Cells[e.ColumnIndex] as DataGridViewComboBoxCell;

                        //int idCategoria = Convert.ToInt32(combo.Value);
                        //string linea = dtgvCompras.Rows[e.RowIndex].Cells[1].Value.ToString();
                        //detalleDoc.Where(x => x.numLinea == int.Parse(linea)).FirstOrDefault().id_categoria = idCategoria;
                    }
                }
                
            }
            catch (Exception)
            {


            }

        }

        private void recuperarEntidad(tbProducto entidad)
        {
            if (entidad != null)
            {
                detalleDoc.Where(x => x.numLinea == linea).SingleOrDefault().idProducto = entidad.idProducto;
            }
            linea = 0;
            actualizarGridView(detalleDoc);
        }

        private void eliminarLinea(int linea)
        {

            foreach (var detalle in detalleDoc)
            {
                if (detalle.numLinea ==linea)
                {
                    detalleDoc.Remove(detalle);
                    break;
                }

            }

            cargarTotales(detalleDoc);
            actualizarGridView(detalleDoc);
        }

        private void dtgvCompras_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                if (!banderaBusqueda)
                {               
                    string linea = "";
                    decimal utilidad = 0, precioFinal = 0, precioCosto = 0;
                    try
                    {
                        //cuando cambia la cantidad
                        if (dtgvCompras.Columns[e.ColumnIndex].Name == "colUtilidad")
                        {
                            if (dtgvCompras.Rows[e.RowIndex].Cells[3].Value != null)
                            {

                                linea = dtgvCompras.Rows[e.RowIndex].Cells[2].Value.ToString();

                                if (dtgvCompras.Rows[e.RowIndex].Cells[10].Value != null)
                                {
                                    if (Utility.isNumeroDecimal((dtgvCompras.Rows[e.RowIndex].Cells[10].Value.ToString())))
                                    {
                                        utilidad = decimal.Parse(dtgvCompras.Rows[e.RowIndex].Cells[10].Value.ToString());
                                        detalleDoc.Where(x => x.numLinea == int.Parse(linea)).FirstOrDefault().utilidad = utilidad;

                                    }
                                    else
                                    {
                                        detalleDoc.Where(x => x.numLinea == int.Parse(linea)).FirstOrDefault().utilidad = 0;
                                        dtgvCompras.CancelEdit(); dtgvCompras.RefreshEdit();
                                        MessageBox.Show("Se produjo un error al cambiar los datos del producto, corrija los datos", "Ingreso de datos", MessageBoxButtons.OK, MessageBoxIcon.Error);

                                    }

                                }                    
                            }

                
                        }
                        else if (dtgvCompras.Columns[e.ColumnIndex].Name == "colId")
                        {
                            if (dtgvCompras.Rows[e.RowIndex].Cells[3].Value.ToString() != null && Utility.isNumeroDecimal(dtgvCompras.Rows[e.RowIndex].Cells[3].Value.ToString(), false))
                            {
                                detalleDoc.Where(x => x.numLinea == int.Parse(dtgvCompras.Rows[e.RowIndex].Cells[2].Value.ToString())).SingleOrDefault().idProducto = dtgvCompras.Rows[e.RowIndex].Cells[3].Value.ToString();
                                actualizarGridView(detalleDoc);
                            }
                            else
                            {
                                MessageBox.Show("El valor indicado no es correcto.", "Código", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }

                        }
                        else if (dtgvCompras.Columns[e.ColumnIndex].Name == "colPrecioVenta1")
                        {
                            linea = dtgvCompras.Rows[e.RowIndex].Cells[2].Value.ToString();
                            if (Utility.isNumeroDecimal((dtgvCompras.Rows[e.RowIndex].Cells[11].Value.ToString())) &&
                                        Utility.isNumeroDecimal((dtgvCompras.Rows[e.RowIndex].Cells[7].Value.ToString())))
                            {
                                
                               
                                var compra= detalleDoc.Where(x => x.numLinea == int.Parse(dtgvCompras.Rows[e.RowIndex].Cells[2].Value.ToString())).SingleOrDefault();
                                precioFinal = decimal.Parse(dtgvCompras.Rows[e.RowIndex].Cells[11].Value.ToString());
                                precioCosto = 0;

                                if (compra.cantidad == compra.cantidadVenta)
                                {
                                    precioCosto = (decimal)(((compra.precio * compra.cantidad) - compra.montoTotaDesc + compra.montoOtroImp) / compra.cantidad);

                                }
                                else
                                {
                                    precioCosto = (decimal)(((compra.precio * compra.cantidad) - compra.montoTotaDesc + compra.montoOtroImp) / compra.cantidadVenta);

                                }


                                ProductoDTO pro = ProductosUtility.calcularPrecioUtilidad(precioCosto, precioFinal, precioFinal, precioFinal, (decimal)detalleDoc.Where(x => x.numLinea == int.Parse(linea)).FirstOrDefault().tarifaImpVenta);

                                detalleDoc.Where(x => x.numLinea == int.Parse(linea)).FirstOrDefault().utilidad = pro.Utilidad1;
                               
                       
                            }
                            else
                            {
                                detalleDoc.Where(x => x.numLinea == int.Parse(linea)).FirstOrDefault().utilidad = 0;
                                dtgvCompras.CancelEdit(); dtgvCompras.RefreshEdit();
                                MessageBox.Show("Se produjo un error al cambiar los datos del producto, corrija los datos", "Ingreso de datos", MessageBoxButtons.OK, MessageBoxIcon.Error);

                            }
                       
                        }
                        else if (dtgvCompras.Columns[e.ColumnIndex].Name == "colNombre")
                        {
                            linea = dtgvCompras.Rows[e.RowIndex].Cells[2].Value.ToString();
                            if (dtgvCompras.Rows[e.RowIndex].Cells[4].Value.ToString() != string.Empty)
                            {

                                detalleDoc.Where(x => x.numLinea == int.Parse(linea)).FirstOrDefault().nombreProducto = dtgvCompras.Rows[e.RowIndex].Cells[4].Value.ToString().Trim().ToUpper();
                                //detalleDoc.Where(x => x.numLinea == int.Parse(linea)).FirstOrDefault(). = pro.Utilidad1;
                            }
                            else
                            {
                                dtgvCompras.Rows[e.RowIndex].Cells[4].Value = detalleDoc.Where(x => x.numLinea == int.Parse(linea)).FirstOrDefault().nombreProducto;
                                dtgvCompras.CancelEdit(); dtgvCompras.RefreshEdit();
                                MessageBox.Show("Debe indicar un nombre para el producto.", "Ingreso de datos", MessageBoxButtons.OK, MessageBoxIcon.Error);

                            }
                         
                        }
                        else if (dtgvCompras.Columns[e.ColumnIndex].Name == "colCategoria")
                        {
                            linea = dtgvCompras.Rows[e.RowIndex].Cells[2].Value.ToString();


                            detalleDoc.Where(x => x.numLinea == int.Parse(linea)).FirstOrDefault().id_categoria = int.Parse(dtgvCompras.Rows[e.RowIndex].Cells[12].Value.ToString());
                            //detalleDoc.Where(x => x.numLinea == int.Parse(linea)).FirstOrDefault(). = pro.Utilidad1;


                        }
                        else if (dtgvCompras.Columns[e.ColumnIndex].Name == "colImp")
                        {
                            linea = dtgvCompras.Rows[e.RowIndex].Cells[2].Value.ToString();


                            detalleDoc.Where(x => x.numLinea == int.Parse(linea)).FirstOrDefault().tarifaImpVenta = decimal.Parse(dtgvCompras.Rows[e.RowIndex].Cells[8].Value.ToString());
                            //detalleDoc.Where(x => x.numLinea == int.Parse(linea)).FirstOrDefault(). = pro.Utilidad1;


                        }
                        else if (dtgvCompras.Columns[e.ColumnIndex].Name == "colCant")
                        {
                            linea = dtgvCompras.Rows[e.RowIndex].Cells[2].Value.ToString();


                            detalleDoc.Where(x => x.numLinea == int.Parse(linea)).FirstOrDefault().cantidadVenta = int.Parse(dtgvCompras.Rows[e.RowIndex].Cells[5].Value.ToString());
                            //detalleDoc.Where(x => x.numLinea == int.Parse(linea)).FirstOrDefault(). = pro.Utilidad1;
                           

                        }

                        cargarTotales(detalleDoc);
                        actualizarGridView(detalleDoc);
                    }
                    catch (Exception ex)
                    {
                        dtgvCompras.CancelEdit(); dtgvCompras.RefreshEdit();
                        MessageBox.Show("Se produjo un error al cambiar los datos del producto, corrija los datos", "Ingreso de datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }


        }

        private void tlsBtnCancelar_Click(object sender, EventArgs e)
        {

        }

        private void cboMoneda_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (((ComboBox)sender).Text == "USD")
            {
                chkCambiarMoneda.Visible = true;
                chkCambiarMoneda.Enabled = true;
                txtTipoCambio.Visible = true;


            }
            else
            {
                chkCambiarMoneda.Visible = false;
                chkCambiarMoneda.Enabled = false;
                txtTipoCambio.Visible = false;
            }
        }

        private void cboTipoDoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (((ComboBox)sender).Text != string.Empty)
            {
                if (((ComboBox)sender).Text == "Nota de Crédito")
                {

                    chkActualiza.Checked = false;
                    chkActualiza.Enabled = false;


                }
                else
                {
                    chkActualiza.Checked = true;
                    chkActualiza.Enabled = true;
                }


            }

        }

        private void chkCambiarMoneda_CheckedChanged(object sender, EventArgs e)
        {


            foreach (var detalle in detalleDoc)
            {
                if (chkCambiarMoneda.Checked)
                {

                    detalle.precio = detalle.precio * decimal.Parse(txtTipoCambio.Text);
                    detalle.montoTotal = detalle.precio * detalle.cantidad;

                    detalle.utilidad = (decimal)Global.Usuario.tbEmpresa.tbParametrosEmpresa.First().utilidadBase;
                    detalle.montoTotaDesc = detalle.montoTotaDesc * decimal.Parse(txtTipoCambio.Text);
                    detalle.montoTotalImp = detalle.montoTotalImp * decimal.Parse(txtTipoCambio.Text);
                    detalle.montoTotalExo = detalle.montoTotalExo * decimal.Parse(txtTipoCambio.Text);
                    detalle.montoTotalLinea = detalle.montoTotalLinea * decimal.Parse(txtTipoCambio.Text);

                }
                else
                {
                    detalle.precio = detalle.precio / decimal.Parse(txtTipoCambio.Text);
                    detalle.montoTotal = detalle.precio / detalle.cantidad;

                    detalle.utilidad = (decimal)Global.Usuario.tbEmpresa.tbParametrosEmpresa.First().utilidadBase;
                    detalle.montoTotaDesc = detalle.montoTotaDesc / decimal.Parse(txtTipoCambio.Text);
                    detalle.montoTotalImp = detalle.montoTotalImp / decimal.Parse(txtTipoCambio.Text);
                    detalle.montoTotalExo = detalle.montoTotalExo / decimal.Parse(txtTipoCambio.Text);
                    detalle.montoTotalLinea = detalle.montoTotalLinea / decimal.Parse(txtTipoCambio.Text);
                }


            }

            cargarTotales(detalleDoc);
            actualizarGridView(detalleDoc);
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (compraGlobal != null && compraGlobal.tbDetalleCompras != null)
            {
                foreach (var item in compraGlobal.tbDetalleCompras)
                {
                    item.actualizaInvent = chkActualizaInventario.Checked;

                }

                actualizarGridView(compraGlobal.tbDetalleCompras);
            }
        }

        //private void dtgvCompras_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        //{
        //    //linea = dtgvCompras.Rows[e.RowIndex].Cells[1].Value.ToString();

        //    combo = e.Control as ComboBox;
        //    if (combo != null)
        //    {
        //        // AVOID ATTACHMENT TO MULTIPLE EVENT HANDLERS
        //        combo.SelectedIndexChanged -= new EventHandler(combo_SelectedIndexChanged);

        //        //THEN NOW ADD
        //        combo.SelectedIndexChanged += combo_SelectedIndexChanged;
        //    }
        //}
        //private void combo_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    //int categoriaId = ((tbCategoriaProducto)(sender as ComboBox).SelectedItem).id;




        //}
    }
}
