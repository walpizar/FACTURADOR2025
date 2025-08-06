namespace PresentationLayer
{
    partial class frmBuscarRoles
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
            this.txtBuscarRol = new System.Windows.Forms.TextBox();
            this.lstvRolesAlmacenados = new System.Windows.Forms.ListView();
            this.colID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colNombre = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colEstado = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chkEstado = new System.Windows.Forms.CheckBox();
            this.btnSeleccionar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtBuscarRol
            // 
            this.txtBuscarRol.Location = new System.Drawing.Point(33, 34);
            this.txtBuscarRol.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtBuscarRol.Name = "txtBuscarRol";
            this.txtBuscarRol.Size = new System.Drawing.Size(596, 22);
            this.txtBuscarRol.TabIndex = 0;
            this.txtBuscarRol.TextChanged += new System.EventHandler(this.txtBuscarRol_TextChanged);
            // 
            // lstvRolesAlmacenados
            // 
            this.lstvRolesAlmacenados.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colID,
            this.colNombre,
            this.colEstado});
            this.lstvRolesAlmacenados.FullRowSelect = true;
            this.lstvRolesAlmacenados.HideSelection = false;
            this.lstvRolesAlmacenados.Location = new System.Drawing.Point(33, 82);
            this.lstvRolesAlmacenados.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.lstvRolesAlmacenados.Name = "lstvRolesAlmacenados";
            this.lstvRolesAlmacenados.Size = new System.Drawing.Size(596, 287);
            this.lstvRolesAlmacenados.TabIndex = 1;
            this.lstvRolesAlmacenados.UseCompatibleStateImageBehavior = false;
            this.lstvRolesAlmacenados.View = System.Windows.Forms.View.Details;
            this.lstvRolesAlmacenados.SelectedIndexChanged += new System.EventHandler(this.lstvRolesAlmacenados_SelectedIndexChanged);
            this.lstvRolesAlmacenados.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lstvRolesAlmacenados_MouseDoubleClick);
            // 
            // colID
            // 
            this.colID.Text = "ID";
            this.colID.Width = 100;
            // 
            // colNombre
            // 
            this.colNombre.Text = "Nombre";
            this.colNombre.Width = 250;
            // 
            // colEstado
            // 
            this.colEstado.Text = "Estado";
            this.colEstado.Width = 150;
            // 
            // chkEstado
            // 
            this.chkEstado.AutoSize = true;
            this.chkEstado.Location = new System.Drawing.Point(33, 378);
            this.chkEstado.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.chkEstado.Name = "chkEstado";
            this.chkEstado.Size = new System.Drawing.Size(74, 21);
            this.chkEstado.TabIndex = 2;
            this.chkEstado.Text = "Estado";
            this.chkEstado.UseVisualStyleBackColor = true;
            // 
            // btnSeleccionar
            // 
            this.btnSeleccionar.Location = new System.Drawing.Point(521, 378);
            this.btnSeleccionar.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnSeleccionar.Name = "btnSeleccionar";
            this.btnSeleccionar.Size = new System.Drawing.Size(100, 28);
            this.btnSeleccionar.TabIndex = 3;
            this.btnSeleccionar.Text = "Seleccionar";
            this.btnSeleccionar.UseVisualStyleBackColor = true;
            this.btnSeleccionar.Click += new System.EventHandler(this.btnSeleccionar_Click);
            // 
            // frmBuscarRoles
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(647, 422);
            this.Controls.Add(this.btnSeleccionar);
            this.Controls.Add(this.chkEstado);
            this.Controls.Add(this.lstvRolesAlmacenados);
            this.Controls.Add(this.txtBuscarRol);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmBuscarRoles";
            this.Text = "Buscar Roles";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmBuscarRoles_FormClosed);
            this.Load += new System.EventHandler(this.frmBuscarRoles_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtBuscarRol;
        private System.Windows.Forms.ListView lstvRolesAlmacenados;
        private System.Windows.Forms.ColumnHeader colID;
        private System.Windows.Forms.ColumnHeader colNombre;
        private System.Windows.Forms.ColumnHeader colEstado;
        private System.Windows.Forms.CheckBox chkEstado;
        private System.Windows.Forms.Button btnSeleccionar;
    }
}