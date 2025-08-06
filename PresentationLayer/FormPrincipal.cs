using AutoUpdaterDotNET;
using BusinessLayer;
using CommonLayer;
using EntityLayer;
using Microsoft.WindowsAPICodePack.Shell;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.Entity.Core.EntityClient;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Printing;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PresentationLayer
{
    public partial class FormPrincipal : Form
    {
        bool respuestaAprobaDesc = false;
        List<tbEmpresaActividades> listaAct;
        BActividadesEconomicas actIns = new BActividadesEconomicas();
        BActualizaciones actualiIns = new BActualizaciones();
        bool cerrando = false;
        public string pathArchivoConfig { get; set; }

        public int reporteNum { get; set; }


        public FormPrincipal()
        {
            InitializeComponent();
            cargarArchivoConfiguracion();
            if (cerrando)
            {
                this.Close();

            }
            else
            {
                configurarBaseDatos();
                if (Global.Configuracion.actualizaciones == (int)Enums.EstadoConfig.Si)
                {
                    CheckForUpdates();
                }
                licencia();
                AddVersionNumber();
            }


           // validarCertificados();
            validarFormatos();
            
        }

        private void configurarBaseDatos()
        {
            try
            {
                //obtnego el script de appConfig
                var originalConnectionString = ConfigurationManager.ConnectionStrings["dbSisSodInaEntities"].ConnectionString;
                var ecsBuilder = new EntityConnectionStringBuilder(originalConnectionString);
                string soruce = "";

                //obtengo del archivo de configuracion el servidor e instancia
                if (Global.Configuracion.instance == "0" || Global.Configuracion.instance == string.Empty)
                {
                    soruce = string.Format(@"{0}", Global.Configuracion.server);
                }
                else
                {
                    soruce = string.Format(@"{0}\{1}", Global.Configuracion.server, Global.Configuracion.instance);
                }


                //creo el nuevo string de conexion
                var sqlCsBuilder = new SqlConnectionStringBuilder(ecsBuilder.ProviderConnectionString)
                {
                    UserID = "sa",
                    Password = "crpp",
                    InitialCatalog = "dbSISSODINA",
                    DataSource = soruce
                };

                //verifico si funciona la conexionen caso de error se genera la execpcion de abajo
                SqlHelper sql = new SqlHelper(sqlCsBuilder.ToString());
                if (sql.isConnected)
                {

                    var providerConnectionString = sqlCsBuilder.ToString();
                    ecsBuilder.ProviderConnectionString = providerConnectionString;

                    Global.conexionReportes = sqlCsBuilder.ConnectionString;
                    Global.conexion = ecsBuilder.ToString();
                    //si es servidor busco actualizaciones de base de datos
                    if (Global.Configuracion.tipoEstacion == (int)Enums.tipoEstacion.servidor)
                    {
                        //envio a verificar si hay actuaizaciones de BD.
                        varificarActualizacionesBaseDatos(soruce);
                    }
                }

            }
            catch (Exception)
            {

                MessageBox.Show("Error al establecer conexión con la base de datos", "Base datos", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }

        private void varificarActualizacionesBaseDatos(string servidor)
        {
            try
            {
                //verifico si existe el archivo             
                string path = Directory.GetCurrentDirectory() + "\\temp\\script.sql";

                //  string path = "C:\\Users\\walpi\\Desktop\\script.sql";
                if (File.Exists(path))
                {
                    //leo el archico
                    string script = File.ReadAllText(path);

                    actualiIns.updateDataBase(script);
                    //elimino para que no se vuelva a ejecutar
                    File.Delete(path);
                }

            }
            catch (Exception)
            {

                MessageBox.Show("Error al ejecutar actualización de base datos", "Base datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void licencia()
        {
            try
            {
                if (Utility.AccesoInternet())
                {
                    Global.licenciaWeb = false;
                    FtpWebRequest request =
                    (FtpWebRequest)WebRequest.Create("ftp://ftp.espartanosolutions.com/licencias.json");
                    request.Credentials = new NetworkCredential("release@espartanosolutions.com", "Casa8383*");
                    request.Method = WebRequestMethods.Ftp.DownloadFile;

                    using (WebResponse response = request.GetResponse())
                    {
                        Stream responseStream = response.GetResponseStream();
                        Encoding encode = System.Text.Encoding.GetEncoding("utf-8");
                        using (var reader = new StreamReader(responseStream, encode))
                        {
                            string json = reader.ReadToEnd();
                            Global.licencias = JsonConvert.DeserializeObject<List<clsEmpresas>>(json);

                        }
                    }

                    Global.licenciaWeb = true;
                }
                else
                {
                    Global.licenciaWeb = false;
                }


            }
            catch (Exception ex)
            {

                Global.licenciaWeb = false;
            }

        }
        async Task CheckForUpdates()
        {
            try
            {
                AutoUpdater.HttpUserAgent = "AutoUpdater";
                AutoUpdater.ReportErrors = true;
                AutoUpdater.Mandatory = true;
                AutoUpdater.UpdateMode = Mode.ForcedDownload;
                AutoUpdater.RunUpdateAsAdmin = true;
                // AutoUpdater.DownloadPath = KnownFolders.LocalAppData.Path+ "\\EspartanoSoftware";
                AutoUpdater.DownloadPath = KnownFolders.Downloads.Path;
                BasicAuthentication basicAuthentication = new BasicAuthentication("release@espartanosolutions.com", "Casa8383*");
                AutoUpdater.BasicAuthXML = AutoUpdater.BasicAuthDownload = AutoUpdater.BasicAuthChangeLog = basicAuthentication;
                AutoUpdater.Start("ftp://ftp.espartanosolutions.com/releases/version.xml", new NetworkCredential("release@espartanosolutions.com", "Casa8383*"));

            }
            catch (Exception)
            {

                MessageBox.Show("Error al obtener la actualización", "Actualizacion", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void AddVersionNumber()
        {
            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            FileVersionInfo versionInfo = FileVersionInfo.GetVersionInfo(assembly.Location);

            lblTitulo.Text += $" v.{ versionInfo.FileVersion }";



        }


        private void FormPrincipal_Load(object sender, EventArgs e)
        {
            try
            {              
                timer1.Start();
                this.WindowState = FormWindowState.Maximized;
                Global.cerrar = false;
                inicio();
              
    

            }
            catch (Exception ex)
            {
                MessageBox.Show("Se generó un error" + ex.Message, "Seguridad", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        private void inicio()
        {
            panelformularios.Controls.Clear();
            cargarLogeo();
            if (!Global.cerrar)
            {
               


               imprimirDatos();
               cargarOpciones();

                validarCertificados();

                if (Global.Configuracion.respaldo == (int)Enums.EstadoConfig.Si)
                {
                    //DialogResult resp = MessageBox.Show("Desea realizar el respaldo de la base de datos.", "Seguridad", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    //if (resp == DialogResult.Yes)
                    //{
                        // MessageBox.Show("El respaldo se generará en segundo plano.","Seguridad", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        Backup respaldo = new Backup();

                    //}
                }

               // validarMensajeViciminetoLicencia();
                cargarActividades();
                //abirr el dashboard
                if (Global.Usuario.idRol == (int)Enums.roles.Administracion)
                {
                    AbrirFormulario<frmDashboard>((int)Enums.formularios.dashboard);
                    btnDashboard.BackColor = Color.FromArgb(12, 61, 92);

                }

                if (Global.Configuracion.aperturaCierre == (int)Enums.EstadoConfig.Si)
                {
         
                    inciarCaja();
                }
                registrarIngreso();

             
            }
            else
            {
                this.Close();
            }
        }


        public void registrarIngreso()
        {
            //registra el acceso en segundo plano
            BackgroundWorker tarea = new BackgroundWorker();

            tarea.DoWork += registrarIngresoAsync;
            tarea.RunWorkerAsync();

        }

        private void registrarIngresoAsync(object sender, DoWorkEventArgs e)
        {
            try
            {
                try
                {
                    if (Utility.AccesoInternet())
                    {

                        FtpWebRequest request =
                        (FtpWebRequest)WebRequest.Create("ftp://ftp.espartanosolutions.com/acceso.json");
                        request.Credentials = new NetworkCredential("release@espartanosolutions.com", "Casa8383*");
                        request.Method = WebRequestMethods.Ftp.DownloadFile;
                        var lista = new List<clsTerminal>();
                        using (WebResponse response = request.GetResponse())
                        {
                            Stream responseStream = response.GetResponseStream();
                            Encoding encode = System.Text.Encoding.GetEncoding("utf-8");
                            using (var reader = new StreamReader(responseStream, encode))
                            {
                                string json = reader.ReadToEnd();
                                lista = JsonConvert.DeserializeObject<List<clsTerminal>>(json);

                            }

                            if (!System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable())
                            {
                                Console.WriteLine("No Network Available");
                            }

                            IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());

                            var ippaddress = host
                                .AddressList
                                .FirstOrDefault(ip => ip.AddressFamily == AddressFamily.InterNetwork);


                            clsTerminal terminal = new clsTerminal();
                            terminal.ip = ippaddress.ToString();                            
                            terminal.id = Global.Usuario.tbEmpresa.id.Trim();                      
                            terminal.nombreEmpresa = Global.actividadEconomic.nombreComercial.Trim().ToUpper();
                            terminal.fecha = Utility.getDate();

                            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
                            FileVersionInfo versionInfo = FileVersionInfo.GetVersionInfo(assembly.Location);

                            terminal.version = versionInfo.FileVersion;

                            lista.Add(terminal);

                            var listaJson = JsonConvert.SerializeObject(lista);


                            subirArchivoIngreso(listaJson);


                        }

                    }

                }
                catch (Exception ex)  
                {

                }
            }
            catch (Exception)
            {


            }
        }

       

        private void subirArchivoIngreso(string listaJson)
        {
            try
            {
                FtpWebRequest request =
                       (FtpWebRequest)WebRequest.Create("ftp://ftp.espartanosolutions.com/acceso.json");
                request.Credentials = new NetworkCredential("release@espartanosolutions.com", "Casa8383*");
                request.Method = WebRequestMethods.Ftp.UploadFile;


                byte[] fileContents = Encoding.UTF8.GetBytes(listaJson);
            


                request.ContentLength = fileContents.Length;

                using (Stream requestStream = request.GetRequestStream())
                {
                    requestStream.Write(fileContents, 0, fileContents.Length);
                }

                using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
                {
                    Console.WriteLine($"Upload File Complete, status { response.StatusDescription}");
                }

            }
            catch (Exception ex)
            {

          
            }
        }

       

        private void cargarOpciones()
        {

            bool factElect = (bool)Global.Usuario.tbEmpresa.tbParametrosEmpresa.First().facturacionElectronica;


            if (Global.Usuario.idRol == (int)Enums.roles.facturadorSinPrivilegio || Global.Usuario.idRol == (int)Enums.roles.facturadorSuperMas)
            {
                btnFacturacion.Enabled = true;
                btnProductos.Enabled = false;
                btnClientes.Enabled = true;
                btnCompras.Enabled = false;
                btnAbonos.Enabled = true;
                btnCajas.Enabled = true;
                btnDocumentos.Enabled = true;
                btnEmpresa.Enabled = false;
                mnuUsuarios.Enabled = false;
                btnMantenimiento.Enabled = false;
                btnConsultas.Enabled = false;
                btnReportes.Enabled = false;
                btnProcesos.Enabled = false;
                btnDashboard.Enabled = false;



            }
            else if (Global.Usuario.idRol == (int)Enums.roles.facturador)
            {
                mnuUsuarios.Enabled = false;
                btnFacturacion.Enabled = true;
                btnProductos.Enabled = false;
                btnClientes.Enabled = true;
                btnCompras.Enabled = false;
                btnAbonos.Enabled = true;
                btnCajas.Enabled = true;
                btnDocumentos.Enabled = true;
                btnEmpresa.Enabled = false;           
                btnMantenimiento.Enabled = false;
                btnConsultas.Enabled = false;
                btnReportes.Enabled = false;
                btnProcesos.Enabled = false;
                btnDashboard.Enabled = false;

            }
            else
            {
                mnuUsuarios.Enabled = true;
                btnFacturacion.Enabled = true;
                btnProductos.Enabled = true;
                btnClientes.Enabled = true;
                btnCompras.Enabled = true;
                btnAbonos.Enabled = true;
                btnCajas.Enabled = true;
                btnDocumentos.Enabled = true;
                btnEmpresa.Enabled = true;             
                btnMantenimiento.Enabled = true;
                btnConsultas.Enabled = true;
                btnReportes.Enabled = true;
                btnProcesos.Enabled = true;
                btnDashboard.Enabled = true;
            }
         

            if (Global.Configuracion.imprimeExtra == (int)Enums.Estado.Activo && (bool)Global.Usuario.tbEmpresa.tbParametrosEmpresa.SingleOrDefault().comandas)
            {

                mnuAcompa.Visible = true;
            }
            else
            {
                mnuAcompa.Visible = false;
            }




            if (Global.Configuracion.aperturaCierre == (int)Enums.Estado.Activo)
            {

                btnCierreCaja.Enabled = true;
                mnuEstadoCaja.Visible = true;
                
            }
            else
            {
                btnCierreCaja.Enabled = false;
                mnuEstadoCaja.Visible = false;
               
            }



            if (Global.Usuario.idRol == (int)Enums.roles.facturadorSuperMas)
            {
                btnCierreCaja.Visible = false;
                lblCierreCaja.Visible = false;

            }
            else
            {
                btnCierreCaja.Visible = true;
                lblCierreCaja.Visible = true;
            }

            if (!factElect)
            {
                //dashboard
                btnValidacionHAC.Enabled = false;

                //menu
                mnuValidarHacienda.Visible = false;
                mnuComprasEnvHacienda.Visible = false;
            }
        }

            private void cargarLogeo()
        {
            try
            {
                frmLogin login = new frmLogin();

                if (!Global.cerrar)
                {
                    login.ShowDialog();
                }


            }
            catch (Exception ex)
            {
                cerrando = true;
                MessageBox.Show("Datos incorrecto, verifique.", "Logeo incorrecto", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }


        }

        private void cargarActividades()
        {
            try
            {
                listaAct = actIns.getListaEmpresaActividad(Global.Usuario.idEmpresa, (int)Global.Usuario.idTipoIdEmpresa).OrderByDescending(y => y.principal).ToList();
                cboActividades.DisplayMember = "Text";
                cboActividades.ValueMember = "Value";


                if (listaAct.Count == 0)
                {
                    //MessageBox.Show();
                }
                else
                {
                    if (listaAct.Count != 1)
                    {
                        cboActividades.Items.Add(new { Text = "ACTIVIDADES MÚLTIPLES", Value = "0" });

                    }

                    foreach (var item in listaAct)
                    {
                        cboActividades.Items.Add(new { Text = item.tbActividades.nombreAct.Trim(), Value = item.CodActividad });
                    }

                    if (listaAct.Count != 1)
                    {
                        cboActividades.SelectedIndex = 1;
                        Global.busquedaProductos = 1;
                    }
                    else
                    {
                        cboActividades.SelectedIndex = 0;
                        Global.busquedaProductos = 0;
                    }


                }

                cboActividades.SelectedValue = Global.actividadEconomic.CodActividad;
                cboActividades.Enabled = listaAct.Count != 1;

            }
            catch (Exception)
            {

                MessageBox.Show("Error al cargar actividades Económicas", "Actividades Económicas", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool validarFormatos()
        {
            NumberFormatInfo nfi = System.Globalization.CultureInfo.CurrentCulture.NumberFormat;
            string SeparadorDecimal = nfi.NumberDecimalSeparator;
            string separadorDeMiles = nfi.NumberGroupSeparator;



            if (SeparadorDecimal != "." || separadorDeMiles != ",")
            {
                MessageBox.Show("Error con el formato numerico de la aplicación. Formato correcto decimales (.), separador de miles(,)", "Formato numerico.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();

            }
            return SeparadorDecimal == "." && separadorDeMiles == ",";


        }

        private bool validarCertificados()
        {
            bool bandera = true;
            try
            {
                //6D7EBCE317B1FB0D1F781FDD9DB0CADE747851CA
                if ((bool)Global.Usuario.tbEmpresa.tbParametrosEmpresa.FirstOrDefault().facturacionElectronica)
                {
                    var certificado = Global.Usuario.tbEmpresa.certificadoInstalado.ToString();

                    if (certificado is null || certificado == "")
                    {
                        bandera = false;
                        MessageBox.Show("No tiene Clave criptográfica asignada. Los documnentos electrónicos se rechazarán por Hacienda.", "Llave critográfica", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }
                    else
                    {
                        X509Store store = new X509Store("My", StoreLocation.CurrentUser);
                        store.Open((OpenFlags.ReadOnly | OpenFlags.OpenExistingOnly));
                        X509Certificate2Collection CertCol = store.Certificates;


                        var cert = CertCol.Find(X509FindType.FindByThumbprint, certificado, true);
                        if (cert.Count == 0)
                        {
                            bandera = false;
                            MessageBox.Show("Llave criptografica no es válida o se encuentra vencida. Los documentos electrónicos se RECHAZARÁN por Hacienda.", "Llave critográfica", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        }

                    }

                }
            }
            catch (Exception)
            {


            }


            return bandera;
        }

        private void validarMensajeViciminetoLicencia()
        {
            DateTime fechaInicio = Utility.getDate().Date;
            DateTime fechaFin = Global.Usuario.tbEmpresa.fechaCaducidad.Value.Date;
            TimeSpan tSpan = fechaFin - fechaInicio;
            int days = tSpan.Days;

            if (days < 6)
            {
                MessageBox.Show(String.Format("Su licenciamiento vencerá en {0} días. Fecha de Vencimiento: {1}. Recuerde ponerse al día con anticipación y evitar inconvenientes.", days, fechaFin.ToShortDateString()), "***IMPORTANTE*** Licenciamiento", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }

        private void cargarArchivoConfiguracion()
        {
            try
            {
                string path = KnownFolders.Windows.Path + "/EspartanoSolutions/";
                string[] filePaths = Directory.GetFiles(path, "*.json",
                                         SearchOption.TopDirectoryOnly);
                if (filePaths.Count() > 0)
                {
                    if (filePaths.Count() == 1)
                    {
                        path = filePaths[0];
                    }
                    else
                    {
                        //cargar combo con las opciones
                        frmEmpresaCombo empresas = new frmEmpresaCombo();
                        empresas.listaEmpresas = filePaths;
                        empresas.pasarDatosEvent += rutaArchivo;
                        empresas.ShowDialog();
                        path = pathArchivoConfig;
                    }

                }
                else
                {
                    MessageBox.Show("Falta archivo de configuración del sistema. Favor contactar al Administrador", "Configuración", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    cerrando = true;
                    this.Close();
                }

                if (File.Exists(path))
                {
                    string Jsontxt = File.ReadAllText(path);
                    clsConfiguracion config = JsonConvert.DeserializeObject<clsConfiguracion>(Jsontxt);
                    config.rutaImpresoraExtra = config.rutaImpresoraExtra.Replace("//", @"\");

                           
                    Global.Configuracion = config;
                    //clsEvento evento = new clsEvento(Jsontxt, "1");
                }
                else
                {
                    MessageBox.Show("Falta archivo de configuración del sistema. Favor contactar al Administrador", "Configuración", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    cerrando = true;
                    this.Close();
                }

               // setImpresora();
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        private void setImpresora()
        {
            try
            {
                ////string print = "";

                ////for (int i = 0; i < PrinterSettings.InstalledPrinters.Count; i++)
                ////{
                ////    print = PrinterSettings.InstalledPrinters[i];
                ////    Global.Configuracion.i




                ////}

                PrintServer myPrintServer = new PrintServer(@"\\theServer");

                // List the print server's queues
                PrintQueueCollection myPrintQueues = myPrintServer.GetPrintQueues();
                String printQueueNames = "My Print Queues:\n\n";
                foreach (PrintQueue pq in myPrintQueues)
                {
                    printQueueNames += "\t" + pq.Name + "\n";
                }
                Console.WriteLine(printQueueNames);
                Console.WriteLine("\nPress Return to continue.");
                Console.ReadLine();




            }
            catch (Exception)
            {

                throw;
            }
        }

        private void rutaArchivo(string path)
        {
            if (path != null)
            {
                pathArchivoConfig = path;
            }
            else
            {
                cerrando = true;

                this.Close();
            }
        }

        private void inciarCaja()
        {

            BMovimiento movIns = new BMovimiento();
            tbCajasMovimientos mov = movIns.verificarCierreCaja(Global.Configuracion.caja, Global.Configuracion.sucursal);
            Global.Configuracion.mov = mov != null ? mov : null;

            if (mov == null || mov.fechaCierre != null)
            {
                frmInicioCaja frm = new frmInicioCaja();
                frm.ShowDialog();
            }

            else
            {

                if (mov.usuarioApertura.ToUpper().Trim() == Global.Usuario.nombreUsuario.ToUpper().Trim())
                {
                    MessageBox.Show("No se ha realizado cierre de caja, fecha inicio caja: " + mov.fechaApertura + " por el Usuario: " + mov.usuarioApertura.ToUpper().Trim(), "Estado Caja", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    // mnuCierreCaja.Enabled = true;


                }
                else
                {
                    // mnuCierreCaja.Enabled = false;
                    MessageBox.Show("La caja se encuentra Abierta por el usuario: " + mov.usuarioApertura.ToUpper().Trim() + ", con fecha: " + mov.fechaApertura, "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }


            }
        }


        //RESIZE METODO PARA REDIMENCIONAR/CAMBIAR TAMAÑO A FORMULARIO EN TIEMPO DE EJECUCION ----------------------------------------------------------
        private int tolerance = 12;
        private const int WM_NCHITTEST = 132;
        private const int HTBOTTOMRIGHT = 17;
        private Rectangle sizeGripRectangle;

        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case WM_NCHITTEST:
                    base.WndProc(ref m);
                    var hitPoint = this.PointToClient(new Point(m.LParam.ToInt32() & 0xffff, m.LParam.ToInt32() >> 16));
                    if (sizeGripRectangle.Contains(hitPoint))
                        m.Result = new IntPtr(HTBOTTOMRIGHT);
                    break;
                default:
                    base.WndProc(ref m);
                    break;
            }
        }
        //----------------DIBUJAR RECTANGULO / EXCLUIR ESQUINA PANEL 
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            var region = new Region(new Rectangle(0, 0, this.ClientRectangle.Width, this.ClientRectangle.Height));

            sizeGripRectangle = new Rectangle(this.ClientRectangle.Width - tolerance, this.ClientRectangle.Height - tolerance, tolerance, tolerance);

            region.Exclude(sizeGripRectangle);
            this.panelformularios.Region = region;
            this.Invalidate();
        }
        //----------------COLOR Y GRIP DE RECTANGULO INFERIOR
        protected override void OnPaint(PaintEventArgs e)
        {
            SolidBrush blueBrush = new SolidBrush(Color.FromArgb(244, 244, 244));
            e.Graphics.FillRectangle(blueBrush, sizeGripRectangle);

            base.OnPaint(e);
            ControlPaint.DrawSizeGrip(e.Graphics, Color.Transparent, sizeGripRectangle);
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        //Capturar posicion y tamaño antes de maximizar para restaurar
        int lx, ly;
        int sw, sh;
        private void btnMaximizar_Click(object sender, EventArgs e)
        {
            lx = this.Location.X;
            ly = this.Location.Y;
            sw = this.Size.Width;
            sh = this.Size.Height;
            btnMaximizar.Visible = false;
            btnRestaurar.Visible = true;
            this.Size = Screen.PrimaryScreen.WorkingArea.Size;
            this.Location = Screen.PrimaryScreen.WorkingArea.Location;
        }

        private void btnRestaurar_Click(object sender, EventArgs e)
        {
            btnMaximizar.Visible = true;
            btnRestaurar.Visible = false;
            this.Size = new Size(sw, sh);
            this.Location = new Point(lx, ly);
        }

        private void panelBarraTitulo_MouseMove(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void btnMinimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

       
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();



        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

      

   




        private void btnMenu_Click(object sender, EventArgs e)
        {
            if (panelMenu.Width == 200)
            {
                panelMenu.Width = 10;

            }
            else
            {
                panelMenu.Width = 200;
            }
        }

        private void btnsalir_Click(object sender, EventArgs e)
        {
            DialogResult dialog = MessageBox.Show("Esta seguro que desea cerra la Aplicación?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

            if (dialog == DialogResult.Yes)
            {
                cerrarRespaldo();
                Application.Exit();
            }
        }

        private void btnCambiarUser_Click(object sender, EventArgs e)
        {
            DialogResult resp = MessageBox.Show("Desea cerrar sesión?", "Cerrar sesion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (resp == DialogResult.Yes)
            {

                inicio();

            }
        }


       

        private void btnRespaldo_Click(object sender, EventArgs e)
        {
            frmRespaldoRestaurar respaldo = new frmRespaldoRestaurar();
            respaldo.ShowDialog();
        }

        private void cboActividades_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((string)cboActividades.SelectedValue != Global.actividadEconomic.CodActividad)
            {
                if (((ComboBox)sender).SelectedIndex == 0)
                {
                    Global.actividadEconomic = listaAct.Where(x => x.principal == true).SingleOrDefault();
                    Global.busquedaProductos = 0;
                }
                else
                {
                    Global.actividadEconomic = listaAct.Where(x => x.CodActividad == ((dynamic)((ComboBox)sender).SelectedItem).Value).SingleOrDefault();
                    Global.busquedaProductos = 1;
                }
                imprimirDatos();

            }
        }

        private void imprimirDatos()
        {
            lblNombre.Text = Global.Usuario.nombreUsuario.Trim().ToUpper();
            lblSucursal.Text = Global.Configuracion.sucursal.ToString();
            lblCaja.Text = Global.Configuracion.caja.ToString();
            pictImage.Image = Image.FromFile(Global.Configuracion.logoRuta);
            //lblEmpresa.Text= Global.actividadEconomic.nombreComercial.Trim();
            // lblActividad.Text = Global.actividadEconomic.nombreComercial.Trim();
            //mnuCambiarActividad.Visible = Global.multiActividad;
            lblEmpresa.Text = Global.actividadEconomic.nombreComercial.Trim();
        }

        private void FormPrincipal_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                try
                {
                    DialogResult resp = MessageBox.Show("Desea cerrar sesión?", "Cerrar sesion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (resp == DialogResult.Yes)
                    {

                        inicio();

                    }
                }
                catch (Exception)
                {

                    MessageBox.Show("Error", "Cerrar Sesión", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            else if (e.Modifiers == Keys.Alt && e.KeyCode == Keys.F12)
            {
                frmFacturacionContingencia frm = new frmFacturacionContingencia();
                frm.ShowDialog();
            }
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            DialogResult dialog = MessageBox.Show("Esta seguro que desea cerra la Aplicación?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

            if (dialog == DialogResult.Yes)
            {       
                cerrarRespaldo();
                Application.Exit();
            }  

        }

        private void pictureBox4_Click_1(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            btnMaximizar.Visible = true;
            btnRestaurar.Visible = false;
            this.Size = new Size(sw, sh);
            this.Location = new Point(lx, ly);
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            lx = this.Location.X;
            ly = this.Location.Y;
            sw = this.Size.Width;
            sh = this.Size.Height;
            btnMaximizar.Visible = false;
            btnRestaurar.Visible = true;
            this.Size = Screen.PrimaryScreen.WorkingArea.Size;
            this.Location = Screen.PrimaryScreen.WorkingArea.Location;
        }

        private void cerrarRespaldo()
        {           
                
            if (Global.Configuracion.respaldo == (int)Enums.EstadoConfig.Si)
            {
                DialogResult resp = MessageBox.Show("Desea realizar el respaldo de la base de datos.", "Seguridad", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (resp == DialogResult.Yes)
                {
                    // MessageBox.Show("El respaldo se generará en segundo plano.","Seguridad", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    frmBackUp respaldo = new frmBackUp();
                    respaldo.ShowDialog();

                }
            }
                        
        }

     


        #region APERTURA DE FORMULARIOS

        private void AbrirFormulario<MiForm>(int tipoForm) where MiForm : Form, new()
        {
            try
            {
                Form formulario;
                formulario = panelformularios.Controls.OfType<MiForm>().FirstOrDefault();//Busca en la colecion el formulario

                if (formulario != null &&  (tipoForm == (int)Enums.formularios.dashboard || tipoForm == (int)Enums.formularios.EntradaDinero || tipoForm == (int)Enums.formularios.SalidaDinero 
                    || tipoForm == (int)Enums.formularios.reporte || tipoForm == (int)Enums.formularios.facturacion || tipoForm == (int)Enums.formularios.facturacionReducida
                    ||  tipoForm == (int)Enums.formularios.facturacionSuper  || tipoForm == (int)Enums.formularios.estadoCaja))
                {
                    panelformularios.Controls.Remove(formulario);
                    formulario = null;
                }



                if (formulario == null)
                {
                    formulario = new MiForm();
                    verificarParametros(ref formulario, tipoForm);
                    formulario.TopLevel = false;
                    formulario.FormBorderStyle = FormBorderStyle.None;
                    formulario.Dock = DockStyle.Fill;
                    panelformularios.Controls.Add(formulario);
                    panelformularios.Tag = formulario;
                    formulario.Show();
                    formulario.BringToFront();
                    formulario.FormClosed += new FormClosedEventHandler(CloseForms);
                }
                ////si el formulario/instancia existe
                else
                {

                    formulario.BringToFront();

                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error el abrir el formulario", "Abrir Formulario", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (Global.Configuracion.pantallaFacturacion == (int)Enums.pantallaFacturacion.amplia)
            {
                AbrirFormulario<frmFacturacion>((int)Enums.formularios.facturacion);
            }
            else if (Global.Configuracion.pantallaFacturacion == (int)Enums.pantallaFacturacion.reducida)
            {
                AbrirFormulario<frmFacturacion1>((int)Enums.formularios.facturacionSuper);
            }
            else if (Global.Configuracion.pantallaFacturacion == (int)Enums.pantallaFacturacion.SuperReducida)
            {
                AbrirFormulario<frmFacturacionReducida>((int)Enums.formularios.facturacionReducida);
            }else if (Global.Configuracion.pantallaFacturacion == (int)Enums.pantallaFacturacion.fotos)
            {
                AbrirFormulario<frmFacturacionFotos>((int)Enums.formularios.facturacionFotos);
            }

            btnFacturacion.BackColor = Color.FromArgb(12, 61, 92);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            AbrirFormulario<frmProductos2>();
            btnProductos.BackColor = Color.FromArgb(12, 61, 92);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            AbrirFormulario<frmClientes>();
            btnClientes.BackColor = Color.FromArgb(12, 61, 92);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            AbrirFormulario<frmCompras>();
            btnCompras.BackColor = Color.FromArgb(12, 61, 92);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            AbrirFormulario<frmAbonoCredito>();
            btnAbonos.BackColor = Color.FromArgb(12, 61, 92);
        }
        private void AbrirFormulario<MiForm>() where MiForm : Form, new()
        {
            AbrirFormulario<MiForm>(0);
            btnProductos.BackColor = Color.FromArgb(12, 61, 92);
        }

        private void menuSalidaDinero_Click(object sender, EventArgs e)
        {
            AbrirFormulario<frmMovimientoDeDinero>((int)Enums.tipoMovimiento.SalidaDinero);
            menuSalidaDinero.BackColor = Color.FromArgb(12, 61, 92);
        }

        private void mnuEntradaDinero_Click(object sender, EventArgs e)
        {
            AbrirFormulario<frmMovimientoDeDinero>((int)Enums.tipoMovimiento.EntradaDinero);
            mnuEntradaDinero.BackColor = Color.FromArgb(12, 61, 92);
        }

        private void mnuFacturacion_Click(object sender, EventArgs e)
        {
            if (Global.Configuracion.pantallaFacturacion == (int)Enums.pantallaFacturacion.amplia)
            {
                AbrirFormulario<frmFacturacion>((int)Enums.formularios.facturacion);
            }
            else if (Global.Configuracion.pantallaFacturacion == (int)Enums.pantallaFacturacion.reducida)
            {
                AbrirFormulario<frmFacturacion1>((int)Enums.formularios.facturacionSuper);
            }
            else
            {
                AbrirFormulario<frmFacturacionReducida>((int)Enums.formularios.facturacionReducida);
            }

            mnuFacturacion.BackColor = Color.FromArgb(12, 61, 92);
        }

        private void mnuEntradasDineroCaja_Click(object sender, EventArgs e)
        {
            AbrirFormulario<frmMovimientoDeDinero>((int)Enums.tipoMovimiento.EntradaDinero);
            mnuEntradasDineroCaja.BackColor = Color.FromArgb(12, 61, 92);

        }

        private void mnuEmpresas_Click(object sender, EventArgs e)
        {
            AbrirFormulario<frmEmpresa>();
            mnuEmpresas.BackColor = Color.FromArgb(12, 61, 92);
        }

        private void mnuParametros_Click(object sender, EventArgs e)
        {
            AbrirFormulario<frmParametrosEmpresa>();
            mnuParametros.BackColor = Color.FromArgb(12, 61, 92);
        }

        private void mnuSalidaDineroCaja_Click(object sender, EventArgs e)
        {
            AbrirFormulario<frmMovimientoDeDinero>((int)Enums.tipoMovimiento.SalidaDinero);
            mnuSalidaDineroCaja.BackColor = Color.FromArgb(12, 61, 92);
        }

        private void mnuInventario_Click(object sender, EventArgs e)
        {
            AbrirFormulario<frmInventario>();
            mnuInventario.BackColor = Color.FromArgb(12, 61, 92);
        }

        private void mnuAbonos_Click(object sender, EventArgs e)
        {
            AbrirFormulario<frmAbonoCredito>();
            mnuAbonos.BackColor = Color.FromArgb(12, 61, 92);
        }

        private void mnuCompras_Click(object sender, EventArgs e)
        {
            AbrirFormulario<frmCompras>();
            mnuCompras.BackColor = Color.FromArgb(12, 61, 92);
        }

        private void mnuOrdenCompra_Click(object sender, EventArgs e)
        {
            AbrirFormulario<frmOrdenesCompra>();
            mnuOrdenCompra.BackColor = Color.FromArgb(12, 61, 92);
        }

        private void mnuGastos_Click(object sender, EventArgs e)
        {
            AbrirFormulario<frmGastos>();
            mnuGastos.BackColor = Color.FromArgb(12, 61, 92);
        }

        private void mnuImportarPreciosProducto_Click(object sender, EventArgs e)
        {
            AbrirFormulario<frmImportaListaPreciosJSON>();
            mnuImportarPreciosProducto.BackColor = Color.FromArgb(12, 61, 92);
        }

        private void mnuExportarPreciosProducto_Click(object sender, EventArgs e)
        {
            AbrirFormulario<frmExportListaPreciosJSON>();
            mnuExportarPreciosProducto.BackColor = Color.FromArgb(12, 61, 92);
        }

        private void mnuEstadoCaja_Click(object sender, EventArgs e)
        {
            AbrirFormulario<frmEstadoCaja>((int)Enums.formularios.estadoCaja);
            mnuEstadoCaja.BackColor = Color.FromArgb(12, 61, 92);
        }

        private void mnuDocEmitidos_Click(object sender, EventArgs e)
        {
            AbrirFormulario<frmBuscarDocumentos>();
            mnuDocEmitidos.BackColor = Color.FromArgb(12, 61, 92);
        }

        private void mnuValidarHacienda_Click(object sender, EventArgs e)
        {
            AbrirFormulario<frmValidacionDocsHacienda>();
            mnuValidarHacienda.BackColor = Color.FromArgb(12, 61, 92);
        }

        private void btnDocumentos_Click(object sender, EventArgs e)
        {
            AbrirFormulario<frmBuscarDocumentos>();
            btnDocumentos.BackColor = Color.FromArgb(12, 61, 92);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AbrirFormulario<frmValidacionDocsHacienda>();
            btnValidacionHAC.BackColor = Color.FromArgb(12, 61, 92);

        }

        private void mnuUsuarios_Click(object sender, EventArgs e)
        {
            AbrirFormulario<frmUsuario>();
            mnuUsuarios.BackColor = Color.FromArgb(12, 61, 92);
        }

        private void mnuProducto_Click(object sender, EventArgs e)
        {
            AbrirFormulario<frmProductos2>();
            mnuProducto.BackColor = Color.FromArgb(12, 61, 92);
        }

        private void mnuCategorias_Click(object sender, EventArgs e)
        {
            AbrirFormulario<frmCategoriaProducto>();
            mnuCategorias.BackColor = Color.FromArgb(12, 61, 92);
        }

        private void mnuMedidas_Click(object sender, EventArgs e)
        {
            AbrirFormulario<frmTipoMedida>();
            mnuMedidas.BackColor = Color.FromArgb(12, 61, 92);
        }

        private void mnuAcompanamiento_Click(object sender, EventArgs e)
        {
            AbrirFormulario<frmAcompanamiento>();
            mnuAcompanamiento.BackColor = Color.FromArgb(12, 61, 92);
        }

        private void mnuAcompaProducto_Click(object sender, EventArgs e)
        {
            AbrirFormulario<frmProductoAcompanam>();
            mnuAcompaProducto.BackColor = Color.FromArgb(12, 61, 92);
        }

        private void mnuProveedor_Click(object sender, EventArgs e)
        {
            AbrirFormulario<frmProveedores2>();
            mnuProveedores.BackColor = Color.FromArgb(12, 61, 92);
        }

        private void mnuClientes_Click(object sender, EventArgs e)
        {
            AbrirFormulario<frmClientes>();
            mnuClientes.BackColor = Color.FromArgb(12, 61, 92);
        }

        private void mnuTipoClientes_Click(object sender, EventArgs e)
        {
            AbrirFormulario<frmTipoCliente>();
            mnuTipoClientes.BackColor = Color.FromArgb(12, 61, 92);
        }

        private void mnuReporteCantidadProducto_Click(object sender, EventArgs e)
        {
            reporteNum = (int)Enums.reportes.productosGeneral;
            AbrirFormulario<frmReportes>((int)Enums.formularios.reporte);
            mnuReporteCantidadProducto.BackColor = Color.FromArgb(12, 61, 92);
         
        }

        private void mnuReporteVentaEsp_Click(object sender, EventArgs e)
        {
            reporteNum = (int)Enums.reportes.productoVentaEsp;
            AbrirFormulario<frmReportes>((int)Enums.formularios.reporte);
            mnuReporteVentaEsp.BackColor = Color.FromArgb(12, 61, 92);
        }

        private void mnuInvGeneral_Click(object sender, EventArgs e)
        {
            reporteNum = (int)Enums.reportes.inventarioGeneral;
            AbrirFormulario<frmReportes>((int)Enums.formularios.reporte);
            mnuInvGeneral.BackColor = Color.FromArgb(12, 61, 92);
        }

        private void mnuInvBajo_Click(object sender, EventArgs e)
        {
            reporteNum = (int)Enums.reportes.inventarioBajo;
            AbrirFormulario<frmReportes>((int)Enums.formularios.reporte);
            mnuInvBajo.BackColor = Color.FromArgb(12, 61, 92);
        }

        private void mnuInvSobre_Click(object sender, EventArgs e)
        {
            reporteNum = (int)Enums.reportes.inventarioSobre;
            AbrirFormulario<frmReportes>((int)Enums.formularios.reporte);
            mnuInvSobre.BackColor = Color.FromArgb(12, 61, 92);
        }

        private void mnuInvCategoria_Click(object sender, EventArgs e)
        {
            reporteNum = (int)Enums.reportes.inventarioCategoria;
            AbrirFormulario<frmReportes>((int)Enums.formularios.reporte);
            mnuInvCategoria.BackColor = Color.FromArgb(12, 61, 92);
        }
        private void mnuProductosFechas_Click(object sender, EventArgs e)
        {
            reporteNum = (int)Enums.reportes.ventasProductoFechaEsp;
            AbrirFormulario<frmReportes>((int)Enums.formularios.reporte);
            mnuProductosFechas.BackColor = Color.FromArgb(12, 61, 92);
        }
        private void mnuInvMenor0_Click(object sender, EventArgs e)
        {
            reporteNum = (int)Enums.reportes.InventarioMenorCero;
            AbrirFormulario<frmReportes>((int)Enums.formularios.reporte);
            mnuInvMenor0.BackColor = Color.FromArgb(12, 61, 92);
        }

        private void mnuInvCostoProv_Click(object sender, EventArgs e)
        {
            reporteNum = (int)Enums.reportes.inventarioCostoProv;
            AbrirFormulario<frmReportes>((int)Enums.formularios.reporte);
            mnuInvCostoProv.BackColor = Color.FromArgb(12, 61, 92);
        }

        private void mnuInvCostoCategoria_Click(object sender, EventArgs e)
        {
            reporteNum = (int)Enums.reportes.inventarioCostoCat;
            AbrirFormulario<frmReportes>((int)Enums.formularios.reporte);
            mnuInvCostoCategoria.BackColor = Color.FromArgb(12, 61, 92);
        }

        private void mnuInvCostoGeneral_Click(object sender, EventArgs e)
        {
            reporteNum = (int)Enums.reportes.costosInventario;
            AbrirFormulario<frmReportes>((int)Enums.formularios.reporte);
            mnuInvCostoGeneral.BackColor = Color.FromArgb(12, 61, 92);
        }

        private void mnuVentahoyResum_Click(object sender, EventArgs e)
        {
            reporteNum = (int)Enums.reportes.reporteGeneralVenta;
            AbrirFormulario<frmReportes>((int)Enums.formularios.reporte);
            mnuVentahoyResum.BackColor = Color.FromArgb(12, 61, 92);
        }

        private void mnuVentahoyDetall_Click(object sender, EventArgs e)
        {
            reporteNum = (int)Enums.reportes.ventasHoyDetallada;
            AbrirFormulario<frmReportes>((int)Enums.formularios.reporte);
            mnuVentahoyDetall.BackColor = Color.FromArgb(12, 61, 92);
        }

        private void mnuVentasResumidasFechas_Click(object sender, EventArgs e)
        {
            reporteNum = (int)Enums.reportes.ventasFechaInicioFin;
            AbrirFormulario<frmReportes>((int)Enums.formularios.reporte);
            mnuVentasResumidasFechas.BackColor = Color.FromArgb(12, 61, 92);
        }

        private void mnuVentasDetalladaFechas_Click(object sender, EventArgs e)
        {
            reporteNum = (int)Enums.reportes.ventasDetallada;
            AbrirFormulario<frmReportes>((int)Enums.formularios.reporte);
            mnuVentasDetalladaFechas.BackColor = Color.FromArgb(12, 61, 92);
        }

        private void mnuVentasProductoFechas_Click(object sender, EventArgs e)
        {
            reporteNum = (int)Enums.reportes.ventasProductoFechaEsp;
            AbrirFormulario<frmReportes>((int)Enums.formularios.reporte);
            mnuVentasProductoFechas.BackColor = Color.FromArgb(12, 61, 92);
        }

        private void mnuVentasMargenFechas_Click(object sender, EventArgs e)
        {
            reporteNum = (int)Enums.reportes.margenGancanciasVentas;
            AbrirFormulario<frmReportes>((int)Enums.formularios.reporte);
            mnuVentasMargenFechas.BackColor = Color.FromArgb(12, 61, 92);
        }

        private void mnuVentasMargenVendedorFechas_Click(object sender, EventArgs e)
        {
            reporteNum = (int)Enums.reportes.gananciasXVendedor;
            AbrirFormulario<frmReportes>((int)Enums.formularios.reporte);
            mnuVentasMargenVendedorFechas.BackColor = Color.FromArgb(12, 61, 92);
        }

        private void mnuVentasUsuario_Click(object sender, EventArgs e)
        {
            reporteNum = (int)Enums.reportes.ventasUsuarios;
            AbrirFormulario<frmReportes>((int)Enums.formularios.reporte);
            mnuVentasUsuario.BackColor = Color.FromArgb(12, 61, 92);
        }

        private void mnuVentaAgrupXClienteFechas_Click(object sender, EventArgs e)
        {
            reporteNum = (int)Enums.reportes.ventasAgrupadasFechaEsp;
            AbrirFormulario<frmReportes>((int)Enums.formularios.reporte);
            mnuVentaAgrupXClienteFechas.BackColor = Color.FromArgb(12, 61, 92);
        }

        private void mnuVentasResumAgrupaXActFecha_Click(object sender, EventArgs e)
        {
            reporteNum = (int)Enums.reportes.ventasResumidaAgrupadasFechaEsp;
            AbrirFormulario<frmReportes>((int)Enums.formularios.reporte);
            mnuVentasResumAgrupaXActFecha.BackColor = Color.FromArgb(12, 61, 92);
        }

        private void mnuComprasEnvHacienda_Click(object sender, EventArgs e)
        {
            reporteNum = (int)Enums.reportes.comprasReporteHacienda;
            AbrirFormulario<frmReportes>((int)Enums.formularios.reporte);
            mnuComprasEnvHacienda.BackColor = Color.FromArgb(12, 61, 92);
        }

        private void mnuComprasAgrupXProvFechas_Click(object sender, EventArgs e)
        {
            reporteNum = (int)Enums.reportes.comprasFechaEsp;
            AbrirFormulario<frmReportes>((int)Enums.formularios.reporte);
            mnuComprasAgrupXProvFechas.BackColor = Color.FromArgb(12, 61, 92);

        }

        private void mnuComprasResAgruXActFechas_Click(object sender, EventArgs e)
        {
            reporteNum = (int)Enums.reportes.comprasResumen;
            AbrirFormulario<frmReportes>((int)Enums.formularios.reporte);
            mnuComprasResAgruXActFechas.BackColor = Color.FromArgb(12, 61, 92);
        }

        private void mnuNCGeneralFechas_Click(object sender, EventArgs e)
        {
            reporteNum = (int)Enums.reportes.notasCreditoFechaIncioFin;
            AbrirFormulario<frmReportes>((int)Enums.formularios.reporte);
            mnuNCGeneralFechas.BackColor = Color.FromArgb(12, 61, 92);
        }

        private void mnuClienteEstadoCuenta_Click(object sender, EventArgs e)
        {
            reporteNum = (int)Enums.reportes.estadoCuentaCliente;
            AbrirFormulario<frmReportes>((int)Enums.formularios.reporte);
            mnuClienteEstadoCuenta.BackColor = Color.FromArgb(12, 61, 92);
        }

        private void mnuClienteAbonos_Click(object sender, EventArgs e)
        {
            reporteNum = (int)Enums.reportes.abonos;
            AbrirFormulario<frmReportes>((int)Enums.formularios.reporte);
            mnuClienteAbonos.BackColor = Color.FromArgb(12, 61, 92);
        }

        private void mnuCPDocVenc_Click(object sender, EventArgs e)
        {
            reporteNum = (int)Enums.reportes.morosos;
            AbrirFormulario<frmReportes>((int)Enums.formularios.reporte);
            mnuCPDocVenc.BackColor = Color.FromArgb(12, 61, 92);
        }

        private void mnuNPAbonosHoy_Click(object sender, EventArgs e)
        {
            reporteNum = (int)Enums.reportes.abonosHoy;
            AbrirFormulario<frmReportes>((int)Enums.formularios.reporte);
            mnuNPAbonosHoy.BackColor = Color.FromArgb(12, 61, 92);
        }

        private void mnuCPFechas_Click(object sender, EventArgs e)
        {
            reporteNum = (int)Enums.reportes.abonosFechas;
            AbrirFormulario<frmReportes>((int)Enums.formularios.reporte);
            mnuCPFechas.BackColor = Color.FromArgb(12, 61, 92);
        }

        private void mnuEstadoCuentaGeneralFechas_Click(object sender, EventArgs e)
        {
            reporteNum = (int)Enums.reportes.estadoCuentaFechas;
            AbrirFormulario<frmReportes>((int)Enums.formularios.reporte);
            mnuEstadoCuentaGeneralFechas.BackColor = Color.FromArgb(12, 61, 92);
        }

        private void btnRespaldar_Click(object sender, EventArgs e)
        {
            frmRespaldoRestaurar respaldo = new frmRespaldoRestaurar();
            respaldo.ShowDialog();
        }

        private void mnuCambiarContr_Click(object sender, EventArgs e)
        {
            AbrirFormulario<frmCambiarContrasena>();
            mnuCambiarContr.BackColor = Color.FromArgb(12, 61, 92);
        }
        private void btnCierreCaja_Click(object sender, EventArgs e)
        {
            if (Global.Usuario.idRol != (int)Enums.roles.Administracion && Global.Usuario.idRol == (int)Enums.roles.facturadorSuperMas)
            {
                aprobar();
                if (respuestaAprobaDesc)
                {
                    AbrirFormulario<frmEstadoCaja>((int)Enums.formularios.estadoCaja);
                    btnCierreCaja.BackColor = Color.FromArgb(12, 61, 92);

                }

                respuestaAprobaDesc = false;
            }
            else
            {
                AbrirFormulario<frmEstadoCaja>((int)Enums.formularios.estadoCaja);                
                btnCierreCaja.BackColor = Color.FromArgb(12, 61, 92);
            }

            respuestaAprobaDesc = false;
        }
        private void aprobar()
        {
            if (Global.Usuario.idRol != (int)Enums.roles.Administracion)
            {
                frmAprobacion aprobacionForm = new frmAprobacion();
                aprobacionForm.pasarDatosEvent += respuestaAprobacion;
                aprobacionForm.ShowDialog();
            }
            else
            {
                respuestaAprobaDesc = true;
            }
        }

        private void respuestaAprobacion(bool aprob)
        {
            respuestaAprobaDesc = aprob;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }


        private void verificarParametros(ref Form formulario, int tipoForm)
        {
            if (tipoForm == (int)Enums.formularios.EntradaDinero)
            {
                ((frmMovimientoDeDinero)formulario).tipoMov = (int)Enums.formularios.EntradaDinero;
            }
            else if (tipoForm == (int)Enums.formularios.SalidaDinero)
            {
                ((frmMovimientoDeDinero)formulario).tipoMov = (int)Enums.formularios.SalidaDinero;

            }
            else if (tipoForm == (int)Enums.formularios.reporte)
            {
                ((frmReportes)formulario).reporte = reporteNum;

            }
        }

        private void CloseForms(object sender, FormClosedEventArgs e)
        {
            if (Application.OpenForms["frmFacturacion"] == null)
                btnFacturacion.BackColor = Color.FromArgb(4, 41, 68);
            if (Application.OpenForms["frmProductos2"] == null)
                btnProductos.BackColor = Color.FromArgb(4, 41, 68);
            if (Application.OpenForms["frmClientes"] == null)
                btnClientes.BackColor = Color.FromArgb(4, 41, 68);
            if (Application.OpenForms["frmCompras"] == null)
                btnCompras.BackColor = Color.FromArgb(4, 41, 68);


        }


        #endregion

        #region APERTURA SUBMENU

        private void Open_DropDownMenu(ContextMenuStrip menu, Object sender)
        {
            Control control = (Control)sender;
            menu.VisibleChanged += new EventHandler((sender2, ev) => DropDownMenu_VisibleChanged(sender2, ev, control));
            menu.Show(control, control.Width, 0);

        }

        private void DropDownMenu_VisibleChanged(object sender, EventArgs ev, Control control)
        {
            ContextMenuStrip menu = (ContextMenuStrip)sender;
            if (menu.Visible)
            {
                menu.BackColor = Color.FromArgb(26, 32, 40);
                menu.ForeColor = Color.White;


            }

        }
        private void button2_Click_1(object sender, EventArgs e)
        {
            Open_DropDownMenu(mnuEmpresa, sender);
        }
        private void btnReportes_Click(object sender, EventArgs e)
        {
            Open_DropDownMenu(mnuReportes, sender);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Open_DropDownMenu(mnuCaja, sender);
        }

        private void btnSeguridad_Click(object sender, EventArgs e)
        {
            Open_DropDownMenu(mnuSeguridad, sender);
        }

        private void btnMantenimiento_Click(object sender, EventArgs e)
        {
            Open_DropDownMenu(mnuMantenimiento, sender);

        }

        private void panelBarraTitulo_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panelformularios_Paint(object sender, PaintEventArgs e)
        {

        }

  

        private void btnCambiarUsu_Click(object sender, EventArgs e)
        {

        }

        private void btnCierre_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblHora.Text = DateTime.Now.ToString("hh:mm:ss tt", CultureInfo.InvariantCulture);
            lblFecha.Text = DateTime.Now.ToShortDateString();
        }

        private void btnDashboard_Click(object sender, EventArgs e)
        {
            AbrirFormulario<frmDashboard>((int)Enums.formularios.dashboard);
            btnDashboard.BackColor = Color.FromArgb(12, 61, 92);
        }

        private void mnuCierreCaja_Click(object sender, EventArgs e)
        {
            AbrirFormulario<frmBuscarCierreCaja>();
            mnuCierreCaja.BackColor = Color.FromArgb(12, 61, 92);

        }

        private void btnCalculadora_Click(object sender, EventArgs e)
        {
            Process calc = new Process { StartInfo = { FileName = @"calc.exe" } };
            calc.Start();

            calc.WaitForExit();
        }

        private void lblTitulo_Click(object sender, EventArgs e)
        {

        }

        private void panelMenu_Paint(object sender, PaintEventArgs e)
        {

        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            AbrirFormulario<frmBuscarPromociones>();
            mnuPromociones.BackColor = Color.FromArgb(12, 61, 92);
        }

        private void toolStripMenuItem3_Click_1(object sender, EventArgs e)
        {
            AbrirFormulario<frmEtiquetas>();
            mnuEtiquetas.BackColor = Color.FromArgb(12, 61, 92);
        }

        private void porEstadoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            reporteNum = (int)Enums.reportes.promocionEstado;
            AbrirFormulario<frmReportes>((int)Enums.formularios.reporte);
            mnuPromocioPorEstado.BackColor = Color.FromArgb(12, 61, 92);

        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void resumidaAgrupadasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            reporteNum = (int)Enums.reportes.gastos;
            AbrirFormulario<frmReportes>((int)Enums.formularios.reporte);
            mnuReporteGastosFechas.BackColor = Color.FromArgb(12, 61, 92);
        }

        private void agrupadasXProveedorFechasEspecíficasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            reporteNum = (int)Enums.reportes.gastosPorProveedor;
            AbrirFormulario<frmReportes>((int)Enums.formularios.reporte);
            mnuReporteGastosXProveedor.BackColor = Color.FromArgb(12, 61, 92);
        }

        private void btnConsultas_Click(object sender, EventArgs e)
        {
            Open_DropDownMenu(mnuConsultas, sender);
        }

        private void btnProcesos_Click(object sender, EventArgs e)
        {
            Open_DropDownMenu(mnuProcesos, sender);
        }



        #endregion

      


    }
}
