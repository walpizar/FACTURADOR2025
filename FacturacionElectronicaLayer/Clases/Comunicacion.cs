// FirmaElectronicaCR es un programa para la firma y envio de documentos XML para la Factura Electrónica de Costa Rica
//
// Comunicacion es la clase para el envío del documento XML para la Factura Electrónica de Costa Rica
//
// Esta clase de Firma fue realizado tomando como base el trabajo realizado por:
// - Departamento de Nuevas Tecnologías - Dirección General de Urbanismo Ayuntamiento de Cartagena
// - XAdES Starter Kit desarrollado por Microsoft Francia
// - Cambios y funcionalidad para Costa Rica - Roy Rojas - royrojas@dotnetcr.com
//
// La clase comunicación fue creada en conjunto con Cristhian Sancho
//
// Este programa es software libre: puede redistribuirlo y / o modificarlo
// bajo los + términos de la Licencia Pública General Reducida de GNU publicada por
// la Free Software Foundation, ya sea la versión 3 de la licencia, o
// (a su opción) cualquier versión posterior.C:\Users\walpi\Desktop\SISSOD INA 12-2018 FINAL\SisSodIna\FacturacionElectronicaLayer\Clases\Comunicacion.cs
//
// Este programa se distribuye con la esperanza de que sea útil,
// pero SIN NINGUNA GARANTÍA; sin siquiera la garantía implícita de
// COMERCIABILIDAD O IDONEIDAD PARA UN PROPÓSITO PARTICULAR. 
// Licencia pública general menor de GNU para más detalles.
//
// Deberías haber recibido una copia de la Licencia Pública General Reducida de GNU
// junto con este programa.Si no, vea http://www.gnu.org/licenses/.
//
// This program Is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY Or FITNESS FOR A PARTICULAR PURPOSE.See the
// GNU Lesser General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public License
// along with this program.If Not, see http://www.gnu.org/licenses/. 


using CommonLayer;
using CommonLayer.DTO;
using CommonLayer.Exceptions.BussinessExceptions;
using CommonLayer.Logs;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Policy;
using System.Threading.Tasks;
using System.Xml;

namespace FacturacionElectronicaLayer.Clases
{
    public class Comunicacion
    {
        private string URL = string.Empty;


        public Comunicacion()
        {

            URL = Utility.URL_RECEPCION((bool)Global.Usuario.tbEmpresa.ambientePruebas);


        }

        public XmlDocument xmlRespuesta { get; set; }
        public string jsonEnvio { get; set; }
        public string jsonRespuesta { get; set; }
        public string mensajeRespuesta { get; set; }
        public string estadoFactura { get; set; }
        public string statusCode { get; set; }
        public string estadoEnvio { get; set; }
        public bool existeRespuesta { get; set; }
        public string xmlCodificado { get; set; }

        public bool yaRecibidoPreviamente { get; private set; } = false;

        public RespuestaExoneracion exoneracion { get; set; }
        public async Task<String> EnvioDatos(string TK, Recepcion objRecepcion)
        {
            try
            {
                String URL_RECEPCION = URL;



                HttpClient http = new HttpClient();

                Newtonsoft.Json.Linq.JObject JsonObject = new Newtonsoft.Json.Linq.JObject();
                JsonObject.Add(new Newtonsoft.Json.Linq.JProperty("clave", objRecepcion.clave));
                JsonObject.Add(new JProperty("fecha", objRecepcion.fecha));
                JsonObject.Add(new JProperty("emisor",
                                             new JObject(new JProperty("tipoIdentificacion", objRecepcion.emisor.TipoIdentificacion),
                                                         new JProperty("numeroIdentificacion", objRecepcion.emisor.numeroIdentificacion.Trim()))));

                if (objRecepcion.receptor.sinReceptor == false)
                {
                    JsonObject.Add(new JProperty("receptor",
                                             new JObject(new JProperty("tipoIdentificacion", objRecepcion.receptor.TipoIdentificacion),
                                                         new JProperty("numeroIdentificacion", objRecepcion.receptor.numeroIdentificacion))));
                }

                JsonObject.Add(new JProperty("comprobanteXml", objRecepcion.comprobanteXml));

                jsonEnvio = JsonObject.ToString();

                StringContent oString = new StringContent(JsonObject.ToString());

                http.DefaultRequestHeaders.Add("authorization", ("Bearer " + TK));

                HttpResponseMessage response = http.PostAsync((URL_RECEPCION + "recepcion"), oString).Result;
                string res = await response.Content.ReadAsStringAsync();

                object Localizacion = response.StatusCode;
                // mensajeRespuesta = Localizacion
                estadoEnvio = response.StatusCode.ToString();


                //http = new HttpClient();
                //http.DefaultRequestHeaders.Add("authorization", ("Bearer " + TK));
                //response = http.GetAsync((URL_RECEPCION + ("recepcion/" + objRecepcion.clave))).Result;
                //res = await response.Content.ReadAsStringAsync();

                //jsonRespuesta = res.ToString();

                //RespuestaHacienda RH = Newtonsoft.Json.JsonConvert.DeserializeObject<RespuestaHacienda>(res);
                //if (RH !=null)
                //{
                //    existeRespuesta = true;
                //    if ((RH.respuesta_xml != null ))
                //    {
                //        xmlRespuesta = Funciones.DecodeBase64ToXML(RH.respuesta_xml);
                //    }
                //    estadoFactura = RH.ind_estado;

                //}
                //else
                //{

                //    existeRespuesta = false;
                //}

                //mensajeRespuesta = estadoFactura;
                statusCode = response.StatusCode.ToString();
                //cerrar sesion

                return statusCode;
            }
            catch (Exception ex)
            {
               
                throw new FacturacionElectronicaException(ex);
            }
        }


