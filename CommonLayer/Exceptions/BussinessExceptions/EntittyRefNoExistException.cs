namespace CommonLayer.Exceptions.BusisnessExceptions
{
    public class EntittyRefNoExistException : System.Exception
    {

        public EntittyRefNoExistException() :
            base("La compra de origen no existe en la base datos. Incluya la compra original.")
        {

        }
    }
}
