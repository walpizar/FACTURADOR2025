using DataLayer;
using EntityLayer;
using System.Collections.Generic;

namespace BusinessLayer
{
    public class BClienteActividadEconomica
    {
        private readonly DClienteActividadEconomica dataInst = new DClienteActividadEconomica();

        /// <summary>
        /// Obtiene las actividades económicas activas asignadas a un cliente.
        /// </summary>
        public List<tbClientesActividadesEconomicas> ObtenerPorCliente(string idCliente, int tipoIdCliente)
        {
            return dataInst.ObtenerPorCliente(idCliente, tipoIdCliente);
        }

        /// <summary>
        /// Guarda el conjunto completo de actividades económicas de un cliente:
        /// inserta las nuevas, actualiza el estado/principal de las existentes,
        /// y desactiva las que ya no están en la lista.
        /// </summary>
        public void GuardarActividades(
            string idCliente,
            int tipoIdCliente,
            List<tbClientesActividadesEconomicas> actividades,
            string usuario)
        {
            dataInst.Guardar(idCliente, tipoIdCliente, actividades, usuario);
        }
    }
}