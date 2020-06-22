using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;
using Logica;
using ConsolaUI.Tablas;
using ConsolaUI.Utilidades;

namespace ConsolaUI.Menues
{
    class SubMenuMostrarPorPokebola
    {
        public static void Iniciar()
        {
            bool seguirEnMenu = true;

            do
            {
                Menu.HeaderPrincipal();
                Menu.BannerMenu("Tipo de Pokebola");
                MenuBox.ResetearFondo();
                OpcionesMenu();

                int opcionSeleccionada = ValidarIngresoUsuario();

                if (opcionSeleccionada >= 1 && opcionSeleccionada < Enum.GetNames(typeof(Pokebola)).Length)
                {
                    Console.Clear();
                    MostrarPorPokebola((Pokebola)opcionSeleccionada);
                }
                else
                    seguirEnMenu = false;


            } while (seguirEnMenu);

        }


        static void MostrarPorPokebola(Pokebola pokebola)
        {
            LogicaBox box = new LogicaBox(LogicaPC.BoxSeleccionada);
            Pokemon[] pokemonPorPokebola = box.ObtenerPorPokebola(pokebola);

            if (!MenuMostrarPokemon.Mostrar(pokemonPorPokebola))
            {
                Menu.CambiarColor(ConsoleColor.Red);
                Console.WriteLine($"No hay pokemones capturados con la '{pokebola.ToString().ToLower()}' en esta box.");
            }

            Menu.EspereUnaTecla();
        }

        static void OpcionesMenu()
        {
            int cont = 0;

            Console.WriteLine("Seleccione un tipo de pokebola: ");
            Menu.CambiarColor(ConsoleColor.Green);
            foreach (string tipoPokebola in Enum.GetNames(typeof(Pokebola)))
            {
                if (cont != 0)
                    Console.WriteLine("{0}{1}. {2}", Menu.Identar(3), cont, tipoPokebola);
                cont++;
            }

            MenuBox.ResetearFondo();
            Console.WriteLine("{0}. Volver al menú mostrar\n", cont);
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
                //Tengo en cuenta la opcion extra para volver al menu anterior
                if (opcionSeleccionada >= 1 && opcionSeleccionada <= Enum.GetNames(typeof(Pokebola)).Length)
                    return true;
            }

            return false;
        }


    }
}
