﻿using CommonLayer.Exceptions.DataExceptions;
using CommonLayer.Interfaces;
using EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;
namespace DataLayer
{
    public class DEliminarFactura : IDataGeneric<tbDocumento>
    {
        public tbDocumento Actualizar(tbDocumento EntidadEliminar)
        {
            try
            {


                using (Entities context = new Entities())
                {


                    context.Entry(EntidadEliminar).State = System.Data.Entity.EntityState.Modified;
                    //para la herencia se utiliza el mismo codigo ej: context.entry(cliente.tbpersona).state = 

                    context.SaveChanges();
                    return EntidadEliminar;





                }


            }
            catch (Exception ex)
            {

                throw new UpdateEntityException("Eliminar Factura");
            }


        }

        public tbDocumento GetEntity(tbDocumento entity)
        {
            throw new NotImplementedException();
        }

        public List<tbDocumento> GetListEntities(int estado)
        {
            throw new NotImplementedException();
        }

        public List<tbDocumento> GetListEntitiesFactura(int estado, DateTime fecha, string usuario)
        {
            try
            {
                //List<tbFactura> ListFactura = new List<tbFactura>();
                using (Entities context = new Entities())
                {
                    return (from u in context.tbDocumento.Include("tbClientes.tbPersona").Include("tbDetalleDocumento.tbProducto")
                            where u.estado == true
                            && u.fecha_crea >= fecha
                            && u.usuario_crea == usuario
                            select u).ToList();
                }

            }
            catch (Exception)
            {
                throw new ListEntityException("Factura");
            }
        }

        public List<tbDetalleDocumento> GetListEntitiesDetalle(int id)
        {

            using (Entities context = new Entities())
            {
                return (from u in context.tbDetalleDocumento.Include("tbProducto")
                        where u.idDoc == id
                        select u).ToList();
            }
        }
        public tbDocumento Guardar(tbDocumento entity)
        {
            throw new NotImplementedException();
        }
    }
}
