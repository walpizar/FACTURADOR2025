using CommonLayer.Exceptions.BusisnessExceptions;
using DataLayer;
using EntityLayer;
using System;
using System.Collections.Generic;
namespace BusinessLayer
{
    public class BCajas

    {
        DCajas Inscajas = new DCajas();


        public tbCajasMovimientos guardar(tbCajasMovimientos cajas)
        {

            tbCajasMovimientos cajasL = Inscajas.GetEntity(cajas);
            if (cajasL == null)
            {

                return Inscajas.Guardar(cajas);
            }
            else
            {



                throw new EntityExistException("  cajas ");




            }
        }

        public tbCajasMovimientos modificar(tbCajasMovimientos cajas)
        {


            return Inscajas.Actualizar(cajas);

        }
        public tbCajasMovimientos eliminar(tbCajasMovimientos cajas)
        {


            return Inscajas.Actualizar(cajas);

        }

        public List<tbCajasMovimientos> getListCierres(DateTime inicio, DateTime fin)
        {


            return Inscajas.GetListCierres(inicio, fin);


        }

        public List<tbCajasMovimientos> getListTipoing(int estado)
        {


            return Inscajas.GetListEntities(estado);


        }


        public tbCajasMovimientos getEntity(tbCajasMovimientos cajas)
        {
            return Inscajas.GetEntity(cajas);
        }


    }
}
