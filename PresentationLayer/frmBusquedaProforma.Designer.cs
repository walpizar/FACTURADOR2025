namespace PresentationLayer
{
    partial class frmBusquedaProforma
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmBusquedaProforma));
            this.label9 = new System.Windows.Forms.Label();
            this.txtFactura = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtApell2 = new System.Windows.Forms.TextBox();
            this.txtApell1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.txtCedula = new System.Windows.Forms.TextBox();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.lsvFacturas = new System.Windows.Forms.ListView();
            this.colIdFactura = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colId = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colNombre = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colFecha = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colMontoTotal = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lstvProformasGenerales = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(61, 15);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(59, 13);
            this.label9.TabIndex = 32;
            this.label9.Text = "#Proforma:";
            // 
            // txtFactura
            // 
            this.txtFactura.Location = new System.Drawing.Point(122, 12);
            this.txtFactura.Name = "txtFactura";
            this.txtFactura.Size = new System.Drawing.Size(266, 20);
            this.txtFactura.TabIndex = 31;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(402, 37);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(93, 13);
            this.label8.TabIndex = 30;
            this.label8.Text = "Segundo Apellido:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(414, 15);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(79, 13);
            this.label7.TabIndex = 29;
            this.label7.Text = "Primer Apellido:";
            // 
            // txtApell2
            // 
            this.txtApell2.Location = new System.Drawing.Point(499, 34);
            this.txtApell2.Name = "txtApell2";
            this.txtApell2.Size = new System.Drawing.Size(266, 20);
            this.txtApell2.TabIndex = 28;
            // 
            // txtApell1
            // 
            this.txtApell1.Location = new System.Drawing.Point(499, 12);
            this.txtApell1.Name = "txtApell1";
            this.txtApell1.Size = new System.Drawing.Size(266, 20);
            this.txtApell1.TabIndex = 27;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(37, 61);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 13);
            this.label2.TabIndex = 22;
            this.label2.Text = "Nombre Cliente:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(108, 13);
            this.label1.TabIndex = 21;
            this.label1.Text = "Identificación Cliente:";
            // 
            // txtNombre
            // 
            this.txtNombre.Location = new System.Drawing.Point(122, 58);
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Size = new System.Drawing.Size(266, 20);
            this.txtNombre.TabIndex = 20;
            // 
            // txtCedula
            // 
            this.txtCedula.Location = new System.Drawing.Point(122, 35);
            this.txtCedula.Name = "txtCedula";
            this.txtCedula.Size = new System.Drawing.Size(266, 20);
            this.txtCedula.TabIndex = 19;
            // 
            // btnBuscar
            // 
            this.btnBuscar.Image = ((System.Drawing.Image)(resources.GetObject("btnBuscar.Image")));
            this.btnBuscar.Location = new System.Drawing.Point(771, 12);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(55, 52);
            this.btnBuscar.TabIndex = 33;
            this.btnBuscar.UseVisualStyleBackColor = true;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // lsvFacturas
            // 
            this.lsvFacturas.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colIdFactura,
            this.colId,
            this.colNombre,
            this.colFecha,
            this.colMontoTotal});
            this.lsvFacturas.FullRowSelect = true;
            this.lsvFacturas.GridLines = true;
            this.lsvFacturas.HideSelection = false;
            this.lsvFacturas.HoverSelection = true;
            this.lsvFacturas.Location = new System.Drawing.Point(13, 102);
            this.lsvFacturas.Name = "lsvFacturas";
            this.lsvFacturas.Size = new System.Drawing.Size(813, 227);
            this.lsvFacturas.TabIndex = 34;
            this.lsvFacturas.UseCompatibleStateImageBehavior = false;
            this.lsvFacturas.View = System.Windows.Forms.View.Details;
            this.lsvFacturas.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lsvFacturas_MouseDoubleClick);
            // 
            // colIdFactura
            // 
            this.colIdFactura.Text = "Factura #";
            this.colIdFactura.Width = 95;
            // 
            // colId
            // 
            this.colId.Text = "Cédula";
            this.colId.Width = 111;
            // 
            // colNombre
            // 
            this.colNombre.Text = "Nombre";
            this.colNombre.Width = 283;
            // 
            // colFecha
            // 
            this.colFecha.Text = "Fecha";
            this.colFecha.Width = 176;
            // 
            // colMontoTotal
            // 
            this.colMontoTotal.Text = "MontoTotal";
            this.colMontoTotal.Width = 100;
            // 
            // lstvProformasGenerales
            // 
            this.lstvProformasGenerales.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5});
            this.lstvProformasGenerales.FullRowSelect = true;
            this.lstvProformasGenerales.GridLines = true;
            this.lstvProformasGenerales.HideSelection = false;
            this.lstvProformasGenerales.HoverSelection = true;
            this.lstvProformasGenerales.Location = new System.Drawing.Point(13, 351);
            this.lstvProformasGenerales.Name = "lstvProformasGenerales";
            this.lstvProformasGenerales.Size = new System.Drawing.Size(814, 225);
            this.lstvProformasGenerales.TabIndex = 86;
            this.lstvProformasGenerales.UseCompatibleStateImageBehavior = false;
            this.lstvProformasGenerales.View = System.Windows.Forms.View.Details;
            this.lstvProformasGenerales.SelectedIndexChanged += new System.EventHandler(this.lstvProformasGenerales_SelectedIndexChanged);
            this.lstvProformasGenerales.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lstvProformasGenerales_MouseDoubleClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Factura #";
            this.columnHeader1.Width = 95;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Cédula";
            this.columnHeader2.Width = 111;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Nombre";
            this.columnHeader3.Width = 283;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Fecha";
            this.columnHeader4.Width = 176;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "MontoTotal";
            this.columnHeader5.Width = 100;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(12, 86);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(103, 16);
            this.label3.TabIndex = 87;
            this.label3.Text = "PROFORMAS";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(10, 332);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(198, 16);
            this.label4.TabIndex = 88;
            this.label4.Text = "PROFORMAS GENERALES";
            // 
            // frmBusquedaProforma
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(832, 588);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lstvProformasGenerales);
            this.Controls.Add(this.lsvFacturas);
            this.Controls.Add(this.btnBuscar);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txtFactura);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtApell2);
            this.Controls.Add(this.txtApell1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtNombre);
            this.Controls.Add(this.txtCedula);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "frmBusquedaProforma";
            this.Text = "Consulta:Proformas";
            this.Load += new System.EventHandler(this.frmBusquedaProforma_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtFactura;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtApell2;
        private System.Windows.Forms.TextBox txtApell1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.TextBox txtCedula;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.ListView lsvFacturas;
        private System.Windows.Forms.ColumnHeader colIdFactura;
        private System.Windows.Forms.ColumnHeader colId;
        private System.Windows.Forms.ColumnHeader colNombre;
        private System.Windows.Forms.ColumnHeader colFecha;
        private System.Windows.Forms.ColumnHeader colMontoTotal;
        private System.Windows.Forms.ListView lstvProformasGenerales;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
    }
}