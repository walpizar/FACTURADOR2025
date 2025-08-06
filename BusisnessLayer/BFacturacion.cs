﻿using CommonLayer;
using CommonLayer.DTO;
using CommonLayer.Exceptions.BusisnessExceptions;
using CommonLayer.Exceptions.BussinessExceptions;
using CommonLayer.Logs;
using DataLayer;
using EntityLayer;
using FacturacionElectronicaLayer.ClasesDatos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;

namespace BusinessLayer
{

    public class BFacturacion
    {
        DFacturacion DFacturaIns = new DFacturacion();
        DEmpresa empresaIns = new DEmpresa();
        string codigoPais = Global.Usuario.tbEmpresa.tbParametrosEmpresa.FirstOrDefault().codigoPais;
        string sucursal = Global.Configuracion.sucursal.ToString();
        string caja = Global.Configuracion.caja.ToString();
        DTipoMedida medidaIns = new DTipoMedida();
        BProducto productoIns = new BProducto();

        List<tbReporteHacienda> listaGuardados = null;


        public tbDocumento guadar(tbDocumento facturaGlobal)
        {


            if (facturaGlobal.tipoDocumento != (int)Enums.TipoDocumento.ProformaGeneral && facturaGlobal.tipoDocumento != (int)Enums.TipoDocumento.Proforma)
            {


                facturaGlobal.id = DFacturaIns.getNewID(facturaGlobal.tipoDocumento) + 1;

                facturaGlobal.consecutivo = CreaNumeroConsecutivo(sucursal,
                                                                   caja,
                                                                   facturaGlobal.tipoDocumento.ToString().Trim(),
                                                                   facturaGlobal.id.ToString().Trim());
                string codigoSeguridad = CreaCodigoSeguridad(facturaGlobal.tipoDocumento.ToString().Trim(),
                                                                sucursal,
                                                                caja,
                                                                facturaGlobal.fecha,
                                                                facturaGlobal.id.ToString().Trim());
                facturaGlobal.clave = CreaClave(codigoPais,
                                            facturaGlobal.fecha.Day.ToString(),
                                            facturaGlobal.fecha.Month.ToString(),
                                            facturaGlobal.fecha.Year.ToString(),
                                            Global.Usuario.idEmpresa.Trim(),
                                            facturaGlobal.consecutivo,
                                            facturaGlobal.estadoFactura.ToString().Trim(),
                                            codigoSeguridad);


                if (facturaGlobal.tipoDocumento != (int)Enums.TipoDocumento.ProformaGeneral && facturaGlobal.tipoDocumento != (int)Enums.TipoDocumento.Proforma)
                {

                    facturaGlobal.id = DFacturaIns.getNewID(facturaGlobal.tipoDocumento) + 1;

                    foreach (var item in facturaGlobal.tbDetalleDocumento)
                    {
                        item.idDoc = facturaGlobal.id;
                        item.idTipoDoc = facturaGlobal.tipoDocumento;

                    }
                    foreach (var item in facturaGlobal.tbPagos)
                    {
                        item.idDoc = facturaGlobal.id;
                        item.tipoDoc = facturaGlobal.tipoDocumento;

                    }

                }

                return DFacturaIns.Guardar(facturaGlobal);

            }
            else
            {
                var fact = DFacturaIns.GetEntity(facturaGlobal, false);
                if (fact is null)
                {
                    facturaGlobal.id = 0;
                }


                if (facturaGlobal.id == 0)
                {
                    facturaGlobal.id = DFacturaIns.getNewID(facturaGlobal.tipoDocumento) + 1;

                    foreach (var item in facturaGlobal.tbDetalleDocumento)
                    {
                        item.idDoc = facturaGlobal.id;
                        item.idTipoDoc = facturaGlobal.tipoDocumento;

                    }


                    facturaGlobal = DFacturaIns.Guardar(facturaGlobal);

                }
                else
                {
                    foreach (var item in facturaGlobal.tbDetalleDocumento)
                    {
                        item.idDoc = facturaGlobal.id;
                        item.idTipoDoc = facturaGlobal.tipoDocumento;

                    }
                    facturaGlobal = DFacturaIns.modificarProforma(facturaGlobal);
                }


                return facturaGlobal;
            }




        }

        public string guadarListaCompras(List<tbCompras> listaCompras)
        {
            bTipoMedidas medidaIns = new bTipoMedidas();
            string cuerpo = "", encabezado = "";
            int ok = 0;
            int bad = 0;
            int cantidad = listaCompras.Count;
            int conta = 1;
            try
            {

                foreach (var compras in listaCompras)
                {

                    try
                    {

                        var compra = guadarCompra(compras);
                        ok++;

                    }
                    catch (Exception ex)
                    {
                        bad++;
                        cuerpo += string.Format("{5}- Error en la factura #{1}, Proveedor: {2}-{3}. Mensaje:{4}{0}{0} ", Environment.NewLine, compras.numFactura.Trim(), compras.idProveedor.Trim(), compras.nombreProveedor.Trim(), ex.Message, conta++);
                        // conta++;
                    }

                }

            }
            catch (Exception)
            {

                throw;
            }


            encabezado = string.Format("{0}Cantidad de archivos:{1} Cantidad procesadas:{2} Cantidad sin procesar:{3}{0}{0}", Environment.NewLine, cantidad, ok, bad);
            encabezado += cuerpo;
            return encabezado;

        }

        public tbCompras guadarCompra(tbCompras facturaGlobal)
        {
            DCategoriaProducto cateIns = new DCategoriaProducto();
            tbCompras compraOriginal = null;

            if (Global.Usuario.tbEmpresa.id.Trim() != facturaGlobal.idEmpresa.Trim())
            {
                throw new CompraNoEmpresaException(facturaGlobal.numFactura);

            }

            if (facturaGlobal.tipoDoc == (int)Enums.TipoDocumento.NotaCredito)
            {
                compraOriginal = DFacturaIns.getByClave(facturaGlobal.claveEmisor);
                if (compraOriginal != null)
                {
                    throw new CompraExistException();

                }
                if (facturaGlobal.claveRef.Length == 20)
                {
                    compraOriginal = DFacturaIns.getComprasByConsecutivo(facturaGlobal.claveRef);
                    if (compraOriginal == null)
                    {
                        throw new EntittyRefNoExistException();

                    }
                }
                else
                {
                    compraOriginal = DFacturaIns.getByClave(facturaGlobal.claveRef);
                    if (compraOriginal == null)
                    {
                        throw new EntittyRefNoExistException();

                    }

                }

            }
            else
            {
                if (facturaGlobal.claveEmisor != null && facturaGlobal.claveEmisor != string.Empty)
                {
                    compraOriginal = DFacturaIns.getByClave(facturaGlobal.claveEmisor);
                }
                else
                {
                    compraOriginal = DFacturaIns.getByNumFact(facturaGlobal.numFactura, facturaGlobal.tipoIdProveedor, facturaGlobal.idProveedor);
                }

                if (compraOriginal != null)
                {
                    throw new CompraExistException();

                }
            }
            //valido datos de la compra

            foreach (var detalle in facturaGlobal.tbDetalleCompras)
            {
                //ASIGNO LOS ID DE MEDIDA SEGUN NOMENCLATURA
                tbTipoMedidas medida = new tbTipoMedidas();
                medida.nomenclatura = detalle.nomenclatura;
                detalle.idMedida = medidaIns.GetEnityByNomenclatura(medida).idTipoMedida;

   

            }
            // cambio la moneda cuando es dolar y el usuario realizo el cambio a colon
            if (facturaGlobal.tipoMoneda == (int)Enums.TipoMoneda.USD && (bool)facturaGlobal.cambiarColon &&
                facturaGlobal.tipoCambio != null && facturaGlobal.tipoCambio > 0)
            {
                facturaGlobal.tipoMoneda = (int)Enums.TipoMoneda.CRC;
                //foreach (var detalle in facturaGlobal.tbDetalleCompras)
                //{

                //    detalle.precio = detalle.precio * (decimal)facturaGlobal.tipoCambio;
                //    detalle.montoTotal = detalle.precio * detalle.cantidad;

                //    detalle.montoTotaDesc = detalle.montoTotaDesc * (decimal)facturaGlobal.tipoCambio;
                //    detalle.montoTotalImp = detalle.montoTotalImp * (decimal)facturaGlobal.tipoCambio;
                //    detalle.montoTotalExo = detalle.montoTotalExo * (decimal)facturaGlobal.tipoCambio;
                //    detalle.montoTotalLinea = detalle.montoTotalLinea * (decimal)facturaGlobal.tipoCambio;
                //    facturaGlobal.observaciones += " FACTURA EN DOLARES";
                //}


            }



            //asigno los id's

            int id = DFacturaIns.getNewIDCompra(facturaGlobal.tipoDoc) + 1;

            facturaGlobal.id = id;

            if (facturaGlobal.tipoDoc != (int)Enums.TipoDocumento.Proforma)
            {


                facturaGlobal.consecutivo = CreaNumeroConsecutivo(sucursal,
                                                                        caja,
                                                                        facturaGlobal.tipoDoc.ToString().Trim(),
                                                                        facturaGlobal.id.ToString().Trim());
                string codigoSeguridad = CreaCodigoSeguridad(facturaGlobal.tipoDoc.ToString().Trim(),
                                                                sucursal,
                                                                caja,
                                                                facturaGlobal.fecha,
                                                                facturaGlobal.id.ToString().Trim());
                facturaGlobal.clave = CreaClave(codigoPais,
                                            facturaGlobal.fecha.Day.ToString(),
                                            facturaGlobal.fecha.Month.ToString(),
                                            facturaGlobal.fecha.Year.ToString(),
                                            Global.Usuario.tbEmpresa.id.Trim(),
                                            facturaGlobal.consecutivo,
                                            facturaGlobal.tipoCompra.ToString().Trim(),
                                            codigoSeguridad);
            }

            int codPro = productoIns.obtenerNuevoId();
            foreach (var item in facturaGlobal.tbDetalleCompras)
            {

                item.idCompra = facturaGlobal.id;
                item.TipoCompra = facturaGlobal.tipoDoc;
                if (item.idProducto == "0")
                {
                    item.idProducto = codPro.ToString().PadLeft(5, '0');
                    codPro++;
                }

            }





            return DFacturaIns.GuardarCompra(facturaGlobal);
        }

