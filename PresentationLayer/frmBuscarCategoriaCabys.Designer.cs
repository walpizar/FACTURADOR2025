namespace PresentationLayer
{
    partial class frmBuscarCategoriaCabys
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">
        /// true if managed resources should be disposed; otherwise, false.
        /// </param>
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
        /// Required method for Designer support.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmBuscarCategoriaCabys));
            this.lstvBienServicios = new System.Windows.Forms.ListView();
            this.colID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colImpuesto = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colProductoServ = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.txtbusqueda = new System.Windows.Forms.TextBox();
            this.lblNombreProducto = new System.Windows.Forms.Label();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.btnAnterior = new System.Windows.Forms.Button();
            this.btnSiguiente = new System.Windows.Forms.Button();
            this.lblPagina = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lstvBienServicios
            // 
            this.lstvBienServicios.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colID,
            this.colName,
            this.colImpuesto,
            this.colProductoServ});
            this.lstvBienServicios.FullRowSelect = true;
            this.lstvBienServicios.HideSelection = false;
            this.lstvBienServicios.Location = new System.Drawing.Point(17, 65);
            this.lstvBienServicios.Margin = new System.Windows.Forms.Padding(2);
            this.lstvBienServicios.MultiSelect = false;
            this.lstvBienServicios.Name = "lstvBienServicios";
            this.lstvBienServicios.Size = new System.Drawing.Size(1005, 298);
            this.lstvBienServicios.TabIndex = 1;
            this.lstvBienServicios.UseCompatibleStateImageBehavior = false;
            this.lstvBienServicios.View = System.Windows.Forms.View.Details;
            this.lstvBienServicios.SelectedIndexChanged += new System.EventHandler(this.lstvBienServicios_SelectedIndexChanged);
            this.lstvBienServicios.DoubleClick += new System.EventHandler(this.lstvBienServicios_DoubleClick);
            // 
            // colID
            // 
            this.colID.Text = "ID";
            this.colID.Width = 150;
            // 
            // colName
            // 
            this.colName.Text = "Nombre";
            this.colName.Width = 600;
            // 
            // colImpuesto
            // 
            this.colImpuesto.Text = "%Impuesto";
            this.colImpuesto.Width = 90;
            // 
            // colProductoServ
            // 
            this.colProductoServ.Text = "Tipo";
            this.colProductoServ.Width = 150;
            // 
            // txtbusqueda
            // 
            this.txtbusqueda.Location = new System.Drawing.Point(92, 31);
            this.txtbusqueda.Margin = new System.Windows.Forms.Padding(2);
            this.txtbusqueda.Name = "txtbusqueda";
            this.txtbusqueda.Size = new System.Drawing.Size(349, 20);
            this.txtbusqueda.TabIndex = 13;
            this.txtbusqueda.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtbusqueda_KeyDown);
            // 
            // lblNombreProducto
            // 
            this.lblNombreProducto.AutoSize = true;
            this.lblNombreProducto.Location = new System.Drawing.Point(14, 34);
            this.lblNombreProducto.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblNombreProducto.Name = "lblNombreProducto";
            this.lblNombreProducto.Size = new System.Drawing.Size(74, 13);
            this.lblNombreProducto.TabIndex = 12;
            this.lblNombreProducto.Text = "Bien/Servicio:";
            // 
            // btnBuscar
            // 
            this.btnBuscar.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnBuscar.BackgroundImage")));
            this.btnBuscar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnBuscar.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBuscar.ForeColor = System.Drawing.Color.Transparent;
            this.btnBuscar.Location = new System.Drawing.Point(455, 12);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(52, 48);
            this.btnBuscar.TabIndex = 35;
            this.btnBuscar.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.btnBuscar.UseVisualStyleBackColor = true;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // btnAnterior
            // 
            this.btnAnterior.Location = new System.Drawing.Point(310, 376);
            this.btnAnterior.Name = "btnAnterior";
            this.btnAnterior.Size = new System.Drawing.Size(110, 32);
            this.btnAnterior.TabIndex = 36;
            this.btnAnterior.Text = "<< Anterior";
            this.btnAnterior.UseVisualStyleBackColor = true;
            this.btnAnterior.Click += new System.EventHandler(this.btnAnterior_Click);
            // 
            // btnSiguiente
            // 
            this.btnSiguiente.Location = new System.Drawing.Point(650, 376);
            this.btnSiguiente.Name = "btnSiguiente";
            this.btnSiguiente.Size = new System.Drawing.Size(110, 32);
            this.btnSiguiente.TabIndex = 38;
            this.btnSiguiente.Text = "Siguiente >>";
            this.btnSiguiente.UseVisualStyleBackColor = true;
            this.btnSiguiente.Click += new System.EventHandler(this.btnSiguiente_Click);
            // 
            // lblPagina
            // 
            this.lblPagina.AutoSize = true;
            this.lblPagina.Location = new System.Drawing.Point(438, 386);
            this.lblPagina.Name = "lblPagina";
            this.lblPagina.Size = new System.Drawing.Size(73, 13);
            this.lblPagina.TabIndex = 37;
            this.lblPagina.Text = "Página 1 de 1";
            // 
            // frmBuscarCategoriaCabys
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1031, 420);
            this.Controls.Add(this.btnAnterior);
            this.Controls.Add(this.lblPagina);
            this.Controls.Add(this.btnSiguiente);
            this.Controls.Add(this.btnBuscar);
            this.Controls.Add(this.txtbusqueda);
            this.Controls.Add(this.lblNombreProducto);
            this.Controls.Add(this.lstvBienServicios);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "frmBuscarCategoriaCabys";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Categorías CABYS";
            this.Load += new System.EventHandler(this.frmBuscarCategoriaCabys_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView lstvBienServicios;
        private System.Windows.Forms.ColumnHeader colID;
        private System.Windows.Forms.ColumnHeader colName;
        private System.Windows.Forms.ColumnHeader colImpuesto;
        private System.Windows.Forms.ColumnHeader colProductoServ;

        private System.Windows.Forms.TextBox txtbusqueda;
        private System.Windows.Forms.Label lblNombreProducto;
        private System.Windows.Forms.Button btnBuscar;

        private System.Windows.Forms.Button btnAnterior;
        private System.Windows.Forms.Button btnSiguiente;
        private System.Windows.Forms.Label lblPagina;
    }
}