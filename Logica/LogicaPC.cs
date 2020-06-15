using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using Entidades;
using Entidades.Excepciones;
using Datos;

namespace Logica
{
    public static class LogicaPC
    {
        static LogicaPC()
        {
            limiteDexNacional = 151;
            _boxSeleccionada = PC.Boxes[0];
        }

        public static readonly byte limiteDexNacional;
        private static Box _boxSeleccionada;

        //ACCIONES EN PC

        public static void IniciarPC()
        {
            PCDAL.CargarData();

            //Cargo valores a algunas cajas [Son 12 cajas]
            LogicaBox caja1 = new LogicaBox(PC.Boxes[0]);
            LogicaBox caja3 = new LogicaBox(PC.Boxes[2]);
            LogicaBox caja5 = new LogicaBox(PC.Boxes[4]);
            LogicaBox caja10 = new LogicaBox(PC.Boxes[9]);

            caja1.CargaInicial();
            caja5.CargaInicial();
            caja3.CargaInicial();
            caja10.CargaInicial();
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

        public static Pokemon[] LegendariosCapturados()
        {
            int[] nroDexLegendarios = new int[PC.Legendarios.Length];
            Pokemon[] pokeLegendariosCapturados = new Pokemon[PC.Legendarios.Length];

            #region Obtener el nroDex de los pokemons legendarios
            for (int i = 0; i < nroDexLegendarios.Length; i++)
            {
                nroDexLegendarios[i] = PC.Legendarios[i].NroDex;
            }

            #endregion

            #region Obtener los pokemones que coincidan con el dex de los legendarios
            foreach (Box box in PC.Boxes)
            {
                for (int i = 0; i < box.Pokemones.Length; i++)
                {
                    if (nroDexLegendarios.Contains(box.Pokemones[i].NroDex))
                    {
                        pokeLegendariosCapturados[i] = box.Pokemones[i];
                    }
                }
            }
            #endregion

            return pokeLegendariosCapturados;
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
            int[] capturasPorDex = new int[limiteDexNacional];
            int capturasMax = int.MinValue;
            int nroDexMax = 1;

            #region Calcular la cantidad de capturas por cada nroDex
            for (int nroDex = 1; nroDex <= limiteDexNacional; nroDex++)
            {
                capturasPorDex[nroDex - 1] = NroCapturasPorDex((byte)nroDex);
            }
            #endregion

            #region Obtener el nro de dex con mayor nro de capturas
            for (int i = 0; i < limiteDexNacional; i++)
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
                foreach (Pokemon pokemon in box.Pokemones)
                {
                    if (pokemon.AtrapadoCon == Pokebola.Masterball)
                        return true;
                }
            }

            return false;
        }

    }


    //Helpful code

    //    foreach (Box box in PC.Boxes)
    //{
    //    foreach (Pokemon pokemon in box.Pokemones)
    //    {

    //    }

    //}

}
