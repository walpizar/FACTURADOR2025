using System;

namespace CommonLayer.Exceptions
{
    public class IsNotANumberException : Exception
    {
        IsNotANumberException() : base("Esta celda sólo permite ingresar números, por favor ingresa un número")
        {

        }
    }
}
