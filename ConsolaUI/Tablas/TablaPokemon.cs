using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;
using Logica;
using ConsolaUI.Menues;

namespace ConsolaUI.Tablas
{
    public static class TablaPokemon
    {
        static TablaPokemon(){}

        public static string GenerarEncabezado()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat(" {0,-4}|", "Id");
            sb.AppendFormat(" {0,-6}|", "NroDex");
            sb.AppendFormat(" {0,-15}|", "Nombre");
            sb.AppendFormat(" {0,-6}|", "Nivel");
            sb.AppendFormat(" {0,-12}|", "Genero");
            sb.AppendFormat(" {0,-12}|", "Tipo");
            sb.AppendFormat(" {0,-12}|", "Atrapado Con");
            sb.AppendFormat(" {0,-12}|", "Entrenador");
            sb.AppendFormat(" {0,-40}", "Ataques");

            //No fue agregado porque supera el ancho de la consola
            //sb.Append(string.Format(" {0,-10}|", "Tiene Item"));

            return sb.ToString();
        }

        public static string GenerarFila(Pokemon pokemon)
        {
            LogicaBox box = new LogicaBox(LogicaPC.BoxSeleccionada);
            string[] ataques = box.ObtenerAtaques(pokemon);

            StringBuilder sb = new StringBuilder();
            sb.AppendFormat(" {0,-4}|", pokemon.Id + 1);
            sb.AppendFormat(" {0,-6}|", pokemon.NroDex);
            sb.AppendFormat(" {0,-15}|", pokemon.Nombre);
            sb.AppendFormat(" {0,-6}|", pokemon.Nivel);
            sb.AppendFormat(" {0,-12}|", pokemon.Genero);
            sb.AppendFormat(" {0,-12}|", pokemon.Tipo);
            sb.AppendFormat(" {0,-12}|", pokemon.AtrapadoCon);
            sb.AppendFormat(" {0,-12}|", pokemon.Entrenador.NombreOT);

            #region Generando un campo para los ataques
            if (ataques.Length != 0)
            {
                foreach (string ataque in ataques)
                {
                    sb.AppendFormat(" {0,-10} -", ataque);
                }
            }
            else
                sb.AppendFormat(" {0,-40}", "SIN ATAQUES");
            #endregion

            //No fue agregado porque supera el ancho de la consola
            //sb.Append(string.Format("{0,-10}|", pokemon.TieneItem ? "Si" : "No"));

            return sb.ToString();
        }

        public static string GenerarLineaSeparacion()
        {
            StringBuilder sb = new StringBuilder();
            int longitudEncabezado = GenerarEncabezado().Length;

            for (int i = 0; i < longitudEncabezado; i++)
            {
                sb.Append("-");
            }

            return sb.ToString();
        }

        public static void GenerarTabla(Pokemon[] pokemones)
        {
            Menu.CambiarColor(ConsoleColor.Yellow);
            Console.WriteLine("\n{0}", GenerarEncabezado());
            Console.WriteLine(GenerarLineaSeparacion());
            foreach (Pokemon pokemon in pokemones)
            {
                Console.WriteLine(GenerarFila(pokemon));
                Console.WriteLine(GenerarLineaSeparacion());
            }
        }

        public static void GenerarTabla(Pokemon pokemon)
        {
            Menu.CambiarColor(ConsoleColor.Yellow);
            Console.WriteLine("\n{0}", GenerarEncabezado());
            Console.WriteLine(GenerarLineaSeparacion());
            Console.WriteLine(GenerarFila(pokemon));
        }
    }
}
