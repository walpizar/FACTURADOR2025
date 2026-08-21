namespace CommonLayer
{
    public static class Enums

    {

        public enum TipoCodigoRomana
        {
            Peso = 1,   // El código de barras embebe el PESO en gramos
            Precio = 2  // El código de barras embebe el PRECIO ya calculado
        }

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
        public enum TiposCabys
        {
            ProductosAgriculturaSilviculturaPesca = 0,/*AMPLIA(BARES-RESTAURANTES-SODA)*/
            MineralesElectricidadGasAgua = 1,/*REDUCIDA (FERRETERIA,MINISUPER)*/
            ProductosAlimenticiosBebidasTabacoTextilesPrendasCuero =2,/*tactil pantalla pequeña*/
            Bienestransportables = 3,
            ProductosMetálicosMaquinariaEquipo = 4,
            ConstruccionesServiciosConstrucción = 5,
            ServVentaDistribAlojamienServComidasBebidaServTranspoServElectriGasAgua = 6,
            ServFinancierosServConexosServInmobiliariosAlquilerFinan = 7,
            ServPrestadosEmpresasServiciosProduc = 8,
            SerVComunidadSocialesPersonales=9
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
            PagoDeVentaCredito = 11,
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
            sinpe=6,
            Otros = 99
        }

        public enum TipoDocumento
        {

            FacturaElectronica = 1,
            NotaDebitoElectronica = 2,
            NotaCreditoElectronica = 3,
            TiqueteElectronico = 4,
            ComprasSimplificada = 8,
            ReciboElectronicoPago = 10,
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

        public enum InstitucionExoneracion
        {
            /// <summary>
            /// Ministerio de Hacienda
            /// </summary>
            MinisterioHacienda = 1,

            /// <summary>
            /// Ministerio de Relaciones Exteriores y Culto
            /// </summary>
            MinisterioRelacionesExterioresCulto = 2,

            /// <summary>
            /// Ministerio de Agricultura y Ganadería
            /// </summary>
            MinisterioAgriculturaGanaderia = 3,

            /// <summary>
            /// Ministerio de Economía, Industria y Comercio
            /// </summary>
            MinisterioEconomiaIndustriaComercio = 4,

            /// <summary>
            /// Cruz Roja Costarricense
            /// </summary>
            CruzRojaCostarricense = 5,

            /// <summary>
            /// Benemérito Cuerpo de Bomberos de Costa Rica
            /// </summary>
            BomberosCostaRica = 6,

            /// <summary>
            /// Asociación Obras del Espíritu Santo
            /// </summary>
            ObrasEspiritoSanto = 7,

            /// <summary>
            /// Federación Cruzada Nacional de protección al Anciano (Fecrunapa)
            /// </summary>
            Fecrunapa = 8,

            /// <summary>
            /// Escuela de Agricultura de la Región Húmeda (EARTH)
            /// </summary>
            EARTH = 9,

            /// <summary>
            /// Instituto Centroamericano de Administración de Empresas (INCAE)
            /// </summary>
            INCAE = 10,

            /// <summary>
            /// Junta de Protección Social (JPS)
            /// </summary>
            JPS = 11,

            /// <summary>
            /// Autoridad Reguladora de los Servicios Públicos (ARESEP)
            /// </summary>
            ARESEP = 12,

            /// <summary>
            /// Otros
            /// </summary>
            Otros = 99
        }





    }
}
