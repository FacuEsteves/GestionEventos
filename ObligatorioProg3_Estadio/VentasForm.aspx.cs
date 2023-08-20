using ObligatiorioProg3_Estadio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using QRCoder;
using System.Drawing;
using System.IO;
using Biblioteca;
using SkiaSharp;
using Newtonsoft.Json;
using Toolkit.Core.Extensions;
using System.Windows;
using System.Web.DynamicData;
using System.Security.Cryptography.X509Certificates;
using System.Diagnostics;
using System.Text;
using Toolkit.Core.Patterns;
using Toolkit.Core.CodeGen;


namespace ObligatorioProg3_Estadio
{
    public partial class VentasForm : System.Web.UI.Page
    {
        protected List<ViewDisponibilidad> puertasDisponibles;
        string id = null;
        protected string MiArregloJson
        {
            get { return JsonConvert.SerializeObject(puertasDisponibles); }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            id = Request.QueryString["id"];

            PersistenciaEvento persistenciaEvento = Sistema.fabricaPeristencia.ObtenerPersistenciaEvento();
            int idevento = Convert.ToInt32(id);
            Evento ev = persistenciaEvento.buscarNombre(idevento);
            string nombreevento = ev.nombre;
            //lbl_aviso.Text = nombreevento.ToString();
            this.Page.Session["nombreevento"] = nombreevento.ToString();
            cargarsvg();
            puertasDisponibles = (List<ViewDisponibilidad>)this.Page.Session["puertasDis"];            
        }
        protected void btn_registrarCompra_Click(object sender, EventArgs e)
        {
            if (txt_CI_cliente.Text == "" || txt_nombre.Text == "" || txt_apellido.Text == "" || txt_correo.Text == "" || HiddenField.Value == "")
            {
                lbl_aviso.Text = "Ingrese la informacion necesaria para realizar la compra";
                return;
            } 


            PersistenciaPersona persistenciaPer = Sistema.fabricaPeristencia.ObtenerPersistenciaPersona();
            PersistenciaCliente persistenciaCli = Sistema.fabricaPeristencia.ObtenerPersistenciaCliente();
            PersistenciaCompra persistenciaCompra = Sistema.fabricaPeristencia.ObtenerPersistenciaCompra();
            PersistenciaPuerta persistenciapuerta = Sistema.fabricaPeristencia.ObtenerPersistenciaPuerta();
            PersistenciaDatosEvento persistenciaDatosEvento = Sistema.fabricaPeristencia.ObtenerPersistenciaDatosEvento();
            PersistenciaEventoPromocion persistenciaEventoPromocion = Sistema.fabricaPeristencia.ObtenerPersistenciaEventoPromocion();
            PersistenciaDetalleCompra persistenciaDetalleCompra = Sistema.fabricaPeristencia.ObtenerPeristenciaDetalleCompra();

            List<String> detallecompra = new List<String>();
            int idp = Convert.ToInt32(HiddenField.Value);
            Puerta p = persistenciapuerta.buscar(idp);
            int idev = Convert.ToInt32(Request.QueryString["id"]);
            DatosEvento de = persistenciaDatosEvento.buscarPrecio(idev, idp);
            Persona per = new Persona();
            Cliente cli = new Cliente();
            GeneradorQR generator = new GeneradorQR();
            GeneradorPDF generarpdf = new GeneradorPDF();
            Compra compra = new Compra();

            per.nrodoc = txt_CI_cliente.Text;
            per.nombre = txt_nombre.Text;
            per.apellido = txt_apellido.Text;
            per.correo = txt_correo.Text;
            cli.nrodoc = per.nrodoc;
            cli.socio = chech_SOCIO.Checked;
            
            string idtribuna = p.tribuna;
            string evento = Convert.ToString(this.Page.Session["nombreevento"]);
            string costo = Convert.ToString(de.costo);
            
            int guardadosPersona = persistenciaPer.guardar(per);
            //se ejecuta esto cuando todo salio bien
            if (guardadosPersona != 0)
                {
                int guardadosClientes = persistenciaCli.guardar(cli);
                compra.evento = idev;
                compra.puerta = idp;
                compra.cliente = per.nrodoc;
                int guardarcompra = persistenciaCompra.guardar(compra);


                if (guardarcompra != 0)
                {
                    DetalleCompra detallecompra1 = new DetalleCompra();
                    Compra com = persistenciaCompra.buscarCliente(cli.nrodoc); //obtener el idcompra para detalle compra
                    detallecompra1.compra = com.idcompra;
                    detallecompra1.precio = de.costo;
                    detallecompra1.acceso = 1;
                    int GuardarDC = persistenciaDetalleCompra.guardar(detallecompra1);
                        if (GuardarDC != 0)
                        {                               
                            EventoPromocion ev = persistenciaEventoPromocion.buscar(idev);
                            ListadePromos FabricaPromo = new ListadePromos();
                            FabricaPromociones fabricaPromociones = FabricaPromo.ObtenerPromo(ev);
                            fabricaPromociones.AplicarPromo(com.idcompra);
                            lbl_aviso.Text = "Compra realizada con éxito!";                              
                        }
                }
            }     

            cargarsvg();
        }

