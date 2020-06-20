using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Excepciones
{
    public class BoxLlenaException: Exception
    {
        static readonly string _mensajeError = "La box está llena.";

        public BoxLlenaException():base(_mensajeError) 
        {
        
        }
    }
}
