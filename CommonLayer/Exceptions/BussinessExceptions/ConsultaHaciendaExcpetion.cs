using System;

namespace CommonLayer.Exceptions.BussinessExceptions
{
    public class ConsultaHaciendaExcpetion : Exception
    {
        public ConsultaHaciendaExcpetion() : base()
        {

        }
        public ConsultaHaciendaExcpetion(Exception ex) :
           base(ex.Message)
        {

        }
    }
}
