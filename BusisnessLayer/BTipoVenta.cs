using DataLayer;
using EntityLayer;
using System.Collections.Generic;
//using CommontLayer.exceptions.bussinessException;

namespace BusinessLayer
{
    public class BTipoVenta
    {
        DtipoVenta tipoVenta = new DtipoVenta();




        public List<tbTipoVenta> getListaTipoVenta()
        {
            return tipoVenta.GetListEntities(3);
        }



    }
}

