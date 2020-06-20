using Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public static class PCDAL
    {
        public static void CargarData() 
        {
            PC.Jugador = new Entrenador(10235, "Red");

            Random rnd = new Random();
            PC.Contactos = new List<Entrenador>
            {
                new Entrenador(rnd.Next(15000, 25000), "Professor OAK"),
                new Entrenador(rnd.Next(15000, 25000), "Blue"),
                new Entrenador(rnd.Next(15000, 25000), "Yellow"),
                new Entrenador(rnd.Next(15000, 25000), "Brock"),
                new Entrenador(rnd.Next(15000, 25000), "Giovanni")
            };

            PC.Legendarios = new Pokemon[]
            {
                new Pokemon(144,"Articuno"),
                new Pokemon(145,"Zapdos"),
                new Pokemon(146,"Moltres"),
                new Pokemon(150,"Mewtwo"),
                new Pokemon(151,"Mew")
            };

            InicializarBoxes();
        }

        public static void InicializarBoxes()
        {
            for (int posBox = 0; posBox < PC.Boxes.Length; posBox++)
            {
                InicializarBox(posBox);
            }
        }

        private static void InicializarBox(int posicionBox)
        {
            PC.Boxes[posicionBox] = new Box(posicionBox);
        }

        public static void ResetearBox(Box box)
        {
            PC.Boxes[box.Id] = new Box(box.Id);
        }

        public static void IntercambiarBoxes(Box box1, Box box2)
        {
            int auxId = box2.Id;

            box2.Id = box1.Id;
            PC.Boxes[box2.Id] = box2; //Box2 se guarda en la posicion de Box1

            box1.Id = auxId;
            PC.Boxes[box1.Id] = box1; //Box1 se guarda en la posicion de Box2

        }

        public static void ModificarCantidadBoxes(int cantidad)
        {
            //Backupear las boxes actuales de la PC
            Box[] boxesACopiar = PC.Boxes;

            //Redefinir la cantidad de cajas de la PC
            PC.NroBoxes = cantidad;
            PC.Boxes = new Box[PC.NroBoxes];

            InicializarBoxes();

            //Copiar las boxes al nuevo array
            for (int pos = 0; pos < boxesACopiar.Length; pos++)
            {
                PC.Boxes[pos] = boxesACopiar[pos];
            }
        }

    }
}
