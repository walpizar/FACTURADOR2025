using DataLayer;
using EntityLayer;

namespace BusinessLayer
{
    public class BEmpresa
    {
        DEmpresa empresaIns = new DEmpresa();
        public tbEmpresa getEntity(tbEmpresa entity)
        {
            return empresaIns.GetEntity(entity);
        }
        public tbEmpresa getEntityForPrint(tbEmpresa entity)
        {
            return empresaIns.getEntityForPrint(entity);
        }
        public tbParametrosEmpresa getEntityParametros(tbParametrosEmpresa entity)
        {
            return empresaIns.GetEntityParametros(entity);
        }
        public tbEmpresa guardar(tbEmpresa entity)
        {
            return empresaIns.Guardar(entity);
        }
        public tbEmpresa modificar(tbEmpresa entity)
        {
            return empresaIns.Actualizar(entity);
        }

        public tbEmpresa ActualizarEmpresa(tbEmpresa empresa)
        {
            return empresaIns.ActualizarEmpresa(empresa);
        }

        public tbParametrosEmpresa GuardarParametros(tbParametrosEmpresa entity)
        {
            return empresaIns.GuardarParametros(entity);
        }
        public tbParametrosEmpresa modificarParamtros(tbParametrosEmpresa entity)
        {
            return empresaIns.ActualizarParametros(entity);
        }
    }
}
