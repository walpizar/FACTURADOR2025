using CommonLayer.Exceptions.DataExceptions;
using CommonLayer.Interfaces;
using EntityLayer;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DataLayer
{
    public class DCajas : IDataGeneric<tbCajasMovimientos>
    {
        public tbCajasMovimientos GetEntity(tbCajasMovimientos caja)
        {
            tbCajasMovimientos cajas;

            try
            {
                using (Entities context = new Entities())
                {



                    cajas = (from u in context.tbCajasMovimientos

                             select u).SingleOrDefault();//me devuelve una sola entidad

                    return cajas;
                }

            }
            catch (Exception)
            {
                throw new EntityException("caja");
            }


        }


        public tbCajasMovimientos Guardar(tbCajasMovimientos caja)
        {

            try
            {
                using (Entities context = new Entities())
                {
                    context.tbCajasMovimientos.Add(caja);
                    context.SaveChanges();
                }
            }

            catch (Exception ex)
            {
                throw new SaveEntityException("caja");
            }



            return caja;
        }



        public tbCajasMovimientos Actualizar(tbCajasMovimientos caja)
        {
            try
            {


                using (Entities context = new Entities())
                {


                    context.Entry(caja).State = System.Data.Entity.EntityState.Modified;
                    //para la herencia se utiliza el mismo codigo ej: context.entry(cliente.tbpersona).state = 

                    context.SaveChanges();
                    return caja;





                }


            }
            catch (Exception ex)
            {

                throw new UpdateEntityException("caja");
            }


        }



        public List<tbCajasMovimientos> GetListEntities(int estado)//aqui accedo a la lista
        {

            try
            {

                List<tbCajasMovimientos> cajas = new List<tbCajasMovimientos>();

                using (Entities context = new Entities())//utilizamos el using para todas las consultas
                {



                    return (from tipIn in context.tbCajasMovimientos
                            select tipIn).ToList();


                }

            }

            catch (Exception ex)
            {

                throw new ListEntityException("cajas");
            }




        }

        public List<tbCajasMovimientos> GetListCierres(DateTime inicio, DateTime fin)//aqui accedo a la lista
        {

            try
            {
                List<tbCajasMovimientos> cajas = new List<tbCajasMovimientos>();

                using (Entities context = new Entities())//utilizamos el using para todas las consultas
                {

                    return (from p in context.tbCajasMovimientos
                            where  DbFunctions.TruncateTime(p.fechaApertura) >= inicio.Date
                            orderby p.id descending
                            select p).ToList();

                    //return (from p in context.tbCajasMovimientos
                    //        where p.fechaCierre!= null && DbFunctions.TruncateTime(p.fechaCierre) >= inicio && DbFunctions.TruncateTime(p.fechaCierre) <= fin
                    //        orderby p.id descending
                    //        select p).ToList();


                }

            }

            catch (Exception ex)
            {

                throw new ListEntityException("cajas");
            }




        }

    }
}
