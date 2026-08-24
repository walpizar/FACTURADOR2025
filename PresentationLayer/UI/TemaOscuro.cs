using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

public static class TemaSistema
{
    // Tema CLARO: fondo lavanda + azul de acento (tomado de la imagen de
    // referencia). Reemplaza por completo la paleta oscura anterior.
    // Todos los tonos aplicados con un -30% adicional sobre sus valores
    // (cada canal RGB al 70% del original).
    // Paleta azul grisácea con jerarquía visual.
    public static readonly Color Fondo = Color.FromArgb(52, 73, 94);
    public static readonly Color FondoSecundario = Color.FromArgb(63, 84, 104);
    public static readonly Color FondoControl = Color.FromArgb(232, 236, 240);
    public static readonly Color Texto = Color.FromArgb(25, 32, 40);
    public static readonly Color TextoClaro = Color.White;
    public static readonly Color TextoSecundario = Color.FromArgb(205, 214, 222);
    public static readonly Color Acento = Color.FromArgb(29, 111, 163);
    public static readonly Color AcentoHover = Color.FromArgb(39, 129, 186);
    public static readonly Color AcentoPresionado = Color.FromArgb(21, 88, 132);
    public static readonly Color Exito = Color.FromArgb(46, 125, 50);
    public static readonly Color Advertencia = Color.FromArgb(202, 138, 4);
    public static readonly Color Peligro = Color.FromArgb(170, 55, 55);
    public static readonly Color Neutro = Color.FromArgb(84, 101, 116);
    public static readonly Color Borde = Color.FromArgb(104, 126, 145);

    // Encabezados de grid: navy oscuro para que el header siga
    // destacando incluso en un tema claro (blanco sobre azul oscuro).
    private static readonly Color HeaderNavy = Color.FromArgb(22, 29, 48);      // #161D30 — oscurecido

    public static void Aplicar(Form formulario)
    {
        if (formulario == null)
            return;

        formulario.BackColor = Fondo;
        formulario.ForeColor = Texto;

        AplicarControles(formulario.Controls);
    }
    private static void AjustarTextoBotonDinamico(Button boton)
    {
        if (boton == null || string.IsNullOrWhiteSpace(boton.Text))
            return;

        float tamanioFuente;

        // Tamaño inicial según la altura del botón
        if (boton.Height <= 28)
            tamanioFuente = 7.5f;
        else if (boton.Height <= 34)
            tamanioFuente = 8.5f;
        else if (boton.Height <= 40)
            tamanioFuente = 9.0f;
        else if (boton.Height <= 48)
            tamanioFuente = 9.5f;
        else
            tamanioFuente = 10.0f;

        int anchoDisponible =
            boton.ClientSize.Width
            - boton.Padding.Left
            - boton.Padding.Right
            - 8;

        if (boton.Image != null)
        {
            anchoDisponible -= boton.Image.Width + 10;
        }

        float tamanioMinimo = 6.5f;

        while (tamanioFuente >= tamanioMinimo)
        {
            using (Font fuente = new Font(
                boton.Font.FontFamily,
                tamanioFuente,
                FontStyle.Bold))
            {
                Size medida = TextRenderer.MeasureText(
                    boton.Text,
                    fuente,
                    new Size(anchoDisponible, boton.Height),
                    TextFormatFlags.SingleLine
                );

                if (medida.Width <= anchoDisponible)
                {
                    boton.Font = new Font(
                        boton.Font.FontFamily,
                        tamanioFuente,
                        FontStyle.Bold
                    );

                    return;
                }
            }

            tamanioFuente -= 0.5f;
        }

        boton.Font = new Font(
            boton.Font.FontFamily,
            tamanioMinimo,
            FontStyle.Bold
        );
    }
    private static void AplicarControles(Control.ControlCollection controles)
    {
        foreach (Control control in controles)
        {
            if (control is Label)
            {
                control.ForeColor = TextoClaro;
                control.BackColor = Color.Transparent;
            }
            else if (control is GroupBox)
            {
                control.BackColor = Fondo;
                control.ForeColor = TextoClaro;
            }
            else if (control is FlowLayoutPanel || control is TableLayoutPanel)
            {
                control.BackColor = Fondo;
                control.ForeColor = TextoClaro;
            }
            else if (control is Panel panel)
            {
                panel.BackColor = FondoSecundario;
                panel.ForeColor = TextoClaro;
                AplicarBordePanel(panel);
            }
            else if (control is TextBox txt)
            {
                txt.BackColor = FondoControl;
                txt.ForeColor = Texto;
                txt.BorderStyle = BorderStyle.FixedSingle;
            }
            else if (control is MaskedTextBox masked)
            {
                masked.BackColor = FondoControl;
                masked.ForeColor = Texto;
                masked.BorderStyle = BorderStyle.FixedSingle;
            }
            else if (control is RichTextBox rich)
            {
                rich.BackColor = FondoControl;
                rich.ForeColor = Texto;
                rich.BorderStyle = BorderStyle.FixedSingle;
            }
            else if (control is ComboBox cbo)
            {
                cbo.BackColor = FondoControl;
                cbo.ForeColor = Texto;
            }
            else if (control is NumericUpDown numeric)
            {
                numeric.BackColor = FondoControl;
                numeric.ForeColor = Texto;
            }
            else if (control is DateTimePicker fecha)
            {
                fecha.CalendarMonthBackground = FondoControl;
                fecha.CalendarForeColor = Texto;
            }
            else if (control is CheckBox || control is RadioButton)
            {
                control.BackColor = Color.Transparent;
                control.ForeColor = Texto;
            }
            else if (control is DataGridView dgv)
            {
                AplicarDataGridView(dgv);
            }
            else if (control is ListView listView)
            {
                listView.BackColor = FondoControl;
                listView.ForeColor = Texto;
            }
            else if (control is ListBox listBox)
            {
                listBox.BackColor = FondoControl;
                listBox.ForeColor = Texto;
            }
            else if (control is Button boton)
            {
                AplicarBoton(boton);
            }

            if (control.HasChildren)
                AplicarControles(control.Controls);
        }
    }

