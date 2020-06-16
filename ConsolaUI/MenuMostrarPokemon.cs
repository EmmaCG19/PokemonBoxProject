﻿using ConsolaUI.Tablas;
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
            bool seguirEnMenu = true;
            
            do
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
                        SubMenuMostrarPorTipo.Iniciar();
                        break;
                    case OpcionesMenuMostrar.PorNroDex:
                        MostrarPorNroDex();
                        break;
                    case OpcionesMenuMostrar.PorRangoNivel:
                        break;
                    case OpcionesMenuMostrar.PorPokebola:
                        break;
                    case OpcionesMenuMostrar.Huevos:
                        break;
                    case OpcionesMenuMostrar.MenuBox:
                        seguirEnMenu = false;
                        break;
                }

            } while (seguirEnMenu);

        }

        /// <summary>
        /// Muestra en una tabla los pokemones pasados por parámetro.
        /// </summary>
        /// <param name="pokemonesAMostrar"></param>
        /// <returns>Un indicador si se pudo o no mostrar la información</returns>
        public static bool Mostrar(Pokemon[] pokemonesAMostrar)
        {
            //Menu.HeaderPrincipal();
            if (pokemonesAMostrar.Length != 0)
            {
                TablaPokemon.GenerarTabla(pokemonesAMostrar);
                return true;
            }

            return false;
        }

        /// <summary>
        /// Muestra todos los pokemones capturados. Se informa si no hay capturados.
        /// </summary>
        static void MostrarTodos()
        {
            Menu.HeaderPrincipal();

            LogicaBox box = new LogicaBox(LogicaPC.BoxSeleccionada);
            Pokemon[] pokemonesCapturados = box.ObtenerTodosLosCapturados();

            if (!Mostrar(pokemonesCapturados))
            {
                Menu.CambiarColor(ConsoleColor.Red);
                Console.WriteLine("No hay pokemones capturados en esta box");
            }

            Menu.EspereUnaTecla();
        }

        static void MostrarPorNroDex()
        {
            Menu.HeaderPrincipal();
            
            LogicaBox box = new LogicaBox(LogicaPC.BoxSeleccionada);
            Pokemon[] pokemonConMismaDex = box.ObtenerPorNroPokedex(Validacion.ValidarNroDex());

            if (!Mostrar(pokemonConMismaDex))
            {
                Menu.CambiarColor(ConsoleColor.Red);
                Console.WriteLine("No hay pokemones con esa dex en esta box");
            }

            Menu.EspereUnaTecla();
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
            sb.AppendLine($"7. Volver al menu de {LogicaPC.BoxSeleccionada.Nombre.ToLower()}");
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
            Console.Clear();
            
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
                case OpcionesMenuMostrar.MenuBox:
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
            Huevos = ConsoleKey.NumPad6,
            MenuBox = ConsoleKey.NumPad7
        }

    }
}