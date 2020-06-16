using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;
using Logica;
using ConsolaUI.Tablas;

namespace ConsolaUI
{
    public static class SubMenuMostrarPorTipo
    {
        public static void Iniciar() 
        {
            bool seguirEnMenu = true;

            do
            {
                Menu.HeaderPrincipal();
                Menu.BannerMenu("Tipo de Pokemon");
                OpcionesMenu();

                int opcionSeleccionada = ValidarIngresoUsuario();

                if (opcionSeleccionada >= 0 && opcionSeleccionada <= Enum.GetNames(typeof(Tipo)).Length)
                {
                    Console.Clear();
                    MostrarPorTipo((Tipo)opcionSeleccionada);
                }
                else
                    seguirEnMenu = false;

                Menu.EspereUnaTecla();

            } while (seguirEnMenu);
            
        }


        static void MostrarPorTipo(Tipo tipo) 
        {
            LogicaBox box = new LogicaBox(LogicaPC.BoxSeleccionada);
            Pokemon[] pokemonPorTipo = box.ObtenerPorTipo(tipo);

            if (!MenuMostrarPokemon.Mostrar(pokemonPorTipo)) 
            {
                Menu.CambiarColor(ConsoleColor.Red);
                Console.WriteLine($"No hay pokemones capturados del tipo '{tipo}' en esta box.");
            }

        }

        static void OpcionesMenu() 
        {
            int cont = 0;
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("Seleccione un tipo de pokemon: ");
            foreach (string tipoPokemon in Enum.GetNames(typeof(Tipo)))
            {
                sb.AppendLine(string.Format("{0}. {1}", cont++, tipoPokemon));
            }

            Console.WriteLine(sb);
        }

        static int ValidarIngresoUsuario() 
        {
            int opcionSeleccionada;

            Console.Write("Ingrese su opcion: ");
            while (!EsOpcionValida(Console.ReadLine(), out opcionSeleccionada))
            {
                Menu.CambiarColor(ConsoleColor.Red);
                Console.WriteLine("\nLa opcion ingresada no es válida, vuelva a intentarlo..");
                Menu.ResetearColor();
                Console.Write("Ingrese su opcion: ");
            }
            Console.WriteLine();

            return opcionSeleccionada;

        }

        static bool EsOpcionValida(string valorIngresado, out int opcionSeleccionada) 
        {
            if (int.TryParse(valorIngresado, out opcionSeleccionada))
            {
                if (opcionSeleccionada >= 0 && opcionSeleccionada <= Enum.GetNames(typeof(Tipo)).Length)
                    return true;
            }

            return false;
        }

    }
}
