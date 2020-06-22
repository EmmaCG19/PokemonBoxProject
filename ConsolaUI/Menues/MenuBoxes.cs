using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logica;
using Entidades;
using Entidades.Excepciones;
using ConsolaUI.Utilidades;

namespace ConsolaUI.Menues
{
    class MenuBoxes
    {
        public static void Iniciar()
        {
            bool seguirEnMenu = true;

            do
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
                        ResetearBox();
                        break;
                    case OpcionesMenuBoxes.Intercambiar:
                        IntercambiarBoxes();
                        break;
                    case OpcionesMenuBoxes.Agregar:
                        AgregarBoxes();
                        break;
                    case OpcionesMenuBoxes.Volver:
                        seguirEnMenu = false;
                        break;
                    default:
                        break;
                }

            } while (seguirEnMenu);

        }

        /// <summary>
        /// Resetea y elimina toda la informacion cargada en la box
        /// </summary>
        static void ResetearBox()
        {
            Menu.HeaderPrincipal();
            int nroBox = ValidarBox("Ingrese un nro de box");

            try
            {
                Box box = LogicaPC.ObtenerBox(nroBox);

                if (Validaciones.ValidarSoN("Desea resetear la box"))
                {
                    LogicaPC.ResetearBox(box);
                    Menu.CambiarColor(ConsoleColor.Yellow);
                    Console.WriteLine("La box ha sido reseteada");
                }
                else
                {
                    Menu.CambiarColor(ConsoleColor.Red);
                    Console.WriteLine("No se ha reseteado la box");
                }
            }
            catch (NoExisteBoxException e)
            {
                Menu.CambiarColor(ConsoleColor.Red);
                Console.WriteLine(e.Message);
            }

            Menu.EspereUnaTecla();
        }

        /// <summary>
        /// Intercambia la posicion de dos boxes
        /// </summary>
        static void IntercambiarBoxes()
        {
            Menu.HeaderPrincipal();

            try
            {
                Box box1 = LogicaPC.ObtenerBox(ValidarBox("Ingrese la primera box"));
                Box box2 = LogicaPC.ObtenerBox(ValidarBox("Ingrese la segunda box"));

                LogicaPC.IntercambiarBoxes(box1, box2);
                Menu.CambiarColor(ConsoleColor.Yellow);
                Console.WriteLine("Las boxes fueron intercambiadas");
            }
            catch (NoExisteBoxException e)
            {
                Menu.CambiarColor(ConsoleColor.Red);
                Console.WriteLine(e.Message);
            }

            Menu.EspereUnaTecla();

        }

        /// <summary>
        /// Suma una cantidad de boxes a la PC
        /// </summary>
        static void AgregarBoxes()
        {
            Menu.HeaderPrincipal();

            int cantBoxes;
            Console.Write("Ingrese cuantas boxes desea agregar: ");

            while (!int.TryParse(Console.ReadLine(), out cantBoxes) || cantBoxes < 1)
            {
                Menu.CambiarColor(ConsoleColor.Red);
                Console.WriteLine("La cantidad ingresada es inválida, vuelva a intentarlo...\n");
                Menu.ResetearColor();
                Console.Write("\nIngrese cuantas boxes desea agregar: ");
            }

            try
            {
                LogicaPC.AgregarBoxes(cantBoxes);
                Menu.CambiarColor(ConsoleColor.Yellow);
                Console.WriteLine("Las boxes fueron agregadas");
            }
            catch (NoExisteBoxException e)
            {
                Menu.CambiarColor(ConsoleColor.Red);
                Console.WriteLine(e.Message);
            }

            Menu.EspereUnaTecla();

        }

        static int ValidarBox(string mensajeIngreso)
        {
            int opcionSeleccionada;
            Console.Write("{0} [1-{1}]: ", mensajeIngreso, PC.Boxes.Length);
            while (!EsBoxValida(Console.ReadLine(), out opcionSeleccionada))
            {
                Menu.CambiarColor(ConsoleColor.Red);
                Console.WriteLine("La opcion ingresada es inválida, vuelva a intentarlo...");
                Menu.ResetearColor();
                Console.Write("\n{0} [1-{1}]: ", mensajeIngreso, PC.Boxes.Length);
            }

            Console.WriteLine();
            return opcionSeleccionada - 1;
        }

        /// <summary>
        /// Valida si la box existe dentro de la PC
        /// </summary>
        /// <param name="valorIngresado"></param>
        /// <param name="opcionSeleccionada"></param>
        /// <returns></returns>
        static bool EsBoxValida(string valorIngresado, out int opcionSeleccionada)
        {
            if (int.TryParse(valorIngresado, out opcionSeleccionada))
            {
                if (opcionSeleccionada > 0 && opcionSeleccionada <= PC.Boxes.Length)
                    return true;
            }

            return false;
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
                Console.WriteLine("\nLa opcion ingresada no es válida, vuelva a intentarlo..\n");
                Menu.ResetearColor();
                Console.Write("Ingrese su opcion: ");
            }
            Console.WriteLine();

            return opcionSeleccionada;
        }

        static bool EsOpcionValida(ConsoleKeyInfo teclaIngresada, out OpcionesMenuBoxes menuSeleccionado)
        {
            menuSeleccionado = (OpcionesMenuBoxes)teclaIngresada.Key;

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
