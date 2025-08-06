using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLayer.Licencia
{
    public class ResponseLicencias
    {
        public DateTime fechaVenc { get; set; }
        public bool estado { get; set; }
        public bool bloqueo { get; set; }

    }
}
