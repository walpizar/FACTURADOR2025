using System.Collections.Generic;

namespace CommonLayer.Interfaces
{
    public interface IDataGeneric<T>
    {
        T Guardar(T entity);
        T Actualizar(T entity);
        T GetEntity(T entity);
        List<T> GetListEntities(int estado);
    }
}
