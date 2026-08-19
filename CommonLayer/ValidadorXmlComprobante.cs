using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace CommonLayer
{
    /// <summary>
    /// Valida que un archivo XML sea un comprobante electrónico real
    /// (factura, tiquete, nota crédito/débito, etc.) y NO un XML de
    /// respuesta/confirmación de Hacienda (MensajeReceptor, MensajeHacienda),
    /// que a veces terminan mezclados en la misma carpeta de correo/descargas.
    /// </summary>
    public static class ValidadorXmlComprobante
    {
        /// <summary>
        /// Nodos raíz válidos para "esto es una factura electrónica que
        /// se puede procesar como compra/gasto".
        /// </summary>
        private static readonly HashSet<string> TiposValidos = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            "FacturaElectronica",
            "TiqueteElectronico",
            "NotaCreditoElectronica",
            "NotaDebitoElectronica",
            "FacturaElectronicaCompra",
            "FacturaElectronicaExportacion",
            "ReciboElectronicoPago"
        };

        /// <summary>
        /// Nodos raíz que son XML de Hacienda pero NO son un comprobante
        /// (son mensajes de confirmación/respuesta) — se rechazan
        /// explícitamente con un mensaje claro, en vez de caer en el
        /// genérico "tipo no reconocido".
        /// </summary>
        private static readonly HashSet<string> TiposExcluidos = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            "MensajeReceptor",
            "MensajeHacienda"
        };

        /// <summary>
        /// Valida el nodo raíz del XML en la ruta indicada.
        /// </summary>
        /// <param name="rutaArchivo">Ruta al archivo .xml a validar.</param>
        /// <param name="motivoRechazo">
        /// Mensaje explicando por qué se rechazó, si EsXmlDeFacturaElectronica
        /// devuelve false. Null si es válido.
        /// </param>
        public static bool EsXmlDeFacturaElectronica(string rutaArchivo, out string motivoRechazo)
        {
            motivoRechazo = null;

            try
            {
                XDocument doc = XDocument.Load(rutaArchivo);
                string raiz = doc.Root?.Name.LocalName;

                if (string.IsNullOrEmpty(raiz))
                {
                    motivoRechazo = "El archivo no tiene un nodo raíz reconocible.";
                    return false;
                }

                if (TiposExcluidos.Contains(raiz))
                {
                    motivoRechazo = $"El archivo es un XML de respuesta/confirmación de Hacienda " +
                                     $"(\"{raiz}\"), no una factura electrónica. Cargue el XML del " +
                                     $"comprobante, no el de la confirmación.";
                    return false;
                }

                if (!TiposValidos.Contains(raiz))
                {
                    motivoRechazo = $"El nodo raíz \"{raiz}\" no corresponde a un comprobante " +
                                     $"electrónico reconocido.";
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                motivoRechazo = "No se pudo leer el archivo como XML: " + ex.Message;
                return false;
            }
        }
    }
}