        public bool guadarFacturaAbonos(clsAbonos abono)
        {

            try
            {

                foreach (var item in abono.abonos)
                {
                    item.tbClientes = null;
                    //item.tbDetalleDocumento = null;
                }

                DFacturaIns.abonos(abono.abonos);
                return true;

            }
            catch (Exception ex)
            {

                throw ex;
            }




        }

        // private void enviarCorreoAbono(IEnumerable<tbDocumento> docs, string correoEle, tbClientes cli, decimal abonoTotal, decimal adeudadoGeneral)

        public tbCompras GetEntityCompra(tbCompras compra)
        {
            return DFacturaIns.GetEntityCompra(compra);
        }
        public List<tbDetalleDocumento> getEntityDetails(tbDocumento facturaGlobal)
        {
            return DFacturaIns.getEntityDetails(facturaGlobal);
        }
        public tbDocumento getEntity(tbDocumento facturaGlobal)
        {
            return getEntity(facturaGlobal, true);
        }
        public tbDocumento getLastEntity(string nombreUsuario)
        {
            return DFacturaIns.GetLastEntity(nombreUsuario);
        }
        public tbDocumento getEntity(tbDocumento facturaGlobal, bool anexos)
        {

            return DFacturaIns.GetEntity(facturaGlobal, anexos);
        }
        public tbDocumento getEntityByClave(tbDocumento facturaGlobal)
        {

            return DFacturaIns.GetEntityByClave(facturaGlobal);
        }
        public tbDocumento getEntityByKey(int id, int tipoDoc)
        {

            return DFacturaIns.getEntityByKey(id, tipoDoc);
        }
        public IEnumerable<tbDocumento> listaFacturas()
        {


            return DFacturaIns.GetListEntities();
        }

        public IEnumerable<tbDocumento> listaFacturasFechas(DateTime inicio, DateTime fin)
        {


            return DFacturaIns.GetListEntitiesFechas(inicio, fin);
        }

        public IEnumerable<DocumentoHaciendaDTO> getListaValidacion(DateTime inicio, DateTime fin)
        {


            return DFacturaIns.getListaValidacion(inicio, fin);
        }


        public IEnumerable<DocumentoHaciendaDTO> listaFacturasInconsistentes()
        {

            return DFacturaIns.getListaInconsistentesAsync();
        }

        public IEnumerable<tbReporteHacienda> listaMensajesCompras()
        {


            return DFacturaIns.GetListMensajesCompras();
        }

        public IEnumerable<tbReporteHacienda> listaMensajesComprasIncosistentes()
        {


            return DFacturaIns.GetListMensajesComprasIncosistentes();
        }

        public IEnumerable<tbCompras> listaComprasSimplificada()
        {


            return DFacturaIns.listaComprasSimplificada();
        }

        public IEnumerable<tbCompras> listaComprasSimplificadaInconsitentes()
        {


            return DFacturaIns.listaComprasSimplificadaInconsistente();
        }
        /// <summary>
        /// reporte a hacienda.
        /// </summary>
        /// <param name="facturaGlobal"></param>
        /// <returns></returns>




        public string envioMensaje(List<tbReporteHacienda> listaMensajes)
        {
            string mensaje = string.Empty;
            int procesados = 0, incorrectos = 0;
            tbReporteHacienda mensajeHacienda = null;
            listaGuardados = new List<tbReporteHacienda>();
            List<tbReporteHacienda> existentes = new List<tbReporteHacienda>();
            try
            {
                foreach (var msj in listaMensajes)
                {
                    mensajeHacienda = DFacturaIns.GetMensajeByClaveIdEmisor(msj.claveDocEmisor.Trim(), msj.idEmisor.Trim());

                    if (mensajeHacienda is null)
                    {
                        procesados++;
                        int id = DFacturaIns.getNewIDMensaje() + 1;
                        msj.id = id;
                        msj.consecutivoReceptor = CreaNumeroConsecutivoMensaje(sucursal,
                                                                   caja,
                                                                  msj.estadoRecibido.ToString(),
                                                                   msj.id.ToString().Trim());

                        string codigoSeguridad = CreaCodigoSeguridad(getTipoDocMensajes(msj.estadoRecibido.ToString()),
                                                         sucursal,
                                                         caja,
                                                         msj.fecha,
                                                         msj.id.ToString().Trim());
                        msj.claveReceptor = CreaClave(codigoPais,
                                                    msj.fecha.Day.ToString(),
                                                    msj.fecha.Month.ToString(),
                                                    msj.fecha.Year.ToString(),
                                                    Global.Usuario.tbEmpresa.id.Trim(),
                                                    msj.consecutivoReceptor,
                                                    "1",//estado 1
                                                    codigoSeguridad);





                        mensajeHacienda = DFacturaIns.GuardarMensaje(msj);
                        listaGuardados.Add(mensajeHacienda);
                        // bool vA=enviarMensajeHacienda(mensajeHacienda);

                    }
                    else
                    {
                        mensaje += string.Format("Ya exite. Clave: {1} Emisor: {2} Archivo: {3}{0} ", Environment.NewLine, msj.claveDocEmisor, msj.idEmisor, msj.nombreArchivo);
                        existentes.Add(msj);


                        incorrectos++;
                    }



                }
                //elimina los que ya fueron procesados anteriormente.
                foreach (var item in existentes)
                {
                    listaMensajes.Remove(item);
                }
                return mensaje;


            }
            catch (Exception ex)
            {
                clsEvento evento = new clsEvento(ex.Message, "1");
                throw ex;
            }
            //if (mensaje==string.Empty)
            //{
            //   return string.Format("Total Procesados: {1}{0}Correctos: {2}{0}Incorrectos: {3}", Environment.NewLine, listaMensajes.Count(), procesados,incorrectos);

            //}
            //else
            //{
            //    return string.Format("Total Procesados: {1}{0}Correctos: {2}{0}Incorrectos: {3}{0}Mensaje:{4}", Environment.NewLine, listaMensajes.Count(), procesados, incorrectos, mensaje);



            //}

        }

        public decimal validarCredito(string idCliente, int? tipoIdCliente)
        {
            return  DFacturaIns.validarCredito(idCliente, tipoIdCliente);
        }

        public void reportarMensajesHacienda(object o, DoWorkEventArgs e)
        {
            try
            {
                if (Utility.AccesoInternet())
                {
                    foreach (var msj in listaGuardados)
                    {
                        enviarMensajeHacienda(msj);


                        consultarMensaje(msj);



                    }
                }


            }
            catch (Exception ex)
            {
                clsEvento evento = new clsEvento(ex.Message, "1");
                throw ex;
            }

        }

        public void reportarMensajesHacienda(List<tbReporteHacienda> lista)
        {
            try
            {
                if (Utility.AccesoInternet())
                {
                    foreach (var msj in lista)
                    {
                        if (msj.reporteAceptaHacienda == false)
                        {
                            enviarMensajeHacienda(msj);
                            System.Threading.Thread.Sleep(1500);
                        }


                        consultarMensaje(msj);



                    }
                }


            }
            catch (Exception ex)
            {
                clsEvento evento = new clsEvento(ex.Message, "1");
                throw ex;
            }

        }



        public string getTipoDocMensajes(string tipoResp)
        {
            string resp = string.Empty;
            if (tipoResp == "1")
            {
                resp = "05";
            }
            else if (tipoResp == "2")
            {
                resp = "06";
            }
            else if (tipoResp == "3")
            {
                resp = "07";
            }
            return resp;

        }

