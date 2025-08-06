using System;

namespace CommonLayer.Exceptions.BussinessExceptions
{
    public class TokenException : Exception
    {
        public TokenException() : base()
        {

        }
        public TokenException(Exception ex) :
           base(ex.Message)
        {

        }
    }
}
