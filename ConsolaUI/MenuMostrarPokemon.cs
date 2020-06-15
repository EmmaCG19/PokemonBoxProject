using ConsolaUI.Tablas;
using Entidades;
using Logica;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsolaUI
{
    public static class MenuMostrarPokemon
    {
        public static void Iniciar()
        {
            Menu.HeaderPrincipal();
            Menu.BannerMenu("Mostrar Pokemones");
            Menu.CambiarColor(LogicaPC.BoxSeleccionada.Fondo);

            OpcionesMenu();

            switch (ValidarIngresoUsuario())
            {
                case OpcionesMenuMostrar.Todos:
                    MostrarTodos();
                    break;
                case OpcionesMenuMostrar.PorTipo:
                    break;
                case OpcionesMenuMostrar.PorNroDex:
                    break;
                case OpcionesMenuMostrar.PorRangoNivel:
                    break;
                case OpcionesMenuMostrar.PorPokebola:
                    break;
                case OpcionesMenuMostrar.Huevos:
                    break;
                default:
                    break;
            }
        }

        static void MostrarTodos()
        {
            LogicaBox box = new LogicaBox(LogicaPC.BoxSeleccionada);
            Pokemon[] pokemonesCapturados = box.ObtenerTodosLosCapturados();

            Menu.HeaderPrincipal();
            if (pokemonesCapturados.Length != 0)
            {
                TablaPokemon.GenerarTabla(pokemonesCapturados);
            }
            else
            {
                Menu.CambiarColor(ConsoleColor.Red);
                Console.WriteLine("No hay pokemones capturados en esta box");
            }

        }

        static void OpcionesMenu()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("1. Mostrar todos");
            sb.AppendLine("2. Mostrar por tipo");
            sb.AppendLine("3. Mostrar por nroDex");
            sb.AppendLine("4. Mostrar por rango de nivel");
            sb.AppendLine("5. Mostrar por pokebola");
            sb.AppendLine("6. Mostrar huevos");
            sb.AppendLine();

            Console.WriteLine(sb);
        }

        static OpcionesMenuMostrar ValidarIngresoUsuario()
        {
            OpcionesMenuMostrar opcionSeleccionada;

            Console.Write("Ingrese su opcion: ");
            while (!EsOpcionValida(Console.ReadKey(), out opcionSeleccionada))
            {
                Menu.CambiarColor(ConsoleColor.Red);
                Console.WriteLine("\nLa opcion ingresada no es válida, vuelva a intentarlo..");
                Menu.ResetearColor();
                Console.Write("Ingrese su opcion: ");
            }
            Console.WriteLine();

            return opcionSeleccionada;

        }

        static bool EsOpcionValida(ConsoleKeyInfo teclaIngresada, out OpcionesMenuMostrar menuSeleccionado)
        {
            menuSeleccionado = (OpcionesMenuMostrar)teclaIngresada.Key;

            //El usuario presiona una tecla y esta tiene que coincidir con las disponibles
            switch (menuSeleccionado)
            {
                case OpcionesMenuMostrar.Todos:
                case OpcionesMenuMostrar.PorTipo:
                case OpcionesMenuMostrar.PorNroDex:
                case OpcionesMenuMostrar.PorRangoNivel:
                case OpcionesMenuMostrar.PorPokebola:
                case OpcionesMenuMostrar.Huevos:
                    return true;
                default:
                    return false;
            }

        }

        enum OpcionesMenuMostrar
        {
            Todos = ConsoleKey.NumPad1,
            PorTipo = ConsoleKey.NumPad2,
            PorNroDex = ConsoleKey.NumPad3,
            PorRangoNivel = ConsoleKey.NumPad4,
            PorPokebola = ConsoleKey.NumPad5,
            Huevos = ConsoleKey.NumPad6
            //Ordenados por nivel
        }

    }
}
