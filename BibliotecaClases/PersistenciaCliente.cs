using ObligatorioProg3_Estadio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObligatiorioProg3_Estadio
{
    public interface PersistenciaCliente
    {
        Cliente buscar(String nrodoc);
        int guardar(Cliente c);
        int eliminar(String nrodoc);
        List<Cliente> lista();
    }
}
