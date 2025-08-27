using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLayer.DTO
{
    public class ActividadEconomicaResponse
    {
      
            public string Nombre { get; set; }
            public string Identificacion { get; set; }
            public List<Actividad> actividades { get; set; }
      
    }

    public class Actividad
    {
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public char Estado { get; set; }

        // Propiedad para mostrar en el combo
        public string Display => $"{Codigo} - {Descripcion}";
    }
}
