using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLayer.DTO
{
    public class CabysDTO
    {
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public List<string> Categorias { get; set; }
        public int Impuesto { get; set; }
        public string Uri { get; set; }
        public string Estado { get; set; }
    }

    public class CabysResponseDTO
    {
        public int Total { get; set; }
        public int Cantidad { get; set; }
        public List<CabysDTO> Cabys { get; set; }
    }
}
