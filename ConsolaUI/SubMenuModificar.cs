using Entidades;
using Logica;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace ConsolaUI
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

            Menu.HeaderPrincipal();
            LogicaBox box = new LogicaBox(LogicaPC.BoxSeleccionada);
            string nuevoNombre;

            try
            {
                Pokemon pokemon = box.ObtenerPokemon(Validacion.ValidarId());
                
                if (pokemon is Huevo)
                {
                    Menu.CambiarColor(ConsoleColor.Red);
                    Console.WriteLine("No se pueden modificar el nombre de un huevo");
                }
                else
                {
                    nuevoNombre = Validacion.ValidarNombre();
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

            Menu.ResetearColor();
            Menu.EspereUnaTecla();

        }

        static void CambiarAtaquesPokemon()
        {
            Menu.HeaderPrincipal();
            LogicaBox box = new LogicaBox(LogicaPC.BoxSeleccionada);
            string[] nuevosAtaques = new string[Pokemon.LimiteAtaques];

            try
            {
                Pokemon pokemon = box.ObtenerPokemon(Validacion.ValidarId());
                
                if (pokemon is Huevo)
                {
                    Menu.CambiarColor(ConsoleColor.Red);
                    Console.WriteLine("No se pueden modificar los ataques de un huevo");
                }
                else
                {
                    nuevosAtaques = Validacion.ValidarAtaques();
                    box.CambiarAtaques(pokemon, nuevosAtaques);
                    Menu.CambiarColor(ConsoleColor.Yellow);
                    Console.WriteLine("El moveset del pokemon ha sido modificado");
                }

            }
            catch (NoExistePokemonException e)
            {
                Menu.CambiarColor(ConsoleColor.Red);
                Console.WriteLine(e.Message);
            }

            Menu.ResetearColor();
            Menu.EspereUnaTecla();

        }

        static void CambiarItemPokemon()
        {
            Menu.HeaderPrincipal();
            LogicaBox box = new LogicaBox(LogicaPC.BoxSeleccionada);

            try
            {
                Pokemon pokemon = box.ObtenerPokemon(Validacion.ValidarId());

                if (pokemon is Huevo)
                {
                    Menu.CambiarColor(ConsoleColor.Red);
                    Console.WriteLine("No se pueden cambiar el item de un huevo");
                }
                else
                {
                    box.CambiarStatusItem(pokemon);
                    Menu.CambiarColor(ConsoleColor.Yellow);
                    Console.WriteLine("El item del pokemon se modifico");
                }

            }
            catch (NoExistePokemonException e)
            {
                Menu.CambiarColor(ConsoleColor.Red);
                Console.WriteLine(e.Message);
            }

            Menu.ResetearColor();
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
