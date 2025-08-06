using DataLayer;
using EntityLayer;

namespace BusinessLayer
{

    public class BCanton
    {
        DCanton cantonIns = new DCanton();

        public tbCanton GetEntity(string idC, string idP)
        {
            return cantonIns.GetEntity(idC, idP);
        }
    }
}
