using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Entidades;
using Logica;

namespace ConsolaUI
{
    public class MenuPC
    {
        public static void Iniciar()
        {
            LogicaPC.IniciarPC();

            bool seguirEnMenu = true;

            do
            {
                Menu.HeaderPrincipal();
                Menu.BannerMenu("Menu PC");
                OpcionesMenu();

                switch (ValidarIngresoUsuario())
                {
                    case OpcionesMenuPC.Boxes:
                        MenuBoxes.Iniciar();
                        break;
                    case OpcionesMenuPC.Mail:
                        break;
                    case OpcionesMenuPC.Informe:
                        break;
                    case OpcionesMenuPC.Salir:
                        seguirEnMenu = false;
                        Thread.Sleep(3000);
                        Environment.Exit(0);
                        break;
                    default:
                        break;
                }

                //Menu.EspereUnaTecla(); -- Incluirlo en todas las acciones que requieran un retorno al menu

            } while (seguirEnMenu);
            
        }

        static OpcionesMenuPC ValidarIngresoUsuario()
        {
            OpcionesMenuPC opcionSeleccionada;

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

        static bool EsOpcionValida(ConsoleKeyInfo teclaIngresada, out OpcionesMenuPC menuSeleccionado)
        {
            menuSeleccionado = (OpcionesMenuPC)teclaIngresada.Key;

            //El usuario presiona una tecla y esta tiene que coincidir con las disponibles
            switch (menuSeleccionado)
            {
                case OpcionesMenuPC.Boxes:
                case OpcionesMenuPC.Mail:
                case OpcionesMenuPC.Informe:
                case OpcionesMenuPC.Salir:
                    return true;
                default:
                    return false;
            }
        }

        static void OpcionesMenu()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("1. Ir a la seccion BOX");
            sb.AppendLine("2. Enviar Mail");
            sb.AppendLine("3. Informes");
            sb.AppendLine("4. Salir");
            sb.AppendLine();

            Console.WriteLine(sb.ToString());
        }

        enum OpcionesMenuPC
        {
            Boxes = ConsoleKey.NumPad1,
            Mail = ConsoleKey.NumPad2,
            Informe = ConsoleKey.NumPad3,
            Salir = ConsoleKey.NumPad4
        }

    }
}
