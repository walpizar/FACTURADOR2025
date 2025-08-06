using EntityLayer;
using System.Collections.Generic;

namespace CommonLayer
{
    public class clsDocumentoCorreo
    {

        public clsDocumentoCorreo( List<string> correoDestino)
        {         
            this.correoDestino = correoDestino; 

        }
        public clsDocumentoCorreo(tbDocumento doc, List<string> correoDestino, bool cargarAdjuntos, int tipoAdjuntos)
        {
            this.doc = doc;
            this.correoDestino = correoDestino;
            this.cargarAdjuntos = cargarAdjuntos;
            this.tipoAdjuntos = tipoAdjuntos;



        }
        public clsDocumentoCorreo(tbDocumento doc, List<string> correoDestino, bool cargarAdjuntos, int tipoAdjuntos, bool precios)
        {
            this.doc = doc;
            this.correoDestino = correoDestino;
            this.cargarAdjuntos = cargarAdjuntos;
            this.tipoAdjuntos = tipoAdjuntos;
            this.preciosProforma = precios;


        }
        public clsDocumentoCorreo(tbOrdenCompra doc, List<string> correoDestino, bool cargarAdjuntos, int tipoAdjuntos)
        {
            this.ordenCompra = doc;
            this.correoDestino = correoDestino;
            this.cargarAdjuntos = cargarAdjuntos;
            this.tipoAdjuntos = tipoAdjuntos;

        }

        public clsDocumentoCorreo(tbReporteHacienda msj, List<string> correoDestino, bool cargarAdjuntos, int tipoAdjuntos)
        {
            this.msj = msj;
            this.correoDestino = correoDestino;
            this.cargarAdjuntos = cargarAdjuntos;
            this.tipoAdjuntos = tipoAdjuntos;

        }

        public tbDocumento doc { get; set; }
        public List<string> correoDestino { get; set; }
        public bool cargarAdjuntos { get; set; }
        public int tipoAdjuntos { get; set; }
        public tbReporteHacienda msj { get; set; }

        public tbOrdenCompra ordenCompra { get; set; }

        public bool preciosProforma { get; set; }


    }
}
