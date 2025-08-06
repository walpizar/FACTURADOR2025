namespace PresentationLayer
{
    partial class frmParametrosEmpresa
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmParametrosEmpresa));
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
            this.gbxDatos = new System.Windows.Forms.GroupBox();
            this.chkEtiquetas = new System.Windows.Forms.CheckBox();
            this.chkPromociones = new System.Windows.Forms.CheckBox();
            this.chkPrecioVariable = new System.Windows.Forms.CheckBox();
            this.chkCierreCajaAdmin = new System.Windows.Forms.CheckBox();
            this.chkAprobacionEliminar = new System.Windows.Forms.CheckBox();
            this.mskCantComandas = new System.Windows.Forms.MaskedTextBox();
            this.lblCantComandas = new System.Windows.Forms.Label();
            this.chkCierreCorreo = new System.Windows.Forms.CheckBox();
            this.cboTipoComanda = new System.Windows.Forms.ComboBox();
            this.chkComanda = new System.Windows.Forms.CheckBox();
            this.txtSucursal = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.txtCodigoPais = new System.Windows.Forms.TextBox();
            this.txtCaja = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.btnRuta = new System.Windows.Forms.Button();
            this.txtRutaBackup = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.txtPorcServicioMesa = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.chkServicioMesa = new System.Windows.Forms.CheckBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.txtPrecioBase = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtPlazoMaxProf = new System.Windows.Forms.TextBox();
            this.chkObligaClienteFacturacion = new System.Windows.Forms.CheckBox();
            this.chkFacturacionElectronica = new System.Windows.Forms.CheckBox();
            this.chkAprobDes = new System.Windows.Forms.CheckBox();
            this.chkManejaInventario = new System.Windows.Forms.CheckBox();
            this.txtPlazoCredMax = new System.Windows.Forms.TextBox();
            this.txtDescBase = new System.Windows.Forms.TextBox();
            this.txtCambioDolar = new System.Windows.Forms.TextBox();
            this.txtUtilidadBase = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.chkValidaCabys = new System.Windows.Forms.CheckBox();
            this.tlsMenu.SuspendLayout();
            this.gbxDatos.SuspendLayout();
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
            this.tlsMenu.Size = new System.Drawing.Size(447, 39);
            this.tlsMenu.TabIndex = 13;
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
            // gbxDatos
            // 
            this.gbxDatos.Controls.Add(this.chkValidaCabys);
            this.gbxDatos.Controls.Add(this.chkEtiquetas);
            this.gbxDatos.Controls.Add(this.chkPromociones);
            this.gbxDatos.Controls.Add(this.chkPrecioVariable);
            this.gbxDatos.Controls.Add(this.chkCierreCajaAdmin);
            this.gbxDatos.Controls.Add(this.chkAprobacionEliminar);
            this.gbxDatos.Controls.Add(this.mskCantComandas);
            this.gbxDatos.Controls.Add(this.lblCantComandas);
            this.gbxDatos.Controls.Add(this.chkCierreCorreo);
            this.gbxDatos.Controls.Add(this.cboTipoComanda);
            this.gbxDatos.Controls.Add(this.chkComanda);
            this.gbxDatos.Controls.Add(this.txtSucursal);
            this.gbxDatos.Controls.Add(this.label16);
            this.gbxDatos.Controls.Add(this.txtCodigoPais);
            this.gbxDatos.Controls.Add(this.txtCaja);
            this.gbxDatos.Controls.Add(this.label17);
            this.gbxDatos.Controls.Add(this.label18);
            this.gbxDatos.Controls.Add(this.btnRuta);
            this.gbxDatos.Controls.Add(this.txtRutaBackup);
            this.gbxDatos.Controls.Add(this.label14);
            this.gbxDatos.Controls.Add(this.txtPorcServicioMesa);
            this.gbxDatos.Controls.Add(this.label13);
            this.gbxDatos.Controls.Add(this.chkServicioMesa);
            this.gbxDatos.Controls.Add(this.label12);
            this.gbxDatos.Controls.Add(this.label11);
            this.gbxDatos.Controls.Add(this.txtPrecioBase);
            this.gbxDatos.Controls.Add(this.label10);
            this.gbxDatos.Controls.Add(this.label9);
            this.gbxDatos.Controls.Add(this.label7);
            this.gbxDatos.Controls.Add(this.label4);
            this.gbxDatos.Controls.Add(this.label3);
            this.gbxDatos.Controls.Add(this.label2);
            this.gbxDatos.Controls.Add(this.txtPlazoMaxProf);
            this.gbxDatos.Controls.Add(this.chkObligaClienteFacturacion);
            this.gbxDatos.Controls.Add(this.chkFacturacionElectronica);
            this.gbxDatos.Controls.Add(this.chkAprobDes);
            this.gbxDatos.Controls.Add(this.chkManejaInventario);
            this.gbxDatos.Controls.Add(this.txtPlazoCredMax);
            this.gbxDatos.Controls.Add(this.txtDescBase);
            this.gbxDatos.Controls.Add(this.txtCambioDolar);
            this.gbxDatos.Controls.Add(this.txtUtilidadBase);
            this.gbxDatos.Controls.Add(this.label5);
            this.gbxDatos.Controls.Add(this.label6);
            this.gbxDatos.Controls.Add(this.label1);
            this.gbxDatos.Controls.Add(this.label8);
            this.gbxDatos.Controls.Add(this.label15);
            this.gbxDatos.Location = new System.Drawing.Point(9, 34);
            this.gbxDatos.Margin = new System.Windows.Forms.Padding(2);
            this.gbxDatos.Name = "gbxDatos";
            this.gbxDatos.Padding = new System.Windows.Forms.Padding(2);
            this.gbxDatos.Size = new System.Drawing.Size(425, 502);
            this.gbxDatos.TabIndex = 14;
            this.gbxDatos.TabStop = false;
            this.gbxDatos.Text = "Parámetros Empresa";
            // 
            // chkEtiquetas
            // 
            this.chkEtiquetas.AutoSize = true;
            this.chkEtiquetas.Location = new System.Drawing.Point(234, 280);
            this.chkEtiquetas.Margin = new System.Windows.Forms.Padding(2);
            this.chkEtiquetas.Name = "chkEtiquetas";
            this.chkEtiquetas.Size = new System.Drawing.Size(70, 17);
            this.chkEtiquetas.TabIndex = 88;
            this.chkEtiquetas.Text = "Etiquetas";
            this.chkEtiquetas.UseVisualStyleBackColor = true;
            // 
            // chkPromociones
            // 
            this.chkPromociones.AutoSize = true;
            this.chkPromociones.Location = new System.Drawing.Point(234, 302);
            this.chkPromociones.Margin = new System.Windows.Forms.Padding(2);
            this.chkPromociones.Name = "chkPromociones";
            this.chkPromociones.Size = new System.Drawing.Size(87, 17);
            this.chkPromociones.TabIndex = 87;
            this.chkPromociones.Text = "Promociones";
            this.chkPromociones.UseVisualStyleBackColor = true;
            // 
            // chkPrecioVariable
            // 
            this.chkPrecioVariable.AutoSize = true;
            this.chkPrecioVariable.Location = new System.Drawing.Point(4, 474);
            this.chkPrecioVariable.Margin = new System.Windows.Forms.Padding(2);
            this.chkPrecioVariable.Name = "chkPrecioVariable";
            this.chkPrecioVariable.Size = new System.Drawing.Size(280, 17);
            this.chkPrecioVariable.TabIndex = 86;
            this.chkPrecioVariable.Text = "Permite cambiar el precio del producto en Facturación";
            this.chkPrecioVariable.UseVisualStyleBackColor = true;
            // 
            // chkCierreCajaAdmin
            // 
            this.chkCierreCajaAdmin.AutoSize = true;
            this.chkCierreCajaAdmin.Location = new System.Drawing.Point(4, 454);
            this.chkCierreCajaAdmin.Margin = new System.Windows.Forms.Padding(2);
            this.chkCierreCajaAdmin.Name = "chkCierreCajaAdmin";
            this.chkCierreCajaAdmin.Size = new System.Drawing.Size(133, 17);
            this.chkCierreCajaAdmin.TabIndex = 85;
            this.chkCierreCajaAdmin.Text = "Solo Admin Cierra Caja";
            this.chkCierreCajaAdmin.UseVisualStyleBackColor = true;
            // 
            // chkAprobacionEliminar
            // 
            this.chkAprobacionEliminar.AutoSize = true;
            this.chkAprobacionEliminar.Location = new System.Drawing.Point(4, 433);
            this.chkAprobacionEliminar.Margin = new System.Windows.Forms.Padding(2);
            this.chkAprobacionEliminar.Name = "chkAprobacionEliminar";
            this.chkAprobacionEliminar.Size = new System.Drawing.Size(229, 17);
            this.chkAprobacionEliminar.TabIndex = 84;
            this.chkAprobacionEliminar.Text = "Aprobación Eliminar Productos Facturación";
            this.chkAprobacionEliminar.UseVisualStyleBackColor = true;
            // 
            // mskCantComandas
            // 
            this.mskCantComandas.Location = new System.Drawing.Point(316, 389);
            this.mskCantComandas.Mask = "##";
            this.mskCantComandas.Name = "mskCantComandas";
            this.mskCantComandas.Size = new System.Drawing.Size(53, 20);
            this.mskCantComandas.TabIndex = 83;
            this.mskCantComandas.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals;
            // 
            // lblCantComandas
            // 
            this.lblCantComandas.AutoSize = true;
            this.lblCantComandas.Location = new System.Drawing.Point(206, 392);
            this.lblCantComandas.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblCantComandas.Name = "lblCantComandas";
            this.lblCantComandas.Size = new System.Drawing.Size(105, 13);
            this.lblCantComandas.TabIndex = 82;
            this.lblCantComandas.Text = "Cantidad Comandas:";
            // 
            // chkCierreCorreo
            // 
            this.chkCierreCorreo.AutoSize = true;
            this.chkCierreCorreo.Location = new System.Drawing.Point(4, 412);
            this.chkCierreCorreo.Margin = new System.Windows.Forms.Padding(2);
            this.chkCierreCorreo.Name = "chkCierreCorreo";
            this.chkCierreCorreo.Size = new System.Drawing.Size(162, 17);
            this.chkCierreCorreo.TabIndex = 80;
            this.chkCierreCorreo.Text = "Enviar por Correo Cierre Caja";
            this.chkCierreCorreo.UseVisualStyleBackColor = true;
            // 
            // cboTipoComanda
            // 
            this.cboTipoComanda.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTipoComanda.FormattingEnabled = true;
            this.cboTipoComanda.Location = new System.Drawing.Point(80, 390);
            this.cboTipoComanda.Name = "cboTipoComanda";
            this.cboTipoComanda.Size = new System.Drawing.Size(121, 21);
            this.cboTipoComanda.TabIndex = 79;
            // 
            // chkComanda
            // 
            this.chkComanda.AutoSize = true;
            this.chkComanda.Location = new System.Drawing.Point(4, 391);
            this.chkComanda.Margin = new System.Windows.Forms.Padding(2);
            this.chkComanda.Name = "chkComanda";
            this.chkComanda.Size = new System.Drawing.Size(71, 17);
            this.chkComanda.TabIndex = 78;
            this.chkComanda.Text = "Comanda";
            this.chkComanda.UseVisualStyleBackColor = true;
            this.chkComanda.CheckedChanged += new System.EventHandler(this.chkComanda_CheckedChanged);
            // 
            // txtSucursal
            // 
            this.txtSucursal.Location = new System.Drawing.Point(140, 26);
            this.txtSucursal.Margin = new System.Windows.Forms.Padding(2);
            this.txtSucursal.Name = "txtSucursal";
            this.txtSucursal.Size = new System.Drawing.Size(90, 20);
            this.txtSucursal.TabIndex = 77;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(85, 27);
            this.label16.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(51, 13);
            this.label16.TabIndex = 76;
            this.label16.Text = "Sucursal:";
            // 
            // txtCodigoPais
            // 
            this.txtCodigoPais.Location = new System.Drawing.Point(140, 71);
            this.txtCodigoPais.Margin = new System.Windows.Forms.Padding(2);
            this.txtCodigoPais.Name = "txtCodigoPais";
            this.txtCodigoPais.Size = new System.Drawing.Size(90, 20);
            this.txtCodigoPais.TabIndex = 75;
            // 
            // txtCaja
            // 
            this.txtCaja.Location = new System.Drawing.Point(140, 48);
            this.txtCaja.Margin = new System.Windows.Forms.Padding(2);
            this.txtCaja.Name = "txtCaja";
            this.txtCaja.Size = new System.Drawing.Size(90, 20);
            this.txtCaja.TabIndex = 74;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(105, 49);
            this.label17.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(31, 13);
            this.label17.TabIndex = 73;
            this.label17.Text = "Caja:";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(69, 73);
            this.label18.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(68, 13);
            this.label18.TabIndex = 72;
            this.label18.Text = "Código País:";
            // 
            // btnRuta
            // 
            this.btnRuta.Location = new System.Drawing.Point(390, 227);
            this.btnRuta.Margin = new System.Windows.Forms.Padding(2);
            this.btnRuta.Name = "btnRuta";
            this.btnRuta.Size = new System.Drawing.Size(31, 19);
            this.btnRuta.TabIndex = 70;
            this.btnRuta.Text = "...";
            this.btnRuta.UseVisualStyleBackColor = true;
            this.btnRuta.Click += new System.EventHandler(this.btnRuta_Click);
            // 
            // txtRutaBackup
            // 
            this.txtRutaBackup.Location = new System.Drawing.Point(140, 228);
            this.txtRutaBackup.Margin = new System.Windows.Forms.Padding(2);
            this.txtRutaBackup.Name = "txtRutaBackup";
            this.txtRutaBackup.Size = new System.Drawing.Size(248, 20);
            this.txtRutaBackup.TabIndex = 69;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(56, 232);
            this.label14.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(81, 13);
            this.label14.TabIndex = 68;
            this.label14.Text = "Ruta Respaldo:";
            // 
            // txtPorcServicioMesa
            // 
            this.txtPorcServicioMesa.Location = new System.Drawing.Point(96, 367);
            this.txtPorcServicioMesa.Margin = new System.Windows.Forms.Padding(2);
            this.txtPorcServicioMesa.Name = "txtPorcServicioMesa";
            this.txtPorcServicioMesa.Size = new System.Drawing.Size(59, 20);
            this.txtPorcServicioMesa.TabIndex = 67;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(161, 369);
            this.label13.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(15, 13);
            this.label13.TabIndex = 66;
            this.label13.Text = "%";
            // 
            // chkServicioMesa
            // 
            this.chkServicioMesa.AutoSize = true;
            this.chkServicioMesa.Location = new System.Drawing.Point(4, 368);
            this.chkServicioMesa.Margin = new System.Windows.Forms.Padding(2);
            this.chkServicioMesa.Name = "chkServicioMesa";
            this.chkServicioMesa.Size = new System.Drawing.Size(93, 17);
            this.chkServicioMesa.TabIndex = 64;
            this.chkServicioMesa.Text = "Servicio Mesa";
            this.chkServicioMesa.UseVisualStyleBackColor = true;
            this.chkServicioMesa.CheckedChanged += new System.EventHandler(this.ChkServicioMesa_CheckedChanged);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(231, 95);
            this.label12.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(45, 13);
            this.label12.TabIndex = 63;
            this.label12.Text = "Colones";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(436, 28);
            this.label11.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(45, 13);
            this.label11.TabIndex = 62;
            this.label11.Text = "Colones";
            // 
            // txtPrecioBase
            // 
            this.txtPrecioBase.Location = new System.Drawing.Point(140, 93);
            this.txtPrecioBase.Margin = new System.Windows.Forms.Padding(2);
            this.txtPrecioBase.Name = "txtPrecioBase";
            this.txtPrecioBase.Size = new System.Drawing.Size(90, 20);
            this.txtPrecioBase.TabIndex = 61;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(69, 95);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(67, 13);
            this.label10.TabIndex = 60;
            this.label10.Text = "Precio Base:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(231, 205);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(28, 13);
            this.label9.TabIndex = 59;
            this.label9.Text = "días";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(231, 184);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(28, 13);
            this.label7.TabIndex = 58;
            this.label7.Text = "días";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(231, 162);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(45, 13);
            this.label4.TabIndex = 57;
            this.label4.Text = "Colones";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(230, 139);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(15, 13);
            this.label3.TabIndex = 56;
            this.label3.Text = "%";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(230, 117);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(15, 13);
            this.label2.TabIndex = 55;
            this.label2.Text = "%";
            // 
            // txtPlazoMaxProf
            // 
            this.txtPlazoMaxProf.Location = new System.Drawing.Point(140, 205);
            this.txtPlazoMaxProf.Margin = new System.Windows.Forms.Padding(2);
            this.txtPlazoMaxProf.Name = "txtPlazoMaxProf";
            this.txtPlazoMaxProf.Size = new System.Drawing.Size(90, 20);
            this.txtPlazoMaxProf.TabIndex = 54;
            // 
            // chkObligaClienteFacturacion
            // 
            this.chkObligaClienteFacturacion.AutoSize = true;
            this.chkObligaClienteFacturacion.Location = new System.Drawing.Point(4, 346);
            this.chkObligaClienteFacturacion.Margin = new System.Windows.Forms.Padding(2);
            this.chkObligaClienteFacturacion.Name = "chkObligaClienteFacturacion";
            this.chkObligaClienteFacturacion.Size = new System.Drawing.Size(185, 17);
            this.chkObligaClienteFacturacion.TabIndex = 52;
            this.chkObligaClienteFacturacion.Text = "Obligatorio Cliente en Facturación";
            this.chkObligaClienteFacturacion.UseVisualStyleBackColor = true;
            // 
            // chkFacturacionElectronica
            // 
            this.chkFacturacionElectronica.AutoSize = true;
            this.chkFacturacionElectronica.Location = new System.Drawing.Point(4, 324);
            this.chkFacturacionElectronica.Margin = new System.Windows.Forms.Padding(2);
            this.chkFacturacionElectronica.Name = "chkFacturacionElectronica";
            this.chkFacturacionElectronica.Size = new System.Drawing.Size(138, 17);
            this.chkFacturacionElectronica.TabIndex = 51;
            this.chkFacturacionElectronica.Text = "Facturación Electrónica";
            this.chkFacturacionElectronica.UseVisualStyleBackColor = true;
            // 
            // chkAprobDes
            // 
            this.chkAprobDes.AutoSize = true;
            this.chkAprobDes.Location = new System.Drawing.Point(4, 280);
            this.chkAprobDes.Margin = new System.Windows.Forms.Padding(2);
            this.chkAprobDes.Name = "chkAprobDes";
            this.chkAprobDes.Size = new System.Drawing.Size(135, 17);
            this.chkAprobDes.TabIndex = 50;
            this.chkAprobDes.Text = "Aprobación Descuento";
            this.chkAprobDes.UseVisualStyleBackColor = true;
            // 
            // chkManejaInventario
            // 
            this.chkManejaInventario.AutoSize = true;
            this.chkManejaInventario.Location = new System.Drawing.Point(4, 302);
            this.chkManejaInventario.Margin = new System.Windows.Forms.Padding(2);
            this.chkManejaInventario.Name = "chkManejaInventario";
            this.chkManejaInventario.Size = new System.Drawing.Size(111, 17);
            this.chkManejaInventario.TabIndex = 49;
            this.chkManejaInventario.Text = "Maneja Inventario";
            this.chkManejaInventario.UseVisualStyleBackColor = true;
            // 
            // txtPlazoCredMax
            // 
            this.txtPlazoCredMax.Location = new System.Drawing.Point(140, 182);
            this.txtPlazoCredMax.Margin = new System.Windows.Forms.Padding(2);
            this.txtPlazoCredMax.Name = "txtPlazoCredMax";
            this.txtPlazoCredMax.Size = new System.Drawing.Size(90, 20);
            this.txtPlazoCredMax.TabIndex = 44;
            // 
            // txtDescBase
            // 
            this.txtDescBase.Location = new System.Drawing.Point(140, 137);
            this.txtDescBase.Margin = new System.Windows.Forms.Padding(2);
            this.txtDescBase.Name = "txtDescBase";
            this.txtDescBase.Size = new System.Drawing.Size(90, 20);
            this.txtDescBase.TabIndex = 43;
            // 
            // txtCambioDolar
            // 
            this.txtCambioDolar.Location = new System.Drawing.Point(140, 160);
            this.txtCambioDolar.Margin = new System.Windows.Forms.Padding(2);
            this.txtCambioDolar.Name = "txtCambioDolar";
            this.txtCambioDolar.Size = new System.Drawing.Size(90, 20);
            this.txtCambioDolar.TabIndex = 42;
            // 
            // txtUtilidadBase
            // 
            this.txtUtilidadBase.Location = new System.Drawing.Point(140, 115);
            this.txtUtilidadBase.Margin = new System.Windows.Forms.Padding(2);
            this.txtUtilidadBase.Name = "txtUtilidadBase";
            this.txtUtilidadBase.Size = new System.Drawing.Size(90, 20);
            this.txtUtilidadBase.TabIndex = 41;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(62, 162);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(73, 13);
            this.label5.TabIndex = 36;
            this.label5.Text = "Cambio Dólar:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(64, 117);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(72, 13);
            this.label6.TabIndex = 37;
            this.label6.Text = "Utilidad Base:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(48, 141);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 13);
            this.label1.TabIndex = 33;
            this.label1.Text = "Descuento Base:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(25, 184);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(111, 13);
            this.label8.TabIndex = 39;
            this.label8.Text = "Plazo Máximo Crédito:";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(10, 206);
            this.label15.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(125, 13);
            this.label15.TabIndex = 40;
            this.label15.Text = "Plazo Máximo Proformas:";
            // 
            // chkValidaCabys
            // 
            this.chkValidaCabys.AutoSize = true;
            this.chkValidaCabys.Location = new System.Drawing.Point(234, 323);
            this.chkValidaCabys.Margin = new System.Windows.Forms.Padding(2);
            this.chkValidaCabys.Name = "chkValidaCabys";
            this.chkValidaCabys.Size = new System.Drawing.Size(146, 17);
            this.chkValidaCabys.TabIndex = 89;
            this.chkValidaCabys.Text = "Valida CABYS al Facturar";
            this.chkValidaCabys.UseVisualStyleBackColor = true;
            // 
            // frmParametrosEmpresa
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(447, 535);
            this.Controls.Add(this.gbxDatos);
            this.Controls.Add(this.tlsMenu);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "frmParametrosEmpresa";
            this.Text = "Mantenimiento: Parámetros Empresa";
            this.Load += new System.EventHandler(this.frmParametrosEmpresa_Load);
            this.tlsMenu.ResumeLayout(false);
            this.tlsMenu.PerformLayout();
            this.gbxDatos.ResumeLayout(false);
            this.gbxDatos.PerformLayout();
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
        private System.Windows.Forms.GroupBox gbxDatos;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtPlazoMaxProf;
        private System.Windows.Forms.CheckBox chkObligaClienteFacturacion;
        private System.Windows.Forms.CheckBox chkFacturacionElectronica;
        private System.Windows.Forms.CheckBox chkAprobDes;
        private System.Windows.Forms.CheckBox chkManejaInventario;
        private System.Windows.Forms.TextBox txtPlazoCredMax;
        private System.Windows.Forms.TextBox txtDescBase;
        private System.Windows.Forms.TextBox txtCambioDolar;
        private System.Windows.Forms.TextBox txtUtilidadBase;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox txtPrecioBase;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.CheckBox chkServicioMesa;
        private System.Windows.Forms.TextBox txtPorcServicioMesa;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Button btnRuta;
        private System.Windows.Forms.TextBox txtRutaBackup;
        private System.Windows.Forms.TextBox txtSucursal;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox txtCodigoPais;
        private System.Windows.Forms.TextBox txtCaja;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.CheckBox chkComanda;
        private System.Windows.Forms.ComboBox cboTipoComanda;
        private System.Windows.Forms.CheckBox chkCierreCorreo;
        private System.Windows.Forms.MaskedTextBox mskCantComandas;
        private System.Windows.Forms.Label lblCantComandas;
        private System.Windows.Forms.CheckBox chkCierreCajaAdmin;
        private System.Windows.Forms.CheckBox chkAprobacionEliminar;
        private System.Windows.Forms.CheckBox chkPrecioVariable;
        private System.Windows.Forms.CheckBox chkEtiquetas;
        private System.Windows.Forms.CheckBox chkPromociones;
        private System.Windows.Forms.CheckBox chkValidaCabys;
    }
}