namespace PresentationLayer
{
    partial class frmAceptacionDocumentos
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private System.Windows.Forms.ToolStrip tlsMenu;
        private System.Windows.Forms.ToolStripButton btnCargarPendientes;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnCargarXml;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripLabel lblFechaDesde;
        private System.Windows.Forms.ToolStripLabel lblFechaHasta;
        private System.Windows.Forms.ToolStripButton btnCargarCorreo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparatorCorreo;
        private System.Windows.Forms.DateTimePicker dtpFechaInicio;
        private System.Windows.Forms.DateTimePicker dtpFechaFin;
        private System.Windows.Forms.ToolStripButton btnMarcarTodos;
        private System.Windows.Forms.ToolStripButton btnDesmarcarTodos;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton btnProcesar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton btnConsultarEstado;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripButton btnSalir;

        private System.Windows.Forms.DataGridView dgvDocumentos;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colSel;
        private System.Windows.Forms.DataGridViewTextBoxColumn colOrigen;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFactura;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTipoDoc;
        private System.Windows.Forms.DataGridViewTextBoxColumn colClave;
        private System.Windows.Forms.DataGridViewTextBoxColumn colProveedor;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFecha;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTotal;
        private System.Windows.Forms.DataGridViewComboBoxColumn colEstado;
        private System.Windows.Forms.DataGridViewTextBoxColumn colRazon;
        private System.Windows.Forms.DataGridViewTextBoxColumn colEstadoHacienda;

        private System.Windows.Forms.Label lblAplicarA;
        private System.Windows.Forms.ComboBox cboEstadoMasivo;
        private System.Windows.Forms.TextBox txtRazonMasiva;
        private System.Windows.Forms.Button btnAplicarAMarcados;

        private System.Windows.Forms.Label lblResumen;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.ProgressBar progressBar1;

