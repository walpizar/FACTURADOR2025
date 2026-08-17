// Este archivo NO es auto-generado por el EDMX: es una extensión manual
// (partial class) para agregar una propiedad de conveniencia que no existe
// en la base de datos ni en el modelo, y que por lo tanto EF no persiste.
//
// Motivo: tbClientesActividadesEconomicas no tiene relación de navegación
// hacia tbActividades en el modelo (no hay FK declarada entre
// CodigoCIIU y tbActividades.codigoAct), así que no podemos usar
// act.tbActividades.nombreAct. En su lugar, DClienteActividadEconomica
// llena este campo manualmente con un JOIN.

namespace EntityLayer
{
    public partial class tbClientesActividadesEconomicas
    {
        /// <summary>
        /// Nombre de la actividad económica (tbActividades.nombreAct).
        /// Se llena manualmente desde DataLayer; no está mapeado a ninguna columna.
        /// </summary>
        public string NombreActividad { get; set; }
    }
}
