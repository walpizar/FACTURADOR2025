
using BusinessLayer;
using CommonLayer;
using EntityLayer;
using PresentationLayer.Reportes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;


namespace PresentationLayer.Clases
{
    public class clsImpresionFactura
    {
        public BFacturacion facturacionB = new BFacturacion();
        BEmpresa empresaIns = new BEmpresa();
        BCanton cantonIns = new BCanton();
        tbCanton _canton { set; get; }
        decimal _paga { set; get; }
        decimal _vuelto { set; get; }
        tbDocumento _doc { set; get; }
        tbClientes _cliente { set; get; }
        tbEmpresa _empresa { set; get; }
        List<tbDocumento> _docs { set; get; }
        List<tbDetalleDocumento> _detalle { set; get; }
        string _alias { set; get; }

        decimal _saldo { set; get; }
        List<string> _lineas { set; get; }
        bool _imprimePrecioProforma { set; get; }

        bool _abreCajon { set; get; }
        List<clsComanda> _listaComandas { set; get; }

        tbMovimientos _mov { set; get; }

         
        public clsImpresionFactura(List<tbDetalleDocumento> detalleDoc, string alias, tbEmpresa empresa )
        {
            _detalle = detalleDoc;
            _alias = alias;
            _empresa = empresa;
            _abreCajon = true;

        }
        public clsImpresionFactura(tbMovimientos mov)
        {
            _mov = mov;
            _abreCajon = true;

        }

        public clsImpresionFactura()
        {

        }
        public clsImpresionFactura(List<clsComanda> listaComandas)
        {
            this._listaComandas = listaComandas;
            _abreCajon = true;


        }
        public clsImpresionFactura(tbEmpresa empresa, List<string> lineas, bool abre)
        {
            _empresa = empresaIns.getEntityForPrint(empresa);
            _lineas = lineas;
            _abreCajon = abre;
        }

        public clsImpresionFactura(List<tbDocumento> docs, tbClientes cliente, tbEmpresa empresa, decimal saldoPend)
        {
            _docs = docs;
            _empresa = empresaIns.getEntityForPrint(empresa);
            _cliente = cliente;
            _saldo = saldoPend;
            _abreCajon = true;

        }
        public clsImpresionFactura(tbDocumento doc, tbEmpresa empresa, bool abrirCajon)
        {
            _doc = doc;
            _empresa = empresaIns.getEntityForPrint(empresa);
            _abreCajon = abrirCajon;

        }
        public clsImpresionFactura(tbDocumento doc, tbEmpresa empresa)
        {
            _doc = doc;
            _empresa = empresaIns.getEntityForPrint(empresa);
            _abreCajon = true;

        }
        public clsImpresionFactura(tbDocumento doc, tbClientes cliente, tbEmpresa empresa, bool imprimePrecioProforma)
        {
            _doc = doc;
            _empresa = empresaIns.getEntityForPrint(empresa);
            _imprimePrecioProforma = imprimePrecioProforma;
            _cliente = cliente;
            _abreCajon = false;

        }
        public clsImpresionFactura(tbDocumento doc, tbEmpresa empresa, decimal paga, decimal vuelto, bool abrirCajon)
        {
            _doc = doc;
            _empresa = empresaIns.getEntityForPrint(empresa);
            _paga = paga;
            _vuelto = vuelto;
            _canton = cantonIns.GetEntity(_empresa.tbPersona.canton, _empresa.tbPersona.provincia);
            _abreCajon = abrirCajon;
        }

        public clsImpresionFactura(tbDocumento doc, tbEmpresa empresa, decimal paga, decimal vuelto)
        {
            _doc = doc;
            _empresa = empresaIns.getEntityForPrint(empresa);
            _paga = paga;
            _vuelto = vuelto;
            _canton = cantonIns.GetEntity(_empresa.tbPersona.canton, _empresa.tbPersona.provincia);
            _abreCajon = true;
        }

        public void printComandas()
        {
            CreaTicket Ticket1 = new CreaTicket(true);


            var llevar = _listaComandas.Where(x => x.llevar == true).ToList().Count();
            if (llevar > 0)
            {
                Ticket1.TextoCentro("PARA LLEVAR");
            }
            Ticket1.TextoCentro("");
            Ticket1.TextoCentro("Fecha:" + Utility.getDate());
            Ticket1.TextoCentro("Pedido:"+_listaComandas[0].alias.Trim());
         
            foreach (var linea in _listaComandas)
            {
                if (linea.estado)
                {
                    Ticket1.TextoCentro("");
                    Ticket1.lineaComanda(linea.nombre.Trim(), linea.cant, "");
                    if (linea.descripcion != null && linea.descripcion!=string.Empty)
                    {
                        Ticket1.TextoIzquierda("Comentario:");
                        Ticket1.VariasLineas(linea.descripcion.Trim().ToUpper());


                    }
                    if (linea.acompanamientos!=null && linea.acompanamientos.Count != 0)
                    {
                        Ticket1.TextoIzquierda("Acompañamientos:");

                        foreach (String acompa in linea.acompanamientos)
                        {
                            Ticket1.TextoIzquierda(acompa);
                        }
                    }

                }
                else
                {
                    Ticket1.TextoCentro("");
                    Ticket1.lineaComanda(linea.nombre.Trim(), linea.cant, "CANCELAR" );
                    if (linea.descripcion != null && linea.descripcion != string.Empty)
                    {
                        Ticket1.TextoIzquierda("Comentario:");
                        Ticket1.TextoIzquierda(linea.descripcion.Trim().ToUpper());


                    }
                    if (linea.acompanamientos != null && linea.acompanamientos.Count != 0)
                    {
                        Ticket1.TextoIzquierda("Acompañamientos:");

                        foreach (String acompa in linea.acompanamientos)
                        {
                            Ticket1.TextoIzquierda(acompa);
                        }
                    }

                }
             
            }
            Ticket1.TextoCentro("");
            Ticket1.LineasAsterisco();
            Ticket1.CortaTicket(); // corta el ticket


        }
        public void printMovimientos()
        {
            CreaTicket Ticket1 = new CreaTicket();          
          
            Ticket1.TextoIzquierda("Fecha:" + Utility.getDate());
            Ticket1.TextoIzquierda("Sucursal:" + Global.Configuracion.sucursal);
            Ticket1.TextoIzquierda("Caja:" + Global.Configuracion.caja);
            Ticket1.TextoIzquierda("Tipo Movimiento:" +( _mov.idTipoMov==(int)Enums.tipoMovimiento.EntradaDinero? "ENTRADA DINERO":"SALIDA DE DINERO"));
            Ticket1.TextoIzquierda("Creado por:" + Global.Usuario.nombreUsuario.Trim().ToUpper());
            Ticket1.TextoCentro("");
            Ticket1.TextoIzquierda("MONTO: "+ _mov.total.ToString());
            Ticket1.TextoIzquierda("OBSERVACIONES:");
            Ticket1.TextoIzquierda(_mov.motivo.Trim().ToUpper());
            Ticket1.LineasAsterisco();
            Ticket1.TextoIzquierda("NOMBRE COMPLETO:_______________________");
            Ticket1.TextoCentro("");
            Ticket1.TextoIzquierda("FIRMA:________________________");
            Ticket1.CortaTicket(); // corta el ticket

        }


