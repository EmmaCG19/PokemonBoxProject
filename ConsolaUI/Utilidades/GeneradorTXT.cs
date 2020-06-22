using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ConsolaUI.Utilidades
{
    public static class GeneradorTXT
    {
        static string GenerarTimeStamp() 
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("{0}-{1}-{2}", DateTime.Now.Day, DateTime.Now.Month, DateTime.Now.Year);
            sb.AppendFormat("__");
            sb.AppendFormat("{0}-{1}-{2}", (DateTime.Now.Hour < 10)? string.Concat("0",DateTime.Now.Hour.ToString()) : DateTime.Now.Hour.ToString(), DateTime.Now.Minute, DateTime.Now.Second);

            return sb.ToString();
        }

        /// <summary>
        /// Generar un archivo txt con la informacion correspondiente
        /// </summary>
        /// <param name="datos"></param>
        public static void GenerarArchivo(string nombre, string info)
        {
            string path = string.Format("{0}_{1}.txt", nombre, GenerarTimeStamp());
            using (StreamWriter sw = new StreamWriter(path))
            {
                sw.WriteLine(DateTime.Now.ToString());
                sw.WriteLine(info);
            }
        }

    }
}
