using DataLayer;
using EntityLayer;
using System.Collections.Generic;

namespace BusinessLayer
{
    public class BImpuestos
    {
        Dimpuestos impIns = new Dimpuestos();//creo un nuevo objeto para eviar a datos
        public List<tbImpuestos> getImpuestos(int estado)
        {
            return impIns.GetListEntities(estado);
        }

    }
}
