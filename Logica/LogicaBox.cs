using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
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

        private readonly BoxDAL _dal;

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
            Pokemon[] todos = this.ObtenerTodosLosCapturados();
            int cantMismoDex = 0;

            #region Calculando la cantidad de pokemones con el mismo nroDex (Exceptuar huevos)
            //No mas de 3 pokemones con el mismo nroDex en la box
            if (!(pokemon is Huevo))
            {
                foreach (Pokemon poke in todos)
                {
                    if (poke.NroDex == pokemon.NroDex)
                        cantMismoDex++;
                }
            }

            if (cantMismoDex >= 3)
                throw new NroDexSuperadoException();
            #endregion

            //Solamente se puede tener UNA copia de los pokemones legendarios
            #region Verifica que el pokemon, siendo legendario, no exista ya en la PC
            foreach (Pokemon legendario in LogicaPC.LegendariosCapturados())
            {
                if (pokemon.NroDex == legendario.NroDex)
                    throw new LegendarioUnicoException();
            }
            #endregion

            //Solamente un pokemon puede estar atrapado con una masterball
            #region Verificar que el pokemon atrapado con Masterball, no exista
            if (LogicaPC.YaExistePokemonConMasterBall() && pokemon.AtrapadoCon == Pokebola.Masterball)
            {
                throw new MasterBallUnicaException();
            }
            #endregion

            _dal.Guardar(pokemon);
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
            Pokemon pokeEnPosicion = _dal.ObtenerPokemon(idPokemon);

            if (pokeEnPosicion is null)
                throw new NoExistePokemonException();

            return pokeEnPosicion;
        }

        public Pokemon[] ObtenerTodosLosCapturados()
        {
            return _dal.ObtenerTodosLosCapturados();
        }

        public Pokemon[] ObtenerPorNroPokedex(int nroPokedex)
        {
            Pokemon[] todos = this.ObtenerTodosLosCapturados();

            #region Obtengo la cantidad de pokemones de ese nroDex
            int cantPokemon = 0;
            foreach (Pokemon pokemon in todos)
            {
                if (pokemon.NroDex == nroPokedex)
                    cantPokemon++;
            }

            #endregion

            #region Guardo los pokemones de ese nroDex en el nuevo array
            Pokemon[] pokemonesMismoDex = new Pokemon[cantPokemon];
            for (int i = 0, j = 0; i < todos.Length; i++)
            {
                if (todos[i].NroDex == nroPokedex)
                    pokemonesMismoDex[j++] = todos[i];
            }
            #endregion

            return pokemonesMismoDex;
        }

        public Pokemon[] ObtenerPorTipo(Tipo tipoPoke)
        {
            Pokemon[] todos = this.ObtenerTodosLosCapturados();

            #region Obtengo la cantidad de pokemones de ese tipo
            int cantPokemon = 0;
            foreach (Pokemon pokemon in todos)
            {
                if (pokemon.Tipo == tipoPoke)
                    cantPokemon++;
            }

            #endregion

            #region Guardo los pokemones de ese tipo en el nuevo array
            Pokemon[] pokemonesPorTipo = new Pokemon[cantPokemon];
            for (int i = 0, j = 0; i < todos.Length; i++)
            {
                if (todos[i].Tipo == tipoPoke)
                    pokemonesPorTipo[j++] = todos[i];
            }
            #endregion

            return pokemonesPorTipo;

        }

        public Pokemon[] ObtenerPorPokebola(Pokebola pokebola)
        {
            Pokemon[] todos = this.ObtenerTodosLosCapturados();

            #region Obtengo la cantidad de pokemones capturados con esa pokebola
            int cantPokemon = 0;
            foreach (Pokemon pokemon in todos)
            {
                if (pokemon.AtrapadoCon == pokebola)
                    cantPokemon++;
            }
            #endregion

            #region Guardo los pokemones capturados con esa pokebola en el nuevo array
            Pokemon[] pokemonesPorPokebola = new Pokemon[cantPokemon];
            for (int i = 0, j = 0; i < todos.Length; i++)
            {
                if (todos[i].AtrapadoCon == pokebola)
                    pokemonesPorPokebola[j++] = todos[i];
            }
            #endregion

            return pokemonesPorPokebola;
        }

        public Pokemon[] ObtenerPorRangoNivel(int nivelMin= 1, int nivelMax= 100)
        {
            Pokemon[] todos = this.ObtenerTodosLosCapturados();

            #region Obtengo la cantidad de pokemones de ese tipo
            int cantPokemon = 0;
            foreach (Pokemon pokemon in todos)
            {
                if (pokemon.Nivel >= nivelMin && pokemon.Nivel <= nivelMax)
                    cantPokemon++;
            }

            #endregion

            #region Guardo los pokemones con ese rango en el nuevo array
            Pokemon[] pokemonPorRangoNivel = new Pokemon[cantPokemon];
            for (int i = 0, j = 0; i < todos.Length; i++)
            {
                if (todos[i].Nivel >= nivelMin && todos[i].Nivel <= nivelMax)
                    pokemonPorRangoNivel[j++] = todos[i];
            }
            #endregion

            return pokemonPorRangoNivel;

        }
         
        public Pokemon[] ObtenerHuevos()
        {
            Pokemon[] todos = this.ObtenerTodosLosCapturados();

            #region Obtener la cantidad de huevos existentes en la box
            int cantHuevos = 0;
            foreach (Pokemon pokemon in todos)
            {
                if (pokemon is Huevo)
                    cantHuevos++;
            }
            #endregion

            #region Guardar los huevos en el nuevo array
            Pokemon[] huevos = new Pokemon[cantHuevos];

            for (int i = 0, j = 0; i < todos.Length; i++)
            {
                if (todos[i] is Huevo)
                    huevos[j++] = todos[i];
            }
            #endregion

            return huevos;
        }

        public void OrdenarPorNivel(ModoOrdenamiento orden) 
        {
            switch (orden) 
            {
                case ModoOrdenamiento.ASC:
                    //Metodo Burbuja or Sort method
                    _dal.Box.Pokemones.OrderBy(p => p.Nivel);
                    break;
                case ModoOrdenamiento.DESC:
                    _dal.Box.Pokemones.OrderByDescending(p => p.Nivel);
                    break;
                default:
                    break;
            }
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
