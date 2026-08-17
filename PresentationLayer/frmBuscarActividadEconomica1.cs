using BusinessLayer;
using EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace PresentationLayer
{
    public partial class frmBuscarActividadEconomica1 : Form
    {
        private readonly BActividades actividadesInst = new BActividades();
        private List<tbActividades> resultadoCatalogo = new List<tbActividades>();

        // Lista de trabajo (copia local) — se muestra en dgvAsignadas
        private readonly List<tbClientesActividadesEconomicas> actividadesAsignadas;
        private readonly string identificacionCliente;
        private bool actualizando = false;

        /// <summary>Resultado final si el usuario presiona Aceptar.</summary>
        public List<tbClientesActividadesEconomicas> ActividadesResultado { get; private set; }

        /// <param name="actividadesActuales">
        /// Actividades ya asignadas al cliente (puede venir vacía si es un cliente nuevo).
        /// </param>
        /// <param name="identificacionCliente">
        /// Cédula/identificación del cliente, usada para consultar Hacienda. Puede venir
        /// vacía (ej. cliente nuevo sin identificación aún) — en ese caso el botón
        /// "Cons. Hacienda" avisa que hace falta la identificación.
        /// </param>
        public frmBuscarActividadEconomica1(
            List<tbClientesActividadesEconomicas> actividadesActuales,
            string identificacionCliente)
        {
            InitializeComponent();

            this.identificacionCliente = identificacionCliente;

            // Copia defensiva: si el usuario cancela, no se toca la lista original del form padre.
            actividadesAsignadas = (actividadesActuales ?? new List<tbClientesActividadesEconomicas>())
                .Select(a => new tbClientesActividadesEconomicas
                {
                    idCliente = a.idCliente,
                    tipoIdCliente = a.tipoIdCliente,
                    CodigoCIIU = a.CodigoCIIU,
                    esPrincipal = a.esPrincipal,
                    estado = true,
                    NombreActividad = a.NombreActividad
                })
                .ToList();
        }

        private void frmBuscarActividadEconomica1_Load(object sender, EventArgs e)
        {
            BuscarCatalogo(string.Empty);
            RedibujarAsignadas();
        }

        // ---------- Catálogo ----------

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            BuscarCatalogo(txtBuscar.Text);
        }

        private void BuscarCatalogo(string filtro)
        {
            var codigosYaAsignados = new HashSet<string>(actividadesAsignadas.Select(a => a.CodigoCIIU));

            resultadoCatalogo = actividadesInst.Buscar(filtro)
                .Where(a => !codigosYaAsignados.Contains(a.codigoAct))
                .ToList();

            dgvCatalogo.Rows.Clear();
            foreach (var act in resultadoCatalogo)
            {
                int idx = dgvCatalogo.Rows.Add(act.codigoAct, act.nombreAct);
                dgvCatalogo.Rows[idx].Tag = act;
            }
        }

        private void dgvCatalogo_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) AgregarDesdeCatalogo();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            AgregarDesdeCatalogo();
        }

        private void AgregarDesdeCatalogo()
        {
            if (dgvCatalogo.CurrentRow == null) return;

            var actividad = (tbActividades)dgvCatalogo.CurrentRow.Tag;

            actividadesAsignadas.Add(new tbClientesActividadesEconomicas
            {
                CodigoCIIU = actividad.codigoAct,
                NombreActividad = actividad.nombreAct,
                esPrincipal = actividadesAsignadas.Count == 0, // la primera queda como principal
                estado = true
            });

            BuscarCatalogo(txtBuscar.Text); // la saca del catálogo (ya está asignada)
            RedibujarAsignadas();
        }

        // ---------- Asignadas ----------

        private void RedibujarAsignadas()
        {
            actualizando = true;
            dgvAsignadas.Rows.Clear();

            foreach (var act in actividadesAsignadas)
            {
                string nombre = act.NombreActividad ?? act.CodigoCIIU;
                int idx = dgvAsignadas.Rows.Add(act.CodigoCIIU, nombre, act.esPrincipal);
                dgvAsignadas.Rows[idx].Tag = act;
            }

            actualizando = false;
        }

        private void dgvAsignadas_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dgvAsignadas.CurrentCell is DataGridViewCheckBoxCell && dgvAsignadas.IsCurrentCellDirty)
            {
                dgvAsignadas.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void dgvAsignadas_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (actualizando || e.RowIndex < 0) return;
            if (dgvAsignadas.Columns[e.ColumnIndex].Name != "colAsigPrincipal") return;

            var filaClic = dgvAsignadas.Rows[e.RowIndex];
            var actividadClic = (tbClientesActividadesEconomicas)filaClic.Tag;
            bool marcada = (bool)(filaClic.Cells["colAsigPrincipal"].Value ?? false);

            if (!marcada)
            {
                // No se permite dejar la lista sin principal: se revierte el desmarcado.
                actualizando = true;
                filaClic.Cells["colAsigPrincipal"].Value = true;
                actualizando = false;
                return;
            }

            actualizando = true;
            foreach (DataGridViewRow fila in dgvAsignadas.Rows)
            {
                var act = (tbClientesActividadesEconomicas)fila.Tag;
                act.esPrincipal = ReferenceEquals(act, actividadClic);
                fila.Cells["colAsigPrincipal"].Value = act.esPrincipal;
            }
            actualizando = false;
        }

        private void btnQuitar_Click(object sender, EventArgs e)
        {
            if (dgvAsignadas.CurrentRow == null)
            {
                MessageBox.Show("Seleccione una actividad para quitar.", "Actividades económicas",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var actividad = (tbClientesActividadesEconomicas)dgvAsignadas.CurrentRow.Tag;
            bool eraPrincipal = actividad.esPrincipal;

            actividadesAsignadas.Remove(actividad);

            if (eraPrincipal && actividadesAsignadas.Count > 0)
            {
                actividadesAsignadas[0].esPrincipal = true;
            }

            RedibujarAsignadas();
            BuscarCatalogo(txtBuscar.Text); // vuelve a aparecer disponible en el catálogo
        }

        // ---------- Hacienda ----------

        private async void btnHacienda_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(identificacionCliente))
            {
                MessageBox.Show(
                    "Debe indicar la identificación del cliente antes de consultar Hacienda.",
                    "Consulta Hacienda", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            btnHacienda.Enabled = false;
            btnHacienda.Text = "Consultando...";

            try
            {
                var resultado = await FacturacionElectronicaLayer.Clases.ConsultasHacienda
                    .ObtenerActividadesAsync(identificacionCliente);

                if (resultado == null || resultado.Actividades == null || resultado.Actividades.Count == 0)
                {
                    MessageBox.Show(
                        "Hacienda no devolvió actividades económicas para esta identificación.",
                        "Consulta Hacienda", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                int agregadas = 0;

                foreach (var act in resultado.Actividades)
                {
                    if (!string.Equals(act.Estado, "A", StringComparison.OrdinalIgnoreCase))
                    {
                        continue; // solo activas según Hacienda
                    }

                    string codigo = (act.Codigo.ToString() ?? string.Empty).Trim();
                    if (string.IsNullOrEmpty(codigo)) continue;

                    bool yaAsignada = actividadesAsignadas.Any(a => a.CodigoCIIU == codigo);
                    if (yaAsignada) continue;

                    actividadesAsignadas.Add(new tbClientesActividadesEconomicas
                    {
                        CodigoCIIU = codigo,
                        NombreActividad = act.Descripcion,
                        esPrincipal = actividadesAsignadas.Count == 0, // primera agregada = principal
                        estado = true
                    });
                    agregadas++;
                }

                RedibujarAsignadas();
                BuscarCatalogo(txtBuscar.Text);

                MessageBox.Show(
                    agregadas > 0
                        ? $"Se agregaron {agregadas} actividad(es) activa(s) según Hacienda. Verifique cuál debe quedar como principal."
                        : "Las actividades activas de Hacienda ya estaban asignadas.",
                    "Consulta Hacienda", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al consultar Hacienda: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnHacienda.Enabled = true;
                btnHacienda.Text = "Cons. Hacienda";
            }
        }

        // ---------- Aceptar / Cancelar ----------

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (actividadesAsignadas.Count == 0)
            {
                MessageBox.Show("Debe asignar al menos una actividad económica.", "Actividades económicas",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!actividadesAsignadas.Any(a => a.esPrincipal))
            {
                MessageBox.Show("Debe existir una actividad marcada como principal.", "Actividades económicas",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            ActividadesResultado = actividadesAsignadas;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}