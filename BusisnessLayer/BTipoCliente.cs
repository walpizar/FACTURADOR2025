
using CommonLayer.Exceptions.BusisnessExceptions;
using DataLayer;
using EntityLayer;
using System.Collections.Generic;


namespace BusinessLayer
{
    public class BTipoCliente
    {
        DTipoCliente tipoInst = new DTipoCliente();

        public tbTipoClientes guardar(tbTipoClientes tipo)
        {

            tbTipoClientes tipoCliente = tipoInst.GetEntity(tipo);
            if (tipoCliente == null)
            {

                return tipoInst.Guardar(tipo);
            }
            else
            {

                if (tipoCliente.estado == true)
                {
                    throw new EntityExistException("tipo Cliente");
                }
                else
                {
                    throw new EntityDisableStateException("Tipo Cliente");
                }


            }
        }

        public tbTipoClientes modificar(tbTipoClientes tipoCliente)
        {


            return tipoInst.Actualizar(tipoCliente);

        }
        public tbTipoClientes eliminar(tbTipoClientes tipoCliente)
        {


            return tipoInst.Actualizar(tipoCliente);

        }



        public List<tbTipoClientes> GetListEntities(int estado)
        {


            return tipoInst.GetListEntities();


        }

        public tbTipoClientes GetEntity(tbTipoClientes Eltipo)
        {
            return tipoInst.GetEntity(Eltipo);
        }



    }
}
