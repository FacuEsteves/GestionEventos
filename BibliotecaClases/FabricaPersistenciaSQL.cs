using ObligatorioProg3_Estadio;
using ObligatiorioProg3_Estadio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObligatiorioProg3_Estadio
{
  
    public class FabricaPersistenciaSQL : FabricaPersitencia
    {
        public PersistenciaCliente ObtenerPersistenciaCliente()
        {
            return new PersistenciaClienteSQL();
        }

        public PersistenciaCompra ObtenerPersistenciaCompra()
        {
            return new PersistenciaCompraSQL();
        }

        public PersistenciaEvento ObtenerPersistenciaEvento()
        {
            return new PersistenciaEventoSQL();
        }

        public PersistenciaPersona ObtenerPersistenciaPersona()
        {
            return new PersistenciaPersonaSQL();
        }

        public PersistenciaPuerta ObtenerPersistenciaPuerta()
        {
            return new PersistenciaPuertaSQL();
        }

        public PersistenciaTribuna ObtenerPersistenciaTribuna()
        {
            return new PersistenciaTribunaSQL();
        }

        public PersistenciaUsuario ObtenerPersistenciaUsuario()
        {
            return new PersistenciaUsuarioSQL();
        }
        public PersistenciaDetalleCompra ObtenerPeristenciaDetalleCompra()
        {
            return new PersistenciaDetalleCompraSQL();
        }

        public PersistenciaDatosEvento ObtenerPersistenciaDatosEvento()
        {
            return new PersistenciaDatosEventoSQL();
        }

        public PersistenciaEstadio ObtenerPersistenciaEstadio()
        {
            return new PersistenciaEstadioSQL();
        }

        public PersistenciaPromocion ObtenerPersistenciaPromocion()
        {
            return new PersistenciaPromocionSQL();
        }

        public PersistenciaEstadioUruguay ObtenerPersistenciaEstadioUruguay()
        {
            return new PersistenciaEstadioUruguaySQL();
        }

        public PersistenciaEventoPromocion ObtenerPersistenciaEventoPromocion()
        {
           return new PersistenciaEventoPromocionSQL();
        }
    }
}
