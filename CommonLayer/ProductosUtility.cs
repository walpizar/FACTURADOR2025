namespace CommonLayer
{
    public static class ProductosUtility
    {
        public static ProductoDTO calcularPrecio(decimal precioReal, decimal utilidad1, decimal imp)
        {
            return calcularPrecio(precioReal, 0, utilidad1, utilidad1, utilidad1, imp);


        }
        public static ProductoDTO calcularPrecio(decimal precioReal,decimal desc, decimal utilidad1, decimal imp)
        {
            return calcularPrecio(precioReal, desc, utilidad1, utilidad1, utilidad1, imp);


        }
        public static ProductoDTO calcularPrecio(decimal precioReal,  decimal utilidad1, decimal utilidad2, decimal utilidad3, decimal imp)
        {
            return calcularPrecio(precioReal, 0, utilidad1, utilidad2, utilidad3, imp);
        }
        
        public static ProductoDTO calcularPrecio(decimal precioReal, decimal desc, decimal utilidad1,  decimal utilidad2, decimal utilidad3, decimal imp)
        {
            ProductoDTO prod=null;


            //utilidad1 = utilidad1 < 0 ? 0 : utilidad1;
            //utilidad2 = utilidad2 < 0 ? 0 : utilidad2;
            //utilidad3 = utilidad3 < 0 ? 0 : utilidad3;
            prod = new ProductoDTO();
            if (precioReal != 0) {

                
                prod.MontoUtilidad1 = precioReal * (1 + (utilidad1 / 100));
                prod.MontoUtilidad2 = precioReal * (1 + (utilidad2 / 100));
                prod.MontoUtilidad3 = precioReal * (1 + (utilidad3 / 100));

                if (desc != 0)
                {

                    prod.MontoUtilidad1 -= (prod.MontoUtilidad1* (desc/100));
                    prod.MontoUtilidad2 -= (prod.MontoUtilidad2 * (desc / 100)); 
                    prod.MontoUtilidad3 -= (prod.MontoUtilidad3 * (desc / 100)); 

                }



                prod.Utilidad1 = (((prod.MontoUtilidad1 / precioReal) * 100) - 100);
                prod.Utilidad2 = (((prod.MontoUtilidad2 / precioReal) * 100) - 100);
                prod.Utilidad3 = (((prod.MontoUtilidad3 / precioReal) * 100) - 100);

                imp = (imp == 99 ? 13 : imp);

                prod.Precio1 = prod.MontoUtilidad1 * (1 + (imp / 100));
                prod.Precio2 = prod.MontoUtilidad2 * (1 + (imp / 100));
                prod.Precio3 = prod.MontoUtilidad3 * (1 + (imp / 100));

            }
            else
            {
                prod.MontoUtilidad1 = 0;
                prod.MontoUtilidad2 = 0;
                prod.MontoUtilidad3 = 0;

                prod.Utilidad1 =0;
                prod.Utilidad2 = 0;
                prod.Utilidad3 = 0;

                prod.Precio1 = 0;
                prod.Precio2 = 0;
                prod.Precio3 = 0;

            }
         

            return prod;

        }
        public static ProductoDTO calcularPrecioUtilidad(decimal precioReal, decimal precioVenta1, decimal precioVenta2, decimal precioVenta3, decimal imp)
        {
            ProductoDTO prod = null;

            if (precioReal != 0)
            {
                prod = new ProductoDTO();

                prod.PreciorReal = precioReal;
                prod.Precio1 = precioVenta1;
                prod.Precio2 = precioVenta2;
                prod.Precio3 = precioVenta3;

                prod.MontoUtilidad1 = prod.Precio1 / (1 + (imp / 100));
                prod.MontoUtilidad2 = prod.Precio2 / (1 + (imp / 100));
                prod.MontoUtilidad3 = prod.Precio3 / (1 + (imp / 100));

                imp = (imp == 99 ? 13 : imp);

               prod.Utilidad1 = (((prod.MontoUtilidad1 / precioReal) * 100) - 100);
                prod.Utilidad2 = (((prod.MontoUtilidad2 / precioReal) * 100) - 100);
                prod.Utilidad3 = (((prod.MontoUtilidad3 / precioReal) * 100) - 100);



            }

            


            return prod;

        }

       
    }
}
