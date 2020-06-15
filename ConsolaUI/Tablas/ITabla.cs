using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsolaUI.Tablas
{
    public interface ITabla<T>
    {
        void GenerarTabla(List<T> items);
        void GenerarTabla(T item);
        string GenerarEncabezado();
        string GenerarFila(T item);
        string GenerarLineaSeparacion();
    }
}
