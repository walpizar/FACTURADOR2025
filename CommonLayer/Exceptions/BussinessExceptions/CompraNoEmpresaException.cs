namespace CommonLayer.Exceptions.BusisnessExceptions
{
    public class CompraNoEmpresaException : System.Exception
    {
        public CompraNoEmpresaException() : base()
        {

        }
        public CompraNoEmpresaException(string entityName) :
            base("El doccumento # " + entityName + " No corresponde a nuestra empresa")
        {

        }
    }
}