    /// <summary>
    /// Los Panel de WinForms no permiten un color de borde propio a través
    /// de BorderStyle (solo None/FixedSingle/Fixed3D, con el color que
    /// imponga el sistema operativo) — por eso se dibuja el borde a mano
    /// con Paint, usando el color Borde de la paleta.
    /// </summary>
    private static void AplicarBordePanel(Panel panel)
    {
        panel.BorderStyle = BorderStyle.None;
        panel.Paint -= Panel_PaintBorde; // evita suscribir el evento 2 veces si Aplicar() se llama más de una vez
        panel.Paint += Panel_PaintBorde;
        panel.Invalidate();
    }

    private static void Panel_PaintBorde(object sender, PaintEventArgs e)
    {
        var panel = (Panel)sender;
        if (panel.Width <= 1 || panel.Height <= 1)
            return;

        e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

        var bounds = new Rectangle(0, 0, panel.Width - 1, panel.Height - 1);
        using (var path = ObtenerRectanguloRedondeado(bounds, 6))
        using (var pen = new Pen(Borde))
        {
            e.Graphics.DrawPath(pen, path);
        }
    }

    private static void AplicarBoton(Button boton)
    {
        Color colorBase = ObtenerColorBoton(boton);

        boton.BackColor = colorBase;
        boton.ForeColor = Color.White;
        boton.FlatStyle = FlatStyle.Flat;

        boton.FlatAppearance.BorderSize = 1;
        boton.FlatAppearance.BorderColor =
            ControlPaint.Dark(colorBase, 0.18f);

        boton.FlatAppearance.MouseOverBackColor =
            ControlPaint.Light(colorBase, 0.10f);

        boton.FlatAppearance.MouseDownBackColor =
            ControlPaint.Dark(colorBase, 0.12f);

        boton.Cursor = Cursors.Hand;
        boton.Region = null;

        bool tieneImagen =
            boton.Image != null ||
            boton.BackgroundImage != null;

        if (tieneImagen)
        {
            AplicarIconoBoton(boton);

            boton.ImageAlign =
                ContentAlignment.MiddleLeft;

            boton.TextAlign =
                ContentAlignment.MiddleLeft;

            boton.TextImageRelation =
                TextImageRelation.ImageBeforeText;

            boton.Padding =
                new Padding(8, 0, 6, 0);
        }
        else
        {
            // Botones que tienen solamente texto
            boton.Image = null;

            boton.ImageAlign =
                ContentAlignment.MiddleCenter;

            boton.TextAlign =
                ContentAlignment.MiddleCenter;

            boton.TextImageRelation =
                TextImageRelation.Overlay;

            boton.Padding =
                new Padding(5, 0, 5, 0);
        }

        // Ajustar el texto después de configurar
        // imagen, padding y alineación.
        AjustarTextoBoton(boton);
    }
    private static void AjustarTextoBoton(Button boton)
    {
        if (string.IsNullOrWhiteSpace(boton.Text))
            return;

        float tamanioFuente;

        // Tamaño inicial según altura del botón
        if (boton.Height <= 26)
            tamanioFuente = 7.5f;
        else if (boton.Height <= 30)
            tamanioFuente = 8.0f;
        else if (boton.Height <= 34)
            tamanioFuente = 8.5f;
        else if (boton.Height <= 40)
            tamanioFuente = 9.0f;
        else if (boton.Height <= 48)
            tamanioFuente = 9.5f;
        else if (boton.Height <= 60)
            tamanioFuente = 10.0f;
        else
            tamanioFuente = 11.0f;

        // Espacio ocupado por imagen
        int espacioImagen = 0;

        if (boton.Image != null)
        {
            espacioImagen =
                boton.Image.Width + 10;
        }

        int anchoDisponible =
            boton.ClientSize.Width
            - boton.Padding.Left
            - boton.Padding.Right
            - espacioImagen
            - 6;

        if (anchoDisponible <= 0)
            return;

        float tamanioMinimo = 6.5f;

        while (tamanioFuente >= tamanioMinimo)
        {
            using (Font fuente =
                new Font(
                    boton.Font.FontFamily,
                    tamanioFuente,
                    FontStyle.Bold))
            {
                Size medida =
                    TextRenderer.MeasureText(
                        boton.Text,
                        fuente,
                        new Size(
                            anchoDisponible,
                            boton.Height),
                        TextFormatFlags.SingleLine
                    );

                if (medida.Width <= anchoDisponible)
                {
                    boton.Font =
                        new Font(
                            boton.Font.FontFamily,
                            tamanioFuente,
                            FontStyle.Bold);

                    return;
                }
            }

            tamanioFuente -= 0.5f;
        }

        // Si no entra ni con el mínimo
        boton.Font =
            new Font(
                boton.Font.FontFamily,
                tamanioMinimo,
                FontStyle.Bold);
    }
    private static Color ObtenerColorBoton(Button boton)
    {
        string tag = (boton.Tag ?? "").ToString().Trim().ToLowerInvariant();
        string texto = ((boton.Name ?? "") + " " + (boton.Text ?? "")).ToLowerInvariant();

        if (tag == "peligro" || texto.Contains("salir") || texto.Contains("eliminar") ||
            texto.Contains("anular") || texto.Contains("cancelar"))
            return Peligro;

        if (tag == "exito" || texto.Contains("guardar") || texto.Contains("aceptar") ||
            texto.Contains("aplicar") || texto.Contains("procesar"))
            return Exito;

        if (tag == "advertencia" || texto.Contains("respaldo") || texto.Contains("cierre") ||
            texto.Contains("reporte") || texto.Contains("imprimir"))
            return Advertencia;

        if (tag == "secundario" || texto.Contains("buscar") || texto.Contains("consultar") ||
            texto.Contains("limpiar") || texto.Contains("volver"))
            return Neutro;

        return Acento;
    }

