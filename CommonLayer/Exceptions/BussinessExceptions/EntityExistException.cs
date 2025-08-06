﻿namespace CommonLayer.Exceptions.BusisnessExceptions
{
    public class EntityExistException : System.Exception
    {
        public EntityExistException() : base()
        {

        }
        public EntityExistException(string entityName) :
            base("La entidad " + entityName + " ya existe en la base de datos.")
        {

        }
    }
}
