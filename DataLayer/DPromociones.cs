using CommonLayer;
using CommonLayer.Exceptions.DataExceptions;
using CommonLayer.Interfaces;
using EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class DPromociones: IDataGeneric<tbPromociones>
    {
        public DPromociones()
        {
        }

        public tbPromociones Actualizar(tbPromociones entity)
        {
            try
            {               

                using (Entities context = new Entities())
                {
                    entity.tbProducto = null;              
                    context.Entry(entity).State = System.Data.Entity.EntityState.Modified;

                    context.SaveChanges();

                    return entity;

                }
            }
            catch (Exception ex)
            {
                throw new UpdateEntityException(ex.Message);
            }
        }



        public tbPromociones GetEntity(tbPromociones entity)
        {
            throw new NotImplementedException();
        }

        public List<tbPromociones> GetListEntities(int estado)
        {
            try
            {
                using (Entities context = new Entities())
                {
                
                    if (estado == (int)Enums.EstadoBusqueda.Activo)
                    {
                        return (from T in context.tbPromociones.Include("tbProducto")
                                where T.estado == true
                                select T).ToList();
                    }
                    else if (estado == (int)Enums.EstadoBusqueda.Inactivos)
                    {
                        return (from T in context.tbPromociones.Include("tbProducto")
                                where T.estado == false
                                select T).ToList();
                    }
                    else 

                    {
                        return (from T in context.tbPromociones.Include("tbProducto")
                                select T).ToList();
                    }                    
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<tbPromociones> getPromosByIdProdFechas(string idProd, DateTime fechaActual)
        {
            return getPromosByIdProdFechas(0, idProd, fechaActual);
        }
        public List<tbPromociones> getPromosByIdProdFechas(int idProm,string idProd, DateTime fechaActual)
        {
            try
            {
                using (Entities context = new Entities())
                {
                    if (idProm==0) {
                        return (from T in context.tbPromociones.Include("tbProducto")
                                where T.estado == true && T.idProducto.Trim().ToUpper() == idProd &&
                                (T.fechaIncio <= fechaActual &&
                                T.fechaCierre >= fechaActual) 
                                select T).ToList();
                    }
                    else
                    {
                        return (from T in context.tbPromociones.Include("tbProducto")
                                where T.id!=idProm && T.estado == true && T.idProducto.Trim().ToUpper() == idProd 
                                select T).ToList();

                    }
                        


                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public tbPromociones Guardar(tbPromociones entity)
        {
            try
            {

                using (Entities context = new Entities())
                {

                    context.tbPromociones.Add(entity);
                    context.SaveChanges();

                    return entity;

                }


            }
            catch (Exception ex)
            {

                throw new SaveEntityException(ex.Message);
            }
        }
    }
}
