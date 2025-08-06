using CommonLayer;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace PresentationLayer
{
    public partial class frmRespaldoRestaurar : Form
    {
        //Configura la carpeta donde se guardarán los backups y el prefijo de nombre.
        private readonly string backupFolder = CommonLayer.Global.Usuario.tbEmpresa.tbParametrosEmpresa.FirstOrDefault().rutaBackUp.Trim();
        private readonly string backupFilePrefix = "dbSISSODINA";
        // Número máximo de backups a conservar.
        private readonly int maxBackups = 3;

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
                // Datos de conexión y de la base de datos a respaldar.
                string servidor = Global.Configuracion.server;         // Ejemplo: "localhost\SQLEXPRESS"
                string baseDatos = "dbSISSODINA";      // Cambia por el nombre de tu BD

                // Genera un nombre de archivo único con timestamp.
                string backupFileName = $"{backupFilePrefix}_{DateTime.Now:yyyyMMdd_HHmmss}.bak";
                string backupFilePath = Path.Combine(backupFolder, backupFileName);

                // Conecta al servidor (usa autenticación integrada; para SQL Server Authentication, proporciona usuario y contraseña).
                ServerConnection serverConnection = new ServerConnection(servidor);
                Server sqlServer = new Server(serverConnection);

                // Configura el objeto Backup.
                Microsoft.SqlServer.Management.Smo.Backup backup = new Microsoft.SqlServer.Management.Smo.Backup()
                {
                    Action = BackupActionType.Database,
                    Database = baseDatos,
                    Initialize = true, // Crea un nuevo archivo, sin acumulación de respaldos.
                    BackupSetName = $"Backup de {baseDatos}",
                    BackupSetDescription = $"Backup completo de la base de datos {baseDatos}",
                    CompressionOption = BackupCompressionOptions.On, // Activa la compresión (si el servidor lo permite).
                    PercentCompleteNotification = 10
                };

                // Agrega el dispositivo de backup (archivo).
                backup.Devices.AddDevice(backupFilePath, DeviceType.File);

                // Suscribirse a los eventos para actualizar la UI.
                backup.PercentComplete += Backup_PercentComplete;
                backup.Complete += Backup_Complete;

                // Reinicia el ProgressBar.
                progressBar1.Value = 0;

                // Inicia el backup de forma asíncrona.
                backup.SqlBackupAsync(sqlServer);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al iniciar el backup: " + ex.Message,
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void Backup_Complete(object sender, ServerMessageEventArgs e)
        {
            // Debido a que este evento se ejecuta en un hilo secundario, usamos Invoke para actualizar la UI.
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() =>
                {
                    MessageBox.Show("Backup completado!", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    // Una vez finalizado, limpia los backups antiguos.
                    CleanupOldBackups();
                   
                }));
            }
            else
            {
                MessageBox.Show("Backup completado!", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CleanupOldBackups();
         
            }
        }

        private void Backup_PercentComplete(object sender, PercentCompleteEventArgs e)
        {
            if (progressBar1.InvokeRequired)
            {
                progressBar1.Invoke(new Action(() => { progressBar1.Value = e.Percent; }));
            }
            else
            {
                progressBar1.Value = e.Percent;
            }
        }

        private void frmRespaldoRestaurar_Load(object sender, EventArgs e)
        {
            txtDirectorio.Text = Global.Usuario.tbEmpresa.tbParametrosEmpresa.FirstOrDefault().rutaBackUp.Trim();
        }

        private void btnsalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CleanupOldBackups()
        {
            try
            {
                DirectoryInfo di = new DirectoryInfo(backupFolder);
                // Filtra los archivos que coinciden con el patrón, por ejemplo: backup_*.bak
                var backupFiles = di.GetFiles($"{backupFilePrefix}_*.bak")
                                    .OrderByDescending(f => f.CreationTime)
                                    .ToList();

                // Si hay más de 'maxBackups' archivos, elimina los más antiguos.
                if (backupFiles.Count > maxBackups)
                {
                    var filesToDelete = backupFiles.Skip(maxBackups);
                    foreach (var file in filesToDelete)
                    {
                        file.Delete();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar backups antiguos: " + ex.Message,
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
