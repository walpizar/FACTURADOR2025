using DataLayer;
using EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class BAcompanamiento
    {
        DAcompanamiento acompaIns = new DAcompanamiento();

        public List<tbAcompanamiento> GetListEntities()
        {
            return acompaIns.GetListEntities();
        }

        public List<tbAcompanamiento> getAcompanamiento(int estado)
        {
            return acompaIns.GetListEntities(estado);
        }
        public List<tbAcompanamiento> getAcompanamientos()
        {
            return acompaIns.GetListEntities();
        }

        public tbAcompanamiento Getentity(tbAcompanamiento entity)
        {
            return acompaIns.GetEntity(entity);
        }

    


        /// <summary>
        /// Actualizamos la informacion de la categoria
        /// </summary>
        /// <param name="categoria"></param>
        /// <returns></returns>
        public tbAcompanamiento actualizar(tbAcompanamiento acompa)
        {
            return acompaIns.Actualizar(acompa);
        }


        /// <summary>
        /// Guardamos la categoria nueva en la base de datos.
        /// </summary>
        /// <param name="categoriaNueva"></param>
        /// <returns></returns>
        public tbAcompanamiento guarda(tbAcompanamiento acompa)
        {

            tbAcompanamiento existe = acompaIns.GetEntity(acompa);
            if (existe == null)
            {

                acompa.id = acompaIns.GetLastID();
                //Enviamos la entidad a guardarse.
                return acompaIns.Guardar(acompa);

            }
            else
            {
                //Lo enviamos a modificar.


                existe.descripcion = acompa.descripcion;
                existe.estado = acompa.estado;
                existe.fecha_ult_mod = acompa.fecha_ult_mod;
                existe.usuario_ult_mod = acompa.usuario_ult_mod;

                return acompaIns.Actualizar(existe);

            }

        }

    }
}
