using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public static class PC
    {
        static PC()
        {
            Titulo = "Pokemon Storage System v1.0";
            NroBoxes = 12;
            Boxes = new Box[NroBoxes];
            Region = Region.Kanto;
            _pokemonesEnDex = 151;
            _nivelMaximo = 100;
        }

        static readonly short _pokemonesEnDex;
        static readonly byte _nivelMaximo;

        public static Entrenador Jugador { get; set; }
        public static Region Region { get; set; }
        public static Box[] Boxes { get; set; }
        public static int NroBoxes { get; set; }
        public static string Titulo { get; set; }
        public static List<Entrenador> Contactos { get; set; }
        public static Pokemon[] Legendarios { get; set; }
        public static short PokemonesEnDex { get { return _pokemonesEnDex; } }
        public static byte NivelMaximo { get { return _nivelMaximo; } }
    }

}
