using CommonLayer.Exceptions.DataExceptions;
using CommonLayer.Interfaces;
using EntityLayer;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DataLayer
{
    public class DOrdenCompra : IDataGeneric<tbOrdenCompra>
    {
        DProveedores provIns = new DProveedores();
        public tbOrdenCompra Actualizar(tbOrdenCompra entity)
        {
            throw new NotImplementedException();
        }

        public tbOrdenCompra GetEntity(tbOrdenCompra entity)
        {
            throw new NotImplementedException();
        }

        public List<tbOrdenCompra> GetListEntities(int estado)
        {
            throw new NotImplementedException();
        }

        public List<tbOrdenCompra> GetListEntities(int estadoOrden, bool estado)
        {
            List<tbOrdenCompra> lista;
            try
            {
                using (Entities context = new Entities())
                {

                    lista = (from p in context.tbOrdenCompra.Include("tbDetalleOrdenCompra").Include("tbTipoPago")
                             where p.estado == estado && p.estadoOrden == estadoOrden
                             select p).ToList();
                    foreach (var orden in lista)
                    {
                        orden.tbProveedores = provIns.GetProveedorById(orden.tipoIdProveedor, orden.idProveedor);

                    }
                    return lista;

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public tbOrdenCompra GetentityByID(int idOrden)
        {
            try
            {
                using (Entities context = new Entities())
                {
                    tbOrdenCompra orden = (from p in context.tbOrdenCompra.Include("tbDetalleOrdenCompra.tbProducto.tbTipoMedidas").Include("tbTipoPago")
                                           where p.estado == true && p.id == idOrden
                                           select p).SingleOrDefault();

                    orden.tbProveedores = provIns.GetProveedorById(orden.tipoIdProveedor, orden.idProveedor);


                    return orden;


                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public int getNewID()
        {
            try
            {
                using (Entities context = new Entities())
                {

                    var existe = (from u in context.tbOrdenCompra
                                  orderby u.id descending
                                  select u);
                    if (existe.Count() == 0)
                    {
                        return 0;
                    }
                    else
                    {
                        var x = (from u in context.tbOrdenCompra
                                 orderby u.id descending
                                 select u).Take(1);

                        return x.First().id;

                    }


                }

            }
            catch (Exception ex)
            {

                throw;
            }
            return -1;

        }

        public tbOrdenCompra Guardar(tbOrdenCompra entity)
        {
            try
            {
                using (Entities context = new Entities())
                {

                    context.tbOrdenCompra.Add(entity);
                    context.SaveChanges();
                    return entity;
                }


            }
            catch (Exception)
            {

                throw new SaveEntityException("Error al guardar la Orden de Compra");
            }




        }
        public tbOrdenCompra Modificar(tbOrdenCompra entity)
        {
            try
            {
                entity.tbEmpresa = null;
                entity.tbProveedores = null;
                entity.tbTipoPago = null;


                using (Entities context = new Entities())
                {
                    foreach (var item in entity.tbDetalleOrdenCompra)
                    {
                        item.tbProducto = null;
                        item.tbTipoMedidas = null;
                        item.tbOrdenCompra = null;

                        item.idDetalle = item.numLinea;
                        item.idOrdenCompra = entity.id;

                        context.Entry(item).State = System.Data.Entity.EntityState.Added;
                    }

                    context.Entry<tbOrdenCompra>(entity).State = System.Data.Entity.EntityState.Modified;


                    IEnumerable<tbDetalleOrdenCompra> ordenes = (from p in context.tbDetalleOrdenCompra
                                                                 where p.idOrdenCompra == entity.id
                                                                 select p).ToList();


                    context.tbDetalleOrdenCompra.RemoveRange(ordenes);


                    context.SaveChanges();
                    return entity;
                }

            }
            catch (Exception EX)
            {
                throw new UpdateEntityException(" Empresas  ");


            }




        }


        public tbOrdenCompra Eliminar(tbOrdenCompra entity)
        {
            try
            {
                using (Entities context = new Entities())
                {

                    context.Entry<tbOrdenCompra>(entity).State = System.Data.Entity.EntityState.Modified;
                    context.SaveChanges();
                    return entity;
                }


            }
            catch (Exception)
            {

                throw new SaveEntityException("Error al guardar la Orden de Compra");
            }

        }
        public List<tbOrdenCompra> getOrderByParams(int id, string idprov, int tipoProv, int estado)
        {
            List<tbOrdenCompra> lista;
            try
            {
                using (Entities context = new Entities())
                {

                    if (id == int.MinValue && idprov == string.Empty)
                    {
                        lista = (from p in context.tbOrdenCompra.Include("tbDetalleOrdenCompra").Include("tbTipoPago")
                                 where p.estado == true && p.estadoOrden == estado
                                 select p).ToList();

                        foreach (var orden in lista)
                        {
                            orden.tbProveedores = provIns.GetProveedorById(orden.tipoIdProveedor, orden.idProveedor);

                        }
                        return lista;

                    }
                    else if (id != int.MinValue && idprov == string.Empty)
                    {
                        lista = (from p in context.tbOrdenCompra.Include("tbDetalleOrdenCompra").Include("tbTipoPago")
                                 where p.estado == true && p.estadoOrden == estado && p.id == id
                                 select p).ToList();

                        foreach (var orden in lista)
                        {
                            orden.tbProveedores = provIns.GetProveedorById(orden.tipoIdProveedor, orden.idProveedor);

                        }
                        return lista;
                    }
                    else if (id == int.MinValue && idprov != string.Empty)
                    {
                        lista = (from p in context.tbOrdenCompra.Include("tbDetalleOrdenCompra").Include("tbTipoPago")
                                 where p.estado == true && p.estadoOrden == estado && p.idProveedor == idprov
                                 && p.tipoIdProveedor == tipoProv
                                 select p).ToList();

                        foreach (var orden in lista)
                        {
                            orden.tbProveedores = provIns.GetProveedorById(orden.tipoIdProveedor, orden.idProveedor);

                        }
                        return lista;
                    }
                    else
                    {
                        lista = (from p in context.tbOrdenCompra.Include("tbDetalleOrdenCompra").Include("tbTipoPago")
                                 where p.estado == true && p.estadoOrden == estado && p.id == id && p.idProveedor == idprov
                                 && p.tipoIdEmpresa == tipoProv
                                 select p).ToList();

                        foreach (var orden in lista)
                        {
                            orden.tbProveedores = provIns.GetProveedorById(orden.tipoIdProveedor, orden.idProveedor);

                        }
                        return lista;

                    }



                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
