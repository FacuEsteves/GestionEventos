using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ObligatiorioProg3_Estadio;
using ObligatorioProg3_Estadio.ServicioConsulta;
using Toolkit.Core.DataTypes.Semantic.Types;
using Toolkit.Core.Extensions;
using Toolkit.Core.Patterns;

namespace ObligatorioProg3_Estadio
{
    public partial class CrearEvento : System.Web.UI.Page
    {
        private List<ObligatiorioProg3_Estadio.DatosEvento> DatosList;
        protected void Page_Load(object sender, EventArgs e)
        {           

            int idevento = Request.QueryString["id"].ToInt();
            
            if (!IsPostBack)
            {
                if (idevento != 0)
                {
                    CheckBoxTribunas.DataBind();
                    cargarevento(idevento);
                }
            }
            
            
        }

        protected void BtnGuardar_Click(object sender, EventArgs e)
        {
            Evento evento = new Evento();
            EventoPromocion ep = new EventoPromocion();
            ObligatiorioProg3_Estadio.DatosEvento de = null;
            DatosList = (List<ObligatiorioProg3_Estadio.DatosEvento>)this.Page.Session["db"];

            PersistenciaEvento persistenciaEvento = Sistema.fabricaPeristencia.ObtenerPersistenciaEvento();
            PersistenciaDatosEvento persistenciaDatosEvento = Sistema.fabricaPeristencia.ObtenerPersistenciaDatosEvento();
            PersistenciaEventoPromocion persistenciaEventoPromocion = Sistema.fabricaPeristencia.ObtenerPersistenciaEventoPromocion();

            if (TxtNombreEvento.Text == "")
            {
                mensaje.Text = "Ingrese Nombre de Evento";
                return;
            }
            if (TxtFechaHoraInicio.Text == "")
            {
                mensaje.Text = "Ingrese Fecha de inicio";
                return;
            }
            if (TxtFechaHoraFin.Text == "")
            {
                mensaje.Text = "Ingrese Fecha de fin";
                return;
            }
            if (TxtFechaHoraInicio.Text != "" && TxtFechaHoraFin.Text != "")
            {
                if (DateTime.Parse(TxtFechaHoraInicio.Text) > DateTime.Parse(TxtFechaHoraFin.Text))
                {
                    mensaje.Text = "La fecha de fin no puede ser mayor a la de inicio";
                    return;
                }
            }
            if (TxtDescuento.Visible == true && (TxtDescuento.Text == "" || TxtDescuento.Text == "0"))
            {
                mensaje.Text = "Ingrese el porcentaje de descuento";
                return;
            }

            List<string> valoresSeleccionados = new List<string>();
            foreach (ListItem item in CheckBoxTribunas.Items)
            {
                if (item.Selected)
                {
                    valoresSeleccionados.Add(item.Value);
                }
            }

            if(valoresSeleccionados.Count == 0)
            {
                mensaje.Text = "Seleccione las tribunas a habilitar";
                return;
            }


            evento.nombre = TxtNombreEvento.Text;
            evento.fecha_hora_inicio = DateTime.Parse(TxtFechaHoraInicio.Text);
            evento.fecha_hora_fin = DateTime.Parse(TxtFechaHoraFin.Text);

            int cont = persistenciaEvento.guardar(evento);

            if (cont != 0)
            {
                int idevento = 0;

                if(Request.QueryString["id"].ToInt() != 0)
                {
                    idevento = Request.QueryString["id"].ToInt();
                }
                else
                {
                    idevento = persistenciaEvento.UltimoId();
                }
                

                for (int i = 0; i < DatosList.Count; i++)
                {
                    //si capacidad es 0, borrar el dato
                    if (DatosList[i].capacidad == 0)
                    {
                        DatosList.Remove(DatosList[i]);
                    }
                    else
                    {
                        DatosList[i].evento = idevento;
                        de = new ObligatiorioProg3_Estadio.DatosEvento();
                        de = DatosList[i];
                        int cont2 = persistenciaDatosEvento.guardar(de);

                        if (cont2 != 0)
                        {
                            mensaje.Text = "GUARDADO DATOS EVENTO";
                        }
                        else
                        {
                            mensaje.Text = "NO GUARDADO DATOS EVENTO";
                        }
                    }
                }

                if (ListPromociones.SelectedIndex != 0)
                {
                    ep.evento = idevento;
                    ep.promocion = Convert.ToInt32(ListPromociones.SelectedValue);
                    ep.Descuento = Convert.ToDouble(TxtDescuento.Text);
                    int cont2 = persistenciaEventoPromocion.guardar(ep);
                    //verificar guardado
                }

                Response.Redirect("ListaEventosA.aspx");
                limpiar();
                return;
            }
            else
            {
                mensaje.Text = "Fallo el guardado";
            }

        }

