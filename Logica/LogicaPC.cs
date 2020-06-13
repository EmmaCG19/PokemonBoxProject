using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;
using Entidades.Excepciones;
using Datos;
using System.ComponentModel;

namespace Logica
{
    public static class LogicaPC
    {
        //REGLA 


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

        //ESTADISTICAS
        public static Pokemon[] PokemonesIntercambiados() 
        {
            //Recorro todas las cajas y obtengo aquellos pokemones que no coincidan con el OT del Jugador
            throw new NotImplementedException();
        }

        public static Pokemon[] PokemonesPorNivel(byte nivel=100)
        {
            //Recorro todas las cajas y obtengo solamente aquellos pokemones de nivel 100
            throw new NotImplementedException();
        }

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

        //Pokemon que más veces aparece (guiarnos por el nroDeDex)
        public static Pokemon PokemonMasCapturado() 
        {

            throw new NotImplementedException();

        }

        private static int NroCapturasPorDex(short nroDex) 
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

        //Deberia ser uno solo - YaExistePokemonConMasterball
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
