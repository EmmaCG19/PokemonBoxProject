using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Entidades;
using Entidades.Excepciones;

namespace Datos
{
    public class BoxDAL : IPokemon
    {
        public BoxDAL(Box box)
        {
            _box = box;
        }

        private readonly Box _box;
        public Box Box { get { return _box; } }

        /// <summary>
        /// Realizo la carga inicial con la informacion de 10 pokemones dentro de la BOX
        /// </summary>
        public void CargaInicial()
        {
            #region Lista de pokemones
            Pokemon[] pokemonesHardcodeados = new Pokemon[]
            {
                new Pokemon(120, "Staryu", 25, Tipo.Agua, Genero.Femenino, PC.Jugador, Pokebola.Greatball, new string[] { "rapidez", "burbuja", "gruñido", "placaje" }, true),
                new Pokemon(1, "Bulbasaur", 5, Tipo.Planta, Genero.Masculino, PC.Jugador, Pokebola.Pokeball, new string[] { "placaje", "gruñido" }, false),
                new Huevo(300),
                new Pokemon(41, "Zubat", 17, Tipo.Veneno, Genero.Femenino, PC.Jugador, Pokebola.Pokeball, new string[] { "absorber", "rayo confusion"}, true),
                new Pokemon(41, "Zubat", 18, Tipo.Veneno, Genero.Masculino, PC.Jugador, Pokebola.Pokeball, new string[] {"absorber", "rayo confusion", "veneno"}, false),
                new Huevo(1000),
                new Pokemon(150, "Mewtwo", 70, Tipo.Psiquico, Genero.Masculino, PC.Jugador, Pokebola.Masterball, new string[] { "telekinesis"}, true),
                new Huevo(500),
                new Pokemon(16, "Charizard", 38, Tipo.Fuego, Genero.Femenino, PC.Jugador, Pokebola.Pokeball, new string[] { "llamarada", "volar"}, true),
                new Pokemon(20, "Pikachu", 10, Tipo.Electrico, Genero.Femenino, new Entrenador(35268, "NoobSaibot"), Pokebola.Safariball, new string[] { "placaje", "impactrueno", "agilidad"}, true)
             };
            #endregion

            foreach (Pokemon pokemon in pokemonesHardcodeados)
            {
                this.Guardar(pokemon);
            }
        }

        /// <summary>
        /// Carga el pokemon en la espacio de la BOX que esté disponible. 
        /// </summary>
        /// <param name="pokemon"></param>
        public void Guardar(Pokemon pokemon)
        {
            int posicion;

            if (!Box.EstaLlena)
            {
                posicion = this.ObtenerEspacioDisponible();
                this.CargarEnPosicion(pokemon, posicion);
            }
            else
                throw new BoxLlenaException();
        }

        public void Liberar(int idPokemon)
        {
            Pokemon pokeALiberar = ObtenerPokemon(idPokemon);

            if (pokeALiberar is null)
                throw new NoExistePokemonException();
            else
                Box.Pokemones[idPokemon] = null;
        }

        public void Mover(Pokemon pokemonActual, int espacioEnBox)
        {
            //Tomar el pokemon que se desea mover
            Pokemon pokemonEnEspacio = ObtenerPokemon(espacioEnBox);

            if (pokemonEnEspacio is null)
            {
                //Si no hay un pokemon, limpiar el espacio actual y lo cargo en el nuevo
                this.Liberar(pokemonActual.Id);
                this.CargarEnPosicion(pokemonActual, espacioEnBox);
            }
            else
            {
                //Intercambiar posicion con el pokemon en espacio
                this.CargarEnPosicion(pokemonEnEspacio, pokemonActual.Id);
                this.CargarEnPosicion(pokemonActual, espacioEnBox);
            }

        }

        /// <summary>
        /// Nos permite obtener un pokemon en base a su id(posicion) dentro de la box. 
        /// </summary>
        /// <param name="idPokemon"></param>
        /// <returns>Si el id pertenece a un pokemon, nos retorna el mismo, sino retorna null.</returns>
        public Pokemon ObtenerPokemon(int idPokemon)
        {
            return Box.Pokemones[idPokemon];
        }

        public Pokemon[] ObtenerTodosLosCapturados()
        {
            int espaciosDisponibles;
            Pokemon[] pokemonesCargados;

            #region Calcular cuantos espacios disponibles hay
            espaciosDisponibles = 0;
            foreach (Pokemon pokemon in Box.Pokemones)
            {
                if (pokemon is null)
                    espaciosDisponibles++;
            }
            #endregion

            #region Obtener los espacios en box donde haya un pokemon cargado
            pokemonesCargados = new Pokemon[Box.Pokemones.Length - espaciosDisponibles];

            for (int posBox = 0, posCargados = 0; posBox < Box.Pokemones.Length; posBox++)
            {
                if (Box.Pokemones[posBox] != null)
                    pokemonesCargados[posCargados++] = Box.Pokemones[posBox];
            }
            #endregion

            return pokemonesCargados;
        }