        protected void CheckBoxTribunas_SelectedIndexChanged(object sender, EventArgs e)
        {
            List < ObligatiorioProg3_Estadio.DatosEvento > session = new List<ObligatiorioProg3_Estadio.DatosEvento>();
            // Obtener los elementos seleccionados del CheckBoxList
            List<string> valoresSeleccionados = new List<string>();
            foreach (ListItem item in CheckBoxTribunas.Items)
            {
                if (item.Selected)
                {
                    valoresSeleccionados.Add(item.Value);
                }
            }
            DatosList = new List<ObligatiorioProg3_Estadio.DatosEvento>();
            
            //cargale los datos anteriores a session por si estan editados
            if (this.Page.Session["db"] != null)
            {
                session = (List<ObligatiorioProg3_Estadio.DatosEvento>)this.Page.Session["db"];
            }


            foreach (string valor in valoresSeleccionados)
            {
                List<ObligatiorioProg3_Estadio.DatosEvento> datos = new List<ObligatiorioProg3_Estadio.DatosEvento>();
                ObligatiorioProg3_Estadio.DatosEvento de = null;

                //guarda las puerta de las tribunas seleccionadas
                PersistenciaDatosEvento persistencia = Sistema.fabricaPeristencia.ObtenerPersistenciaDatosEvento();
                datos = persistencia.listainserta(valor);

                //busca y remueve las puertas ya ingresadas en DatosList
                for (int i = 0; i < DatosList.Count; i++)
                {
                    for (int j = 0; j < datos.Count; j++)
                    {
                        if (DatosList[i].puerta == datos[j].puerta)
                        {
                            datos.RemoveAt(j);
                            break;
                        }
                    }
                }

                //buscar en session si los datos nuevos son diferentes a los viejos
                for (int i = 0; i < DatosList.Count; i++)
                {
                    for (int j = 0; j < session.Count; j++)
                    {
                        if (DatosList[i].puerta == session[j].puerta)
                        {
                            if(DatosList[i].costo != session[j].costo)
                            {
                                DatosList[i].costo = session[j].costo;
                            }
                            
                            break;
                        }
                    }
                }

                //añade las puertas que no estaban ingresadas 
                for (int i = 0; i < datos.Count; i++)
                {
                    de = new ObligatiorioProg3_Estadio.DatosEvento();
                    de.puerta = datos[i].puerta;
                    de.capacidad = cargarcantidad(datos[i].puerta);
                    de.costo = datos[i].costo;
                    DatosList.Add(de);
                }
            }

            this.Page.Session["db"] = DatosList;
            GridPuerta.DataSource = DatosList;
            GridPuerta.DataBind();
        }

        protected void GridPuerta_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridPuerta.EditIndex = e.NewEditIndex;
            GridPuerta.DataSource = this.Page.Session["db"];
            GridPuerta.DataBind();
        }

        protected void GridPuerta_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            DatosList = (List<ObligatiorioProg3_Estadio.DatosEvento>)this.Page.Session["db"];
            ObligatiorioProg3_Estadio.DatosEvento de = new ObligatiorioProg3_Estadio.DatosEvento();
            List<Puerta> puertas = new List<Puerta>();
            PersistenciaPuerta persistenciaPuerta = Sistema.fabricaPeristencia.ObtenerPersistenciaPuerta();
            puertas = persistenciaPuerta.lista();
            int capacidadMax = 0;
           
            de.puerta = DatosList[e.RowIndex].puerta; 
            de.capacidad = DatosList[e.RowIndex].capacidad;
            de.costo = Convert.ToDouble(e.NewValues[0]);

