using DataLayer;
using EntityLayer;
using System.Collections.Generic;

namespace BusinessLayer
{
    public class BOrdenCompra
    {
        DOrdenCompra ordenIns = new DOrdenCompra();
        public tbOrdenCompra Actualizar(tbOrdenCompra entity)
        {
            return ordenIns.Actualizar(entity);
        }

        public tbOrdenCompra GetEntity(tbOrdenCompra entity)
        {
            return ordenIns.GetEntity(entity);
        }

        public List<tbOrdenCompra> GetListEntities(int estado)
        {
            return ordenIns.GetListEntities(estado);
        }

        public List<tbOrdenCompra> GetListEntities(int estadoOrden, bool estado)
        {
            return ordenIns.GetListEntities(estadoOrden, estado);
        }

        public tbOrdenCompra getEntityByID(int orden)
        {
            return ordenIns.GetentityByID(orden);
        }

        public tbOrdenCompra Guardar(tbOrdenCompra entity)
        {

            int id = ordenIns.getNewID() + 1;

            entity.id = id;
            foreach (var item in entity.tbDetalleOrdenCompra)
            {
                item.idOrdenCompra = id;
                item.idDetalle = item.numLinea;
            }


            return ordenIns.Guardar(entity);
        }
        public tbOrdenCompra Modificar(tbOrdenCompra orden)
        {

            return ordenIns.Modificar(orden);
        }

        public tbOrdenCompra Eliminar(tbOrdenCompra orden)
        {
            return ordenIns.Eliminar(orden);
        }

        public List<tbOrdenCompra> getOrderByParams(int id, string idprov, int tipoProv, int estado)
        {
            return ordenIns.getOrderByParams(id, idprov, tipoProv, estado);
        }



    }
}
