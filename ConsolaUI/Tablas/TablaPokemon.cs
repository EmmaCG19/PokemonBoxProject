using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;

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
            sb.Append(string.Format(" {0,-15}|", "Nombre"));
            sb.Append(string.Format(" {0,-6}|", "Nivel"));
            sb.Append(string.Format(" {0,-15}|", "Genero"));
            sb.Append(string.Format(" {0,-15}|", "Tipo"));
            sb.Append(string.Format(" {0,-15}|", "Atrapado Con"));
            sb.Append(string.Format(" {0,-15}|", "Entrenador"));
            //sb.Append(string.Format(" {0,-10}|", "Tiene Item"));
            //sb.Append(string.Format(" {0,-50}", "Ataques"));

            return sb.ToString();
        }

        public static string GenerarFila(Pokemon pokemon)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(string.Format(" {0,-4}|", pokemon.Id + 1));
            sb.Append(string.Format(" {0,-6}|", pokemon.NroDex));
            sb.Append(string.Format(" {0,-15}|", pokemon.Nombre));
            sb.Append(string.Format(" {0,-6}|", pokemon.Nivel));
            sb.Append(string.Format(" {0,-15}|", pokemon.Genero));
            sb.Append(string.Format(" {0,-15}|", pokemon.Tipo));
            sb.Append(string.Format(" {0,-15}|", pokemon.AtrapadoCon));
            sb.Append(string.Format(" {0,-15}|", pokemon.Entrenador.NombreOT));
            //sb.Append(string.Format("{0,-4}|", pokemon.TieneItem ? "Si" : "No"));
            //sb.Append(string.Format("{0,-20}", pokemon.Ataques[0]));

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
            Console.WriteLine(GenerarEncabezado());
            Console.WriteLine(GenerarLineaSeparacion());
            foreach (Pokemon pokemon in pokemones)
            {
                Console.WriteLine(GenerarFila(pokemon));
                Console.WriteLine(GenerarLineaSeparacion());

            }
        }

        public static void GenerarTabla(Pokemon pokemon)
        {
            Console.WriteLine(GenerarEncabezado());
            Console.WriteLine(GenerarLineaSeparacion());
            Console.WriteLine(GenerarFila(pokemon));
        }
    }
}
