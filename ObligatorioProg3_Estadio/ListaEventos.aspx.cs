using ObligatiorioProg3_Estadio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Toolkit.Core.DataTypes.Semantic.Types;

namespace ObligatorioProg3_Estadio
{
    public partial class ListaEventos : System.Web.UI.Page
    {
        PersistenciaEstadioUruguay persistencia = Sistema.fabricaPeristencia.ObtenerPersistenciaEstadioUruguay();
        DateTime fechanullinicio = new DateTime(1753, 1, 1, 12, 0, 0, DateTimeKind.Utc);
        DateTime fechanullfin = new DateTime(2900, 1, 1, 12, 0, 0, DateTimeKind.Utc);
        protected List<ListaEventosVendedor> Eventos;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GridEvento.DataSource = lista();
                GridEvento.DataBind();
            }

            Eventos = (List<ListaEventosVendedor>)this.Page.Session["eventos"]; 
        }

        protected void BtnFiltro_Click(object sender, EventArgs e)
        {
            if (TxtFechaDesde.Text != "" && TxtFechaHasta.Text != "")
            {
                if (DateTime.Parse(TxtFechaDesde.Text) >= DateTime.Parse(TxtFechaHasta.Text))
                {
                    LblAviso.Text = "La fecha de fin no puede ser mayor a la de inicio";
                    return;
                }
            }
            else
            {
                LblAviso.Text = "";
            }

            if (TxtFechaDesde.Text != "" || TxtFechaHasta.Text != "" || TxtBuscarEvento.Text != "")
            {
                BtnBorrarFiltro.Visible = true;
            }
            else
            {
                BtnBorrarFiltro.Visible = false;
            }

            GridEvento.DataSource = lista();
            GridEvento.DataBind();
        }

        protected void GridEvento_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index =  GridEvento.SelectedIndex;

            if(Eventos[index].disponibilidad != "Agotado")//'width=auto,height=auto,,left='+left+'top='+top+',toolbar=no,location=no,status=no,menubar=no,scrollbars=yes,resizable=yes'
            {
                string url = "VentasForm.aspx?id=" + Eventos[index].id;
                string script = "<script type='text/javascript'>"
                        + "var left = (screen.width/2)-(500/2);"
                        + "var top = (screen.height/2)-(500/2);"
                        + "window.open('" + url + "', 'popup', 'fullscreen=yes,scrollbars=yes,resizable=yes');"
                        + "</script>";
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", script);
            }
            else
            {
                LblAviso.Text = "Este evento se encuentra agotado";
            }    
        }

        public List<ListaEventosVendedor> lista()
        {
            List<Evento> eventos = null;
            List<ListaEventosVendedor> listaEventos1 = new List<ListaEventosVendedor>();
            ListaEventosVendedor listaEventos2 = null;

            if (TxtFechaDesde.Text == "" && TxtFechaHasta.Text == "")
            {
                eventos = persistencia.FiltroEvento(TxtBuscarEvento.Text, fechanullinicio, fechanullfin);
            }
            if (TxtFechaDesde.Text == "" && TxtFechaHasta.Text != "")
            {
                eventos = persistencia.FiltroEvento(TxtBuscarEvento.Text, fechanullinicio, DateTime.Parse(TxtFechaHasta.Text));
            }
            if (TxtFechaDesde.Text != "" && TxtFechaHasta.Text == "")
            {
                eventos = persistencia.FiltroEvento(TxtBuscarEvento.Text, DateTime.Parse(TxtFechaDesde.Text), fechanullfin);
            }
            if (TxtFechaDesde.Text != "" && TxtFechaHasta.Text != "")
            {
                eventos = persistencia.FiltroEvento(TxtBuscarEvento.Text, DateTime.Parse(TxtFechaDesde.Text), DateTime.Parse(TxtFechaHasta.Text));
            }

            for (int i = 0; i < eventos.Count; i++)
            {
                //eventos mayores a fecha de hoy

                if (eventos[i].fecha_hora_inicio >= DateTime.Now)
                {
                    PersistenciaEventoPromocion eventoPromocion = Sistema.fabricaPeristencia.ObtenerPersistenciaEventoPromocion();
                    EventoPromocion ep = eventoPromocion.buscar(eventos[i].idevento);
                    String promo = "No hay promocion";
                    String dispo = "Agotado";

                    if (ep != null){
                        if (ep.promocion != 0)
                        {
                            PersistenciaPromocion promocion = Sistema.fabricaPeristencia.ObtenerPersistenciaPromocion();
                            Promocion p = promocion.buscar(ep.promocion);
                            promo = p.descripcion;
                        }
                    }

                    PersistenciaEstadioUruguay persistencias = Sistema.fabricaPeristencia.ObtenerPersistenciaEstadioUruguay();
                    List<ViewDisponibilidad> disponibilidad = persistencias.V_DATOSEVENTO(eventos[i].idevento);
                    int dis = 0;
                    for (int j = 0; j < disponibilidad.Count; j++)
                    {
                        if (disponibilidad[j].disponible > 0)
                        {
                            dis++;
                        }
                    }

                    if (dis > 0)
                    {
                        dispo = "Disponible";
                    }

                    listaEventos2 = new ListaEventosVendedor();
                    listaEventos2.id = eventos[i].idevento;
                    listaEventos2.nombre = eventos[i].nombre;
                    listaEventos2.fechainicio = eventos[i].fecha_hora_inicio;
                    listaEventos2.fechafin = eventos[i].fecha_hora_fin;
                    listaEventos2.promocion = promo;
                    listaEventos2.disponibilidad = dispo;
                    listaEventos1.Add(listaEventos2);
                }
            }

            this.Page.Session["eventos"] = listaEventos1;
            return listaEventos1;
        }

        protected void BtnBorrarFiltro_Click(object sender, EventArgs e)
        {
            TxtBuscarEvento.Text = string.Empty;
            TxtFechaDesde.Text = string.Empty;
            TxtFechaHasta.Text = string.Empty;
            BtnBorrarFiltro.Visible = false;

            GridEvento.DataSource = lista();
            GridEvento.DataBind();
        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            Session["UsuarioActual"] = "";
            Response.Redirect("Login.aspx");
        }
    }
}