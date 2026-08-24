namespace PresentationLayer
{
    partial class frmFacturacion1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmFacturacion1));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnlEncabezado = new System.Windows.Forms.Panel();
            this.lblCliente = new System.Windows.Forms.Label();
            this.txtIdCliente = new System.Windows.Forms.TextBox();
            this.txtCliente = new System.Windows.Forms.TextBox();
            this.btnBuscarCliente = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.txtTel = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtCorreo = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtDireccion = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.cboActividadEconomica = new System.Windows.Forms.ComboBox();
            this.chkEnviar = new System.Windows.Forms.CheckBox();
            this.chkFacturaElectronica = new System.Windows.Forms.CheckBox();
            this.chkTiqueteElectronico = new System.Windows.Forms.CheckBox();
            this.chkDolares = new System.Windows.Forms.CheckBox();
            this.lblCambioDolar = new System.Windows.Forms.Label();
            this.gbxAcciones = new System.Windows.Forms.Panel();
            this.btnBuscarFactura = new System.Windows.Forms.Button();
            this.btnBuscarProforma = new System.Windows.Forms.Button();
            this.sepTool1 = new System.Windows.Forms.Panel();
            this.btnClientes = new System.Windows.Forms.Button();
            this.btnProductos = new System.Windows.Forms.Button();
            this.sepTool2 = new System.Windows.Forms.Panel();
            this.btnValidacion = new System.Windows.Forms.Button();
            this.btnAbonos = new System.Windows.Forms.Button();
            this.btnEnvioCorreos = new System.Windows.Forms.Button();
            this.btnReImprimir = new System.Windows.Forms.Button();
            this.sepTool3 = new System.Windows.Forms.Panel();
            this.btnLimpiarForm = new System.Windows.Forms.Button();
            this.lblProgress = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtCodigo = new System.Windows.Forms.TextBox();
            this.dtgvDetalleFactura = new System.Windows.Forms.DataGridView();
            this.colLinea = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNom = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPrec = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCant = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDesc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colUtilidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSubTot = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPrecioIVA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEliminar = new System.Windows.Forms.DataGridViewLinkColumn();
            this.label3 = new System.Windows.Forms.Label();
            this.lblTotalProducto = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblCantidadLineas = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtObservaciones = new System.Windows.Forms.TextBox();
            this.gbxMontos = new System.Windows.Forms.GroupBox();
            this.lblSubtotal = new System.Windows.Forms.Label();
            this.txtSubtotal = new System.Windows.Forms.TextBox();
            this.lblDescuento = new System.Windows.Forms.Label();
            this.txtPorcetaje = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtDescuento = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtExoneracion = new System.Windows.Forms.TextBox();
            this.lblIva = new System.Windows.Forms.Label();
            this.txtIva = new System.Windows.Forms.TextBox();
            this.lblTotal = new System.Windows.Forms.Label();
            this.txtTotal = new System.Windows.Forms.TextBox();
            this.btnCobrar = new System.Windows.Forms.Button();
            this.btnProforma = new System.Windows.Forms.Button();
            this.label16 = new System.Windows.Forms.Label();
            this.btnsalir = new System.Windows.Forms.PictureBox();
            this.contextPrecios = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.precio1 = new System.Windows.Forms.ToolStripMenuItem();
            this.precio2 = new System.Windows.Forms.ToolStripMenuItem();
            this.precio3 = new System.Windows.Forms.ToolStripMenuItem();
            this.pnlEncabezado.SuspendLayout();
            this.gbxAcciones.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvDetalleFactura)).BeginInit();
            this.gbxMontos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnsalir)).BeginInit();
            this.contextPrecios.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlEncabezado
            // 
            this.pnlEncabezado.BackColor = System.Drawing.Color.White;
            this.pnlEncabezado.Controls.Add(this.lblCliente);
            this.pnlEncabezado.Controls.Add(this.txtIdCliente);
            this.pnlEncabezado.Controls.Add(this.txtCliente);
            this.pnlEncabezado.Controls.Add(this.btnBuscarCliente);
            this.pnlEncabezado.Controls.Add(this.label4);
            this.pnlEncabezado.Controls.Add(this.txtTel);
            this.pnlEncabezado.Controls.Add(this.label6);
            this.pnlEncabezado.Controls.Add(this.txtCorreo);
            this.pnlEncabezado.Controls.Add(this.label1);
            this.pnlEncabezado.Controls.Add(this.txtDireccion);
            this.pnlEncabezado.Controls.Add(this.label18);
            this.pnlEncabezado.Controls.Add(this.cboActividadEconomica);
            this.pnlEncabezado.Controls.Add(this.chkEnviar);
            this.pnlEncabezado.Controls.Add(this.chkFacturaElectronica);
            this.pnlEncabezado.Controls.Add(this.chkTiqueteElectronico);
            this.pnlEncabezado.Controls.Add(this.chkDolares);
            this.pnlEncabezado.Controls.Add(this.lblCambioDolar);
            this.pnlEncabezado.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlEncabezado.Location = new System.Drawing.Point(0, 0);
            this.pnlEncabezado.Name = "pnlEncabezado";
            this.pnlEncabezado.Size = new System.Drawing.Size(1250, 92);
            this.pnlEncabezado.TabIndex = 0;
            // 
            // lblCliente
            // 
            this.lblCliente.AutoSize = true;
            this.lblCliente.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblCliente.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.lblCliente.Location = new System.Drawing.Point(8, 6);
            this.lblCliente.Name = "lblCliente";
            this.lblCliente.Size = new System.Drawing.Size(47, 15);
            this.lblCliente.TabIndex = 35;
            this.lblCliente.Text = "Cliente:";
            // 
            // txtIdCliente
            // 
            this.txtIdCliente.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtIdCliente.Location = new System.Drawing.Point(8, 22);
            this.txtIdCliente.Name = "txtIdCliente";
            this.txtIdCliente.Size = new System.Drawing.Size(110, 24);
            this.txtIdCliente.TabIndex = 38;
            this.txtIdCliente.TextChanged += new System.EventHandler(this.txtIdCliente_TextChanged);
            this.txtIdCliente.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtIdCliente_KeyPress);
            // 
            // txtCliente
            // 
            this.txtCliente.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtCliente.Location = new System.Drawing.Point(124, 22);
            this.txtCliente.Name = "txtCliente";
            this.txtCliente.Size = new System.Drawing.Size(280, 24);
            this.txtCliente.TabIndex = 34;
            // 
            // btnBuscarCliente
            // 
            this.btnBuscarCliente.Image = ((System.Drawing.Image)(resources.GetObject("btnBuscarCliente.Image")));
            this.btnBuscarCliente.Location = new System.Drawing.Point(410, 21);
            this.btnBuscarCliente.Name = "btnBuscarCliente";
            this.btnBuscarCliente.Size = new System.Drawing.Size(34, 25);
            this.btnBuscarCliente.TabIndex = 36;
            this.btnBuscarCliente.UseVisualStyleBackColor = true;
            this.btnBuscarCliente.Click += new System.EventHandler(this.btnBuscarCliente_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.label4.Location = new System.Drawing.Point(454, 6);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(55, 15);
            this.label4.TabIndex = 47;
            this.label4.Text = "Teléfono:";
            // 
            // txtTel
            // 
            this.txtTel.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtTel.Location = new System.Drawing.Point(454, 22);
            this.txtTel.Name = "txtTel";
            this.txtTel.ReadOnly = true;
            this.txtTel.Size = new System.Drawing.Size(110, 24);
            this.txtTel.TabIndex = 46;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.label6.Location = new System.Drawing.Point(572, 6);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(46, 15);
            this.label6.TabIndex = 49;
            this.label6.Text = "Correo:";
            // 
            // txtCorreo
            // 
            this.txtCorreo.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtCorreo.Location = new System.Drawing.Point(572, 22);
            this.txtCorreo.MaxLength = 50;
            this.txtCorreo.Name = "txtCorreo";
            this.txtCorreo.Size = new System.Drawing.Size(190, 24);
            this.txtCorreo.TabIndex = 48;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.label1.Location = new System.Drawing.Point(8, 50);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 15);
            this.label1.TabIndex = 40;
            this.label1.Text = "Dirección:";
            // 
            // txtDireccion
            // 
            this.txtDireccion.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtDireccion.Location = new System.Drawing.Point(124, 47);
            this.txtDireccion.MaxLength = 500;
            this.txtDireccion.Name = "txtDireccion";
            this.txtDireccion.ReadOnly = true;
            this.txtDireccion.Size = new System.Drawing.Size(638, 23);
            this.txtDireccion.TabIndex = 39;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label18.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.label18.Location = new System.Drawing.Point(774, 6);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(60, 15);
            this.label18.TabIndex = 99;
            this.label18.Text = "Actividad:";
            // 
            // cboActividadEconomica
            // 
            this.cboActividadEconomica.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.cboActividadEconomica.FormattingEnabled = true;
            this.cboActividadEconomica.Location = new System.Drawing.Point(774, 22);
            this.cboActividadEconomica.Name = "cboActividadEconomica";
            this.cboActividadEconomica.Size = new System.Drawing.Size(360, 25);
            this.cboActividadEconomica.TabIndex = 98;
            // 
            // chkEnviar
            // 
            this.chkEnviar.AutoSize = true;
            this.chkEnviar.Checked = true;
            this.chkEnviar.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkEnviar.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.chkEnviar.Location = new System.Drawing.Point(572, 50);
            this.chkEnviar.Name = "chkEnviar";
            this.chkEnviar.Size = new System.Drawing.Size(124, 19);
            this.chkEnviar.TabIndex = 52;
            this.chkEnviar.Text = "Correo Electrónico";
            this.chkEnviar.UseVisualStyleBackColor = true;
            this.chkEnviar.CheckedChanged += new System.EventHandler(this.chkEnviar_CheckedChanged);
            // 
            // chkFacturaElectronica
            // 
            this.chkFacturaElectronica.AutoSize = true;
            this.chkFacturaElectronica.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.chkFacturaElectronica.Location = new System.Drawing.Point(774, 50);
            this.chkFacturaElectronica.Name = "chkFacturaElectronica";
            this.chkFacturaElectronica.Size = new System.Drawing.Size(126, 19);
            this.chkFacturaElectronica.TabIndex = 97;
            this.chkFacturaElectronica.Text = "Factura Electrónica";
            this.chkFacturaElectronica.UseVisualStyleBackColor = true;
            // 
            // chkTiqueteElectronico
            // 
            this.chkTiqueteElectronico.AutoSize = true;
            this.chkTiqueteElectronico.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.chkTiqueteElectronico.Location = new System.Drawing.Point(910, 50);
            this.chkTiqueteElectronico.Name = "chkTiqueteElectronico";
            this.chkTiqueteElectronico.Size = new System.Drawing.Size(127, 19);
            this.chkTiqueteElectronico.TabIndex = 77;
            this.chkTiqueteElectronico.Text = "Tiquete Electrónico";
            this.chkTiqueteElectronico.UseVisualStyleBackColor = true;
            // 
            // chkDolares
            // 
            this.chkDolares.AutoSize = true;
            this.chkDolares.Location = new System.Drawing.Point(0, 0);
            this.chkDolares.Name = "chkDolares";
            this.chkDolares.Size = new System.Drawing.Size(62, 17);
            this.chkDolares.TabIndex = 68;
            this.chkDolares.Text = "Dólares";
            this.chkDolares.UseVisualStyleBackColor = true;
            this.chkDolares.Visible = false;
            this.chkDolares.CheckedChanged += new System.EventHandler(this.chkDolares_CheckedChanged);
            // 
            // lblCambioDolar
            // 
            this.lblCambioDolar.AutoSize = true;
            this.lblCambioDolar.Location = new System.Drawing.Point(0, 0);
            this.lblCambioDolar.Name = "lblCambioDolar";
            this.lblCambioDolar.Size = new System.Drawing.Size(0, 13);
            this.lblCambioDolar.TabIndex = 98;
            this.lblCambioDolar.Visible = false;
            // 
            // gbxAcciones
            // 
            this.gbxAcciones.BackColor = System.Drawing.Color.White;
            this.gbxAcciones.Controls.Add(this.btnBuscarFactura);
            this.gbxAcciones.Controls.Add(this.btnBuscarProforma);
            this.gbxAcciones.Controls.Add(this.sepTool1);
            this.gbxAcciones.Controls.Add(this.btnClientes);
            this.gbxAcciones.Controls.Add(this.btnProductos);
            this.gbxAcciones.Controls.Add(this.sepTool2);
            this.gbxAcciones.Controls.Add(this.btnValidacion);
            this.gbxAcciones.Controls.Add(this.btnAbonos);
            this.gbxAcciones.Controls.Add(this.btnEnvioCorreos);
            this.gbxAcciones.Controls.Add(this.btnReImprimir);
            this.gbxAcciones.Controls.Add(this.sepTool3);
            this.gbxAcciones.Controls.Add(this.btnLimpiarForm);
            this.gbxAcciones.Controls.Add(this.lblProgress);
            this.gbxAcciones.Controls.Add(this.label20);
            this.gbxAcciones.Controls.Add(this.label17);
            this.gbxAcciones.Controls.Add(this.label10);
            this.gbxAcciones.Controls.Add(this.label11);
            this.gbxAcciones.Controls.Add(this.label12);
            this.gbxAcciones.Controls.Add(this.label13);
            this.gbxAcciones.Controls.Add(this.label14);
            this.gbxAcciones.Controls.Add(this.label15);
            this.gbxAcciones.Controls.Add(this.label21);
            this.gbxAcciones.Controls.Add(this.label22);
            this.gbxAcciones.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbxAcciones.Location = new System.Drawing.Point(0, 92);
            this.gbxAcciones.Name = "gbxAcciones";
            this.gbxAcciones.Size = new System.Drawing.Size(1250, 46);
            this.gbxAcciones.TabIndex = 50;
            this.gbxAcciones.Enter += new System.EventHandler(this.gbxAcciones_Enter);
            // 
            // btnBuscarFactura
            // 
            this.btnBuscarFactura.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnBuscarFactura.Image = ((System.Drawing.Image)(resources.GetObject("btnBuscarFactura.Image")));
            this.btnBuscarFactura.Location = new System.Drawing.Point(6, 6);
            this.btnBuscarFactura.Name = "btnBuscarFactura";
            this.btnBuscarFactura.Size = new System.Drawing.Size(120, 34);
            this.btnBuscarFactura.TabIndex = 45;
            this.btnBuscarFactura.Text = "Buscar factura";
            this.btnBuscarFactura.UseVisualStyleBackColor = true;
            this.btnBuscarFactura.Click += new System.EventHandler(this.btnBuscarFactura_Click);
            // 
            // btnBuscarProforma
            // 
            this.btnBuscarProforma.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnBuscarProforma.Image = ((System.Drawing.Image)(resources.GetObject("btnBuscarProforma.Image")));
            this.btnBuscarProforma.Location = new System.Drawing.Point(132, 6);
            this.btnBuscarProforma.Name = "btnBuscarProforma";
            this.btnBuscarProforma.Size = new System.Drawing.Size(120, 34);
            this.btnBuscarProforma.TabIndex = 58;
            this.btnBuscarProforma.Text = "Proformas";
            this.btnBuscarProforma.UseVisualStyleBackColor = true;
            this.btnBuscarProforma.Click += new System.EventHandler(this.btnBuscarProforma_Click);
            // 
            // sepTool1
            // 
            this.sepTool1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.sepTool1.Location = new System.Drawing.Point(258, 6);
            this.sepTool1.Name = "sepTool1";
            this.sepTool1.Size = new System.Drawing.Size(1, 34);
            this.sepTool1.TabIndex = 59;
            // 
            // btnClientes
            // 
            this.btnClientes.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnClientes.Image = ((System.Drawing.Image)(resources.GetObject("btnClientes.Image")));
            this.btnClientes.Location = new System.Drawing.Point(268, 6);
            this.btnClientes.Name = "btnClientes";
            this.btnClientes.Size = new System.Drawing.Size(110, 34);
            this.btnClientes.TabIndex = 46;
            this.btnClientes.Text = "Clientes";
            this.btnClientes.UseVisualStyleBackColor = true;
            this.btnClientes.Click += new System.EventHandler(this.btnClientes_Click);
            // 
            // btnProductos
            // 
            this.btnProductos.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnProductos.Image = ((System.Drawing.Image)(resources.GetObject("btnProductos.Image")));
            this.btnProductos.Location = new System.Drawing.Point(384, 6);
            this.btnProductos.Name = "btnProductos";
            this.btnProductos.Size = new System.Drawing.Size(110, 34);
            this.btnProductos.TabIndex = 47;
            this.btnProductos.Text = "Productos";
            this.btnProductos.UseVisualStyleBackColor = true;
            this.btnProductos.Click += new System.EventHandler(this.btnProductos_Click);
            // 
            // sepTool2
            // 
            this.sepTool2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.sepTool2.Location = new System.Drawing.Point(500, 6);
            this.sepTool2.Name = "sepTool2";
            this.sepTool2.Size = new System.Drawing.Size(1, 34);
            this.sepTool2.TabIndex = 60;
            // 
            // btnValidacion
            // 
            this.btnValidacion.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnValidacion.Image = ((System.Drawing.Image)(resources.GetObject("btnValidacion.Image")));
            this.btnValidacion.Location = new System.Drawing.Point(510, 6);
            this.btnValidacion.Name = "btnValidacion";
            this.btnValidacion.Size = new System.Drawing.Size(130, 34);
            this.btnValidacion.TabIndex = 98;
            this.btnValidacion.Text = "Validar Hacienda";
            this.btnValidacion.UseVisualStyleBackColor = true;
            this.btnValidacion.Click += new System.EventHandler(this.btnValidacion_Click_1);
            // 
            // btnAbonos
            // 
            this.btnAbonos.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnAbonos.Image = ((System.Drawing.Image)(resources.GetObject("btnAbonos.Image")));
            this.btnAbonos.Location = new System.Drawing.Point(646, 6);
            this.btnAbonos.Name = "btnAbonos";
            this.btnAbonos.Size = new System.Drawing.Size(100, 34);
            this.btnAbonos.TabIndex = 65;
            this.btnAbonos.Text = "Abonos";
            this.btnAbonos.UseVisualStyleBackColor = true;
            this.btnAbonos.Click += new System.EventHandler(this.btnAbonos_Click);
            // 
            // btnEnvioCorreos
            // 
            this.btnEnvioCorreos.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnEnvioCorreos.Image = ((System.Drawing.Image)(resources.GetObject("btnEnvioCorreos.Image")));
            this.btnEnvioCorreos.Location = new System.Drawing.Point(750, 6);
            this.btnEnvioCorreos.Name = "btnEnvioCorreos";
            this.btnEnvioCorreos.Size = new System.Drawing.Size(120, 34);
            this.btnEnvioCorreos.TabIndex = 52;
            this.btnEnvioCorreos.Text = "Correo facturas";
            this.btnEnvioCorreos.UseVisualStyleBackColor = true;
            this.btnEnvioCorreos.Click += new System.EventHandler(this.btnValidacion_Click);
            // 
            // btnReImprimir
            // 
            this.btnReImprimir.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnReImprimir.Image = ((System.Drawing.Image)(resources.GetObject("btnReImprimir.Image")));
            this.btnReImprimir.Location = new System.Drawing.Point(876, 6);
            this.btnReImprimir.Name = "btnReImprimir";
            this.btnReImprimir.Size = new System.Drawing.Size(110, 34);
            this.btnReImprimir.TabIndex = 61;
            this.btnReImprimir.Text = "Re-imprimir";
            this.btnReImprimir.UseVisualStyleBackColor = true;
            this.btnReImprimir.Click += new System.EventHandler(this.btnReImprimir_Click);
            // 
            // sepTool3
            // 
            this.sepTool3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.sepTool3.Location = new System.Drawing.Point(992, 6);
            this.sepTool3.Name = "sepTool3";
            this.sepTool3.Size = new System.Drawing.Size(1, 34);
            this.sepTool3.TabIndex = 99;
            // 
            // btnLimpiarForm
            // 
            this.btnLimpiarForm.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnLimpiarForm.Image = ((System.Drawing.Image)(resources.GetObject("btnLimpiarForm.Image")));
            this.btnLimpiarForm.Location = new System.Drawing.Point(1002, 6);
            this.btnLimpiarForm.Name = "btnLimpiarForm";
            this.btnLimpiarForm.Size = new System.Drawing.Size(100, 34);
            this.btnLimpiarForm.TabIndex = 33;
            this.btnLimpiarForm.Text = "Limpiar";
            this.btnLimpiarForm.UseVisualStyleBackColor = true;
            this.btnLimpiarForm.Click += new System.EventHandler(this.btnLimpiarForm_Click);
            // 
            // lblProgress
            // 
            this.lblProgress.AutoSize = true;
            this.lblProgress.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lblProgress.ForeColor = System.Drawing.Color.Gray;
            this.lblProgress.Location = new System.Drawing.Point(1108, 12);
            this.lblProgress.Name = "lblProgress";
            this.lblProgress.Size = new System.Drawing.Size(93, 13);
            this.lblProgress.TabIndex = 99;
            this.lblProgress.Text = "Progreso : 0 de 0";
            this.lblProgress.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("Segoe UI", 7.5F);
            this.label20.ForeColor = System.Drawing.Color.Gray;
            this.label20.Location = new System.Drawing.Point(1108, 27);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(131, 12);
            this.label20.TabIndex = 98;
            this.label20.Text = "F2 Cobrar F4 Prod. F5 Cliente";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(0, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(53, 13);
            this.label17.TabIndex = 99;
            this.label17.Text = "Hacienda";
            this.label17.Visible = false;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(0, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(40, 13);
            this.label10.TabIndex = 48;
            this.label10.Text = "Limpiar";
            this.label10.Visible = false;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(0, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(67, 13);
            this.label11.TabIndex = 49;
            this.label11.Text = "Documentos";
            this.label11.Visible = false;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(0, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(44, 13);
            this.label12.TabIndex = 50;
            this.label12.Text = "Clientes";
            this.label12.Visible = false;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(0, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(55, 13);
            this.label13.TabIndex = 51;
            this.label13.Text = "Productos";
            this.label13.Visible = false;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(0, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(82, 13);
            this.label14.TabIndex = 53;
            this.label14.Text = "Correo Facturas";
            this.label14.Visible = false;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(0, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(43, 13);
            this.label15.TabIndex = 67;
            this.label15.Text = "Abonos";
            this.label15.Visible = false;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(0, 0);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(54, 13);
            this.label21.TabIndex = 59;
            this.label21.Text = "Proformas";
            this.label21.Visible = false;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(0, 0);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(58, 13);
            this.label22.TabIndex = 62;
            this.label22.Text = "Re-imprimir";
            this.label22.Visible = false;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.White;
            this.groupBox1.Controls.Add(this.btnBuscar);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtCodigo);
            this.groupBox1.Controls.Add(this.dtgvDetalleFactura);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.lblTotalProducto);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.lblCantidadLineas);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.txtObservaciones);
            this.groupBox1.Location = new System.Drawing.Point(8, 144);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1010, 522);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // btnBuscar
            // 
            this.btnBuscar.Image = ((System.Drawing.Image)(resources.GetObject("btnBuscar.Image")));
            this.btnBuscar.Location = new System.Drawing.Point(457, 9);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(29, 27);
            this.btnBuscar.TabIndex = 72;
            this.btnBuscar.UseVisualStyleBackColor = true;
            this.btnBuscar.Click += new System.EventHandler(this.button1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.label2.Location = new System.Drawing.Point(6, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 17);
            this.label2.TabIndex = 42;
            this.label2.Text = "Código:";
            // 
            // txtCodigo
            // 
            this.txtCodigo.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.txtCodigo.Location = new System.Drawing.Point(70, 11);
            this.txtCodigo.MaxLength = 20;
            this.txtCodigo.Name = "txtCodigo";
            this.txtCodigo.Size = new System.Drawing.Size(382, 24);
            this.txtCodigo.TabIndex = 39;
            this.txtCodigo.TextChanged += new System.EventHandler(this.txtCodigo_TextChanged);
            this.txtCodigo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCodigo_KeyPress_1);
            // 
            // dtgvDetalleFactura
            // 
            this.dtgvDetalleFactura.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dtgvDetalleFactura.BackgroundColor = System.Drawing.Color.White;
            this.dtgvDetalleFactura.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(42)))), ((int)(((byte)(68)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(42)))), ((int)(((byte)(68)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dtgvDetalleFactura.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dtgvDetalleFactura.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgvDetalleFactura.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colLinea,
            this.colId,
            this.colNom,
            this.colPrec,
            this.colCant,
            this.colDesc,
            this.colUtilidad,
            this.colSubTot,
            this.colPrecioIVA,
            this.colEliminar});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(32)))), ((int)(((byte)(40)))));
            dataGridViewCellStyle3.Padding = new System.Windows.Forms.Padding(0, 4, 0, 4);
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(133)))), ((int)(((byte)(244)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dtgvDetalleFactura.DefaultCellStyle = dataGridViewCellStyle3;
            this.dtgvDetalleFactura.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(230)))));
            this.dtgvDetalleFactura.Location = new System.Drawing.Point(6, 38);
            this.dtgvDetalleFactura.MultiSelect = false;
            this.dtgvDetalleFactura.Name = "dtgvDetalleFactura";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Segoe UI", 8F);
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(40)))), ((int)(((byte)(59)))));
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(133)))), ((int)(((byte)(244)))));
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dtgvDetalleFactura.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dtgvDetalleFactura.RowHeadersVisible = false;
            this.dtgvDetalleFactura.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.dtgvDetalleFactura.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dtgvDetalleFactura.Size = new System.Drawing.Size(999, 410);
            this.dtgvDetalleFactura.TabIndex = 22;
            this.dtgvDetalleFactura.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dtgvDetalleFactura_CellContentClick);
            this.dtgvDetalleFactura.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dtgvDetalleFactura_CellMouseDown);
            this.dtgvDetalleFactura.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dtgvDetalleFactura_CellValueChanged);
            // 
            // colLinea
            // 
            this.colLinea.Frozen = true;
            this.colLinea.HeaderText = "Linea";
            this.colLinea.Name = "colLinea";
            this.colLinea.ReadOnly = true;
            this.colLinea.Width = 30;
            // 
            // colId
            // 
            this.colId.Frozen = true;
            this.colId.HeaderText = "Código";
            this.colId.Name = "colId";
            this.colId.ReadOnly = true;
            // 
            // colNom
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.colNom.DefaultCellStyle = dataGridViewCellStyle2;
            this.colNom.FillWeight = 369.5432F;
            this.colNom.Frozen = true;
            this.colNom.HeaderText = "Producto";
            this.colNom.Name = "colNom";
            this.colNom.ReadOnly = true;
            this.colNom.Width = 350;
            // 
            // colPrec
            // 
            this.colPrec.FillWeight = 10.15228F;
            this.colPrec.Frozen = true;
            this.colPrec.HeaderText = "Precio";
            this.colPrec.Name = "colPrec";
            this.colPrec.Width = 85;
            // 
            // colCant
            // 
            this.colCant.FillWeight = 10.15228F;
            this.colCant.Frozen = true;
            this.colCant.HeaderText = "Cant";
            this.colCant.Name = "colCant";
            this.colCant.Width = 45;
            // 
            // colDesc
            // 
            this.colDesc.Frozen = true;
            this.colDesc.HeaderText = "%Desc";
            this.colDesc.Name = "colDesc";
            this.colDesc.Width = 60;
            // 
            // colUtilidad
            // 
            this.colUtilidad.Frozen = true;
            this.colUtilidad.HeaderText = "%Utilidad";
            this.colUtilidad.Name = "colUtilidad";
            this.colUtilidad.ReadOnly = true;
            this.colUtilidad.Width = 60;
            // 
            // colSubTot
            // 
            this.colSubTot.FillWeight = 10.15228F;
            this.colSubTot.Frozen = true;
            this.colSubTot.HeaderText = "Subtotal";
            this.colSubTot.Name = "colSubTot";
            this.colSubTot.ReadOnly = true;
            this.colSubTot.Width = 85;
            // 
            // colPrecioIVA
            // 
            this.colPrecioIVA.Frozen = true;
            this.colPrecioIVA.HeaderText = "PrecioIVA";
            this.colPrecioIVA.Name = "colPrecioIVA";
            this.colPrecioIVA.Width = 85;
            // 
            // colEliminar
            // 
            this.colEliminar.Frozen = true;
            this.colEliminar.HeaderText = "";
            this.colEliminar.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(65)))), ((int)(((byte)(12)))));
            this.colEliminar.Name = "colEliminar";
            this.colEliminar.Text = "Eliminar";
            this.colEliminar.ToolTipText = "Eliminar linea";
            this.colEliminar.UseColumnTextForLinkValue = true;
            this.colEliminar.Width = 85;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.label3.Location = new System.Drawing.Point(522, 14);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(107, 13);
            this.label3.TabIndex = 46;
            this.label3.Text = "Total de productos:";
            // 
            // lblTotalProducto
            // 
            this.lblTotalProducto.AutoSize = true;
            this.lblTotalProducto.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.lblTotalProducto.Location = new System.Drawing.Point(640, 14);
            this.lblTotalProducto.Name = "lblTotalProducto";
            this.lblTotalProducto.Size = new System.Drawing.Size(13, 13);
            this.lblTotalProducto.TabIndex = 47;
            this.lblTotalProducto.Text = "0";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.label5.Location = new System.Drawing.Point(676, 14);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(109, 13);
            this.label5.TabIndex = 48;
            this.label5.Text = "Cantidad de Lineas:";
            // 
            // lblCantidadLineas
            // 
            this.lblCantidadLineas.AutoSize = true;
            this.lblCantidadLineas.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.lblCantidadLineas.Location = new System.Drawing.Point(799, 15);
            this.lblCantidadLineas.Name = "lblCantidadLineas";
            this.lblCantidadLineas.Size = new System.Drawing.Size(13, 13);
            this.lblCantidadLineas.TabIndex = 49;
            this.lblCantidadLineas.Text = "0";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.label7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.label7.Location = new System.Drawing.Point(3, 451);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(102, 17);
            this.label7.TabIndex = 51;
            this.label7.Text = "Observaciones:";
            // 
            // txtObservaciones
            // 
            this.txtObservaciones.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtObservaciones.Location = new System.Drawing.Point(4, 470);
            this.txtObservaciones.MaxLength = 1000;
            this.txtObservaciones.Multiline = true;
            this.txtObservaciones.Name = "txtObservaciones";
            this.txtObservaciones.Size = new System.Drawing.Size(1001, 48);
            this.txtObservaciones.TabIndex = 50;
            // 
            // gbxMontos
            // 
            this.gbxMontos.BackColor = System.Drawing.Color.White;
            this.gbxMontos.Controls.Add(this.lblSubtotal);
            this.gbxMontos.Controls.Add(this.txtSubtotal);
            this.gbxMontos.Controls.Add(this.lblDescuento);
            this.gbxMontos.Controls.Add(this.txtPorcetaje);
            this.gbxMontos.Controls.Add(this.label8);
            this.gbxMontos.Controls.Add(this.txtDescuento);
            this.gbxMontos.Controls.Add(this.label9);
            this.gbxMontos.Controls.Add(this.txtExoneracion);
            this.gbxMontos.Controls.Add(this.lblIva);
            this.gbxMontos.Controls.Add(this.txtIva);
            this.gbxMontos.Controls.Add(this.lblTotal);
            this.gbxMontos.Controls.Add(this.txtTotal);
            this.gbxMontos.Controls.Add(this.btnCobrar);
            this.gbxMontos.Controls.Add(this.btnProforma);
            this.gbxMontos.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.gbxMontos.Location = new System.Drawing.Point(1024, 144);
            this.gbxMontos.Name = "gbxMontos";
            this.gbxMontos.Padding = new System.Windows.Forms.Padding(10);
            this.gbxMontos.Size = new System.Drawing.Size(222, 460);
            this.gbxMontos.TabIndex = 31;
            this.gbxMontos.TabStop = false;
            this.gbxMontos.Text = "Totales";
            // 
            // lblSubtotal
            // 
            this.lblSubtotal.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblSubtotal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.lblSubtotal.Location = new System.Drawing.Point(14, 30);
            this.lblSubtotal.Name = "lblSubtotal";
            this.lblSubtotal.Size = new System.Drawing.Size(120, 20);
            this.lblSubtotal.TabIndex = 36;
            this.lblSubtotal.Text = "Subtotal";
            // 
            // txtSubtotal
            // 
            this.txtSubtotal.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            this.txtSubtotal.Location = new System.Drawing.Point(14, 51);
            this.txtSubtotal.Name = "txtSubtotal";
            this.txtSubtotal.ReadOnly = true;
            this.txtSubtotal.Size = new System.Drawing.Size(194, 26);
            this.txtSubtotal.TabIndex = 35;
            this.txtSubtotal.Text = "0";
            this.txtSubtotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblDescuento
            // 
            this.lblDescuento.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblDescuento.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.lblDescuento.Location = new System.Drawing.Point(14, 80);
            this.lblDescuento.Name = "lblDescuento";
            this.lblDescuento.Size = new System.Drawing.Size(90, 20);
            this.lblDescuento.TabIndex = 29;
            this.lblDescuento.Text = "% Descuento";
            this.lblDescuento.Click += new System.EventHandler(this.lblDescuento_Click);
            // 
            // txtPorcetaje
            // 
            this.txtPorcetaje.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtPorcetaje.Location = new System.Drawing.Point(14, 101);
            this.txtPorcetaje.Name = "txtPorcetaje";
            this.txtPorcetaje.Size = new System.Drawing.Size(45, 24);
            this.txtPorcetaje.TabIndex = 63;
            this.txtPorcetaje.Text = "0";
            this.txtPorcetaje.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtPorcetaje.TextChanged += new System.EventHandler(this.txtPorcetaje_TextChanged);
            this.txtPorcetaje.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPorcetaje_KeyDown);
            this.txtPorcetaje.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPorcetaje_KeyPress);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.label8.Location = new System.Drawing.Point(62, 104);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(19, 17);
            this.label8.TabIndex = 64;
            this.label8.Text = "%";
            // 
            // txtDescuento
            // 
            this.txtDescuento.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtDescuento.Location = new System.Drawing.Point(84, 101);
            this.txtDescuento.Name = "txtDescuento";
            this.txtDescuento.ReadOnly = true;
            this.txtDescuento.Size = new System.Drawing.Size(124, 24);
            this.txtDescuento.TabIndex = 33;
            this.txtDescuento.Text = "0";
            this.txtDescuento.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label9
            // 
            this.label9.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.label9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.label9.Location = new System.Drawing.Point(14, 130);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(120, 20);
            this.label9.TabIndex = 65;
            this.label9.Text = "Exoneración";
            // 
            // txtExoneracion
            // 
            this.txtExoneracion.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtExoneracion.Location = new System.Drawing.Point(14, 150);
            this.txtExoneracion.Name = "txtExoneracion";
            this.txtExoneracion.ReadOnly = true;
            this.txtExoneracion.Size = new System.Drawing.Size(194, 24);
            this.txtExoneracion.TabIndex = 66;
            this.txtExoneracion.Text = "0";
            this.txtExoneracion.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblIva
            // 
            this.lblIva.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblIva.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.lblIva.Location = new System.Drawing.Point(14, 180);
            this.lblIva.Name = "lblIva";
            this.lblIva.Size = new System.Drawing.Size(120, 20);
            this.lblIva.TabIndex = 28;
            this.lblIva.Text = "I.V.A.";
            // 
            // txtIva
            // 
            this.txtIva.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtIva.Location = new System.Drawing.Point(14, 200);
            this.txtIva.Name = "txtIva";
            this.txtIva.ReadOnly = true;
            this.txtIva.Size = new System.Drawing.Size(194, 24);
            this.txtIva.TabIndex = 0;
            this.txtIva.Text = "0";
            this.txtIva.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblTotal
            // 
            this.lblTotal.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.lblTotal.Location = new System.Drawing.Point(14, 230);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(90, 30);
            this.lblTotal.TabIndex = 30;
            this.lblTotal.Text = "Total";
            // 
            // txtTotal
            // 
            this.txtTotal.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold);
            this.txtTotal.Location = new System.Drawing.Point(14, 260);
            this.txtTotal.Name = "txtTotal";
            this.txtTotal.ReadOnly = true;
            this.txtTotal.Size = new System.Drawing.Size(194, 34);
            this.txtTotal.TabIndex = 34;
            this.txtTotal.Text = "0";
            this.txtTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // btnCobrar
            // 
            this.btnCobrar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(6)))), ((int)(((byte)(95)))), ((int)(((byte)(70)))));
            this.btnCobrar.FlatAppearance.BorderSize = 0;
            this.btnCobrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCobrar.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnCobrar.ForeColor = System.Drawing.Color.White;
            this.btnCobrar.Location = new System.Drawing.Point(14, 306);
            this.btnCobrar.Name = "btnCobrar";
            this.btnCobrar.Size = new System.Drawing.Size(194, 46);
            this.btnCobrar.TabIndex = 60;
            this.btnCobrar.Text = "Cobrar";
            this.btnCobrar.UseVisualStyleBackColor = false;
            this.btnCobrar.Click += new System.EventHandler(this.btnCobrar_Click);
            // 
            // btnProforma
            // 
            this.btnProforma.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(209)))), ((int)(((byte)(213)))), ((int)(((byte)(224)))));
            this.btnProforma.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnProforma.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.btnProforma.Location = new System.Drawing.Point(14, 358);
            this.btnProforma.Name = "btnProforma";
            this.btnProforma.Size = new System.Drawing.Size(194, 32);
            this.btnProforma.TabIndex = 61;
            this.btnProforma.Text = "Proforma";
            this.btnProforma.UseVisualStyleBackColor = true;
            this.btnProforma.Click += new System.EventHandler(this.btnProforma_Click);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(0, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(58, 24);
            this.label16.TabIndex = 67;
            this.label16.Text = "Desc:";
            this.label16.Visible = false;
            // 
            // btnsalir
            // 
            this.btnsalir.Image = ((System.Drawing.Image)(resources.GetObject("btnsalir.Image")));
            this.btnsalir.Location = new System.Drawing.Point(1224, 6);
            this.btnsalir.Name = "btnsalir";
            this.btnsalir.Size = new System.Drawing.Size(20, 20);
            this.btnsalir.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btnsalir.TabIndex = 96;
            this.btnsalir.TabStop = false;
            this.btnsalir.Click += new System.EventHandler(this.btnsalir_Click);
            // 
            // contextPrecios
            // 
            this.contextPrecios.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextPrecios.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.precio1,
            this.precio2,
            this.precio3});
            this.contextPrecios.Name = "contextPrecios";
            this.contextPrecios.Size = new System.Drawing.Size(117, 70);
            this.contextPrecios.Opening += new System.ComponentModel.CancelEventHandler(this.contextPrecios_Opening);
            this.contextPrecios.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.contextPrecios_ItemClicked);
            // 
            // precio1
            // 
            this.precio1.Name = "precio1";
            this.precio1.Size = new System.Drawing.Size(116, 22);
            this.precio1.Text = "Precio 1";
            this.precio1.Click += new System.EventHandler(this.precio1_Click);
            // 
            // precio2
            // 
            this.precio2.Name = "precio2";
            this.precio2.Size = new System.Drawing.Size(116, 22);
            this.precio2.Text = "Precio 2";
            // 
            // precio3
            // 
            this.precio3.Name = "precio3";
            this.precio3.Size = new System.Drawing.Size(116, 22);
            this.precio3.Text = "Precio 3";
            // 
            // frmFacturacion1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(239)))), ((int)(((byte)(232)))));
            this.ClientSize = new System.Drawing.Size(1250, 673);
            this.Controls.Add(this.btnsalir);
            this.Controls.Add(this.gbxMontos);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.gbxAcciones);
            this.Controls.Add(this.pnlEncabezado);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(32)))), ((int)(((byte)(40)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "frmFacturacion1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Proceso: Facturación";
            this.Load += new System.EventHandler(this.frmFacturacion1_Load);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.frmFacturacion1_KeyUp);
            this.pnlEncabezado.ResumeLayout(false);
            this.pnlEncabezado.PerformLayout();
            this.gbxAcciones.ResumeLayout(false);
            this.gbxAcciones.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvDetalleFactura)).EndInit();
            this.gbxMontos.ResumeLayout(false);
            this.gbxMontos.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnsalir)).EndInit();
            this.contextPrecios.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnLimpiarForm;
        private System.Windows.Forms.GroupBox gbxMontos;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtPorcetaje;
        private System.Windows.Forms.Button btnProforma;
        private System.Windows.Forms.Button btnCobrar;
        private System.Windows.Forms.Label lblSubtotal;
        private System.Windows.Forms.TextBox txtSubtotal;
        private System.Windows.Forms.TextBox txtTotal;
        private System.Windows.Forms.Label lblIva;
        private System.Windows.Forms.TextBox txtDescuento;
        private System.Windows.Forms.Label lblDescuento;
        private System.Windows.Forms.TextBox txtIva;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Button btnBuscarCliente;
        private System.Windows.Forms.Label lblCliente;
        private System.Windows.Forms.TextBox txtCliente;
        private System.Windows.Forms.DataGridView dtgvDetalleFactura;
        private System.Windows.Forms.TextBox txtIdCliente;
        private System.Windows.Forms.TextBox txtDireccion;
        private System.Windows.Forms.TextBox txtCodigo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnBuscarFactura;
        private System.Windows.Forms.Label lblTotalProducto;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblCantidadLineas;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtTel;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtCorreo;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel gbxAcciones;
        private System.Windows.Forms.Panel sepTool1;
        private System.Windows.Forms.Panel sepTool2;
        private System.Windows.Forms.Panel sepTool3;
        private System.Windows.Forms.CheckBox chkEnviar;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtObservaciones;
        private System.Windows.Forms.TextBox txtExoneracion;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Button btnEnvioCorreos;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button btnProductos;
        private System.Windows.Forms.Button btnClientes;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Button btnBuscarProforma;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Button btnReImprimir;
        private System.Windows.Forms.CheckBox chkTiqueteElectronico;
        private System.Windows.Forms.ContextMenuStrip contextPrecios;
        private System.Windows.Forms.ToolStripMenuItem precio1;
        private System.Windows.Forms.ToolStripMenuItem precio2;
        private System.Windows.Forms.ToolStripMenuItem precio3;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Button btnAbonos;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLinea;
        private System.Windows.Forms.DataGridViewTextBoxColumn colId;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNom;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPrec;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCant;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDesc;
        private System.Windows.Forms.DataGridViewTextBoxColumn colUtilidad;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSubTot;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPrecioIVA;
        private System.Windows.Forms.DataGridViewLinkColumn colEliminar;
        private System.Windows.Forms.PictureBox btnsalir;
        private System.Windows.Forms.CheckBox chkFacturaElectronica;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.CheckBox chkDolares;
        private System.Windows.Forms.Label lblCambioDolar;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label lblProgress;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Button btnValidacion;
        private System.Windows.Forms.ComboBox cboActividadEconomica;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Panel pnlEncabezado;
    }
}