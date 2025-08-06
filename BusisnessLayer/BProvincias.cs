using DataLayer;
using EntityLayer;
using System.Collections.Generic;
namespace BusinessLayer
{
    public class BProvincias

    {
        DProvincia InsProvincia = new DProvincia();



        public List<tbProvincia> getListTipoing(int estado)
        {


            return InsProvincia.GetListEntities(estado);


        }



    }
}
