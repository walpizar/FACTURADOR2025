using EntityLayer;
using System;

namespace CommonLayer
{
    public class clsConfiguracion
    {
        public string server { set; get; }
        public string instance { set; get; }

        public int tipoEstacion { get; set; }
        public int sucursal { get; set; }
        public int caja { get; set; }

        public int imprime { get; set; }
        public int imprimeDoc { get; set; }

        public int imprimeTiquete { get; set; }
        public int imprimeAbonos { get; set; }
        public int imprimeProformas { get; set; }


        public string rutaImpresoraPuntoVenta { get; set; }
        public string rutaImpresoraMediaCarta { get; set; }
        public string rutaImpresoraNormal { get; set; }

        public int pantallaFacturacion { get; set; }
        public int respaldo { get; set; }

        public int aperturaCierre { get; set; }
        public int enviaCorreos { get; set; }

        public int actualizaciones { get; set; }

        public string logoRuta { get; set; }

        public int imprimeExtra { get; set; }
        public string rutaImpresoraExtra { get; set; }

        public tbCajasMovimientos mov { get; set; }
    }

}
