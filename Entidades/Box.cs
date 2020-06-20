using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Box
    {
        static Box()
        {
            _capacidad = 13;
        }

        public Box(int id, string nombre = "Box")
        {
            this.Id = id;
            this.Nombre = (nombre.ToLower().Equals("box")) ? String.Format("{0} {1}", nombre, id+1) : nombre;
            this.Fondo = ConsoleColor.Green;
            this.Pokemones = new Pokemon[Capacidad];
        }

        private static readonly byte _capacidad;

        public int Id { get; set; }
        public string Nombre { get; set; }
        public static byte Capacidad { get { return _capacidad; } }
        public Pokemon[] Pokemones { get; set; }
        public ConsoleColor Fondo { get; set; }

        /// <summary>
        /// Se fija si no quedan posiciones libres dentro de la Box
        /// </summary>
        public bool EstaLlena
        {
            get
            {
                return !this.Pokemones.Contains(null);
            }
        }

    }
}