using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLayer
{
   public class clsMovimientos
    {
        public int tipoMov { get; set; }
        public DateTime fecha { get; set; }
        public decimal monto { get; set; }
        public string  usuarios { get; set; }
        public string observaciones { get; set; }
    }
}
