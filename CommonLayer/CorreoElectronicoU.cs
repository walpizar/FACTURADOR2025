using CommonLayer.Exceptions.BussinessExceptions;
using CommonLayer.Exceptions.PresentationsExceptions;
using CommonLayer.Logs;
using EntityLayer;
using MailKit;
using MailKit.Net.Imap;
using MailKit.Search;
using MimeKit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Mail;
using System.Xml;

namespace CommonLayer
{
    public class CorreoElectronicoU
    {

        private tbDocumento _doc { get; set; }
        private tbReporteHacienda _msj { get; set; }
        private int _tipoCorreo { get; set; }

        private List<string> _adjuntos;
        private string _envioCorreo { get; set; }
        private List<string> _destinoCorreo { get; set; }
        private string _mensaje { get; set; }
        private string _subject { get; set; }
        private string _contrasena { get; set; }

        private bool _cargarAdjuntos { get; set; }

        public CorreoElectronicoU(tbDocumento doc, List<string> correoDestino, bool cargarAdjuntos)
        {
            _tipoCorreo = 1;
            _doc = doc;
            _cargarAdjuntos = cargarAdjuntos;
            _destinoCorreo = correoDestino;

        }


        public CorreoElectronicoU(tbReporteHacienda msj, List<string> correoDestino, bool cargarAdjuntos)
        {

            _tipoCorreo = 2;
            _msj = msj;
            _cargarAdjuntos = cargarAdjuntos;
            _destinoCorreo = correoDestino;

        }

        public CorreoElectronicoU(string msj, List<string> correoDestino)
        {

            _tipoCorreo = 3;
            _mensaje = msj;
            _destinoCorreo = correoDestino;

        }



        public bool enviarCorreo()
        {
            if (_destinoCorreo.Count == 0)
            {
                clsEvento evento = new clsEvento("Destinatario", "1");
                throw new CorreoSinDestinatarioException("No hay destinatarios a quien enviar el correo");
            }
            bool enviado = false;

            try
            {

                _envioCorreo = Global.actividadEconomic.correoElectronicoEmpresa.Trim();
                _contrasena = Global.actividadEconomic.contrasenaCorreo.Trim();
                if (_mensaje == string.Empty)
                {
                    _mensaje = Global.actividadEconomic.cuerpoCorreo.Trim();
                }
                if (_tipoCorreo == 3)
                {
                    _subject = "Abono a cuenta";
                }
                else
                {
                    _subject = Global.actividadEconomic.subjectCorreo.Trim();
                }



                _adjuntos = generarAdjuntos();





                enviado = enviar();

            }
            catch (Exception ex)
            {
                clsEvento evento = new clsEvento(ex.Message, "1");
                return false;
            }



            return enviado;

        }

        private List<string> generarAdjuntos()
        {
            List<string> adjuntos = new List<string>();
            try
            {


                if (_cargarAdjuntos)
                {
                    if (_tipoCorreo == 1)
                    {
                        if (_doc.tipoDocumento != (int)Enums.TipoDocumento.FacturaElectronica)
                        {
                            _subject = Enum.GetName(typeof(Enums.TipoDocumento), _doc.tipoDocumento).ToUpper() + ". " + _subject;

                        }

                        string directorio = Global.Usuario.tbEmpresa.rutaCertificado.Trim();
                        string nombreArchivo = _doc.consecutivo;
                        string tipoDoc = Utility.getPrefixTypeDoc(_doc.tipoDocumento);
                        XmlDocument xml;
                        //string pdfFactura = generarPDF();
                        //if (pdfFactura != string.Empty)
                        //{
                        //    adjuntos.Add(pdfFactura);
                        //}
                        if (_doc.tipoDocumento != (int)Enums.TipoDocumento.Proforma)
                        {

                            if (_doc.xmlFirmado != null)
                            {
                                xml = Utility.DecodeBase64ToXML(_doc.xmlFirmado);
                                string archivo = (directorio + (nombreArchivo + tipoDoc + "_02_Firmado.xml"));
                                if (!File.Exists(archivo))
                                {
                                    xml.Save((directorio + (nombreArchivo + tipoDoc + "_02_Firmado.xml")));
                                    XmlTextWriter xmlTextWriter = new XmlTextWriter((directorio + (nombreArchivo + tipoDoc + "_02_Firmado.xml")), new System.Text.UTF8Encoding(false));
                                    xml.WriteTo(xmlTextWriter);
                                    xmlTextWriter.Close();

                                }

                                adjuntos.Add(archivo);
                            }

                            if (_doc.xmlRespuesta != null)
                            {
                                xml = Utility.DecodeBase64ToXML(_doc.xmlRespuesta);
                                string archivo = (directorio + (nombreArchivo + tipoDoc + "_05_RESP.xml"));
                                if (!File.Exists(archivo))
                                {
                                    xml.Save((directorio + (nombreArchivo + tipoDoc + "_05_RESP.xml")));
                                    XmlTextWriter xmlTextWriter = new XmlTextWriter((directorio + (nombreArchivo + tipoDoc + "_05_RESP.xml")), new System.Text.UTF8Encoding(false));
                                    xml.WriteTo(xmlTextWriter);
                                    xmlTextWriter.Close();

                                }

                                adjuntos.Add(archivo);

                            }

                        }



                    }
                    //adjuntos mensajes
                    else
                    {
                        string directorio = Global.Usuario.tbEmpresa.rutaCertificado.Trim() + Global.Usuario.tbEmpresa.rutaXMLCompras.Trim();
                        string nombreArchivo = _msj.consecutivoReceptor;
                        string tipoDoc = "_MS";
                        string reporte = _msj.estadoRecibido == (int)Enums.EstadoRespuestaHacienda.Aceptado ? Enum.GetName(typeof(Enums.EstadoRespuestaHacienda), _msj.estadoRecibido).ToUpper() : Enum.GetName(typeof(Enums.EstadoRespuestaHacienda), _msj.estadoRecibido).ToUpper() + ". Razón:" + _msj.razon.Trim().ToUpper() + ".Favor de emitir la respectiva NOTA DE CRÉDITO con su respectiva corrección. ";
                        _mensaje = $"Se ha procesado el documento Clave: {_msj.claveDocEmisor}, con el Monto:{_msj.totalFactura} e impuestos:{_msj.totalImp}. Se ha reportado en un estado: {reporte}. Gracias.";
                        _subject = $"Acuse documento recibido CLAVE:{_msj.claveDocEmisor}. " + _subject;
                        XmlDocument xml;
                        if (_msj.xmlFirmado != null)
                        {
                            xml = Utility.DecodeBase64ToXML(_msj.xmlFirmado);
                            string archivo = (directorio + (nombreArchivo + tipoDoc + "_02_Firmado.xml"));
                            if (!File.Exists(archivo))
                            {
                                xml.Save((directorio + (nombreArchivo + tipoDoc + "_02_Firmado.xml")));
                                XmlTextWriter xmlTextWriter = new XmlTextWriter((directorio + (nombreArchivo + tipoDoc + "_02_Firmado.xml")), new System.Text.UTF8Encoding(false));
                                xml.WriteTo(xmlTextWriter);
                                xmlTextWriter.Dispose();

                            }

                            adjuntos.Add(archivo);
                        }

                        if (_msj.xmlRespuesta != null)
                        {


                            xml = Utility.DecodeBase64ToXML(_msj.xmlRespuesta);
                            string archivo = (directorio + (nombreArchivo + tipoDoc + "_05_RESP.xml"));
                            if (!File.Exists(archivo))
                            {
                                xml.Save((directorio + (nombreArchivo + tipoDoc + "_05_RESP.xml")));
                                XmlTextWriter xmlTextWriter = new XmlTextWriter((directorio + (nombreArchivo + tipoDoc + "_05_RESP.xml")), new System.Text.UTF8Encoding(false));
                                xml.WriteTo(xmlTextWriter);
                                xmlTextWriter.Dispose();

                            }

                            adjuntos.Add(archivo);

                        }



                    }



                }



            }
            catch (Exception ex)
            {

                throw ex;
            }



            return adjuntos;
        }


