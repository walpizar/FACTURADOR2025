// Este archivo NO es auto-generado por el EDMX: es una extensión manual
// (partial class) para las 3 columnas nuevas agregadas a tbCompras vía
// AlterTable_tbCompras_MensajeReceptor.sql. Después de correr ese script,
// hacé "Update Model from Database" en el .edmx y esta partial class
// puede eliminarse (las propiedades pasarán a estar en el archivo
// auto-generado). Mientras tanto, esto permite compilar y usarlas ya.

namespace EntityLayer
{
    public partial class tbCompras
    {
        /// <summary>
        /// Condición del IVA para el Mensaje Receptor (nota 18 de Hacienda).
        /// "01" General Crédito IVA, "02" General Crédito parcial del IVA,
        /// "03" Bienes de Capital, "04" Gasto corriente no genera crédito,
        /// "05" Proporcionalidad. Null = no capturado (se usa "01" por defecto al generar el XML).
        /// </summary>
        public string condicionImpuesto { get; set; }

        /// <summary>
        /// Monto del impuesto a acreditar, cuando el monto total del impuesto
        /// pagado no forma en su totalidad parte del crédito aplicable.
        /// Null = no aplica (no se incluye en el XML).
        /// </summary>
        public decimal? montoImpuestoAcreditar { get; set; }

        /// <summary>
        /// Monto total del gasto a aplicar, cuando el monto total del comprobante
        /// no forma en su totalidad un gasto deducible.
        /// Null = no aplica (no se incluye en el XML).
        /// </summary>
        public decimal? montoGastoAplicable { get; set; }
    }
}
