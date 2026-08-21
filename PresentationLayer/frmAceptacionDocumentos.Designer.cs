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

        // ---------- Encabezado: fila 1 (carga de documentos) ----------
        private System.Windows.Forms.Panel pnlEncabezado;
        private System.Windows.Forms.FlowLayoutPanel flpCarga;
        private System.Windows.Forms.Button btnCargarPendientes;
        private System.Windows.Forms.Button btnCargarXml;
        private System.Windows.Forms.Panel sepCarga1;
        private System.Windows.Forms.Label lblFechaDesde;
        private System.Windows.Forms.DateTimePicker dtpFechaInicio;
        private System.Windows.Forms.Label lblFechaHasta;
        private System.Windows.Forms.DateTimePicker dtpFechaFin;
        private System.Windows.Forms.Panel sepCarga2;
        private System.Windows.Forms.Button btnCargarCorreo;

        // ---------- Encabezado: fila 2 (selección y acciones) ----------
        private System.Windows.Forms.FlowLayoutPanel flpAcciones;
        private System.Windows.Forms.Button btnMarcarTodos;
        private System.Windows.Forms.Button btnDesmarcarTodos;
        private System.Windows.Forms.Panel sepAcciones1;
        private System.Windows.Forms.Button btnProcesar;
        private System.Windows.Forms.Button btnConsultarEstado;
        private System.Windows.Forms.Button btnSalir;

        // ---------- Grilla ----------
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

        // ---------- Barra "aplicar a marcados" ----------
        private System.Windows.Forms.Label lblAplicarA;
        private System.Windows.Forms.ComboBox cboEstadoMasivo;
        private System.Windows.Forms.TextBox txtRazonMasiva;
        private System.Windows.Forms.Button btnAplicarAMarcados;

        private System.Windows.Forms.Label lblResumen;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.ProgressBar progressBar1;

        private void InitializeComponent()
        {
            this.pnlEncabezado = new System.Windows.Forms.Panel();
            this.flpCarga = new System.Windows.Forms.FlowLayoutPanel();
            this.btnCargarPendientes = new System.Windows.Forms.Button();
            this.btnCargarXml = new System.Windows.Forms.Button();
            this.sepCarga1 = new System.Windows.Forms.Panel();
            this.lblFechaDesde = new System.Windows.Forms.Label();
            this.dtpFechaInicio = new System.Windows.Forms.DateTimePicker();
            this.lblFechaHasta = new System.Windows.Forms.Label();
            this.dtpFechaFin = new System.Windows.Forms.DateTimePicker();
            this.sepCarga2 = new System.Windows.Forms.Panel();
            this.btnCargarCorreo = new System.Windows.Forms.Button();

            this.flpAcciones = new System.Windows.Forms.FlowLayoutPanel();
            this.btnMarcarTodos = new System.Windows.Forms.Button();
            this.btnDesmarcarTodos = new System.Windows.Forms.Button();
            this.sepAcciones1 = new System.Windows.Forms.Panel();
            this.btnProcesar = new System.Windows.Forms.Button();
            this.btnConsultarEstado = new System.Windows.Forms.Button();
            this.btnSalir = new System.Windows.Forms.Button();

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

            this.pnlEncabezado.SuspendLayout();
            this.flpCarga.SuspendLayout();
            this.flpAcciones.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDocumentos)).BeginInit();
            this.SuspendLayout();
            //
            // pnlEncabezado (contiene las 2 filas agrupadas)
            //
            this.pnlEncabezado.BackColor = System.Drawing.Color.FromArgb(245, 245, 245);
            this.pnlEncabezado.Controls.Add(this.flpAcciones);
            this.pnlEncabezado.Controls.Add(this.flpCarga);
            this.pnlEncabezado.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlEncabezado.Location = new System.Drawing.Point(0, 0);
            this.pnlEncabezado.Name = "pnlEncabezado";
            this.pnlEncabezado.Size = new System.Drawing.Size(984, 76);
            this.pnlEncabezado.TabIndex = 0;
            //
            // flpCarga (fila 1: cargar documentos)
            //
            this.flpCarga.AutoSize = true;
            this.flpCarga.FlowDirection = System.Windows.Forms.FlowDirection.LeftToRight;
            this.flpCarga.WrapContents = false;
            this.flpCarga.Location = new System.Drawing.Point(8, 6);
            this.flpCarga.Name = "flpCarga";
            this.flpCarga.Padding = new System.Windows.Forms.Padding(0, 4, 0, 4);
            this.flpCarga.Size = new System.Drawing.Size(900, 32);
            this.flpCarga.TabIndex = 0;
            this.flpCarga.Controls.Add(this.btnCargarPendientes);
            this.flpCarga.Controls.Add(this.btnCargarXml);
            this.flpCarga.Controls.Add(this.sepCarga1);
            this.flpCarga.Controls.Add(this.lblFechaDesde);
            this.flpCarga.Controls.Add(this.dtpFechaInicio);
            this.flpCarga.Controls.Add(this.lblFechaHasta);
            this.flpCarga.Controls.Add(this.dtpFechaFin);
            this.flpCarga.Controls.Add(this.sepCarga2);
            this.flpCarga.Controls.Add(this.btnCargarCorreo);
            //
            // btnCargarPendientes
            //
            this.btnCargarPendientes.AutoSize = true;
            this.btnCargarPendientes.Margin = new System.Windows.Forms.Padding(0, 0, 6, 0);
            this.btnCargarPendientes.Name = "btnCargarPendientes";
            this.btnCargarPendientes.Padding = new System.Windows.Forms.Padding(8, 3, 8, 3);
            this.btnCargarPendientes.Text = "Cargar pendientes";
            this.btnCargarPendientes.UseVisualStyleBackColor = true;
            this.btnCargarPendientes.Click += new System.EventHandler(this.btnCargarPendientes_Click);
            //
            // btnCargarXml
            //
            this.btnCargarXml.AutoSize = true;
            this.btnCargarXml.Margin = new System.Windows.Forms.Padding(0, 0, 12, 0);
            this.btnCargarXml.Name = "btnCargarXml";
            this.btnCargarXml.Padding = new System.Windows.Forms.Padding(8, 3, 8, 3);
            this.btnCargarXml.Text = "Cargar XML";
            this.btnCargarXml.UseVisualStyleBackColor = true;
            this.btnCargarXml.Click += new System.EventHandler(this.btnCargarXml_Click);
            //
            // sepCarga1 (separador visual, linea vertical)
            //
            this.sepCarga1.BackColor = System.Drawing.Color.FromArgb(210, 210, 210);
            this.sepCarga1.Margin = new System.Windows.Forms.Padding(0, 3, 12, 3);
            this.sepCarga1.Name = "sepCarga1";
            this.sepCarga1.Size = new System.Drawing.Size(1, 24);
            //
            // lblFechaDesde
            //
            this.lblFechaDesde.AutoSize = true;
            this.lblFechaDesde.Margin = new System.Windows.Forms.Padding(0, 8, 4, 0);
            this.lblFechaDesde.Name = "lblFechaDesde";
            this.lblFechaDesde.Text = "Desde:";
            //
            // dtpFechaInicio
            //
            this.dtpFechaInicio.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaInicio.Margin = new System.Windows.Forms.Padding(0, 4, 12, 0);
            this.dtpFechaInicio.Name = "dtpFechaInicio";
            this.dtpFechaInicio.Size = new System.Drawing.Size(104, 20);
            this.dtpFechaInicio.Value = System.DateTime.Now.AddDays(-7);
            //
            // lblFechaHasta
            //
            this.lblFechaHasta.AutoSize = true;
            this.lblFechaHasta.Margin = new System.Windows.Forms.Padding(0, 8, 4, 0);
            this.lblFechaHasta.Name = "lblFechaHasta";
            this.lblFechaHasta.Text = "Hasta:";
            //
            // dtpFechaFin
            //
            this.dtpFechaFin.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaFin.Margin = new System.Windows.Forms.Padding(0, 4, 12, 0);
            this.dtpFechaFin.Name = "dtpFechaFin";
            this.dtpFechaFin.Size = new System.Drawing.Size(104, 20);
            this.dtpFechaFin.Value = System.DateTime.Now;
            //
            // sepCarga2
            //
            this.sepCarga2.BackColor = System.Drawing.Color.FromArgb(210, 210, 210);
            this.sepCarga2.Margin = new System.Windows.Forms.Padding(0, 3, 12, 3);
            this.sepCarga2.Name = "sepCarga2";
            this.sepCarga2.Size = new System.Drawing.Size(1, 24);
            //
            // btnCargarCorreo
            //
            this.btnCargarCorreo.AutoSize = true;
            this.btnCargarCorreo.Margin = new System.Windows.Forms.Padding(0);
            this.btnCargarCorreo.Name = "btnCargarCorreo";
            this.btnCargarCorreo.Padding = new System.Windows.Forms.Padding(8, 3, 8, 3);
            this.btnCargarCorreo.Text = "Cargar por correo";
            this.btnCargarCorreo.UseVisualStyleBackColor = true;
            this.btnCargarCorreo.Click += new System.EventHandler(this.btnCargarCorreo_Click);
            //
            // flpAcciones (fila 2: seleccion y procesamiento)
            //
            this.flpAcciones.AutoSize = true;
            this.flpAcciones.FlowDirection = System.Windows.Forms.FlowDirection.LeftToRight;
            this.flpAcciones.WrapContents = false;
            this.flpAcciones.Location = new System.Drawing.Point(8, 40);
            this.flpAcciones.Name = "flpAcciones";
            this.flpAcciones.Padding = new System.Windows.Forms.Padding(0, 4, 0, 4);
            this.flpAcciones.Size = new System.Drawing.Size(900, 32);
            this.flpAcciones.TabIndex = 1;
            this.flpAcciones.Controls.Add(this.btnMarcarTodos);
            this.flpAcciones.Controls.Add(this.btnDesmarcarTodos);
            this.flpAcciones.Controls.Add(this.sepAcciones1);
            this.flpAcciones.Controls.Add(this.btnProcesar);
            this.flpAcciones.Controls.Add(this.btnConsultarEstado);
            //
            // btnMarcarTodos
            //
            this.btnMarcarTodos.AutoSize = true;
            this.btnMarcarTodos.Margin = new System.Windows.Forms.Padding(0, 0, 6, 0);
            this.btnMarcarTodos.Name = "btnMarcarTodos";
            this.btnMarcarTodos.Padding = new System.Windows.Forms.Padding(8, 3, 8, 3);
            this.btnMarcarTodos.Text = "Marcar todos";
            this.btnMarcarTodos.UseVisualStyleBackColor = true;
            this.btnMarcarTodos.Click += new System.EventHandler(this.btnMarcarTodos_Click);
            //
            // btnDesmarcarTodos
            //
            this.btnDesmarcarTodos.AutoSize = true;
            this.btnDesmarcarTodos.Margin = new System.Windows.Forms.Padding(0, 0, 12, 0);
            this.btnDesmarcarTodos.Name = "btnDesmarcarTodos";
            this.btnDesmarcarTodos.Padding = new System.Windows.Forms.Padding(8, 3, 8, 3);
            this.btnDesmarcarTodos.Text = "Desmarcar todos";
            this.btnDesmarcarTodos.UseVisualStyleBackColor = true;
            this.btnDesmarcarTodos.Click += new System.EventHandler(this.btnDesmarcarTodos_Click);
            //
            // sepAcciones1
            //
            this.sepAcciones1.BackColor = System.Drawing.Color.FromArgb(210, 210, 210);
            this.sepAcciones1.Margin = new System.Windows.Forms.Padding(0, 3, 12, 3);
            this.sepAcciones1.Name = "sepAcciones1";
            this.sepAcciones1.Size = new System.Drawing.Size(1, 24);
            //
            // btnProcesar (accion principal: resaltado)
            //
            this.btnProcesar.AutoSize = true;
            this.btnProcesar.BackColor = System.Drawing.Color.FromArgb(29, 158, 117);
            this.btnProcesar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnProcesar.FlatAppearance.BorderSize = 0;
            this.btnProcesar.ForeColor = System.Drawing.Color.White;
            this.btnProcesar.Margin = new System.Windows.Forms.Padding(0, 0, 6, 0);
            this.btnProcesar.Name = "btnProcesar";
            this.btnProcesar.Padding = new System.Windows.Forms.Padding(10, 4, 10, 4);
            this.btnProcesar.Text = "Procesar seleccionados";
            this.btnProcesar.UseVisualStyleBackColor = false;
            this.btnProcesar.Click += new System.EventHandler(this.btnProcesar_Click);
            //
            // btnConsultarEstado
            //
            this.btnConsultarEstado.AutoSize = true;
            this.btnConsultarEstado.Margin = new System.Windows.Forms.Padding(0);
            this.btnConsultarEstado.Name = "btnConsultarEstado";
            this.btnConsultarEstado.Padding = new System.Windows.Forms.Padding(8, 3, 8, 3);
            this.btnConsultarEstado.Text = "Consultar estado";
            this.btnConsultarEstado.UseVisualStyleBackColor = true;
            this.btnConsultarEstado.Click += new System.EventHandler(this.btnConsultarEstado_Click);
            //
            // btnSalir (esquina superior derecha, fuera de los grupos de flujo)
            //
            this.btnSalir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSalir.Location = new System.Drawing.Point(897, 8);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(75, 25);
            this.btnSalir.TabIndex = 2;
            this.btnSalir.Text = "Salir";
            this.btnSalir.UseVisualStyleBackColor = true;
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            this.pnlEncabezado.Controls.Add(this.btnSalir);
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
            this.dgvDocumentos.Location = new System.Drawing.Point(15, 88);
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
            this.colRazon.HeaderText = "Razon / Detalle";
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
            this.lblAplicarA.Location = new System.Drawing.Point(12, 529);
            this.lblAplicarA.Name = "lblAplicarA";
            this.lblAplicarA.Size = new System.Drawing.Size(153, 13);
            this.lblAplicarA.TabIndex = 7;
            this.lblAplicarA.Text = "Aplicar a marcados -> Estado:";
            //
            // cboEstadoMasivo
            //
            this.cboEstadoMasivo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboEstadoMasivo.Location = new System.Drawing.Point(165, 526);
            this.cboEstadoMasivo.Name = "cboEstadoMasivo";
            this.cboEstadoMasivo.Size = new System.Drawing.Size(150, 21);
            this.cboEstadoMasivo.TabIndex = 2;
            //
            // txtRazonMasiva
            //
            this.txtRazonMasiva.Location = new System.Drawing.Point(325, 526);
            this.txtRazonMasiva.Name = "txtRazonMasiva";
            this.txtRazonMasiva.Size = new System.Drawing.Size(400, 20);
            this.txtRazonMasiva.TabIndex = 3;
            //
            // btnAplicarAMarcados
            //
            this.btnAplicarAMarcados.Location = new System.Drawing.Point(735, 524);
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
            this.lblResumen.Location = new System.Drawing.Point(12, 559);
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
            this.progressBar1.Location = new System.Drawing.Point(735, 556);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(237, 20);
            this.progressBar1.TabIndex = 5;
            this.progressBar1.Visible = false;
            //
            // frmAceptacionDocumentos
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 591);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.lblResumen);
            this.Controls.Add(this.btnAplicarAMarcados);
            this.Controls.Add(this.txtRazonMasiva);
            this.Controls.Add(this.cboEstadoMasivo);
            this.Controls.Add(this.lblAplicarA);
            this.Controls.Add(this.dgvDocumentos);
            this.Controls.Add(this.pnlEncabezado);
            this.MinimumSize = new System.Drawing.Size(700, 450);
            this.Name = "frmAceptacionDocumentos";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Aceptacion de Documentos Electronicos";
            this.Load += new System.EventHandler(this.frmAceptacionDocumentos_Load);
            this.pnlEncabezado.ResumeLayout(false);
            this.pnlEncabezado.PerformLayout();
            this.flpCarga.ResumeLayout(false);
            this.flpCarga.PerformLayout();
            this.flpAcciones.ResumeLayout(false);
            this.flpAcciones.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDocumentos)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}