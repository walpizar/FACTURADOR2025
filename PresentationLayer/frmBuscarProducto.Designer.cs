namespace PresentationLayer
{
    partial class frmBuscarProducto
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmBuscarProducto));
            this.lstvProductos = new System.Windows.Forms.ListView();
            this.colID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colMedida = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colPrecio = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colInventario = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colCabys = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lblNombreProducto = new System.Windows.Forms.Label();
            this.btnSeleccionar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.cboCategoriaProducto = new System.Windows.Forms.ComboBox();
            this.lblCategoria = new System.Windows.Forms.Label();
            this.btnLimpiarForm = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lstvProductos
            // 
            this.lstvProductos.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colID,
            this.colMedida,
            this.colName,
            this.colPrecio,
            this.colInventario,
            this.colCabys});
            this.lstvProductos.FullRowSelect = true;
            this.lstvProductos.HideSelection = false;
            this.lstvProductos.Location = new System.Drawing.Point(7, 80);
            this.lstvProductos.Margin = new System.Windows.Forms.Padding(2);
            this.lstvProductos.MultiSelect = false;
            this.lstvProductos.Name = "lstvProductos";
            this.lstvProductos.Size = new System.Drawing.Size(821, 274);
            this.lstvProductos.TabIndex = 0;
            this.lstvProductos.UseCompatibleStateImageBehavior = false;
            this.lstvProductos.View = System.Windows.Forms.View.Details;
            this.lstvProductos.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lstvProductos_MouseDoubleClick);
            // 
            // colID
            // 
            this.colID.Text = "ID";
            this.colID.Width = 150;
            // 
            // colMedida
            // 
            this.colMedida.Text = "Medida";
            this.colMedida.Width = 50;
            // 
            // colName
            // 
            this.colName.Text = "Nombre";
            this.colName.Width = 400;
            // 
            // colPrecio
            // 
            this.colPrecio.Text = "PrecioIVA";
            this.colPrecio.Width = 90;
            // 
            // colInventario
            // 
            this.colInventario.Text = "Stock";
            // 
            // colCabys
            // 
            this.colCabys.Text = "CABYS";
            this.colCabys.Width = 50;
            // 
            // lblNombreProducto
            // 
            this.lblNombreProducto.AutoSize = true;
            this.lblNombreProducto.Location = new System.Drawing.Point(38, 29);
            this.lblNombreProducto.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblNombreProducto.Name = "lblNombreProducto";
            this.lblNombreProducto.Size = new System.Drawing.Size(53, 13);
            this.lblNombreProducto.TabIndex = 1;
            this.lblNombreProducto.Text = "Producto:";
            // 
            // btnSeleccionar
            // 
            this.btnSeleccionar.Location = new System.Drawing.Point(727, 358);
            this.btnSeleccionar.Margin = new System.Windows.Forms.Padding(2);
            this.btnSeleccionar.Name = "btnSeleccionar";
            this.btnSeleccionar.Size = new System.Drawing.Size(101, 31);
            this.btnSeleccionar.TabIndex = 3;
            this.btnSeleccionar.Text = "Seleccionar";
            this.btnSeleccionar.UseVisualStyleBackColor = true;
            this.btnSeleccionar.Click += new System.EventHandler(this.btnSeleccionar_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Location = new System.Drawing.Point(624, 358);
            this.btnCancelar.Margin = new System.Windows.Forms.Padding(2);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(91, 31);
            this.btnCancelar.TabIndex = 4;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // txtNombre
            // 
            this.txtNombre.Location = new System.Drawing.Point(98, 26);
            this.txtNombre.Margin = new System.Windows.Forms.Padding(2);
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Size = new System.Drawing.Size(372, 20);
            this.txtNombre.TabIndex = 5;
            this.txtNombre.TextChanged += new System.EventHandler(this.txtNombre_TextChanged);
            this.txtNombre.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNombre_KeyPress);
            // 
            // cboCategoriaProducto
            // 
            this.cboCategoriaProducto.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCategoriaProducto.FormattingEnabled = true;
            this.cboCategoriaProducto.Location = new System.Drawing.Point(98, 47);
            this.cboCategoriaProducto.Margin = new System.Windows.Forms.Padding(2);
            this.cboCategoriaProducto.Name = "cboCategoriaProducto";
            this.cboCategoriaProducto.Size = new System.Drawing.Size(186, 21);
            this.cboCategoriaProducto.TabIndex = 10;
            this.cboCategoriaProducto.SelectedIndexChanged += new System.EventHandler(this.cboCategoriaProducto_SelectedIndexChanged);
            this.cboCategoriaProducto.MouseClick += new System.Windows.Forms.MouseEventHandler(this.cboCategoriaProducto_MouseClick);
            // 
            // lblCategoria
            // 
            this.lblCategoria.AutoSize = true;
            this.lblCategoria.Location = new System.Drawing.Point(38, 50);
            this.lblCategoria.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblCategoria.Name = "lblCategoria";
            this.lblCategoria.Size = new System.Drawing.Size(55, 13);
            this.lblCategoria.TabIndex = 11;
            this.lblCategoria.Text = "Categoria:";
            this.lblCategoria.Click += new System.EventHandler(this.lblCategoria_Click);
            this.lblCategoria.MouseClick += new System.Windows.Forms.MouseEventHandler(this.cboCategoriaProducto_MouseClick);
            // 
            // btnLimpiarForm
            // 
            this.btnLimpiarForm.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLimpiarForm.ForeColor = System.Drawing.Color.Transparent;
            this.btnLimpiarForm.Image = ((System.Drawing.Image)(resources.GetObject("btnLimpiarForm.Image")));
            this.btnLimpiarForm.Location = new System.Drawing.Point(776, 18);
            this.btnLimpiarForm.Name = "btnLimpiarForm";
            this.btnLimpiarForm.Size = new System.Drawing.Size(52, 57);
            this.btnLimpiarForm.TabIndex = 34;
            this.btnLimpiarForm.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.btnLimpiarForm.UseVisualStyleBackColor = true;
            this.btnLimpiarForm.Click += new System.EventHandler(this.btnLimpiarForm_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(475, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 35;
            this.label1.Text = "( * ) Todos";
            // 
            // frmBuscarProducto
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(834, 395);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnLimpiarForm);
            this.Controls.Add(this.cboCategoriaProducto);
            this.Controls.Add(this.lblCategoria);
            this.Controls.Add(this.txtNombre);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnSeleccionar);
            this.Controls.Add(this.lblNombreProducto);
            this.Controls.Add(this.lstvProductos);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "frmBuscarProducto";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Búsqueda: Productos";
            this.Load += new System.EventHandler(this.frmBuscarProducto_Load);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.frmBuscarProducto_KeyUp);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView lstvProductos;
        private System.Windows.Forms.ColumnHeader colID;
        private System.Windows.Forms.ColumnHeader colName;
        private System.Windows.Forms.Label lblNombreProducto;
        private System.Windows.Forms.Button btnSeleccionar;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.ComboBox cboCategoriaProducto;
        private System.Windows.Forms.Label lblCategoria;
        private System.Windows.Forms.Button btnLimpiarForm;
        private System.Windows.Forms.ColumnHeader colMedida;
        private System.Windows.Forms.ColumnHeader colPrecio;
        private System.Windows.Forms.ColumnHeader colInventario;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ColumnHeader colCabys;
    }
}