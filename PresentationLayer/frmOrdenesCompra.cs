using BusinessLayer;
using CommonLayer;
using CommonLayer.Exceptions.BusisnessExceptions;
using CommonLayer.Exceptions.DataExceptions;
using EntityLayer;
using PresentationLayer.Clases;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace PresentationLayer
{
    public partial class frmOrdenesCompra : Form
    {
        BOrdenCompra ordenIns = new BOrdenCompra();

        tbProveedores proveedorGlobal;
        tbProducto productoGlobal;
        private List<tbProducto> produtosAgregados = new List<tbProducto>();
        List<tbDetalleOrdenCompra> detalleDoc = new List<tbDetalleOrdenCompra>();
        tbOrdenCompra documento = null;

        private int bandera;

        BProducto productoIns = new BProducto();
        bTipoMedidas medidaIns = new bTipoMedidas();

        public frmOrdenesCompra()
        {
            InitializeComponent();
        }

        private void dtgvCompras_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 7)
            {
                string codigoProducto = dtgvCompras.Rows[e.RowIndex].Cells[1].Value.ToString();
                if (codigoProducto != string.Empty)
                {

                    DialogResult result = MessageBox.Show("¿Desea eliminar el producto " + dtgvCompras.Rows[e.RowIndex].Cells[2].Value.ToString().Trim() + " de la Orden de Compra?", "Eliminar linea", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        eliminarLinea(codigoProducto);

                    }
                }
            }
        }
        private void eliminarLinea(string codigoProducto)
        {
            foreach (var detalle in detalleDoc)
            {
                if (detalle.idProducto.Trim().ToUpper() == codigoProducto.Trim().ToUpper())
                {
                    detalleDoc.Remove(detalle);
                    break;
                }
            }


            actualizarGridView(detalleDoc);
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
                proveedorGlobal = proveedor;

                txtCorreo.Text = documento == null ? proveedor.tbPersona.correoElectronico.Trim() : documento.correo1.Trim();
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

        private void frmOrdenesCompra_Load(object sender, EventArgs e)
        {

            //deshabilitamos el formulario al iniciar la carga.
            MenuGenerico.CambioEstadoMenu(ref tlsMenu, (int)EnumMenu.OpcionMenu.Nuevo);

            Utility.EnableDisableForm(ref gbxCompras, false);


            cargarCombos();
            limpiarForm();
            resetProducto();
        }

        private void cargarCombos()
        {

            cboTipoVenta.DataSource = Enum.GetValues(typeof(Enums.tipoVenta));

        }

        private void btnBuscarProducto_Click(object sender, EventArgs e)
        {
            buscarProducto();
        }

        private void datosProducto(tbProducto producto)
        {
            if (producto != null)
            {
                produtosAgregados.Add(producto);
                productoGlobal = producto;
                txtCodigoProducto.Text = producto.idProducto;

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

        private void actualizarGridView(IEnumerable<tbDetalleOrdenCompra> lista)
        {
            dtgvCompras.Rows.Clear();
            tbTipoMedidas medida = new tbTipoMedidas();
            lista = lista.OrderBy(x => x.numLinea).ToList();
            foreach (tbDetalleOrdenCompra detalle in lista)
            {
                if (productoGlobal == null)
                {
                    productoGlobal = new tbProducto();
                }


                productoGlobal.idProducto = detalle.idProducto.Trim();
                productoGlobal = productoIns.GetEntity(productoGlobal);                //Creamos un objeto de tipo ListviewItem
                DataGridViewRow row = (DataGridViewRow)dtgvCompras.Rows[0].Clone();

                medida.idTipoMedida = detalle.idMedida;
                medida = medidaIns.GetEnityById(medida);
                row.Cells[0].Value = detalle.numLinea.ToString().Trim();
                row.Cells[1].Value = detalle.idProducto.ToString().Trim();
                row.Cells[2].Value = productoGlobal.nombre.Trim();
                row.Cells[3].Value = medida.nomenclatura.Trim();
                row.Cells[4].Value = string.Format("{0:N2}", detalle.cantidad);
                row.Cells[5].Value = string.Format("{0:N2}", productoGlobal.precio_real);
                row.Cells[6].Value = string.Format("{0:N2}", (detalle.cantidad * productoGlobal.precio_real));
                dtgvCompras.Rows.Add(row);
            }

            txtTotal.Text = string.Format("{0:N2}", lista.Sum(x => x.cantidad * x.precioCosto));

        }

        private void actualizarLineas(List<tbDetalleOrdenCompra> detalleDoc)
        {
            int conta = 1;
            foreach (var item in detalleDoc)
            {
                item.numLinea = conta;
                conta++;
            }
        }

        public void limpiarForm()
        {
            documento = null;
            proveedorGlobal = null;
            dtgvCompras.Rows.Clear();
            detalleDoc.Clear();
            cboTipoVenta.SelectedIndex = 0;
            txtCorreo.ResetText();
            chkCorreo.Checked = true;
            txtObservaciones.ResetText();
            txtProveedor.Text = string.Empty;
            dtpFechaCompra.Refresh();
            resetProducto();
            dtgvCompras.Enabled = false;
            mskPlazoCredito.ResetText();
            tlsReporte.Enabled = false;
            txtTotal.ResetText();
            txtTotal.Text = "0";
        }

        private void resetProducto()
        {
            txtCodigoProducto.ResetText();

        }

        private bool validarCampos(string idProd)
        {
            //if (txtCodigoProducto.Text == string.Empty)
            //{
            //    txtCodigoProducto.Focus();
            //    MessageBox.Show("Debe indicar el código del producto", "Error de datos");
            //    return false;
            //}
            if (detalleDoc.Where(x => x.idProducto.Trim() == idProd.Trim()).SingleOrDefault() != null)
            {
                txtCodigoProducto.Focus();
                MessageBox.Show("El producto ya se encuentra en la lista.", "Error de datos");
                return false;
            }
            //if (txtNombreProducto.Text == string.Empty)
            //{
            //    txtNombreProducto.Focus();
            //    MessageBox.Show("Debe indicar el nombre del producto", "Error de datos");
            //    return false;
            //}
            //if (cboMedida.Text == string.Empty)
            //{
            //    cboMedida.Focus();
            //    MessageBox.Show("No se indicó una medida del producto", "Error de datos");
            //    return false;
            //}           
            //if (txtCantidad.Text == string.Empty)
            //{
            //    txtCantidad.Focus();
            //    MessageBox.Show("Debe indicar la cantidad del producto", "Error de datos");
            //    return false;
            //}         

            return true;
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
                        Utility.EnableDisableForm(ref gbxCompras, false);
                        limpiarForm();

                    }
                    break;

                case "Nuevo":
                    bandera = (int)Enums.accionGuardar.Nuevo;
                    MenuGenerico.CambioEstadoMenu(ref tlsMenu, (int)EnumMenu.OpcionMenu.Guardar);
                    Utility.EnableDisableForm(ref gbxCompras, true);


                    limpiarForm();
                    dtgvCompras.Enabled = true;
                    break;

                case "Modificar":
                    bandera = (int)Enums.accionGuardar.Modificar;
                    MenuGenerico.CambioEstadoMenu(ref tlsMenu, (int)EnumMenu.OpcionMenu.Guardar);
                    Utility.EnableDisableForm(ref gbxCompras, true);
                    txtCodigoProducto.Enabled = false;
                    dtgvCompras.Enabled = true;

                    break;
                case "Eliminar":
                    bandera = (int)Enums.accionGuardar.Eliminar;
                    MenuGenerico.CambioEstadoMenu(ref tlsMenu, (int)EnumMenu.OpcionMenu.Guardar);
                    break;

                case "Buscar":

                    if (buscar())
                    {
                        if (documento != null)
                        {
                            MenuGenerico.CambioEstadoMenu(ref tlsMenu, (int)EnumMenu.OpcionMenu.Modificar);
                            if (documento.estadoOrden == (int)Enums.EstadoOrdenCompra.Enviada)
                            {
                                tlsBtnModificar.Enabled = false;
                                tlsBtnEliminar.Enabled = false;

                            }
                            else if (documento.estadoOrden == (int)Enums.EstadoOrdenCompra.EnProceso)
                            {
                                Utility.EnableDisableForm(ref gbxCompras, false);
                                //tlsBtnModificar.Enabled = false;
                            }
                            tlsReporte.Enabled = true;

                        }


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

                case "Reporte":
                    cargarReporte();
                    break;
            }

        }

        private void cargarReporte()
        {
            try
            {

                frmReportes reporte = new frmReportes();
                reporte.reporte = (int)Enums.reportes.ordenCompra;
                reporte.orden = documento.id;
                reporte.ShowDialog();
            }
            catch (Exception)
            {

                MessageBox.Show("Error al cargar el reporte de la Orden de Compra", "Reporte", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }

        private bool buscar()
        {

            try
            {
                frmBuscarOrdenesCompra buscar = new frmBuscarOrdenesCompra();
                buscar.pasarDatosEvent += dataBuscar;
                buscar.ShowDialog();
                return true;
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error al realizar la busqueda de la Orden de Compra", "Orden de Compra", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return true;
            }

        }

        private void dataBuscar(tbOrdenCompra orden)
        {
            if (orden != null)
            {

                documento = orden;

                cargarForm();

            }
        }

        private void cargarForm()
        {
            actualizarGridView(documento.tbDetalleOrdenCompra);
            datosProveedor(documento.tbProveedores);
            foreach (var item in documento.tbDetalleOrdenCompra)
            {
                detalleDoc.Add(item);
            }

            chkCorreo.Checked = documento.notificarCorreo;
            txtObservaciones.Text = documento.observaciones.Trim().ToUpper();
            dtpFechaCompra.Value = documento.fecha;
            mskPlazoCredito.Text = documento.plazo.ToString();
            cboTipoVenta.Text = Enum.GetName(typeof(Enums.tipoVenta), documento.tipoPago);
        }

        private bool accionGuardar(int trans)
        {
            bool isOK = false;
            switch (trans)
            {

                case (int)Enums.accionGuardar.Nuevo:
                    isOK = guardar();
                    break;

                case (int)Enums.accionGuardar.Modificar:
                    isOK = actualizar();
                    break;

                case (int)Enums.accionGuardar.Eliminar:
                    isOK = eliminar();
                    break;

            }

            return isOK;

        }

        private bool actualizar()
        {
            bool processOk = false;
            try
            {
                if (validarCamposDoc())
                {

                    documento.fecha = Utility.getDate();
                    documento.tipoIdProveedor = proveedorGlobal.tipoId;
                    documento.idProveedor = proveedorGlobal.id;

                    documento.idEmpresa = Global.Usuario.tbEmpresa.id;
                    documento.tipoIdEmpresa = Global.Usuario.tbEmpresa.tipoId;

                    documento.tipoPago = (int)cboTipoVenta.SelectedValue;
                    documento.correo1 = txtCorreo.Text.Trim();
                    documento.notificarCorreo = chkCorreo.Checked;

                    documento.plazo = (int)documento.tipoPago == (int)Enums.tipoVenta.Credito ? int.Parse(mskPlazoCredito.Text) : 0;

                    documento.observaciones = txtObservaciones.Text;

                    documento.estadoOrden = (int)Enums.EstadoOrdenCompra.EnProceso;
                    documento.tbDetalleOrdenCompra = detalleDoc;

                    documento.estado = true;

                    //Atributos de Auditoria


                    documento.fecha_ult_mod = Utility.getDate();
                    documento.usuario_ult_mod = Global.Usuario.nombreUsuario.Trim().ToUpper();   // Global.Usuario.nombreUsuario;


                    DialogResult resul = MessageBox.Show("¿Desea cerrar la Orden de Compra?", "Orden de Compra", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (resul == DialogResult.Yes)
                    {
                        documento.estadoOrden = (int)Enums.EstadoOrdenCompra.Enviada;
                    }

                    documento = ordenIns.Modificar(documento);

                    if (Global.Configuracion.enviaCorreos == (int)Enums.EstadoConfig.Si)
                    {
                        if (documento.estadoOrden == (int)Enums.EstadoOrdenCompra.Enviada)
                        {
                            if (documento.notificarCorreo)
                            {
                                enviarCorreo(documento);
                            }

                        }

                    }

                    MessageBox.Show("La Orden de Compra han sido actualizada.", "Actualización.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    processOk = true;
                }
            }
            catch (UpdateEntityException ex)
            {
                MessageBox.Show("Ha ocurrido un error. Comuniquese con el administrador " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                processOk = false;

            }

            return processOk;
        }

        private bool guardar()
        {
            try
            {

                if (validarCamposDoc())
                {

                    documento = crearDocumento();
                    DialogResult resul = MessageBox.Show("¿Desea cerrar la Orden de Compra?", "Orden de Compra", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (resul == DialogResult.Yes)
                    {
                        documento.estadoOrden = (int)Enums.EstadoOrdenCompra.Enviada;
                    }
                    ordenIns.Guardar(documento);


                    if (Global.Configuracion.enviaCorreos == (int)Enums.EstadoConfig.Si)
                    {
                        if (documento.estadoOrden == (int)Enums.EstadoOrdenCompra.Enviada)
                        {
                            if (documento.notificarCorreo)
                            {
                                enviarCorreo(documento);
                            }

                        }

                    }

                    MessageBox.Show("La Orden de Compra se ha guardado correctamente.", "Orden de Compra", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    limpiarForm();


                }
                else
                {
                    return false;
                }
                return true;

            }

            catch (EntityExistException ex)
            {

                MessageBox.Show("Error al guardar la Orden de Compra. Ya se encuentra registrada", "Orden de Compra", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            catch (Exception ex)
            {

                MessageBox.Show("Error al guardar la Orden de Compra.", "Orden de Compra", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }


        }

        private void enviarCorreo(tbOrdenCompra doc)
        {
            try
            {
                List<string> correos = new List<string>();
                if (doc.correo1 != null)
                {
                    correos.Add(doc.correo1.Trim());

                }



                clsDocumentoCorreo docCorreo = new clsDocumentoCorreo(doc, correos, true, (int)Enums.tipoAdjunto.ordenCompra);

                CorreoElectronico.enviarCorreo(docCorreo);


            }
            catch (Exception)
            {

                MessageBox.Show("No se pudo enviar el correo electrónico", "Correo Electrónico", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }


        }

        private bool eliminar()
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

                        documento.estado = false;
                        documento.fecha_ult_mod = Utility.getDate();
                        documento.usuario_ult_mod = Global.Usuario.nombreUsuario;
                        documento = ordenIns.Eliminar(documento);
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
        private tbOrdenCompra crearDocumento()
        {
            tbOrdenCompra documento = new tbOrdenCompra();


            documento.fecha = Utility.getDate();
            documento.tipoIdProveedor = proveedorGlobal.tipoId;
            documento.idProveedor = proveedorGlobal.id;

            documento.idEmpresa = Global.Usuario.tbEmpresa.id;
            documento.tipoIdEmpresa = Global.Usuario.tbEmpresa.tipoId;

            documento.tipoPago = (int)cboTipoVenta.SelectedValue;
            documento.correo1 = txtCorreo.Text.Trim();
            documento.notificarCorreo = chkCorreo.Checked;

            documento.plazo = (int)documento.tipoPago == (int)Enums.tipoVenta.Credito ? int.Parse(mskPlazoCredito.Text) : 0;

            documento.observaciones = txtObservaciones.Text;

            documento.estadoOrden = (int)Enums.EstadoOrdenCompra.EnProceso;
            documento.tbDetalleOrdenCompra = detalleDoc;

            documento.estado = true;

            //Atributos de Auditoria

            documento.fecha_crea = Utility.getDate();
            documento.fecha_ult_mod = Utility.getDate();
            documento.usuario_crea = Global.Usuario.nombreUsuario.Trim().ToUpper();   // Global.Usuario.nombreUsuario;
            documento.usuario_ult_mod = Global.Usuario.nombreUsuario.Trim().ToUpper();   // Global.Usuario.nombreUsuario;

            return documento;
        }


        private bool validarCamposDoc()
        {

            if (proveedorGlobal == null)
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
            if (detalleDoc.Count == 0)
            {
                txtCodigoProducto.Focus();
                MessageBox.Show("Debe agregar productos a la Orden de Compra", "Error de datos");
                return false;
            }


            return true;
        }

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

        private void dtgvCompras_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                //cuando cambia la cantidad
                if (dtgvCompras.Columns[e.ColumnIndex].Name == "colCant")
                {
                    if (dtgvCompras.Rows[e.RowIndex].Cells[1].Value != null)
                    {
                        tbProducto prod;
                        string codigoProducto = dtgvCompras.Rows[e.RowIndex].Cells[1].Value.ToString();

                        if (dtgvCompras.Rows[e.RowIndex].Cells[4].Value != null)
                        {

                            decimal cantidad = decimal.Parse(dtgvCompras.Rows[e.RowIndex].Cells[4].Value.ToString());

                            detalleDoc.Where(x => x.idProducto == codigoProducto).SingleOrDefault().cantidad = cantidad;

                            actualizarGridView(detalleDoc);
                        }

                    }
                }


            }
            catch (Exception ex)
            {
                dtgvCompras.CancelEdit(); dtgvCompras.RefreshEdit();
                MessageBox.Show("Se produjo un error al cambiar los datos del producto, corrija los datos", "Ingreso de datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmOrdenesCompra_KeyUp(object sender, KeyEventArgs e)
        {
            if (dtgvCompras.Enabled)
            {
                if (e.KeyCode == Keys.F4)
                {
                    try
                    {
                        buscarProducto();
                    }
                    catch (Exception)
                    {

                        MessageBox.Show("Error al cargar el producto/Servicio", "Buscar Producto", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }

            }

            else if (e.KeyCode == Keys.F5)
            {
                buscarProveedor();

            }
        }

        private void buscarProducto()
        {
            frmBuscarProducto buscarProduct = new frmBuscarProducto();

            //Asignamos el metodo a la lista en el evento.
            buscarProduct.recuperarEntidadCant += recuperarEntidad;
            buscarProduct.tipoBusqueda = 2;
            buscarProduct.ShowDialog();
        }

        private void recuperarEntidad(tbProducto entidad, decimal cant)
        {
            try
            {
                if (validarCampos(entidad.idProducto))
                {
                    tbDetalleOrdenCompra detalle = new tbDetalleOrdenCompra();

                    detalle.idProducto = entidad.idProducto.Trim();
                    detalle.idMedida = entidad.idMedida;
                    detalle.cantidad = cant;
                    detalle.precioCosto = entidad.precio_real;
                 

                    detalleDoc.Add(detalle);
                    actualizarLineas(detalleDoc);
                    actualizarGridView(detalleDoc);
                    resetProducto();

                }


            }
            catch (Exception)
            {

                MessageBox.Show("Se produjo un error al agregar el producto", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnImportar_Click(object sender, EventArgs e)
        {
            frmBuscarDocumentos form = new frmBuscarDocumentos();
            form.tipoBusquedaDevolver = true;
            form.pasarDatosEvent += docImportado;

            form.ShowDialog();
        }

        private void docImportado(tbDocumento doc)
        {
            detalleDoc.Clear();
            if (doc != null)
            {

                foreach (var det in doc.tbDetalleDocumento.OrderBy(x=>x.numLinea))
                {

                    tbDetalleOrdenCompra detalle = new tbDetalleOrdenCompra();

                    detalle.idProducto = det.idProducto.Trim();
                    detalle.idMedida = det.tbProducto.idMedida;
                    detalle.cantidad = det.cantidad;
                    detalle.precioCosto = (decimal)det.precioCosto;
                    detalle.numLinea = det.numLinea;

                    detalleDoc.Add(detalle);

                }
                
                actualizarGridView(detalleDoc);
                resetProducto();

            }
        }
    }
}
