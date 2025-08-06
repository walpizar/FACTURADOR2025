using EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;


namespace DataLayer
{
    public class DTipoMoneda
    {

        public static List<tbTipoMoneda> GetListTipoMoneda(int estado)
        {
            try
            {
                Entities contect = new Entities();

                return (from m in contect.tbTipoMoneda
                        where m.estado == true
                        select m).ToList();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


    }
}
