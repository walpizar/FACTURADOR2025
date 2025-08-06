using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
using System;
using System.ComponentModel;
using System.Data.SqlClient;
using System.IO;
using System.Linq;

namespace CommonLayer
{
    public class Backup
    {
        //Configura la carpeta donde se guardarán los backups y el prefijo de nombre.
        private readonly string backupFolder = CommonLayer.Global.Usuario.tbEmpresa.tbParametrosEmpresa.FirstOrDefault().rutaBackUp.Trim();
        private readonly string backupFilePrefix = "dbSISSODINA";
        // Número máximo de backups a conservar.
        private readonly int maxBackups = 3;


        public Backup()
        {

            BackgroundWorker tarea = new BackgroundWorker();

            tarea.DoWork += realizarRespaldo;
            tarea.RunWorkerAsync();

        }



        private void realizarRespaldo(object o, DoWorkEventArgs e)
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


                // Inicia el backup de forma asíncrona.
                backup.SqlBackupAsync(sqlServer);


            }
            catch (Exception ex)
            {
                throw ex;
            }


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
               // MessageBox.Show("Error al eliminar backups antiguos: " + ex.Message,
                             //   "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }


}