    private static void AplicarIconoBoton(Button boton)
    {
        // Si tiene BackgroundImage, usarla como Image
        if (boton.Image == null &&
            boton.BackgroundImage != null)
        {
            boton.Image = boton.BackgroundImage;
            boton.BackgroundImage = null;
        }

        if (boton.Image == null)
            return;

        Image original = boton.Image;

        // Aproximadamente 65% de la altura del botón
        int tamanioMaximo =
            (int)(boton.ClientSize.Height * 0.65);

        // Límites razonables
        tamanioMaximo =
            Math.Max(14, tamanioMaximo);

        tamanioMaximo =
            Math.Min(32, tamanioMaximo);

        // Mantener proporción
        float escala = Math.Min(
            (float)tamanioMaximo / original.Width,
            (float)tamanioMaximo / original.Height
        );

        int nuevoAncho =
            Math.Max(
                1,
                (int)(original.Width * escala)
            );

        int nuevoAlto =
            Math.Max(
                1,
                (int)(original.Height * escala)
            );

        // Evitar volver a redimensionar si ya está bien
        if (original.Width == nuevoAncho &&
            original.Height == nuevoAlto)
        {
            return;
        }

        Bitmap nuevaImagen =
            new Bitmap(nuevoAncho, nuevoAlto);

        using (Graphics g =
            Graphics.FromImage(nuevaImagen))
        {
            g.Clear(Color.Transparent);

            g.SmoothingMode =
                SmoothingMode.AntiAlias;

            g.InterpolationMode =
                InterpolationMode.HighQualityBicubic;

            g.PixelOffsetMode =
                PixelOffsetMode.HighQuality;

            g.CompositingQuality =
                CompositingQuality.HighQuality;

            g.DrawImage(
                original,
                new Rectangle(
                    0,
                    0,
                    nuevoAncho,
                    nuevoAlto
                )
            );
        }

        boton.Image = nuevaImagen;
    }