        public void print()
        {
            if (Global.Configuracion.imprime == (int)Enums.EstadoConfig.Si)
            {

                if (_lineas != null && _lineas.Count != 0)
                {
                    printEstadoCaja();
                }
                //imprimir documentos que no son abono
                else if (_doc != null)
                {
                    if (_doc.tipoDocumento == (int)Enums.TipoDocumento.Factura || _doc.tipoDocumento == (int)Enums.TipoDocumento.FacturaElectronica)
                    {

                        if (Global.Configuracion.imprimeDoc == (int)Enums.tipoImpresion.puntoVenta)
                        {
                            printPuntoVenta();
                           // printPuntoVentaReport();
                        }
                        else if (Global.Configuracion.imprimeDoc == (int)Enums.tipoImpresion.mediaCarta)
                        {
                            printMediaCarta();

                        }
                        else if (Global.Configuracion.imprimeDoc == (int)Enums.tipoImpresion.normal)
                        {
                            printMediaCarta();

                        }
                        else if (Global.Configuracion.imprimeDoc == (int)Enums.tipoImpresion.noImprime)
                        {

                            clsPDF.generarPDFFactura(_doc);
                        }

                    }
                    else if (_doc.tipoDocumento == (int)Enums.TipoDocumento.TiqueteElectronico)
                    {

                        if (Global.Configuracion.imprimeTiquete == (int)Enums.tipoImpresion.puntoVenta)
                        {
                           printPuntoVenta();
                             // printPuntoVentaReport();
                        }
                        else if (Global.Configuracion.imprimeTiquete == (int)Enums.tipoImpresion.mediaCarta)
                        {
                            printMediaCarta();

                        }
                        else if (Global.Configuracion.imprimeTiquete == (int)Enums.tipoImpresion.normal)
                        {
                            printMediaCarta();

                        }
                        else if (Global.Configuracion.imprimeTiquete == (int)Enums.tipoImpresion.noImprime)
                        {

                            clsPDF.generarPDFFactura(_doc);
                        }

                    }
                    else if (_doc.tipoDocumento == (int)Enums.TipoDocumento.NotaCredito || _doc.tipoDocumento == (int)Enums.TipoDocumento.NotaCreditoElectronica || _doc.tipoDocumento == (int)Enums.TipoDocumento.NotaDebitoElectronica)
                    {
                        if (Global.Configuracion.imprimeDoc == (int)Enums.tipoImpresion.puntoVenta)
                        {
                            printPuntoVenta();
                           // printPuntoVentaReport();
                        }
                        else if (Global.Configuracion.imprimeDoc == (int)Enums.tipoImpresion.mediaCarta)
                        {
                            printMediaCarta();

                        }
                        else if (Global.Configuracion.imprimeDoc == (int)Enums.tipoImpresion.normal)
                        {
                            printMediaCarta();

                        }
                        else if (Global.Configuracion.imprimeDoc == (int)Enums.tipoImpresion.noImprime)
                        {

                            clsPDF.generarPDFFactura(_doc);
                        }

                    }
                    else if (_doc.tipoDocumento == (int)Enums.TipoDocumento.ProformaGeneral || _doc.tipoDocumento == (int)Enums.TipoDocumento.Proforma)
                    {
                        if (Global.Configuracion.imprimeProformas == (int)Enums.tipoImpresion.puntoVenta)
                        {                           
                            printPuntoVenta();
                           // printPuntoVentaReport();
                        }
                        else if (Global.Configuracion.imprimeProformas == (int)Enums.tipoImpresion.mediaCarta)
                        {
                            printMediaCarta();

                        }
                        else if (Global.Configuracion.imprimeProformas == (int)Enums.tipoImpresion.normal)
                        {
                            printMediaCarta();

                        }
                        else if (Global.Configuracion.imprimeProformas == (int)Enums.tipoImpresion.noImprime)
                        {

                            clsPDF.generarPDFFactura(_doc);
                        }

                    }
                }
                else
                {

                    printPuntoVentaAbono();



                }
            }
            else
            {
                clsPDF.generarPDFFactura(_doc);
            }
        }

