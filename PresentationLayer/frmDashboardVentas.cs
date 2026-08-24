using CommonLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace PresentationLayer
{
    public partial class frmDashboardVentas : FormBase
    {
        // Paleta consistente: verde = positivo, naranja = atencion, azul = neutro/tendencia.
        private static readonly Color ColorVerde = Color.FromArgb(29, 158, 117);
        private static readonly Color ColorNaranja = Color.FromArgb(216, 90, 48);
        private static readonly Color ColorAzul = Color.FromArgb(55, 138, 221);
        private static readonly Color ColorAzulOscuro = Color.FromArgb(12, 68, 124);

        // Fondo gris oscuro: 3 niveles (página < tarjeta < borde/gridline),
        // igual que un dashboard "dark mode" real, no un simple invertido.
        private static readonly Color FondoPagina = Color.FromArgb(30, 30, 30);
        private static readonly Color FondoTarjeta = Color.FromArgb(42, 42, 42);
        private static readonly Color BordeSutil = Color.FromArgb(60, 60, 60);
        private static readonly Color TextoPrimario = Color.FromArgb(235, 235, 235);
        private static readonly Color TextoSecundario = Color.FromArgb(160, 160, 160);

        public frmDashboardVentas()
        {
            InitializeComponent();
        }

        private void frmDashboardVentas_Load(object sender, EventArgs e)
        {
            EstilizarFondoOscuro();
            ConfigurarEstiloGraficos();
            EstilizarTarjetasKpi();
            CargarDashboard();
            timerRefresco.Start();
        }

        /// <summary>
        /// Fondo gris oscuro para todo el formulario, incluyendo los
        /// títulos de sección (que el Designer generó en negro por
        /// defecto — sin este ajuste quedarían invisibles sobre fondo oscuro).
        /// </summary>
        private void EstilizarFondoOscuro()
        {
            this.BackColor = FondoPagina;

            var titulos = new[] { lblTituloTendencia, lblTituloTopVendidos, lblTituloMenosVendidos };
            foreach (var titulo in titulos)
            {
                titulo.ForeColor = TextoPrimario;
            }
        }

        /// <summary>
        /// Reemplaza el borde marcado (FixedSingle) de las tarjetas KPI por
        /// un estilo plano en modo oscuro: sin borde, fondo gris tarjeta
        /// (un tono más claro que el fondo de página, para que se
        /// distingan), número en blanco suave, título en gris claro.
        /// </summary>
        private void EstilizarTarjetasKpi()
        {
            var tarjetas = new[] { pnlVentasDia, pnlCantProductosVenDia, pnlCantCliente, pnlCantPro, pnlInvMenorO };

            foreach (var tarjeta in tarjetas)
            {
                tarjeta.BorderStyle = BorderStyle.None;
                tarjeta.BackColor = FondoTarjeta;
            }

            var valores = new[] { lblventasDia, lblCantProductosVenDia, lblCantCliente, lblCantPro, lblInvMenorO };
            foreach (var lbl in valores)
            {
                lbl.ForeColor = TextoPrimario;
                lbl.BackColor = Color.Transparent;
            }

            var titulos = new[] { lblTituloVentasDia, lblTituloCantProductosVenDia, lblTituloCantCliente, lblTituloCantPro, lblTituloInvMenorO };
            foreach (var lbl in titulos)
            {
                lbl.ForeColor = TextoSecundario;
                lbl.BackColor = Color.Transparent;
                lbl.Font = new Font("Segoe UI", 8.25F, FontStyle.Regular);
            }
        }

        private void timerRefresco_Tick(object sender, EventArgs e)
        {
            CargarDashboard();
        }

        private void CargarDashboard()
        {
            CargarIndicadores();
            CargarGraficoTendencia();
            CargarGraficoTopVendidos();
            CargarGraficoTopMenosVendidos();
        }

        /// <summary>
        /// Da estilo minimalista/moderno a los 3 gráficos: sin bordes, sin
        /// sombras/3D, grillas casi invisibles, colores planos consistentes,
        /// tipografía liviana. Se llama una sola vez, en el Load.
        /// </summary>
        private void ConfigurarEstiloGraficos()
        {
            EstilizarChart(chartTendenciaVentas);
            EstilizarChart(chartTopProductoVend);
            EstilizarChart(chartMenosVendidos);

            chartTendenciaVentas.Series[0].ChartType = SeriesChartType.Column;
            EstilizarSerie(chartTendenciaVentas.Series[0], ColorAzul, mostrarEtiquetaValor: false);

            chartTopProductoVend.Series[0].ChartType = SeriesChartType.Bar;
            EstilizarSerie(chartTopProductoVend.Series[0], ColorVerde, mostrarEtiquetaValor: true);

            chartMenosVendidos.Series[0].ChartType = SeriesChartType.Bar;
            EstilizarSerie(chartMenosVendidos.Series[0], ColorNaranja, mostrarEtiquetaValor: true);
        }

        /// <summary>
        /// Limpia todo el "chrome" pesado del control Chart: bordes,
        /// sombreado 3D, líneas de eje gruesas, fondo con relieve. Deja
        /// una superficie plana, blanca, sin ruido visual.
        /// </summary>
        private void EstilizarChart(Chart chart)
        {
            // Anti-aliasing: texto y líneas se ven nítidos, no pixelados
            // (el control por defecto se ve "duro" sin esto).
            chart.AntiAliasing = AntiAliasingStyles.All;
            chart.TextAntiAliasingQuality = TextAntiAliasingQuality.High;

            // Sin borde del control ni relieve — superficie plana, ahora
            // en el tono de tarjeta oscura (no blanco).
            chart.BorderlineWidth = 0;
            chart.BorderlineColor = Color.Transparent;
            chart.BorderSkin.SkinStyle = BorderSkinStyle.None;
            chart.BackColor = FondoTarjeta;
            chart.Palette = ChartColorPalette.None; // los colores los definimos nosotros, no un ciclo automático

            var area = chart.ChartAreas[0];
            area.BackColor = Color.Transparent; // hereda el FondoTarjeta del chart
            area.BorderWidth = 0;
            area.BorderColor = Color.Transparent;
            area.ShadowColor = Color.Transparent;

            // Grillas sutiles sobre fondo oscuro: un gris apenas más claro
            // que la tarjeta, no la línea gris clara que se usaría sobre
            // fondo blanco (ahí sería invisible / casi invisible al revés).
            EstilizarEje(area.AxisX);
            EstilizarEje(area.AxisY);
        }

        private void EstilizarEje(Axis eje)
        {
            eje.MajorGrid.LineColor = BordeSutil;
            eje.MajorGrid.LineDashStyle = ChartDashStyle.Solid;
            eje.MajorTickMark.Enabled = false;   // sin "rayitas" en el eje, más limpio
            eje.MinorTickMark.Enabled = false;
            eje.LineColor = BordeSutil;
            eje.LabelStyle.Font = new Font("Segoe UI", 8.25F, FontStyle.Regular);
            eje.LabelStyle.ForeColor = TextoSecundario;
            eje.TitleFont = new Font("Segoe UI", 8.25F, FontStyle.Regular);
        }

        /// <summary>
        /// Estilo plano para la serie de datos: barras/columnas sin
        /// degradado ni relieve, con un espesor moderado y separación
        /// entre puntos (no pegadas unas a otras).
        /// </summary>
        private void EstilizarSerie(Series serie, Color color, bool mostrarEtiquetaValor)
        {
            serie.Color = color;
            serie["PixelPointWidth"] = "22";       // grosor de barra fijo, moderno (no tan grueso ni tan fino)
            serie["PointWidth"] = "0.55";          // separación entre barras
            serie.BorderWidth = 0;                 // sin borde alrededor de cada barra
            serie.ShadowColor = Color.Transparent;  // sin sombra bajo la barra

            serie.IsValueShownAsLabel = mostrarEtiquetaValor;
            serie.Font = new Font("Segoe UI", 8.25F, FontStyle.Regular);
            serie.LabelForeColor = TextoPrimario;
        }

        // ---------- Indicadores (tarjetas KPI) ----------

        private void CargarIndicadores()
        {
            try
            {
                using (var conexion = new SqlConnection(Utility.stringConexionReportes()))
                using (var cmd = new SqlCommand("DASHBOARD_CANTIDADES", conexion))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@diaInicio", Utility.getDate().Date));
                    cmd.Parameters.Add(new SqlParameter("@diaFin", Utility.getDate().Date));

                    var ventasDia = new SqlParameter("@ventasDia", SqlDbType.Decimal) { Direction = ParameterDirection.Output, Precision = 18, Scale = 2 };
                    var cantPro = new SqlParameter("@cantPro", SqlDbType.Int) { Direction = ParameterDirection.Output };
                    var cantCliente = new SqlParameter("@cantCliente", SqlDbType.Int) { Direction = ParameterDirection.Output };
                    var invMenorO = new SqlParameter("@InvMenorO", SqlDbType.Int) { Direction = ParameterDirection.Output };
                    var cantProductosVenDia = new SqlParameter("@CantProductosVenDia", SqlDbType.Int) { Direction = ParameterDirection.Output };

                    cmd.Parameters.Add(ventasDia);
                    cmd.Parameters.Add(cantPro);
                    cmd.Parameters.Add(cantCliente);
                    cmd.Parameters.Add(invMenorO);
                    cmd.Parameters.Add(cantProductosVenDia);

                    conexion.Open();
                    cmd.ExecuteNonQuery();

                    lblventasDia.Text = Utility.priceFormat(ventasDia.Value == DBNull.Value ? 0 : (decimal)ventasDia.Value);
                    lblCantProductosVenDia.Text = (cantProductosVenDia.Value == DBNull.Value ? 0 : cantProductosVenDia.Value).ToString();
                    lblCantCliente.Text = (cantCliente.Value == DBNull.Value ? 0 : cantCliente.Value).ToString();
                    lblCantPro.Text = (cantPro.Value == DBNull.Value ? 0 : cantPro.Value).ToString();
                    lblInvMenorO.Text = (invMenorO.Value == DBNull.Value ? 0 : invMenorO.Value).ToString();

                    // Alerta visual simple: inventario negativo en naranja si hay algo que atender.
                    lblInvMenorO.ForeColor = (invMenorO.Value != DBNull.Value && (int)invMenorO.Value > 0)
                        ? ColorNaranja
                        : TextoPrimario;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error al cargar los indicadores del dashboard", "Dashboard", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ---------- Tendencia de ventas (ultimos 7 dias) ----------

        private void CargarGraficoTendencia()
        {
            var dias = new List<string>();
            var totales = new List<decimal>();

            try
            {
                using (var conexion = new SqlConnection(Utility.stringConexionReportes()))
                using (var cmd = new SqlCommand("DASHBOARD_VENTASULTIMOS7DIAS", conexion))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    conexion.Open();

                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            dias.Add(dr.GetDateTime(0).ToString("ddd d"));
                            totales.Add(dr.GetDecimal(1));
                        }
                    }
                }

                chartTendenciaVentas.Series[0].Points.Clear();
                chartTendenciaVentas.Series[0].Points.DataBindXY(dias, totales);

                // Resalta el día de hoy (última barra) con un tono más
                // saturado de la misma familia — no un color distinto,
                // para no romper la paleta plana del resto del gráfico.
                int ultimo = chartTendenciaVentas.Series[0].Points.Count - 1;
                for (int i = 0; i < chartTendenciaVentas.Series[0].Points.Count; i++)
                {
                    chartTendenciaVentas.Series[0].Points[i].Color = (i == ultimo) ? ColorAzulOscuro : ColorAzul;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error al cargar el grafico de tendencia de ventas", "Cargar grafico", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ---------- Top productos mas vendidos ----------

        private void CargarGraficoTopVendidos()
        {
            var producto = new List<string>();
            var cantidad = new List<int>();

            try
            {
                using (var conexion = new SqlConnection(Utility.stringConexionReportes()))
                using (var cmd = new SqlCommand("DASHBOARD_10PRODUCTOVENDIDOS", conexion))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    conexion.Open();

                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            producto.Add(dr.GetString(1));
                            cantidad.Add(dr.GetInt32(2));
                        }
                    }
                }

                chartTopProductoVend.Series[0].Points.Clear();
                chartTopProductoVend.Series[0].Points.DataBindXY(producto, cantidad);
            }
            catch (Exception)
            {
                MessageBox.Show("Error al cargar el grafico de productos mas vendidos", "Cargar grafico", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ---------- Top productos menos vendidos ----------

        private void CargarGraficoTopMenosVendidos()
        {
            var producto = new List<string>();
            var cantidad = new List<int>();

            try
            {
                using (var conexion = new SqlConnection(Utility.stringConexionReportes()))
                using (var cmd = new SqlCommand("DASHBOARD_10PRODUCTOMENOSVENDIDOS", conexion))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    conexion.Open();

                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            producto.Add(dr.GetString(1));
                            cantidad.Add(dr.GetInt32(2));
                        }
                    }
                }

                chartMenosVendidos.Series[0].Points.Clear();
                chartMenosVendidos.Series[0].Points.DataBindXY(producto, cantidad);
            }
            catch (Exception)
            {
                MessageBox.Show("Error al cargar el grafico de productos menos vendidos", "Cargar grafico", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}