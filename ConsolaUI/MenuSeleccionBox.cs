using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Entidades;
using Logica;

namespace ConsolaUI
{
    public static class MenuSeleccionBox
    {
        public static void Iniciar() 
        {
            //Mostramos menu
            Menu.HeaderPrincipal();
            Menu.BannerMenu("Menu Boxes");
            OpcionesMenu();

            //Validamos
            int opcionSeleccionada = ValidarIngresoUsuario();

            //Realizamos accion segun opcion seleccionada
            if (opcionSeleccionada >= 1 && opcionSeleccionada <= PC.Boxes.Length)
            {
                #region Ingresar al menu de la box seleccionada
                LogicaPC.BoxSeleccionada = PC.Boxes[opcionSeleccionada - 1];

                Menu.CambiarColor(ConsoleColor.Green);
                Console.WriteLine($"Ingresando al menu de la box '{LogicaPC.BoxSeleccionada.Nombre}'...");
                Thread.Sleep(3000);
                MenuBox.Iniciar();
                #endregion
            }
            else
            { 
                Console.WriteLine("Volviendo al menu boxes...");
                MenuBoxes.Iniciar();
            }
        }

        static int ValidarIngresoUsuario() 
        {
            int opcionSeleccionada;
            Console.Write("Ingrese el nro de box: ");
            while (!EsOpcionValida(Console.ReadLine(), out opcionSeleccionada))
            {
                Menu.CambiarColor(ConsoleColor.Red);
                Console.WriteLine("\nLa opcion ingresada es inválida, vuelva a intentarlo...");
                Menu.ResetearColor();
                Console.Write("Ingrese el nro de box: ");
            }

            Console.WriteLine();
            return opcionSeleccionada;
        }

        public static bool EsOpcionValida(string valorIngresado, out int opcionSeleccionada) 
        {
            if (int.TryParse(valorIngresado, out opcionSeleccionada)) 
            {
                if (opcionSeleccionada >= 1 && opcionSeleccionada <= PC.Boxes.Length)
                    return true;
            }

            return false;
        }

        public static void OpcionesMenu()
        {
            Console.WriteLine(string.Format("{0}{1}", Menu.Identar(0), "0. Selecciona la box a la que desea ingresar: "));
            #region Generando un submenu dinamico con la lista de cajas y su disponibilidad
            int indice = 0;
            foreach (Box box in PC.Boxes)
            {
                Menu.CambiarColor(ConsoleColor.White);
                string opcionBox = $"{++indice}. {box.Nombre}";
                Console.Write(string.Format("{0}{1} ", Menu.Identar(3), opcionBox));
                ContadorBox(box);
            }
            #endregion
            Console.WriteLine($"2. Volver al menu de boxes\n");
        }

        private static void ContadorBox(Box box)
        {
            LogicaBox boxLogica = new LogicaBox(box);
            int cantidadAtrapados = boxLogica.ObtenerTodosLosCapturados().Length;

            Console.ForegroundColor = (cantidadAtrapados == Box.Capacidad) ? ConsoleColor.Red : Console.ForegroundColor;
            Console.Write("[{0}/{1}]\n", cantidadAtrapados, Box.Capacidad);
            Menu.ResetearColor();
        }
    }
}
