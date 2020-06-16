using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logica;

namespace ConsolaUI
{
    public static class Validacion
    {
        static Validacion()
        {
            limitePokedex = 151;
        }

        //Esta clase estática dispondrá de métodos para poder validar cada uno de los datos del pokemon ingresados por el usuario
        static readonly int limitePokedex;

        /// <summary>
        /// Valida que el ingreso sea un numero de pokedex válido
        /// </summary>
        /// <returns>Nro de dex válido</returns>
        public static int ValidarNroDex()
        {
            int dexIngresado;
            Console.Write("Ingrese un nro de dex [1-151]: ");

            while (!int.TryParse(Console.ReadLine(), out dexIngresado) || (dexIngresado < 1 || dexIngresado > limitePokedex))
            {
                Menu.CambiarColor(ConsoleColor.Red);
                Console.WriteLine("La opcion ingresada es invalida, vuelva a intentarlo...");
                Menu.ResetearColor();
                Console.Write("Ingrese un nro de dex [1-151]: ");
            }

            return dexIngresado;
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
