using DataLayer;
using EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    
    public class BPromociones
    {
        DPromociones promocionesIns = new DPromociones();
        public BPromociones()
        {

        }

        public tbPromociones Actualizar(tbPromociones entity)
        {
            return promocionesIns.Actualizar(entity);
        }
      

        public tbPromociones GetEntity(tbPromociones entity)
        {
            throw new NotImplementedException();
        }

        public List<tbPromociones> GetListEntities(int estado)
        {
            return promocionesIns.GetListEntities(estado);
        }

        public List<tbPromociones> getPromosByIdProdFechas(string idProd, DateTime fecha)
        {
            return promocionesIns.getPromosByIdProdFechas(idProd, fecha);
        }
        public List<tbPromociones> getPromosByIdProdFechas(int idPromo,string idPro, DateTime fecha)
        {
            return promocionesIns.getPromosByIdProdFechas(idPromo, idPro, fecha);
        }

        public tbPromociones Guardar(tbPromociones entity)
        {
            return promocionesIns.Guardar(entity);
        }
    }
}
