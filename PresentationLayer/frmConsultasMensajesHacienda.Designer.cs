namespace PresentationLayer
{
    partial class frmConsultasMensajesHacienda
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmConsultasMensajesHacienda));
            this.txtXMLSinFirma = new System.Windows.Forms.TextBox();
            this.cboTipoBusqueda = new System.Windows.Forms.ComboBox();
            this.txtClave = new System.Windows.Forms.TextBox();
            this.btnConsultar = new System.Windows.Forms.Button();
            this.btnsalir = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.btnsalir)).BeginInit();
            this.SuspendLayout();
            // 
            // txtXMLSinFirma
            // 
            this.txtXMLSinFirma.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtXMLSinFirma.Location = new System.Drawing.Point(10, 53);
            this.txtXMLSinFirma.Multiline = true;
            this.txtXMLSinFirma.Name = "txtXMLSinFirma";
            this.txtXMLSinFirma.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtXMLSinFirma.Size = new System.Drawing.Size(866, 372);
            this.txtXMLSinFirma.TabIndex = 2;
            // 
            // cboTipoBusqueda
            // 
            this.cboTipoBusqueda.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTipoBusqueda.Location = new System.Drawing.Point(9, 24);
            this.cboTipoBusqueda.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cboTipoBusqueda.Name = "cboTipoBusqueda";
            this.cboTipoBusqueda.Size = new System.Drawing.Size(182, 21);
            this.cboTipoBusqueda.TabIndex = 34;
            this.cboTipoBusqueda.SelectedIndexChanged += new System.EventHandler(this.cboTipoBusqueda_SelectedIndexChanged);
            // 
            // txtClave
            // 
            this.txtClave.Location = new System.Drawing.Point(196, 25);
            this.txtClave.Name = "txtClave";
            this.txtClave.Size = new System.Drawing.Size(338, 20);
            this.txtClave.TabIndex = 33;
            // 
            // btnConsultar
            // 
            this.btnConsultar.Image = ((System.Drawing.Image)(resources.GetObject("btnConsultar.Image")));
            this.btnConsultar.Location = new System.Drawing.Point(540, 9);
            this.btnConsultar.Name = "btnConsultar";
            this.btnConsultar.Size = new System.Drawing.Size(46, 37);
            this.btnConsultar.TabIndex = 32;
            this.btnConsultar.UseVisualStyleBackColor = true;
            this.btnConsultar.Click += new System.EventHandler(this.btnConsultar_Click);
            // 
            // btnsalir
            // 
            this.btnsalir.Image = ((System.Drawing.Image)(resources.GetObject("btnsalir.Image")));
            this.btnsalir.Location = new System.Drawing.Point(851, -1);
            this.btnsalir.Name = "btnsalir";
            this.btnsalir.Size = new System.Drawing.Size(25, 25);
            this.btnsalir.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btnsalir.TabIndex = 83;
            this.btnsalir.TabStop = false;
            this.btnsalir.Click += new System.EventHandler(this.btnsalir_Click);
            // 
            // frmConsultasMensajesHacienda
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(880, 456);
            this.Controls.Add(this.btnsalir);
            this.Controls.Add(this.cboTipoBusqueda);
            this.Controls.Add(this.txtClave);
            this.Controls.Add(this.btnConsultar);
            this.Controls.Add(this.txtXMLSinFirma);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "frmConsultasMensajesHacienda";
            this.Text = "Consulta:  Mensajes de compras Hacienda";
            this.Load += new System.EventHandler(this.frmConsultasMensajesHacienda_Load);
            ((System.ComponentModel.ISupportInitialize)(this.btnsalir)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.TextBox txtXMLSinFirma;
        private System.Windows.Forms.ComboBox cboTipoBusqueda;
        internal System.Windows.Forms.TextBox txtClave;
        private System.Windows.Forms.Button btnConsultar;
        private System.Windows.Forms.PictureBox btnsalir;
    }
}