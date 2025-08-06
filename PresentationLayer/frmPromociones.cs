using BusinessLayer;
using CommonLayer;
using EntityLayer;
using PresentationLayer.Reportes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Zen.Barcode;
using static PresentationLayer.Reportes.dsEtiquetas;

namespace PresentationLayer
{
    public partial class frmPromociones : Form
    {
        bTipoMedidas medidaIns = new bTipoMedidas();
        public tbPromociones promocion { get; set; }
        bool isCrear = true;
        BImpuestos ImpIns = new BImpuestos();
        IEnumerable<tbImpuestos> listaImpuestos;
        BPromociones promoIns = new BPromociones();
        BProducto proIns = new BProducto();
        private tbProducto productoSelect;
        public frmPromociones()
        {
            InitializeComponent();
        }

        private void frmPromociones_Load(object sender, EventArgs e)
        {
            listaImpuestos = ImpIns.getImpuestos(1);
            lblTitulo.Text = promocion == null ? "Crear Promoción" : "Modificar Promoción";
            btnAccion.Text = promocion == null ? "Crear" : "Modificar";
            isCrear = promocion == null ? true : false;
            if (!isCrear) {

                cargarPromo(promocion);
                btnBuscar.Enabled = false;
            }
            else
            {
                btnBuscar.Enabled = true;
                limpiar();
            }
            
        }

