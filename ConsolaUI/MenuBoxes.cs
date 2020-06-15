using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logica;
using Entidades;


namespace ConsolaUI
{
    class MenuBoxes
    {
        public static void Iniciar()
        {
            Menu.HeaderPrincipal();
            Menu.BannerMenu("Menu Boxes");
            OpcionesMenu();

            switch (ValidarIngresoUsuario())
            {
                case OpcionesMenuBoxes.Boxes:
                    MenuSeleccionBox.Iniciar();
                    break;
                case OpcionesMenuBoxes.Resetear:
                    break;
                case OpcionesMenuBoxes.Intercambiar:
                    break;
                case OpcionesMenuBoxes.Agregar:
                    break;
                case OpcionesMenuBoxes.Volver:
                    MenuPC.Iniciar();
                    break;
                default:
                    break;
            }
        }


        /// <summary>
        /// Valida la opcion ingresada por el usuario en el Menu Boxes
        /// </summary>
        /// <returns></returns>
        static OpcionesMenuBoxes ValidarIngresoUsuario() 
        {
            OpcionesMenuBoxes opcionSeleccionada;

            Console.Write("Ingrese su opcion: ");
            while (!EsOpcionValida(Console.ReadKey(true), out opcionSeleccionada))
            {
                Menu.CambiarColor(ConsoleColor.Red);
                Console.WriteLine("\nLa opcion ingresada no es válida, vuelva a intentarlo..");
                Menu.ResetearColor();
                Console.Write("Ingrese su opcion: ");
            }
            Console.WriteLine();

            return opcionSeleccionada;
        }


        static bool EsOpcionValida(ConsoleKeyInfo teclaIngresada, out OpcionesMenuBoxes menuSeleccionado) 
        {
            menuSeleccionado = (OpcionesMenuBoxes) teclaIngresada.Key;

            //El usuario presiona una tecla y esta tiene que coincidir con las disponibles
            switch (menuSeleccionado)
            {
                case OpcionesMenuBoxes.Boxes:
                case OpcionesMenuBoxes.Resetear:
                case OpcionesMenuBoxes.Intercambiar:
                case OpcionesMenuBoxes.Agregar:
                case OpcionesMenuBoxes.Volver:
                    return true;
                default:
                    return false;
            }

        }

        static void OpcionesMenu()
        {
            StringBuilder sb = new StringBuilder();
            
            sb.AppendLine("1. Ir a seleccion de Boxes");
            sb.AppendLine("2. Resetear Box");
            sb.AppendLine("3. Intercambiar Boxes");
            sb.AppendLine("4. Agregar más Boxes");
            sb.AppendLine("5. Volver al menú principal");
            sb.AppendLine();

            Console.WriteLine(sb.ToString());
        }

        enum OpcionesMenuBoxes
        {
            Boxes = ConsoleKey.NumPad1,
            Resetear = ConsoleKey.NumPad2,
            Intercambiar = ConsoleKey.NumPad3,
            Agregar = ConsoleKey.NumPad4,
            Volver = ConsoleKey.NumPad5
        }

    }
}