        private void InitializeComponent()
        {
            this.tlsMenu = new System.Windows.Forms.ToolStrip();
            this.btnCargarPendientes = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnCargarXml = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.lblFechaDesde = new System.Windows.Forms.ToolStripLabel();
            this.hostFechaInicio = new System.Windows.Forms.DateTimePicker();
            this.lblFechaHasta = new System.Windows.Forms.ToolStripLabel();
            this.hostFechaFin = new System.Windows.Forms.DateTimePicker();
            this.btnCargarCorreo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparatorCorreo = new System.Windows.Forms.ToolStripSeparator();
            this.btnMarcarTodos = new System.Windows.Forms.ToolStripButton();
            this.btnDesmarcarTodos = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.btnProcesar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.btnConsultarEstado = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.btnSalir = new System.Windows.Forms.ToolStripButton();
            this.dgvDocumentos = new System.Windows.Forms.DataGridView();
            this.colSel = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colOrigen = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colFactura = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTipoDoc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colClave = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colProveedor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colFecha = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEstado = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.colRazon = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEstadoHacienda = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblAplicarA = new System.Windows.Forms.Label();
            this.cboEstadoMasivo = new System.Windows.Forms.ComboBox();
            this.txtRazonMasiva = new System.Windows.Forms.TextBox();
            this.btnAplicarAMarcados = new System.Windows.Forms.Button();
            this.lblResumen = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.tlsMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDocumentos)).BeginInit();
            this.SuspendLayout();
            // 
            // tlsMenu
            // 
            this.tlsMenu.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.tlsMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnCargarPendientes,
            this.toolStripSeparator1,
            this.btnCargarXml,
            this.toolStripSeparator2,
            this.lblFechaDesde,
            this.lblFechaHasta,
            this.btnCargarCorreo,
            this.toolStripSeparatorCorreo,
            this.btnMarcarTodos,
            this.btnDesmarcarTodos,
            this.toolStripSeparator3,
            this.btnProcesar,
            this.toolStripSeparator4,
            this.btnConsultarEstado,
            this.toolStripSeparator5,
            this.btnSalir});
            this.tlsMenu.Location = new System.Drawing.Point(0, 0);
            this.tlsMenu.Name = "tlsMenu";
            this.tlsMenu.Size = new System.Drawing.Size(984, 25);
            this.tlsMenu.TabIndex = 0;
            // 
            // btnCargarPendientes
            // 
            this.btnCargarPendientes.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnCargarPendientes.Name = "btnCargarPendientes";
            this.btnCargarPendientes.Size = new System.Drawing.Size(107, 22);
            this.btnCargarPendientes.Text = "Cargar pendientes";
            this.btnCargarPendientes.Click += new System.EventHandler(this.btnCargarPendientes_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // btnCargarXml
            // 
            this.btnCargarXml.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnCargarXml.Name = "btnCargarXml";
            this.btnCargarXml.Size = new System.Drawing.Size(73, 22);
            this.btnCargarXml.Text = "Cargar XML";
            this.btnCargarXml.Click += new System.EventHandler(this.btnCargarXml_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // lblFechaDesde
            // 
            this.lblFechaDesde.Name = "lblFechaDesde";
            this.lblFechaDesde.Size = new System.Drawing.Size(42, 22);
            this.lblFechaDesde.Text = "Desde:";
            // 
            // hostFechaInicio
            // 
            this.hostFechaInicio.AccessibleName = "hostFechaInicio";
            this.hostFechaInicio.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.hostFechaInicio.Location = new System.Drawing.Point(243, 1);
            this.hostFechaInicio.Name = "hostFechaInicio";
            this.hostFechaInicio.Size = new System.Drawing.Size(104, 20);
            this.hostFechaInicio.TabIndex = 0;
            this.hostFechaInicio.Value = new System.DateTime(2026, 8, 12, 0, 0, 0, 0);
            // 
            // lblFechaHasta
            // 
            this.lblFechaHasta.Name = "lblFechaHasta";
            this.lblFechaHasta.Size = new System.Drawing.Size(40, 22);
            this.lblFechaHasta.Text = "Hasta:";
            // 
            // hostFechaFin
            // 
            this.hostFechaFin.AccessibleName = "hostFechaFin";
            this.hostFechaFin.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.hostFechaFin.Location = new System.Drawing.Point(387, 1);
            this.hostFechaFin.Name = "hostFechaFin";
            this.hostFechaFin.Size = new System.Drawing.Size(104, 20);
            this.hostFechaFin.TabIndex = 1;
            this.hostFechaFin.Value = new System.DateTime(2026, 8, 19, 0, 0, 0, 0);
            // 
            // btnCargarCorreo
            // 
            this.btnCargarCorreo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnCargarCorreo.Name = "btnCargarCorreo";
            this.btnCargarCorreo.Size = new System.Drawing.Size(104, 22);
            this.btnCargarCorreo.Text = "Cargar por correo";
            this.btnCargarCorreo.Click += new System.EventHandler(this.btnCargarCorreo_Click);
            // 
            // toolStripSeparatorCorreo
            // 
            this.toolStripSeparatorCorreo.Name = "toolStripSeparatorCorreo";
            this.toolStripSeparatorCorreo.Size = new System.Drawing.Size(6, 25);
            // 
            // btnMarcarTodos
            // 
            this.btnMarcarTodos.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnMarcarTodos.Name = "btnMarcarTodos";
            this.btnMarcarTodos.Size = new System.Drawing.Size(81, 22);
            this.btnMarcarTodos.Text = "Marcar todos";
            this.btnMarcarTodos.Click += new System.EventHandler(this.btnMarcarTodos_Click);
            // 
            // btnDesmarcarTodos
            // 
            this.btnDesmarcarTodos.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnDesmarcarTodos.Name = "btnDesmarcarTodos";
            this.btnDesmarcarTodos.Size = new System.Drawing.Size(100, 22);
            this.btnDesmarcarTodos.Text = "Desmarcar todos";
            this.btnDesmarcarTodos.Click += new System.EventHandler(this.btnDesmarcarTodos_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // btnProcesar
            // 
            this.btnProcesar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnProcesar.Name = "btnProcesar";
            this.btnProcesar.Size = new System.Drawing.Size(133, 22);
            this.btnProcesar.Text = "Procesar seleccionados";
            this.btnProcesar.Click += new System.EventHandler(this.btnProcesar_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // btnConsultarEstado
            // 
            this.btnConsultarEstado.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnConsultarEstado.Name = "btnConsultarEstado";
            this.btnConsultarEstado.Size = new System.Drawing.Size(100, 22);
            this.btnConsultarEstado.Text = "Consultar estado";
            this.btnConsultarEstado.Click += new System.EventHandler(this.btnConsultarEstado_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 25);
            // 
            // btnSalir
            // 
            this.btnSalir.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(33, 22);
            this.btnSalir.Text = "Salir";
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // dgvDocumentos
            // 
            this.dgvDocumentos.AllowUserToAddRows = false;
            this.dgvDocumentos.AllowUserToDeleteRows = false;
            this.dgvDocumentos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colSel,
            this.colOrigen,
            this.colFactura,
            this.colTipoDoc,
            this.colClave,
            this.colProveedor,
            this.colFecha,
            this.colTotal,
            this.colEstado,
            this.colRazon,
            this.colEstadoHacienda});
            this.dgvDocumentos.Location = new System.Drawing.Point(15, 39);
            this.dgvDocumentos.Name = "dgvDocumentos";
            this.dgvDocumentos.RowHeadersVisible = false;
            this.dgvDocumentos.Size = new System.Drawing.Size(960, 421);
            this.dgvDocumentos.TabIndex = 1;
            this.dgvDocumentos.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDocumentos_CellValueChanged);
            this.dgvDocumentos.CurrentCellDirtyStateChanged += new System.EventHandler(this.dgvDocumentos_CurrentCellDirtyStateChanged);
            // 
            // colSel
            // 
            this.colSel.HeaderText = "Sel.";
            this.colSel.Name = "colSel";
            this.colSel.Width = 35;
            // 
            // colOrigen
            // 
            this.colOrigen.HeaderText = "Origen";
            this.colOrigen.Name = "colOrigen";
            this.colOrigen.ReadOnly = true;
            this.colOrigen.Width = 55;
            // 
            // colFactura
            // 
            this.colFactura.HeaderText = "Factura";
            this.colFactura.Name = "colFactura";
            this.colFactura.ReadOnly = true;
            // 
            // colTipoDoc
            // 
            this.colTipoDoc.HeaderText = "Tipo";
            this.colTipoDoc.Name = "colTipoDoc";
            this.colTipoDoc.ReadOnly = true;
            this.colTipoDoc.Width = 90;
            // 
            // colClave
            // 
            this.colClave.HeaderText = "Clave";
            this.colClave.Name = "colClave";
            this.colClave.ReadOnly = true;
            this.colClave.Width = 140;
            // 
            // colProveedor
            // 
            this.colProveedor.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colProveedor.HeaderText = "Proveedor";
            this.colProveedor.Name = "colProveedor";
            this.colProveedor.ReadOnly = true;
            // 
            // colFecha
            // 
            this.colFecha.HeaderText = "Fecha";
            this.colFecha.Name = "colFecha";
            this.colFecha.ReadOnly = true;
            this.colFecha.Width = 110;
            // 
            // colTotal
            // 
            this.colTotal.HeaderText = "Total";
            this.colTotal.Name = "colTotal";
            this.colTotal.ReadOnly = true;
            this.colTotal.Width = 90;
            // 
            // colEstado
            // 
            this.colEstado.HeaderText = "Estado";
            this.colEstado.Name = "colEstado";
            this.colEstado.Width = 130;
            // 
            // colRazon
            // 
            this.colRazon.HeaderText = "Razón / Detalle";
            this.colRazon.Name = "colRazon";
            this.colRazon.Width = 220;
            // 
            // colEstadoHacienda
            // 
            this.colEstadoHacienda.HeaderText = "Estado en Hacienda";
            this.colEstadoHacienda.Name = "colEstadoHacienda";
            this.colEstadoHacienda.ReadOnly = true;
            this.colEstadoHacienda.Width = 140;
            // 
            // lblAplicarA
            // 
            this.lblAplicarA.AutoSize = true;
            this.lblAplicarA.Location = new System.Drawing.Point(12, 480);
            this.lblAplicarA.Name = "lblAplicarA";
            this.lblAplicarA.Size = new System.Drawing.Size(153, 13);
            this.lblAplicarA.TabIndex = 7;
            this.lblAplicarA.Text = "Aplicar a marcados →  Estado:";
            // 
            // cboEstadoMasivo
            // 
            this.cboEstadoMasivo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboEstadoMasivo.Location = new System.Drawing.Point(165, 477);
            this.cboEstadoMasivo.Name = "cboEstadoMasivo";
            this.cboEstadoMasivo.Size = new System.Drawing.Size(150, 21);
            this.cboEstadoMasivo.TabIndex = 2;
            // 
            // txtRazonMasiva
            // 
            this.txtRazonMasiva.Location = new System.Drawing.Point(325, 477);
            this.txtRazonMasiva.Name = "txtRazonMasiva";
            this.txtRazonMasiva.Size = new System.Drawing.Size(400, 20);
            this.txtRazonMasiva.TabIndex = 3;
            // 
            // btnAplicarAMarcados
            // 
            this.btnAplicarAMarcados.Location = new System.Drawing.Point(735, 475);
            this.btnAplicarAMarcados.Name = "btnAplicarAMarcados";
            this.btnAplicarAMarcados.Size = new System.Drawing.Size(120, 25);
            this.btnAplicarAMarcados.TabIndex = 4;
            this.btnAplicarAMarcados.Text = "Aplicar a marcados";
            this.btnAplicarAMarcados.UseVisualStyleBackColor = true;
            this.btnAplicarAMarcados.Click += new System.EventHandler(this.btnAplicarAMarcados_Click);
            // 
            // lblResumen
            // 
            this.lblResumen.AutoSize = true;
            this.lblResumen.Location = new System.Drawing.Point(12, 510);
            this.lblResumen.Name = "lblResumen";
            this.lblResumen.Size = new System.Drawing.Size(121, 13);
            this.lblResumen.TabIndex = 6;
            this.lblResumen.Text = "0 documentos cargados";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.Filter = "Archivos XML (*.xml)|*.xml";
            this.openFileDialog1.Multiselect = true;
            this.openFileDialog1.Title = "Seleccionar XML de compras";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(735, 507);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(237, 20);
            this.progressBar1.TabIndex = 5;
            this.progressBar1.Visible = false;
            // 
            // frmAceptacionDocumentos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 540);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.lblResumen);
            this.Controls.Add(this.btnAplicarAMarcados);
            this.Controls.Add(this.txtRazonMasiva);
            this.Controls.Add(this.cboEstadoMasivo);
            this.Controls.Add(this.lblAplicarA);
            this.Controls.Add(this.dgvDocumentos);
            this.Controls.Add(this.tlsMenu);
            this.MinimumSize = new System.Drawing.Size(700, 400);
            this.Name = "frmAceptacionDocumentos";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Aceptación de Documentos Electrónicos";
            this.Load += new System.EventHandler(this.frmAceptacionDocumentos_Load);
            this.tlsMenu.ResumeLayout(false);
            this.tlsMenu.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDocumentos)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.DateTimePicker hostFechaInicio;
        private System.Windows.Forms.DateTimePicker hostFechaFin;
    }
}