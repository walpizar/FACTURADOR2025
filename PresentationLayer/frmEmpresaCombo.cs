using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace PresentationLayer
{
    public partial class frmEmpresaCombo : Form
    {
        public delegate void pasaDatos(string path);
        public event pasaDatos pasarDatosEvent;


        public string[] listaEmpresas { get; set; }
        public frmEmpresaCombo()
        {
            InitializeComponent();
        }

        private void frmEmpresaCombo_Load(object sender, EventArgs e)
        {
            string result;
            if (listaEmpresas.Count() > 0)
            {
                foreach (var empresa in listaEmpresas)
                {
                    result = Path.GetFileNameWithoutExtension(empresa);
                    cboEmpresa.Items.Add(result);

                }
            }
            cboEmpresa.SelectedIndex = 0;
        }

        private void btnSeleccionar_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (var empresa in listaEmpresas)
                {
                    if (Path.GetFileNameWithoutExtension(empresa).Trim().ToUpper() == cboEmpresa.Text.Trim().ToUpper())
                    {

                        pasarDatosEvent(empresa);
                        this.Close();

                    }

                }



            }
            catch (Exception)
            {

                throw;
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            pasarDatosEvent(null);
            this.Close();
        }
    }
}
