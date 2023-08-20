using BibliotecaClases;
using ObligatiorioProg3_Estadio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ObligatorioProg3_Estadio
{
    public partial class ConsultaRecaudacion : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            mensaje.Text = "";
            mensaje2.Text = "";

            if (!IsPostBack)
            {
                PersistenciaEvento persistenciaEvento = Sistema.fabricaPeristencia.ObtenerPersistenciaEvento();

                List <Evento>eventos= persistenciaEvento.lista();

                gridEventosFecha.DataSource = eventos;
                gridEventosFecha.DataBind();
            }
        }

        protected void btnFiltroFecha_Click(object sender, EventArgs e)
        {
            try
            {
                PersistenciaEstadioUruguay persistencia = Sistema.fabricaPeristencia.ObtenerPersistenciaEstadioUruguay();
                List<Evento> eventos= persistencia.EventosPorFecha(DateTime.Parse(txtFecha.Text), ddEvento.SelectedValue);

                if (eventos.Count != 0)
                {
                    Session["EventosXFecha"] = eventos;
                    gridEventosFecha.DataSource = eventos;
                    gridEventosFecha.DataBind();
                }
                else
                {
                    mensaje2.Text = "No hay eventos registrados en la fecha seleccionada";

                    Session["EventosXFecha"] = null;

                    gridEventosFecha.DataSource = eventos;
                    gridEventosFecha.DataBind();
                }
            }
            catch (FormatException ex)
            {
                mensaje2.Text = "No se ingreso un valor para realizar la busqueda";

                Session["EventosXFecha"] = null;

                gridEventosFecha.DataSource = "";
                gridEventosFecha.DataBind();

                return;
            }
        }
        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                int idevento = Convert.ToInt32(txtIdevento.Text);

                List<RecaudacionEvento> resumen = new List<RecaudacionEvento>();
                List<RecaudacionEvento> detalle = new List<RecaudacionEvento>();


                PersistenciaEstadioUruguay persistenciaEstadioUruguay = Sistema.fabricaPeristencia.ObtenerPersistenciaEstadioUruguay();

                resumen = persistenciaEstadioUruguay.ResumenRecaudacion(idevento);
                detalle = persistenciaEstadioUruguay.RecaudacionEventos(idevento);

                if (resumen != null && detalle != null)
                {
                    gridResumen.DataSource = resumen;
                    gridRecaudacion.DataSource = detalle;

                    Session["Resumen"] = resumen;
                    Session["Detalle"] = detalle;

                    gridResumen.DataBind();
                    gridRecaudacion.DataBind();

                }
                else
                {
                    errorCargarResumen();
                    mensaje.Text = "El evento buscado no existe / o no proporciono los datos correctos";
                }
            }
            catch(FormatException ex)
            {
                errorCargarResumen();
                mensaje.Text = "Ingrese Formato valido";
                return;
            }

        }

        protected void btnFecha_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime inicio = DateTime.Parse(fInicio.Text);
                DateTime fin = DateTime.Parse(fFin.Text);

                if (fin < inicio)
                {
                    mensaje.Text = "La fecha de fin es menor a la fecha de inicio";
                    return;
                }

                string inicioFormateado = inicio.ToString("yyyy-MM-dd");
                string finFormateado = fin.ToString("yyyy-MM-dd");

                List<RecaudacionEvento> resumen = new List<RecaudacionEvento>();
                List<RecaudacionEvento> detalle = new List<RecaudacionEvento>();


                PersistenciaEstadioUruguay persistenciaEstadioUruguay = Sistema.fabricaPeristencia.ObtenerPersistenciaEstadioUruguay();

                resumen = persistenciaEstadioUruguay.ResumenRecaudacionFecha(Convert.ToDateTime(inicioFormateado),Convert.ToDateTime(finFormateado));
                detalle = persistenciaEstadioUruguay.RecaudacionEventosFecha(Convert.ToDateTime(inicioFormateado),Convert.ToDateTime(finFormateado));

                if (resumen != null && detalle!=null)
                {
                    gridResumen.DataSource = resumen;
                    gridRecaudacion.DataSource = detalle;

                    Session["Resumen"] = resumen;
                    Session["Detalle"] = detalle;

                    gridResumen.DataBind();
                    gridRecaudacion.DataBind();
                }
                else
                {
                    errorCargarResumen();
                    mensaje.Text = "No hay eventos registrados en esa fecha";
                    return;
                }
            }
            catch (FormatException ex)
            {
                errorCargarResumen();
                mensaje.Text = "Ingrese algun dato para realizar la busqueda";
                return;
            }

        }

        protected void btnDescarga_Click(object sender, EventArgs e)
        {
            List<Evento> eventosXFecha = (List<Evento>)Session["EventosXFecha"];

            if (eventosXFecha != null)
            {
                GeneradorPDF generar = new GeneradorPDF();

                byte[] pdf =generar.GenerarPdfEventos(eventosXFecha);

                descargarpdf(pdf);
            }
            else
            {
                mensaje2.Text = "No hay datos que descargar";
            }
        }
        protected void btnPdf_Click(object sender, EventArgs e)
        {
            List<RecaudacionEvento> resumen = (List<RecaudacionEvento>)Session["Resumen"];
            List<RecaudacionEvento> detalle = (List<RecaudacionEvento>)Session["Detalle"];

            if (resumen!= null && detalle!=null)
            {
                GeneradorPDF generar = new GeneradorPDF();

                byte[] pdf = generar.GenerarPdf(resumen, detalle);

                descargarpdf(pdf);
            }
            else
            {
                mensaje.Text = "No hay datos que descargar";
            }
        }

        protected void descargarpdf(byte[] pdf)
        {
            Response.Clear();
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=detalleRecaudacion.pdf");
            Response.BinaryWrite(pdf);
            Response.End();
        }

        protected void errorCargarResumen()
        {
            Session["Resumen"] = null;
            Session["Detalle"] = null;

            gridResumen.DataSource = "";
            gridRecaudacion.DataSource = "";

            gridResumen.DataBind();
            gridRecaudacion.DataBind();
        }
    }
}