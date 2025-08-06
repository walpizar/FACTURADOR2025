using BusinessLayer;
using CommonLayer;
using CommonLayer.Exceptions.BusisnessExceptions;
using CommonLayer.Exceptions.DataExceptions;
using EntityLayer;
using PresentationLayer.Reportes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Zen.Barcode;
using static PresentationLayer.Reportes.dsEtiquetas;

namespace PresentationLayer
{
    public partial class frmProductos2 : Form
    {
        bool isBotonCalcular = false;

        IEnumerable<tbImpuestos> listaImpuestos;

        tbProducto productoGlo = new tbProducto();

        //tbCategoria9Cabys categoriaCABYS;
        //// Creamos una nueva instancia de tipo tbDetalleProducto que enviaremos a eliminar.
        BProducto productoIns = new BProducto();
        BCategoriaProducto CatProductosIns = new BCategoriaProducto();
        BImpuestos ImpIns = new BImpuestos();
        bTipoMedidas medidaIns = new bTipoMedidas();
        tbProveedores proveedor = null;

        int bandera = 1;


        public frmProductos2()
        {
            InitializeComponent();
        }

        private void frmProductos2_Load(object sender, EventArgs e)
        {
           
            
            //deshabilitamos el formulario al iniciar la carga.
            MenuGenerico.CambioEstadoMenu(ref tlsMenu, (int)EnumMenu.OpcionMenu.Nuevo);
            Utility.EnableDisableForm(ref gboDatosProducto, false);

            txtCosto.Enabled = false;

            listaImpuestos = ImpIns.getImpuestos(1);
            cargarCombos();
            cboImpuesto.SelectedIndex = 7; ;
            cboCategoriaProducto.SelectedIndex = 0;
            cboMedida.SelectedIndex = 6;
            reset();
            cargarOpciones();
            txtCosto.Enabled = false;
            chkCocina.Visible = (Global.Configuracion.imprimeExtra == (int)Enums.Estado.Activo);

            chkExento.Enabled = !(bool)Global.Usuario.tbEmpresa.regimenSimplificado;
            chkAplicaExo.Enabled = !(bool)Global.Usuario.tbEmpresa.regimenSimplificado;
            chkExento.Checked = (bool)Global.Usuario.tbEmpresa.regimenSimplificado;
            chkAplicaExo.Checked = !(bool)Global.Usuario.tbEmpresa.regimenSimplificado;

        }

        private void cargarOpciones()
        {
            if (Global.Usuario.tipoId == (int)Enums.roles.facturador)
            {
                txtCosto.Enabled = false;
            }
            else
            {
                txtCosto.Enabled = true;
            }

        }

