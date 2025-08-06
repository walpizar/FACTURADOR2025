using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLayer.DTO
{
    public class DocumentoHaciendaDTO
    {
        public int id { get; set; }
        public int tipoDocumento { get; set; }
        public string consecutivo { get; set; }
        public string clave { get; set; }       
        public System.DateTime fecha { get; set; }     
        public int estadoFactura { get; set; }
        public string EstadoFacturaHacienda { get; set; }
        public bool reporteAceptaHacienda { get; set; }
        public string mensajeReporteHacienda { get; set; }
        public Nullable<bool> mensajeRespHacienda { get; set; }       
        public bool notificarCorreo { get; set; }
        public string correo1 { get; set; }
        public string correo2 { get; set; }        
        public Nullable<int> estadoCorreo { get; set; }
        public string idCliente { get; set; }
    }
}
