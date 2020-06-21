using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;
using Logica;

namespace ConsolaUI.Tablas
{
    public static class TablaPokemon
    {
        static TablaPokemon()
        {
            //ancho de columnas
        }

        public static string GenerarEncabezado()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(string.Format(" {0,-4}|", "Id"));
            sb.Append(string.Format(" {0,-6}|", "NroDex"));
            sb.Append(string.Format(" {0,-10}|", "Nombre"));
            sb.Append(string.Format(" {0,-6}|", "Nivel"));
            sb.Append(string.Format(" {0,-12}|", "Genero"));
            sb.Append(string.Format(" {0,-12}|", "Tipo"));
            sb.Append(string.Format(" {0,-12}|", "Atrapado Con"));
            sb.Append(string.Format(" {0,-12}|", "Entrenador"));
            sb.Append(string.Format(" {0,-40}", "Ataques"));

            //No fue agregado porque supera el ancho de la consola
            //sb.Append(string.Format(" {0,-10}|", "Tiene Item"));

            return sb.ToString();
        }

        public static string GenerarFila(Pokemon pokemon)
        {
            LogicaBox box = new LogicaBox(LogicaPC.BoxSeleccionada);
            string[] ataques = box.ObtenerAtaques(pokemon);

            StringBuilder sb = new StringBuilder();
            sb.Append(string.Format(" {0,-4}|", pokemon.Id + 1));
            sb.Append(string.Format(" {0,-6}|", pokemon.NroDex));
            sb.Append(string.Format(" {0,-10}|", pokemon.Nombre));
            sb.Append(string.Format(" {0,-6}|", pokemon.Nivel));
            sb.Append(string.Format(" {0,-12}|", pokemon.Genero));
            sb.Append(string.Format(" {0,-12}|", pokemon.Tipo));
            sb.Append(string.Format(" {0,-12}|", pokemon.AtrapadoCon));
            sb.Append(string.Format(" {0,-12}|", pokemon.Entrenador.NombreOT));

            #region Generando un campo para los ataques
            if (ataques.Length != 0)
            {
                foreach (string ataque in ataques)
                {
                    sb.Append(string.Format(" {0,-10}-", ataque));
                }
            }
            else
                sb.Append(string.Format(" {0,-40}", "SIN ATAQUES"));
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
