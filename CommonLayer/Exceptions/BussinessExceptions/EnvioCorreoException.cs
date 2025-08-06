using System;

namespace CommonLayer.Exceptions.BussinessExceptions
{
    public class EnvioCorreoException : Exception
    {

        public EnvioCorreoException() : base()
        {

        }
        public EnvioCorreoException(Exception ex) :
           base(ex.Message)
        {

        }
    }
}
