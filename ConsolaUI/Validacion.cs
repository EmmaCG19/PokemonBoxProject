using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logica;
using Entidades;

namespace ConsolaUI
{
    public static class Validacion
    {
        static readonly short _limitePokedex;
        static readonly byte _nivelMaximo;

        static Validacion()
        {
            _limitePokedex = 151;
            _nivelMaximo = 100;
        }

        public static short LimitePokedex { get { return _limitePokedex; } }
        public static byte NivelMaximo { get { return _nivelMaximo; } }

        /// <summary>
        /// Valida que el ingreso sea un numero de pokedex válido
        /// </summary>
        /// <returns>Nro de dex válido</returns>
        public static int ValidarNroDex()
        {
            short dexValida;

            Console.Write($"Ingrese un nro de dex [1-{LimitePokedex}]: ");
            while (!short.TryParse(Console.ReadLine(), out dexValida) || (dexValida < 1 || dexValida > LimitePokedex))
            {
                Menu.CambiarColor(ConsoleColor.Red);
                Console.WriteLine("El nro de dex es invalido, vuelva a intentarlo...");
                Menu.ResetearColor();

                Console.Write($"Ingrese un nro de dex [1-{LimitePokedex}]: ");
            }

            return dexValida;
        }

        public static int ValidarId()
        {
            int posValida;

            Console.Write($"Ingrese una posicion de la box [1-{Box.Capacidad}]: ");
            while (!int.TryParse(Console.ReadLine(), out posValida) || (posValida < 1  || posValida > Box.Capacidad))
            {
                Menu.CambiarColor(ConsoleColor.Red);
                Console.WriteLine("La posicion ingresada es inválida, vuelva a intentarlo...");
                Menu.ResetearColor();

                Console.Write($"Ingrese un nro de dex [1-{Box.Capacidad}]: ");
            }

            //Debo reducir en uno la posicion para que coincida con el array
            return --posValida;
        }
        

        public static byte ValidarNivel(string mensajeIngreso = "Ingresa un nivel [1-151] : ") 
        {
            byte nivelValido;
            Console.Write(mensajeIngreso);

            while (!byte.TryParse(Console.ReadLine(), out nivelValido) || (nivelValido < 1 || nivelValido > NivelMaximo))
            {
                Menu.CambiarColor(ConsoleColor.Red);
                Console.WriteLine("El nivel ingresado es invalido, vuelva a intentarlo...");
                Menu.ResetearColor();
                Console.Write(mensajeIngreso);
            }

            return nivelValido;
        }

        /// <summary>
        /// Valida que el ingreso sea un nombre válido
        /// </summary>
        /// <returns>Nombre válido</returns>
        public static string ValidarNombre() 
        {
            string nombreIngresado;

            Console.Write("Ingrese el nombre: ");
            nombreIngresado = Console.ReadLine();

            while (string.IsNullOrEmpty(nombreIngresado.Trim())) 
            {
                Menu.CambiarColor(ConsoleColor.Red);
                Console.WriteLine("El nombre ingresado es invalido, vuelva a intentarlo...");
                Menu.ResetearColor();
                Console.Write("Ingrese el nombre: ");
                nombreIngresado = Console.ReadLine();
            }

            return nombreIngresado;
        }

    }
}
