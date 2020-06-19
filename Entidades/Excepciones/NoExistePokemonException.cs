using System;
using System.Runtime.Serialization;

namespace Entidades
{
    public class NoExistePokemonException : Exception
    {
        private readonly string _mensajeError = "No existe un pokemon con ese Id en la box";

        public NoExistePokemonException():base() 
        {
            
        }
    }    
}