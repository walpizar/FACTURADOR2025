using CommonLayer.Logs;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CommonLayer.Licencia
{
    public static class Licencias
    {

        public static async void GetLicencia(string idEmpresa)
        {
            try
            {

                String URL_RECEPCION = "https://applicencia.herokuapp.com/";

                HttpClient http = new HttpClient();

                HttpResponseMessage response = http.GetAsync((URL_RECEPCION + ("empresa/licencia/" + idEmpresa))).Result;

                string res = await response.Content.ReadAsStringAsync();

                object Localizacion = response.StatusCode;

                ResponseLicencias lic = JsonConvert.DeserializeObject<ResponseLicencias>(res);
                Global.licencia = lic;
                Global.licenciaWeb = true;
            }
            catch (Exception ex)
            {
                Global.licenciaWeb = false;
                clsEvento evento = new clsEvento("Obtener Licencias API " + ex.Message, "1");
                Global.licencia = null;

            }

        }
    }
}
