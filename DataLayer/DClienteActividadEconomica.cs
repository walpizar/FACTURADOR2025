using EntityLayer;
using System;
using System.Collections.Generic;
using System.Data.Entity; // necesario para .Include (Entity Framework 6)
using System.Linq;

namespace DataLayer
{
    public class DClienteActividadEconomica
    {
        // TODO: reemplazar "Entities" por el nombre real de tu DbContext (EDMX/EF6).
        // Es el mismo que usan las demás clases D* del proyecto (ej. DActividades, DProvincias, etc.).

        /// <summary>
        /// Obtiene las actividades económicas activas asignadas a un cliente,
        /// incluyendo los datos del catálogo (tbActividades) para mostrar el nombre.
        /// </summary>
        public List<tbClientesActividadesEconomicas> ObtenerPorCliente(string idCliente, int tipoIdCliente)
        {
            using (var db = new Entities())
            {
                var asignadas = db.tbClientesActividadesEconomicas
                    .Where(a => a.idCliente == idCliente
                             && a.tipoIdCliente == tipoIdCliente
                             && a.estado)
                    .ToList();

                if (asignadas.Count == 0) return asignadas;

                var codigos = asignadas
                    .Select(a => (a.CodigoCIIU ?? string.Empty).Trim())
                    .Distinct()
                    .ToList();

                // Traemos el catálogo y hacemos el match en memoria (evita problemas
                // de padding/CHAR al traducir la comparación a SQL).
                var nombresPorCodigo = db.tbActividades
                    .ToList()
                    .Where(act => codigos.Contains((act.codigoAct ?? string.Empty).Trim()))
                    .GroupBy(act => act.codigoAct.Trim())
                    .ToDictionary(g => g.Key, g => g.First().nombreAct);

                foreach (var a in asignadas)
                {
                    string codigo = (a.CodigoCIIU ?? string.Empty).Trim();
                    a.NombreActividad = nombresPorCodigo.TryGetValue(codigo, out var nombre)
                        ? nombre
                        : null;
                }

                return asignadas;
            }
        }

        /// <summary>
        /// Sincroniza el conjunto completo de actividades económicas de un cliente contra la base de datos:
        /// - Desactiva (estado = false) las que existían y ya no vienen en la lista nueva.
        /// - Inserta las que son nuevas.
        /// - Actualiza esPrincipal/estado de las que ya existían y siguen asignadas.
        /// </summary>
        public void Guardar(
            string idCliente,
            int tipoIdCliente,
            List<tbClientesActividadesEconomicas> actividades,
            string usuario)
        {
            using (var db = new Entities())
            {
                var existentes = db.tbClientesActividadesEconomicas
                    .Where(a => a.idCliente == idCliente && a.tipoIdCliente == tipoIdCliente)
                    .ToList();

                // Desactiva las que ya no están en la lista nueva
                foreach (var existente in existentes)
                {
                    bool sigueAsignada = actividades.Any(a => a.CodigoCIIU == existente.CodigoCIIU);
                    if (!sigueAsignada && existente.estado)
                    {
                        existente.estado = false;
                        existente.usuario_ult_mod = usuario;
                        existente.fecha_ult_mod = DateTime.Now;
                    }
                }

                // Inserta nuevas / actualiza existentes (principal, estado)
                foreach (var act in actividades)
                {
                    var existente = existentes.FirstOrDefault(a => a.CodigoCIIU == act.CodigoCIIU);

                    if (existente == null)
                    {
                        db.tbClientesActividadesEconomicas.Add(new tbClientesActividadesEconomicas
                        {
                            idCliente = idCliente,
                            tipoIdCliente = tipoIdCliente,
                            CodigoCIIU = act.CodigoCIIU,
                            esPrincipal = act.esPrincipal,
                            estado = true,
                            fecha_crea = DateTime.Now,
                            usuario_crea = usuario
                        });
                    }
                    else
                    {
                        existente.esPrincipal = act.esPrincipal;
                        existente.estado = true;
                        existente.usuario_ult_mod = usuario;
                        existente.fecha_ult_mod = DateTime.Now;
                    }
                }

                db.SaveChanges();
            }
        }
    }
}