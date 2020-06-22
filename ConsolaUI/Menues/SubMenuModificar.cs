using Entidades;
using Logica;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using ConsolaUI.Utilidades;

namespace ConsolaUI.Menues
{
    public static class SubMenuModificar
    {
        public static void Iniciar()
        {
            bool seguirEnMenu = true;

            do
            {
                Menu.HeaderPrincipal();
                Menu.BannerMenu("Menu Modificar Pokemon");
                MenuBox.ResetearFondo();

                OpcionesMenu();

                switch (ValidarIngresoUsuario())
                {
                    case OpcionesMenuModificar.Nombre:
                        CambiarNombrePokemon();
                        break;
                    case OpcionesMenuModificar.Ataques:
                        CambiarAtaquesPokemon();
                        break;
                    case OpcionesMenuModificar.Item:
                        CambiarItemPokemon();
                        break;
                    case OpcionesMenuModificar.Volver:
                        seguirEnMenu = false;
                        break;
                    default:
                        break;
                }

            } while (seguirEnMenu);

        }

        static void CambiarNombrePokemon()
        {

            LogicaBox box = new LogicaBox(LogicaPC.BoxSeleccionada);
            string nuevoNombre;
            
            Menu.HeaderPrincipal();
            MenuBox.ResetearFondo();

            try
            {
                Pokemon pokemon = box.ObtenerPokemon(Validaciones.ValidarId());

                if (pokemon is Huevo)
                {
                    Menu.CambiarColor(ConsoleColor.Red);
                    Console.WriteLine("No se pueden modificar el nombre de un huevo");
                }
                else
                {
                    nuevoNombre = Validaciones.ValidarCadena("Ingrese el nuevo nombre del pokemon");
                    box.CambiarNombre(pokemon, nuevoNombre);
                    Menu.CambiarColor(ConsoleColor.Yellow);
                    Console.WriteLine("El nombre ha sido modificado");
                }

            }
            catch (NoExistePokemonException e)
            {
                Menu.CambiarColor(ConsoleColor.Red);
                Console.WriteLine(e.Message);
            }

            Menu.EspereUnaTecla();

        }

        static void CambiarAtaquesPokemon()
        {
            LogicaBox box = new LogicaBox(LogicaPC.BoxSeleccionada);
            string[] nuevosAtaques;

            Menu.HeaderPrincipal();
            MenuBox.ResetearFondo();

            try
            {
                nuevosAtaques = new string[Pokemon.LimiteAtaques];
                Pokemon pokemon = box.ObtenerPokemon(Validaciones.ValidarId());

                if (pokemon is Huevo)
                {
                    Menu.CambiarColor(ConsoleColor.Red);
                    Console.WriteLine("\nNo se pueden modificar los ataques de un huevo");
                }
                else
                {
                    nuevosAtaques = Validaciones.ValidarAtaques();
                    box.CambiarAtaques(pokemon, nuevosAtaques);
                    Menu.CambiarColor(ConsoleColor.Yellow);
                    Console.WriteLine("\nEl moveset del pokemon ha sido modificado");
                }

            }
            catch (NoExistePokemonException e)
            {
                Menu.CambiarColor(ConsoleColor.Red);
                Console.WriteLine(e.Message);
            }

            Menu.EspereUnaTecla();

        }

        static void CambiarItemPokemon()
        {
            LogicaBox box = new LogicaBox(LogicaPC.BoxSeleccionada);
            
            Menu.HeaderPrincipal();
            MenuBox.ResetearFondo();

            try
            {
                Pokemon pokemon = box.ObtenerPokemon(Validaciones.ValidarId());

                if (pokemon is Huevo)
                {
                    Menu.CambiarColor(ConsoleColor.Red);
                    Console.WriteLine("\nNo se pueden cambiar el item de un huevo");
                }
                else
                {
                    box.CambiarStatusItem(pokemon);
                    Menu.CambiarColor(ConsoleColor.Yellow);
                    Console.WriteLine("\nEl item del pokemon se modifico");
                }

            }
            catch (NoExistePokemonException e)
            {
                Menu.CambiarColor(ConsoleColor.Red);
                Console.WriteLine(e.Message);
            }

            Menu.EspereUnaTecla();

        }

        /// <summary>
        /// Valida la opcion ingresada por el usuario en el Menu Boxes
        /// </summary>
        /// <returns></returns>
        static OpcionesMenuModificar ValidarIngresoUsuario()
        {
            OpcionesMenuModificar opcionSeleccionada;

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

        static bool EsOpcionValida(ConsoleKeyInfo teclaIngresada, out OpcionesMenuModificar menuSeleccionado)
        {
            menuSeleccionado = (OpcionesMenuModificar)teclaIngresada.Key;

            //El usuario presiona una tecla y esta tiene que coincidir con las disponibles
            switch (menuSeleccionado)
            {
                case OpcionesMenuModificar.Nombre:
                case OpcionesMenuModificar.Ataques:
                case OpcionesMenuModificar.Item:
                case OpcionesMenuModificar.Volver:
                    return true;
                default:
                    return false;
            }
        }

        static void OpcionesMenu()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("1. Modificar nombre");
            sb.AppendLine("2. Modificar ataques");
            sb.AppendLine("3. Modificar Item");
            sb.AppendLine($"4. Volver al menu de '{LogicaPC.BoxSeleccionada.Nombre.ToLower()}'");

            Console.WriteLine(sb.ToString());
        }

        enum OpcionesMenuModificar
        {
            Nombre = ConsoleKey.NumPad1,
            Ataques = ConsoleKey.NumPad2,
            Item = ConsoleKey.NumPad3,
            Volver = ConsoleKey.NumPad4,
        }

    }
}
