﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;
using Logica;

namespace ConsolaUI
{
    public static class MenuInforme
    {
        public static void Iniciar()
        {
            bool seguirEnMenu = true;

            do
            {
                Menu.HeaderPrincipal();
                Menu.BannerMenu("Menu Informes");
                OpcionesMenu();

                switch (ValidarIngresoUsuario())
                {
                    case OpcionesMenuInforme.Pokedex:
                        MostrarInformePokedex();
                        break;
                    case OpcionesMenuInforme.Entrenador:
                        MostrarInformeTrainer();
                        break;
                    case OpcionesMenuInforme.ExportarTodo:
                        break;
                    case OpcionesMenuInforme.VolverAMenu:
                        seguirEnMenu = false;
                        break;
                    default:
                        break;
                }

            } while (seguirEnMenu);
        }


        public static void MostrarInformeTrainer()
        {
            Menu.HeaderPrincipal();
            Menu.CambiarColor(ConsoleColor.Green);
            Console.Write(ReporteEntrenador());

            //Preguntar si desea exportar los datos            
            if (Validacion.ValidarSoN("Desea guardar esta informacion?"))
            { 
            

            }

            Menu.EspereUnaTecla();
        }

        public static void MostrarInformePokedex()
        {
            Menu.HeaderPrincipal();
            Menu.CambiarColor(ConsoleColor.Green);
            Console.Write(ReportePokedex());

            //Preguntar si desea exportar los datos            
            if (Validacion.ValidarSoN("Desea guardar esta informacion?"))
            {


            }

            Menu.EspereUnaTecla();
        }

        public static string ReporteEntrenador()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine(Menu.LineaFormateada(100, '-'));
            sb.AppendFormat("{0,61}", "INFORME DE ENTRENADOR\n");
            sb.AppendLine(Menu.LineaFormateada(100, '-'));
            sb.AppendLine();
            sb.AppendFormat("{0}ID: {1}\n", Menu.Identar(3), PC.Jugador.Id);
            sb.AppendFormat("{0}OT: {1}\n", Menu.Identar(3), PC.Jugador.NombreOT);
            sb.AppendFormat("{0}Region: {1}\n", Menu.Identar(3), PC.Region);
            sb.AppendFormat("{0}Mails enviados: {1}\n", Menu.Identar(3), MenuPC.MailsEnviados);

            return sb.ToString(); ;
        }

        public static string ReportePokedex()
        {
            StringBuilder sb = new StringBuilder();

            //Concatenando la informacion
            sb.AppendLine(Menu.LineaFormateada(100, '-'));
            sb.AppendFormat("{0,60}", "INFORME DE POKEDEX\n");
            sb.AppendLine(Menu.LineaFormateada(100, '-'));
            sb.AppendLine();
            sb.AppendFormat("{0}Porcentaje de la pokedex completada: {1}%\n", Menu.Identar(3), LogicaPC.PorcentajeDexCompletada());
            sb.AppendFormat("{0}Cantidad de pokemones atrapados: {1}\n", Menu.Identar(3), LogicaPC.TotalPokemonCapturados() - LogicaPC.CantHuevosEncontrados());
            sb.AppendFormat("{0}Cantidad de huevos obtenidos: {1}\n", Menu.Identar(3), LogicaPC.CantHuevosEncontrados());
            sb.AppendFormat("{0}Cantidad de pokemones intercambiados: {1}\n", Menu.Identar(3), LogicaPC.CantPokemonesIntercambiados());
            sb.AppendFormat("{0}Cantidad de pokemones de LVL (100): {1}\n", Menu.Identar(3), LogicaPC.CantPokemonesPorNivel(100, 100));
            sb.AppendFormat("{0}Nivel promedio de captura: {1}\n", Menu.Identar(3), LogicaPC.NivelPromedioCapturados());
            sb.AppendFormat("{0}Nro dex más capturado: #{1}\n", Menu.Identar(3), LogicaPC.PokemonMasCapturado());

            sb.AppendLine(LineaSeparacion());
            #region Capturas por distintos parámetros
            sb.AppendLine(InformeCapturasPorTipo());
            sb.AppendLine(InformeCapturasPorPokebola());
            sb.AppendLine(InformeCapturasPorGenero());
            #endregion

            sb.AppendLine(LineaSeparacion());
            sb.AppendLine(InformeLegendarios());

            sb.AppendLine(LineaSeparacion());
            sb.AppendLine(InformeMasterBall());

            //Linea de separacion entre items
            //sb.AppendFormat("{0}{1}\n", Menu.Identar(3), Menu.LineaFormateada(50, '-'));
            //sb.AppendFormat("{0}{1}\n");

            return sb.ToString();
        }

        static string InformeMasterBall() 
        {
            StringBuilder sb = new StringBuilder()
            sb.AppendFormat("{0}MASTERBALL: \n", Menu.Identar(3));
            sb.AppendFormat("{0}{1}\n", Menu.Identar(3), Menu.LineaFormateada(12, '-'));

            if (LogicaPC.YaExistePokemonConMasterBall())
            {
                Pokemon pokemon = LogicaPC.PokemonAtrapadoConMasterball();
                sb.AppendFormat("{0}[NroDex: #{1}, Nombre: {2}, Nivel: {3}]\n", Menu.Identar(5), pokemon.NroDex, pokemon.Nombre, pokemon.Nivel);
            }
            else
                sb.AppendFormat("{0}No se ha capturado pokemon con la masterball\n", Menu.Identar(5));


            return sb.ToString();
        }

