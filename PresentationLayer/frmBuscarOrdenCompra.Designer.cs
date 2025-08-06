namespace PresentationLayer
{
    partial class frmBuscarOrdenesCompra
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmBuscarOrdenesCompra));
            this.label1 = new System.Windows.Forms.Label();
            this.txtIdOrden = new System.Windows.Forms.TextBox();
            this.btnseleccionar = new System.Windows.Forms.Button();
            this.lsvbuscar = new System.Windows.Forms.ListView();
            this.colCod = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colFecha = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colProveedor = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colTipoVenta = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colPlazo = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colCantidad = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colEstado = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnBuscar = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.btnBuscarProveedor = new System.Windows.Forms.Button();
            this.txtProveedor = new System.Windows.Forms.TextBox();
            this.cboEstadoOrden = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(29, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 13);
            this.label1.TabIndex = 125;
            this.label1.Text = "#Orden:";
            // 
            // txtIdOrden
            // 
            this.txtIdOrden.Location = new System.Drawing.Point(82, 13);
            this.txtIdOrden.Margin = new System.Windows.Forms.Padding(4);
            this.txtIdOrden.Name = "txtIdOrden";
            this.txtIdOrden.Size = new System.Drawing.Size(212, 20);
            this.txtIdOrden.TabIndex = 124;
            // 
            // btnseleccionar
            // 
            this.btnseleccionar.Location = new System.Drawing.Point(746, 386);
            this.btnseleccionar.Margin = new System.Windows.Forms.Padding(4);
            this.btnseleccionar.Name = "btnseleccionar";
            this.btnseleccionar.Size = new System.Drawing.Size(100, 37);
            this.btnseleccionar.TabIndex = 123;
            this.btnseleccionar.Text = "Seleccionar";
            this.btnseleccionar.UseVisualStyleBackColor = true;
            // 
            // lsvbuscar
            // 
            this.lsvbuscar.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colCod,
            this.colFecha,
            this.colProveedor,
            this.colTipoVenta,
            this.colPlazo,
            this.colCantidad,
            this.colEstado});
            this.lsvbuscar.FullRowSelect = true;
            this.lsvbuscar.GridLines = true;
            this.lsvbuscar.HideSelection = false;
            this.lsvbuscar.HoverSelection = true;
            this.lsvbuscar.Location = new System.Drawing.Point(13, 110);
            this.lsvbuscar.Margin = new System.Windows.Forms.Padding(4);
            this.lsvbuscar.MultiSelect = false;
            this.lsvbuscar.Name = "lsvbuscar";
            this.lsvbuscar.Size = new System.Drawing.Size(833, 268);
            this.lsvbuscar.TabIndex = 122;
            this.lsvbuscar.UseCompatibleStateImageBehavior = false;
            this.lsvbuscar.View = System.Windows.Forms.View.Details;
            this.lsvbuscar.SelectedIndexChanged += new System.EventHandler(this.lsvbuscar_SelectedIndexChanged);
            this.lsvbuscar.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lsvbuscar_MouseDoubleClick);
            // 
            // colCod
            // 
            this.colCod.Text = "#Orden";
            this.colCod.Width = 50;
            // 
            // colFecha
            // 
            this.colFecha.Text = "Fecha";
            this.colFecha.Width = 130;
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
            // colPlazo
            // 
            this.colPlazo.Text = "Plazo";
            this.colPlazo.Width = 40;
            // 
            // colCantidad
            // 
            this.colCantidad.Text = "Cant. Lineas";
            this.colCantidad.Width = 80;
            // 
            // colEstado
            // 
            this.colEstado.Text = "Estado";
            this.colEstado.Width = 80;
            // 
            // btnBuscar
            // 
            this.btnBuscar.Image = ((System.Drawing.Image)(resources.GetObject("btnBuscar.Image")));
            this.btnBuscar.Location = new System.Drawing.Point(792, 38);
            this.btnBuscar.Margin = new System.Windows.Forms.Padding(4);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(54, 51);
            this.btnBuscar.TabIndex = 131;
            this.btnBuscar.UseVisualStyleBackColor = true;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(16, 44);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(59, 13);
            this.label7.TabIndex = 134;
            this.label7.Text = "Proveedor:";
            // 
            // btnBuscarProveedor
            // 
            this.btnBuscarProveedor.Image = ((System.Drawing.Image)(resources.GetObject("btnBuscarProveedor.Image")));
            this.btnBuscarProveedor.Location = new System.Drawing.Point(377, 32);
            this.btnBuscarProveedor.Name = "btnBuscarProveedor";
            this.btnBuscarProveedor.Size = new System.Drawing.Size(33, 33);
            this.btnBuscarProveedor.TabIndex = 133;
            this.btnBuscarProveedor.UseVisualStyleBackColor = true;
            this.btnBuscarProveedor.Click += new System.EventHandler(this.btnBuscarProveedor_Click);
            // 
            // txtProveedor
            // 
            this.txtProveedor.Location = new System.Drawing.Point(82, 41);
            this.txtProveedor.Margin = new System.Windows.Forms.Padding(4);
            this.txtProveedor.Name = "txtProveedor";
            this.txtProveedor.ReadOnly = true;
            this.txtProveedor.Size = new System.Drawing.Size(291, 20);
            this.txtProveedor.TabIndex = 132;
            // 
            // cboEstadoOrden
            // 
            this.cboEstadoOrden.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboEstadoOrden.FormattingEnabled = true;
            this.cboEstadoOrden.Location = new System.Drawing.Point(82, 68);
            this.cboEstadoOrden.Name = "cboEstadoOrden";
            this.cboEstadoOrden.Size = new System.Drawing.Size(174, 21);
            this.cboEstadoOrden.TabIndex = 135;
            this.cboEstadoOrden.SelectedIndexChanged += new System.EventHandler(this.cboEstadoOrden_SelectedIndexChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(1, 71);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(75, 13);
            this.label10.TabIndex = 136;
            this.label10.Text = "Estado Orden:";
            // 
            // frmBuscarOrdenesCompra
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(863, 435);
            this.Controls.Add(this.cboEstadoOrden);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.btnBuscarProveedor);
            this.Controls.Add(this.txtProveedor);
            this.Controls.Add(this.btnBuscar);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtIdOrden);
            this.Controls.Add(this.btnseleccionar);
            this.Controls.Add(this.lsvbuscar);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Name = "frmBuscarOrdenesCompra";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Buscar: Ordenes de Compra";
            this.Load += new System.EventHandler(this.frmBuscarOrdenesCompra_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtIdOrden;
        private System.Windows.Forms.Button btnseleccionar;
        private System.Windows.Forms.ListView lsvbuscar;
        private System.Windows.Forms.ColumnHeader colCod;
        private System.Windows.Forms.ColumnHeader colProveedor;
        private System.Windows.Forms.ColumnHeader colTipoVenta;
        private System.Windows.Forms.ColumnHeader colPlazo;
        private System.Windows.Forms.ColumnHeader colFecha;
        private System.Windows.Forms.ColumnHeader colCantidad;
        private System.Windows.Forms.ColumnHeader colEstado;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnBuscarProveedor;
        private System.Windows.Forms.TextBox txtProveedor;
        private System.Windows.Forms.ComboBox cboEstadoOrden;
        private System.Windows.Forms.Label label10;
    }
}