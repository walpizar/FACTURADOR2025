using CommonLayer;
using CommonLayer.Exceptions.DataExceptions;
using CommonLayer.Interfaces;
using CommonLayer.Logs;
using EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

namespace DataLayer
{
    public class DCompras : IDataGeneric<tbCompras>
    {

        DProductos productoIns = new DProductos();
        public int getNewID(int tipoDoc)
        {
            try
            {
                using (Entities context = new Entities())
                {

                    var existe = (from u in context.tbCompras
                                  where u.tipoDoc == tipoDoc
                                  orderby u.id descending
                                  select u);
                    if (existe.Count() == 0)
                    {
                        return 0;
                    }
                    else
                    {
                        var x = (from u in context.tbCompras
                                 where u.tipoDoc == tipoDoc
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

        public tbCompras Actualizar(tbCompras entity)
        {
            try
            {
                using (Entities context = new Entities())
                {
                    //SE LE AGRAGA OTRO CONTEX CON LA RELACION A LA TABLA DE LA CUAL OCUPAMOS DATOS.....

                    context.Entry(entity).State = System.Data.Entity.EntityState.Modified;

                    foreach (var linea in entity.tbDetalleCompras)
                    {
                        if (linea.actualizaInvent)
                        {
                            tbProducto pro = productoIns.GetEntityById(linea.idProducto);
                            pro.tbProveedores = null;
                            pro.tbImpuestos = null;
                            if (entity.tipoDoc == (int)Enums.TipoDocumento.NotaCredito)
                            {
                                pro.tbInventario.cantidad += (decimal)linea.cantidadVenta;
                            }
                            else
                            {
                                pro.tbInventario.cantidad -= (decimal)linea.cantidadVenta;
                            }

                            context.Entry(pro.tbInventario).State = System.Data.Entity.EntityState.Modified;
                        }

                    }


                    context.SaveChanges();
                    return entity;
                }

            }
            catch (Exception EX)
            {
                throw new UpdateEntityException(" Compra ");


            }
        }

        public tbCompras GetEntity(tbCompras entity)
        {
            throw new NotImplementedException();
        }
        public tbCompras GetEntityComprasByClave(string clave)
        {
            try
            {
                using (Entities context = new Entities())
                {

                    return (from p in context.tbCompras.Include("tbDetalleCompras")
                            where p.claveEmisor.Trim() == clave.Trim()
                            select p).SingleOrDefault();

                }



            }
            catch (Exception ex)
            {
                clsEvento evento = new clsEvento(ex.Message, "1");
                throw ex;
            }
            return null;
        }

        public List<tbCompras> GetListEntities(int estado)
        {
            try
            {
                using (Entities context = new Entities())
                {

                    var list = (from p in context.tbCompras.Include("tbDetalleCompras").Include("tbTipoPago").Include("tbTipoVenta")
                                where p.estado == true
                                select p).ToList();

                    return list;



                }



            }
            catch (Exception ex)
            {
                clsEvento evento = new clsEvento(ex.Message, "1");
                throw ex;
            }
            return null;
        }
  
        /// <summary>
        /// Obtiene las compras (facturas recibidas) que todavía requieren
        /// atención: las que no se han confirmado ante Hacienda
        /// (reporteAceptaHacienda = false/null), Y también las que ya se
        /// enviaron pero todavía no tienen respuesta final de Hacienda
        /// (EstadoFacturaHacienda = null) — esas siguen "pendientes" hasta
        /// que se conozca si Hacienda las aceptó o rechazó.
        /// </summary>
        public List<tbCompras> GetPendientesConfirmar()
        {
            using (var db = new Entities())
            {
                return db.tbCompras
                    .Include(c => c.tbDetalleCompras)
                    .Where(c => c.reporteAceptaHacienda == false
                             || c.reporteAceptaHacienda == null
                             || c.EstadoFacturaHacienda == null)
                    .OrderBy(c => c.fechaCompra)
                    .ToList();
            }
        }
        public tbCompras Guardar(tbCompras entity)
        {
            try
            {

                using (Entities context = new Entities())
                {


                    context.tbCompras.Add(entity);


                    context.SaveChanges();

                }
                return entity;
            }
            catch (Exception ex)
            {

                throw new SaveEntityException("Error en Factura");
            }
        }
    }
}
