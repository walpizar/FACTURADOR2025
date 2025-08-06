using CommonLayer;
using CrystalDecisions.Shared;
using EntityLayer;
using PresentationLayer.Reportes;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;

namespace PresentationLayer.Clases
{
    public static class clsPDF
    {
        public static string generarPDFFactura(tbDocumento doc)
        {
            return generarPDFFactura(doc, false);
        }

        public static string generarPDFFactura(tbDocumento doc, bool precios)
        {
            try
            {
                string pdf = string.Empty;
                string tipoDoc = Utility.getPrefixTypeDoc(doc.tipoDocumento);
                string directorio = Global.Usuario.tbEmpresa.rutaCertificado.Trim();
                //string conec=Properties.Settings.Default.dbSISSODINAConnectionString1.ToString();
                SqlConnection _SqlConnection = new SqlConnection(Utility.stringConexionReportes());
                dsReportes ds = new dsReportes();
                if (doc.idCliente == null)
                {

                    rptFacturaESinCliente Reporte = new rptFacturaESinCliente();

                    //creamos una nueva instancia del table adapter que usaremos para obtener la información de la base de datos
                    Reportes.dsReportesTableAdapters.sp_FacturaElectronicaSinClienteTableAdapter dt = new Reportes.dsReportesTableAdapters.sp_FacturaElectronicaSinClienteTableAdapter();

                    _SqlConnection.Open();

                    //le pasamos la conexión al tableadapter
                    dt.Connection = _SqlConnection;
                    //llenamos el tableadapter con el método fill
                    dt.Fill(ds.sp_FacturaElectronicaSinCliente, doc.id, doc.tipoDocumento);

                    Zen.Barcode.CodeQrBarcodeDraw barcode = new Zen.Barcode.CodeQrBarcodeDraw();

                    foreach (DataRow dr in ds.Tables["sp_FacturaElectronicaSinCliente"].Rows)
                    {
                        
                        string conse = doc.clave != null ? doc.clave.ToString() : doc.tipoDocumento.ToString() + doc.id.ToString();
                        Image bar = barcode.Draw(conse, 6);
                        dr["Barcode"] = Utility.ImageToByteArray(bar);
                        dr["LogoEmp"] = Utility.UrlImageToByteArray(@Global.Configuracion.logoRuta.Trim());


                    }

                    Reporte.SetDataSource(ds);
                    string id = doc.consecutivo == null ? doc.id.ToString() : doc.consecutivo;


                    //Reporte.SetParameterValue("path", Global.Configuracion.logoRuta.Trim());
                    pdf = directorio.Trim() + (id.Trim() + tipoDoc + "_PDF.pdf");
                    Reporte.ExportToDisk(ExportFormatType.PortableDocFormat, pdf);
                    Reporte.Close();
                    Reporte.Dispose();



                }
                else
                {
                    if (doc.tipoDocumento == (int)Enums.TipoDocumento.ProformaGeneral || doc.tipoDocumento == (int)Enums.TipoDocumento.Proforma)
                    {

                        if (!precios)
                        {
                            rptProforma Reporte = new rptProforma();
                            //creamos una nueva instancia del DataSet


                            //creamos una nueva instancia del table adapter que usaremos para obtener la información de la base de datos
                            Reportes.dsReportesTableAdapters.sp_FacturaElectronicaTableAdapter dt = new Reportes.dsReportesTableAdapters.sp_FacturaElectronicaTableAdapter();

                            _SqlConnection.Open();

                            //le pasamos la conexión al tableadapter
                            dt.Connection = _SqlConnection;
                            //llenamos el tableadapter con el método fill
                            dt.Fill(ds.sp_FacturaElectronica, doc.id, doc.tipoDocumento);

                            Zen.Barcode.CodeQrBarcodeDraw barcode = new Zen.Barcode.CodeQrBarcodeDraw();

                            foreach (DataRow dr in ds.Tables["sp_FacturaElectronica"].Rows)
                            {
                                string conse = doc.clave != null ? doc.clave.ToString() : doc.tipoDocumento.ToString() + doc.id.ToString();
                                Image bar = barcode.Draw(conse, 6);
                                dr["LogoEmp"] = Utility.UrlImageToByteArray(@Global.Configuracion.logoRuta.Trim());
                                dr["Barcode"] = Utility.ImageToByteArray(bar);
                            }

                            Reporte.SetDataSource(ds);
                           // Reporte.SetParameterValue("LogoEmp", Global.Configuracion.logoRuta.Trim());
                            string id = doc.consecutivo == null ? doc.id.ToString() : doc.consecutivo;


                            pdf = directorio.Trim() + (id.Trim() + tipoDoc + "_PDF.pdf");
                            Reporte.ExportToDisk(ExportFormatType.PortableDocFormat, pdf);
                            Reporte.Close();
                            Reporte.Dispose();
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
                            dt.Fill(ds.sp_FacturaElectronica, doc.id, doc.tipoDocumento);

                            Zen.Barcode.CodeQrBarcodeDraw barcode = new Zen.Barcode.CodeQrBarcodeDraw();

                            foreach (DataRow dr in ds.Tables["sp_FacturaElectronica"].Rows)
                            {
                                string conse = doc.clave != null ? doc.clave.ToString() : doc.tipoDocumento.ToString() + doc.id.ToString();
                                Image bar = barcode.Draw(conse, 6);
                                dr["LogoEmp"] = Utility.UrlImageToByteArray(@Global.Configuracion.logoRuta.Trim());
                                dr["Barcode"] = Utility.ImageToByteArray(bar);
                            }

                            Reporte.SetDataSource(ds);         
                            string id = doc.consecutivo == null ? doc.id.ToString() : doc.consecutivo;


                            pdf = directorio.Trim() + (id.Trim() + tipoDoc + "_PDF.pdf");
                            Reporte.ExportToDisk(ExportFormatType.PortableDocFormat, pdf);
                            Reporte.Close();
                            Reporte.Dispose();

                        }



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
                        dt.Fill(ds.sp_FacturaElectronica, doc.id, doc.tipoDocumento);

                        Zen.Barcode.CodeQrBarcodeDraw barcode = new Zen.Barcode.CodeQrBarcodeDraw();

                        foreach (DataRow dr in ds.Tables["sp_FacturaElectronica"].Rows)
                        {
                            string conse = doc.clave != null ? doc.clave.ToString() : doc.tipoDocumento.ToString() + doc.id.ToString();
                            Image bar = barcode.Draw(conse, 6);
                            dr["LogoEmp"] = Utility.UrlImageToByteArray(@Global.Configuracion.logoRuta.Trim());
                            dr["Barcode"] = Utility.ImageToByteArray(bar);
                        }

                        Reporte.SetDataSource(ds);
                      //  Reporte.SetParameterValue("LogoEmp", Global.Configuracion.logoRuta.Trim());

                        string id = doc.consecutivo == null ? doc.id.ToString() : doc.consecutivo;


                        pdf = directorio.Trim() + (id.Trim() + tipoDoc + "_PDF.pdf");
                        Reporte.ExportToDisk(ExportFormatType.PortableDocFormat, pdf);
                        Reporte.Close();
                        Reporte.Dispose();

                    }

                }

                return pdf;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static string generarPDFOrdenCompra(tbOrdenCompra doc)
        {
            try
            {
                string pdf = string.Empty;
                string tipoDoc = Utility.getPrefixTypeDoc((int)Enums.TipoDocumento.OrdenCompra);
                string directorio = Global.Usuario.tbEmpresa.rutaCertificado.Trim();


                SqlConnection _SqlConnection = new SqlConnection(Utility.stringConexionReportes());
                dsReportes ds = new dsReportes();

                rptOrdenCompra Reporte = new rptOrdenCompra();
                //creamos una nueva instancia del DataSet


                //creamos una nueva instancia del table adapter que usaremos para obtener la información de la base de datos
                Reportes.dsReportesTableAdapters.sp_OrdenCompraTableAdapter dt = new Reportes.dsReportesTableAdapters.sp_OrdenCompraTableAdapter();

                _SqlConnection.Open();

                //le pasamos la conexión al tableadapter
                dt.Connection = _SqlConnection;
                //llenamos el tableadapter con el método fill
                dt.Fill(ds.sp_OrdenCompra, doc.id);

                string path = @Global.Configuracion.logoRuta.Trim();

                foreach (DataRow dr in ds.Tables["sp_OrdenCompra"].Rows)
                {
                    dr["LogoEmp"] = Utility.UrlImageToByteArray(path);
                }



                Reporte.SetDataSource(ds);

                string id = doc.id.ToString();

                pdf = directorio.Trim() + (id.Trim() + tipoDoc + "_PDF.pdf");
                Reporte.ExportToDisk(ExportFormatType.PortableDocFormat, pdf);
                Reporte.Close();
                Reporte.Dispose();

                return pdf;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

    }
}