        private tbReporteHacienda enviarMensajeHacienda(tbReporteHacienda mensajeHacienda)
        {
            Emisor _emisor;
            Receptor _receptor = null;
            try
            {
                FacturaElectronicaCR factura = new FacturaElectronicaCR(mensajeHacienda);


                XmlDocument xml = factura.CreaXMLMensajeHacienda();
                mensajeHacienda.xmlSinFirma = Utility.EncodeStrToBase64(xml.OuterXml);

                string directorio = Global.Usuario.tbEmpresa.rutaXMLCompras.Trim();
                string nombreArchivo = mensajeHacienda.consecutivoReceptor;
                string tipoDoc = "_MS";
                //sino existe el directorio lo crea
                if (!Directory.Exists(directorio))
                {
                    Directory.CreateDirectory(directorio);
                }

                xml.Save((directorio + (nombreArchivo + tipoDoc + "_01_SF.xml")));
                XmlTextWriter xmlTextWriter = new XmlTextWriter((directorio + (nombreArchivo + tipoDoc + "_01_SF.xml")), new System.Text.UTF8Encoding(false));
                xml.WriteTo(xmlTextWriter);
                xmlTextWriter.Dispose();
                xmlTextWriter = null;
                xml = null;

                XmlDocument xmlElectronica = new XmlDocument();
                FacturacionElectronicaLayer.Clases.Firma _firma = new FacturacionElectronicaLayer.Clases.Firma();
                xmlElectronica = _firma.FirmaXML_Xades((directorio + nombreArchivo + tipoDoc), Global.Usuario.tbEmpresa.certificadoInstalado.Trim());
                mensajeHacienda.xmlFirmado = Utility.EncodeStrToBase64(xmlElectronica.OuterXml);

                //json


                FacturacionElectronicaLayer.Clases.Emisor myEmisor = new FacturacionElectronicaLayer.Clases.Emisor();
                myEmisor.numeroIdentificacion = mensajeHacienda.idEmisor.ToString().Trim();
                myEmisor.TipoIdentificacion = mensajeHacienda.tipoIdEmisor.ToString().Trim().PadLeft(2, '0');

                FacturacionElectronicaLayer.Clases.Receptor myReceptor = new FacturacionElectronicaLayer.Clases.Receptor();

                myReceptor.sinReceptor = false;
                myReceptor.numeroIdentificacion = mensajeHacienda.idEmpresa.Trim();
                myReceptor.TipoIdentificacion = mensajeHacienda.tipoIdEmpresa.ToString().Trim();

                FacturacionElectronicaLayer.Clases.RecepcionMensaje myRecepcion = new FacturacionElectronicaLayer.Clases.RecepcionMensaje();
                myRecepcion.emisor = myEmisor;
                myRecepcion.receptor = myReceptor;

                myRecepcion.clave = mensajeHacienda.claveDocEmisor;
                myRecepcion.fecha = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:sszzz");
                myRecepcion.comprobanteXml = mensajeHacienda.xmlFirmado;
                myRecepcion.consecutivoReceptor = mensajeHacienda.consecutivoReceptor;
                xmlElectronica = null;

                string Token = "";
                Token = getToken();
                //  this.txtTokenHacienda.Text = Token;

                FacturacionElectronicaLayer.Clases.Comunicacion enviaFactura = new FacturacionElectronicaLayer.Clases.Comunicacion();
                enviaFactura.EnvioMensaje(Token, myRecepcion);

         

                string jsonEnvio = "";
                jsonEnvio = enviaFactura.jsonEnvio;
                //string jsonRespuesta = "";
                //jsonRespuesta = enviaFactura.jsonRespuesta;

                System.IO.StreamWriter outputFile = new System.IO.StreamWriter((directorio
                                + (nombreArchivo + tipoDoc + "_03_jsonEnvio.txt")));
                outputFile.Write(jsonEnvio);
                outputFile.Close();

                if (enviaFactura.statusCode == "Accepted")
                {
                    mensajeHacienda.reporteAceptaHacienda = true;
                    mensajeHacienda.mensajeReporteHacienda = enviaFactura.estadoEnvio;
                    mensajeHacienda.rutaRespuestaHacienda = enviaFactura.mensajeRespuesta;
                }


                DFacturaIns.ActualizarMensaje(mensajeHacienda);
                return mensajeHacienda;
            }
            catch (Exception ex)
            {
                clsEvento evento = new clsEvento(ex.Message, "1");
                throw ex;

            }


            return null;

        }
        //public tbCompras CompraSimplificadaElectronica(tbCompras facturaGlobal)
        //{

        //    Emisor _emisor;
        //    Receptor _receptor = null;

        //    try
        //    {
        //        //_emisor: la empresa que factura, en este caso el proveedor al cual se le compró
        //        string NombreEmpresa = "";
        //        if (facturaGlobal.tbProveedores.tipoId == (int)Enums.TipoId.Juridica)
        //        {
        //            NombreEmpresa = facturaGlobal.tbProveedores.tbPersona.nombre.Trim().ToUpper();
        //        }
        //        else
        //        {

        //            NombreEmpresa = facturaGlobal.tbProveedores.tbPersona.nombre.Trim().ToUpper() + " " + facturaGlobal.tbProveedores.tbPersona.apellido1.Trim().ToUpper() + " " + facturaGlobal.tbProveedores.tbPersona.apellido2.Trim().ToUpper();
        //        }

        //        string tipoId = facturaGlobal.tbProveedores.tipoId.ToString().PadLeft(2, '0');
        //        string id = facturaGlobal.tbProveedores.id.ToString().Trim();



        //        _emisor = new Emisor(NombreEmpresa, tipoId, id, facturaGlobal.tbProveedores.tbPersona.provincia.Trim(), facturaGlobal.tbProveedores.tbPersona.canton.Trim().PadLeft(2, '0'), facturaGlobal.tbProveedores.tbPersona.distrito.Trim().PadLeft(2, '0'), facturaGlobal.tbProveedores.tbPersona.barrio.Trim().PadLeft(2, '0'), facturaGlobal.tbProveedores.tbPersona.otrasSenas.Trim().ToUpper(), facturaGlobal.tbProveedores.tbPersona.codigoPaisTel.Trim(), facturaGlobal.tbProveedores.tbPersona.telefono, facturaGlobal.tbProveedores.tbPersona.correoElectronico.Trim(), facturaGlobal.tbProveedores.codigoActividad);

        //        //recepetor  mi empres0a, en este caso es cliente             

        //        tbEmpresa _empresa = Global.Usuario.tbEmpresa;
        //        if (_empresa.tipoId == (int)Enums.TipoId.Juridica)
        //        {
        //            NombreEmpresa = _empresa.tbPersona.nombre.Trim().ToUpper();
        //        }
        //        else
        //        {

        //            NombreEmpresa = _empresa.tbPersona.nombre.Trim().ToUpper() + " " + _empresa.tbPersona.apellido1.Trim().ToUpper() + " " + _empresa.tbPersona.apellido2.Trim().ToUpper();
        //        }

        //        tipoId = _empresa.tipoId.ToString().PadLeft(2, '0');
        //        id = _empresa.id.ToString().Trim();


        //        _receptor = new Receptor(NombreEmpresa, tipoId, id, _empresa.tbPersona.provincia.Trim(), _empresa.tbPersona.canton.Trim().PadLeft(2, '0'), _empresa.tbPersona.distrito.Trim().PadLeft(2, '0'), _empresa.tbPersona.barrio.Trim().PadLeft(2, '0'), _empresa.tbPersona.otrasSenas.ToUpper(), _empresa.tbPersona.codigoPaisTel.Trim(), _empresa.tbPersona.telefono, _empresa.tbPersona.correoElectronico.Trim(), null, DateTime.MinValue, null, int.MinValue, null);


        //        List<tbTipoMedidas> medidas = medidaIns.GetListEntities(1);

        //        FacturaElectronicaCR factura = new FacturaElectronicaCR(facturaGlobal, facturaGlobal.consecutivo, facturaGlobal.clave, _emisor, _receptor, facturaGlobal.tipoCompra.ToString().Trim().PadLeft(2, '0'), facturaGlobal.plazo.ToString(), facturaGlobal.tipoPago.ToString().Trim().PadLeft(2, '0'), facturaGlobal.tbDetalleCompras, Enum.GetName(typeof(Enums.TipoMoneda), facturaGlobal.tipoMoneda).Trim(), (decimal)facturaGlobal.tipoCambio, Global.Usuario.tbEmpresa, facturaGlobal.observaciones, medidas);



        //        XmlDocument xml = factura.CreaXMLComprasSimplificadaHacienda();
        //        facturaGlobal.xmlSinFirma = Utility.EncodeStrToBase64(xml.OuterXml);

        //        string directorio = Global.Usuario.tbEmpresa.rutaCertificado.Trim();
        //        string nombreArchivo = facturaGlobal.consecutivo;
        //        string tipoDoc = Utility.getPrefixTypeDoc(facturaGlobal.tipoDoc);

        //        //sino existe el directorio lo crea
        //        if (!Directory.Exists(directorio))
        //        {
        //            Directory.CreateDirectory(directorio);
        //        }

        //        xml.Save((directorio + (nombreArchivo + tipoDoc + "_01_SF.xml")));
        //        XmlTextWriter xmlTextWriter = new XmlTextWriter((directorio + (nombreArchivo + tipoDoc + "_01_SF.xml")), new System.Text.UTF8Encoding(false));
        //        xml.WriteTo(xmlTextWriter);
        //        xmlTextWriter.Dispose();
        //        xmlTextWriter = null;
        //        xml = null;

        //        FacturacionElectronicaLayer.Clases.Firma _firma = new FacturacionElectronicaLayer.Clases.Firma();
        //        XmlDocument xmlElectronica = _firma.FirmaXML_Xades((directorio + nombreArchivo + tipoDoc), Global.Usuario.tbEmpresa.certificadoInstalado.Trim());
        //        facturaGlobal.xmlFirmado = Utility.EncodeStrToBase64(xmlElectronica.OuterXml);


        //        FacturacionElectronicaLayer.Clases.Emisor myEmisor = new FacturacionElectronicaLayer.Clases.Emisor();
        //        myEmisor.numeroIdentificacion = _empresa.id.ToString().Trim();
        //        myEmisor.TipoIdentificacion = _empresa.tipoId.ToString().Trim().PadLeft(2, '0');

        //        FacturacionElectronicaLayer.Clases.Receptor myReceptor = new FacturacionElectronicaLayer.Clases.Receptor();

        //        myReceptor.sinReceptor = false;
        //        myReceptor.numeroIdentificacion = id.Trim();
        //        myReceptor.TipoIdentificacion = tipoId.ToString().Trim();

        //        FacturacionElectronicaLayer.Clases.Recepcion myRecepcion = new FacturacionElectronicaLayer.Clases.Recepcion();
        //        myRecepcion.emisor = myEmisor;
        //        myRecepcion.receptor = myReceptor;

