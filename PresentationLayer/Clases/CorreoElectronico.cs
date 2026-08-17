using CommonLayer;
using CommonLayer.Exceptions.BussinessExceptions;
using CommonLayer.Exceptions.PresentationsExceptions;
using CommonLayer.Logs;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Mail;
using System.Xml;
using static CommonLayer.Enums;





namespace PresentationLayer.Clases
{
    public static class CorreoElectronico
    {

        public static clsDocumentoCorreo _docImp { get; set; }
        //private tbDocumento _doc { get; set; }
        //private tbReporteHacienda _msj { get; set; }
        //private int _tipoCorreo { get; set; }

        private static List<string> _adjuntos;
        private static string _envioCorreo { get; set; }
        //private List<string> _destinoCorreo { get; set; }
        private static string _mensaje { get; set; }
        private static string _subject { get; set; }
        private static string _contrasena { get; set; }

        private static bool _precios { get; set; }
        //private bool _cargarAdjuntos { get; set; }

        //public CorreoElectronico(tbDocumento doc,List<string>correoDestino,bool cargarAdjuntos)
        //{
        //    _tipoCorreo = 1;
        //     _doc = doc;
        //    _cargarAdjuntos = cargarAdjuntos;
        //    _destinoCorreo= correoDestino;

        //}


        //public CorreoElectronico(tbReporteHacienda msj, List<string> correoDestino, bool cargarAdjuntos)
        //{

        //    _tipoCorreo = 2;
        //    _msj = msj;
        //    _cargarAdjuntos = cargarAdjuntos;
        //    _destinoCorreo = correoDestino;

        //}
        public static bool enviarCorreoCierreCaja(clsDocumentoCorreo docImp, string cuerpo, string subject)
        {
            try
            {
                _docImp = docImp;
                _envioCorreo = Global.actividadEconomic.correoElectronicoEmpresa.Trim();
                _contrasena = Global.actividadEconomic.contrasenaCorreo.Trim();
                _mensaje = cuerpo;
                _subject = subject;
                return enviar();


            }
            catch (Exception)
            {

                return false;
            }     
        
        }

            public static bool enviarCorreo(clsDocumentoCorreo docImp)
            {
            _docImp = docImp;
            _envioCorreo = Global.actividadEconomic.correoElectronicoEmpresa.Trim();
            _contrasena = Global.actividadEconomic.contrasenaCorreo.Trim();
            _mensaje = Global.actividadEconomic.cuerpoCorreo.Trim();
            _subject = Global.actividadEconomic.subjectCorreo.Trim();
            _precios = docImp.preciosProforma;

            if (_docImp.correoDestino.Count == 0)
            {
                clsEvento evento = new clsEvento("Destinatario", "1");
                throw new CorreoSinDestinatarioException("No hay destinatarios a quien enviar el correo");
            }
            bool enviado = false;

            try
            {
                if (_docImp.doc.tipoDocumento == (int)Enums.TipoDocumento.FacturaElectronica || _docImp.doc.tipoDocumento == (int)Enums.TipoDocumento.TiqueteElectronico
                   ||  _docImp.doc.tipoDocumento == (int)Enums.TipoDocumento.NotaCreditoElectronica
                   || _docImp.doc.tipoDocumento == (int)Enums.TipoDocumento.NotaDebitoElectronica)
                {
                    _subject = _subject + ". " + Enum.GetName(typeof(Enums.TipoDocumento), _docImp.doc.tipoDocumento).ToUpper();
                    _mensaje = string.Format("Consecutivo: {0} {1}{1}{2}", _docImp.doc.consecutivo, Environment.NewLine, _mensaje);

                }               
              

                _adjuntos = generarAdjuntos();


                enviado = enviar();

            }
            catch (EnvioCorreoException ex)
            {
                clsEvento evento = new clsEvento(ex.Message, "1");
                throw ex;
            }
            
            catch (Exception ex)
            {

                clsEvento evento = new clsEvento(ex.Message, "1");
                // clsEvento evento = new clsEvento(ex.Message, "1");
                throw ex;
            }


            return enviado;

        }

