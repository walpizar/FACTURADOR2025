
namespace PresentationLayer
{
    partial class frmPromociones
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPromociones));
            this.lblTitulo = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.gbxPromocion = new System.Windows.Forms.GroupBox();
            this.txtDescuento = new System.Windows.Forms.TextBox();
            this.dtpFin = new System.Windows.Forms.DateTimePicker();
            this.dtpInicio = new System.Windows.Forms.DateTimePicker();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.txtPrecioVenta1Promo = new System.Windows.Forms.TextBox();
            this.label24 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.txtUtilidad1Promo = new System.Windows.Forms.TextBox();
            this.gbxProducto = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtImp = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtPrecioReal = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.txtPrecioVenta3 = new System.Windows.Forms.TextBox();
            this.txtPrecioVenta2 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.txtUtilidad3 = new System.Windows.Forms.TextBox();
            this.txtUtilidad2 = new System.Windows.Forms.TextBox();
            this.txtNombreProducto = new System.Windows.Forms.TextBox();
            this.txtCodigoProducto = new System.Windows.Forms.TextBox();
            this.lblNombreProducto = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtUtilidad1 = new System.Windows.Forms.TextBox();
            this.lblCodigoProducto = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.txtPrecioVenta1 = new System.Windows.Forms.TextBox();
            this.btnAccion = new System.Windows.Forms.Button();
            this.btnCerrar = new System.Windows.Forms.Button();
            this.btnsalir = new System.Windows.Forms.PictureBox();
            this.groupBox1.SuspendLayout();
            this.gbxPromocion.SuspendLayout();
            this.gbxProducto.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnsalir)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo.Location = new System.Drawing.Point(173, 21);
            this.lblTitulo.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(137, 25);
            this.lblTitulo.TabIndex = 99;
            this.lblTitulo.Text = "Promociones";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.gbxPromocion);
            this.groupBox1.Controls.Add(this.gbxProducto);
            this.groupBox1.Location = new System.Drawing.Point(20, 49);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(449, 447);
            this.groupBox1.TabIndex = 100;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Datos Promoción";
            // 
            // gbxPromocion
            // 
            this.gbxPromocion.Controls.Add(this.txtDescuento);
            this.gbxPromocion.Controls.Add(this.dtpFin);
            this.gbxPromocion.Controls.Add(this.dtpInicio);
            this.gbxPromocion.Controls.Add(this.label12);
            this.gbxPromocion.Controls.Add(this.label11);
            this.gbxPromocion.Controls.Add(this.label10);
            this.gbxPromocion.Controls.Add(this.label22);
            this.gbxPromocion.Controls.Add(this.txtPrecioVenta1Promo);
            this.gbxPromocion.Controls.Add(this.label24);
            this.gbxPromocion.Controls.Add(this.label23);
            this.gbxPromocion.Controls.Add(this.txtUtilidad1Promo);
            this.gbxPromocion.Location = new System.Drawing.Point(16, 274);
            this.gbxPromocion.Name = "gbxPromocion";
            this.gbxPromocion.Size = new System.Drawing.Size(415, 149);
            this.gbxPromocion.TabIndex = 102;
            this.gbxPromocion.TabStop = false;
            this.gbxPromocion.Text = "Promoción";
            // 
            // txtDescuento
            // 
            this.txtDescuento.Location = new System.Drawing.Point(89, 74);
            this.txtDescuento.Margin = new System.Windows.Forms.Padding(2);
            this.txtDescuento.MaxLength = 30;
            this.txtDescuento.Name = "txtDescuento";
            this.txtDescuento.ReadOnly = true;
            this.txtDescuento.Size = new System.Drawing.Size(56, 20);
            this.txtDescuento.TabIndex = 128;
            this.txtDescuento.TextChanged += new System.EventHandler(this.txtDescuento_TextChanged);
            this.txtDescuento.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDescuento_KeyPress);
            // 
            // dtpFin
            // 
            this.dtpFin.CustomFormat = "dd/MM/yyyy HH:mm:ss";
            this.dtpFin.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFin.Location = new System.Drawing.Point(88, 50);
            this.dtpFin.Name = "dtpFin";
            this.dtpFin.Size = new System.Drawing.Size(150, 20);
            this.dtpFin.TabIndex = 133;
            // 
            // dtpInicio
            // 
            this.dtpInicio.CustomFormat = "dd/MM/yyyy HH:mm:ss";
            this.dtpInicio.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpInicio.Location = new System.Drawing.Point(88, 26);
            this.dtpInicio.MinDate = new System.DateTime(2022, 1, 1, 0, 0, 0, 0);
            this.dtpInicio.Name = "dtpInicio";
            this.dtpInicio.Size = new System.Drawing.Size(150, 20);
            this.dtpInicio.TabIndex = 132;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(15, 29);
            this.label12.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(68, 13);
            this.label12.TabIndex = 131;
            this.label12.Text = "Fecha Inicio:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(26, 53);
            this.label11.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(57, 13);
            this.label11.TabIndex = 130;
            this.label11.Text = "Fecha Fin:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(21, 77);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(62, 13);
            this.label10.TabIndex = 128;
            this.label10.Text = "Descuento:";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(33, 105);
            this.label22.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(54, 13);
            this.label22.TabIndex = 129;
            this.label22.Text = "Utilidad 1:";
            // 
            // txtPrecioVenta1Promo
            // 
            this.txtPrecioVenta1Promo.Location = new System.Drawing.Point(296, 102);
            this.txtPrecioVenta1Promo.Margin = new System.Windows.Forms.Padding(2);
            this.txtPrecioVenta1Promo.MaxLength = 30;
            this.txtPrecioVenta1Promo.Name = "txtPrecioVenta1Promo";
            this.txtPrecioVenta1Promo.Size = new System.Drawing.Size(78, 20);
            this.txtPrecioVenta1Promo.TabIndex = 131;
            this.txtPrecioVenta1Promo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPrecioVenta1Promo_KeyPress);
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(213, 105);
            this.label24.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(80, 13);
            this.label24.TabIndex = 132;
            this.label24.Text = "Precio Venta 1:";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label23.Location = new System.Drawing.Point(145, 105);
            this.label23.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(20, 17);
            this.label23.TabIndex = 130;
            this.label23.Text = "%";
            // 
            // txtUtilidad1Promo
            // 
            this.txtUtilidad1Promo.Enabled = false;
            this.txtUtilidad1Promo.Location = new System.Drawing.Point(89, 102);
            this.txtUtilidad1Promo.Margin = new System.Windows.Forms.Padding(2);
            this.txtUtilidad1Promo.MaxLength = 30;
            this.txtUtilidad1Promo.Name = "txtUtilidad1Promo";
            this.txtUtilidad1Promo.Size = new System.Drawing.Size(56, 20);
            this.txtUtilidad1Promo.TabIndex = 128;
            // 
            // gbxProducto
            // 
            this.gbxProducto.Controls.Add(this.label8);
            this.gbxProducto.Controls.Add(this.btnBuscar);
            this.gbxProducto.Controls.Add(this.label7);
            this.gbxProducto.Controls.Add(this.label5);
            this.gbxProducto.Controls.Add(this.label3);
            this.gbxProducto.Controls.Add(this.txtImp);
            this.gbxProducto.Controls.Add(this.label2);
            this.gbxProducto.Controls.Add(this.txtPrecioReal);
            this.gbxProducto.Controls.Add(this.label18);
            this.gbxProducto.Controls.Add(this.label19);
            this.gbxProducto.Controls.Add(this.txtPrecioVenta3);
            this.gbxProducto.Controls.Add(this.txtPrecioVenta2);
            this.gbxProducto.Controls.Add(this.label4);
            this.gbxProducto.Controls.Add(this.label9);
            this.gbxProducto.Controls.Add(this.txtUtilidad3);
            this.gbxProducto.Controls.Add(this.txtUtilidad2);
            this.gbxProducto.Controls.Add(this.txtNombreProducto);
            this.gbxProducto.Controls.Add(this.txtCodigoProducto);
            this.gbxProducto.Controls.Add(this.lblNombreProducto);
            this.gbxProducto.Controls.Add(this.label1);
            this.gbxProducto.Controls.Add(this.txtUtilidad1);
            this.gbxProducto.Controls.Add(this.lblCodigoProducto);
            this.gbxProducto.Controls.Add(this.label6);
            this.gbxProducto.Controls.Add(this.label20);
            this.gbxProducto.Controls.Add(this.txtPrecioVenta1);
            this.gbxProducto.Location = new System.Drawing.Point(16, 19);
            this.gbxProducto.Name = "gbxProducto";
            this.gbxProducto.Size = new System.Drawing.Size(415, 249);
            this.gbxProducto.TabIndex = 101;
            this.gbxProducto.TabStop = false;
            this.gbxProducto.Text = "Producto";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(347, 130);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(20, 17);
            this.label8.TabIndex = 127;
            this.label8.Text = "%";
            // 
            // btnBuscar
            // 
            this.btnBuscar.Image = ((System.Drawing.Image)(resources.GetObject("btnBuscar.Image")));
            this.btnBuscar.Location = new System.Drawing.Point(224, 11);
            this.btnBuscar.Margin = new System.Windows.Forms.Padding(2);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(29, 27);
            this.btnBuscar.TabIndex = 126;
            this.btnBuscar.UseVisualStyleBackColor = true;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(148, 209);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(20, 17);
            this.label7.TabIndex = 125;
            this.label7.Text = "%";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(148, 184);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(20, 17);
            this.label5.TabIndex = 124;
            this.label5.Text = "%";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(221, 132);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 13);
            this.label3.TabIndex = 123;
            this.label3.Text = "Impuesto:";
            // 
            // txtImp
            // 
            this.txtImp.Enabled = false;
            this.txtImp.Location = new System.Drawing.Point(299, 129);
            this.txtImp.Margin = new System.Windows.Forms.Padding(2);
            this.txtImp.MaxLength = 30;
            this.txtImp.Name = "txtImp";
            this.txtImp.Size = new System.Drawing.Size(44, 20);
            this.txtImp.TabIndex = 122;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 132);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 13);
            this.label2.TabIndex = 121;
            this.label2.Text = "Precio Costo:";
            // 
            // txtPrecioReal
            // 
            this.txtPrecioReal.Enabled = false;
            this.txtPrecioReal.Location = new System.Drawing.Point(92, 129);
            this.txtPrecioReal.Margin = new System.Windows.Forms.Padding(2);
            this.txtPrecioReal.MaxLength = 30;
            this.txtPrecioReal.Name = "txtPrecioReal";
            this.txtPrecioReal.Size = new System.Drawing.Size(99, 20);
            this.txtPrecioReal.TabIndex = 120;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(216, 214);
            this.label18.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(80, 13);
            this.label18.TabIndex = 119;
            this.label18.Text = "Precio Venta 3:";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(216, 189);
            this.label19.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(80, 13);
            this.label19.TabIndex = 118;
            this.label19.Text = "Precio Venta 2:";
            // 
            // txtPrecioVenta3
            // 
            this.txtPrecioVenta3.Enabled = false;
            this.txtPrecioVenta3.Location = new System.Drawing.Point(299, 211);
            this.txtPrecioVenta3.Margin = new System.Windows.Forms.Padding(2);
            this.txtPrecioVenta3.MaxLength = 30;
            this.txtPrecioVenta3.Name = "txtPrecioVenta3";
            this.txtPrecioVenta3.Size = new System.Drawing.Size(78, 20);
            this.txtPrecioVenta3.TabIndex = 117;
            // 
            // txtPrecioVenta2
            // 
            this.txtPrecioVenta2.Enabled = false;
            this.txtPrecioVenta2.Location = new System.Drawing.Point(299, 186);
            this.txtPrecioVenta2.Margin = new System.Windows.Forms.Padding(2);
            this.txtPrecioVenta2.MaxLength = 30;
            this.txtPrecioVenta2.Name = "txtPrecioVenta2";
            this.txtPrecioVenta2.Size = new System.Drawing.Size(78, 20);
            this.txtPrecioVenta2.TabIndex = 116;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(36, 211);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(54, 13);
            this.label4.TabIndex = 115;
            this.label4.Text = "Utilidad 3:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(36, 186);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(54, 13);
            this.label9.TabIndex = 114;
            this.label9.Text = "Utilidad 2:";
            // 
            // txtUtilidad3
            // 
            this.txtUtilidad3.Enabled = false;
            this.txtUtilidad3.Location = new System.Drawing.Point(92, 208);
            this.txtUtilidad3.Margin = new System.Windows.Forms.Padding(2);
            this.txtUtilidad3.MaxLength = 30;
            this.txtUtilidad3.Name = "txtUtilidad3";
            this.txtUtilidad3.Size = new System.Drawing.Size(56, 20);
            this.txtUtilidad3.TabIndex = 113;
            // 
            // txtUtilidad2
            // 
            this.txtUtilidad2.Enabled = false;
            this.txtUtilidad2.Location = new System.Drawing.Point(92, 183);
            this.txtUtilidad2.Margin = new System.Windows.Forms.Padding(2);
            this.txtUtilidad2.MaxLength = 30;
            this.txtUtilidad2.Name = "txtUtilidad2";
            this.txtUtilidad2.Size = new System.Drawing.Size(56, 20);
            this.txtUtilidad2.TabIndex = 112;
            // 
            // txtNombreProducto
            // 
            this.txtNombreProducto.Enabled = false;
            this.txtNombreProducto.Location = new System.Drawing.Point(92, 44);
            this.txtNombreProducto.Margin = new System.Windows.Forms.Padding(2);
            this.txtNombreProducto.MaxLength = 160;
            this.txtNombreProducto.Multiline = true;
            this.txtNombreProducto.Name = "txtNombreProducto";
            this.txtNombreProducto.Size = new System.Drawing.Size(285, 78);
            this.txtNombreProducto.TabIndex = 105;
            // 
            // txtCodigoProducto
            // 
            this.txtCodigoProducto.Enabled = false;
            this.txtCodigoProducto.Location = new System.Drawing.Point(92, 18);
            this.txtCodigoProducto.Margin = new System.Windows.Forms.Padding(2);
            this.txtCodigoProducto.MaxLength = 50;
            this.txtCodigoProducto.Name = "txtCodigoProducto";
            this.txtCodigoProducto.Size = new System.Drawing.Size(127, 20);
            this.txtCodigoProducto.TabIndex = 104;
            // 
            // lblNombreProducto
            // 
            this.lblNombreProducto.AutoSize = true;
            this.lblNombreProducto.Location = new System.Drawing.Point(40, 44);
            this.lblNombreProducto.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblNombreProducto.Name = "lblNombreProducto";
            this.lblNombreProducto.Size = new System.Drawing.Size(47, 13);
            this.lblNombreProducto.TabIndex = 106;
            this.lblNombreProducto.Text = "Nombre:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(36, 162);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 13);
            this.label1.TabIndex = 108;
            this.label1.Text = "Utilidad 1:";
            // 
            // txtUtilidad1
            // 
            this.txtUtilidad1.Enabled = false;
            this.txtUtilidad1.Location = new System.Drawing.Point(92, 159);
            this.txtUtilidad1.Margin = new System.Windows.Forms.Padding(2);
            this.txtUtilidad1.MaxLength = 30;
            this.txtUtilidad1.Name = "txtUtilidad1";
            this.txtUtilidad1.Size = new System.Drawing.Size(56, 20);
            this.txtUtilidad1.TabIndex = 107;
            // 
            // lblCodigoProducto
            // 
            this.lblCodigoProducto.AutoSize = true;
            this.lblCodigoProducto.Location = new System.Drawing.Point(44, 20);
            this.lblCodigoProducto.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblCodigoProducto.Name = "lblCodigoProducto";
            this.lblCodigoProducto.Size = new System.Drawing.Size(43, 13);
            this.lblCodigoProducto.TabIndex = 103;
            this.lblCodigoProducto.Text = "Codigo:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(148, 162);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(20, 17);
            this.label6.TabIndex = 109;
            this.label6.Text = "%";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(216, 165);
            this.label20.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(80, 13);
            this.label20.TabIndex = 111;
            this.label20.Text = "Precio Venta 1:";
            // 
            // txtPrecioVenta1
            // 
            this.txtPrecioVenta1.Enabled = false;
            this.txtPrecioVenta1.Location = new System.Drawing.Point(299, 162);
            this.txtPrecioVenta1.Margin = new System.Windows.Forms.Padding(2);
            this.txtPrecioVenta1.MaxLength = 30;
            this.txtPrecioVenta1.Name = "txtPrecioVenta1";
            this.txtPrecioVenta1.Size = new System.Drawing.Size(78, 20);
            this.txtPrecioVenta1.TabIndex = 110;
            // 
            // btnAccion
            // 
            this.btnAccion.Location = new System.Drawing.Point(371, 502);
            this.btnAccion.Name = "btnAccion";
            this.btnAccion.Size = new System.Drawing.Size(99, 38);
            this.btnAccion.TabIndex = 101;
            this.btnAccion.Text = "btn";
            this.btnAccion.UseVisualStyleBackColor = true;
            this.btnAccion.Click += new System.EventHandler(this.btnAccion_Click);
            // 
            // btnCerrar
            // 
            this.btnCerrar.Location = new System.Drawing.Point(265, 502);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(99, 38);
            this.btnCerrar.TabIndex = 102;
            this.btnCerrar.Text = "Cerrar";
            this.btnCerrar.UseVisualStyleBackColor = true;
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);
            // 
            // btnsalir
            // 
            this.btnsalir.Image = ((System.Drawing.Image)(resources.GetObject("btnsalir.Image")));
            this.btnsalir.Location = new System.Drawing.Point(450, 12);
            this.btnsalir.Name = "btnsalir";
            this.btnsalir.Size = new System.Drawing.Size(20, 20);
            this.btnsalir.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btnsalir.TabIndex = 103;
            this.btnsalir.TabStop = false;
            this.btnsalir.Click += new System.EventHandler(this.btnsalir_Click);
            // 
            // frmPromociones
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(482, 555);
            this.Controls.Add(this.btnsalir);
            this.Controls.Add(this.btnCerrar);
            this.Controls.Add(this.btnAccion);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lblTitulo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmPromociones";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmPromociones";
            this.Load += new System.EventHandler(this.frmPromociones_Load);
            this.groupBox1.ResumeLayout(false);
            this.gbxPromocion.ResumeLayout(false);
            this.gbxPromocion.PerformLayout();
            this.gbxProducto.ResumeLayout(false);
            this.gbxProducto.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnsalir)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnAccion;
        private System.Windows.Forms.Button btnCerrar;
        private System.Windows.Forms.GroupBox gbxPromocion;
        private System.Windows.Forms.GroupBox gbxProducto;
        private System.Windows.Forms.TextBox txtNombreProducto;
        private System.Windows.Forms.TextBox txtCodigoProducto;
        private System.Windows.Forms.Label lblNombreProducto;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtUtilidad1;
        private System.Windows.Forms.Label lblCodigoProducto;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.TextBox txtPrecioVenta1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtUtilidad3;
        private System.Windows.Forms.TextBox txtUtilidad2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtPrecioReal;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TextBox txtPrecioVenta3;
        private System.Windows.Forms.TextBox txtPrecioVenta2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtImp;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.DateTimePicker dtpInicio;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.DateTimePicker dtpFin;
        private System.Windows.Forms.PictureBox btnsalir;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.TextBox txtPrecioVenta1Promo;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.TextBox txtUtilidad1Promo;
        private System.Windows.Forms.TextBox txtDescuento;
    }
}