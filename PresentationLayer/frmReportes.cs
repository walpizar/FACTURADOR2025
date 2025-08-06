
using CommonLayer;
using CommonLayer.Logs;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.ReportAppServer.DataDefModel;
using EntityLayer;
using PresentationLayer.Reportes;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace PresentationLayer
{
    public partial class frmReportes : Form
    {
      
        public DateTime fechaInicio { get; set; }
        public DateTime fechaFin { get; set; }

        public int tipoDoc { get; set; }
        public string codigoProducto { get; set; }
        public tbClientes clienteGlo { get; set; }
        public int estadoPromocion { get; set; }
        public string usuario { get; set; }
        public int porcentaje { get; set; }
        public tbDocumento doc { get; set; }


        public int reporte { get; set; }
        public int orden { get; set; }

        public frmReportes()
        {
            InitializeComponent();
        }

       
       
        private void frmReportes_Load(object sender, EventArgs e)
        {
            bool activaLogo = false;
            try
            {
                SqlConnection _SqlConnection = new SqlConnection(Utility.stringConexionReportes());

                dsReportes ds = new dsReportes();
                _SqlConnection.Open();
                object Reporte = new object();
                if (reporte == (int)Enums.reportes.inventarioGeneral)
                {
                    Reporte = new rptInventarioCategoria();
                    Reportes.dsReportesTableAdapters.sp_InventarioGeneralTableAdapter dt = new Reportes.dsReportesTableAdapters.sp_InventarioGeneralTableAdapter();
                    dt.Connection = _SqlConnection;
                    dt.Fill(ds.sp_InventarioGeneral);


                }

                else if (reporte == (int)Enums.reportes.inventarioBajo)
                {

                    Reporte = new rptIneventarioBajo();
                    Reportes.dsReportesTableAdapters.sp_InventarioBajoTableAdapter dt = new Reportes.dsReportesTableAdapters.sp_InventarioBajoTableAdapter();
                    dt.Connection = _SqlConnection;
                    dt.Fill(ds.sp_InventarioBajo);



                }
                else if (reporte == (int)Enums.reportes.inventarioSobre)
                {
                    Reporte = new rptInventarioSobre();
                    Reportes.dsReportesTableAdapters.sp_InventarioSobreTableAdapter dt = new Reportes.dsReportesTableAdapters.sp_InventarioSobreTableAdapter();
                    dt.Connection = _SqlConnection;
                    dt.Fill(ds.sp_InventarioSobre);
                }
                else if (reporte == (int)Enums.reportes.productosGeneral)
                {
                    Reporte = new rptProdcutosGeneral();
                    Reportes.dsReportesTableAdapters.sp_productosGeneralTableAdapter dt = new Reportes.dsReportesTableAdapters.sp_productosGeneralTableAdapter();
                    dt.Connection = _SqlConnection;
                    dt.Fill(ds.sp_productosGeneral);
                }
                else if (reporte == (int)Enums.reportes.inventarioCategoria)
                {
                    Reporte = new rptInventarioGeneral();
                    Reportes.dsReportesTableAdapters.sp_InventarioGeneralTableAdapter dt = new Reportes.dsReportesTableAdapters.sp_InventarioGeneralTableAdapter();
                    dt.Connection = _SqlConnection;
                    dt.Fill(ds.sp_InventarioGeneral);
                }
                else if (reporte == (int)Enums.reportes.reporteGeneralVenta)
                {
                    this.fechaInicio = Utility.getDate();
                    this.fechaFin = Utility.getDate();
                    Reporte = new rptVentaDia();
                    Reportes.dsReportesTableAdapters.spReporteVentasPorFechaEspTableAdapter dt = new Reportes.dsReportesTableAdapters.spReporteVentasPorFechaEspTableAdapter();
                    dt.Connection = _SqlConnection;
                    dt.Fill(ds.spReporteVentasPorFechaEsp, this.fechaInicio, this.fechaFin,1);

                }
                else if (reporte == (int)Enums.reportes.ventasFechaInicioFin)
                {
                    frmFechaInicoFin buscar = new frmFechaInicoFin();
                    buscar.pasarDatosEvent += datosFechas;
                    buscar.ShowDialog();
                    if (this.fechaInicio == DateTime.MinValue && this.fechaFin == DateTime.MinValue)
                    {
                        this.Close();
                    }

                    Reporte = new rptVentasGeneralesFechas();
                    Reportes.dsReportesTableAdapters.spReporteVentasPorFechaEspTableAdapter dt = new Reportes.dsReportesTableAdapters.spReporteVentasPorFechaEspTableAdapter();
                    dt.Connection = _SqlConnection;
                    dt.Fill(ds.spReporteVentasPorFechaEsp, this.fechaInicio, this.fechaFin,1);
                }
                else if (reporte == (int)Enums.reportes.ventasDetallada)
                {
                    frmFechaInicoFin buscar = new frmFechaInicoFin();
                    buscar.pasarDatosEvent += datosFechas;
                    buscar.ShowDialog();
                    if (this.fechaInicio == DateTime.MinValue && this.fechaFin == DateTime.MinValue)
                    {
                        this.Close();
                    }

                    Reporte = new rptVentasDetallasFechasEsp();
                    Reportes.dsReportesTableAdapters.spVentasDetalladaFechasEspTableAdapter dt = new Reportes.dsReportesTableAdapters.spVentasDetalladaFechasEspTableAdapter();
                    dt.Connection = _SqlConnection;
                    dt.Fill(ds.spVentasDetalladaFechasEsp, this.fechaInicio, this.fechaFin);
                }
                else if (reporte == (int)Enums.reportes.ventasHoyDetallada)
                {
                    this.fechaInicio = Utility.getDate();
                    this.fechaFin = Utility.getDate();

                    Reporte = new rptVentasDetallasFechasEsp();
                    Reportes.dsReportesTableAdapters.spVentasDetalladaFechasEspTableAdapter dt = new Reportes.dsReportesTableAdapters.spVentasDetalladaFechasEspTableAdapter();
                    dt.Connection = _SqlConnection;
                    dt.Fill(ds.spVentasDetalladaFechasEsp, this.fechaInicio, this.fechaFin);
                }
                else if (reporte == (int)Enums.reportes.ventasAgrupadasFechaEsp)
                {
                    frmFechaInicoFinTipoDoc buscar = new frmFechaInicoFinTipoDoc();
                    buscar.pasarDatosEvent += datosFechas;
                    buscar.ShowDialog();
                    if (this.fechaInicio == DateTime.MinValue && this.fechaFin == DateTime.MinValue)
                    {
                        this.Close();
                    }

                    Reporte = new rptVentasAgrupadasActividadFechasEsp();
                    Reportes.dsReportesTableAdapters.spReporteVentasPorFechaEspTableAdapter dt = new Reportes.dsReportesTableAdapters.spReporteVentasPorFechaEspTableAdapter();
                    dt.Connection = _SqlConnection;
                    dt.Fill(ds.spReporteVentasPorFechaEsp, this.fechaInicio, this.fechaFin, this.tipoDoc);
                }
                else if (reporte == (int)Enums.reportes.ventasResumidaAgrupadasFechaEsp)
                {
                    frmFechaInicoFinTipoDoc buscar = new frmFechaInicoFinTipoDoc();
                    buscar.pasarDatosEvent += datosFechas;
                    buscar.ShowDialog();
                    if (this.fechaInicio == DateTime.MinValue && this.fechaFin == DateTime.MinValue)
                    {
                        this.Close();
                    }

                    Reporte = new rptVentasAgrupadasActividadFechasEspResumen();
                    Reportes.dsReportesTableAdapters.spReporteVentasPorFechaEspTableAdapter dt = new Reportes.dsReportesTableAdapters.spReporteVentasPorFechaEspTableAdapter();
                    dt.Connection = _SqlConnection;
                    dt.Fill(ds.spReporteVentasPorFechaEsp, this.fechaInicio, this.fechaFin, this.tipoDoc);
                }
                else if (reporte == (int)Enums.reportes.comprasReporteHacienda)
                {
                    frmFechaInicoFin buscar = new frmFechaInicoFin();
                    buscar.pasarDatosEvent += datosFechas;
                    buscar.ShowDialog();
                    if (this.fechaInicio == DateTime.MinValue && this.fechaFin == DateTime.MinValue)
                    {
                        this.Close();
                    }

                    Reporte = new rpComprasHacienda();
                    Reportes.dsReportesTableAdapters.spReportecComprasFechasEspTableAdapter dt = new Reportes.dsReportesTableAdapters.spReportecComprasFechasEspTableAdapter();
                    dt.Connection = _SqlConnection;
                    dt.Fill(ds.spReportecComprasFechasEsp, this.fechaInicio, this.fechaFin);
                }
                else if (reporte == (int)Enums.reportes.notasCreditoFechaIncioFin)
                {
                    frmFechaInicoFin buscar = new frmFechaInicoFin();
                    buscar.pasarDatosEvent += datosFechas;
                    buscar.ShowDialog();
                    if (this.fechaInicio == DateTime.MinValue && this.fechaFin == DateTime.MinValue)
                    {
                        this.Close();
                    }

                    Reporte = new rptNotasCreditoFechaIncioFin();
                    Reportes.dsReportesTableAdapters.sp_NotasCreditoPorFechaEspTableAdapter dt = new Reportes.dsReportesTableAdapters.sp_NotasCreditoPorFechaEspTableAdapter();
                    dt.Connection = _SqlConnection;
                    dt.Fill(ds.sp_NotasCreditoPorFechaEsp, this.fechaInicio, this.fechaFin);
                }
                else if (reporte == (int)Enums.reportes.estadoCuentaCliente)
                {
                    frmClienteReporte buscar = new frmClienteReporte();
                    buscar.pasarDatosEvent += datoscliente;
                    buscar.ShowDialog();
                    if (clienteGlo == null)
                    {
                        this.Close();
                        return;
                    }

                    Reporte = new rptEstadoCuentaCliente();
                    Reportes.dsReportesTableAdapters.sp_EstadoCuentaClienteTableAdapter dt = new Reportes.dsReportesTableAdapters.sp_EstadoCuentaClienteTableAdapter();
                    dt.Connection = _SqlConnection;
                    dt.Fill(ds.sp_EstadoCuentaCliente, (int)clienteGlo.tipoId, clienteGlo.id.Trim().ToString());
                }
                else if (reporte == (int)Enums.reportes.estadoCuentaFechas)
                {
                    frmFechaInicoFin buscar = new frmFechaInicoFin();
                    buscar.pasarDatosEvent += datosFechas;
                    buscar.ShowDialog();
                    if (this.fechaInicio == DateTime.MinValue && this.fechaFin == DateTime.MinValue)
                    {
                        this.Close();
                    }

                    Reporte = new rptEstadoCuentaFechas();
                    Reportes.dsReportesTableAdapters.sp_EstadoCuentaFechasTableAdapter dt = new Reportes.dsReportesTableAdapters.sp_EstadoCuentaFechasTableAdapter();
                    dt.Connection = _SqlConnection;
                    dt.Fill(ds.sp_EstadoCuentaFechas, this.fechaInicio, this.fechaFin);
                }

                else if (reporte == (int)Enums.reportes.abonos)
                {
                    frmClienteReporte buscar = new frmClienteReporte();
                    buscar.pasarDatosEvent += datoscliente;
                    buscar.ShowDialog();
                    if (clienteGlo == null)
                    {
                        this.Close();
                        return;
                    }

                    Reporte = new rptAbonosCliente();
                    Reportes.dsReportesTableAdapters.sp_AbonosClienteTableAdapter dt = new Reportes.dsReportesTableAdapters.sp_AbonosClienteTableAdapter();
                    dt.Connection = _SqlConnection;
                    dt.Fill(ds.sp_AbonosCliente, (int)clienteGlo.tipoId, clienteGlo.id.Trim().ToString());
                }
                else if (reporte == (int)Enums.reportes.morosos)
                {

                    Reporte = new rptMoridadClientes();
                    Reportes.dsReportesTableAdapters.sp_MorosidadClientesTableAdapter dt = new Reportes.dsReportesTableAdapters.sp_MorosidadClientesTableAdapter();
                    dt.Connection = _SqlConnection;
                    dt.Fill(ds.sp_MorosidadClientes);
                }
                else if (reporte == (int)Enums.reportes.inventarioCostoCat)
                {

                    Reporte = new rptInventarioCostoCategoria();
                    Reportes.dsReportesTableAdapters.sp_inventarioActualCostoTableAdapter dt = new Reportes.dsReportesTableAdapters.sp_inventarioActualCostoTableAdapter();
                    dt.Connection = _SqlConnection;
                    dt.Fill(ds.sp_inventarioActualCosto);
                }
                else if (reporte == (int)Enums.reportes.inventarioCostoProv)
                {

                    Reporte = new rptInventarioCostoProv();
                    Reportes.dsReportesTableAdapters.sp_inventarioActualCostoTableAdapter dt = new Reportes.dsReportesTableAdapters.sp_inventarioActualCostoTableAdapter();
                    dt.Connection = _SqlConnection;
                    dt.Fill(ds.sp_inventarioActualCosto);
                }
                else if (reporte == (int)Enums.reportes.inventarioProvedorCat)
                {

                    Reporte = new rptInventarioProveedorCate();
                    Reportes.dsReportesTableAdapters.sp_InventarioProveedorTableAdapter dt = new Reportes.dsReportesTableAdapters.sp_InventarioProveedorTableAdapter();
                    dt.Connection = _SqlConnection;
                    dt.Fill(ds.sp_InventarioProveedor);
                }
                else if (reporte == (int)Enums.reportes.ventasProductoFechaEsp)
                {
                    frmFechaInicoFin buscar = new frmFechaInicoFin();
                    buscar.pasarDatosEvent += datosFechas;
                    buscar.ShowDialog();
                    if (this.fechaInicio == DateTime.MinValue && this.fechaFin == DateTime.MinValue)
                    {
                        this.Close();
                    }

                    Reporte = new rptVentasProductosFechasE();

                    Reportes.dsReportesTableAdapters.spVentasProduco2FechasTableAdapter dt = new Reportes.dsReportesTableAdapters.spVentasProduco2FechasTableAdapter();
                    dt.Connection = _SqlConnection;
                    dt.Fill(ds.spVentasProduco2Fechas, this.fechaInicio, this.fechaFin);
                }
                else if (reporte == (int)Enums.reportes.comprasFechaEsp)
                {
                    frmFechaInicoFin buscar = new frmFechaInicoFin();
                    buscar.pasarDatosEvent += datosFechas;
                    buscar.ShowDialog();
                    if (this.fechaInicio == DateTime.MinValue && this.fechaFin == DateTime.MinValue)
                    {
                        this.Close();
                    }

                    Reporte = new rptComprasAgrupadasEmpresaFechaEsp1();
                    Reportes.dsReportesTableAdapters.spReporteComprasPorFechaEspTableAdapter dt = new Reportes.dsReportesTableAdapters.spReporteComprasPorFechaEspTableAdapter();
                    dt.Connection = _SqlConnection;
                    dt.Fill(ds.spReporteComprasPorFechaEsp, this.fechaInicio, this.fechaFin, (int)Enums.TipoDocumento.Compras);
                }
                else if (reporte == (int)Enums.reportes.comprasResumen)
                {
                    frmFechaInicoFin buscar = new frmFechaInicoFin();
                    buscar.pasarDatosEvent += datosFechas;
                    buscar.ShowDialog();
                    if (this.fechaInicio == DateTime.MinValue && this.fechaFin == DateTime.MinValue)
                    {
                        this.Close();
                    }

                    Reporte = new rptComprasAgrupadasEmpresaFechaEspResumen();
                    Reportes.dsReportesTableAdapters.spReporteComprasPorFechaEspTableAdapter dt = new Reportes.dsReportesTableAdapters.spReporteComprasPorFechaEspTableAdapter();
                    dt.Connection = _SqlConnection;
                    dt.Fill(ds.spReporteComprasPorFechaEsp, this.fechaInicio, this.fechaFin, (int)Enums.TipoDocumento.Compras);
                }
                else if (reporte == (int)Enums.reportes.gastos)
                {
                    frmFechaInicoFin buscar = new frmFechaInicoFin();
                    buscar.pasarDatosEvent += datosFechas;
                    buscar.ShowDialog();
                    if (this.fechaInicio == DateTime.MinValue && this.fechaFin == DateTime.MinValue)
                    {
                        this.Close();
                    }

                    Reporte = new rptComprasAgrupadasEmpresaFechaEspResumen();
                    Reportes.dsReportesTableAdapters.spReporteComprasPorFechaEspTableAdapter dt = new Reportes.dsReportesTableAdapters.spReporteComprasPorFechaEspTableAdapter();
                    dt.Connection = _SqlConnection;
                    dt.Fill(ds.spReporteComprasPorFechaEsp, this.fechaInicio, this.fechaFin, (int)Enums.TipoDocumento.Gastos);
                }
                else if (reporte == (int)Enums.reportes.gastosPorProveedor)
                {
                    frmFechaInicoFin buscar = new frmFechaInicoFin();
                    buscar.pasarDatosEvent += datosFechas;
                    buscar.ShowDialog();
                    if (this.fechaInicio == DateTime.MinValue && this.fechaFin == DateTime.MinValue)
                    {
                        this.Close();
                    }

                    Reporte = new rptComprasAgrupadasEmpresaFechaEsp1();
                    Reportes.dsReportesTableAdapters.spReporteComprasPorFechaEspTableAdapter dt = new Reportes.dsReportesTableAdapters.spReporteComprasPorFechaEspTableAdapter();
                    dt.Connection = _SqlConnection;
                    dt.Fill(ds.spReporteComprasPorFechaEsp, this.fechaInicio, this.fechaFin, (int)Enums.TipoDocumento.Gastos);
                }
                else if (reporte == (int)Enums.reportes.margenGancanciasVentas)
                {
                    frmFechaInicoFin buscar = new frmFechaInicoFin();
                    buscar.pasarDatosEvent += datosFechas;
                    buscar.ShowDialog();
                    if (this.fechaInicio == DateTime.MinValue && this.fechaFin == DateTime.MinValue)
                    {
                        this.Close();
                    }

                    Reporte = new rptGananciasVentasFechasEsp();
                    Reportes.dsReportesTableAdapters.sp_GananciasVentasFechasEspTableAdapter dt = new Reportes.dsReportesTableAdapters.sp_GananciasVentasFechasEspTableAdapter();
                    dt.Connection = _SqlConnection;
                    dt.Fill(ds.sp_GananciasVentasFechasEsp, this.fechaInicio, this.fechaFin);
                }
                else if (reporte == (int)Enums.reportes.gananciasXVendedor)
                {
                    frmFechaInicoFin buscar = new frmFechaInicoFin();
                    buscar.pasarDatosEvent += datosFechas;
                    buscar.ShowDialog();
                    if (this.fechaInicio == DateTime.MinValue && this.fechaFin == DateTime.MinValue)
                    {
                        this.Close();
                    }

                    Reporte = new rptMargenesGananciasVentasXVendedor();
                    Reportes.dsReportesTableAdapters.sp_GananciasVentasFechasEspTableAdapter dt = new Reportes.dsReportesTableAdapters.sp_GananciasVentasFechasEspTableAdapter();
                    dt.Connection = _SqlConnection;
                    dt.Fill(ds.sp_GananciasVentasFechasEsp, this.fechaInicio, this.fechaFin);
                }
                else if (reporte == (int)Enums.reportes.ventasUsuarios)
                {
                    frmFechasUsuarioPorc buscar = new frmFechasUsuarioPorc();
                    buscar.pasarDatosEvent += datosBusqueda;
                    buscar.ShowDialog();
                    if (this.fechaInicio == DateTime.MinValue && this.fechaFin == DateTime.MinValue)
                    {
                        this.Close();
                    }

                    Reporte = new rptVentasUsuarios();
                    Reportes.dsReportesTableAdapters.spVentasUsuarioTableAdapter dt = new Reportes.dsReportesTableAdapters.spVentasUsuarioTableAdapter();
                    dt.Connection = _SqlConnection;
                    dt.Fill(ds.spVentasUsuario, this.fechaInicio, this.fechaFin, this.usuario.Trim(),this.porcentaje);
                }
                else if (reporte == (int)Enums.reportes.InventarioMenorCero)
                {
                    Reporte = new rptIneventarioMenorCero();
                    Reportes.dsReportesTableAdapters.sp_InventarioMenorCeroTableAdapter dt = new Reportes.dsReportesTableAdapters.sp_InventarioMenorCeroTableAdapter();
                    dt.Connection = _SqlConnection;
                    dt.Fill(ds.sp_InventarioMenorCero);


                }
                else if (reporte == (int)Enums.reportes.costosInventario)
                {
                    Reporte = new rptInventarioCosto();
                    Reportes.dsReportesTableAdapters.sp_inventarioActualCostoTableAdapter dt = new Reportes.dsReportesTableAdapters.sp_inventarioActualCostoTableAdapter();
                    dt.Connection = _SqlConnection;
                    dt.Fill(ds.sp_inventarioActualCosto);
                }
                else if (reporte == (int)Enums.reportes.ordenCompra)
                {
                    Reporte = new rptOrdenCompra();
                     Reportes.dsReportesTableAdapters.sp_OrdenCompraTableAdapter dt = new Reportes.dsReportesTableAdapters.sp_OrdenCompraTableAdapter();
                    dt.Connection = _SqlConnection;
                    dt.Fill(ds.sp_OrdenCompra, orden);

                    string path = @Global.Configuracion.logoRuta.Trim();

                    foreach (DataRow dr in ds.Tables["sp_OrdenCompra"].Rows)
                    {          
                        dr["LogoEmp"] = Utility.UrlImageToByteArray(path);
                    }

                }
                else if (reporte == (int)Enums.reportes.factura)
                {
                    activaLogo = true;
                    if (doc.idCliente != null)
                    {
                        Reporte = new rptFacturaE();
                        Reportes.dsReportesTableAdapters.sp_FacturaElectronicaTableAdapter dt = new Reportes.dsReportesTableAdapters.sp_FacturaElectronicaTableAdapter();
                        dt.Connection = _SqlConnection;
                        dt.Fill(ds.sp_FacturaElectronica, doc.id, doc.tipoDocumento);
                    }
                    else
                    {
                        Reporte = new rptFacturaESinCliente1();
                        Reportes.dsReportesTableAdapters.sp_FacturaElectronicaSinClienteTableAdapter dt = new Reportes.dsReportesTableAdapters.sp_FacturaElectronicaSinClienteTableAdapter();
                        dt.Connection = _SqlConnection;
                        dt.Fill(ds.sp_FacturaElectronicaSinCliente, doc.id, doc.tipoDocumento);
                    }
                }
                else if (reporte == (int)Enums.reportes.proformaSinDetalle)
                {
                    activaLogo = true;
                    Reporte = new rptProforma();
                    Reportes.dsReportesTableAdapters.sp_FacturaElectronicaTableAdapter dt = new Reportes.dsReportesTableAdapters.sp_FacturaElectronicaTableAdapter();
                    dt.Connection = _SqlConnection;
                    dt.Fill(ds.sp_FacturaElectronica, doc.id, doc.tipoDocumento);


                }
                else if (reporte == (int)Enums.reportes.abonosHoy)
                {
                    this.fechaInicio = Utility.getDate();
                    this.fechaFin = Utility.getDate();
                    Reporte = new rptAbonosFechaEsp();
                    Reportes.dsReportesTableAdapters.spAbonosFechasEspTableAdapter dt = new Reportes.dsReportesTableAdapters.spAbonosFechasEspTableAdapter();
                    dt.Connection = _SqlConnection;
                    dt.Fill(ds.spAbonosFechasEsp, this.fechaInicio, this.fechaFin);


                }
                else if (reporte == (int)Enums.reportes.abonosFechas)
                {
                    frmFechaInicoFin buscar = new frmFechaInicoFin();
                    buscar.pasarDatosEvent += datosFechas;
                    buscar.ShowDialog();
                    if (this.fechaInicio == DateTime.MinValue && this.fechaFin == DateTime.MinValue)
                    {
                        this.Close();
                    }

                    Reporte = new rptAbonosFechaEsp();
                    Reportes.dsReportesTableAdapters.spAbonosFechasEspTableAdapter dt = new Reportes.dsReportesTableAdapters.spAbonosFechasEspTableAdapter();
                    dt.Connection = _SqlConnection;
                    dt.Fill(ds.spAbonosFechasEsp, this.fechaInicio, this.fechaFin);


                }
                else if (reporte == (int)Enums.reportes.promocionEstado)
                {
                    frmEstadoPromocion buscar = new frmEstadoPromocion();
                    buscar.pasarDatosEvent += pasarEstado;
                    buscar.ShowDialog();
                    if (this.estadoPromocion == int.MinValue )
                    {
                        this.Close();
                    }

                    Reporte = new rptPromocionesEstado();
                    Reportes.dsReportesTableAdapters.sp_promocionesPorEstadoTableAdapter dt = new Reportes.dsReportesTableAdapters.sp_promocionesPorEstadoTableAdapter();
                    dt.Connection = _SqlConnection;
                    dt.Fill(ds.sp_promocionesPorEstado, this.estadoPromocion);


                }
                else if (reporte == (int)Enums.reportes.estadoCuentaFechas)
                {
                    frmFechaInicoFin buscar = new frmFechaInicoFin();
                    buscar.pasarDatosEvent += datosFechas;
                    buscar.ShowDialog();
                    if (this.fechaInicio == DateTime.MinValue && this.fechaFin == DateTime.MinValue)
                    {
                        this.Close();
                    }

                    Reporte = new rptAbonosFechaEsp();
                    Reportes.dsReportesTableAdapters.spAbonosFechasEspTableAdapter dt = new Reportes.dsReportesTableAdapters.spAbonosFechasEspTableAdapter();
                    dt.Connection = _SqlConnection;
                    dt.Fill(ds.spAbonosFechasEsp, this.fechaInicio, this.fechaFin);


                }
                else if (reporte == (int)Enums.reportes.productoVentaEsp)
                {
                    frmFechasInicioFinProducto buscar = new frmFechasInicioFinProducto();
                    buscar.pasarDatosEvent += datosFechasProducto;
                    buscar.ShowDialog();
                    if (this.fechaInicio == DateTime.MinValue && this.fechaFin == DateTime.MinValue && this.codigoProducto is null && this.codigoProducto== string.Empty)
                    {
                        this.Close();
                    }

                    Reporte = new rptProductosVentasEsp ();
                    Reportes.dsReportesTableAdapters.sp_ServiciosFechasTableAdapter dt = new Reportes.dsReportesTableAdapters.sp_ServiciosFechasTableAdapter();
                    dt.Connection = _SqlConnection;
                    dt.Fill(ds.sp_ServiciosFechas, this.fechaInicio, this.fechaFin,codigoProducto);
                }
                ((ReportDocument)Reporte).SetDataSource(ds);
               
                
               

                if (activaLogo)
                {
                    string path=  @Global.Configuracion.logoRuta.Trim();
                    string procedure = doc.idCliente == null ? "sp_FacturaElectronicaSinCliente" : "sp_FacturaElectronica";
                    Zen.Barcode.CodeQrBarcodeDraw barcode = new Zen.Barcode.CodeQrBarcodeDraw();
                    string conse = doc.clave != null ? doc.clave.ToString() : doc.tipoDocumento.ToString() + doc.id.ToString();
                    Image bar = barcode.Draw(conse, 6);           


                    foreach (DataRow dr in ds.Tables[procedure].Rows)
                    {

                        dr["LogoEmp"] = Utility.UrlImageToByteArray(path);
                        dr["Barcode"] = Utility.ImageToByteArray(bar);

                    }

                    // ((ReportDocument)Reporte).SetParameterValue("path", path);

                } 
                crvReporte.ReportSource = Reporte;
                crvReporte.Refresh();
            }
            catch (Exception ex)
            {
            

                MessageBox.Show("Error al cargar el reporte.", "Error reporte", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }



        }

        private void datosBusqueda(DateTime fechaInicio, DateTime fechaFin, int porcentaje, string usuarios)
        {
            this.fechaFin = fechaFin;
            this.fechaInicio = fechaInicio;
            this.usuario = usuarios;
            this.porcentaje = porcentaje;

        }

        private void pasarEstado(int estado)
        {
            this.estadoPromocion = estado;
        }

        private void datosFechasProducto(DateTime fechaInicio, DateTime fechaFin, string codigoProducto)
        {
            this.fechaFin = fechaFin;
            this.fechaInicio = fechaInicio;
            this.codigoProducto = codigoProducto;
        }

        private void datoscliente(tbClientes entity)
        {

            clienteGlo = entity;

        }

        private void datosFechas(DateTime fechaInicio, DateTime fechaFin)
        {
            this.fechaFin = fechaFin;
            this.fechaInicio = fechaInicio;
        }

        private void datosFechas(DateTime fechaInicio, DateTime fechaFin, int tipo)
        {
            this.fechaFin = fechaFin;
            this.fechaInicio = fechaInicio;
            this.tipoDoc = tipo;
        }


        private void btnsalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
