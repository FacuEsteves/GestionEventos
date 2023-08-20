using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObligatiorioProg3_Estadio
{
    public interface PersistenciaEstadio
    {
        Estadio buscar(String nrodoc);

        int guardar(Persona p);
        int eliminar(String nrodoc);
        List<Persona> lista();
    }
}
