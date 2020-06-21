using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Huevo : Pokemon
    {
        public Huevo(short pasosEclosion) :
        base(0, "Huevo", 1, Tipo.NoAsignado, Genero.Indefinido, new Entrenador(-1,"No asignado"), Pokebola.NoAsignado, new string[Pokemon.LimiteAtaques], false)
        {
            this.PasosEclosion = pasosEclosion;
        }

        public short PasosEclosion { get; set; }

    }
}
