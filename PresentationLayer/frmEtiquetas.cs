using BusinessLayer;
using CommonLayer;
using CrystalDecisions.ReportAppServer.ReportDefModel;
using EntityLayer;
using PresentationLayer.Reportes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Zen.Barcode;
using static PresentationLayer.Reportes.dsEtiquetas;

namespace PresentationLayer
{

 

    public partial class frmEtiquetas : Form
    {
        BPromociones promoIns = new BPromociones();
        BProducto bPro = new BProducto();
        BProducto bProducto = new BProducto();
        bTipoMedidas bMedida = new bTipoMedidas();
        BImpuestos ImpIns = new BImpuestos();
        IEnumerable<tbImpuestos> listaImpuestos;

        public List<tbProducto> listaProductos { get; set; }
        public List<tbPromociones> listaPromos { get; set; }
        public List<EtiquetaDto> listaEtiquetas { get; set; }

        public frmEtiquetas()
        {
            InitializeComponent();
            listaProductos = new List<tbProducto>();
        }

        private void frmEtiquetas_Load(object sender, EventArgs e)
        {
            listaImpuestos = ImpIns.getImpuestos(1);
            obtnerProductos();
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

            //bool isOK = false;

            frmBuscarProducto buscarProduct = new frmBuscarProducto();

     
            buscarProduct.recuperarEntidad += recuperarEntidad;
            buscarProduct.tipoBusqueda = 0;
            buscarProduct.ShowDialog();

        }

        private void recuperarEntidad(tbProducto entidad)
        {
            //Cargamos los valores a la entidad.
            if (entidad != null)
            {
                listaProductos.Add(entidad);  

                listaEtiquetas = crearlistaEtiquetas();

                cargarLista(listaEtiquetas);
            }
        }

       
        private tbProducto buscarProducto(string idProd)
        {
            tbProducto producto = new tbProducto();
            if (idProd != string.Empty)
            {
                producto.idProducto = idProd;
               
                producto = bProducto.GetEntity(producto, (int)Enums.EstadoBusqueda.Activo);

                if (producto == null)
                {

                    producto = null;
                    MessageBox.Show("El producto digitado no se encuentra en la base datos.", "Producto Inexistente", MessageBoxButtons.OK, MessageBoxIcon.Stop);


                }
            }
            else
            {
                producto = null;


            }
            return producto;

        }

        private void obtnerProductos()
        {
           

            listaProductos = bProducto.getProductos( (int)Enums.EstadoBusqueda.Activo);

            listaProductos = listaProductos.Where(x => x.fecha_ult_mod.Date >= dtpInicio.Value.Date && x.fecha_ult_mod.Date <= dtpFin.Value.Date ).ToList();


            listaPromos = promoIns.GetListEntities((int)Enums.EstadoBusqueda.Activo);

            listaPromos = listaPromos.Where(x => x.fecha_ult_mod.Date >= dtpInicio.Value.Date && x.fecha_ult_mod.Date <= dtpFin.Value.Date).ToList();


            listaEtiquetas = crearlistaEtiquetas();

            cargarLista(listaEtiquetas);
        }

        private List<EtiquetaDto> crearlistaEtiquetas()
        {
            listaEtiquetas = new List<EtiquetaDto>();
            foreach (var pro in listaProductos)
            {
                EtiquetaDto etiqueta = new EtiquetaDto();
                etiqueta.id = pro.idProducto.Trim();
                etiqueta.nombre = pro.nombre.ToUpper().Trim();
                etiqueta.precio = pro.precioVenta1;
                etiqueta.promo = false;

                tbTipoMedidas medida = new tbTipoMedidas();
                medida.idTipoMedida = pro.idMedida;

                etiqueta.nomenclatura = bMedida.GetEnityById(medida).nomenclatura;

                listaEtiquetas.Add(etiqueta);
            }

            foreach (var promo in listaPromos)
            {
              
                var prod = ProductosUtility.calcularPrecio(promo.tbProducto.precio_real, promo.descuento, (decimal)promo.tbProducto.utilidad1Porc, (decimal)listaImpuestos.SingleOrDefault(x => x.id == promo.tbProducto.idTipoImpuesto).valor);
                EtiquetaDto etiqueta = new EtiquetaDto();
                etiqueta.id = promo.idProducto.Trim();
                etiqueta.nombre = promo.tbProducto.nombre.ToUpper().Trim();
                etiqueta.precio = prod.Precio1;
                etiqueta.promo = true;

                tbTipoMedidas medida = new tbTipoMedidas();
                medida.idTipoMedida = promo.tbProducto.idMedida;

                etiqueta.nomenclatura = bMedida.GetEnityById(medida).nomenclatura;

                listaEtiquetas.Add(etiqueta);
            }

            return listaEtiquetas;
        }

