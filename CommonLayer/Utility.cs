using MailKit;
using MailKit.Net.Imap;
using MailKit.Search;
using MimeKit;
using System;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml;

namespace CommonLayer
{
    public class Utility
    {
        public static void ObtenerCorreosConAdjuntosXml(string email, string password)
        {
            using (var client = new ImapClient())
            {
                try
                {
                    // Conectarse al servidor de Gmail IMAP
                    client.Connect("imap.gmail.com", 993, MailKit.Security.SecureSocketOptions.SslOnConnect);

                    // Autenticarse
                    client.Authenticate(email, password);

                    // Seleccionar la carpeta de entrada (INBOX)
                    client.Inbox.Open(FolderAccess.ReadWrite);

                    // Buscar correos no leídos
                    var uids = client.Inbox.Search(SearchQuery.NotSeen);

                    foreach (var uid in uids)
                    {
                        var message = client.Inbox.GetMessage(uid);

                        // Filtrar correos que tienen archivos adjuntos XML
                        foreach (var attachment in message.Attachments)
                        {
                            if (attachment is MimePart mimePart && mimePart.FileName.EndsWith(".xml", StringComparison.OrdinalIgnoreCase))
                            {
                                // Guardar el archivo adjunto XML
                                string filePath = Path.Combine("Ruta/Para/Guardar", mimePart.FileName);

                                using (var stream = File.Create(filePath))
                                {
                                    mimePart.Content.DecodeTo(stream);
                                }

                                Console.WriteLine($"Archivo guardado: {filePath}");
                            }
                        }

                        // Marcar el mensaje como leído
                        //client.Inbox.AddFlags(uid, MessageFlags.Seen, true);
                    }

                    // Desconectarse del cliente
                    client.Disconnect(true);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
        }
        public static (string nombre, string apellido1, string apellido2) ObtenerNombreYApellidos(string nombreCompleto)
        {
            // Separar el nombre completo en partes usando Split
            string[] partes = nombreCompleto.Split(' ');

            string nombre = string.Empty;
            string apellido1 = string.Empty;
            string apellido2 = string.Empty;

            // Valorar la cantidad de partes
            if (partes.Length == 2)
            {
                // 2 partes: El primero es el nombre, el segundo es apellido1 
                nombre = partes[0];
                apellido1 = partes[1];
                apellido2 = "";
            }
            else if (partes.Length == 3)
            {
                // 3 partes: El primero es el nombre, el segundo es apellido1 y el tercero apellido2
                nombre = partes[0];
                apellido1 = partes[1];
                apellido2 = partes[2];
            }
            else if (partes.Length == 4)
            {
                // 4 partes: Los primeros dos son los nombres, los otros dos son los apellidos
                nombre = partes[0] + " " + partes[1];
                apellido1 = partes[2];
                apellido2 = partes[3];
            }
            else if (partes.Length >= 5)
            {
                // 5 o más partes: Los primeros "n-2" son el nombre, los dos últimos son los apellidos
                for (int i = 0; i < partes.Length - 2; i++)
                {
                    nombre += partes[i] + " ";
                }
                nombre = nombre.Trim(); // Eliminar el espacio extra al final
                apellido1 = partes[partes.Length - 2];
                apellido2 = partes[partes.Length - 1];
            }

            return (nombre, apellido1, apellido2);
        }
        public static string DoFormat(decimal myNumber)
        {
            return string.Format("{0:0.00}", myNumber).Replace(".00", "");
        }
        public static string TruncFormat(decimal value)
        {
            decimal truncated = Math.Truncate(value * 100) / 100; // corta a 2 decimales
            return (truncated % 1 == 0)
                ? ((int)truncated).ToString()
                : truncated.ToString("0.##", System.Globalization.CultureInfo.InvariantCulture);
        }
        public static string priceFormat(decimal myNumber)
        {
            CultureInfo culture1 = CultureInfo.CurrentCulture;
            NumberFormatInfo formato = culture1.NumberFormat;
            return myNumber.ToString("N", formato);

        }
        //public byte[] ImageToByteArray(System.Drawing.Image imageIn)
        //{
        //    using (var ms = new MemoryStream())
        //    {
        //        imageIn.Save(ms, imageIn.RawFormat);
        //        return ms.ToArray();
        //    }
        //}

        public static bool AccesoInternet()
        {

            try
            {
                System.Net.IPHostEntry host = System.Net.Dns.GetHostEntry("www.google.com");
                return true;

            }
            catch (Exception es)
            {

                return false;
            }

        }
        public static string stringConexionReportes()
        {

            //INA
            return Global.conexionReportes;

            //CHARLY SANTA ROSA
            //  return "data source=.;initial catalog=dbSISSODINA;user id=sa;password=crpp;";


            //return System.Configuration.ConfigurationManager.
            //       ConnectionStrings["dbSisSodInaEntities"].ConnectionString;

            //MI PC
            //return "data source=.;initial catalog=dbSISSODINA;user id=sa;password=crpp;"

            //MERY
            //return "data source=DESKTOP-R0MNKF5\\MSSQLSERVER01;initial catalog=dbSISSODINA;user id=sa;password=crpp;"


            // juan jose
            //return "data source=localhost\\SQLEXPRESS01;initial catalog=dbSISSODINA;user id=sa;password=crpp;";


            // local -- SODA PARADA GUATUSO-ropa --ferreteria-- ferreteria nueva-- GRANERO
            // return "data source=localhost\\SQLEXPRESS;initial catalog=dbSISSODINA;user id=sa;password=crpp;";

            //POLLOS
            //return "data source=localhost\\MSSQLSERVER3;initial catalog=dbSISSODINA;user id=sa;password=crpp;"


            //return "data source=.;initial catalog=dbSISSODINA;user id=sa;password=crpp;";

            //local ferreteria clientes
            // return "data source=192.168.100.7;initial catalog=dbSISSODINA;user id=sa;password=crpp;";


        }
        public static XmlDocument DecodeBase64ToXML(string valor)
        {
            byte[] myBase64ret = Convert.FromBase64String(valor);
            string myStr = System.Text.Encoding.UTF8.GetString(myBase64ret);
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(myStr);
            return xmlDoc;
        }
        public static Image ByteArrayToImage(byte[] data)
        {
            MemoryStream ms = new MemoryStream(data);
            Image returnImage = Image.FromStream(ms);
            return returnImage;
        }

        public static string DecodeBase64ToString(string valor)
        {
            byte[] myBase64ret = Convert.FromBase64String(valor);
            string myStr = System.Text.Encoding.UTF8.GetString(myBase64ret);
            return myStr;
        }
        public static string DecodeBase64ImageToStrig(Image image)
        {
            byte[] myBase64ret = ImageToByteArray(image);
            string myStr = System.Text.Encoding.UTF8.GetString(myBase64ret);
            return myStr;
        }
  


        public static string EncodeStrToBase64(string valor)
        {
            byte[] myByte = System.Text.Encoding.UTF8.GetBytes(valor);
            string myBase64 = Convert.ToBase64String(myByte);
            return myBase64;
        }

  
        //public static string GetConnectionString()
        //{
        //    string model = "dbSisSodInaModel";
        //    string dataSource = "";
        //    if (Global.Configuracion.instance==null)
        //    {
        //        dataSource = Global.Configuracion.server;
        //    }
        //    else
        //    {
        //        dataSource = string.Format(@"{0}\{1}", Global.Configuracion.server, Global.Configuracion.instance);
        //    }

        //    // Build the provider connection string with configurable settings
        //    var providerSB = new SqlConnectionStringBuilder
        //    {
        //        InitialCatalog = Global.Configuracion.dataBase,
        //        DataSource = dataSource,
        //        UserID = Global.Configuracion.user,
        //        Password = Global.Configuracion.password
        //    };

        //    var efConnection = new EntityConnectionStringBuilder();
        //    // or the config file based connection without provider connection string
        //    // var efConnection = new EntityConnectionStringBuilder(@"metadata=res://*/model1.csdl|res://*/model1.ssdl|res://*/model1.msl;provider=System.Data.SqlClient;");
        //    efConnection.Provider = "System.Data.SqlClient";
        //    efConnection.ProviderConnectionString = providerSB.ConnectionString;
        //    // based on whether you choose to supply the app.config connection string to the constructor
        //    efConnection.Metadata = string.Format("res://*/Model.{0}.csdl|res://*/Model.{0}.ssdl|res://*/Model.{0}.msl", model); ;
        //    return efConnection.ToString();

        //}


        public static DateTime getDate()
        {
            return DateTime.Now;
        }
        public static DateTime GetDateByDay()
        {
            return DateTime.Today;
        }

        public static byte[] UrlImageToByteArray(string imagenUrl)
        {
            byte[] imageByte = null;
            FileStream fs = new FileStream(imagenUrl, FileMode.Open, FileAccess.Read);
            BinaryReader br = new BinaryReader(fs);
            imageByte = new byte[fs.Length];
            br.Read(imageByte, 0, (int)fs.Length);
            br.Close();
            fs.Close();
            return imageByte;
        }

        public static byte[] ImageToByteArray(Image x)
        {
            ImageConverter _imageConverter = new ImageConverter();
            byte[] xByte = (byte[])_imageConverter.ConvertTo(x, typeof(byte[]));
            return xByte;
        }

        public static Image stringToImage(string inputString)
        {
            byte[] imageBytes = Convert.FromBase64String(inputString);
            MemoryStream ms = new MemoryStream(imageBytes);

            Image image = Image.FromStream(ms, true, true);

            return image;
        }



        public static bool isValidEmail(string inputEmail)
        {
            string strRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                  @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                  @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
            Regex re = new Regex(strRegex);
            if (re.IsMatch(inputEmail.Trim()))
                return (true);
            else
                return (false);
        }

        public static bool isNumeroDecimal(string valor)
        {
            return isNumeroDecimal(valor, true);
        }
        public static bool isNumeroDecimal(string valor, bool validCero)
        {
            decimal res = 0;
            if (valor != string.Empty)
            {
                decimal.TryParse(valor, out res);
                if (validCero)
                {
                    if (res != 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;

                    }

                }
                else
                {
                    return true;
                }


            }

            return false;

        }

        public static bool isNumerInt(string valor)
        {
            int res = 0;
            if (valor != string.Empty)
            {

                if (int.TryParse(valor, out res))
                {
                    return true;
                }
                else
                {
                    return false;

                }

            }
            return false;
        }
        public static string getPrefixTypeDoc(int tipo)
        {
            string nome = string.Empty;
            if (tipo == (int)Enums.TipoDocumento.NotaCreditoElectronica)
            {
                nome = "_NC";
            }
            else if (tipo == (int)Enums.TipoDocumento.NotaDebitoElectronica)
            {

                nome = "_ND";
            }
            else if (tipo == (int)Enums.TipoDocumento.FacturaElectronica)
            {

                nome = "_FE";
            }
            else if (tipo == (int)Enums.TipoDocumento.TiqueteElectronico)
            {

                nome = "_TE";
            }
            else if (tipo == (int)Enums.TipoDocumento.Proforma)
            {

                nome = "_PROFORMA";
            }
            else if (tipo == (int)Enums.TipoDocumento.ComprasSimplificada)
            {

                nome = "_CS";
            }
            else if (tipo == (int)Enums.TipoDocumento.OrdenCompra)
            {

                nome = "_OC";
            }
            return nome;


        }
        /// <summary>
        /// Habilitamos y deshabilitamos el formulario.
        /// </summary>
        /// <param name="gbx"></param>
        /// <param name="estado"></param>
        public static void EnableDisableForm(ref GroupBox gbx, bool estado)
        {


            foreach (Object obj in gbx.Controls)
            {

                if (obj is TextBox)
                {

                    ((TextBox)obj).Enabled = estado;

                }
                if (obj is MaskedTextBox)
                {

                    ((MaskedTextBox)obj).Enabled = estado;

                }

                if (obj is ComboBox)
                {

                    ((ComboBox)obj).Enabled = estado;

                }

                if (obj is GroupBox)
                {

                    ((GroupBox)obj).Enabled = estado;

                }
                if (obj is CheckBox)
                {

                    ((CheckBox)obj).Enabled = estado;

                }
                if (obj is RadioButton)
                {

                    ((RadioButton)obj).Enabled = estado;

                }

                if (obj is ListView)
                {

                    ((ListView)obj).Enabled = estado;

                }
                if (obj is DataGrid)
                {

                    ((DataGrid)obj).Enabled = estado;

                }
                if (obj is DateTimePicker)
                {

                    ((DateTimePicker)obj).Enabled = estado;

                }
                if (obj is Button)
                {

                    ((Button)obj).Enabled = estado;

                }
                if (obj is PictureBox)
                {

                    ((PictureBox)obj).Enabled = estado;

                }
            }

        }

        /// <summary>
        /// Reseteamos el formulario.
        /// </summary>
        /// <param name="gbx"></param>
        public static void ResetForm(ref GroupBox gbx)
        {
            foreach (Object obj in gbx.Controls)
            {

                if (obj is TextBox)
                {
                    if (((TextBox)obj).Name.Contains("Int"))
                    {

                        ((TextBox)obj).Text = "0";
                    }
                    else
                    {
                        ((TextBox)obj).Text = string.Empty;

                    }


                }
                if (obj is MaskedTextBox)
                {

                    ((MaskedTextBox)obj).Text = string.Empty;

                }

                if (obj is ComboBox)
                {
                    if (((ComboBox)obj).Items.Count == 0)
                    {
                        ((ComboBox)obj).Text = string.Empty;

                    }
                    else
                    {
                        ((ComboBox)obj).SelectedIndex = 0;
                    }

                    //((ComboBox)obj).SelectedIndex = 0;
                    //((ComboBox)obj).SelectedItem = 0;

                }


                if (obj is CheckBox)
                {

                    ((CheckBox)obj).Checked = false;

                }

                if (obj is ListView)
                {

                    ((ListView)obj).Items.Clear();

                }
                if (obj is DataGridView)
                {

                    ((DataGridView)obj).Rows.Clear();

                }
                if (obj is DateTimePicker)
                {

                    ((DateTimePicker)obj).Value = DateTime.Now;

                }

                if (obj is PictureBox)
                {

                    ((PictureBox)obj).Image = null;

                }

            }

        }


        public static int redondearNumero(decimal valor)
        {
            int entero = (int)valor;
            return ((entero + 5) / 10 * 10);

        }

        public static String URL_RECEPCION(bool pruebas)
        {
            if (pruebas)
            {
                return "https://api-sandbox.comprobanteselectronicos.go.cr/recepcion/v1/";

            }
            else
            {
              
                return "https://api.comprobanteselectronicos.go.cr/recepcion/v1/";
            }
        }

        public static String IDP_CLIENT_TOKEN(bool pruebas)
        {
            if (pruebas)
            {
                return "api-stag";

            }
            else
            {
                return "api-prod";
            }
        }

        public static String IDP_URI_TOKEN(bool pruebas)
        {
            if (pruebas)
            {
                return "https://idp.comprobanteselectronicos.go.cr/auth/realms/rut-stag/protocol/openid-connect/token";

            }
            else
            {
                return "https://idp.comprobanteselectronicos.go.cr/auth/realms/rut/protocol/openid-connect/token";
            }
        }
    }
}
