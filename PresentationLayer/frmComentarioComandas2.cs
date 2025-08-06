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
    public partial class frmComentarioComandas2 : Form
    {
        List<String> acompa = new List<string>();
        private List<clsComanda> _listaComandas { get; set; }
        public delegate void pasaDatos(List<clsComanda> lista);
        public event pasaDatos pasarDatosEvent;

        BProducto proIns = new BProducto();
        private bool isCargar = false;
        public frmComentarioComandas2()
        {
            InitializeComponent();
        }
        public frmComentarioComandas2(List<clsComanda> listaComandas)
        {
            InitializeComponent();
            _listaComandas = listaComandas;
        }



        private void frmComentarioComandas2_Load(object sender, EventArgs e)
        {

            isCargar = true;
            cargarDatos();

        }

        private void cargarDatos()
        {
            int conta = 0;
            int tam = 0;
            foreach (clsComanda detalle in _listaComandas)
            {
                conta++;
                List<tbAcompanamiento> listaAcomp = proIns.getProductoAcompa(detalle.codigoProducto);

                //creo el grupo para agregar controles
                GroupBox grupo = new GroupBox();
                grupo.Location = new Point(5, 15+tam );
                grupo.Height = 110;
                grupo.Width = 700;
                grupo.Text = detalle.numero.ToString();
                grupo.BackColor = detalle.estado ? Color.LightGreen : Color.Red;


                //control id
                TextBox idText = new TextBox();
                idText.Name = "id";
                idText.Width = 50;
                idText.Location= new Point(10, 15);
                idText.Text = detalle.codigoProducto.Trim();
                idText.Enabled = false;

                TextBox idProducto = new TextBox();
                idProducto.Name = "nombre";
                idProducto.Width = 240;
                idProducto.Location = new Point(70, 15);
                idProducto.Text =  detalle.nombre.Trim().ToUpper();
                idProducto.Enabled = false;


                TextBox idDesc = new TextBox();
                idDesc.Name = "desc";
                idDesc.Width = 300;
                idDesc.Height = 50;
                idDesc.Multiline = true;
                idDesc.Location = new Point(5, 40);             
                idDesc.Enabled = true;

                //agrego controles al grupo
                grupo.Controls.Add(idText);
                grupo.Controls.Add(idProducto);
                grupo.Controls.Add(idDesc);

                //agrego los acompañamientos al grupo

                int acompConta = 0, acomConta2 = 0, cantidad=0;
                foreach (var acomp in listaAcomp)
                {
                    cantidad++;
                    CheckBox check = new CheckBox();
                    check.Name =  acomp.id.ToString();
                    check.Location = new Point((320 +acompConta), 5+acomConta2);
                    check.Text = acomp.nombre.Trim().ToUpper();
                    check.Font = new Font(FontFamily.GenericMonospace, 7,FontStyle.Bold);
                    check.AutoSize=true;
                    
                    grupo.Controls.Add(check);

                    acompConta += 130;

                    if (cantidad == 3)
                    {
                        cantidad = 0;
                        acompConta = 0;
                        acomConta2 += 21;

                    }
              
                }
                panel1.Controls.Add(grupo);
                tam += 115;


            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            pasarDatosEvent(null);
            this.Close();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (actualizarComandas())
            {
                pasarDatosEvent(_listaComandas);
                this.Close();
            }
            else
            {
                MessageBox.Show("No se pudo imprimir la comanda", "Comandas", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }     
        }

        private bool actualizarComandas()
        {
            try
            {

                foreach (GroupBox control in panel1.Controls)
                {
                    List<Control> listControl = new List<Control>(control.Controls.OfType<TextBox>().OrderBy(x => x.TabIndex));                  

                    if (listControl.Where(x => x.Name == "desc").SingleOrDefault().Text != string.Empty)
                    {
                        _listaComandas.Where(x => x.numero == int.Parse(control.Text)).SingleOrDefault().descripcion = 
                            listControl.Where(x => x.Name == "desc").SingleOrDefault().Text.Trim().ToUpper();
                    }

                    List<CheckBox> listCheck = new List<CheckBox>(control.Controls.OfType<CheckBox>().OrderBy(x => x.TabIndex));

                     acompa = new List<string>();
                    foreach (CheckBox item in listCheck.Where(x=>x.Checked))
                    { 
                        acompa.Add(item.Text.Trim().ToUpper());                       
                    }

                    if (acompa.Count != 0)
                    {
                        _listaComandas.Where(x => x.numero == int.Parse(control.Text)).SingleOrDefault().acompanamientos = acompa;
                    }


                    _listaComandas.Where(x => x.numero == int.Parse(control.Text)).SingleOrDefault().llevar = chkLlevar.Checked;

                }
            }
            catch (Exception ex)
            {

                return false;
            }
            return true;
           
        }

        private void frmComentarioComandas2_KeyUp(object sender, KeyEventArgs e)
        {
            if (!isCargar)
            {
                if (e.KeyCode == Keys.Enter)
                {

                    if (actualizarComandas())
                    {
                        pasarDatosEvent(_listaComandas);
                        this.Close();
                    }
                }

            }
            
            isCargar = false;
        }
    }
}
