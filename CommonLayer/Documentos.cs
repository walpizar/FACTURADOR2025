using EntityLayer;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Xml;
using System.Xml.Linq;

namespace CommonLayer
{
    public static class Documentos
    {
        public static tbCompras ObtenerCompraXmlV44(string file)
        {
            // Cargar con LINQ to XML para trabajar con namespaces de forma más flexible
            XDocument doc = XDocument.Load(file, LoadOptions.PreserveWhitespace | LoadOptions.SetLineInfo);

            // Resolver namespace, si existe
            // Usa el namespace por defecto del documento si lo hay; de lo contrario, opera sin namespace.
            XNamespace ns = doc.Root?.GetDefaultNamespace() ?? XNamespace.None;

            // Funciones locales de ayuda (safe read & parse)
            string S(XElement parent, string localName) =>
                parent?.Element(ns + localName)?.Value?.Trim();

            string SMany(XElement parent, params string[] names)
            {
                foreach (var n in names)
                {
                    var v = S(parent, n);
                    if (!string.IsNullOrWhiteSpace(v)) return v;
                }
                return null;
            }

            bool TryDec(string s, out decimal v) =>
                decimal.TryParse(s, NumberStyles.Any, CultureInfo.InvariantCulture, out v);

            bool TryInt(string s, out int v) => int.TryParse(s, NumberStyles.Any, CultureInfo.InvariantCulture, out v);

            decimal D(string s, decimal def = 0m) => TryDec(s, out var v) ? v : def;
            int I(string s, int def = 0) => TryInt(s, out var v) ? v : def;

            DateTime DT(string s, DateTime? def = null)
            {
                if (DateTime.TryParse(s, CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal | DateTimeStyles.RoundtripKind, out var d))
                    return d;
                if (DateTime.TryParse(s, CultureInfo.InvariantCulture, DateTimeStyles.None, out d))
                    return d;
                return def ?? DateTime.Now;
            }

            // Instancia del resultado
            var documento = new tbCompras
            {
                reporteElectronico = false,
                estado = true,
                fecha = Utility.getDate(),
                fechaReporte = Utility.getDate(),
                fecha_crea = Utility.getDate(),
                fecha_ult_mod = Utility.getDate(),
                usuario_crea = Global.Usuario.nombreUsuario.Trim().ToUpper(),
                usuario_ult_mod = Global.Usuario.nombreUsuario.Trim().ToUpper(),
                sucursal = Global.Configuracion.sucursal,
                caja = Global.Configuracion.caja,
                codigoActividadEmpresa = Global.actividadEconomic.CodActividad,
                tipoPago = 1 // Valor por defecto, se sobreescribe si hay datos

            };

            // Tipo de documento por elemento raíz
            var rootName = doc.Root?.Name.LocalName?.ToUpperInvariant() ?? "";
            bool esNC = rootName == "NOTACREDITOELECTRONICA";
            documento.tipoDoc = esNC ? (int)Enums.TipoDocumento.NotaCredito : (int)Enums.TipoDocumento.Compras;

            // Campos de cabecera
            documento.claveEmisor = S(doc.Root, "Clave");
            documento.consecutivoEmisor = S(doc.Root, "NumeroConsecutivo");
            // Num factura con mismo criterio previo (10..20)
            if (!string.IsNullOrWhiteSpace(documento.consecutivoEmisor) && documento.consecutivoEmisor.Length >= 20)
                documento.numFactura = documento.consecutivoEmisor.Substring(10, 10);

            // CodigoActividad (nuevo nombre correcto)
            var codigoActividad = S(doc.Root, "CodigoActividadEmisor");
            if (!string.IsNullOrWhiteSpace(codigoActividad))
                documento.CodActividad = codigoActividad;

            //var codigoActividadReceptor = S(doc.Root, "CodigoActividadRecpetor");
            //if (!string.IsNullOrWhiteSpace(codigoActividadReceptor))
            //    documento.ac = codigoActividadReceptor;

            // Fecha emision
            documento.fechaCompra = Utility.ParseFechaHacienda(S(doc.Root, "FechaEmision"));

            // Condición de venta y medio de pago
            documento.tipoCompra = I(S(doc.Root, "CondicionVenta"));
            // Plazo (solo si crédito)
            var plazoStr = S(doc.Root, "PlazoCredito");
            if (!string.IsNullOrWhiteSpace(plazoStr) && Utility.isNumeroDecimal(plazoStr))
                documento.plazo = (int)documento.tipoCompra == (int)Enums.tipoVenta.Credito ? I(plazoStr) : 0;
            else
                documento.plazo = 0;

            // Medios de pago: múltiples <MedioPago>, seleccionar el primero como "tipoPago" y guardar todos si se requiere
            var mediosPago = doc.Root.Descendants(ns + "MedioPago").Select(x => x.Value?.Trim()).Where(v => !string.IsNullOrWhiteSpace(v)).ToList();
            if (mediosPago.Count > 0)
                documento.tipoPago = I(mediosPago[0]);

            // Emisor
            var emisor = doc.Root.Element(ns + "Emisor");
            if (emisor != null)
            {
                var ident = emisor.Element(ns + "Identificacion");
                documento.tipoIdProveedor = I(S(ident, "Tipo"));
                documento.idProveedor = S(ident, "Numero");
                documento.nombreProveedor = S(emisor, "Nombre");
            }

            // Receptor (empresa propia)
            var receptor = doc.Root.Element(ns + "Receptor");
            if (receptor != null)
            {
                var identR = receptor.Element(ns + "Identificacion");
                documento.tipoIdEmpresa = I(S(identR, "Tipo"));
                documento.idEmpresa = S(identR, "Numero");
            }

            // Moneda
            var resumen = doc.Root.Element(ns + "ResumenFactura");
            if (resumen != null)
            {
                var codigoTipoMoneda = resumen.Element(ns + "CodigoTipoMoneda");
                string codMon = S(codigoTipoMoneda, "CodigoMoneda");
                if (string.Equals(codMon, "USD", StringComparison.OrdinalIgnoreCase))
                {
                    documento.tipoMoneda = (int)Enums.TipoMoneda.USD;
                    documento.tipoCambio = D(S(codigoTipoMoneda, "TipoCambio"), 1m);
                    documento.cambiarColon = true;
                }
                else
                {
                    documento.tipoMoneda = (int)Enums.TipoMoneda.CRC;
                    documento.cambiarColon = false;
                }
            }
            else
            {
                documento.tipoMoneda = (int)Enums.TipoMoneda.CRC;
                documento.cambiarColon = false;
            }

            // InformacionReferencia (para NC/ND) - nombres correctos
            var infoRef = doc.Root.Element(ns + "InformacionReferencia");
            if (infoRef != null && esNC)
            {
                documento.tipoDocRef = I(S(infoRef, "TipoDoc"));
                documento.claveRef = S(infoRef, "Numero");
                documento.fechaRef = DT(S(infoRef, "FechaEmision"));
                documento.codigoRef = I(S(infoRef, "Codigo"));
                documento.razon = S(infoRef, "Razon");
            }

            // DetalleServicio
            var listaDetalleCompras = new List<tbDetalleCompras>();
            var detServicio = doc.Root.Element(ns + "DetalleServicio");
            if (detServicio != null)
            {
                foreach (var linea in detServicio.Elements(ns + "LineaDetalle"))
                {
                    var d = new tbDetalleCompras
                    {
                        actualizaPrecio = false,
                        actualizaInvent = false
                    };

                    d.numLinea = I(S(linea, "NumeroLinea"));

                    // CABYS en <Codigo>
                    d.codigoCabys = S(linea, "CodigoCABYS");

                    // CodigoComercial opcional -> proveedor
                    var codCom = linea.Element(ns + "CodigoComercial");
                    if (codCom != null)
                        d.idProductoProveedor = S(codCom, "Codigo");
                    else
                        d.idProductoProveedor = d.codigoCabys; // fallback

                    d.idProducto = "0";

                    d.nombreProducto = S(linea, "Detalle");
                    if (!string.IsNullOrEmpty(d.nombreProducto) && d.nombreProducto.Length > 159)
                        d.nombreProducto = d.nombreProducto.Substring(0, 159);

                    d.nomenclatura = S(linea, "UnidadMedida");

                    d.precio = D(S(linea, "PrecioUnitario"));
                    d.cantidad = D(S(linea, "Cantidad"));
                    d.cantidadVenta = d.cantidad;

                    d.montoTotal = D(S(linea, "MontoTotal"));

                    // Descuento
                    var nodoDesc = linea.Element(ns + "Descuento");
                    if (nodoDesc != null)
                        d.montoTotaDesc = D(S(nodoDesc, "MontoDescuento"));

                    // Impuestos (pueden venir varios)
                    decimal acumuladoOtrosImp = 0m;
                    decimal impuestoPrincipalMonto = 0m;
                    decimal impuestoPrincipalTarifa = 0m;
                    foreach (var imp in linea.Elements(ns + "Impuesto"))
                    {
                        var codigoImp = S(imp, "Codigo");           // "01" IVA
                        var tarifa = D(S(imp, "Tarifa"));
                        var monto = D(S(imp, "Monto"));

                        // Si tarifa es 0, lo tratamos como otros/0% y lo acumulamos aparte
                        if (tarifa == 0m)
                            acumuladoOtrosImp += monto;
                        else
                        {
                            // Considerar IVA como principal si Codigo = "01"
                            if (string.Equals(codigoImp, "01"))
                            {
                                impuestoPrincipalTarifa = tarifa;
                                impuestoPrincipalMonto += monto;
                            }
                            else
                            {
                                // otros impuestos con tarifa > 0 (si aplica)
                                acumuladoOtrosImp += monto;
                            }
                        }

                        // Exoneración (si existe)
                        var exo = imp.Element(ns + "Exoneracion");
                        if (exo != null)
                            d.montoTotalExo = D(S(exo, "MontoExoneracion"));
                    }

                    d.montoOtroImp = acumuladoOtrosImp;
                    d.tarifaImp = impuestoPrincipalTarifa;
                    d.montoTotalImp = impuestoPrincipalMonto;
                    d.tarifaImpVenta = d.tarifaImp;
                   
                    d.montoTotalLinea = D(S(linea, "MontoTotalLinea"));

                    listaDetalleCompras.Add(d);
                }
            }
            documento.tbDetalleCompras = listaDetalleCompras;

            return documento;
        }

