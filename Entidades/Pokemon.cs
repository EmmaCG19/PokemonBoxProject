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
        }

        public Pokemon(short nroDex, string nombre, byte nivel, Tipo tipo, Sexo sexo, Entrenador entrenador, Pokebola pokebola, string[] ataques, bool tieneItem)
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
        private readonly Sexo _sexo;

        public int Id { get; set; }
        public short NroDex { get { return _nroDex; } }
        public string Nombre { get; set; }
        public byte Nivel { get { return _nivel; } }
        public Entrenador Entrenador { get { return _entrenador; } }
        public Tipo Tipo { get { return _tipo; } }
        public Pokebola AtrapadoCon { get { return _pokebola; } }
        public Sexo Sexo { get{ return _sexo; } }
        public bool TieneItem { get; set; }
        public string[] Ataques { get; set; }

    }

    public enum Tipo
    {
        NoAsignado = 0,
        Normal,
        Electrico,
        Agua,
        Fuego,
        Planta,
        Tierra,
        Roca,
        Volador,
        Bicho,
        Lucha,
        Veneno,
        Fantasma,
        Siniestro,
        Psiquico,
        Hielo,
        Acero,
        Dragon,
    }

    public enum Pokebola
    {
        NoAsignado = 0,
        Pokeball,
        Greatball,
        Ultraball,
        Safariball,
        Masterball,
    }

    public enum Sexo
    {
        NoAsignado = 0,
        Masculino,
        Femenino,
    }
}
