using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;

namespace ConsolaUI
{
    public abstract class Menu
    {
        static Menu()
        {
            Console.Title = PC.Titulo.ToUpper();
            _colorMenu = ConsoleColor.Green;
        }

        private static ConsoleColor _colorMenu;
        public static ConsoleColor ColorMenu
        {
            get { return _colorMenu; }
            set
            {
                _colorMenu = value;
                Console.ForegroundColor = _colorMenu;
            }
        }

        public static void HeaderPrincipal()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            string mensaje = string.Format("Player:{0}{1,30}Time:{2}", PC.Jugador.NombreOT, "|", DateTime.Now.ToLocalTime());
            Console.WriteLine(mensaje);
            DibujarLinea(mensaje.Length, '-');
            Console.WriteLine();
        }

        public abstract void Iniciar();

        public static void BannerMenu(string nombreDelMenu)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            DibujarLinea(65, '*');
            DibujarLinea(65, '*');

            //La cantidad de asteriscos tiene que ser proporcional a la longitud de la palabra
            Console.WriteLine("{0}{1}{2}", "***********************", nombreDelMenu, "***********************");
            DibujarLinea(65, '*');
            DibujarLinea(65, '*');
            Console.WriteLine();
        }

        public static void DibujarLinea(int longitud, char simbolo)
        {
            for (int i = 0; i < longitud; i++)
            {
                Console.Write(simbolo);
            }
            Console.WriteLine();
        }

    }
}
