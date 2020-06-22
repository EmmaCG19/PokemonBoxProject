﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;

namespace ConsolaUI
{
    public static class Menu
    {
        static Menu()
        {
            Console.Title = PC.Titulo.ToUpper();
            _colorMenu = ConsoleColor.Magenta;
            largoFila = 65;
        }

        //Propiedad largo de fila
        public static readonly int largoFila;
        static readonly ConsoleColor _colorMenu;

        /// <summary>
        /// Cambiar el color del menu al valor establecido inicialmente.
        /// </summary>
        public static void ResetearColor()
        {
            Console.ForegroundColor = _colorMenu;
        }

        /// <summary>
        /// Cambia el color del tipado del menu
        /// </summary>
        /// <param name="color"></param>
        public static void CambiarColor(ConsoleColor color)
        {
            Console.ForegroundColor = color;
        }

        public static void HeaderPrincipal()
        {
            Console.Clear();
            Menu.CambiarColor(ConsoleColor.Yellow);
            Console.WriteLine(InfoJugador());
            Console.WriteLine(LineaFormateada(Menu.largoFila, '-'));
            ResetearColor();
            Console.WriteLine(); 
        }

        public static string InfoJugador() 
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("Player: {0,-29}Time: {1}", PC.Jugador.NombreOT, DateTime.Now.ToLocalTime());
            return sb.ToString();
        }

        public static void BannerMenu(string nombreDelMenu)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;

            int largoFilaPalabra = largoFila - nombreDelMenu.Length;
            string lineaLadoDer;
            string lineaLadoIzq;

            //Corregir cantidas de dots de cada lado 
            if (largoFilaPalabra % 2 == 0)
            {
                lineaLadoIzq = LineaFormateada(largoFilaPalabra / 2, '*');
                lineaLadoDer = LineaFormateada(largoFilaPalabra / 2, '*');
            }
            else
            {
                lineaLadoIzq = LineaFormateada(largoFilaPalabra / 2, '*');
                lineaLadoDer = LineaFormateada((largoFilaPalabra / 2 - 1), '*');
            }

            Console.WriteLine(LineaFormateada(largoFila, '*'));
            Console.WriteLine(LineaFormateada(largoFila, '*'));
            Console.WriteLine("{0} {1} {2}", lineaLadoIzq, nombreDelMenu.ToUpper(), lineaLadoDer);
            Console.WriteLine(LineaFormateada(largoFila, '*'));
            Console.WriteLine(LineaFormateada(largoFila, '*'));
            Console.WriteLine();
            Menu.ResetearColor();
        }


        public static string LineaFormateada(int longitud, char simbolo)
        {
            StringBuilder sb = new StringBuilder();

            //Tener en cuenta los espacios
            for (int i = 0; i < longitud - 2; i++)
            {
                sb.Append(simbolo);
            }

            return sb.ToString();
        }

        public static string Identar(int espacios)
        {
            return "".PadLeft(espacios);
        }

        public static void EspereUnaTecla() 
        {
            Console.WriteLine();
            CambiarColor(ConsoleColor.Cyan);
            Console.Write("Presione una tecla para continuar...");
            Console.ReadLine();
            Console.Clear();
        }
    }
}


