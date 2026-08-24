using BusinessLayer.Respaldos;
using CommonLayer;
using System;
using System.Linq;
using System.Windows.Forms;

namespace PresentationLayer
{
    public partial class frmRespaldoRestaurar : FormBase
    {
        private ServicioRespaldoBaseDatos servicioRespaldo;

        public frmRespaldoRestaurar()
        {
            InitializeComponent();
            progressBar1.Minimum = 0;
            progressBar1.Maximum = 100;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            realizarRespaldo();
        }

        private void realizarRespaldo()
        {
            try
            {
                // El servicio arma el servidor solo (server + instance,
                // ej. ".\SQLEXPRESS") a partir de Global.Configuracion,
                // y la carpeta de destino desde tbParametrosEmpresa.rutaBackUp.
                // Retención por defecto: últimos 7 días.
                servicioRespaldo = ServicioRespaldoBaseDatos.CrearDesdeConfiguracionActual();

                servicioRespaldo.ProgresoActualizado += ServicioRespaldo_ProgresoActualizado;
                servicioRespaldo.RespaldoCompletado += ServicioRespaldo_RespaldoCompletado;

                progressBar1.Value = 0;
                servicioRespaldo.IniciarRespaldo();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al iniciar el backup: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ServicioRespaldo_ProgresoActualizado(object sender, int porcentaje)
        {
            if (progressBar1.InvokeRequired)
            {
                progressBar1.Invoke(new Action(() => progressBar1.Value = porcentaje));
            }
            else
            {
                progressBar1.Value = porcentaje;
            }
        }

        private void ServicioRespaldo_RespaldoCompletado(object sender, CommonLayer.DTO.ResultadoRespaldo resultado)
        {
            Action mostrarResultado = () =>
            {
                if (resultado.Exitoso)
                {
                    MessageBox.Show("Backup completado!", "Información",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(
                        "Error al iniciar el backup: " + resultado.MensajeError +
                        Environment.NewLine + Environment.NewLine +
                        "Detalle: " + resultado.DetalleError,
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            };

            if (this.InvokeRequired)
            {
                this.Invoke(mostrarResultado);
            }
            else
            {
                mostrarResultado();
            }

            // Nos desuscribimos: evita que si el usuario hace varios backups
            // seguidos desde este form, los handlers se acumulen y disparen
            // el MessageBox varias veces por un solo evento.
            servicioRespaldo.ProgresoActualizado -= ServicioRespaldo_ProgresoActualizado;
            servicioRespaldo.RespaldoCompletado -= ServicioRespaldo_RespaldoCompletado;
        }

        private void frmRespaldoRestaurar_Load(object sender, EventArgs e)
        {
            txtDirectorio.Text = Global.Usuario.tbEmpresa.tbParametrosEmpresa
                .FirstOrDefault().rutaBackUp.Trim();
        }

        private void btnsalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}