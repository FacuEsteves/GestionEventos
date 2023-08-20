using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaClases
{
    public class RecaudacionEvento
    {
        public int idevento { get; set; }
        public string evento { get; set; }
        public int puerta { get; set; }
        public int vendidas { get; set; }
        public double recaudacion { get; set; }
        public int lugaresLibres { get; set; }
        public DateTime inicio { get; set; }
        public DateTime fin { get; set; }
    }
}
