using System;
using System.Runtime.Serialization;

namespace Entidades
{
    public class NoExistePokemonException : Exception
    {
        public NoExistePokemonException() { }
    }
}