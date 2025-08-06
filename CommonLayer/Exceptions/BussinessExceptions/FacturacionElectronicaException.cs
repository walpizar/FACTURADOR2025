using System;

namespace CommonLayer.Exceptions.BussinessExceptions
{
    public class FacturacionElectronicaException : Exception
    {

        public FacturacionElectronicaException() : base()
        {

        }

        public FacturacionElectronicaException(string mensaje) : base(mensaje)
        {

        }
        public FacturacionElectronicaException(Exception ex) :
           base(ex.Message)
        {

        }

    }
}
