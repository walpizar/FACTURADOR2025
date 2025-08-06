using CommonLayer;
using CommonLayer.DTO;
using CommonLayer.Exceptions.DataExceptions;
using CommonLayer.Interfaces;
using CommonLayer.Logs;
using EntityLayer;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace DataLayer
{
    public class DFacturacion : IDataGeneric<tbDocumento>
    {
        DProductos productoIns = new DProductos();
        DCategoriaProducto cateIns = new DCategoriaProducto();
        DInventario inventarioIns = new DInventario();
        DClientes clienteIns = new DClientes();
        DProveedores proveedorIns = new DProveedores();
        DEliminarFactura detalleFacturaIns = new DEliminarFactura();
        DExoneraciones exoIns = new DExoneraciones();
        Dimpuestos impuestoIns = new Dimpuestos();
        DTipoMedida medidaIns = new DTipoMedida();
        public int getNewID(int tipoDoc)
        {
            try
            {
                using (Entities context = new Entities())
                {

                    var existe = (from u in context.tbDocumento
                                  where u.tipoDocumento == tipoDoc
                                  orderby u.id descending
                                  select u);
                    if (existe.Count() == 0)
                    {
                        return 0;
                    }
                    else
                    {
                        var x = (from u in context.tbDocumento
                                 where u.tipoDocumento == tipoDoc
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
        public int getNewIDCompra(int tipoDoc)
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
        public int getNewIDMensaje()
        {
            try
            {
                using (Entities context = new Entities())
                {

                    var existe = (from u in context.tbReporteHacienda
                                  orderby u.id descending
                                  select u);
                    if (existe.Count() == 0)
                    {
                        return 0;
                    }
                    else
                    {
                        var x = (from u in context.tbReporteHacienda
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

        public bool abonos(IEnumerable<tbDocumento> listaFact)
        {
            List<tbPagos> listaAbonos = new List<tbPagos>();
            try
            {

                using (Entities context = new Entities())
                {
                    foreach (var doc in listaFact)
                    {

                        foreach (var abono in doc.tbPagos)
                        {
                            if (abono.idAbono == 0)
                            {
                                context.Entry(abono).State = System.Data.Entity.EntityState.Added;


                            }
                            else
                            {
                                context.Entry(abono).State = System.Data.Entity.EntityState.Detached;

                            }
                        }
                    }

                    foreach (var doc in listaFact)
                    {
                        context.Entry(doc).State = System.Data.Entity.EntityState.Modified;

                        context.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new UpdateEntityException("facturacion actualizacion. Electronica");
            }

            return true;
        }


        public tbDocumento Actualizar(tbDocumento entity)
        {

            try
            {

                using (Entities context = new Entities())
                {
                    if(entity.tbDetalleDocumento != null)
                    {
                        foreach (var detalle in entity.tbDetalleDocumento)
                        {
                            detalle.tbProducto = null;

                        }

                    }
                 

                    context.Entry(entity).State = System.Data.Entity.EntityState.Modified;

                    context.SaveChanges();
                    return entity;

                }
            }
            catch (Exception ex)
            {
                throw new UpdateEntityException("facturacion actualizacion. Electronica");
            }



        }


        public tbCompras ActualizarCompraSimplificada(tbCompras entity)
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
            catch (Exception ex)
            {
                throw new UpdateEntityException("facturacion actualizacion. Electronica");
            }



        }
        //private bool actualizarInventario(string idProducto, decimal cantidad)
        //{
        //    List<tbDetalleProducto> ingredientesLista = detalleProductoIns.GetListEntitiesByIdProduct(idProducto);
        //    tbInventario inventario;
        //    foreach(tbDetalleProducto detalle in ingredientesLista)
        //    {

        //        inventario = inventarioIns.GetEntityByIngrediente(detalle.idIngrediente);
        //        if(inventario !=null)
        //        {

        //           // inventario.cantidad = Math.Round(inventario.cantidad - (detalle.cantidad * cantidad), 2);


        //        }


        //    }



        //    return true;
        //}

        public tbDocumento getByConsecutivo(string consecutivo)
        {

            try
            {
                using (Entities context = new Entities())
                {

                    return (from p in context.tbDocumento.Include("tbClientes.tbPersona").Include("tbDetalleDocumento.tbProducto").Include("tbDetalleDocumento.tbProducto.tbImpuestos").Include("tbTipoMoneda")
                            where p.consecutivo == consecutivo
                            select p).FirstOrDefault();

                }

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public tbDocumento getById(int id, int tipo)
        {
            try
            {
                using (Entities context = new Entities())
                {

                    var doc = (from p in context.tbDocumento.Include("tbPagos").Include("tbDetalleDocumento.tbProducto.tbImpuestos").Include("tbTipoMoneda")
                               where p.id == id && p.tipoDocumento == tipo
                               select p).FirstOrDefault();
                    if (doc.idCliente != null)
                    {
                        doc.tbClientes = clienteIns.GetClienteById(doc.idCliente, (int)doc.tipoIdCliente);
                    }
                    return doc;
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }


        }

        public tbCompras getComprasByConsecutivo(string clave)
        {
            try
            {
                using (Entities context = new Entities())
                {

                    return (from p in context.tbCompras
                            where p.consecutivoEmisor == clave
                            && p.estado == true
                            select p).FirstOrDefault();

                }

            }
            catch (Exception ex)
            {

                throw ex;
            }


        }
        public tbCompras getByClave(string clave)
        {
            try
            {
                using (Entities context = new Entities())
                {

                    return (from p in context.tbCompras
                            where p.claveEmisor == clave && p.estado == true
                            select p).FirstOrDefault();

                }

            }
            catch (Exception ex)
            {

                throw ex;
            }


        }
        public tbCompras getByNumFact(string factura, int tipoId, string id)
        {
            try
            {
                using (Entities context = new Entities())
                {

                    var doc = (from p in context.tbCompras
                               where p.numFactura == factura.Trim() && p.tipoIdProveedor == tipoId && p.idProveedor == id && p.estado == true &&
                               p.estado == true
                               select p).FirstOrDefault();

                    return doc;
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }


        }

        public List<tbDetalleDocumento> getEntityDetails(tbDocumento entity)
        {
            try
            {
                using (Entities context = new Entities())
                {
                    return (from p in context.tbDetalleDocumento.Include("tbProducto")
                            where p.idDoc == entity.id && p.idTipoDoc == entity.tipoDocumento
                            select p).ToList();
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        

        public tbDocumento getEntityByKey(int id, int tipoDocumento)
        {
            try
            {
                using (Entities context = new Entities())
                {

                        var doc = (from p in context.tbDocumento.Include("tbDetalleDocumento.tbProducto")
                                   where p.id == id && p.tipoDocumento == tipoDocumento
                                   select p).FirstOrDefault();

                    if (doc.idCliente != null)
                    {
                        doc.tbClientes = clienteIns.GetClienteById((int)doc.tipoIdCliente, doc.idCliente);

                    }
                    foreach (var item in doc.tbDetalleDocumento)
                    {

                        item.tbProducto.tbCategoriaProducto = cateIns.GetEntityById(item.tbProducto.id_categoria);
                        //  item.tbProducto.tbTipoMedidas = medidaIns.GetEnityById(item.tbProducto.idMedida);
                    }
                    return doc;

                   
                    
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public tbDocumento GetEntity(tbDocumento entity)
        {
            return GetEntity(entity, true);
        }
        public tbDocumento GetEntity(tbDocumento entity, bool cargaEntAnexas)
        {
            try
            {
                using (Entities context = new Entities())
                {

                    if (cargaEntAnexas)
                    {
                        var doc = (from p in context.tbDocumento.Include("tbDetalleDocumento.tbProducto.tbImpuestos").Include("tbDetalleDocumento.tbProducto.tbTipoMedidas").Include("tbPagos")
                                   where p.id == entity.id && p.tipoDocumento == entity.tipoDocumento
                                   select p).FirstOrDefault();

                        if (doc.idCliente != null)
                        {
                            doc.tbClientes = clienteIns.GetClienteById((int)doc.tipoIdCliente, doc.idCliente);

                        }
                        foreach (var item in doc.tbDetalleDocumento)
                        {

                            item.tbProducto.tbCategoriaProducto = cateIns.GetEntityById(item.tbProducto.id_categoria);
                            //  item.tbProducto.tbTipoMedidas = medidaIns.GetEnityById(item.tbProducto.idMedida);
                        }
                        return doc;

                    }
                    else
                    {
                        var doc = (from p in context.tbDocumento.Include("tbClientes").Include("tbDetalleDocumento.tbProducto").Include("tbPagos")
                                   where p.id == entity.id && p.tipoDocumento == entity.tipoDocumento
                                   select p).FirstOrDefault();

                        if (doc != null)
                        {
                            if (doc.idCliente != null)
                            {
                                doc.tbClientes = clienteIns.GetClienteById((int)doc.tipoIdCliente, doc.idCliente);
                                if (doc.tbClientes.idExonercion != null)
                                {
                                    doc.tbClientes.tbExoneraciones = exoIns.GetEntity((int)doc.tbClientes.idExonercion);
                                }
                            }
                        }
                        else
                        {
                            return null;
                        }



                        return doc;

                    }
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public tbDocumento GetLastEntity(string nombreUsuario)
        {
            try
            {
                using (Entities context = new Entities())
                {

                    var last = (from p in context.tbDocumento.Include("tbDetalleDocumento.tbProducto.tbImpuestos").Include("tbPagos")
                                where p.estado == true && (p.tipoDocumento == (int)Enums.TipoDocumento.Factura || p.tipoDocumento == (int)Enums.TipoDocumento.FacturaElectronica || p.tipoDocumento == (int)Enums.TipoDocumento.TiqueteElectronico)
                                && p.usuario_crea.Trim().ToUpper() == nombreUsuario
                                select p).ToList().LastOrDefault();


                    if (last.idCliente != null)
                    {
                        last.tbClientes = clienteIns.GetClienteById((int)last.tipoIdCliente, last.idCliente);

                    }
                    foreach (var item in last.tbDetalleDocumento)
                    {

                        item.tbProducto.tbCategoriaProducto = cateIns.GetEntityById(item.tbProducto.id_categoria);
                        item.tbProducto.tbTipoMedidas = medidaIns.GetEnityById(item.tbProducto.idMedida);
                    }
                    return last;

                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public tbCompras GetEntityCompra(tbCompras entity)
        {
            try
            {
                using (Entities context = new Entities())
                {


                    var doc = (from p in context.tbCompras.Include("tbDetalleCompras").Include("tbTipoPago").Include("tbTipoVenta")
                               where p.id == entity.id && p.tipoDoc == entity.tipoDoc
                               select p).FirstOrDefault();

                    //if (doc.idProveedor != null)
                    //{
                    //    doc.tbProveedores = proveedorIns.GetProveedorById((int)doc.tipoIdProveedor, doc.idProveedor);

                    //}
                    return doc;


                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public tbDocumento GetEntityByClave(tbDocumento entity)
        {
            try
            {
                using (Entities context = new Entities())
                {
                    var doc = (from p in context.tbDocumento.Include("tbClientes").Include("tbDetalleDocumento.tbProducto").Include("tbDetalleDocumento.tbProducto.tbImpuestos")
                               where p.clave == entity.clave
                               select p).FirstOrDefault();
                    if (doc.idCliente != null)
                    {
                        doc.tbClientes = clienteIns.GetClienteById((int)doc.tipoIdCliente, doc.idCliente);

                    }

                    return doc;

                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public tbDocumento GetEntityByClave(string clave)
        {
            try
            {
                using (Entities context = new Entities())
                {
                    var doc = (from p in context.tbDocumento.Include("tbClientes").Include("tbDetalleDocumento.tbProducto").Include("tbDetalleDocumento.tbProducto.tbImpuestos")
                               where p.clave == clave
                               select p).FirstOrDefault();
                    if (doc.idCliente != null)
                    {
                        doc.tbClientes = clienteIns.GetClienteById((int)doc.tipoIdCliente, doc.idCliente);

                    }
                    return doc;
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public decimal validarCredito(string idCliente, int? tipoIdCliente)
        {
            using (var context = new Entities())
            {
                var idParam = new SqlParameter("@id", idCliente);
                var tipoParam = new SqlParameter("@tipo", tipoIdCliente);

                var credito = context.Database.SqlQuery<decimal>(
                    "EXEC sp_validarCreditoCliente @id, @tipo", idParam, tipoParam).FirstOrDefault();

                return credito;
            }
        }

        public IEnumerable<tbDocumento> getListFacturasAceptadasHacienda()
        {
            try
            {
                using (Entities context = new Entities())
                {
                    var list = (from p in context.tbDocumento.Include("tbDetalleDocumento").Include("tbClientes").Include("tbDetalleDocumento.tbProducto")
                                where (p.reporteAceptaHacienda == true && p.mensajeReporteHacienda.Contains("Accepted") && p.EstadoFacturaHacienda.Contains("aceptado") || p.EstadoFacturaHacienda.Contains("rechazado"))
                                select p).ToList();

                    foreach (var item in list)
                    {
                        if (item.idCliente != null)
                        {
                            item.tbClientes = clienteIns.GetClienteById((int)item.tipoIdCliente, item.idCliente);

                        }

                    }
                    return list;

                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public IEnumerable<tbDocumento> getListAllDocumentosBySearch()
        {
            try
            {
                using (Entities context = new Entities())
                {
                    var list = (from p in context.tbDocumento.Include("tbClientes.tbPersona")
                                orderby p.fecha descending
                                select p).ToList();

                    return list;
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }


        }
        public async Task<IEnumerable<tbDocumento>> getListAllDocumentosDateBySearchAsyc(DateTime fechaInicio, DateTime fechaFin)
        {
            try
            {
                fechaFin = fechaFin.AddDays(1);
                using (Entities context = new Entities())
                {
                    /* return await  context.tbDocumento.SqlQuery("select * from tbDocumento inner " +
                         "join tbClientes on tbDocumento.idCliente = tbClientes.id and tbDocumento.tipoIdCliente = tbClientes.tipoId " +
                         "inner join tbPersona on tbClientes.id = tbPersona.identificacion and tbClientes.tipoId = tbPersona.tipoId " +
                         "order by tbDocumento.fecha desc").ToListAsync();*/


                    return await (from p in context.tbDocumento.Include("tbClientes.tbPersona")                               
                                  where DbFunctions.TruncateTime(p.fecha) >= fechaInicio && DbFunctions.TruncateTime(p.fecha) <= fechaFin
                                  orderby p.fecha descending
                                  select p).AsNoTracking().ToListAsync();
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }


        }
        public async Task<IEnumerable<tbDocumento>> getListAllDocumentosBySearchAsyc()
        {
            try
            {
                using (Entities context = new Entities())
                {
                    /* return await  context.tbDocumento.SqlQuery("select * from tbDocumento inner " +
                         "join tbClientes on tbDocumento.idCliente = tbClientes.id and tbDocumento.tipoIdCliente = tbClientes.tipoId " +
                         "inner join tbPersona on tbClientes.id = tbPersona.identificacion and tbClientes.tipoId = tbPersona.tipoId " +
                         "order by tbDocumento.fecha desc").ToListAsync();*/

                
                   return await (from p in context.tbDocumento.Include("tbClientes.tbPersona")
                           orderby p.fecha descending
                           select p).AsNoTracking().ToListAsync();
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }


        }
        public IEnumerable<tbDocumento> getListAllDocumentos()
        {
            try
            {
                using (Entities context = new Entities())
                {
                    var list = (from p in context.tbDocumento.Include("tbDetalleDocumento").Include("tbDetalleDocumento.tbProducto.tbImpuestos").Include("tbPagos")
                                select p).ToList();

                    foreach (var doc in list)
                    {
                        if (doc.idCliente != null)
                        {
                            doc.tbClientes = clienteIns.GetClienteById((int)doc.tipoIdCliente, doc.idCliente);
                        }
                    }
                    return list;
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }


        }

        public tbDocumento getProformasById(int id, int tipo)
        {
            try
            {
                tbDocumento doc;
                using (Entities context = new Entities())
                {
                    doc = (from p in context.tbDocumento.Include("tbDetalleDocumento").Include("tbDetalleDocumento.tbProducto.tbImpuestos")
                           where p.estado == true && p.tipoDocumento == tipo && p.id == id
                           select p).SingleOrDefault();

                    if (doc != null)
                    {
                        if (doc.idCliente != null)
                        {
                            doc.tbClientes = clienteIns.GetClienteById((int)doc.tipoIdCliente, doc.idCliente);
                        }
                    }


                    return doc;
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }


        }
        public IEnumerable<tbDocumento> getListProformas()
        {
            try
            {

                using (Entities context = new Entities())
                {
                    var list = (from p in context.tbDocumento.Include("tbDetalleDocumento").Include("tbDetalleDocumento.tbProducto.tbImpuestos")
                                where p.estado == true && p.tipoDocumento == (int)Enums.TipoDocumento.Proforma
                                select p).ToList();
                    list = list.Where(x => x.fecha.AddDays((int)x.plazo).Date >= DateTime.Now.Date).ToList();
                    foreach (var doc in list)
                    {
                        if (doc.idCliente != null)
                        {
                            doc.tbClientes = clienteIns.GetClienteById((int)doc.tipoIdCliente, doc.idCliente);
                        }
                    }
                    return list;
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }


        }



        public IEnumerable<tbDocumento> getListProformasGenerales()
        {
            try
            {

                using (Entities context = new Entities())
                {
                    var list = (from p in context.tbDocumento.Include("tbDetalleDocumento").Include("tbDetalleDocumento.tbProducto.tbImpuestos")
                                where p.estado == true && p.tipoDocumento == (int)Enums.TipoDocumento.ProformaGeneral
                                select p).ToList();
                    foreach (var doc in list)
                    {
                        if (doc.idCliente != null)
                        {
                            doc.tbClientes = clienteIns.GetClienteById((int)doc.tipoIdCliente, doc.idCliente);
                        }
                    }
                    return list;
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }


        }
        public IEnumerable<tbPagos> getAbonosByFecha(DateTime fecha, int sucursal, int caja)
        {
            try
            {
                using (Entities context = new Entities())
                {
                    var list = (from p in context.tbPagos.Include("tbDocumento")
                                where p.sucursal == sucursal && p.caja == caja
                                select p).ToList();

                    return list.Where(x => x.estado == true && x.fecha_crea >= fecha );
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }


        }



        public IEnumerable<tbPagos> getAbonosByFecha(DateTime fecha, int tipoPago, int sucursal, int caja)
        {
            try
            {
                using (Entities context = new Entities())
                {
                    var list = (from p in context.tbPagos.Include("tbDocumento")
                                where p.sucursal == sucursal && p.caja == caja
                                select p).ToList();

                    return list.Where(x => x.estado == true && x.fecha_crea  >= fecha && x.tipoPago == tipoPago);
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }


        }

        public IEnumerable<tbPagos> getAbonosByFechaAsNotTracking(DateTime fechaInicio, DateTime fechaFin, int sucursal, int caja)
        {
            try
            {
                using (Entities context = new Entities())
                {
                    if (fechaFin == DateTime.MinValue)
                    {
                        return (from p in context.tbPagos.Include("tbDocumento")
                                where p.sucursal == sucursal && p.caja == caja &&
                                p.fecha_crea >= fechaInicio 
                                select p).AsNoTracking().ToList();

                    }
                    else
                    {
                        return (from p in context.tbPagos.Include("tbDocumento")
                                where p.sucursal == sucursal && p.caja == caja &&
                                p.fecha_crea >= fechaInicio && p.fecha_crea <= fechaFin
                                select p).AsNoTracking().ToList();

                    }
              

                    
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }


        }
        


        public IEnumerable<tbDocumento> getNCByFechaAsNotTracking(DateTime fecha, int sucursal, int caja)
        {
            try
            {
                using (Entities context = new Entities())
                {
                    var list = (from p in context.tbDocumento.Include("tbDetalleDocumento")
                                where p.sucursal == sucursal && p.caja == caja && p.estado == true
                                select p).AsNoTracking().ToList();

                    return list.Where(x => (x.tipoDocumento == (int)Enums.TipoDocumento.NotaCredito || x.tipoDocumento == (int)Enums.TipoDocumento.NotaCreditoElectronica
                   ) && x.fecha >= fecha);
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }


        }
        public IEnumerable<tbDocumento> getNCByFecha(DateTime fecha, int sucursal, int caja)
        {
            try
            {
                using (Entities context = new Entities())
                {
                    var list = (from p in context.tbDocumento.Include("tbDetalleDocumento")
                                where p.sucursal == sucursal && p.caja == caja
                                select p).ToList();

                    return list.Where(x => x.estado == true && (x.tipoDocumento == (int)Enums.TipoDocumento.NotaCredito || x.tipoDocumento == (int)Enums.TipoDocumento.NotaCreditoElectronica
                   ) && x.fecha >= fecha);
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }


        }
        public IEnumerable<tbDocumento> getNCByRef(string referen)
        {
            try
            {
                using (Entities context = new Entities())
                {
                    var list = (from p in context.tbDocumento.Include("tbDetalleDocumento")
                                where p.estado == true && p.tipoDocumento == (int)Enums.TipoDocumento.NotaCredito || p.tipoDocumento == (int)Enums.TipoDocumento.NotaCreditoElectronica

                                select p).ToList();

                    return list.Where(x => x.claveRef.Trim() == referen.Trim()).ToList();
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }


        }
        public IEnumerable<tbDocumento> getVentasByFecha(DateTime fecha, int sucursal, int caja)
        {
            try
            {
                using (Entities context = new Entities())
                {
                    var list = (from p in context.tbDocumento.Include("tbDetalleDocumento").Include("tbPagos")
                                where p.sucursal == sucursal && p.caja == caja
                                select p).ToList();

                    return list.Where(x => x.estado == true && (x.tipoDocumento == (int)Enums.TipoDocumento.Factura || x.tipoDocumento == (int)Enums.TipoDocumento.FacturaElectronica
                    || x.tipoDocumento == (int)Enums.TipoDocumento.TiqueteElectronico) && x.fecha >= fecha);
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }


        }

        public IEnumerable<tbDocumento> getDocsByNoTrack( int sucursal, int caja,DateTime fechaApertura, DateTime fechaCierre)
        {
            try
            {
                using (Entities context = new Entities())

                {
                    if (fechaCierre == DateTime.MinValue)
                    {
                        return (from p in context.tbDocumento.Include("tbDetalleDocumento").Include("tbPagos")
                                where p.sucursal == sucursal && p.caja == caja
                                && p.fecha >= fechaApertura
                                && p.estado == true
                                select p).AsNoTracking().ToList();

                    }
                    else
                    {
                        return (from p in context.tbDocumento.Include("tbDetalleDocumento").Include("tbPagos")
                                where p.sucursal == sucursal && p.caja == caja
                                && p.fecha >= fechaApertura && p.fecha<=fechaCierre
                                && p.estado == true
                                select p).AsNoTracking().ToList();
                    }


                  /*  return list.Where(x => (x.tipoDocumento == (int)Enums.TipoDocumento.Factura || x.tipoDocumento == (int)Enums.TipoDocumento.FacturaElectronica
                    || x.tipoDocumento == (int)Enums.TipoDocumento.TiqueteElectronico) && x.fecha >= fecha);*/
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }


        }

        public IEnumerable<tbDocumento> getVentasByFechaNoTrack(DateTime fecha, int sucursal, int caja)
        {
            try
            {
                using (Entities context = new Entities())
                {
                    var list = (from p in context.tbDocumento.Include("tbDetalleDocumento").Include("tbPagos")
                                where p.sucursal == sucursal && p.caja == caja
                                select p).AsNoTracking().ToList();

                    return list.Where(x => x.estado == true && (x.tipoDocumento == (int)Enums.TipoDocumento.Factura || x.tipoDocumento == (int)Enums.TipoDocumento.FacturaElectronica
                    || x.tipoDocumento == (int)Enums.TipoDocumento.TiqueteElectronico) && x.fecha >= fecha);
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }


        }
        public List<tbDocumento> getListDocCreditoPendienteByCliente(int tipo, string id)
        {
            try
            {
                using (Entities context = new Entities())
                {
                    var x = (from p in context.tbDocumento.Include("tbPagos").Include("tbDetalleDocumento").Include("tbDetalleDocumento.tbProducto")
                             where p.estado == true && p.idCliente.Trim() == id.Trim() && p.tipoIdCliente == tipo && p.tipoVenta == (int)Enums.tipoVenta.Credito && p.estadoFactura == (int)Enums.EstadoFactura.Pendiente
                             select p).ToList();
                    return x;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public List<tbDocumento> getListFactPendiente()
        {
            try
            {
                using (Entities context = new Entities())
                {
                    return (from p in context.tbDocumento.Include("tbDetalleDocumento").Include("tbDetalleDocumento.tbProducto")
                            where p.estadoFactura == (int)Enums.EstadoFactura.Pendiente
                            select p).ToList();
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }


        }

        public IEnumerable<tbCompras> listaComprasSimplificada()
        {
            try
            {
                using (Entities context = new Entities())
                {

                    return (from p in context.tbCompras
                            where p.estado == true && p.reporteElectronico == true
                            select p).ToList();
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
            return null;

        }

        public IEnumerable<tbCompras> listaComprasSimplificadaInconsistente()
        {
            try
            {
                using (Entities context = new Entities())
                {

                    return (from p in context.tbCompras
                            where p.estado == true && p.reporteElectronico == true && (p.reporteAceptaHacienda == false
                   || p.mensajeRespHacienda == false || p.EstadoFacturaHacienda.Trim().ToUpper() == "PROCESANDO")
                            select p).ToList();
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
            return null;

        }

        public IEnumerable<tbReporteHacienda> GetListMensajesComprasIncosistentes()
        {
            try
            {
                using (Entities context = new Entities())
                {

                    return (from p in context.tbReporteHacienda.Include("tbCompras")
                            where p.mensajeRespHacienda == false || p.reporteAceptaHacienda == false ||
                            p.EstadoRespHacienda.Trim().ToUpper() == "PROCESANDO"
                            select p).ToList();
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
            return null;

        }

        public IEnumerable<tbReporteHacienda> GetListMensajesCompras()
        {
            try
            {
                using (Entities context = new Entities())
                {

                    return (from p in context.tbReporteHacienda.Include("tbCompras")
                            select p).ToList();
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
            return null;

        }
        //public IEnumerable<tbDocumento> GetListEntitiesFechas(DateTime inicio, DateTime fin)
        //{
        //    using (var context = new Entities())
        //    {
               
        //        var inicioParam = new SqlParameter("@Inicio", inicio);
        //        var finParam = new SqlParameter("@Fin", fin);
                

        //        var documentos = context.Database.SqlQuery<tbDocumento>(
        //            "EXEC sp_obtenerDocumentos @Inicio, @Fin",
        //            inicioParam, finParam
        //        ).ToList();

        //        return documentos;
        //    }
        //}

        public IEnumerable<tbDocumento> GetListEntitiesFechas(DateTime incio, DateTime fin)
        {
            try
            {
                using (Entities context = new Entities())
                {



                    var list = (from p in context.tbDocumento.Include("tbDetalleDocumento.tbProducto")
                                where p.reporteElectronic == true &&
                                   DbFunctions.TruncateTime(p.fecha) >= incio && DbFunctions.TruncateTime(p.fecha) <= fin
                                && p.estadoFactura != (int)Enums.EstadoFactura.Eliminada
                                select p).ToList();

                    foreach (var item in list)
                    {
                        if (item.idCliente != null)
                        {

                            item.tbClientes = clienteIns.GetClienteById((int)item.tipoIdCliente, item.idCliente.Trim());

                        }
                    }
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

        public IEnumerable<tbDocumento> GetListEntities()
        {
            try
            {
                using (Entities context = new Entities())
                {

                    var list = (from p in context.tbDocumento.Include("tbDetalleDocumento").Include("tbDetalleDocumento.tbProducto")
                                where p.estadoFactura != (int)Enums.EstadoFactura.Eliminada
                                && p.reporteElectronic == true
                                select p).ToList();

                    foreach (var item in list)
                    {
                        if (item.idCliente != null)
                        {

                            item.tbClientes = clienteIns.GetClienteById((int)item.tipoIdCliente, item.idCliente.Trim());

                        }
                    }
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
        public tbReporteHacienda GetMensajeById(int id)
        {
            try
            {
                using (Entities context = new Entities())
                {
                    return (from p in context.tbReporteHacienda
                            where p.id == id
                            select p).FirstOrDefault();
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public tbReporteHacienda GetMensajeByClave(string clave)
        {
            try
            {
                using (Entities context = new Entities())
                {
                    return (from p in context.tbReporteHacienda
                            where p.claveReceptor == clave
                            select p).FirstOrDefault();
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public tbReporteHacienda GetMensajeByConsecutivo(string consecutivo)
        {
            try
            {
                using (Entities context = new Entities())
                {
                    return (from p in context.tbReporteHacienda
                            where p.consecutivoReceptor == consecutivo
                            select p).FirstOrDefault();
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        public tbReporteHacienda GetMensajeByClaveIdEmisor(string clave, string emisor)
        {
            try
            {
                using (Entities context = new Entities())
                {
                    return (from p in context.tbReporteHacienda
                            where p.claveDocEmisor == clave && p.idEmisor == emisor
                            select p).FirstOrDefault();
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public tbReporteHacienda ActualizarMensaje(tbReporteHacienda entity)
        {
            try
            {
                using (Entities context = new Entities())
                {

                    context.Entry(entity).State = System.Data.Entity.EntityState.Modified;

                    context.SaveChanges();

                }
                return entity;
            }
            catch (Exception ex)
            {

                throw new SaveEntityException("Error al reportar mensaje");
            }
        }



        public tbReporteHacienda GuardarMensaje(tbReporteHacienda entity)
        {
            try
            {
                using (Entities context = new Entities())
                {

                    context.tbReporteHacienda.Add(entity);
                    context.SaveChanges();

                }
                return entity;
            }
            catch (Exception ex)
            {

                clsEvento evento = new clsEvento(ex, "1");
                throw new SaveEntityException("Error al reportar mensaje");
            }
        }


        public tbDocumento modificarProforma(tbDocumento entity)
        {

            try
            {

                using (Entities context = new Entities())
                {
                    //entity.tbAbonos = null;
                    //tbDocumento doc = GetEntity(entity);
                    entity.tbPagos = null;
                    entity.tbClientes = null;
                    entity.tbEmpresa = null;
                    entity.tbTipoDocumento = null;
                    entity.tbTipoMoneda = null;
                    entity.tbTipoPago = null;
                    entity.tbTipoVenta = null;
                    foreach (var item in entity.tbDetalleDocumento)
                    {
                        item.tbProducto = null;
                        item.tbDocumento = null;

                        context.Entry(item).State = System.Data.Entity.EntityState.Added;

                    }
                    context.Entry(entity).State = System.Data.Entity.EntityState.Added;

                    IEnumerable<tbDetalleDocumento> detalle = (from p in context.tbDetalleDocumento
                                                               where p.idTipoDoc == entity.tipoDocumento && p.idDoc == entity.id
                                                               select p).ToList();


                    tbDocumento doc = (from p in context.tbDocumento
                                       where p.tipoDocumento == entity.tipoDocumento && p.id == entity.id
                                       select p).SingleOrDefault();


                    context.tbDetalleDocumento.RemoveRange(detalle);
                    context.tbDocumento.Remove(doc);


                    context.SaveChanges();
                    return entity;

                }
            }
            catch (Exception ex)
            {
                throw new UpdateEntityException("Proforma actualización.");
            }



        }
        public tbDocumento Guardar(tbDocumento entity)
        {
            try
            {

                entity.tbClientes = null;
                using (Entities context = new Entities())
                {
                    foreach (var item in entity.tbDetalleDocumento)
                    {
                        if (item.tbProducto != null)
                        {
                            item.tbProducto = null;
                        }

                        context.Entry(item).State = System.Data.Entity.EntityState.Added;
                    }
                    foreach (var item in entity.tbPagos)
                    {
                        //if (item.tipoPago==(int)Enums.TipoPago.Otros)
                        //{
                        //    context.Entry(item).State = System.Data.Entity.EntityState.Deleted;
                        //}
                        //else
                        //{
                        context.Entry(item).State = System.Data.Entity.EntityState.Added;
                        //}



                    }

                    context.tbDocumento.Add(entity);

                    //if (entity.tipoDocumento == (int)Enums.TipoDocumento.NotaCreditoElectronica || entity.tipoDocumento == (int)Enums.TipoDocumento.NotaDebitoElectronica)
                    //{
                    //    if (entity.claveRef != null)
                    //    {
                    //        if (entity.codigoRef == (int)Enums.TipoRef.AnulaDocumentoReferencia )
                    //        {
                    //            var docActualizar = GetEntityByClave(entity.claveRef);
                    //            docActualizar.estado = false;//elimino logicamente
                    //            docActualizar.fecha_ult_mod = Utility.getDate();
                    //            docActualizar.usuario_ult_mod = Global.Usuario.nombreUsuario.Trim();
                    //            context.Entry(docActualizar).State = System.Data.Entity.EntityState.Modified;
                    //        }

                    //    }

                    //}

                    if ((bool)Global.Usuario.tbEmpresa.tbParametrosEmpresa.First().manejaInventario && entity.tipoDocumento != (int)Enums.TipoDocumento.Proforma && entity.tipoDocumento != (int)Enums.TipoDocumento.ProformaGeneral)
                    {
                        foreach (tbDetalleDocumento detalle in entity.tbDetalleDocumento)
                        {
                            if (detalle.idProducto != "SM")
                            {
                                tbInventario inven = new tbInventario();
                                inven.idProducto = detalle.idProducto;
                                inven = inventarioIns.GetEntity(inven);
                                //si guardo factura desde 0, y el estado es cancelado, actualizo el inventario
                                if (entity.tipoDocumento == (int)Enums.TipoDocumento.FacturaElectronica || entity.tipoDocumento == (int)Enums.TipoDocumento.Factura || entity.tipoDocumento == (int)Enums.TipoDocumento.TiqueteElectronico)
                                {
                                    inven.cantidad = inven.cantidad - detalle.cantidad;
                                }
                                if (entity.tipoDocumento == (int)Enums.TipoDocumento.NotaCreditoElectronica || entity.tipoDocumento == (int)Enums.TipoDocumento.NotaDebitoElectronica || entity.tipoDocumento == (int)Enums.TipoDocumento.NotaCredito)
                                {
                                    inven.cantidad = inven.cantidad + detalle.cantidad;

                                }

                                context.Entry(inven).State = System.Data.Entity.EntityState.Modified;

                            }

                        }



                    }

                    context.SaveChanges();

                }
                return entity;
            }
            catch (Exception ex)
            {
                clsEvento evento = new clsEvento(ex, "1");
                throw new SaveEntityException("Error en Factura");
            }
        }
        public tbCompras GuardarCompra(tbCompras entity)
        {
            try
            {
                //entity.tbProveedores = null;
                //entity.tbReporteHacienda = null;
                //entity.tbTipoPago = null;
                //entity.tbTipoVenta = null;

                using (Entities context = new Entities())
                {
                    bool banderaInv = false;
                    bool banderaMod = false;
                    context.tbCompras.Add(entity);
                    //crear el producto
                    foreach (var linea in entity.tbDetalleCompras)
                    {//verifica si tiene q actualizalo o creealo



                        banderaInv = false;
                        banderaMod = false;

                        tbProducto pro = linea.idProducto == "0" ? null : productoIns.GetEntityById(linea.idProducto);
                        banderaInv = pro == null ? false : true;
                        if (linea.actualizaPrecio || linea.actualizaInvent)
                        {
                            if (pro != null)
                            {
                                if (linea.actualizaPrecio)
                                {
                                    ProductoDTO proP;
                        
                                    if (linea.cantidad != linea.cantidadVenta)
                                    {

                                        pro.precio_real = (decimal)(((linea.precio * linea.cantidad)- linea.montoTotaDesc+linea.montoOtroImp)/ linea.cantidadVenta);
                                    }
                                    else
                                    {
                                        pro.precio_real =  (decimal)(((linea.precio * linea.cantidad) - linea.montoTotaDesc + linea.montoOtroImp) / linea.cantidad);

                                    }
                                    //envia a calcular precios
                                    proP = ProductosUtility.calcularPrecio(pro.precio_real, (decimal)linea.utilidad, (decimal)pro.utilidad2Porc, (decimal)pro.utilidad3Porc, (decimal)linea.tarifaImpVenta);

                                    pro.precioUtilidad1 = proP.MontoUtilidad1;
                                    pro.precioUtilidad2 = proP.MontoUtilidad2;
                                    pro.precioUtilidad3 = proP.MontoUtilidad3;

                                    pro.utilidad1Porc = proP.Utilidad1;
                                    pro.utilidad2Porc = proP.Utilidad2;
                                    pro.utilidad3Porc = proP.Utilidad3;

                                    pro.precioVenta1 = proP.Precio1;
                                    pro.precioVenta2 = proP.Precio2;
                                    pro.precioVenta3 = proP.Precio3;
                                  
                                    if (linea.montoTotalImp != 0)
                                    {

                                        tbImpuestos impuesto = impuestoIns.GetListEntities(1).ToList().Where(x => x.valor == (linea.tarifaImpVenta == 99 ? 13:linea.tarifaImpVenta)).FirstOrDefault();
                                        pro.esExento = false;
                                        pro.idTipoImpuesto = impuesto.id;

                                    }
                                    else
                                    {

                                        pro.esExento = true;
                                        pro.idTipoImpuesto = (int)Enums.ImpuestosID.tarifa0;
                                    }
                                }

                                //auditoria
                                pro.fecha_ult_mod = Utility.getDate();
                                pro.usuario_ult_mod = Global.Usuario.nombreUsuario.Trim().ToUpper();   // Global.Usuario.nombreUsuario;


                                pro.nombre = linea.nombreProducto;
                                pro.id_categoria = (int)linea.id_categoria;                             
                                

                                if (linea.codigoCabys != null && linea.codigoCabys != string.Empty && linea.codigoCabys != "0")
                                {
                                    pro.codigoCabys = linea.codigoCabys;
                                }

                                pro.tbProveedores = null;
                                pro.tbTipoMedidas = null;
                                pro.tbDetalleDocumentoPendiente = null;
                                pro.tbCategoriaProducto = null;
                                pro.tbDetalleDocumento = null;
                                pro.tbImpuestos = null;
                                pro.tbPromociones = null;
                                pro.tbDetalleOrdenCompra = null;
                              

                                context.Entry(pro).State = System.Data.Entity.EntityState.Modified;

                                if (linea.actualizaInvent)
                                {
                                    if (entity.tipoDoc == (int)Enums.TipoDocumento.NotaCredito)
                                    {
                                        pro.tbInventario.cantidad -= (decimal)linea.cantidadVenta;
                                    }
                                    else
                                    {
                                        pro.tbInventario.cantidad += (decimal)linea.cantidadVenta;
                                    }

                                    pro.tbImpuestos = null;
                                    pro.tbProveedores = null;
                                    pro.tbTipoMedidas = null;
                                    pro.tbPromociones = null;
                                    context.Entry(pro.tbInventario).State = System.Data.Entity.EntityState.Modified;

                                }

                            }
                            else
                            {

                                if (linea.idProducto != null && linea.idProducto != string.Empty)
                                {
                                    banderaMod = false;
                                    banderaInv = false;
                                    //crea el producto nuevo
                                    pro = new tbProducto();


                                    pro.idProducto = linea.idProducto;
                                    //Almacenamos el nuevo producto
                                    pro.nombre = linea.nombreProducto.ToUpper().Trim();

                                    pro.id_categoria = (int)linea.id_categoria;

                                    pro.idMedida = (int)linea.idMedidaVenta;



                                    if (proveedorIns.GetProveedorByIdExist(entity.tipoIdProveedor, entity.idProveedor))
                                    {
                                        pro.idTipoIdProveedor = entity.tipoIdProveedor;
                                        pro.idProveedor = entity.idProveedor;

                                    }
                                                     
                                    if (linea.cantidad != linea.cantidadVenta)
                                    {

                                        pro.precio_real = (decimal)(((linea.precio * linea.cantidad) - linea.montoTotaDesc + linea.montoOtroImp) / linea.cantidadVenta);
                                    }
                                    else
                                    {
                                        pro.precio_real = (decimal)(((linea.precio * linea.cantidad) - linea.montoTotaDesc + linea.montoOtroImp) / linea.cantidad);

                                    }

                                    pro.precioVariable = Global.Usuario.tbEmpresa.tbParametrosEmpresa.FirstOrDefault().precioVariable;

                                    pro.codigoActividad = Global.actividadEconomic.CodActividad;
                                    int imp = 0;

                                    if (linea.montoTotalImp != 0)
                                    {

                                        tbImpuestos impuesto = impuestoIns.GetListEntities(1).ToList().Where(x => x.valor == (linea.tarifaImpVenta == 99 ? 13 : linea.tarifaImpVenta)).FirstOrDefault();
                                        imp = (int)impuesto.valor;
                                        pro.esExento = false;
                                        pro.idTipoImpuesto = impuesto.id;


                                    }
                                    else
                                    {
                                        imp = 0;
                                        pro.esExento = true;
                                        pro.idTipoImpuesto = (int)Enums.ImpuestosID.tarifa0;
                                    }

                                    ProductoDTO prodP = ProductosUtility.calcularPrecio(pro.precio_real, (decimal)linea.utilidad, imp);

                                    pro.precioUtilidad1 = prodP.MontoUtilidad1;
                                    pro.precioUtilidad2 = prodP.MontoUtilidad2;
                                    pro.precioUtilidad3 = prodP.MontoUtilidad3;

                                    pro.utilidad1Porc = prodP.Utilidad1;
                                    pro.utilidad2Porc = prodP.Utilidad2;
                                    pro.utilidad3Porc = prodP.Utilidad3;

                                    pro.precioVenta1 = prodP.Precio1;
                                    pro.precioVenta2 = prodP.Precio2;
                                    pro.precioVenta3 = prodP.Precio3;

                                    //descuento
                                    decimal desc = (decimal)Global.Usuario.tbEmpresa.tbParametrosEmpresa.FirstOrDefault().descuentoBase;

                                    if (desc == null || desc == 0)
                                    {
                                        pro.aplicaDescuento = false;
                                        pro.descuento_max = 0;
                                    }
                                    else
                                    {
                                        pro.aplicaDescuento = true;
                                        pro.descuento_max = desc;
                                    }

                                    if (linea.codigoCabys != null && linea.codigoCabys != string.Empty && linea.codigoCabys != "0")
                                    {
                                        pro.codigoCabys = linea.codigoCabys;
                                    }
                                    pro.aplicaExo = true;
                                    pro.codigoActividad = Global.actividadEconomic.CodActividad;

                                    pro.cocina = false;


                                    //Atributos de Auditoria
                                    pro.estado = true;
                                    pro.fecha_crea = Utility.getDate();
                                    pro.fecha_ult_mod = Utility.getDate();
                                    pro.usuario_crea = Global.Usuario.nombreUsuario.Trim().ToUpper();   // Global.Usuario.nombreUsuario;
                                    pro.usuario_ult_mod = Global.Usuario.nombreUsuario.Trim().ToUpper();   // Global.Usuario.nombreUsuario;

                                    tbInventario inventario = new tbInventario();

                                    inventario.idProducto = linea.idProducto;
                                    inventario.cantidad = linea.actualizaInvent ? (decimal)linea.cantidadVenta:0;
                                    inventario.cant_max = linea.actualizaInvent ? (decimal)linea.cantidadVenta : 0;
                                    inventario.cant_min = linea.actualizaInvent ? (decimal)linea.cantidadVenta : 0;

                                    inventario.estado = true;
                                    inventario.fecha_crea = Utility.getDate();
                                    inventario.fecha_ult_mod = Utility.getDate();
                                    inventario.usuario_crea = Global.Usuario.nombreUsuario.Trim().ToUpper();   // Global.Usuario.nombreUsuario;
                                    inventario.usuario_ult_mod = Global.Usuario.nombreUsuario.Trim().ToUpper();   // Global.Usuario.nombreUsuario;


                                    pro.tbInventario = inventario;


                                    context.Entry(pro).State = System.Data.Entity.EntityState.Added;

                                }


                            }

                        }

                        //if (banderaInv || banderaMod)
                        //{
                        //    if (linea.actualizaInvent)
                        //    {
                        //        if (entity.tipoDoc == (int)Enums.TipoDocumento.NotaCredito)
                        //        {
                        //            pro.tbInventario.cantidad -= (decimal)linea.cantidadVenta;
                        //        }
                        //        else
                        //        {
                        //            pro.tbInventario.cantidad += (decimal)linea.cantidadVenta;
                        //        }

                        //    }
                        //    pro.tbImpuestos = null;
                        //    pro.tbProveedores = null;
                        //    pro.tbTipoMedidas = null;
                        //    context.Entry(pro.tbInventario).State = System.Data.Entity.EntityState.Modified;

                        //}
                    }


                    context.SaveChanges();

                }
                return entity;
            }
            catch (Exception ex)
            {
                clsEvento evento = new clsEvento(ex.Message, "1");
                throw new SaveEntityException("Error en Factura");
            }
         }
        public List<tbDocumento> GetListEntities(int estado)
        {
            throw new NotImplementedException();
        }


        public IEnumerable<DocumentoHaciendaDTO> getListaInconsistentesAsync()
        {

            using (var context = new Entities())
            {


                var documentos = context.Database.SqlQuery<DocumentoHaciendaDTO>(
                    "EXEC sp_obtenerDocumentosPendientesHacienda"
                ).ToList();

                return documentos;
            }

        }
        public IEnumerable<DocumentoHaciendaDTO> getListaValidacion(DateTime inicio, DateTime fin)
        {

            using (var context = new Entities())
            {
                var inicioParam = new SqlParameter("@inicio", inicio);
                var finParam = new SqlParameter("@fin", fin);

                var documentos = context.Database.SqlQuery<DocumentoHaciendaDTO>(
                    "EXEC sp_obtenerDocsValidar @inicio, @fin", inicioParam, finParam).ToList();

                return documentos;
            }

        }

        public IEnumerable<tbDocumento> getListaInconsistentes()
        {
            try
            {
                using (Entities context = new Entities())
                {

                    return (from p in context.tbDocumento
                            where p.estadoFactura != (int)Enums.EstadoFactura.Eliminada
                            && p.reporteElectronic == true && (p.reporteAceptaHacienda == false ||
                            p.mensajeRespHacienda == false || p.mensajeReporteHacienda.Trim().ToUpper() == "PROCESANDO")
                            select p).ToList();
                }

            }
            catch (Exception ex)
            {
                clsEvento evento = new clsEvento(ex.Message, "1");
                throw ex;
            }
            return null;

        }

        public IEnumerable<tbDocumento> getDocsCorreosPendientes()
        {
            try
            {
                using (var context = new Entities())
                {
                   return context.Database.SqlQuery<tbDocumento>("ObtenerDocumentosParaNotificar").ToList();
                }
             

            }
            catch (Exception ex)
            {
                clsEvento evento = new clsEvento(ex.Message, "1");
                throw ex;
            }
            return null;

        }
    }


}
