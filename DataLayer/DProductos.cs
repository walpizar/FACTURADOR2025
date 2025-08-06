using CommonLayer;
using CommonLayer.Exceptions.DataExceptions;
using CommonLayer.Interfaces;
using EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataLayer
{
    public class DProductos : IDataGeneric<tbProducto>
    {

        /// <summary>
        /// Guardamos la entidad en la base de datos.
        /// </summary>
        /// <param name="productoNuevo"></param>
        /// <returns></returns>
        public tbProducto Guardar(tbProducto productoNuevo)
        {

            try
            {

                using (Entities context = new Entities())
                {

                    context.tbProducto.Add(productoNuevo);
                    context.SaveChanges();

                    return productoNuevo;

                }


            }
            catch (Exception ex)
            {

                throw new SaveEntityException(ex.Message);
            }

        }

        public tbProducto GuardarAcompa(tbProducto pro, List<tbAcompanamiento> lista)
        {

            try
            {
                using (Entities context = new Entities())
                {

                    context.Database.ExecuteSqlCommand("Delete tbProdutoAcompa where idProducto = {0}", pro.idProducto);
                    context.SaveChanges();


                    pro = GetEntityByIdAlone(pro.idProducto);
                    context.Entry(pro).State = System.Data.Entity.EntityState.Modified;

                    //foreach (var item in pro.tbAcompanamiento)
                    //{
                    //    if (!lista.Contains(item))
                    //    {
                    //        var ac = (from p in context.tbAcompanamiento
                    //                  where p.id == item.id
                    //                  select p).SingleOrDefault();
                    //        pro.tbAcompanamiento.Remove(ac);
                    //    }

                    //}

                    foreach (var item in lista)
                    {
                       
                            var ac = (from p in context.tbAcompanamiento
                                                  where p.id ==item.id
                                                  select p).SingleOrDefault();
                            pro.tbAcompanamiento.Add(ac);
                      
                    }

                  






                    //if (selectedCoursesHS.Contains(course.CourseID.ToString()))
                    //{
                    //    if (!instructorCourses.Contains(course.CourseID))
                    //    {
                    //        instructorToUpdate.Courses.Add(course);
                    //    }
                    //}

                    //pro = GetEntityByIdAlone(pro.idProducto);
                    //context.Entry(pro.tbAcompanamiento).State = System.Data.Entity.EntityState.Modified;
                    //foreach (var item in lista)
                    //{
                        
                    //    pro.tbAcompanamiento.Add(item);
                    //}
                    ////foreach (var item in pro.tbAcompanamiento)
                    ////{
                    ////    context.Entry(item).State = System.Data.Entity.EntityState.Modified;
                    ////}



                    context.SaveChanges();
                    return pro;
                }

            }
            catch (Exception EX)
            {
                throw new UpdateEntityException(" Empresas  ");


            }

        }

        public bool ActualizarProductosImports(List<tbProducto> listaPro)
        {
            try
            {
                using (Entities context = new Entities())
                {
                    foreach (var item in listaPro)
                    {
                        tbProducto pro = GetEntityById(item.idProducto);
                        if (pro != null)
                        {
                            pro.tbImpuestos = null;

                            pro.idMedida = item.idMedida;
                            pro.nombre = item.nombre;
                            pro.precio_real = item.precio_real;
                            pro.precioVenta1 = item.precioVenta1;
                            pro.precioVenta2 = item.precioVenta2;
                            pro.precioVenta3 = item.precioVenta3;
                            pro.precioVariable = item.precioVariable;
                            pro.precioUtilidad1 = item.precioUtilidad1;
                            pro.precioUtilidad2 = item.precioUtilidad2;
                            pro.precioUtilidad3 = item.precioUtilidad3;
                            pro.utilidad1Porc = item.utilidad1Porc;
                            pro.utilidad2Porc = item.utilidad2Porc;
                            pro.utilidad3Porc = item.utilidad3Porc;
                            pro.aplicaDescuento = item.aplicaDescuento;
                            pro.descuento_max = item.descuento_max;
                            pro.esExento = item.esExento;
                            pro.codigoCabys = item.codigoCabys;
                            context.Entry(pro).State = System.Data.Entity.EntityState.Modified;
                        }
                    }

                    context.SaveChanges();

                    return true;

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        /// <summary>
        /// Actualizamos el producto en la base de datos.
        /// </summary>
        /// <param name="productoNuevo"></param>
        /// <returns></returns>
        public tbProducto Actualizar(tbProducto productoNuevo)
        {

            try
            {
                bool bandera = false;

                using (Entities context = new Entities())
                {
                    productoNuevo.tbImpuestos = null;
                    productoNuevo.tbTipoMedidas = null;
                    productoNuevo.tbProveedores = null;
                    productoNuevo.tbCategoriaProducto = null;
                    context.Entry(productoNuevo).State = System.Data.Entity.EntityState.Modified;
                    context.Entry(productoNuevo.tbInventario).State = System.Data.Entity.EntityState.Modified;


                    context.SaveChanges();

                    return productoNuevo;

                }
            }
            catch (Exception ex)
            {
                throw new UpdateEntityException(ex.Message);
            }

        }




        /// <summary>
        /// Recuperamos los productos de la base de datos.
        /// </summary>
        /// <returns></returns>
        public List<tbProducto> GetListEntities(int estado)
        {
            try
            {

                using (Entities context = new Entities())
                {


                    if (estado == (int)Enums.EstadoBusqueda.Activo)
                    {
                        if (Global.busquedaProductos == 0)
                        {
                            return (from p in context.tbProducto.Include("tbProveedores").Include("tbInventario").Include("tbImpuestos").Include("tbTipoMedidas")
                                    where p.estado == true
                                    select p).ToList();
                        }
                        else
                        {
                            return (from p in context.tbProducto.Include("tbProveedores").Include("tbInventario").Include("tbImpuestos").Include("tbTipoMedidas")
                                    where p.estado == true && p.codigoActividad.Trim() == Global.actividadEconomic.CodActividad.Trim()
                                    select p).ToList();

                        }



                    }
                    else if (estado == (int)Enums.EstadoBusqueda.Inactivos)
                    {

                        return (from p in context.tbProducto.Include("tbProveedores").Include("tbInventario").Include("tbImpuestos").Include("tbTipoMedidas")
                                where p.estado == false
                                select p).ToList();


                    }
                    else
                    {

                        return (from p in context.tbProducto.Include("tbProveedores").Include("tbInventario").Include("tbImpuestos").Include("tbTipoMedidas")
                                select p).ToList();


                    }


                }


            }
            catch (Exception ex)
            {

                throw ex;
            }


        }

        public List<tbAcompanamiento> getProductoAcompa(string id)
        {
            try
            {

                using (Entities context = new Entities())
                {


                    return (from p in context.tbProducto.Include("tbAcompanamiento")
                            where p.idProducto == id
                               select p).SingleOrDefault().tbAcompanamiento.ToList();

                     
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }


        }

        public List<tbProducto> GetListEntitiesBusqueda(int estado, string valor)
        {
            try
            {

                using (Entities context = new Entities())
                {


                    if (estado == (int)Enums.EstadoBusqueda.Activo)
                    {
                        List<tbProducto> lista;
                        if (valor != null)
                        {
                            if (Global.busquedaProductos == 0)
                            {
                                lista = (from p in context.tbProducto.Include("tbTipoMedidas").Include("tbInventario")
                                        where p.nombre.Trim().ToUpper().Contains(valor.ToUpper().Trim()) ||
                                        p.idProducto.Trim().ToUpper().Contains(valor.ToUpper().Trim())
                                        select p).ToList();
                            }
                            else
                            {
                                lista = (from p in context.tbProducto.Include("tbTipoMedidas").Include("tbInventario")
                                        where p.codigoActividad.Trim() == Global.actividadEconomic.CodActividad.Trim() && p.nombre.Trim().ToUpper().Contains(valor.ToUpper().Trim()) ||
                                        p.idProducto.Trim().ToUpper().Contains(valor.ToUpper().Trim())
                                        select p).ToList();
                            }


                            return lista.Where(p => p.estado == true).ToList();

                        }



                    }
                    else if (estado == (int)Enums.EstadoBusqueda.Inactivos)
                    {
                        List<tbProducto> lista = new List<tbProducto>();
                        if (valor != null)
                        {
                            lista = (from p in context.tbProducto.Include("tbTipoMedidas").Include("tbInventario")
                                    where  p.nombre.Trim().ToUpper().Contains(valor.ToUpper().Trim()) ||
                                    p.idProducto.Trim().ToUpper().Contains(valor.ToUpper().Trim())
                                    select p).ToList();

                        }

                        return lista.Where(p => p.estado == false).ToList();

                    }
                    return null;


                }


            }
            catch (Exception ex)
            {

                throw ex;
            }


        }



        /// <summary>
        /// Recuperamos la entidad de la base de datos.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public tbProducto GetEntityActualizar(tbProducto entity)
        {
            try
            {

                using (Entities context = new Entities())
                {

                    entity = (from p in context.tbProducto.Include("tbProveedores").Include("tbInventario").Include("tbImpuestos")
                              where p.idProducto == entity.idProducto
                              select p).SingleOrDefault();

                    return entity;
                }


            }
            catch (Exception ex)
            {

                throw new Exception("Ha ocurrido el siguiente error: " + ex.Message);
            }


        }
        public tbProducto GetEntity(tbProducto entity)
        {

            return GetEntity(entity, (int)Enums.EstadoBusqueda.Todos);
        }


        /// <summary>
        /// Recuperamos la entidad de la base de datos.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public tbProducto GetEntity(tbProducto entity, int estado)
        {
            try
            {
                if (estado== (int)Enums.EstadoBusqueda.Todos)
                {
                    using (Entities context = new Entities())
                    {

                        entity = (from p in context.tbProducto.Include("tbProveedores").Include("tbInventario").Include("tbImpuestos")
                                  where p.idProducto == entity.idProducto  
                                  select p).SingleOrDefault();

                        return entity;
                    }

                }
                else if(estado == (int)Enums.EstadoBusqueda.Activo)
                {
                    using (Entities context = new Entities())
                    {

                        entity = (from p in context.tbProducto.Include("tbProveedores").Include("tbInventario").Include("tbImpuestos")
                                  where p.idProducto == entity.idProducto && p.estado == true
                                  select p).SingleOrDefault();

                        return entity;
                    }
                }
                else
                {
                    using (Entities context = new Entities())
                    {

                        entity = (from p in context.tbProducto.Include("tbProveedores").Include("tbInventario").Include("tbImpuestos")
                                  where p.idProducto == entity.idProducto && p.estado == false
                                  select p).SingleOrDefault();

                        return entity;
                    }

                }

             


            }
            catch (Exception ex)
            {

                throw new Exception("Ha ocurrido el siguiente error: " + ex.Message);
            }


        }
        public tbProducto GetEntityByNombre(tbProducto entity)
        {
            try
            {

                using (Entities context = new Entities())
                {

                    entity = (from p in context.tbProducto.Include("tbProveedores").Include("tbInventario").Include("tbImpuestos")
                              where p.nombre.Trim().ToUpper().Contains( entity.nombre.Trim().ToUpper())
                              select p).FirstOrDefault();

                    return entity;
                }


            }
            catch (Exception ex)
            {

                throw new Exception("Ha ocurrido el siguiente error: " + ex.Message);
            }


        }
      
        public tbProducto GetEntityById(string id)
        {
            try
            {

                using (Entities context = new Entities())
                {

                    return (from p in context.tbProducto.Include("tbProveedores").Include("tbInventario").
                            Include("tbImpuestos").Include("tbTipoMedidas")
                            where p.idProducto == id
                            select p).SingleOrDefault();


                }


            }
            catch (Exception ex)
            {

                throw new Exception("Ha ocurrido el siguiente error: " + ex.Message);
            }


        }

       

        public tbProducto GetEntityByIdAlone(string id)
        {
            try
            {

                using (Entities context = new Entities())
                {

                    return (from p in context.tbProducto.Include("tbAcompanamiento")
                            where p.idProducto == id
                            select p).SingleOrDefault();


                }


            }
            catch (Exception ex)
            {

                throw new Exception("Ha ocurrido el siguiente error: " + ex.Message);
            }


        }


        public List<tbProducto> getListProductoByCategoria(int id)
        {

            try
            {

                using (Entities context = new Entities())
                {


                    //Recuperamos la lista de productos segun la categoria
                    return (from p in context.tbProducto
                            where p.id_categoria == id
                            select p).ToList();

                }

            }

            catch (Exception ex)
            {
                throw ex;
            }


        }



    }
}