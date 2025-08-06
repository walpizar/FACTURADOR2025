using BusinessLayer;
using CommonLayer;
using EntityLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Imaging;




namespace PresentationLayer
{
    public partial class frmFacturacionFotos : Form
    {
    

        List<tbCategoriaProducto> listaCategorias;
        BCategoriaProducto categoriaIns = new BCategoriaProducto();
        bTipoMedidas medidaIns = new bTipoMedidas();
        tbTipoMedidas medida = new tbTipoMedidas();
        List<tbImpuestos> listaImp;
        BImpuestos impIns = new BImpuestos();
        BPromociones promoIns = new BPromociones();

        List<tbProducto> listaProductos = new List<tbProducto>();
        const int sizeCuadro = 75;
        const float sizeText = 6;

        public frmFacturacionFotos()
        {
            InitializeComponent();
        }

        private void frmFacturacionFotos_Load(object sender, EventArgs e)
        {
            cargarDatos();
        }
        private void cargarDatos()
        {
            try
            {
                pnlCategorias.Controls.Clear();
                pnlProductos.Controls.Clear();
                listaCategorias = categoriaIns.getCategorias(1);              
                cargaCategorias(listaCategorias);
                cargarProductos(listaCategorias);
            }
            catch (Exception)
            {

                MessageBox.Show("Error al cargar datos.", "Cargar datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void cargarProductos(List<tbCategoriaProducto> listaCategorias)
        {
            if (listaProductos != null)
            {

                listaProductos.Clear();
            }



            foreach (tbCategoriaProducto categoria in listaCategorias)
            {
                List<tbProducto> prod = categoria.tbProducto.Where(x => x.estado == true).ToList();
                foreach (tbProducto pro in prod)
                {
                    if (pro.idProducto != "SM")
                    {
                        listaProductos.Add(pro);
                    }


                }
            }

        }

        private void cargaCategorias(List<tbCategoriaProducto> listaCategorias)
        {
            Button btn;
            int y = 0;
            int x = 0;

            foreach (tbCategoriaProducto categoria in listaCategorias)
            {
                if (categoria.tbProducto.Count != 0)
                {
                    btn = new Button();
                    btn.Name = "cat" + categoria.id.ToString();
                    btn.Text = categoria.nombre.Trim();
                    btn.Location = new System.Drawing.Point(sizeCuadro * x, sizeCuadro * y);
                    btn.Size = new System.Drawing.Size(sizeCuadro, sizeCuadro);
                    btn.BackColor = Color.DarkBlue;
                    btn.Font = new Font("Microsoft Sans Serif", sizeText, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    btn.ForeColor = Color.White;
                    btn.TextAlign = ContentAlignment.MiddleCenter;
                    if (File.Exists(categoria.fotocategoria))
                    {

                        Image imagen = new Bitmap(categoria.fotocategoria.Trim());
                        Image final = new Bitmap(imagen, 90, 70);

                        btn.Image = final;
                        btn.ImageAlign = ContentAlignment.TopCenter;

                    }
                    btn.Click += new System.EventHandler(cargarProductosPorCategoria);

                    pnlCategorias.Controls.Add(btn);
                    x++;

                    if (x == 5)
                    {
                        x = 0;
                        y++;

                    }

                }


            }

        }

        private void cargarProductosPorCategoria(object sender, EventArgs e)
        {
            try
            {


                string categoria = ((Button)sender).Name.Replace("cat", "");


                pnlProductos.Controls.Clear();
                int idCategoria = int.Parse(categoria);
                //  listaProductos = productoIns.getListProductoByCategoria(idCategoria);


                pintarProductos(idCategoria);

                



            }
            catch (Exception)
            {

                MessageBox.Show("Error al agregar el producto a facturación.", "Agregar productos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void pintarProductos(int idCategoria)
        {
            pnlProductos.Controls.Clear();
            if (idCategoria != 0)
            {
                listaProductos = listaProductos.Where(p=> p.id_categoria == idCategoria).ToList();
            }

            Button btn;
            int y = 0;
            int x = 0;

            foreach (tbProducto pro in listaProductos)
            {
             

                btn = new Button();
                btn.Name = "pro" + pro.idProducto.ToString();
                btn.Text = pro.nombre.Trim();
                btn.Location = new System.Drawing.Point(sizeCuadro * x, sizeCuadro * y);
                btn.Size = new System.Drawing.Size(sizeCuadro, sizeCuadro);
                // btn.BackColor = Color.SeaGreen;
                btn.Font = new System.Drawing.Font("Microsoft Sans Serif", sizeText, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                btn.ForeColor = Color.Black;
                btn.BackColor = Color.White;
                btn.TextAlign = ContentAlignment.BottomCenter;

                if (pro.fotoPro != null)
                {

                    Image img = Utility.ByteArrayToImage(pro.fotoPro);
                    Size size = new Size();
                    size.Width = sizeCuadro;
                    size.Height = sizeCuadro - 20;
                    img = new Bitmap(img, size);
                    btn.Image = img;
                    btn.ImageAlign = ContentAlignment.TopCenter;

                }

                btn.Click += new System.EventHandler(agregarProductoFacturacion);
                pnlProductos.Controls.Add(btn);
                x++;

                if (x == 10)
                {
                    x = 0;
                    y++;
                    }
                
            }
        }

        private void agregarProductoFacturacion(object sender, EventArgs e)
        {
            try
            {


                //en caso que fuera un producto lo mando agregar al detalle de la factura
                string producto = ((Button)sender).Name.Replace("pro", "");
                foreach (tbProducto pro in listaProductos)
                {

                    if (pro.idProducto == producto)
                    {

                       // agregarProductoDetalleFactura(pro);

                        break;

                    }
                }

            }
            catch (Exception)
            {

                MessageBox.Show("Error al agregar el producto a facturación.", "Agregar productos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void txtCodigo_TextChanged(object sender, EventArgs e)
        {
            if (txtCodigo.Text != string.Empty)
            {
                buscarProducto(txtCodigo.Text);
            }
            else
            {
                cargarDatos();
            }
  
        }

        private void buscarProducto(string text)
        {
         
            listaProductos.Clear();
            foreach (var cat in listaCategorias)
            {
                foreach (var pro in cat.tbProducto)
                {

                    if (pro.nombre.ToUpper().Contains(text.Trim().ToUpper()) || pro.idProducto.ToUpper().Contains(text.Trim().ToUpper()))
                    {

                        listaProductos.Add(pro);
                    }
                }

            }

            //cargarlista
            pintarProductos(0);
        }
    }
}