    /// <summary>
    /// Recorta el control a un rectángulo con esquinas redondeadas. Se
    /// vuelve a calcular en cada Resize, para que no se deforme si el
    /// control cambia de tamaño (por ejemplo, con Anchor/Dock).
    /// </summary>
    private static void AplicarEsquinasRedondeadas(Control control, int radio)
    {
        void Redondear(object s, EventArgs e)
        {
            if (control.Width <= 0 || control.Height <= 0)
                return;

            using (var path = ObtenerRectanguloRedondeado(control.ClientRectangle, radio))
            {
                control.Region = new Region(path);
            }
        }

        control.Resize -= Redondear; // evita duplicar el handler si Aplicar() corre más de una vez
        control.Resize += Redondear;
        Redondear(control, EventArgs.Empty);
    }

    private static GraphicsPath ObtenerRectanguloRedondeado(Rectangle bounds, int radio)
    {
        int d = radio * 2;
        d = Math.Min(d, Math.Min(bounds.Width, bounds.Height));

        var path = new GraphicsPath();
        path.AddArc(bounds.X, bounds.Y, d, d, 180, 90);
        path.AddArc(bounds.Right - d, bounds.Y, d, d, 270, 90);
        path.AddArc(bounds.Right - d, bounds.Bottom - d, d, d, 0, 90);
        path.AddArc(bounds.X, bounds.Bottom - d, d, d, 90, 90);
        path.CloseFigure();
        return path;
    }

    private static void AplicarDataGridView(DataGridView dgv)
    {
        dgv.BackgroundColor = FondoSecundario;
        dgv.BorderStyle = BorderStyle.FixedSingle;
        dgv.GridColor = Borde;
        dgv.EnableHeadersVisualStyles = false;

        dgv.DefaultCellStyle.BackColor = FondoControl;
        dgv.DefaultCellStyle.ForeColor = Texto;
        dgv.DefaultCellStyle.SelectionBackColor = Acento;
        dgv.DefaultCellStyle.SelectionForeColor = Color.White;

        // Franja alterna: lavanda muy sutil (mismo tono de página, apenas
        // perceptible), para diferenciar filas sin oscurecer el tema claro.
        dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(172, 172, 176); // -30% sobre #F5F6FB
        dgv.AlternatingRowsDefaultCellStyle.ForeColor = Texto;

        // Mismo navy que el sidebar — une visualmente el grid con el menú.
        dgv.ColumnHeadersDefaultCellStyle.BackColor = HeaderNavy;
        dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
        dgv.ColumnHeadersDefaultCellStyle.SelectionBackColor = HeaderNavy;
        dgv.ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.White;

        dgv.RowHeadersDefaultCellStyle.BackColor = FondoSecundario;
        dgv.RowHeadersDefaultCellStyle.ForeColor = Texto;
        dgv.RowHeadersDefaultCellStyle.SelectionBackColor = Acento;
        dgv.RowHeadersDefaultCellStyle.SelectionForeColor = Color.White;
    }
}

/// <summary>
/// Formulario base para aplicar TemaSistema automáticamente.
/// Cambie ': Form' por ': FormBase' en los formularios que desee tematizar.
/// </summary>
public class FormBase : Form
{
    public FormBase()
    {
        BackColor = TemaSistema.Fondo;
        ForeColor = TemaSistema.TextoClaro;
    }

    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        TemaSistema.Aplicar(this);
    }
}