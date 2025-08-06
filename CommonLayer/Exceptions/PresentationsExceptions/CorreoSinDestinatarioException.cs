using System;

namespace CommonLayer.Exceptions.PresentationsExceptions
{
    public class CorreoSinDestinatarioException : Exception

    {
        public CorreoSinDestinatarioException(string message) : base(message)
        {
        }
    }
}
