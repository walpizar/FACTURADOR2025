﻿using CommonLayer.Interfaces;
using EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataLayer
{
    class DPersona : IDataGeneric<tbPersona>
    {
        public tbPersona Actualizar(tbPersona entity)
        {
            throw new NotImplementedException();
        }

        public tbPersona GetEntity(tbPersona entity)
        {

            tbPersona per;
            try
            {
                using (Entities context = new Entities())
                {

                    per = (from e in context.tbPersona//desde a el el context de tabla empleado y la table de persona
                           where e.tipoId == entity.tipoId && e.identificacion == entity.identificacion //donde si e.tipoid es = emplead.id
                           select e).SingleOrDefault();// seleccione e

                    return per;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<tbPersona> GetListEntities(int estado)
        {
            throw new NotImplementedException();
        }

        public tbPersona Guardar(tbPersona entity)
        {
            throw new NotImplementedException();
        }
    }
}
