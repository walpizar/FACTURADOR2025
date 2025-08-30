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
            string servidor = Global.Configuracion.server;   // "localhost\\SQLEXPRESS"
            string baseDatos = "dbSISSODINA";
            string fileName = $"{backupFilePrefix}_{DateTime.Now:yyyyMMdd_HHmmss}.bak";
            string filePath = Path.Combine(backupFolder, fileName);

            // Cree SIEMPRE una conexión nueva por operación
            var conn = new ServerConnection(servidor)
            {
                // Si usa Windows Auth (predeterminado):
                //LoginSecure = true,

                //Si usa SQL Auth,
                //descomente y configure:
                 LoginSecure = false,
                 Login = "sa",
                 Password = "crpp",

                ConnectTimeout = 30
            };

            var server = new Server(conn);

            var backup = new Microsoft.SqlServer.Management.Smo.Backup
            {
                Action = BackupActionType.Database,
                Database = baseDatos,
                Initialize = true,
                BackupSetName = $"Backup de {baseDatos}",
                BackupSetDescription = $"Backup completo de la base de datos {baseDatos}",
                CompressionOption = BackupCompressionOptions.On,
                PercentCompleteNotification = 10
            };
            backup.Devices.AddDevice(filePath, DeviceType.File);

            // Progreso opcional hacia el BackgroundWorker
            backup.PercentComplete += (s, args) =>
            {
                if (((BackgroundWorker)o).WorkerReportsProgress)
                    ((BackgroundWorker)o).ReportProgress(args.Percent);
            };

            try
            {
                backup.SqlBackup(server); // Bloquea hasta terminar: sin carreras ni hilos extra
                e.Result = filePath;      // reporte de salida
            }
            catch (Exception ex)
            {
                // Manejo de errores
                e.Result = ex;
                throw;
            }
            finally
            {
                // Limpieza explícita
                conn.Disconnect();
                server.ConnectionContext.Disconnect();
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
