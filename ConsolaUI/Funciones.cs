using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsolaUI
{
    public static class Funciones
    {
        public static void InicializarArrayString(string [] array) 
        {
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = String.Empty;
            }
        }

    }
}
