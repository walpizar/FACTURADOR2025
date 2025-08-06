namespace PresentationLayer
{
    partial class frmBuscarMovimiento
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
            this.gbxMovimientos = new System.Windows.Forms.GroupBox();
            this.btnSelecionar = new System.Windows.Forms.Button();
            this.lstvMovimientos = new System.Windows.Forms.ListView();
            this.colID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colTipoMov = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colMotivo = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colEstado = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.txtBuscarMovimiento = new System.Windows.Forms.TextBox();
            this.gbxMovimientos.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbxMovimientos
            // 
            this.gbxMovimientos.Controls.Add(this.btnSelecionar);
            this.gbxMovimientos.Controls.Add(this.lstvMovimientos);
            this.gbxMovimientos.Controls.Add(this.txtBuscarMovimiento);
            this.gbxMovimientos.Location = new System.Drawing.Point(8, 15);
            this.gbxMovimientos.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.gbxMovimientos.Name = "gbxMovimientos";
            this.gbxMovimientos.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.gbxMovimientos.Size = new System.Drawing.Size(675, 414);
            this.gbxMovimientos.TabIndex = 0;
            this.gbxMovimientos.TabStop = false;
            this.gbxMovimientos.Text = "Movimeintos Realizados";
            this.gbxMovimientos.Enter += new System.EventHandler(this.gbxMovimientos_Enter);
            // 
            // btnSelecionar
            // 
            this.btnSelecionar.Location = new System.Drawing.Point(484, 363);
            this.btnSelecionar.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnSelecionar.Name = "btnSelecionar";
            this.btnSelecionar.Size = new System.Drawing.Size(168, 28);
            this.btnSelecionar.TabIndex = 4;
            this.btnSelecionar.Text = "Seleccionar";
            this.btnSelecionar.UseVisualStyleBackColor = true;
            this.btnSelecionar.Click += new System.EventHandler(this.btnSelecionar_Click);
            // 
            // lstvMovimientos
            // 
            this.lstvMovimientos.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colID,
            this.colTipoMov,
            this.colMotivo,
            this.colEstado});
            this.lstvMovimientos.FullRowSelect = true;
            this.lstvMovimientos.GridLines = true;
            this.lstvMovimientos.HideSelection = false;
            this.lstvMovimientos.Location = new System.Drawing.Point(8, 107);
            this.lstvMovimientos.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.lstvMovimientos.Name = "lstvMovimientos";
            this.lstvMovimientos.Size = new System.Drawing.Size(643, 248);
            this.lstvMovimientos.TabIndex = 3;
            this.lstvMovimientos.UseCompatibleStateImageBehavior = false;
            this.lstvMovimientos.View = System.Windows.Forms.View.Details;
            this.lstvMovimientos.SelectedIndexChanged += new System.EventHandler(this.lstvMovimientos_SelectedIndexChanged);
            this.lstvMovimientos.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lstvMovimientos_MouseDoubleClick);
            // 
            // colID
            // 
            this.colID.Text = "ID";
            // 
            // colTipoMov
            // 
            this.colTipoMov.Text = "Tipo Movimiento";
            this.colTipoMov.Width = 152;
            // 
            // colMotivo
            // 
            this.colMotivo.Text = "Motivo";
            this.colMotivo.Width = 190;
            // 
            // colEstado
            // 
            this.colEstado.Text = "Estado";
            this.colEstado.Width = 78;
            // 
            // txtBuscarMovimiento
            // 
            this.txtBuscarMovimiento.Location = new System.Drawing.Point(8, 60);
            this.txtBuscarMovimiento.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtBuscarMovimiento.Name = "txtBuscarMovimiento";
            this.txtBuscarMovimiento.Size = new System.Drawing.Size(643, 22);
            this.txtBuscarMovimiento.TabIndex = 2;
            this.txtBuscarMovimiento.TextChanged += new System.EventHandler(this.txtBuscar_TextChanged);
            // 
            // frmBuscarMovimiento
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(689, 443);
            this.Controls.Add(this.gbxMovimientos);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmBuscarMovimiento";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Buscar  Movimiento";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.cerrarForm);
            this.Load += new System.EventHandler(this.frmBuscarMovimiento_Load);
            this.gbxMovimientos.ResumeLayout(false);
            this.gbxMovimientos.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbxMovimientos;
        private System.Windows.Forms.TextBox txtBuscarMovimiento;
        private System.Windows.Forms.ListView lstvMovimientos;
        private System.Windows.Forms.ColumnHeader colID;
        private System.Windows.Forms.ColumnHeader colTipoMov;
        private System.Windows.Forms.ColumnHeader colMotivo;
        private System.Windows.Forms.ColumnHeader colEstado;
        private System.Windows.Forms.Button btnSelecionar;
    }
}