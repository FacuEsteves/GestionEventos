using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ObligatiorioProg3_Estadio;
using ObligatorioProg3_Estadio;
using BibliotecaClases;
using Biblioteca;
using Toolkit.Drawing.Geom.Shapes;

namespace ObligatiorioProg3_Estadio
{
    public class PromoDescuento : FabricaPromociones
    {
        public void AplicarPromo(int id)
        {
            PersistenciaDetalleCompra persistenciaDetalleCompra = Sistema.fabricaPeristencia.ObtenerPeristenciaDetalleCompra();
            PersistenciaCompra persistenciaCompra = Sistema.fabricaPeristencia.ObtenerPersistenciaCompra();
            PersistenciaEventoPromocion persistenciaEventoPromocion = Sistema.fabricaPeristencia.ObtenerPersistenciaEventoPromocion();
            PersistenciaEvento persistenciaEvento = Sistema.fabricaPeristencia.ObtenerPersistenciaEvento();
            PersistenciaPersona persistenciapersona = Sistema.fabricaPeristencia.ObtenerPersistenciaPersona();
            PersistenciaPuerta persistenciaPuerta = Sistema.fabricaPeristencia.ObtenerPersistenciaPuerta();
            PersistenciaPromocion persistenciaPromocion = Sistema.fabricaPeristencia.ObtenerPersistenciaPromocion();

            DetalleCompra det = persistenciaDetalleCompra.buscar(id); //buscamos la compra
            Compra comp = persistenciaCompra.buscar(id); //buscamos el id del evento para saber el % de la promocion
            EventoPromocion ep = persistenciaEventoPromocion.buscar(comp.evento); // descuento 
            Promocion promo = persistenciaPromocion.buscar(ep.promocion); // devolvera el nombre de la promo
            double resultado = 0;
            
            resultado = det.precio - ((det.precio * ep.Descuento) / 100);
            det.precio = resultado;
            det.acceso = 1;
            persistenciaDetalleCompra.guardar(det);

            Evento ev = persistenciaEvento.buscarNombre(comp.evento);
            Persona per = persistenciapersona.buscar(comp.cliente);
            Puerta puerta = persistenciaPuerta.buscar(comp.puerta);

            GeneradorQR generator = new GeneradorQR();
            GeneradorPDF generarpdf = new GeneradorPDF();
            List<String> detallecompra = new List<String>();

            String Socio = "-";
            string[] elementos = { ev.nombre, per.nrodoc, per.nombre, per.apellido, per.correo, comp.puerta.ToString(), puerta.tribuna.ToString(), det.precio.ToString(),promo.descripcion, Socio};
            detallecompra.AddRange(elementos);

            byte[] qr = generator.GenerarQR(Convert.ToString(id));
            byte[] pdf = generarpdf.GenerarPdfConQR(qr, detallecompra);

            PersistenciaEmail.enviarEmail("Envio de entrada", pdf,"Factura de Compra", per.correo);

        }
    }
}
