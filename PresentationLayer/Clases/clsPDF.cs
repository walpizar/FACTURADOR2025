using CommonLayer;
using CommonLayer.Logs;
using CrystalDecisions.Shared;
using EntityLayer;
using PresentationLayer.Reportes;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;

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
                if (doc == null) throw new ArgumentNullException("doc");

                string tipoDoc = Utility.getPrefixTypeDoc(doc.tipoDocumento);
                string directorio = (Global.Usuario.tbEmpresa.rutaCertificado ?? string.Empty).Trim();
                if (string.IsNullOrWhiteSpace(directorio))
                    throw new DirectoryNotFoundException("No hay rutaCertificado configurada.");

                if (!Directory.Exists(directorio))
                    Directory.CreateDirectory(directorio);

                string id = string.IsNullOrWhiteSpace(doc.consecutivo) ? doc.id.ToString() : doc.consecutivo.Trim();

                // Ruta final del PDF
                string nombrePdf = id + tipoDoc + "_PDF.pdf";
                string pdf = Path.Combine(directorio, nombrePdf);

                // Si ya existe, intenta eliminarlo (para evitar ExportToDisk sobre un archivo bloqueado)
                BorrarArchivoConReintento(pdf, 6, 400);

                var ds = new dsReportes();

                using (var cn = new SqlConnection(Utility.stringConexionReportes()))
                {
                    cn.Open();

                    // Caso SIN cliente
                    if (doc.idCliente == null)
                    {
                        using (var reporte = new rptFacturaESinCliente())
                        using (var dt = new Reportes.dsReportesTableAdapters.sp_FacturaElectronicaSinClienteTableAdapter())
                        {
                            dt.Connection = cn;
                            dt.Fill(ds.sp_FacturaElectronicaSinCliente, doc.id, doc.tipoDocumento);

                            InjectarLogoYQr(ds.Tables["sp_FacturaElectronicaSinCliente"], doc);

                            reporte.SetDataSource(ds);
                            reporte.ExportToDisk(ExportFormatType.PortableDocFormat, pdf);

                            // Close/Dispose extra (Crystal)
                            reporte.Close();
                        }
                    }
                    else
                    {
                        bool esProforma = (doc.tipoDocumento == (int)Enums.TipoDocumento.ProformaGeneral ||
                                          doc.tipoDocumento == (int)Enums.TipoDocumento.Proforma);

                        // Proforma sin precios
                        if (esProforma && !precios)
                        {
                            using (var reporte = new rptProforma())
                            using (var dt = new Reportes.dsReportesTableAdapters.sp_FacturaElectronicaTableAdapter())
                            {
                                dt.Connection = cn;
                                dt.Fill(ds.sp_FacturaElectronica, doc.id, doc.tipoDocumento);

                                InjectarLogoYQr(ds.Tables["sp_FacturaElectronica"], doc);

                                reporte.SetDataSource(ds);
                                reporte.ExportToDisk(ExportFormatType.PortableDocFormat, pdf);
                                reporte.Close();
                            }
                        }
                        else
                        {
                            // Factura normal / proforma con precios / otros docs usando rptFacturaE
                            using (var reporte = new rptFacturaE())
                            using (var dt = new Reportes.dsReportesTableAdapters.sp_FacturaElectronicaTableAdapter())
                            {
                                dt.Connection = cn;
                                dt.Fill(ds.sp_FacturaElectronica, doc.id, doc.tipoDocumento);

                                InjectarLogoYQr(ds.Tables["sp_FacturaElectronica"], doc);

                                reporte.SetDataSource(ds);
                                reporte.ExportToDisk(ExportFormatType.PortableDocFormat, pdf);
                                reporte.Close();
                            }
                        }
                    }
                }

                // Workaround típico para liberar handles Crystal rápidamente
                ForzarLiberacionCrystal();

                // Validación rápida: esperar a que el archivo esté listo (opcional)
                EsperarArchivoListo(pdf, 4000);

                return pdf;
            }
            catch (Exception ex)
            {
                //trasa en el visor de suceso
                //visor de suceso windows: Aplicación > Errores

                clsEvento evento = new clsEvento(ex.Message, "1");


                throw; // mantiene stack trace
            }
        }

        private static void InjectarLogoYQr(DataTable table, tbDocumento doc)
        {
            Zen.Barcode.CodeQrBarcodeDraw barcode = new Zen.Barcode.CodeQrBarcodeDraw();

            foreach (DataRow dr in table.Rows)
            {
                string conse = doc.clave != null ? doc.clave.ToString() : doc.tipoDocumento.ToString() + doc.id.ToString();
                Image bar = barcode.Draw(conse, 6);

                dr["Barcode"] = Utility.ImageToByteArray(bar);
                dr["LogoEmp"] = Utility.UrlImageToByteArray((Global.Configuracion.logoRuta ?? string.Empty).Trim());

                // Liberar imagen barcode (evita handles GDI)
                bar.Dispose();
            }
        }

        private static void BorrarArchivoConReintento(string ruta, int intentos, int esperaMs)
        {
            if (!File.Exists(ruta)) return;

            for (int i = 0; i < intentos; i++)
            {
                try
                {
                    File.Delete(ruta);
                    return;
                }
                catch (IOException)
                {
                    System.Threading.Thread.Sleep(esperaMs);
                }
                catch (UnauthorizedAccessException)
                {
                    System.Threading.Thread.Sleep(esperaMs);
                }
            }
            // Si no se puede borrar, se deja que falle el ExportToDisk con el mensaje real
        }

        private static void ForzarLiberacionCrystal()
        {
            // Crystal a veces retiene handles hasta que el GC recolecta.
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
        }

        private static void EsperarArchivoListo(string ruta, int msMax)
        {
            int transcurrido = 0;
            while (transcurrido < msMax)
            {
                if (File.Exists(ruta) && !EstaBloqueado(ruta))
                    return;

                System.Threading.Thread.Sleep(300);
                transcurrido += 300;
            }
        }

        private static bool EstaBloqueado(string ruta)
        {
            FileStream fs = null;
            try
            {
                fs = new FileStream(ruta, FileMode.Open, FileAccess.ReadWrite, FileShare.None);
                return false;
            }
            catch (IOException)
            {
                return true;
            }
            finally
            {
                if (fs != null) fs.Dispose();
            }
        }

        //public static string generarPDFFactura(tbDocumento doc, bool precios)
        //{
        //    try
        //    {
        //        string pdf = string.Empty;
        //        string tipoDoc = Utility.getPrefixTypeDoc(doc.tipoDocumento);
        //        string directorio = Global.Usuario.tbEmpresa.rutaCertificado.Trim();
        //        //string conec=Properties.Settings.Default.dbSISSODINAConnectionString1.ToString();
        //        SqlConnection _SqlConnection = new SqlConnection(Utility.stringConexionReportes());
        //        dsReportes ds = new dsReportes();
        //        if (doc.idCliente == null)
        //        {

        //            rptFacturaESinCliente Reporte = new rptFacturaESinCliente();

        //            //creamos una nueva instancia del table adapter que usaremos para obtener la información de la base de datos
        //            Reportes.dsReportesTableAdapters.sp_FacturaElectronicaSinClienteTableAdapter dt = new Reportes.dsReportesTableAdapters.sp_FacturaElectronicaSinClienteTableAdapter();

        //            _SqlConnection.Open();

        //            //le pasamos la conexión al tableadapter
        //            dt.Connection = _SqlConnection;
        //            //llenamos el tableadapter con el método fill
        //            dt.Fill(ds.sp_FacturaElectronicaSinCliente, doc.id, doc.tipoDocumento);

        //            Zen.Barcode.CodeQrBarcodeDraw barcode = new Zen.Barcode.CodeQrBarcodeDraw();

        //            foreach (DataRow dr in ds.Tables["sp_FacturaElectronicaSinCliente"].Rows)
        //            {

        //                string conse = doc.clave != null ? doc.clave.ToString() : doc.tipoDocumento.ToString() + doc.id.ToString();
        //                Image bar = barcode.Draw(conse, 6);
        //                dr["Barcode"] = Utility.ImageToByteArray(bar);
        //                dr["LogoEmp"] = Utility.UrlImageToByteArray(@Global.Configuracion.logoRuta.Trim());


        //            }

        //            Reporte.SetDataSource(ds);
        //            string id = doc.consecutivo == null ? doc.id.ToString() : doc.consecutivo;


        //            //Reporte.SetParameterValue("path", Global.Configuracion.logoRuta.Trim());
        //            pdf = directorio.Trim() + (id.Trim() + tipoDoc + "_PDF.pdf");
        //            Reporte.ExportToDisk(ExportFormatType.PortableDocFormat, pdf);
        //            Reporte.Close();
        //            Reporte.Dispose();



        //        }
        //        else
        //        {
        //            if (doc.tipoDocumento == (int)Enums.TipoDocumento.ProformaGeneral || doc.tipoDocumento == (int)Enums.TipoDocumento.Proforma)
        //            {

        //                if (!precios)
        //                {
        //                    rptProforma Reporte = new rptProforma();
        //                    //creamos una nueva instancia del DataSet


        //                    //creamos una nueva instancia del table adapter que usaremos para obtener la información de la base de datos
        //                    Reportes.dsReportesTableAdapters.sp_FacturaElectronicaTableAdapter dt = new Reportes.dsReportesTableAdapters.sp_FacturaElectronicaTableAdapter();

        //                    _SqlConnection.Open();

        //                    //le pasamos la conexión al tableadapter
        //                    dt.Connection = _SqlConnection;
        //                    //llenamos el tableadapter con el método fill
        //                    dt.Fill(ds.sp_FacturaElectronica, doc.id, doc.tipoDocumento);

        //                    Zen.Barcode.CodeQrBarcodeDraw barcode = new Zen.Barcode.CodeQrBarcodeDraw();

        //                    foreach (DataRow dr in ds.Tables["sp_FacturaElectronica"].Rows)
        //                    {
        //                        string conse = doc.clave != null ? doc.clave.ToString() : doc.tipoDocumento.ToString() + doc.id.ToString();
        //                        Image bar = barcode.Draw(conse, 6);
        //                        dr["LogoEmp"] = Utility.UrlImageToByteArray(@Global.Configuracion.logoRuta.Trim());
        //                        dr["Barcode"] = Utility.ImageToByteArray(bar);
        //                    }

        //                    Reporte.SetDataSource(ds);
        //                   // Reporte.SetParameterValue("LogoEmp", Global.Configuracion.logoRuta.Trim());
        //                    string id = doc.consecutivo == null ? doc.id.ToString() : doc.consecutivo;


        //                    pdf = directorio.Trim() + (id.Trim() + tipoDoc + "_PDF.pdf");
        //                    Reporte.ExportToDisk(ExportFormatType.PortableDocFormat, pdf);
        //                    Reporte.Close();
        //                    Reporte.Dispose();
        //                }
        //                else
        //                {
        //                    rptFacturaE Reporte = new rptFacturaE();
        //                    //creamos una nueva instancia del DataSet


        //                    //creamos una nueva instancia del table adapter que usaremos para obtener la información de la base de datos
        //                    Reportes.dsReportesTableAdapters.sp_FacturaElectronicaTableAdapter dt = new Reportes.dsReportesTableAdapters.sp_FacturaElectronicaTableAdapter();

        //                    _SqlConnection.Open();

        //                    //le pasamos la conexión al tableadapter
        //                    dt.Connection = _SqlConnection;
        //                    //llenamos el tableadapter con el método fill
        //                    dt.Fill(ds.sp_FacturaElectronica, doc.id, doc.tipoDocumento);

        //                    Zen.Barcode.CodeQrBarcodeDraw barcode = new Zen.Barcode.CodeQrBarcodeDraw();

        //                    foreach (DataRow dr in ds.Tables["sp_FacturaElectronica"].Rows)
        //                    {
        //                        string conse = doc.clave != null ? doc.clave.ToString() : doc.tipoDocumento.ToString() + doc.id.ToString();
        //                        Image bar = barcode.Draw(conse, 6);
        //                        dr["LogoEmp"] = Utility.UrlImageToByteArray(@Global.Configuracion.logoRuta.Trim());
        //                        dr["Barcode"] = Utility.ImageToByteArray(bar);
        //                    }

        //                    Reporte.SetDataSource(ds);         
        //                    string id = doc.consecutivo == null ? doc.id.ToString() : doc.consecutivo;


        //                    pdf = directorio.Trim() + (id.Trim() + tipoDoc + "_PDF.pdf");
        //                    Reporte.ExportToDisk(ExportFormatType.PortableDocFormat, pdf);
        //                    Reporte.Close();
        //                    Reporte.Dispose();

        //                }



        //            }
        //            else
        //            {
        //                rptFacturaE Reporte = new rptFacturaE();
        //                //creamos una nueva instancia del DataSet


        //                //creamos una nueva instancia del table adapter que usaremos para obtener la información de la base de datos
        //                Reportes.dsReportesTableAdapters.sp_FacturaElectronicaTableAdapter dt = new Reportes.dsReportesTableAdapters.sp_FacturaElectronicaTableAdapter();

        //                _SqlConnection.Open();

        //                //le pasamos la conexión al tableadapter
        //                dt.Connection = _SqlConnection;
        //                //llenamos el tableadapter con el método fill
        //                dt.Fill(ds.sp_FacturaElectronica, doc.id, doc.tipoDocumento);

        //                Zen.Barcode.CodeQrBarcodeDraw barcode = new Zen.Barcode.CodeQrBarcodeDraw();

        //                foreach (DataRow dr in ds.Tables["sp_FacturaElectronica"].Rows)
        //                {
        //                    string conse = doc.clave != null ? doc.clave.ToString() : doc.tipoDocumento.ToString() + doc.id.ToString();
        //                    Image bar = barcode.Draw(conse, 6);
        //                    dr["LogoEmp"] = Utility.UrlImageToByteArray(@Global.Configuracion.logoRuta.Trim());
        //                    dr["Barcode"] = Utility.ImageToByteArray(bar);
        //                }

        //                Reporte.SetDataSource(ds);
        //              //  Reporte.SetParameterValue("LogoEmp", Global.Configuracion.logoRuta.Trim());

        //                string id = doc.consecutivo == null ? doc.id.ToString() : doc.consecutivo;


        //                pdf = directorio.Trim() + (id.Trim() + tipoDoc + "_PDF.pdf");
        //                Reporte.ExportToDisk(ExportFormatType.PortableDocFormat, pdf);
        //                Reporte.Close();
        //                Reporte.Dispose();

        //            }

        //        }

        //        return pdf;

        //    }
        //    catch (Exception ex)
        //    {

        //        throw ex;
        //    }
        //}

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
