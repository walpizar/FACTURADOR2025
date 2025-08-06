namespace CommonLayer.Exceptions.DataExceptions
{
    public class SaveEntityException : System.Exception
    {
        public SaveEntityException() : base()
        {

        }
        public SaveEntityException(string entityName) :
            base("Error al guardar la entidad " + entityName)
        {

        }
    }
}
