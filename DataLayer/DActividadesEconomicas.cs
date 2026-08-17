using CommonLayer.Exceptions.DataExceptions;
using CommonLayer.Interfaces;
using EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataLayer
{
    public class DActividadesEconomicas : IDataGeneric<tbActividades>
    {
        public List<tbClientesActividadesEconomicas> ObtenerPorCliente(string idCliente, int tipoIdCliente)
        {
            using (var db = new Entities())
            {
                var query = from ca in db.tbClientesActividadesEconomicas
                            join act in db.tbActividades on ca.CodigoCIIU equals act.codigoAct into actJoin
                            from act in actJoin.DefaultIfEmpty()
                            where ca.idCliente == idCliente
                               && ca.tipoIdCliente == tipoIdCliente
                               && ca.estado
                            select new { Asignacion = ca, NombreAct = act.nombreAct };

                var resultado = query.ToList();

                foreach (var item in resultado)
                {
                    item.Asignacion.NombreActividad = item.NombreAct;
                }

                return resultado.Select(x => x.Asignacion).ToList();
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


        public tbActividades Actualizar(tbActividades entity)
        {
            throw new NotImplementedException();
        }

        public tbActividades GetEntity(tbActividades entity)
        {
            try
            {
                using (Entities context = new Entities())

                    return (from p in context.tbActividades
                            where p.codigoAct == entity.codigoAct
                            select p).SingleOrDefault();



            }
            catch (Exception)
            {

                throw new SaveEntityException("Actividad Economica");
            }
        }

        public List<tbEmpresaActividades> getListaEmpresaActividad(string id, int tipo)
        {
            try
            {
                // SE AGREGA EL USING
                using (Entities context = new Entities())

                    //PARA JALAR LOS DATOS DE LAS TABLAS RELACIONADAS SE USA LA PALABRA RESERVADA INCLUDE....Y ENTRE("")EL NOMBRE DE LAS TABLAS RELACIONADAS.
                    return (from p in context.tbEmpresaActividades.Include("tbActividades")
                            where p.idEmpresa == id && p.tipoId == tipo
                            select p
                           ).ToList();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<tbActividades> GetListEntities(int estado)
        {
            try
            {
                // SE AGREGA EL USING
                using (Entities context = new Entities())

                    //PARA JALAR LOS DATOS DE LAS TABLAS RELACIONADAS SE USA LA PALABRA RESERVADA INCLUDE....Y ENTRE("")EL NOMBRE DE LAS TABLAS RELACIONADAS.
                    return (from p in context.tbActividades
                            select p).ToList();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public tbActividades Guardar(tbActividades entity)
        {
            throw new NotImplementedException();
        }
    }
}
