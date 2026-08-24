namespace PresentationLayer
{
    partial class frmFacturacion
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmFacturacion));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tabFacturacion = new System.Windows.Forms.TabControl();
            this.tabFact = new System.Windows.Forms.TabPage();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.chkComandas = new System.Windows.Forms.CheckBox();
            this.chkServicioMesa = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lkEliminarPendiente = new System.Windows.Forms.LinkLabel();
            this.lblPendientes = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lblAlias = new System.Windows.Forms.Label();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.txtCodigo = new System.Windows.Forms.TextBox();
            this.dtgvDetalleFactura = new System.Windows.Forms.DataGridView();
            this.colNom = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPrec = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCant = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDes = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSubTot = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPrecioVenta = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEliminar = new System.Windows.Forms.DataGridViewLinkColumn();
            this.label3 = new System.Windows.Forms.Label();
            this.lblTotalProducto = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblCantidadLineas = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.txtObservaciones = new System.Windows.Forms.TextBox();
            this.gbxMontos = new System.Windows.Forms.GroupBox();
            this.lblSubtotal = new System.Windows.Forms.Label();
            this.txtSubtotal = new System.Windows.Forms.TextBox();
            this.lblDescuento = new System.Windows.Forms.Label();
            this.txtDescuento = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtPorcetaje = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtExoneracion = new System.Windows.Forms.TextBox();
            this.lblIva = new System.Windows.Forms.Label();
            this.txtIva = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.txtSub = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.txtServicioMesa = new System.Windows.Forms.TextBox();
            this.lblTotal = new System.Windows.Forms.Label();
            this.txtTotal = new System.Windows.Forms.TextBox();
            this.btnCobrar = new System.Windows.Forms.Button();
            this.btnProforma = new System.Windows.Forms.Button();
            this.btnDividir = new System.Windows.Forms.Button();
            this.btnPendiente = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.pnlCategorias = new System.Windows.Forms.Panel();
            this.label15 = new System.Windows.Forms.Label();
            this.pnlProductos = new System.Windows.Forms.Panel();
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
            this.label18 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.pnlEncabezado = new System.Windows.Forms.Panel();
            this.lblCliente = new System.Windows.Forms.Label();
            this.txtCliente = new System.Windows.Forms.TextBox();
            this.btnBuscarCliente = new System.Windows.Forms.Button();
            this.txtIdCliente = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtTel = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtCorreo = new System.Windows.Forms.TextBox();
            this.chkEnviar = new System.Windows.Forms.CheckBox();
            this.label23 = new System.Windows.Forms.Label();
            this.cboActividadEconomica = new System.Windows.Forms.ComboBox();
            this.chkFacturaElectronica = new System.Windows.Forms.CheckBox();
            this.chkTiqueteElectronico = new System.Windows.Forms.CheckBox();
            this.tabPendientes = new System.Windows.Forms.TabPage();
            this.btnEliminarPendientes = new System.Windows.Forms.Button();
            this.gbxPendientes = new System.Windows.Forms.GroupBox();
            this.contextPrecios = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.precio1 = new System.Windows.Forms.ToolStripMenuItem();
            this.precio2 = new System.Windows.Forms.ToolStripMenuItem();
            this.precio3 = new System.Windows.Forms.ToolStripMenuItem();
            this.tabFacturacion.SuspendLayout();
            this.tabFact.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvDetalleFactura)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.gbxMontos.SuspendLayout();
            this.gbxAcciones.SuspendLayout();
            this.pnlEncabezado.SuspendLayout();
            this.tabPendientes.SuspendLayout();
            this.contextPrecios.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabFacturacion
            // 
            this.tabFacturacion.Controls.Add(this.tabFact);
            this.tabFacturacion.Controls.Add(this.tabPendientes);
            this.tabFacturacion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabFacturacion.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.tabFacturacion.Location = new System.Drawing.Point(0, 0);
            this.tabFacturacion.Name = "tabFacturacion";
            this.tabFacturacion.SelectedIndex = 0;
            this.tabFacturacion.Size = new System.Drawing.Size(1297, 830);
            this.tabFacturacion.TabIndex = 23;
            // 
            // tabFact
            // 
            this.tabFact.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(239)))), ((int)(((byte)(232)))));
            this.tabFact.Controls.Add(this.tabControl1);
            this.tabFact.Controls.Add(this.gbxMontos);
            this.tabFact.Controls.Add(this.label5);
            this.tabFact.Controls.Add(this.pnlCategorias);
            this.tabFact.Controls.Add(this.label15);
            this.tabFact.Controls.Add(this.pnlProductos);
            this.tabFact.Controls.Add(this.gbxAcciones);
            this.tabFact.Controls.Add(this.pnlEncabezado);
            this.tabFact.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.tabFact.Location = new System.Drawing.Point(4, 26);
            this.tabFact.Name = "tabFact";
            this.tabFact.Padding = new System.Windows.Forms.Padding(6);
            this.tabFact.Size = new System.Drawing.Size(1289, 800);
            this.tabFact.TabIndex = 0;
            this.tabFact.Text = "Facturación";
            this.tabFact.Click += new System.EventHandler(this.tabFact_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.tabControl1.Location = new System.Drawing.Point(8, 124);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(941, 357);
            this.tabControl1.TabIndex = 75;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.White;
            this.tabPage1.Controls.Add(this.chkComandas);
            this.tabPage1.Controls.Add(this.chkServicioMesa);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.lblTotalProducto);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.lblCantidadLineas);
            this.tabPage1.Location = new System.Drawing.Point(4, 24);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(933, 329);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Facturación";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // chkComandas
            // 
            this.chkComandas.AutoSize = true;
            this.chkComandas.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.chkComandas.Location = new System.Drawing.Point(565, 306);
            this.chkComandas.Name = "chkComandas";
            this.chkComandas.Size = new System.Drawing.Size(134, 19);
            this.chkComandas.TabIndex = 56;
            this.chkComandas.Text = "Imprimir Comandas";
            this.chkComandas.UseVisualStyleBackColor = true;
            // 
            // chkServicioMesa
            // 
            this.chkServicioMesa.AutoSize = true;
            this.chkServicioMesa.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.chkServicioMesa.Location = new System.Drawing.Point(736, 306);
            this.chkServicioMesa.Name = "chkServicioMesa";
            this.chkServicioMesa.Size = new System.Drawing.Size(120, 19);
            this.chkServicioMesa.TabIndex = 55;
            this.chkServicioMesa.Text = "Servicio de Mesa";
            this.chkServicioMesa.UseVisualStyleBackColor = true;
            this.chkServicioMesa.CheckedChanged += new System.EventHandler(this.chkServicioMesa_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.White;
            this.groupBox1.Controls.Add(this.lkEliminarPendiente);
            this.groupBox1.Controls.Add(this.lblPendientes);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.lblAlias);
            this.groupBox1.Controls.Add(this.btnBuscar);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.txtCodigo);
            this.groupBox1.Controls.Add(this.dtgvDetalleFactura);
            this.groupBox1.Location = new System.Drawing.Point(13, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(914, 298);
            this.groupBox1.TabIndex = 54;
            this.groupBox1.TabStop = false;
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // lkEliminarPendiente
            // 
            this.lkEliminarPendiente.AutoSize = true;
            this.lkEliminarPendiente.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.lkEliminarPendiente.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(65)))), ((int)(((byte)(12)))));
            this.lkEliminarPendiente.Location = new System.Drawing.Point(635, 32);
            this.lkEliminarPendiente.Name = "lkEliminarPendiente";
            this.lkEliminarPendiente.Size = new System.Drawing.Size(111, 15);
            this.lkEliminarPendiente.TabIndex = 76;
            this.lkEliminarPendiente.TabStop = true;
            this.lkEliminarPendiente.Text = "Eliminar Pendiente";
            this.lkEliminarPendiente.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lkEliminarPendiente_LinkClicked);
            // 
            // lblPendientes
            // 
            this.lblPendientes.AutoSize = true;
            this.lblPendientes.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.lblPendientes.Location = new System.Drawing.Point(727, 13);
            this.lblPendientes.Name = "lblPendientes";
            this.lblPendientes.Size = new System.Drawing.Size(13, 13);
            this.lblPendientes.TabIndex = 75;
            this.lblPendientes.Text = "0";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.label7.Location = new System.Drawing.Point(636, 13);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(68, 13);
            this.label7.TabIndex = 74;
            this.label7.Text = "Pendientes:";
            // 
            // lblAlias
            // 
            this.lblAlias.AutoSize = true;
            this.lblAlias.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblAlias.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(65)))), ((int)(((byte)(12)))));
            this.lblAlias.Location = new System.Drawing.Point(770, 17);
            this.lblAlias.Name = "lblAlias";
            this.lblAlias.Size = new System.Drawing.Size(0, 20);
            this.lblAlias.TabIndex = 73;
            // 
            // btnBuscar
            // 
            this.btnBuscar.Image = ((System.Drawing.Image)(resources.GetObject("btnBuscar.Image")));
            this.btnBuscar.Location = new System.Drawing.Point(593, 14);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(36, 34);
            this.btnBuscar.TabIndex = 72;
            this.btnBuscar.UseVisualStyleBackColor = true;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.label9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.label9.Location = new System.Drawing.Point(15, 20);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(56, 17);
            this.label9.TabIndex = 42;
            this.label9.Text = "Código:";
            // 
            // txtCodigo
            // 
            this.txtCodigo.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.txtCodigo.Location = new System.Drawing.Point(104, 17);
            this.txtCodigo.MaxLength = 20;
            this.txtCodigo.Name = "txtCodigo";
            this.txtCodigo.Size = new System.Drawing.Size(477, 24);
            this.txtCodigo.TabIndex = 39;
            this.txtCodigo.TextChanged += new System.EventHandler(this.txtCodigo_TextChanged);
            this.txtCodigo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCodigo_KeyPress);
            // 
            // dtgvDetalleFactura
            // 
            this.dtgvDetalleFactura.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dtgvDetalleFactura.BackgroundColor = System.Drawing.Color.White;
            this.dtgvDetalleFactura.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(42)))), ((int)(((byte)(68)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 9F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(42)))), ((int)(((byte)(68)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dtgvDetalleFactura.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dtgvDetalleFactura.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgvDetalleFactura.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colNom,
            this.colPrec,
            this.colCant,
            this.colDes,
            this.colSubTot,
            this.colId,
            this.colPrecioVenta,
            this.colEliminar});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(32)))), ((int)(((byte)(40)))));
            dataGridViewCellStyle2.Padding = new System.Windows.Forms.Padding(0, 4, 0, 4);
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(133)))), ((int)(((byte)(244)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dtgvDetalleFactura.DefaultCellStyle = dataGridViewCellStyle2;
            this.dtgvDetalleFactura.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(230)))));
            this.dtgvDetalleFactura.Location = new System.Drawing.Point(0, 55);
            this.dtgvDetalleFactura.MultiSelect = false;
            this.dtgvDetalleFactura.Name = "dtgvDetalleFactura";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 9F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(40)))), ((int)(((byte)(59)))));
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(133)))), ((int)(((byte)(244)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dtgvDetalleFactura.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dtgvDetalleFactura.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.dtgvDetalleFactura.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dtgvDetalleFactura.Size = new System.Drawing.Size(913, 230);
            this.dtgvDetalleFactura.TabIndex = 22;
            this.dtgvDetalleFactura.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dtgvDetalleFactura_CellContentClick_1);
            this.dtgvDetalleFactura.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dtgvDetalleFactura_CellMouseDown);
            this.dtgvDetalleFactura.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dtgvDetalleFactura_CellValueChanged_1);
            // 
            // colNom
            // 
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
            this.colPrec.HeaderText = "Precio Antes Imp";
            this.colPrec.Name = "colPrec";
            // 
            // colCant
            // 
            this.colCant.FillWeight = 10.15228F;
            this.colCant.Frozen = true;
            this.colCant.HeaderText = "Cantidad";
            this.colCant.Name = "colCant";
            this.colCant.Width = 60;
            // 
            // colDes
            // 
            this.colDes.Frozen = true;
            this.colDes.HeaderText = "Desc.Max";
            this.colDes.Name = "colDes";
            this.colDes.ReadOnly = true;
            this.colDes.Width = 65;
            // 
            // colSubTot
            // 
            this.colSubTot.FillWeight = 10.15228F;
            this.colSubTot.Frozen = true;
            this.colSubTot.HeaderText = "Subtotal";
            this.colSubTot.Name = "colSubTot";
            this.colSubTot.ReadOnly = true;
            // 
            // colId
            // 
            this.colId.HeaderText = "Id";
            this.colId.Name = "colId";
            this.colId.Visible = false;
            // 
            // colPrecioVenta
            // 
            this.colPrecioVenta.FillWeight = 10.15228F;
            this.colPrecioVenta.Frozen = true;
            this.colPrecioVenta.HeaderText = "Precio Venta";
            this.colPrecioVenta.Name = "colPrecioVenta";
            // 
            // colEliminar
            // 
            this.colEliminar.Frozen = true;
            this.colEliminar.HeaderText = "";
            this.colEliminar.Name = "colEliminar";
            this.colEliminar.Text = "Eliminar";
            this.colEliminar.ToolTipText = "Eliminar linea";
            this.colEliminar.UseColumnTextForLinkValue = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.label3.Location = new System.Drawing.Point(10, 305);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(107, 13);
            this.label3.TabIndex = 46;
            this.label3.Text = "Total de productos:";
            // 
            // lblTotalProducto
            // 
            this.lblTotalProducto.AutoSize = true;
            this.lblTotalProducto.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.lblTotalProducto.Location = new System.Drawing.Point(121, 306);
            this.lblTotalProducto.Name = "lblTotalProducto";
            this.lblTotalProducto.Size = new System.Drawing.Size(13, 13);
            this.lblTotalProducto.TabIndex = 47;
            this.lblTotalProducto.Text = "0";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(202, 306);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(109, 13);
            this.label2.TabIndex = 48;
            this.label2.Text = "Cantidad de Lineas:";
            // 
            // lblCantidadLineas
            // 
            this.lblCantidadLineas.AutoSize = true;
            this.lblCantidadLineas.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.lblCantidadLineas.Location = new System.Drawing.Point(316, 306);
            this.lblCantidadLineas.Name = "lblCantidadLineas";
            this.lblCantidadLineas.Size = new System.Drawing.Size(13, 13);
            this.lblCantidadLineas.TabIndex = 49;
            this.lblCantidadLineas.Text = "0";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.txtObservaciones);
            this.tabPage2.Location = new System.Drawing.Point(4, 24);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(933, 329);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Observaciones";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // txtObservaciones
            // 
            this.txtObservaciones.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtObservaciones.Location = new System.Drawing.Point(6, 15);
            this.txtObservaciones.MaxLength = 1000;
            this.txtObservaciones.Multiline = true;
            this.txtObservaciones.Name = "txtObservaciones";
            this.txtObservaciones.Size = new System.Drawing.Size(915, 300);
            this.txtObservaciones.TabIndex = 52;
            // 
            // gbxMontos
            // 
            this.gbxMontos.BackColor = System.Drawing.Color.White;
            this.gbxMontos.Controls.Add(this.lblSubtotal);
            this.gbxMontos.Controls.Add(this.txtSubtotal);
            this.gbxMontos.Controls.Add(this.lblDescuento);
            this.gbxMontos.Controls.Add(this.txtDescuento);
            this.gbxMontos.Controls.Add(this.label8);
            this.gbxMontos.Controls.Add(this.txtPorcetaje);
            this.gbxMontos.Controls.Add(this.label1);
            this.gbxMontos.Controls.Add(this.txtExoneracion);
            this.gbxMontos.Controls.Add(this.lblIva);
            this.gbxMontos.Controls.Add(this.txtIva);
            this.gbxMontos.Controls.Add(this.label17);
            this.gbxMontos.Controls.Add(this.txtSub);
            this.gbxMontos.Controls.Add(this.label16);
            this.gbxMontos.Controls.Add(this.txtServicioMesa);
            this.gbxMontos.Controls.Add(this.lblTotal);
            this.gbxMontos.Controls.Add(this.txtTotal);
            this.gbxMontos.Controls.Add(this.btnCobrar);
            this.gbxMontos.Controls.Add(this.btnProforma);
            this.gbxMontos.Controls.Add(this.btnDividir);
            this.gbxMontos.Controls.Add(this.btnPendiente);
            this.gbxMontos.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.gbxMontos.Location = new System.Drawing.Point(960, 120);
            this.gbxMontos.Name = "gbxMontos";
            this.gbxMontos.Padding = new System.Windows.Forms.Padding(10);
            this.gbxMontos.Size = new System.Drawing.Size(316, 412);
            this.gbxMontos.TabIndex = 69;
            this.gbxMontos.TabStop = false;
            this.gbxMontos.Text = "Totales";
            // 
            // lblSubtotal
            // 
            this.lblSubtotal.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblSubtotal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.lblSubtotal.Location = new System.Drawing.Point(14, 13);
            this.lblSubtotal.Name = "lblSubtotal";
            this.lblSubtotal.Size = new System.Drawing.Size(120, 20);
            this.lblSubtotal.TabIndex = 36;
            this.lblSubtotal.Text = "Importe";
            // 
            // txtSubtotal
            // 
            this.txtSubtotal.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            this.txtSubtotal.Location = new System.Drawing.Point(148, 10);
            this.txtSubtotal.Name = "txtSubtotal";
            this.txtSubtotal.ReadOnly = true;
            this.txtSubtotal.Size = new System.Drawing.Size(150, 26);
            this.txtSubtotal.TabIndex = 35;
            this.txtSubtotal.Text = "0";
            this.txtSubtotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblDescuento
            // 
            this.lblDescuento.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblDescuento.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.lblDescuento.Location = new System.Drawing.Point(14, 43);
            this.lblDescuento.Name = "lblDescuento";
            this.lblDescuento.Size = new System.Drawing.Size(90, 20);
            this.lblDescuento.TabIndex = 29;
            this.lblDescuento.Text = "Descuento";
            // 
            // txtDescuento
            // 
            this.txtDescuento.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtDescuento.Location = new System.Drawing.Point(218, 40);
            this.txtDescuento.Name = "txtDescuento";
            this.txtDescuento.ReadOnly = true;
            this.txtDescuento.Size = new System.Drawing.Size(80, 24);
            this.txtDescuento.TabIndex = 33;
            this.txtDescuento.Text = "0";
            this.txtDescuento.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.label8.Location = new System.Drawing.Point(196, 43);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(19, 17);
            this.label8.TabIndex = 64;
            this.label8.Text = "%";
            // 
            // txtPorcetaje
            // 
            this.txtPorcetaje.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtPorcetaje.Location = new System.Drawing.Point(148, 40);
            this.txtPorcetaje.Name = "txtPorcetaje";
            this.txtPorcetaje.Size = new System.Drawing.Size(45, 24);
            this.txtPorcetaje.TabIndex = 63;
            this.txtPorcetaje.Text = "0";
            this.txtPorcetaje.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtPorcetaje.TextChanged += new System.EventHandler(this.txtPorcetaje_TextChanged);
            this.txtPorcetaje.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPorcetaje_KeyPress);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.label1.Location = new System.Drawing.Point(14, 73);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(120, 20);
            this.label1.TabIndex = 65;
            this.label1.Text = "Exoneración";
            // 
            // txtExoneracion
            // 
            this.txtExoneracion.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtExoneracion.Location = new System.Drawing.Point(148, 70);
            this.txtExoneracion.Name = "txtExoneracion";
            this.txtExoneracion.ReadOnly = true;
            this.txtExoneracion.Size = new System.Drawing.Size(150, 24);
            this.txtExoneracion.TabIndex = 66;
            this.txtExoneracion.Text = "0";
            this.txtExoneracion.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblIva
            // 
            this.lblIva.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblIva.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.lblIva.Location = new System.Drawing.Point(14, 103);
            this.lblIva.Name = "lblIva";
            this.lblIva.Size = new System.Drawing.Size(120, 20);
            this.lblIva.TabIndex = 28;
            this.lblIva.Text = "I.V.A.";
            // 
            // txtIva
            // 
            this.txtIva.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtIva.Location = new System.Drawing.Point(148, 100);
            this.txtIva.Name = "txtIva";
            this.txtIva.ReadOnly = true;
            this.txtIva.Size = new System.Drawing.Size(150, 24);
            this.txtIva.TabIndex = 0;
            this.txtIva.Text = "0";
            this.txtIva.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label17
            // 
            this.label17.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.label17.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.label17.Location = new System.Drawing.Point(14, 133);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(120, 20);
            this.label17.TabIndex = 72;
            this.label17.Text = "Subtotal";
            // 
            // txtSub
            // 
            this.txtSub.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtSub.Location = new System.Drawing.Point(148, 130);
            this.txtSub.Name = "txtSub";
            this.txtSub.ReadOnly = true;
            this.txtSub.Size = new System.Drawing.Size(150, 24);
            this.txtSub.TabIndex = 73;
            this.txtSub.Text = "0";
            this.txtSub.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label16
            // 
            this.label16.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.label16.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.label16.Location = new System.Drawing.Point(14, 163);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(120, 20);
            this.label16.TabIndex = 74;
            this.label16.Text = "Serv. de Mesa";
            // 
            // txtServicioMesa
            // 
            this.txtServicioMesa.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtServicioMesa.Location = new System.Drawing.Point(148, 160);
            this.txtServicioMesa.Name = "txtServicioMesa";
            this.txtServicioMesa.ReadOnly = true;
            this.txtServicioMesa.Size = new System.Drawing.Size(150, 24);
            this.txtServicioMesa.TabIndex = 75;
            this.txtServicioMesa.Text = "0";
            this.txtServicioMesa.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblTotal
            // 
            this.lblTotal.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.lblTotal.Location = new System.Drawing.Point(14, 195);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(90, 30);
            this.lblTotal.TabIndex = 30;
            this.lblTotal.Text = "Total";
            // 
            // txtTotal
            // 
            this.txtTotal.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold);
            this.txtTotal.Location = new System.Drawing.Point(115, 192);
            this.txtTotal.Name = "txtTotal";
            this.txtTotal.ReadOnly = true;
            this.txtTotal.Size = new System.Drawing.Size(183, 34);
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
            this.btnCobrar.Location = new System.Drawing.Point(14, 238);
            this.btnCobrar.Name = "btnCobrar";
            this.btnCobrar.Size = new System.Drawing.Size(284, 46);
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
            this.btnProforma.Location = new System.Drawing.Point(14, 289);
            this.btnProforma.Name = "btnProforma";
            this.btnProforma.Size = new System.Drawing.Size(284, 32);
            this.btnProforma.TabIndex = 61;
            this.btnProforma.Text = "Proforma";
            this.btnProforma.UseVisualStyleBackColor = true;
            this.btnProforma.Click += new System.EventHandler(this.btnProforma_Click);
            // 
            // btnDividir
            // 
            this.btnDividir.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(209)))), ((int)(((byte)(213)))), ((int)(((byte)(224)))));
            this.btnDividir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDividir.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.btnDividir.Location = new System.Drawing.Point(14, 327);
            this.btnDividir.Name = "btnDividir";
            this.btnDividir.Size = new System.Drawing.Size(284, 32);
            this.btnDividir.TabIndex = 68;
            this.btnDividir.Text = "Dividir cuenta";
            this.btnDividir.UseVisualStyleBackColor = true;
            this.btnDividir.Click += new System.EventHandler(this.btnDividir_Click);
            // 
            // btnPendiente
            // 
            this.btnPendiente.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(209)))), ((int)(((byte)(213)))), ((int)(((byte)(224)))));
            this.btnPendiente.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPendiente.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.btnPendiente.Location = new System.Drawing.Point(14, 365);
            this.btnPendiente.Name = "btnPendiente";
            this.btnPendiente.Size = new System.Drawing.Size(284, 32);
            this.btnPendiente.TabIndex = 67;
            this.btnPendiente.Text = "Marcar pendiente";
            this.btnPendiente.UseVisualStyleBackColor = true;
            this.btnPendiente.Click += new System.EventHandler(this.btnPendiente_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(40)))), ((int)(((byte)(59)))));
            this.label5.Location = new System.Drawing.Point(14, 516);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(81, 19);
            this.label5.TabIndex = 73;
            this.label5.Text = "Categorías";
            // 
            // pnlCategorias
            // 
            this.pnlCategorias.AutoScroll = true;
            this.pnlCategorias.BackColor = System.Drawing.Color.White;
            this.pnlCategorias.Location = new System.Drawing.Point(12, 538);
            this.pnlCategorias.Name = "pnlCategorias";
            this.pnlCategorias.Size = new System.Drawing.Size(406, 258);
            this.pnlCategorias.TabIndex = 77;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.label15.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(40)))), ((int)(((byte)(59)))));
            this.label15.Location = new System.Drawing.Point(424, 516);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(150, 19);
            this.label15.TabIndex = 74;
            this.label15.Text = "Productos / Artículos";
            // 
            // pnlProductos
            // 
            this.pnlProductos.AutoScroll = true;
            this.pnlProductos.BackColor = System.Drawing.Color.White;
            this.pnlProductos.Location = new System.Drawing.Point(424, 538);
            this.pnlProductos.Name = "pnlProductos";
            this.pnlProductos.Size = new System.Drawing.Size(852, 258);
            this.pnlProductos.TabIndex = 78;
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
            this.gbxAcciones.Controls.Add(this.label18);
            this.gbxAcciones.Controls.Add(this.label19);
            this.gbxAcciones.Controls.Add(this.label10);
            this.gbxAcciones.Controls.Add(this.label11);
            this.gbxAcciones.Controls.Add(this.label12);
            this.gbxAcciones.Controls.Add(this.label13);
            this.gbxAcciones.Controls.Add(this.label14);
            this.gbxAcciones.Controls.Add(this.label21);
            this.gbxAcciones.Controls.Add(this.label22);
            this.gbxAcciones.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbxAcciones.Location = new System.Drawing.Point(6, 72);
            this.gbxAcciones.Name = "gbxAcciones";
            this.gbxAcciones.Size = new System.Drawing.Size(1277, 46);
            this.gbxAcciones.TabIndex = 65;
            // 
            // btnBuscarFactura
            // 
            this.btnBuscarFactura.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnBuscarFactura.BackgroundImage")));
            this.btnBuscarFactura.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnBuscarFactura.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnBuscarFactura.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnBuscarFactura.Location = new System.Drawing.Point(6, 6);
            this.btnBuscarFactura.Name = "btnBuscarFactura";
            this.btnBuscarFactura.Size = new System.Drawing.Size(120, 35);
            this.btnBuscarFactura.TabIndex = 45;
            this.btnBuscarFactura.Text = "  Buscar factura";
            this.btnBuscarFactura.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnBuscarFactura.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnBuscarFactura.UseVisualStyleBackColor = true;
            this.btnBuscarFactura.Click += new System.EventHandler(this.btnBuscarFactura_Click);
            // 
            // btnBuscarProforma
            // 
            this.btnBuscarProforma.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnBuscarProforma.BackgroundImage")));
            this.btnBuscarProforma.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnBuscarProforma.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnBuscarProforma.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnBuscarProforma.Location = new System.Drawing.Point(132, 6);
            this.btnBuscarProforma.Name = "btnBuscarProforma";
            this.btnBuscarProforma.Size = new System.Drawing.Size(120, 35);
            this.btnBuscarProforma.TabIndex = 58;
            this.btnBuscarProforma.Text = "  Proformas";
            this.btnBuscarProforma.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
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
            this.btnClientes.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClientes.BackgroundImage")));
            this.btnClientes.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnClientes.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnClientes.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClientes.Location = new System.Drawing.Point(265, 6);
            this.btnClientes.Name = "btnClientes";
            this.btnClientes.Size = new System.Drawing.Size(120, 35);
            this.btnClientes.TabIndex = 46;
            this.btnClientes.Text = "  Clientes";
            this.btnClientes.UseVisualStyleBackColor = true;
            this.btnClientes.Click += new System.EventHandler(this.btnClientes_Click);
            // 
            // btnProductos
            // 
            this.btnProductos.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnProductos.BackgroundImage")));
            this.btnProductos.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnProductos.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnProductos.Location = new System.Drawing.Point(388, 6);
            this.btnProductos.Name = "btnProductos";
            this.btnProductos.Size = new System.Drawing.Size(120, 35);
            this.btnProductos.TabIndex = 47;
            this.btnProductos.Text = "  Productos";
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
            this.btnValidacion.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnValidacion.BackgroundImage")));
            this.btnValidacion.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnValidacion.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnValidacion.Location = new System.Drawing.Point(510, 6);
            this.btnValidacion.Name = "btnValidacion";
            this.btnValidacion.Size = new System.Drawing.Size(120, 35);
            this.btnValidacion.TabIndex = 52;
            this.btnValidacion.Text = "  Validar Hacienda";
            this.btnValidacion.UseVisualStyleBackColor = true;
            this.btnValidacion.Click += new System.EventHandler(this.btnValidacion_Click);
            // 
            // btnAbonos
            // 
            this.btnAbonos.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnAbonos.BackgroundImage")));
            this.btnAbonos.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnAbonos.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnAbonos.Location = new System.Drawing.Point(633, 6);
            this.btnAbonos.Name = "btnAbonos";
            this.btnAbonos.Size = new System.Drawing.Size(120, 35);
            this.btnAbonos.TabIndex = 63;
            this.btnAbonos.Text = "  Abonos";
            this.btnAbonos.UseVisualStyleBackColor = true;
            this.btnAbonos.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnEnvioCorreos
            // 
            this.btnEnvioCorreos.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnEnvioCorreos.BackgroundImage")));
            this.btnEnvioCorreos.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnEnvioCorreos.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnEnvioCorreos.Location = new System.Drawing.Point(754, 6);
            this.btnEnvioCorreos.Name = "btnEnvioCorreos";
            this.btnEnvioCorreos.Size = new System.Drawing.Size(120, 35);
            this.btnEnvioCorreos.TabIndex = 100;
            this.btnEnvioCorreos.Text = "  Correo facturas";
            this.btnEnvioCorreos.UseVisualStyleBackColor = true;
            this.btnEnvioCorreos.Click += new System.EventHandler(this.btnEnvioCorreos_Click);
            // 
            // btnReImprimir
            // 
            this.btnReImprimir.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnReImprimir.BackgroundImage")));
            this.btnReImprimir.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnReImprimir.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnReImprimir.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnReImprimir.Location = new System.Drawing.Point(876, 6);
            this.btnReImprimir.Name = "btnReImprimir";
            this.btnReImprimir.Size = new System.Drawing.Size(120, 35);
            this.btnReImprimir.TabIndex = 61;
            this.btnReImprimir.Text = "  Re-imprimir";
            this.btnReImprimir.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnReImprimir.UseVisualStyleBackColor = true;
            this.btnReImprimir.Click += new System.EventHandler(this.btnReImprimir_Click);
            // 
            // sepTool3
            // 
            this.sepTool3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.sepTool3.Location = new System.Drawing.Point(992, 6);
            this.sepTool3.Name = "sepTool3";
            this.sepTool3.Size = new System.Drawing.Size(1, 34);
            this.sepTool3.TabIndex = 101;
            // 
            // btnLimpiarForm
            // 
            this.btnLimpiarForm.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnLimpiarForm.BackgroundImage")));
            this.btnLimpiarForm.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnLimpiarForm.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnLimpiarForm.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnLimpiarForm.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLimpiarForm.Location = new System.Drawing.Point(998, 6);
            this.btnLimpiarForm.Name = "btnLimpiarForm";
            this.btnLimpiarForm.Size = new System.Drawing.Size(120, 35);
            this.btnLimpiarForm.TabIndex = 33;
            this.btnLimpiarForm.Text = "Limpiar";
            this.btnLimpiarForm.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnLimpiarForm.UseVisualStyleBackColor = true;
            this.btnLimpiarForm.Click += new System.EventHandler(this.btnLimpiarForm_Click);
            // 
            // lblProgress
            // 
            this.lblProgress.AutoSize = true;
            this.lblProgress.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lblProgress.ForeColor = System.Drawing.Color.Gray;
            this.lblProgress.Location = new System.Drawing.Point(1118, 9);
            this.lblProgress.Name = "lblProgress";
            this.lblProgress.Size = new System.Drawing.Size(93, 13);
            this.lblProgress.TabIndex = 102;
            this.lblProgress.Text = "Progreso : 0 de 0";
            this.lblProgress.Click += new System.EventHandler(this.lblProgress_Click);
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("Segoe UI", 7.5F);
            this.label20.ForeColor = System.Drawing.Color.Gray;
            this.label20.Location = new System.Drawing.Point(1122, 24);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(131, 12);
            this.label20.TabIndex = 84;
            this.label20.Text = "F2 Cobrar F4 Prod. F5 Cliente";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(0, 0);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(53, 17);
            this.label18.TabIndex = 64;
            this.label18.Text = "Abonos";
            this.label18.Visible = false;
            this.label18.Click += new System.EventHandler(this.label18_Click);
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(0, 0);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(101, 17);
            this.label19.TabIndex = 101;
            this.label19.Text = "Correo Facturas";
            this.label19.Visible = false;
            this.label19.Click += new System.EventHandler(this.label19_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(0, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(51, 17);
            this.label10.TabIndex = 48;
            this.label10.Text = "Limpiar";
            this.label10.Visible = false;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(0, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(81, 17);
            this.label11.TabIndex = 49;
            this.label11.Text = "Documentos";
            this.label11.Visible = false;
            this.label11.Click += new System.EventHandler(this.label11_Click);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(0, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(53, 17);
            this.label12.TabIndex = 50;
            this.label12.Text = "Clientes";
            this.label12.Visible = false;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(0, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(67, 17);
            this.label13.TabIndex = 51;
            this.label13.Text = "Productos";
            this.label13.Visible = false;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(0, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(62, 17);
            this.label14.TabIndex = 53;
            this.label14.Text = "Hacienda";
            this.label14.Visible = false;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(0, 0);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(69, 17);
            this.label21.TabIndex = 59;
            this.label21.Text = "Proformas";
            this.label21.Visible = false;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(0, 0);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(77, 17);
            this.label22.TabIndex = 62;
            this.label22.Text = "Re-imprimir";
            this.label22.Visible = false;
            // 
            // pnlEncabezado
            // 
            this.pnlEncabezado.BackColor = System.Drawing.Color.White;
            this.pnlEncabezado.Controls.Add(this.lblCliente);
            this.pnlEncabezado.Controls.Add(this.txtCliente);
            this.pnlEncabezado.Controls.Add(this.btnBuscarCliente);
            this.pnlEncabezado.Controls.Add(this.txtIdCliente);
            this.pnlEncabezado.Controls.Add(this.label4);
            this.pnlEncabezado.Controls.Add(this.txtTel);
            this.pnlEncabezado.Controls.Add(this.label6);
            this.pnlEncabezado.Controls.Add(this.txtCorreo);
            this.pnlEncabezado.Controls.Add(this.chkEnviar);
            this.pnlEncabezado.Controls.Add(this.label23);
            this.pnlEncabezado.Controls.Add(this.cboActividadEconomica);
            this.pnlEncabezado.Controls.Add(this.chkFacturaElectronica);
            this.pnlEncabezado.Controls.Add(this.chkTiqueteElectronico);
            this.pnlEncabezado.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlEncabezado.Location = new System.Drawing.Point(6, 6);
            this.pnlEncabezado.Name = "pnlEncabezado";
            this.pnlEncabezado.Size = new System.Drawing.Size(1277, 66);
            this.pnlEncabezado.TabIndex = 0;
            // 
            // lblCliente
            // 
            this.lblCliente.AutoSize = true;
            this.lblCliente.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblCliente.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.lblCliente.Location = new System.Drawing.Point(6, 6);
            this.lblCliente.Name = "lblCliente";
            this.lblCliente.Size = new System.Drawing.Size(47, 15);
            this.lblCliente.TabIndex = 56;
            this.lblCliente.Text = "Cliente:";
            // 
            // txtCliente
            // 
            this.txtCliente.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtCliente.Location = new System.Drawing.Point(132, 22);
            this.txtCliente.Name = "txtCliente";
            this.txtCliente.Size = new System.Drawing.Size(280, 24);
            this.txtCliente.TabIndex = 55;
            // 
            // btnBuscarCliente
            // 
            this.btnBuscarCliente.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnBuscarCliente.BackgroundImage")));
            this.btnBuscarCliente.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnBuscarCliente.Location = new System.Drawing.Point(415, 13);
            this.btnBuscarCliente.Name = "btnBuscarCliente";
            this.btnBuscarCliente.Size = new System.Drawing.Size(45, 41);
            this.btnBuscarCliente.TabIndex = 57;
            this.btnBuscarCliente.UseVisualStyleBackColor = true;
            this.btnBuscarCliente.Click += new System.EventHandler(this.btnBuscarCliente_Click_1);
            // 
            // txtIdCliente
            // 
            this.txtIdCliente.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtIdCliente.Location = new System.Drawing.Point(6, 22);
            this.txtIdCliente.Name = "txtIdCliente";
            this.txtIdCliente.Size = new System.Drawing.Size(120, 24);
            this.txtIdCliente.TabIndex = 58;
            this.txtIdCliente.TextChanged += new System.EventHandler(this.txtIdCliente_TextChanged);
            this.txtIdCliente.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtIdCliente_KeyPress);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.label4.Location = new System.Drawing.Point(462, 6);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(55, 15);
            this.label4.TabIndex = 62;
            this.label4.Text = "Teléfono:";
            // 
            // txtTel
            // 
            this.txtTel.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtTel.Location = new System.Drawing.Point(462, 22);
            this.txtTel.Name = "txtTel";
            this.txtTel.ReadOnly = true;
            this.txtTel.Size = new System.Drawing.Size(115, 24);
            this.txtTel.TabIndex = 61;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.label6.Location = new System.Drawing.Point(584, 6);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(46, 15);
            this.label6.TabIndex = 64;
            this.label6.Text = "Correo:";
            // 
            // txtCorreo
            // 
            this.txtCorreo.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtCorreo.Location = new System.Drawing.Point(584, 22);
            this.txtCorreo.Name = "txtCorreo";
            this.txtCorreo.Size = new System.Drawing.Size(190, 24);
            this.txtCorreo.TabIndex = 63;
            // 
            // chkEnviar
            // 
            this.chkEnviar.AutoSize = true;
            this.chkEnviar.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.chkEnviar.Location = new System.Drawing.Point(584, 47);
            this.chkEnviar.Name = "chkEnviar";
            this.chkEnviar.Size = new System.Drawing.Size(116, 19);
            this.chkEnviar.TabIndex = 67;
            this.chkEnviar.Text = "Enviar por correo";
            this.chkEnviar.UseVisualStyleBackColor = true;
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label23.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.label23.Location = new System.Drawing.Point(788, 6);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(60, 15);
            this.label23.TabIndex = 100;
            this.label23.Text = "Actividad:";
            // 
            // cboActividadEconomica
            // 
            this.cboActividadEconomica.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.cboActividadEconomica.FormattingEnabled = true;
            this.cboActividadEconomica.Location = new System.Drawing.Point(788, 22);
            this.cboActividadEconomica.Name = "cboActividadEconomica";
            this.cboActividadEconomica.Size = new System.Drawing.Size(300, 25);
            this.cboActividadEconomica.TabIndex = 99;
            // 
            // chkFacturaElectronica
            // 
            this.chkFacturaElectronica.AutoSize = true;
            this.chkFacturaElectronica.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.chkFacturaElectronica.Location = new System.Drawing.Point(1098, 13);
            this.chkFacturaElectronica.Name = "chkFacturaElectronica";
            this.chkFacturaElectronica.Size = new System.Drawing.Size(126, 19);
            this.chkFacturaElectronica.TabIndex = 79;
            this.chkFacturaElectronica.Text = "Factura Electrónica";
            this.chkFacturaElectronica.UseVisualStyleBackColor = true;
            // 
            // chkTiqueteElectronico
            // 
            this.chkTiqueteElectronico.AutoSize = true;
            this.chkTiqueteElectronico.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.chkTiqueteElectronico.Location = new System.Drawing.Point(1098, 35);
            this.chkTiqueteElectronico.Name = "chkTiqueteElectronico";
            this.chkTiqueteElectronico.Size = new System.Drawing.Size(127, 19);
            this.chkTiqueteElectronico.TabIndex = 76;
            this.chkTiqueteElectronico.Text = "Tiquete Electrónico";
            this.chkTiqueteElectronico.UseVisualStyleBackColor = true;
            // 
            // tabPendientes
            // 
            this.tabPendientes.Controls.Add(this.btnEliminarPendientes);
            this.tabPendientes.Controls.Add(this.gbxPendientes);
            this.tabPendientes.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.tabPendientes.Location = new System.Drawing.Point(4, 26);
            this.tabPendientes.Name = "tabPendientes";
            this.tabPendientes.Padding = new System.Windows.Forms.Padding(6);
            this.tabPendientes.Size = new System.Drawing.Size(1289, 800);
            this.tabPendientes.TabIndex = 1;
            this.tabPendientes.Text = "Pendientes";
            this.tabPendientes.UseVisualStyleBackColor = true;
            // 
            // btnEliminarPendientes
            // 
            this.btnEliminarPendientes.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(65)))), ((int)(((byte)(12)))));
            this.btnEliminarPendientes.FlatAppearance.BorderSize = 0;
            this.btnEliminarPendientes.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEliminarPendientes.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.btnEliminarPendientes.ForeColor = System.Drawing.Color.White;
            this.btnEliminarPendientes.Location = new System.Drawing.Point(9, 7);
            this.btnEliminarPendientes.Name = "btnEliminarPendientes";
            this.btnEliminarPendientes.Size = new System.Drawing.Size(181, 41);
            this.btnEliminarPendientes.TabIndex = 20;
            this.btnEliminarPendientes.Text = "Eliminar Pendientes";
            this.btnEliminarPendientes.UseVisualStyleBackColor = false;
            this.btnEliminarPendientes.Click += new System.EventHandler(this.btnEliminarPendientes_Click);
            // 
            // gbxPendientes
            // 
            this.gbxPendientes.BackColor = System.Drawing.Color.White;
            this.gbxPendientes.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.gbxPendientes.Location = new System.Drawing.Point(9, 55);
            this.gbxPendientes.Name = "gbxPendientes";
            this.gbxPendientes.Padding = new System.Windows.Forms.Padding(4);
            this.gbxPendientes.Size = new System.Drawing.Size(1266, 728);
            this.gbxPendientes.TabIndex = 19;
            this.gbxPendientes.TabStop = false;
            this.gbxPendientes.Enter += new System.EventHandler(this.gbxPendientes_Enter);
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
            // frmFacturacion
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(239)))), ((int)(((byte)(232)))));
            this.ClientSize = new System.Drawing.Size(1297, 830);
            this.Controls.Add(this.tabFacturacion);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(32)))), ((int)(((byte)(40)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.Name = "frmFacturacion";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Facturación";
            this.Load += new System.EventHandler(this.frmFacturacion_Load);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.frmFacturacion_KeyUp);
            this.tabFacturacion.ResumeLayout(false);
            this.tabFact.ResumeLayout(false);
            this.tabFact.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvDetalleFactura)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.gbxMontos.ResumeLayout(false);
            this.gbxMontos.PerformLayout();
            this.gbxAcciones.ResumeLayout(false);
            this.gbxAcciones.PerformLayout();
            this.pnlEncabezado.ResumeLayout(false);
            this.pnlEncabezado.PerformLayout();
            this.tabPendientes.ResumeLayout(false);
            this.contextPrecios.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabFacturacion;
        private System.Windows.Forms.TabPage tabFact;
        private System.Windows.Forms.CheckBox chkEnviar;
        private System.Windows.Forms.Panel gbxAcciones;
        private System.Windows.Forms.Panel sepTool1;
        private System.Windows.Forms.Panel sepTool2;
        private System.Windows.Forms.Panel sepTool3;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Button btnReImprimir;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Button btnBuscarProforma;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Button btnValidacion;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button btnProductos;
        private System.Windows.Forms.Button btnClientes;
        private System.Windows.Forms.Button btnBuscarFactura;
        private System.Windows.Forms.Button btnLimpiarForm;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtCorreo;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtTel;
        private System.Windows.Forms.TextBox txtIdCliente;
        private System.Windows.Forms.Button btnBuscarCliente;
        private System.Windows.Forms.Label lblCliente;
        private System.Windows.Forms.TextBox txtCliente;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.Label lblCantidadLineas;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblTotalProducto;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtCodigo;
        private System.Windows.Forms.DataGridView dtgvDetalleFactura;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TextBox txtObservaciones;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TabPage tabPendientes;
        private System.Windows.Forms.GroupBox gbxPendientes;
        private System.Windows.Forms.Label lblAlias;
        private System.Windows.Forms.Label lblPendientes;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.CheckBox chkServicioMesa;
        private System.Windows.Forms.Button btnEliminarPendientes;
        private System.Windows.Forms.CheckBox chkTiqueteElectronico;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Button btnAbonos;
        private System.Windows.Forms.ContextMenuStrip contextPrecios;
        private System.Windows.Forms.ToolStripMenuItem precio1;
        private System.Windows.Forms.ToolStripMenuItem precio2;
        private System.Windows.Forms.ToolStripMenuItem precio3;
        private System.Windows.Forms.LinkLabel lkEliminarPendiente;
        private System.Windows.Forms.CheckBox chkComandas;
        private System.Windows.Forms.Panel pnlCategorias;
        private System.Windows.Forms.Panel pnlProductos;
        private System.Windows.Forms.CheckBox chkFacturaElectronica;
        private System.Windows.Forms.GroupBox gbxMontos;
        private System.Windows.Forms.TextBox txtServicioMesa;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox txtSub;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Button btnDividir;
        private System.Windows.Forms.Button btnPendiente;
        private System.Windows.Forms.TextBox txtExoneracion;
        private System.Windows.Forms.Label label1;
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
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label lblProgress;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Button btnEnvioCorreos;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNom;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPrec;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCant;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDes;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSubTot;
        private System.Windows.Forms.DataGridViewTextBoxColumn colId;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPrecioVenta;
        private System.Windows.Forms.DataGridViewLinkColumn colEliminar;
        private System.Windows.Forms.ComboBox cboActividadEconomica;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Panel pnlEncabezado;
    }
}