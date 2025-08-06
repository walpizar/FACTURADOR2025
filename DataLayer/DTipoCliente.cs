using CommonLayer;
using CommonLayer.Exceptions.DataExceptions;
using CommonLayer.Interfaces;
using EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;



namespace DataLayer
{
    public class DTipoCliente : IDataGeneric<tbTipoClientes>
    {


        public static bool existTipoCliente(string nombreTipoCliente)
        {
            bool exist = false;
            try
            {
                using (Entities context = new Entities())
                {

                    tbTipoClientes tipoCliente = (from p in context.tbTipoClientes
                                                  where p.nombre == nombreTipoCliente
                                                  select p).SingleOrDefault();
                    if (tipoCliente != null)
                    {
                        exist = true;
                    }

                }

            }
            catch (Exception exP)
            {
                throw exP;
            }


            return exist;




        }




        public tbTipoClientes GetEntity(tbTipoClientes Eltipo)
        {
            tbTipoClientes tipoCliente;
            try
            {

                using (Entities context = new Entities())
                {
                    tipoCliente = (from p in context.tbTipoClientes
                                   where p.nombre == Eltipo.nombre
                                   select p).SingleOrDefault();
                }






                return tipoCliente;


            }
            catch (Exception ex)
            {

                throw new EntityException(ex.Message);
            }

        }





        public
            tbTipoClientes Guardar(tbTipoClientes tipoCliente)
        {
            try
            {
                using (Entities context = new Entities())
                {
                    context.tbTipoClientes.Add(tipoCliente);
                    context.SaveChanges();
                }






            }
            catch (Exception ex)
            {
                throw new SaveEntityException(ex.Message);
            }


            return tipoCliente;

        }


        public tbTipoClientes Actualizar(tbTipoClientes tipoCliente)
        {
            try
            {
                using (Entities context = new Entities())
                {
                    context.Entry(tipoCliente).State = System.Data.Entity.EntityState.Modified;

                    //si tuviera que llamar  a personao otra entidad debo combiarle el estado a mi entidad como a la otra entidadcomo en la linea anterior
                    context.SaveChanges();
                    return tipoCliente;
                }






            }
            catch
            {

                throw new UpdateEntityException();
            }


        }

        public List<tbTipoClientes> GetListEntities()
        {

            return GetListEntities(3);

        }


        public List<tbTipoClientes> GetListEntities(int estado)
        {

            try
            {

                using (Entities context = new Entities())
                {
                    List<tbTipoClientes> tipoCliente = new List<tbTipoClientes>();
                    if (estado == (int)Enums.EstadoBusqueda.Activo)
                    {
                        tipoCliente = (from p in context.tbTipoClientes
                                       where p.estado == true
                                       select p).ToList();

                    }


                    else if (estado == (int)Enums.EstadoBusqueda.Activo)
                    {
                        tipoCliente = (from p in context.tbTipoClientes
                                       where p.estado == true
                                       select p).ToList();
                    }





                    else if (estado == (int)Enums.EstadoBusqueda.Inactivos)
                    {

                        tipoCliente = (from p in context.tbTipoClientes
                                       where p.estado == false
                                       select p).ToList();

                    }
                    else if (estado == (int)Enums.EstadoBusqueda.Todos)
                    {




                        tipoCliente = (from p in context.tbTipoClientes
                                       select p).ToList();
                    }

                    return tipoCliente;


                }
            }
            catch (Exception ex)
            {

                throw new ListEntityException(ex.Message);
            }



        }


    }

}




