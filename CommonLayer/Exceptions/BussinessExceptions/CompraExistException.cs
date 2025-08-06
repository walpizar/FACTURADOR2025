namespace CommonLayer.Exceptions.BusisnessExceptions
{
    public class CompraExistException : System.Exception
    {
        public CompraExistException() : base("El documento ya existe en la base de datos.")
        {

        }

    }
}
