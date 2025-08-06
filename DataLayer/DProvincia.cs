using CommonLayer.Exceptions.DataExceptions;
using CommonLayer.Interfaces;
using EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataLayer
{
    public class DProvincia : IDataGeneric<tbProvincia>
    {
        public tbProvincia Actualizar(tbProvincia entity)
        {
            throw new NotImplementedException();
        }

        public tbProvincia GetEntity(tbProvincia provincia)
        {
            tbProvincia prov;

            try
            {
                using (Entities context = new Entities())
                {


                    //desde      CONSULTA:
                    prov = (from u in context.tbProvincia.Include("tbCanton.tbDistrito")
                            where u.Cod == provincia.Cod
                            select u).SingleOrDefault();//me devuelve una sola entidad

                    return prov;
                }

            }
            catch (Exception)
            {
                throw new EntityException("prov");
            }


        }



        public List<tbProvincia> GetListEntities(int estado)
        {
            try
            {

                List<tbProvincia> provincia = new List<tbProvincia>();

                using (Entities context = new Entities())//utilizamos el using para todas las consultas
                {



                    provincia = (from tipIn in context.tbProvincia.Include("tbCanton.tbDistrito.tbBarrios")
                                 select tipIn).ToList();


                }
                return provincia;




            }

            catch (Exception ex)
            {

                throw new ListEntityException("provincia");
            }
        }

        public tbProvincia Guardar(tbProvincia entity)
        {
            throw new NotImplementedException();
        }
    }
}
