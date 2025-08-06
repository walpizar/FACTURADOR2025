using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLayer
{
    public class EtiquetaDto
    {
        public string id { get; set; }
        public string nombre { get; set; }
        public decimal precio { get; set; }
        public bool promo { get; set; }
        public string nomenclatura { get; set; }
    }
}