        public static tbCompras obtenerCompraXml(string file)
        {


            tbCompras documento = new tbCompras();
            XmlDocument xDoc = new XmlDocument();

            xDoc.Load(file);


            bool isNC = xDoc.DocumentElement.Name.ToUpper() == "NOTACREDITOELECTRONICA";

            documento.reporteElectronico = false;

            documento.tipoDoc = isNC ? (int)Enums.TipoDocumento.NotaCredito : (int)Enums.TipoDocumento.Compras;


            documento.reporteElectronico = false;
            documento.claveEmisor = xDoc.GetElementsByTagName("Clave").Item(0).InnerText;
            documento.consecutivoEmisor = xDoc.GetElementsByTagName("NumeroConsecutivo").Item(0).InnerText;

            if (xDoc.GetElementsByTagName("CodigoActividad").Item(0) != null)
            {
                documento.CodActividad = xDoc.GetElementsByTagName("CodigoActividad").Item(0).InnerText;

            }
         
            documento.fecha = Utility.getDate();
            documento.numFactura = documento.consecutivoEmisor.Substring(10, 10);
            documento.fechaCompra = DateTime.Parse(xDoc.GetElementsByTagName("FechaEmision").Item(0).InnerText);
            documento.fechaReporte = Utility.getDate();
            documento.tipoPago = int.Parse(xDoc.GetElementsByTagName("MedioPago").Item(0).InnerText);
            documento.tipoCompra = int.Parse(xDoc.GetElementsByTagName("CondicionVenta").Item(0).InnerText);


            //proveddor 
            var emisor = xDoc.GetElementsByTagName("Emisor");
            var identificacion = ((XmlElement)emisor[0]).GetElementsByTagName("Identificacion");
            //var correo = ((XmlElement)emisor[0]).GetElementsByTagName("CorreoElectronico").Item(0).InnerText;

   

            documento.tipoIdProveedor = int.Parse(((XmlElement)identificacion[0]).GetElementsByTagName("Tipo").Item(0).InnerText);
            documento.idProveedor = ((XmlElement)identificacion[0]).GetElementsByTagName("Numero").Item(0).InnerText;
            documento.nombreProveedor = ((XmlElement)emisor[0]).GetElementsByTagName("Nombre").Item(0).InnerText;
            
            //receptor
            var receptor = xDoc.GetElementsByTagName("Receptor");
            var identificacionReceptor = ((XmlElement)receptor[0]).GetElementsByTagName("Identificacion");
            documento.tipoIdEmpresa = int.Parse(((XmlElement)identificacionReceptor[0]).GetElementsByTagName("Tipo").Item(0).InnerText);
            documento.idEmpresa = ((XmlElement)identificacionReceptor[0]).GetElementsByTagName("Numero").Item(0).InnerText;

            if (xDoc.GetElementsByTagName("PlazoCredito").Item(0) != null)
            {
                if (Utility.isNumeroDecimal(xDoc.GetElementsByTagName("PlazoCredito").Item(0).InnerText))
                {
                    documento.plazo = (int)documento.tipoCompra == (int)Enums.tipoVenta.Credito ? int.Parse(xDoc.GetElementsByTagName("PlazoCredito").Item(0).InnerText) : 0;

                }
                else
                {
                    documento.plazo = 0;
                }
            }




            documento.sucursal = Global.Configuracion.sucursal;
            documento.caja = Global.Configuracion.caja;

            //moneda
            var resumen = xDoc.GetElementsByTagName("ResumenFactura");
            string codigoMoneda = "CRC";
            if (resumen.Count != 0)
            {
                var codigoTipoMoneda = ((XmlElement)resumen[0]).GetElementsByTagName("CodigoTipoMoneda");
                if (codigoTipoMoneda.Count > 0)
                {
                    documento.tipoMoneda = ((XmlElement)codigoTipoMoneda[0]).GetElementsByTagName("CodigoMoneda").Item(0).InnerText.Trim() == "USD" ? 1 : 0;
                }
                else
                {
                    documento.tipoMoneda = 0;
                }




                if (documento.tipoMoneda == 0)
                {
                    documento.tipoMoneda = (int)Enums.TipoMoneda.CRC;
                    documento.cambiarColon = false;
                }
                else
                {
                    documento.tipoMoneda = (int)Enums.TipoMoneda.USD;
                    documento.tipoCambio = decimal.Parse(((XmlElement)codigoTipoMoneda[0]).GetElementsByTagName("TipoCambio").Item(0).InnerText.Trim());
                    documento.cambiarColon = true;
                }

            }

            //en caso de nota de credito
            if (documento.tipoDoc == (int)Enums.TipoDocumento.NotaCredito)
            {

                documento.tipoDocRef = int.Parse(((XmlElement)xDoc.GetElementsByTagName("InformacionReferencia")[0]).GetElementsByTagName("TipoDoc").Item(0).InnerText);
                documento.claveRef = ((XmlElement)xDoc.GetElementsByTagName("InformacionReferencia")[0]).GetElementsByTagName("Numero").Item(0).InnerText;
                documento.fechaRef = DateTime.Parse(((XmlElement)xDoc.GetElementsByTagName("InformacionReferencia")[0]).GetElementsByTagName("FechaEmision").Item(0).InnerText);
                documento.codigoRef = int.Parse(((XmlElement)xDoc.GetElementsByTagName("InformacionReferencia")[0]).GetElementsByTagName("Codigo").Item(0).InnerText);
                documento.razon = ((XmlElement)xDoc.GetElementsByTagName("InformacionReferencia")[0]).GetElementsByTagName("Razon").Item(0).InnerText;


            }

            documento.estado = true;

            //Atributos de Auditoria

            documento.fecha_crea = Utility.getDate();
            documento.fecha_ult_mod = Utility.getDate();
            documento.usuario_crea = Global.Usuario.nombreUsuario.Trim().ToUpper();   // Global.Usuario.nombreUsuario;
            documento.usuario_ult_mod = Global.Usuario.nombreUsuario.Trim().ToUpper();   // Global.Usuario.nombreUsuario;

            documento.codigoActividadEmpresa = Global.actividadEconomic.CodActividad;



            List<tbDetalleCompras> listaDetalleCompras = new List<tbDetalleCompras>();
            var detalleSer = xDoc.GetElementsByTagName("DetalleServicio").Item(0);
            foreach (var item in detalleSer)
            {
                try
                {

                    tbDetalleCompras detalle = new tbDetalleCompras();
                    detalle.actualizaPrecio = false;
                    detalle.actualizaInvent = false;

                    detalle.numLinea = int.Parse(((XmlElement)item).GetElementsByTagName("NumeroLinea").Item(0).InnerText);


                    if (((XmlElement)item).GetElementsByTagName("Codigo").Item(0) != null)
                    {
                        detalle.codigoCabys = ((XmlElement)item).GetElementsByTagName("Codigo").Item(0).InnerText;
                    }


                    var codComercialNode = ((XmlElement)item).GetElementsByTagName("CodigoComercial").Item(0);
                    if (codComercialNode != null)
                    {

                        detalle.idProductoProveedor = ((XmlElement)codComercialNode).GetElementsByTagName("Codigo").Item(0).InnerText;

                    }
                    else
                    {
                        detalle.idProductoProveedor = ((XmlElement)item).GetElementsByTagName("Codigo").Item(0).InnerText;
                    }


                    detalle.idProducto = "0";

                    
                    detalle.nombreProducto = ((XmlElement)item).GetElementsByTagName("Detalle").Item(0).InnerText;
                    if (detalle.nombreProducto.Length > 159)
                    {
                        detalle.nombreProducto = detalle.nombreProducto.Substring(0, 159);
 
                    }
                    detalle.nomenclatura = ((XmlElement)item).GetElementsByTagName("UnidadMedida").Item(0).InnerText;


                    string precio = ((XmlElement)item).GetElementsByTagName("PrecioUnitario").Item(0).InnerText.Trim();

                    detalle.precio = decimal.Parse(string.Format(precio, CultureInfo.CurrentCulture));

                    var cant = ((XmlElement)item).GetElementsByTagName("Cantidad").Item(0).InnerText.Trim();
                    detalle.cantidad = decimal.Parse(cant);
                    detalle.cantidadVenta = detalle.cantidad;

                    var montoTotal = ((XmlElement)item).GetElementsByTagName("MontoTotal").Item(0).InnerText.Trim();
                    detalle.montoTotal = decimal.Parse(montoTotal);

                    var des = ((XmlElement)item).GetElementsByTagName("Descuento").Item(0);
                    if (des != null)
                    {
                        detalle.montoTotaDesc = decimal.Parse(((XmlElement)des).GetElementsByTagName("MontoDescuento").Item(0).InnerText.Trim());

                    }
                    detalle.montoOtroImp = 0;
                    var impuesto = ((XmlElement)item).GetElementsByTagName("Impuesto").Item(0);
                    if (impuesto != null)
                    {
                        decimal impAntes = 0, tarifaImp = 0;
                        foreach (var listaImp in ((XmlElement)item).GetElementsByTagName("Impuesto"))
                        {
                            tarifaImp = decimal.Parse(((XmlElement)listaImp).GetElementsByTagName("Tarifa").Item(0).InnerText.Trim());
                            if (tarifaImp == 0)
                            {

                                impAntes += decimal.Parse(((XmlElement)listaImp).GetElementsByTagName("Monto").Item(0).InnerText.Trim());
                            }
                            else
                            {
                                detalle.tarifaImp = decimal.Parse(((XmlElement)listaImp).GetElementsByTagName("Tarifa").Item(0).InnerText.Trim());
                                detalle.montoTotalImp = decimal.Parse(((XmlElement)listaImp).GetElementsByTagName("Monto").Item(0).InnerText.Trim());

                            }

                        }
                        detalle.montoOtroImp = impAntes;

                        var exo = ((XmlElement)impuesto).GetElementsByTagName("Exoneracion").Item(0);
                        if (exo != null)
                        {

                            detalle.montoTotalExo = decimal.Parse(((XmlElement)exo).GetElementsByTagName("MontoExoneracion").Item(0).InnerText.Trim());
                        }

                        //detalle.tarifaImp = decimal.Parse(((XmlElement)impuesto).GetElementsByTagName("Tarifa").Item(0).InnerText.Trim());
                        //detalle.montoTotalImp = decimal.Parse(((XmlElement)impuesto).GetElementsByTagName("Monto").Item(0).InnerText.Trim());

                        //if (detalle.tarifaImp==0 && detalle.montoTotalImp!=0)
                        //{
                        //    detalle.tarifaImp = 99;

                        //}


                    }

                    
                    detalle.tarifaImpVenta = detalle.tarifaImp;

                    var totaLinea = ((XmlElement)item).GetElementsByTagName("MontoTotalLinea").Item(0).InnerText.Trim();
                    detalle.montoTotalLinea = decimal.Parse(totaLinea);

                    detalle.numLinea = int.Parse(((XmlElement)item).GetElementsByTagName("NumeroLinea").Item(0).InnerText);

                    //if (detalleDoc.Where(x=>x.idProducto==detalle.idProducto).SingleOrDefault()!=null)
                    //{
                    //    string mensaje = string.Format("La linea #{0} del XML se encuentra repetida({1}). Se eliminó del detalle.", ((XmlElement)item).GetElementsByTagName("NumeroLinea").Item(0).InnerText, detalle.nombreProducto.Trim());

                    //    MessageBox.Show(mensaje,"Linea repetida", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //}
                    //else
                    //{



                    listaDetalleCompras.Add(detalle);
                    //}



                }
                catch (Exception ex)
                {


                }


            }

            documento.tbDetalleCompras = listaDetalleCompras;







            return documento;

        }


    }
}
