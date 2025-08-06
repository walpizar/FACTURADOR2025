using System;
using System.Windows.Forms;


namespace PresentationLayer
{
    static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {

            //Aqui habria que poner el formulario de incio de sesion.

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);


            Application.Run(new FormPrincipal());
        }

    }
}
