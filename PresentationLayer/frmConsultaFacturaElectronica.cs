using BusinessLayer;
using CommonLayer;
using CommonLayer.Exceptions.BussinessExceptions;
using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PresentationLayer
{
    public partial class frmConsultaFacturaElectronica : Form
    {
        BFacturacion facturacion = new BFacturacion();
        public string clave { get; set; }


        public frmConsultaFacturaElectronica()
        {
            InitializeComponent();
        }

        private async void frmConsultaFacturaElectronica_Load(object sender, EventArgs e)
        {
            cboTipoDoc.DataSource = Enum.GetValues(typeof(Enums.TipoDocumento));

            cboTipoBusqueda.DataSource = Enum.GetValues(typeof(Enums.ConsultarHacienda));
            if (clave != null)
            {
                if (clave != string.Empty)
                {
                    txtClave.Text = clave;
                     Consultar();

                }
            }



        }

        private async void btnConsultar_Click(object sender, EventArgs e)
        {

            await Consultar();
        }

        private async 
        Task
Consultar()
        {
            if (!Utility.AccesoInternet())
            {
                MessageBox.Show("No hay acceso a internet", "Sin Internet",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtClave.Text))
            {
                MessageBox.Show("Debe indicar un valor a buscar");
                return;
            }

            try
            {
                string resultadoXml = null;

                switch ((Enums.ConsultarHacienda)cboTipoBusqueda.SelectedValue)
                {
                    case Enums.ConsultarHacienda.Clave:
                        resultadoXml = facturacion
                            .consultarFacturaElectronicaPorClave(txtClave.Text.Trim()).Result;
                        break;

                    case Enums.ConsultarHacienda.Consecutivo:
                        // Ahora sí se espera el Task<string>
                        resultadoXml = facturacion
                            .consultarFacturaElectronicaPorConsecutivoAsync(txtClave.Text.Trim()).Result;
                        break;

                    default:
                        if (cboTipoDoc.SelectedValue == null ||
                            (int)cboTipoDoc.SelectedValue == 0)
                        {
                            MessageBox.Show("Debe indicar un tipo de documento para poder consultar por ID de Documento");
                            return;
                        }
                        // Si este método es síncrono y devuelve string, no necesita await
                        resultadoXml = facturacion
                            .consultarFacturaElectronicaPorIdFact(
                                int.Parse(txtClave.Text.Trim()),
                                (int)cboTipoDoc.SelectedValue).Result;
                        break;
                }
                    string mensajeLimpio = Regex.Replace( resultadoXml, @":Signature.*$","",RegexOptions.Multiline);

                // Si además quieres recortar espacios sobrantes
                mensajeLimpio = mensajeLimpio.Trim();
                // Finalmente asignas el XML decodificado o crudo al TextBox
                txtXMLSinFirma.Text = mensajeLimpio;
            }
            catch (TokenException tex)
            {
                MessageBox.Show(tex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Se produjo un error al consultar a Hacienda:\n" + ex.Message);
            }
        }

        private void cboTipoBusqueda_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtXMLSinFirma.Text = string.Empty;
            if ((int)cboTipoBusqueda.SelectedValue == (int)Enums.ConsultarHacienda.IDDocumento)
            {
                cboTipoDoc.SelectedIndex = 1;
                cboTipoDoc.SelectedIndex = 0;
                cboTipoDoc.Enabled = true;


            }
            else
            {
                cboTipoDoc.Text = string.Empty;
                cboTipoDoc.SelectedIndex = 0;
                cboTipoDoc.Enabled = false;
            }
        }

        private void cboTipoDoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtXMLSinFirma.Text = string.Empty;
        }

        private void btnsalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
