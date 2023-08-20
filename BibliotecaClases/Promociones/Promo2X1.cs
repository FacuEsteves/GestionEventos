using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ObligatorioProg3_Estadio;
using ObligatiorioProg3_Estadio;

namespace ObligatiorioProg3_Estadio
{
    public class Promo2X1 : FabricaPromociones
    {
     
        public void AplicarPromo(int id) //idcompra
        {
            PersistenciaDetalleCompra persistenciaDetalleCompra = Sistema.fabricaPeristencia.ObtenerPeristenciaDetalleCompra();
            PersistenciaCompra persistenciaCompra = Sistema.fabricaPeristencia.ObtenerPersistenciaCompra();
            PersistenciaEventoPromocion persistenciaEventoPromocion = Sistema.fabricaPeristencia.ObtenerPersistenciaEventoPromocion();
            PersistenciaPromocion persistenciaPromocion = Sistema.fabricaPeristencia.ObtenerPersistenciaPromocion();
            PersistenciaEvento persistenciaEvento = Sistema.fabricaPeristencia.ObtenerPersistenciaEvento();
            PersistenciaPersona persistenciapersona = Sistema.fabricaPeristencia.ObtenerPersistenciaPersona();
            PersistenciaPuerta persistenciaPuerta = Sistema.fabricaPeristencia.ObtenerPersistenciaPuerta();

            GeneradorQR generator = new GeneradorQR();
            GeneradorPDF generarpdf = new GeneradorPDF();
            List<String> detallecompra = new List<String>();
            DetalleCompra det = persistenciaDetalleCompra.buscar(id); //buscamos la compra
            Compra comp = persistenciaCompra.buscar(id); //buscamos el id del evento para saber el % de la promocion
            EventoPromocion ep = persistenciaEventoPromocion.buscar(comp.evento); // descuento 
            Promocion promo = persistenciaPromocion.buscar(ep.promocion); // devolvera el nombre de la promo

            Evento ev = persistenciaEvento.buscarNombre(comp.evento);
            Persona per = persistenciapersona.buscar(comp.cliente);
            Puerta puerta = persistenciaPuerta.buscar(comp.puerta);
           
            det.acceso = 2; //2x1
            string Socio = "Plan Cliente";
            string[] elementos = { ev.nombre, per.nrodoc, per.nombre, per.apellido, per.correo, comp.puerta.ToString(), puerta.tribuna.ToString(), det.precio.ToString(), promo.descripcion, Socio};
            detallecompra.AddRange(elementos);

            persistenciaDetalleCompra.guardar(det);

            byte[] qr = generator.GenerarQR(Convert.ToString(id));
            byte[] pdf = generarpdf.GenerarPdfConQR(qr, detallecompra);
          
            PersistenciaEmail.enviarEmail("Envio de entrada", pdf, "Factura de Compra", per.correo);
           
        }
    }
}
