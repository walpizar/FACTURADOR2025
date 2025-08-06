using CommonLayer.Licencia;
using EntityLayer;
using System.Collections.Generic;

namespace CommonLayer
{
    public static class Global
    {

        //public static int sucursal { get; set; }
        //public static int NumeroCaja { get; set; }
        public static int busquedaProductos { get; set; }
        public static tbEmpresaActividades actividadEconomic { get; set; }
        public static bool multiActividad { get; set; }
        public static string actividadPrincial { get; set; }

        public static clsConfiguracion Configuracion { get; set; }
        public static tbUsuarios Usuario { get; set; }

        public static bool licenciaWeb { set; get; }

        public static List<clsEmpresas> licencias { set; get; }

        public static ResponseLicencias licencia { get; set; }

        public static string conexion { set; get; }
        public static string conexionReportes { set; get; }

        public static bool cerrar { set; get; }

    }
}