        //        myRecepcion.clave = facturaGlobal.clave;
        //        myRecepcion.fecha = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:sszzz");
        //        myRecepcion.comprobanteXml = facturaGlobal.xmlFirmado;

        //        xmlElectronica = null;

        //        string Token = "";
        //        Token = getToken();
        //        //  this.txtTokenHacienda.Text = Token;

        //        FacturacionElectronicaLayer.Clases.Comunicacion enviaFactura = new FacturacionElectronicaLayer.Clases.Comunicacion();
        //        enviaFactura.EnvioDatos(Token, myRecepcion);
        //        string jsonEnvio = "";
        //        jsonEnvio = enviaFactura.jsonEnvio;
        //        //string jsonRespuesta = "";
        //        //jsonRespuesta = enviaFactura.jsonRespuesta;

        //        System.IO.StreamWriter outputFile = new System.IO.StreamWriter((directorio
        //                        + (nombreArchivo + tipoDoc + "_03_jsonEnvio.txt")));
        //        outputFile.Write(jsonEnvio);
        //        outputFile.Close();


        //        if (enviaFactura.statusCode == "Accepted")
        //        {
        //            facturaGlobal.reporteAceptaHacienda = true;
        //            facturaGlobal.mensajeReporteHacienda = enviaFactura.estadoEnvio;


        //        }
        //        else
        //        {
        //            facturaGlobal.reporteAceptaHacienda = false;



        //        }
        //        facturaGlobal.mensajeRespHacienda = false;
        //        facturaGlobal = modificarCompraSImplificada(facturaGlobal);

        //        return facturaGlobal;

        //    }
        //    catch (TokenException ex)
        //    {
        //        clsEvento evento = new clsEvento(ex.Message, "1");
        //        throw ex;
        //    }
        //    catch (generarXMLException ex)
        //    {
        //        clsEvento evento = new clsEvento(ex.Message, "1");
        //        throw ex;
        //    }
        //    catch (Exception ex)
        //    {
        //        clsEvento evento = new clsEvento(ex.Message, "1");
        //        throw new FacturacionElectronicaException(ex);
        //    }

        //}
        public tbDocumento FacturarElectronicamente(tbDocumento facturaGlobal)
        {

            Emisor _emisor;
            Receptor _receptor = null;
            tbDocumento facturaEliminar = null;
            tbEmpresa _empresa = new tbEmpresa();
            _empresa.tipoId = facturaGlobal.tipoIdEmpresa;
            _empresa.id = facturaGlobal.idEmpresa;
            _empresa = empresaIns.GetEntity(_empresa);

            try
            {
                facturaGlobal = getEntity(facturaGlobal);

                if (facturaGlobal.tipoDocumento == (int)Enums.TipoDocumento.NotaCreditoElectronica ||
                    facturaGlobal.tipoDocumento == (int)Enums.TipoDocumento.NotaDebitoElectronica)
                {

                    facturaEliminar = DFacturaIns.GetEntityByClave(facturaGlobal.claveRef);
                }



                if (facturaGlobal.clave == null)
                {


                    string numeroConsecutivo = CreaNumeroConsecutivo(sucursal,
                                                                  caja,
                                                                  facturaGlobal.tipoDocumento.ToString().Trim(),
                                                                  facturaGlobal.id.ToString().Trim());
                    string codigoSeguridad = CreaCodigoSeguridad(facturaGlobal.tipoDocumento.ToString().Trim(),
                                                                    sucursal,
                                                                    caja,
                                                                    facturaGlobal.fecha,
                                                                    facturaGlobal.id.ToString().Trim());
                    string clave = CreaClave(codigoPais,
                                                facturaGlobal.fecha.Day.ToString(),
                                                facturaGlobal.fecha.Month.ToString(),
                                                facturaGlobal.fecha.Year.ToString(),
                                                Global.Usuario.tbPersona.tbEmpresa.id.Trim(),
                                                numeroConsecutivo,
                                                facturaGlobal.estadoFactura.ToString().Trim(),
                                                codigoSeguridad);




                }



                //_emisor: la empresa que factura
                string NombreEmpresa = "";
                if (_empresa.tipoId == (int)Enums.TipoId.Juridica)
                {
                    NombreEmpresa = _empresa.tbPersona.nombre.Trim().ToUpper();
                }
                else
                {

                    NombreEmpresa = _empresa.tbPersona.nombre.Trim().ToUpper() + " " + _empresa.tbPersona.apellido1.Trim().ToUpper() + " " + _empresa.tbPersona.apellido2.Trim().ToUpper();
                }

                string tipoId = _empresa.tipoId.ToString().PadLeft(2, '0');
                string id = _empresa.id.ToString().Trim();



                _emisor = new Emisor(NombreEmpresa, tipoId, id, _empresa.tbPersona.provincia.Trim(), _empresa.tbPersona.canton.Trim().PadLeft(2, '0'), _empresa.tbPersona.distrito.Trim().PadLeft(2, '0'), _empresa.tbPersona.barrio.Trim().PadLeft(2, '0'), _empresa.tbPersona.otrasSenas.Trim().ToUpper(), _empresa.tbPersona.codigoPaisTel.Trim(), _empresa.tbPersona.telefono, _empresa.tbPersona.correoElectronico.Trim(), Global.actividadEconomic.CodActividad.Trim());

                //recepetor al cliente al cual se factura


                if (facturaGlobal.tipoIdCliente != null)
                {
                    tbClientes cliente = facturaGlobal.tbClientes;

                    if (cliente.tipoId == (int)Enums.TipoId.Juridica)
                    {
                        NombreEmpresa = cliente.tbPersona.nombre.Trim().ToUpper();
                    }
                    else
                    {

                        NombreEmpresa = cliente.tbPersona.nombre.Trim().ToUpper() + " " + cliente.tbPersona.apellido1.Trim().ToUpper() + " " + cliente.tbPersona.apellido2.Trim().ToUpper();
                    }

                    tipoId = cliente.tipoId.ToString().PadLeft(2, '0');
                    id = cliente.id.ToString().Trim();

                    if ((bool)cliente.exonera)
                    {
                        _receptor = new Receptor(NombreEmpresa, tipoId, id, cliente.tbPersona.provincia.Trim(), cliente.tbPersona.canton.Trim().PadLeft(2, '0'), cliente.tbPersona.distrito.Trim().PadLeft(2, '0'), cliente.tbPersona.barrio.Trim().PadLeft(2, '0'), cliente.tbPersona.otrasSenas.ToUpper(), cliente.tbPersona.codigoPaisTel.Trim(), cliente.tbPersona.telefono, cliente.tbPersona.correoElectronico.Trim(), cliente.idExonercion.ToString(), (DateTime)cliente.FechaEmisionExo, cliente.institucionExo, (int)cliente.tbExoneraciones.valor, cliente.numeroDocumentoExo);

                    }
                    else
                    {
                        _receptor = new Receptor(NombreEmpresa, tipoId, id, cliente.tbPersona.provincia.Trim(), cliente.tbPersona.canton.Trim().PadLeft(2, '0'), cliente.tbPersona.distrito.Trim().PadLeft(2, '0'), cliente.tbPersona.barrio.Trim().PadLeft(2, '0'), cliente.tbPersona.otrasSenas.ToUpper(), cliente.tbPersona.codigoPaisTel.Trim(), cliente.tbPersona.telefono, cliente.tbPersona.correoElectronico.Trim(), null, DateTime.MinValue, null, int.MinValue, null);

                    }

                }
                FacturaElectronicaCR factura = null;

                List<tbTipoMedidas> medidas = medidaIns.GetListEntities(1);
                if (facturaGlobal.tipoDocumento == (int)Enums.TipoDocumento.NotaCreditoElectronica)
                {
                    factura = new FacturaElectronicaCR(facturaGlobal, facturaGlobal.consecutivo, facturaGlobal.clave, _emisor, _receptor, facturaEliminar.tipoVenta.ToString().Trim().PadLeft(2, '0'), facturaEliminar.plazo.ToString(), facturaEliminar.tipoPago.ToString().Trim().PadLeft(2, '0'), facturaEliminar.tbDetalleDocumento, Enum.GetName(typeof(Enums.TipoMoneda), facturaGlobal.tipoMoneda).Trim(), (decimal)facturaEliminar.tipoCambio, Global.Usuario.tbEmpresa, facturaEliminar.observaciones, medidas);

                }
                else
                {
                    factura = new FacturaElectronicaCR(facturaGlobal, facturaGlobal.consecutivo, facturaGlobal.clave, _emisor, _receptor, facturaGlobal.tipoVenta.ToString().Trim().PadLeft(2, '0'), facturaGlobal.plazo.ToString(), facturaGlobal.tipoPago.ToString().Trim().PadLeft(2, '0'), facturaGlobal.tbDetalleDocumento, Enum.GetName(typeof(Enums.TipoMoneda), facturaGlobal.tipoMoneda).Trim(), (decimal)facturaGlobal.tipoCambio, Global.Usuario.tbEmpresa, facturaGlobal.observaciones, medidas);

                }



                XmlDocument xml = factura.CreaXMLFacturaElectronica();
                facturaGlobal.xmlSinFirma = Utility.EncodeStrToBase64(xml.OuterXml);

                string directorio = Global.Usuario.tbEmpresa.rutaCertificado.Trim();
                string nombreArchivo = facturaGlobal.consecutivo;
                string tipoDoc = Utility.getPrefixTypeDoc(facturaGlobal.tipoDocumento);

                if (!Directory.Exists(directorio))
                {
                    Directory.CreateDirectory(directorio);
                }
                string rutaXml = directorio + (nombreArchivo + tipoDoc + "_01_SF.xml");
                if (File.Exists(rutaXml))
                {
                    File.Delete(rutaXml);
                }

                xml.Save(rutaXml);
                XmlTextWriter xmlTextWriter = new XmlTextWriter(rutaXml, new System.Text.UTF8Encoding(false));
                xml.WriteTo(xmlTextWriter);
                xmlTextWriter.Dispose();
                xmlTextWriter = null;
                xml = null;

                FacturacionElectronicaLayer.Clases.Firma _firma = new FacturacionElectronicaLayer.Clases.Firma();
                XmlDocument xmlElectronica = _firma.FirmaXML_Xades((directorio + nombreArchivo + tipoDoc), Global.Usuario.tbEmpresa.certificadoInstalado.Trim());
                facturaGlobal.xmlFirmado = Utility.EncodeStrToBase64(xmlElectronica.OuterXml);


                FacturacionElectronicaLayer.Clases.Emisor myEmisor = new FacturacionElectronicaLayer.Clases.Emisor();
                myEmisor.numeroIdentificacion = _empresa.id.ToString().Trim();
                myEmisor.TipoIdentificacion = _empresa.tipoId.ToString().Trim().PadLeft(2, '0');

                FacturacionElectronicaLayer.Clases.Receptor myReceptor = new FacturacionElectronicaLayer.Clases.Receptor();

                myReceptor.sinReceptor = false;
                myReceptor.numeroIdentificacion = id.Trim();
                myReceptor.TipoIdentificacion = tipoId.ToString().Trim();

                FacturacionElectronicaLayer.Clases.Recepcion myRecepcion = new FacturacionElectronicaLayer.Clases.Recepcion();
                myRecepcion.emisor = myEmisor;
                myRecepcion.receptor = myReceptor;

                myRecepcion.clave = facturaGlobal.clave;
                myRecepcion.fecha = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:sszzz");
                myRecepcion.comprobanteXml = facturaGlobal.xmlFirmado;

                xmlElectronica = null;

                string Token = "";
                Token = getToken();
                //  this.txtTokenHacienda.Text = Token;

                FacturacionElectronicaLayer.Clases.Comunicacion enviaFactura = new FacturacionElectronicaLayer.Clases.Comunicacion();
                enviaFactura.EnvioDatos(Token, myRecepcion);
                string jsonEnvio = "";
                jsonEnvio = enviaFactura.jsonEnvio;
                //string jsonRespuesta = "";
                //jsonRespuesta = enviaFactura.jsonRespuesta;

                System.IO.StreamWriter outputFile = new System.IO.StreamWriter((directorio
                                + (nombreArchivo + tipoDoc + "_03_jsonEnvio.txt")));

                if (File.Exists(nombreArchivo + tipoDoc + "_03_jsonEnvio.txt"))
                {
                    File.Delete(nombreArchivo + tipoDoc + "_03_jsonEnvio.txt");
                }


                outputFile.Write(jsonEnvio);
                outputFile.Close();


                if (enviaFactura.statusCode == "Accepted")
                {
                    facturaGlobal.reporteAceptaHacienda = true;
                    facturaGlobal.mensajeReporteHacienda = enviaFactura.estadoEnvio;


                }
                else
                {
                    facturaGlobal.reporteAceptaHacienda = false;



                }
                facturaGlobal.mensajeRespHacienda = false;
                facturaGlobal = modificar(facturaGlobal);

                return facturaGlobal;

            }
            catch (TokenException ex)
            {
                clsEvento evento = new clsEvento(ex.Message, "1");


                throw ex;
            }
            catch (generarXMLException ex)
            {
               
                clsEvento evento = new clsEvento(ex.Message, "1");
                throw ex;
            }
            catch (Exception ex)
            {
                
                clsEvento evento = new clsEvento(ex.Message, "1");
                throw new FacturacionElectronicaException(ex);
            }

        }





