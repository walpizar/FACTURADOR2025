namespace PresentationLayer
{
    partial class frmExportListaPreciosJSON
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmExportListaPreciosJSON));
            this.btnRutaXML = new System.Windows.Forms.Button();
            this.txtRutaXML = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.btnExportar = new System.Windows.Forms.Button();
            this.btnsalir = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.btnsalir)).BeginInit();
            this.SuspendLayout();
            // 
            // btnRutaXML
            // 
            this.btnRutaXML.Location = new System.Drawing.Point(429, 28);
            this.btnRutaXML.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnRutaXML.Name = "btnRutaXML";
            this.btnRutaXML.Size = new System.Drawing.Size(31, 19);
            this.btnRutaXML.TabIndex = 37;
            this.btnRutaXML.Text = "...";
            this.btnRutaXML.UseVisualStyleBackColor = true;
            this.btnRutaXML.Click += new System.EventHandler(this.btnRutaXML_Click);
            // 
            // txtRutaXML
            // 
            this.txtRutaXML.Location = new System.Drawing.Point(92, 28);
            this.txtRutaXML.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtRutaXML.Name = "txtRutaXML";
            this.txtRutaXML.ReadOnly = true;
            this.txtRutaXML.Size = new System.Drawing.Size(333, 20);
            this.txtRutaXML.TabIndex = 36;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(2, 31);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(86, 13);
            this.label8.TabIndex = 35;
            this.label8.Text = "Carpeta Destino:";
            // 
            // btnExportar
            // 
            this.btnExportar.Location = new System.Drawing.Point(349, 52);
            this.btnExportar.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnExportar.Name = "btnExportar";
            this.btnExportar.Size = new System.Drawing.Size(111, 29);
            this.btnExportar.TabIndex = 38;
            this.btnExportar.Text = "Exportar";
            this.btnExportar.UseVisualStyleBackColor = true;
            this.btnExportar.Click += new System.EventHandler(this.btnExportar_Click);
            // 
            // btnsalir
            // 
            this.btnsalir.Image = ((System.Drawing.Image)(resources.GetObject("btnsalir.Image")));
            this.btnsalir.Location = new System.Drawing.Point(440, 3);
            this.btnsalir.Name = "btnsalir";
            this.btnsalir.Size = new System.Drawing.Size(20, 20);
            this.btnsalir.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btnsalir.TabIndex = 94;
            this.btnsalir.TabStop = false;
            this.btnsalir.Click += new System.EventHandler(this.btnsalir_Click);
            // 
            // frmExportListaPreciosJSON
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(469, 100);
            this.Controls.Add(this.btnsalir);
            this.Controls.Add(this.btnExportar);
            this.Controls.Add(this.btnRutaXML);
            this.Controls.Add(this.txtRutaXML);
            this.Controls.Add(this.label8);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "frmExportListaPreciosJSON";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Exportar Datos";
            this.Load += new System.EventHandler(this.frmExportListaPreciosJSON_Load);
            ((System.ComponentModel.ISupportInitialize)(this.btnsalir)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnRutaXML;
        private System.Windows.Forms.TextBox txtRutaXML;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnExportar;
        private System.Windows.Forms.PictureBox btnsalir;
    }
}