        private static List<string> generarAdjuntos()
        {
            var adjuntos = new List<string>();

            try
            {
                if (!_docImp.cargarAdjuntos)
                    return adjuntos;

                // FACTURA / TIQUETE / NC / ND / PROFORMA
                if (_docImp.tipoAdjuntos == (int)Enums.tipoAdjunto.factura)
                {
                    string directorio = (Global.Usuario.tbEmpresa.rutaCertificado ?? string.Empty).Trim();
                    AsegurarDirectorio(directorio);

                    string nombreArchivo = _docImp.doc.consecutivo;
                   string tipoDoc = Utility.getPrefixTypeDoc(_docImp.doc.tipoDocumento);

                    // 1) PDF (puede venir bloqueado si el generador no liberó stream)
                    string pdfFactura = clsPDF.generarPDFFactura(_docImp.doc, _precios);
                    if (!string.IsNullOrWhiteSpace(pdfFactura))
                    {
                        // Reintento corto por si el PDF está “terminando de cerrarse”
                        EsperarArchivoListo(pdfFactura, 4000);
                        adjuntos.Add(pdfFactura);
                    }

                    // 2) XMLs (solo si NO es proforma)
                    if (_docImp.doc.tipoDocumento != (int)Enums.TipoDocumento.Proforma)
                    {
                        if (_docImp.doc.xmlFirmado != null)
                        {
                            var xmlFirmado = Utility.DecodeBase64ToXML(_docImp.doc.xmlFirmado);
                            string rutaFirmado = Path.Combine(directorio, nombreArchivo + tipoDoc + "_02_Firmado.xml");
                            GuardarXmlSeguro(xmlFirmado, rutaFirmado);
                            adjuntos.Add(rutaFirmado);
                        }

                        if (_docImp.doc.xmlRespuesta != null)
                        {
                            var xmlResp = Utility.DecodeBase64ToXML(_docImp.doc.xmlRespuesta);
                            string rutaResp = Path.Combine(directorio, nombreArchivo + tipoDoc + "_05_RESP.xml");
                            GuardarXmlSeguro(xmlResp, rutaResp);
                            adjuntos.Add(rutaResp);
                        }
                    }
                }
                // MENSAJE (acuse)
                else if (_docImp.tipoAdjuntos == (int)Enums.tipoAdjunto.mensaje)
                {
                    string directorioBase = (Global.Usuario.tbEmpresa.rutaCertificado ?? string.Empty).Trim();
                    string subRuta = (Global.Usuario.tbEmpresa.rutaXMLCompras ?? string.Empty).Trim();

                    string directorio = Path.Combine(directorioBase, subRuta);
                    AsegurarDirectorio(directorio);

                    string nombreArchivo = _docImp.msj.consecutivoReceptor;
                    string tipoDoc = "_MS";

                    string estado = Enum.GetName(typeof(Enums.EstadoRespuestaHacienda), _docImp.msj.estadoRecibido).ToUpper();
                    string reporte = (_docImp.msj.estadoRecibido == (int)Enums.EstadoRespuestaHacienda.Aceptado)
                        ? estado
                        : estado + ". Razón:" + (_docImp.msj.razon ?? string.Empty).Trim().ToUpper() +
                          ".Favor de emitir la respectiva NOTA DE CRÉDITO con su respectiva corrección. ";

                    _mensaje = string.Format(
                        "Se ha procesado el documento Clave: {0}, con el Monto:{1} e impuestos:{2}. Se ha reportado en un estado: {3}. Gracias.",
                        _docImp.msj.claveDocEmisor,
                        _docImp.msj.totalFactura,
                        _docImp.msj.totalImp,
                        reporte
                    );

                    _subject = "Acuse documento recibido CLAVE:" + _docImp.msj.claveDocEmisor + ". " + _subject;

                    if (_docImp.msj.xmlFirmado != null)
                    {
                        var xmlFirmado = Utility.DecodeBase64ToXML(_docImp.msj.xmlFirmado);
                        string rutaFirmado = Path.Combine(directorio, nombreArchivo + tipoDoc + "_02_Firmado.xml");
                        GuardarXmlSeguro(xmlFirmado, rutaFirmado);
                        adjuntos.Add(rutaFirmado);
                    }

                    if (_docImp.msj.xmlRespuesta != null)
                    {
                        var xmlResp = Utility.DecodeBase64ToXML(_docImp.msj.xmlRespuesta);
                        string rutaResp = Path.Combine(directorio, nombreArchivo + tipoDoc + "_05_RESP.xml");
                        GuardarXmlSeguro(xmlResp, rutaResp);
                        adjuntos.Add(rutaResp);
                    }
                }
                // ORDEN DE COMPRA
                else if (_docImp.tipoAdjuntos == (int)Enums.tipoAdjunto.ordenCompra)
                {
                    _subject = "Orden de Compra #" + _docImp.ordenCompra.id;
                    _mensaje = "Estimado proveedor, se adjunta orden de compra para su atención. Gracias.";

                    string directorio = (Global.Usuario.tbEmpresa.rutaCertificado ?? string.Empty).Trim();
                    AsegurarDirectorio(directorio);

                    string pdf = clsPDF.generarPDFOrdenCompra(_docImp.ordenCompra);
                    if (!string.IsNullOrWhiteSpace(pdf))
                    {
                        EsperarArchivoListo(pdf, 4000);
                        adjuntos.Add(pdf);
                    }
                }

                return adjuntos;
            }
            catch (Exception ex)
            {
                clsEvento evento = new clsEvento(ex.Message, "1");
                throw;
            }
        }

