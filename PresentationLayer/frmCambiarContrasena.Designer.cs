﻿namespace PresentationLayer
{
    partial class frmCambiarContrasena
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCambiarContrasena));
            this.label1 = new System.Windows.Forms.Label();
            this.txtAnterior = new System.Windows.Forms.TextBox();
            this.txtNueva = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtConfirmacion = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnProcesar = new System.Windows.Forms.Button();
            this.btnsalir = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.btnsalir)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(56, 30);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Anterior:";
            // 
            // txtAnterior
            // 
            this.txtAnterior.Location = new System.Drawing.Point(106, 26);
            this.txtAnterior.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtAnterior.Name = "txtAnterior";
            this.txtAnterior.PasswordChar = '*';
            this.txtAnterior.Size = new System.Drawing.Size(196, 20);
            this.txtAnterior.TabIndex = 1;
            // 
            // txtNueva
            // 
            this.txtNueva.Location = new System.Drawing.Point(106, 49);
            this.txtNueva.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtNueva.Name = "txtNueva";
            this.txtNueva.PasswordChar = '*';
            this.txtNueva.Size = new System.Drawing.Size(196, 20);
            this.txtNueva.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(62, 53);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Nueva:";
            // 
            // txtConfirmacion
            // 
            this.txtConfirmacion.Location = new System.Drawing.Point(106, 72);
            this.txtConfirmacion.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtConfirmacion.Name = "txtConfirmacion";
            this.txtConfirmacion.PasswordChar = '*';
            this.txtConfirmacion.Size = new System.Drawing.Size(196, 20);
            this.txtConfirmacion.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 74);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Confirmar Nueva:";
            // 
            // btnProcesar
            // 
            this.btnProcesar.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnProcesar.Image = ((System.Drawing.Image)(resources.GetObject("btnProcesar.Image")));
            this.btnProcesar.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnProcesar.Location = new System.Drawing.Point(223, 102);
            this.btnProcesar.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnProcesar.Name = "btnProcesar";
            this.btnProcesar.Size = new System.Drawing.Size(79, 51);
            this.btnProcesar.TabIndex = 18;
            this.btnProcesar.Text = "Procesar";
            this.btnProcesar.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnProcesar.UseVisualStyleBackColor = true;
            this.btnProcesar.Click += new System.EventHandler(this.btnProcesar_Click);
            // 
            // btnsalir
            // 
            this.btnsalir.Image = ((System.Drawing.Image)(resources.GetObject("btnsalir.Image")));
            this.btnsalir.Location = new System.Drawing.Point(280, 2);
            this.btnsalir.Name = "btnsalir";
            this.btnsalir.Size = new System.Drawing.Size(22, 20);
            this.btnsalir.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btnsalir.TabIndex = 83;
            this.btnsalir.TabStop = false;
            this.btnsalir.Click += new System.EventHandler(this.btnsalir_Click);
            // 
            // frmCambiarContrasena
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(321, 157);
            this.Controls.Add(this.btnsalir);
            this.Controls.Add(this.btnProcesar);
            this.Controls.Add(this.txtConfirmacion);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtNueva);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtAnterior);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "frmCambiarContrasena";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cambiar Contraseña";
            this.Load += new System.EventHandler(this.frmCambiarContrasena_Load);
            ((System.ComponentModel.ISupportInitialize)(this.btnsalir)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtAnterior;
        private System.Windows.Forms.TextBox txtNueva;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtConfirmacion;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnProcesar;
        private System.Windows.Forms.PictureBox btnsalir;
    }
}