using CommonLayer;
using CommonLayer.Exceptions.DataExceptions;
using CommonLayer.Interfaces;
using EntityLayer;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DataLayer
{
    public class DMovimientos : IDataGeneric<tbMovimientos>
    {
        DInventario inventarioD = new DInventario();
        public tbMovimientos GetEntity(tbMovimientos elMovimiento)
        {


            tbMovimientos movimiento;
            try
            {
                using (Entities context = new Entities())
                {
                    movimiento = (from p in context.tbMovimientos.Include("tbTipoMovimiento")
                                  where p.idMovimiento == elMovimiento.idMovimiento
                                  select p).SingleOrDefault();//singleordefault me conviete en un solo registro
                }
                return movimiento;
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }
        //public tbMovimientos Guardar(tbMovimientos movimientos)
        //{
        //    tbInventario inv;
        //    try
        //    {
        //        using (Entities context = new Entities())
        //        {

        //            if (movimientos.idTipoMov == (int)Enums.tipoMovimiento.PagoProveedor)
        //            {


        //                foreach (tbDetalleMovimiento detalle in movimientos.tbDetalleMovimiento)
        //                {
        //                    //inv = new tbInventario();
        //                    //inv.idIngrediente = detalle.idIngrediente;
        //                    //inv = inventarioD.GetEntity(inv);
        //                    //inv.cantidad = inv.cantidad + detalle.cantidad;

        //                    //inv.fecha_ult_mod = movimientos.fecha_ult_mod;
        //                    //inv.usuario_ult_mod = movimientos.usuario_ult_mod;
        //                    //context.Entry(inv).State = System.Data.Entity.EntityState.Modified;

        //                }






        //            }
        //            context.tbMovimientos.Add(movimientos);

        //            context.SaveChanges();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }


        //    return movimientos;
        //}
        public tbMovimientos Modificar(tbMovimientos modificarMov)
        {
            try
            {
                using (Entities context = new Entities())
                {
                    context.Entry(modificarMov).State = System.Data.Entity.EntityState.Modified;//mnada entidad y la modifica
                    context.SaveChanges();
                    return modificarMov;
                }
            }
            catch (Exception)
            {
                throw new UpdateEntityException();
            }

        }

        public tbCajasMovimientos verificarCierreCaja(int caja, int sucursal)
        {
            try
            {
                using (Entities context = new Entities())
                {

                    IEnumerable<tbCajasMovimientos> listaInicio = (from p in context.tbCajasMovimientos
                                                              where  p.caja == caja & p.sucursal == sucursal
                                                              orderby p.fechaApertura
                                                              select p).ToList();

                    tbCajasMovimientos mov = listaInicio.LastOrDefault();

                    return mov;



                }

            }
            catch (Exception ex)
            {
                throw new ListEntityException("Lista");
            }


        }
        public List<tbMovimientos> GetListEntities()
        {
            return GetListEntities(1);
        }    
        



        public List<tbMovimientos> getListMovByFecha(DateTime fecha, int sucursal, int caja)
        {
            //List<tbMovimientos> listaMov;
            try
            {
                using (Entities context = new Entities())
                {

                    IEnumerable<tbMovimientos> lista = (from p in context.tbMovimientos
                                                        where p.estado == true && p.sucursal == sucursal && p.caja == caja
                                                        select p).ToList();//

                    return lista.Where(x => x.fecha >= fecha).ToList();
                }

            }
            catch (Exception ex)
            {
                throw new ListEntityException("Lista");
            }

        }

        

        public List<tbCajasMovimientos> getIncioCajaByFecha(DateTime fechaInicio, DateTime fechaFin, int sucursal, int caja)
        {
            //List<tbMovimientos> listaMov;
            try
            {
                using (Entities context = new Entities())
                {
                    if (fechaFin == DateTime.MinValue)
                    {
                        return  (from p in context.tbCajasMovimientos
                                where  p.sucursal == sucursal && p.caja == caja 
                                && p.fechaApertura>= fechaInicio
                                                                 select p).AsNoTracking().ToList();//

                 
                    }
                    else
                    {
                        IEnumerable<tbCajasMovimientos> lista= (from p in context.tbCajasMovimientos
                                where  p.sucursal == sucursal && p.caja == caja 
                                select p).AsNoTracking().ToList();//
                        return lista.Where(x => x.fechaApertura >= fechaInicio && x.fechaCierre <= fechaFin).ToList();
                    }


                }

            }
            catch (Exception ex)
            {
                throw new ListEntityException("Lista");
            }

        }

        public List<tbMovimientos> getListMovByFechaAsNotTracking(DateTime fechaInicio, DateTime fechaFin, int sucursal, int caja)
        {
            //List<tbMovimientos> listaMov;
            try
            {
                using (Entities context = new Entities())
                {
                    if (fechaFin==DateTime.MinValue)
                    {
                        return  (from p in context.tbMovimientos
                                        where p.estado == true && p.sucursal == sucursal && p.caja == caja &&
                                        p.fecha >= fechaInicio 
                                        select p).AsNoTracking().ToList();//

                    }
                    else
                    {
                        return  (from p in context.tbMovimientos
                                        where p.estado == true && p.sucursal == sucursal && p.caja == caja &&
                                        p.fecha >= fechaInicio && p.fecha <= fechaFin
                                        select p).AsNoTracking().ToList();//

                    }


                }

            }
            catch (Exception ex)
            {
                throw new ListEntityException("Lista");
            }

        }



        public List<tbMovimientos> GetListEntities(int estado)
        {
            //List<tbMovimientos> listaMov;
            try
            {
                using (Entities context = new Entities())
                {
                    if (estado == 1)
                    {
                        return (from p in context.tbMovimientos
                                where p.estado == true
                                select p).ToList();//
                    }
                    else if (estado == 2)
                    {
                        return (from p in context.tbMovimientos
                                where p.estado == false
                                select p).ToList();//
                    }
                    else
                    {
                        return (from p in context.tbMovimientos
                                select p).ToList();//
                    }

                }

            }
            catch (Exception ex)
            {
                throw new ListEntityException("Lista");
            }

        }

        public tbMovimientos Actualizar(tbMovimientos entity)
        {
            throw new NotImplementedException();
        }

        public tbMovimientos ModificarLista(List<tbMovimientos> modificarMov)
        {
            try
            {
                tbMovimientos mov = new tbMovimientos();
                using (Entities context = new Entities())
                {
                    context.Entry(mov).State = System.Data.Entity.EntityState.Modified;//mnada entidad y la modifica
                    context.SaveChanges();
                    return mov;
                }
            }
            catch (Exception)
            {
                throw new UpdateEntityException();
            }

        }

        public tbMovimientos Guardar(tbMovimientos entity)
        {
            try
            {
                using (Entities context = new Entities())
                {
                    context.tbMovimientos.Add(entity);//mnada entidad y la modifica
                    context.SaveChanges();
                    return entity;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            

        }

        public tbCajasMovimientos GuardarInicioCaja(tbCajasMovimientos entity)
        {
            try
            {
                using (Entities context = new Entities())
                {
                    context.tbCajasMovimientos.Add(entity);//mnada entidad y la modifica
                    context.SaveChanges();
                    return entity;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public tbCajasMovimientos GuardarCierreCaja(tbCajasMovimientos entity)
        {
            try
            {
                using (Entities context = new Entities())
                {
                    context.Entry(entity).State = System.Data.Entity.EntityState.Modified;//mnada entidad y la modifica
                    context.SaveChanges();
                    return entity;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
    }
}