        // ============================================================
        // Reemplaza el método EnvioMensaje existente. Requiere que la clase
        // ya tenga declarados los campos: jsonEnvio, estadoEnvio, mensajeRespuesta,
        // statusCode, jsonRespuesta (ya existían, algunos comentados en el original).
        // Agregar si falta: using System.Net.Http.Headers;
        // ============================================================

        // ============================================================
        // Reemplaza el método EnvioMensaje existente. Requiere que la clase
        // ya tenga declarados los campos: jsonEnvio, estadoEnvio, mensajeRespuesta,
        // statusCode, jsonRespuesta (ya existían, algunos comentados en el original).
        // Agregar si falta: using System.Net.Http.Headers; using System.Linq;
        // ============================================================

        // ============================================================
        // Reemplaza el método EnvioMensaje existente. Requiere que la clase
        // ya tenga declarados los campos: jsonEnvio, estadoEnvio, mensajeRespuesta,
        // statusCode, jsonRespuesta (ya existían, algunos comentados en el original).
        // Agregar si falta: using System.Net.Http.Headers; using System.Linq;
        //
        // Agregar también esta propiedad nueva a la clase (junto a jsonEnvio, etc.):
        //
        //   /// <summary>
        //   /// true cuando Hacienda respondió con 400 pero el motivo (x-error-cause)
        //   /// indica que el comprobante YA HABÍA SIDO RECIBIDO antes — en ese caso
        //   /// no es un error real: Hacienda ya tiene el documento, solo hace falta
        //   /// consultar el estado en vez de reintentar el envío.
        //   /// </summary>
        //   public bool yaRecibidoPreviamente { get; private set; } = false;
        // ============================================================

        // ============================================================
        // Reemplaza el método EnvioMensaje existente Y cambia su firma:
        // antes devolvía Task<string>, ahora devuelve Task<ResultadoEnvioHacienda>.
        //
        // Como cambia la firma, hay que actualizar también a quien lo llama
        // (enviarMensajeHacienda) — ver el archivo aparte con esa actualización.
        //
        // Requiere: using CommonLayer.DTO; using System.Net.Http.Headers;
        //           using System.Linq;
        //
        // Las propiedades jsonEnvio/estadoEnvio/mensajeRespuesta/statusCode/
        // jsonRespuesta/yaRecibidoPreviamente de la clase se pueden dejar (por si
        // otro código viejo las lee) o eliminar — ya no son necesarias porque
        // todo viaja en el objeto de retorno. Acá se dejan de todas formas
        // asignadas, por compatibilidad con el resto de la clase.
        // ============================================================

