using System;

namespace CommonLayer.Exceptions.BussinessExceptions
{
    public class generarXMLException : Exception
    {
        public generarXMLException() : base()
        {

        }
        public generarXMLException(Exception ex) :
           base(ex.Message)
        {

        }
    }
}
