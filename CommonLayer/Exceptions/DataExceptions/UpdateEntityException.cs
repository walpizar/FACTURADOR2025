using System;


namespace CommonLayer.Exceptions.DataExceptions
{
    public class UpdateEntityException : Exception
    {
        public UpdateEntityException() : base()
        {

        }
        public UpdateEntityException(string entityName) :
            base("Error al actualizar la entidad " + entityName)
        {

        }

    }
}
