using CommonLayer;
using CommonLayer.Exceptions.BussinessExceptions;
using CommonLayer.Exceptions.PresentationsExceptions;
using CommonLayer.Logs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Mail;
using System.Xml;

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
               
                throw ex;
            }
            
            catch (Exception ex)
            {
                // clsEvento evento = new clsEvento(ex.Message, "1");
                throw ex;
            }


            return enviado;

        }

        private static List<string> generarAdjuntos()
        {
            List<string> adjuntos = new List<string>();
            try
            {


                if (_docImp.cargarAdjuntos)
                {
                    if (_docImp.tipoAdjuntos == (int)Enums.tipoAdjunto.factura)
                    {
                       
                        string directorio = Global.Usuario.tbEmpresa.rutaCertificado.Trim();
                        string nombreArchivo = _docImp.doc.consecutivo;
                        string tipoDoc = Utility.getPrefixTypeDoc(_docImp.doc.tipoDocumento);
                        XmlDocument xml;


                        string pdfFactura = clsPDF.generarPDFFactura(_docImp.doc, _precios);
                        if (pdfFactura != string.Empty)
                        {
                            adjuntos.Add(pdfFactura);
                        }
                        if (_docImp.doc.tipoDocumento != (int)Enums.TipoDocumento.Proforma)
                        {

                            if (_docImp.doc.xmlFirmado != null)
                            {
                                xml = Utility.DecodeBase64ToXML(_docImp.doc.xmlFirmado);
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

                            if (_docImp.doc.xmlRespuesta != null)
                            {
                                xml = Utility.DecodeBase64ToXML(_docImp.doc.xmlRespuesta);
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
                    else if (_docImp.tipoAdjuntos == (int)Enums.tipoAdjunto.mensaje)
                    {
                        string directorio = Global.Usuario.tbEmpresa.rutaCertificado.Trim() + Global.Usuario.tbEmpresa.rutaXMLCompras.Trim();
                        string nombreArchivo = _docImp.msj.consecutivoReceptor;
                        string tipoDoc = "_MS";
                        string reporte = _docImp.msj.estadoRecibido == (int)Enums.EstadoRespuestaHacienda.Aceptado ? Enum.GetName(typeof(Enums.EstadoRespuestaHacienda), _docImp.msj.estadoRecibido).ToUpper() : Enum.GetName(typeof(Enums.EstadoRespuestaHacienda), _docImp.msj.estadoRecibido).ToUpper() + ". Razón:" + _docImp.msj.razon.Trim().ToUpper() + ".Favor de emitir la respectiva NOTA DE CRÉDITO con su respectiva corrección. ";
                        _mensaje = $"Se ha procesado el documento Clave: {_docImp.msj.claveDocEmisor}, con el Monto:{_docImp.msj.totalFactura} e impuestos:{_docImp.msj.totalImp}. Se ha reportado en un estado: {reporte}. Gracias.";
                        _subject = $"Acuse documento recibido CLAVE:{_docImp.msj.claveDocEmisor}. " + _subject;
                        XmlDocument xml;
                        if (_docImp.msj.xmlFirmado != null)
                        {
                            xml = Utility.DecodeBase64ToXML(_docImp.msj.xmlFirmado);
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

                        if (_docImp.msj.xmlRespuesta != null)
                        {


                            xml = Utility.DecodeBase64ToXML(_docImp.msj.xmlRespuesta);
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
                    else if (_docImp.tipoAdjuntos == (int)Enums.tipoAdjunto.ordenCompra)
                    {

                        _subject = "Orden de Compra #" + _docImp.ordenCompra.id;
                        _mensaje = "Estimado proveedor, se adjunta orden de compra para su atención. Gracias.";




                        string directorio = Global.Usuario.tbEmpresa.rutaCertificado.Trim();
                        string nombreArchivo = _docImp.ordenCompra.id.ToString();
                        string tipoDoc = Utility.getPrefixTypeDoc((int)Enums.TipoDocumento.OrdenCompra);


                        string pdfFactura = clsPDF.generarPDFOrdenCompra(_docImp.ordenCompra);
                        if (pdfFactura != string.Empty)
                        {
                            adjuntos.Add(pdfFactura);
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                clsEvento evento = new clsEvento(ex.Message, "1");
                throw ex;
            }
            return adjuntos;
        }




        private static bool enviar()
        {
            bool ok = false;
            try
            {
                //Creamos un nuevo Objeto de mensaje
                System.Net.Mail.MailMessage mmsg = new System.Net.Mail.MailMessage();

                //Direccion de correo electronico a la que queremos enviar el mensaje
                foreach (string item in _docImp.correoDestino)
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
                    if (_adjuntos != null)
                    {

                        foreach (var item in _adjuntos)
                        {
                            mmsg.Attachments.Add(new Attachment(item));
                        }

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
                cliente.EnableSsl = true;
                //Lo siguiente es obligatorio si enviamos el mensaje desde Gmail
                if (_envioCorreo.Contains("hotmail.com") || _envioCorreo.Contains("outlook.com") || _envioCorreo.Contains("live.com"))
                {
                    cliente.Port = 587;
                    cliente.Host = "smtp-mail.outlook.com";
                }
                else if (_envioCorreo.Contains("gmail.com"))
                {
                    cliente.Port = 587;
                    cliente.Host = "smtp.gmail.com"; //Para Gmail "smtp.gmail.com";


                }else if (_envioCorreo.Contains("espartanosolutions.com"))
                {
                    cliente.Port = 26;
                    cliente.Host = "mail.espartanosolutions.com"; //Para Gmail "smtp.gmail.com";
                    cliente.EnableSsl = false;

                }
               

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
                    mmsg.Attachments.Dispose();
                    mmsg.Dispose();
                    cliente = null;
                    throw new EnvioCorreoException(ex);
                }


                mmsg.Attachments.Dispose();
                mmsg.Dispose();
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
