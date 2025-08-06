namespace PresentationLayer
{
    partial class frmOrdenesCompra
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmOrdenesCompra));
            this.gbxCompras = new System.Windows.Forms.GroupBox();
            this.gbxDetalleCompra = new System.Windows.Forms.GroupBox();
            this.btnBuscarProducto = new System.Windows.Forms.Button();
            this.label16 = new System.Windows.Forms.Label();
            this.txtCodigoProducto = new System.Windows.Forms.TextBox();
            this.gbxCompra = new System.Windows.Forms.GroupBox();
            this.mskPlazoCredito = new System.Windows.Forms.MaskedTextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.cboTipoVenta = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.chkCorreo = new System.Windows.Forms.CheckBox();
            this.txtCorreo = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtObservaciones = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.btnBuscarProveedor = new System.Windows.Forms.Button();
            this.txtProveedor = new System.Windows.Forms.TextBox();
            this.dtpFechaCompra = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.tlsMenu = new System.Windows.Forms.ToolStrip();
            this.tlsBtnGuardar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tlsBtnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tlsBtnModificar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tlsBtnEliminar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.tlsBtnBuscar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.tlsBtnCancelar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.tlsReporte = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.tlsBtnSalir = new System.Windows.Forms.ToolStripButton();
            this.dtgvCompras = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNom = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMedida = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCant = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPrecio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEliminar = new System.Windows.Forms.DataGridViewLinkColumn();
            this.label2 = new System.Windows.Forms.Label();
            this.txtTotal = new System.Windows.Forms.TextBox();
            this.btnImportar = new System.Windows.Forms.Button();
            this.gbxCompras.SuspendLayout();
            this.gbxDetalleCompra.SuspendLayout();
            this.gbxCompra.SuspendLayout();
            this.tlsMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvCompras)).BeginInit();
            this.SuspendLayout();
            // 
            // gbxCompras
            // 
            this.gbxCompras.Controls.Add(this.btnImportar);
            this.gbxCompras.Controls.Add(this.gbxDetalleCompra);
            this.gbxCompras.Controls.Add(this.gbxCompra);
            this.gbxCompras.Location = new System.Drawing.Point(12, 55);
            this.gbxCompras.Name = "gbxCompras";
            this.gbxCompras.Size = new System.Drawing.Size(1066, 313);
            this.gbxCompras.TabIndex = 115;
            this.gbxCompras.TabStop = false;
            this.gbxCompras.Text = "Orden de Compra";
            // 
            // gbxDetalleCompra
            // 
            this.gbxDetalleCompra.Controls.Add(this.btnBuscarProducto);
            this.gbxDetalleCompra.Controls.Add(this.label16);
            this.gbxDetalleCompra.Controls.Add(this.txtCodigoProducto);
            this.gbxDetalleCompra.Location = new System.Drawing.Point(5, 239);
            this.gbxDetalleCompra.Name = "gbxDetalleCompra";
            this.gbxDetalleCompra.Size = new System.Drawing.Size(860, 64);
            this.gbxDetalleCompra.TabIndex = 111;
            this.gbxDetalleCompra.TabStop = false;
            this.gbxDetalleCompra.Text = "Detalle de Orden de Compra";
            // 
            // btnBuscarProducto
            // 
            this.btnBuscarProducto.Image = ((System.Drawing.Image)(resources.GetObject("btnBuscarProducto.Image")));
            this.btnBuscarProducto.Location = new System.Drawing.Point(502, 14);
            this.btnBuscarProducto.Name = "btnBuscarProducto";
            this.btnBuscarProducto.Size = new System.Drawing.Size(35, 29);
            this.btnBuscarProducto.TabIndex = 15;
            this.btnBuscarProducto.UseVisualStyleBackColor = true;
            this.btnBuscarProducto.Click += new System.EventHandler(this.btnBuscarProducto_Click);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(16, 22);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(89, 13);
            this.label16.TabIndex = 95;
            this.label16.Text = "Código Producto:";
            // 
            // txtCodigoProducto
            // 
            this.txtCodigoProducto.Location = new System.Drawing.Point(112, 19);
            this.txtCodigoProducto.Margin = new System.Windows.Forms.Padding(4);
            this.txtCodigoProducto.MaxLength = 50;
            this.txtCodigoProducto.Name = "txtCodigoProducto";
            this.txtCodigoProducto.ReadOnly = true;
            this.txtCodigoProducto.Size = new System.Drawing.Size(383, 20);
            this.txtCodigoProducto.TabIndex = 14;
            this.txtCodigoProducto.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCodigoProducto_KeyPress);
            // 
            // gbxCompra
            // 
            this.gbxCompra.Controls.Add(this.mskPlazoCredito);
            this.gbxCompra.Controls.Add(this.label14);
            this.gbxCompra.Controls.Add(this.cboTipoVenta);
            this.gbxCompra.Controls.Add(this.label10);
            this.gbxCompra.Controls.Add(this.label8);
            this.gbxCompra.Controls.Add(this.chkCorreo);
            this.gbxCompra.Controls.Add(this.txtCorreo);
            this.gbxCompra.Controls.Add(this.label1);
            this.gbxCompra.Controls.Add(this.label5);
            this.gbxCompra.Controls.Add(this.txtObservaciones);
            this.gbxCompra.Controls.Add(this.label7);
            this.gbxCompra.Controls.Add(this.btnBuscarProveedor);
            this.gbxCompra.Controls.Add(this.txtProveedor);
            this.gbxCompra.Controls.Add(this.dtpFechaCompra);
            this.gbxCompra.Controls.Add(this.label4);
            this.gbxCompra.Location = new System.Drawing.Point(8, 22);
            this.gbxCompra.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gbxCompra.Name = "gbxCompra";
            this.gbxCompra.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gbxCompra.Size = new System.Drawing.Size(857, 201);
            this.gbxCompra.TabIndex = 82;
            this.gbxCompra.TabStop = false;
            this.gbxCompra.Text = "Datos de Orden";
            // 
            // mskPlazoCredito
            // 
            this.mskPlazoCredito.Location = new System.Drawing.Point(558, 73);
            this.mskPlazoCredito.Mask = "###";
            this.mskPlazoCredito.Name = "mskPlazoCredito";
            this.mskPlazoCredito.Size = new System.Drawing.Size(57, 20);
            this.mskPlazoCredito.TabIndex = 120;
            this.mskPlazoCredito.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(621, 76);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(31, 13);
            this.label14.TabIndex = 122;
            this.label14.Text = "días.";
            // 
            // cboTipoVenta
            // 
            this.cboTipoVenta.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTipoVenta.FormattingEnabled = true;
            this.cboTipoVenta.Location = new System.Drawing.Point(558, 46);
            this.cboTipoVenta.Name = "cboTipoVenta";
            this.cboTipoVenta.Size = new System.Drawing.Size(174, 21);
            this.cboTipoVenta.TabIndex = 118;
            this.cboTipoVenta.SelectedIndexChanged += new System.EventHandler(this.cboTipoVenta_SelectedIndexChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(482, 50);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(70, 13);
            this.label10.TabIndex = 119;
            this.label10.Text = "Tipo Compra:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(516, 76);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(36, 13);
            this.label8.TabIndex = 121;
            this.label8.Text = "Plazo:";
            // 
            // chkCorreo
            // 
            this.chkCorreo.AutoSize = true;
            this.chkCorreo.Location = new System.Drawing.Point(415, 49);
            this.chkCorreo.Name = "chkCorreo";
            this.chkCorreo.Size = new System.Drawing.Size(65, 17);
            this.chkCorreo.TabIndex = 117;
            this.chkCorreo.Text = "Notificar";
            this.chkCorreo.UseVisualStyleBackColor = true;
            // 
            // txtCorreo
            // 
            this.txtCorreo.Location = new System.Drawing.Point(116, 45);
            this.txtCorreo.Margin = new System.Windows.Forms.Padding(4);
            this.txtCorreo.MaxLength = 50;
            this.txtCorreo.Name = "txtCorreo";
            this.txtCorreo.Size = new System.Drawing.Size(291, 20);
            this.txtCorreo.TabIndex = 116;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 49);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 13);
            this.label1.TabIndex = 115;
            this.label1.Text = "Correo Electónico:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(28, 110);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(81, 13);
            this.label5.TabIndex = 114;
            this.label5.Text = "Observaciones:";
            // 
            // txtObservaciones
            // 
            this.txtObservaciones.Location = new System.Drawing.Point(116, 107);
            this.txtObservaciones.Margin = new System.Windows.Forms.Padding(4);
            this.txtObservaciones.MaxLength = 500;
            this.txtObservaciones.Multiline = true;
            this.txtObservaciones.Name = "txtObservaciones";
            this.txtObservaciones.Size = new System.Drawing.Size(700, 75);
            this.txtObservaciones.TabIndex = 13;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(50, 22);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(59, 13);
            this.label7.TabIndex = 110;
            this.label7.Text = "Proveedor:";
            // 
            // btnBuscarProveedor
            // 
            this.btnBuscarProveedor.Image = ((System.Drawing.Image)(resources.GetObject("btnBuscarProveedor.Image")));
            this.btnBuscarProveedor.Location = new System.Drawing.Point(411, 10);
            this.btnBuscarProveedor.Name = "btnBuscarProveedor";
            this.btnBuscarProveedor.Size = new System.Drawing.Size(33, 33);
            this.btnBuscarProveedor.TabIndex = 5;
            this.btnBuscarProveedor.UseVisualStyleBackColor = true;
            this.btnBuscarProveedor.Click += new System.EventHandler(this.btnBuscarProveedor_Click);
            // 
            // txtProveedor
            // 
            this.txtProveedor.Location = new System.Drawing.Point(116, 19);
            this.txtProveedor.Margin = new System.Windows.Forms.Padding(4);
            this.txtProveedor.Name = "txtProveedor";
            this.txtProveedor.ReadOnly = true;
            this.txtProveedor.Size = new System.Drawing.Size(291, 20);
            this.txtProveedor.TabIndex = 4;
            // 
            // dtpFechaCompra
            // 
            this.dtpFechaCompra.CustomFormat = "dd/MM/yyyy HH:mm:ss ";
            this.dtpFechaCompra.Enabled = false;
            this.dtpFechaCompra.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaCompra.Location = new System.Drawing.Point(558, 20);
            this.dtpFechaCompra.Margin = new System.Windows.Forms.Padding(4);
            this.dtpFechaCompra.Name = "dtpFechaCompra";
            this.dtpFechaCompra.Size = new System.Drawing.Size(174, 20);
            this.dtpFechaCompra.TabIndex = 12;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(511, 24);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(40, 13);
            this.label4.TabIndex = 70;
            this.label4.Text = "Fecha:";
            // 
            // tlsMenu
            // 
            this.tlsMenu.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.tlsMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tlsBtnGuardar,
            this.toolStripSeparator1,
            this.tlsBtnNuevo,
            this.toolStripSeparator2,
            this.tlsBtnModificar,
            this.toolStripSeparator3,
            this.tlsBtnEliminar,
            this.toolStripSeparator4,
            this.tlsBtnBuscar,
            this.toolStripSeparator5,
            this.tlsBtnCancelar,
            this.toolStripSeparator6,
            this.tlsReporte,
            this.toolStripSeparator7,
            this.tlsBtnSalir});
            this.tlsMenu.Location = new System.Drawing.Point(0, 0);
            this.tlsMenu.Name = "tlsMenu";
            this.tlsMenu.Size = new System.Drawing.Size(1090, 39);
            this.tlsMenu.TabIndex = 117;
            this.tlsMenu.Text = "tlsMenu";
            this.tlsMenu.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.tlsMenu_ItemClicked);
            // 
            // tlsBtnGuardar
            // 
            this.tlsBtnGuardar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tlsBtnGuardar.Image = ((System.Drawing.Image)(resources.GetObject("tlsBtnGuardar.Image")));
            this.tlsBtnGuardar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlsBtnGuardar.MergeIndex = 1;
            this.tlsBtnGuardar.Name = "tlsBtnGuardar";
            this.tlsBtnGuardar.Size = new System.Drawing.Size(36, 36);
            this.tlsBtnGuardar.Text = "Guardar";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 39);
            // 
            // tlsBtnNuevo
            // 
            this.tlsBtnNuevo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tlsBtnNuevo.Image = ((System.Drawing.Image)(resources.GetObject("tlsBtnNuevo.Image")));
            this.tlsBtnNuevo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlsBtnNuevo.MergeIndex = 2;
            this.tlsBtnNuevo.Name = "tlsBtnNuevo";
            this.tlsBtnNuevo.Size = new System.Drawing.Size(36, 36);
            this.tlsBtnNuevo.Text = "Nuevo";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 39);
            // 
            // tlsBtnModificar
            // 
            this.tlsBtnModificar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tlsBtnModificar.Image = ((System.Drawing.Image)(resources.GetObject("tlsBtnModificar.Image")));
            this.tlsBtnModificar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlsBtnModificar.MergeIndex = 3;
            this.tlsBtnModificar.Name = "tlsBtnModificar";
            this.tlsBtnModificar.Size = new System.Drawing.Size(36, 36);
            this.tlsBtnModificar.Text = "Modificar";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 39);
            // 
            // tlsBtnEliminar
            // 
            this.tlsBtnEliminar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tlsBtnEliminar.Image = ((System.Drawing.Image)(resources.GetObject("tlsBtnEliminar.Image")));
            this.tlsBtnEliminar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlsBtnEliminar.MergeIndex = 4;
            this.tlsBtnEliminar.Name = "tlsBtnEliminar";
            this.tlsBtnEliminar.Size = new System.Drawing.Size(36, 36);
            this.tlsBtnEliminar.Text = "Eliminar";
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 39);
            // 
            // tlsBtnBuscar
            // 
            this.tlsBtnBuscar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tlsBtnBuscar.Image = ((System.Drawing.Image)(resources.GetObject("tlsBtnBuscar.Image")));
            this.tlsBtnBuscar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlsBtnBuscar.MergeIndex = 5;
            this.tlsBtnBuscar.Name = "tlsBtnBuscar";
            this.tlsBtnBuscar.Size = new System.Drawing.Size(36, 36);
            this.tlsBtnBuscar.Text = "Buscar";
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 39);
            // 
            // tlsBtnCancelar
            // 
            this.tlsBtnCancelar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tlsBtnCancelar.Image = ((System.Drawing.Image)(resources.GetObject("tlsBtnCancelar.Image")));
            this.tlsBtnCancelar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlsBtnCancelar.MergeIndex = 6;
            this.tlsBtnCancelar.Name = "tlsBtnCancelar";
            this.tlsBtnCancelar.Size = new System.Drawing.Size(36, 36);
            this.tlsBtnCancelar.Text = "Cancelar";
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(6, 39);
            // 
            // tlsReporte
            // 
            this.tlsReporte.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tlsReporte.Image = ((System.Drawing.Image)(resources.GetObject("tlsReporte.Image")));
            this.tlsReporte.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlsReporte.Name = "tlsReporte";
            this.tlsReporte.Size = new System.Drawing.Size(36, 36);
            this.tlsReporte.Text = "Reporte";
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(6, 39);
            // 
            // tlsBtnSalir
            // 
            this.tlsBtnSalir.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tlsBtnSalir.Image = ((System.Drawing.Image)(resources.GetObject("tlsBtnSalir.Image")));
            this.tlsBtnSalir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlsBtnSalir.MergeIndex = 7;
            this.tlsBtnSalir.Name = "tlsBtnSalir";
            this.tlsBtnSalir.Size = new System.Drawing.Size(36, 36);
            this.tlsBtnSalir.Text = "Salir";
            // 
            // dtgvCompras
            // 
            this.dtgvCompras.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dtgvCompras.BackgroundColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Arial", 7F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dtgvCompras.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dtgvCompras.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgvCompras.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.colId,
            this.colNom,
            this.colMedida,
            this.colCant,
            this.colPrecio,
            this.colTotal,
            this.colEliminar});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.Padding = new System.Windows.Forms.Padding(0, 4, 0, 4);
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dtgvCompras.DefaultCellStyle = dataGridViewCellStyle3;
            this.dtgvCompras.EnableHeadersVisualStyles = false;
            this.dtgvCompras.GridColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.dtgvCompras.Location = new System.Drawing.Point(12, 374);
            this.dtgvCompras.MultiSelect = false;
            this.dtgvCompras.Name = "dtgvCompras";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dtgvCompras.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dtgvCompras.RowHeadersVisible = false;
            this.dtgvCompras.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.dtgvCompras.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dtgvCompras.Size = new System.Drawing.Size(1066, 292);
            this.dtgvCompras.TabIndex = 118;
            this.dtgvCompras.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dtgvCompras_CellContentClick);
            this.dtgvCompras.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dtgvCompras_CellValueChanged);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.Frozen = true;
            this.dataGridViewTextBoxColumn1.HeaderText = "Linea";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Width = 30;
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
            this.colNom.Width = 500;
            // 
            // colMedida
            // 
            this.colMedida.Frozen = true;
            this.colMedida.HeaderText = "Medida";
            this.colMedida.Name = "colMedida";
            this.colMedida.ReadOnly = true;
            // 
            // colCant
            // 
            this.colCant.FillWeight = 10.15228F;
            this.colCant.Frozen = true;
            this.colCant.HeaderText = "Cant";
            this.colCant.Name = "colCant";
            this.colCant.Width = 45;
            // 
            // colPrecio
            // 
            this.colPrecio.Frozen = true;
            this.colPrecio.HeaderText = "Precio";
            this.colPrecio.Name = "colPrecio";
            this.colPrecio.ReadOnly = true;
            // 
            // colTotal
            // 
            this.colTotal.Frozen = true;
            this.colTotal.HeaderText = "Total Línea";
            this.colTotal.Name = "colTotal";
            this.colTotal.ReadOnly = true;
            // 
            // colEliminar
            // 
            this.colEliminar.Frozen = true;
            this.colEliminar.HeaderText = "";
            this.colEliminar.Name = "colEliminar";
            this.colEliminar.Text = "Eliminar";
            this.colEliminar.ToolTipText = "Eliminar linea";
            this.colEliminar.UseColumnTextForLinkValue = true;
            this.colEliminar.Width = 85;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(727, 676);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(137, 16);
            this.label2.TabIndex = 124;
            this.label2.Text = "Total de Proforma:";
            // 
            // txtTotal
            // 
            this.txtTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotal.Location = new System.Drawing.Point(871, 673);
            this.txtTotal.Margin = new System.Windows.Forms.Padding(4);
            this.txtTotal.Name = "txtTotal";
            this.txtTotal.ReadOnly = true;
            this.txtTotal.Size = new System.Drawing.Size(206, 22);
            this.txtTotal.TabIndex = 123;
            // 
            // btnImportar
            // 
            this.btnImportar.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnImportar.BackgroundImage")));
            this.btnImportar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnImportar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnImportar.Location = new System.Drawing.Point(871, 27);
            this.btnImportar.Name = "btnImportar";
            this.btnImportar.Size = new System.Drawing.Size(95, 62);
            this.btnImportar.TabIndex = 112;
            this.btnImportar.Text = "Importar Documento";
            this.btnImportar.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnImportar.UseVisualStyleBackColor = true;
            this.btnImportar.Click += new System.EventHandler(this.btnImportar_Click);
            // 
            // frmOrdenesCompra
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(1090, 722);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dtgvCompras);
            this.Controls.Add(this.tlsMenu);
            this.Controls.Add(this.txtTotal);
            this.Controls.Add(this.gbxCompras);
            this.KeyPreview = true;
            this.Name = "frmOrdenesCompra";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Proceso:Ordenes de Compra";
            this.Load += new System.EventHandler(this.frmOrdenesCompra_Load);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.frmOrdenesCompra_KeyUp);
            this.gbxCompras.ResumeLayout(false);
            this.gbxDetalleCompra.ResumeLayout(false);
            this.gbxDetalleCompra.PerformLayout();
            this.gbxCompra.ResumeLayout(false);
            this.gbxCompra.PerformLayout();
            this.tlsMenu.ResumeLayout(false);
            this.tlsMenu.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvCompras)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.GroupBox gbxCompras;
        private System.Windows.Forms.GroupBox gbxDetalleCompra;
        private System.Windows.Forms.Button btnBuscarProducto;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox txtCodigoProducto;
        private System.Windows.Forms.GroupBox gbxCompra;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtObservaciones;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnBuscarProveedor;
        private System.Windows.Forms.TextBox txtProveedor;
        private System.Windows.Forms.DateTimePicker dtpFechaCompra;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ToolStrip tlsMenu;
        private System.Windows.Forms.ToolStripButton tlsBtnGuardar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tlsBtnNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton tlsBtnModificar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton tlsBtnEliminar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton tlsBtnBuscar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripButton tlsBtnCancelar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripButton tlsBtnSalir;
        private System.Windows.Forms.DataGridView dtgvCompras;
        private System.Windows.Forms.TextBox txtCorreo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chkCorreo;
        private System.Windows.Forms.ComboBox cboTipoVenta;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.MaskedTextBox mskPlazoCredito;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ToolStripButton tlsReporte;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn colId;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNom;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMedida;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCant;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPrecio;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTotal;
        private System.Windows.Forms.DataGridViewLinkColumn colEliminar;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtTotal;
        private System.Windows.Forms.Button btnImportar;
    }
}