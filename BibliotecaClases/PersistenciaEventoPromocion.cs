using ObligatiorioProg3_Estadio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObligatorioProg3_Estadio
{
    public interface PersistenciaEventoPromocion
    {
        EventoPromocion buscar(int idev);
        int guardar(EventoPromocion eventoPromocion);

    }
}
