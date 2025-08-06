using BusinessLayer;
using CommonLayer;
using CommonLayer.Exceptions.BussinessExceptions;
using EntityLayer;
using PresentationLayer.Clases;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace PresentationLayer
{
    public partial class frmFacturacionReducida : Form
    {

        List<tbCategoriaProducto> listaCategorias;
        BCategoriaProducto categoriaIns = new BCategoriaProducto();
        bTipoMedidas medidaIns = new bTipoMedidas();
        tbTipoMedidas medida = new tbTipoMedidas();
        List<tbImpuestos> listaImp;
        BImpuestos impIns = new BImpuestos();
        BPromociones promoIns = new BPromociones();

        bool isAlias = false;

        bool activarComandas = false;
        List<clsComanda> listaComandas = new List<clsComanda>();

        BPendientes pendientesIns = new BPendientes();
        BProducto productoIns = new BProducto();
        BFacturacion facturaIns = new BFacturacion();
        BInventario invetnarioIns = new BInventario();
        tbCategoriaProducto categoriaProducGlo = new tbCategoriaProducto();
        BProducto BProducto = new BProducto();
        Bcliente BCliente = new Bcliente();
        BUsuario usuarioIns = new BUsuario();
        List<tbProducto> listaProductos = new List<tbProducto>();
        List<tbDetalleDocumento> listaDetalleDocumento = new List<tbDetalleDocumento>();
        List<string> listaProductosPromo = new List<string>();
        tbClientes clienteGlo = null;
        tbDocumento documentoGlo = null;
        bool exoneracionClie = false;
        bool respuestaAprobaDesc = false;
        decimal porcDesc = 0;
        int tipoDoc = 1;
        decimal peso = decimal.MinValue;
        string alias = string.Empty;
        bool existeRespuesta = false;

        const float sizeText = 7;
        const int sizeCuadro = 60;
        private bool isProforma { set; get; }

        public delegate void FacturaGuardar(tbDocumento entity);
        public event FacturaGuardar detalleFacturacion;

        //para envio de correos mediante boton
        private int totalEmails = 0;

        private BackgroundWorker backgroundWorker;


        public frmFacturacionReducida()
        {
            InitializeComponent();
        }
        private void formatoGrid()
        {
            dtgvDetalleFactura.BorderStyle = BorderStyle.None;
            dtgvDetalleFactura.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            dtgvDetalleFactura.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dtgvDetalleFactura.DefaultCellStyle.SelectionBackColor = Color.OrangeRed;
            dtgvDetalleFactura.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dtgvDetalleFactura.BackgroundColor = Color.White;

            dtgvDetalleFactura.EnableHeadersVisualStyles = false;
            dtgvDetalleFactura.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dtgvDetalleFactura.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
            dtgvDetalleFactura.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

        }

        private void frmFacturacionReducida_Load(object sender, EventArgs e)
        {
            //pra envio de correos mediant boton
            backgroundWorker = new BackgroundWorker();
            backgroundWorker.DoWork += BackgroundWorker_DoWork;
            backgroundWorker.ProgressChanged += BackgroundWorker_ProgressChanged;
            backgroundWorker.RunWorkerCompleted += BackgroundWorker_RunWorkerCompleted;
            backgroundWorker.WorkerReportsProgress = true;

            listaImp = impIns.getImpuestos((int)Enums.Estado.Activo);
            chkTiqueteElectronico.Enabled = false;
            chkTiqueteElectronico.Checked = true;
            txtCliente.Enabled = true;
            cargarOpciones();
            isProforma = false;

            chkComandas.Visible = (bool)Global.Usuario.tbEmpresa.tbParametrosEmpresa.SingleOrDefault().comandas;
            chkComandas.Checked = (bool)Global.Usuario.tbEmpresa.tbParametrosEmpresa.SingleOrDefault().comandas;

            cargarOpcionesFacturaElectronica();

            formatoGrid();
            cargarDatos();
            txtCodigo.Focus();
            if (!Utility.AccesoInternet())
            {
                MessageBox.Show("No hay acceso a internet", "Sin Internet", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            lblPendientes.Text = pendientesIns.CantidadPendientes().ToString();

            cargarPendientes();
            txtCodigo.Select();
        }

        private void BackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            lblProgress.Text = "Completado!";
            btnEnvioCorreos.Enabled = true;
        }

        private void BackgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            var progressInfo = (dynamic)e.UserState;
            lblProgress.Text = $"Enviando: {progressInfo.Sent} de {progressInfo.Total}";
        }

        private void BackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                var lista = facturaIns.docsCorreoPendietes();
                totalEmails = lista.Count; // Establece el total de correos a enviar

                for (int i = 0; i < lista.Count; i++)
                {
                    SendEmail(lista[i]);
                    int progress = (i + 1) * 100 / lista.Count;
                    backgroundWorker.ReportProgress(progress, new { Sent = i + 1, Total = lista.Count });

                }

            }
            catch (Exception ex)
            {

                // MessageBox.Show("Error enviando correos automaticamente", "Envio Correos", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }
        private void SendEmail(tbDocumento documento)
        {
            bool bandera = false;
            if (documento.tipoDocumento == (int)Enums.TipoDocumento.Proforma || documento.tipoDocumento == (int)Enums.TipoDocumento.ProformaGeneral ||
                documento.tipoDocumento == (int)Enums.TipoDocumento.Factura || documento.tipoDocumento == (int)Enums.TipoDocumento.NotaCredito)
            {

                bandera = true;
            }
            else if (documento.tipoDocumento == (int)Enums.TipoDocumento.FacturaElectronica || documento.tipoDocumento == (int)Enums.TipoDocumento.TiqueteElectronico
                || documento.tipoDocumento == (int)Enums.TipoDocumento.NotaCreditoElectronica || documento.tipoDocumento == (int)Enums.TipoDocumento.NotaDebitoElectronica)
            {
                if (documento.EstadoFacturaHacienda != null)
                {
                    if (documento.EstadoFacturaHacienda.Trim().ToUpper().Equals("ACEPTADO"))
                    {

                        bandera = true;
                    }

                }
            }


            if (bandera)
            {

                List<string> correos = new List<string>();
                if (documento.correo1 != null)
                {
                    correos.Add(documento.correo1.Trim());

                }
                if (documento.correo2 != null)
                {
                    correos.Add(documento.correo2.Trim());

                }
                try
                {
                    clsDocumentoCorreo docCorreo = new clsDocumentoCorreo(documento, correos, true, (int)Enums.tipoAdjunto.factura);
                    bandera = CorreoElectronico.enviarCorreo(docCorreo);
                    if (bandera)
                    {

                        var doc = facturaIns.getEntity(documento);
                        doc.estadoCorreo = (int)Enums.EstadoCorreo.Enviado;
                        facturaIns.modificar(doc);


                    }
                }

                catch (Exception ex)
                {


                }
            }

        }

        private void cargarOpcionesFacturaElectronica()
        {
            bool factElect = (bool)Global.Usuario.tbEmpresa.tbParametrosEmpresa.First().facturacionElectronica;
            if (!factElect)
            {
                chkTiqueteElectronico.Checked = false;
                chkFacturaElectronica.Checked = false;
                chkTiqueteElectronico.Visible = false;
                chkFacturaElectronica.Visible = false;


            }
            else
            {
                bool regimenSimp = (bool)Global.Usuario.tbEmpresa.regimenSimplificado;
                if (!regimenSimp)
                {
                    chkTiqueteElectronico.Visible = true;
                    chkFacturaElectronica.Visible = false;

                    chkFacturaElectronica.Enabled = false;
                    chkTiqueteElectronico.Enabled = false;


                    chkTiqueteElectronico.Checked = true;
                    chkFacturaElectronica.Checked = true;
                }
                else
                {
                    int tipoRegimen = (int)Global.Usuario.tbEmpresa.tipoFacturacionRegimen;
                    if (tipoRegimen == (int)Enums.TipoFacturacionElectRegimenSimplificado.Todo)
                    {
                        chkTiqueteElectronico.Visible = true;
                        chkFacturaElectronica.Visible = false;

                        chkFacturaElectronica.Enabled = false;
                        chkTiqueteElectronico.Enabled = false;


                        chkTiqueteElectronico.Checked = true;
                        chkFacturaElectronica.Checked = true;

                    }
                    else
                    {
                        chkTiqueteElectronico.Visible = false;
                        chkFacturaElectronica.Visible = true;

                        chkFacturaElectronica.Enabled = true;
                        chkTiqueteElectronico.Enabled = false;


                        chkTiqueteElectronico.Checked = false;
                        chkFacturaElectronica.Checked = false;
                    }
                }

            }
        }
        private void cargarOpciones()
        {
            btnReImprimir.Enabled = (bool)Global.Usuario.tbEmpresa.imprimeDoc;

            chkServicioMesa.Visible = (bool)Global.Usuario.tbEmpresa.tbParametrosEmpresa.FirstOrDefault().servicioMesa;
            if (chkServicioMesa.Visible)
            {
                chkServicioMesa.Text = "Servicio Mesa(" + Global.Usuario.tbEmpresa.tbParametrosEmpresa.FirstOrDefault().porcServicioMesa + "%)";
            }

            bool factElect = (bool)Global.Usuario.tbEmpresa.tbParametrosEmpresa.First().facturacionElectronica;
            if (!factElect)
            {
                btnValidacion.Enabled = false;


            }
            //if (Global.Usuario.idRol == (int)Enums.roles.facturadorSuperMas)
            //{
            //    btnLimpiarForm.Enabled = false;


            //}
            //else
            //{
            //    btnLimpiarForm.Enabled = true;
            //}

            if (Global.Usuario.idRol == (int)Enums.roles.facturadorSuperMas || Global.Usuario.idRol == (int)Enums.roles.facturadorSinPrivilegio)
            {
                btnProductos.Enabled = false;


            }
            else if (Global.Usuario.idRol == (int)Enums.roles.facturador)
            {
                btnProductos.Enabled = true;
            }
            else
            {
                btnProductos.Enabled = true;
            }
        }

        private void cargarDatos()
        {
            try
            {
                listaCategorias = categoriaIns.getCategorias(1);
                cargaCategorias(listaCategorias);
                cargarProductos(listaCategorias);
            }
            catch (Exception)
            {

                MessageBox.Show("Error al cargar datos.", "Cargar datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }



        }

        private void cerrarForm()
        {
            this.Close();
        }

        private void cargarProductos(List<tbCategoriaProducto> listaCategorias)
        {
            if (listaProductos != null)
            {

                listaProductos.Clear();
            }



            foreach (tbCategoriaProducto categoria in listaCategorias)
            {
                List<tbProducto> prod = categoria.tbProducto.Where(x => x.estado == true).ToList();
                foreach (tbProducto pro in prod)
                {
                    if (pro.idProducto != "SM")
                    {
                        listaProductos.Add(pro);
                    }


                }
            }

        }

        private void cargaCategorias(List<tbCategoriaProducto> listaCategorias)
        {
            Button btn;
            int y = 0;
            int x = 0;

            foreach (tbCategoriaProducto categoria in listaCategorias)
            {
                if (categoria.tbProducto.Count != 0)
                {
                    btn = new Button();
                    btn.Name = "cat" + categoria.id.ToString();
                    btn.Text = categoria.nombre.Trim();
                    btn.Location = new System.Drawing.Point(sizeCuadro * x, sizeCuadro * y);
                    btn.Size = new System.Drawing.Size(sizeCuadro, sizeCuadro);
                    btn.BackColor = Color.DarkBlue;
                    btn.Font = new System.Drawing.Font("Bahnschrift", sizeText, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    btn.ForeColor = Color.White;
                    btn.TextAlign = ContentAlignment.MiddleCenter;
                    if (File.Exists(categoria.fotocategoria))
                    {

                        Image imagen = new Bitmap(categoria.fotocategoria.Trim());
                        Image final = new Bitmap(imagen, 90, 70);

                        btn.Image = final;
                        btn.ImageAlign = ContentAlignment.TopCenter;

                    }
                    btn.Click += new System.EventHandler(agregarProductoFacturacion);

                    pnlCategorias.Controls.Add(btn);
                    x++;

                    if (x == 5)
                    {
                        x = 0;
                        y++;

                    }

                }


            }

        }

        private void agregarProductoFacturacion(object sender, EventArgs e)
        {
            try
            {
                string isCat = ((Button)sender).Name.Substring(0, 3);


                if (isCat == "cat")
                {

                    string categoria = ((Button)sender).Name.Replace("cat", "");


                    pnlProductos.Controls.Clear();
                    int idCategoria = int.Parse(categoria);
                    //  listaProductos = productoIns.getListProductoByCategoria(idCategoria);

                    Button btn;
                    int y = 0;
                    int x = 0;

                    foreach (tbProducto pro in listaProductos)
                    {
                        if (pro.id_categoria == idCategoria)
                        {

                            btn = new Button();
                            btn.Name = "pro" + pro.idProducto.ToString();
                            btn.Text = pro.nombre.Trim();
                            btn.Location = new System.Drawing.Point(sizeCuadro * x + 5, sizeCuadro * y);
                            btn.Size = new System.Drawing.Size(sizeCuadro + 5, sizeCuadro);
                            btn.BackColor = Color.SeaGreen;
                            btn.Font = new System.Drawing.Font("Bahnschrift", sizeText, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                            btn.ForeColor = Color.White;
                            btn.TextAlign = ContentAlignment.MiddleCenter;
                            //if (File.Exists(pro.fotoPro))
                            //{
                            //    Image imagen = new Bitmap(pro.fotoPro);
                            //    Image final = new Bitmap(imagen, 90, 70);
                            //    btn.Image = final;
                            //    btn.ImageAlign = ContentAlignment.TopCenter;
                            //}

                            btn.Click += new System.EventHandler(agregarProductoFacturacion);
                            pnlProductos.Controls.Add(btn);
                            x++;

                            if (x == 10)
                            {
                                x = 0;
                                y++;
                            }
                        }
                    }

                }
                else
                {

                    //en caso que fuera un producto lo mando agregar al detalle de la factura
                    string producto = ((Button)sender).Name.Replace("pro", "");
                    foreach (tbProducto pro in listaProductos)
                    {

                        if (pro.idProducto == producto)
                        {

                            agregarProductoDetalleFactura(pro);

                            break;

                        }
                    }
                }
            }
            catch (Exception)
            {

                MessageBox.Show("Error al agregar el producto a facturación.", "Agregar productos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void txtIdCliente_TextChanged(object sender, EventArgs e)
        {
            if (txtIdCliente.Text == string.Empty)
            {
                txtIdCliente.Text = string.Empty;
                txtCliente.Text = string.Empty;
                txtTel.Text = string.Empty;
                txtCorreo.Text = string.Empty;
                exoneracionClie = false;
                clienteGlo = null;
                chkTiqueteElectronico.Enabled = false;
                chkTiqueteElectronico.Checked = false;


                calcularMontosT();

            }
        }

        private void calcularMontosT()
        {
            try
            {
               
                calcularSubtotales();
                calcularPromociones();
                calcularDescuentos();
                calcularImpuestos();
                calcularTotales();
                asignarLineasNumero();
                imprimirTotales();
             
            }
            catch (Exception)
            {

                MessageBox.Show("Error al calcular montos de facturación.", "Calcular montos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void calcularSubtotales()
        {
            foreach (tbDetalleDocumento det in listaDetalleDocumento)
            {
                det.montoTotal = det.precio * det.cantidad;
            }
        }

        private void calcularServicioMesa()
        {
            listaDetalleDocumento.Remove(listaDetalleDocumento.Where(x => x.idProducto == "SM").SingleOrDefault());
            if (chkServicioMesa.Checked)
            {
                tbDetalleDocumento detalle = new tbDetalleDocumento();
                detalle.idProducto = "SM";
                detalle.cantidad = 1;
                detalle.precioCosto = listaDetalleDocumento.Sum(x => x.totalLinea) * ((decimal)Global.Usuario.tbEmpresa.tbParametrosEmpresa.FirstOrDefault().porcServicioMesa / 100);

                detalle.precio = listaDetalleDocumento.Sum(x => x.totalLinea) * ((decimal)Global.Usuario.tbEmpresa.tbParametrosEmpresa.FirstOrDefault().porcServicioMesa / 100);
                detalle.montoTotal = detalle.cantidad * detalle.precio;
                detalle.porcExo = 0;
                detalle.porcImp = 0;


                detalle.totalLinea = detalle.cantidad * detalle.montoTotal;
                listaDetalleDocumento.Add(detalle);

            }
        }

        private void asignarLineasNumero()
        {
            int linea = 0;
            foreach (tbDetalleDocumento det in listaDetalleDocumento)
            {
                linea++;
                det.numLinea = linea;
            }

        }

        private void calcularTotales()
        {

            foreach (tbDetalleDocumento detalle in listaDetalleDocumento)
            {
                if (detalle.idProducto != "SM")
                {
                    detalle.totalLinea = (detalle.montoTotal - detalle.montoTotalDesc) + detalle.montoTotalImp - detalle.montoTotalExo;

                }


            }

        }

        private void imprimirTotales()
        {
            decimal total = 0, desc = 0, iva = 0, subtotal = 0, exo = 0, sm = 0;

            foreach (tbDetalleDocumento detalle in listaDetalleDocumento)
            {
                if (detalle.idProducto != "SM")
                {
                    total += detalle.totalLinea;
                    desc += detalle.montoTotalDesc;
                    iva += detalle.montoTotalImp;
                    exo += detalle.montoTotalExo;
                    subtotal += detalle.montoTotal;
                }
            }

            txtSubtotal.Text = string.Format("{0:N2}", subtotal);
            txtDescuento.Text = string.Format("{0:N2}", desc);
            txtIva.Text = string.Format("{0:N2}", iva);
            txtExoneracion.Text = string.Format("{0:N2}", exo);
            txtSub.Text = string.Format("{0:N2}", (subtotal - desc + iva) - exo);
            txtServicioMesa.Text = string.Format("{0:N2}", sm);
            if (listaDetalleDocumento.Where(x => x.idProducto == "SM").SingleOrDefault() != null)
            {
                sm = listaDetalleDocumento.Where(x => x.idProducto == "SM").Sum(x => x.totalLinea);
                txtServicioMesa.Text = string.Format("{0:N2}", sm);
            }
            txtTotal.Text = string.Format("{0:N2}", (total + sm));


            if (txtSubtotal.Text == string.Empty)
            {
                txtSubtotal.Text = "0";

            }
            if (txtTotal.Text == string.Empty)
            {
                txtTotal.Text = "0";

            }

            if (txtPorcetaje.Text == string.Empty)
            {
                txtPorcetaje.Text = "0";

            }
            if (txtDescuento.Text == string.Empty)
            {
                txtDescuento.Text = "0";

            }
            if (txtIva.Text == string.Empty)
            {
                txtIva.Text = "0";

            }
            if (txtExoneracion.Text == string.Empty)
            {
                txtExoneracion.Text = "0";

            }
            if (txtSub.Text == string.Empty)
            {
                txtSub.Text = "0";

            }
            if (txtServicioMesa.Text == string.Empty)
            {
                txtServicioMesa.Text = "0";

            }
        }

        private void calcularImpuestos()
        {

            foreach (tbDetalleDocumento detalle in listaDetalleDocumento)
            {
                if (detalle.idProducto != "SM")
                {
                    //sino es excento el producto
                    if (!detalle.tbProducto.esExento)
                    {


                        detalle.montoTotalImp = (detalle.montoTotal - detalle.montoTotalDesc)
                            * (((decimal)detalle.tbProducto.tbImpuestos.valor) / 100);
                        detalle.porcImp = (int)detalle.tbProducto.tbImpuestos.valor;
                        detalle.porcExo = 0;

                        detalle.montoTotalExo = 0;
                        //aplica exoneracion al cliente
                        if (exoneracionClie && (bool)detalle.tbProducto.aplicaExo)
                        {
                            if (clienteGlo.porcExo >= (decimal)detalle.tbProducto.tbImpuestos.valor)
                            {
                                detalle.montoTotalExo = (detalle.montoTotal - detalle.montoTotalDesc)
                            * (((decimal)detalle.tbProducto.tbImpuestos.valor) / 100);
                                detalle.porcExo = (int)detalle.tbProducto.tbImpuestos.valor;
                            }
                            else
                            {
                                detalle.montoTotalExo = (detalle.montoTotal - detalle.montoTotalDesc)
                               * (((decimal)clienteGlo.porcExo) / 100);
                                detalle.porcExo = (int)clienteGlo.porcExo;

                            }
                        }
                    }
                    else
                    {//no aplica impuesto ya que el producto es excento.
                        detalle.montoTotalImp = 0;
                        detalle.montoTotalExo = 0;
                        detalle.porcExo = 0;
                        detalle.porcImp = 0;

                    }

                }

            }
        }

        private void calcularPromociones()
        {
            if ((bool)Global.Usuario.tbEmpresa.tbParametrosEmpresa.FirstOrDefault().promociones)
            {
                foreach (tbDetalleDocumento det in listaDetalleDocumento)
                {
                    var promos = promoIns.getPromosByIdProdFechas(det.idProducto, Utility.getDate());
                    if (promos.Count != 0)
                    {
                        //ordeno descendentemente para obtener el que se actualizo de utlimo
                        promos = promos.OrderByDescending(x => x.fecha_ult_mod).ToList();
                        decimal descPromo = promoIns.getPromosByIdProdFechas(det.idProducto, Utility.getDate()).FirstOrDefault().descuento;
                        det.descuento = (decimal)descPromo;
                        det.montoTotalDesc = det.montoTotal * (descPromo / 100);
                        if (!listaProductosPromo.Contains(det.idProducto))
                        {
                            listaProductosPromo.Add(det.idProducto);
                        }
                    }
                    else
                    {
                        det.descuento = 0;
                        det.montoTotalDesc = 0;

                    }

                }

            }
        }
        private void calcularDescuentos()
        {

            decimal descMaxEmp = ((decimal)Global.Usuario.tbEmpresa.tbParametrosEmpresa.First().descuentoBase) / 100;
            bool aprobDescEmp = (bool)Global.Usuario.tbEmpresa.tbParametrosEmpresa.First().aprobacionDescuento;
            bool validaDesc = true;
            decimal porc = 0;

            if (txtPorcetaje.Text != string.Empty)
            {

                porc = decimal.Parse(txtPorcetaje.Text) / 100;

            }
            else
            {
                txtDescuento.Text = "0";
            }
            //verifica si el descuento es mayoral porcentaje permitido en la empresa, de lo contrario que aplique
            if (porc > descMaxEmp)
            {//se debe de solicitar aprobacion en caso que la empresa lo requiera, segun el parametro de la empresa
                if (aprobDescEmp)
                {
                    if (!respuestaAprobaDesc && porcDesc != decimal.Parse(txtPorcetaje.Text.Trim()))
                    {

                        //form para aprobacion de un administrador true aprueba, false no aprueba

                        aprobar();
                        porcDesc = decimal.Parse(txtPorcetaje.Text.Trim());  
                        validaDesc = respuestaAprobaDesc;
                    }
                    else
                    {
                        validaDesc = true;
                    }
                }
                else
                {

                    validaDesc = true;

                }
            }
            if (validaDesc)
            {
                foreach (tbDetalleDocumento detalle in listaDetalleDocumento)
                {                  
                    if (detalle.idProducto != "SM")
                    {                      
                        if (!listaProductosPromo.Contains(detalle.idProducto))
                        {
                            if ((bool)detalle.tbProducto.aplicaDescuento)
                            {
                                if (porc > ((detalle.tbProducto.descuento_max) / 100))
                                {
                                    detalle.descuento = (decimal)detalle.tbProducto.descuento_max;
                                    detalle.montoTotalDesc = detalle.montoTotal * ((decimal)detalle.tbProducto.descuento_max / 100);
                                }
                                else
                                {
                                    detalle.descuento = (decimal)porc * 100;
                                    detalle.montoTotalDesc = detalle.montoTotal * porc;
                                }
                            }
                            else
                            {
                                detalle.descuento = 0;
                                detalle.montoTotalDesc = 0;

                            }

                        }
                        
                    }

                }

            }
            else
            {
                porcDesc = 0;
                txtPorcetaje.Text = porcDesc.ToString();
                calcularMontosT();
            }
            respuestaAprobaDesc = false;

        }

        private void respuestaAprobacion(bool aprob)
        {
            respuestaAprobaDesc = aprob;
            porcDesc = decimal.Parse(txtPorcetaje.Text.Trim());
        }

        private void btnBuscarCliente_Click(object sender, EventArgs e)
        {
            try
            {
                FrmBuscar buscar = new FrmBuscar();
                buscar.pasarDatosEvent += dataBuscar;
                buscar.ShowDialog();
            }
            catch (Exception)
            {

                MessageBox.Show("Error al buscar el cliente.", "Buscar cliente", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataBuscar(tbClientes cliente)
        {
            try
            {
                exoneracionClie = false;
                if (cliente != null)
                {
                    txtCliente.Enabled = false;
                    chkTiqueteElectronico.Enabled = true;
                    chkTiqueteElectronico.Checked = false;
                    chkEnviar.Checked = true;
                    clienteGlo = cliente;
                    if ((bool)cliente.exonera)
                    {
                        DialogResult result = MessageBox.Show("El cliente seleccionado aplica para exoneración de impuesto, ¿Desea aplicar la exoneración de impuestos?", "Exoneración de Impuestos", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (result == DialogResult.Yes)
                        {
                            exoneracionClie = true;

                        }
                        else
                        {
                            exoneracionClie = false;
                        }

                    }
                    else
                    {
                        if ((bool)cliente.aplicaDescAuto)
                        {

                            txtPorcetaje.Text = cliente.descuentoMax.ToString();
                        }
                    }
                    txtIdCliente.Text = cliente.id.Trim();
                    if (cliente.tipoId == 1)
                    {
                        txtCliente.Text = cliente.tbPersona.nombre.Trim().ToUpper() + " " + cliente.tbPersona.apellido1.Trim().ToUpper() + " " + cliente.tbPersona.apellido2.Trim().ToUpper();

                    }
                    else
                    {
                        txtCliente.Text = cliente.tbPersona.nombre.Trim().ToUpper();

                    }
                    txtTel.Text = cliente.tbPersona.telefono.ToString().Trim().ToUpper();
                    txtCorreo.Text = cliente.tbPersona.correoElectronico.Trim();

                    calcularMontosT();

                }
            }
            catch (Exception)
            {

                MessageBox.Show("Error al buscar el cliente.", "Buscar cliente", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            buscarProducto();
        }

        private void buscarProducto()
        {

            //bool isOK = false;

            frmBuscarProducto buscarProduct = new frmBuscarProducto();


            buscarProduct.recuperarEntidadCant += recuperarEntidad;
            buscarProduct.tipoBusqueda = 2;
            buscarProduct.ShowDialog();



        }
        private void recuperarEntidad(tbProducto entidad, decimal cant)
        {

            //Cargamos los valores a la entidad.
            if (entidad != null)
            {
                agregarProductoDetalleFactura(entidad, 1, cant, 0, true, true);
            }
        }
        private void agregarProductoDetalleFactura(tbProducto pro)
        {

            agregarProductoDetalleFactura(pro, 1, 1, 0, true, false);
        }

        private void agregarProductoDetalleFactura(tbProducto pro, int tipo, decimal cantidad, decimal desc, bool acumular, bool isActualizacion)
        {
            try
            {

                bool aprobAdminEliminar = (bool)Global.Usuario.tbEmpresa.tbParametrosEmpresa.FirstOrDefault().aprobarEliminar;

                //cantidad = Math.Round(cantidad, 3);
                if (pro.codigoCabys == null || pro.codigoCabys == string.Empty || pro.codigoCabys.Count() != 13)
                {
                    MessageBox.Show("El producto no posee código CABYS.", "Cabys", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;

                }
                if ((bool)Global.Usuario.tbEmpresa.tbParametrosEmpresa.FirstOrDefault().validaCabys)
                {
                    var respCabys = ConsultasAPI.ConsultaCABYS(pro.codigoCabys.Trim()).Result;
                    if (!respCabys)
                    {
                        MessageBox.Show("El Cabys del producto se encuentra deshabilitado en Hacienda, las factura se RECHAZARÁ. Cambie el CABYS antes de facturar", "Cabys", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }



                bool banderaExitProd = false;
                medida.idTipoMedida = pro.idMedida;
                medida = medidaIns.GetEnityById(medida);
                if (!isActualizacion)
                {
                    if (medida.nomenclatura.Trim().ToUpper() == Enum.GetName(typeof(Enums.TipoMedida), Enums.TipoMedida.kg).Trim().ToUpper())
                    {
                        frmCantidad cantidadfrm = new frmCantidad();
                        cantidadfrm.pasarDatosEvent += cantidadPasarDatos;
                        cantidadfrm.ShowDialog();
                        if (peso == decimal.MinValue)
                        {
                            return;
                        }
                        cantidad = decimal.Parse(string.Format("{0:f3}", peso));
                    }
                }


                if (tipo == 1)//cant
                {

                    bool banderaInventario = verificarInventario(pro, cantidad, acumular);


                    if (banderaInventario)
                    {
                        foreach (tbDetalleDocumento det in listaDetalleDocumento)
                        {

                            if (det.idProducto == pro.idProducto)
                            {
                                if (acumular)
                                {
                                    det.cantidad += cantidad;
                                    //det.precio = buscarPrecioProducto(pro);
                                }
                                else
                                {
                                    if (aprobAdminEliminar && cantidad < det.cantidad)
                                    {
                                        aprobar();
                                        if (respuestaAprobaDesc)
                                        {
                                            det.cantidad = cantidad;
                                        }

                                    }
                                    else
                                    {
                                        det.cantidad = cantidad;

                                    }

                                }
                               // det.precio = decimal.Parse(string.Format("{0:f5}", det.precio));
                                det.montoTotal = det.precio * det.cantidad;
                                // det.descuento = 0;

                                banderaExitProd = true;
                                break;
                            }
                        }
                        //prodcuto nuevo
                        if (!banderaExitProd)
                        {

                            tbDetalleDocumento detalle = new tbDetalleDocumento();
                            detalle.cantidad = cantidad;
                            detalle.idProducto = pro.idProducto;
                            detalle.precio = buscarPrecioProducto(pro);
                            detalle.precioCosto = pro.precio_real;
                            detalle.montoTotal = detalle.precio * detalle.cantidad;
                            detalle.montoTotalDesc = 0;
                            detalle.montoTotalExo = 0;
                            detalle.montoTotalImp = 0;
                            detalle.porcExo = 0;
                            detalle.porcImp = 0;
                            detalle.tbProducto = pro;
                            listaDetalleDocumento.Add(detalle);
                        }
                    }
                    else
                    {
                        MessageBox.Show("El producto ingresado ya no cuenta con existencia en inventario. Cantidad existencia (" + pro.tbInventario.cantidad + ")", "Inexistencia Inventario", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    }

                }
                else if (tipo == 2)//precio
                {
                    listaDetalleDocumento.Where(x => x.idProducto == pro.idProducto).SingleOrDefault().precio = cantidad;

                    listaDetalleDocumento.Where(x => x.idProducto == pro.idProducto).SingleOrDefault().montoTotal =
                        listaDetalleDocumento.Where(x => x.idProducto == pro.idProducto).SingleOrDefault().precio *
                        listaDetalleDocumento.Where(x => x.idProducto == pro.idProducto).SingleOrDefault().cantidad;



                }
                else if (tipo == 3)//precio IVA
                {

                }
                else
                {


                    listaDetalleDocumento.Where(x => x.idProducto == pro.idProducto).SingleOrDefault().descuento = desc;


                }

                calcularMontosT();
                agregarProductoGrid();
                txtCodigo.Select();
                respuestaAprobaDesc = false;
            }
            catch (Exception)
            {

                MessageBox.Show("Error al agregar el producto a facturación.", "Agregar producto", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }



        private void cantidadPasarDatos(decimal peso)
        {
            this.peso = peso;
        }

        private void agregarProductoGrid()
        {
            try
            {
                decimal cantidadProd = 0;
                dtgvDetalleFactura.Rows.Clear();
                bool banderaSm = false;
                foreach (tbDetalleDocumento detalle in listaDetalleDocumento)
                {
                    if (detalle.idProducto != "SM")
                    {
                        cantidadProd += detalle.cantidad;
                        DataGridViewRow row = (DataGridViewRow)dtgvDetalleFactura.Rows[0].Clone();

                        row.Cells[0].Value = detalle.tbProducto.nombre.Trim();
                        if (detalle.precio == 0)
                        {
                            row.Cells[1].Value = "0.00";
                        }
                        else
                        {
                            row.Cells[1].Value = string.Format("{0:N2}", detalle.precio);
                        }


                        row.Cells[2].Value = string.Format("{0:N3}", detalle.cantidad);
                        row.Cells[3].Value = string.Format("{0:N2}", detalle.tbProducto.descuento_max);
                        row.Cells[4].Value = string.Format("{0:N2}", detalle.montoTotal);
                        row.Cells[5].Value = detalle.idProducto.ToString().Trim();

                        dtgvDetalleFactura.Rows.Add(row);
                        if (dtgvDetalleFactura.Rows.Count > 0)
                        {
                            dtgvDetalleFactura.FirstDisplayedScrollingRowIndex = dtgvDetalleFactura.Rows.Count - 1;
                        }
                    }
                    else
                    {
                        banderaSm = true;
                    }

                    // dtgvDetalleFactura.Rows[listaDetalleDocumento.Count-1].Selected=true;
                }
                lblTotalProducto.Text = string.Format("{0:N2}", cantidadProd);

                if (banderaSm)
                {
                    lblCantidadLineas.Text = (listaDetalleDocumento.Count - 1).ToString();
                }
                else
                {
                    lblCantidadLineas.Text = (listaDetalleDocumento.Count).ToString();

                }


                dtgvDetalleFactura.Rows[dtgvDetalleFactura.RowCount - 1].Selected = true;
            }
            catch (Exception)
            {

                MessageBox.Show("Error al agregar el producto a a la lista.", "Agregar producto lista", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }



        }

        private decimal buscarPrecioProducto(tbProducto pro)
        {
            decimal precioPro = 0;

            //Global.Usuario = usuarioIns.getLoginUsuario(Global.Usuario);
            int preciobase = (int)Global.Usuario.tbEmpresa.tbParametrosEmpresa.First().precioBase;


            if (clienteGlo != null)
            {
                precioPro = clienteGlo.precioAplicar;

            }

            //if ((bool)Global.Usuario.tbEmpresa.tbParametrosEmpresa.FirstOrDefault().promociones)
            //{

            //    var promos = promoIns.getPromosByIdProdFechas(pro.idProducto, Utility.getDate());
            //    if (promos.Count != 0)
            //    {
            //        //ordeno descendentemente para obtener el que se actualizo de utlimo
            //        promos = promos.OrderByDescending(x => x.fecha_ult_mod).ToList();
            //        descPromo = promoIns.getPromosByIdProdFechas(pro.idProducto, Utility.getDate()).FirstOrDefault().descuento;

            //    }

            //}

            decimal imp = (decimal)listaImp.Where(x => x.id == pro.idTipoImpuesto).FirstOrDefault().valor;
            ProductoDTO producto = ProductosUtility.calcularPrecio(pro.precio_real, (decimal)(pro.utilidad1Porc), (decimal)(pro.utilidad2Porc), (decimal)(pro.utilidad3Porc), imp);

            if (preciobase == 1)
            {
                precioPro = (decimal)producto.MontoUtilidad1;
            }
            else if (preciobase == 2)
            {
                precioPro = (decimal)producto.MontoUtilidad2;
            }
            else
            {
                precioPro = (decimal)producto.MontoUtilidad3;
            }


            return precioPro;
        }



        private bool verificarInventario(tbProducto pro, decimal cantidadSolic, bool acumular)
        {
            bool verificar = true;
            decimal cantidad = 0;

            bool verificaInventario = (bool)Global.Usuario.tbEmpresa.tbParametrosEmpresa.First().manejaInventario;
            decimal cantidadInventario = (decimal)pro.tbInventario.cantidad;
            if (verificaInventario)
            {
                foreach (tbDetalleDocumento det in listaDetalleDocumento)
                {

                    if (det.idProducto == pro.idProducto)
                    {
                        cantidad = det.cantidad;
                        break;
                    }
                }
                if (acumular)
                {
                    cantidad += cantidadSolic;
                }
                else
                {
                    cantidad = cantidadSolic;
                }


                if (pro.tbInventario.cantidad >= cantidad)
                {
                    verificar = true;
                }
                else
                {
                    verificar = false;
                }

            }


            return verificar;

        }

        private void txtCodigo_KeyPress(object sender, KeyPressEventArgs e)
        {
            tbProducto prod = null;
            if ((int)e.KeyChar == (int)Keys.Enter)
            {
                string codigo = txtCodigo.Text;
                if (codigo != string.Empty)
                {
                    prod = buscarProducto(codigo);
                    if (prod != null)
                    {
                        agregarProductoDetalleFactura(prod);
                    }

                }
                txtCodigo.Text = string.Empty;
                txtCodigo.Select();
            }
        }

        private tbProducto buscarProducto(string idProd)
        {
            tbProducto producto = new tbProducto();
            if (idProd != string.Empty)
            {
                producto.idProducto = idProd;

                Global.Usuario = usuarioIns.getLoginUsuario(Global.Usuario);
                producto = BProducto.GetEntity(producto, (int)Enums.EstadoBusqueda.Activo);

                if (producto == null)
                {
                    producto = null;
                    MessageBox.Show("El producto digitado no se encuentra en la base datos.", "Producto Inexistente", MessageBoxButtons.OK, MessageBoxIcon.Stop);

                }
            }
            else
            {
                producto = null;


            }
            return producto;

        }

        private void txtIdCliente_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((int)e.KeyChar == (int)Keys.Enter)
            {
                if (txtIdCliente.Text != string.Empty)
                {
                    try
                    {
                        clienteGlo = BCliente.GetClienteById(txtIdCliente.Text.Trim());
                        if (clienteGlo != null)
                        {
                            dataBuscar(clienteGlo);
                        }
                        else
                        {
                            DialogResult resp = MessageBox.Show($"No se encontró el Cliente con el ID: {txtIdCliente.Text.Trim()}, ¿Desea agregarlo nuevo?", "Buscar Cliente", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                            if (resp == DialogResult.Yes)
                            {
                                try
                                {
                                    frmClientes buscar = new frmClientes();
                                    buscar.id = txtIdCliente.Text;
                                    buscar.pasarDatosEvent += dataBuscar;
                                    buscar.ShowDialog();
                                }
                                catch (Exception)
                                {

                                    MessageBox.Show("Error al buscar el cliente.", "Buscar cliente", MessageBoxButtons.OK, MessageBoxIcon.Question);
                                }
                            }
                            else
                            {
                                clienteGlo = null;
                                txtIdCliente.Text = string.Empty;
                                txtCliente.Text = string.Empty;

                                txtTel.Text = string.Empty;
                                txtCorreo.Text = string.Empty;
                                txtCorreo2.Text = string.Empty;
                            }

                        }
                    }
                    catch (Exception)
                    {
                        txtCliente.Text = string.Empty;
                        MessageBox.Show("No se pudo obtener el Cliente, verifique los datos", "Buscar Cliente", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }
                }
            }
        }

        private void dtgvDetalleFactura_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 6)
                {
                    string codigoProducto = dtgvDetalleFactura.Rows[e.RowIndex].Cells[5].Value.ToString();
                    if (codigoProducto != string.Empty)
                    {

                        DialogResult result = MessageBox.Show("¿Desea eliminar el producto " + dtgvDetalleFactura.Rows[e.RowIndex].Cells[0].Value.ToString().Trim() + " de la factura?", "Eliminar linea", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (result == DialogResult.Yes)
                        {
                           
                            eliminarProducto(codigoProducto);

                        }
                    }
                }
            }
            catch (Exception)
            {

                MessageBox.Show("Error al eliminiar la línea", "Eliminar línea", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void eliminarProducto(string codigoProducto)
        {
            bool bandera = false;
            if ((bool)Global.Usuario.tbEmpresa.tbParametrosEmpresa.FirstOrDefault().aprobarEliminar)
            {
                aprobar();
                bandera = respuestaAprobaDesc;
            }
            else
            {
                bandera = true;

            }

            if (bandera)
            {

                foreach (tbDetalleDocumento detalle in listaDetalleDocumento)
                {
                    if (detalle.idProducto.Trim() == codigoProducto.Trim())
                    {
                        listaDetalleDocumento.Remove(detalle);
                        listaProductosPromo.Remove(detalle.idProducto);
                        break;
                    }

                }


                calcularMontosT();
                agregarProductoGrid();
                

            }

            respuestaAprobaDesc = false;



        }

        public bool aprobarDescuento(decimal desc)
        {

            decimal descMaxEmp = ((decimal)Global.Usuario.tbEmpresa.tbParametrosEmpresa.First().descuentoBase) / 100;
            bool aprobDescEmp = (bool)Global.Usuario.tbEmpresa.tbParametrosEmpresa.First().aprobacionDescuento;
            bool validaDesc = true;
            desc = desc / 100;


            //verifica si el descuento es mayoral porcentaje permitido en la empresa, de lo contrario que aplique
            if (desc > descMaxEmp)
            {//se debe de solicitar aprobacion en caso que la empresa lo requiera, segun el parametro de la empresa
                if (aprobDescEmp)
                {
                    if (!respuestaAprobaDesc)
                    {

                        //form para aprobacion de un administrador true aprueba, false no aprueba
                        aprobar();
                        porcDesc = decimal.Parse(txtPorcetaje.Text.Trim());
                        return respuestaAprobaDesc;
                    }
                    else
                    {
                        return true;
                    }
                }
                else
                {

                    return true;

                }
            }
            return true;
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

 

        private void dtgvDetalleFactura_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            decimal cantidad = 0;
            string codigoProducto = string.Empty;
            decimal precioProd = 0;

            try
            {
                
                if (e.RowIndex != -1)
                {
                    //cuando cambia la cantidad
                    if (dtgvDetalleFactura.Columns[e.ColumnIndex].Name == "colCant")
                    {
                        if (dtgvDetalleFactura.Rows[e.RowIndex].Cells[5].Value != null)
                        {
                            tbProducto prod;
                            codigoProducto = dtgvDetalleFactura.Rows[e.RowIndex].Cells[5].Value.ToString();

                            if (dtgvDetalleFactura.Rows[e.RowIndex].Cells[2].Value != null)
                            {

                                cantidad = decimal.Parse(dtgvDetalleFactura.Rows[e.RowIndex].Cells[2].Value.ToString());

                                prod = buscarProducto(codigoProducto);
                                if (prod != null)
                                {
                                    agregarProductoDetalleFactura(prod, 1, cantidad,0, false, true);
                                }
                            }

                        }
                    }
                    //cuando cambia el precio
                    if (dtgvDetalleFactura.Columns[e.ColumnIndex].Name == "colPrec")
                    {
                        if (dtgvDetalleFactura.Rows[e.RowIndex].Cells[5].Value != null)
                        {
                            tbProducto prod;
                            codigoProducto = dtgvDetalleFactura.Rows[e.RowIndex].Cells[5].Value.ToString();

                            if (dtgvDetalleFactura.Rows[e.RowIndex].Cells[1].Value != null)
                            {

                                precioProd = decimal.Parse(dtgvDetalleFactura.Rows[e.RowIndex].Cells[1].Value.ToString());

                                prod = buscarProducto(codigoProducto);
                                if (prod != null)
                                {
                                    bool precVar = (bool)Global.Usuario.tbEmpresa.tbParametrosEmpresa.FirstOrDefault().precioVariable;

                                    if (precVar)
                                    {
                                        if ((bool)prod.precioVariable)
                                        {//el valor de 2 es para actualizar el precio y 1 para cantidad

                                            agregarProductoDetalleFactura(prod, 2, precioProd, 0, false, true);
                                        }
                                        else
                                        {
                                            agregarProductoGrid();
                                            MessageBox.Show("El precio de este producto no puede ser actualizado", "Precio Producto", MessageBoxButtons.OK, MessageBoxIcon.Error);

                                        }
                                    }
                                    else
                                    {
                                        agregarProductoGrid();
                                        MessageBox.Show("El precio de este producto no puede ser actualizado", "Precio Producto", MessageBoxButtons.OK, MessageBoxIcon.Error);

                                    }

                                }
                            }

                        }

                    }
                    if (dtgvDetalleFactura.Columns[e.ColumnIndex].Name == "colDesc")
                    {
                        if (dtgvDetalleFactura.Rows[e.RowIndex].Cells[1].Value != null)
                        {
                            tbProducto prod;
                            codigoProducto = dtgvDetalleFactura.Rows[e.RowIndex].Cells[1].Value.ToString();

                            if (dtgvDetalleFactura.Rows[e.RowIndex].Cells[5].Value != null)
                            {

                                if (Utility.isNumeroDecimal(dtgvDetalleFactura.Rows[e.RowIndex].Cells[5].Value.ToString(), false))
                                {

                                    if (listaDetalleDocumento.Where(x => x.idProducto.Trim() == codigoProducto).SingleOrDefault() != null)
                                    {
                                        if (aprobarDescuento(decimal.Parse(dtgvDetalleFactura.Rows[e.RowIndex].Cells[5].Value.ToString())))
                                        {
                                            listaDetalleDocumento.Where(x => x.idProducto.Trim() == codigoProducto).SingleOrDefault().descuento =
                                                decimal.Parse(dtgvDetalleFactura.Rows[e.RowIndex].Cells[5].Value.ToString());

                                            calcularMontosT();
                                            agregarProductoGrid();
                                        }

                                    }
                                }
                            }

                        }

                    }
                    if (dtgvDetalleFactura.Columns[e.ColumnIndex].Name == "colDesc")
                    {
                        if (dtgvDetalleFactura.Rows[e.RowIndex].Cells[1].Value != null)
                        {
                            tbProducto prod;
                            codigoProducto = dtgvDetalleFactura.Rows[e.RowIndex].Cells[1].Value.ToString();

                            if (dtgvDetalleFactura.Rows[e.RowIndex].Cells[5].Value != null)
                            {

                                if (Utility.isNumeroDecimal(dtgvDetalleFactura.Rows[e.RowIndex].Cells[5].Value.ToString(), false))
                                {

                                    if (listaDetalleDocumento.Where(x => x.idProducto.Trim() == codigoProducto).SingleOrDefault() != null)
                                    {
                                        if (aprobarDescuento(decimal.Parse(dtgvDetalleFactura.Rows[e.RowIndex].Cells[5].Value.ToString())))
                                        {
                                            listaDetalleDocumento.Where(x => x.idProducto.Trim() == codigoProducto).SingleOrDefault().descuento =
                                                decimal.Parse(dtgvDetalleFactura.Rows[e.RowIndex].Cells[5].Value.ToString());

                                            calcularMontosT();
                                            agregarProductoGrid();
                                        }

                                    }
                                }
                            }

                        }

                    }
                }

            }
            catch (Exception)
            {

                MessageBox.Show("Se produjo un error al cambiar los datos del producto, corrija los datos", "Ingreso de datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCobrar_Click(object sender, EventArgs e)
        {
            realizarCobro();
        }

        private void realizarCobro()
        {
            try
            {
                tipoDoc = (int)Enums.TipoDocumento.FacturaElectronica;
                dtgvDetalleFactura.EndEdit();
                //calcularMontosT();
                agregarProductoGrid();
                if (listaDetalleDocumento.Count != 0 && txtTotal.Text != "0")
                {
                    if (validarCampos())
                    {

                        tbDocumento documento = crearDocumento();
                        frmCobrar form = new frmCobrar();
                        //en caso de poner cliente sin que se encuentre registrado en al base datos y es tiquete electronico, s
                        //solo para imprimir

                        form.recuperarTotal += respuesta;
                        form.facturaGlobal = documento;
                        form.ShowDialog();
                    }
                }
                else
                {
                    MessageBox.Show("No hay productos o el TOTAL a cobrar es 0.", "Cobrar", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception)
            {

                MessageBox.Show("Error al realizar el cobro", "Cobrar", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool validarCampos()
        {
            try
            {
                bool validaCliente = (bool)Global.Usuario.tbEmpresa.tbParametrosEmpresa.First().clienteObligatorioFact;

                if (validaCliente || tipoDoc == (int)Enums.TipoDocumento.Proforma)
                {
                    if (clienteGlo == null)
                    {
                        MessageBox.Show("Debe indicar un cliente", "Faltan datos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        btnBuscarCliente.Focus();
                        return false;
                    }

                }
                if (chkEnviar.Checked)
                {
                    if (txtCorreo.Text == string.Empty)
                    {
                        MessageBox.Show("Debe Ingresar un correo electrónico", "Faltan datos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtCorreo.Focus();
                        return false;

                    }
                    if (!Utility.isValidEmail(txtCorreo.Text))
                    {
                        MessageBox.Show("El formato del correo es incorrecto", "Faltan datos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtCorreo.Focus();
                        return false;
                    }
                    if (txtCorreo2.Text != string.Empty)
                    {
                        if (!Utility.isValidEmail(txtCorreo2.Text))
                        {
                            MessageBox.Show("El formato del correo es incorrecto", "Faltan datos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            txtCorreo2.Focus();
                            return false;
                        }

                    }
                }

                if (Global.Usuario.tbEmpresa.regimenSimplificado && (int)Global.Usuario.tbEmpresa.tipoFacturacionRegimen == (int)Enums.TipoFacturacionElectRegimenSimplificado.SoloFacturacionConCliente)
                {
                    if (chkFacturaElectronica.Checked)
                    {
                        if (clienteGlo == null)
                        {
                            MessageBox.Show("Debe indicar un cliente", "Faltan datos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            btnBuscarCliente.Focus();
                            return false;
                        }

                    }


                }
                return true;
            }
            catch (Exception)
            {

                MessageBox.Show("Error al validar los campos", "Validar", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return false;
        }

        private tbDocumento crearDocumento()
        {


            tbDocumento documento = new tbDocumento();
            documento.tipoDocumento = (int)Enums.TipoDocumento.FacturaElectronica;
            documento.fecha = Utility.getDate();


            documento.tipoMoneda = (int)Enums.TipoMoneda.CRC;
            documento.tipoCambio = 0;


            documento.reporteElectronic = (bool)Global.Usuario.tbEmpresa.tbParametrosEmpresa.First().facturacionElectronica;
            documento.tipoVenta = (int)Enums.tipoVenta.Contado;
            documento.plazo = 0;
            documento.tipoPago = (int)Enums.TipoPago.Efectivo;
            documento.refPago = string.Empty;
            documento.estadoFactura = (int)Enums.EstadoFactura.Cancelada;
            documento.reporteAceptaHacienda = false;
            documento.notificarCorreo = chkEnviar.Checked;
            documento.estadoCorreo = (int)Enums.EstadoCorreo.SinEnviar;
            documento.observaciones = txtObservaciones.Text.ToUpper().Trim();
            documento.idEmpresa = Global.Usuario.tbEmpresa.id;
            documento.tipoIdEmpresa = Global.Usuario.tbEmpresa.tipoId;


            documento.codigoActividad = Global.actividadEconomic.CodActividad;

            documento.sucursal = Global.Configuracion.sucursal;
            documento.caja = Global.Configuracion.caja;

            //si no marco el check de enviar correo, deja los campos de correo electronico a notificar null
            if ((bool)documento.notificarCorreo)
            {
                documento.correo1 = txtCorreo.Text == string.Empty ? null : txtCorreo.Text.Trim();
                documento.correo2 = txtCorreo2.Text == string.Empty ? null : txtCorreo2.Text.Trim();

            }

            //cliente
            if (clienteGlo != null)
            {
                documento.idCliente = clienteGlo.id;
                documento.tipoIdCliente = clienteGlo.tipoId;
                documento.tbClientes = clienteGlo;

            }

            if (documento.reporteElectronic)
            {
                if ((bool)Global.Usuario.tbEmpresa.regimenSimplificado)
                {

                    if (Global.Usuario.tbEmpresa.tipoFacturacionRegimen == (int)Enums.TipoFacturacionElectRegimenSimplificado.SoloFacturacionConCliente)
                    {
                        if (chkFacturaElectronica.Checked)
                        {
                            if (clienteGlo != null)
                            {
                                documento.tipoDocumento = (int)Enums.TipoDocumento.FacturaElectronica;
                                if (chkTiqueteElectronico.Checked)
                                {
                                    documento.tipoDocumento = (int)Enums.TipoDocumento.TiqueteElectronico;
                                }
                            }
                            else
                            {
                                documento.tipoDocumento = (int)Enums.TipoDocumento.Factura;
                                documento.reporteElectronic = false;
                            }


                        }
                        else
                        {
                            documento.tipoDocumento = (int)Enums.TipoDocumento.Factura;
                            documento.reporteElectronic = false;
                        }




                    }
                    else if (Global.Usuario.tbEmpresa.tipoFacturacionRegimen == (int)Enums.TipoFacturacionElectRegimenSimplificado.Todo)
                    {
                        if (clienteGlo != null)
                        {
                            documento.tipoDocumento = (int)Enums.TipoDocumento.FacturaElectronica;
                            if (chkTiqueteElectronico.Checked)
                            {
                                documento.tipoDocumento = (int)Enums.TipoDocumento.TiqueteElectronico;
                            }
                        }
                        else
                        {
                            documento.tipoDocumento = (int)Enums.TipoDocumento.TiqueteElectronico;
                        }
                    }
                }
                else
                {
                    if (clienteGlo != null)
                    {
                        documento.tipoDocumento = (int)Enums.TipoDocumento.FacturaElectronica;
                        if (chkTiqueteElectronico.Checked)
                        {
                            documento.tipoDocumento = (int)Enums.TipoDocumento.TiqueteElectronico;
                        }
                    }
                    else
                    {
                        documento.tipoDocumento = (int)Enums.TipoDocumento.TiqueteElectronico;

                    }
                }
            }
            else
            {
                documento.tipoDocumento = (int)Enums.TipoDocumento.Factura;
                documento.reporteElectronic = false;

            }

            documento.estado = true;




            if (documento.idCliente == null && txtCliente.Text != string.Empty)
            {
                documento.clienteNombre = txtCliente.Text.ToUpper().Trim();
            }


            documento.tbDetalleDocumento = listaDetalleDocumento;

            //Atributos de Auditoria

            documento.fecha_crea = Utility.getDate();
            documento.fecha_ult_mod = Utility.getDate();
            documento.usuario_crea = Global.Usuario.nombreUsuario.Trim().ToUpper();   // Global.Usuario.nombreUsuario;
            documento.usuario_ult_mod = Global.Usuario.nombreUsuario.Trim().ToUpper();   // Global.Usuario.nombreUsuario;

            return documento;

            //}

        }

        private void respuesta(tbDocumento doc, bool precio)
        {
            try
            {

                if (doc != null)
                {
                    existeRespuesta = true;
                    if ((doc.tipoDocumento != (int)Enums.TipoDocumento.Proforma && doc.tipoDocumento != (int)Enums.TipoDocumento.ProformaGeneral))
                    {
                        if (isAlias)
                        {
                            var pend = pendientesIns.GetEntityByAlias(lblAlias.Text.Trim());
                            pendientesIns.removeByAlias(pend.alias);
                            cargarPendientes();
                        }
                        else
                        {
                            if ((bool)Global.Usuario.tbEmpresa.tbParametrosEmpresa.SingleOrDefault().comandas && chkComandas.Checked && ((int)Global.Usuario.tbEmpresa.tbParametrosEmpresa.SingleOrDefault().comandasTipo == (int)Enums.TipoComanda.Facturar || (int)Global.Usuario.tbEmpresa.tbParametrosEmpresa.SingleOrDefault().comandasTipo == (int)Enums.TipoComanda.FacturarPendientes))
                            {

                                if (Global.Configuracion.imprimeExtra == (int)Enums.Estado.Activo && (bool)Global.Usuario.tbEmpresa.tbParametrosEmpresa.SingleOrDefault().comandas)
                                {
                                    List<clsComanda> listaComandas = new List<clsComanda>();


                                    foreach (var item in doc.tbDetalleDocumento.Where(x => x.tbProducto.cocina == true).ToList())
                                    {
                                        for (int i = 0; i < item.cantidad; i++)
                                        {
                                            clsComanda comanda = new clsComanda();
                                            comanda.alias = doc.id.ToString();
                                            comanda.cant = 1;
                                            comanda.codigoProducto = item.idProducto;
                                            comanda.nombre = item.tbProducto.nombre;
                                            comanda.estado = true;
                                            comanda.numero = i + 1;


                                            listaComandas.Add(comanda);



                                        }

                                    }

                                    enviarComandas(listaComandas);



                                }
                            }


                        }

                    }



                    if (Utility.AccesoInternet())
                    {

                        BackgroundWorker tarea = new BackgroundWorker();
                        documentoGlo = doc;
                        tarea.DoWork += reportarFacturacionElectronica;
                        tarea.RunWorkerAsync();

                    }
                    else
                    {
                        MessageBox.Show("No hay acceso a internet", "Sin Internet", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }



                    if (doc.id != 0)
                    {
                        MessageBox.Show("El documento ha sido emitido correctamente", "Documento creado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        
                    }

                    limpiarFactura();
                }
                else
                {
                    MessageBox.Show("El documento no se ha guardado, intente nuevamente, de lo contrario contacte con el administrador.", "Error al crear Documento", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }

            catch (Exception ex)
            {

                MessageBox.Show("Error en el sistema, contacte al administrador", "Error general", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        public async void reportarFacturacionElectronica(object o, DoWorkEventArgs e)
        {
            tbDocumento doc = documentoGlo;
            try
            {
                if (doc.reporteElectronic)
                {
                    try
                    {
                        doc = facturaIns.FacturarElectronicamente(doc);
                        System.Threading.Thread.Sleep(5000);
                        string mensaje = await facturaIns.consultarFacturaElectronicaPorClave(doc.clave);

                    }
                    catch (Exception)
                    {

                        MessageBox.Show("Error al enviar documento o consultar en Hacienda", "Error Hacienda", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }

                }
                //envio la factura a hacienda

                try
                {
                    facturaIns.validarDocumentosDiarias();

                }
                catch (Exception)
                {

                    MessageBox.Show("Error al consultar el estado del documento en Hacienda, valida el estado del documento", "Error al consultar el estado del documento", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }

                try
                {
                    enviarCorreos();

                }
                catch (Exception)
                {

                   // MessageBox.Show("Error enviando correos automaticamente", "Envio Correos", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }

            }
            catch (FacturacionElectronicaException ex)
            {
                MessageBox.Show("Error al realizar la facturación electronica", "Factura Electrónica", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (EnvioCorreoException ex)
            {
                MessageBox.Show("Error al enviar la facturación por correo electrónico", "Correo electrónico", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (TokenException ex)
            {
                MessageBox.Show("Error al obtener el Token en Hacienda", "Facturación electrónica", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (ConsultaHaciendaExcpetion ex)
            {
                MessageBox.Show("Error al consultar hacienda la factura electrónica", "Facturación electrónica", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (generarXMLException ex)
            {
                MessageBox.Show("Error al generar el XML de la factura", "Facturación electrónica", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception EX)
            {
                MessageBox.Show("Error general de facturación electrónica", "Facturación electrónica", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void enviarCorreos()
        {
            var lista = facturaIns.docsCorreoPendietes();
            if (lista.Count() != 0)
            {
                bool bandera = false;

                foreach (var documento in lista)
                {
                    bandera = false;
                    if (documento.tipoDocumento == (int)Enums.TipoDocumento.Proforma || documento.tipoDocumento == (int)Enums.TipoDocumento.ProformaGeneral ||
                        documento.tipoDocumento == (int)Enums.TipoDocumento.Factura || documento.tipoDocumento == (int)Enums.TipoDocumento.NotaCredito)
                    {

                        bandera = true;
                    }
                    else if (documento.tipoDocumento == (int)Enums.TipoDocumento.FacturaElectronica || documento.tipoDocumento == (int)Enums.TipoDocumento.TiqueteElectronico
                        || documento.tipoDocumento == (int)Enums.TipoDocumento.NotaCreditoElectronica || documento.tipoDocumento == (int)Enums.TipoDocumento.NotaDebitoElectronica)
                    {
                        if (documento.EstadoFacturaHacienda != null)
                        {
                            if (documento.EstadoFacturaHacienda.Trim().ToUpper().Equals("ACEPTADO"))
                            {

                                bandera = true;
                            }

                        }
                    }

                    if (bandera)
                    {

                        List<string> correos = new List<string>();
                        if (documento.correo1 != null)
                        {
                            correos.Add(documento.correo1.Trim());

                        }
                        if (documento.correo2 != null)
                        {
                            correos.Add(documento.correo2.Trim());

                        }
                        try
                        {
                            clsDocumentoCorreo docCorreo = new clsDocumentoCorreo(documento, correos, true, (int)Enums.tipoAdjunto.factura);
                            bandera = CorreoElectronico.enviarCorreo(docCorreo);
                            if (bandera)
                            {

                                var doc = facturaIns.getEntity(documento);
                                doc.estadoCorreo = (int)Enums.EstadoCorreo.Enviado;
                                facturaIns.modificar(doc);


                            }
                        }
                     
                        catch (Exception ex)
                        {

                            
                        }


                    }


                }
            }

        }

        private void limpiarFactura()
        {

            if (chkFacturaElectronica.Visible)
            {
                chkFacturaElectronica.Checked = false;
            }
            
            dtgvDetalleFactura.Rows.Clear();
            listaDetalleDocumento.Clear();
            listaProductosPromo.Clear();
            txtCodigo.Text = string.Empty;
            clienteGlo = null;
            exoneracionClie = false;
            existeRespuesta = false;
            isProforma = false;

            txtCliente.Enabled = true;

            chkEnviar.Checked = false;
            txtCorreo2.Text = string.Empty;



            txtIdCliente.Text = string.Empty;
            txtCliente.Text = string.Empty;
            txtTel.Text = string.Empty;
            txtCorreo.Text = string.Empty;
            txtCorreo2.Text = string.Empty;

            txtSubtotal.Text = "0";
            txtIva.Text = "0";
            txtPorcetaje.Text = "0";
            txtDescuento.Text = "0";
            txtTotal.Text = "0";
            lblTotalProducto.Text = "0";
            txtExoneracion.Text = "0";
            txtSub.Text = "0";
            txtServicioMesa.Text = "0";
            lblCantidadLineas.Text = "0";


            txtObservaciones.Text = string.Empty;

            respuestaAprobaDesc = false;
            porcDesc = 0;
            peso = decimal.MinValue;
            alias = string.Empty;
            isAlias = false;
            lblAlias.Text = string.Empty;

            lblPendientes.Text = pendientesIns.CantidadPendientes().ToString();
            chkServicioMesa.Checked = false;

            chkTiqueteElectronico.Enabled = false;
            chkTiqueteElectronico.Checked = true;

            txtCodigo.Select();
        }

        private void btnLimpiarForm_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Desea limpiar por completo lo facturado?", "Confirmacion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                if ((bool)Global.Usuario.tbEmpresa.tbParametrosEmpresa.FirstOrDefault().aprobarEliminar)
                {
                    aprobar();
                    if (respuestaAprobaDesc)
                    {
                        limpiarFactura();

                    }

                }
                else
                {
                    limpiarFactura();
                }



            }

            respuestaAprobaDesc = false;
        }

        private void btnReImprimir_Click(object sender, EventArgs e)
        {
            try
            {
                tbDocumento doc = facturaIns.getLastEntity(Global.Usuario.nombreUsuario.Trim().ToUpper());
                if (doc != null)
                {

                    DialogResult dialogResult = MessageBox.Show("Desea imprimir el documento?", "Imprimir", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dialogResult == DialogResult.Yes)
                    {

                        clsImpresionFactura imprimir = new clsImpresionFactura(doc, Global.Usuario.tbEmpresa);
                        imprimir.print();

                    }
                }
                else
                {
                    MessageBox.Show("No hay documentos que imprimir", "Imprimir", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
            catch (Exception)
            {

                MessageBox.Show("Error al imprimir el último documento.", "Re-Imprimir", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBuscarFactura_Click(object sender, EventArgs e)
        {
            frmBuscarDocumentos form = new frmBuscarDocumentos();
            form.tipoBusquedaDevolver = false;
            form.ShowDialog();
        }

        private void btnProductos_Click(object sender, EventArgs e)
        {
            frmProductos2 form = new frmProductos2();
            form.ShowDialog();
            cargarDatos();
        }

        private void btnClientes_Click(object sender, EventArgs e)
        {
            frmClientes form = new frmClientes();
            form.ShowDialog();
        }

        private void btnValidacion_Click(object sender, EventArgs e)
        {
            frmValidacionDocsHacienda form = new frmValidacionDocsHacienda();
            form.ShowDialog();
        }

        private void txtPorcetaje_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((int)e.KeyChar == (int)Keys.Enter)
            {

                try
                {
                    if (txtPorcetaje.Text == string.Empty)
                    {


                        txtDescuento.Text = "0";
                    }

                    if (aprobarDescuento(decimal.Parse(txtPorcetaje.Text)))
                    {
                        foreach (tbDetalleDocumento det in listaDetalleDocumento)
                        {
                            det.descuento = decimal.Parse(txtPorcetaje.Text);
                        }
                    }
                    else
                    {

                        txtPorcetaje.Text = "0.0";

                    }




                    calcularMontosT();
                    agregarProductoGrid();
                }
                catch (Exception)
                {


                    MessageBox.Show("Se produjo un error al ingresar el producto, corrija los datos", "Calcular descuento", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void btnPendiente_Click(object sender, EventArgs e)
        {
            pendiente();
            

        }

        private void pendiente()
        {
            try
            {

                agregarPendiente();
                limpiarFactura();
                cargarPendientes();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error al agreagar o actualizar pendientes", "Pendientes", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private tbDocumentosPendiente obtenerPendientes(List<tbDetalleDocumento> listaDetalleDocumento)
        {
            tbDocumentosPendiente pendiente = new tbDocumentosPendiente();

            foreach (var detalle in listaDetalleDocumento)
            {
                tbDetalleDocumentoPendiente detalleP = new tbDetalleDocumentoPendiente();

                detalleP.alias = alias;
                detalleP.idProducto = detalle.idProducto;
                detalleP.cantidad = detalle.cantidad;
                detalleP.descuento = detalle.descuento;
                detalleP.montoTotal = detalle.montoTotal;
                detalleP.montoTotalDesc = detalle.montoTotalDesc;
                detalleP.montoTotalExo = detalle.montoTotalExo;
                detalleP.montoTotalImp = detalle.montoTotalImp;
                detalleP.precioCosto = detalle.precioCosto;
                detalleP.porcImp = detalle.porcImp;
                detalleP.porcExo = detalle.porcExo;
                detalleP.numLinea = detalle.numLinea;
                detalleP.precio = detalle.precio;
                //detalleP.tbProducto = detalle.tbProducto;
                detalleP.totalLinea = detalle.totalLinea;
                pendiente.tbDetalleDocumentoPendiente.Add(detalleP);


            }
            pendiente.alias = alias;
            pendiente.servicioMesa = chkServicioMesa.Checked;

            return pendiente;
        }
        private void agregarPendiente()
        {
            if (listaDetalleDocumento.Count != 0 && txtTotal.Text != "0")
            {
                bool exist = isAlias && lblAlias.Text != string.Empty;
                isAlias = true;
                //VERIFICAR SI EXISTE EL ALIAS EN LA BD
                if (!exist)
                {
                    do
                    {
                        frmAliasMesa aliasForm = new frmAliasMesa();
                        aliasForm.pasarDatosEvent += aliasPasarDatos;
                        aliasForm.ShowDialog();
                        exist = pendientesIns.existAlias(alias);
                        if (exist)
                        {
                            MessageBox.Show("Ya existe ese ALIAS/MESA, ingreso uno diferente", "ALIAS/MESA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                    } while (exist);
                    lblAlias.Text = alias;
                }
                else
                {
                    alias = lblAlias.Text;

                }

                tbDocumentosPendiente pendieteAntiguo = pendientesIns.GetEntityByAlias(alias);

                tbDocumentosPendiente pendiente = obtenerPendientes(listaDetalleDocumento);


                pendientesIns.Guardar(pendiente);


                if ((bool)Global.Usuario.tbEmpresa.tbParametrosEmpresa.SingleOrDefault().comandas && chkComandas.Checked && ((int)Global.Usuario.tbEmpresa.tbParametrosEmpresa.SingleOrDefault().comandasTipo == (int)Enums.TipoComanda.Pendientes || (int)Global.Usuario.tbEmpresa.tbParametrosEmpresa.SingleOrDefault().comandasTipo == (int)Enums.TipoComanda.FacturarPendientes))
                {
                    enviarComandas(pendieteAntiguo);

                }

                MessageBox.Show("Pendiente actualizado", "Pendiente", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("No hay productos o el TOTAL a cobrar es 0.", "Pendiente", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void enviarComandas(tbDocumentosPendiente pendieteAntiguo)
        {
            if (Global.Configuracion.imprimeExtra == (int)Enums.Estado.Activo && (bool)Global.Usuario.tbEmpresa.tbParametrosEmpresa.SingleOrDefault().comandas)
            {
                tbDocumentosPendiente pendieteNuevo = pendientesIns.GetEntityByAlias(alias);

                List<clsComanda> listaComandas = new List<clsComanda>();

                foreach (var item in pendieteNuevo.tbDetalleDocumentoPendiente.Where(x => x.tbProducto.cocina == true).ToList())
                {
                    var pend = pendieteAntiguo == null ? null : pendieteAntiguo.tbDetalleDocumentoPendiente.Where(x => x.idProducto.Trim() == item.idProducto.Trim() && x.tbProducto.cocina == true).FirstOrDefault();

                    if (pend == null)
                    {
                        for (int i = 0; i < item.cantidad; i++)
                        {
                            clsComanda comanda = new clsComanda();
                            comanda.alias = item.alias;
                            comanda.cant = 1;
                            comanda.codigoProducto = item.idProducto;
                            comanda.nombre = item.tbProducto.nombre;
                            comanda.estado = true;
                            listaComandas.Add(comanda);
                        }

                    }
                    else if (item.cantidad != pend.cantidad)
                    {
                        var cantidad = item.cantidad - pend.cantidad;
                        cantidad = Math.Abs(cantidad);

                        for (int i = 0; i < cantidad; i++)
                        {
                            clsComanda comanda = new clsComanda();
                            comanda.alias = item.alias;
                            comanda.cant = cantidad;
                            comanda.codigoProducto = item.idProducto;
                            comanda.nombre = item.tbProducto.nombre;
                            comanda.estado = (item.cantidad - pend.cantidad) > 0 ? true : false;
                            listaComandas.Add(comanda);
                        }

                    }

                }


                if (pendieteAntiguo != null)
                {
                    foreach (var item in pendieteAntiguo.tbDetalleDocumentoPendiente.Where(x => x.tbProducto.cocina == true).ToList())
                    {

                        var pend = pendieteNuevo.tbDetalleDocumentoPendiente.Where(x => x.idProducto.Trim() == item.idProducto.Trim() && x.tbProducto.cocina == true).FirstOrDefault();

                        if (pend == null)
                        {
                            for (int i = 0; i < item.cantidad; i++)
                            {
                                clsComanda comanda = new clsComanda();
                                comanda.alias = item.alias;
                                comanda.cant = item.cantidad;
                                comanda.codigoProducto = item.idProducto;
                                comanda.nombre = item.tbProducto.nombre;
                                comanda.estado = false;
                                listaComandas.Add(comanda);
                            }


                        }
                    }

                }
                enviarComandas(listaComandas);
            }
        }

        private void enviarComandas(List<clsComanda> listaComandas)
        {
            for (int i = 0; i < listaComandas.Count; i++)
            {
                listaComandas[i].numero = i + 1;

            }
            if (listaComandas.Count > 0)
            {
                //DialogResult result = MessageBox.Show("¿Desea agregar comentarios a la orden?", "Comentarios", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                //if (result == DialogResult.Yes)
                //{
                frmComentarioComandas2 frmComentarios = new frmComentarioComandas2(listaComandas);
                frmComentarios.pasarDatosEvent += pasarComanda;
                frmComentarios.ShowDialog();
                //}
                //else
                //{
                //    imprimirComandas(listaComandas);

                //}
            }
        }

        private void pasarComanda(List<clsComanda> lista)
        {
            listaComandas = lista;
            if (listaComandas != null && listaComandas.Count > 0)
            {
                imprimirComandas(listaComandas);
                listaComandas.Clear();
            }
        }

        private void cargarAliasMesa(object sender, EventArgs e)
        {
            try
            {
                limpiarFactura();
                string alias = ((Button)sender).Name.Trim().ToUpper();
                tbDocumentosPendiente pendiente = pendientesIns.GetEntityByAlias(alias);
                lblAlias.Text = pendiente.alias;


                if (pendiente != null)
                {

                    foreach (var detalle in pendiente.tbDetalleDocumentoPendiente)
                    {
                        tbDetalleDocumento detalleP = new tbDetalleDocumento();
                        detalleP.idProducto = detalle.idProducto;
                        detalleP.cantidad = detalle.cantidad;
                        detalleP.descuento = detalle.descuento;
                        detalleP.montoTotal = detalle.montoTotal;
                        detalleP.montoTotalDesc = detalle.montoTotalDesc;
                        detalleP.montoTotalExo = detalle.montoTotalExo;
                        detalleP.montoTotalImp = detalle.montoTotalImp;
                        detalleP.porcExo = detalle.porcExo;
                        detalleP.porcImp = detalle.porcImp;
                        detalleP.precioCosto = detalle.precioCosto;
                        detalleP.numLinea = detalle.numLinea;
                        detalleP.precio = detalle.precio;
                        detalleP.tbProducto = detalle.tbProducto;
                        detalleP.totalLinea = detalle.totalLinea;

                        listaDetalleDocumento.Add(detalleP);

                    }
                    chkServicioMesa.Checked = pendiente.servicioMesa;
                    calcularMontosT();
                    agregarProductoGrid();
                    tabFacturacion.SelectedTab = tabFacturacion.TabPages[0];
                    isAlias = true;

                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error al cargar el pendiente", "Pendiente", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }

        private void imprimirComandas(List<clsComanda> lista)
        {
            if (lista.Count > 0)
            {
                clsImpresionFactura imprimir = new clsImpresionFactura(lista);
                for (int y = 0; y < Global.Usuario.tbEmpresa.tbParametrosEmpresa.First().cantComandas; y++)
                {
                    imprimir.printComandas();
                }
            }
        }
          
        private void cargarPendientes()
        {
            Button btn;
            int y = 0;
            int x = 0;
            gbxPendientes.Controls.Clear();
            foreach (var pend in pendientesIns.GetListEntities())
            {
                btn = new Button();
                btn.Name = pend.alias;
                btn.Text = pend.alias.Trim();
                btn.Location = new System.Drawing.Point(100 * x, 100 * y);
                btn.Size = new System.Drawing.Size(100, 100);
                btn.BackColor = Color.DarkOrange;
                btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                btn.ForeColor = Color.White;
                btn.TextAlign = ContentAlignment.MiddleCenter;

                btn.Click += new System.EventHandler(cargarAliasMesa);

                gbxPendientes.Controls.Add(btn);
                x++;

                if (x == 10)
                {
                    x = 0;
                    y++;
                }

            }
        }

        private void aliasPasarDatos(string alias)
        {
            if (alias != null)
            {
                this.alias = alias;
            }
            else
            {
                alias = string.Empty;
            }
        }

        private void btnDividir_Click(object sender, EventArgs e)
        {
            bool activarComandas = false;
            if (listaDetalleDocumento.Count != 0 && txtTotal.Text != "0")
            {

                frmDividirCuenta form = new frmDividirCuenta();
                form.pasarDatosPendientes += pasarDatosPendientes;
                form.documentoTotal = crearDocumento();
                form.ShowDialog();
            }
            else
            {
                MessageBox.Show("No hay productos o el TOTAL a cobrar es 0.", "Pendiente", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void pasarDatosPendientes(tbDocumento documento)
        {
            if (documento == null)
            {
                if (lblAlias.Text != string.Empty && isAlias)
                {
                    var fact = pendientesIns.GetEntityByAlias(lblAlias.Text.Trim());
                    pendientesIns.removeByAlias(fact.alias);
                    cargarPendientes();
                }
                limpiarFactura();
                MessageBox.Show("Se realizó el cobro total de la factura", "Cobro", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                listaDetalleDocumento = (List<tbDetalleDocumento>)documento.tbDetalleDocumento;
                //actualizar si esta pendiente
                chkServicioMesa.Checked = listaDetalleDocumento.Where(x => x.idProducto == "SM").SingleOrDefault() != null;

                agregarPendiente();
                cargarPendientes();

                calcularMontosT();
                agregarProductoGrid();

            }
        }

        private void btnProforma_Click(object sender, EventArgs e)
        {
            tipoDoc = (int)Enums.TipoDocumento.Proforma;
            dtgvDetalleFactura.EndEdit();
            calcularMontosT();
            agregarProductoGrid();
            if (listaDetalleDocumento.Count != 0 && txtTotal.Text != "0")
            {
                if (validarCampos())
                {

                    tbDocumento documento = crearDocumento();
                    documento.tipoDocumento = tipoDoc;
                    documento.reporteElectronic = false;
                    if (txtCorreo.Text != string.Empty)
                    {
                        documento.correo1 = txtCorreo.Text.Trim();
                    }

                    if (txtCorreo2.Text != string.Empty)
                    {
                        documento.correo2 = txtCorreo2.Text.Trim();
                    }
                    documento.notificarCorreo = false;
                    frmProforma form = new frmProforma();
                    form.recuperarTotal += respuesta;
                    form.facturaGlobal = documento;
                    form.ShowDialog();
                }
            }
            else
            {
                MessageBox.Show("No hay productos o el TOTAL a cobrar es 0.", "Cobrar", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBuscarProforma_Click(object sender, EventArgs e)
        {
            frmBusquedaProforma form = new frmBusquedaProforma();
            form.pasarDatosEvent += datosProforma;
            form.ShowDialog();
        }
        private void datosProforma(tbDocumento entity)
        {

            List<string> errores = new List<string>();
            documentoGlo = entity;
            string mensaje = "";


            if (listaDetalleDocumento == null)
            {
                listaDetalleDocumento = new List<tbDetalleDocumento>();
            }
            isProforma = true;
            listaDetalleDocumento.Clear();
            // listaDetalleDocumento=(List<tbDetalleDocumento>)entity.tbDetalleDocumento;

            foreach (var item in entity.tbDetalleDocumento)
            {

                if (item.tbProducto.codigoCabys == null || item.tbProducto.codigoCabys == string.Empty)
                {
                    mensaje = string.Format("Producto: {0}-{1}.", item.idProducto.Trim(), item.tbProducto.nombre.Trim().ToUpper());
                    errores.Add(mensaje);
                }
                else
                {
                    item.tbDocumento = null;
                    listaDetalleDocumento.Add(item);
                }

            }


            if (entity.tbClientes != null)
            {
                dataBuscar(entity.tbClientes);
            }


            if (errores.Count > 0)
            {
                string mensajeCabys = "Sin código Cabys: ";
                foreach (var err in errores)
                {
                    mensajeCabys += string.Format("{0}{1}", Environment.NewLine, err);
                }


                MessageBox.Show(mensajeCabys, "Productos EXCLUIDOS por falta de código CABYS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (listaDetalleDocumento.Count > 0)
            {
                calcularMontosT();
                agregarProductoGrid();
            }
            else
            {
                MessageBox.Show("Proforma sin productos válidos.", "Proformas", MessageBoxButtons.OK, MessageBoxIcon.Error);
                limpiarFactura();
            }
        }

        private void chkServicioMesa_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                calcularMontosT();
                agregarProductoGrid();
            }
            catch (Exception)
            {

                MessageBox.Show("Error al agregar el servicio de mesa a la facturación", "Facturación", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEliminarPendientes_Click(object sender, EventArgs e)
        {

            try
            {
                DialogResult result = MessageBox.Show("¿Desea eliminar TODOS los pendientes?", "Pendientes", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    pendientesIns.removeAll();
                    MessageBox.Show("Se ha eliminado los pendientes", "Pendientes", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    cargarPendientes();
                    lblPendientes.Text = "0";
                }
            }
            catch (Exception)
            {

                MessageBox.Show("Error al eliminar los pendientes", "Pendientes", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }

        private void btnAbonos_Click(object sender, EventArgs e)
        {
            frmAbonoCredito form = new frmAbonoCredito();
            form.ShowDialog();
        }

        private void dtgvDetalleFactura_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Right:
                    {
                        dtgvDetalleFactura.CurrentCell = dtgvDetalleFactura.Rows[e.RowIndex].Cells[e.ColumnIndex];
                        Rectangle coordenada = dtgvDetalleFactura.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false);

                        int anchoCelda = coordenada.Location.X; //Ancho de la localizacion de la celda
                        int altoCelda = coordenada.Location.Y;  //Alto de la localizacion de la celda

                        //Y para mostrar el menú lo haces de esta forma:  
                        int X = anchoCelda + dtgvDetalleFactura.Location.X + 200;
                        int Y = altoCelda + dtgvDetalleFactura.Location.Y - 80;


                        contextPrecios.Show(dtgvDetalleFactura, new Point(X, Y));//places the menu at the pointer position
                    }
                    break;
            }


        }

        private void contextPrecios_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void contextPrecios_Opening(object sender, CancelEventArgs e)
        {

        }

        private void lkEliminarPendiente_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                activarComandas = true;
                if (lblAlias.Text != string.Empty)
                {
                    DialogResult result = MessageBox.Show("¿Desea eliminar el pendiente: " + lblAlias.Text.Trim() + "?", "Pendientes", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        if (Global.Configuracion.imprimeExtra == (int)Enums.Estado.Activo && activarComandas)
                        {
                            eliminarComandas(lblAlias.Text.Trim());
                        }

                        pendientesIns.removeByAlias(lblAlias.Text.Trim());
                        MessageBox.Show("Se ha eliminado el pendiente", "Pendientes", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        limpiarFactura();
                        cargarPendientes();

                    }

                }
                else
                {
                    MessageBox.Show("No hay pendiente cargado.", "Pendientes", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }

            }
            catch (Exception)
            {

                MessageBox.Show("Error al eliminar los pendientes", "Pendientes", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }
        private void eliminarComandas(string v)
        {
            listaComandas.Clear();
            var fact = pendientesIns.GetEntityByAlias(lblAlias.Text.Trim());
            foreach (var item in fact.tbDetalleDocumentoPendiente.Where(x => x.tbProducto.cocina == true).ToList())
            {


                clsComanda comanda = new clsComanda();
                comanda.alias = item.alias;
                comanda.cant = item.cantidad;
                comanda.codigoProducto = item.idProducto;
                comanda.nombre = item.tbProducto.nombre;
                comanda.estado = false;
                listaComandas.Add(comanda);



            }

            imprimirComandas(listaComandas);
        }
        private void frmFacturacionReducida_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F4)
            {
                try
                {
                    buscarProducto();
                }
                catch (Exception)
                {

                    MessageBox.Show("Error al cargar el producto/Servicio", "Buscar Producto", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            else if (e.KeyCode == Keys.F5)
            {
                buscarCliente();

            }
            else if (e.KeyCode == Keys.F2)
            {

                realizarCobro();

            }
            else if (e.KeyCode == Keys.Add)
            {
                aumentarCantidadProducto();
            }
            else if (e.KeyCode == Keys.Subtract)
            {
                disminuirCantidadProducto();

            }
            else if (e.KeyCode == Keys.Right)
            {
                agragarCantidad();

            }
            else if (e.KeyCode == Keys.F12)
            {
                if (Global.Usuario.idRol == (int)Enums.roles.Administracion)
                {

                    abrirCajon();

                }
                else
                {
                    aprobar();
                    if (respuestaAprobaDesc)
                    {
                        abrirCajon();
                    }
                    respuestaAprobaDesc = false;

                }

            }
            else if (e.KeyCode == Keys.F9)
            {
                imprimirCuenta();

            }
            else if (e.KeyCode == Keys.F10)
            {
                pendiente();

            }
            else if (e.KeyCode == Keys.F8)
            {
                if (tabFacturacion.SelectedIndex == 0)
                {
                    tabFacturacion.SelectedTab = tabFacturacion.TabPages[1];

                }
                else
                {
                    tabFacturacion.SelectedTab = tabFacturacion.TabPages[0];

                }


            }
        }

        private void imprimirCuenta()
        {
            if (listaDetalleDocumento.Count != 0 && txtTotal.Text != "0" && lblAlias.Text != string.Empty)
            {
                foreach (var item in listaDetalleDocumento)
                {
                    item.tbProducto = productoIns.GetEntityById(item.idProducto);
                }

                clsImpresionFactura imprimir = new clsImpresionFactura(listaDetalleDocumento, lblAlias.Text, Global.Usuario.tbEmpresa);
                imprimir.printCuenta();



            }
            else
            {


                MessageBox.Show("No hay cuenta pendiente para imprimir.", "Imprimir cuenta", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }
        private void abrirCajon()
        {
            clsImpresionFactura imprimir = new clsImpresionFactura();
            imprimir.abrirCajon();
        }

        private void agragarCantidad()
        {
            try
            {
                if (listaDetalleDocumento.Count > 0)
                {
                    frmCantidad cantidadfrm = new frmCantidad();
                    cantidadfrm.pasarDatosEvent += cantidadPasarDatos;
                    cantidadfrm.ShowDialog();
                    if (peso == decimal.MinValue)
                    {
                        return;
                    }
                    var cantidad = decimal.Parse(string.Format("{0:f3}", peso));
                    tbProducto pro = buscarProducto(listaDetalleDocumento.LastOrDefault().idProducto);
                    agregarProductoDetalleFactura(pro, 1, cantidad, 0,false, true);

                }

            }
            catch (Exception)
            {


                MessageBox.Show("Eror al agragar la cantidad.", "Agregar cantidad al producto", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void buscarCliente()
        {
            try
            {

                FrmBuscar buscar = new FrmBuscar();
                buscar.pasarDatosEvent += dataBuscar;
                buscar.ShowDialog();

            }
            catch (Exception)
            {

                MessageBox.Show("Se produjo un error al buscar el cliente", "Busqueda Cliente", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void disminuirCantidadProducto()
        {
            if (listaDetalleDocumento.Count > 0)
            {
                if (listaDetalleDocumento.LastOrDefault().cantidad - 1 < 1)
                {


                    MessageBox.Show("La cantidad debe ser mayor a 0.", "Agregar producto", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    tbProducto pro = buscarProducto(listaDetalleDocumento.LastOrDefault().idProducto);


                    agregarProductoDetalleFactura(pro, 1, -1,0, true, true);

                    txtCodigo.ResetText();
                    txtCodigo.Select();
                }

            }

        }

        private void aumentarCantidadProducto()
        {
            if (listaDetalleDocumento.Count > 0)
            {
                tbProducto pro = buscarProducto(listaDetalleDocumento.LastOrDefault().idProducto);
                agregarProductoDetalleFactura(pro);
                txtCodigo.ResetText();
                txtCodigo.Select();
            }

        }

        private void contextPrecios_Opening_1(object sender, CancelEventArgs e)
        {
            if (dtgvDetalleFactura.CurrentRow.Cells[1].Value != null)
            {
                string codigoProducto = dtgvDetalleFactura.CurrentRow.Cells[1].Value.ToString();
                if (codigoProducto != string.Empty)
                {
                    tbProducto prod = buscarProducto(codigoProducto);
                    decimal descPromo = 0;
                    if ((bool)Global.Usuario.tbEmpresa.tbParametrosEmpresa.FirstOrDefault().promociones)
                    {

                        var promos = promoIns.getPromosByIdProdFechas(prod.idProducto, Utility.getDate());
                        if (promos.Count != 0)
                        {
                            //ordeno descendentemente para obtener el que se actualizo de utlimo
                            promos = promos.OrderByDescending(x => x.fecha_ult_mod).ToList();
                            descPromo = promoIns.getPromosByIdProdFechas(prod.idProducto, Utility.getDate()).FirstOrDefault().descuento;

                        }

                    }

                    decimal imp = (decimal)listaImp.Where(x => x.id == prod.idTipoImpuesto).FirstOrDefault().valor;
                    ProductoDTO producto = ProductosUtility.calcularPrecio(prod.precio_real, (decimal)(prod.utilidad1Porc - descPromo), (decimal)(prod.utilidad2Porc - descPromo), (decimal)(prod.utilidad3Porc - descPromo), imp);


                    contextPrecios.Items[0].Enabled = true;
                    contextPrecios.Items[1].Enabled = true;
                    contextPrecios.Items[2].Enabled = true;
                    contextPrecios.Items[0].Text = "Precio 1 (" + producto.MontoUtilidad1 + ")";
                    contextPrecios.Items[1].Text = "Precio 2 (" + producto.MontoUtilidad2 + ")";
                    contextPrecios.Items[2].Text = "Precio 3 (" + producto.MontoUtilidad3 + ")";
                    if (prod.precioVenta1 <= 0)
                    {
                        contextPrecios.Items[0].Enabled = false;

                    }
                    if (prod.precioVenta2 <= 0)
                    {
                        contextPrecios.Items[1].Enabled = false;

                    }
                    if (prod.precioVenta3 <= 0)
                    {
                        contextPrecios.Items[2].Enabled = false;

                    }

                }
            }
        }

        private void contextPrecios_ItemClicked_1(object sender, ToolStripItemClickedEventArgs e)
        {
            try
            {
                if (dtgvDetalleFactura.CurrentRow.Cells[1].Value != null)
                {
                    bool bandera = false;
                    string codigoProducto = dtgvDetalleFactura.CurrentRow.Cells[1].Value.ToString();
                    if (codigoProducto != string.Empty)
                    {
                        decimal descPromo = 0;
                        tbProducto prod = buscarProducto(codigoProducto);

                        if ((bool)Global.Usuario.tbEmpresa.tbParametrosEmpresa.FirstOrDefault().promociones)
                        {

                            var promos = promoIns.getPromosByIdProdFechas(prod.idProducto, Utility.getDate());
                            if (promos.Count != 0)
                            {
                                //ordeno descendentemente para obtener el que se actualizo de utlimo
                                promos = promos.OrderByDescending(x => x.fecha_ult_mod).ToList();
                                descPromo = promoIns.getPromosByIdProdFechas(prod.idProducto, Utility.getDate()).FirstOrDefault().descuento;

                            }

                        }

                        decimal imp = (decimal)listaImp.Where(x => x.id == prod.idTipoImpuesto).FirstOrDefault().valor;
                        ProductoDTO producto = ProductosUtility.calcularPrecio(prod.precio_real, (decimal)(prod.utilidad1Porc - descPromo), (decimal)(prod.utilidad2Porc - descPromo), (decimal)(prod.utilidad3Porc - descPromo), imp);


                        if (prod != null)
                        {
                            decimal precioProd = 0, utilidad = 0;
                            if (e.ClickedItem.Name == "precio1")
                            {
                                precioProd = (decimal)producto.MontoUtilidad1;
                                utilidad = (decimal)producto.Utilidad1;
                            }
                            else if (e.ClickedItem.Name == "precio2")
                            {
                                bandera = true;
                                precioProd = (decimal)producto.MontoUtilidad2;
                                utilidad = (decimal)producto.Utilidad2;
                            }
                            else if (e.ClickedItem.Name == "precio3")
                            {
                                bandera = true;
                                precioProd = (decimal)producto.MontoUtilidad3;
                                utilidad = (decimal)producto.Utilidad3;
                            }



                            if (bandera)
                            {

                                aprobar();
                                if (respuestaAprobaDesc)
                                {
                                    agregarProductoDetalleFactura(prod, 2, precioProd, 0, false, true);
                                }
                                respuestaAprobaDesc = false;

                            }
                            else
                            {
                                agregarProductoDetalleFactura(prod, 2, precioProd, 0, false, true);
                            }




                        }


                    }
                }


            }
            catch (Exception)
            {

                MessageBox.Show("Error al asignar el precio", "Precio", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void label20_Click(object sender, EventArgs e)
        {

        }

        private void btnEnvioCorreos_Click(object sender, EventArgs e)
        {
            lblProgress.Text = $"0 de 0";
            backgroundWorker.RunWorkerAsync();
            if (btnEnvioCorreos.Enabled)
            {
                btnEnvioCorreos.Enabled = false;

            }

        }

        private void gbxAcciones_Enter(object sender, EventArgs e)
        {

        }
    }
}