        private async Task<string> getTokenAsync()
        {
            try
            {
                FacturacionElectronicaLayer.Clases.TokenHacienda iTokenHacienda = new FacturacionElectronicaLayer.Clases.TokenHacienda();
                await iTokenHacienda.GetTokenHacienda(Global.Usuario.tbEmpresa.usuarioApiHacienda.Trim(), Global.Usuario.tbEmpresa.claveApiHacienda.ToString().Trim());
                return iTokenHacienda.accessToken;
            }
            catch (Exception ex)
            {
                clsEvento evento = new clsEvento(ex.Message, "1");
                throw ex;
            }
        }
        private string getToken()
        {
            try
            {
                FacturacionElectronicaLayer.Clases.TokenHacienda iTokenHacienda = new FacturacionElectronicaLayer.Clases.TokenHacienda();
                iTokenHacienda.GetTokenHacienda(Global.Usuario.tbEmpresa.usuarioApiHacienda.Trim(), Global.Usuario.tbEmpresa.claveApiHacienda.ToString().Trim());
                if (iTokenHacienda.accessToken == string.Empty)
                {
                    throw new TokenException();
                }
                return iTokenHacienda.accessToken;
            }
            catch (Exception ex)
            {
                clsEvento evento = new clsEvento(ex.Message, "1");
                throw new TokenException(ex);
            }
        }

        public tbDocumento modificar(tbDocumento facturaGlobal)
        {
            return DFacturaIns.Actualizar(facturaGlobal);
        }

        public tbCompras modificarCompraSImplificada(tbCompras facturaGlobal)
        {
            return DFacturaIns.ActualizarCompraSimplificada(facturaGlobal);
        }
        public List<tbDocumento> getListDocCreditoPendienteByCliente(int tipo, string id)
        {
            return DFacturaIns.getListDocCreditoPendienteByCliente(tipo, id.Trim());
        }

        public List<tbDocumento> getListFactPendiente()
        {
            return DFacturaIns.getListFactPendiente();
        }

        public IEnumerable<tbDocumento> getListFacturasAceptadasHacienda()
        {
            return DFacturaIns.getListFacturasAceptadasHacienda();
        }
        public IEnumerable<tbDocumento> getListAllDocumentos()
        {
            return DFacturaIns.getListAllDocumentos();
        }
        public IEnumerable<tbDocumento> getListAllDocumentosBySearch()
        {
            return DFacturaIns.getListAllDocumentosBySearch();
        }
        public async Task<IEnumerable<tbDocumento>> getListAllDocumentosBySearchAsync()
        {
            return await DFacturaIns.getListAllDocumentosBySearchAsyc();
        }
        public async Task<IEnumerable<tbDocumento>> getListAllDocumentosDateBySearchAsync(DateTime fechaInicio, DateTime fechaFin)
        {
            return await DFacturaIns.getListAllDocumentosDateBySearchAsyc(fechaInicio, fechaFin);
        }

        public IEnumerable<tbDocumento> getListProformasGenerales()
        {
            return DFacturaIns.getListProformasGenerales();
        }
        public tbDocumento getProformasByID(int id, int tipo)
        {
            return DFacturaIns.getProformasById(id, tipo);
        }
        public IEnumerable<tbDocumento> getListProformas()
        {
            return DFacturaIns.getListProformas();
        }

        public IEnumerable<tbDocumento> getDocsByNoTrack( int sucursal, int caja, DateTime fechaApertura, DateTime fechaCierre)
        {
            return DFacturaIns.getDocsByNoTrack( sucursal, caja, fechaApertura, fechaCierre);
        }

        public IEnumerable<tbDocumento> getVentasByFechaNoTrack(DateTime fecha, int sucursal, int caja)
        {
            return DFacturaIns.getVentasByFechaNoTrack(fecha, sucursal, caja);
        }

        public IEnumerable<tbDocumento> getVentasByFecha(DateTime fecha, int sucursal, int caja)
        {
            return DFacturaIns.getVentasByFecha(fecha, sucursal, caja);
        }
        public IEnumerable<tbDocumento> getNCByFecha(DateTime fecha, int sucursal, int caja)
        {
            return DFacturaIns.getNCByFecha(fecha, sucursal, caja);
        }

        public IEnumerable<tbDocumento> getNCByFechaAsNotTracking(DateTime fecha, int sucursal, int caja)
        {
            return DFacturaIns.getNCByFechaAsNotTracking(fecha, sucursal, caja);
        }


        public IEnumerable<tbDocumento> getNCByRef(string referen)
        {
            return DFacturaIns.getNCByRef(referen);
        }

        public IEnumerable<tbPagos> getAbonosByFecha(DateTime fecha, int tipoPago, int sucursal, int caja)
        {
            return DFacturaIns.getAbonosByFecha(fecha, tipoPago, sucursal, caja);
        }

        public IEnumerable<tbPagos> getAbonosByFechaAsNotTracking(DateTime fechaInicio,DateTime fechaFin, int sucursal, int caja)
        {
            return DFacturaIns.getAbonosByFechaAsNotTracking(fechaInicio, fechaFin, sucursal, caja);
        }
        public IEnumerable<tbPagos> getAbonosByFecha(DateTime fecha,  int sucursal, int caja)
        {
            return DFacturaIns.getAbonosByFecha(fecha, sucursal, caja);
        }


