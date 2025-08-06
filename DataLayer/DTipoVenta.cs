using CommonLayer.Interfaces;
using EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;


namespace DataLayer
{
    public class DtipoVenta : IDataGeneric<tbTipoVenta>
    {
        public tbTipoVenta Actualizar(tbTipoVenta entity)
        {
            throw new NotImplementedException();
        }

        public tbTipoVenta GetEntity(tbTipoVenta entity)
        {
            throw new NotImplementedException();
        }

        public List<tbTipoVenta> GetListEntities(int estado)
        {
            try
            {



                using (Entities context = new Entities())//utilizamos el using para todas las consultas
                {



                    return (from tipIn in context.tbTipoVenta
                            select tipIn).ToList();


                }





            }

            catch (Exception ex)
            {

                throw ex;
            }
        }

        public tbTipoVenta Guardar(tbTipoVenta entity)
        {
            throw new NotImplementedException();
        }
    }
}





