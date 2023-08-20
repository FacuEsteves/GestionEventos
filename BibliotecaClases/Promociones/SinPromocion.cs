using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Biblioteca;
using ObligatiorioProg3_Estadio;
using ObligatorioProg3_Estadio;
using Toolkit.Core.CodeGen;
using Toolkit.Core.DataTypes.Semantic.Types;
using QRCoder;
using System.Drawing;
using System.IO;
using SkiaSharp;
using Newtonsoft.Json;
using Toolkit.Core.Extensions;
using System.Windows;
using System.Security.Cryptography.X509Certificates;
using Toolkit.Core.Patterns;

namespace ObligatiorioProg3_Estadio
{
    public class SinPromocion : FabricaPromociones
    {
        public void AplicarPromo(int id) //idcompra
        {
            PersistenciaCompra persistenciaCompra = Sistema.fabricaPeristencia.ObtenerPersistenciaCompra();
            PersistenciaEventoPromocion persistenciaEventoPromocion = Sistema.fabricaPeristencia.ObtenerPersistenciaEventoPromocion();
            PersistenciaDetalleCompra persistenciaDetalleCompra = Sistema.fabricaPeristencia.ObtenerPeristenciaDetalleCompra();
          
            PersistenciaEvento persistenciaEvento = Sistema.fabricaPeristencia.ObtenerPersistenciaEvento();
            PersistenciaPersona persistenciapersona = Sistema.fabricaPeristencia.ObtenerPersistenciaPersona();
            PersistenciaPuerta persistenciaPuerta = Sistema.fabricaPeristencia.ObtenerPersistenciaPuerta();

            Compra comp = persistenciaCompra.buscar(id);
            DetalleCompra det = persistenciaDetalleCompra.buscar(id);
            Evento ev = persistenciaEvento.buscarNombre(comp.evento);
            Persona per = persistenciapersona.buscar(comp.cliente);
            Puerta puerta = persistenciaPuerta.buscar(comp.puerta);
            
            GeneradorQR generator = new GeneradorQR();
            GeneradorPDF generarpdf = new GeneradorPDF();
            List<String> detallecompra = new List<String>();
            String Promoc = "Sin promocion";
            String Socio = "Plan Cliente";
            string[] elementos = { ev.nombre, per.nrodoc, per.nombre, per.apellido, per.correo, comp.puerta.ToString(), puerta.tribuna.ToString(), det.precio.ToString() , Promoc, Socio};
            detallecompra.AddRange(elementos);

            byte[] qr = generator.GenerarQR(Convert.ToString(id));
            byte[] pdf = generarpdf.GenerarPdfConQR(qr, detallecompra);

            PersistenciaEmail.enviarEmail("Envio de entrada", pdf,"Factura de Compra", per.correo);
        }
    }
}
