using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ObligatorioProg3_Estadio;
namespace ObligatiorioProg3_Estadio
{
    public interface PersistenciaEvento
    {
        Evento buscar(int id);
        int guardar(Evento evento);
        int eliminar(int id);
        List<Evento> lista();
        int UltimoId();

        Evento buscarNombre (int id);   
    }
}
