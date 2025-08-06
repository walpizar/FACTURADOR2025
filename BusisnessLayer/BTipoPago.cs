using DataLayer;
using EntityLayer;
using System.Collections.Generic;
//using CommontLayer.exceptions.bussinessException;

namespace BusinessLayer
{
    public class BTipoPago
    {
        DTipoPago tipoPago = new DTipoPago();


        public List<tbTipoPago> getListaTipoPagos()
        {
            return tipoPago.GetListEntities(3);
        }


    }
}

