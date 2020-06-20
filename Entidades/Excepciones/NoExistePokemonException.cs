using System;
using System.Runtime.Serialization;

namespace Entidades
{
    public class NoExistePokemonException : Exception
    {
        static readonly string _error = "No existe un pokemon en esa posicion de la box";

        public NoExistePokemonException():base(_error) 
        {
            
        }
    }    
}