        private void printEstadoCaja()
        {
            CreaTicket Ticket1 = new CreaTicket();
            string nombreEmpresa = string.Empty;
            string nombreComercial = string.Empty;
            if (Global.actividadEconomic.nombreComercial != null)
            {
                nombreComercial = Global.actividadEconomic.nombreComercial.Trim().ToUpper();
            }

            if (_empresa.tipoId == (int)Enums.TipoId.Fisica)
            {
                nombreEmpresa = _empresa.tbPersona.nombre.ToUpper().ToString().Trim() + " " +
                        _empresa.tbPersona.apellido1.ToUpper().ToString().Trim() + " " + _empresa.tbPersona.apellido2.ToUpper().ToString().Trim();
            }
            else
            {
                nombreEmpresa = _empresa.tbPersona.nombre.ToUpper().ToString().Trim();

            }


            if (nombreComercial != string.Empty)
            {
                Ticket1.TextoCentro(nombreComercial);
            }
            Ticket1.TextoCentro(nombreEmpresa);
            Ticket1.TextoCentro(_empresa.tbPersona.tbBarrios.tbDistrito.Nombre.Trim().ToUpper() + "-" + _empresa.tbPersona.tbBarrios.tbDistrito.tbCanton.Nombre.Trim().ToUpper() + "-" + _empresa.tbPersona.tbBarrios.tbDistrito.tbCanton.tbProvincia.Nombre.Trim().ToUpper());
            Ticket1.TextoCentro((_empresa.tipoId == (int)Enums.TipoId.Fisica ? "Ced Fisica:" : "Ced Juridica:") + _empresa.tbPersona.identificacion.ToString().Trim());
            Ticket1.TextoCentro("Tel:" + _empresa.tbPersona.telefono.ToString());
            Ticket1.TextoIzquierda("Fecha:" + Utility.getDate());
            Ticket1.TextoCentro("");
            Ticket1.TextoCentro("ESTADO CAJA");
            Ticket1.TextoCentro("");

            foreach (var linea in _lineas)
            {
                if (linea == "SALTO")
                {
                    Ticket1.TextoCentro("");
                }
                else
                {
                    Ticket1.TextoIzquierda(linea);
                }

            }
            Ticket1.TextoCentro("");
            Ticket1.LineasAsterisco();
            Ticket1.CortaTicket(); // corta el ticket
            if (_abreCajon)
            {
                Ticket1.AbreCajon();  //abre el cajon

            }
       
        }

        private void printPuntoVentaAbono()
        {
            CreaTicket Ticket1 = new CreaTicket();
            Ticket1.AbreCajon();  //abre el cajon
            string nombreEmpresa = string.Empty;
            string nombreComercial = string.Empty;
            if (Global.actividadEconomic.nombreComercial != null)
            {
                nombreComercial = Global.actividadEconomic.nombreComercial.Trim().ToUpper();
            }

            if (_empresa.tipoId == (int)Enums.TipoId.Fisica)
            {
                nombreEmpresa = _empresa.tbPersona.nombre.ToUpper().ToString().Trim() + " " +
                        _empresa.tbPersona.apellido1.ToUpper().ToString().Trim() + " " + _empresa.tbPersona.apellido2.ToUpper().ToString().Trim();
            }
            else
            {
                nombreEmpresa = _empresa.tbPersona.nombre.ToUpper().ToString().Trim();

            }       


            if (nombreComercial != string.Empty)
            {
                Ticket1.TextoCentro(nombreComercial);
            }
            Ticket1.TextoCentro(nombreEmpresa);
            Ticket1.TextoCentro((_empresa.tipoId == (int)Enums.TipoId.Fisica ? "Ced Fisica:" : "Ced Juridica:") + _empresa.tbPersona.identificacion.ToString().Trim());
            Ticket1.TextoCentro(_empresa.tbPersona.tbBarrios.tbDistrito.Nombre.Trim().ToUpper() + "-" + _empresa.tbPersona.tbBarrios.tbDistrito.tbCanton.Nombre.Trim().ToUpper() + "-" + _empresa.tbPersona.tbBarrios.tbDistrito.tbCanton.tbProvincia.Nombre.Trim().ToUpper());
            Ticket1.TextoCentro(_empresa.tbPersona.otrasSenas.Trim().ToUpper());
            Ticket1.TextoCentro("Correo:" + Global.actividadEconomic.correoCompras.Trim());
            Ticket1.TextoCentro("Tel:" + _empresa.tbPersona.telefono.ToString());
            Ticket1.TextoIzquierda("Fecha:" + Utility.getDate());
            Ticket1.TextoIzquierda("Fecha:" + Utility.getDate());
            Ticket1.TextoCentro("");
            Ticket1.TextoCentro("ABONOS");
            Ticket1.TextoCentro("");
            if (_cliente.id != null)
            {

                string nombre = "";
                string id = _cliente.tbPersona.identificacion.ToString().Trim();
                if (_cliente.tbPersona.tipoId == (int)Enums.TipoId.Fisica)
                {

                    nombre = _cliente.tbPersona.nombre.ToUpper().ToString().Trim() + " " +
                       _cliente.tbPersona.apellido1.ToUpper().ToString().Trim() + " " + _cliente.tbPersona.apellido2.ToUpper().ToString().Trim();
                }
                else
                {
                    nombre = _cliente.tbPersona.nombre.ToUpper().ToString().Trim();
                }
                Ticket1.TextoIzquierda("ID Cliente:" + id);
                Ticket1.TextoIzquierda("Cliente:" + nombre);

            }

            Ticket1.LineasGuion(); // imprime una linea de guiones
            decimal totalAbonos = 0;
            decimal NC = 0;
            foreach (var abono in _docs)
            {
                var nc = facturacionB.getNCByRef(abono.clave);
                foreach (var notaCredito in nc)
                {
                    NC += (decimal)notaCredito.tbDetalleDocumento.Sum(x => x.totalLinea);

                }
                Ticket1.TextoIzquierda("# Factura:" + abono.id);
                Ticket1.TextoIzquierda("Saldo Anterior:" + string.Format("{0:N2}", abono.tbDetalleDocumento.Sum(x => x.totalLinea) - NC - abono.tbPagos.Sum(x => x.monto) + abono.tbPagos.Last().monto));
                Ticket1.TextoIzquierda("Monto abonado:" + string.Format("{0:N2}", abono.tbPagos.Last().monto));
                totalAbonos += (decimal)abono.tbPagos.Last().monto;
                Ticket1.TextoIzquierda("Saldo Actual:" + string.Format("{0:N2}", abono.tbDetalleDocumento.Sum(x => x.totalLinea) - NC - abono.tbPagos.Sum(x => x.monto)));
                Ticket1.TextoIzquierda("Estado Factura:" + Enum.GetName(typeof(Enums.EstadoFactura), abono.estadoFactura));
                Ticket1.TextoIzquierda("");
            }
            Ticket1.LineasAsterisco();
            Ticket1.TextoIzquierda("Total Abonado:" + string.Format("{0:N2}", totalAbonos));
            Ticket1.TextoIzquierda("Saldo Pendiente:" + string.Format("{0:N2}", _saldo));
            Ticket1.LineasAsterisco();
            Ticket1.TextoIzquierda("Hecho por:" + Global.Usuario.nombreUsuario.Trim().ToUpper());
            Ticket1.TextoCentro("GRACIAS POR PREFERIRNOS");

            Ticket1.CortaTicket(); // corta el ticket
        }


