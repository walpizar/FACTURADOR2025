using System.Xml;

namespace FacturacionElectronicaLayer.ClasesDatos
{
    using CommonLayer;
    using CommonLayer.Exceptions.BussinessExceptions;
    using EntityLayer;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

    public class FacturaElectronicaCR
    {


        private System.IO.MemoryStream mXML;
        private tbDocumento _doc = null;
        private tbCompras _compras = null;
        private string _numeroConsecutivo = "";
        private string _numeroClave = "";
        private Emisor _emisor;
        private Receptor _receptor;
        private string _condicionVenta = "";
        private string _plazoCredito = "";
        private string _medioPago = "";
        private ICollection<tbDetalleDocumento> _listaDetalle;
        private ICollection<tbDetalleCompras> _listaDetalleCompras;
        private string _codigoMoneda = "";
        private decimal _tipoCambio;
        private string _Obsv;
        private tbEmpresa _empresa;
        private tbReporteHacienda _mensajeHacienda;
        private ICollection<tbTipoMedidas> _listaMedidas;


        public FacturaElectronicaCR(tbReporteHacienda mensaje)
        {

            _mensajeHacienda = mensaje;


        }


        public FacturaElectronicaCR(tbDocumento doc, string numeroConsecutivo, string numeroClave, Emisor emisor, Receptor receptor,
                                    string condicionVenta, string plazoCredito, string medioPago,
                                    ICollection<tbDetalleDocumento> listaDetalle, string codigoMoneda, decimal tipoCambio, tbEmpresa empresa, string obsv, ICollection<tbTipoMedidas> listaMedida)
        {
            _doc = doc;
            _numeroConsecutivo = numeroConsecutivo;
            _numeroClave = numeroClave;
            _emisor = emisor;
            _receptor = receptor;
            _condicionVenta = condicionVenta;
            _plazoCredito = plazoCredito;
            _medioPago = medioPago;
            _listaDetalle = listaDetalle;
            _codigoMoneda = codigoMoneda;
            _tipoCambio = tipoCambio;
            _empresa = empresa;
            _Obsv = obsv;
            _listaMedidas = listaMedida;

        }

        public FacturaElectronicaCR(tbCompras compras, string numeroConsecutivo, string numeroClave, Emisor emisor, Receptor receptor,
                                    string condicionVenta, string plazoCredito, string medioPago,
                                    ICollection<tbDetalleCompras> listaDetalle, string codigoMoneda, decimal tipoCambio, tbEmpresa empresa, string obsv, ICollection<tbTipoMedidas> listaMedida)
        {
            _compras = compras;
            _numeroConsecutivo = numeroConsecutivo;
            _numeroClave = numeroClave;
            _emisor = emisor;
            _receptor = receptor;
            _condicionVenta = condicionVenta;
            _plazoCredito = plazoCredito;
            _medioPago = medioPago;
            _listaDetalleCompras = listaDetalle;
            _codigoMoneda = codigoMoneda;
            _tipoCambio = tipoCambio;
            _empresa = empresa;
            _Obsv = obsv;
            _listaMedidas = listaMedida;

        }



        // 'Segun la normativa, estos son los datos basicos que seguramente van a ocupar,
        // 'Es posible que algunos no los ocupen y requieran otros, es normal y los veremos conforme vayamos 
        // 'desarrollando la factura. Cuando se envien los datos a Hacienda, ahi seguramente nos daremos cuenta en las pruebas

        public XmlDocument CreaXMLComprasSimplificadaHacienda()
        {
            try
            {
                mXML = new System.IO.MemoryStream();

                System.Xml.XmlTextWriter writer = new System.Xml.XmlTextWriter(mXML, System.Text.Encoding.UTF8);

                XmlDocument docXML = new XmlDocument();

                GeneraXMLComprasSimplificadoElectronica4_3(writer);

                mXML.Seek(0, System.IO.SeekOrigin.Begin);

                docXML.Load(mXML);

                writer.Close();

                // Retorna el documento xml y ahi se puede salvar docXML.Save
                return docXML;
            }
            catch (Exception ex)
            {
                throw new generarXMLException(ex);
            }
        }

        public XmlDocument CreaXMLMensajeHacienda()
        {
            try
            {
                mXML = new System.IO.MemoryStream();

                System.Xml.XmlTextWriter writer = new System.Xml.XmlTextWriter(mXML, System.Text.Encoding.UTF8);

                XmlDocument docXML = new XmlDocument();

                GeneraXMLReceptorHacienda4_3(writer);

                mXML.Seek(0, System.IO.SeekOrigin.Begin);

                docXML.Load(mXML);

                writer.Close();

                // Retorna el documento xml y ahi se puede salvar docXML.Save
                return docXML;
            }
            catch (Exception ex)
            {
                throw new generarXMLException(ex);
            }
        }
        public XmlDocument CreaXMLFacturaElectronica()
        {
            try
            {
                mXML = new System.IO.MemoryStream();

                System.Xml.XmlTextWriter writer = new System.Xml.XmlTextWriter(mXML, System.Text.Encoding.UTF8);

                XmlDocument docXML = new XmlDocument();

                if (_doc.tipoDocumento == (int)Enums.TipoDocumento.FacturaElectronica)
                {
                    //if (_doc.idCliente !=null && (bool)_doc.clienteContribuyente)
                    //{
                    //genera factura al contribuyente
                    GeneraXMLFacturaElectronica4_4(writer);
                    //}
                    //else
                    //{
                    //    //genera contribuyente a no contribuyente
                    //    GeneraXMLFacturaElectronicaNoContribuyente4_3(writer);

                    //}


                }
                else if (_doc.tipoDocumento == (int)Enums.TipoDocumento.NotaCreditoElectronica)
                {
                    GeneraXMLNotaCredito4_3(writer);
                }
                else if (_doc.tipoDocumento == (int)Enums.TipoDocumento.TiqueteElectronico)
                {
                    GeneraXMLTiqueteElectronico4_4(writer);
                }


                mXML.Seek(0, System.IO.SeekOrigin.Begin);

                docXML.Load(mXML);

                writer.Close();

                // Retorna el documento xml y ahi se puede salvar docXML.Save
                return docXML;
            }
            catch (Exception ex)
            {
                throw new generarXMLException(ex);
            }
        }
        #region 4.3

        private void GeneraXMLTiqueteElectronico4_3(System.Xml.XmlTextWriter writer) // As System.Xml.XmlTextWriter
        {
            try
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("TiqueteElectronico");

                writer.WriteAttributeString("xmlns", "https://cdn.comprobanteselectronicos.go.cr/xml-schemas/v4.3/tiqueteElectronico");
                writer.WriteAttributeString("xmlns:ds", "http://www.w3.org/2000/09/xmldsig#");
                writer.WriteAttributeString("xmlns:vc", "http://www.w3.org/2007/XMLSchema-versioning");
                writer.WriteAttributeString("xmlns:xs", "http://www.w3.org/2001/XMLSchema");

                CuerpoDocumento4_3(ref writer);

                // 'Aqui va la firma, despues la agregamos.

                writer.WriteEndElement();
                writer.WriteEndDocument();
                writer.Flush();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void GeneraXMLTiqueteElectronico4_4(XmlTextWriter writer)
        {
            try
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("TiqueteElectronico");

                // Namespaces actualizados para versión 4.4
                writer.WriteAttributeString("xmlns", "https://cdn.comprobanteselectronicos.go.cr/xml-schemas/v4.4/tiqueteElectronico");
                writer.WriteAttributeString("xmlns:ds", "http://www.w3.org/2000/09/xmldsig#");
                writer.WriteAttributeString("xmlns:vc", "http://www.w3.org/2007/XMLSchema-versioning");
                writer.WriteAttributeString("xmlns:xs", "http://www.w3.org/2001/XMLSchema");

                CuerpoDocumento4_4(ref writer);

                // 'Aqui va la firma, despues la agregamos.

                writer.WriteEndElement(); // </TiqueteElectronico>
                writer.WriteEndDocument();
                writer.Flush();
            }
            catch (Exception)
            {
                throw; // conserva el stack trace original
            }
        }

