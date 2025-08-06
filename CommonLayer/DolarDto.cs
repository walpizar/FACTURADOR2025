using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLayer
{
    public class Compra
    {
        public DateTime fecha { get; set; }
        public decimal valor { get; set; }
    }

    public class Venta
    {
        public DateTime fecha { get; set; }
        public decimal valor { get; set; }
    }


    public class DolarDto
    {
        public Compra compra { get; set; }
        public Venta venta { get; set; }

    }
}
