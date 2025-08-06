using CommonLayer;
using CommonLayer.Exceptions.DataExceptions;
using EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class DAcompanamiento
    {
        public List<tbAcompanamiento> GetListEntities(int estado)
        {

            try
            {
                using (Entities context = new Entities())
                {

                    if (estado == (int)Enums.EstadoBusqueda.Activo)
                    {
                        //Recuperamos los datos de la tabla con el estado en activo.
                        return (from p in context.tbAcompanamiento.Include("tbProducto")
                                where p.estado == true
                                select p).ToList();
                    }
                    else if (estado == (int)Enums.EstadoBusqueda.Inactivos)
                    {
                        //Recuperamos los valores con el estado en inactivo
                        return (from p in context.tbAcompanamiento.Include("tbProducto")
                                where p.estado == false
                                select p).ToList();

                    }
                    else
                    {
                        //Recuperamos todos los valores de la tabla.
                        return (from p in context.tbAcompanamiento.Include("tbProducto")
                                select p).ToList();

                    }

                }
            }
            catch (Exception ex)
            {

                throw new Exception("Ha ocurrido el siguiente error: " + ex.ToString());
            }



        }
        public List<tbAcompanamiento> GetListEntities()
        {

            try
            {
                using (Entities context = new Entities())
                {

                  
                        return (from p in context.tbAcompanamiento.Include("tbProducto")
                                where p.estado == true
                                select p).ToList();                   

                }
            }
            catch (Exception ex)
            {

                throw new Exception("Ha ocurrido el siguiente error: " + ex.ToString());
            }



        }

        public tbAcompanamiento Actualizar(tbAcompanamiento acompa)
        {
            try
            {
                using (Entities context = new Entities())
                {

                    context.Entry(acompa).State = System.Data.Entity.EntityState.Modified;
                    context.SaveChanges();

                    return acompa;

                }

            }


            catch (Exception ex)
            {
                throw new UpdateEntityException("Ha ocurrido el siguiente error: " + ex.ToString());
            }

        }


        /// <summary>
        /// Almacenados la categoria en la base de datos.
        /// </summary>
        /// <param name="categoriaNueva"></param>
        /// <returns></returns>
        public tbAcompanamiento Guardar(tbAcompanamiento acompa)
        {

            try
            {

                using (Entities context = new Entities())
                {
                    context.tbAcompanamiento.Add(acompa);
                    context.SaveChanges();

                    return acompa;

                }


            }


            catch (Exception ex)
            {
                throw new SaveEntityException("Ha ocurrido el siguiente error: " + ex.ToString());
            }

        }




        /// <summary>
        /// Recuperamos la entidad de la base de datos para saber si ya existe.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public tbAcompanamiento GetEntity(tbAcompanamiento entity)
        {

            try
            {

                using (Entities context = new Entities())
                {


                    entity = (from p in context.tbAcompanamiento
                              where p.nombre == entity.nombre
                              select p).SingleOrDefault();

                    return entity;
                }

            }
            catch (Exception ex)
            {

                throw new SaveEntityException(ex.Message);
            }


        }

        public int GetLastID()
        {

            try
            {

                using (Entities context = new Entities())
                {


                    var lista = (from p in context.tbAcompanamiento                              
                              select p).ToList();

                    return lista.Count==0 ? 1: lista.Max(x=>x.id)+1;
                }

            }
            catch (Exception ex)
            {

                throw new SaveEntityException(ex.Message);
            }


        }




    }
}
