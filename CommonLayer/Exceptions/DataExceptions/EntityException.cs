namespace CommonLayer.Exceptions.DataExceptions
{
    public class EntityException : System.Exception
    {
        public EntityException() : base()
        {

        }
        public EntityException(string entityName) :
            base("Error al obtener la entidad " + entityName)
        {

        }
    }
}
