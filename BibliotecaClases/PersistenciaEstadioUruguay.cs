using BibliotecaClases;
using ObligatorioProg3_Estadio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObligatiorioProg3_Estadio
{
    public interface PersistenciaEstadioUruguay
    {
        List<RecaudacionEvento> ResumenRecaudacion(int idevento);
        List<RecaudacionEvento> RecaudacionEventos(int idevento);
        List<RecaudacionEvento> ResumenRecaudacionFecha(DateTime inicio,DateTime fin);
        List<RecaudacionEvento> RecaudacionEventosFecha(DateTime inicio, DateTime fin);
        List<Evento> EventosPorFecha(DateTime fecha, String tipo);
        List<ViewDisponibilidad> V_DATOSEVENTO(int idevento);
        List<Evento> FiltroEvento(String nombre_evento, DateTime fecha_inicio, DateTime fecha_fin);
    }
}
