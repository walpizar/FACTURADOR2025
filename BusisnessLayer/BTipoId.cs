using DataLayer;
using EntityLayer;
using System.Collections.Generic;
//using CommontLayer.exceptions.bussinessException;

namespace BusinessLayer
{
    public class BTipoId
    {
        DTipoId tipoId = new DTipoId();


        public List<tbTipoId> getListaTipoId()
        {
            return tipoId.GetListEntities(3);
        }




    }
}

