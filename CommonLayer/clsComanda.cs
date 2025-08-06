using EntityLayer;
using System.Collections.Generic;

namespace CommonLayer
{
    public class clsComanda
    {
        public int numero { get; set; }
        public string alias { get; set; }
        public decimal cant { get; set; }
        public string codigoProducto { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public bool estado { get; set; }
        public bool llevar { get; set; }
        public List<string> acompanamientos  { get; set; }

    }
}
