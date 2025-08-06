
namespace PresentationLayer
{
    partial class frmEtiquetas
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmEtiquetas));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkTodos = new System.Windows.Forms.CheckBox();
            this.lstvProductos = new System.Windows.Forms.ListView();
            this.colID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colPrecio = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colPromo = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnEliminar = new System.Windows.Forms.Button();
            this.lblNombreProducto = new System.Windows.Forms.Label();
            this.btnLimpiarForm = new System.Windows.Forms.Button();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.gbxFechas = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.dtpFin = new System.Windows.Forms.DateTimePicker();
            this.dtpInicio = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.txtCodigo = new System.Windows.Forms.TextBox();
            this.btnsalir = new System.Windows.Forms.PictureBox();
            this.btnReImprimir = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.copies = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.gbxFechas.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnsalir)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.copies)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkTodos);
            this.groupBox1.Controls.Add(this.lstvProductos);
            this.groupBox1.Controls.Add(this.btnEliminar);
            this.groupBox1.Location = new System.Drawing.Point(14, 127);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(768, 416);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Productos";
            // 
            // chkTodos
            // 
            this.chkTodos.AutoSize = true;
            this.chkTodos.Location = new System.Drawing.Point(7, 35);
            this.chkTodos.Name = "chkTodos";
            this.chkTodos.Size = new System.Drawing.Size(56, 17);
            this.chkTodos.TabIndex = 3;
            this.chkTodos.Text = "Todos";
            this.chkTodos.UseVisualStyleBackColor = true;
            this.chkTodos.CheckedChanged += new System.EventHandler(this.chkTodos_CheckedChanged);
            // 
            // lstvProductos
            // 
            this.lstvProductos.CheckBoxes = true;
            this.lstvProductos.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colID,
            this.colName,
            this.colPrecio,
            this.colPromo});
            this.lstvProductos.FullRowSelect = true;
            this.lstvProductos.HideSelection = false;
            this.lstvProductos.Location = new System.Drawing.Point(5, 58);
            this.lstvProductos.Margin = new System.Windows.Forms.Padding(2);
            this.lstvProductos.MultiSelect = false;
            this.lstvProductos.Name = "lstvProductos";
            this.lstvProductos.Size = new System.Drawing.Size(757, 344);
            this.lstvProductos.TabIndex = 1;
            this.lstvProductos.UseCompatibleStateImageBehavior = false;
            this.lstvProductos.View = System.Windows.Forms.View.Details;
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
            // colPrecio
            // 
            this.colPrecio.Text = "PrecioIVA";
            this.colPrecio.Width = 90;
            // 
            // colPromo
            // 
            this.colPromo.Text = "Promoción";
            this.colPromo.Width = 80;
            // 
            // btnEliminar
            // 
            this.btnEliminar.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnEliminar.BackgroundImage")));
            this.btnEliminar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnEliminar.Location = new System.Drawing.Point(720, 10);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(42, 43);
            this.btnEliminar.TabIndex = 2;
            this.btnEliminar.UseVisualStyleBackColor = true;
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
            // 
            // lblNombreProducto
            // 
            this.lblNombreProducto.AutoSize = true;
            this.lblNombreProducto.Location = new System.Drawing.Point(37, 55);
            this.lblNombreProducto.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblNombreProducto.Name = "lblNombreProducto";
            this.lblNombreProducto.Size = new System.Drawing.Size(53, 13);
            this.lblNombreProducto.TabIndex = 6;
            this.lblNombreProducto.Text = "Producto:";
            // 
            // btnLimpiarForm
            // 
            this.btnLimpiarForm.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLimpiarForm.ForeColor = System.Drawing.Color.Transparent;
            this.btnLimpiarForm.Image = ((System.Drawing.Image)(resources.GetObject("btnLimpiarForm.Image")));
            this.btnLimpiarForm.Location = new System.Drawing.Point(726, 62);
            this.btnLimpiarForm.Name = "btnLimpiarForm";
            this.btnLimpiarForm.Size = new System.Drawing.Size(55, 59);
            this.btnLimpiarForm.TabIndex = 35;
            this.btnLimpiarForm.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.btnLimpiarForm.UseVisualStyleBackColor = true;
            this.btnLimpiarForm.Click += new System.EventHandler(this.btnLimpiarForm_Click);
            // 
            // btnBuscar
            // 
            this.btnBuscar.Image = ((System.Drawing.Image)(resources.GetObject("btnBuscar.Image")));
            this.btnBuscar.Location = new System.Drawing.Point(664, 62);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(56, 59);
            this.btnBuscar.TabIndex = 37;
            this.btnBuscar.UseVisualStyleBackColor = true;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // gbxFechas
            // 
            this.gbxFechas.Controls.Add(this.label6);
            this.gbxFechas.Controls.Add(this.label5);
            this.gbxFechas.Controls.Add(this.dtpFin);
            this.gbxFechas.Controls.Add(this.dtpInicio);
            this.gbxFechas.Location = new System.Drawing.Point(95, 77);
            this.gbxFechas.Name = "gbxFechas";
            this.gbxFechas.Size = new System.Drawing.Size(283, 37);
            this.gbxFechas.TabIndex = 36;
            this.gbxFechas.TabStop = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(146, 15);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(15, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "al";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(7, 15);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(27, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "De: ";
            // 
            // dtpFin
            // 
            this.dtpFin.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFin.Location = new System.Drawing.Point(167, 11);
            this.dtpFin.Name = "dtpFin";
            this.dtpFin.Size = new System.Drawing.Size(101, 20);
            this.dtpFin.TabIndex = 2;
            this.dtpFin.ValueChanged += new System.EventHandler(this.dtpFin_ValueChanged);
            // 
            // dtpInicio
            // 
            this.dtpInicio.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpInicio.Location = new System.Drawing.Point(34, 11);
            this.dtpInicio.Name = "dtpInicio";
            this.dtpInicio.Size = new System.Drawing.Size(108, 20);
            this.dtpInicio.TabIndex = 0;
            this.dtpInicio.ValueChanged += new System.EventHandler(this.dtpInicio_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 88);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 13);
            this.label1.TabIndex = 38;
            this.label1.Text = "Fecha Ult Mod:";
            // 
            // txtCodigo
            // 
            this.txtCodigo.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCodigo.Location = new System.Drawing.Point(95, 53);
            this.txtCodigo.MaxLength = 20;
            this.txtCodigo.Name = "txtCodigo";
            this.txtCodigo.Size = new System.Drawing.Size(283, 23);
            this.txtCodigo.TabIndex = 40;
            this.txtCodigo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCodigo_KeyPress);
            // 
            // btnsalir
            // 
            this.btnsalir.Image = ((System.Drawing.Image)(resources.GetObject("btnsalir.Image")));
            this.btnsalir.Location = new System.Drawing.Point(761, 12);
            this.btnsalir.Name = "btnsalir";
            this.btnsalir.Size = new System.Drawing.Size(20, 20);
            this.btnsalir.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btnsalir.TabIndex = 95;
            this.btnsalir.TabStop = false;
            this.btnsalir.Click += new System.EventHandler(this.btnsalir_Click);
            // 
            // btnReImprimir
            // 
            this.btnReImprimir.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReImprimir.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnReImprimir.Image = ((System.Drawing.Image)(resources.GetObject("btnReImprimir.Image")));
            this.btnReImprimir.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnReImprimir.Location = new System.Drawing.Point(714, 549);
            this.btnReImprimir.Name = "btnReImprimir";
            this.btnReImprimir.Size = new System.Drawing.Size(68, 69);
            this.btnReImprimir.TabIndex = 96;
            this.btnReImprimir.Text = "Imprimir";
            this.btnReImprimir.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnReImprimir.UseVisualStyleBackColor = true;
            this.btnReImprimir.Click += new System.EventHandler(this.btnReImprimir_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(278, 12);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(235, 25);
            this.label2.TabIndex = 97;
            this.label2.Text = "Etiquetas de Productos";
            // 
            // copies
            // 
            this.copies.Location = new System.Drawing.Point(627, 575);
            this.copies.Name = "copies";
            this.copies.Size = new System.Drawing.Size(68, 20);
            this.copies.TabIndex = 98;
            this.copies.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(580, 577);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(42, 13);
            this.label3.TabIndex = 99;
            this.label3.Text = "Copias:";
            // 
            // frmEtiquetas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(793, 623);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.copies);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnReImprimir);
            this.Controls.Add(this.btnsalir);
            this.Controls.Add(this.txtCodigo);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnBuscar);
            this.Controls.Add(this.gbxFechas);
            this.Controls.Add(this.btnLimpiarForm);
            this.Controls.Add(this.lblNombreProducto);
            this.Controls.Add(this.groupBox1);
            this.Name = "frmEtiquetas";
            this.Text = "frmEtiquetas";
            this.Load += new System.EventHandler(this.frmEtiquetas_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.gbxFechas.ResumeLayout(false);
            this.gbxFechas.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnsalir)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.copies)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.Label lblNombreProducto;
        private System.Windows.Forms.Button btnLimpiarForm;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.GroupBox gbxFechas;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker dtpFin;
        private System.Windows.Forms.DateTimePicker dtpInicio;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListView lstvProductos;
        private System.Windows.Forms.ColumnHeader colID;
        private System.Windows.Forms.ColumnHeader colName;
        private System.Windows.Forms.ColumnHeader colPrecio;
        private System.Windows.Forms.TextBox txtCodigo;
        private System.Windows.Forms.PictureBox btnsalir;
        private System.Windows.Forms.Button btnReImprimir;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown copies;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox chkTodos;
        private System.Windows.Forms.ColumnHeader colPromo;
    }
}