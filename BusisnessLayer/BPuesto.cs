using CommonLayer.Exceptions.BusisnessExceptions;
using DataLayer;
using EntityLayer;
using System.Collections.Generic;



namespace BusinessLayer
{
    public class BPuesto
    {
        DPuesto PuestoIns = new DPuesto();//creo un nuevo objeto para eviar a datos
        public List<tbTipoPuesto> getlistEntities(int estado)
        {

            return PuestoIns.GetListEntities(estado);
        }

        public tbTipoPuesto guardar(tbTipoPuesto puesto)
        {

            tbTipoPuesto puestoTra = PuestoIns.GetEntity(puesto);
            if (puestoTra == null)
            {

                return PuestoIns.Guardar(puesto);
            }
            else
            {

                if (puestoTra.estado == true)
                {
                    throw new EntityExistException("puesto");
                }
                else
                {
                    throw new EntityDisableStateException("puesto");
                }


            }
        }

        public tbTipoPuesto modificar(tbTipoPuesto entity)
        {

            return PuestoIns.Actualizar(entity);

        }
        public tbTipoPuesto eliminar(tbTipoPuesto entity)
        {

            return PuestoIns.Actualizar(entity);

        }




    }

}