            for(int i=0; i< DatosList.Count; i++)
            {
                if (DatosList[i].puerta == de.puerta) 
                {
                    for(int j=0; j < puertas.Count; j++)
                    {
                        if (puertas[j].idpuerta == DatosList[i].puerta)
                        {
                            if(DropCapacidad.SelectedIndex == 0)
                            {
                                capacidadMax = puertas[j].capacidad_total;
                            }
                            else
                            {
                                capacidadMax = puertas[j].capacidad_parcial;
                            }
                            break;
                        }
                    }

                    DatosList[i].costo = de.costo;

                    if(capacidadMax < de.capacidad)
                    {
                        mensaje.Text = "Supera la capacidad maxima";
                        break;
                    }
                    else
                    {
                        DatosList[i].capacidad = de.capacidad;
                    }

                    break;
                }
            }
            this.Page.Session["db"] = DatosList;
        }

        protected void GridPuerta_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridPuerta.EditIndex = -1;
            GridPuerta.DataSource = this.Page.Session["db"];
            GridPuerta.DataBind();
        }
    
        public void limpiar()
        {
            DropCapacidad.SelectedIndex = 0;
            TxtNombreEvento.Text = string.Empty;
            TxtFechaHoraInicio.Text = string.Empty;
            TxtFechaHoraFin.Text = string.Empty;
            ListPromociones.SelectedIndex = 0;
            TxtDescuento.Text = string.Empty;
            foreach (ListItem item in CheckBoxTribunas.Items)
            {
                item.Selected = false;
            }
            this.Page.Session.Remove("db");
            GridPuerta.DataSource = null;
            GridPuerta.DataBind();
        }

        protected void ListPromociones_SelectedIndexChanged(object sender, EventArgs e)
        {
            char buscado = '%';

            if (ListPromociones.SelectedIndex != 0)
            {
                Promocion promo = new Promocion();
                PersistenciaPromocion promocion = Sistema.fabricaPeristencia.ObtenerPersistenciaPromocion();
                promo = promocion.buscar(Convert.ToInt32(ListPromociones.SelectedValue));
                string descripcion = promo.descripcion;

                if (descripcion.IndexOf(buscado) != -1)
                {
                    LabelDescuento.Visible = true;
                    TxtDescuento.Visible = true;
                }
                else
                {
                    LabelDescuento.Visible = false;
                    TxtDescuento.Visible = false;
                }
            }
            else
            {
                LabelDescuento.Visible = false;
                TxtDescuento.Visible = false;
            }
            
        }

        protected int cargarcantidad(int idpuerta)
        {
            PersistenciaPuerta persistenciaPuerta = Sistema.fabricaPeristencia.ObtenerPersistenciaPuerta();
            List<Puerta> puertas = persistenciaPuerta.lista();
            int cant = 0;

            for (int j = 0; j < puertas.Count; j++)
            {
                if (puertas[j].idpuerta == idpuerta)
                {
                    if (DropCapacidad.SelectedIndex == 0)
                    {
                        cant = puertas[j].capacidad_total;
                    }
                    else
                    {
                        cant = puertas[j].capacidad_parcial;
                    }
                    break;
                }
            }
            return cant;
        }

        public void cargarevento(int idevento)
        {
            //DATOS DE EL EVENTO
     
            PersistenciaEvento persistenciaEvento = Sistema.fabricaPeristencia.ObtenerPersistenciaEvento();
            Evento evento = persistenciaEvento.buscar(idevento);
            TxtNombreEvento.Text = evento.nombre;
            TxtFechaHoraInicio.Text = evento.fecha_hora_inicio.ToString("yyyy-MM-ddThh:mm");
            TxtFechaHoraFin.Text = evento.fecha_hora_fin.ToString("yyyy-MM-ddThh:mm");

            //PROMOCION DE EL EVENTO

            PersistenciaEventoPromocion persistenciaEventoPromocion = Sistema.fabricaPeristencia.ObtenerPersistenciaEventoPromocion();
            EventoPromocion eventoPromocion = persistenciaEventoPromocion.buscar(idevento);
            if(eventoPromocion != null)
            {
                ListPromociones.SelectedValue = eventoPromocion.promocion.ToString();

                char buscado = '%';

                Promocion promo = new Promocion();
                PersistenciaPromocion promocion = Sistema.fabricaPeristencia.ObtenerPersistenciaPromocion();
                promo = promocion.buscar(Convert.ToInt32(eventoPromocion.promocion));
                string descripcion = promo.descripcion;

                if (descripcion.IndexOf(buscado) != -1)
                {
                    LabelDescuento.Visible = true;
                    TxtDescuento.Visible = true;
                    TxtDescuento.Text = eventoPromocion.Descuento.ToString();
                }
            }
            else
            {
                ListPromociones.SelectedIndex = 0;
            }

            //PUERTAS HABILITADAS EN EL EVENTO

            PersistenciaDatosEvento persistenciaDatosEvento = Sistema.fabricaPeristencia.ObtenerPersistenciaDatosEvento();
            List<ObligatiorioProg3_Estadio.DatosEvento> listadatos = persistenciaDatosEvento.listaBuscar(idevento);

            for(int i = 0;i < listadatos.Count;i++)
            {
                PersistenciaPuerta persistenciaPuerta = Sistema.fabricaPeristencia.ObtenerPersistenciaPuerta();
                Puerta puerta = persistenciaPuerta.buscar(listadatos[i].puerta);

                foreach (ListItem item in CheckBoxTribunas.Items)
                {
                    if(item.Value == puerta.tribuna && item.Selected == false)
                    {
                        item.Selected = true;
                    }
                }

                if (listadatos[i].capacidad == puerta.capacidad_total)
                {
                    DropCapacidad.SelectedIndex = 0;
                }
                if (listadatos[i].capacidad == puerta.capacidad_parcial)
                {
                    DropCapacidad.SelectedIndex = 1;
                }
            }

            DatosList = listadatos;
            this.Page.Session["db"] = DatosList;
            GridPuerta.DataSource = DatosList;
            GridPuerta.DataBind();
        }

    }
}

// recargar lista al des seleccionar una tribuna linea 100
//