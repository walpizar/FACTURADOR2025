using CommonLayer.DTO;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
using System;
using System.IO;
using System.Linq;

namespace BusinessLayer.Respaldos
{
    /// <summary>
    /// Servicio de respaldo de base de datos SQL Server, reutilizable desde
    /// cualquier formulario, tarea programada, o proceso en background.
    /// No conoce nada de UI (sin MessageBox, sin ProgressBar) — se comunica
    /// exclusivamente por eventos, así cada consumidor decide cómo mostrar
    /// el progreso/resultado.
    /// </summary>
    public class ServicioRespaldoBaseDatos
    {
        /// <summary>Se dispara con el porcentaje de avance (0-100).</summary>
        public event EventHandler<int> ProgresoActualizado;

        /// <summary>Se dispara al terminar (éxito o error).</summary>
        public event EventHandler<ResultadoRespaldo> RespaldoCompletado;

        private readonly string _servidor;
        private readonly string _baseDatos;
        private readonly string _carpetaDestino;
        private readonly string _prefijoArchivo;
        private readonly int _diasRetencion;

        /// <param name="servidor">
        /// Ej. "localhost\SQLEXPRESS" o "DESKTOP-GQ7VEQ2\SQLEXPRESS".
        /// Para instancias con nombre, el nombre de instancia es obligatorio
        /// (no alcanza con "." solo).
        /// </param>
        /// <param name="baseDatos">Nombre de la base de datos a respaldar.</param>
        /// <param name="carpetaDestino">Carpeta donde se guardan los .bak.</param>
        /// <param name="prefijoArchivo">Prefijo del nombre de archivo (ej. "dbSISSODINA").</param>
        /// <param name="diasRetencion">
        /// Cuántos días de respaldos conservar (por fecha de creación del
        /// archivo). Los más viejos que ese rango se borran. Por defecto 7.
        /// </param>
        public ServicioRespaldoBaseDatos(
            string servidor,
            string baseDatos,
            string carpetaDestino,
            string prefijoArchivo,
            int diasRetencion = 7)
        {
            _servidor = servidor;
            _baseDatos = baseDatos;
            _carpetaDestino = carpetaDestino;
            _prefijoArchivo = prefijoArchivo;
            _diasRetencion = diasRetencion;
        }

        /// <summary>
        /// Construye el servicio a partir de la configuración/empresa actual
        /// del sistema. Combina Global.Configuracion.server (ej. ".") con
        /// Global.Configuracion.instance (ej. "SQLEXPRESS") para armar el
        /// nombre completo del servidor (ej. ".\SQLEXPRESS") — para
        /// instancias con nombre, el servidor solo (".") no alcanza para
        /// conectar, hace falta el nombre de instancia.
        /// </summary>
        public static ServicioRespaldoBaseDatos CrearDesdeConfiguracionActual(
            string baseDatos = "dbSISSODINA",
            string prefijoArchivo = "dbSISSODINA",
            int diasRetencion = 7)
        {
            string servidor = ConformarNombreServidor(
                CommonLayer.Global.Configuracion.server,
                CommonLayer.Global.Configuracion.instance);

            string carpetaDestino = CommonLayer.Global.Usuario.tbEmpresa.tbParametrosEmpresa
                .FirstOrDefault().rutaBackUp.Trim();

            return new ServicioRespaldoBaseDatos(
                servidor, baseDatos, carpetaDestino, prefijoArchivo, diasRetencion);
        }

        /// <summary>
        /// Arma el nombre completo de servidor a partir de servidor base +
        /// instancia, validando que el servidor no venga vacío.
        /// - server vacío/null → se usa "." (instancia local por defecto)
        ///   como fallback razonable, en vez de fallar directo.
        /// - instance vacío/null → se usa solo el servidor (instancia
        ///   default, sin nombre).
        /// - ambos con valor → "server\instance" (ej. ".\SQLEXPRESS").
        /// - si el servidor YA incluye "\" (alguien puso "localhost\SQLEXPRESS"
        ///   directo en el campo "server"), no se duplica el sufijo.
        /// </summary>
        private static string ConformarNombreServidor(string server, string instance)
        {
            server = (server ?? string.Empty).Trim();
            instance = (instance ?? string.Empty).Trim();

            if (string.IsNullOrEmpty(server))
            {
                server = "."; // fallback: instancia local por defecto
            }

            if (string.IsNullOrEmpty(instance))
            {
                return server;
            }

            if (server.Contains("\\"))
            {
                // El campo server ya trae "servidor\instancia" completo;
                // no lo pisamos con el campo instance por separado.
                return server;
            }

            return $"{server}\\{instance}";
        }

