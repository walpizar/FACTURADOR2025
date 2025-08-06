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
    public partial class frmComentariosComandas : Form
    {
        private List<clsComanda> _listaComandas { get; set; }
        public delegate void pasaDatos(List<clsComanda> lista);
        public event pasaDatos pasarDatosEvent;

        BProducto proIns = new BProducto(); 

        public frmComentariosComandas()
        {
            InitializeComponent();
        }

        public frmComentariosComandas(List<clsComanda> listaComandas)
        {
            InitializeComponent();
            _listaComandas = listaComandas;
        }

        private void actualizarGridView()
        {
            dtgvComentarios.Rows.Clear();
            int conta = 0;

            foreach (clsComanda detalle in _listaComandas)
            {
                conta++;
                List<tbAcompanamiento> listaAcomp = proIns.getProductoAcompa(detalle.codigoProducto);

               

                int rowId = dtgvComentarios.Rows.Add();

                // Grab the new row!
                DataGridViewRow row = dtgvComentarios.Rows[rowId];

                //Creamos un objeto de tipo ListviewItem

                row.Cells[0].Value = detalle.numero;             
                row.Cells[1].Value = detalle.nombre;
                row.Cells[2].Value = detalle.descripcion==null?string.Empty: detalle.descripcion;
               

               // dtgvComentarios.Rows.Add(row);

              
            }

        }

        private void frmComentariosComandas_Load(object sender, EventArgs e)
        {

            actualizarGridView();
        }

        private void dtgvComentarios_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                //colComentario
                if (dtgvComentarios.Columns[e.ColumnIndex].Name == "colComentario")
                {
                    int numero = int.Parse(dtgvComentarios.Rows[e.RowIndex].Cells[0].Value.ToString());
                    if (numero != -1)
                    {
                        _listaComandas.Where(x => x.numero == numero).SingleOrDefault().descripcion = dtgvComentarios.Rows[e.RowIndex].Cells[2].Value.ToString().Trim().ToUpper();

                    }
                }

            }
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            dtgvComentarios.EndEdit();
            pasarDatosEvent(_listaComandas);
            this.Close();

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            pasarDatosEvent(null);
            this.Close();
        }

        private void chkLlevar_CheckedChanged(object sender, EventArgs e)
        {
            foreach (var item in _listaComandas)
            {
                item.llevar = chkLlevar.Checked;

            }
              
        }
    }
}