        public static string CreaNumeroConsecutivo(string CasaMatriz, string TerminalPOS, string TipoComprobante, string NumeroFactura)
        {
            // 'CasaMatriz debe de se de tres caracteres m�ximo
            // 'Terminal debe ser máximo 5 cataracteres
            // 'Tipo comprobante dos caracteres
            // 'NumeroFactura diez caracteres, si se llega al numero máximo, comienza de 1 nuevamente
            try
            {
                if ((CasaMatriz.Trim().Length > 3))
                {
                    throw new Exception("Casa Matiz no debe de superar los 3 caracteres");
                }

                if ((TerminalPOS.Trim().Length > 5))
                {
                    throw new Exception("Numero de terminal no debe de superar los 5 caracteres");
                }

                if ((TipoComprobante.Trim().Length > 2))
                {
                    throw new Exception("Tipo Comprobante no debe de superar los 2 caracteres");
                }

                if ((NumeroFactura.Trim().Length > 10))
                {
                    throw new Exception("Numero Factura no debe de superar los 10 caracteres");
                }

                string NumeroSecuencia = "";
                NumeroSecuencia = CasaMatriz.Trim().PadLeft(3, '0');
                NumeroSecuencia = (NumeroSecuencia + TerminalPOS.Trim().PadLeft(5, '0'));
                NumeroSecuencia = (NumeroSecuencia + TipoComprobante.Trim().PadLeft(2, '0'));
                NumeroSecuencia = (NumeroSecuencia + NumeroFactura.Trim().PadLeft(10, '0'));
                if ((NumeroSecuencia.Trim().Length < 20))
                {
                    throw new Exception("Numero de secuencia inválido, debe tener 20 caracteres");
                }

                return NumeroSecuencia;
            }
            catch (Exception ex)
            {
                clsEvento evento = new clsEvento(ex.Message, "1");
                throw ex;
            }

        }
        private static string CreaNumeroConsecutivoMensaje(string CasaMatriz, string TerminalPOS, string tipoResp, string NumeroFactura)
        {
            // 'CasaMatriz debe de se de tres caracteres m�ximo
            // 'Terminal debe ser máximo 5 cataracteres
            // 'Tipo comprobante dos caracteres
            // 'NumeroFactura diez caracteres, si se llega al numero máximo, comienza de 1 nuevamente
            try
            {
                string resp = string.Empty;
                if (tipoResp == "1")
                {
                    resp = "05";
                }
                else if (tipoResp == "2")
                {
                    resp = "06";
                }
                else if (tipoResp == "3")
                {
                    resp = "07";
                }



                if ((CasaMatriz.Trim().Length > 3))
                {
                    throw new Exception("Casa Matiz no debe de superar los 3 caracteres");
                }

                if ((TerminalPOS.Trim().Length > 5))
                {
                    throw new Exception("Numero de terminal no debe de superar los 5 caracteres");
                }
                if ((resp.Trim().Length > 2))
                {
                    throw new Exception("Numero de respuesta de no debe de supera los 2 caracteres");
                }

                if ((NumeroFactura.Trim().Length > 10))
                {
                    throw new Exception("Numero Factura no debe de superar los 10 caracteres");
                }

                string NumeroSecuencia = "";
                NumeroSecuencia = CasaMatriz.Trim().PadLeft(3, '0');
                NumeroSecuencia = (NumeroSecuencia + TerminalPOS.Trim().PadLeft(5, '0'));
                NumeroSecuencia = (NumeroSecuencia + resp.Trim().PadLeft(2, '0'));
                NumeroSecuencia = (NumeroSecuencia + NumeroFactura.Trim().PadLeft(10, '0'));
                if ((NumeroSecuencia.Trim().Length < 20))
                {
                    throw new Exception("Numero de secuencia inválido, debe tener 20 caracteres");
                }

                return NumeroSecuencia;
            }
            catch (Exception ex)
            {
                clsEvento evento = new clsEvento(ex.Message, "1");
                throw ex;
            }

        }

        public static string CreaClave(string CodigoPais, string Dia, string Mes, string Anno, string NumeroIdentifiaccion, string NumeracionConsecutiva, string SituacionComprobante, string CodigoSeguridad)
        {
            // 'CodigoPais tres caracteres 
            // 'Dia y Mes dos caracteres
            // 'Año dos caracteres
            // 'Numero idenficacion es el numero de cedula del _emisor, 12 caracteres máximo
            // 'Numero consecutivo 20 caracteres, generados en la funcion CreaNumeroSecuencia
            // 'Situacion comprobante un caracter, 1.Normal 2.Contingencia 3.Sin Internet
            // 'Codigo Seguridad 8 caracteres generado con la funci�n CreaCodigoSeguridad
            try
            {
                if (Anno.Trim().Length == 4)
                    Anno = Anno.Substring(2, 2);

                if ((CodigoPais.Trim().Length != 3))
                {
                    throw new Exception("Codigo país  debe tener 3 caracteres");
                }

                if ((Dia.Trim().Length > 2))
                {
                    throw new Exception("Día no debe de superar los 2 caracteres");
                }

                if ((Mes.Trim().Length > 2))
                {
                    throw new Exception("Mes no debe de superar los 2 caracteres");
                }

                if ((Anno.Trim().Length > 2))
                {
                    throw new Exception("Año no debe de superar los 2 caracteres");
                }

                if ((NumeroIdentifiaccion.Trim().Length > 12))
                {
                    throw new Exception("Número Identifiacción de superar los 12 caracteres");
                }

                if ((NumeracionConsecutiva.Trim().Length != 20))
                {
                    throw new Exception("Numero Consecutivo  debe tener 20 caracteres");
                }

                if ((SituacionComprobante.Trim().Length > 1))
                {
                    throw new Exception("Situacion Comprobante debe tener un caracter");
                }

                if ((CodigoSeguridad.Trim().Length > 8))
                {
                    throw new Exception("Código seguridad no debe de superar los 8 caracteres");
                }

                string Clave = "";
                Clave = CodigoPais;
                Clave = (Clave + Dia.PadLeft(2, '0'));
                Clave = (Clave + Mes.PadLeft(2, '0'));
                Clave = (Clave + Anno.PadLeft(2, '0'));
                Clave = (Clave + NumeroIdentifiaccion.PadLeft(12, '0'));
                Clave = (Clave + NumeracionConsecutiva);
                Clave = (Clave + SituacionComprobante);
                Clave = (Clave + CodigoSeguridad);
                if ((Clave.Length != 50))
                {
                    throw new Exception("Clave inválida, debe de tener 50 caracteres");
                }

                return Clave;
            }
            catch (Exception ex)
            {
                clsEvento evento = new clsEvento(ex.Message, "1");
                throw ex;
            }

        }

        public static string CreaCodigoSeguridad(string TipoComprobante, string Localidad,
                                         string CodigoPuntoVenta, DateTime Fecha,
                                         string NumeroFactura)
        {
            // 'Los parametros TipoComprobante, Localidad y CodigoPuntoVenta pueden modificarse por otros valores, siempre manteniendo la longitud
            // 'Tipo Comprobante debe tener 2 caracteres máximo
            // 'Localidad debe tener 3 caracteres máximo
            // 'Punto de Venta debe de tener 5 caracteres máximo
            // 'Fecha es un campo datetime, debe ser la fecha de la factura
            // 'Número Factura debe tener máximo 10 caracteres y debe ser el mismo parámetro que se usa en la funcion GeneraNumeroSecuencia
            try
            {
                if ((TipoComprobante.Trim().Length > 2))
                {
                    throw new Exception("Tipo Comprobante debe tener 2 caracteres");
                }

                if ((Localidad.Trim().Length > 3))
                {
                    throw new Exception("Localidad no debe de superar los 3 caracteres");
                }

                if ((CodigoPuntoVenta.Trim().Length > 5))
                {
                    throw new Exception("Codigo Punto Venta no debe de superar los 5 caracteres");
                }

                if ((NumeroFactura.Trim().Length > 10))
                {
                    throw new Exception("Numero Factura no de superar los 10 caracteres");
                }

                string concatenado = "";
                concatenado = (concatenado + TipoComprobante.PadLeft(2, '0'));
                concatenado = (concatenado + Localidad.PadLeft(3, '0'));
                concatenado = (concatenado + CodigoPuntoVenta.PadLeft(5, '0'));
                concatenado = (concatenado + Fecha.ToString("yyyyMMddHHmmss"));
                concatenado = (concatenado + NumeroFactura.PadLeft(10, '0'));
                if ((concatenado.Length != 34))
                {
                    throw new Exception("El concatenado debe de ser de 34 caracteres para poder calcular el código de seguridad");
                }

                int calculo = 0;
                calculo = (calculo
                            + (int.Parse(concatenado.Substring(0, 1)) * 3));
                calculo = (calculo
                            + (int.Parse(concatenado.Substring(1, 1)) * 2));
                calculo = (calculo
                            + (int.Parse(concatenado.Substring(2, 1)) * 9));
                calculo = (calculo
                            + (int.Parse(concatenado.Substring(3, 1)) * 8));
                calculo = (calculo
                            + (int.Parse(concatenado.Substring(4, 1)) * 7));
                calculo = (calculo
                            + (int.Parse(concatenado.Substring(5, 1)) * 6));
                calculo = (calculo
                            + (int.Parse(concatenado.Substring(6, 1)) * 5));
                calculo = (calculo
                            + (int.Parse(concatenado.Substring(7, 1)) * 4));
                calculo = (calculo
                            + (int.Parse(concatenado.Substring(8, 1)) * 3));
                calculo = (calculo
                            + (int.Parse(concatenado.Substring(9, 1)) * 2));
                calculo = (calculo
                            + (int.Parse(concatenado.Substring(10, 1)) * 9));
                calculo = (calculo
                            + (int.Parse(concatenado.Substring(11, 1)) * 8));
                calculo = (calculo
                            + (int.Parse(concatenado.Substring(12, 1)) * 7));
                calculo = (calculo
                            + (int.Parse(concatenado.Substring(13, 1)) * 6));
                calculo = (calculo
                            + (int.Parse(concatenado.Substring(14, 1)) * 5));
                calculo = (calculo
                            + (int.Parse(concatenado.Substring(15, 1)) * 4));
                calculo = (calculo
                            + (int.Parse(concatenado.Substring(16, 1)) * 3));
                calculo = (calculo
                            + (int.Parse(concatenado.Substring(17, 1)) * 2));
                calculo = (calculo
                            + (int.Parse(concatenado.Substring(18, 1)) * 9));
                calculo = (calculo
                            + (int.Parse(concatenado.Substring(19, 1)) * 8));
                calculo = (calculo
                            + (int.Parse(concatenado.Substring(20, 1)) * 7));
                calculo = (calculo
                            + (int.Parse(concatenado.Substring(21, 1)) * 6));
                calculo = (calculo
                            + (int.Parse(concatenado.Substring(22, 1)) * 5));
                calculo = (calculo
                            + (int.Parse(concatenado.Substring(23, 1)) * 4));
                calculo = (calculo
                            + (int.Parse(concatenado.Substring(24, 1)) * 3));
                calculo = (calculo
                            + (int.Parse(concatenado.Substring(25, 1)) * 2));
                calculo = (calculo
                            + (int.Parse(concatenado.Substring(26, 1)) * 9));
                calculo = (calculo
                            + (int.Parse(concatenado.Substring(27, 1)) * 8));
                calculo = (calculo
                            + (int.Parse(concatenado.Substring(28, 1)) * 7));
                calculo = (calculo
                            + (int.Parse(concatenado.Substring(29, 1)) * 6));
                calculo = (calculo
                            + (int.Parse(concatenado.Substring(30, 1)) * 5));
                calculo = (calculo
                            + (int.Parse(concatenado.Substring(31, 1)) * 4));
                calculo = (calculo
                            + (int.Parse(concatenado.Substring(32, 1)) * 3));
                calculo = (calculo
                            + (int.Parse(concatenado.Substring(33, 1)) * 2));
                int mDV = 0;
                int digitoMod = 0;
                digitoMod = (calculo % 11);
                if (((digitoMod == 0)
                            || (digitoMod == 1)))
                {
                    mDV = 0;
                }
                else
                {
                    mDV = (11 - digitoMod);
                }

                return (TipoComprobante.PadLeft(2, '0')
                            + (calculo.ToString().PadLeft(5, '0') + mDV.ToString()));
            }
            catch (Exception ex)
            {
                clsEvento evento = new clsEvento(ex.Message, "1");
                throw ex;
            }

        }
        public async Task<String> consultarFacturaElectronicaPorIdFact(int id, int tipo)
        {
            var doc = DFacturaIns.getById(id, tipo);
            if (doc != null)
            {
                return await consultarFacturaElectronicaPorClave(doc.clave);

            }
            return "Sin respuesta, puede que la ID del documento no sea válida.";
        }
        public string consultarMensajePorIdFact(int id)
        {
            var msj = DFacturaIns.GetMensajeById(id);
            if (msj != null)
            {
                if (!msj.reporteAceptaHacienda)
                {
                    enviarMensajeHacienda(msj);
                }


                if (msj.rutaRespuestaHacienda != null)
                {
                    return consultarMensaje(msj);
                }


            }
            return "Sin respuesta, puede que la ID del documento no sea válida.";
        }


