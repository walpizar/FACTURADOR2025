namespace PresentationLayer
{
    partial class frmDashboardVentas
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        // ---------- Tarjetas KPI ----------
        private System.Windows.Forms.Panel pnlVentasDia;
        private System.Windows.Forms.Label lblTituloVentasDia;
        private System.Windows.Forms.Label lblventasDia;

        private System.Windows.Forms.Panel pnlCantProductosVenDia;
        private System.Windows.Forms.Label lblTituloCantProductosVenDia;
        private System.Windows.Forms.Label lblCantProductosVenDia;

        private System.Windows.Forms.Panel pnlCantCliente;
        private System.Windows.Forms.Label lblTituloCantCliente;
        private System.Windows.Forms.Label lblCantCliente;

        private System.Windows.Forms.Panel pnlCantPro;
        private System.Windows.Forms.Label lblTituloCantPro;
        private System.Windows.Forms.Label lblCantPro;

        private System.Windows.Forms.Panel pnlInvMenorO;
        private System.Windows.Forms.Label lblTituloInvMenorO;
        private System.Windows.Forms.Label lblInvMenorO;

        // ---------- Graficos ----------
        private System.Windows.Forms.Label lblTituloTendencia;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartTendenciaVentas;

        private System.Windows.Forms.Label lblTituloTopVendidos;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartTopProductoVend;

        private System.Windows.Forms.Label lblTituloMenosVendidos;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartMenosVendidos;

        private System.Windows.Forms.Timer timerRefresco;

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend3 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();

            this.pnlVentasDia = new System.Windows.Forms.Panel();
            this.lblventasDia = new System.Windows.Forms.Label();
            this.lblTituloVentasDia = new System.Windows.Forms.Label();

            this.pnlCantProductosVenDia = new System.Windows.Forms.Panel();
            this.lblCantProductosVenDia = new System.Windows.Forms.Label();
            this.lblTituloCantProductosVenDia = new System.Windows.Forms.Label();

            this.pnlCantCliente = new System.Windows.Forms.Panel();
            this.lblCantCliente = new System.Windows.Forms.Label();
            this.lblTituloCantCliente = new System.Windows.Forms.Label();

            this.pnlCantPro = new System.Windows.Forms.Panel();
            this.lblCantPro = new System.Windows.Forms.Label();
            this.lblTituloCantPro = new System.Windows.Forms.Label();

            this.pnlInvMenorO = new System.Windows.Forms.Panel();
            this.lblInvMenorO = new System.Windows.Forms.Label();
            this.lblTituloInvMenorO = new System.Windows.Forms.Label();

            this.lblTituloTendencia = new System.Windows.Forms.Label();
            this.chartTendenciaVentas = new System.Windows.Forms.DataVisualization.Charting.Chart();

            this.lblTituloTopVendidos = new System.Windows.Forms.Label();
            this.chartTopProductoVend = new System.Windows.Forms.DataVisualization.Charting.Chart();

            this.lblTituloMenosVendidos = new System.Windows.Forms.Label();
            this.chartMenosVendidos = new System.Windows.Forms.DataVisualization.Charting.Chart();

            this.timerRefresco = new System.Windows.Forms.Timer(this.components);

            this.pnlVentasDia.SuspendLayout();
            this.pnlCantProductosVenDia.SuspendLayout();
            this.pnlCantCliente.SuspendLayout();
            this.pnlCantPro.SuspendLayout();
            this.pnlInvMenorO.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartTendenciaVentas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartTopProductoVend)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartMenosVendidos)).BeginInit();
            this.SuspendLayout();
            //
            // pnlVentasDia
            //
            this.pnlVentasDia.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlVentasDia.Controls.Add(this.lblventasDia);
            this.pnlVentasDia.Controls.Add(this.lblTituloVentasDia);
            this.pnlVentasDia.Location = new System.Drawing.Point(12, 12);
            this.pnlVentasDia.Name = "pnlVentasDia";
            this.pnlVentasDia.Size = new System.Drawing.Size(184, 70);
            this.pnlVentasDia.TabIndex = 0;
            //
            // lblventasDia
            //
            this.lblventasDia.AutoSize = true;
            this.lblventasDia.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblventasDia.Location = new System.Drawing.Point(10, 8);
            this.lblventasDia.Name = "lblventasDia";
            this.lblventasDia.Size = new System.Drawing.Size(30, 30);
            this.lblventasDia.Text = "0";
            //
            // lblTituloVentasDia
            //
            this.lblTituloVentasDia.AutoSize = true;
            this.lblTituloVentasDia.ForeColor = System.Drawing.Color.Gray;
            this.lblTituloVentasDia.Location = new System.Drawing.Point(10, 44);
            this.lblTituloVentasDia.Name = "lblTituloVentasDia";
            this.lblTituloVentasDia.Size = new System.Drawing.Size(88, 13);
            this.lblTituloVentasDia.Text = "Ventas del dia";
            //
            // pnlCantProductosVenDia
            //
            this.pnlCantProductosVenDia.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlCantProductosVenDia.Controls.Add(this.lblCantProductosVenDia);
            this.pnlCantProductosVenDia.Controls.Add(this.lblTituloCantProductosVenDia);
            this.pnlCantProductosVenDia.Location = new System.Drawing.Point(204, 12);
            this.pnlCantProductosVenDia.Name = "pnlCantProductosVenDia";
            this.pnlCantProductosVenDia.Size = new System.Drawing.Size(184, 70);
            this.pnlCantProductosVenDia.TabIndex = 1;
            //
            // lblCantProductosVenDia
            //
            this.lblCantProductosVenDia.AutoSize = true;
            this.lblCantProductosVenDia.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblCantProductosVenDia.Location = new System.Drawing.Point(10, 8);
            this.lblCantProductosVenDia.Name = "lblCantProductosVenDia";
            this.lblCantProductosVenDia.Size = new System.Drawing.Size(30, 30);
            this.lblCantProductosVenDia.Text = "0";
            //
            // lblTituloCantProductosVenDia
            //
            this.lblTituloCantProductosVenDia.AutoSize = true;
            this.lblTituloCantProductosVenDia.ForeColor = System.Drawing.Color.Gray;
            this.lblTituloCantProductosVenDia.Location = new System.Drawing.Point(10, 44);
            this.lblTituloCantProductosVenDia.Name = "lblTituloCantProductosVenDia";
            this.lblTituloCantProductosVenDia.Size = new System.Drawing.Size(133, 13);
            this.lblTituloCantProductosVenDia.Text = "Productos vendidos hoy";
            //
            // pnlCantCliente
            //
            this.pnlCantCliente.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlCantCliente.Controls.Add(this.lblCantCliente);
            this.pnlCantCliente.Controls.Add(this.lblTituloCantCliente);
            this.pnlCantCliente.Location = new System.Drawing.Point(396, 12);
            this.pnlCantCliente.Name = "pnlCantCliente";
            this.pnlCantCliente.Size = new System.Drawing.Size(184, 70);
            this.pnlCantCliente.TabIndex = 2;
            //
            // lblCantCliente
            //
            this.lblCantCliente.AutoSize = true;
            this.lblCantCliente.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblCantCliente.Location = new System.Drawing.Point(10, 8);
            this.lblCantCliente.Name = "lblCantCliente";
            this.lblCantCliente.Size = new System.Drawing.Size(30, 30);
            this.lblCantCliente.Text = "0";
            //
            // lblTituloCantCliente
            //
            this.lblTituloCantCliente.AutoSize = true;
            this.lblTituloCantCliente.ForeColor = System.Drawing.Color.Gray;
            this.lblTituloCantCliente.Location = new System.Drawing.Point(10, 44);
            this.lblTituloCantCliente.Name = "lblTituloCantCliente";
            this.lblTituloCantCliente.Size = new System.Drawing.Size(105, 13);
            this.lblTituloCantCliente.Text = "Cantidad de clientes";
            //
            // pnlCantPro
            //
            this.pnlCantPro.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlCantPro.Controls.Add(this.lblCantPro);
            this.pnlCantPro.Controls.Add(this.lblTituloCantPro);
            this.pnlCantPro.Location = new System.Drawing.Point(588, 12);
            this.pnlCantPro.Name = "pnlCantPro";
            this.pnlCantPro.Size = new System.Drawing.Size(184, 70);
            this.pnlCantPro.TabIndex = 3;
            //
            // lblCantPro
            //
            this.lblCantPro.AutoSize = true;
            this.lblCantPro.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblCantPro.Location = new System.Drawing.Point(10, 8);
            this.lblCantPro.Name = "lblCantPro";
            this.lblCantPro.Size = new System.Drawing.Size(30, 30);
            this.lblCantPro.Text = "0";
            //
            // lblTituloCantPro
            //
            this.lblTituloCantPro.AutoSize = true;
            this.lblTituloCantPro.ForeColor = System.Drawing.Color.Gray;
            this.lblTituloCantPro.Location = new System.Drawing.Point(10, 44);
            this.lblTituloCantPro.Name = "lblTituloCantPro";
            this.lblTituloCantPro.Size = new System.Drawing.Size(112, 13);
            this.lblTituloCantPro.Text = "Cantidad de productos";
            //
            // pnlInvMenorO
            //
            this.pnlInvMenorO.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlInvMenorO.Controls.Add(this.lblInvMenorO);
            this.pnlInvMenorO.Controls.Add(this.lblTituloInvMenorO);
            this.pnlInvMenorO.Location = new System.Drawing.Point(780, 12);
            this.pnlInvMenorO.Name = "pnlInvMenorO";
            this.pnlInvMenorO.Size = new System.Drawing.Size(184, 70);
            this.pnlInvMenorO.TabIndex = 4;
            //
            // lblInvMenorO
            //
            this.lblInvMenorO.AutoSize = true;
            this.lblInvMenorO.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblInvMenorO.Location = new System.Drawing.Point(10, 8);
            this.lblInvMenorO.Name = "lblInvMenorO";
            this.lblInvMenorO.Size = new System.Drawing.Size(30, 30);
            this.lblInvMenorO.Text = "0";
            //
            // lblTituloInvMenorO
            //
            this.lblTituloInvMenorO.AutoSize = true;
            this.lblTituloInvMenorO.ForeColor = System.Drawing.Color.Gray;
            this.lblTituloInvMenorO.Location = new System.Drawing.Point(10, 44);
            this.lblTituloInvMenorO.Name = "lblTituloInvMenorO";
            this.lblTituloInvMenorO.Size = new System.Drawing.Size(78, 13);
            this.lblTituloInvMenorO.Text = "Inventario menor a 0";
            //
            // lblTituloTendencia
            //
            this.lblTituloTendencia.AutoSize = true;
            this.lblTituloTendencia.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.lblTituloTendencia.Location = new System.Drawing.Point(12, 96);
            this.lblTituloTendencia.Name = "lblTituloTendencia";
            this.lblTituloTendencia.Size = new System.Drawing.Size(180, 17);
            this.lblTituloTendencia.Text = "Ventas ultimos 7 dias";
            //
            // chartTendenciaVentas
            //
            chartArea1.Name = "ChartArea1";
            this.chartTendenciaVentas.ChartAreas.Add(chartArea1);
            legend1.Enabled = false;
            legend1.Name = "Legend1";
            this.chartTendenciaVentas.Legends.Add(legend1);
            this.chartTendenciaVentas.Location = new System.Drawing.Point(12, 116);
            this.chartTendenciaVentas.Name = "chartTendenciaVentas";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chartTendenciaVentas.Series.Add(series1);
            this.chartTendenciaVentas.Size = new System.Drawing.Size(952, 180);
            this.chartTendenciaVentas.TabIndex = 5;
            this.chartTendenciaVentas.Text = "chartTendenciaVentas";
            //
            // lblTituloTopVendidos
            //
            this.lblTituloTopVendidos.AutoSize = true;
            this.lblTituloTopVendidos.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.lblTituloTopVendidos.Location = new System.Drawing.Point(12, 306);
            this.lblTituloTopVendidos.Name = "lblTituloTopVendidos";
            this.lblTituloTopVendidos.Size = new System.Drawing.Size(180, 17);
            this.lblTituloTopVendidos.Text = "Top productos mas vendidos";
            //
            // chartTopProductoVend
            //
            chartArea2.Name = "ChartArea1";
            this.chartTopProductoVend.ChartAreas.Add(chartArea2);
            legend2.Enabled = false;
            legend2.Name = "Legend1";
            this.chartTopProductoVend.Legends.Add(legend2);
            this.chartTopProductoVend.Location = new System.Drawing.Point(12, 326);
            this.chartTopProductoVend.Name = "chartTopProductoVend";
            series2.ChartArea = "ChartArea1";
            series2.Legend = "Legend1";
            series2.Name = "Series1";
            this.chartTopProductoVend.Series.Add(series2);
            this.chartTopProductoVend.Size = new System.Drawing.Size(468, 320);
            this.chartTopProductoVend.TabIndex = 6;
            this.chartTopProductoVend.Text = "chartTopProductoVend";
            //
            // lblTituloMenosVendidos
            //
            this.lblTituloMenosVendidos.AutoSize = true;
            this.lblTituloMenosVendidos.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.lblTituloMenosVendidos.Location = new System.Drawing.Point(496, 306);
            this.lblTituloMenosVendidos.Name = "lblTituloMenosVendidos";
            this.lblTituloMenosVendidos.Size = new System.Drawing.Size(180, 17);
            this.lblTituloMenosVendidos.Text = "Top productos menos vendidos";
            //
            // chartMenosVendidos
            //
            chartArea3.Name = "ChartArea1";
            this.chartMenosVendidos.ChartAreas.Add(chartArea3);
            legend3.Enabled = false;
            legend3.Name = "Legend1";
            this.chartMenosVendidos.Legends.Add(legend3);
            this.chartMenosVendidos.Location = new System.Drawing.Point(496, 326);
            this.chartMenosVendidos.Name = "chartMenosVendidos";
            series3.ChartArea = "ChartArea1";
            series3.Legend = "Legend1";
            series3.Name = "Series1";
            this.chartMenosVendidos.Series.Add(series3);
            this.chartMenosVendidos.Size = new System.Drawing.Size(468, 320);
            this.chartMenosVendidos.TabIndex = 7;
            this.chartMenosVendidos.Text = "chartMenosVendidos";
            //
            // timerRefresco
            //
            this.timerRefresco.Interval = 300000;
            this.timerRefresco.Tick += new System.EventHandler(this.timerRefresco_Tick);
            //
            // frmDashboardVentas
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(984, 662);
            this.Controls.Add(this.chartMenosVendidos);
            this.Controls.Add(this.lblTituloMenosVendidos);
            this.Controls.Add(this.chartTopProductoVend);
            this.Controls.Add(this.lblTituloTopVendidos);
            this.Controls.Add(this.chartTendenciaVentas);
            this.Controls.Add(this.lblTituloTendencia);
            this.Controls.Add(this.pnlInvMenorO);
            this.Controls.Add(this.pnlCantPro);
            this.Controls.Add(this.pnlCantCliente);
            this.Controls.Add(this.pnlCantProductosVenDia);
            this.Controls.Add(this.pnlVentasDia);
            this.Name = "frmDashboardVentas";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Dashboard de Ventas";
            this.Load += new System.EventHandler(this.frmDashboardVentas_Load);
            this.pnlVentasDia.ResumeLayout(false);
            this.pnlVentasDia.PerformLayout();
            this.pnlCantProductosVenDia.ResumeLayout(false);
            this.pnlCantProductosVenDia.PerformLayout();
            this.pnlCantCliente.ResumeLayout(false);
            this.pnlCantCliente.PerformLayout();
            this.pnlCantPro.ResumeLayout(false);
            this.pnlCantPro.PerformLayout();
            this.pnlInvMenorO.ResumeLayout(false);
            this.pnlInvMenorO.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartTendenciaVentas)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartTopProductoVend)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartMenosVendidos)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}