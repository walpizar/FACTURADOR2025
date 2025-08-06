using BusinessLayer;
using CommonLayer;
using EntityLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PresentationLayer
{
    public partial class frmBuscarPromociones : Form
    {
        int bandera = 1;
        tbProducto productoGlo = new tbProducto();
        BPromociones promoIns = new BPromociones();
        BImpuestos ImpIns = new BImpuestos();
        IEnumerable<tbImpuestos> listaImpuestos;
        IEnumerable<tbPromociones> listaPromos;

        public frmBuscarPromociones()
        {
            InitializeComponent();
        }

        
       

        private bool eliminarProducto()
        {
            throw new NotImplementedException();
        }

        private bool actualizarProducto()
        {
            throw new NotImplementedException();
        }

        private bool guardarProducto()
        {
            throw new NotImplementedException();
        }

        private void reset()
        {
            //Utility.ResetForm(ref gboDatos);
            //Utility.ResetForm(ref gbxDesc);
            //Utility.ResetForm(ref gbxImpuestos);
            //Utility.ResetForm(ref gbxInv);
            //Utility.ResetForm(ref gbxUtilidades);
            //cboCategoriaProducto.SelectedIndex = 0;
            //cboImpuesto.SelectedIndex = 0;
            //cboMedida.SelectedIndex = 6;
            //txtCantidadActual.Text = "0";
            //txtCategoriaCabys.ResetText();
            //txtCantMax.Text = "0";
            //txtCantMin.Text = "0";
            //txtDescuentoMax.Text = "0";
            //chkDescuento.Checked = false;
            //chkExento.Checked = false;
            //chkPrecioVariable.Checked = false;
            //chkAplicaExo.Checked = true;
            //txtDescuentoMax.Text = "0";
            //txtDescuentoMax.Enabled = false;
            //chkCocina.Checked = false;

            //chkAplicaExo.Enabled = !(bool)Global.Usuario.tbEmpresa.regimenSimplificado;
            //chkExento.Checked = (bool)Global.Usuario.tbEmpresa.regimenSimplificado;
            //chkAplicaExo.Checked = !(bool)Global.Usuario.tbEmpresa.regimenSimplificado;

        }
        
        private void recuperarEntidad(tbProducto entidad)
        {
            throw new NotImplementedException();
        }

        private void btnsalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmPromociones_Load(object sender, EventArgs e)
        {
            gbxFechas.Enabled = false;
            listaImpuestos = ImpIns.getImpuestos(1);
            cboEstado.SelectedIndex = 0;
            cargarDatos();
        }

        private void cargarDatos()
        {
       
            listaPromos = promoIns.GetListEntities((int)Enums.EstadoBusqueda.Activo);
           
            //formatoGrid();
            cargarLista((List<tbPromociones>)listaPromos);
        }
        private void formatoGrid()
        {
            dtgvPromociones.BorderStyle = BorderStyle.None;
            dtgvPromociones.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            dtgvPromociones.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dtgvPromociones.DefaultCellStyle.SelectionBackColor = Color.OrangeRed;
            dtgvPromociones.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dtgvPromociones.BackgroundColor = Color.White;



            dtgvPromociones.EnableHeadersVisualStyles = false;
            dtgvPromociones.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dtgvPromociones.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
            dtgvPromociones.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            //  dtgvDetalleFactura.DefaultCellStyle.Font = new Font("Microsoft Sans Serif", 16);



        }
        private void cargarLista(List<tbPromociones> lista)
        {
            try
            {
                lista = lista.OrderByDescending(x => x.fechaIncio).ToList();
                dtgvPromociones.Rows.Clear();

                foreach (var doc in lista)
                {

                    DataGridViewRow row = (DataGridViewRow)dtgvPromociones.Rows[0].Clone();
                    row.Cells[0].Value = doc.id;
                    row.Cells[1].Value = doc.tbProducto.idProducto;
                    row.Cells[2].Value = doc.tbProducto.nombre.Trim();

                    row.Cells[3].Value = Utility.priceFormat(doc.tbProducto.precioVenta1);
                    row.Cells[4].Value = doc.fechaIncio;
                    row.Cells[5].Value = doc.fechaCierre;
                    row.Cells[6].Value = Utility.priceFormat( doc.descuento);

                    decimal imp= (decimal)listaImpuestos.Where(x => x.id == doc.tbProducto.idTipoImpuesto).SingleOrDefault().valor;
                    ProductoDTO pro = ProductosUtility.calcularPrecio(doc.tbProducto.precio_real, doc.descuento,(decimal)doc.tbProducto.utilidad1Porc, imp);
                    row.Cells[7].Value = Utility.priceFormat(pro.Precio1);

                    row.Cells[8].Value = "Editar";
                    row.Cells[9].Value = "Eliminar";



                    //activar color

                    if (doc.fechaIncio <= Utility.getDate() && doc.fechaCierre >= Utility.getDate())
                    {
                        row.DefaultCellStyle.BackColor =  Color.LimeGreen;

                    }
                    else if(doc.fechaCierre < Utility.getDate())
                    {
                        row.DefaultCellStyle.BackColor = Color.Red;
                    }
                    else if(doc.fechaIncio> Utility.getDate())
                    {
                        row.DefaultCellStyle.BackColor = Color.Yellow;
                    }




                    dtgvPromociones.Rows.Add(row);
                    // dtgvDetalleFactura.Rows[listaDetalleDocumento.Count-1].Selected=true;
                }
            }
            catch (Exception ex)
            {
                
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            frmPromociones form = new frmPromociones();
            form.ShowDialog();
            cargarDatos();
        }

        private void dtgvPromociones_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 8)
            {

                if (dtgvPromociones.Rows[e.RowIndex].Cells[0].Value!=null)
                {
                    int idPromo = (int)dtgvPromociones.Rows[e.RowIndex].Cells[0].Value;
                    var promo = listaPromos.Where(x => x.id == idPromo).SingleOrDefault();
                    frmPromociones form = new frmPromociones();
                    form.promocion = promo;
                    form.ShowDialog();
                    cargarDatos();
                }

                

            }
            else if (e.ColumnIndex == 9)
            {
                if (dtgvPromociones.Rows[e.RowIndex].Cells[0].Value != null)
                {
                    int idPromo = (int)dtgvPromociones.Rows[e.RowIndex].Cells[0].Value;
                    var promo = listaPromos.Where(x => x.id == idPromo).SingleOrDefault();

                    DialogResult result = MessageBox.Show("¿Desea eliminar la promoción del producto: " + promo.idProducto + "-" + promo.tbProducto.nombre.Trim().ToUpper() + "?", "Eliminar Promoción", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        promo.estado = false;
                        promoIns.Actualizar(promo);

                    }

                    cargarDatos();

                }
               
            }
        }

        private void gboDatos_Enter(object sender, EventArgs e)
        {

        }

        private void btnRefrescar_Click(object sender, EventArgs e)
        {
            chkFechas.Checked = false;
            cboEstado.SelectedIndex = 0;
            cargarDatos();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (dtpFin.Value.Date>=dtpInicio.Value.Date)
            {
                aplicarFiltro();
                txtId.ResetText();
            }
            else
            {
                MessageBox.Show("Las fecha fin no puede ser menor a la fecha incio","Error Fechas",MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            

        }

        private void aplicarFiltro()
        {
            cargarDatos();
            if (txtId.Text != string.Empty)
            {
                listaPromos = listaPromos.Where(x => x.idProducto.Trim().ToUpper() == txtId.Text.Trim().ToUpper()).ToList();
            }

            if (cboEstado.SelectedIndex != 0)
            {
                //activas
                if (cboEstado.SelectedIndex == 1)
                {
                    listaPromos = listaPromos.Where(x => x.fechaIncio <= Utility.getDate() && x.fechaCierre >= Utility.getDate()).ToList();
                }
                else if(cboEstado.SelectedIndex == 2) //pendientes
                {
                    listaPromos = listaPromos.Where(x => x.fechaIncio > Utility.getDate()).ToList();

                }
                else // vencidas
                {
                    listaPromos = listaPromos.Where(x => x.fechaCierre < Utility.getDate()).ToList();

                }
            }
            if (chkFechas.Checked)
            {
                listaPromos = listaPromos.Where(x => x.fechaIncio.Date >= dtpInicio.Value.Date && x.fechaIncio.Date <= dtpFin.Value.Date).ToList();


            }


            cargarLista((List<tbPromociones>)listaPromos);
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
    }
}
