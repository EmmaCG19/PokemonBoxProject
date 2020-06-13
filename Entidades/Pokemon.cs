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
        //Si todos los pokemones tendrian el mismo entrenador, deberia definirlo en el constructor estatico
        static Pokemon()
        {
            limiteAtaques = 4;
            //nivelMaximo = 100; --Al validar el ingreso del usuario
            //nroDexLimite = 151; --Al validar el ingreso del usuario
        }

        public Pokemon(short nroDex, string nombre) { _nroDex = nroDex; this.Nombre = nombre; }

        public Pokemon(short nroDex, string nombre, byte nivel, Tipo tipo, Genero sexo, Entrenador entrenador, Pokebola pokebola, string[] ataques, bool tieneItem)
        {
            _nroDex = nroDex;
            _tipo = tipo;
            _pokebola = pokebola;
            _entrenador = entrenador;
            _nivel = nivel;
            _sexo = sexo;

            this.Nombre = nombre;
            this.Ataques = (ataques.Length <= limiteAtaques)? ataques: new string[limiteAtaques];
            this.TieneItem = tieneItem;
        }

        protected static readonly byte limiteAtaques;
        private readonly byte _nivel;
        private readonly Tipo _tipo;
        private readonly Entrenador _entrenador;
        private readonly Pokebola _pokebola;
        private readonly short _nroDex;
        private readonly Genero _sexo;

        public int Id { get; set; }
        public short NroDex { get { return _nroDex; } }
        public string Nombre { get; set; }
        public byte Nivel { get { return _nivel; } }
        public Entrenador Entrenador { get { return _entrenador; } }
        public Tipo Tipo { get { return _tipo; } }
        public Pokebola AtrapadoCon { get { return _pokebola; } }
        public Genero Sexo { get{ return _sexo; } }
        public bool TieneItem { get; set; }
        public string[] Ataques { get; set; }
    }

    
}
