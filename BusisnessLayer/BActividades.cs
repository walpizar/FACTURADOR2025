using DataLayer;
using EntityLayer;
using System.Collections.Generic;

namespace BusinessLayer
{
    public class BActividades
    {
        private readonly DActividades dataInst = new DActividades();

        public List<tbActividades> Buscar(string filtro) => dataInst.Buscar(filtro);
    }
}