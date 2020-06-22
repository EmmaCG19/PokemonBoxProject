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
using Microsoft.Win32.SafeHandles;

namespace Logica
{
    public static class LogicaPC
    {
        //ACCIONES EN PC

        /// <summary>
        /// Inicia la PC y carga toda la información relevante del juego, como también inicializa y carga las boxes.
        /// </summary>
        public static void IniciarPC()
        {
            PCDAL.CargarData();
            BoxSeleccionada = PC.Boxes[0];

            LogicaBox caja1 = new LogicaBox(BoxSeleccionada);
            caja1.CargaInicial();
        }

        public static Box BoxSeleccionada { get; set; }

        public static void AgregarBoxes(int cantidad)
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

        public static Box ObtenerBox(int posBox)
        {
            return PCDAL.ObtenerBox(posBox);
        }

        //ESTADISTICAS

        public static int TotalPokemonCapturados()
        {
            int cantCapturados = 0;

            foreach (Box box in PC.Boxes)
            {
                LogicaBox boxBL = new LogicaBox(box);
                cantCapturados += boxBL.ObtenerTodosLosCapturados().Length;
            }

            return cantCapturados;
        }

        public static float PorcentajeDexCompletada()
        {
            int[] totalCapturasEnDex = new int[PC.PokemonesEnDex];
            int cantEnDex = 0;

            //Recorrer todos los nros de Dex y ver si existe aunque sea un ejemplar del mismo en la PC
            for (int i = 0; i < totalCapturasEnDex.Length; i++)
            {
                totalCapturasEnDex[i] = NroCapturasPorDex((short)(i + 1));
            }

            //Contar aquellas posiciones que no sean 0
            foreach (int capturadosPorDex in totalCapturasEnDex)
            {
                if (capturadosPorDex != 0)
                    cantEnDex++;
            }

            //Calcular el porcentaje en base a los 151 pokemones disponibles    
            return (cantEnDex * PC.PokemonesEnDex) / 100;
        }

        /// <summary>
        /// Calcula el nivel promedio de todos los pokemones capturados.
        /// </summary>
        /// <returns>Un entero que nos indica el nivel promedio</returns>
        public static int NivelPromedioCapturados()
        {
            int totalNivel = 0;
            int promedioNivel = 0;

            foreach (Box box in PC.Boxes)
            {
                LogicaBox boxBL = new LogicaBox(box);

                foreach (Pokemon pokemon in boxBL.ObtenerTodosLosCapturados())
                {
                    totalNivel += pokemon.Nivel;
                }
            }

            //Evitar que arroje excepcion por dividir por 0, si no se capturaron pokemones
            if (totalNivel != 0)
                promedioNivel = (int)Math.Floor((decimal)totalNivel / (TotalPokemonCapturados() - CantHuevosEncontrados()));


            return promedioNivel;
        }

        public static int CantHuevosEncontrados()
        {
            int cantHuevos = 0;
            foreach (Box box in PC.Boxes)
            {
                LogicaBox boxBL = new LogicaBox(box);
                cantHuevos += boxBL.ObtenerHuevos().Length;

            }

            return cantHuevos;
        }

        /// <summary>
        /// Devuelve una lista de los legendarios atrapados
        /// </summary>
        /// <returns></returns>
        public static List<Pokemon> LegendariosCapturados()
        {
            //El array de legendarios capturados no puede ser más de 5 porque hay un ejemplar por legendario

            List<Pokemon> legendariosCapturados = new List<Pokemon>();
            int[] dexLegendarios = new int[PC.Legendarios.Length];

            #region Obtener el nroDex de los pokemons legendarios
            for (int pos = 0; pos < dexLegendarios.Length; pos++)
            {
                dexLegendarios[pos] = PC.Legendarios[pos].NroDex;
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
                LogicaBox boxBL = new LogicaBox(box);
                pokemonesPorTipo += boxBL.ObtenerPorTipo(tipo).Length;
            }

            return pokemonesPorTipo;
        }

        public static int CantPokemonesPorGenero(Genero genero)
        {
            int pokemonesPorGenero = 0;

            foreach (Box box in PC.Boxes)
            {
                LogicaBox boxBL = new LogicaBox(box);
                pokemonesPorGenero += boxBL.ObtenerPorGenero(genero).Length;
            }

            return pokemonesPorGenero;
        }

        public static int CantPokemonesPorPokebola(Pokebola pokebola)
        {
            int pokemonesPorPokebola = 0;

            foreach (Box box in PC.Boxes)
            {
                LogicaBox boxBL = new LogicaBox(box);
                pokemonesPorPokebola += boxBL.ObtenerPorPokebola(pokebola).Length;
            }

            return pokemonesPorPokebola;

        }

        public static int NroCapturasPorDex(short nroDex)
        {
            int capturasPorDex = 0;

            foreach (Box box in PC.Boxes)
            {
                LogicaBox boxBL = new LogicaBox(box);
                capturasPorDex += boxBL.ObtenerPorNroPokedex(nroDex).Length;
            }

            return capturasPorDex;
        }

        public static int CantPokemonesIntercambiados()
        {
            int cantIntercambiados = 0;

            foreach (Box box in PC.Boxes)
            {
                LogicaBox boxBL = new LogicaBox(box);

                foreach (Pokemon pokemon in boxBL.ObtenerTodosLosCapturados())
                {
                    if (pokemon.Entrenador.NombreOT.ToLower().Trim() != PC.Jugador.NombreOT.ToLower().Trim())
                        cantIntercambiados++;
                }
            }

            return cantIntercambiados;
        }

        public static int CantPokemonesPorNivel(byte nivelMin, byte nivelMax)
        {
            int pokemonesDeLvl = 0;

            foreach (Box box in PC.Boxes)
            {
                LogicaBox boxBL = new LogicaBox(box);
                pokemonesDeLvl += boxBL.ObtenerPorRangoNivel(nivelMin, nivelMax).Length;
            }

            return pokemonesDeLvl;
        }

        /// <summary>
        /// Devuelve el nro de dex del pokemon que mas se atrapo
        /// </summary>
        /// <returns></returns>
        public static short PokemonMasCapturado()
        {
            int[] capturasPorDex = new int[PC.PokemonesEnDex];
            int capturasMax = int.MinValue;
            int nroDexMax = 0;

            #region Calcular la cantidad de capturas por cada nroDex
            for (int nroDex = 1; nroDex <= PC.PokemonesEnDex; nroDex++)
            {
                capturasPorDex[nroDex - 1] = NroCapturasPorDex((short)nroDex);
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

            return (short)nroDexMax;
        }

        public static Pokemon PokemonAtrapadoConMasterball()
        {
            foreach (Box box in PC.Boxes)
            {
                LogicaBox boxBL = new LogicaBox(box);

                foreach (Pokemon pokemon in boxBL.ObtenerTodosLosCapturados())
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
            if (PokemonAtrapadoConMasterball() != null)
                return true;

            return false;
        }

    }
}
