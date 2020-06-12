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
        static PCDAL()
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

            InicializarBoxes();
        }

        //Traer la informacion de las boxes de alguna fuente de datos(memoria, excel, txt, base de datos...)
        public static void InicializarBoxes()
        {
            for (int posBox = 0; posBox < PC.Boxes.Length; posBox++)
            {
                InicializarBox(posBox);
            }
        }

        public static void InicializarBox(int posicionBox)
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

        public static void RedefinirCantidadDeBoxes(int cantidad)
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
