using EntityLayer;
using System.Collections.Generic;

namespace CommonLayer
{
    public class clsAbonos
    {
        public IEnumerable<tbDocumento> abonos { get; set; }

        public string correo { get; set; }

        public tbClientes cliente { get; set; }

        public decimal montoAbondo { get; set; }

        public decimal saldoPendiente { get; set; }

        public bool enviarCoreo { get; set; }

        public bool imprimir { set; get; }

    }
}
