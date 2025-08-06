using DataLayer;
using EntityLayer;
using System.Collections.Generic;
//using CommontLayer.exceptions.bussinessException;

namespace BusinessLayer
{
    public class BExoneraciones
    {
        DExoneraciones exoneracion = new DExoneraciones();


        public List<tbExoneraciones> getListaExoneraciones()
        {
            return exoneracion.GetListEntities(3);
        }




    }
}