        public string consultarFacturaElectronicaPorIdFact(tbDocumento entity)
        {
            return  consultarFacturaElectronicaPorClave(string.Empty, entity).Result;
        }
        public async Task<String> consultarFacturaElectronicaPorClave(string clave)
        {

            return  consultarFacturaElectronicaPorClave(clave, null).Result;
        }

        public async Task<String> consultarFacturaElectronicaPorConsecutivoAsync(string consec)
        {
            var doc = DFacturaIns.getByConsecutivo(consec);
            if (doc != null)
            {
                return  await consultarFacturaElectronicaPorClave(doc.clave);

            }
            return "Sin respuesta, puede que la ID del documento no sea válida.";


        }
        private async Task<String> consultarFacturaElectronicaPorClave(string clave, tbDocumento doc)
        {
   
            string mensaje = "";

            try
            {
                if(doc!=null && clave == string.Empty)
                {
                   
                    if (clave == string.Empty)
                    {

                        doc = getEntity(doc);

                    }
                    else
                    {
                        doc = new tbDocumento();
                        doc.clave = clave;
                        doc = getEntityByClave(doc);
                    }
                    if (doc != null && doc.clave != null)
                    {
                        clave = doc.clave.ToString().Trim();
                    }

                }
                else
                {
                    doc = new tbDocumento();
                    doc.clave = clave;
                    doc = getEntityByClave(doc);

                }

                string directorio = Global.Usuario.tbEmpresa.rutaCertificado.Trim();

               

                if (clave != string.Empty)
                {
                    string tipoDod = Utility.getPrefixTypeDoc(doc.tipoDocumento);
                    //obtengo respuesto y guardo el xml de respuesta en la carpeta correspondiente
                    string Token = "";
                    Token = getToken();
                    FacturacionElectronicaLayer.Clases.Comunicacion factura = new FacturacionElectronicaLayer.Clases.Comunicacion();

                    try
                    {
                        mensaje= await factura.ConsultaEstatusComprobante(Token, clave);
                       
                    }
                    catch (Exception)
                    {

                        mensaje = "Sin respuesta, Error al consultar Estatus de documento.";
                    }
                    //cierro sesion hacienda
                    //factura.CerrarSesion(Token);

                    if (factura.statusCode == null ||factura.statusCode == "BadRequest" || factura.mensajeRespuesta=="error")
                    {
                        mensaje = "Sin respuesta, puede que la ID del documento no sea válida.";
                    }
                    else
                    {
                        String mensajeDetalle = clsMensajeXMLHaciendaRespuesta.FormatearMensaje(factura.xmlRespuesta.InnerXml);
                        mensaje = string.Format("Estado Factura: {1}{0}Mensaje Hacienda:{0}{2}", Environment.NewLine, factura.mensajeRespuesta, factura.xmlRespuesta == null ? "Sin respuesta" : mensajeDetalle);



                        var estadoFactura = factura.estadoFactura == null ? "" : factura.estadoFactura;
                        if (factura.statusCode != "BadRequest" && (doc.EstadoFacturaHacienda == null || doc.EstadoFacturaHacienda.Trim().ToUpper() != estadoFactura.Trim().ToUpper()))
                        {
                            string jsonRespuesta = "";
                            string consecutivo = doc.consecutivo.Trim();
                            jsonRespuesta = factura.jsonRespuesta;


                            System.IO.StreamWriter outputFile = new System.IO.StreamWriter((directorio
                                            + (consecutivo + tipoDod + "_04_jsonRespuesta.txt")));
                            outputFile.Write(jsonRespuesta);
                            outputFile.Close();


                            if (!(factura.xmlRespuesta == null))
                            {
                                doc.xmlRespuesta = factura.xmlCodificado;
                                factura.xmlRespuesta.Save((directorio
                                                + (consecutivo + tipoDod + "_05_RESP.xml")));

                            }
                            else
                            {
                                outputFile = new System.IO.StreamWriter((directorio
                                                + (consecutivo + tipoDod + "_05_RESP_SinRespuesta.txt")));
                                outputFile.Write("");
                                outputFile.Close();


                            }

                            if (factura.statusCode == "OK")
                            {
                                doc.reporteAceptaHacienda = true;
                                doc.mensajeReporteHacienda = "Accepted";
                                doc.mensajeRespHacienda = true;
                                doc.EstadoFacturaHacienda = factura.estadoFactura;

                                doc = modificar(doc);


                                mensaje = string.Format("Estado Factura: {1}{0}Mensaje Hacienda:{0}{2}", Environment.NewLine, factura.mensajeRespuesta, factura.xmlRespuesta == null ? "Sin respuesta" : factura.xmlRespuesta.InnerText);


                            }

                        }



                    }






                    //if (!doc.reporteAceptaHacienda)
                    //{
                    //    try
                    //    {
                    //        doc= FacturarElectronicamente(doc);
                    //        mensaje = string.Format("Estado Factura: {1}{0}Mensaje Hacienda:{0}{2}", Environment.NewLine, doc.mensajeReporteHacienda, doc.EstadoFacturaHacienda == null ? "Sin respuesta" : doc.EstadoFacturaHacienda);

                    //    }
                    //    catch (Exception ex)
                    //    {

                    //           clsEvento evento = new clsEvento(ex.Message, "1");
                    //           throw ex;    

                    //    }

                    //}


                }
                if (mensaje == string.Empty)
                {
                    mensaje = "Sin respuesta!";
                }
                return mensaje;
            }
            catch (Exception ex)
            {
                //clsEvento evento = new clsEvento(ex.Message, "1");
                mensaje = "Error al consultar Hacienda!";
            }

            return mensaje;


        }

        //public string consultarCompraSimplificada(tbCompras doc)
        //{

        //    string mensaje = "";
        //    string clave = doc.clave;
        //    try
        //    {

        //        string directorio = Global.Usuario.tbEmpresa.rutaCertificado.Trim();

        //        if (doc != null)
        //        {
        //            if (!doc.reporteAceptaHacienda)
        //            {
        //                try
        //                {
        //                    CompraSimplificadaElectronica(doc);
        //                }
        //                catch (Exception)
        //                {

        //                    throw;
        //                }

        //            }


        //        }
        //        if (doc != null && clave != string.Empty && doc.reporteAceptaHacienda == true)
        //        {
        //            string tipoDod = Utility.getPrefixTypeDoc(doc.tipoDoc);
        //            //obtengo respuesto y guardo el xml de respuesta en la carpeta correspondiente
        //            string Token = "";
        //            Token = getToken();
        //            FacturacionElectronicaLayer.Clases.Comunicacion factura = new FacturacionElectronicaLayer.Clases.Comunicacion();

