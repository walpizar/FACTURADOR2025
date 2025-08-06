namespace PresentationLayer
{
    partial class buscarAct
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
            this.txtBuscar = new System.Windows.Forms.TextBox();
            this.lsvbuscar = new System.Windows.Forms.ListView();
            this.colId = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colTipocliente = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnseleccionar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtBuscar
            // 
            this.txtBuscar.Location = new System.Drawing.Point(10, 11);
            this.txtBuscar.Name = "txtBuscar";
            this.txtBuscar.Size = new System.Drawing.Size(396, 20);
            this.txtBuscar.TabIndex = 4;
            this.txtBuscar.TextChanged += new System.EventHandler(this.txtBuscar_TextChanged);
            // 
            // lsvbuscar
            // 
            this.lsvbuscar.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colId,
            this.colTipocliente});
            this.lsvbuscar.FullRowSelect = true;
            this.lsvbuscar.GridLines = true;
            this.lsvbuscar.HideSelection = false;
            this.lsvbuscar.HoverSelection = true;
            this.lsvbuscar.Location = new System.Drawing.Point(10, 37);
            this.lsvbuscar.MultiSelect = false;
            this.lsvbuscar.Name = "lsvbuscar";
            this.lsvbuscar.Size = new System.Drawing.Size(549, 198);
            this.lsvbuscar.TabIndex = 3;
            this.lsvbuscar.UseCompatibleStateImageBehavior = false;
            this.lsvbuscar.View = System.Windows.Forms.View.Details;
            this.lsvbuscar.SelectedIndexChanged += new System.EventHandler(this.lsvbuscar_SelectedIndexChanged);
            this.lsvbuscar.DoubleClick += new System.EventHandler(this.lsvbuscar_DoubleClick);
            // 
            // colId
            // 
            this.colId.Text = "Código Act";
            this.colId.Width = 86;
            // 
            // colTipocliente
            // 
            this.colTipocliente.Text = "Nombre";
            this.colTipocliente.Width = 650;
            // 
            // btnseleccionar
            // 
            this.btnseleccionar.Location = new System.Drawing.Point(483, 236);
            this.btnseleccionar.Name = "btnseleccionar";
            this.btnseleccionar.Size = new System.Drawing.Size(75, 23);
            this.btnseleccionar.TabIndex = 5;
            this.btnseleccionar.Text = "Seleccionar";
            this.btnseleccionar.UseVisualStyleBackColor = true;
            this.btnseleccionar.Click += new System.EventHandler(this.btnseleccionar_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Location = new System.Drawing.Point(402, 236);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(75, 23);
            this.btnCancelar.TabIndex = 6;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // buscarAct
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(568, 270);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnseleccionar);
            this.Controls.Add(this.txtBuscar);
            this.Controls.Add(this.lsvbuscar);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "buscarAct";
            this.Text = "Buscar: Actividad Económica";
            this.Load += new System.EventHandler(this.frmBuscarActividadEconomica_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtBuscar;
        private System.Windows.Forms.ListView lsvbuscar;
        private System.Windows.Forms.ColumnHeader colId;
        private System.Windows.Forms.ColumnHeader colTipocliente;
        private System.Windows.Forms.Button btnseleccionar;
        private System.Windows.Forms.Button btnCancelar;
    }
}