        private void cargarCombos()
        {



            cboCategoriaProducto.ValueMember = "id";
            cboCategoriaProducto.DisplayMember = "nombre";
            cboCategoriaProducto.DataSource = CatProductosIns.getCategorias((int)Enums.EstadoBusqueda.Activo);


            cboImpuesto.ValueMember = "id";
            cboImpuesto.DisplayMember = "descripcion";
            cboImpuesto.DataSource = listaImpuestos;

            cboMedida.ValueMember = "idTipoMedida";
            cboMedida.DisplayMember = "nombre";
            cboMedida.DataSource = medidaIns.getlistatipomedida();

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void buscarProveedor()
        {
            try
            {

                frmBuscarProveedores buscarProve = new frmBuscarProveedores();
                buscarProve.pasarDatosEvent += dataBuscar;

                buscarProve.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void dataBuscar(tbProveedores prove)
        {
            if (prove != null)
            {
                proveedor = prove;
                txtIdProveedor.Text = prove.id;
            }


        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (chkExento.Checked)
            {
                cboImpuesto.SelectedIndex = 0;
                cboImpuesto.Enabled = false;

                //txtPrecioVenta1.Enabled = true;
                //txtPrecioVenta2.Enabled = true;
                //txtPrecioVenta3.Enabled = true;

            }
            else
            {
                //txtPrecioVenta1.Enabled = false;
                //txtPrecioVenta2.Enabled = false;
                //txtPrecioVenta3.Enabled = false;
                cboImpuesto.Enabled = true;
                cboImpuesto.SelectedIndex = 7;

            }
            calcularMontos();
        }

        private void txtCosto_TextChanged(object sender, EventArgs e)
        {


            isBotonCalcular = false;
            if (!isBotonCalcular)
            {
                completarUtilidad();
                calcularMontos();
            }
        }
        private void completarUtilidad()
        {

            float utilidadBase = Global.Usuario.tbEmpresa.tbParametrosEmpresa.ElementAt(0).utilidadBase;
            if (txtUtilidad1.Text == string.Empty || txtUtilidad1.Text == "0")
            {
                txtUtilidad1.Text = utilidadBase.ToString();

            }
            if (txtUtilidad2.Text == string.Empty || txtUtilidad2.Text == "0")
            {
                txtUtilidad2.Text = utilidadBase.ToString();

            }
            if (txtUtilidad3.Text == string.Empty || txtUtilidad3.Text == "0")
            {
                txtUtilidad3.Text = utilidadBase.ToString();

            }
        }


        private void txtUtilidad1_TextChanged(object sender, EventArgs e)
        {

            if (!isBotonCalcular)
            {
                calcularMontos();
            }
            isBotonCalcular = false;
        }

        private void txtUtilidad2_TextChanged(object sender, EventArgs e)
        {

            if (!isBotonCalcular)
            {
                calcularMontos();
            }
            isBotonCalcular = false;
        }

        private void txtUtilidad3_TextChanged(object sender, EventArgs e)
        {

            if (!isBotonCalcular)
            {
                calcularMontos();
            }
            isBotonCalcular = false;
        }

        private void cboImpuesto_SelectedIndexChanged(object sender, EventArgs e)
        {
            isBotonCalcular = false;
            if (!isBotonCalcular)
            {
                calcularMontos();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            buscarProveedor();
        }

        private void chkDescuento_CheckedChanged(object sender, EventArgs e)
        {
            if (chkDescuento.Checked)
            {
                txtDescuentoMax.Text = "0";
                txtDescuentoMax.Enabled = true;
                
              

            }
            else
            {
                txtDescuentoMax.Text = "0";
                txtDescuentoMax.Enabled = false;
                
            }
         
        }

        private void tlsMenu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

            accionMenu(e.ClickedItem.Text);
        }

        private bool accionGuardar(int trans)
        {
            bool isOK = false;
            switch (trans)
            {

                case (int)Enums.accionGuardar.Nuevo:
                    isOK = guardarProducto();
                    break;

                case (int)Enums.accionGuardar.Modificar:
                    isOK = actualizarProducto();
                    break;

                case (int)Enums.accionGuardar.Eliminar:
                    isOK = eliminarProducto();
                    break;

            }

            return isOK;

        }

        /// <summary>
        /// Validamos los campos en el formulario de producto.
        /// </summary>
        /// <returns></returns>
        private bool validarCampos()
        {
            bool isOK = true;

            //if (txtCodigoProducto.Text == string.Empty)
            //{
            //    MessageBox.Show("Debe ingresar el codigo del producto", "Error");
            //    txtCodigoProducto.Focus();
            //    isOK = false;

            // } else

            if (txtNombreProducto.Text == string.Empty)
            {
                MessageBox.Show("Debe ingresar un nombre del producto", "Error");
                txtNombreProducto.Focus();
                isOK = false;

            }
            else if (txtCosto.Text == string.Empty || txtCosto.Text == "0")
            {

                MessageBox.Show("Debe ingresar el precio de costo del producto", "Error");
                txtCosto.Focus();
                isOK = false;
            }
            else if (cboCategoriaProducto.Text == string.Empty)
            {
                MessageBox.Show("Debe ingresar una categoria del producto.", "Error");
                isOK = false;
            }
            else if (cboMedida.Text == string.Empty)
            {
                MessageBox.Show("Debe ingresar una medida del producto.", "Error");
                isOK = false;
            }
            else if (!chkExento.Checked && (cboImpuesto.Text == string.Empty || cboImpuesto.Text == "0"))
            {
                MessageBox.Show("Debe ingresar los impuestos correspondientes del producto.", "Error");
                isOK = false;
            }
            else if ((txtUtilidad1.Text == string.Empty) && (txtUtilidad2.Text == string.Empty) && (txtUtilidad3.Text == string.Empty))
            {

                MessageBox.Show("Debe ingresar utilidad al producto.", "Error");
                isOK = false;
            }
            else if (chkDescuento.Checked && (txtDescuentoMax.Text == string.Empty || txtDescuentoMax.Text == "0"))
            {
                MessageBox.Show("Debe ingresar el descuento máximo del producto.", "Error");
                isOK = false;
            }
            else if (txtCategoriaCabys.Text == string.Empty)
            {
                MessageBox.Show("Debe ingresar la categoria CABYS del producto.", "Error");
                isOK = false;

            }
            else if (txtPrecioVenta1.Text == string.Empty)
            {
                MessageBox.Show("Debe ingresar el precio de venta 1.", "Error");
                isOK = false;

            }
            else if (txtPrecioVenta2.Text == string.Empty)
            {
                MessageBox.Show("Debe ingresar el precio de venta 2.", "Error");
                isOK = false;

            }
            else if (txtPrecioVenta3.Text == string.Empty)
            {
                MessageBox.Show("Debe ingresar el precio de venta 3.", "Error");
                isOK = false;

            }


            return isOK;

        }

        private bool guardarProducto()
        {

            bool isOK = false;
            if (validarCampos())
            {
                

                tbProducto productoNuevo = new tbProducto();


                try
                {
                    //Instanciamos las entidades que ocupamos para almacenar un producto nuevo.

                    productoNuevo.idProducto = txtCodigoProducto.Text.Trim();

                    //Almacenamos el nuevo producto
                    productoNuevo.nombre = txtNombreProducto.Text.ToUpper().Trim();
                    productoNuevo.id_categoria = (int)cboCategoriaProducto.SelectedValue;
                    productoNuevo.idMedida = (int)cboMedida.SelectedValue;

                    //proveedor
                    if (txtIdProveedor.Text != string.Empty)
                    {

                        productoNuevo.idProveedor = proveedor.id;
                        proveedor.tipoId = proveedor.tipoId;

                    }
                    //costo
                    productoNuevo.precio_real = decimal.Parse(txtCosto.Text);
                    productoNuevo.precioVariable = chkPrecioVariable.Checked;
                    if (txtCategoriaCabys.Text != string.Empty)
                    {
                        productoNuevo.codigoCabys = txtCategoriaCabys.Text.Trim();
                    }

                    //precio utilidad+ impuestos**precio venta
                    productoNuevo.esExento = chkExento.Checked;
                    productoNuevo.idTipoImpuesto = (int)cboImpuesto.SelectedValue;                 

                    productoNuevo.precioVenta1 = decimal.Parse(txtPrecioVenta1.Text);
                    productoNuevo.precioVenta2 = decimal.Parse(txtPrecioVenta2.Text);
                    productoNuevo.precioVenta3 = decimal.Parse(txtPrecioVenta3.Text);


                    decimal valorImp = (decimal)listaImpuestos.Where(x => x.id == productoNuevo.idTipoImpuesto).SingleOrDefault().valor;
                    ProductoDTO prod = ProductosUtility.calcularPrecioUtilidad(productoNuevo.precio_real, decimal.Parse(txtPrecioVenta1.Text), decimal.Parse(txtPrecioVenta2.Text), decimal.Parse(txtPrecioVenta3.Text), valorImp);


                    //recalculo utilidad
                    productoNuevo.utilidad1Porc = prod.Utilidad1;
                    productoNuevo.utilidad2Porc = prod.Utilidad2;
                    productoNuevo.utilidad3Porc = prod.Utilidad3;
                    productoNuevo.precioUtilidad1 = prod.MontoUtilidad1;
                    productoNuevo.precioUtilidad2 = prod.MontoUtilidad2;
                    productoNuevo.precioUtilidad3 = prod.MontoUtilidad3;


                    productoNuevo.cocina = chkCocina.Checked;

                    //descuento
                    productoNuevo.aplicaDescuento = chkDescuento.Checked;
                    productoNuevo.descuento_max = chkDescuento.Checked ? int.Parse(txtDescuentoMax.Text) : 0;
     

                    productoNuevo.aplicaExo = chkAplicaExo.Checked;

                    //Atributos de Auditoria
                    productoNuevo.estado = true;
                    productoNuevo.fecha_crea = Utility.getDate();
                    productoNuevo.fecha_ult_mod = Utility.getDate();
                    productoNuevo.usuario_crea = Global.Usuario.nombreUsuario.Trim().ToUpper();   // Global.Usuario.nombreUsuario;
                    productoNuevo.usuario_ult_mod = Global.Usuario.nombreUsuario.Trim().ToUpper();   // Global.Usuario.nombreUsuario;


                    productoNuevo.codigoActividad = Global.actividadEconomic.CodActividad;   // Global.Usuario.nombreUsuario;


                    if (pictureBox1.Image!=null)
                    {
                        //guardo imagen
                        productoNuevo.fotoPro = Utility.ImageToByteArray(pictureBox1.Image);
                    }
           

                    //inventario

                    tbInventario inventario = new tbInventario();

                    inventario.idProducto = productoNuevo.idProducto;
                    inventario.cantidad = txtCantidadActual.Text == string.Empty ? 0 : decimal.Parse(txtCantidadActual.Text);
                    inventario.cant_max = txtCantMax.Text == string.Empty ? 0 : decimal.Parse(txtCantMax.Text);
                    inventario.cant_min = txtCantMin.Text == string.Empty ? 0 : decimal.Parse(txtCantMin.Text);

                    inventario.estado = true;
                    inventario.fecha_crea = Utility.getDate();
                    inventario.fecha_ult_mod = Utility.getDate();
                    inventario.usuario_crea = Global.Usuario.nombreUsuario.Trim().ToUpper();   // Global.Usuario.nombreUsuario;
                    inventario.usuario_ult_mod = Global.Usuario.nombreUsuario.Trim().ToUpper();   // Global.Usuario.nombreUsuario;

                    productoNuevo.tbInventario = inventario;




                    //productoNuevo.foto = destinoImage;

                    productoNuevo = productoIns.guardarProducto(productoNuevo);


                    MessageBox.Show("Los datos han sido almacenada en la base de datos.", "Guardar", MessageBoxButtons.OK, MessageBoxIcon.Information);


                    if ((bool)Global.Usuario.tbEmpresa.tbParametrosEmpresa.FirstOrDefault().etiquetas) {

                        imprimirEtiqueta(productoNuevo);

                    }



                    isOK = true;


                }
                catch (SaveEntityException ex)
                {

                    MessageBox.Show(ex.Message);
                    isOK = false;

                }
                catch (EntityExistException ex)
                {
                    MessageBox.Show(ex.Message,"Producto Existente", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    isOK = false;

                }
                catch (EntityDisableStateException ex)
                {
                    if (MessageBox.Show("Datos ya existe en la base datos, ¿Desea actualizarlos?", "Datos Existentes", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {

                        productoGlo = productoIns.GetEntity(productoNuevo);

                        actualizarProducto();
                        isOK = true;

                    }

                }


            }
            else
            {
                isOK = false;
            }
            return isOK;
        }

        private void imprimirEtiqueta(tbProducto productoNuevo)
        {
            try
            {
                DialogResult result = MessageBox.Show("Desea imprimir la etiquetas del producto?", "Crear Etiquetas", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    dsEtiquetas et = new dsEtiquetas();
                    var draw = new Code128BarcodeDraw(Code128Checksum.Instance);

                    var image = draw.Draw(productoNuevo.idProducto, 70, 1);
                    ProductosRow row = et.Productos.NewProductosRow();
                        
                    row.id = productoNuevo.idProducto;
                    row.nombre = productoNuevo.nombre.Trim().ToUpper();

                    row.precio = Utility.priceFormat(productoNuevo.precioVenta1);

                    //obtengo la medida
                    tbTipoMedidas medida = new tbTipoMedidas();
                    medida.idTipoMedida = productoNuevo.idMedida;
                    medida = medidaIns.GetEnityById(medida);

                    row.unidad = medida.nomenclatura.ToString();
                    row.Barcode = Utility.ImageToByteArray(image);
                    et.Productos.AddProductosRow(row);

                    rptEtiquetas2 reporte = new rptEtiquetas2();

                    reporte.SetDataSource(et);

                    reporte.PrintOptions.PrinterName = Global.Configuracion.rutaImpresoraExtra;
                    reporte.PrintToPrinter(1, false, 0, 0);

                }
            }
            catch (Exception)
            {

                throw;
            }

        }


        /// <summary>
        /// Actualizamos la informacion del producto en la base de datos.
        /// </summary>
        /// <returns></returns>
        private bool actualizarProducto()
        {
            bool isOK = false;
            try
            {
                if (validarCampos())
                {
                    //Almacenamos el nuevo producto
                    productoGlo.nombre = txtNombreProducto.Text.ToUpper().Trim();
                    productoGlo.id_categoria = (int)cboCategoriaProducto.SelectedValue;
                    productoGlo.idMedida = (int)cboMedida.SelectedValue;

                    //proveedor
                    if (txtIdProveedor.Text != string.Empty)
                    {

                        productoGlo.idProveedor = proveedor.id;
                        productoGlo.idTipoIdProveedor = proveedor.tipoId;

                    }
                    else
                    {

                        productoGlo.idProveedor = "";
                        productoGlo.idTipoIdProveedor = null;
                        productoGlo.tbProveedores = null;
                    }
                    //costo
                    productoGlo.precio_real = decimal.Parse(txtCosto.Text);
                    productoGlo.precioVariable = chkPrecioVariable.Checked;

                    if (txtCategoriaCabys.Text != string.Empty)
                    {
                        productoGlo.codigoCabys = txtCategoriaCabys.Text.Trim();
                    }

                     //impuestos
                    productoGlo.idTipoImpuesto = (int)cboImpuesto.SelectedValue;

                    decimal valorImp = (decimal)listaImpuestos.Where(x => x.id == productoGlo.idTipoImpuesto).SingleOrDefault().valor;
                    ProductoDTO prod = ProductosUtility.calcularPrecioUtilidad(productoGlo.precio_real, decimal.Parse(txtPrecioVenta1.Text), decimal.Parse(txtPrecioVenta2.Text), decimal.Parse(txtPrecioVenta3.Text), valorImp);

                  
                    productoGlo.esExento = chkExento.Checked;
                    productoGlo.precioVenta1 = decimal.Parse(txtPrecioVenta1.Text);
                    productoGlo.precioVenta2 = decimal.Parse(txtPrecioVenta2.Text);
                    productoGlo.precioVenta3 = decimal.Parse(txtPrecioVenta3.Text);


                    //recalculo utilidad
                    productoGlo.utilidad1Porc = prod.Utilidad1;
                    productoGlo.utilidad2Porc = prod.Utilidad2;
                    productoGlo.utilidad3Porc = prod.Utilidad3;

                    productoGlo.precioUtilidad1 = prod.MontoUtilidad1;
                    productoGlo.precioUtilidad2 = prod.MontoUtilidad2;
                    productoGlo.precioUtilidad3 = prod.MontoUtilidad3;


                    //descuento
                    productoGlo.aplicaDescuento = chkDescuento.Checked;
                    productoGlo.descuento_max = chkDescuento.Checked ? decimal.Parse(txtDescuentoMax.Text) : 0;
                
                    //cocina
                    productoGlo.cocina = chkCocina.Checked;            

                    productoGlo.estado = true;
                    productoGlo.fecha_ult_mod = Utility.getDate();
                    productoGlo.usuario_ult_mod = Global.Usuario.nombreUsuario;
                    //inventario
                    productoGlo.tbInventario.cantidad = decimal.Parse(txtCantidadActual.Text);
                    productoGlo.tbInventario.cant_max = decimal.Parse(txtCantMax.Text);
                    productoGlo.tbInventario.cant_min = decimal.Parse(txtCantMin.Text);

                    productoGlo.tbInventario.estado = true;
                    productoGlo.tbInventario.fecha_ult_mod = Utility.getDate();
                    productoGlo.tbInventario.usuario_ult_mod = Global.Usuario.nombreUsuario.Trim().ToUpper(); ;  // Global.Usuario.nombreUsuario;
                    //actividad economica pertenece
                    productoGlo.codigoActividad = Global.actividadEconomic.CodActividad;

                    productoGlo.aplicaExo = chkAplicaExo.Checked;
                   
                    if (pictureBox1.Image!=null)
                    {
                        //guardo imagen
                        productoGlo.fotoPro = Utility.ImageToByteArray(pictureBox1.Image);

                    }

                    productoGlo = productoIns.actualizarProducto(productoGlo);
                    MessageBox.Show("Los datos han sido actualizados en la base de datos.", "Actualización.", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    if ((bool)Global.Usuario.tbEmpresa.tbParametrosEmpresa.FirstOrDefault().etiquetas)
                    {

                        imprimirEtiqueta(productoGlo);

                    }

                    isOK = true;
                }


            }
            catch (Exception ex)
            {

                MessageBox.Show("Ha ocurrido un error. Comuniquese con el administrador " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                isOK = false;

            }

            return isOK;
        }

        private bool eliminarProducto()
        {

            bool isOK = false;

            try
            {
                productoGlo.estado = false;

                //Auditoria.
                productoGlo.usuario_ult_mod = Global.Usuario.nombreUsuario;
                productoGlo.fecha_ult_mod = Utility.getDate();

                productoGlo = productoIns.actualizarProducto(productoGlo);


                MessageBox.Show("Los datos han sido actualizados en la base de datos.", "Actualización.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                isOK = true;

            }

            catch (DeletedRowInaccessibleException ex)
            {
                MessageBox.Show("Se ha generado el siguiente error: " + ex.Message, "Producto desactivado.");
                isOK = false;
            }

            catch (Exception ex)
            {
                MessageBox.Show("Ha ocurrido un error. Comuniquese con el administrador " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return isOK;
        }
        private void recuperarEntidad(tbProducto entidad)
        {
            //Cargamos los valores a la entidad.
            productoGlo = entidad;


            if (productoGlo != null)
            {

                //Ingresamos los datos del producto en el formulario.
                txtCodigoProducto.Text = productoGlo.idProducto.Trim().ToString();
                txtNombreProducto.Text = productoGlo.nombre.ToUpper();

                cboMedida.SelectedValue = productoGlo.idMedida;
                cboCategoriaProducto.SelectedValue = productoGlo.id_categoria;
                cboImpuesto.SelectedValue = productoGlo.idTipoImpuesto;
                txtCategoriaCabys.Text = entidad.codigoCabys != null ? entidad.codigoCabys.Trim() : string.Empty;

                //proveedor
                proveedor = productoGlo.tbProveedores;
                if (proveedor != null)
                {
                    txtIdProveedor.Text = proveedor.id;

                }

                chkPrecioVariable.Checked = (bool)productoGlo.precioVariable;
                txtCosto.Text = productoGlo.precio_real.ToString();

                txtUtilidad1.Text = productoGlo.utilidad1Porc.ToString();
                txtUtilidad2.Text = productoGlo.utilidad2Porc.ToString();
                txtUtilidad3.Text = productoGlo.utilidad3Porc.ToString();
                txtPrecio1.Text = productoGlo.precioUtilidad1.ToString();
                txtPrecio2.Text = productoGlo.precioUtilidad2.ToString();
                txtPrecio3.Text = productoGlo.precioUtilidad3.ToString();

                //impuesto
                chkExento.Checked = productoGlo.esExento;
                cboImpuesto.SelectedValue = productoGlo.idTipoImpuesto;

                txtPrecioVenta1.Text = productoGlo.precioVenta1.ToString();
                txtPrecioVenta2.Text = productoGlo.precioVenta2.ToString();
                txtPrecioVenta3.Text = productoGlo.precioVenta3.ToString();
                //descuento
                chkDescuento.Checked = (bool)productoGlo.aplicaDescuento;
                txtDescuentoMax.Text = productoGlo.descuento_max.ToString();
           

                //inventario
                txtCantidadActual.Text = productoGlo.tbInventario.cantidad.ToString();
                txtCantMax.Text = productoGlo.tbInventario.cant_max.ToString();
                txtCantMin.Text = productoGlo.tbInventario.cant_min.ToString();

                if (productoGlo.fotoPro!=null)
                {
                    pictureBox1.Image = Utility.ByteArrayToImage(productoGlo.fotoPro);
                }
              
                //cocina
               chkCocina.Checked= (bool)productoGlo.cocina;

                chkAplicaExo.Checked = (bool)productoGlo.aplicaExo==null? true : (bool)productoGlo.aplicaExo;

            }

        }

        private bool buscarProducto()
        {

            bool isOK = false;

            frmBuscarProducto buscarProduct = new frmBuscarProducto();

            //Asignamos el metodo a la lista en el evento.
            buscarProduct.recuperarEntidad += recuperarEntidad;

            buscarProduct.ShowDialog();

            if (productoGlo.idProducto == string.Empty)
            {
                isOK = false;
            }
            else
            {
                isOK = true;
            }

            return isOK;

        }

        private void reset()
        {
            Utility.ResetForm(ref gboDatosProducto);
            Utility.ResetForm(ref gbxDesc);
            Utility.ResetForm(ref gbxImpuestos);
            Utility.ResetForm(ref gbxInv);
            Utility.ResetForm(ref gbxUtilidades);
            cboCategoriaProducto.SelectedIndex = 0;
            cboImpuesto.SelectedIndex = 0;
            cboMedida.SelectedIndex = 6;
            txtCantidadActual.Text = "0";
            txtCategoriaCabys.ResetText();
            txtCantMax.Text = "0";
            txtCantMin.Text = "0";
            txtDescuentoMax.Text = "0";
            chkDescuento.Checked = false;
            chkExento.Checked = false;
            chkPrecioVariable.Checked = false;
            chkAplicaExo.Checked = true;
            txtDescuentoMax.Text = "0";
            txtDescuentoMax.Enabled = false;
            chkCocina.Checked = false;

            chkAplicaExo.Enabled = !(bool)Global.Usuario.tbEmpresa.regimenSimplificado;
            chkExento.Checked = (bool)Global.Usuario.tbEmpresa.regimenSimplificado;
            chkAplicaExo.Checked = !(bool)Global.Usuario.tbEmpresa.regimenSimplificado;

            completarUtilidad();
        }
        private void accionMenu(string accion)
        {

            switch (accion)
            {
                case "Guardar":

                    if (accionGuardar(bandera))
                    {
                        MenuGenerico.CambioEstadoMenu(ref tlsMenu, (int)EnumMenu.OpcionMenu.Nuevo);
                        Utility.EnableDisableForm(ref gboDatosProducto, false);
                        reset();

                    }
                    break;

                case "Nuevo":
                    bandera = (int)Enums.accionGuardar.Nuevo;
                    MenuGenerico.CambioEstadoMenu(ref tlsMenu, (int)EnumMenu.OpcionMenu.Guardar);
                    Utility.EnableDisableForm(ref gboDatosProducto, true);
                    reset();
                    txtCosto.Enabled = true;
                    //txtCategoriaCabys.Enabled = false;
                    txtIdProveedor.Enabled = false;

                    break;

                case "Modificar":
                    bandera = (int)Enums.accionGuardar.Modificar;
                    MenuGenerico.CambioEstadoMenu(ref tlsMenu, (int)EnumMenu.OpcionMenu.Guardar);
                    Utility.EnableDisableForm(ref gboDatosProducto, true);
                    txtCodigoProducto.Enabled = false;
                    txtCategoriaCabys.Enabled = false;
                    txtIdProveedor.Enabled = false;
                    txtCosto.Enabled = Global.Usuario.idRol == (int)Enums.roles.Administracion;

                    txtPrecioVenta1.Enabled = true;
                    txtPrecioVenta2.Enabled = true;
                    txtPrecioVenta3.Enabled = true;
                    break;
                case "Eliminar":
                    bandera = (int)Enums.accionGuardar.Eliminar;
                    MenuGenerico.CambioEstadoMenu(ref tlsMenu, (int)EnumMenu.OpcionMenu.Guardar);
                    break;

                case "Buscar":


                    if (buscarProducto())
                    {

                        MenuGenerico.CambioEstadoMenu(ref tlsMenu, (int)EnumMenu.OpcionMenu.Modificar);
                        Utility.EnableDisableForm(ref gboDatosProducto, false);

                    }
                    break;

                case "Cancelar":
                    MenuGenerico.CambioEstadoMenu(ref tlsMenu, (int)EnumMenu.OpcionMenu.Nuevo);
                    Utility.EnableDisableForm(ref gboDatosProducto, false);
                    reset();


                    break;

                case "Salir":
                    this.Dispose();
                    break;
            }

        }

        private void txtPrecioVenta1_TextChanged(object sender, EventArgs e)
        {
            if (((TextBox)sender).Name == "txtPrecioVenta1")
            {



            }
            else
            {

            }
        }

        private void calcularMontos()
        {

            decimal precio1 = 0, precio2 = 0, precio3 = 0, impuesto = 0, costo = 0;

            if (txtCosto.Text == string.Empty)
            {
                txtCosto.Text = "0";

            }
            costo = decimal.Parse(txtCosto.Text);
            if (txtUtilidad1.Text == string.Empty)
            {
                txtUtilidad1.Text = "0";
            }
            if (txtUtilidad2.Text == string.Empty)
            {
                txtUtilidad2.Text = "0";
            }
            if (txtUtilidad3.Text == string.Empty)
            {
                txtUtilidad3.Text = "0";
            }

            if (cboImpuesto.SelectedIndex != -1)
            {
                int idImp = int.Parse(cboImpuesto.SelectedValue.ToString());

                impuesto = (decimal)listaImpuestos.Where(x => x.id == idImp).SingleOrDefault().valor;


            }

            ProductoDTO pro = ProductosUtility.calcularPrecio(decimal.Parse(txtCosto.Text), decimal.Parse(txtUtilidad1.Text), decimal.Parse(txtUtilidad2.Text),
                decimal.Parse(txtUtilidad3.Text), impuesto);
            if (pro != null)
            {
                txtPrecio1.Text = pro.MontoUtilidad1.ToString();
                txtPrecio2.Text = pro.MontoUtilidad2.ToString();
                txtPrecio3.Text = pro.MontoUtilidad3.ToString();


                txtPrecioVenta1.Text = pro.Precio1.ToString();
                txtPrecioVenta2.Text = pro.Precio2.ToString();
                txtPrecioVenta3.Text = pro.Precio3.ToString();



            }
    

        }

        private void btnCalcular_Click(object sender, EventArgs e)
        {
            isBotonCalcular = true;
            try
            {

                
                if (!Utility.isNumeroDecimal(txtCosto.Text.Trim()))
                {
                    MessageBox.Show("Debe indicar un precio de costo del producto.", "Calcular precio", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtCosto.Text = "0";
                    return;

                }

                decimal precioCosto = decimal.Parse(txtCosto.Text.Trim());
                decimal impuesto = (decimal)listaImpuestos.Where(x => x.id == (int)cboImpuesto.SelectedValue).SingleOrDefault().valor;



               ProductoDTO pro = ProductosUtility.calcularPrecioUtilidad(precioCosto, decimal.Parse(txtPrecioVenta1.Text), decimal.Parse(txtPrecioVenta2.Text),
               decimal.Parse(txtPrecioVenta3.Text), impuesto);

                txtPrecio1.Text = pro.MontoUtilidad1.ToString();
                txtPrecio2.Text = pro.MontoUtilidad2.ToString();
                txtPrecio3.Text = pro.MontoUtilidad3.ToString();

                txtUtilidad1.Text = pro.Utilidad1.ToString();
                txtUtilidad2.Text = pro.Utilidad2.ToString();
                txtUtilidad3.Text = pro.Utilidad3.ToString();

                txtPrecioVenta1.Text = pro.Precio1.ToString();
                txtPrecioVenta2.Text = pro.Precio2.ToString();
                txtPrecioVenta3.Text = pro.Precio3.ToString();

            }
            catch (Exception)
            {

                MessageBox.Show("Error al calcular el impuesto", "Calcular Impuesto", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            frmBuscarCategoriaCabys categorias = new frmBuscarCategoriaCabys();
            categorias.pasarDatosEvent += dataBuscarCategoriaCabys;
            categorias.ShowDialog();
        }

        private void dataBuscarCategoriaCabys(string codigo)
        {
            if (codigo != null)
            {
               // categoriaCABYS = codigo;
                this.txtCategoriaCabys.Text = codigo;

            }

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            // image filters  
            open.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp";
            if (open.ShowDialog() == DialogResult.OK)
            {
                // display image in picture box  
                pictureBox1.Image = new Bitmap(open.FileName);
                // image file path  
               //extBox1.Text = open.FileName;
            }
        }
    }
}
