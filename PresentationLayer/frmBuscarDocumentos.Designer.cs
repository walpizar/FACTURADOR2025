namespace PresentationLayer
{
    partial class frmBuscarDocumentos
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmBuscarDocumentos));
            this.lsvFacturas = new System.Windows.Forms.ListView();
            this.colIdFactura = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colNombreTipoDoc = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colId = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colNombre = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colFecha = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colConsecutivo = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.txtCedula = new System.Windows.Forms.TextBox();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtClave = new System.Windows.Forms.TextBox();
            this.txtConsecutivo = new System.Windows.Forms.TextBox();
            this.gbxFechas = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.dtpFin = new System.Windows.Forms.DateTimePicker();
            this.dtpInicio = new System.Windows.Forms.DateTimePicker();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.chkFechas = new System.Windows.Forms.CheckBox();
            this.txtApell1 = new System.Windows.Forms.TextBox();
            this.txtApell2 = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.txtFactura = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.cboTipoDoc = new System.Windows.Forms.ComboBox();
            this.cboEstado = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.btnRefrescar = new System.Windows.Forms.Button();
            this.btnsalir = new System.Windows.Forms.PictureBox();
            this.gbxFechas.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnsalir)).BeginInit();
            this.SuspendLayout();
            // 
            // lsvFacturas
            // 
            this.lsvFacturas.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colIdFactura,
            this.colNombreTipoDoc,
            this.colId,
            this.colNombre,
            this.colFecha,
            this.colConsecutivo});
            this.lsvFacturas.FullRowSelect = true;
            this.lsvFacturas.GridLines = true;
            this.lsvFacturas.HideSelection = false;
            this.lsvFacturas.HoverSelection = true;
            this.lsvFacturas.Location = new System.Drawing.Point(7, 138);
            this.lsvFacturas.Name = "lsvFacturas";
            this.lsvFacturas.Size = new System.Drawing.Size(946, 292);
            this.lsvFacturas.TabIndex = 1;
            this.lsvFacturas.UseCompatibleStateImageBehavior = false;
            this.lsvFacturas.View = System.Windows.Forms.View.Details;
            this.lsvFacturas.SelectedIndexChanged += new System.EventHandler(this.lsvFacturas_SelectedIndexChanged);
            this.lsvFacturas.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lsvFacturas_MouseDoubleClick);
            // 
            // colIdFactura
            // 
            this.colIdFactura.Text = "Factura #";
            this.colIdFactura.Width = 70;
            // 
            // colNombreTipoDoc
            // 
            this.colNombreTipoDoc.Text = "Tipo Documento";
            this.colNombreTipoDoc.Width = 140;
            // 
            // colId
            // 
            this.colId.Text = "Cédula";
            this.colId.Width = 100;
            // 
            // colNombre
            // 
            this.colNombre.Text = "Nombre";
            this.colNombre.Width = 350;
            // 
            // colFecha
            // 
            this.colFecha.Text = "Fecha";
            this.colFecha.Width = 130;
            // 
            // colConsecutivo
            // 
            this.colConsecutivo.Text = "Consecutivo";
            this.colConsecutivo.Width = 150;
            // 
            // txtCedula
            // 
            this.txtCedula.Location = new System.Drawing.Point(518, 10);
            this.txtCedula.Name = "txtCedula";
            this.txtCedula.Size = new System.Drawing.Size(283, 20);
            this.txtCedula.TabIndex = 2;
            // 
            // txtNombre
            // 
            this.txtNombre.Location = new System.Drawing.Point(518, 34);
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Size = new System.Drawing.Size(283, 20);
            this.txtNombre.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(399, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(108, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Identificación Cliente:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(425, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Nombre Cliente:";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(63, 87);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Clave:";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(31, 64);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(69, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Consecutivo:";
            // 
            // txtClave
            // 
            this.txtClave.Location = new System.Drawing.Point(108, 83);
            this.txtClave.Name = "txtClave";
            this.txtClave.Size = new System.Drawing.Size(283, 20);
            this.txtClave.TabIndex = 7;
            // 
            // txtConsecutivo
            // 
            this.txtConsecutivo.Location = new System.Drawing.Point(108, 59);
            this.txtConsecutivo.Name = "txtConsecutivo";
            this.txtConsecutivo.Size = new System.Drawing.Size(283, 20);
            this.txtConsecutivo.TabIndex = 6;
            // 
            // gbxFechas
            // 
            this.gbxFechas.Controls.Add(this.label6);
            this.gbxFechas.Controls.Add(this.label5);
            this.gbxFechas.Controls.Add(this.dtpFin);
            this.gbxFechas.Controls.Add(this.dtpInicio);
            this.gbxFechas.Location = new System.Drawing.Point(518, 98);
            this.gbxFechas.Name = "gbxFechas";
            this.gbxFechas.Size = new System.Drawing.Size(283, 37);
            this.gbxFechas.TabIndex = 10;
            this.gbxFechas.TabStop = false;
            this.gbxFechas.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(146, 15);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(15, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "al";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(7, 15);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(27, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "De: ";
            // 
            // dtpFin
            // 
            this.dtpFin.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFin.Location = new System.Drawing.Point(167, 11);
            this.dtpFin.Name = "dtpFin";
            this.dtpFin.Size = new System.Drawing.Size(101, 20);
            this.dtpFin.TabIndex = 2;
            // 
            // dtpInicio
            // 
            this.dtpInicio.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpInicio.Location = new System.Drawing.Point(34, 11);
            this.dtpInicio.Name = "dtpInicio";
            this.dtpInicio.Size = new System.Drawing.Size(108, 20);
            this.dtpInicio.TabIndex = 0;
            // 
            // btnBuscar
            // 
            this.btnBuscar.Image = ((System.Drawing.Image)(resources.GetObject("btnBuscar.Image")));
            this.btnBuscar.Location = new System.Drawing.Point(830, 84);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(58, 51);
            this.btnBuscar.TabIndex = 11;
            this.btnBuscar.UseVisualStyleBackColor = true;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // chkFechas
            // 
            this.chkFechas.AutoSize = true;
            this.chkFechas.Location = new System.Drawing.Point(404, 114);
            this.chkFechas.Margin = new System.Windows.Forms.Padding(2);
            this.chkFechas.Name = "chkFechas";
            this.chkFechas.Size = new System.Drawing.Size(106, 17);
            this.chkFechas.TabIndex = 12;
            this.chkFechas.Text = "Rango de Fecha";
            this.chkFechas.UseVisualStyleBackColor = true;
            this.chkFechas.Visible = false;
            this.chkFechas.CheckedChanged += new System.EventHandler(this.chkFechas_CheckedChanged);
            // 
            // txtApell1
            // 
            this.txtApell1.Location = new System.Drawing.Point(518, 58);
            this.txtApell1.Name = "txtApell1";
            this.txtApell1.Size = new System.Drawing.Size(283, 20);
            this.txtApell1.TabIndex = 13;
            // 
            // txtApell2
            // 
            this.txtApell2.Location = new System.Drawing.Point(518, 81);
            this.txtApell2.Name = "txtApell2";
            this.txtApell2.Size = new System.Drawing.Size(283, 20);
            this.txtApell2.TabIndex = 14;
            this.txtApell2.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(427, 59);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(79, 13);
            this.label7.TabIndex = 15;
            this.label7.Text = "Primer Apellido:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(414, 84);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(93, 13);
            this.label8.TabIndex = 16;
            this.label8.Text = "Segundo Apellido:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(44, 40);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(56, 13);
            this.label9.TabIndex = 18;
            this.label9.Text = "Factura #:";
            // 
            // txtFactura
            // 
            this.txtFactura.Location = new System.Drawing.Point(108, 35);
            this.txtFactura.Name = "txtFactura";
            this.txtFactura.Size = new System.Drawing.Size(283, 20);
            this.txtFactura.TabIndex = 17;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(12, 15);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(89, 13);
            this.label10.TabIndex = 19;
            this.label10.Text = "Tipo Documento:";
            // 
            // cboTipoDoc
            // 
            this.cboTipoDoc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTipoDoc.FormattingEnabled = true;
            this.cboTipoDoc.Location = new System.Drawing.Point(108, 10);
            this.cboTipoDoc.Margin = new System.Windows.Forms.Padding(2);
            this.cboTipoDoc.Name = "cboTipoDoc";
            this.cboTipoDoc.Size = new System.Drawing.Size(283, 21);
            this.cboTipoDoc.TabIndex = 20;
            // 
            // cboEstado
            // 
            this.cboEstado.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboEstado.FormattingEnabled = true;
            this.cboEstado.Location = new System.Drawing.Point(108, 109);
            this.cboEstado.Margin = new System.Windows.Forms.Padding(2);
            this.cboEstado.Name = "cboEstado";
            this.cboEstado.Size = new System.Drawing.Size(283, 21);
            this.cboEstado.TabIndex = 21;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(59, 114);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(43, 13);
            this.label11.TabIndex = 22;
            this.label11.Text = "Estado:";
            // 
            // btnRefrescar
            // 
            this.btnRefrescar.Image = ((System.Drawing.Image)(resources.GetObject("btnRefrescar.Image")));
            this.btnRefrescar.Location = new System.Drawing.Point(894, 85);
            this.btnRefrescar.Name = "btnRefrescar";
            this.btnRefrescar.Size = new System.Drawing.Size(57, 50);
            this.btnRefrescar.TabIndex = 37;
            this.btnRefrescar.UseVisualStyleBackColor = true;
            this.btnRefrescar.Click += new System.EventHandler(this.btnRefrescar_Click);
            // 
            // btnsalir
            // 
            this.btnsalir.Image = ((System.Drawing.Image)(resources.GetObject("btnsalir.Image")));
            this.btnsalir.Location = new System.Drawing.Point(931, 5);
            this.btnsalir.Name = "btnsalir";
            this.btnsalir.Size = new System.Drawing.Size(20, 20);
            this.btnsalir.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btnsalir.TabIndex = 94;
            this.btnsalir.TabStop = false;
            this.btnsalir.Click += new System.EventHandler(this.btnsalir_Click);
            // 
            // frmBuscarDocumentos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSize = true;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(960, 448);
            this.Controls.Add(this.btnsalir);
            this.Controls.Add(this.btnRefrescar);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.cboEstado);
            this.Controls.Add(this.cboTipoDoc);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txtFactura);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtApell2);
            this.Controls.Add(this.txtApell1);
            this.Controls.Add(this.chkFechas);
            this.Controls.Add(this.btnBuscar);
            this.Controls.Add(this.gbxFechas);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtClave);
            this.Controls.Add(this.txtConsecutivo);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtNombre);
            this.Controls.Add(this.txtCedula);
            this.Controls.Add(this.lsvFacturas);
            this.Name = "frmBuscarDocumentos";
            this.Text = "Búsqueda: Documentos Emitidos";
            this.Load += new System.EventHandler(this.frmBuscarFactura_Load);
            this.gbxFechas.ResumeLayout(false);
            this.gbxFechas.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnsalir)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView lsvFacturas;
        private System.Windows.Forms.ColumnHeader colId;
        private System.Windows.Forms.ColumnHeader colNombre;
        private System.Windows.Forms.ColumnHeader colFecha;
        private System.Windows.Forms.ColumnHeader colConsecutivo;
        private System.Windows.Forms.TextBox txtCedula;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtClave;
        private System.Windows.Forms.TextBox txtConsecutivo;
        private System.Windows.Forms.GroupBox gbxFechas;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker dtpFin;
        private System.Windows.Forms.DateTimePicker dtpInicio;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.CheckBox chkFechas;
        private System.Windows.Forms.TextBox txtApell1;
        private System.Windows.Forms.TextBox txtApell2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtFactura;
        private System.Windows.Forms.ColumnHeader colIdFactura;
        private System.Windows.Forms.ColumnHeader colNombreTipoDoc;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox cboTipoDoc;
        private System.Windows.Forms.ComboBox cboEstado;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button btnRefrescar;
        private System.Windows.Forms.PictureBox btnsalir;
    }
}