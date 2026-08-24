using BusinessLayer;
using CommonLayer;
using CommonLayer.DTO;
using CommonLayer.Exceptions.DataExceptions;
using EntityLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PresentationLayer
{
    public partial class frmBuscarCategoriaCabys : FormBase
    {
        private BCategoriaProducto CatProductIns = new BCategoriaProducto();

        private List<tbCategoria9Cabys> lista;
        private List<tbCategoria9Cabys> listaFiltrada;

        // PAGINACIÓN
        private int paginaActual = 1;
        private int registrosPorPagina = 100;
        private int totalPaginas = 1;

        public delegate void pasaDatos(string codigo, int tipo);
        public event pasaDatos pasarDatosEvent;

        private string codigo;

        public frmBuscarCategoriaCabys()
        {
            InitializeComponent();
        }

        private void frmBuscarCategoriaCabys_Load(object sender, EventArgs e)
        {
            try
            {
                // Se obtiene la lista completa desde la BD
                lista = CatProductIns.getCat9Cabys();

                if (lista == null)
                    lista = new List<tbCategoria9Cabys>();

                listaFiltrada = lista;

                paginaActual = 1;

                MostrarPagina();

                txtbusqueda.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al cargar los Bienes/Servicios CABYS.\n\n" + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Muestra únicamente los registros correspondientes
        /// a la página actual.
        /// </summary>
        private void MostrarPagina()
        {
            try
            {
                lstvBienServicios.BeginUpdate();
                lstvBienServicios.Items.Clear();

                if (listaFiltrada == null || listaFiltrada.Count == 0)
                {
                    paginaActual = 1;
                    totalPaginas = 1;

                    lblPagina.Text = "Página 1 de 1 - 0 registros";

                    btnAnterior.Enabled = false;
                    btnSiguiente.Enabled = false;

                    return;
                }

                // Calcular total de páginas
                totalPaginas = (int)Math.Ceiling(
                    (double)listaFiltrada.Count / registrosPorPagina);

                if (paginaActual < 1)
                    paginaActual = 1;

                if (paginaActual > totalPaginas)
                    paginaActual = totalPaginas;

                // Obtener solamente los elementos de la página
                List<tbCategoria9Cabys> pagina = listaFiltrada
                    .Skip((paginaActual - 1) * registrosPorPagina)
                    .Take(registrosPorPagina)
                    .ToList();

                foreach (tbCategoria9Cabys p in pagina)
                {
                    ListViewItem item = new ListViewItem();

                    item.Text = !string.IsNullOrEmpty(p.idCategoria9)
                        ? p.idCategoria9.Trim()
                        : "";

                    item.SubItems.Add(
                        !string.IsNullOrEmpty(p.nombre)
                            ? p.nombre.Trim()
                            : "");

                    item.SubItems.Add(
                        p.impuesto.ToString());

                    item.SubItems.Add(
                        Utility.ObtenerTipoCabys(p.idCategoria9));

                    lstvBienServicios.Items.Add(item);
                }

                // Información de página
                lblPagina.Text =
                    "Página " + paginaActual +
                    " de " + totalPaginas +
                    "  |  " +
                    listaFiltrada.Count.ToString("N0") +
                    " registros";

                // Habilitar/deshabilitar navegación
                btnAnterior.Enabled = paginaActual > 1;
                btnSiguiente.Enabled = paginaActual < totalPaginas;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al mostrar los registros CABYS.\n\n" + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                lstvBienServicios.EndUpdate();
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            Buscar();
        }

        private void Buscar()
        {
            try
            {
                string texto = txtbusqueda.Text.Trim();

                if (string.IsNullOrWhiteSpace(texto))
                {
                    listaFiltrada = lista;
                }
                else
                {
                    listaFiltrada = lista
                        .Where(c =>
                            (
                                !string.IsNullOrEmpty(c.idCategoria9) &&
                                c.idCategoria9.IndexOf(
                                    texto,
                                    StringComparison.OrdinalIgnoreCase) >= 0
                            )
                            ||
                            (
                                !string.IsNullOrEmpty(c.nombre) &&
                                c.nombre.IndexOf(
                                    texto,
                                    StringComparison.OrdinalIgnoreCase) >= 0
                            )
                        )
                        .ToList();
                }

                paginaActual = 1;

                MostrarPagina();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al buscar el Bien/Servicio.\n\n" + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnAnterior_Click(object sender, EventArgs e)
        {
            if (paginaActual > 1)
            {
                paginaActual--;
                MostrarPagina();
            }
        }

        private void btnSiguiente_Click(object sender, EventArgs e)
        {
            if (paginaActual < totalPaginas)
            {
                paginaActual++;
                MostrarPagina();
            }
        }

        private void txtbusqueda_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Buscar();

                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void lstvBienServicios_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (lstvBienServicios.SelectedItems.Count > 0)
                {
                    codigo = lstvBienServicios.SelectedItems[0].Text;

                    string tipoTexto =
                        lstvBienServicios.SelectedItems[0]
                            .SubItems[3]
                            .Text
                            .Trim()
                            .ToUpper();

                    int tipo = tipoTexto == "SERVICIOS" ? 1 : 2;

                    if (pasarDatosEvent != null)
                    {
                        pasarDatosEvent(codigo, tipo);
                    }

                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al seleccionar el CABYS.\n\n" + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void lstvBienServicios_SelectedIndexChanged(
            object sender,
            EventArgs e)
        {
        }
    }
}