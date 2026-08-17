using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace FacturacionElectronicaLayer.Clases
{
    /// <summary>
    /// Actividad económica tal como la devuelve el API público de Hacienda.
    /// IMPORTANTE: el "codigo" viene como número plano (ej. 751101), sin el
    /// formato con punto que pueda usar el catálogo local (tbActividades).
    /// Verificar con una consulta real si hace falta convertir el formato
    /// antes de intentar cruzarlo contra el catálogo local.
    /// </summary>
    public class HaciendaActividadDTO
    {
        [JsonProperty("estado")]
        public string Estado { get; set; }   // "A" = Activa, "I" = Inactiva

        [JsonProperty("codigo")]
        public string Codigo { get; set; }

        [JsonProperty("descripcion")]
        public string Descripcion { get; set; }
    }

    public class HaciendaRegimenDTO
    {
        [JsonProperty("codigo")]
        public int Codigo { get; set; }

        [JsonProperty("descripcion")]
        public string Descripcion { get; set; }
    }

    public class HaciendaContribuyenteDTO
    {
        [JsonProperty("nombre")]
        public string Nombre { get; set; }

        [JsonProperty("tipoIdentificacion")]
        public string TipoIdentificacion { get; set; }

        [JsonProperty("regimen")]
        public HaciendaRegimenDTO Regimen { get; set; }

        [JsonProperty("actividades")]
        public List<HaciendaActividadDTO> Actividades { get; set; }
    }

    public static class ConsultasHacienda
    {
        private const string BaseUrl = "https://api.hacienda.go.cr/fe/ae";

        /// <summary>
        /// Consulta las actividades económicas registradas en Hacienda para una identificación.
        /// Devuelve null si la identificación no existe o el servicio no respondió OK.
        /// </summary>
        public static async Task<HaciendaContribuyenteDTO> ObtenerActividadesAsync(string identificacion)
        {
            if (string.IsNullOrWhiteSpace(identificacion))
            {
                return null;
            }

            using (var http = new HttpClient())
            {
                http.Timeout = TimeSpan.FromSeconds(15);

                HttpResponseMessage response;
                try
                {
                    response = await http.GetAsync($"{BaseUrl}?identificacion={identificacion.Trim()}");
                }
                catch (Exception)
                {
                    // Sin conexión, timeout, DNS, etc.
                    return null;
                }

                if (!response.IsSuccessStatusCode)
                {
                    return null;
                }

                var json = await response.Content.ReadAsStringAsync();

                if (string.IsNullOrWhiteSpace(json))
                {
                    return null;
                }

                return JsonConvert.DeserializeObject<HaciendaContribuyenteDTO>(json);
            }
        }
    }
}
