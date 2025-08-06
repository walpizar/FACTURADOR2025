using CommonLayer.Interfaces;
using EntityLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace DataLayer
{
    public class DCanton : IDataGeneric<tbCanton>
    {
        public tbCanton Actualizar(tbCanton entity)
        {
            throw new NotImplementedException();
        }

        public tbCanton GetEntity(string idC, string idP)
        {
            try
            {
                using (Entities context = new Entities())
                {
                    return (from p in context.tbCanton.Include("tbProvincia")
                            where p.Canton == idC && p.Provincia == idP
                            select p).SingleOrDefault();//singleordefault me conviete en un solo registro
                }

            }
            catch (Exception ex)
            {
                throw new EntityException();
            }
        }

        public tbCanton GetEntity(tbCanton entity)
        {
            throw new NotImplementedException();
        }

        public List<tbCanton> GetListEntities(int estado)
        {
            throw new NotImplementedException();
        }

        public tbCanton Guardar(tbCanton entity)
        {
            throw new NotImplementedException();
        }
    }
}
