namespace PresentationLayer
{
    partial class frmBuscarCompras
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmBuscarCompras));
            this.txtNumFact = new System.Windows.Forms.TextBox();
            this.btnseleccionar = new System.Windows.Forms.Button();
            this.lsvbuscar = new System.Windows.Forms.ListView();
            this.colIdF = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colFechaCompra = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colFechaReporte = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colProveedor = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colTipoVenta = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colTipoPago = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colTotal = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label1 = new System.Windows.Forms.Label();
            this.dtpFechaCompra = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.btnBuscarProveedor = new System.Windows.Forms.Button();
            this.txtProveedor = new System.Windows.Forms.TextBox();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.cboTipoVenta = new System.Windows.Forms.ComboBox();
            this.cboTipoPago = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.colTipo = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // txtNumFact
            // 
            this.txtNumFact.Location = new System.Drawing.Point(170, 29);
            this.txtNumFact.Margin = new System.Windows.Forms.Padding(4);
            this.txtNumFact.Name = "txtNumFact";
            this.txtNumFact.Size = new System.Drawing.Size(212, 20);
            this.txtNumFact.TabIndex = 5;
            // 
            // btnseleccionar
            // 
            this.btnseleccionar.Location = new System.Drawing.Point(1008, 400);
            this.btnseleccionar.Margin = new System.Windows.Forms.Padding(4);
            this.btnseleccionar.Name = "btnseleccionar";
            this.btnseleccionar.Size = new System.Drawing.Size(100, 37);
            this.btnseleccionar.TabIndex = 4;
            this.btnseleccionar.Text = "Seleccionar";
            this.btnseleccionar.UseVisualStyleBackColor = true;
            this.btnseleccionar.Click += new System.EventHandler(this.btnseleccionar_Click);
            // 
            // lsvbuscar
            // 
            this.lsvbuscar.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colIdF,
            this.colTipo,
            this.colFechaCompra,
            this.colFechaReporte,
            this.colProveedor,
            this.colTipoVenta,
            this.colTipoPago,
            this.colTotal});
            this.lsvbuscar.FullRowSelect = true;
            this.lsvbuscar.GridLines = true;
            this.lsvbuscar.HideSelection = false;
            this.lsvbuscar.HoverSelection = true;
            this.lsvbuscar.Location = new System.Drawing.Point(16, 149);
            this.lsvbuscar.Margin = new System.Windows.Forms.Padding(4);
            this.lsvbuscar.MultiSelect = false;
            this.lsvbuscar.Name = "lsvbuscar";
            this.lsvbuscar.Size = new System.Drawing.Size(1095, 243);
            this.lsvbuscar.TabIndex = 3;
            this.lsvbuscar.UseCompatibleStateImageBehavior = false;
            this.lsvbuscar.View = System.Windows.Forms.View.Details;
            this.lsvbuscar.SelectedIndexChanged += new System.EventHandler(this.lsvbuscar_SelectedIndexChanged);
            this.lsvbuscar.DoubleClick += new System.EventHandler(this.lsvbuscar_DoubleClick);
            // 
            // colIdF
            // 
            this.colIdF.Text = "#Factura";
            this.colIdF.Width = 71;
            // 
            // colFechaCompra
            // 
            this.colFechaCompra.Text = "Fecha Compra";
            this.colFechaCompra.Width = 150;
            // 
            // colFechaReporte
            // 
            this.colFechaReporte.Text = "Fecha Reporte";
            this.colFechaReporte.Width = 160;
            // 
            // colProveedor
            // 
            this.colProveedor.Text = "Proveedor";
            this.colProveedor.Width = 330;
            // 
            // colTipoVenta
            // 
            this.colTipoVenta.Text = "Tipo Venta";
            this.colTipoVenta.Width = 80;
            // 
            // colTipoPago
            // 
            this.colTipoPago.Text = "Tipo Pago";
            this.colTipoPago.Width = 80;
            // 
            // colTotal
            // 
            this.colTotal.Text = "Total Facturado";
            this.colTotal.Width = 100;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(116, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "#Fact:";
            // 
            // dtpFechaCompra
            // 
            this.dtpFechaCompra.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaCompra.Location = new System.Drawing.Point(170, 56);
            this.dtpFechaCompra.Name = "dtpFechaCompra";
            this.dtpFechaCompra.ShowCheckBox = true;
            this.dtpFechaCompra.Size = new System.Drawing.Size(212, 20);
            this.dtpFechaCompra.TabIndex = 10;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(60, 57);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(79, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Fecha Compra:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(85, 86);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(59, 13);
            this.label7.TabIndex = 113;
            this.label7.Text = "Proveedor:";
            // 
            // btnBuscarProveedor
            // 
            this.btnBuscarProveedor.Location = new System.Drawing.Point(468, 82);
            this.btnBuscarProveedor.Name = "btnBuscarProveedor";
            this.btnBuscarProveedor.Size = new System.Drawing.Size(47, 31);
            this.btnBuscarProveedor.TabIndex = 112;
            this.btnBuscarProveedor.Text = "...";
            this.btnBuscarProveedor.UseVisualStyleBackColor = true;
            this.btnBuscarProveedor.Click += new System.EventHandler(this.btnBuscarProveedor_Click);
            // 
            // txtProveedor
            // 
            this.txtProveedor.Location = new System.Drawing.Point(170, 85);
            this.txtProveedor.Margin = new System.Windows.Forms.Padding(4);
            this.txtProveedor.Name = "txtProveedor";
            this.txtProveedor.ReadOnly = true;
            this.txtProveedor.Size = new System.Drawing.Size(291, 20);
            this.txtProveedor.TabIndex = 111;
            // 
            // btnBuscar
            // 
            this.btnBuscar.Image = ((System.Drawing.Image)(resources.GetObject("btnBuscar.Image")));
            this.btnBuscar.Location = new System.Drawing.Point(988, 62);
            this.btnBuscar.Margin = new System.Windows.Forms.Padding(4);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(73, 64);
            this.btnBuscar.TabIndex = 114;
            this.btnBuscar.UseVisualStyleBackColor = true;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // cboTipoVenta
            // 
            this.cboTipoVenta.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTipoVenta.FormattingEnabled = true;
            this.cboTipoVenta.Location = new System.Drawing.Point(713, 28);
            this.cboTipoVenta.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cboTipoVenta.Name = "cboTipoVenta";
            this.cboTipoVenta.Size = new System.Drawing.Size(193, 21);
            this.cboTipoVenta.TabIndex = 117;
            // 
            // cboTipoPago
            // 
            this.cboTipoPago.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTipoPago.FormattingEnabled = true;
            this.cboTipoPago.Location = new System.Drawing.Point(713, 56);
            this.cboTipoPago.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cboTipoPago.Name = "cboTipoPago";
            this.cboTipoPago.Size = new System.Drawing.Size(193, 21);
            this.cboTipoPago.TabIndex = 119;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(626, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 13);
            this.label2.TabIndex = 120;
            this.label2.Text = "Tipo Compra:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(630, 58);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 13);
            this.label4.TabIndex = 121;
            this.label4.Text = "Tipo Pago:";
            // 
            // colTipo
            // 
            this.colTipo.Text = "Documento";
            this.colTipo.Width = 100;
            // 
            // frmBuscarCompras
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(1121, 450);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cboTipoPago);
            this.Controls.Add(this.cboTipoVenta);
            this.Controls.Add(this.btnBuscar);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.btnBuscarProveedor);
            this.Controls.Add(this.txtProveedor);
            this.Controls.Add(this.dtpFechaCompra);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtNumFact);
            this.Controls.Add(this.btnseleccionar);
            this.Controls.Add(this.lsvbuscar);
            this.Name = "frmBuscarCompras";
            this.Text = "Búsqueda: Compras";
            this.Load += new System.EventHandler(this.frmBuscarCompras_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtNumFact;
        private System.Windows.Forms.Button btnseleccionar;
        private System.Windows.Forms.ListView lsvbuscar;
        private System.Windows.Forms.ColumnHeader colIdF;
        private System.Windows.Forms.ColumnHeader colFechaCompra;
        private System.Windows.Forms.ColumnHeader colFechaReporte;
        private System.Windows.Forms.ColumnHeader colProveedor;
        private System.Windows.Forms.ColumnHeader colTipoVenta;
        private System.Windows.Forms.ColumnHeader colTipoPago;
        private System.Windows.Forms.ColumnHeader colTotal;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpFechaCompra;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnBuscarProveedor;
        private System.Windows.Forms.TextBox txtProveedor;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.ComboBox cboTipoVenta;
        private System.Windows.Forms.ComboBox cboTipoPago;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ColumnHeader colTipo;
    }
}