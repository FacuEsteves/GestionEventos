using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using ObligatorioProg3_Estadio;
namespace ObligatiorioProg3_Estadio
{
  public interface PersistenciaPuerta
    {
        Puerta buscar(int idpuerta);
        int guardar(Puerta p);
        int eliminar(int id);
        List<Puerta> lista();
    }
}
