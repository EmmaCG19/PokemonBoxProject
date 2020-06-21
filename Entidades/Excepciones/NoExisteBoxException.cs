using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Excepciones
{
    public class NoExisteBoxException: Exception
    {
        static readonly string _mensajeError = "No existe una box con esa posición";

        public NoExisteBoxException():base(_mensajeError)
        {

        }
    }
}
