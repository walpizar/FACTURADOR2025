using CommonLayer.Interfaces;
using EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;


namespace DataLayer
{
    public class DExoneraciones : IDataGeneric<tbExoneraciones>
    {
        public tbExoneraciones Actualizar(tbExoneraciones entity)
        {
            throw new NotImplementedException();
        }

        public tbExoneraciones GetEntity(int id)
        {
            try
            {



                using (Entities context = new Entities())//utilizamos el using para todas las consultas
                {



                    return (from tipIn in context.tbExoneraciones
                            where tipIn.id == id
                            select tipIn
                            ).SingleOrDefault();


                }





            }

            catch (Exception ex)
            {

                throw ex;
            }
        }

        public tbExoneraciones GetEntity(tbExoneraciones entity)
        {
            throw new NotImplementedException();
        }

        public List<tbExoneraciones> GetListEntities(int estado)
        {
            try
            {



                using (Entities context = new Entities())//utilizamos el using para todas las consultas
                {



                    return (from tipIn in context.tbExoneraciones
                            select tipIn).ToList();


                }





            }

            catch (Exception ex)
            {

                throw ex;
            }
        }

        public tbExoneraciones Guardar(tbExoneraciones entity)
        {
            throw new NotImplementedException();
        }
    }
}





