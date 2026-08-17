using EntityLayer;
using System.Collections.Generic;
using System.Linq;

namespace DataLayer
{
    public class DActividades
    {
        public List<tbActividades> Buscar(string filtro)
        {
            using (var db = new Entities()) // TODO: reemplazar por tu DbContext real
            {
                var query = db.tbActividades.AsQueryable();

                if (!string.IsNullOrWhiteSpace(filtro))
                {
                    filtro = filtro.Trim().ToLower();
                    query = query.Where(a =>
                        a.codigoAct.ToLower().Contains(filtro) ||
                        a.nombreAct.ToLower().Contains(filtro));
                }

                return query.OrderBy(a => a.codigoAct).Take(100).ToList();
            }
        }
    }
}