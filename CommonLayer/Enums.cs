namespace CommonLayer
{
    public static class Enums

    {
        public enum tipoAdjunto
        {
            factura = 1,
            mensaje = 2,
            ordenCompra = 3

        }
        public enum tipoEstacion
        {
            servidor = 1,
            cliente = 2

        }
        public enum pantallaFacturacion
        {
            amplia = 1,/*AMPLIA(BARES-RESTAURANTES-SODA)*/
            reducida = 2,/*REDUCIDA (FERRETERIA,MINISUPER)*/
            SuperReducida = 3,/*tactil pantalla pequeña*/
            fotos=4

        }

        public enum tipoImpresion
        {
            noImprime = 0,
            puntoVenta = 1,
            mediaCarta = 2,
            normal = 3
        }

        public enum ConsultarHacienda
        {
            Clave = 1,
            Consecutivo = 2,
            IDDocumento = 3

        }
        public enum accionGuardar
        {
            Nuevo = 1,
            Modificar = 2,
            Eliminar = 3

        }
        public enum EstadoBusqueda
        {
            Activo = 1,
            Inactivos = 2,
            Todos = 3

        }

        public enum TipoId
        {
            Fisica = 1,
            Juridica = 2,
            Dimex = 3,
            Nite = 4

        }

        public enum Sexo
        {
            Femenino = 1,
            Masculino = 2
        }
        public enum TipoCompra
        {
            Contado = 1,
            Credito = 2

        }

        public enum tipoVenta
        {
            Contado = 1,
            Credito = 2,
            Consignacion = 3,
            Apartado = 4,
            ArrendamientoOpcionCompra = 5,
            ArrendamientoFuncionFinanciera = 6,
            CobroAFavorTercero = 7,
            ServPrestadoEstadoCredito = 8,
            PagoServicioPrestadoEstado = 9,
            Otros = 99

        }

        public enum TipoPagoDevolucion
        {
            Efectivo = 1,        
            Transferencia = 4

        }
        public enum TipoPago
        {
            Efectivo = 1,
            Tarjeta = 2,
            Cheque = 3,
            Transferencia = 4,
            RecaudadoTerceros = 5,
            Otros = 99
        }

        public enum TipoDocumento
        {

            FacturaElectronica = 1,
            NotaDebitoElectronica = 2,
            NotaCreditoElectronica = 3,
            TiqueteElectronico = 4,
            ComprasSimplificada = 8,
            Proforma = 20,
            Compras = 21,
            Factura = 22,
            NotaCredito = 23,
            Todos = 0,
            ProformaGeneral = 24,
            OrdenCompra = 25,
            Gastos=26,
            NotaCreditoGasto=27

        }

        public enum TipoComanda
        {
            Facturar = 1,
            Pendientes = 2,
            FacturarPendientes=3

        }

        public enum TipoMoneda
        {

            CRC = 1,
            USD = 2


        }

        public enum TipoFacturacionElectRegimenSimplificado
        {
            Todo = 1,
            SoloFacturacionConCliente = 2,
        }

        public enum Mensajes
        {

            Aceptado = 1,
            AceptadoParcial = 2,
            Rechazado = 3
        }
        public enum EstadoOrdenCompra
        {
            EnProceso = 1,
            Enviada = 2
        }

        public enum EstadoFactura
        {
            Cancelada = 1,
            Pendiente = 2,
            Eliminada = 3
        }

        public enum EstadoCorreo
        {
            SinEnviar = 0,
            Enviado = 1
        }

        public enum tipoMovimiento
        {
            InicioCaja = 1,
            CierreCaja = 2,
            EntradaDinero = 3,
            SalidaDinero = 4,
            PagoProveedor = 5,
            PagoEmpleado = 6,
            Credito = 7,
            Abono = 8

        }
        public enum formularios
        {
            InicioCaja = 1,
            CierreCaja = 2,
            EntradaDinero = 3,
            SalidaDinero = 4,
            PagoProveedor = 5,
            PagoEmpleado = 6,
            Credito = 7,
            Abono = 8,
            reporte=9,
            facturacion=10,
            facturacionReducida=11,
            facturacionSuper=12,
            dashboard=13, 
            estadoCaja=14,
            facturacionFotos=15

        }

        public enum requerimientos
        {
            Transacion = 1,
            Mantenimiento = 2,
            Tipos = 3,
            Buscar_Cliente = 4,
            Cancelar_Factura = 5,
            Cancelar_Detalle = 6
        }

        public enum reportes
        {
            inventarioGeneral = 1,
            inventarioBajo = 2,
            inventarioSobre = 3,
            inventarioCategoria = 4,
            reporteGeneralVenta = 5,
            ventasFechaInicioFin = 6,
            notasCreditoFechaIncioFin = 7,
            estadoCuentaCliente = 8,
            morosos = 9,
            ventasAgrupadasFechaEsp = 10,
            comprasReporteHacienda = 11,
            inventarioProvedorCat = 12,
            ventasProductoFechaEsp = 13,
            comprasFechaEsp = 14,
            margenGancanciasVentas = 15,
            ventasUsuarios = 16,
            InventarioMenorCero = 17,
            costosInventario = 18,
            gananciasXVendedor = 19,
            ordenCompra = 20,
            factura = 21,
            proformaSinDetalle = 22,
            abonos = 23,
            ventasResumidaAgrupadasFechaEsp = 24,
            comprasResumen=25,
            productosGeneral=26,
            inventarioCostoCat=27,
            inventarioCostoProv=28,
            abonosFechas=29,
            estadoCuentaFechas=30, 
            abonosHoy=31,
            productoVentaEsp=32,
            ventasDetallada=33,
            ventasHoyDetallada=34,
            promocionEstado=35,
            gastos = 36,
            gastosPorProveedor=37

        }

        public enum roles
        {
            Administracion = 1,
            facturador = 2,
            facturadorSinPrivilegio=3,
            facturadorSuperMas=4

        }
        public enum TipoNegocio
        {
            Otro = 1,
            Restaurante = 2
        }
        public enum TipoRef
        {
            AnulaDocumentoReferencia = 1,
            CorrigeTextoDocumentoReferencia = 2,
            CorrigeMonto = 3,
            ReferenciaOtroDocumento = 4,
            SustituyeComprobanteProvisionalContingencia = 5,
            Otros = 99


        }
        public enum busquedaProductoCompraXML
        {
            CódigoProducto = 1,
            NombreProducto = 2,
            Ambas = 3
        }

        public enum TipoDocRef
        {
            FacturaElectronica = 1,
            NotaDebitoElectronica = 2,
            NotaCreditoElectronica = 3,
            TiqueteElectronico = 4,


        }
        public enum TipoMedida
        {
            Sp = 4,
            m = 5,
            kg = 6,
            m2 = 12,
            m3 = 13,
            L = 16,
            Unid = 17,
            g = 18

        }
        public enum EstadoConfig
        {
            No = 0,
            Si = 1

        }

     
        public enum Estado
        {
            Eliminado = 0,
            Activo = 1, 
            Todos= 2

        }

        public enum EstadoRespuestaHacienda
        {
            Aceptado = 1,
            AceptadoParcial = 2,
            Rechazado = 3

        }

        public enum ImpuestosID
        {
            tarifa0 = 1,
            tarifaReducida1 = 2,
            tarifaReducida2 = 3,
            tarifaReducida4 = 4,
            transitorio0 = 5,
            transitorio4 = 6,
            transitorio8 = 7,
            tarifaGeneral = 8


        }

        public enum ImpuestosValor
        {
            tarifa0 = 0,
            tarifaReducida1 = 2,
            tarifaReducida2 = 2,
            tarifaReducida4 = 4,
            transitorio0 = 0,
            transitorio4 = 4,
            transitorio8 = 8,
            tarifaGeneral = 13


        }

        public enum TipoReporteHacienda
        {
           Todas=1,
           DocumentosElectronicos=2,
           DocumenosNoElectronicos=3


        }





    }
}
