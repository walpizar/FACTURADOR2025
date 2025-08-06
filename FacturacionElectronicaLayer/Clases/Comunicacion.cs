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
using CommonLayer.Exceptions.BussinessExceptions;
using CommonLayer.Logs;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
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


        public async Task<String> EnvioMensaje(string TK, RecepcionMensaje objRecepcion)
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

                JsonObject.Add(new JProperty("consecutivoReceptor", objRecepcion.consecutivoReceptor));
                JsonObject.Add(new JProperty("comprobanteXml", objRecepcion.comprobanteXml));



                jsonEnvio = JsonObject.ToString();

                StringContent oString = new StringContent(JsonObject.ToString());

                http.DefaultRequestHeaders.Add("authorization", ("Bearer " + TK));

                HttpResponseMessage response = http.PostAsync((URL_RECEPCION + "recepcion"), oString).Result;
                string res = await response.Content.ReadAsStringAsync();

                object Localizacion = response.StatusCode;
                // mensajeRespuesta = Localizacion
                estadoEnvio = response.StatusCode.ToString();
                mensajeRespuesta = response.Headers.Location.ToString();

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
                return statusCode;
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


       
    }
}