        public void abrirCajon()
        {
            CreaTicket Ticket1 = new CreaTicket();
            Ticket1.AbreCajon();  //abre el cajon

        }

        private void printPuntoVenta()
        {

            CreaTicket Ticket1 = new CreaTicket();
            if (_abreCajon)
            {
                Ticket1.AbreCajon();  //abre el cajon
            }
            
            string nombreEmpresa = string.Empty;
            string nombreComercial = string.Empty;
            if (Global.actividadEconomic.nombreComercial != null)
            {
                nombreComercial = Global.actividadEconomic.nombreComercial.Trim().ToUpper();
            }

            if (_empresa.tipoId == (int)Enums.TipoId.Fisica)
            {
                nombreEmpresa = _empresa.tbPersona.nombre.ToUpper().ToString().Trim() + " " +
                        _empresa.tbPersona.apellido1.ToUpper().ToString().Trim() + " " + _empresa.tbPersona.apellido2.ToUpper().ToString().Trim();
            }
            else
            {
                nombreEmpresa = _empresa.tbPersona.nombre.ToUpper().ToString().Trim();

            }


            if (nombreComercial != string.Empty)
            {
                Ticket1.TextoCentro(nombreComercial);
            }
            Ticket1.TextoCentro(nombreEmpresa);
            Ticket1.TextoCentro(_empresa.tbPersona.tbBarrios.tbDistrito.Nombre.Trim().ToUpper() + "-" + _empresa.tbPersona.tbBarrios.tbDistrito.tbCanton.Nombre.Trim().ToUpper() + "-" + _empresa.tbPersona.tbBarrios.tbDistrito.tbCanton.tbProvincia.Nombre.Trim().ToUpper());
            Ticket1.TextoCentro(obtenerTipoDoc(_empresa.tbPersona.tipoId) + _empresa.tbPersona.identificacion.ToString().Trim());
            Ticket1.TextoCentro("Tel:" + _empresa.tbPersona.telefono.ToString());
            Ticket1.TextoIzquierda("Factura #:" + _doc.id);
            Ticket1.TextoIzquierda("Fecha:" + _doc.fecha);
            Ticket1.TextoIzquierda("Tipo Venta:" + Enum.GetName(typeof(Enums.tipoVenta), _doc.tipoVenta));
            Ticket1.TextoIzquierda("Forma Pago:" + Enum.GetName(typeof(Enums.TipoPago), _doc.tipoPago));
            if (_doc.plazo!=0)
            {
                Ticket1.TextoIzquierda("Plazo:" + _doc.plazo);
                Ticket1.TextoIzquierda("Vence:" + _doc.fecha.AddDays((int)_doc.plazo).ToShortDateString());
            }          
            Ticket1.TextoCentro("");
            if (_doc.tipoDocumento == (int)Enums.TipoDocumento.FacturaElectronica)
            {
                Ticket1.TextoCentro("FACTURA ELECTRONICA");
            }
            else if (_doc.tipoDocumento == (int)Enums.TipoDocumento.TiqueteElectronico)
            {
                Ticket1.TextoCentro("TIQUETE ELECTRONICO");
            }

            if (_doc.tipoDocumento != (int)Enums.TipoDocumento.Proforma && _doc.tipoDocumento != (int)Enums.TipoDocumento.ProformaGeneral)
            {
                Ticket1.TextoIzquierda("Consecutivo:" + _doc.consecutivo == null ? "" : _doc.consecutivo);
                Ticket1.TextoCentro(_doc.clave == null ? "" : _doc.clave.Substring(0, 40));
                Ticket1.TextoCentro(_doc.clave == null ? "" : _doc.clave.Substring(40, 10));

            }


            if (_cliente != null)
            {
                _doc.tbClientes = _cliente;
            }


            Ticket1.TextoCentro("");
            if (_doc.idCliente != null)
            {

                string nombre = "";
                string id = _doc.tbClientes.tbPersona.identificacion.ToString().Trim();
                if (_doc.tbClientes.tbPersona.tipoId == (int)Enums.TipoId.Fisica)
                {

                    nombre = _doc.tbClientes.tbPersona.nombre.ToUpper().ToString().Trim() + " " +
                       _doc.tbClientes.tbPersona.apellido1.ToUpper().ToString().Trim() + " " + _doc.tbClientes.tbPersona.apellido2.ToUpper().ToString().Trim();
                }
                else
                {
                    nombre = _doc.tbClientes.tbPersona.nombre.ToUpper().ToString().Trim();
                }
                Ticket1.TextoIzquierda("ID Cliente:" + id);
                Ticket1.TextoIzquierda("Cliente:" + nombre);

            }
            else
            {
                if (_doc.clienteNombre != null)
                {
                    Ticket1.TextoIzquierda("Cliente:" + _doc.clienteNombre.Trim().ToUpper());
                }
            }

            Ticket1.LineasGuion(); // imprime una linea de guiones
            Ticket1.EncabezadoVenta(); // imprime encabezados
            foreach (tbDetalleDocumento item in _doc.tbDetalleDocumento)
            {

                string impuesto = item.porcImp == 13 ? "A" : item.porcImp == 8 ? "*B" : item.porcImp==4? "*C" : item.porcImp == 2 ? "D" : item.porcImp == 1 ? "E":"";

                if(_doc.tipoDocumento== (int)Enums.TipoDocumento.Proforma || _doc.tipoDocumento == (int)Enums.TipoDocumento.ProformaGeneral)
                {
                    if (_imprimePrecioProforma)
                    {
                        Ticket1.AgregaArticulo(item.tbProducto.nombre.Trim().ToUpper(), item.cantidad, item.precio, item.montoTotal, impuesto);
                    }
                    else
                    {
                        Ticket1.AgregaArticuloSinPrecio(item.tbProducto.nombre.Trim().ToUpper(), item.cantidad); //imprime una linea de descripcion
                    }               

                }
                else
                {
                    Ticket1.AgregaArticulo(item.tbProducto.nombre.Trim().ToUpper(), item.cantidad, item.precio, item.montoTotal, impuesto); //imprime una linea de descripcion

                }
            }

            Ticket1.LineasTotales(); // imprime linea 

            Ticket1.AgregaTotales("SubTotal", _doc.tbDetalleDocumento.Sum(x => x.montoTotal)); // imprime linea con total
            Ticket1.AgregaTotales("Descuento", _doc.tbDetalleDocumento.Sum(x => x.montoTotalDesc));
            decimal exo = _doc.tbDetalleDocumento.Sum(x => x.montoTotalExo);
            if (exo != 0)
            {
                Ticket1.AgregaTotales("Exoneracion", exo);
            }
            Ticket1.AgregaTotales("IVA", _doc.tbDetalleDocumento.Sum(x => x.montoTotalImp));
            Ticket1.AgregaTotales("Total", _doc.tbDetalleDocumento.Sum(x => x.totalLinea)); // imprime linea con total
            Ticket1.LineasGuion();
            Ticket1.AgregaTotales("Pago", _paga); // imprime linea con total
            Ticket1.AgregaTotales("Vuelto", _vuelto); // imprime linea con total
            Ticket1.LineasGuion();
            if (_doc.observaciones != null)
            {
                Ticket1.TextoIzquierda("Observación: "+ _doc.observaciones); // imprime linea con total
            }

            if (_doc.razon != null)
            {
                Ticket1.TextoIzquierda("Razón: " + _doc.razon); // imprime linea con total
            }
            Ticket1.LineasGuion();
            Ticket1.TextoIzquierda("Gravado: A=13%  B=8%  C=4%  D=2%  E=1%");
            Ticket1.LineasGuion();
            Ticket1.TextoIzquierda("Hecho por:" + Global.Usuario.nombreUsuario.Trim().ToUpper());
            Ticket1.TextoIzquierda("Autorizada mediante resolución No. DGT-R");
            Ticket1.TextoIzquierda("-033-2019 del 20 de junio del 2019 V4.3");

            Ticket1.TextoCentro("GRACIAS POR PREFERIRNOS");

            Ticket1.CortaTicket(); // corta el ticket

       }