        public async Task<ResultadoEnvioHacienda> EnvioMensaje(string TK, RecepcionMensaje objRecepcion)
        {
            try
            {
                string URL_RECEPCION = URL;

                Newtonsoft.Json.Linq.JObject JsonObject = new Newtonsoft.Json.Linq.JObject();
                JsonObject.Add(new JProperty("clave", objRecepcion.clave));
                JsonObject.Add(new JProperty("fecha", objRecepcion.fecha));
                JsonObject.Add(new JProperty("emisor",
                    new JObject(
                        new JProperty("tipoIdentificacion", objRecepcion.emisor.TipoIdentificacion),
                        new JProperty("numeroIdentificacion", objRecepcion.emisor.numeroIdentificacion.Trim()))));

                if (objRecepcion.receptor.sinReceptor == false)
                {
                    JsonObject.Add(new JProperty("receptor",
                        new JObject(
                            new JProperty("tipoIdentificacion", objRecepcion.receptor.TipoIdentificacion),
                            new JProperty("numeroIdentificacion", objRecepcion.receptor.numeroIdentificacion))));
                }

                JsonObject.Add(new JProperty("consecutivoReceptor", objRecepcion.consecutivoReceptor));
                JsonObject.Add(new JProperty("comprobanteXml", objRecepcion.comprobanteXml));

                string jsonEnvioLocal = JsonObject.ToString();
                jsonEnvio = jsonEnvioLocal; // compatibilidad con código que lea esta propiedad

                // Content-Type explícito: la API de Hacienda espera application/json.
                var oString = new StringContent(jsonEnvioLocal, System.Text.Encoding.UTF8, "application/json");

                using (var http = new HttpClient())
                {
                    http.DefaultRequestHeaders.Authorization =
                        new AuthenticationHeaderValue("Bearer", TK);

                    HttpResponseMessage response;
                    try
                    {
                        // await en vez de .Result: evita el riesgo de deadlock en el
                        // hilo de UI de WinForms cuando se bloquea sobre una Task async.
                        response = await http.PostAsync(URL_RECEPCION + "recepcion", oString);
                    }
                    catch (HttpRequestException ex)
                    {
                        // Error de red/conexión: no hubo respuesta de Hacienda en absoluto
                        // (timeout, DNS, sin internet, etc.) — se distingue de un rechazo
                        // real (400/401/etc.), donde Hacienda SÍ respondió.
                        var resultadoRed = new ResultadoEnvioHacienda
                        {
                            Exitoso = false,
                            YaRecibidoPreviamente = false,
                            StatusCode = "SinConexion",
                            Mensaje = "No se pudo contactar a Hacienda: " + ex.Message,
                            JsonEnvio = jsonEnvioLocal,
                            JsonRespuesta = null
                        };

                        estadoEnvio = resultadoRed.StatusCode;
                        mensajeRespuesta = resultadoRed.Mensaje;
                        statusCode = resultadoRed.StatusCode;
                        yaRecibidoPreviamente = false;

                        return resultadoRed;
                    }

                    string res = await response.Content.ReadAsStringAsync();
                    jsonRespuesta = res;

                    string statusCodeLocal = response.StatusCode.ToString();
                    statusCode = statusCodeLocal;
                    estadoEnvio = statusCodeLocal;

                    // Hacienda a veces manda el motivo real del rechazo en el header
                    // "x-error-cause" (ej. "El comprobante [...] ya fue recibido
                    // anteriormente."), no en el cuerpo.
                    string errorCause = null;
                    if (response.Headers.TryGetValues("x-error-cause", out var valoresErrorCause))
                    {
                        errorCause = valoresErrorCause.FirstOrDefault();
                    }

                    // Caso especial: "ya fue recibido anteriormente" NO es un error
                    // real de negocio — Hacienda ya tiene el documento.
                    bool yaRecibidoLocal = !string.IsNullOrWhiteSpace(errorCause) &&
                        errorCause.IndexOf("recibido anteriormente", StringComparison.OrdinalIgnoreCase) >= 0;
                    yaRecibidoPreviamente = yaRecibidoLocal;

                    string mensajeLocal;

                    if (response.IsSuccessStatusCode && response.Headers.Location != null)
                    {
                        // Éxito normal: el header Location trae la URL de consulta.
                        mensajeLocal = response.Headers.Location.ToString();
                    }
                    else if (!string.IsNullOrWhiteSpace(errorCause))
                    {
                        // Prioridad 1: el motivo explícito que da Hacienda (incluye
                        // tanto "ya recibido" como cualquier otro rechazo real).
                        mensajeLocal = errorCause;
                    }
                    else
                    {
                        // Prioridad 2: el cuerpo de la respuesta como último recurso.
                        mensajeLocal = string.IsNullOrWhiteSpace(res)
                            ? $"Hacienda respondió {statusCodeLocal} sin detalle adicional."
                            : res;
                    }

                    mensajeRespuesta = mensajeLocal;

                    var resultado = new ResultadoEnvioHacienda
                    {
                        Exitoso = response.IsSuccessStatusCode || yaRecibidoLocal,
                        YaRecibidoPreviamente = yaRecibidoLocal,
                        StatusCode = statusCodeLocal,
                        Mensaje = mensajeLocal,
                        JsonEnvio = jsonEnvioLocal,
                        JsonRespuesta = res
                    };

                    return resultado;
                }
            }
            catch (Exception ex)
            {
                throw new FacturacionElectronicaException(ex);
            }
        }
        public async Task<string> ConsultarFacturaPorClaveAsync(string clave, string token)
        {
            try
            {
                // URL of the endpoint to send the request to
                string URL_RECEPCION = URL;

                // Create a new HttpClient instance within a using statement for proper disposal
                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                    try
                    {
                        HttpResponseMessage response = await httpClient.GetAsync(URL_RECEPCION);

                        if (response.IsSuccessStatusCode)
                        {
                            // La respuesta viene en formato JSON
                            string responseBody = await response.Content.ReadAsStringAsync();
                            return responseBody;
                        }
                        else
                        {
                            string errorContent = await response.Content.ReadAsStringAsync();
                            throw new Exception($"Error al consultar la clave: {response.StatusCode} - {errorContent}");
                        }
                    }
                    catch (Exception ex)
                    {
                        // Aquí podrías loguear con clsEvento o manejar el error según tu lógica
                        throw new Exception("Error consultando la factura por clave", ex);
                    }
                }
            }

            
            catch (HttpRequestException httpEx)
            {
                // Handle HTTP-specific exceptions
                return this.mensajeRespuesta = "error";
                //throw new RespuestaHaciendaException(httpEx);
            }
            catch (Exception ex)
            {
                // Handle general exceptions
                return this.mensajeRespuesta = "error";
                //throw new RespuestaHaciendaException(ex);
            }
            return mensajeRespuesta;


        }


