using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Entidades;
using Logica;
using ConsolaUI.Utilidades;

namespace ConsolaUI.Menues
{
    public static class MenuPC
    {
        static MenuPC()
        {
            _cantMailsEnviados = 0;
        }

        static int _cantMailsEnviados;
        public static int MailsEnviados { get { return _cantMailsEnviados; } }

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
                        EnviarMail();
                        break;
                    case OpcionesMenuPC.Informe:
                        MenuInforme.Iniciar();
                        break;
                    case OpcionesMenuPC.Salir:
                        #region Salir del programa
                        if (Validaciones.ValidarSoN("Desea salir del programa?"))
                        {
                            seguirEnMenu = false;
                            Thread.Sleep(1500);
                            Environment.Exit(0);
                        }
                        #endregion
                        break;
                    default:
                        break;
                }

            } while (seguirEnMenu);

        }

        /// <summary>
        /// Envia un mail a uno de los NPC agendados como contacto
        /// </summary>
        static void EnviarMail()
        {
            Menu.HeaderPrincipal();
            ListaContactos();

            Entrenador destinatario = ValidarContacto();
            string mensaje = Validaciones.ValidarCadena("Escriba su mensaje");

            Menu.HeaderPrincipal();
            Menu.CambiarColor(ConsoleColor.Yellow);
            Console.WriteLine("Para: {0}", destinatario.NombreOT);
            Console.WriteLine("{0}{1}\n", Menu.Identar(12), mensaje);
            Menu.ResetearColor();

            if (Validaciones.ValidarSoN("Desea enviar el mail?"))
            {
                Console.WriteLine("\nEl mail ha sido enviado exitosamente.");
                _cantMailsEnviados++;
            }
            else
            {
                Menu.CambiarColor(ConsoleColor.Red);
                Console.WriteLine("\nEl mail ha sido cancelado.");
            }

            Menu.EspereUnaTecla();
        }

        static Entrenador ValidarContacto()
        {
            int contacto;

            Console.Write("Seleccione el destinario: ");

            while (!int.TryParse(Console.ReadLine(), out contacto) || contacto < 1 || contacto > PC.Contactos.Count)
            {
                Menu.CambiarColor(ConsoleColor.Red);
                Console.WriteLine("La opcion seleccionado es inválida, vuelva a intentarlo...\n");
                Menu.ResetearColor();
                Console.Write("Seleccione el destinario: ");
            }

            return PC.Contactos[contacto - 1];
        }

        static void ListaContactos()
        {
            StringBuilder sb = new StringBuilder();
            int cont = 0;

            sb.AppendLine("Lista de contactos: ");
            foreach (Entrenador entrenador in PC.Contactos)
            {
                sb.AppendFormat("{0}{1}. {2}\n", Menu.Identar(3), ++cont, entrenador.NombreOT);

            }

            Console.WriteLine(sb.ToString());
        }

        static OpcionesMenuPC ValidarIngresoUsuario()
        {
            OpcionesMenuPC opcionSeleccionada;

            Console.Write("Seleccione una opcion: ");
            while (!EsOpcionValida(Console.ReadKey(), out opcionSeleccionada))
            {
                Menu.CambiarColor(ConsoleColor.Red);
                Console.WriteLine("\nLa opcion ingresada no es válida, vuelva a intentarlo..");
                Menu.ResetearColor();
                Console.Write("\nSeleccione una opcion: ");
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
