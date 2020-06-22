using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logica;
using ConsolaUI.Tablas;
using Entidades.Excepciones;

namespace ConsolaUI
{
    public static class MenuBox
    {
        public static void Iniciar()
        {
            bool seguirEnMenu = true;

            do
            {
                Menu.HeaderPrincipal();
                Menu.BannerMenu(LogicaPC.BoxSeleccionada.Nombre);
                ResetearFondo();
                OpcionesMenu();

                switch (ValidarIngresoUsuario())
                {
                    case OpcionesMenuBox.Mostrar:
                        MenuMostrarPokemon.Iniciar();
                        break;
                    case OpcionesMenuBox.Buscar:
                        BuscarPokemon();
                        break;
                    case OpcionesMenuBox.Guardar:
                        GuardarPokemon();
                        break;
                    case OpcionesMenuBox.Liberar:
                        LiberarPokemon();
                        break;
                    case OpcionesMenuBox.Modificar:
                        SubMenuModificar.Iniciar();
                        break;
                    case OpcionesMenuBox.Mover:
                        MoverPokemon();
                        break;
                    case OpcionesMenuBox.CambiarNombreBox:
                        CambiarNombreBox();
                        break;
                    case OpcionesMenuBox.CambiarFondoBox:
                        CambiarFondoBox();
                        break;
                    case OpcionesMenuBox.VolverMenuBoxes:
                        seguirEnMenu = false;
                        break;
                    default:
                        break;
                }

            } while (seguirEnMenu);

        }

        /// <summary>
        /// Busca un pokemon en la box basandose en su Id y muestra el resultado
        /// </summary>
        static void BuscarPokemon()
        {
            Menu.HeaderPrincipal();
            ResetearFondo();

            LogicaBox box = new LogicaBox(LogicaPC.BoxSeleccionada);
            int pokemonId = Validacion.ValidarId();

            try
            {
                Pokemon pokemonEncontrado = box.ObtenerPokemon(pokemonId);
                TablaPokemon.GenerarTabla(pokemonEncontrado);

            }
            catch (NoExistePokemonException e)
            {
                Menu.CambiarColor(ConsoleColor.Red);
                Console.WriteLine(e.Message);
            }

            Menu.EspereUnaTecla();
        }

        /// <summary>
        /// Guarda el pokemon en un espacio de la box actual. En caso de no lograrse el guardado, muestra el mensaje de error.
        ///</summary>
        static void GuardarPokemon()
        {
            Menu.HeaderPrincipal();
            ResetearFondo();

            LogicaBox box = new LogicaBox(LogicaPC.BoxSeleccionada);
            string mensajeError = "El pokemon no puede ser guardado porque";

            try
            {
                #region Le pregunto al usuario si quiere guardar un huevo o un pokemon

                ConsoleKeyInfo teclaPresionada;
                bool teclaInvalida;

                do
                {
                    teclaInvalida = false;

                    Console.Write("El pokemon a guardar es un huevo? [S/N]: ");
                    teclaPresionada = Console.ReadKey();

                    if (teclaPresionada.Key == ConsoleKey.S)
                    {
                        //Pokemon huevo
                        short cantidadPasos = Validacion.ValidarCantPasos();
                        box.Guardar(new Huevo(cantidadPasos));
                        Menu.CambiarColor(ConsoleColor.Yellow);
                        Console.WriteLine("El huevo ha sido cargado");
                    }
                    else if (teclaPresionada.Key == ConsoleKey.N)
                    {
                        //Pokemon normal
                        box.Guardar(CargaDatosPokemon());
                        Menu.CambiarColor(ConsoleColor.Yellow);
                        Console.WriteLine("El pokemon ha sido cargado");
                    }
                    else
                    {
                        //Ingreso invalido
                        teclaInvalida = true;
                        Menu.CambiarColor(ConsoleColor.Red);
                        Console.WriteLine("La tecla ingresada es inválida, vuelva a intentarlo...\n");
                        Menu.ResetearColor();
                    }

                } while (teclaInvalida);
                #endregion

            }
            catch (BoxLlenaException e)
            {
                //Cargar en otra box disponible??
                Menu.CambiarColor(ConsoleColor.Red);
                Console.WriteLine("{0} {1}", mensajeError, e.Message.ToLower());
            }
            catch (MasterBallUnicaException e)
            {
                Menu.CambiarColor(ConsoleColor.Red);
                Console.WriteLine("{0} {1}", mensajeError, e.Message.ToLower());
            }
            catch (LegendarioUnicoException e)
            {
                Menu.CambiarColor(ConsoleColor.Red);
                Console.WriteLine("{0} {1}", mensajeError, e.Message.ToLower());
            }
            catch (NroDexSuperadoException e)
            {
                Menu.CambiarColor(ConsoleColor.Red);
                Console.WriteLine("{0} {1}", mensajeError, e.Message.ToLower());
            }

            Menu.ResetearColor();
            Menu.EspereUnaTecla();
        }