        private void btnLimpiarForm_Click(object sender, EventArgs e)
        {
            listaProductos.Clear();
            listaPromos.Clear();

            obtnerProductos();
        }

        private void dtpInicio_ValueChanged(object sender, EventArgs e)
        {
            obtnerProductos();
        }

        private void btnsalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cargarLista(List<EtiquetaDto> listaEtiquetas)
        {
            lstvProductos.Items.Clear();
            foreach (var item in listaEtiquetas)
            {

                //Creamos un objeto de tipo ListviewItem
                ListViewItem linea = new ListViewItem();
              
                linea.Text = item.id.ToString();
                linea.SubItems.Add(item.nombre.ToUpper());
                linea.SubItems.Add(Utility.priceFormat(item.precio));
                linea.SubItems.Add(item.promo==true?"Sí":"No");

                //Agregamos el item a la lista.
                lstvProductos.Items.Add(linea);

            }
        }

        private void txtCodigo_KeyPress(object sender, KeyPressEventArgs e)
        {
            tbProducto prod = null;
            if ((int)e.KeyChar == (int)Keys.Enter)
            {
                string codigo = txtCodigo.Text;
                if (codigo != string.Empty)
                {
                    prod = buscarProducto(codigo);
                    if (prod != null)
                    {
                        listaProductos.Add(prod);
                    }

                    var lista = crearlistaEtiquetas();
                    cargarLista(lista);

                }
                txtCodigo.Text = string.Empty;
                txtCodigo.Select();
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in lstvProductos.Items)
            {
                if (item.Checked)
                {

                    string id = item.SubItems[0].Text.Trim();
                    bool promo = item.SubItems[3].Text.Trim() == "Sí" ? true : false;

                    if (promo)
                    {
                        tbPromociones promocio = listaPromos.Where(x => x.idProducto.Trim() == item.SubItems[0].Text.Trim()).FirstOrDefault();
                        if (promocio != null)
                        {
                            listaPromos.Remove(promocio);
                        }
                    }
                    else
                    {
                        tbProducto pro = listaProductos.Where(x => x.idProducto.Trim() == item.SubItems[0].Text.Trim()).FirstOrDefault();
                        if (pro != null)
                        {
                            listaProductos.Remove(pro);
                        }
                    }

                }
              

            }
            var lista = crearlistaEtiquetas();
            cargarLista(lista);
         
        }

        private void dtpFin_ValueChanged(object sender, EventArgs e)
        {
            obtnerProductos();
        }

        private void btnReImprimir_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult result = MessageBox.Show("Desea imprimir la etiquetas de la lista?", "Crear Etiquetas", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    //frmReporteEtiquetas reporte = new frmReporteEtiquetas();
                    //reporte.listaProductos = this.listaProductos;
                    //reporte.Show();
                    bool bandera = false;

                    var listaEtiquetas = crearlistaEtiquetas();
             

                    dsEtiquetas et = new dsEtiquetas();
                    var draw = new Code128BarcodeDraw(Code128Checksum.Instance);

                    foreach (var etiqueta in listaEtiquetas)
                    {
                        bandera = true;
                        var image = draw.Draw(etiqueta.id, 70, 1);
                        ProductosRow row = et.Productos.NewProductosRow();
                        var producto = bPro.GetEntityById(etiqueta.id);
                        row.id = producto.idProducto;


                        row.nombre = etiqueta.nombre.Trim().ToUpper();

                        row.precio = Utility.priceFormat(etiqueta.precio);
                       
                     
                        row.unidad = etiqueta.nomenclatura;

                        row.Barcode = Utility.ImageToByteArray(image);
                        et.Productos.AddProductosRow(row);

                    }

                    if (bandera)
                    {
                      
                        rptEtiquetas2 reporte = new rptEtiquetas2();

                        reporte.SetDataSource(et);

                        reporte.PrintOptions.PrinterName = Global.Configuracion.rutaImpresoraExtra;
                        reporte.PrintToPrinter((int)copies.Value, false, 0, 0);

                        obtnerProductos();

                    }

                }
            }
            catch (Exception)
            {

                MessageBox.Show("Error al imprimir, cierre la ventana y vuelva a cargar los productos","Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
         

        }

        private void chkTodos_CheckedChanged(object sender, EventArgs e)
        {
            foreach (ListViewItem item in lstvProductos.Items)
            {
                item.Checked = chkTodos.Checked;
            }
        }

        //private void PrintReport(rptEtiquetas reportDocument, string printerName)
        //{
        //    PageMargins margins = new PageMargins();
        //    margins.Left = 1;
        //    margins.Right = 1;
        //    margins.Bottom = 1;
        //    margins.Top = 1;
        //    reportDocument.PrintOptions.ApplyPageMargins(;

        //    reportDocument.PrintOptions.PrinterName = printerName;

        //    reportDocument.PrintToPrinter(1, false, 0, 0);
        //}
    }


}
