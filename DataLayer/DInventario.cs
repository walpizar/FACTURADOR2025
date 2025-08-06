using CommonLayer;
using CommonLayer.Exceptions.DataExceptions;
using CommonLayer.Interfaces;
using EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataLayer
{
    public class DInventario : IDataGeneric<tbInventario>
    {


        public List<tbInventario> getInvetarioByQuery(string query)
        {

            try
            {

                using (Entities context = new Entities())
                {

                    if (query == string.Empty)
                    {
                        return (from u in context.tbInventario.Include("tbProducto.tbTipoMedidas").Include("tbProducto.tbCategoriaProducto")
                                where u.estado == true && u.tbProducto.estado == true
                                select u).ToList();

                    }
                    else
                    {
                        return (from u in context.tbInventario.Include("tbProducto.tbTipoMedidas").Include("tbProducto.tbCategoriaProducto")
                                where u.estado == true && u.idProducto.Trim().ToUpper().Contains(query.Trim().ToUpper()) ||
                                 u.tbProducto.nombre.Trim().ToUpper().Contains(query.Trim().ToUpper()) && u.tbProducto.estado == true
                                select u).ToList();

                    }





                }
            }
            catch (Exception ex)
            {
                throw new ListEntityException("Inventario");

            }
        }



        public List<tbInventario> GetListEntities(int estado)
        {
            List<tbInventario> inventario;
            try
            {

                using (Entities context = new Entities())
                {

                    if (estado == (int)Enums.EstadoBusqueda.Activo)
                    {
                        inventario = (from u in context.tbInventario.Include("tbProducto.tbTipoMedidas").Include("tbProducto.tbCategoriaProducto")                                      
                                      select u).ToList();
                        inventario = inventario.Where(p => p.tbProducto.estado == true).ToList();

                    }
                    else if (estado == (int)Enums.EstadoBusqueda.Inactivos)
                    {

                        inventario = (from u in context.tbInventario.Include("tbProducto.tbTipoMedidas").Include("tbProducto.tbCategoriaProducto")
                                      where u.estado == false
                                      select u).ToList();
                        inventario = inventario.Where(p => p.tbProducto.estado == false).ToList();

                    }
                    else
                    {
                        inventario = (from u in context.tbInventario.Include("tbProducto.tbTipoMedidas").Include("tbProducto.tbCategoriaProducto")
                                      select u).ToList();
                    }
                    return inventario;

                }
            }
            catch (Exception ex)
            {
                throw new ListEntityException("Inventario");

            }
        }








        public tbInventario Actualizar(tbInventario inve)
        {
            try
            {
                using (Entities update = new Entities())
                {

                    update.Entry(inve).State = System.Data.Entity.EntityState.Modified;
                    //si se quiere modicar mas entidas se pone . despues del envi y se le da la tabla
                    update.SaveChanges();

                    return inve;
                }
            }
            catch (Exception ex)
            {

                throw new UpdateEntityException("Inventario");
            }


        }

        public tbInventario Guardar(tbInventario entity)
        {
            throw new NotImplementedException();
        }

        public tbInventario GetEntity(tbInventario entity)
        {


            tbInventario inv;
            try
            {
                using (Entities context = new Entities())
                {
                    inv = (from p in context.tbInventario
                           where p.idProducto == entity.idProducto
                           select p).SingleOrDefault();//singleordefault me conviete en un solo registro
                }
                return inv;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public tbInventario GetEntityByIngrediente(int idIngrediente)
        {


            tbInventario inv;
            try
            {
                //using (Entities context = new Entities())
                //{
                //    inv = (from p in context.tbInventario
                //           where p.idIngrediente ==idIngrediente
                //           select p).SingleOrDefault();//singleordefault me conviete en un solo registro
                //}
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
