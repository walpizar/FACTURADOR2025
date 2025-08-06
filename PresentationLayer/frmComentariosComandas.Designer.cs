namespace PresentationLayer
{
    partial class frmComentariosComandas
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dtgvComentarios = new System.Windows.Forms.DataGridView();
            this.colCodigo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNom = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colComentario = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnAceptar = new System.Windows.Forms.Button();
            this.chkLlevar = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvComentarios)).BeginInit();
            this.SuspendLayout();
            // 
            // dtgvComentarios
            // 
            this.dtgvComentarios.AllowUserToAddRows = false;
            this.dtgvComentarios.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dtgvComentarios.BackgroundColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle13.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle13.Font = new System.Drawing.Font("Arial", 7F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle13.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle13.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle13.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle13.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dtgvComentarios.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle13;
            this.dtgvComentarios.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgvComentarios.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colCodigo,
            this.colNom,
            this.colComentario});
            dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle15.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle15.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle15.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle15.Padding = new System.Windows.Forms.Padding(0, 4, 0, 4);
            dataGridViewCellStyle15.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle15.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle15.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dtgvComentarios.DefaultCellStyle = dataGridViewCellStyle15;
            this.dtgvComentarios.EnableHeadersVisualStyles = false;
            this.dtgvComentarios.GridColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.dtgvComentarios.Location = new System.Drawing.Point(12, 12);
            this.dtgvComentarios.MultiSelect = false;
            this.dtgvComentarios.Name = "dtgvComentarios";
            dataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle16.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle16.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle16.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle16.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle16.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle16.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dtgvComentarios.RowHeadersDefaultCellStyle = dataGridViewCellStyle16;
            this.dtgvComentarios.RowHeadersVisible = false;
            this.dtgvComentarios.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.dtgvComentarios.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dtgvComentarios.Size = new System.Drawing.Size(812, 292);
            this.dtgvComentarios.TabIndex = 119;
            this.dtgvComentarios.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dtgvComentarios_CellValueChanged);
            // 
            // colCodigo
            // 
            this.colCodigo.Frozen = true;
            this.colCodigo.HeaderText = "colCodigoProducto";
            this.colCodigo.Name = "colCodigo";
            this.colCodigo.ReadOnly = true;
            this.colCodigo.Visible = false;
            // 
            // colNom
            // 
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.colNom.DefaultCellStyle = dataGridViewCellStyle14;
            this.colNom.FillWeight = 369.5432F;
            this.colNom.Frozen = true;
            this.colNom.HeaderText = "Producto";
            this.colNom.Name = "colNom";
            this.colNom.ReadOnly = true;
            this.colNom.Width = 400;
            // 
            // colComentario
            // 
            this.colComentario.HeaderText = "Comentario";
            this.colComentario.MaxInputLength = 40;
            this.colComentario.Name = "colComentario";
            this.colComentario.Width = 400;
            // 
            // btnCancelar
            // 
            this.btnCancelar.Location = new System.Drawing.Point(647, 308);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(85, 33);
            this.btnCancelar.TabIndex = 120;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnAceptar
            // 
            this.btnAceptar.Location = new System.Drawing.Point(738, 308);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(86, 33);
            this.btnAceptar.TabIndex = 121;
            this.btnAceptar.Text = "Aceptar";
            this.btnAceptar.UseVisualStyleBackColor = true;
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
            // 
            // chkLlevar
            // 
            this.chkLlevar.AutoSize = true;
            this.chkLlevar.Location = new System.Drawing.Point(553, 317);
            this.chkLlevar.Name = "chkLlevar";
            this.chkLlevar.Size = new System.Drawing.Size(76, 17);
            this.chkLlevar.TabIndex = 122;
            this.chkLlevar.Text = "Para llevar";
            this.chkLlevar.UseVisualStyleBackColor = true;
            this.chkLlevar.CheckedChanged += new System.EventHandler(this.chkLlevar_CheckedChanged);
            // 
            // frmComentariosComandas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(834, 353);
            this.Controls.Add(this.chkLlevar);
            this.Controls.Add(this.btnAceptar);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.dtgvComentarios);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmComentariosComandas";
            this.Text = "Comentarios Comandas";
            this.Load += new System.EventHandler(this.frmComentariosComandas_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dtgvComentarios)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dtgvComentarios;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnAceptar;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCodigo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNom;
        private System.Windows.Forms.DataGridViewTextBoxColumn colComentario;
        private System.Windows.Forms.CheckBox chkLlevar;
    }
}