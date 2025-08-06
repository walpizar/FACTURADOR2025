namespace PresentationLayer
{
    partial class frmCertificado
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
            this.grdCertificados = new System.Windows.Forms.DataGridView();
            this.NombreCertificadoDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ThumbprintDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Cliente = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Inicio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Fin = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DataSet1 = new System.Data.DataSet();
            this.DataTable1 = new System.Data.DataTable();
            this.DataColumn1 = new System.Data.DataColumn();
            this.DataColumn2 = new System.Data.DataColumn();
            this.dataColumn3 = new System.Data.DataColumn();
            this.dataColumn4 = new System.Data.DataColumn();
            this.dataColumn5 = new System.Data.DataColumn();
            ((System.ComponentModel.ISupportInitialize)(this.grdCertificados)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DataSet1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DataTable1)).BeginInit();
            this.SuspendLayout();
            // 
            // grdCertificados
            // 
            this.grdCertificados.AllowUserToAddRows = false;
            this.grdCertificados.AllowUserToDeleteRows = false;
            this.grdCertificados.AllowUserToResizeRows = false;
            this.grdCertificados.AutoGenerateColumns = false;
            this.grdCertificados.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.NombreCertificadoDataGridViewTextBoxColumn,
            this.ThumbprintDataGridViewTextBoxColumn,
            this.Cliente,
            this.Inicio,
            this.Fin});
            this.grdCertificados.DataSource = this.DataSet1;
            this.grdCertificados.Location = new System.Drawing.Point(10, 23);
            this.grdCertificados.Name = "grdCertificados";
            this.grdCertificados.Size = new System.Drawing.Size(944, 245);
            this.grdCertificados.TabIndex = 2;
            this.grdCertificados.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdCertificados_CellContentDoubleClick);
            // 
            // NombreCertificadoDataGridViewTextBoxColumn
            // 
            this.NombreCertificadoDataGridViewTextBoxColumn.DataPropertyName = "NombreCertificado";
            this.NombreCertificadoDataGridViewTextBoxColumn.HeaderText = "NombreCertificado";
            this.NombreCertificadoDataGridViewTextBoxColumn.Name = "NombreCertificadoDataGridViewTextBoxColumn";
            this.NombreCertificadoDataGridViewTextBoxColumn.ReadOnly = true;
            this.NombreCertificadoDataGridViewTextBoxColumn.Width = 200;
            // 
            // ThumbprintDataGridViewTextBoxColumn
            // 
            this.ThumbprintDataGridViewTextBoxColumn.DataPropertyName = "Thumbprint";
            this.ThumbprintDataGridViewTextBoxColumn.HeaderText = "Thumbprint";
            this.ThumbprintDataGridViewTextBoxColumn.Name = "ThumbprintDataGridViewTextBoxColumn";
            this.ThumbprintDataGridViewTextBoxColumn.ReadOnly = true;
            this.ThumbprintDataGridViewTextBoxColumn.Width = 200;
            // 
            // Cliente
            // 
            this.Cliente.DataPropertyName = "Cliente";
            this.Cliente.HeaderText = "Cliente";
            this.Cliente.Name = "Cliente";
            this.Cliente.ReadOnly = true;
            this.Cliente.Width = 200;
            // 
            // Inicio
            // 
            this.Inicio.DataPropertyName = "Inicio";
            this.Inicio.HeaderText = "Inicio";
            this.Inicio.Name = "Inicio";
            this.Inicio.ReadOnly = true;
            this.Inicio.Width = 60;
            // 
            // Fin
            // 
            this.Fin.DataPropertyName = "Fin";
            this.Fin.HeaderText = "Fin";
            this.Fin.Name = "Fin";
            this.Fin.ReadOnly = true;
            this.Fin.Width = 60;
            // 
            // DataSet1
            // 
            this.DataSet1.DataSetName = "NewDataSet";
            this.DataSet1.Tables.AddRange(new System.Data.DataTable[] {
            this.DataTable1});
            // 
            // DataTable1
            // 
            this.DataTable1.Columns.AddRange(new System.Data.DataColumn[] {
            this.DataColumn1,
            this.DataColumn2,
            this.dataColumn3,
            this.dataColumn4,
            this.dataColumn5});
            this.DataTable1.TableName = "Certificados";
            // 
            // DataColumn1
            // 
            this.DataColumn1.ColumnName = "NombreCertificado";
            // 
            // DataColumn2
            // 
            this.DataColumn2.ColumnName = "Thumbprint";
            // 
            // dataColumn3
            // 
            this.dataColumn3.ColumnName = "Cliente";
            // 
            // dataColumn4
            // 
            this.dataColumn4.ColumnName = "Inicio";
            // 
            // dataColumn5
            // 
            this.dataColumn5.ColumnName = "Fin";
            // 
            // frmCertificado
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(974, 289);
            this.Controls.Add(this.grdCertificados);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "frmCertificado";
            this.Text = "Certificados Instalados";
            this.Load += new System.EventHandler(this.frmCertificado_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grdCertificados)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DataSet1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DataTable1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.DataGridView grdCertificados;
        internal System.Data.DataSet DataSet1;
        internal System.Data.DataTable DataTable1;
        internal System.Data.DataColumn DataColumn1;
        internal System.Data.DataColumn DataColumn2;
        private System.Data.DataColumn dataColumn3;
        private System.Data.DataColumn dataColumn4;
        private System.Data.DataColumn dataColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn NombreCertificadoDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ThumbprintDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn Cliente;
        private System.Windows.Forms.DataGridViewTextBoxColumn Inicio;
        private System.Windows.Forms.DataGridViewTextBoxColumn Fin;
    }
}