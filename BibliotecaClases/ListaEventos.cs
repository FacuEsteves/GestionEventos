using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObligatorioProg3_Estadio
{
    public class ListaEventosVendedor
    {
        public int id { get; set; }
        public String nombre { get; set; }
        public DateTime fechainicio { get; set; }
        public DateTime fechafin { get; set; }
        public String promocion { get; set; }
        public String disponibilidad { get; set; }
    }
}
