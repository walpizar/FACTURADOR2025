namespace CommonLayer.DTO
{
    /// <summary>
    /// Resultado de una operación de respaldo de base de datos, entregado
    /// vía el evento ServicioRespaldoBaseDatos.RespaldoCompletado. Contiene
    /// toda la info que antes se mostraba directo en un MessageBox — ahora
    /// queda a criterio de quien llama (formulario, tarea programada, etc.)
    /// decidir cómo presentarla.
    /// </summary>
    public class ResultadoRespaldo
    {
        public bool Exitoso { get; set; }
        public string RutaArchivo { get; set; }
        public string MensajeError { get; set; }
        public string DetalleError { get; set; }
    }
}