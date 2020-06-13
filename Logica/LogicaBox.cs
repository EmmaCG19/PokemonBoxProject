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
    /// <summary>
    /// Esta logica se aplica a cada una de las boxes de la PC
    /// </summary>
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

        public void CambiarStatusItem(Pokemon pokemon)
        {
            _dal.CambiarStatusItem(pokemon);
        }

        public void CambiarNombre(Pokemon pokemon, string nombre)
        {
            _dal.CambiarNombre(pokemon, nombre);
        }

        public void Guardar(Pokemon pokemon)
        {
            //No mas de 3 pokemones con el mismo nroDex en la box
            //Solamente un pokemon puede estar atrapado con una masterball

            int cantMismoDex = 0;
            #region Calculando la cantidad de pokemones con el mismo nroDex
            foreach (Pokemon poke in _dal.Box.Pokemones)
            {
                if (poke.NroDex == pokemon.NroDex)
                {
                    cantMismoDex++;
                }
            }
            #endregion

            if (LogicaPC.YaExistePokemonConMasterBall())
            {
                throw new MasterBallUnicaException();
            }
            else 
            {
                if (cantMismoDex < 3)
                    _dal.Guardar(pokemon);
                else
                    throw new NroDexSuperadoException();
            }
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

        public Pokemon[] ObtenerPorTipo(Tipo tipoPoke)
        {
            return (Pokemon[])this.ObtenerTodos()
                                  .Where(p => p.Tipo == tipoPoke);
        }

        public Pokemon[] ObtenerPorPokebola(Pokebola pokebola)
        {
            return (Pokemon[])this.ObtenerTodos()
                                  .Where(p => p.AtrapadoCon == pokebola);
        }

        public Pokemon[] ObtenerPorRangoNiveles(int nivelMin= 1, int nivelMax= 100)
        {
            return (Pokemon[])this.ObtenerTodos()
                                  .Where(p => p.Nivel >= nivelMin && p.Nivel <= nivelMax);
        }

        public Pokemon[] ObtenerHuevos()
        {
            return (Pokemon[])this.ObtenerTodos()
                                  .Where(p => p is Huevo);
        }

        public void OrdenarPorNivel(ModoOrdenamiento orden) 
        {
            switch (orden) 
            {
                case ModoOrdenamiento.ASC:
                    _dal.Box.Pokemones.OrderBy(p => p.Nivel);
                    break;
                case ModoOrdenamiento.DESC:
                    _dal.Box.Pokemones.OrderByDescending(p => p.Nivel);
                    break;
                default:
                    break;
            }
        }

        public Pokemon[] ObtenerPorNroPokedex(int nroPokedex)
        {
            return (Pokemon[])this.ObtenerTodos()
                                  .Where(p => p.NroDex == nroPokedex);
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
