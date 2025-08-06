using CommonLayer;
using CommonLayer.Exceptions.DataExceptions;
using CommonLayer.Interfaces;
using EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataLayer
{
    public partial class DClientes : IDataGeneric<tbClientes>
    {// LLAMA A LAS INTERFACES Y SE CREAN LOS METODOS CON LOS MISMOS PARAMETROS QUE FUERON DECLARADOS...

        DPersona persona = new DPersona();
        DExoneraciones impExo = new DExoneraciones();
        public tbClientes GetEntity(tbClientes clientes)
        {
            tbClientes cliente;


            try
            {
                using (Entities context = new Entities())

                    cliente = (from p in context.tbClientes
                               where p.id == clientes.id
                               && p.tipoId == clientes.tipoId

                               select p).SingleOrDefault();

                return cliente;

            }
            catch (Exception)
            {

                throw new SaveEntityException("Cliente");
            }
        }



        public tbClientes Guardar(tbClientes clientes)
        {


            try
            {
                using (Entities context = new Entities())
                {

                    if (persona.GetEntity(clientes.tbPersona) != null)
                    {


                        context.Entry(clientes.tbPersona).State = System.Data.Entity.EntityState.Modified;



                    }

                    context.tbClientes.Add(clientes);
                    context.SaveChanges();

                }

            }
            catch (Exception ex)
            {
                throw new SaveEntityException("Cliente");

            }
            return clientes;
        }

        public tbClientes Actualizar(tbClientes cliente)
        {

            try
            {
                using (Entities context = new Entities())
                {
                    //SE LE AGRAGA OTRO CONTEX CON LA RELACION A LA TABLA DE LA CUAL OCUPAMOS DATOS.....
                    //context.Entry(cliente.tbExoneraciones).State = System.Data.Entity.EntityState.Detached;
                    //cliente.tbExoneraciones = null;
                    context.Entry(cliente).State = System.Data.Entity.EntityState.Modified;

                    context.Entry(cliente.tbPersona).State = System.Data.Entity.EntityState.Modified;

                    context.SaveChanges();
                    return cliente;
                }

            }
            catch (Exception EX)
            {
                throw new UpdateEntityException(" Cliente  ");


            }
        }

        public static List<tbTipoClientes> getTipoCliente()
        {
            List<tbTipoClientes> tipoCLiente;

            try
            {

                using (Entities context = new Entities())
                {

                    tipoCLiente = (from p in context.tbTipoClientes select p).ToList();


                    return tipoCLiente;
                }

            }
            catch (Exception)
            {
                throw new ListEntityException(" Cliente");

            }

        }


        public List<tbClientes> GetListClientes(int TipoCliente)
        {
            List<tbClientes> tipoCliente;
            try
            {
                Entities context = new Entities();

                tipoCliente = (from p in context.tbClientes
                               where p.estado == true
                               select p).ToList();

                return tipoCliente;

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public List<tbClientes> GetListEntities(int estado)
        {

            List<tbClientes> list = new List<tbClientes>();
            try
            {
                // SE AGREGA EL USING
                using (Entities context = new Entities())
                    if (estado == (int)Enums.EstadoBusqueda.Activo)
                    {
                        //PARA JALAR LOS DATOS DE LAS TABLAS RELACIONADAS SE USA LA PALABRA RESERVADA INCLUDE....Y ENTRE("")EL NOMBRE DE LAS TABLAS RELACIONADAS.
                        list = (from p in context.tbClientes.Include("tbPersona").Include("tbTipoClientes").Include("tbExoneraciones")
                                where p.estado == true
                                select p).ToList();




                    }
                    else if (estado == (int)Enums.EstadoBusqueda.Inactivos)
                    {
                        list = (from p in context.tbClientes.Include("tbPersona").Include("tbTipoClientes").Include("tbExoneraciones")

                                where p.estado == false
                                select p).ToList();
                    }
                    else
                    {
                        list = (from p in context.tbClientes.Include("tbPersona").Include("tbTipoClientes").Include("tbExoneraciones")
                                select p).ToList();
                    }





                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public tbClientes GetClienteById(string id, int tipo)
        {

            try
            {
                using (Entities context = new Entities())
                {
                    return (from p in context.tbClientes.Include("tbPersona").Include("tbTipoClientes").Include("tbExoneraciones")
                            where p.id == id && p.tipoId == tipo
                            select p).SingleOrDefault();

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public tbClientes GetClienteById(string id)
        {

            try
            {
                using (Entities context = new Entities())
                {
                    return (from p in context.tbClientes.Include("tbPersona").Include("tbTipoClientes").Include("tbExoneraciones")
                            where p.estado == true && p.id == id
                            select p).SingleOrDefault();

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public tbClientes GetClienteByIdPersona(int tipo, string id)
        {

            try
            {
                using (Entities context = new Entities())
                {
                    return (from p in context.tbClientes.Include("tbPersona")
                            where p.id == id.Trim() && p.tipoId == tipo
                            select p).SingleOrDefault();

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public tbClientes GetClienteById(int tipo, string id)
        {

            try
            {
                using (Entities context = new Entities())
                {
                    return (from p in context.tbClientes.Include("tbPersona").Include("tbTipoClientes").Include("tbExoneraciones")
                            where p.id == id.Trim() && p.tipoId == tipo
                            select p).SingleOrDefault();

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        //public tbPersonasTribunalS GetClienteByIdTribunal(string id)
        //{

        //    try
        //    {
        //        using (Entities context = new Entities())
        //        {
        //            return (from p in context.tbPersonasTribunalS
        //                    where p.ID == id
        //                    select p).SingleOrDefault();

        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //}
    }
}



