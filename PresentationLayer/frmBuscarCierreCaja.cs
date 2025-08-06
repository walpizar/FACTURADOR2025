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
    public partial class frmBuscarCierreCaja : Form
    {
        BCajas cajaIns = new BCajas();
        private tbCajasMovimientos cierreSelect { get; set; }
        private List<tbCajasMovimientos> listaCierres { get; set; }
        public frmBuscarCierreCaja()
        {
            InitializeComponent();
        }

        private void frmBuscarCierreCaja_Load(object sender, EventArgs e)
        {
            cargarDatos();
        }

        private void cargarDatos()
        {

            listaCierres = cajaIns.getListCierres(dtpInicio.Value.Date, dtpFin.Value.Date);
            cargarLista();



        }

        private void cargarLista()
        {
            try
            {
                lsvbuscar.Items.Clear();
                foreach (tbCajasMovimientos p in listaCierres)
                {
                    ListViewItem item = new ListViewItem();

                    item.Text = p.id.ToString().Trim();
                    item.SubItems.Add(p.sucursal.ToString());
                    item.SubItems.Add(p.caja.ToString());
                    item.SubItems.Add(p.usuarioApertura==null ? "": p.usuarioApertura.ToUpper().Trim());
                    item.SubItems.Add(p.fechaApertura.ToString()) ;
                    item.SubItems.Add(Utility.priceFormat(p.montoApertura));
                    item.SubItems.Add(p.usuarioCierre == null ? "" : p.usuarioCierre.ToUpper().Trim());
                    item.SubItems.Add(p.fechaCierre == null ? "": p.fechaCierre.ToString());
                    item.SubItems.Add(p.montoCierre == null ? "" : Utility.priceFormat((decimal)p.montoCierre)) ;

                    lsvbuscar.Items.Add(item);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            cargarDatos();
        }

        private void lsvbuscar_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (cierreSelect!=null)
            {
                frmEstadoCaja frm = new frmEstadoCaja();
                frm.cajaCierre = cierreSelect;
                frm.ShowDialog();
            }
        }

       

        private void lsvbuscar_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (lsvbuscar.SelectedItems.Count > 0)
                {
                    string idSelected = lsvbuscar.SelectedItems[0].Text;

                    cierreSelect = listaCierres.Where(x => x.id == int.Parse(idSelected)).FirstOrDefault();


                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void btnsalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
