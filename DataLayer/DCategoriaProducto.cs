using CommonLayer;
using CommonLayer.Exceptions.DataExceptions;
using CommonLayer.Interfaces;
using EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataLayer
{
    public class DCategoriaProducto : IDataGeneric<tbCategoriaProducto>
    {


        /// <summary>
        /// Recuperamos todo las categorias que hay en la base de datos.
        /// </summary>
        /// <returns></returns>
        public List<tbCategoriaProducto> GetListEntities(int estado)
        {

            try
            {
                using (Entities context = new Entities())
                {

                    if (estado == (int)Enums.EstadoBusqueda.Activo)
                    {
                        //Recuperamos los datos de la tabla con el estado en activo.
                        return (from p in context.tbCategoriaProducto.Include("tbProducto.tbInventario").Include("tbProducto.tbImpuestos")
                                where p.estado == true
                                select p).ToList();
                    }
                    else if (estado == (int)Enums.EstadoBusqueda.Inactivos)
                    {
                        //Recuperamos los valores con el estado en inactivo
                        return (from p in context.tbCategoriaProducto.Include("tbProducto.tbInventario").Include("tbProducto.tbImpuestos")
                                where p.estado == false
                                select p).ToList();

                    }
                    else
                    {
                        //Recuperamos todos los valores de la tabla.
                        //Recuperamos los valores con el estado en inactivo
                        return (from p in context.tbCategoriaProducto.Include("tbProducto.tbInventario").Include("tbProducto.tbImpuestos")                              
                                select p).ToList();

                    }

                }
            }
            catch (Exception ex)
            {

                throw new Exception("Ha ocurrido el siguiente error: " + ex.ToString());
            }



        }
        public List<tbCategoriaProducto> GetListEntities()
        {

            try
            {
                using (Entities context = new Entities())
                {

                    //Recuperamos los datos de la tabla con el estado en activo.
                    return (from p in context.tbCategoriaProducto
                            where p.estado == true
                            select p).ToList();
                }
            }
            catch (Exception ex)
            {

                throw new Exception("Ha ocurrido el siguiente error: " + ex.ToString());
            }



        }

        /// <summary>
        /// Recuperamos las categorias para realizar el reporte.
        /// </summary>
        /// <returns></returns>
        public List<tbCategoriaProducto> getCategoriasReporte()
        {

            //Creamos una lista de tipo tbCategorias.
            List<tbCategoriaProducto> categorias = new List<tbCategoriaProducto>();

            try
            {
                using (Entities context = new Entities())
                {
                    //recuperamos los datos en una lista generica
                    var listaCategorias = (from p in context.tbCategoriaProducto
                                           select new { p.id, p.nombre, p.descripcion, p.estado, p.fecha_crea, p.usuario_crea }).ToList();


                    //Recorremos la lista generica para obtener los datos.
                    foreach (var p in listaCategorias)
                    {

                        tbCategoriaProducto cat = new tbCategoriaProducto();
                        cat.id = p.id;
                        cat.nombre = p.nombre;
                        cat.descripcion = p.descripcion;
                        cat.fecha_crea = p.fecha_crea;
                        cat.estado = p.estado;
                        cat.usuario_crea = p.usuario_crea;

                        categorias.Add(cat);

                    }

                }

                return categorias;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }


        /// <summary>
        /// Actualizamos la informacion de la categoria.
        /// </summary>
        /// <param name="categoriaNueva"></param>
        /// <returns></returns>
        public tbCategoriaProducto Actualizar(tbCategoriaProducto categoriaNueva)
        {
            try
            {
                using (Entities context = new Entities())
                {

                    context.Entry(categoriaNueva).State = System.Data.Entity.EntityState.Modified;
                    context.SaveChanges();

                    return categoriaNueva;

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
        public tbCategoriaProducto Guardar(tbCategoriaProducto categoriaNueva)
        {

            try
            {

                using (Entities context = new Entities())
                {
                    context.tbCategoriaProducto.Add(categoriaNueva);
                    context.SaveChanges();

                    return categoriaNueva;

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
        public tbCategoriaProducto GetEntity(tbCategoriaProducto entity)
        {

            try
            {

                using (Entities context = new Entities())
                {


                    entity = (from p in context.tbCategoriaProducto
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

        public tbCategoriaProducto GetEntityById(int id)
        {

            try
            {

                using (Entities context = new Entities())
                {


                    return (from p in context.tbCategoriaProducto
                            where p.id == id
                            select p).SingleOrDefault();


                }

            }
            catch (Exception ex)
            {

                throw new SaveEntityException(ex.Message);
            }


        }

        //public List<tbIngredientes> GetListIngrediente(int idBuscar)
        //{
        //    try
        //    {

        //        using (Entities context = new Entities())
        //        {

        //            return (from p in context.tbIngredientes.Include("tbTipoMedidas")
        //                    where p.idTipoIngrediente == idBuscar
        //                    select p).ToList();

        //        }


        //    }
        //    catch (Exception ex)
        //    {

        //        throw new Exception(ex.Message);
        //    }
        //}


        ///// <summary>
        ///// Retornamos los diferentes tipos de ingredientes.
        ///// </summary>
        ///// <returns></returns>
        //public List<tbTipoIngrediente> GetListTipoIngredientes()
        //{
        //    try
        //    {

        //        using (Entities context = new Entities())
        //        {
        //            return (from p in context.tbTipoIngrediente
        //                    select p).ToList();
        //        }


        //    }
        //    catch (Exception ex)
        //    {

        //        throw new Exception(ex.Message); ;
        //    }
        //}

        //cabys

        //public List<tbCategoria1Cabys> getCat1Cabys()
        //{

        //    try
        //    {

        //        using (Entities context = new Entities())
        //        {
        //            return (from p in context.tbCategoria1Cabys
        //                    select p).ToList();
        //        }


        //    }
        //    catch (Exception ex)
        //    {

        //        throw new Exception(ex.Message); ;
        //    }

        //}

        //public List<tbCategoria2Cabys> getCat2Cabys(string cat1)
        //{

        //    try
        //    {

        //        using (Entities context = new Entities())
        //        {
        //            return (from p in context.tbCategoria2Cabys
        //                    where p.idCategoria1 == cat1
        //                    select p).ToList();
        //        }


        //    }
        //    catch (Exception ex)
        //    {

        //        throw new Exception(ex.Message); ;
        //    }

        //}
        //public List<tbCategoria3Cabys> getCat3Cabys(string cat1, string cat2)
        //{

        //    try
        //    {

        //        using (Entities context = new Entities())
        //        {
        //            return (from p in context.tbCategoria3Cabys
        //                    where p.idCategoria1 == cat1 && p.idCategoria2 == cat2
        //                    select p).ToList();
        //        }


        //    }
        //    catch (Exception ex)
        //    {

        //        throw new Exception(ex.Message); ;
        //    }

        //}
        //public List<tbCategoria4Cabys> getCat4Cabys(string cat1, string cat2, string cat3)
        //{

        //    try
        //    {

        //        using (Entities context = new Entities())
        //        {
        //            return (from p in context.tbCategoria4Cabys
        //                    where p.idCategoria1 == cat1 && p.idCategoria2 == cat2 && p.idCategoria3 == cat3
        //                    select p).ToList();
        //        }


        //    }
        //    catch (Exception ex)
        //    {

        //        throw new Exception(ex.Message); ;
        //    }

        //}
        //public List<tbCategoria5Cabys> getCat5Cabys(string cat1, string cat2, string cat3, string cat4)
        //{

        //    try
        //    {

        //        using (Entities context = new Entities())
        //        {
        //            return (from p in context.tbCategoria5Cabys
        //                    where p.idCategoria1 == cat1 && p.idCategoria2 == cat2 && p.idCategoria3 == cat3 && p.idCategoria4 == cat4
        //                    select p).ToList();
        //        }


        //    }
        //    catch (Exception ex)
        //    {

        //        throw new Exception(ex.Message); ;
        //    }

        //}
        //public List<tbCategoria6Cabys> getCat6Cabys(string cat1, string cat2, string cat3, string cat4, string cat5)
        //{

        //    try
        //    {

        //        using (Entities context = new Entities())
        //        {
        //            return (from p in context.tbCategoria6Cabys
        //                    where p.idCategoria1 == cat1 && p.idCategoria2 == cat2 && p.idCategoria3 == cat3 && p.idCategoria4 == cat4
        //                    && p.idCategoria5 == cat5
        //                    select p).ToList();
        //        }


        //    }
        //    catch (Exception ex)
        //    {

        //        throw new Exception(ex.Message); ;
        //    }

        //}
        //public List<tbCategoria7Cabys> getCat7Cabys(string cat1, string cat2, string cat3, string cat4, string cat5,
        //    string cat6)
        //{

        //    try
        //    {

        //        using (Entities context = new Entities())
        //        {
        //            return (from p in context.tbCategoria7Cabys
        //                    where p.idCategoria1 == cat1 && p.idCategoria2 == cat2 && p.idCategoria3 == cat3 && p.idCategoria4 == cat4
        //                   && p.idCategoria5 == cat5 && p.idCategoria6 == cat6
        //                    select p).ToList();
        //        }


        //    }
        //    catch (Exception ex)
        //    {

        //        throw new Exception(ex.Message); ;
        //    }

        //}
        //public List<tbCategoria8Cabys> getCat8Cabys(string cat1, string cat2, string cat3, string cat4, string cat5,
        //    string cat6, string cat7)
        //{

        //    try
        //    {

        //        using (Entities context = new Entities())
        //        {
        //            return (from p in context.tbCategoria8Cabys
        //                    where p.idCategoria1 == cat1 && p.idCategoria2 == cat2 && p.idCategoria3 == cat3 && p.idCategoria4 == cat4
        //                  && p.idCategoria5 == cat5 && p.idCategoria6 == cat6 && p.idCategoria7 == cat7
        //                    select p).ToList();
        //        }


        //    }
        //    catch (Exception ex)
        //    {

        //        throw new Exception(ex.Message); ;
        //    }

        //}
        //public List<tbCategoria9Cabys> getCat9Cabys(string cat1, string cat2, string cat3, string cat4, string cat5,
        //    string cat6, string cat7, string cat8)
        //{

        //    try
        //    {

        //        using (Entities context = new Entities())
        //        {
        //            return (from p in context.tbCategoria9Cabys
        //                    where p.idCategoria1 == cat1 && p.idCategoria2 == cat2 && p.idCategoria3 == cat3 && p.idCategoria4 == cat4
        //                 && p.idCategoria5 == cat5 && p.idCategoria6 == cat6 && p.idCategoria7 == cat7 && p.idCategoria8 == cat8
        //                    select p).ToList();
        //        }


        //    }
        //    catch (Exception ex)
        //    {

        //        throw new Exception(ex.Message); ;
        //    }

        //}

        //public List<tbCategoria9Cabys> getCat9CabysByText(string text)
        //{

        //    try
        //    {

        //        using (Entities context = new Entities())
        //        {
        //            return (from p in context.tbCategoria9Cabys
        //                    where p.nombre.Trim().ToUpper().Contains(text) || p.idCategoria9.Trim().ToUpper().Contains(text)
        //                    select p).ToList();
        //        }


        //    }
        //    catch (Exception ex)
        //    {

        //        throw new Exception(ex.Message); ;
        //    }

        //}

        //public tbCategoria9Cabys getCat9CabysById(string text)
        //{

        //    try
        //    {

        //        using (Entities context = new Entities())
        //        {
        //            return (from p in context.tbCategoria9Cabys
        //                    where p.idCategoria9.Trim() == text.Trim()
        //                    select p).SingleOrDefault();
        //        }


        //    }
        //    catch (Exception ex)
        //    {

        //        throw new Exception(ex.Message); ;
        //    }

        //}

    }




}