        /// <summary>
        /// Libera al pokemon, es decir, lo elimina de los pokemones listados en la box.
        /// </summary>
        static void LiberarPokemon()
        {
            Menu.HeaderPrincipal();
            ResetearFondo();

            LogicaBox box = new LogicaBox(LogicaPC.BoxSeleccionada);
            int idPokemon = Validacion.ValidarId();

            try
            {
                TablaPokemon.GenerarTabla(box.ObtenerPokemon(idPokemon));

                //Bonus: Agregar Confirmacion
                box.Liberar(idPokemon);
                Menu.CambiarColor(ConsoleColor.Yellow);
                Console.WriteLine("\nEl pokemon ha sido liberado");

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
        /// Le pide al usuario que ingrese los datos del pokemon a cargar y valida la informacion
        /// </summary>
        /// <returns>El pokemon cargado</returns>
        static Pokemon CargaDatosPokemon()
        {
            #region Se deben validar los datos y por ultimo instanciar un nuevo Pokemon con los campos ingresados

            //Cargamos el nroDex
            short nroDex = Validacion.ValidarNroDex();

            //Cargamos el nombre
            string nombre = Validacion.ValidarCadena("Ingrese el nombre del pokemon");

            //Cargamos el nivel
            byte nivel = Validacion.ValidarNivel();

            //Cargamos el tipo
            Tipo tipo = Validacion.ValidarTipo();

            //Cargamos la pokebola
            Pokebola pokebola = Validacion.ValidarPokebola();

            //Cargamos el genero
            Genero genero = Validacion.ValidarGenero();

            //Cargamos los ataques
            string[] ataques = Validacion.ValidarAtaques();

            //Cargamos el trainer
            Entrenador entrenador = Validacion.ValidarEntrenador();

            //Cargamos el item
            bool item = Validacion.ValidarItem();

            #endregion

            return new Pokemon(nroDex, nombre, nivel, tipo, genero, entrenador, pokebola, ataques, item);
        }

        /// <summary>
        /// Permite reubicar al pokemon en otra posicion de la BOX. Si no está ocupada la posicion, lo almacena ahi. En caso contrario, intercambia posiciones.
        /// </summary>
        static void MoverPokemon()
        {
            LogicaBox box = new LogicaBox(LogicaPC.BoxSeleccionada);

            Menu.HeaderPrincipal();
            ResetearFondo();

            try
            {
                Console.Write("Pokemon a mover: ");
                Pokemon pokemonAMover = box.ObtenerPokemon(Validacion.ValidarId());

                Console.Write("Posicion a donde desea moverlo: ");
                int posicionSeleccionada = Validacion.ValidarId();

                box.Mover(pokemonAMover, posicionSeleccionada);
                Menu.CambiarColor(ConsoleColor.Yellow);
                Console.WriteLine("El pokemon ha sido movido a la posicion seleccionada");

            }
            catch (NoExistePokemonException e)
            {
                Menu.CambiarColor(ConsoleColor.Red);
                Console.WriteLine("No se puede mover el pokemon porque {0}", e.Message);
            }

            Menu.EspereUnaTecla();
        }

        static void CambiarNombreBox()
        {
            Menu.HeaderPrincipal();
            ResetearFondo();

            string nombreViejo = LogicaPC.BoxSeleccionada.Nombre;
            string nombreNuevo = Validacion.ValidarCadena("Ingrese el nuevo nombre de la box");

            LogicaPC.BoxSeleccionada.Nombre = nombreNuevo; 
            Console.WriteLine("La box '{0}' pasó a llamarse '{1}'", nombreViejo, nombreNuevo);

            Menu.EspereUnaTecla();
        }

        public static void CambiarFondoBox()
        {
            Menu.HeaderPrincipal();
            ResetearFondo();
            OpcionesMenuColor();

            LogicaPC.BoxSeleccionada.Fondo = ValidarColorBox();
            Menu.CambiarColor(LogicaPC.BoxSeleccionada.Fondo);

            Menu.EspereUnaTecla();
        }

        public static void ResetearFondo()
        {
            Menu.CambiarColor(LogicaPC.BoxSeleccionada.Fondo);
        }

        static ConsoleColor ValidarColorBox()
        {
            ConsoleColor colorSeleccionado;
            Console.Write("Ingrese una opcion: ");

            while (!Enum.TryParse<ConsoleColor>(Console.ReadLine(), out colorSeleccionado) || !Enum.IsDefined(typeof(ConsoleColor), colorSeleccionado))
            {
                Menu.CambiarColor(ConsoleColor.Red);
                Console.WriteLine("El color seleccionado no es valido, vuelva a intentarlo...");
                MenuBox.ResetearFondo();
                Console.Write("Seleccione un color: ");
            }

            return colorSeleccionado;
        }

        static OpcionesMenuBox ValidarIngresoUsuario()
        {
            OpcionesMenuBox opcionSeleccionada;

            Console.Write("Ingrese su opcion: ");
            while (!EsOpcionValida(Console.ReadKey(), out opcionSeleccionada))
            {
                Menu.CambiarColor(ConsoleColor.Red);
                Console.WriteLine("\nLa opcion ingresada no es válida, vuelva a intentarlo..");
                ResetearFondo();
                Console.Write("Ingrese su opcion: ");
            }
            Console.WriteLine();

            return opcionSeleccionada;
        }

        static bool EsOpcionValida(ConsoleKeyInfo teclaIngresada, out OpcionesMenuBox menuSeleccionado)
        {
            menuSeleccionado = (OpcionesMenuBox)teclaIngresada.Key;

            //El usuario presiona una tecla y esta tiene que coincidir con las disponibles
            switch (menuSeleccionado)
            {
                case OpcionesMenuBox.Mostrar:
                case OpcionesMenuBox.Buscar:
                case OpcionesMenuBox.Guardar:
                case OpcionesMenuBox.Liberar:
                case OpcionesMenuBox.Modificar:
                case OpcionesMenuBox.Mover:
                case OpcionesMenuBox.CambiarNombreBox:
                case OpcionesMenuBox.CambiarFondoBox:
                case OpcionesMenuBox.VolverMenuBoxes:
                    return true;
                default:
                    return false;
            }

        }
        
        static void OpcionesMenuColor()
        {
            StringBuilder sb = new StringBuilder();
            int contColor = 0;

            sb.AppendLine("Seleccione un color: ");
            foreach (string color in Enum.GetNames(typeof(ConsoleColor)))
            {
                sb.AppendFormat("{0}{1}. {2}\n", Menu.Identar(3), contColor++, color);

            }

            Console.WriteLine(sb.ToString());
        }

        static void OpcionesMenu()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("1. Mostrar Pokemones"); //Mostrar pokemones ordenados aca?
            sb.AppendLine("2. Buscar Pokemon");
            sb.AppendLine("3. Guardar Pokemon");
            sb.AppendLine("4. Liberar Pokemon");
            sb.AppendLine("5. Modificar Pokemon");
            sb.AppendLine("6. Mover Pokemon");
            sb.AppendLine("7. Cambiar Nombre de Box");
            sb.AppendLine("8. Cambiar Fondo de Box");
            sb.AppendLine("9. Volver al menu de Boxes");

            Console.WriteLine(sb.ToString());
        }

        enum OpcionesMenuBox
        {
            Mostrar = ConsoleKey.NumPad1,
            Buscar = ConsoleKey.NumPad2,
            Guardar = ConsoleKey.NumPad3,
            Liberar = ConsoleKey.NumPad4,
            Modificar = ConsoleKey.NumPad5,
            Mover = ConsoleKey.NumPad6,
            CambiarNombreBox = ConsoleKey.NumPad7,
            CambiarFondoBox = ConsoleKey.NumPad8,
            VolverMenuBoxes = ConsoleKey.NumPad9,

        }


    }
}