        protected void chech_SOCIO_CheckedChanged(object sender, EventArgs e)
        {
            PersistenciaCliente persistenciaCli = Sistema.fabricaPeristencia.ObtenerPersistenciaCliente();
           /* if (chech_SOCIO.Checked == true)
            {
                
            }*/
        }

        public void cargarsvg()
        {
            PersistenciaPuerta persistenciaPuerta = Sistema.fabricaPeristencia.ObtenerPersistenciaPuerta();
            List<Puerta> puertas = persistenciaPuerta.lista();

            PersistenciaEstadioUruguay persistenciaEstadio = Sistema.fabricaPeristencia.ObtenerPersistenciaEstadioUruguay();
            List<ViewDisponibilidad> disponibilidad = persistenciaEstadio.V_DATOSEVENTO(id.ToInt());

            ViewDisponibilidad view = null;

            //ELIMINAR PUERTAS YA REGISTRADAS
            for (int j = 0; j < disponibilidad.Count; j++)
            {
                for (int i = 0; i < puertas.Count; i++)
                {
                    if (disponibilidad[j].idpuerta == puertas[i].idpuerta)
                    {
                        puertas.Remove(puertas[i]);
                        break;
                    }
                }
            }

            // AGREGRAR RESTO PUERTAS 
            for (int i = 0; i < puertas.Count; i++)
            {
                view = new ViewDisponibilidad();
                view.idevento = 0;
                view.idpuerta = puertas[i].idpuerta;
                view.disponible = -1;
                disponibilidad.Add(view);
            }

            this.Page.Session["puertasDis"] = disponibilidad;
            puertasDisponibles = (List<ViewDisponibilidad>)this.Page.Session["puertasDis"];
        }

        protected void txt_CI_cliente_TextChanged(object sender, EventArgs e)
        {
            PersistenciaCliente persistenciaCli = Sistema.fabricaPeristencia.ObtenerPersistenciaCliente();
            Persona per = new Persona();
            per.nrodoc = txt_CI_cliente.Text;
            Cliente c = persistenciaCli.buscar(per.nrodoc);
           if(c != null) { 
            if (c.nrodoc == per.nrodoc)
            {
                txt_CI_cliente.Text = c.nrodoc;
                txt_nombre.Text = c.nombre;
                txt_apellido.Text = c.apellido;
                txt_correo.Text = c.correo;
                chech_SOCIO.Checked = c.socio;

            }
            }
            else
            {
                txt_nombre.Text = "";
                txt_apellido.Text = "";
                txt_correo.Text = "";
                chech_SOCIO.Checked = false;
            }
        }
    }
}