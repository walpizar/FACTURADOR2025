using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CommonLayer
{
    public class clsMensajeXMLHaciendaRespuesta
    {

      
        public static string FormatearMensaje(string xml)
        {
            // 1. Cargar el XML
            XDocument doc = XDocument.Parse(xml);

            // 2. Buscar el elemento <MensajeHacienda> (independiente de su namespace)
            var msgElem = doc.Descendants()
                             .FirstOrDefault(e => e.Name.LocalName == "MensajeHacienda");
            if (msgElem == null)
                throw new InvalidOperationException("No se encontró <MensajeHacienda>.");

            // 3. Tomar su namespace real (puede ser v4.3 o v4.4 u otro)
            XNamespace ns = msgElem.Name.Namespace;

            // 4. Namespace de firma (siempre igual)
            XNamespace ds = "http://www.w3.org/2000/09/xmldsig#";

            // 5. Eliminar nodo de firma, si existe
            msgElem.Elements(ds + "Signature").Remove();

            // 6. Construir salida con los campos deseados
            var sb = new StringBuilder();
            void AppendIfExists(string localName, string label)
            {
                var el = msgElem.Element(ns + localName);
                if (el != null && !String.IsNullOrWhiteSpace(el.Value))
                    sb.AppendLine($"{label}: {el.Value.Trim()}");
            }

            AppendIfExists("Clave", "Clave");
            AppendIfExists("NombreEmisor", "Emisor");
            AppendIfExists("NumeroCedulaEmisor", "Cédula Emisor");
            AppendIfExists("MontoTotalImpuesto", "Monto Impuesto");
            AppendIfExists("TotalFactura", "Total Factura");
            AppendIfExists("Mensaje", "Código Mensaje");
            AppendIfExists("DetalleMensaje", "Detalle Mensaje");
            // …puede añadir más tags según necesidad

            return sb.ToString().TrimEnd();
        }
    }
   
}
