using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Excepciones
{
    public class MasterBallUnicaException: Exception
    {
        static readonly string _mensajeError = "Ya existe un pokemon capturado con la masterball.";

        public MasterBallUnicaException():base(_mensajeError)
        {


        }

    }
}
