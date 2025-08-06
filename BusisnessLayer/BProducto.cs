using CommonLayer;
using CommonLayer.Exceptions.BusisnessExceptions;
using DataLayer;
using EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLayer
{
    public class BProducto
    {

        DProductos productoIns = new DProductos();


        /// <summary>
        /// Recuperamos el detalle del producto.
        /// </summary>
        /// <param name="idBuscar"></param>
        /// <returns></returns>
        //public List<tbDetalleProducto> getDetalleProducto(string idBuscar)
        //{
        //    return productoIns.getDetalleProducto(idBuscar);
        //}

        //public tbDetalleProducto getDetalleProdByIngreProd(int idIngre, string idProd)
        //{
        //    return productoIns.GetEntityByIdProdcutoIngre(idIngre, idProd);
        //}


        /// <summary>
        /// Recuperamos los productos de la base de datos.
        /// </summary>
        /// <returns></returns>
        public List<tbProducto> getProductos(int estado)
        {

            return productoIns.GetListEntities(estado);

        }

        public List<tbAcompanamiento> getProductoAcompa(string id)
        {

            return productoIns.getProductoAcompa(id);

        }

        public List<tbProducto> getProductosBusqueda(int estado, string valor)
        {

            return productoIns.GetListEntitiesBusqueda(estado, valor);

        }

        public bool actualizarProductosImport(List<tbProducto> listaPro)
        {

            return productoIns.ActualizarProductosImports(listaPro);
        }


        /// <summary>
        /// Pasamos la informacion para actualizar en la base da datos.
        /// </summary>
        /// <param name="productoNuevo"></param>
        /// <returns></returns>
        public tbProducto actualizarProducto(tbProducto productoNuevo)
        {
            try
            {

                // Ejecutamos el actualizar del producto
                return productoIns.Actualizar(productoNuevo);

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }



        /// <summary>
        /// Recuperamos la entidad.
        /// </summary>
        /// <param name="producto"></param>
        /// <returns></returns>
        public tbProducto GetEntity(tbProducto producto)
        {

            return productoIns.GetEntity(producto);

        }

        public tbProducto GetEntity(tbProducto producto, int estado)
        {

            return productoIns.GetEntity(producto, estado);

        }
        public tbProducto GetEntityByNombre(tbProducto producto)
        {

            return productoIns.GetEntityByNombre(producto);

        }
        public tbProducto GetEntityById(string id)
        {

            return productoIns.GetEntityById(id);

        }
       
        public int obtenerNuevoId()
        {
            int mayor = 0;
            IEnumerable<tbProducto> lista = productoIns.GetListEntities(3);
            lista = lista.Where(x => x.idProducto.Count() == 5).ToList();
            foreach (var item in lista)
            {
                if (Utility.isNumerInt(item.idProducto))
                {
                    if (int.Parse(item.idProducto) > mayor)
                    {
                        mayor = int.Parse(item.idProducto);
                    }
                }

            }

            return (++mayor);

        }
        public tbProducto guardarProductoAcompa(tbProducto pro, List<tbAcompanamiento> lista)
        {

            return productoIns.GuardarAcompa(pro, lista);


        }

            /// <summary>
            /// Pasamos la informacion para almacenar en la base de datos.
            /// </summary>
            /// <param name="productoNUevo"></param>
            /// <returns></returns>
            public tbProducto guardarProducto(tbProducto productoNUevo)
        {

            tbProducto productoExiste = productoIns.GetEntity(productoNUevo);



            if (productoExiste == null)
            {

                if (productoNUevo.idProducto == String.Empty)
                {
                    int cod = obtenerNuevoId();

                    productoNUevo.idProducto = cod.ToString().PadLeft(5, '0');


                }

                return productoIns.Guardar(productoNUevo);
            }
            else
            {
                if (productoExiste.estado == true)
                {
                    throw new EntityExistException("producto");
                }
                else
                {
                    throw new EntityDisableStateException("producto");
                }
            }
        }




        /// <summary>
        /// Recuperamos el nombre del ingrediente segun su iD.
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        //public List<tbIngredientes> getNombreIngrediente(int p)
        //{
        //    return productoIns.getNombreIngrediente(p);
        //}



        //public tbIngredientes getNombrePorID(int Id)
        //{
        //    return productoIns.getNomrePreID(Id);
        //}


        /// <summary>
        /// Recuperamos el producto segun su categoria
        /// </summary>
        /// <param name="cate"></param>
        /// <returns></returns>
        public List<tbProducto> getListProductoByCategoria(int cate)
        {
            return productoIns.getListProductoByCategoria(cate);
        }



        /// <summary>
        /// Eliminamos el detalle del producto de la base de datos.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        //public tbDetalleProducto BorrarDetalleProducto(tbDetalleProducto entity)
        //{
        //    return productoIns.EliminarDetalleProducto(entity);

        //}

        ///// <summary>
        ///// Guardamos el detalle de producto nuevo.
        ///// </summary>
        ///// <param name="entity"></param>
        ///// <returns></returns>
        //public tbDetalleProducto guardarDetalleProducto(tbDetalleProducto entity)
        //{
        //    return productoIns.GuardarDetalleProducto(entity);
        //}

    }
}
