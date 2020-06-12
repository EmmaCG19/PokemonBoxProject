using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    //Definir el comportamiento para manejar pokemones en las boxes
    public interface IPokemon
    {
        void Guardar(Pokemon pokemon);
        void Liberar(int idPokemon);
        void ModificarStatusItem(Pokemon pokemon);
        void CambiarAtaques(Pokemon pokemon, string[] ataques);
        void CambiarNombre(Pokemon pokemon, string nombre);
        void Mover(Pokemon pokemon, int espacioEnBox);
        Pokemon ObtenerPokemon(int idPokemon);
        Pokemon[] ObtenerTodos();
    }
}
