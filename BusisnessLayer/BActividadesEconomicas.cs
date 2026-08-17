using DataLayer;
using EntityLayer;
using System.Collections.Generic;

namespace BusinessLayer
{
    public class BActividadesEconomicas
    {
        DActividadesEconomicas DActIns = new DActividadesEconomicas();

        public List<tbClientesActividadesEconomicas> ObtenerPorCliente(string idCliente, int tipoIdCliente)
          => DActIns.ObtenerPorCliente(idCliente, tipoIdCliente);

        public void GuardarActividades(string idCliente, int tipoIdCliente,
            List<tbClientesActividadesEconomicas> actividades, string usuario)
            => DActIns.Guardar(idCliente, tipoIdCliente, actividades, usuario);

      
        public List<tbActividades> GetListEntities(int estado)
        {
            return DActIns.GetListEntities(estado);
        }
        public tbActividades getById(tbActividades act)
        {
            return DActIns.GetEntity(act);
        }

        public List<tbEmpresaActividades> getListaEmpresaActividad(string id, int tipo)
        {
            return DActIns.getListaEmpresaActividad(id, tipo);
        }

        public List<tbEmpresaActividades> getActividadesEconomicasCliente(string id, int tipo)
        {
            return DActIns.getListaEmpresaActividad(id, tipo);
        }

    }
}