        private string obtenerTipoDoc(int tipoId)
        {
            if ((int)Enums.TipoId.Fisica== tipoId)
            {
                return "Física: ";
            }
            else if ((int)Enums.TipoId.Juridica == tipoId)
            {
                return "Jurídica: ";
            }
            else if ((int)Enums.TipoId.Dimex == tipoId)
            {
                return "Dimex: ";
            }
            else
            {
                return "Nite: ";
            }

        }

        public void printCuenta()
        {

            CreaTicket Ticket1 = new CreaTicket();
            _empresa = empresaIns.getEntityForPrint(Global.Usuario.tbEmpresa);
            string nombreEmpresa = string.Empty;
            string nombreComercial = string.Empty;
            if (Global.actividadEconomic.nombreComercial != null)
            {
                nombreComercial = Global.actividadEconomic.nombreComercial.Trim().ToUpper();
            }

            if (_empresa.tipoId == (int)Enums.TipoId.Fisica)
            {
                nombreEmpresa = _empresa.tbPersona.nombre.ToUpper().ToString().Trim() + " " +
                        _empresa.tbPersona.apellido1.ToUpper().ToString().Trim() + " " + _empresa.tbPersona.apellido2.ToUpper().ToString().Trim();
            }
            else
            {
                nombreEmpresa = _empresa.tbPersona.nombre.ToUpper().ToString().Trim();

            }


            if (nombreComercial != string.Empty)
            {
                Ticket1.TextoCentro(nombreComercial);
            }
            Ticket1.TextoCentro(nombreEmpresa);
            Ticket1.TextoCentro(_empresa.tbPersona.tbBarrios.tbDistrito.Nombre.Trim().ToUpper() + "-" + _empresa.tbPersona.tbBarrios.tbDistrito.tbCanton.Nombre.Trim().ToUpper() + "-" + _empresa.tbPersona.tbBarrios.tbDistrito.tbCanton.tbProvincia.Nombre.Trim().ToUpper());
            Ticket1.TextoCentro((_empresa.tipoId == (int)Enums.TipoId.Fisica ? "Ced Fisica:" : "Ced Juridica:") + _empresa.tbPersona.identificacion.ToString().Trim());
            Ticket1.TextoCentro("Tel:" + _empresa.tbPersona.telefono.ToString());
            Ticket1.TextoIzquierda("COMPROBANTE #:" + _alias.Trim().ToUpper());
            Ticket1.TextoIzquierda("Fecha:" + Utility.getDate());
           

            foreach (tbDetalleDocumento item in _detalle)
            {
                string impuesto = item.porcImp == 13 ? "A" : item.porcImp == 8 ? "*B" : item.porcImp == 4 ? "*C" : item.porcImp == 2 ? "D" : item.porcImp == 1 ? "E" : "";
                Ticket1.AgregaArticulo(item.tbProducto.nombre.Trim().ToUpper(), item.cantidad, item.precio, item.montoTotal, impuesto); //imprime una linea de descripcion
            }
            Ticket1.LineasGuion();
            Ticket1.TextoIzquierda("Gravado: A=13%  B=8%  C=4%  D=2%  E=1%");
            Ticket1.LineasGuion();
            Ticket1.LineasTotales(); // imprime linea 

            Ticket1.AgregaTotales("SubTotal", _detalle.Sum(x => x.montoTotal)); // imprime linea con total
            Ticket1.AgregaTotales("Descuento", _detalle.Sum(x => x.montoTotalDesc));
            decimal exo = _detalle.Sum(x => x.montoTotalExo);
            if (exo != 0)
            {
                Ticket1.AgregaTotales("Exoneracion", exo);
            }
            Ticket1.AgregaTotales("IVA", _detalle.Sum(x => x.montoTotalImp));
            Ticket1.AgregaTotales("Total", _detalle.Sum(x => x.totalLinea)); // imprime linea con total
            Ticket1.LineasGuion();           
            Ticket1.TextoIzquierda("Hecho por:" + Global.Usuario.nombreUsuario.Trim().ToUpper());
           

            Ticket1.TextoCentro("GRACIAS POR PREFERIRNOS");
            Ticket1.CortaTicket(); // corta el ticket

        }



