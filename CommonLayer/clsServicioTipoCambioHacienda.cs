using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CommonLayer
{
    public class ServicioTipoCambioHacienda
    {
        private const string UrlTipoCambio =
            "https://api.hacienda.go.cr/indicadores/tc/dolar";

        private static readonly HttpClient httpClient = new HttpClient
        {
            Timeout = TimeSpan.FromSeconds(20)
        };

        public async Task<decimal?> ObtenerTipoCambioVentaAsync(
            CancellationToken cancellationToken = default)
        {
            using (HttpResponseMessage response =
                   await httpClient.GetAsync(
                       UrlTipoCambio,
                       cancellationToken))
            {
                response.EnsureSuccessStatusCode();

                string contenido =
                    await response.Content.ReadAsStringAsync();

                return ExtraerVenta(contenido);
            }
        }

        private static decimal? ExtraerVenta(string contenido)
        {
            if (string.IsNullOrWhiteSpace(contenido))
                return null;

            return JObject.Parse(contenido)
                          .SelectToken("venta.valor")
                          ?.Value<decimal>();
        }
    }
}
