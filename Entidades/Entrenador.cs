using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Entrenador
    {
        public Entrenador()
        {

        }

        public Entrenador(int id, string nombre)
        {
            this.Id = id;
            this.NombreOT = nombre;
            this.Region = Region.Kanto;
        }

        public int Id { get; set; }
        public string NombreOT { get; set; }
        public Region Region { get; set; }
    }
}