        //            try
        //            {
        //                factura.ConsultaEstatus(Token, clave);
        //            }
        //            catch (Exception ex)
        //            {

        //                throw ex;
        //            }



        //            string jsonRespuesta = "";
        //            string consecutivo = doc.consecutivo.Trim();
        //            jsonRespuesta = factura.jsonRespuesta;


        //            System.IO.StreamWriter outputFile = new System.IO.StreamWriter((directorio
        //                            + (consecutivo + tipoDod + "_04_jsonRespuesta.txt")));
        //            outputFile.Write(jsonRespuesta);
        //            outputFile.Close();


        //            if (!(factura.xmlRespuesta == null))
        //            {
        //                doc.xmlRespuesta = factura.xmlCodificado;
        //                factura.xmlRespuesta.Save((directorio
        //                                + (consecutivo + tipoDod + "_05_RESP.xml")));

        //            }
        //            else
        //            {
        //                outputFile = new System.IO.StreamWriter((directorio
        //                                + (consecutivo + tipoDod + "_05_RESP_SinRespuesta.txt")));
        //                outputFile.Write("");
        //                outputFile.Close();


        //            }

        //            if (factura.statusCode == "OK")
        //            {
        //                doc.reporteAceptaHacienda = true;
        //                doc.mensajeReporteHacienda = "Accepted";
        //                doc.mensajeRespHacienda = true;
        //                doc.EstadoFacturaHacienda = factura.estadoFactura;
        //                //doc.
        //                doc = modificarCompraSImplificada(doc);

        //                if (true)
        //                {

        //                }
        //                mensaje = string.Format("Estado Factura: {1}{0}Mensaje Hacienda:{0}{2}", Environment.NewLine, factura.mensajeRespuesta, factura.xmlRespuesta == null ? "Sin respuesta" : factura.xmlRespuesta.InnerText);


        //            }

        //        }
        //        else
        //        {
        //            mensaje = "Sin respuesta, puede que la ID del documento no sea válida.";
        //        }


        //        if (mensaje == string.Empty)
        //        {
        //            mensaje = "Sin respuesta, puede que la ID del documento no sea válida.";
        //        }
        //        return mensaje;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new ConsultaHaciendaExcpetion(ex);
        //    }

        //    return mensaje;


        //}
        public tbReporteHacienda consultarMensajePorClave(string clave)
        {

            return DFacturaIns.GetMensajeByClave(clave);
        }

        public tbReporteHacienda consultarMensajePorConsecutivo(string consecutivo)
        {

            return DFacturaIns.GetMensajeByConsecutivo(consecutivo);
        }

        public tbReporteHacienda consultarMensajePorId(int id)
        {

            return DFacturaIns.GetMensajeById(id);
        }
        public string consultarMensaje(tbReporteHacienda msj)
        {
            string mensaje = string.Empty;
            try
            {
                string tipoDod = "_MS";
                //obtengo respuesto y guardo el xml de respuesta en la carpeta correspondiente
                string Token = "";
                Token = getToken();
                FacturacionElectronicaLayer.Clases.Comunicacion factura = new FacturacionElectronicaLayer.Clases.Comunicacion();

                try
                {
                    factura.ConsultaEstatusMensajes(Token, msj.rutaRespuestaHacienda);
                }
                catch (Exception ex)
                {

                    throw ex;
                }

                factura.CerrarSesion(Token);
                string directorio = Global.Usuario.tbEmpresa.rutaXMLCompras.Trim();
                string jsonRespuesta = "";
                string consecutivo = msj.consecutivoReceptor.Trim();
                jsonRespuesta = factura.jsonRespuesta;


                System.IO.StreamWriter outputFile = new System.IO.StreamWriter((directorio
                                + (consecutivo + tipoDod + "_04_jsonRespuesta.txt")));
                outputFile.Write(jsonRespuesta);
                outputFile.Close();


                if (!(factura.xmlRespuesta == null))
                {
                    msj.xmlRespuesta = factura.xmlCodificado;
                    factura.xmlRespuesta.Save((directorio
                                    + (consecutivo + tipoDod + "_05_RESP.xml")));

                }
                else
                {
                    outputFile = new System.IO.StreamWriter((directorio
                                    + (consecutivo + tipoDod + "_05_RESP_SinRespuesta.txt")));
                    outputFile.Write("");
                    outputFile.Close();


                }
                if (factura.statusCode == "OK")
                {
                    msj.mensajeRespHacienda = true;
                    msj.EstadoRespHacienda = factura.estadoFactura;

                }
                mensaje = string.Format("Estado Mensaje: {1}{0}Mensaje Hacienda:{0}{2}", Environment.NewLine, factura.mensajeRespuesta, factura.xmlRespuesta == null ? "Sin respuesta" : factura.xmlRespuesta.InnerText);


                DFacturaIns.ActualizarMensaje(msj);




            }
            catch (Exception ex)
            {

                mensaje = "Error al consultar verifique los datos.";
            }

            return mensaje;
        }

        //private string consultarCompraSimplificadoPorClave(string clave, tbCompras doc)
        //{

        //    string mensaje = "";

        //    try
        //    {

        //        string directorio = Global.Usuario.tbEmpresa.rutaCertificado.Trim();

        //        if (doc != null)
        //        {
        //            if (!doc.reporteAceptaHacienda)
        //            {
        //                try
        //                {
        //                    CompraSimplificadaElectronica(doc);
        //                }
        //                catch (Exception)
        //                {

        //                    throw;
        //                }

        //            }


        //        }
        //        if (doc != null && clave != string.Empty && doc.reporteAceptaHacienda == true)
        //        {
        //            string tipoDod = Utility.getPrefixTypeDoc(doc.tipoDoc);
        //            //obtengo respuesto y guardo el xml de respuesta en la carpeta correspondiente
        //            string Token = "";
        //            Token = getToken();
        //            FacturacionElectronicaLayer.Clases.Comunicacion factura = new FacturacionElectronicaLayer.Clases.Comunicacion();

        //            try
        //            {
        //                factura.ConsultaEstatus(Token, clave);
        //            }
        //            catch (Exception ex)
        //            {

        //                throw ex;
        //            }



        //            string jsonRespuesta = "";
        //            string consecutivo = doc.consecutivo.Trim();
        //            jsonRespuesta = factura.jsonRespuesta;


        //            System.IO.StreamWriter outputFile = new System.IO.StreamWriter((directorio
        //                            + (consecutivo + tipoDod + "_04_jsonRespuesta.txt")));
        //            outputFile.Write(jsonRespuesta);
        //            outputFile.Close();


        //            if (!(factura.xmlRespuesta == null))
        //            {
        //                doc.xmlRespuesta = factura.xmlCodificado;
        //                factura.xmlRespuesta.Save((directorio
        //                                + (consecutivo + tipoDod + "_05_RESP.xml")));

        //            }
        //            else
        //            {
        //                outputFile = new System.IO.StreamWriter((directorio
        //                                + (consecutivo + tipoDod + "_05_RESP_SinRespuesta.txt")));
        //                outputFile.Write("");
        //                outputFile.Close();


        //            }

        //            if (factura.statusCode == "OK")
        //            {
        //                doc.reporteAceptaHacienda = true;
        //                doc.mensajeReporteHacienda = "Accepted";
        //                doc.mensajeRespHacienda = true;
        //                doc.EstadoFacturaHacienda = factura.estadoFactura;
        //                //doc.
        //                doc = modificarCompraSImplificada(doc);

        //                if (true)
        //                {

        //                }
        //                mensaje = string.Format("Estado Factura: {1}{0}Mensaje Hacienda:{0}{2}", Environment.NewLine, factura.mensajeRespuesta, factura.xmlRespuesta == null ? "Sin respuesta" : factura.xmlRespuesta.InnerText);


        //            }

        //        }
        //        else
        //        {
        //            mensaje = "Sin respuesta, puede que la ID del documento no sea válida.";
        //        }


        //        if (mensaje == string.Empty)
        //        {
        //            mensaje = "Sin respuesta, puede que la ID del documento no sea válida.";
        //        }
        //        return mensaje;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new ConsultaHaciendaExcpetion(ex);
        //    }

        //    return mensaje;


        //}


        public List<tbDocumento> validarDocumentosDiarias()
        { 
            List<tbDocumento> listProcesadas = new List<tbDocumento>();
           
            try
            {
         

                IEnumerable<DocumentoHaciendaDTO> facturasLista = listaFacturasInconsistentes();           

                foreach (var doc in facturasLista)
                {
                    try
                    {
                        if(!doc.reporteAceptaHacienda)
                        {
                            tbDocumento documento = (tbDocumento)getEntityByKey(doc.id, doc.tipoDocumento);
                            FacturarElectronicamente(documento);

                        }
                        else
                        {
                            consultarFacturaElectronicaPorClave(doc.clave);
                        }
                  

                        //var document = getEntity(doc);

                        //if ((bool)document.mensajeRespHacienda && document.mensajeReporteHacienda.Trim().ToUpper().Equals("ACCEPTED"))
                        //{

                        //    listProcesadas.Add(document);
                        //}

                    }
                    catch (Exception ex)
                    {
                    

                    }

                }

            }
            catch (Exception ex)
            {
                throw ex;
                
            }
            return listProcesadas;
        }

      

        public List<tbDocumento> docsCorreoPendietes()
        {
            

            try
            {


                return (List<tbDocumento>)DFacturaIns.getDocsCorreosPendientes();


            }
            catch (Exception ex)
            {
                throw ex;

            }
          
        }

    }
}
