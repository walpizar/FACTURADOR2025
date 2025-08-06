using DataLayer;

namespace BusinessLayer
{
    public class BActualizaciones
    {
        DActualizaciones DActIns = new DActualizaciones();
        public bool updateDataBase(string query)
        {
            return DActIns.updateDataBase(query);
        }

    }
}