        static string InformeLegendarios()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("{0}POKEMONES LEGENDARIOS: \n", Menu.Identar(3));
            sb.AppendFormat("{0}{1}\n", Menu.Identar(3), Menu.LineaFormateada(25, '-'));

            #region Mostrando los legendarios capturados
            List<Pokemon> legendariosCapturados = LogicaPC.LegendariosCapturados();
            if (legendariosCapturados.Count != 0)
            {
                foreach (Pokemon pokemon in legendariosCapturados)
                {
                    sb.AppendFormat("{0}[NroDex: #{1}, Nombre: {2}, Nivel: {3}]\n", Menu.Identar(5), pokemon.NroDex, pokemon.Nombre, pokemon.Nivel);
                }
            }
            else
                sb.AppendFormat("{0}No se han capturado legendarios por el momento\n", Menu.Identar(5));
            #endregion

            return sb.ToString();
        }

        static string InformeCapturasPorTipo()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendFormat("{0}CAPTURAS POR TIPO: \n", Menu.Identar(3));
            sb.AppendFormat("{0}{1}\n", Menu.Identar(3), Menu.LineaFormateada(20, '-'));
            foreach (int tipoPoke in Enum.GetValues(typeof(Tipo)))
            {
                if (tipoPoke != 0)
                    sb.AppendFormat("{0}Pokemones atrapados tipo '{1}': {2}\n", Menu.Identar(5), Enum.GetName(typeof(Tipo), tipoPoke), LogicaPC.CantPokemonesPorTipo((Tipo)tipoPoke));
            }

            return sb.ToString();
        }

        static string InformeCapturasPorPokebola()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendFormat("{0}CAPTURAS POR POKEBOLA: \n", Menu.Identar(3));
            sb.AppendFormat("{0}{1}\n", Menu.Identar(3), Menu.LineaFormateada(21, '-'));
            foreach (int pokebola in Enum.GetValues(typeof(Pokebola)))
            {
                if (pokebola != 0)
                    sb.AppendFormat("{0}Pokemones atrapados con una '{1}': {2}\n", Menu.Identar(5), Enum.GetName(typeof(Pokebola), pokebola), LogicaPC.CantPokemonesPorPokebola((Pokebola)pokebola));
            }

            return sb.ToString();
        }

        static string InformeCapturasPorGenero()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("{0}CAPTURAS POR GENERO: \n", Menu.Identar(3));
            sb.AppendFormat("{0}{1}\n", Menu.Identar(3), Menu.LineaFormateada(21, '-'));
            foreach (int genero in Enum.GetValues(typeof(Genero)))
            {
                sb.AppendFormat("{0}Pokemones atrapados con género '{1}': {2}\n", Menu.Identar(5), Enum.GetName(typeof(Genero), genero), LogicaPC.CantPokemonesPorGenero((Genero)genero));
            }

            return sb.ToString();
        }

        static string LineaSeparacion()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine();
            sb.AppendFormat("{0}{1}", Menu.Identar(3), Menu.LineaFormateada(55, '-'));

            return sb.ToString();
        }

        static OpcionesMenuInforme ValidarIngresoUsuario()
        {
            OpcionesMenuInforme opcionSeleccionada;

            Console.Write("Ingrese su opcion: ");
            while (!EsOpcionValida(Console.ReadKey(), out opcionSeleccionada))
            {
                Menu.CambiarColor(ConsoleColor.Red);
                Console.WriteLine("\nLa opcion ingresada no es válida, vuelva a intentarlo..");
                Menu.ResetearColor();
                Console.Write("\nIngrese su opcion: ");
            }
            Console.WriteLine();

            return opcionSeleccionada;
        }

        static bool EsOpcionValida(ConsoleKeyInfo teclaIngresada, out OpcionesMenuInforme menuSeleccionado)
        {
            menuSeleccionado = (OpcionesMenuInforme)teclaIngresada.Key;

            switch (menuSeleccionado)
            {
                case OpcionesMenuInforme.Pokedex:
                case OpcionesMenuInforme.Entrenador:
                case OpcionesMenuInforme.ExportarTodo:
                case OpcionesMenuInforme.VolverAMenu:
                    return true;
                default:
                    return false;
            }

        }

        static void OpcionesMenu()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("1. Estadisticas sobre la pokedex");
            sb.AppendLine("2. Estadisticas sobre el entrenador");
            sb.AppendLine("3. Exportar todos los datos");
            sb.AppendLine("4. Volver al Menu PC");

            Console.WriteLine(sb.ToString());
        }

        enum OpcionesMenuInforme
        {
            Pokedex = ConsoleKey.NumPad1,
            Entrenador = ConsoleKey.NumPad2,
            ExportarTodo = ConsoleKey.NumPad3,
            VolverAMenu = ConsoleKey.NumPad4
        }


    }
}