        private void printMediaCarta()
        {
            string path = @Global.Configuracion.logoRuta.Trim();

            SqlConnection _SqlConnection = new SqlConnection(Utility.stringConexionReportes());
            dsReportes ds = new dsReportes();
            if (_doc.idCliente == null)
            {
                rptFacturaESinCliente Reporte = new rptFacturaESinCliente();

                //creamos una nueva instancia del table adapter que usaremos para obtener la información de la base de datos
                Reportes.dsReportesTableAdapters.sp_FacturaElectronicaSinClienteTableAdapter dt = new Reportes.dsReportesTableAdapters.sp_FacturaElectronicaSinClienteTableAdapter();

                _SqlConnection.Open();

                //le pasamos la conexión al tableadapter
                dt.Connection = _SqlConnection;
                //llenamos el tableadapter con el método fill
                dt.Fill(ds.sp_FacturaElectronicaSinCliente, _doc.id, _doc.tipoDocumento);

                Zen.Barcode.CodeQrBarcodeDraw barcode = new Zen.Barcode.CodeQrBarcodeDraw();
                string conse = _doc.clave != null ? _doc.clave.ToString() : _doc.tipoDocumento.ToString() + _doc.id.ToString();
                Image bar = barcode.Draw(conse, 6);

                foreach (DataRow dr in ds.Tables["sp_FacturaElectronicaSinCliente"].Rows)
                {

                
                    dr["Barcode"] = Utility.ImageToByteArray(bar);
                    dr["LogoEmp"] = Utility.UrlImageToByteArray(path);
                }


                Reporte.SetDataSource(ds);

                //Reporte.SetParameterValue("path", Global.Configuracion.logoRuta.Trim());

                Reporte.PrintOptions.PrinterName = Global.Configuracion.rutaImpresoraNormal;
                Reporte.PrintToPrinter(1, false, 0, 0);



                Reporte.Close();


            }
            else
            {
                if (_doc.tipoDocumento == (int)Enums.TipoDocumento.Proforma || _doc.tipoDocumento == (int)Enums.TipoDocumento.Proforma)
                {
                    if (!_imprimePrecioProforma)
                    {
                        rptProforma ReportePro = new rptProforma();
                        //creamos una nueva instancia del DataSet
                        //creamos una nueva instancia del table adapter que usaremos para obtener la información de la base de datos
                        Reportes.dsReportesTableAdapters.sp_FacturaElectronicaTableAdapter dtP = new Reportes.dsReportesTableAdapters.sp_FacturaElectronicaTableAdapter();

                        _SqlConnection.Open();

                        //le pasamos la conexión al tableadapter
                        dtP.Connection = _SqlConnection;
                        //llenamos el tableadapter con el método fill
                        dtP.Fill(ds.sp_FacturaElectronica, _doc.id, _doc.tipoDocumento);

                        Zen.Barcode.CodeQrBarcodeDraw barcodeP = new Zen.Barcode.CodeQrBarcodeDraw();

                        foreach (DataRow dr in ds.Tables["sp_FacturaElectronica"].Rows)
                        {
                            string conse = _doc.clave != null ? _doc.clave.ToString() : _doc.tipoDocumento.ToString() + _doc.id.ToString();
                            Image bar = barcodeP.Draw(conse, 6);
                            dr["Barcode"] = Utility.ImageToByteArray(bar);
                            dr["LogoEmp"] = Utility.UrlImageToByteArray(path);
                        }


                        ReportePro.SetDataSource(ds);

                        //ReportePro.SetParameterValue("path", Global.Configuracion.logoRuta.Trim());

                        ReportePro.PrintOptions.PrinterName = Global.Configuracion.rutaImpresoraNormal;
                        ReportePro.PrintToPrinter(1, false, 0, 0);

                        ReportePro.Close();
                        return;
                    }
                }


                rptFacturaE Reporte = new rptFacturaE();
                //creamos una nueva instancia del DataSet
                //creamos una nueva instancia del table adapter que usaremos para obtener la información de la base de datos
                Reportes.dsReportesTableAdapters.sp_FacturaElectronicaTableAdapter dt = new Reportes.dsReportesTableAdapters.sp_FacturaElectronicaTableAdapter();

                _SqlConnection.Open();

                //le pasamos la conexión al tableadapter
                dt.Connection = _SqlConnection;
                //llenamos el tableadapter con el método fill
                dt.Fill(ds.sp_FacturaElectronica, _doc.id, _doc.tipoDocumento);

                Zen.Barcode.CodeQrBarcodeDraw barcode = new Zen.Barcode.CodeQrBarcodeDraw();

                foreach (DataRow dr in ds.Tables["sp_FacturaElectronica"].Rows)
                {
                    string conse = _doc.clave != null ? _doc.clave.ToString() : _doc.tipoDocumento.ToString() + _doc.id.ToString();
                    Image bar = barcode.Draw(conse, 6);
                    dr["Barcode"] = Utility.ImageToByteArray(bar);
                    dr["LogoEmp"] = Utility.UrlImageToByteArray(path);
                }

                Reporte.SetDataSource(ds);
                //Reporte.SetParameterValue("path", Global.Configuracion.logoRuta.Trim());

                Reporte.PrintOptions.PrinterName = Global.Configuracion.rutaImpresoraNormal;
                Reporte.PrintToPrinter(1, false, 0, 0);

                Reporte.Close();

            }

        }




