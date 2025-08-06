using CommonLayer;
using System;

namespace CommonLayer
{
    public class CreaTicket
    {
        public CreaTicket(bool cocina)
        {
            this.impresora = Global.Configuracion.rutaImpresoraExtra;
        }
        public CreaTicket()
        {
            this.impresora = Global.Configuracion.rutaImpresoraPuntoVenta;
        }



        string ticket = "";
        string parte1, parte2;
        string impresora = Global.Configuracion.rutaImpresoraPuntoVenta; // nombre exacto de la impresora como esta en el panel de control
        int max, cort;
        public void LineasGuion()
        {
            ticket = "----------------------------------------\n";   // agrega lineas separadoras -
            RawPrinterHelper.SendStringToPrinter(impresora, ticket); // imprime linea
        }
        public void LineasAsterisco()
        {
            ticket = "****************************************\n";   // agrega lineas separadoras *
            RawPrinterHelper.SendStringToPrinter(impresora, ticket); // imprime linea
        }
        public void LineasIgual()
        {
            ticket = "========================================\n";   // agrega lineas separadoras =
            RawPrinterHelper.SendStringToPrinter(impresora, ticket); // imprime linea
        }
        public void LineasTotales()
        {
            ticket = "                             -----------\n"; ;   // agrega lineas de total
            RawPrinterHelper.SendStringToPrinter(impresora, ticket); // imprime linea
        }
        public void EncabezadoVenta()
        {
            ticket = "Producto        Cant   P.Unit    Importe\n";   // agrega lineas de  encabezados
            RawPrinterHelper.SendStringToPrinter(impresora, ticket); // imprime texto
        }
        public void VariasLineas(string par1)                          // agrega texto a la izquierda
        {
            
            max = par1.Length;
         
            if (max > 40)                                 // **********
            {         
                do
                {
                    max= par1.Length>40? 40 : par1.Length;
                 
                    ticket = par1.Substring(0, max) + "\n";
                    RawPrinterHelper.SendStringToPrinter(impresora, ticket); // imprime texto                
                    
                    par1 = par1.Remove(0, max);
               

                } while (par1.Length> 0);

            }
            else { 
                
                parte1 = par1;
                ticket = parte1 + "\n";
                RawPrinterHelper.SendStringToPrinter(impresora, ticket); // imprime texto
            }                      // **********
        
        }
        public void TextoIzquierda(string par1)                          // agrega texto a la izquierda
        {
            max = par1.Length;
            if (max > 40)                                 // **********
            {
                cort = max - 40;
                parte1 = par1.Remove(40, cort);        // si es mayor que 40 caracteres, lo corta
            }
            else { parte1 = par1; }                      // **********
            ticket = parte1 + "\n";
            RawPrinterHelper.SendStringToPrinter(impresora, ticket); // imprime texto
        }
        public void TextoDerecha(string par1)
        {
            ticket = "";
            max = par1.Length;
            if (max > 40)                                 // **********
            {
                cort = max - 40;
                parte1 = par1.Remove(40, cort);           // si es mayor que 40 caracteres, lo corta
            }
            else { parte1 = par1; }                      // **********
            max = 40 - par1.Length;                     // obtiene la cantidad de espacios para llegar a 40
            for (int i = 0; i < max; i++)
            {
                ticket += " ";                          // agrega espacios para alinear a la derecha
            }
            ticket += parte1 + "\n";                    //Agrega el texto
            RawPrinterHelper.SendStringToPrinter(impresora, ticket); // imprime texto
        }
        public void TextoCentro(string par1)
        {
            ticket = "";
            max = par1.Length;
            if (max > 40)                                 // **********
            {
                cort = max - 40;
                parte1 = par1.Remove(40, cort);          // si es mayor que 40 caracteres, lo corta
            }
            else { parte1 = par1; }                      // **********
            max = (int)(40 - parte1.Length) / 2;         // saca la cantidad de espacios libres y divide entre dos
            for (int i = 0; i < max; i++)                // **********
            {
                ticket += " ";                           // Agrega espacios antes del texto a centrar
            }                                            // **********
            ticket += parte1 + "\n";
            RawPrinterHelper.SendStringToPrinter(impresora, ticket); // imprime texto
        }
        public void TextoExtremos(string par1, string par2)
        {
            max = par1.Length;
            if (max > 18)                                 // **********
            {
                cort = max - 18;
                parte1 = par1.Remove(18, cort);          // si par1 es mayor que 18 lo corta
            }
            else { parte1 = par1; }                      // **********
            ticket = parte1;                             // agrega el primer parametro
            max = par2.Length;
            if (max > 18)                                 // **********
            {
                cort = max - 18;
                parte2 = par2.Remove(18, cort);          // si par2 es mayor que 18 lo corta
            }
            else { parte2 = par2; }
            max = 40 - (parte1.Length + parte2.Length);
            for (int i = 0; i < max; i++)                 // **********
            {
                ticket += " ";                            // Agrega espacios para poner par2 al final
            }                                             // **********
            ticket += parte2 + "\n";                     // agrega el segundo parametro al final
            RawPrinterHelper.SendStringToPrinter(impresora, ticket); // imprime texto
        }
        public void lineaComanda(string par1, decimal cant, string estado)
        {
            estado = "**" + estado + "**";
               cant = Math.Abs(cant);
            cant = decimal.Parse(string.Format("{0:N1}", cant));           

          
                max = par1.Length;
                if (max > 30)                                 // **********
                {
                    cort = max - 30;
                    parte1 = par1.Remove(30, cort);          // corta a 16 la descripcion del articulo
                }
                else { parte1 = par1; }                      // **********
                ticket = parte1;                           // agrega articulo
                                   // agrega cantidad
                max = 20 - estado.Length;
                for (int i = 0; i < max; i++)                // **********
                {
                    ticket += " ";                           // Agrega espacios
                }                                           // **********
                                                        // **********
                ticket += estado + "\n"; // agrega precio
                RawPrinterHelper.SendStringToPrinter(impresora, ticket); // imprime texto
        
        }
        public void AgregaTotales(string par1, decimal total)
        {
            total = decimal.Parse(string.Format("{0:N2}", total));
            max = par1.Length;
            if (max > 25)                                 // **********
            {
                cort = max - 25;
                parte1 = par1.Remove(25, cort);          // si es mayor que 25 lo corta
            }
            else { parte1 = par1; }                      // **********
            ticket = parte1;
            parte2 = (String.Format("{0:N2}", total.ToString().Trim()));
            max = 40 - (parte1.Length + parte2.Length);
            for (int i = 0; i < max; i++)                // **********
            {
                ticket += " ";                           // Agrega espacios para poner el valor de moneda al final
            }                                            // **********
            ticket += parte2 + "\n";
            RawPrinterHelper.SendStringToPrinter(impresora, ticket); // imprime texto
        }
        public void AgregaArticuloSinPrecio(string par1, decimal cant)
        {
            cant = decimal.Parse(string.Format("{0:N2}", cant));
          

            if (Utility.DoFormat(cant).Length <= 5 ) // valida que cant precio y total esten dentro de rango
            {
                max = par1.Length;
                if (max > 16)                                 // **********
                {
                    cort = max - 16;
                    parte1 = par1.Remove(16, cort);          // corta a 16 la descripcion del articulo
                }
                else { parte1 = par1; }                      // **********
                ticket = parte1;                             // agrega articulo
                max = (5 - Utility.DoFormat(cant).Length) + (16 - parte1.Length);
                for (int i = 0; i < max; i++)                // **********
                {
                    ticket += " ";                           // Agrega espacios para poner el valor de cantidad
                }
                ticket += Utility.DoFormat(cant);                   // agrega cantidad
               
                RawPrinterHelper.SendStringToPrinter(impresora, ticket); // imprime texto
            }
            else
            {
                //  MessageBox.Show("Valores fuera de rango");
                RawPrinterHelper.SendStringToPrinter(impresora, "Error, valor fuera de rango\n"); // imprime texto
            }
        }
        public void AgregaArticulo(string par1, decimal cant, decimal precio, decimal total, string imp)
        {
            // Formatear sin redondear
            string cantStr = Utility.TruncFormat(cant);
            string precioStr = Utility.TruncFormat(precio);
            string totalStr = Utility.TruncFormat(total);

            // Validar que los valores no excedan su ancho máximo
            if (cantStr.Length <= 5 &&
                precioStr.Length <= 10 &&
                totalStr.Length <= 10)
            {
                // 1. Descripción alineada a la izquierda en 15 caracteres
                string parte1 = (par1.Length > 15) ? par1.Substring(0, 15) : par1.PadRight(15);
                ticket = parte1; // exactamente 15 caracteres

                // 2. Cantidad centrada en 5 caracteres
                int cantPad = 5 - cantStr.Length;
                ticket += new string(' ', cantPad / 2) + cantStr + new string(' ', cantPad - cantPad / 2);

                // 3. Precio centrado en 8 caracteres
                int precioPad = 10 - precioStr.Length;
                ticket += new string(' ', precioPad / 2) + precioStr + new string(' ', precioPad - precioPad / 2);

                // 4. Total centrado en 10 caracteres
                int totalPad = 10 - totalStr.Length;
                ticket += new string(' ', totalPad / 2) + totalStr + new string(' ', totalPad - totalPad / 2);

                // 5. Asegurar que la línea tenga exactamente 38 caracteres
                ticket = ticket.Length > 40 ? ticket.Substring(0, 40) : ticket.PadRight(40);

                // 6. Agregar código adicional (como "01") y salto de línea
                ticket += imp + "\n";

                // 7. Enviar a impresora
                RawPrinterHelper.SendStringToPrinter(impresora, ticket);
            }
            else
            {
                RawPrinterHelper.SendStringToPrinter(impresora, "Error, valor fuera de rango\n");
            }


        }
        public void CortaTicket()
        {
            string corte = "\x1B" + "m";                  // caracteres de corte
            string avance = "\x1B" + "d" + "\x09";        // avanza 9 renglones
            RawPrinterHelper.SendStringToPrinter(impresora, avance); // avanza
            RawPrinterHelper.SendStringToPrinter(impresora, corte); // corta
        }
        public void AbreCajon()
        {
            string cajon0 = "\x1B" + "p" + "\x00" + "\x0F" + "\x96";                  // caracteres de apertura cajon 0
            string cajon1 = "\x1B" + "p" + "\x01" + "\x0F" + "\x96";                 // caracteres de apertura cajon 1
            RawPrinterHelper.SendStringToPrinter(impresora, cajon0); // abre cajon0
            //RawPrinterHelper.SendStringToPrinter(impresora, cajon1); // abre cajon1
        }
    }
}
