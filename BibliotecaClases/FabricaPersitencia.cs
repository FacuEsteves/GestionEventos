using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ObligatorioProg3_Estadio;

namespace ObligatiorioProg3_Estadio
{
    public interface FabricaPersitencia
    {
        PersistenciaEvento ObtenerPersistenciaEvento();
        PersistenciaCompra ObtenerPersistenciaCompra();
        PersistenciaPersona ObtenerPersistenciaPersona();
        PersistenciaCliente ObtenerPersistenciaCliente();
        PersistenciaUsuario ObtenerPersistenciaUsuario();
        PersistenciaTribuna ObtenerPersistenciaTribuna();
        PersistenciaPuerta ObtenerPersistenciaPuerta();
        PersistenciaDetalleCompra ObtenerPeristenciaDetalleCompra();
        PersistenciaDatosEvento ObtenerPersistenciaDatosEvento();
        PersistenciaEstadio ObtenerPersistenciaEstadio();
        PersistenciaPromocion ObtenerPersistenciaPromocion();
        PersistenciaEstadioUruguay ObtenerPersistenciaEstadioUruguay();
        PersistenciaEventoPromocion ObtenerPersistenciaEventoPromocion();
    }
}