        private static void AsegurarDirectorio(string directorio)
        {
            if (string.IsNullOrWhiteSpace(directorio))
                throw new DirectoryNotFoundException("Directorio no válido para guardar adjuntos.");

            if (!Directory.Exists(directorio))
                Directory.CreateDirectory(directorio);
        }
        private static void GuardarXmlSeguro(XmlDocument xml, string ruta)
        {
            // Si ya existe, igual se puede adjuntar; si quieres sobrescribir, cambia la lógica.
            if (File.Exists(ruta))
                return;

            // Reintentos por si hay lock momentáneo (antivirus, indexador, etc.)
            int intentos = 6;       // ~3 segundos (6 * 500ms)
            int esperaMs = 500;

            for (int i = 0; i < intentos; i++)
            {
                try
                {
                    using (var fs = new FileStream(ruta, FileMode.CreateNew, FileAccess.Write, FileShare.Read))
                    using (var writer = new XmlTextWriter(fs, new System.Text.UTF8Encoding(false)))
                    {
                        writer.Formatting = Formatting.Indented;
                        xml.WriteTo(writer);
                        writer.Flush();
                    }
                    return;
                }
                catch (IOException)
                {
                    System.Threading.Thread.Sleep(esperaMs);
                }
            }

            // Último intento: si sigue fallando, explota con mensaje claro
            throw new IOException("No se pudo guardar el XML porque el archivo está en uso o no se pudo crear: " + ruta);
        }

