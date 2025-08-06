using CommonLayer.Interfaces;
using EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;


namespace DataLayer
{
    public class DTipoId : IDataGeneric<tbTipoId>
    {
        public tbTipoId Actualizar(tbTipoId entity)
        {
            throw new NotImplementedException();
        }

        public tbTipoId GetEntity(tbTipoId entity)
        {
            throw new NotImplementedException();
        }

        public List<tbTipoId> GetListEntities(int estado)
        {
            try
            {
                using (Entities context = new Entities())
                {

                    return (from T in context.tbTipoId
                            select T).ToList();

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public tbTipoId Guardar(tbTipoId entity)
        {
            throw new NotImplementedException();
        }
    }
}





