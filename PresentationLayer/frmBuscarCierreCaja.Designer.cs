namespace PresentationLayer
{
    partial class frmBuscarCierreCaja
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmBuscarCierreCaja));
            this.lsvbuscar = new System.Windows.Forms.ListView();
            this.colId = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colSucursal = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colCaja = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colUsuario = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colFechaApertura = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colMontoApertura = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colUsuarioCierre = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colFechaCierre = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colMontoCierre = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.gbxFechas = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.dtpFin = new System.Windows.Forms.DateTimePicker();
            this.dtpInicio = new System.Windows.Forms.DateTimePicker();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.btnsalir = new System.Windows.Forms.PictureBox();
            this.gbxFechas.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnsalir)).BeginInit();
            this.SuspendLayout();
            // 
            // lsvbuscar
            // 
            this.lsvbuscar.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colId,
            this.colSucursal,
            this.colCaja,
            this.colUsuario,
            this.colFechaApertura,
            this.colMontoApertura,
            this.colUsuarioCierre,
            this.colFechaCierre,
            this.colMontoCierre});
            this.lsvbuscar.FullRowSelect = true;
            this.lsvbuscar.GridLines = true;
            this.lsvbuscar.HideSelection = false;
            this.lsvbuscar.HoverSelection = true;
            this.lsvbuscar.Location = new System.Drawing.Point(12, 55);
            this.lsvbuscar.MultiSelect = false;
            this.lsvbuscar.Name = "lsvbuscar";
            this.lsvbuscar.Size = new System.Drawing.Size(920, 445);
            this.lsvbuscar.TabIndex = 3;
            this.lsvbuscar.UseCompatibleStateImageBehavior = false;
            this.lsvbuscar.View = System.Windows.Forms.View.Details;
            this.lsvbuscar.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lsvbuscar_MouseClick);
            this.lsvbuscar.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lsvbuscar_MouseDoubleClick);
            // 
            // colId
            // 
            this.colId.Text = "ID";
            // 
            // colSucursal
            // 
            this.colSucursal.Text = "Sucursal";
            // 
            // colCaja
            // 
            this.colCaja.Text = "Caja";
            // 
            // colUsuario
            // 
            this.colUsuario.Text = "Usuario Apertura";
            this.colUsuario.Width = 120;
            // 
            // colFechaApertura
            // 
            this.colFechaApertura.Text = "Apertura";
            this.colFechaApertura.Width = 120;
            // 
            // colMontoApertura
            // 
            this.colMontoApertura.Text = "Monto Apertura";
            this.colMontoApertura.Width = 120;
            // 
            // colUsuarioCierre
            // 
            this.colUsuarioCierre.DisplayIndex = 8;
            this.colUsuarioCierre.Text = "Usuario Cierre";
            this.colUsuarioCierre.Width = 120;
            // 
            // colFechaCierre
            // 
            this.colFechaCierre.DisplayIndex = 6;
            this.colFechaCierre.Text = "Cierre";
            this.colFechaCierre.Width = 120;
            // 
            // colMontoCierre
            // 
            this.colMontoCierre.DisplayIndex = 7;
            this.colMontoCierre.Text = "MontoCierre";
            this.colMontoCierre.Width = 120;
            // 
            // gbxFechas
            // 
            this.gbxFechas.Controls.Add(this.label6);
            this.gbxFechas.Controls.Add(this.dtpFin);
            this.gbxFechas.Controls.Add(this.dtpInicio);
            this.gbxFechas.Location = new System.Drawing.Point(12, 12);
            this.gbxFechas.Name = "gbxFechas";
            this.gbxFechas.Size = new System.Drawing.Size(283, 37);
            this.gbxFechas.TabIndex = 11;
            this.gbxFechas.TabStop = false;
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
            this.btnBuscar.Location = new System.Drawing.Point(313, 12);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(34, 32);
            this.btnBuscar.TabIndex = 12;
            this.btnBuscar.UseVisualStyleBackColor = true;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // btnsalir
            // 
            this.btnsalir.Image = ((System.Drawing.Image)(resources.GetObject("btnsalir.Image")));
            this.btnsalir.Location = new System.Drawing.Point(909, 20);
            this.btnsalir.Name = "btnsalir";
            this.btnsalir.Size = new System.Drawing.Size(20, 20);
            this.btnsalir.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btnsalir.TabIndex = 95;
            this.btnsalir.TabStop = false;
            this.btnsalir.Click += new System.EventHandler(this.btnsalir_Click);
            // 
            // frmBuscarCierreCaja
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(941, 512);
            this.Controls.Add(this.btnsalir);
            this.Controls.Add(this.btnBuscar);
            this.Controls.Add(this.gbxFechas);
            this.Controls.Add(this.lsvbuscar);
            this.Name = "frmBuscarCierreCaja";
            this.Text = "Consulta: Cierre Cajas";
            this.Load += new System.EventHandler(this.frmBuscarCierreCaja_Load);
            this.gbxFechas.ResumeLayout(false);
            this.gbxFechas.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnsalir)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ListView lsvbuscar;
        private System.Windows.Forms.ColumnHeader colId;
        private System.Windows.Forms.ColumnHeader colSucursal;
        private System.Windows.Forms.ColumnHeader colCaja;
        private System.Windows.Forms.ColumnHeader colUsuario;
        private System.Windows.Forms.GroupBox gbxFechas;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DateTimePicker dtpFin;
        private System.Windows.Forms.DateTimePicker dtpInicio;
        private System.Windows.Forms.ColumnHeader colFechaApertura;
        private System.Windows.Forms.ColumnHeader colMontoApertura;
        private System.Windows.Forms.ColumnHeader colFechaCierre;
        private System.Windows.Forms.ColumnHeader colMontoCierre;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.PictureBox btnsalir;
        private System.Windows.Forms.ColumnHeader colUsuarioCierre;
    }
}