        private void printPuntoVentaReport()
        {
            string path = @Global.Configuracion.logoRuta.Trim();

            SqlConnection _SqlConnection = new SqlConnection(Utility.stringConexionReportes());
            dsReportes ds = new dsReportes();
            if (_doc.idCliente == null)
            {
                rptTicketPuntoVentaSinCliente Reporte = new rptTicketPuntoVentaSinCliente();

                //creamos una nueva instancia del table adapter que usaremos para obtener la información de la base de datos
                Reportes.dsReportesTableAdapters.sp_FacturaElectronicaSinClienteTableAdapter dt = new Reportes.dsReportesTableAdapters.sp_FacturaElectronicaSinClienteTableAdapter();

                _SqlConnection.Open();

                //le pasamos la conexión al tableadapter
                dt.Connection = _SqlConnection;
                //llenamos el tableadapter con el método fill
                dt.Fill(ds.sp_FacturaElectronicaSinCliente, _doc.id, _doc.tipoDocumento);

                Zen.Barcode.CodeQrBarcodeDraw barcode = new Zen.Barcode.CodeQrBarcodeDraw();

                foreach (DataRow dr in ds.Tables["sp_FacturaElectronicaSinCliente"].Rows)
                {

                    string conse = _doc.clave != null ? _doc.clave.ToString() : _doc.tipoDocumento.ToString() + _doc.id.ToString();
                    Image bar = barcode.Draw(conse, 6);
                    dr["Barcode"] = Utility.ImageToByteArray(bar);
                    dr["LogoEmp"] = Utility.UrlImageToByteArray(path);
                }


                Reporte.SetDataSource(ds);
                Reporte.SetParameterValue("pago", this._paga);
                Reporte.SetParameterValue("vuelto", this._vuelto);
                Reporte.PrintOptions.PrinterName = Global.Configuracion.rutaImpresoraNormal;
                Reporte.PrintToPrinter(1, false, 0, 0);



                Reporte.Close();


            }
            else
            {
                //if (_doc.tipoDocumento == (int)Enums.TipoDocumento.Proforma || _doc.tipoDocumento == (int)Enums.TipoDocumento.Proforma)
                //{
                //    if (!_imprimePrecioProforma)
                //    {
                //        rptProforma ReportePro = new rptProforma();
                //        //creamos una nueva instancia del DataSet
                //        //creamos una nueva instancia del table adapter que usaremos para obtener la información de la base de datos
                //        Reportes.dsReportesTableAdapters.sp_FacturaElectronicaTableAdapter dtP = new Reportes.dsReportesTableAdapters.sp_FacturaElectronicaTableAdapter();

                //        _SqlConnection.Open();

                //        //le pasamos la conexión al tableadapter
                //        dtP.Connection = _SqlConnection;
                //        //llenamos el tableadapter con el método fill
                //        dtP.Fill(ds.sp_FacturaElectronica, _doc.id, _doc.tipoDocumento);

                //        Zen.Barcode.CodeQrBarcodeDraw barcodeP = new Zen.Barcode.CodeQrBarcodeDraw();

                //        foreach (DataRow dr in ds.Tables["sp_FacturaElectronica"].Rows)
                //        {
                //            string conse = _doc.clave != null ? _doc.clave.ToString() : _doc.tipoDocumento.ToString() + _doc.id.ToString();
                //            Image bar = barcodeP.Draw(conse, 6);
                //            dr["Barcode"] = Utility.ImageToByteArray(bar);
                //        }


                //        ReportePro.SetDataSource(ds);

                //        ReportePro.SetParameterValue("path", Global.Configuracion.logoRuta.Trim());

                //        ReportePro.PrintOptions.PrinterName = Global.Configuracion.rutaImpresoraNormal;
                //        ReportePro.PrintToPrinter(1, false, 0, 0);

                //        ReportePro.Close();
                //        return;
                //    }
                //}


                rptTicketCliente Reporte = new rptTicketCliente();
                //creamos una nueva instancia del DataSet
                //creamos una nueva instancia del table adapter que usaremos para obtener la información de la base de datos
                Reportes.dsReportesTableAdapters.sp_FacturaElectronicaTableAdapter dt = new Reportes.dsReportesTableAdapters.sp_FacturaElectronicaTableAdapter();

                _SqlConnection.Open();

                //le pasamos la conexión al tableadapter
                dt.Connection = _SqlConnection;
                //llenamos el tableadapter con el método fill
                dt.Fill(ds.sp_FacturaElectronica, _doc.id, _doc.tipoDocumento);

                Zen.Barcode.CodeQrBarcodeDraw barcode = new Zen.Barcode.CodeQrBarcodeDraw();

                foreach (DataRow dr in ds.Tables["sp_FacturaElectronica"].Rows)
                {
                    string conse = _doc.clave != null ? _doc.clave.ToString() : _doc.tipoDocumento.ToString() + _doc.id.ToString();
                    Image bar = barcode.Draw(conse, 6);
                    dr["Barcode"] = Utility.ImageToByteArray(bar);
                    dr["LogoEmp"] = Utility.UrlImageToByteArray(path);
                }

                Reporte.SetDataSource(ds);

                Reporte.SetParameterValue("pago", this._paga);
                Reporte.SetParameterValue("vuelto", this._vuelto);
                Reporte.PrintOptions.PrinterName = Global.Configuracion.rutaImpresoraNormal;
                Reporte.PrintToPrinter(1, false, 0, 0);

                Reporte.Close();

            }

        }



