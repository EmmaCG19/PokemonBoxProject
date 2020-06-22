using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logica;
using ConsolaUI.Tablas;
using Entidades;
using Entidades.Excepciones;
using ConsolaUI.Utilidades;

namespace ConsolaUI.Menues
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
            int pokemonId = Validaciones.ValidarId();

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
                if (Validaciones.ValidarSoN("El pokemon a guardar es un huevo?"))
                {
                    #region Cargar un huevo
                    ResetearFondo();
                    short cantidadPasos = Validaciones.ValidarCantPasos();
                    box.Guardar(new Huevo(cantidadPasos));
                    Menu.CambiarColor(ConsoleColor.Yellow);
                    Console.WriteLine("El huevo ha sido cargado");
                    #endregion

                }
                else
                {
                    #region Cargar un pokemon 
                    ResetearFondo();
                    Pokemon pokemonNuevo = CargaDatosPokemon();
                    Menu.HeaderPrincipal();
                    TablaPokemon.GenerarTabla(pokemonNuevo);
                    ResetearFondo();

                    if (Validaciones.ValidarSoN("Desea guardar este pokemon?"))
                    {
                        ResetearFondo();
                        box.Guardar(pokemonNuevo);
                        Menu.CambiarColor(ConsoleColor.Yellow);
                        Console.WriteLine("El pokemon ha sido cargado");
                    }
                    else
                    {
                        Menu.CambiarColor(ConsoleColor.Red);
                        Console.WriteLine("Se ha cancelado la carga del pokemon");
                    }
                    #endregion

                }

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
            int idPokemon = Validaciones.ValidarId();

            try
            {
                TablaPokemon.GenerarTabla(box.ObtenerPokemon(idPokemon));
                ResetearFondo();

                #region Confirmando la eliminacion
                if (Validaciones.ValidarSoN("Desea eliminar el pokemon?"))
                {
                    box.Liberar(idPokemon);
                    Menu.CambiarColor(ConsoleColor.Yellow);
                    Console.WriteLine("\nEl pokemon ha sido liberado");
                }
                else
                {
                    Menu.CambiarColor(ConsoleColor.Red);
                    Console.WriteLine("\nLa liberacion se ha cancelado");
                }
                #endregion
            }
            catch (NoExistePokemonException e)
            {
                Menu.CambiarColor(ConsoleColor.Red);
                Console.WriteLine(e.Message);
            }

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
            short nroDex = Validaciones.ValidarNroDex();

            //Cargamos el nombre
            string nombre = Validaciones.ValidarCadena("Ingrese el nombre del pokemon");

            //Cargamos el nivel
            byte nivel = Validaciones.ValidarNivel();

            //Cargamos el tipo
            Tipo tipo = Validaciones.ValidarTipo();

            //Cargamos la pokebola
            Pokebola pokebola = Validaciones.ValidarPokebola();

            //Cargamos el genero
            Genero genero = Validaciones.ValidarGenero();

            //Cargamos los ataques
            string[] ataques = Validaciones.ValidarAtaques();

            //Cargamos el trainer
            Entrenador entrenador = Validaciones.ValidarEntrenador();

            //Cargamos el item
            bool item = Validaciones.ValidarItem();
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
                Pokemon pokemonAMover = box.ObtenerPokemon(Validaciones.ValidarId());

                Console.Write("Posicion a donde desea moverlo: ");
                int posicionSeleccionada = Validaciones.ValidarId();

                box.Mover(pokemonAMover, posicionSeleccionada);
                Menu.CambiarColor(ConsoleColor.Yellow);
                Console.WriteLine("\nEl pokemon ha sido movido a la posicion seleccionada");

            }
            catch (NoExistePokemonException e)
            {
                Menu.CambiarColor(ConsoleColor.Red);
                Console.WriteLine("\nError al mover porque {0}", e.Message.ToLower());
            }

            Menu.EspereUnaTecla();
        }

        static void CambiarNombreBox()
        {
            Menu.HeaderPrincipal();
            ResetearFondo();

            string nombreViejo = LogicaPC.BoxSeleccionada.Nombre;
            string nombreNuevo = Validaciones.ValidarCadena("Ingrese el nuevo nombre de la box");

            LogicaPC.BoxSeleccionada.Nombre = nombreNuevo;
            ResetearFondo();
            Console.WriteLine("\nLa box '{0}' pasó a llamarse '{1}'", nombreViejo, nombreNuevo);

            Menu.EspereUnaTecla();
        }

        public static void CambiarFondoBox()
        {
            Menu.HeaderPrincipal();
            ResetearFondo();
            OpcionesMenuColor();

            LogicaPC.BoxSeleccionada.Fondo = ValidarColorBox();
            Menu.CambiarColor(LogicaPC.BoxSeleccionada.Fondo);
            Console.WriteLine("\nEl color de la box ha sido cambiado");

            Menu.EspereUnaTecla();
        }

        /// <summary>
        /// Vuelve a settear el color de fondo que tiene asignado la box
        /// </summary>
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

            Console.Write("Seleccione una opcion: ");
            while (!EsOpcionValida(Console.ReadKey(), out opcionSeleccionada))
            {
                Menu.CambiarColor(ConsoleColor.Red);
                Console.WriteLine("\nLa opcion seleccionada no es válida, vuelva a intentarlo..");
                ResetearFondo();
                Console.Write("\nSeleccione una opcion: ");
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
