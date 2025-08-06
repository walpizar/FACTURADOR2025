using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLayer.DTO
{

    public class RegimenDTO
    {
        public int Codigo { get; set; }
        public string Descripcion { get; set; }
    }
    
    public class SituacionDTO
    {
        public string Moroso { get; set; }
        public string Omiso { get; set; }
        public string Estado { get; set; }
        public string AdministracionTributaria { get; set; }
    }

    public class ActividadDTO
    {
        public string Estado { get; set; }
        public string Tipo { get; set; }
        public int Codigo { get; set; }
        public string Descripcion { get; set; }
    }

    public class ClienteResponseDTO
    {
        public string Nombre { get; set; }
        public string TipoIdentificacion { get; set; }
        public RegimenDTO Regimen { get; set; }
        public SituacionDTO Situacion { get; set; }
        public List<ActividadDTO> Actividades { get; set; }
    }
}
