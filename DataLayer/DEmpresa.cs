using CommonLayer.Exceptions.DataExceptions;
using CommonLayer.Interfaces;
using EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataLayer
{
    public class DEmpresa : IDataGeneric<tbEmpresa>
    {
        public tbEmpresa Actualizar(tbEmpresa entity)
        {
            try
            {
                using (Entities context = new Entities())
                {
                    foreach (var item in entity.tbEmpresaActividades)
                    {
                        item.tbActividades = null;
                        item.tbEmpresa = null;

                        context.Entry(item).State = System.Data.Entity.EntityState.Added;
                    }

                    context.Entry(entity).State = System.Data.Entity.EntityState.Modified;


                    IEnumerable<tbEmpresaActividades> actividades = (from p in context.tbEmpresaActividades
                                                                     where p.idEmpresa == entity.id && p.tipoId == entity.tipoId
                                                                     select p).ToList();


                    context.tbEmpresaActividades.RemoveRange(actividades);


                    context.SaveChanges();
                    return entity;
                }

            }
            catch (Exception EX)
            {
                throw new UpdateEntityException(" Empresas  ");


            }
        }

        public tbEmpresa ActualizarEmpresa(tbEmpresa entity)
        {
            try
            {
                using (Entities context = new Entities())
                {

                    context.Entry(entity).State = System.Data.Entity.EntityState.Modified;
                    context.SaveChanges();
                    return entity;
                }

            }
            catch (Exception EX)
            {
                throw new UpdateEntityException(" Empresas  ");


            }
        }



        public tbParametrosEmpresa ActualizarParametros(tbParametrosEmpresa entity)
        {
            try
            {
                using (Entities context = new Entities())
                {


                    context.Entry(entity).State = System.Data.Entity.EntityState.Modified;


                    context.SaveChanges();
                    return entity;
                }

            }
            catch (Exception EX)
            {
                throw new UpdateEntityException(" Parámetros Empresa  ");


            }
        }

        public tbEmpresa GetEntity(tbEmpresa entity)
        {

            try
            {
                using (Entities context = new Entities())

                    entity = (from p in context.tbEmpresa.Include("tbPersona").Include("tbParametrosEmpresa").Include("tbEmpresaActividades.tbActividades")
                              where p.id == entity.id
                              && p.tipoId == entity.tipoId

                              select p).SingleOrDefault();

                return entity;

            }
            catch (Exception ex)
            {

                throw new SaveEntityException("Empresa");
            }

        }
        public tbEmpresa getEntityForPrint(tbEmpresa entity)
        {

            try
            {
                using (Entities context = new Entities())

                    entity = (from p in context.tbEmpresa.Include("tbPersona.tbBarrios.tbDistrito.tbCanton.tbProvincia")
                              where p.id == entity.id
                              && p.tipoId == entity.tipoId

                              select p).SingleOrDefault();

                return entity;

            }
            catch (Exception)
            {

                throw new SaveEntityException("Empresa");
            }

        }

        public tbParametrosEmpresa GetEntityParametros(tbParametrosEmpresa entity)
        {
            try
            {
                using (Entities context = new Entities())

                    entity = (from p in context.tbParametrosEmpresa
                              where p.idEmpresa == entity.idEmpresa
                              && p.idTipoEmpresa == entity.idTipoEmpresa

                              select p).SingleOrDefault();

                return entity;

            }
            catch (Exception ex)
            {

                throw new SaveEntityException("Parámetros Empresa");
            }

        }

        public List<tbEmpresa> GetListEntities(int estado)
        {
            throw new NotImplementedException();
        }

        public tbEmpresa Guardar(tbEmpresa entity)
        {
            try
            {
                using (Entities context = new Entities())
                {



                    context.tbEmpresa.Add(entity);
                    context.SaveChanges();

                }

            }
            catch (Exception ex)
            {
                throw new SaveEntityException("Empresa");

            }
            return entity;

        }

        public tbParametrosEmpresa GuardarParametros(tbParametrosEmpresa entity)
        {
            try
            {
                using (Entities context = new Entities())
                {



                    context.tbParametrosEmpresa.Add(entity);
                    context.SaveChanges();

                }

            }
            catch (Exception ex)
            {
                throw new SaveEntityException("Empresa");

            }
            return entity;

        }
    }
}
