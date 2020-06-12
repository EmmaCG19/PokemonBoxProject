using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;
using Entidades;
using Entidades.Excepciones;

namespace Logica
{
    public class LogicaBox : IPokemon
    {
        public LogicaBox(Box box) 
        {
            _dal = new BoxDAL(box);
        }

        private BoxDAL _dal;


        //FUNCIONALIDADES DE LOS POKEMONES DE LA BOX

        public void CargaInicial() 
        {
            _dal.CargaInicial();
        }

        public void CambiarAtaques(Pokemon pokemon, string[] ataques)
        {
            _dal.CambiarAtaques(pokemon, ataques);
        }

        public void ModificarStatusItem(Pokemon pokemon)
        {
            _dal.ModificarStatusItem(pokemon);
        }

        public void CambiarNombre(Pokemon pokemon, string nombre)
        {
            _dal.CambiarNombre(pokemon, nombre);
        }

        public void Guardar(Pokemon pokemon)
        {
            //No mas de 3 pokemones con el mismo nroDex en la box

            int cantMismoDex = 0;
            #region Calculando la cantidad de pokemones con el mismo nroDex
            foreach (Pokemon poke in _dal.Box.Pokemones)
            {
                if(poke.NroDex == pokemon.NroDex) 
                {
                    cantMismoDex++;
                }
            }
            #endregion

            if (cantMismoDex < 3)
                _dal.Guardar(pokemon);
            else
                throw new NroDexSuperadoException();
        }

        public void Liberar(int idPokemon)
        {
            _dal.Liberar(idPokemon);
        }

        public void Mover(Pokemon pokemon, int espacioEnBox)
        {
            _dal.Mover(pokemon, espacioEnBox);
        }

        public Pokemon ObtenerPokemon(int idPokemon)
        {
            return _dal.ObtenerPokemon(idPokemon);
        }

        public Pokemon[] ObtenerTodos()
        {
            return _dal.ObtenerTodos();
        }

        //FUNCIONALIDADES DE LA BOX

        public void CambiarFondo(ConsoleColor color) 
        {
            _dal.Box.Fondo = color;
        }

        public void CambiarNombre(string nombre) 
        {
            _dal.Box.Nombre = nombre;
        }

       
    }
}
