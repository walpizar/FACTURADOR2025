namespace PresentationLayer
{
    partial class frmBuscarRequerimientos
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
            this.lsvtBuscarRequeri = new System.Windows.Forms.ListView();
            this.ID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Nombre = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmEstado = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnSeleccionar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtBuscar
            // 
            this.txtBuscar.Location = new System.Drawing.Point(17, 30);
            this.txtBuscar.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtBuscar.Name = "txtBuscar";
            this.txtBuscar.Size = new System.Drawing.Size(443, 22);
            this.txtBuscar.TabIndex = 0;
            this.txtBuscar.TextChanged += new System.EventHandler(this.txtBuscar_TextChanged);
            // 
            // lsvtBuscarRequeri
            // 
            this.lsvtBuscarRequeri.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ID,
            this.Nombre,
            this.clmEstado});
            this.lsvtBuscarRequeri.FullRowSelect = true;
            this.lsvtBuscarRequeri.GridLines = true;
            this.lsvtBuscarRequeri.HideSelection = false;
            this.lsvtBuscarRequeri.Location = new System.Drawing.Point(16, 62);
            this.lsvtBuscarRequeri.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.lsvtBuscarRequeri.Name = "lsvtBuscarRequeri";
            this.lsvtBuscarRequeri.Size = new System.Drawing.Size(444, 221);
            this.lsvtBuscarRequeri.TabIndex = 1;
            this.lsvtBuscarRequeri.UseCompatibleStateImageBehavior = false;
            this.lsvtBuscarRequeri.View = System.Windows.Forms.View.Details;
            this.lsvtBuscarRequeri.SelectedIndexChanged += new System.EventHandler(this.lsvtBuscarRequeri_SelectedIndexChanged);
            this.lsvtBuscarRequeri.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lsvtBuscarRequerimientos);
            // 
            // ID
            // 
            this.ID.Text = "ID";
            this.ID.Width = 97;
            // 
            // Nombre
            // 
            this.Nombre.Text = "Nombre";
            this.Nombre.Width = 169;
            // 
            // clmEstado
            // 
            this.clmEstado.Text = "Estado";
            // 
            // btnSeleccionar
            // 
            this.btnSeleccionar.Enabled = false;
            this.btnSeleccionar.Location = new System.Drawing.Point(361, 295);
            this.btnSeleccionar.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnSeleccionar.Name = "btnSeleccionar";
            this.btnSeleccionar.Size = new System.Drawing.Size(100, 58);
            this.btnSeleccionar.TabIndex = 3;
            this.btnSeleccionar.Text = "Seleccionar";
            this.btnSeleccionar.UseVisualStyleBackColor = true;
            this.btnSeleccionar.Click += new System.EventHandler(this.btnSeleccionar_Click_1);
            // 
            // frmBuscarRequerimientos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(483, 368);
            this.Controls.Add(this.btnSeleccionar);
            this.Controls.Add(this.lsvtBuscarRequeri);
            this.Controls.Add(this.txtBuscar);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "frmBuscarRequerimientos";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Buscar requerimientos";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.CerrarForm);
            this.Load += new System.EventHandler(this.frmBuscarRequerimientos_Load);
            this.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lsvtRequerimientos);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtBuscar;
        private System.Windows.Forms.Button btnSeleccionar;
        private System.Windows.Forms.ColumnHeader ID;
        private System.Windows.Forms.ColumnHeader Nombre;
        private System.Windows.Forms.ListView lsvtBuscarRequeri;
        private System.Windows.Forms.ColumnHeader clmEstado;
    }
}