        public void CambiarItem(Pokemon pokemon, string nombreItem)
        {
            throw new NotImplementedException();
        }

        public void CambiarStatusItem(Pokemon pokemon)
        {
            pokemon.TieneItem = !pokemon.TieneItem;
        }

        public void CambiarNombre(Pokemon pokemon, string nombre)
        {
            if (!nombre.Trim().Equals(String.Empty))
            {
                pokemon.Nombre = nombre;
            }
        }

        public void CambiarAtaques(Pokemon pokemon, string[] nuevosAtaques)
        {

            for (int posNuevo = 0, posActual = 0; posNuevo < Pokemon.LimiteAtaques; posNuevo++)
            {
                if (nuevosAtaques[posNuevo] != null)
                {
                    if (!AtaqueExistente(pokemon, nuevosAtaques[posNuevo]))
                    {
                        if (PosicionSinAtaque(pokemon.Ataques) != -1)
                        {
                            #region Moveset del pokemon no está completo. El ataque nuevo se agrega en la primera posicion libre.
                            int posLibre = PosicionSinAtaque(pokemon.Ataques);
                            pokemon.Ataques[posLibre] = nuevosAtaques[posNuevo];
                            #endregion
                        }
                        else
                        {
                            #region Moveset del pokemon está completo. El ataque nuevo reemplaza al viejo de esa posicion.
                            pokemon.Ataques[posActual++] = nuevosAtaques[posNuevo];
                            #endregion
                        }

                    }
                }
            }

        }

        /// <summary>
        /// Se obtienen los ataques cargados del pokemon.
        /// </summary>
        /// <param name="pokemon"></param>
        /// <returns></returns>
        public string[] ObtenerAtaques(Pokemon pokemon)
        {
            #region Obtengo la cantidad de ataques que fueron cargados, es decir, que no sean nulos.
            int cantAtaques = 0;

            //Pokemon llega con un array de 3 ataques. 
            //Necesito completar el espacio faltante con null.

            foreach (string ataque in pokemon.Ataques)
            {
                if (ataque != null)
                    cantAtaques++;
            }
            #endregion

            #region Agrego los ataques cargados a un nuevo array con la cantidad obtenida anteriormente
            string[] ataquesCargados = new string[cantAtaques];
            for (int i = 0, j = 0; i < Pokemon.LimiteAtaques; i++)
            {
                if (pokemon.Ataques[i] != null)
                    ataquesCargados[j++] = pokemon.Ataques[i];
            }
            #endregion

            return ataquesCargados;
        }

        private bool AtaqueExistente(Pokemon pokemon, string nombreAtaque)
        {
            string[] ataques = this.ObtenerAtaques(pokemon);

            //Obtener los nombres de los ataques que estan cargados en el array
            for (int i = 0; i < ataques.Length; i++)
            {
                if (ataques[i].ToLower().Trim() == nombreAtaque.ToLower().Trim())
                {
                    return true;
                }
            }

            return false;
        }

        private int PosicionSinAtaque(string[] listaAtaques)
        {
            //retorna -1 si no hay posiciones vacias
            int posSinAtaque = -1;

            for (int pos = 0; pos < listaAtaques.Length; pos++)
            {
                if (listaAtaques[pos] is null)
                {
                    posSinAtaque = pos;
                    break;
                }
            }

            return posSinAtaque;
        }

        /// <summary>
        /// Se carga el pokemon dentro de la box con la posicion especificada por parámetro
        /// </summary>
        /// <param name="pokemon"></param>
        /// <param name="espacioEnBox"></param>
        private void CargarEnPosicion(Pokemon pokemon, int espacioEnBox)
        {
            pokemon.Id = espacioEnBox;
            Box.Pokemones[espacioEnBox] = pokemon;
        }

        /// <summary>
        /// Busca un espacio disponible dentro la Box.
        /// </summary>
        /// <returns>En caso de encontrar un espacio, retorna la posicion disponible. En caso contrario, retorna -1.</returns>
        private int ObtenerEspacioDisponible()
        {
            int posEncontrada = -1;

            for (int pos = 0; pos < Box.Pokemones.Length; pos++)
            {
                if (Box.Pokemones[pos] is null)
                {
                    posEncontrada = pos;
                    break;
                }
            }
            return posEncontrada;
        }


    }
}
