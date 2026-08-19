namespace CommonLayer.DTO
{
    /// <summary>
    /// Resultado de un envío a Hacienda (factura, mensaje receptor, etc.).
    /// Encapsula el código HTTP, el mensaje/motivo, y distingue el caso
    /// especial de "ya fue recibido anteriormente" (que no es un error de
    /// negocio real, aunque Hacienda lo devuelva como 400).
    /// </summary>
    public class ResultadoEnvioHacienda
    {
        /// <summary>
        /// true si Hacienda aceptó el envío ahora mismo (202 Accepted),
        /// o si ya lo tenía de un envío anterior (YaRecibidoPreviamente).
        /// En ambos casos el documento YA ESTÁ en Hacienda.
        /// </summary>
        public bool Exitoso { get; set; }

        /// <summary>
        /// true cuando el motivo del rechazo (x-error-cause) indica que el
        /// comprobante ya había sido recibido en un envío anterior — no es
        /// un error real, solo un reintento innecesario.
        /// </summary>
        public bool YaRecibidoPreviamente { get; set; }

        /// <summary>Código de estado HTTP devuelto por Hacienda (ej. "Accepted", "BadRequest").</summary>
        public string StatusCode { get; set; }

        /// <summary>
        /// Mensaje/motivo más específico disponible: header x-error-cause si
        /// vino, si no el cuerpo de la respuesta, si no un texto genérico.
        /// </summary>
        public string Mensaje { get; set; }

        /// <summary>JSON que se envió a Hacienda (para depuración/auditoría).</summary>
        public string JsonEnvio { get; set; }

        /// <summary>Cuerpo crudo de la respuesta de Hacienda (para depuración/auditoría).</summary>
        public string JsonRespuesta { get; set; }
    }
}