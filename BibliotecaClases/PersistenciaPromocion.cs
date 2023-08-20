using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObligatiorioProg3_Estadio
{
public interface PersistenciaPromocion
    {
        Promocion buscar(int idpromo);

        int guardar(Persona p);
        int eliminar(String nrodoc);
        List<Persona> lista();
    }
}
