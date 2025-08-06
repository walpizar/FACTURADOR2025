using System;

namespace CommonLayer.Exceptions.BussinessExceptions
{
    public class RespuestaHaciendaException : Exception
    {
        public RespuestaHaciendaException()
        {
        }

        public RespuestaHaciendaException(Exception ex) : base(ex.Message)
        {
        }
    }
}
