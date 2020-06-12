using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Entidades;
using Logica;

namespace ConsolaUI
{
    public class MenuPC
    {
        public void Iniciar()
        {
            Menu.HeaderPrincipal();
            Menu.BannerMenu("Menu Principal");

            OpcionesMenuPrincipal();

            int opcionSeleccionada = default;
            while (!int.TryParse(Console.ReadLine(), out opcionSeleccionada))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("La opcion ingresada no es válida, vuelva a intentarlo..");
            }

            Console.ResetColor();

            switch ((OpcionesMenu)opcionSeleccionada)
            {
                case OpcionesMenu.Box:
                    //Submenu BOX
                    Console.WriteLine("Accedio al box");
                    break;
                case OpcionesMenu.Mail:
                    Console.WriteLine("Accedio al mail");
                    break;
                case OpcionesMenu.Informe:
                    break;
                case OpcionesMenu.Salir:
                    Console.WriteLine("Salio");
                    break;
                default:
                    break;
            }
        }

        public static void OpcionesMenuPrincipal()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("1. Ir a la seccion BOX");
            sb.AppendLine("2. Enviar Mail");
            sb.AppendLine("3. Salir");
            sb.AppendLine();

            Console.WriteLine("{0, -20}", sb);
        }

        enum OpcionesMenu
        {
            Box = 1,
            Mail,
            Informe,
            Salir
        }

    }
}