        private static void EsperarArchivoListo(string ruta, int msMax)
        {
            int transcurrido = 0;
            while (transcurrido < msMax)
            {
                if (File.Exists(ruta) && !EstaBloqueado(ruta))
                    return;

                System.Threading.Thread.Sleep(300);
                transcurrido += 300;
            }
            // No se lanza excepción aquí para no romper flujo; si quieres que sea estricto, lanza IOException
        }
        private static bool EstaBloqueado(string ruta)
        {
            FileStream fs = null;
            try
            {
                fs = new FileStream(ruta, FileMode.Open, FileAccess.ReadWrite, FileShare.None);
                return false;
            }
            catch (IOException)
            {
                return true;
            }
            finally
            {
                if (fs != null) fs.Dispose();
            }
        }



private static bool enviar()
    {
        List<string> archivosTemporales = new List<string>();

        try
        {
            using (var mmsg = new System.Net.Mail.MailMessage())
            {
                foreach (string item in _docImp.correoDestino)
                    mmsg.To.Add(item);

                mmsg.Subject = _subject;
                mmsg.SubjectEncoding = System.Text.Encoding.UTF8;

                mmsg.Body = _mensaje;
                mmsg.BodyEncoding = System.Text.Encoding.UTF8;
                mmsg.IsBodyHtml = false;

                mmsg.From = new System.Net.Mail.MailAddress(_envioCorreo);

                // ====== ADJUNTOS CON COPIA TEMPORAL ======
                if (_adjuntos != null)
                {
                    foreach (var rutaOriginal in _adjuntos)
                    {
                        if (!File.Exists(rutaOriginal))
                            throw new FileNotFoundException("No existe el archivo: " + rutaOriginal);

                            string baseName = Path.GetFileNameWithoutExtension(rutaOriginal);
                            string ext = Path.GetExtension(rutaOriginal);
                            string nombreTemp = $"{baseName}{ext}";
                            string rutaTemp = Path.Combine(Path.GetTempPath(), nombreTemp);

                            // Pequeña espera por si el generador aún está liberando el archivo
                            System.Threading.Thread.Sleep(300);

                        File.Copy(rutaOriginal, rutaTemp, true);

                        archivosTemporales.Add(rutaTemp);

                        mmsg.Attachments.Add(new Attachment(rutaTemp));
                    }
                }

                // Convertir MailMessage -> MimeMessage (MailKit)
                MimeMessage mensaje = MimeMessage.CreateFromMailMessage(mmsg);

                // ====== AQUÍ SÍ: MailKit SmtpClient ======
                using (var cliente = new MailKit.Net.Smtp.SmtpClient())
                {
                    string host = "";
                    int port = 0;
                    SecureSocketOptions security = SecureSocketOptions.StartTls;

                    if (_envioCorreo.Contains("hotmail.com") ||
                        _envioCorreo.Contains("outlook.com") ||
                        _envioCorreo.Contains("live.com"))
                    {
                        host = "smtp-mail.outlook.com";
                        port = 587;
                        security = SecureSocketOptions.StartTls;
                    }
                    else if (_envioCorreo.Contains("gmail.com"))
                    {
                        host = "smtp.gmail.com";
                        port = 587;
                        security = SecureSocketOptions.StartTls;
                    }
                    else if (_envioCorreo.Contains("espartanosolutions.com"))
                    {
                        host = "mail.espartanosolutions.com";
                        port = 465; // recomendado por el proveedor
                        security = SecureSocketOptions.SslOnConnect;
                    }
                    else
                    {
                        throw new InvalidOperationException("Dominio de correo no soportado para configuración SMTP: " + _envioCorreo);
                    }

                        // (Opcional) evita algunos fallos por OAuth2/modern auth cuando usted usa usuario/clave
                        cliente.CheckCertificateRevocation = false;
                        cliente.AuthenticationMechanisms.Remove("XOAUTH2");

                    cliente.Connect(host, port, security);

                    // Autenticación requerida según el proveedor
                    cliente.Authenticate(_envioCorreo, _contrasena);

                    cliente.Send(mensaje);

                    cliente.Disconnect(true);
                }
            }

            return true;
        }
        catch (Exception ex)
        {
            // visor de sucesos
            clsEvento evento = new clsEvento(ex.Message, "1");
            throw new EnvioCorreoException(ex);
        }
        finally
        {
            // ====== LIMPIEZA DE ARCHIVOS TEMPORALES ======
            foreach (var temp in archivosTemporales)
            {
                try
                {
                    if (File.Exists(temp))
                        File.Delete(temp);
                }
                catch
                {
                    // Si no se puede borrar, no bloqueamos el proceso
                }
            }
        }
    }



}
}
