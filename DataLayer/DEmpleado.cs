using CommonLayer;
using CommonLayer.Exceptions.DataExceptions;
using CommonLayer.Interfaces;
using EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;


namespace DataLayer
{
    public class DEmpleado : IDataGeneric<tbEmpleado>
    {

        DPersona persona = new DPersona();
        public tbEmpleado Guardar(tbEmpleado empleado)
        {
            try
            {
                using (Entities contex = new Entities())
                {
                    if (persona.GetEntity(empleado.tbPersona) != null)
                    {


                        contex.Entry(empleado.tbPersona).State = System.Data.Entity.EntityState.Modified;



                    }

                    contex.tbEmpleado.Add(empleado);
                    contex.SaveChanges();

                }
            }
            catch (Exception ex)
            {

                throw new SaveEntityException("empleado");

            }

            return empleado;

        }


        public tbEmpleado GetEntity(tbEmpleado emplead)
        {
            tbEmpleado empleado;
            try
            {
                using (Entities context = new Entities())
                {

                    empleado = (from e in context.tbEmpleado.Include("tbPersona")//desde a el el context de tabla empleado y la table de persona
                                where e.tipoId == emplead.tipoId && e.id == emplead.id //donde si e.tipoid es = emplead.id
                                select e).SingleOrDefault();// seleccione e

                    return empleado;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<tbEmpleado> GetListEntities(int estado)
        {

            try
            {
                using (Entities context = new Entities())
                {
                    List<tbEmpleado> empleado = new List<tbEmpleado>();
                    if (estado == (int)Enums.EstadoBusqueda.Activo)
                    {

                        empleado = (from e in context.tbEmpleado.AsNoTracking().Include("tbPersona").Include("tbTipoPuesto")
                                    where e.estado == true
                                    select e).ToList();

                    }

                    else if (estado == (int)Enums.EstadoBusqueda.Activo)
                    {

                        empleado = (from e in context.tbEmpleado.Include("tbPersona").Include("tbTipoPuesto")
                                    where e.estado == false
                                    select e).ToList();

                    }
                    if (estado == (int)Enums.EstadoBusqueda.Todos)
                    {

                        empleado = (from e in context.tbEmpleado.Include("tbPersona").Include("tbTipoPuesto")
                                    select e).ToList();

                    }
                    return empleado;

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public tbEmpleado Actualizar(tbEmpleado empleado)
        {
            try
            {

                using (Entities context = new Entities())
                {


                    context.Entry(empleado).State = System.Data.Entity.EntityState.Modified;
                    context.Entry(empleado.tbPersona).State = System.Data.Entity.EntityState.Modified;
                    context.SaveChanges();
                    return empleado;

                }
            }
            catch (Exception ex)
            {
                throw new UpdateEntityException("empleado");
            }
        }


    }
}
