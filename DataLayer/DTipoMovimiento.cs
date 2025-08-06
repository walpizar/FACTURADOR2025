﻿using CommonLayer.Exceptions.DataExceptions;
using CommonLayer.Interfaces;
using EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataLayer
{
    public class DTipoMovimiento : IDataGeneric<tbTipoMovimiento>
    {

        public List<tbTipoMovimiento> GetListEntities(int estado)
        {
            //List<tbTipoMovimiento> listaTipMov;
            try
            {
                using (Entities context = new Entities())
                {
                    if (estado == 1)
                    {
                        return (from p in context.tbTipoMovimiento
                                where p.estado == true
                                select p).ToList();//
                    }
                    if (estado == 2)
                    {
                        return (from p in context.tbTipoMovimiento
                                where p.estado == false
                                select p).ToList();//
                    }
                    else
                    {
                        return (from p in context.tbTipoMovimiento
                                select p).ToList();//
                    }

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<tbTipoMovimiento> GetListEntities()
        {

            return GetListEntities(3);

        }

        public tbTipoMovimiento Guardar(tbTipoMovimiento movimiento)
        {
            try
            {
                using (Entities context = new Entities())
                {
                    context.tbTipoMovimiento.Add(movimiento);
                    context.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw new SaveEntityException("Tipo de Movimiento");
            }
            return movimiento;
        }
        public tbTipoMovimiento Actualizar(tbTipoMovimiento movimiento)
        {
            try
            {
                using (Entities context = new Entities())
                {
                    context.Entry(movimiento).State = System.Data.Entity.EntityState.Modified;
                    //se hay que modificar otra entidad se agrega la line ad earriba con el nombre de 
                    context.SaveChanges();
                    return movimiento;
                }
            }
            catch (Exception ex)
            {

                throw new UpdateEntityException("Actualizar");
            }
        }
        public tbTipoMovimiento GetEntity(tbTipoMovimiento tmovimiento)
        {
            tbTipoMovimiento movimiento;
            try
            {
                using (Entities context = new Entities())
                {
                    movimiento = (from p in context.tbTipoMovimiento
                                  where p.idTipo == tmovimiento.idTipo
                                  select p).SingleOrDefault();

                }
                return movimiento;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
    }
}
