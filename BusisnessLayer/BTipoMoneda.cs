using DataLayer;
using EntityLayer;
using System.Collections.Generic;

namespace BusinessLayer
{
    public class BTipoMoneda
    {
        // DTipoMedida TipoMonedaIns = new DTipoMedida();

        public List<tbTipoMoneda> GetListTipoMoneda(int estado)
        {
            return DTipoMoneda.GetListTipoMoneda(1);
        }
    }
}
