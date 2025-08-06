using Newtonsoft.Json;
using System;

namespace FacturacionElectronicaLayer.Clases
{
    public class ClasesJson
    {
    }

    public class RespuestaHacienda
    {
        [JsonProperty("clave")]
        public string clave { get; set; }

        [JsonProperty("fecha")]
        public string fecha { get; set; }

        [JsonProperty("ind-estado")]
        public string ind_estado { get; set; }

        [JsonProperty("respuesta-xml")]
        public string respuesta_xml { get; set; }
    }

    public class RespuestaExoneracion
    {
        [JsonProperty("numeroDocumento")]
        public string numeroDocumento { get; set; }

        [JsonProperty("identificacion")]
        public string identificacion { get; set; }

        [JsonProperty("codigoProyectoCFIA")]
        public string codigoProyectoCFIA { get; set; }

        [JsonProperty("fechaEmision")]
        public string fechaEmision { get; set; }

        [JsonProperty("fechaVencimiento")]
        public string fechaVencimiento { get; set; }

        [JsonProperty("porcentajeExoneracion")]
        public string porcentajeExoneracion { get; set; }

        [JsonProperty("nombreInstitucion")]
        public string nombreInstitucion { get; set; }

        [JsonProperty("tipoDocumento")]
        public TipoDocExo tipoDoc { get; set; }
    }

    public class TipoDocExo
    {
        [JsonProperty("codigo")]
        public string codigo { get; set; }

        [JsonProperty("descripcion")]
        public string descripcion { get; set; }
        
    }

    public class Token
    {
        [JsonProperty("access_token")]
        public string access_token { get; set; }

        [JsonProperty("refresh_token")]
        public string refresh_token { get; set; }
    }

    public class Recepcion
    {
        [JsonProperty("clave")]
        public string clave { get; set; }

        [JsonProperty("fecha")]
        public string fecha { get; set; }

        [JsonProperty("emisor")]
        public Emisor emisor { get; set; }

        [JsonProperty("receptor")]
        public Receptor receptor { get; set; }

        [JsonProperty("comprobanteXml")]
        public string comprobanteXml { get; set; }
    }



    public class RecepcionMensaje
    {
        [JsonProperty("clave")]
        public string clave { get; set; }

        [JsonProperty("fecha")]
        public string fecha { get; set; }

        [JsonProperty("emisor")]
        public Emisor emisor { get; set; }

        [JsonProperty("receptor")]
        public Receptor receptor { get; set; }

        [JsonProperty("comprobanteXml")]
        public string comprobanteXml { get; set; }

        [JsonProperty("consecutivoReceptor")]
        public string consecutivoReceptor { get; set; }
    }

    public class Emisor
    {
        [JsonProperty("TipoIdentificacion")]
        public string TipoIdentificacion { get; set; }

        [JsonProperty("numeroIdentificacion")]
        public string numeroIdentificacion { get; set; }
    }

    public class Receptor
    {
        [JsonProperty("TipoIdentificacion")]
        public string TipoIdentificacion { get; set; }

        [JsonProperty("numeroIdentificacion")]
        public string numeroIdentificacion { get; set; }

        public Boolean sinReceptor { get; set; }
    }
}