        public async void CerrarSesion(string TK)
        {
            // URL del endpoint para cerrar sesión
            string url = "https://idp.comprobanteselectronicos.go.cr/auth/realms/rut-stag/protocol/openid-connect/logout";

            // Datos del formulario
            var formData = new FormUrlEncodedContent(new[]
            {
            new KeyValuePair<string, string>("client_id", "api-stag"),
            new KeyValuePair<string, string>("refresh_token", TK)
        });

            // Configurar el cliente HTTP
            using (var client = new HttpClient())
            {
                try
                {
                    // Realizar la solicitud POST y obtener la respuesta
                    HttpResponseMessage response = await client.PostAsync(url, formData);

                    // Verificar si la solicitud fue exitosa
                    if (response.IsSuccessStatusCode)
                    {
                        // Leer la respuesta (opcional)
                        string responseBody = await response.Content.ReadAsStringAsync();
                        Console.WriteLine("Sesión cerrada exitosamente.");
                        Console.WriteLine(responseBody);
                    }
                    else
                    {
                        Console.WriteLine($"La solicitud no fue exitosa. Código de estado: {response.StatusCode}");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Ha ocurrido un error: {e.Message}");
                }
            }
        }


        public async void ConsultaExoneracionXAutorizacion(string autorizacion)
        {
            try
            {


                String URL_RECEPCION = "https://api.hacienda.go.cr/fe/ex?autorizacion=";

                HttpClient http = new HttpClient();
              

                HttpResponseMessage response = http.GetAsync((URL_RECEPCION + (autorizacion))).Result;

                string res = await response.Content.ReadAsStringAsync();

                RespuestaExoneracion RH = Newtonsoft.Json.JsonConvert.DeserializeObject<RespuestaExoneracion>(res);

                //si el numero de doc es null es que no encontro por lo tanto pone exoneracino null
                exoneracion = RH.numeroDocumento==null? null : RH;
               

            }
            catch (Exception ex)
            {
          
                throw new RespuestaHaciendaException(ex);
            }
        }

        public async Task<String> ConsultaEstatusComprobante(string TK, string claveConsultar)
        {
            try
            {
                // URL of the endpoint to send the request to
                string URL_RECEPCION = URL;

                // Create a new HttpClient instance within a using statement for proper disposal
                using (HttpClient http = new HttpClient { Timeout = TimeSpan.FromSeconds(5) })
                {
                    // Add the authorization header with the Bearer token
                    http.DefaultRequestHeaders.Add("Authorization", "Bearer " + TK);

                    // Send a GET request asynchronously and wait for the response
                    HttpResponseMessage response = http.GetAsync(URL_RECEPCION + ("recepcion/" + claveConsultar)).Result;



                    // Ensure the response indicates success
                    response.EnsureSuccessStatusCode();

                    // Read the response content as a string
                    string res = await response.Content.ReadAsStringAsync();

                    // Get the status code of the response
                    object Localizacion = response.StatusCode;

                    jsonRespuesta = res.ToString();
                    RespuestaHacienda RH = Newtonsoft.Json.JsonConvert.DeserializeObject<RespuestaHacienda>(res);
                    if (RH != null)
                    {

                        if ((RH.respuesta_xml != "" && RH.respuesta_xml != null))
                        {
                            xmlCodificado = RH.respuesta_xml;
                            xmlRespuesta = Utility.DecodeBase64ToXML(RH.respuesta_xml);
                        }
                        estadoFactura = RH.ind_estado;

                    }



                    statusCode = response.StatusCode.ToString();
                    mensajeRespuesta = ("Confirmación: " + (statusCode + "\r\n"));
                    mensajeRespuesta = (mensajeRespuesta + ("Estado: " + estadoFactura));
                }

            }
            catch (HttpRequestException httpEx)
            {
                // Handle HTTP-specific exceptions
                return this.mensajeRespuesta = "error";
                //throw new RespuestaHaciendaException(httpEx);
            }
            catch (Exception ex)
            {
                // Handle general exceptions
                return this.mensajeRespuesta = "error";
                //throw new RespuestaHaciendaException(ex);
            }
            return mensajeRespuesta;

        }




        public async void ConsultaEstatusMensajes(string TK, string url)
        {
            try
            {

                HttpClient http = new HttpClient();

                http.DefaultRequestHeaders.Add("authorization", ("Bearer " + TK));

                HttpResponseMessage response = http.GetAsync(url).Result;
                string res = await response.Content.ReadAsStringAsync();

                object Localizacion = response.StatusCode;

                jsonRespuesta = res.ToString();
                RespuestaHacienda RH = Newtonsoft.Json.JsonConvert.DeserializeObject<RespuestaHacienda>(res);
                if (RH != null)
                {

                    if ((RH.respuesta_xml != "" && RH.respuesta_xml != null))
                    {
                        xmlCodificado = RH.respuesta_xml;
                        xmlRespuesta = Utility.DecodeBase64ToXML(RH.respuesta_xml);
                    }
                    estadoFactura = RH.ind_estado;

                }

                statusCode = response.StatusCode.ToString();
                mensajeRespuesta = ("Confirmación: " + (statusCode + "\r\n"));
                mensajeRespuesta = (mensajeRespuesta + ("Estado: " + estadoFactura));
            }
            catch (Exception ex)
            {
               // clsEvento evento = new clsEvento(ex.Message, "1");
                throw new RespuestaHaciendaException(ex);
            }
        }

        // ============================================================
        // Agregar este método a la clase Comunicacion (junto a EnvioMensaje).
        // Reutiliza la clase RespuestaHacienda que ya tenías referenciada
        // (comentada) dentro de enviarMensajeHacienda, con las propiedades
        // ind_estado y respuesta_xml.
        // ============================================================

        /// <summary>
        /// Consulta en Hacienda el estado de una respuesta/Mensaje Receptor ya
        /// enviado. La clave de consulta para un Mensaje Receptor NO es la clave
        /// del documento original sola: es "{clave}-{consecutivoReceptor}".
        /// </summary>
        /// <param name="TK">Token vigente.</param>
        /// <param name="clave">Clave del documento original (50 dígitos).</param>
        /// <param name="consecutivoReceptor">
        /// Consecutivo que usaste al enviar la confirmación (el mismo que mandaste
        /// en el campo "consecutivoReceptor" del envío original).
        /// </param>
        public async Task<RespuestaHacienda> ConsultarMensaje(string TK, string clave, string consecutivoReceptor)
        {
            try
            {
                string URL_RECEPCION = URL;
                string claveConsulta = string.IsNullOrWhiteSpace(consecutivoReceptor)
                    ? clave.Trim()
                    : $"{clave.Trim()}-{consecutivoReceptor.Trim()}";

                using (var http = new HttpClient())
                {
                    http.DefaultRequestHeaders.Authorization =
                        new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", TK);

                    HttpResponseMessage response = await http.GetAsync(URL_RECEPCION + "recepcion/" + claveConsulta);

                    string res = await response.Content.ReadAsStringAsync();
                    jsonRespuesta = res;

                    if (!response.IsSuccessStatusCode)
                    {
                        // 404 = todavía no hay respuesta de Hacienda (sigue "Recibido"/procesando).
                        // Cualquier otro código de error se refleja igual en jsonRespuesta para diagnóstico.
                        return null;
                    }

                    RespuestaHacienda RH = Newtonsoft.Json.JsonConvert.DeserializeObject<RespuestaHacienda>(res);

                    if (RH != null && RH.respuesta_xml != null)
                    {
                        xmlRespuesta = Utility.DecodeBase64ToXML(RH.respuesta_xml);
                    }

                    return RH;
                }
            }
            catch (Exception ex)
            {
                throw new FacturacionElectronicaException(ex);
            }
        }



    }
}