        private void GeneraXMLFacturaElectronica4_4(System.Xml.XmlTextWriter writer) // As System.Xml.XmlTextWriter
        {
            try
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("FacturaElectronica");
                writer.WriteAttributeString("xmlns",
                    "https://cdn.comprobanteselectronicos.go.cr/xml-schemas/v4.4/facturaElectronica");
                writer.WriteAttributeString("xmlns", "ds", null,
                    "http://www.w3.org/2000/09/xmldsig#");
                writer.WriteAttributeString("xmlns", "vc", null,
                    "http://www.w3.org/2007/XMLSchema-versioning");
                writer.WriteAttributeString("xmlns", "xsi", null,
                    "http://www.w3.org/2001/XMLSchema-instance");
                writer.WriteAttributeString("xsi", "schemaLocation",
                    "http://www.w3.org/2001/XMLSchema-instance",
                    "https://cdn.comprobanteselectronicos.go.cr/xml-schemas/v4.4/facturaElectronica " +
                    @"C:\PCCentral\MinisterioHacienda\mhcr-xml-schemas\jaxb\FacturaElectronica\v4.4\FacturaElectronica.xsd");
               

                CuerpoDocumento4_4(ref writer);


                // 'Aqui va la firma, despues la agregamos.

                writer.WriteEndElement();
                writer.WriteEndDocument();
                writer.Flush();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void CuerpoDocumento4_4(ref XmlTextWriter writer)
        {
            // Cabecera del comprobante
            writer.WriteElementString("Clave", _numeroClave);
            writer.WriteElementString("ProveedorSistemas", "603480811");
            writer.WriteElementString("CodigoActividadEmisor", _doc.codigoActividad.Trim().PadLeft(6, '0'));
            writer.WriteElementString("NumeroConsecutivo", _numeroConsecutivo);
            writer.WriteElementString("FechaEmision", DateTime.Now.ToString("yyyy-MM-ddTHH:mm:sszzz", CultureInfo.InvariantCulture));

            // EMISOR
            writer.WriteStartElement("Emisor");
            writer.WriteElementString("Nombre", _emisor.Nombre);
            writer.WriteStartElement("Identificacion");
            writer.WriteElementString("Tipo", _emisor.Identificacion_Tipo);
            writer.WriteElementString("Numero", _emisor.Identificacion_Numero);
            writer.WriteEndElement();
            writer.WriteStartElement("Ubicacion");
            writer.WriteElementString("Provincia", _emisor.Ubicacion_Provincia);
            writer.WriteElementString("Canton", _emisor.Ubicacion_Canton);
            writer.WriteElementString("Distrito", _emisor.Ubicacion_Distrito);
            writer.WriteElementString("OtrasSenas", _emisor.Ubicacion_OtrasSenas);
            writer.WriteEndElement();
            writer.WriteStartElement("Telefono");
            writer.WriteElementString("CodigoPais", _emisor.Telefono_CodigoPais);
            writer.WriteElementString("NumTelefono", _emisor.Telefono_Numero.ToString());
            writer.WriteEndElement();
            writer.WriteElementString("CorreoElectronico", _emisor.CorreoElectronico);
            writer.WriteEndElement();

            // RECEPTOR (opcional)
            if (_receptor != null)
            {
                writer.WriteStartElement("Receptor");
                writer.WriteElementString("Nombre", _receptor.Nombre);
                writer.WriteStartElement("Identificacion");
                writer.WriteElementString("Tipo", _receptor.Identificacion_Tipo);
                writer.WriteElementString("Numero", _receptor.Identificacion_Numero);
                writer.WriteEndElement();
                writer.WriteElementString("CorreoElectronico", _receptor.CorreoElectronico);
                writer.WriteEndElement();
            }

            // Condición de venta
            writer.WriteElementString("CondicionVenta", _condicionVenta);
            //if (_condicionVenta == "99")
            //    writer.WriteElementString("CondicionVentaOtros", "Se debe describir puntualmente la condición de la venta");
            if (_condicionVenta == "02")
                writer.WriteElementString("PlazoCredito", _plazoCredito);

            // DETALLE SERVICIO
            writer.WriteStartElement("DetalleServicio");
            foreach (var detalle in _listaDetalle)
            {
                writer.WriteStartElement("LineaDetalle");
                writer.WriteElementString("NumeroLinea", detalle.numLinea.ToString());
                writer.WriteElementString("CodigoCABYS", detalle.tbProducto.codigoCabys.Trim());

                writer.WriteStartElement("CodigoComercial");
                writer.WriteElementString("Tipo", "01");
                writer.WriteElementString("Codigo", detalle.idProducto.ToString().Trim());
                writer.WriteEndElement();

                writer.WriteElementString("Cantidad", detalle.cantidad.ToString("F3", CultureInfo.InvariantCulture));
                writer.WriteElementString("UnidadMedida",
                    _listaMedidas.Single(m => m.idTipoMedida == detalle.tbProducto.idMedida)
                                  .nomenclatura.Trim());

                writer.WriteElementString("Detalle", detalle.tbProducto.nombre.Trim());
                writer.WriteElementString("PrecioUnitario", detalle.precio.ToString("F5", CultureInfo.InvariantCulture));

                writer.WriteElementString("MontoTotal", detalle.montoTotal.ToString("F5", CultureInfo.InvariantCulture));
                if (detalle.montoTotalDesc != 0)
                {
                    writer.WriteStartElement("Descuento");
                    writer.WriteElementString("MontoDescuento", detalle.montoTotalDesc.ToString("F5", CultureInfo.InvariantCulture));
                    writer.WriteElementString("NaturalezaDescuento", "Descuento aplicado al cliente");
                    writer.WriteEndElement();
                }


                writer.WriteElementString("SubTotal", (detalle.montoTotal - detalle.montoTotalDesc)
                    .ToString("F5", CultureInfo.InvariantCulture));


         
               
         
               writer.WriteElementString("BaseImponible", (detalle.montoTotal - detalle.montoTotalDesc)
                    .ToString("F5", CultureInfo.InvariantCulture));

                writer.WriteStartElement("Impuesto");
                writer.WriteElementString("Codigo", "01");
                writer.WriteElementString("CodigoTarifaIVA", detalle.tbProducto.tbImpuestos.id.ToString().PadLeft(2, '0'));
                writer.WriteElementString("Tarifa", (detalle.porcImp ?? 0m).ToString("F2", CultureInfo.InvariantCulture));
                writer.WriteElementString("Monto", detalle.montoTotalImp.ToString("F5", CultureInfo.InvariantCulture));
                if (detalle.montoTotalExo != 0)
                {
                    writer.WriteStartElement("Exoneracion");
                        writer.WriteElementString("TipoDocumento", _receptor.tipoExoneracion.PadLeft(2, '0'));
                        writer.WriteElementString("NumeroDocumento", _receptor.docExoneracion.Trim());
                        writer.WriteElementString("NombreInstitucion", _receptor.institucionExo.Trim());
                        writer.WriteElementString("FechaEmision", _receptor.fechaEmisionExo.ToString("yyyy-MM-ddTHH:mm:sszzz"));
                        writer.WriteElementString("PorcentajeExoneracion", (detalle.porcExo ?? 0m).ToString("F5", CultureInfo.InvariantCulture));
                        writer.WriteElementString("MontoExoneracion", detalle.montoTotalExo.ToString("F5", CultureInfo.InvariantCulture));
                    writer.WriteEndElement();
                }
                writer.WriteEndElement();
                //0 xq no se fabrica
                writer.WriteElementString("ImpuestoAsumidoEmisorFabrica", 0.ToString("F5", CultureInfo.InvariantCulture));
                writer.WriteElementString("ImpuestoNeto", detalle.montoTotalImp.ToString("F5", CultureInfo.InvariantCulture));








                writer.WriteElementString("MontoTotalLinea", detalle.totalLinea.ToString("F5", CultureInfo.InvariantCulture));
            writer.WriteEndElement();
            }
            writer.WriteEndElement(); // DetalleServicio

            // RESUMEN FACTURA (solo IVA cobrado y BaseImponible antes de totales)
            decimal totalDescuento = 0m, totalComprobante = 0m, impuestosTotal = 0m;
            decimal totalProdGrav = 0m, totalServGrav = 0m;
            decimal totalProdExc = 0m, totalServExc = 0m;
            decimal totalProdExo = 0m, totalServExo = 0m;

            foreach (var d in _listaDetalle)
            {
                totalDescuento   += d.montoTotalDesc;
                totalComprobante += d.totalLinea;
                impuestosTotal   += d.montoTotalImp - d.montoTotalExo;

                var porcExo = (decimal)(d.porcExo ?? 0m) / 100m;
                var porcImp = (decimal)(d.porcImp ?? 0m) / 100m;
                var factor = porcImp != 0m ? porcExo / porcImp : 0m;
                bool isServ = _listaMedidas.Single(m => m.idTipoMedida == d.tbProducto.idMedida)
                                     .nomenclatura.Trim().Equals("SP", StringComparison.OrdinalIgnoreCase);

                if (isServ)
                {
                    if (d.montoTotalImp != 0)
                        totalServGrav += (1 - factor) * d.montoTotal;
                    else
                        totalServExc += d.montoTotal;
                    totalServExo += factor * d.montoTotal;
                }
                else
                {
                    if (d.montoTotalImp != 0)
                        totalProdGrav += (1 - factor) * d.montoTotal;
                    else
                        totalProdExc += d.montoTotal;
                    totalProdExo += factor * d.montoTotal;
                }
            }

            decimal totalGravado = totalProdGrav + totalServGrav;
            decimal totalExento = totalProdExc  + totalServExc;
            decimal totalExonerado = totalProdExo  + totalServExo;
            decimal totalVenta = totalGravado + totalExento + totalExonerado;
            decimal totalVentaNeta = totalVenta   - totalDescuento;

            writer.WriteStartElement("ResumenFactura");
            
            writer.WriteStartElement("CodigoTipoMoneda");
            writer.WriteElementString("CodigoMoneda", "CRC");
            writer.WriteElementString("TipoCambio", 1.ToString());
            writer.WriteEndElement();

           
            writer.WriteElementString("TotalServGravados", String.Format("{0:F5}", totalServGrav));
            writer.WriteElementString("TotalServExentos", String.Format("{0:F5}", totalServExc));


            if (totalServExo!=0)
            {
                writer.WriteElementString("TotalServExonerado", String.Format("{0:F5}", totalServExo));
            }
           
            writer.WriteElementString("TotalMercanciasGravadas", String.Format("{0:F5}", totalProdGrav));
            writer.WriteElementString("TotalMercanciasExentas", String.Format("{0:F5}", totalProdExc));
            if (totalProdExo != 0)
            {
                writer.WriteElementString("TotalMercExonerada", String.Format("{0:F5}", totalProdExo));
            }

      
            writer.WriteElementString("TotalGravado", String.Format("{0:F5}", totalGravado));
            writer.WriteElementString("TotalExento", String.Format("{0:F5}", totalExento));

            if (totalExonerado != 0)
            {
                writer.WriteElementString("TotalExonerado", String.Format("{0:F5}", totalExonerado));
            }
            
            writer.WriteElementString("TotalVenta", String.Format("{0:F5}", totalVenta));
            
            if (totalDescuento != 0)
            {
                writer.WriteElementString("TotalDescuentos", String.Format("{0:F5}", totalDescuento));
            }
            
            writer.WriteElementString("TotalVentaNeta", String.Format("{0:F5}", totalVentaNeta));




            var impuestosAgrupados = _listaDetalle
            .GroupBy(d => d.tbProducto.tbImpuestos.valor)
            .Select(g => new
            {
                id = g.Key,
                Total = g.Sum(x => x.montoTotalImp),
                Impuestos = g.Select(x => x.tbProducto.tbImpuestos.id).Distinct().ToList()
            })
            .ToList();


        
            foreach (var d in impuestosAgrupados)
            {

                writer.WriteStartElement("TotalDesgloseImpuesto");
                writer.WriteElementString("Codigo", 01.ToString().PadLeft(2, '0'));
                writer.WriteElementString("CodigoTarifaIVA", d.Impuestos[0].ToString().PadLeft(2, '0'));
                writer.WriteElementString("TotalMontoImpuesto", String.Format("{0:F5}", d.Total));
                writer.WriteEndElement();

            }
        


            writer.WriteElementString("TotalImpuesto", String.Format("{0:F5}", impuestosTotal));

            writer.WriteStartElement("MedioPago");

            //agrupa tipos de pago y los suma
            var pagosAgrupados = _doc.tbPagos
            .GroupBy(p => p.tipoPago)
            .Select(g => new
            {
                TipoPago = g.Key,
                Total = g.Sum(x => x.monto)
            })
            .ToList();
            foreach (var p in pagosAgrupados)
            {
                

               
                writer.WriteElementString("TipoMedioPago", p.TipoPago.ToString().PadLeft(2, '0'));
                writer.WriteElementString("TotalMedioPago", String.Format("{0:F5}", p.Total));

            }
                     

            writer.WriteEndElement(); // fin MedioPago

            writer.WriteElementString("TotalComprobante", String.Format("{0:F5}", totalComprobante));
            writer.WriteEndElement(); // ResumenFactura

            // INFORMACIÓN REFERENCIA (nota crédito/débito)
            if (_doc.tipoDocumento == (int)Enums.TipoDocumento.NotaCreditoElectronica ||
                _doc.tipoDocumento == (int)Enums.TipoDocumento.NotaDebitoElectronica)
            {
                writer.WriteStartElement("InformacionReferencia");
                writer.WriteElementString("TipoDoc", _doc.tipoDocRef.ToString().PadLeft(2, '0'));
                writer.WriteElementString("Numero", _doc.claveRef);
                writer.WriteElementString("FechaEmision", _doc.fechaRef.Value.ToString("yyyy-MM-ddTHH:mm:sszzz", CultureInfo.InvariantCulture));
                writer.WriteElementString("Codigo", _doc.codigoRef.ToString().PadLeft(2, '0'));
                writer.WriteElementString("Razon", _doc.razon.ToUpper().Substring(0, 180).Trim());
                writer.WriteEndElement();
            }

            // OTROS (observaciones)
            if (!string.IsNullOrWhiteSpace(_doc.observaciones))
            {
                writer.WriteStartElement("Otros");
                writer.WriteElementString("OtroTexto", _doc.observaciones.Substring(0, 180).Trim());
                writer.WriteEndElement();
            }
        }


        private void GeneraXMLFacturaElectronica4_3(System.Xml.XmlTextWriter writer) // As System.Xml.XmlTextWriter
        {
            try
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("FacturaElectronica");

                writer.WriteAttributeString("xmlns", "https://cdn.comprobanteselectronicos.go.cr/xml-schemas/v4.3/facturaElectronica");
                writer.WriteAttributeString("xmlns:ds", "http://www.w3.org/2000/09/xmldsig#");
                writer.WriteAttributeString("xmlns:vc", "http://www.w3.org/2007/XMLSchema-versioning");
                writer.WriteAttributeString("xmlns:xs", "http://www.w3.org/2001/XMLSchema");
                /*
                writer.WriteAttributeString("xmlns:targetNamespace", "https://cdn.comprobanteselectronicos.go.cr/xml-schemas/v4.3/facturaElectronica");
                writer.WriteAttributeString("xmlns:elementFormDefault", "qualified");
                writer.WriteAttributeString("xmlns:attributeFormDefault", "unqualified");
                writer.WriteAttributeString("xmlns:version", "4.3");
                writer.WriteAttributeString("xmlns:vc:minVersion", "1.1");
                */

                CuerpoDocumento4_3(ref writer);


                // 'Aqui va la firma, despues la agregamos.

                writer.WriteEndElement();
                writer.WriteEndDocument();
                writer.Flush();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        private void GeneraXMLComprasSimplificadoElectronica4_3(System.Xml.XmlTextWriter writer) // As System.Xml.XmlTextWriter
        {
            try
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("FacturaElectronicaCompra");

                writer.WriteAttributeString("xmlns", "https://cdn.comprobanteselectronicos.go.cr/xml-schemas/v4.3/facturaElectronicaCompra");
                writer.WriteAttributeString("xmlns:ds", "http://www.w3.org/2000/09/xmldsig#");
                writer.WriteAttributeString("xmlns:vc", "http://www.w3.org/2007/XMLSchema-versioning");
                writer.WriteAttributeString("xmlns:xs", "http://www.w3.org/2001/XMLSchema");


                CuerpoDocumento4_3(ref writer);

                // 'Aqui va la firma, despues la agregamos.

                writer.WriteEndElement();
                writer.WriteEndDocument();
                writer.Flush();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void GeneraXMLNotaCredito4_3(System.Xml.XmlTextWriter writer) // As System.Xml.XmlTextWriter
        {
            try
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("NotaCreditoElectronica");

                writer.WriteAttributeString("xmlns", "https://cdn.comprobanteselectronicos.go.cr/xml-schemas/v4.3/notaCreditoElectronica");
                writer.WriteAttributeString("xmlns:ds", "http://www.w3.org/2000/09/xmldsig#");
                writer.WriteAttributeString("xmlns:vc", "http://www.w3.org/2007/XMLSchema-versioning");
                writer.WriteAttributeString("xmlns:xs", "http://www.w3.org/2001/XMLSchema");



                CuerpoDocumento4_3(ref writer);



                // 'Aqui va la firma, despues la agregamos.

                writer.WriteEndElement();
                writer.WriteEndDocument();
                writer.Flush();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void GeneraXMLMensajeHacienda4_3(System.Xml.XmlTextWriter writer) // As System.Xml.XmlTextWriter
        {
            try
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("MensajeHacienda");

                writer.WriteAttributeString("xmlns", "https://cdn.comprobanteselectronicos.go.cr/xml-schemas/v4.3/mensajeHacienda");
                writer.WriteAttributeString("xmlns:ds", "http://www.w3.org/2000/09/xmldsig#");
                writer.WriteAttributeString("xmlns:vc", "http://www.w3.org/2007/XMLSchema-versioning");
                writer.WriteAttributeString("xmlns:xs", "http://www.w3.org/2001/XMLSchema");


                // La clave se crea con la función CreaClave de la clase Datos
                writer.WriteElementString("Clave", _mensajeHacienda.claveDocEmisor);

                //EMISOR
                writer.WriteElementString("NombreEmisor", _mensajeHacienda.nombreEmisor.Trim());
                writer.WriteElementString("TipoIdentificacionEmisor", _mensajeHacienda.tipoIdEmisor.ToString().Trim());
                writer.WriteElementString("NumeroCedulaEmisor", _mensajeHacienda.idEmisor.Trim());


                //RECEPTOR
                writer.WriteElementString("NombreReceptor", _mensajeHacienda.nombreReceptor.Trim());//nombre
                writer.WriteElementString("TipoIdentificacionReceptor", _mensajeHacienda.tipoIdEmpresa.ToString().Trim());
                writer.WriteElementString("NumeroCedulaReceptor", _mensajeHacienda.idEmpresa.Trim());


                //MENSAJE HACIENDA
                writer.WriteElementString("Mensaje", _mensajeHacienda.estadoRecibido.ToString());
                if (_mensajeHacienda.razon != null)
                {
                    writer.WriteElementString("DetalleMensaje", _mensajeHacienda.razon.ToString());
                }


                //IMPUESTOS

                writer.WriteElementString("MontoTotalImpuesto", String.Format("{0:N3}", _mensajeHacienda.totalImp.ToString().Trim()));



                //TOTAL DE FACTURA
                writer.WriteElementString("TotalFactura", String.Format("{0:N3}", _mensajeHacienda.totalFactura.ToString().Trim()));


                writer.WriteEndElement();
                writer.WriteEndDocument();
                writer.Flush();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void GeneraXMLReceptorHacienda4_3(System.Xml.XmlTextWriter writer) // As System.Xml.XmlTextWriter
        {
            try
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("MensajeReceptor");

                writer.WriteAttributeString("xmlns", "https://cdn.comprobanteselectronicos.go.cr/xml-schemas/v4.3/mensajeReceptor");
                writer.WriteAttributeString("xmlns:ds", "http://www.w3.org/2000/09/xmldsig#");
                writer.WriteAttributeString("xmlns:vc", "http://www.w3.org/2007/XMLSchema-versioning");
                writer.WriteAttributeString("xmlns:xs", "http://www.w3.org/2001/XMLSchema");



                // La clave se crea con la función CreaClave de la clase Datos
                writer.WriteElementString("Clave", _mensajeHacienda.claveDocEmisor);

                // La clave se crea con la función CreaClave de la clase Datos
                writer.WriteElementString("NumeroCedulaEmisor", _mensajeHacienda.idEmisor.Trim());
                writer.WriteElementString("FechaEmisionDoc", _mensajeHacienda.fechaEmision.ToString("yyyy-MM-ddTHH:mm:sszzz"));

                writer.WriteElementString("Mensaje", _mensajeHacienda.estadoRecibido.ToString());
                if (_mensajeHacienda.estadoRecibido != 1)
                {
                    writer.WriteElementString("DetalleMensaje", _mensajeHacienda.razon.ToString());
                }
                writer.WriteElementString("MontoTotalImpuesto", String.Format("{0:N3}", _mensajeHacienda.totalImp.ToString().Trim()));
                writer.WriteElementString("CodigoActividad", _mensajeHacienda.codigoActividadEmisor);

                writer.WriteElementString("TotalFactura", String.Format("{0:N3}", _mensajeHacienda.totalFactura.ToString().Trim()));


                writer.WriteElementString("NumeroCedulaReceptor", _mensajeHacienda.idEmpresa.Trim());
                writer.WriteElementString("NumeroConsecutivoReceptor", _mensajeHacienda.consecutivoReceptor.Trim());

                writer.WriteEndElement();
                writer.WriteEndDocument();
                writer.Flush();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void CuerpoDocumento4_3(ref XmlTextWriter writer)
        {



            // La clave se crea con la función CreaClave de la clase Datos
            writer.WriteElementString("Clave", _numeroClave);
            writer.WriteElementString("CodigoActividad", _doc.codigoActividad.Trim().ToString().PadLeft(6, '0'));



            // 'El numero de secuencia es de 20 caracteres, 
            // 'Se debe de crear con la función CreaNumeroSecuencia de la clase Datos
            writer.WriteElementString("NumeroConsecutivo", _numeroConsecutivo);

            // 'El formato de la fecha es yyyy-MM-ddTHH:mm:sszzz
            writer.WriteElementString("FechaEmision", DateTime.Now.ToString("yyyy-MM-ddTHH:mm:sszzz"));

            writer.WriteStartElement("Emisor");

            writer.WriteElementString("Nombre", _emisor.Nombre);
            writer.WriteStartElement("Identificacion");
            writer.WriteElementString("Tipo", _emisor.Identificacion_Tipo);
            writer.WriteElementString("Numero", _emisor.Identificacion_Numero);
            writer.WriteEndElement(); // 'Identificacion

            // '-----------------------------------
            // 'Los datos de las ubicaciones los puede tomar de las tablas de datos, 
            // 'Debe ser exacto al que hacienda tiene registrado para su compañia
            writer.WriteStartElement("Ubicacion");
            writer.WriteElementString("Provincia", _emisor.Ubicacion_Provincia);
            writer.WriteElementString("Canton", _emisor.Ubicacion_Canton);
            writer.WriteElementString("Distrito", _emisor.Ubicacion_Distrito);
            writer.WriteElementString("Barrio", _emisor.Ubicacion_Barrio);
            writer.WriteElementString("OtrasSenas", _emisor.Ubicacion_OtrasSenas);
            writer.WriteEndElement(); // 'Ubicacion

            writer.WriteStartElement("Telefono");
            writer.WriteElementString("CodigoPais", _emisor.Telefono_CodigoPais);
            writer.WriteElementString("NumTelefono", _emisor.Telefono_Numero.ToString());
            writer.WriteEndElement(); // 'Telefono

            writer.WriteElementString("CorreoElectronico", _emisor.CorreoElectronico);

            writer.WriteEndElement(); // Emisor

            if (_receptor != null)
            {
                writer.WriteStartElement("Receptor");                      // 'Receptor es similar con emisor, los datos opcionales siempre siempre siempre omitirlos.

                writer.WriteElementString("Nombre", _receptor.Nombre);
                writer.WriteStartElement("Identificacion");
                // 'Los tipos de identificacion los puede ver en la tabla de datos
                writer.WriteElementString("Tipo", _receptor.Identificacion_Tipo);
                writer.WriteElementString("Numero", _receptor.Identificacion_Numero);
                writer.WriteEndElement(); // 'Identificacion

                writer.WriteElementString("CorreoElectronico", _receptor.CorreoElectronico);

                writer.WriteEndElement(); // Receptor
            }

            // 'Loa datos estan en la tabla correspondiente
            writer.WriteElementString("CondicionVenta", _condicionVenta);
            // '01: Contado
            // '02: Credito
            // '03: Consignación
            // '04: Apartado
            // '05: Arrendamiento con opcion de compra
            // '06: Arrendamiento con función financiera
            // '99: Otros

            // 'Este dato se muestra si la condicion venta es credito
            if (_condicionVenta == "02")
            {
                writer.WriteElementString("PlazoCredito", _plazoCredito);
            }

            writer.WriteElementString("MedioPago", _medioPago);
            // '01: Efectivo
            // '02: Tarjeta
            // '03: Cheque
            // '04: Transferecia - deposito bancario
            // '05: Recaudado por terceros            
            // '99: Otros

            writer.WriteStartElement("DetalleServicio");

            foreach (tbDetalleDocumento detalle in _listaDetalle)
            {
                writer.WriteStartElement("LineaDetalle");
                writer.WriteElementString("NumeroLinea", detalle.numLinea.ToString().Trim());


                //CODIGO CABYS EN CASO EN QUE EL PRODCUTO LO TENGA DEFINIDIO REQUERIDO OBLIGATORIO DESPUES DEL 01/12/2020
                if (detalle.tbProducto.codigoCabys != null && detalle.tbProducto.codigoCabys != string.Empty)
                {
                    writer.WriteElementString("Codigo", detalle.tbProducto.codigoCabys.Trim());
                }



                writer.WriteStartElement("CodigoComercial");
                writer.WriteElementString("Tipo", "01");
                writer.WriteElementString("Codigo", detalle.idProducto.ToString().Trim());
                writer.WriteEndElement(); // 'codigo comercial

                writer.WriteElementString("Cantidad", String.Format("{0:F3}", detalle.cantidad));
                // 'Para las unidades de medida ver la tabla correspondiente
                writer.WriteElementString("UnidadMedida", _listaMedidas.Where(x => x.idTipoMedida == detalle.tbProducto.idMedida).SingleOrDefault().nomenclatura.ToString().Trim());//nomenlatura kg etc
                writer.WriteElementString("Detalle", detalle.tbProducto.nombre.ToString().Trim());
                writer.WriteElementString("PrecioUnitario", String.Format("{0:F5}", detalle.precio));
                writer.WriteElementString("MontoTotal", String.Format("{0:F5}", detalle.montoTotal));

                if (detalle.montoTotalDesc != 0)
                {
                    writer.WriteStartElement("Descuento");
                    writer.WriteElementString("MontoDescuento", String.Format("{0:F5}", detalle.montoTotalDesc));
                    writer.WriteElementString("NaturalezaDescuento", "Descuento aplicado al cliente");//agregar naturaleza desc                        
                    writer.WriteEndElement(); // 'descuento

                }
                writer.WriteElementString("SubTotal", String.Format("{0:F5}", (detalle.montoTotal - detalle.montoTotalDesc)));

                if (detalle.montoTotalImp != 0)
                {
                    writer.WriteStartElement("Impuesto");
                    writer.WriteElementString("Codigo", "01");// impueto aplicado codigo

                    //s debe obtener el codigo ya sea de la tabla detalle o segun la terifa
                    writer.WriteElementString("CodigoTarifa", detalle.tbProducto.tbImpuestos.id.ToString().PadLeft(2, '0'));// impueto aplicado codigo);


                    writer.WriteElementString("Tarifa", String.Format("{0:F5}", detalle.porcImp));// %aplicado valor
                    writer.WriteElementString("Monto", String.Format("{0:F5}", (detalle.montoTotalImp)));

                    //exoneracion 
                    if (detalle.montoTotalExo != 0)
                    {
                        writer.WriteStartElement("Exoneracion");
                        writer.WriteElementString("TipoDocumento", _receptor.tipoExoneracion.PadLeft(2, '0'));// impueto aplicado codigo
                        writer.WriteElementString("NumeroDocumento", _receptor.docExoneracion.Trim());// impueto aplicado codigo

                        writer.WriteElementString("NombreInstitucion", _receptor.institucionExo.Trim());// impueto aplicado codigo0
                        writer.WriteElementString("FechaEmision", _receptor.fechaEmisionExo.ToString("yyyy-MM-ddTHH:mm:sszzz"));// impueto aplicado codigo
                        writer.WriteElementString("PorcentajeExoneracion", detalle.porcExo.ToString());// impueto aplicado codigo

                        writer.WriteElementString("MontoExoneracion", string.Format("{0:F5}", detalle.montoTotalExo));// impueto aplicado codigo
                        writer.WriteEndElement(); // exoneracion
                    }
                    writer.WriteEndElement(); // Impuesto
                }

                writer.WriteElementString("ImpuestoNeto", String.Format("{0:F5}", (detalle.montoTotalImp - detalle.montoTotalExo)));
                writer.WriteElementString("MontoTotalLinea", String.Format("{0:F5}", detalle.totalLinea));
                writer.WriteEndElement(); // LineaDetalle
            }
            writer.WriteEndElement(); // DetalleServicio
            writer.WriteStartElement("ResumenFactura");

            //writer.WriteStartElement("CodigoTipoMoneda");
            //writer.WriteElementString("CodigoMoneda", _codigoMoneda);

            //if (_codigoMoneda.ToUpper().Trim() == "USD")
            //{
            //    writer.WriteElementString("TipoCambio", String.Format("{0:N3}", _empresa.tbParametrosEmpresa.First().cambioDolar.ToString().Trim()));

            //}
            //else
            //{
            //    writer.WriteElementString("TipoCambio", String.Format("{0:N3}", "1"));

            //}
            //writer.WriteEndElement(); // CodigoTipoMoneda

            decimal totalProdGravados = 0;
            decimal totalProdExcentos = 0;
            decimal totalSevGravados = 0;
            decimal totalServExcentos = 0;
            decimal totalServExonerado = 0;
            decimal totalProdExonerado = 0;

            decimal totalDescuento = 0;
            decimal totalComprobante = 0;
            decimal impuestos = 0;

            foreach (tbDetalleDocumento detalle in _listaDetalle)
            {
                totalDescuento += detalle.montoTotalDesc;//total de decuento
                totalComprobante += detalle.totalLinea;//total de comprobante
                impuestos += detalle.montoTotalImp - detalle.montoTotalExo;
                decimal porcExo = (decimal)detalle.porcExo / 100;
                decimal porcImp = (decimal)detalle.porcImp / 100;

                tbTipoMedidas medida = _listaMedidas.Where(x => x.idTipoMedida == detalle.tbProducto.idMedida).SingleOrDefault(); 

                if (medida.nomenclatura.Trim().ToUpper() == "SP")//valida si es producto o servicio y acumula en su respectiva variable
                {
                    if (detalle.montoTotalImp != 0)
                    {
                        totalSevGravados += (1 - ((decimal)porcExo) / (decimal)porcImp) * (decimal)detalle.montoTotal;
                        totalServExonerado += (((decimal)porcExo) / porcImp * detalle.montoTotal);

                    }
                    else
                    {
                        totalServExcentos += detalle.montoTotal;
                    }
                }
                else//si es producto
                {
                    if (detalle.montoTotalImp != 0)
                    {

                        totalProdGravados += (1 - ((decimal)porcExo) / (decimal)porcImp) * (decimal)detalle.montoTotal;
                        totalProdExonerado += (((decimal)porcExo) / porcImp * detalle.montoTotal);
                    }
                    else
                    {
                        totalProdExcentos += detalle.montoTotal;
                    }

                }
            }

            writer.WriteElementString("TotalServGravados", String.Format("{0:F5}", totalSevGravados));
            writer.WriteElementString("TotalServExentos", String.Format("{0:F5}", totalServExcentos));
            writer.WriteElementString("TotalServExonerado", String.Format("{0:F5}", totalServExonerado));
            writer.WriteElementString("TotalMercanciasGravadas", String.Format("{0:F5}", totalProdGravados));
            writer.WriteElementString("TotalMercanciasExentas", String.Format("{0:F5}", totalProdExcentos));
            writer.WriteElementString("TotalMercExonerada", String.Format("{0:F5}", totalProdExonerado));

            decimal totalGravados = totalSevGravados + totalProdGravados;
            decimal totalExentos = totalProdExcentos + totalServExcentos;
            decimal totalExo = totalProdExonerado + totalServExonerado;

            writer.WriteElementString("TotalGravado", String.Format("{0:F5}", totalGravados));
            writer.WriteElementString("TotalExento", String.Format("{0:F5}", totalExentos));
            writer.WriteElementString("TotalExonerado", String.Format("{0:F5}", totalExo));

            decimal totalVenta = totalGravados + totalExentos + totalExo;
            decimal totalVentaNeta = totalVenta - totalDescuento;//calula el monto de descuento

            writer.WriteElementString("TotalVenta", String.Format("{0:F5}", totalVenta));
            writer.WriteElementString("TotalDescuentos", String.Format("{0:F5}", totalDescuento));
            writer.WriteElementString("TotalVentaNeta", String.Format("{0:F5}", totalVentaNeta));
            writer.WriteElementString("TotalImpuesto", String.Format("{0:F5}", impuestos));
            writer.WriteElementString("TotalComprobante", String.Format("{0:F5}", totalComprobante));
            writer.WriteEndElement(); // ResumenFactura

            // 'Estos datos te los tiene que brindar los encargados del area financiera
            //writer.WriteStartElement("Normativa");
            //writer.WriteElementString("NumeroResolucion", _empresa.numeroResolucion.Trim());
            //writer.WriteElementString("FechaResolucion", ((DateTime)_empresa.fechaResolucio).ToString("dd-MM-yyyy HH:MM:ss"));
            //writer.WriteEndElement(); // Normativa

            if (_doc.tipoDocumento == (int)Enums.TipoDocumento.NotaCreditoElectronica ||
                _doc.tipoDocumento == (int)Enums.TipoDocumento.NotaDebitoElectronica)
            {
                writer.WriteStartElement("InformacionReferencia");//referencia
                writer.WriteElementString("TipoDoc", _doc.tipoDocRef.ToString().PadLeft(2, '0'));
                writer.WriteElementString("Numero", _doc.claveRef);
                writer.WriteElementString("FechaEmision", _doc.fechaRef.Value.ToString("yyyy-MM-ddTHH:mm:sszzz"));
                writer.WriteElementString("Codigo", _doc.codigoRef.ToString().PadLeft(2, '0'));
                writer.WriteElementString("Razon", _doc.razon.ToUpper().Substring(0, 180).Trim());
                writer.WriteEndElement(); // referencia
            }

            if (_doc.observaciones != null && _doc.observaciones.Trim() != string.Empty)
            {
                //otros
                writer.WriteStartElement("Otros");
                writer.WriteElementString("OtroTexto", _doc.observaciones.Substring(0, 180).Trim());
                writer.WriteEndElement();//Otros
            }

        }
        #endregion


        #region 4.2

        private void GeneraXMLMensajeHacienda(System.Xml.XmlTextWriter writer) // As System.Xml.XmlTextWriter
        {
            try
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("MensajeReceptor");

                writer.WriteAttributeString("xmlns", "https://tribunet.hacienda.go.cr/docs/esquemas/2017/v4.2/mensajeReceptor");
                writer.WriteAttributeString("xmlns:ds", "http://www.w3.org/2000/09/xmldsig#");
                writer.WriteAttributeString("xmlns:vc", "http://www.w3.org/2007/XMLSchema-versioning");
                writer.WriteAttributeString("xmlns:xs", "http://www.w3.org/2001/XMLSchema");


                // La clave se crea con la función CreaClave de la clase Datos
                writer.WriteElementString("Clave", _mensajeHacienda.claveDocEmisor);

                // La clave se crea con la función CreaClave de la clase Datos
                writer.WriteElementString("NumeroCedulaEmisor", _mensajeHacienda.idEmisor.Trim());
                writer.WriteElementString("FechaEmisionDoc", _mensajeHacienda.fechaEmision.ToString("yyyy-MM-ddTHH:mm:sszzz"));

                writer.WriteElementString("Mensaje", _mensajeHacienda.estadoRecibido.ToString());

                if (_mensajeHacienda.totalImp != 0)
                {
                    writer.WriteElementString("MontoTotalImpuesto", String.Format("{0:N3}", _mensajeHacienda.totalImp.ToString().Trim()));


                }

                writer.WriteElementString("TotalFactura", String.Format("{0:N3}", _mensajeHacienda.totalFactura.ToString().Trim()));


                writer.WriteElementString("NumeroCedulaReceptor", _mensajeHacienda.idEmpresa.Trim());
                writer.WriteElementString("NumeroConsecutivoReceptor", _mensajeHacienda.consecutivoReceptor.Trim());

                writer.WriteEndElement();
                writer.WriteEndDocument();
                writer.Flush();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void GeneraXMLFacturaElectronica(System.Xml.XmlTextWriter writer) // As System.Xml.XmlTextWriter
        {
            try
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("FacturaElectronica");

                writer.WriteAttributeString("xmlns", "https://tribunet.hacienda.go.cr/docs/esquemas/2017/v4.2/facturaElectronica");
                writer.WriteAttributeString("xmlns:ds", "http://www.w3.org/2000/09/xmldsig#");
                writer.WriteAttributeString("xmlns:vc", "http://www.w3.org/2007/XMLSchema-versioning");
                writer.WriteAttributeString("xmlns:xs", "http://www.w3.org/2001/XMLSchema");

                // La clave se crea con la función CreaClave de la clase Datos
                writer.WriteElementString("Clave", _numeroClave);

                // 'El numero de secuencia es de 20 caracteres, 
                // 'Se debe de crear con la función CreaNumeroSecuencia de la clase Datos
                writer.WriteElementString("NumeroConsecutivo", _numeroConsecutivo);

                // 'El formato de la fecha es yyyy-MM-ddTHH:mm:sszzz
                writer.WriteElementString("FechaEmision", DateTime.Now.ToString("yyyy-MM-ddTHH:mm:sszzz"));

                writer.WriteStartElement("Emisor");

                writer.WriteElementString("Nombre", _emisor.Nombre);
                writer.WriteStartElement("Identificacion");
                writer.WriteElementString("Tipo", _emisor.Identificacion_Tipo);
                writer.WriteElementString("Numero", _emisor.Identificacion_Numero);
                writer.WriteEndElement(); // 'Identificacion

                // '-----------------------------------
                // 'Los datos de las ubicaciones los puede tomar de las tablas de datos, 
                // 'Debe ser exacto al que hacienda tiene registrado para su compañia
                writer.WriteStartElement("Ubicacion");
                writer.WriteElementString("Provincia", _emisor.Ubicacion_Provincia);
                writer.WriteElementString("Canton", _emisor.Ubicacion_Canton);
                writer.WriteElementString("Distrito", _emisor.Ubicacion_Distrito);
                writer.WriteElementString("Barrio", _emisor.Ubicacion_Barrio);
                writer.WriteElementString("OtrasSenas", _emisor.Ubicacion_OtrasSenas);
                writer.WriteEndElement(); // 'Ubicacion

                writer.WriteStartElement("Telefono");
                writer.WriteElementString("CodigoPais", _emisor.Telefono_CodigoPais);
                writer.WriteElementString("NumTelefono", _emisor.Telefono_Numero.ToString());
                writer.WriteEndElement(); // 'Telefono

                writer.WriteElementString("CorreoElectronico", _emisor.CorreoElectronico);

                writer.WriteEndElement(); // Emisor
                                          // '------------------------------------
                                          // 'Receptor es similar con emisor, los datos opcionales siempre siempre siempre omitirlos.
                if (_receptor != null)
                {
                    writer.WriteStartElement("Receptor");
                    writer.WriteElementString("Nombre", _receptor.Nombre);
                    writer.WriteStartElement("Identificacion");
                    // 'Los tipos de identificacion los puede ver en la tabla de datos
                    writer.WriteElementString("Tipo", _receptor.Identificacion_Tipo);
                    writer.WriteElementString("Numero", _receptor.Identificacion_Numero);
                    writer.WriteEndElement(); // 'Identificacion

                    //writer.WriteStartElement("Telefono");
                    //writer.WriteElementString("CodigoPais", _receptor.Telefono_CodigoPais);
                    //writer.WriteElementString("NumTelefono", _receptor.Telefono_Numero.ToString());
                    //writer.WriteEndElement(); // 'Telefono

                    writer.WriteElementString("CorreoElectronico", _receptor.CorreoElectronico);

                    writer.WriteEndElement(); // Receptor

                }

                // 'Loa datos estan en la tabla correspondiente
                writer.WriteElementString("CondicionVenta", _condicionVenta);
                // '01: Contado
                // '02: Credito
                // '03: Consignación
                // '04: Apartado
                // '05: Arrendamiento con opcion de compra
                // '06: Arrendamiento con función financiera
                // '99: Otros

                // 'Este dato se muestra si la condicion venta es credito

                writer.WriteElementString("PlazoCredito", _plazoCredito);



                writer.WriteElementString("MedioPago", _medioPago);
                // '01: Efectivo
                // '02: Tarjeta
                // '03: Cheque
                // '04: Transferecia - deposito bancario
                // '05: Recaudado por terceros            
                // '99: Otros

                writer.WriteStartElement("DetalleServicio");
                //-----variables resumes de montos pagados


                // '-------------------------------------
                foreach (tbDetalleDocumento detalle in _listaDetalle)
                {
                    writer.WriteStartElement("LineaDetalle");

                    writer.WriteElementString("NumeroLinea", detalle.numLinea.ToString().Trim());

                    writer.WriteStartElement("Codigo");
                    writer.WriteElementString("Tipo", "01");//codigo del prodcuto del vendedor nota 12.
                    writer.WriteElementString("Codigo", detalle.idProducto.ToString().Trim());
                    writer.WriteEndElement(); // 'Codigo

                    int cant = (int)detalle.cantidad;//convierte entero, elimina decimales
                    writer.WriteElementString("Cantidad", cant.ToString());
                    // 'Para las unidades de medida ver la tabla correspondiente
                    writer.WriteElementString("UnidadMedida", Enum.GetName(typeof(Enums.TipoMedida), detalle.tbProducto.idMedida));//nomenlatura kg etc

                    writer.WriteElementString("Detalle", detalle.tbProducto.nombre.ToString().Trim());


                    writer.WriteElementString("PrecioUnitario", String.Format("{0:N3}", detalle.precio.ToString().Trim()));
                    writer.WriteElementString("MontoTotal", String.Format("{0:N3}", detalle.montoTotal.ToString().Trim()));
                    writer.WriteElementString("MontoDescuento", String.Format("{0:N3}", detalle.montoTotalDesc.ToString().Trim()));


                    if (detalle.montoTotalDesc == 0)
                    {

                        writer.WriteElementString("NaturalezaDescuento", "Sin descuento");//agregar naturaleza desc
                    }
                    else
                    {
                        writer.WriteElementString("NaturalezaDescuento", "Descuento aplicado al cliente");//agregar naturaleza desc
                    }

                    writer.WriteElementString("SubTotal", String.Format("{0:N3}", (detalle.montoTotal - detalle.montoTotalDesc).ToString().Trim()));




                    if (detalle.montoTotalImp != 0)
                    {

                        writer.WriteStartElement("Impuesto");
                        writer.WriteElementString("Codigo", detalle.tbProducto.tbImpuestos.id.ToString().PadLeft(2, '0'));// impueto aplicado codigo
                        writer.WriteElementString("Tarifa", detalle.tbProducto.tbImpuestos.valor.ToString());// %aplicado valor
                        writer.WriteElementString("Monto", String.Format("{0:N3}", (detalle.montoTotalImp).ToString().Trim()));
                        writer.WriteEndElement(); // Impuesto
                    }



                    //if (!detalle.tbProducto.esExento)
                    //{
                    //    if (detalle.tbProducto.tbTipoMedidas.nomenclatura.ToUpper().Trim() == "SP")
                    //    {
                    //        if (detalle.montoTotalImp==0)
                    //        {

                    //            totalSevGravados += detalle.montoTotal;
                    //        }
                    //        else
                    //        {
                    //            totalSevGravados += detalle.montoTotal-detalle.montoTotalExo;
                    //        }


                    //    }
                    //    else
                    //    {
                    //        totalProdGravados += detalle.montoTotalExo;
                    //    }
                    //    impuestos += detalle.montoTotalImp;
                    //    exoneracion += detalle.montoTotalExo;

                    //}
                    //else
                    //{
                    //    if (detalle.tbProducto.tbTipoMedidas.nomenclatura.ToUpper().Trim()=="SP")
                    //    {
                    //        totalServExcentos += detalle.montoTotal;

                    //    }
                    //    else
                    //    {
                    //        totalProdExcentos += detalle.montoTotal;
                    //    }

                    //}


                    writer.WriteElementString("MontoTotalLinea", String.Format("{0:N3}", detalle.totalLinea.ToString().Trim()));

                    writer.WriteEndElement(); // LineaDetalle
                }
                // '-------------------------------------

                writer.WriteEndElement(); // DetalleServicio


                writer.WriteStartElement("ResumenFactura");

                // Estos campos son opcionales, solo fin desea facturar en dólares
                writer.WriteElementString("CodigoMoneda", _codigoMoneda);
                if (_codigoMoneda.ToUpper().Trim() == "USD")
                {
                    writer.WriteElementString("TipoCambio", String.Format("{0:N3}", _empresa.tbParametrosEmpresa.First().cambioDolar.ToString().Trim()));

                }
                else
                {
                    writer.WriteElementString("TipoCambio", String.Format("{0:N3}", "1"));

                }
                // =================

                // 'En esta parte los totales se pueden ir sumando linea a linea cuando se carga el detalle
                // 'ó se pasa como parametros al inicio


                decimal totalProdGravados = 0;
                decimal totalProdExcentos = 0;
                decimal totalSevGravados = 0;
                decimal totalServExcentos = 0;
                decimal totalDescuento = 0;
                decimal totalComprobante = 0;
                decimal impuestos = 0;

                foreach (tbDetalleDocumento detalle in _listaDetalle)
                {
                    totalDescuento += detalle.montoTotalDesc;//total de decuento
                    totalComprobante += detalle.totalLinea;//total de comprobante


                    if (detalle.montoTotalImp == 0)//valida si exonera es 0, si tiene valor de impuesto no exonera
                    {//acumulo monto total, sin descuento. monto total es precio*cantidad
                        if (Enum.GetName(typeof(Enums.TipoMedida), detalle.tbProducto.idMedida).Trim().ToUpper() == "SP")//valida si es producto o servicio y acumula en su respectiva variable
                        {

                            totalServExcentos += detalle.montoTotal;
                        }
                        else//si es producto
                        {

                            totalProdExcentos += detalle.montoTotal;

                        }


                    }
                    else
                    {
                        impuestos += detalle.montoTotalImp;

                        if (Enum.GetName(typeof(Enums.TipoMedida), detalle.tbProducto.idMedida).Trim().ToUpper() == "SP")
                        {//si tiene impuesto y es un servicio

                            totalSevGravados += detalle.montoTotal;
                        }
                        else
                        {

                            totalProdGravados += detalle.montoTotal;//si es un prodcuto

                        }


                    }



                }
                writer.WriteElementString("TotalServGravados", String.Format("{0:N3}", totalSevGravados.ToString()));
                writer.WriteElementString("TotalServExentos", String.Format("{0:N3}", totalServExcentos.ToString()));

                writer.WriteElementString("TotalMercanciasGravadas", String.Format("{0:N3}", totalProdGravados.ToString()));
                writer.WriteElementString("TotalMercanciasExentas", String.Format("{0:N3}", totalProdExcentos.ToString()));

                decimal totalGravados = totalSevGravados + totalProdGravados;
                decimal totalExentos = totalProdExcentos + totalServExcentos;

                writer.WriteElementString("TotalGravado", String.Format("{0:N3}", totalGravados.ToString()));
                writer.WriteElementString("TotalExento", String.Format("{0:N3}", totalExentos.ToString()));

                decimal totalVenta = totalGravados + totalExentos;//total de venta, gravados y exonerados
                decimal totalVentaNeta = totalVenta - totalDescuento;//calula el monto de descuento
                writer.WriteElementString("TotalVenta", String.Format("{0:N3}", totalVenta.ToString()));
                writer.WriteElementString("TotalDescuentos", String.Format("{0:N3}", totalDescuento.ToString()));
                writer.WriteElementString("TotalVentaNeta", String.Format("{0:N3}", totalVentaNeta.ToString()));
                if (totalGravados != 0)
                {
                    writer.WriteElementString("TotalImpuesto", String.Format("{0:N3}", impuestos.ToString()));
                }

                writer.WriteElementString("TotalComprobante", String.Format("{0:N3}", totalComprobante.ToString()));
                writer.WriteEndElement(); // ResumenFactura

                // 'Estos datos te los tiene que brindar los encargados del area financiera
                writer.WriteStartElement("Normativa");
                writer.WriteElementString("NumeroResolucion", _empresa.numeroResolucion.Trim());
                writer.WriteElementString("FechaResolucion", ((DateTime)_empresa.fechaResolucio).ToString("dd-MM-yyyy HH:MM:ss"));
                writer.WriteEndElement(); // Normativa

                if (_doc.observaciones != null && _doc.observaciones.Trim() != string.Empty)
                {
                    //otros
                    writer.WriteStartElement("Otros");
                    writer.WriteElementString("OtroTexto", _doc.observaciones.Substring(0, 180).Trim());
                    writer.WriteEndElement();//Otros
                }

                // 'Aqui va la firma, despues la agregamos.

                writer.WriteEndElement();
                writer.WriteEndDocument();
                writer.Flush();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void GeneraXMLNotaCredito(System.Xml.XmlTextWriter writer) // As System.Xml.XmlTextWriter
        {
            try
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("NotaCreditoElectronica");

                writer.WriteAttributeString("xmlns", "https://tribunet.hacienda.go.cr/docs/esquemas/2017/v4.2/notaCreditoElectronica");
                writer.WriteAttributeString("xmlns:xsd", "http://www.w3.org/2001/XMLSchema");
                writer.WriteAttributeString("xmlns:xsi", "http://www.w3.org/2001/XMLSchema-instance");
                writer.WriteAttributeString("xsi:schemaLocation", "https://tribunet.hacienda.go.cr/docs/esquemas/2017/v4.2/notaCreditoElectronica https://tribunet.hacienda.go.cr/docs/esquemas/2017/v4.2/notaCreditoElectronica.xsd");

                // La clave se crea con la función CreaClave de la clase Datos
                writer.WriteElementString("Clave", _numeroClave);

                // 'El numero de secuencia es de 20 caracteres, 
                // 'Se debe de crear con la función CreaNumeroSecuencia de la clase Datos
                writer.WriteElementString("NumeroConsecutivo", _numeroConsecutivo);

                // 'El formato de la fecha es yyyy-MM-ddTHH:mm:sszzz
                writer.WriteElementString("FechaEmision", DateTime.Now.ToString("yyyy-MM-ddTHH:mm:sszzz"));

                writer.WriteStartElement("Emisor");

                writer.WriteElementString("Nombre", _emisor.Nombre);
                writer.WriteStartElement("Identificacion");
                writer.WriteElementString("Tipo", _emisor.Identificacion_Tipo);
                writer.WriteElementString("Numero", _emisor.Identificacion_Numero);
                writer.WriteEndElement(); // 'Identificacion

                // '-----------------------------------
                // 'Los datos de las ubicaciones los puede tomar de las tablas de datos, 
                // 'Debe ser exacto al que hacienda tiene registrado para su compañia
                writer.WriteStartElement("Ubicacion");
                writer.WriteElementString("Provincia", _emisor.Ubicacion_Provincia);
                writer.WriteElementString("Canton", _emisor.Ubicacion_Canton);
                writer.WriteElementString("Distrito", _emisor.Ubicacion_Distrito);
                writer.WriteElementString("Barrio", _emisor.Ubicacion_Barrio);
                writer.WriteElementString("OtrasSenas", _emisor.Ubicacion_OtrasSenas);
                writer.WriteEndElement(); // 'Ubicacion

                writer.WriteStartElement("Telefono");
                writer.WriteElementString("CodigoPais", _emisor.Telefono_CodigoPais);
                writer.WriteElementString("NumTelefono", _emisor.Telefono_Numero.ToString());
                writer.WriteEndElement(); // 'Telefono

                writer.WriteElementString("CorreoElectronico", _emisor.CorreoElectronico);

                writer.WriteEndElement(); // Emisor
                                          // '------------------------------------
                                          // 'Receptor es similar con emisor, los datos opcionales siempre siempre siempre omitirlos.
                                          // 'La ubicacion para el receptor es opcional por ejemplo
                if (_receptor != null)
                {
                    writer.WriteStartElement("Receptor");
                    writer.WriteElementString("Nombre", _receptor.Nombre);
                    writer.WriteStartElement("Identificacion");
                    // 'Los tipos de identificacion los puede ver en la tabla de datos
                    writer.WriteElementString("Tipo", _receptor.Identificacion_Tipo);
                    writer.WriteElementString("Numero", _receptor.Identificacion_Numero);
                    writer.WriteEndElement(); // 'Identificacion

                    //writer.WriteStartElement("Telefono");
                    //writer.WriteElementString("CodigoPais", _receptor.Telefono_CodigoPais);
                    //writer.WriteElementString("NumTelefono", _receptor.Telefono_Numero.ToString());
                    //writer.WriteEndElement(); // 'Telefono

                    writer.WriteElementString("CorreoElectronico", _receptor.CorreoElectronico);

                    writer.WriteEndElement(); // Receptor

                }

                // 'Loa datos estan en la tabla correspondiente
                writer.WriteElementString("CondicionVenta", _condicionVenta);
                // '01: Contado
                // '02: Credito
                // '03: Consignación
                // '04: Apartado
                // '05: Arrendamiento con opcion de compra
                // '06: Arrendamiento con función financiera
                // '99: Otros

                // 'Este dato se muestra si la condicion venta es credito

                writer.WriteElementString("PlazoCredito", _plazoCredito);



                writer.WriteElementString("MedioPago", _medioPago);
                // '01: Efectivo
                // '02: Tarjeta
                // '03: Cheque
                // '04: Transferecia - deposito bancario
                // '05: Recaudado por terceros            
                // '99: Otros

                writer.WriteStartElement("DetalleServicio");
                //-----variables resumes de montos pagados


                // '-------------------------------------
                foreach (tbDetalleDocumento detalle in _listaDetalle)
                {
                    writer.WriteStartElement("LineaDetalle");

                    writer.WriteElementString("NumeroLinea", detalle.numLinea.ToString().Trim());

                    //writer.WriteStartElement("Codigo");
                    //writer.WriteElementString("Tipo", "01");//codigo del prodcuto del vendedor nota 12.
                    //writer.WriteElementString("Codigo", detalle.idProducto.ToString().Trim());
                    //writer.WriteEndElement(); // 'Codigo

                    int cant = (int)detalle.cantidad;//convierte entero, elimina decimales
                    writer.WriteElementString("Cantidad", cant.ToString());
                    // 'Para las unidades de medida ver la tabla correspondiente
                    writer.WriteElementString("UnidadMedida", Enum.GetName(typeof(Enums.TipoMedida), detalle.tbProducto.idMedida));//nomenlatura kg etc

                    writer.WriteElementString("Detalle", detalle.tbProducto.nombre.ToString().Trim());


                    writer.WriteElementString("PrecioUnitario", String.Format("{0:N3}", detalle.precio.ToString().Trim()));
                    writer.WriteElementString("MontoTotal", String.Format("{0:N3}", detalle.montoTotal.ToString().Trim()));
                    writer.WriteElementString("MontoDescuento", String.Format("{0:N3}", detalle.montoTotalDesc.ToString().Trim()));


                    if (detalle.montoTotalDesc == 0)
                    {

                        writer.WriteElementString("NaturalezaDescuento", "Sin descuento");//agregar naturaleza desc
                    }
                    else
                    {
                        writer.WriteElementString("NaturalezaDescuento", "Descuento aplicado al cliente");//agregar naturaleza desc
                    }

                    writer.WriteElementString("SubTotal", String.Format("{0:N3}", (detalle.montoTotal - detalle.montoTotalDesc).ToString().Trim()));




                    if (detalle.montoTotalImp != 0)
                    {

                        writer.WriteStartElement("Impuesto");
                        writer.WriteElementString("Codigo", detalle.tbProducto.tbImpuestos.id.ToString().PadLeft(2, '0'));// impueto aplicado codigo
                        writer.WriteElementString("Tarifa", detalle.tbProducto.tbImpuestos.valor.ToString());// %aplicado valor
                        writer.WriteElementString("Monto", String.Format("{0:N3}", (detalle.montoTotalImp).ToString().Trim()));
                        writer.WriteEndElement(); // Impuesto
                    }
                    writer.WriteElementString("MontoTotalLinea", String.Format("{0:N3}", detalle.totalLinea.ToString().Trim()));
                    writer.WriteEndElement(); // LineaDetalle
                }
                // '-------------------------------------

                writer.WriteEndElement(); // DetalleServicio


                writer.WriteStartElement("ResumenFactura");

                // Estos campos son opcionales, solo fin desea facturar en dólares
                writer.WriteElementString("CodigoMoneda", _codigoMoneda);
                if (_codigoMoneda.ToUpper().Trim() == "USD")
                {
                    writer.WriteElementString("TipoCambio", String.Format("{0:N3}", _empresa.tbParametrosEmpresa.First().cambioDolar.ToString().Trim()));

                }
                else
                {
                    writer.WriteElementString("TipoCambio", String.Format("{0:N3}", "1"));

                }
                // =================

                // 'En esta parte los totales se pueden ir sumando linea a linea cuando se carga el detalle
                // 'ó se pasa como parametros al inicio


                decimal totalProdGravados = 0;
                decimal totalProdExcentos = 0;
                decimal totalSevGravados = 0;
                decimal totalServExcentos = 0;
                decimal totalDescuento = 0;
                decimal totalComprobante = 0;
                decimal impuestos = 0;

                foreach (tbDetalleDocumento detalle in _listaDetalle)
                {
                    totalDescuento += detalle.montoTotalDesc;//total de decuento
                    totalComprobante += detalle.totalLinea;//total de comprobante


                    if (detalle.montoTotalImp == 0)//valida si exonera es 0, si tiene valor de impuesto no exonera
                    {//acumulo monto total, sin descuento. monto total es precio*cantidad
                        if (Enum.GetName(typeof(Enums.TipoMedida), detalle.tbProducto.idMedida).Trim().ToUpper() == "SP")//valida si es producto o servicio y acumula en su respectiva variable
                        {

                            totalServExcentos += detalle.montoTotal;
                        }
                        else//si es producto
                        {

                            totalProdExcentos += detalle.montoTotal;

                        }
                    }
                    else
                    {
                        impuestos += detalle.montoTotalImp;

                        if (Enum.GetName(typeof(Enums.TipoMedida), detalle.tbProducto.idMedida).Trim().ToUpper() == "SP")
                        {//si tiene impuesto y es un servicio

                            totalSevGravados += detalle.montoTotal;
                        }
                        else
                        {

                            totalProdGravados += detalle.montoTotal;//si es un prodcuto

                        }
                    }
                }
                writer.WriteElementString("TotalServGravados", String.Format("{0:N3}", totalSevGravados.ToString()));
                writer.WriteElementString("TotalServExentos", String.Format("{0:N3}", totalServExcentos.ToString()));

                writer.WriteElementString("TotalMercanciasGravadas", String.Format("{0:N3}", totalProdGravados.ToString()));
                writer.WriteElementString("TotalMercanciasExentas", String.Format("{0:N3}", totalProdExcentos.ToString()));

                decimal totalGravados = totalSevGravados + totalProdGravados;
                decimal totalExentos = totalProdExcentos + totalServExcentos;

                writer.WriteElementString("TotalGravado", String.Format("{0:N3}", totalGravados.ToString()));
                writer.WriteElementString("TotalExento", String.Format("{0:N3}", totalExentos.ToString()));

                decimal totalVenta = totalGravados + totalExentos;//total de venta, gravados y exonerados
                decimal totalVentaNeta = totalVenta - totalDescuento;//calula el monto de descuento
                writer.WriteElementString("TotalVenta", String.Format("{0:N3}", totalVenta.ToString()));
                writer.WriteElementString("TotalDescuentos", String.Format("{0:N3}", totalDescuento.ToString()));
                writer.WriteElementString("TotalVentaNeta", String.Format("{0:N3}", totalVentaNeta.ToString()));
                if (totalGravados != 0)
                {
                    writer.WriteElementString("TotalImpuesto", String.Format("{0:N3}", impuestos.ToString()));
                }

                writer.WriteElementString("TotalComprobante", String.Format("{0:N3}", totalComprobante.ToString()));
                writer.WriteEndElement(); // ResumenFactura


                writer.WriteStartElement("InformacionReferencia");//referencia
                writer.WriteElementString("TipoDoc", _doc.tipoDocRef.ToString().PadLeft(2, '0'));
                writer.WriteElementString("Numero", _doc.claveRef);
                writer.WriteElementString("FechaEmision", _doc.fechaRef.Value.ToString("yyyy-MM-ddTHH:mm:sszzz"));
                writer.WriteElementString("Codigo", _doc.codigoRef.ToString().PadLeft(2, '0'));
                writer.WriteElementString("Razon", _doc.razon.ToUpper().Substring(0, 180).Trim());
                writer.WriteEndElement(); // referencia


                // 'Estos datos te los tiene que brindar los encargados del area financiera
                writer.WriteStartElement("Normativa");
                writer.WriteElementString("NumeroResolucion", _empresa.numeroResolucion.Trim());
                writer.WriteElementString("FechaResolucion", ((DateTime)_empresa.fechaResolucio).ToString("dd-MM-yyyy HH:MM:ss"));
                writer.WriteEndElement(); // Normativa

                // 'Aqui va la firma, despues la agregamos.

                writer.WriteEndElement();
                writer.WriteEndDocument();
                writer.Flush();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

    }

}
