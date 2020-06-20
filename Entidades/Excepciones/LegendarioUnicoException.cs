using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Excepciones
{
    public class LegendarioUnicoException: Exception
    {
        static readonly string _mensajeError = "El pokemon legendario ya fue capturado";

        public LegendarioUnicoException():base(_mensajeError)
        {

        }
    }
}
