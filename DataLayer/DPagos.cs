using CommonLayer.Exceptions.DataExceptions;
using EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataLayer
{
    public class DPagos
    {
        public List<tbPagos> GetListEntities()
        {
            // List<tbCreditos> listaCreditos;
            try
            {
                Entities context = new Entities();


                return (from p in context.tbPagos
                        select p).ToList();//



            }
            catch (Exception)
            {
                throw new EntityException();
            }
        } //GetListEntities
        public tbPagos Guardar(tbPagos abono)
        {
            try
            {
                using (Entities context = new Entities())
                {
                    context.tbPagos.Add(abono);
                    context.SaveChanges();

                }
            }
            catch (Exception ex)
            {
                throw ex;
                //throw new SaveEntityException();
            }


            return abono;
        } //Guardar

        public tbPagos Modificar(tbPagos abono)
        {
            try
            {
                tbPagos creditoNuevo = new tbPagos();
                using (Entities context = new Entities())
                {
                    context.Entry(abono).State = System.Data.Entity.EntityState.Modified;//mnada entidad y la modifica
                    //Si viniera otra tabla relacionada que se debiera modificar se pone la misma linea de coduigo de arriba 
                    //y se manda a modificar
                    context.SaveChanges();
                    return creditoNuevo;
                }
            }
            catch (Exception ex)
            {
                throw new UpdateEntityException();
            }

        }
    }
}
