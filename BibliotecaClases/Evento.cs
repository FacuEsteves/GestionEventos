using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ObligatorioProg3_Estadio;
namespace ObligatiorioProg3_Estadio
{
  public class Evento
    {
        public int idevento { get; set; }
        public string nombre { get; set; }   
        public DateTime fecha_hora_inicio { get; set; }
        public DateTime fecha_hora_fin { get; set; }
    }
}
