namespace CommonLayer
{
    public class ProductoDTO
    {

        decimal preciorReal;
        decimal utilidad1;
        decimal utilidad2;
        decimal utilidad3;

        decimal montoUtilidad1;
        decimal montoUtilidad2;
        decimal montoUtilidad3;


        decimal precio1;
        decimal precio2;
        decimal precio3;

        public decimal PreciorReal { get => preciorReal; set => preciorReal = value; }
        public decimal Utilidad1 { get => utilidad1; set => utilidad1 = value; }
        public decimal Utilidad2 { get => utilidad2; set => utilidad2 = value; }
        public decimal Utilidad3 { get => utilidad3; set => utilidad3 = value; }
        public decimal Precio1 { get => precio1; set => precio1 = value; }
        public decimal Precio2 { get => precio2; set => precio2 = value; }
        public decimal Precio3 { get => precio3; set => precio3 = value; }

        public decimal MontoUtilidad1 { get => montoUtilidad1; set => montoUtilidad1 = value; }
        public decimal MontoUtilidad2 { get => montoUtilidad2; set => montoUtilidad2 = value; }
        public decimal MontoUtilidad3 { get => montoUtilidad3; set => montoUtilidad3 = value; }
    }
}
