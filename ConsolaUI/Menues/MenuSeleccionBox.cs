using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Entidades;
using Logica;
using ConsolaUI.Utilidades;

namespace ConsolaUI.Menues
{
    public static class MenuSeleccionBox
    {
        public static void Iniciar()
        {
            bool seguirEnMenu = true;

            do
            {
                Menu.HeaderPrincipal();
                Menu.BannerMenu("Menu Seleccion Boxes");
                OpcionesMenu();

                int opcionSeleccionada = ValidarIngresoUsuario();

                if (opcionSeleccionada >= 1 && opcionSeleccionada <= PC.Boxes.Length)
                {
                    LogicaPC.BoxSeleccionada = PC.Boxes[opcionSeleccionada - 1];
                    Menu.CambiarColor(ConsoleColor.Green);
                    PantallaCargaABox();

                }
                else if (opcionSeleccionada == 0)
                {
                    seguirEnMenu = false;
                    Console.WriteLine("Volviendo al menu de seleccion...");
                }

            } while (seguirEnMenu);

        }

        public static int ValidarIngresoUsuario()
        {
            int opcionSeleccionada;
            Console.Write("Ingrese el nro de box: ");
            while (!EsOpcionValida(Console.ReadLine(), out opcionSeleccionada))
            {
                Menu.CambiarColor(ConsoleColor.Red);
                Console.WriteLine("La opcion ingresada es inválida, vuelva a intentarlo...");
                Menu.ResetearColor();
                Console.Write("\nIngrese el nro de box: ");
            }

            Console.WriteLine();
            return opcionSeleccionada;
        }

        static bool EsOpcionValida(string valorIngresado, out int opcionSeleccionada)
        {
            if (int.TryParse(valorIngresado, out opcionSeleccionada))
            {
                if (opcionSeleccionada >= 0 && opcionSeleccionada <= PC.Boxes.Length)
                    return true;
            }

            return false;
        }

        static void OpcionesMenu()
        {
            Console.WriteLine("Box disponibles: ");
            #region Generando un submenu dinamico con la lista de cajas y su disponibilidad
            int indice = 0;
            foreach (Box box in PC.Boxes)
            {
                Menu.CambiarColor(ConsoleColor.White);
                string opcionBox = $"{++indice}. {box.Nombre}";
                Console.Write("{0}{1} ", Menu.Identar(4), opcionBox);
                ContadorBox(box);
            }
            Console.WriteLine();
            #endregion
            Console.WriteLine("0. Volver al menu de seleccion\n");

        }

        static void ContadorBox(Box box)
        {
            LogicaBox boxLogica = new LogicaBox(box);
            int cantidadAtrapados = boxLogica.ObtenerTodosLosCapturados().Length;

            Console.ForegroundColor = (cantidadAtrapados == Box.Capacidad) ? ConsoleColor.Red : Console.ForegroundColor;
            Console.Write("[{0}/{1}]\n", cantidadAtrapados, Box.Capacidad);
            Menu.ResetearColor();
        }

        /// <summary>
        /// Simula una loading screen antes de pasar a la box seleccionada
        /// </summary>
        static void PantallaCargaABox()
        {
            Console.Clear();
            Console.WriteLine($"Ingresando al menu de la box '{LogicaPC.BoxSeleccionada.Nombre}'.");
            Thread.Sleep(500);

            Console.Clear();
            Console.WriteLine($"Ingresando al menu de la box '{LogicaPC.BoxSeleccionada.Nombre}'..");
            Thread.Sleep(500);

            Console.Clear();
            Console.WriteLine($"Ingresando al menu de la box '{LogicaPC.BoxSeleccionada.Nombre}'...");
            Thread.Sleep(500);

            MenuBox.Iniciar();
        }
    }
}
