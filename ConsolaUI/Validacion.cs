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

        /// <summary>
        /// Valida que el ingreso sea un numero de pokedex válido
        /// </summary>
        /// <returns>Nro de dex válido</returns>
        public static short ValidarNroDex()
        {
            short dexValida;

            Console.Write($"\nIngrese un nro de dex [1-{PC.PokemonesEnDex}]: ");
            while (!short.TryParse(Console.ReadLine(), out dexValida) || (dexValida < 1 || dexValida > PC.PokemonesEnDex))
            {
                Menu.CambiarColor(ConsoleColor.Red);
                Console.WriteLine("El nro de dex es invalido, vuelva a intentarlo...");
                Menu.ResetearColor();

                Console.Write($"\nIngrese un nro de dex [1-{PC.PokemonesEnDex}]: ");
            }

            return dexValida;
        }

        /// <summary>
        /// Valida que la posicion ingresada se encuentre dentro de la box
        /// </summary>
        /// <returns></returns>
        public static int ValidarId()
        {
            int posValida;

            Console.Write($"\nIngrese una posicion de la box [1-{Box.Capacidad}]: ");
            while (!int.TryParse(Console.ReadLine(), out posValida) || (posValida < 1 || posValida > Box.Capacidad))
            {
                Menu.CambiarColor(ConsoleColor.Red);
                Console.WriteLine("La posicion ingresada es inválida, vuelva a intentarlo...");
                Menu.ResetearColor();

                Console.Write($"\nIngrese un nro de dex [1-{Box.Capacidad}]: ");
            }

            //Debo reducir en uno la posicion para que coincida con el array
            return --posValida;
        }

        /// <summary>
        /// Valida y retorna un nivel que sea valido
        /// </summary>
        /// <param name="mensajeIngreso"></param>
        /// <returns></returns>
        public static byte ValidarNivel(string mensajeIngreso = "\nIngresa un nivel [1-100] : ")
        {
            byte nivelValido;
            Console.Write(mensajeIngreso);

            while (!byte.TryParse(Console.ReadLine(), out nivelValido) || (nivelValido < 1 || nivelValido > PC.NivelMaximo))
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

            Console.Write("\nIngrese el nombre: ");
            nombreIngresado = Console.ReadLine();

            while (string.IsNullOrEmpty(nombreIngresado.Trim()))
            {
                Menu.CambiarColor(ConsoleColor.Red);
                Console.WriteLine("El nombre ingresado es invalido, vuelva a intentarlo...");
                Menu.ResetearColor();
                Console.Write("\nIngrese el nombre: ");
                nombreIngresado = Console.ReadLine();
            }

            return nombreIngresado;
        }

        /// <summary>
        /// Valida el tipo del pokemon seleccionado por el usuario
        /// </summary>
        /// <returns></returns>
        public static Tipo ValidarTipo()
        {
            Console.WriteLine("Tipos de pokemon: ");
            OpcionesTipo();

            Console.Write("\nSeleccione una opcion: ");

            Tipo tipoSeleccionado;

            //No vamos a tener en cuenta el tipo 'No asignado'. Es unicamente para los huevos.
            while (!Enum.TryParse<Tipo>(Console.ReadLine(), out tipoSeleccionado) || !Enum.IsDefined(typeof(Tipo), tipoSeleccionado) || tipoSeleccionado == 0)
            {
                Menu.CambiarColor(ConsoleColor.Red);
                Console.WriteLine("El tipo seleccionado no es valido, vuelva a intentarlo");
                Menu.ResetearColor();
                Console.Write("Seleccione una opcion: ");
            }

            return tipoSeleccionado;
        }

        /// <summary>
        /// Valida el genero del pokemon seleccionado por el usuario
        /// </summary>
        /// <returns></returns>
        public static Genero ValidarGenero()
        {
            Console.WriteLine("Tipos de género: ");
            OpcionesGenero();


            Genero generoSeleccionado;
            Console.Write("Seleccione una opcion: ");

            //Existen pokemones que no tienen Genero. Por ejemplo, Ditto.
            while (!Enum.TryParse<Genero>(Console.ReadLine(), out generoSeleccionado) || !Enum.IsDefined(typeof(Genero), generoSeleccionado))
            {
                Menu.CambiarColor(ConsoleColor.Red);
                Console.WriteLine("El tipo seleccionado no es valido, vuelva a intentarlo");
                Menu.ResetearColor();
                Console.Write("\nSeleccione una opcion: ");
            }

            return generoSeleccionado;
        }

        /// <summary>
        /// Valida la pokebola con la que se atrapo el pokemon seleccionado por el usuario
        /// </summary>
        /// <returns></returns>
        public static Pokebola ValidarPokebola()
        {

            Console.WriteLine("\nTipos de Pokebola: ");
            OpcionesPokebola();

            Console.Write("Seleccione una opcion: ");

            Pokebola pokebolaSeleccionada;

            //No vamos a tener en cuenta la pokebola 'No asignada'. Es unicamente para los huevos.
            while (!Enum.TryParse<Pokebola>(Console.ReadLine(), out pokebolaSeleccionada) || !Enum.IsDefined(typeof(Pokebola), pokebolaSeleccionada) || pokebolaSeleccionada == 0)

            {
                Menu.CambiarColor(ConsoleColor.Red);
                Console.WriteLine("El tipo seleccionado no es valido, vuelva a intentarlo");
                Menu.ResetearColor();
                Console.Write("\nSeleccione una opcion: ");
            }

            return pokebolaSeleccionada;
        }

        /// <summary>
        /// Valida la existencia de un item en el pokemon
        /// </summary>
        /// <returns></returns>
        public static bool ValidarItem()
        {
            ConsoleKeyInfo teclaPresionada;
            bool tieneItem = false;

            do
            {
                Console.Write("\nTiene un item equipado? S/N: ");
                teclaPresionada = Console.ReadKey();

                if (teclaPresionada.Key == ConsoleKey.S)
                    tieneItem = true;
                else if (teclaPresionada.Key == ConsoleKey.N)
                    tieneItem = false;
                else
                {
                    Menu.CambiarColor(ConsoleColor.Red);
                    Console.WriteLine("La tecla ingresada es inválida, vuelva a intentarlo...");
                    Menu.ResetearColor();
                }

                Console.WriteLine();

            } while (teclaPresionada.Key != ConsoleKey.S && teclaPresionada.Key != ConsoleKey.N);

            return tieneItem;
        }

        /// <summary>
        /// Valida los ataques ingresados por el usuario
        /// </summary>
        /// <returns>Un array con los ataques cargados</returns>
        public static string[] ValidarAtaques()
        {
            string[] ataquesIngresados = new string[4];
            Funciones.InicializarArrayString(ataquesIngresados);

            bool finalizarCarga = false;
            

            for (int i = 0; i < ataquesIngresados.Length; i++)
            {
                #region Ingreso de ataques
                string ataqueIngresado;

                Console.Write($"Ingrese el ataque {i + 1}/4: ");
                ataqueIngresado = Console.ReadLine();

                # region Validar nombre ataque
                while (string.IsNullOrEmpty(ataqueIngresado.Trim()))
                {
                    Menu.CambiarColor(ConsoleColor.Red);
                    Console.WriteLine("El ataque ingresado no es valido, vuelva a intentarlo...");
                    Menu.ResetearColor();

                    Console.Write($"Ingrese el ataque {i + 1}/4: ");
                    ataqueIngresado = Console.ReadLine();
                }
                #endregion

                ataquesIngresados[i] = ataqueIngresado;
                #endregion

                #region Se le pregunta al usuario si desea seguir agregando
                ConsoleKeyInfo respuesta;
                bool seguirPreguntando;
                do
                {
                    seguirPreguntando = false;
                    Console.Write("\nDesea seguir agregando ataques? [S/N]: ");
                    respuesta = Console.ReadKey();

                    if (respuesta.Key == ConsoleKey.N || i == ataquesIngresados.Length - 1)
                    {
                        Menu.CambiarColor(ConsoleColor.Yellow);
                        Console.WriteLine("\nCarga de ataques finalizada");
                        finalizarCarga = true;
                        break;
                    }
                    else if (respuesta.Key != ConsoleKey.S)
                    {
                        Menu.CambiarColor(ConsoleColor.Red);
                        Console.WriteLine("\nLa tecla presionada es invalida, vuelva a intentarlo..");
                        seguirPreguntando = true;
                    }

                    Menu.ResetearColor();

                } while (seguirPreguntando);
                #endregion

                Console.WriteLine();
             
                //Dejar de cargar ataques
                if (finalizarCarga)
                    break;
            }

            return ataquesIngresados;
        }

        /// <summary>
        /// Valida el entrenador que capturo el pokemon. Puede ser el actual u otro que le intercambio el pokemon.
        /// </summary>
        /// <returns></returns>
        public static Entrenador ValidarEntrenador()
        {
            Entrenador trainer = new Entrenador();

            ConsoleKeyInfo teclaPresionada;
            bool teclaInvalida = false;

            do
            {
                Console.Write("\nEl pokemon fue capturado por el entrenador actual? [S/N] : ");
                teclaPresionada = Console.ReadKey();

                if (teclaPresionada.Key == ConsoleKey.S)
                {
                    //El entrenador es Red
                    trainer = PC.Jugador;

                }
                else if (teclaPresionada.Key == ConsoleKey.N)
                {
                    //Pedir datos del entrenador que lo capturo
                    Random rnd = new Random();

                    trainer.Id = rnd.Next(15000, 25000);
                    trainer.NombreOT = ValidarNombre();
                }
                else
                {
                    //Ingreso invalido
                    teclaInvalida = true;
                    Menu.CambiarColor(ConsoleColor.Red);
                    Console.WriteLine("La tecla ingresada es inválida, vuelva a intentarlo...");
                    Menu.ResetearColor();
                }

            } while (teclaInvalida);

            return trainer;
        }

        /// <summary>
        /// Valida la cantidad de pasos ingresados que requiere un huevo para eclosionar
        /// </summary>
        /// <returns></returns>
        public static short ValidarCantPasos()
        {
            short cantPasos;
            Console.Write("\nIngrese la cantidad de pasos: ");

            while (!short.TryParse(Console.ReadLine(), out cantPasos) || cantPasos < 1)
            {
                Menu.CambiarColor(ConsoleColor.Red);
                Console.WriteLine("La cantidad de pasos es inválida, vuelva a ingresarla...");
                Menu.ResetearColor();
                Console.Write("\nIngrese la cantidad de pasos: ");

            }

            return cantPasos;

        }

        static void OpcionesTipo()
        {
            StringBuilder sb = new StringBuilder();
            var tiposDisponibles = Enum.GetNames(typeof(Tipo));

            for (int i = 1; i < tiposDisponibles.Length; i++)
            {
                sb.AppendLine(string.Format("{2}{0}.{1}", i, tiposDisponibles[i], Menu.Identar(3)));
            }

            Console.WriteLine(sb);
        }

        static void OpcionesPokebola()
        {
            StringBuilder sb = new StringBuilder();
            var pokebolasDisponibles = Enum.GetNames(typeof(Pokebola));

            for (int i = 1; i < pokebolasDisponibles.Length; i++)
            {
                sb.AppendLine(string.Format("{2}{0}.{1}", i, pokebolasDisponibles[i], Menu.Identar(3)));
            }

            Console.WriteLine(sb);
        }

        static void OpcionesGenero()
        {
            StringBuilder sb = new StringBuilder();
            var generos = Enum.GetNames(typeof(Genero));

            for (int i = 0; i < generos.Length; i++)
            {
                sb.AppendLine(string.Format("{2}{0}.{1}", i, generos[i], Menu.Identar(3)));
            }

            Console.WriteLine(sb);
        }


    }
}
