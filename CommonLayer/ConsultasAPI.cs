using CommonLayer.DTO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CommonLayer
{
    public static class ConsultasAPI
    {

        public static async Task<bool> ConsultaCABYS(string code)
        {
            try
            {
                HttpClient http = new HttpClient();

                HttpResponseMessage response = http.GetAsync("https://api.hacienda.go.cr/fe/cabys?codigo=" + code).Result;

                string res = await response.Content.ReadAsStringAsync();

                var cabysResp = Newtonsoft.Json.JsonConvert.DeserializeObject<CabysDTO[]>(res);
                if (cabysResp.Length == 0)
                {
                    return false;
                }


                return true;

            }
            catch (Exception ex)
            {
                return true;
            }
        }

        public static async Task<decimal> CambioDolar()
        {
            try
            {

                HttpClient http = new HttpClient();

                HttpResponseMessage response = http.GetAsync("https://api.hacienda.go.cr/indicadores/tc/dolar").Result;

                string res = await response.Content.ReadAsStringAsync();

                var valorDolar = Newtonsoft.Json.JsonConvert.DeserializeObject<DolarDto>(res);
                if (valorDolar != null)
                {
                    return valorDolar.venta.valor;
                }


                return 0;

            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        public static async Task<List<CabysDTO>> obtenerCABYS(string busqueda)
        {
            string url;

        
            // Si la búsqueda es un número (código CABYS), se usa el parámetro 'codigo'
            if (Regex.IsMatch(busqueda, @"^\d+$"))
            {
                url = $"https://api.hacienda.go.cr/fe/cabys?codigo={busqueda}";
            }
            else
            {
                // Si no es un número, se busca por la descripción (parámetro 'q')
                url = $"https://api.hacienda.go.cr/fe/cabys?q={Uri.EscapeDataString(busqueda)}";
            }

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    client.Timeout = TimeSpan.FromSeconds(30);
                    HttpResponseMessage response = await client.GetAsync(url);

                    if (response.IsSuccessStatusCode)
                    {
                        string jsonResponse = await response.Content.ReadAsStringAsync();
                        CabysResponseDTO cabys = JsonConvert.DeserializeObject<CabysResponseDTO>(jsonResponse);

                        return cabys.Cabys;
                    }
                    else
                    {
                        Console.WriteLine($"Error: {response.StatusCode}");
                        return null;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Exception: " + e.Message);
                    return null;
                }
            }
        }

        public static async Task<ClienteResponseDTO> ObtenerCliente(string identificacion)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(identificacion))
                    return null;

                identificacion = identificacion.Trim();

                string url =
                    $"https://api.hacienda.go.cr/fe/ae?identificacion={identificacion}";

                using (HttpClient client = new HttpClient())
                {
                    client.Timeout = TimeSpan.FromSeconds(15);

                    HttpResponseMessage response = await client.GetAsync(url);

                    if (!response.IsSuccessStatusCode)
                        return null;

                    string jsonResponse =
                        await response.Content.ReadAsStringAsync();

                    if (string.IsNullOrWhiteSpace(jsonResponse))
                        return null;

                    return JsonConvert.DeserializeObject<ClienteResponseDTO>(
                        jsonResponse
                    );
                }
            }
            catch (TaskCanceledException)
            {
                // Timeout de Hacienda
                return null;
            }
            catch (HttpRequestException)
            {
                // Error de conexión
                return null;
            }
            catch (JsonException)
            {
                // Respuesta con formato inesperado
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
