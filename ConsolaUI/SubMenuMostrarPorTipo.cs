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
                MenuBox.ResetearFondo();
                OpcionesMenu();

                int opcionSeleccionada = ValidarIngresoUsuario();

                if (opcionSeleccionada >= 1 && opcionSeleccionada < Enum.GetNames(typeof(Tipo)).Length)
                {
                    Console.Clear();
                    MostrarPorTipo((Tipo)opcionSeleccionada);
                }
                else 
                    seguirEnMenu = false;


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

            Menu.EspereUnaTecla();

        }

        static void OpcionesMenu()
        {
            int cont = 0;

            Console.WriteLine("A) Seleccione un tipo de pokemon: ");
            Menu.CambiarColor(ConsoleColor.Green);
            foreach (string tipoPokemon in Enum.GetNames(typeof(Tipo)))
            {

                if (cont != 0)
                    Console.WriteLine("{0}{1}. {2}", Menu.Identar(3), cont, tipoPokemon);
                cont++;
            }
            
            MenuBox.ResetearFondo();
            Console.WriteLine("{0}. Volver al menú mostrar", cont);
        }

        static int ValidarIngresoUsuario()
        { 
            int opcionSeleccionada;

            Console.Write("Ingrese su opcion: ");
            while (!EsOpcionValida(Console.ReadLine(), out opcionSeleccionada))
            {
                Menu.CambiarColor(ConsoleColor.Red);
                Console.WriteLine("La opcion ingresada no es válida, vuelva a intentarlo..\n");
                MenuBox.ResetearFondo();
                Console.Write("Ingrese su opcion: ");
            }
            Console.WriteLine();

            return opcionSeleccionada;

        }

        static bool EsOpcionValida(string valorIngresado, out int opcionSeleccionada)
        {
            if (int.TryParse(valorIngresado, out opcionSeleccionada))
            {
                if (opcionSeleccionada >= 1 && opcionSeleccionada <= Enum.GetNames(typeof(Tipo)).Length)
                    return true;
            }

            return false;
        }

    }
}
