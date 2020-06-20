using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Entidades.Excepciones;

namespace Entidades
{
    public class Pokemon
    {
        static Pokemon()
        {
            limiteAtaques = 4;
        }

        public Pokemon(short nroDex, string nombre) { _nroDex = nroDex; this.Nombre = nombre; }

        public Pokemon(short nroDex, string nombre, byte nivel, Tipo tipo, Genero sexo, Entrenador entrenador, Pokebola pokebola, string[] ataques, bool tieneItem)
        {
            _nroDex = nroDex;
            _tipo = tipo;
            _pokebola = pokebola;
            _entrenador = entrenador;
            _nivel = nivel;
            _genero = sexo;

            #region Inicializar array de ataques
            _ataquesVacio = new string[limiteAtaques];
            
            for (int i = 0; i < limiteAtaques; i++)
            {
                _ataquesVacio[i] = "Espacio disponible";
            }
            #endregion

            this.Ataques = (ataques.Length <= limiteAtaques)? ataques: _ataquesVacio; 
            this.TieneItem = tieneItem;
            this.Nombre = nombre;
        }

        protected static readonly byte limiteAtaques;
        private readonly string [] _ataquesVacio;
        private readonly byte _nivel;
        private readonly Tipo _tipo;
        private readonly Entrenador _entrenador;
        private readonly Pokebola _pokebola;
        private readonly short _nroDex;
        private readonly Genero _genero;

        public int Id { get; set; }
        public short NroDex { get { return _nroDex; } }
        public string Nombre { get; set; }
        public byte Nivel { get { return _nivel; } }
        public Entrenador Entrenador { get { return _entrenador; } }
        public Tipo Tipo { get { return _tipo; } }
        public Pokebola AtrapadoCon { get { return _pokebola; } }
        public Genero Genero { get{ return _genero; } }
        public bool TieneItem { get; set; }
        public string[] Ataques { get; set; }
    }

    
}
