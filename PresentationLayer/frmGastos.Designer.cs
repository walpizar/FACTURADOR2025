namespace PresentationLayer
{
    partial class frmGastos
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
            System.Windows.Forms.Label label7;
            System.Windows.Forms.Label label10;
            System.Windows.Forms.Label label11;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmGastos));
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.btnCargar = new System.Windows.Forms.Button();
            this.lsvDoc = new System.Windows.Forms.ListView();
            this.colIdFact = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colTipoDoc = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colClave = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colMoneda = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colIdEmisor = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colEmisor = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColFechaEmision = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColImp = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColTotal = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtID = new System.Windows.Forms.TextBox();
            this.txtEmisor = new System.Windows.Forms.TextBox();
            this.txtClave = new System.Windows.Forms.TextBox();
            this.txtFecha = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtCambio = new System.Windows.Forms.TextBox();
            this.txtTipoDoc = new System.Windows.Forms.TextBox();
            this.txtMoneda = new System.Windows.Forms.TextBox();
            this.txtFactura = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtTotal = new System.Windows.Forms.TextBox();
            this.txtImp = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.btnProcesar = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtMensaje = new System.Windows.Forms.TextBox();
            this.chkCambiarColon = new System.Windows.Forms.CheckBox();
            this.btnsalir = new System.Windows.Forms.PictureBox();
            this.label12 = new System.Windows.Forms.Label();
            this.btnCorreo = new System.Windows.Forms.Button();
            this.dtpIncio = new System.Windows.Forms.DateTimePicker();
            this.dtpFin = new System.Windows.Forms.DateTimePicker();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            label7 = new System.Windows.Forms.Label();
            label10 = new System.Windows.Forms.Label();
            label11 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnsalir)).BeginInit();
            this.SuspendLayout();
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new System.Drawing.Point(7, 204);
            label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            label7.Name = "label7";
            label7.Size = new System.Drawing.Size(58, 13);
            label7.TabIndex = 10;
            label7.Text = "Impuestos:";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new System.Drawing.Point(10, 13);
            label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            label10.Name = "label10";
            label10.Size = new System.Drawing.Size(150, 13);
            label10.TabIndex = 10;
            label10.Text = "Últimos Mensajes procesados:";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new System.Drawing.Point(16, 159);
            label11.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            label11.Name = "label11";
            label11.Size = new System.Drawing.Size(49, 13);
            label11.TabIndex = 16;
            label11.Text = "Moneda:";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // btnCargar
            // 
            this.btnCargar.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCargar.Image = ((System.Drawing.Image)(resources.GetObject("btnCargar.Image")));
            this.btnCargar.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnCargar.Location = new System.Drawing.Point(16, 7);
            this.btnCargar.Margin = new System.Windows.Forms.Padding(2);
            this.btnCargar.Name = "btnCargar";
            this.btnCargar.Size = new System.Drawing.Size(100, 54);
            this.btnCargar.TabIndex = 1;
            this.btnCargar.Text = "Cargar Archivos Local";
            this.btnCargar.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnCargar.UseVisualStyleBackColor = true;
            this.btnCargar.Click += new System.EventHandler(this.btnCargar_Click);
            // 
            // lsvDoc
            // 
            this.lsvDoc.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colIdFact,
            this.colTipoDoc,
            this.colClave,
            this.colMoneda,
            this.colIdEmisor,
            this.colEmisor,
            this.ColFechaEmision,
            this.ColImp,
            this.ColTotal});
            this.lsvDoc.FullRowSelect = true;
            this.lsvDoc.GridLines = true;
            this.lsvDoc.HideSelection = false;
            this.lsvDoc.HoverSelection = true;
            this.lsvDoc.Location = new System.Drawing.Point(10, 80);
            this.lsvDoc.MultiSelect = false;
            this.lsvDoc.Name = "lsvDoc";
            this.lsvDoc.Size = new System.Drawing.Size(1136, 192);
            this.lsvDoc.TabIndex = 2;
            this.lsvDoc.UseCompatibleStateImageBehavior = false;
            this.lsvDoc.View = System.Windows.Forms.View.Details;
            this.lsvDoc.SelectedIndexChanged += new System.EventHandler(this.lsvDoc_SelectedIndexChanged);
            this.lsvDoc.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lsvDoc_MouseClick);
            // 
            // colIdFact
            // 
            this.colIdFact.Text = "#Factura";
            this.colIdFact.Width = 70;
            // 
            // colTipoDoc
            // 
            this.colTipoDoc.Text = "Tipo";
            this.colTipoDoc.Width = 100;
            // 
            // colClave
            // 
            this.colClave.Text = "Clave";
            this.colClave.Width = 200;
            // 
            // colMoneda
            // 
            this.colMoneda.Text = "Moneda";
            // 
            // colIdEmisor
            // 
            this.colIdEmisor.Text = "Id Emisor";
            this.colIdEmisor.Width = 120;
            // 
            // colEmisor
            // 
            this.colEmisor.Text = "Emisor";
            this.colEmisor.Width = 250;
            // 
            // ColFechaEmision
            // 
            this.ColFechaEmision.Text = "Fecha Emisión";
            this.ColFechaEmision.Width = 130;
            // 
            // ColImp
            // 
            this.ColImp.Text = "Impuestos";
            this.ColImp.Width = 90;
            // 
            // ColTotal
            // 
            this.ColTotal.Text = "Total";
            this.ColTotal.Width = 90;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(21, 63);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Clave:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(18, 84);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(40, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Fecha:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(40, 110);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(21, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "ID:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(20, 133);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 13);
            this.label6.TabIndex = 10;
            this.label6.Text = "Emisor:";
            // 
            // txtID
            // 
            this.txtID.Location = new System.Drawing.Point(64, 108);
            this.txtID.Margin = new System.Windows.Forms.Padding(2);
            this.txtID.Name = "txtID";
            this.txtID.ReadOnly = true;
            this.txtID.Size = new System.Drawing.Size(281, 20);
            this.txtID.TabIndex = 11;
            // 
            // txtEmisor
            // 
            this.txtEmisor.Location = new System.Drawing.Point(64, 131);
            this.txtEmisor.Margin = new System.Windows.Forms.Padding(2);
            this.txtEmisor.Name = "txtEmisor";
            this.txtEmisor.ReadOnly = true;
            this.txtEmisor.Size = new System.Drawing.Size(281, 20);
            this.txtEmisor.TabIndex = 12;
            // 
            // txtClave
            // 
            this.txtClave.Location = new System.Drawing.Point(64, 62);
            this.txtClave.Margin = new System.Windows.Forms.Padding(2);
            this.txtClave.Name = "txtClave";
            this.txtClave.ReadOnly = true;
            this.txtClave.Size = new System.Drawing.Size(281, 20);
            this.txtClave.TabIndex = 13;
            // 
            // txtFecha
            // 
            this.txtFecha.Location = new System.Drawing.Point(64, 84);
            this.txtFecha.Margin = new System.Windows.Forms.Padding(2);
            this.txtFecha.Name = "txtFecha";
            this.txtFecha.ReadOnly = true;
            this.txtFecha.Size = new System.Drawing.Size(281, 20);
            this.txtFecha.TabIndex = 14;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtCambio);
            this.groupBox1.Controls.Add(this.txtTipoDoc);
            this.groupBox1.Controls.Add(this.txtMoneda);
            this.groupBox1.Controls.Add(this.txtFactura);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(label11);
            this.groupBox1.Controls.Add(this.txtTotal);
            this.groupBox1.Controls.Add(this.txtImp);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.txtFecha);
            this.groupBox1.Controls.Add(label7);
            this.groupBox1.Controls.Add(this.txtClave);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtEmisor);
            this.groupBox1.Controls.Add(this.txtID);
            this.groupBox1.Location = new System.Drawing.Point(14, 281);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(378, 258);
            this.groupBox1.TabIndex = 15;
            this.groupBox1.TabStop = false;
            // 
            // txtCambio
            // 
            this.txtCambio.Location = new System.Drawing.Point(66, 179);
            this.txtCambio.Margin = new System.Windows.Forms.Padding(2);
            this.txtCambio.Name = "txtCambio";
            this.txtCambio.ReadOnly = true;
            this.txtCambio.Size = new System.Drawing.Size(160, 20);
            this.txtCambio.TabIndex = 19;
            // 
            // txtTipoDoc
            // 
            this.txtTipoDoc.Location = new System.Drawing.Point(64, 41);
            this.txtTipoDoc.Margin = new System.Windows.Forms.Padding(2);
            this.txtTipoDoc.Name = "txtTipoDoc";
            this.txtTipoDoc.ReadOnly = true;
            this.txtTipoDoc.Size = new System.Drawing.Size(281, 20);
            this.txtTipoDoc.TabIndex = 22;
            // 
            // txtMoneda
            // 
            this.txtMoneda.Location = new System.Drawing.Point(66, 157);
            this.txtMoneda.Margin = new System.Windows.Forms.Padding(2);
            this.txtMoneda.Name = "txtMoneda";
            this.txtMoneda.ReadOnly = true;
            this.txtMoneda.Size = new System.Drawing.Size(160, 20);
            this.txtMoneda.TabIndex = 18;
            // 
            // txtFactura
            // 
            this.txtFactura.Location = new System.Drawing.Point(64, 18);
            this.txtFactura.Margin = new System.Windows.Forms.Padding(2);
            this.txtFactura.Name = "txtFactura";
            this.txtFactura.ReadOnly = true;
            this.txtFactura.Size = new System.Drawing.Size(281, 20);
            this.txtFactura.TabIndex = 21;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 41);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 13);
            this.label1.TabIndex = 20;
            this.label1.Text = "TipoDoc:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(20, 182);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(45, 13);
            this.label9.TabIndex = 17;
            this.label9.Text = "Cambio:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 20);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 13);
            this.label2.TabIndex = 19;
            this.label2.Text = "Factura:";
            // 
            // txtTotal
            // 
            this.txtTotal.Location = new System.Drawing.Point(66, 224);
            this.txtTotal.Margin = new System.Windows.Forms.Padding(2);
            this.txtTotal.Name = "txtTotal";
            this.txtTotal.ReadOnly = true;
            this.txtTotal.Size = new System.Drawing.Size(160, 20);
            this.txtTotal.TabIndex = 15;
            // 
            // txtImp
            // 
            this.txtImp.Location = new System.Drawing.Point(66, 202);
            this.txtImp.Margin = new System.Windows.Forms.Padding(2);
            this.txtImp.Name = "txtImp";
            this.txtImp.ReadOnly = true;
            this.txtImp.Size = new System.Drawing.Size(160, 20);
            this.txtImp.TabIndex = 14;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(31, 227);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(34, 13);
            this.label8.TabIndex = 11;
            this.label8.Text = "Total:";
            // 
            // btnProcesar
            // 
            this.btnProcesar.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnProcesar.Image = ((System.Drawing.Image)(resources.GetObject("btnProcesar.Image")));
            this.btnProcesar.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnProcesar.Location = new System.Drawing.Point(1052, 516);
            this.btnProcesar.Margin = new System.Windows.Forms.Padding(2);
            this.btnProcesar.Name = "btnProcesar";
            this.btnProcesar.Size = new System.Drawing.Size(79, 61);
            this.btnProcesar.TabIndex = 17;
            this.btnProcesar.Text = "Procesar";
            this.btnProcesar.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnProcesar.UseVisualStyleBackColor = true;
            this.btnProcesar.Click += new System.EventHandler(this.btnProcesar_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtMensaje);
            this.groupBox2.Controls.Add(label10);
            this.groupBox2.Location = new System.Drawing.Point(410, 277);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox2.Size = new System.Drawing.Size(738, 235);
            this.groupBox2.TabIndex = 18;
            this.groupBox2.TabStop = false;
            // 
            // txtMensaje
            // 
            this.txtMensaje.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtMensaje.Location = new System.Drawing.Point(13, 29);
            this.txtMensaje.Margin = new System.Windows.Forms.Padding(2);
            this.txtMensaje.MaxLength = 5000;
            this.txtMensaje.Multiline = true;
            this.txtMensaje.Name = "txtMensaje";
            this.txtMensaje.ReadOnly = true;
            this.txtMensaje.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtMensaje.Size = new System.Drawing.Size(708, 202);
            this.txtMensaje.TabIndex = 19;
            this.txtMensaje.WordWrap = false;
            // 
            // chkCambiarColon
            // 
            this.chkCambiarColon.AutoSize = true;
            this.chkCambiarColon.Location = new System.Drawing.Point(934, 537);
            this.chkCambiarColon.Name = "chkCambiarColon";
            this.chkCambiarColon.Size = new System.Drawing.Size(103, 17);
            this.chkCambiarColon.TabIndex = 23;
            this.chkCambiarColon.Text = "Cambiar a Colón";
            this.chkCambiarColon.UseVisualStyleBackColor = true;
            this.chkCambiarColon.CheckedChanged += new System.EventHandler(this.chkCambiarColon_CheckedChanged);
            // 
            // btnsalir
            // 
            this.btnsalir.Image = ((System.Drawing.Image)(resources.GetObject("btnsalir.Image")));
            this.btnsalir.Location = new System.Drawing.Point(1126, 7);
            this.btnsalir.Name = "btnsalir";
            this.btnsalir.Size = new System.Drawing.Size(20, 20);
            this.btnsalir.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btnsalir.TabIndex = 94;
            this.btnsalir.TabStop = false;
            this.btnsalir.Click += new System.EventHandler(this.btnsalir_Click);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(631, 2);
            this.label12.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(169, 25);
            this.label12.TabIndex = 98;
            this.label12.Text = "Reportar Gastos";
            // 
            // btnCorreo
            // 
            this.btnCorreo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnCorreo.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCorreo.Image = ((System.Drawing.Image)(resources.GetObject("btnCorreo.Image")));
            this.btnCorreo.Location = new System.Drawing.Point(410, 11);
            this.btnCorreo.Margin = new System.Windows.Forms.Padding(2);
            this.btnCorreo.Name = "btnCorreo";
            this.btnCorreo.Size = new System.Drawing.Size(110, 54);
            this.btnCorreo.TabIndex = 99;
            this.btnCorreo.Text = "Cargar desde correo";
            this.btnCorreo.UseVisualStyleBackColor = true;
            this.btnCorreo.Click += new System.EventHandler(this.button1_Click);
            // 
            // dtpIncio
            // 
            this.dtpIncio.Location = new System.Drawing.Point(194, 11);
            this.dtpIncio.Name = "dtpIncio";
            this.dtpIncio.Size = new System.Drawing.Size(200, 20);
            this.dtpIncio.TabIndex = 100;
            // 
            // dtpFin
            // 
            this.dtpFin.Location = new System.Drawing.Point(194, 38);
            this.dtpFin.Name = "dtpFin";
            this.dtpFin.Size = new System.Drawing.Size(200, 20);
            this.dtpFin.TabIndex = 101;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(145, 14);
            this.label13.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(35, 13);
            this.label13.TabIndex = 23;
            this.label13.Text = "Inicio:";
            this.label13.Click += new System.EventHandler(this.label13_Click);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(156, 38);
            this.label14.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(24, 13);
            this.label14.TabIndex = 102;
            this.label14.Text = "Fin:";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(382, 158);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(440, 36);
            this.progressBar1.TabIndex = 103;
            this.progressBar1.Visible = false;
            // 
            // frmGastos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(1159, 583);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.dtpFin);
            this.Controls.Add(this.dtpIncio);
            this.Controls.Add(this.btnCorreo);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.btnsalir);
            this.Controls.Add(this.chkCambiarColon);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btnProcesar);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lsvDoc);
            this.Controls.Add(this.btnCargar);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "frmGastos";
            this.Text = "Reporte de compras a Hacienda";
            this.Load += new System.EventHandler(this.frmMensajesHacienda_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnsalir)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button btnCargar;
        private System.Windows.Forms.ListView lsvDoc;
        private System.Windows.Forms.ColumnHeader colIdFact;
        private System.Windows.Forms.ColumnHeader colIdEmisor;
        private System.Windows.Forms.ColumnHeader colEmisor;
        private System.Windows.Forms.ColumnHeader ColFechaEmision;
        private System.Windows.Forms.ColumnHeader ColImp;
        private System.Windows.Forms.ColumnHeader ColTotal;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtID;
        private System.Windows.Forms.TextBox txtEmisor;
        private System.Windows.Forms.TextBox txtClave;
        private System.Windows.Forms.TextBox txtFecha;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtTotal;
        private System.Windows.Forms.TextBox txtImp;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnProcesar;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtMensaje;
        private System.Windows.Forms.ColumnHeader colTipoDoc;
        private System.Windows.Forms.ColumnHeader colMoneda;
        private System.Windows.Forms.TextBox txtTipoDoc;
        private System.Windows.Forms.TextBox txtFactura;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox chkCambiarColon;
        private System.Windows.Forms.ColumnHeader colClave;
        private System.Windows.Forms.TextBox txtCambio;
        private System.Windows.Forms.TextBox txtMoneda;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.PictureBox btnsalir;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Button btnCorreo;
        private System.Windows.Forms.DateTimePicker dtpIncio;
        private System.Windows.Forms.DateTimePicker dtpFin;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.ProgressBar progressBar1;
    }
}