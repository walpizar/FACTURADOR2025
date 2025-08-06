namespace PresentationLayer
{
    partial class frmProductoAcompanam
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmProductoAcompanam));
            this.tlsMenu = new System.Windows.Forms.ToolStrip();
            this.tlsBtnGuardar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tlsBtnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tlsBtnModificar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tlsBtnEliminar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.tlsBtnBuscar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.tlsBtnCancelar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.tlsBtnSalir = new System.Windows.Forms.ToolStripButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lstvAcomp = new System.Windows.Forms.ListView();
            this.colID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.btnBuscarProducto = new System.Windows.Forms.Button();
            this.label16 = new System.Windows.Forms.Label();
            this.txtCodigoProducto = new System.Windows.Forms.TextBox();
            this.tlsMenu.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlsMenu
            // 
            this.tlsMenu.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.tlsMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tlsBtnGuardar,
            this.toolStripSeparator1,
            this.tlsBtnNuevo,
            this.toolStripSeparator2,
            this.tlsBtnModificar,
            this.toolStripSeparator3,
            this.tlsBtnEliminar,
            this.toolStripSeparator4,
            this.tlsBtnBuscar,
            this.toolStripSeparator5,
            this.tlsBtnCancelar,
            this.toolStripSeparator6,
            this.tlsBtnSalir});
            this.tlsMenu.Location = new System.Drawing.Point(0, 0);
            this.tlsMenu.Name = "tlsMenu";
            this.tlsMenu.Size = new System.Drawing.Size(763, 39);
            this.tlsMenu.TabIndex = 15;
            this.tlsMenu.Text = "toolStrip1";
            this.tlsMenu.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.tlsMenu_ItemClicked);
            // 
            // tlsBtnGuardar
            // 
            this.tlsBtnGuardar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tlsBtnGuardar.Image = ((System.Drawing.Image)(resources.GetObject("tlsBtnGuardar.Image")));
            this.tlsBtnGuardar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlsBtnGuardar.MergeIndex = 1;
            this.tlsBtnGuardar.Name = "tlsBtnGuardar";
            this.tlsBtnGuardar.Size = new System.Drawing.Size(36, 36);
            this.tlsBtnGuardar.Text = "Guardar";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 39);
            // 
            // tlsBtnNuevo
            // 
            this.tlsBtnNuevo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tlsBtnNuevo.Image = ((System.Drawing.Image)(resources.GetObject("tlsBtnNuevo.Image")));
            this.tlsBtnNuevo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlsBtnNuevo.MergeIndex = 2;
            this.tlsBtnNuevo.Name = "tlsBtnNuevo";
            this.tlsBtnNuevo.Size = new System.Drawing.Size(36, 36);
            this.tlsBtnNuevo.Text = "Nuevo";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 39);
            // 
            // tlsBtnModificar
            // 
            this.tlsBtnModificar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tlsBtnModificar.Image = ((System.Drawing.Image)(resources.GetObject("tlsBtnModificar.Image")));
            this.tlsBtnModificar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlsBtnModificar.MergeIndex = 3;
            this.tlsBtnModificar.Name = "tlsBtnModificar";
            this.tlsBtnModificar.Size = new System.Drawing.Size(36, 36);
            this.tlsBtnModificar.Text = "Modificar";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 39);
            // 
            // tlsBtnEliminar
            // 
            this.tlsBtnEliminar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tlsBtnEliminar.Image = ((System.Drawing.Image)(resources.GetObject("tlsBtnEliminar.Image")));
            this.tlsBtnEliminar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlsBtnEliminar.MergeIndex = 4;
            this.tlsBtnEliminar.Name = "tlsBtnEliminar";
            this.tlsBtnEliminar.Size = new System.Drawing.Size(36, 36);
            this.tlsBtnEliminar.Text = "Eliminar";
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 39);
            // 
            // tlsBtnBuscar
            // 
            this.tlsBtnBuscar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tlsBtnBuscar.Image = ((System.Drawing.Image)(resources.GetObject("tlsBtnBuscar.Image")));
            this.tlsBtnBuscar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlsBtnBuscar.MergeIndex = 5;
            this.tlsBtnBuscar.Name = "tlsBtnBuscar";
            this.tlsBtnBuscar.Size = new System.Drawing.Size(36, 36);
            this.tlsBtnBuscar.Text = "Buscar";
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 39);
            // 
            // tlsBtnCancelar
            // 
            this.tlsBtnCancelar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tlsBtnCancelar.Image = ((System.Drawing.Image)(resources.GetObject("tlsBtnCancelar.Image")));
            this.tlsBtnCancelar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlsBtnCancelar.MergeIndex = 6;
            this.tlsBtnCancelar.Name = "tlsBtnCancelar";
            this.tlsBtnCancelar.Size = new System.Drawing.Size(36, 36);
            this.tlsBtnCancelar.Text = "Cancelar";
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(6, 39);
            // 
            // tlsBtnSalir
            // 
            this.tlsBtnSalir.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tlsBtnSalir.Image = ((System.Drawing.Image)(resources.GetObject("tlsBtnSalir.Image")));
            this.tlsBtnSalir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlsBtnSalir.MergeIndex = 7;
            this.tlsBtnSalir.Name = "tlsBtnSalir";
            this.tlsBtnSalir.Size = new System.Drawing.Size(36, 36);
            this.tlsBtnSalir.Text = "Salir";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Location = new System.Drawing.Point(10, 49);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(740, 437);
            this.groupBox1.TabIndex = 16;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Producto Acompañamientos";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.lstvAcomp);
            this.groupBox3.Location = new System.Drawing.Point(6, 98);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(728, 333);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Acompañamientos";
            // 
            // lstvAcomp
            // 
            this.lstvAcomp.CheckBoxes = true;
            this.lstvAcomp.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colID,
            this.colName});
            this.lstvAcomp.FullRowSelect = true;
            this.lstvAcomp.HideSelection = false;
            this.lstvAcomp.Location = new System.Drawing.Point(9, 18);
            this.lstvAcomp.Margin = new System.Windows.Forms.Padding(2);
            this.lstvAcomp.MultiSelect = false;
            this.lstvAcomp.Name = "lstvAcomp";
            this.lstvAcomp.Size = new System.Drawing.Size(714, 310);
            this.lstvAcomp.TabIndex = 1;
            this.lstvAcomp.UseCompatibleStateImageBehavior = false;
            this.lstvAcomp.View = System.Windows.Forms.View.Details;
            this.lstvAcomp.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.lstvAcomp_ItemChecked);
            // 
            // colID
            // 
            this.colID.Text = "ID";
            this.colID.Width = 150;
            // 
            // colName
            // 
            this.colName.Text = "Nombre";
            this.colName.Width = 400;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.txtNombre);
            this.groupBox2.Controls.Add(this.btnBuscarProducto);
            this.groupBox2.Controls.Add(this.label16);
            this.groupBox2.Controls.Add(this.txtCodigoProducto);
            this.groupBox2.Location = new System.Drawing.Point(6, 19);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(728, 73);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Producto";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 49);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 13);
            this.label1.TabIndex = 100;
            this.label1.Text = "Nombre:";
            // 
            // txtNombre
            // 
            this.txtNombre.Enabled = false;
            this.txtNombre.Location = new System.Drawing.Point(102, 46);
            this.txtNombre.Margin = new System.Windows.Forms.Padding(4);
            this.txtNombre.MaxLength = 50;
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Size = new System.Drawing.Size(217, 20);
            this.txtNombre.TabIndex = 99;
            // 
            // btnBuscarProducto
            // 
            this.btnBuscarProducto.Image = ((System.Drawing.Image)(resources.GetObject("btnBuscarProducto.Image")));
            this.btnBuscarProducto.Location = new System.Drawing.Point(326, 24);
            this.btnBuscarProducto.Name = "btnBuscarProducto";
            this.btnBuscarProducto.Size = new System.Drawing.Size(40, 40);
            this.btnBuscarProducto.TabIndex = 97;
            this.btnBuscarProducto.UseVisualStyleBackColor = true;
            this.btnBuscarProducto.Click += new System.EventHandler(this.btnBuscarProducto_Click);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(6, 27);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(43, 13);
            this.label16.TabIndex = 98;
            this.label16.Text = "Código:";
            // 
            // txtCodigoProducto
            // 
            this.txtCodigoProducto.Enabled = false;
            this.txtCodigoProducto.Location = new System.Drawing.Point(102, 24);
            this.txtCodigoProducto.Margin = new System.Windows.Forms.Padding(4);
            this.txtCodigoProducto.MaxLength = 50;
            this.txtCodigoProducto.Name = "txtCodigoProducto";
            this.txtCodigoProducto.Size = new System.Drawing.Size(217, 20);
            this.txtCodigoProducto.TabIndex = 96;
            // 
            // frmProductoAcompanam
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(763, 493);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.tlsMenu);
            this.Name = "frmProductoAcompanam";
            this.Text = "Mantenimento: Asignar Acompañamientos Producto";
            this.Load += new System.EventHandler(this.frmProductoAcompanam_Load);
            this.tlsMenu.ResumeLayout(false);
            this.tlsMenu.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip tlsMenu;
        private System.Windows.Forms.ToolStripButton tlsBtnGuardar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tlsBtnNuevo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton tlsBtnModificar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton tlsBtnEliminar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton tlsBtnBuscar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripButton tlsBtnCancelar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripButton tlsBtnSalir;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.Button btnBuscarProducto;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox txtCodigoProducto;
        private System.Windows.Forms.ListView lstvAcomp;
        private System.Windows.Forms.ColumnHeader colID;
        private System.Windows.Forms.ColumnHeader colName;
    }
}