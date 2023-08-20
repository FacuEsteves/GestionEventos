using ObligatiorioProg3_Estadio;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObligatiorioProg3_Estadio
{
    public class DatosEvento 
    {
        public int evento { get; set; }
        public int puerta { get; set; }
        public int capacidad { get; set; }
        public Double costo { get; set; }

    }
}