        private void limpiar()
        {
            DateTime date1 = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 06, 00, 00);
            dtpInicio.Value = date1;
            date1 = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 59, 00);
            dtpFin.Value = date1;
        }

        private void cargarPromo(tbPromociones promocion)
        {
            cargarDatosProducto(promocion.tbProducto);
            txtDescuento.Text = promocion.descuento.ToString() ;
            dtpInicio.Value = promocion.fechaIncio;
            dtpFin.Value = promocion.fechaCierre;
            calcularPromo();


        }

        private void btnBuscar_Click(object sender, EventArgs e)
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
        private void buscarProducto()
        {

           

            frmBuscarProducto buscarProduct = new frmBuscarProducto();

            //Asignamos el metodo a la lista en el evento.
            buscarProduct.recuperarEntidad += recuperarEntidad;
            buscarProduct.tipoBusqueda = 0;
            buscarProduct.ShowDialog();

           
        }

        private void recuperarEntidad(tbProducto entidad)
        {
           if(entidad != null)
            {
                cargarDatosProducto(entidad);
            }
        }

        private void cargarDatosProducto(tbProducto pro)
        {
            productoSelect = pro;
            txtCodigoProducto.Text = pro.idProducto;
            txtNombreProducto.Text = pro.nombre.ToUpper().Trim();

            txtDescuento.Text = "0";

            txtPrecioReal.Text = Utility.priceFormat(pro.precio_real);

            txtImp.Text = listaImpuestos.Where(x => x.id == pro.idTipoImpuesto).SingleOrDefault().valor.ToString();

            txtUtilidad1.Text = pro.utilidad1Porc.ToString();
            txtUtilidad2.Text = pro.utilidad2Porc.ToString();
            txtUtilidad3.Text = pro.utilidad3Porc.ToString();

            txtPrecioVenta1.Text = Utility.priceFormat(pro.precioVenta1);
            txtPrecioVenta2.Text = Utility.priceFormat(pro.precioVenta2);
            txtPrecioVenta3.Text = Utility.priceFormat(pro.precioVenta3);

            calcularPromo();
        }

       
        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAccion_Click(object sender, EventArgs e)
        {
            try
            {
                if (isCrear)
                {
                    if (validar())
                    {
                        calcularprecioPromoPrecioVenta();
                        tbPromociones pro = crearPromocion();
                        pro = promoIns.Guardar(pro);


                        MessageBox.Show("Los datos han sido almacenada en la base de datos.", "Guardar", MessageBoxButtons.OK, MessageBoxIcon.Information);
                  
                        if ((bool)Global.Usuario.tbEmpresa.tbParametrosEmpresa.FirstOrDefault().etiquetas)
                        {

                            imprimirEtiqueta(productoSelect, decimal.Parse(txtPrecioVenta1Promo.Text));

                        }

                        this.Close();
                    }


                }
                //modificar
                else
                {
                    if (validar())
                    {
                        calcularprecioPromoPrecioVenta();

                        promocion.descuento = decimal.Parse(txtDescuento.Text);
                        promocion.fechaIncio = dtpInicio.Value;
                        promocion.fechaCierre = dtpFin.Value;

                        promocion.usuario_ult_mod = Global.Usuario.nombreUsuario;
                        promocion.fecha_ult_mod = Utility.getDate();
                        promocion = promoIns.Actualizar(promocion);


                        MessageBox.Show("Los datos han sido modificados en la base de datos.", "Modificar", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        if ((bool)Global.Usuario.tbEmpresa.tbParametrosEmpresa.FirstOrDefault().etiquetas)
                        {

                            imprimirEtiqueta(productoSelect, decimal.Parse(txtPrecioVenta1Promo.Text));

                        }

                        this.Close();

                    }
                      
                }
            }
            catch (Exception)
            {

                MessageBox.Show("Error al guardar o modificar la promoción","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
           
        }

        private bool validar()
        {
            decimal utilidadMax = obtenerUtilidadMax(); 
           

            if (txtCodigoProducto.Text == string.Empty)
            {
                MessageBox.Show("Debe indicar el producto", "Error");
                txtCodigoProducto.Focus();
                return false;

            }
            else if (decimal.Parse(txtDescuento.Text) <= 0)
            {

                MessageBox.Show("El descuento no es valido", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtDescuento.Focus();
                return false;
            }
            //else if (txtDescuento.Value > utilidadMax)
            //{

            //    MessageBox.Show("El descuento no puede ser mayor a la utilidad MAYOR del Producto", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    txtDescuento.Focus();
            //    return false;
            //}
            else if (dtpFin.Value<dtpInicio.Value)
            {

                MessageBox.Show("La fecha fin indicado no puede ser menor que la fecha de inicio", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtDescuento.Focus();
                return false;
            }

            if (isCrear)
            {

                //validacion de no poder agregar una promocion entre fechas de otra
                var listaPromo = promoIns.getPromosByIdProdFechas(txtCodigoProducto.Text.Trim().ToUpper(), dtpInicio.Value);
                if (listaPromo.Count != 0)
                {
                    MessageBox.Show("No puede crear la promoción ya que existe una promoción entre las fechas indicadas.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    dtpInicio.Focus();
                    return false;

                }

                listaPromo = promoIns.getPromosByIdProdFechas(txtCodigoProducto.Text.Trim().ToUpper(), dtpFin.Value);
                if (listaPromo.Count != 0)
                {
                    MessageBox.Show("No puede crear la promoción ya que existe una promoción activa entre las fechas indicadas.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    dtpFin.Focus();
                    return false;

                }
            }
            else //cuando es modificar que no tome las fechas de la prmocion a modificar
            {
                //validacion de no poder agregar una promocion entre fechas de otra
                var listaPromo = promoIns.getPromosByIdProdFechas(promocion.id, txtCodigoProducto.Text.Trim().ToUpper(), dtpInicio.Value);


                if (listaPromo.Count != 0)
                {
                    DateTime fecha =DateTime.MinValue;

                    var listAbajo = listaPromo.Where(x => x.fechaCierre <= promocion.fechaIncio).ToList();
                    if (listAbajo.Count!=0)
                    {
                        fecha = listAbajo.Max(x => x.fechaCierre);
                        if (dtpInicio.Value <= fecha)
                        {
                            MessageBox.Show("No puede crear la promoción ya que existe una promoción entre las fechas indicadas.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            dtpInicio.Focus();
                            return false;

                        }
                    }

                    var listArriba = listaPromo.Where(x => x.fechaIncio >= promocion.fechaCierre).ToList();
                    if (listArriba.Count!=0)
                    {
                        fecha = listArriba.Min(x => x.fechaIncio);
                        if (dtpFin.Value >= fecha)
                        {
                            MessageBox.Show("No puede crear la promoción ya que existe una promoción entre las fechas indicadas.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            dtpInicio.Focus();
                            return false;

                        }
                    }
                }
            }
                

            return true;
        }

        private decimal obtenerUtilidadMax()
        {
            if (decimal.Parse(txtUtilidad1.Text) > decimal.Parse(txtUtilidad2.Text) &&
               decimal.Parse(txtUtilidad1.Text) > decimal.Parse(txtUtilidad3.Text))
            {
                return decimal.Parse(txtUtilidad1.Text);

            }
            else if (decimal.Parse(txtUtilidad2.Text) > decimal.Parse(txtUtilidad3.Text))
            {
                return decimal.Parse(txtUtilidad2.Text);
            }
            else
            {
                return decimal.Parse(txtUtilidad3.Text);
            }
        }

        private tbPromociones crearPromocion()
        {
            tbPromociones promo = new tbPromociones();
            promo.idProducto = txtCodigoProducto.Text.Trim();
            

            promo.descuento = decimal.Parse(txtDescuento.Text);
            promo.fechaIncio = dtpInicio.Value;
            promo.fechaCierre = dtpFin.Value;
            promo.estado = true;
            promo.fecha_crea = Utility.getDate();
            promo.fecha_ult_mod = Utility.getDate();
            promo.usuario_ult_mod = Global.Usuario.nombreUsuario;
            promo.usuario_crea = Global.Usuario.nombreUsuario;

            return promo;

        }

        private void btnsalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtDescuento_ValueChanged(object sender, EventArgs e)
        {
            calcularPromo();
        }

        private void txtPrecioVenta1Promo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((int)e.KeyChar == (int)Keys.Enter)
            {
                if (Utility.isNumeroDecimal(txtPrecioVenta1Promo.Text))
                {
                    if (productoSelect.precioVenta1 < decimal.Parse(txtPrecioVenta1Promo.Text))
                    {
                        txtDescuento.Text = "0";
                        txtPrecioVenta1Promo.Text = Utility.priceFormat(productoSelect.precioVenta1);
                        MessageBox.Show("El precio promoción no puede ser mayor al precio de venta normal del producto", "Promoción", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    }
                    else
                    {
                        calcularprecioPromoPrecioVenta();

                    }
                }
                else
                {
                    txtDescuento.Text = "0";
                    txtPrecioVenta1Promo.Text = Utility.priceFormat(productoSelect.precioVenta1);
                    MessageBox.Show("El precio de venta de promoción no tiene formato correcto.", "Promoción", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }


            }
        }

        private void calcularprecioPromoPrecioVenta()
        {
       
        
            decimal desc = ((productoSelect.precioVenta1 - decimal.Parse(txtPrecioVenta1Promo.Text)) / productoSelect.precioVenta1)*100;
            txtDescuento.Text = desc.ToString();
            txtUtilidad1Promo.Text = (productoSelect.utilidad1Porc - desc).ToString();


        }
        private void calcularPromo()
        {
            if (txtCodigoProducto.Text != string.Empty)
            {

               
                decimal precio = 0;
                if (decimal.Parse(txtDescuento.Text) != 0)
                {


                    ProductoDTO pro = ProductosUtility.calcularPrecio(productoSelect.precio_real, decimal.Parse(txtDescuento.Text), (decimal)productoSelect.utilidad1Porc, (decimal)listaImpuestos.Where(x => x.id == productoSelect.idTipoImpuesto).SingleOrDefault().valor);

                    precio = pro.Precio1;


                }
                else
                {
                    txtUtilidad1Promo.Text = productoSelect.utilidad1Porc.ToString();
                    precio = productoSelect.precioVenta1;

                }




                // txtUtilidad1Promo.Text = Utility.priceFormat(pro.Utilidad1);
                //txtUtilidad2Promo.Text = Utility.priceFormat(pro.Utilidad2);
                //txtUtilidad3Promo.Text = Utility.priceFormat(pro.Utilidad3);

                txtPrecioVenta1Promo.Text = Utility.priceFormat(precio);
                //txtPrecioVenta2Promo.Text = Utility.priceFormat(pro.Precio2);
                //txtPrecioVenta3Promo.Text = Utility.priceFormat(pro.Precio3);

            }
        }


        private void txtDescuento_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((int)e.KeyChar == (int)Keys.Enter)
            {
                if (Utility.isNumeroDecimal(txtDescuento.Text))
               {
                    calcularPromo();
                }
                else
                {
                    txtDescuento.Text = "0";
                    txtPrecioVenta1Promo.Text = Utility.priceFormat(productoSelect.precioVenta1);
                    MessageBox.Show("El descuento de promoción no tiene formato correcto.", "Promoción", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }


                
            }
        }

        private void txtDescuento_TextChanged(object sender, EventArgs e)
        {

        }

        private void imprimirEtiqueta(tbProducto productoNuevo, decimal precioPromo)
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

                    row.precio = Utility.priceFormat(precioPromo);

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

    }
}
