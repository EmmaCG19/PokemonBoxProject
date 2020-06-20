using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using Entidades;
using Entidades.Excepciones;
using Datos;
using System.Runtime.CompilerServices;
using System.Net;

namespace Logica
{
    public static class LogicaPC
    {
        static LogicaPC()
        {
            PCDAL.CargarData();
            _boxSeleccionada = PC.Boxes[0];
        }

        private static Box _boxSeleccionada;

        //ACCIONES EN PC

        public static void IniciarPC()
        {
            LogicaBox caja1 = new LogicaBox(BoxSeleccionada);
            caja1.CargaInicial();
        }

        public static Box BoxSeleccionada { get { return _boxSeleccionada; } set { _boxSeleccionada = value; } }

        /// <summary>
        /// Envia un mail a uno de los NPC agendados como contacto
        /// </summary>
        /// <param name="nombreEntrenador"></param>
        /// <returns></returns>
        public static bool EnviarMail(string entrenadorOT)
        {
            //Buscar el nombre en los contactos de la PC
            foreach (Entrenador trainer in PC.Contactos)
            {
                //Enviar mail al entrenador que pasamos por parametro
                if (trainer.NombreOT.Contains(entrenadorOT))
                    return true;
            }

            return false;
        }

        public static void ModificarCantBoxes(int cantidad)
        {
            PCDAL.ModificarCantidadBoxes(cantidad);
        }

        public static void ResetearBox(Box box)
        {
            PCDAL.ResetearBox(box);
        }

        public static void IntercambiarBoxes(Box box1, Box box2)
        {
            PCDAL.IntercambiarBoxes(box1, box2);
        }

        //ESTADISTICAS

        public static List<Pokemon> LegendariosCapturados()
        {
            //El array de legendarios capturados no puede ser más de 5 porque hay un ejemplar por legendario

            List<Pokemon> legendariosCapturados = new List<Pokemon>();
            int[] dexLegendarios = new int[PC.Legendarios.Length];
            
            #region Obtener el nroDex de los pokemons legendarios
            for (int i = 0; i < dexLegendarios.Length; i++)
            {
                dexLegendarios[i] = PC.Legendarios[i].NroDex;
            }
            #endregion


            #region Obtener los pokemones que coincidan con el dex de los legendarios

            for (int posPC = 0; posPC < PC.Boxes.Length; posPC++)
            {
                //Quiero obtener todos los pokemon de cada uno de las boxes
                LogicaBox boxBL = new LogicaBox(PC.Boxes[posPC]);
                Pokemon[] pokemonesEnBox = boxBL.ObtenerTodosLosCapturados();

                for (int posBOX = 0; posBOX < pokemonesEnBox.Length; posBOX++)
                {
                    //Voy a recorrer cada una de las boxes y me fijo si el pokemon de esa box
                    if (dexLegendarios.Contains(pokemonesEnBox[posBOX].NroDex))
                    {
                        legendariosCapturados.Add(pokemonesEnBox[posBOX]);
                    }
                }

            }


            #endregion

            return legendariosCapturados;
        }

        public static int CantPokemonesPorTipo(Tipo tipo)
        {
            int pokemonesPorTipo = 0;

            foreach (Box box in PC.Boxes)
            {
                foreach (Pokemon pokemon in box.Pokemones)
                {
                    if (pokemon.Tipo == tipo)
                        pokemonesPorTipo++;
                }
            }

            return pokemonesPorTipo;
        }

        public static int CantPokemonesPorPokebola(Pokebola pokebola)
        {
            int pokemonesPorPokebola = 0;

            foreach (Box box in PC.Boxes)
            {
                foreach (Pokemon pokemon in box.Pokemones)
                {
                    if (pokemon.AtrapadoCon == pokebola)
                        pokemonesPorPokebola++;
                }
            }

            return pokemonesPorPokebola;

        }

        public static int NroCapturasPorDex(short nroDex)
        {
            int cantidadVeces = 0;

            foreach (Box box in PC.Boxes)
            {
                foreach (Pokemon pokemon in box.Pokemones)
                {
                    if (pokemon.NroDex == nroDex)
                        cantidadVeces++;
                }
            }

            return cantidadVeces;
        }

        public static Pokemon[] PokemonesIntercambiados()
        {
            //Recorro todas las cajas y obtengo aquellos pokemones que no coincidan con el OT del Jugador
            throw new NotImplementedException();
        }

        public static int PokemonesPorNivel(byte nivel)
        {
            //Recorro todas las cajas y obtengo solamente aquellos pokemones de nivel N
            int pokemonesDeLvl = 0;

            foreach (Box box in PC.Boxes)
            {
                foreach (Pokemon pokemon in box.Pokemones)
                {
                    if (pokemon.Nivel == nivel)
                        pokemonesDeLvl++;
                }
            }

            return pokemonesDeLvl;
        }

        public static string PokemonMasCapturado()
        {
            int[] capturasPorDex = new int[PC.PokemonesEnDex];
            int capturasMax = int.MinValue;
            int nroDexMax = 1;

            #region Calcular la cantidad de capturas por cada nroDex
            for (int nroDex = 1; nroDex <= PC.PokemonesEnDex; nroDex++)
            {
                capturasPorDex[nroDex - 1] = NroCapturasPorDex((byte)nroDex);
            }
            #endregion

            #region Obtener el nro de dex con mayor nro de capturas
            for (int i = 0; i < PC.PokemonesEnDex; i++)
            {
                if (capturasPorDex[i] > capturasMax)
                {
                    capturasMax = capturasPorDex[i];
                    nroDexMax = i + 1;
                }
            }
            #endregion

            #region Retornar nombre del pokemon con ese nroDex
            string nombrePokemon = String.Empty;
            foreach (Box box in PC.Boxes)
            {
                foreach (Pokemon pokemon in box.Pokemones)
                {
                    if (pokemon.NroDex == nroDexMax)
                    {
                        nombrePokemon = pokemon.Nombre;
                        break;
                    }
                }
            }
            #endregion

            return nombrePokemon;
        }

        public static Pokemon PokemonAtrapadoConMasterball()
        {
            foreach (Box box in PC.Boxes)
            {
                foreach (Pokemon pokemon in box.Pokemones)
                {
                    if (pokemon.AtrapadoCon == Pokebola.Masterball)
                        return pokemon;
                }
            }

            return null;
        }

        /// <summary>
        /// Nos dice si ya existe un pokemon en la PC que fue capturado con una masterball.
        /// </summary>
        /// <returns></returns>
        public static bool YaExistePokemonConMasterBall()
        {
            //Buscar si hay algun pokemon entre todos los capturados con masterball
            foreach (Box box in PC.Boxes)
            {
                LogicaBox boxBL = new LogicaBox(box);

                foreach (Pokemon pokemon in boxBL.ObtenerTodosLosCapturados())
                {
                    if (pokemon.AtrapadoCon == Pokebola.Masterball)
                        return true;
                }
            }

            return false;
        }

    }
}
