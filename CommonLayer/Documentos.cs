using EntityLayer;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Xml;

namespace CommonLayer
{
    public static class Documentos
    {

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
            documento.CodActividad = xDoc.GetElementsByTagName("CodigoActividad").Item(0).InnerText;

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
