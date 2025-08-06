namespace PresentationLayer
{
    partial class frmBuscarAcompa
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
            this.btnEjecutar = new System.Windows.Forms.Button();
            this.lstvAcompa = new System.Windows.Forms.ListView();
            this.colId = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colNombre = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colEstado = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnCancelar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnEjecutar
            // 
            this.btnEjecutar.Location = new System.Drawing.Point(192, 197);
            this.btnEjecutar.Name = "btnEjecutar";
            this.btnEjecutar.Size = new System.Drawing.Size(75, 24);
            this.btnEjecutar.TabIndex = 10;
            this.btnEjecutar.Text = "Seleccionar";
            this.btnEjecutar.UseVisualStyleBackColor = true;
            this.btnEjecutar.Click += new System.EventHandler(this.btnEjecutar_Click);
            // 
            // lstvAcompa
            // 
            this.lstvAcompa.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colId,
            this.colNombre,
            this.colEstado});
            this.lstvAcompa.FullRowSelect = true;
            this.lstvAcompa.GridLines = true;
            this.lstvAcompa.HideSelection = false;
            this.lstvAcompa.Location = new System.Drawing.Point(12, 38);
            this.lstvAcompa.MultiSelect = false;
            this.lstvAcompa.Name = "lstvAcompa";
            this.lstvAcompa.Size = new System.Drawing.Size(255, 150);
            this.lstvAcompa.TabIndex = 8;
            this.lstvAcompa.UseCompatibleStateImageBehavior = false;
            this.lstvAcompa.View = System.Windows.Forms.View.Details;
            this.lstvAcompa.SelectedIndexChanged += new System.EventHandler(this.lstvAcompa_SelectedIndexChanged);
            // 
            // colId
            // 
            this.colId.Text = "ID";
            this.colId.Width = 39;
            // 
            // colNombre
            // 
            this.colNombre.Text = "Nombre";
            this.colNombre.Width = 138;
            // 
            // colEstado
            // 
            this.colEstado.Text = "Estado";
            this.colEstado.Width = 69;
            // 
            // btnCancelar
            // 
            this.btnCancelar.Location = new System.Drawing.Point(113, 197);
            this.btnCancelar.Margin = new System.Windows.Forms.Padding(2);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(74, 24);
            this.btnCancelar.TabIndex = 11;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // frmBuscarAcompa
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(280, 232);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnEjecutar);
            this.Controls.Add(this.lstvAcompa);
            this.Name = "frmBuscarAcompa";
            this.Text = "Buscar: Acompañamiento";
            this.Load += new System.EventHandler(this.frmBuscarAcompa_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnEjecutar;
        private System.Windows.Forms.ListView lstvAcompa;
        private System.Windows.Forms.ColumnHeader colId;
        private System.Windows.Forms.ColumnHeader colNombre;
        private System.Windows.Forms.ColumnHeader colEstado;
        private System.Windows.Forms.Button btnCancelar;
    }
}