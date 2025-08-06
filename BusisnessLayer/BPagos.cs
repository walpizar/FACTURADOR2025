using DataLayer;
using EntityLayer;
using System;
using System.Collections.Generic;

namespace BusinessLayer
{
    public class BPagos
    {

        public DPagos pagosIns = new DPagos();
        public List<tbPagos> GetListEntities()
        {
            return pagosIns.GetListEntities();
        }

        public tbPagos Guardar(tbPagos abono)//funcion recibe objeto
        {


            return pagosIns.Guardar(abono);



        }

        public List<tbPagos> Modificar(List<tbPagos> entity)//tabla de creditos no de clientes
        {
            try
            {
                foreach (tbPagos u in entity)
                {
                    pagosIns.Modificar(u);

                }

                return entity;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


    }
}
