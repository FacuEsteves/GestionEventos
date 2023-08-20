using ObligatiorioProg3_Estadio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObligatorioProg3_Estadio
{
    public class Usuario : Persona
    {
        public String nombreUsuario { get; set; }
        public String contrasenia { get; set; }
        public Boolean rol { get; set; }

    }
}