        private bool enviar()
        {
            bool ok = false;
            try
            {
                //Creamos un nuevo Objeto de mensaje
                System.Net.Mail.MailMessage mmsg = new System.Net.Mail.MailMessage();

                //Direccion de correo electronico a la que queremos enviar el mensaje
                foreach (string item in _destinoCorreo)
                {
                    mmsg.To.Add(item);
                }


                //Nota: La propiedad To es una colección que permite enviar el mensaje a más de un destinatario

                //Asunto
                mmsg.Subject = _subject;
                mmsg.SubjectEncoding = System.Text.Encoding.UTF8;

                ////Direccion de correo electronico que queremos que reciba una copia del mensaje
                //mmsg.Bcc.Add(_destinoCorreo); //Opcional

                //Cuerpo del Mensaje
                mmsg.Body = _mensaje;
                mmsg.BodyEncoding = System.Text.Encoding.UTF8;
                mmsg.IsBodyHtml = false; //Si no queremos que se envíe como HTML

                //Correo electronico desde la que enviamos el mensaje
                mmsg.From = new System.Net.Mail.MailAddress(_envioCorreo);
                try
                {

                    foreach (var item in _adjuntos)
                    {
                        mmsg.Attachments.Add(new Attachment(item));
                    }
                }
                catch (Exception)

                {


                }





                /*-------------------------CLIENTE DE CORREO----------------------*/



                //Creamos un objeto de cliente de correo
                System.Net.Mail.SmtpClient cliente = new System.Net.Mail.SmtpClient();

                //Hay que crear las credenciales del correo emisor
                cliente.Credentials =
                    new System.Net.NetworkCredential(_envioCorreo, _contrasena);

                //Lo siguiente es obligatorio si enviamos el mensaje desde Gmail
                if (_envioCorreo.Contains("hotmail.com") || _envioCorreo.Contains("outlook.com") || _envioCorreo.Contains("live.com"))
                {
                    cliente.Port = 25;
                    cliente.Host = "smtp.live.com";
                }
                else if (_envioCorreo.Contains("gmail.com"))
                {
                    cliente.Port = 25;
                    cliente.Host = "smtp.gmail.com"; //Para Gmail "smtp.gmail.com";


                }
                cliente.EnableSsl = true;


                //cliente.DeliveryMethod = SmtpDeliveryMethod.Network;
                //cliente.UseDefaultCredentials = false;
                /*-------------------------ENVIO DE CORREO----------------------*/

                try
                {
                    //Enviamos el mensaje      
                    cliente.Send(mmsg);
                }
                catch (System.Net.Mail.SmtpException ex)
                {
                    mmsg = null;
                    cliente = null;
                    throw new EnvioCorreoException(ex);
                }


                mmsg = null;
                cliente = null;
                ok = true;

            }
            catch (Exception ex)
            {


                throw new EnvioCorreoException(ex);


            }


            return ok;
        }

       

    }
}
