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
            //_limiteDexNacional = 151;
        }

        //static readonly byte _limiteDexNacional;

        //ACCIONES EN PC

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

        public static bool CapturasteTodosLosLegendarios()
        {
            throw new NotImplementedException();
        }

        public static int CantPokemonesPorTipo(Tipo tipo)
        {
            throw new NotImplementedException();

        }

        public static int CantPokemonesPorPokebola(Pokebola pokebola)
        {
            throw new NotImplementedException();

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

        public static Pokemon[] PokemonesPorNivel(byte nivel = 100)
        {
            //Recorro todas las cajas y obtengo solamente aquellos pokemones de nivel 100
            throw new NotImplementedException();
        }

        public static string PokemonMasCapturado()
        {
            byte limiteDex = 151;
            int[] capturasPorDex = new int[limiteDex];
            int capturasMax = int.MinValue;
            int nroDexMax = 1;

            #region Calcular la cantidad de capturas por cada nroDex
            for (int nroDex = 1; nroDex <= limiteDex; nroDex++)
            {
                capturasPorDex[nroDex - 1] = NroCapturasPorDex((byte)nroDex);
            }
            #endregion

            #region Obtener el nro de dex con mayor nro de capturas
            for (int i = 0; i < limiteDex; i++)
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
}
