using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logica;


//Falta limpiar la pantalla
namespace ConsolaUI
{
    public static class MenuBox
    {
        public static void Iniciar()
        {
            bool seguirEnMenu = true;

            do
            {
                Menu.HeaderPrincipal();
                Menu.BannerMenu(LogicaPC.BoxSeleccionada.Nombre);
                OpcionesMenu();

                Menu.CambiarColor(ConsoleColor.Green);
                switch (ValidarIngresoUsuario())
                {
                    case OpcionesMenuBox.Mostrar:
                        MenuMostrarPokemon.Iniciar();
                        break;
                    case OpcionesMenuBox.Buscar:
                        break;
                    case OpcionesMenuBox.Guardar:
                        break;
                    case OpcionesMenuBox.Liberar:
                        break;
                    case OpcionesMenuBox.Modificar:
                        break;
                    case OpcionesMenuBox.Mover:
                        break;
                    case OpcionesMenuBox.CambiarNombreBox:
                        break;
                    case OpcionesMenuBox.CambiarFondoBox:
                        break;
                    case OpcionesMenuBox.VolverMenuBoxes:
                        seguirEnMenu = false;
                        break;
                    default:
                        break;
                }

            } while (seguirEnMenu);

        }

        

        static OpcionesMenuBox ValidarIngresoUsuario() 
        {
            OpcionesMenuBox opcionSeleccionada;

            Console.Write("Ingrese su opcion: ");
            while (!EsOpcionValida(Console.ReadKey(), out opcionSeleccionada))
            {
                Menu.CambiarColor(ConsoleColor.Red);
                Console.WriteLine("\nLa opcion ingresada no es válida, vuelva a intentarlo..");
                Menu.ResetearColor();
                Console.Write("Ingrese su opcion: ");
            }
            Console.WriteLine();

            return opcionSeleccionada;
        }


        static bool EsOpcionValida(ConsoleKeyInfo teclaIngresada, out OpcionesMenuBox menuSeleccionado)
        {
            menuSeleccionado = (OpcionesMenuBox)teclaIngresada.Key;

            //El usuario presiona una tecla y esta tiene que coincidir con las disponibles
            switch (menuSeleccionado)
            {
                case OpcionesMenuBox.Mostrar:
                case OpcionesMenuBox.Buscar:
                case OpcionesMenuBox.Guardar:
                case OpcionesMenuBox.Liberar:
                case OpcionesMenuBox.Modificar:
                case OpcionesMenuBox.Mover:
                case OpcionesMenuBox.CambiarNombreBox:
                case OpcionesMenuBox.CambiarFondoBox:
                case OpcionesMenuBox.VolverMenuBoxes:
                    return true;
                default:
                    return false;
            }

        }

        static void OpcionesMenu()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("1. Mostrar Pokemones"); //Mostrar pokemones ordenados aca?
            sb.AppendLine("2. Buscar Pokemon");
            sb.AppendLine("3. Guardar Pokemon");
            sb.AppendLine("4. Liberar Pokemon");
            sb.AppendLine("5. Modificar Pokemon");
            sb.AppendLine("6. Mover Pokemon");
            sb.AppendLine("7. Cambiar Nombre de Box");
            sb.AppendLine("8. Cambiar Fondo de Box");
            sb.AppendLine("9. Volver al menu de Boxes");
            sb.AppendLine();

            Console.WriteLine(sb.ToString());
        }

        enum OpcionesMenuBox
        {
            Mostrar = ConsoleKey.NumPad1,
            Buscar = ConsoleKey.NumPad2,
            Guardar = ConsoleKey.NumPad3,
            Liberar = ConsoleKey.NumPad4,
            Modificar = ConsoleKey.NumPad5,
            Mover = ConsoleKey.NumPad6,
            CambiarNombreBox = ConsoleKey.NumPad7,
            CambiarFondoBox = ConsoleKey.NumPad8,
            VolverMenuBoxes = ConsoleKey.NumPad9,

        }
    }
}
