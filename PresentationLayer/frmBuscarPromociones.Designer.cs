
namespace PresentationLayer
{
    partial class frmBuscarPromociones
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmBuscarPromociones));
            this.gboDatos = new System.Windows.Forms.GroupBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label28 = new System.Windows.Forms.Label();
            this.dtgvPromociones = new System.Windows.Forms.DataGridView();
            this.colIdTipoDoc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colIdProducto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colProducto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPrecio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colFechaInicio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colFechaFin = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDes = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPrecioPromo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEditar = new System.Windows.Forms.DataGridViewButtonColumn();
            this.colEliminar = new System.Windows.Forms.DataGridViewButtonColumn();
            this.btnAgregar = new System.Windows.Forms.Button();
            this.txtId = new System.Windows.Forms.TextBox();
            this.lblNombreProducto = new System.Windows.Forms.Label();
            this.gbxFechas = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.dtpFin = new System.Windows.Forms.DateTimePicker();
            this.dtpInicio = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.btnsalir = new System.Windows.Forms.PictureBox();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.btnRefrescar = new System.Windows.Forms.Button();
            this.cboEstado = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.chkFechas = new System.Windows.Forms.CheckBox();
            this.gboDatos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvPromociones)).BeginInit();
            this.gbxFechas.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnsalir)).BeginInit();
            this.SuspendLayout();
            // 
            // gboDatos
            // 
            this.gboDatos.Controls.Add(this.panel3);
            this.gboDatos.Controls.Add(this.label3);
            this.gboDatos.Controls.Add(this.panel2);
            this.gboDatos.Controls.Add(this.label1);
            this.gboDatos.Controls.Add(this.panel1);
            this.gboDatos.Controls.Add(this.label28);
            this.gboDatos.Controls.Add(this.dtgvPromociones);
            this.gboDatos.Controls.Add(this.btnAgregar);
            this.gboDatos.Location = new System.Drawing.Point(12, 146);
            this.gboDatos.Name = "gboDatos";
            this.gboDatos.Size = new System.Drawing.Size(1038, 500);
            this.gboDatos.TabIndex = 18;
            this.gboDatos.TabStop = false;
            this.gboDatos.Text = "Promociones";
            this.gboDatos.Enter += new System.EventHandler(this.gboDatos_Enter);
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Yellow;
            this.panel3.Location = new System.Drawing.Point(98, 19);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(28, 28);
            this.panel3.TabIndex = 122;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(128, 26);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 13);
            this.label3.TabIndex = 121;
            this.label3.Text = "Pendiente";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.LimeGreen;
            this.panel2.Location = new System.Drawing.Point(12, 19);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(28, 28);
            this.panel2.TabIndex = 122;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(42, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 13);
            this.label1.TabIndex = 121;
            this.label1.Text = "Activa";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Red;
            this.panel1.Location = new System.Drawing.Point(196, 19);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(28, 28);
            this.panel1.TabIndex = 120;
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(229, 26);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(51, 13);
            this.label28.TabIndex = 119;
            this.label28.Text = "Vencidas";
            // 
            // dtgvPromociones
            // 
            this.dtgvPromociones.AllowUserToDeleteRows = false;
            this.dtgvPromociones.AllowUserToOrderColumns = true;
            this.dtgvPromociones.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dtgvPromociones.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgvPromociones.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colIdTipoDoc,
            this.colIdProducto,
            this.colProducto,
            this.colPrecio,
            this.colFechaInicio,
            this.colFechaFin,
            this.colDes,
            this.colPrecioPromo,
            this.colEditar,
            this.colEliminar});
            this.dtgvPromociones.GridColor = System.Drawing.SystemColors.ActiveBorder;
            this.dtgvPromociones.Location = new System.Drawing.Point(12, 60);
            this.dtgvPromociones.MultiSelect = false;
            this.dtgvPromociones.Name = "dtgvPromociones";
            this.dtgvPromociones.ReadOnly = true;
            this.dtgvPromociones.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.dtgvPromociones.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dtgvPromociones.ShowEditingIcon = false;
            this.dtgvPromociones.Size = new System.Drawing.Size(1018, 421);
            this.dtgvPromociones.TabIndex = 25;
            this.dtgvPromociones.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dtgvPromociones_CellContentClick);
            // 
            // colIdTipoDoc
            // 
            this.colIdTipoDoc.HeaderText = "ID";
            this.colIdTipoDoc.Name = "colIdTipoDoc";
            this.colIdTipoDoc.ReadOnly = true;
            this.colIdTipoDoc.Width = 40;
            // 
            // colIdProducto
            // 
            this.colIdProducto.HeaderText = "Código Producto";
            this.colIdProducto.Name = "colIdProducto";
            this.colIdProducto.ReadOnly = true;
            // 
            // colProducto
            // 
            this.colProducto.HeaderText = "Producto";
            this.colProducto.Name = "colProducto";
            this.colProducto.ReadOnly = true;
            this.colProducto.Width = 200;
            // 
            // colPrecio
            // 
            this.colPrecio.HeaderText = "Precio";
            this.colPrecio.Name = "colPrecio";
            this.colPrecio.ReadOnly = true;
            this.colPrecio.Width = 80;
            // 
            // colFechaInicio
            // 
            this.colFechaInicio.HeaderText = "Fecha Inicio";
            this.colFechaInicio.Name = "colFechaInicio";
            this.colFechaInicio.ReadOnly = true;
            this.colFechaInicio.Width = 120;
            // 
            // colFechaFin
            // 
            this.colFechaFin.HeaderText = "Fecha Fin";
            this.colFechaFin.Name = "colFechaFin";
            this.colFechaFin.ReadOnly = true;
            this.colFechaFin.Width = 120;
            // 
            // colDes
            // 
            this.colDes.HeaderText = "Desc";
            this.colDes.Name = "colDes";
            this.colDes.ReadOnly = true;
            this.colDes.Width = 50;
            // 
            // colPrecioPromo
            // 
            this.colPrecioPromo.HeaderText = "Precio Promo";
            this.colPrecioPromo.Name = "colPrecioPromo";
            this.colPrecioPromo.ReadOnly = true;
            // 
            // colEditar
            // 
            this.colEditar.HeaderText = "";
            this.colEditar.Name = "colEditar";
            this.colEditar.ReadOnly = true;
            this.colEditar.Text = "Editar";
            this.colEditar.ToolTipText = "Editar";
            this.colEditar.Width = 80;
            // 
            // colEliminar
            // 
            this.colEliminar.HeaderText = "";
            this.colEliminar.Name = "colEliminar";
            this.colEliminar.ReadOnly = true;
            this.colEliminar.Text = "Eliminar";
            this.colEliminar.ToolTipText = "Eliminar";
            this.colEliminar.Width = 80;
            // 
            // btnAgregar
            // 
            this.btnAgregar.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnAgregar.BackgroundImage")));
            this.btnAgregar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnAgregar.Location = new System.Drawing.Point(988, 11);
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.Size = new System.Drawing.Size(42, 43);
            this.btnAgregar.TabIndex = 24;
            this.btnAgregar.UseVisualStyleBackColor = true;
            this.btnAgregar.Click += new System.EventHandler(this.btnAgregar_Click);
            // 
            // txtId
            // 
            this.txtId.Location = new System.Drawing.Point(99, 35);
            this.txtId.Margin = new System.Windows.Forms.Padding(2);
            this.txtId.Name = "txtId";
            this.txtId.Size = new System.Drawing.Size(230, 20);
            this.txtId.TabIndex = 20;
            // 
            // lblNombreProducto
            // 
            this.lblNombreProducto.AutoSize = true;
            this.lblNombreProducto.Location = new System.Drawing.Point(43, 38);
            this.lblNombreProducto.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblNombreProducto.Name = "lblNombreProducto";
            this.lblNombreProducto.Size = new System.Drawing.Size(53, 13);
            this.lblNombreProducto.TabIndex = 19;
            this.lblNombreProducto.Text = "Producto:";
            // 
            // gbxFechas
            // 
            this.gbxFechas.Controls.Add(this.label6);
            this.gbxFechas.Controls.Add(this.label5);
            this.gbxFechas.Controls.Add(this.dtpFin);
            this.gbxFechas.Controls.Add(this.dtpInicio);
            this.gbxFechas.Location = new System.Drawing.Point(16, 103);
            this.gbxFechas.Name = "gbxFechas";
            this.gbxFechas.Size = new System.Drawing.Size(321, 37);
            this.gbxFechas.TabIndex = 21;
            this.gbxFechas.TabStop = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(192, 15);
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
            this.label5.Size = new System.Drawing.Size(68, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "Fecha Inicio:";
            // 
            // dtpFin
            // 
            this.dtpFin.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFin.Location = new System.Drawing.Point(213, 11);
            this.dtpFin.Name = "dtpFin";
            this.dtpFin.Size = new System.Drawing.Size(101, 20);
            this.dtpFin.TabIndex = 2;
            // 
            // dtpInicio
            // 
            this.dtpInicio.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpInicio.Location = new System.Drawing.Point(80, 11);
            this.dtpInicio.Name = "dtpInicio";
            this.dtpInicio.Size = new System.Drawing.Size(108, 20);
            this.dtpInicio.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(374, 9);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(284, 25);
            this.label2.TabIndex = 98;
            this.label2.Text = "Mantenimiento Promociones";
            // 
            // btnsalir
            // 
            this.btnsalir.Image = ((System.Drawing.Image)(resources.GetObject("btnsalir.Image")));
            this.btnsalir.Location = new System.Drawing.Point(1030, 6);
            this.btnsalir.Name = "btnsalir";
            this.btnsalir.Size = new System.Drawing.Size(20, 20);
            this.btnsalir.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btnsalir.TabIndex = 99;
            this.btnsalir.TabStop = false;
            this.btnsalir.Click += new System.EventHandler(this.btnsalir_Click);
            // 
            // btnBuscar
            // 
            this.btnBuscar.Image = ((System.Drawing.Image)(resources.GetObject("btnBuscar.Image")));
            this.btnBuscar.Location = new System.Drawing.Point(360, 72);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(58, 51);
            this.btnBuscar.TabIndex = 22;
            this.btnBuscar.UseVisualStyleBackColor = true;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // btnRefrescar
            // 
            this.btnRefrescar.Image = ((System.Drawing.Image)(resources.GetObject("btnRefrescar.Image")));
            this.btnRefrescar.Location = new System.Drawing.Point(424, 72);
            this.btnRefrescar.Name = "btnRefrescar";
            this.btnRefrescar.Size = new System.Drawing.Size(51, 50);
            this.btnRefrescar.TabIndex = 37;
            this.btnRefrescar.UseVisualStyleBackColor = true;
            this.btnRefrescar.Click += new System.EventHandler(this.btnRefrescar_Click);
            // 
            // cboEstado
            // 
            this.cboEstado.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboEstado.FormattingEnabled = true;
            this.cboEstado.Items.AddRange(new object[] {
            "Todas",
            "Activas",
            "Pendientes",
            "Vencidas"});
            this.cboEstado.Location = new System.Drawing.Point(99, 64);
            this.cboEstado.Name = "cboEstado";
            this.cboEstado.Size = new System.Drawing.Size(121, 21);
            this.cboEstado.TabIndex = 100;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(51, 66);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(43, 13);
            this.label4.TabIndex = 101;
            this.label4.Text = "Estado:";
            // 
            // chkFechas
            // 
            this.chkFechas.AutoSize = true;
            this.chkFechas.Location = new System.Drawing.Point(16, 90);
            this.chkFechas.Name = "chkFechas";
            this.chkFechas.Size = new System.Drawing.Size(61, 17);
            this.chkFechas.TabIndex = 13;
            this.chkFechas.Text = "Fechas";
            this.chkFechas.UseVisualStyleBackColor = true;
            this.chkFechas.CheckedChanged += new System.EventHandler(this.chkFechas_CheckedChanged);
            // 
            // frmBuscarPromociones
            // 
            this.AcceptButton = this.btnBuscar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(1058, 647);
            this.Controls.Add(this.chkFechas);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cboEstado);
            this.Controls.Add(this.btnRefrescar);
            this.Controls.Add(this.btnsalir);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnBuscar);
            this.Controls.Add(this.gbxFechas);
            this.Controls.Add(this.txtId);
            this.Controls.Add(this.lblNombreProducto);
            this.Controls.Add(this.gboDatos);
            this.Name = "frmBuscarPromociones";
            this.Text = "frmPromociones";
            this.Load += new System.EventHandler(this.frmPromociones_Load);
            this.gboDatos.ResumeLayout(false);
            this.gboDatos.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvPromociones)).EndInit();
            this.gbxFechas.ResumeLayout(false);
            this.gbxFechas.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnsalir)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.GroupBox gboDatos;
        private System.Windows.Forms.TextBox txtId;
        private System.Windows.Forms.Label lblNombreProducto;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.GroupBox gbxFechas;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker dtpFin;
        private System.Windows.Forms.DateTimePicker dtpInicio;
        private System.Windows.Forms.Button btnAgregar;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox btnsalir;
        private System.Windows.Forms.DataGridView dtgvPromociones;
        private System.Windows.Forms.DataGridViewTextBoxColumn colIdTipoDoc;
        private System.Windows.Forms.DataGridViewTextBoxColumn colIdProducto;
        private System.Windows.Forms.DataGridViewTextBoxColumn colProducto;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPrecio;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFechaInicio;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFechaFin;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDes;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPrecioPromo;
        private System.Windows.Forms.DataGridViewButtonColumn colEditar;
        private System.Windows.Forms.DataGridViewButtonColumn colEliminar;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnRefrescar;
        private System.Windows.Forms.ComboBox cboEstado;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox chkFechas;
    }
}