using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Excepciones
{
    public class NroDexSuperadoException : Exception
    {
        static readonly string _mensajeError = "El Nro de pokedex ya tiene 3 pokemones en la box";

        public NroDexSuperadoException():base(_mensajeError)
        {

        }
    }
}
