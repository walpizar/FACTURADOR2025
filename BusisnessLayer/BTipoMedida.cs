﻿using CommonLayer.Exceptions.BusisnessExceptions;
using DataLayer;
using EntityLayer;
using System.Collections.Generic;
//using CommontLayer.exceptions.bussinessException;

namespace BusinessLayer
{
    public class bTipoMedidas
    {
        DTipoMedida tipoMedidaD = new DTipoMedida();

        public bool eliminar(tbTipoMedidas TipoGLo)
        {

            return tipoMedidaD.eliminar(TipoGLo);
        }


        public bool guardar(tbTipoMedidas tipomedida)
        {
            tbTipoMedidas verificartipomedida = tipoMedidaD.GetEnity(tipomedida);
            if (verificartipomedida == null)
            {
                return tipoMedidaD.Guardar(tipomedida);
            }
            else
            {
                if (tipomedida.estado == true)
                {
                    throw new EntityExistException("tipo Medida");
                }
                else
                {
                    throw new EntityDisableStateException("tipo Medida");
                }
            }
        }
        public List<tbTipoMedidas> getlistatipomedida()
        {
            return tipoMedidaD.GetListEntities(3);
        }



        public bool modificar(tbTipoMedidas tipomedida)
        {
            return tipoMedidaD.Actualizar(tipomedida);
        }
        public tbTipoMedidas GetEnity(tbTipoMedidas NombreTipoMedida)
        {
            return tipoMedidaD.GetEnity(NombreTipoMedida);
        }
        public tbTipoMedidas GetEnityById(tbTipoMedidas medida)
        {
            return tipoMedidaD.GetEnityById(medida);
        }
        public tbTipoMedidas GetEnityByNomenclatura(tbTipoMedidas medida)
        {
            return tipoMedidaD.GetEnityByNomenclatura(medida);
        }
        public tbTipoMedidas GetEnityByNomenclatura(string nomenclatura)
        {
            return tipoMedidaD.GetEnityByNomenclatura(nomenclatura);
        }
    }
}