        private void printCartaCompleta()
        {
            string path = @Global.Configuracion.logoRuta.Trim();


            SqlConnection _SqlConnection = new SqlConnection(Utility.stringConexionReportes());
            dsReportes ds = new dsReportes();
            if (_doc.idCliente == null)
            {

                rptFacturaESinCliente Reporte = new rptFacturaESinCliente();

                //creamos una nueva instancia del table adapter que usaremos para obtener la información de la base de datos
                Reportes.dsReportesTableAdapters.sp_FacturaElectronicaSinClienteTableAdapter dt = new Reportes.dsReportesTableAdapters.sp_FacturaElectronicaSinClienteTableAdapter();
                
                _SqlConnection.Open();

                //le pasamos la conexión al tableadapter
                dt.Connection = _SqlConnection;
                //llenamos el tableadapter con el método fill
                dt.Fill(ds.sp_FacturaElectronicaSinCliente, _doc.id, _doc.tipoDocumento);

                Zen.Barcode.CodeQrBarcodeDraw barcode = new Zen.Barcode.CodeQrBarcodeDraw();

                foreach (DataRow dr in ds.Tables["sp_FacturaElectronicaSinCliente"].Rows)
                {

                    string conse = _doc.clave != null ? _doc.clave.ToString() : _doc.tipoDocumento.ToString() + _doc.id.ToString();
                    Image bar = barcode.Draw(conse, 6);
                    dr["Barcode"] = Utility.ImageToByteArray(bar);
                    dr["LogoEmp"] = Utility.UrlImageToByteArray(path);
                }

                Reporte.SetDataSource(ds);

               // Reporte.SetParameterValue("path", Global.Configuracion.logoRuta.Trim());

                Reporte.PrintToPrinter(1, false, 0, 0);

                Reporte.Close();


            }
            else
            {
                rptFacturaE Reporte = new rptFacturaE();
                //creamos una nueva instancia del DataSet


                //creamos una nueva instancia del table adapter que usaremos para obtener la información de la base de datos
                Reportes.dsReportesTableAdapters.sp_FacturaElectronicaTableAdapter dt = new Reportes.dsReportesTableAdapters.sp_FacturaElectronicaTableAdapter();

                _SqlConnection.Open();

                //le pasamos la conexión al tableadapter
                dt.Connection = _SqlConnection;
                //llenamos el tableadapter con el método fill
                dt.Fill(ds.sp_FacturaElectronica, _doc.id, _doc.tipoDocumento);

                Zen.Barcode.CodeQrBarcodeDraw barcode = new Zen.Barcode.CodeQrBarcodeDraw();

                foreach (DataRow dr in ds.Tables["sp_FacturaElectronica"].Rows)
                {
                    string conse = _doc.clave != null ? _doc.clave.ToString() : _doc.tipoDocumento.ToString() + _doc.id.ToString();
                    Image bar = barcode.Draw(conse, 6);
                    dr["Barcode"] = Utility.ImageToByteArray(bar);
                    dr["LogoEmp"] = Utility.UrlImageToByteArray(path);
                }


                Reporte.SetDataSource(ds);
               // Reporte.SetParameterValue("path", Global.Configuracion.logoRuta.Trim());
                Reporte.PrintToPrinter(1, false, 0, 0);




                Reporte.Close();





            }

        }

        //void imprimirFactura(object sender, PrintPageEventArgs e)
        //{


        //    //Configuramos el docmumento para imprimir.
        //    Graphics plantilla = e.Graphics;
        //    Font font = new Font("Courier New", 10);
        //    int startX = 50;
        //    int startY = 55;
        //    int Offset = 40;

        //    plantilla.DrawString(_empresa.tbPersona.nombre,
        //                        new Font("Courier New", 14),
        //                        new SolidBrush(Color.Black), startX, startY + Offset);

        //    Offset = Offset + 20;
        //    plantilla.DrawString(_empresa.tbPersona.otrasSenas,
        //             new Font("Courier New", 14),
        //             new SolidBrush(Color.Black), startX, startY + Offset);

        //    Offset = Offset + 20;
        //    plantilla.DrawString("Número factura: " + _doc.id,
        //             new Font("Courier New", 12),
        //             new SolidBrush(Color.Black), startX, startY + Offset);

        //    Offset = Offset + 20;
        //    plantilla.DrawString("Fecha: " + _doc.fecha,
        //             new Font("Courier New", 12),
        //             new SolidBrush(Color.Black), startX, startY + Offset);

        //    Offset = Offset + 20;
        //    plantilla.DrawString("Atendido por: " + _doc.usuario_crea,
        //             new Font("Courier New", 12),
        //             new SolidBrush(Color.Black), startX, startY + Offset);

        //    Offset = Offset + 40;
        //    plantilla.DrawString("Cant.      Nombre.        P/u",
        //             new Font("Courier New", 12),
        //             new SolidBrush(Color.Black), startX, startY + Offset);

        //    Offset = Offset + 20;
        //    plantilla.DrawString("---------------------------------------",
        //             new Font("Courier New", 12),
        //             new SolidBrush(Color.Black), startX, startY + Offset);

        //    //Aqui ira el ciclo para recorrer los productos comprados.
        //    foreach (tbDetalleDocumento item in _doc.tbDetalleDocumento)
        //    {

        //        Offset = Offset + 20;
        //        plantilla.DrawString(item.cantidad.ToString().Trim() ,
        //                 new Font("Courier New", 12),
        //                 new SolidBrush(Color.Black), startX, startY + Offset);

        //        //Este sera el nombre del producto, debo recorrer la lista de productos y ver cual coincide con el ID.

        //        plantilla.DrawString(item.tbProducto.nombre.Trim(),
        //            new Font("Courier New", 12),
        //            new SolidBrush(Color.Black), 100, startY + Offset);



        //        plantilla.DrawString(item.totalLinea.ToString().Trim(),
        //         new Font("Courier New", 12),
        //         new SolidBrush(Color.Black), 300, startY + Offset);

        //    }



        //    Offset = Offset + 20;
        //    plantilla.DrawString("---------------------------------------",
        //             new Font("Courier New", 12),
        //             new SolidBrush(Color.Black), startX, startY + Offset);

        //    Offset = Offset + 40;
        //    plantilla.DrawString( "Subtotal: " + 23432,
        //    new Font("Courier New", 12),
        //             new SolidBrush(Color.Black), startX, startY + Offset);


        //    Offset = Offset + 20;
        //    //plantilla.DrawString("IVA: " + facturaGlo.iva,
        //    //         new Font("Courier New", 12),
        //    //         new SolidBrush(Color.Black), startX, startY + Offset);


        //    //Offset = Offset + 20;
        //    //plantilla.DrawString("Pago con: " + facturaGlo.pago,
        //    //         new Font("Courier New", 12),
        //    //         new SolidBrush(Color.Black), startX, startY + Offset);


        //    //Offset = Offset + 20;
        //    //plantilla.DrawString("Vuelto: " + (facturaGlo.pago - facturaGlo.total).ToString(),
        //    //         new Font("Courier New", 12),
        //    //         new SolidBrush(Color.Black), startX, startY + Offset);

        //}


    }
}
