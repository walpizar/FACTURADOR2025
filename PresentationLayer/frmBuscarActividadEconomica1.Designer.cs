namespace PresentationLayer
{
    partial class frmBuscarActividadEconomica1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtBuscar;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dgvCatalogo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCatCodigo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCatNombre;
        private System.Windows.Forms.Button btnAgregar;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView dgvAsignadas;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAsigCodigo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAsigNombre;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colAsigPrincipal;
        private System.Windows.Forms.Button btnQuitar;
        private System.Windows.Forms.Button btnHacienda;
        private System.Windows.Forms.Button btnAceptar;
        private System.Windows.Forms.Button btnCancelar;

        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.txtBuscar = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dgvCatalogo = new System.Windows.Forms.DataGridView();
            this.colCatCodigo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCatNombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnAgregar = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.dgvAsignadas = new System.Windows.Forms.DataGridView();
            this.colAsigCodigo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAsigNombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAsigPrincipal = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.btnQuitar = new System.Windows.Forms.Button();
            this.btnHacienda = new System.Windows.Forms.Button();
            this.btnAceptar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCatalogo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAsignadas)).BeginInit();
            this.SuspendLayout();
            //
            // label1 (título catálogo)
            //
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(120, 13);
            this.label1.Text = "Catálogo de actividades";
            //
            // label2 (Buscar:)
            //
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 38);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 13);
            this.label2.Text = "Buscar:";
            //
            // txtBuscar
            //
            this.txtBuscar.Location = new System.Drawing.Point(63, 35);
            this.txtBuscar.Name = "txtBuscar";
            this.txtBuscar.Size = new System.Drawing.Size(300, 20);
            this.txtBuscar.TextChanged += new System.EventHandler(this.txtBuscar_TextChanged);
            //
            // dgvCatalogo
            //
            this.dgvCatalogo.AllowUserToAddRows = false;
            this.dgvCatalogo.AllowUserToDeleteRows = false;
            this.dgvCatalogo.AllowUserToResizeRows = false;
            this.dgvCatalogo.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colCatCodigo, this.colCatNombre});
            this.dgvCatalogo.Location = new System.Drawing.Point(12, 61);
            this.dgvCatalogo.MultiSelect = false;
            this.dgvCatalogo.Name = "dgvCatalogo";
            this.dgvCatalogo.ReadOnly = true;
            this.dgvCatalogo.RowHeadersVisible = false;
            this.dgvCatalogo.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCatalogo.Size = new System.Drawing.Size(460, 180);
            this.dgvCatalogo.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvCatalogo_CellDoubleClick);
            //
            // colCatCodigo
            //
            this.colCatCodigo.HeaderText = "Código";
            this.colCatCodigo.Name = "colCatCodigo";
            this.colCatCodigo.ReadOnly = true;
            this.colCatCodigo.Width = 80;
            //
            // colCatNombre
            //
            this.colCatNombre.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colCatNombre.HeaderText = "Actividad";
            this.colCatNombre.Name = "colCatNombre";
            this.colCatNombre.ReadOnly = true;
            //
            // btnHacienda
            //
            this.btnHacienda.Location = new System.Drawing.Point(375, 33);
            this.btnHacienda.Name = "btnHacienda";
            this.btnHacienda.Size = new System.Drawing.Size(97, 25);
            this.btnHacienda.Text = "Cons. Hacienda";
            this.btnHacienda.UseVisualStyleBackColor = true;
            this.btnHacienda.Click += new System.EventHandler(this.btnHacienda_Click);
            //
            // btnAgregar
            //
            this.btnAgregar.Location = new System.Drawing.Point(397, 247);
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.Size = new System.Drawing.Size(75, 25);
            this.btnAgregar.Text = "Agregar >>";
            this.btnAgregar.UseVisualStyleBackColor = true;
            this.btnAgregar.Click += new System.EventHandler(this.btnAgregar_Click);
            //
            // label3 (título asignadas)
            //
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 285);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(180, 13);
            this.label3.Text = "Actividades asignadas al cliente";
            //
            // dgvAsignadas
            //
            this.dgvAsignadas.AllowUserToAddRows = false;
            this.dgvAsignadas.AllowUserToDeleteRows = false;
            this.dgvAsignadas.AllowUserToResizeRows = false;
            this.dgvAsignadas.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colAsigCodigo, this.colAsigNombre, this.colAsigPrincipal});
            this.dgvAsignadas.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgvAsignadas.Location = new System.Drawing.Point(12, 305);
            this.dgvAsignadas.MultiSelect = false;
            this.dgvAsignadas.Name = "dgvAsignadas";
            this.dgvAsignadas.RowHeadersVisible = false;
            this.dgvAsignadas.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvAsignadas.Size = new System.Drawing.Size(460, 140);
            this.dgvAsignadas.CurrentCellDirtyStateChanged += new System.EventHandler(this.dgvAsignadas_CurrentCellDirtyStateChanged);
            this.dgvAsignadas.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvAsignadas_CellValueChanged);
            //
            // colAsigCodigo
            //
            this.colAsigCodigo.HeaderText = "Código";
            this.colAsigCodigo.Name = "colAsigCodigo";
            this.colAsigCodigo.ReadOnly = true;
            this.colAsigCodigo.Width = 70;
            //
            // colAsigNombre
            //
            this.colAsigNombre.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colAsigNombre.HeaderText = "Actividad";
            this.colAsigNombre.Name = "colAsigNombre";
            this.colAsigNombre.ReadOnly = true;
            //
            // colAsigPrincipal
            //
            this.colAsigPrincipal.HeaderText = "Principal";
            this.colAsigPrincipal.Name = "colAsigPrincipal";
            this.colAsigPrincipal.Width = 60;
            //
            // btnQuitar
            //
            this.btnQuitar.Location = new System.Drawing.Point(397, 451);
            this.btnQuitar.Name = "btnQuitar";
            this.btnQuitar.Size = new System.Drawing.Size(75, 25);
            this.btnQuitar.Text = "Quitar";
            this.btnQuitar.UseVisualStyleBackColor = true;
            this.btnQuitar.Click += new System.EventHandler(this.btnQuitar_Click);
            //
            // btnAceptar
            //
            this.btnAceptar.Location = new System.Drawing.Point(316, 482);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(75, 28);
            this.btnAceptar.Text = "Aceptar";
            this.btnAceptar.UseVisualStyleBackColor = true;
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
            //
            // btnCancelar
            //
            this.btnCancelar.Location = new System.Drawing.Point(397, 482);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(75, 28);
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            //
            // frmBuscarActividadEconomica
            //
            this.AcceptButton = this.btnAceptar;
            this.CancelButton = this.btnCancelar;
            this.ClientSize = new System.Drawing.Size(484, 522);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnAceptar);
            this.Controls.Add(this.btnHacienda);
            this.Controls.Add(this.btnQuitar);
            this.Controls.Add(this.dgvAsignadas);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnAgregar);
            this.Controls.Add(this.dgvCatalogo);
            this.Controls.Add(this.txtBuscar);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmBuscarActividadEconomica";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Actividades económicas del cliente";
            this.Load += new System.EventHandler(this.frmBuscarActividadEconomica1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCatalogo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAsignadas)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}