        /// <summary>
        /// Inicia el respaldo de forma asíncrona (no bloquea el hilo que
        /// llama). El resultado llega por el evento RespaldoCompletado,
        /// SIEMPRE en el hilo del BackgroundWorker/SMO — quien se suscriba
        /// desde una UI debe hacer su propio Invoke/BeginInvoke.
        /// </summary>
        public void IniciarRespaldo()
        {
            try
            {
                if (!Directory.Exists(_carpetaDestino))
                {
                    Directory.CreateDirectory(_carpetaDestino);
                }

                string nombreArchivo = $"{_prefijoArchivo}_{DateTime.Now:yyyyMMdd_HHmmss}.bak";
                string rutaCompleta = Path.Combine(_carpetaDestino, nombreArchivo);

                var conexion = new ServerConnection(_servidor);
                var servidorSql = new Server(conexion);

                var backup = new Backup
                {
                    Action = BackupActionType.Database,
                    Database = _baseDatos,
                    Initialize = true,
                    BackupSetName = $"Backup de {_baseDatos}",
                    BackupSetDescription = $"Backup completo de la base de datos {_baseDatos}",
                    // Sin CompressionOption: SQL Server Express no la soporta.
                    // Si tu servidor es Standard/Enterprise, podés agregar
                    // CompressionOption = BackupCompressionOptions.On acá.
                    PercentCompleteNotification = 10
                };

                backup.Devices.AddDevice(rutaCompleta, DeviceType.File);

                backup.PercentComplete += (s, e) =>
                {
                    ProgresoActualizado?.Invoke(this, e.Percent);
                };

                backup.Complete += (s, e) =>
                {
                    LimpiarRespaldosAntiguos();
                    RespaldoCompletado?.Invoke(this, new ResultadoRespaldo
                    {
                        Exitoso = true,
                        RutaArchivo = rutaCompleta
                    });
                };

                backup.SqlBackupAsync(servidorSql);
            }
            catch (Exception ex)
            {
                string detalle = ex.InnerException != null ? ex.InnerException.Message : ex.Message;

                RespaldoCompletado?.Invoke(this, new ResultadoRespaldo
                {
                    Exitoso = false,
                    MensajeError = ex.Message,
                    DetalleError = detalle
                });
            }
        }

        /// <summary>
        /// Versión síncrona (bloquea hasta terminar). Útil para llamarla
        /// desde dentro de un BackgroundWorker.DoWork o una tarea programada,
        /// donde ya estás en un hilo secundario y no hace falta el patrón
        /// async de SMO.
        /// </summary>
        public ResultadoRespaldo EjecutarRespaldoSincrono()
        {
            try
            {
                if (!Directory.Exists(_carpetaDestino))
                {
                    Directory.CreateDirectory(_carpetaDestino);
                }

                string nombreArchivo = $"{_prefijoArchivo}_{DateTime.Now:yyyyMMdd_HHmmss}.bak";
                string rutaCompleta = Path.Combine(_carpetaDestino, nombreArchivo);

                var conexion = new ServerConnection(_servidor);
                var servidorSql = new Server(conexion);

                var backup = new Backup
                {
                    Action = BackupActionType.Database,
                    Database = _baseDatos,
                    Initialize = true,
                    BackupSetName = $"Backup de {_baseDatos}",
                    BackupSetDescription = $"Backup completo de la base de datos {_baseDatos}",
                    PercentCompleteNotification = 10
                };

                backup.Devices.AddDevice(rutaCompleta, DeviceType.File);
                backup.PercentComplete += (s, e) => ProgresoActualizado?.Invoke(this, e.Percent);

                backup.SqlBackup(servidorSql); // bloquea hasta terminar

                LimpiarRespaldosAntiguos();

                var resultado = new ResultadoRespaldo { Exitoso = true, RutaArchivo = rutaCompleta };
                RespaldoCompletado?.Invoke(this, resultado);
                return resultado;
            }
            catch (Exception ex)
            {
                string detalle = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                var resultado = new ResultadoRespaldo
                {
                    Exitoso = false,
                    MensajeError = ex.Message,
                    DetalleError = detalle
                };
                RespaldoCompletado?.Invoke(this, resultado);
                return resultado;
            }
        }

        /// <summary>
        /// Elimina los respaldos con más de _diasRetencion días de antigüedad
        /// (según fecha de creación del archivo), conservando el resto. Se
        /// llama automáticamente al terminar un respaldo exitoso, pero
        /// también se puede invocar manualmente.
        /// </summary>
        public void LimpiarRespaldosAntiguos()
        {
            try
            {
                if (!Directory.Exists(_carpetaDestino))
                    return;

                DirectoryInfo directorio = new DirectoryInfo(_carpetaDestino);

                // Obtener los respaldos ordenados:
                // más reciente -> más antiguo
                var respaldos = directorio
                    .GetFiles($"{_prefijoArchivo}_*.bak")
                    .OrderByDescending(f => f.LastWriteTime)
                    .ToList();

                // Validar parámetro
                int cantidadConservar = _diasRetencion;

                if (cantidadConservar < 1)
                    cantidadConservar = 1;

                // Si no excede la cantidad permitida, no borrar nada
                if (respaldos.Count <= cantidadConservar)
                    return;

                // Saltar los más recientes y eliminar los más antiguos
                var respaldosAEliminar = respaldos
                    .Skip(cantidadConservar)
                    .ToList();

                foreach (FileInfo archivo in respaldosAEliminar)
                {
                    try
                    {
                        archivo.Delete();
                    }
                    catch
                    {
                        // Si un archivo no se puede eliminar,
                        // continúa con los demás.
                    }
                }
            }
            catch
            {
                // Un error de limpieza no debe afectar
                // el respaldo recién realizado.
            }
        }
    }
}