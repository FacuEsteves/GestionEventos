using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Ajax.Utilities;
using ObligatiorioProg3_Estadio;
using Toolkit.Core.Extensions;

namespace ObligatorioProg3_Estadio
{
	public partial class UsuariosForm : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{

            if (!IsPostBack)
            {
                mensaje.Text = "";
                txtNombre.Text = "";
                txtApellido.Text = "";
                txtNroDoc.Text = "";
                txtCorreo.Text = "";
                txtUser.Text = "";
                txtPass.Text = "";
                CheckBoxAdmin.Checked = false;
            }

            PersistenciaUsuario persistenciaUsuario = Sistema.fabricaPeristencia.ObtenerPersistenciaUsuario();

            gridUsers.DataSource = persistenciaUsuario.lista();
            gridUsers.DataBind();
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {

            if (txtNombre.Text == "" || txtApellido.Text == "" || txtNroDoc.Text == "" || txtCorreo.Text == "" || txtUser.Text == "" || txtPass.Text == "")
            {
                mensaje.ForeColor = Color.Red;
                mensaje.Text = ("Faltan Ingresar Datos ,Recuerde que para guardar un nuevo usuario todos los campos deben ser completados, en caso de querer editar debe ingresar la contraseña del usuario para confirmar");
                return;
            }

            PersistenciaPersona persistenciaPersona = Sistema.fabricaPeristencia.ObtenerPersistenciaPersona();
            PersistenciaUsuario persistenciaUser = Sistema.fabricaPeristencia.ObtenerPersistenciaUsuario();

            Usuario u = new Usuario();
            Encriptador enc = new Encriptador();
            string doc = txtNroDoc.Text;

            //dos funciones, dos procedimientos, un trigger

            string pass = txtPass.Text;

            u.nombre = txtNombre.Text;
            u.apellido = txtApellido.Text;
            u.nrodoc = doc;
            u.correo = txtCorreo.Text;
            u.nombreUsuario = txtUser.Text;
            u.contrasenia = enc.ToSHA256(pass);
            u.rol = CheckBoxAdmin.Checked;

            //Guardar por etapas primero persona despues usuario

            int cont = persistenciaPersona.guardar(u);

            if (cont == 0)
            {
                mensaje.ForeColor=Color.Red;
                mensaje.Text = "Fallo al guardar el usuario";
                return;
            }
            else
            {
                int cont2 = persistenciaUser.guardar(u);

                if (cont2 == 0)
                {
                    persistenciaPersona.eliminar(doc);
                    mensaje.ForeColor = Color.Red;
                    mensaje.Text = "Fallo el guardado, compruebe los datos que intento ingresar";
                }
                else
                {
                    mensaje.ForeColor = Color.ForestGreen;
                    mensaje.Text = "Usuario guardado con exito";
                    txtNombre.Text = "";
                    txtApellido.Text = "";
                    txtNroDoc.Text = "";
                    txtCorreo.Text = "";
                    txtUser.Text = "";
                    txtPass.Text = "";
                    CheckBoxAdmin.Checked = false;
                    gridUsers.DataSource = persistenciaUser.lista();
                    gridUsers.DataBind();
                }
            }
        }

        protected void gridUsers_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string nroDoc = Convert.ToString(e.Values["nrodoc"]);

            PersistenciaPersona persistenciaPersona = Sistema.fabricaPeristencia.ObtenerPersistenciaPersona();
            PersistenciaUsuario persistenciaUser = Sistema.fabricaPeristencia.ObtenerPersistenciaUsuario();

            int cont=persistenciaUser.eliminar(nroDoc);
            int cont1=persistenciaPersona.eliminar(nroDoc);

            if (cont==0 && cont1 == 0)
            {
                mensaje.ForeColor = Color.Red;
                mensaje.Text = "Hubo un error al eliminar el usuario, intente de nuevo mas tarde," +
                    "Si el error continua contactese con el Programador";
            }
            else
            {
                mensaje.ForeColor = Color.ForestGreen;
                mensaje.Text = "Usuario borrado con exito!";
            }
            gridUsers.DataSource = persistenciaUser.lista();
            gridUsers.DataBind();
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            mensajeBusqueda.Text = "";

            if (buscarTxt.Text == "")
            {
                mensajeBusqueda.ForeColor = Color.Red;
                mensajeBusqueda.Text = "Para buscar debe seguir algun criterio";
                return;
            }
            PersistenciaUsuario persistenciaUsuario = Sistema.fabricaPeristencia.ObtenerPersistenciaUsuario();

            string query = buscarTxt.Text;

            List<Usuario> lista = new List<Usuario>();
            lista= persistenciaUsuario.buscarQuery(query);

            if (lista.Count == 0)
            {
                mensajeBusqueda.Text = ("No se encontraron resultados para esa busqueda");
                gridUsers.DataSource = lista;
                gridUsers.DataBind();
            }
            else
            {
                gridUsers.DataSource = lista;
                gridUsers.DataBind();
            }

        }

        protected void ddFiltroRol_SelectedIndexChanged(object sender, EventArgs e)
        {
            buscarTxt.Text="";

            PersistenciaUsuario persistenciaUsuario = Sistema.fabricaPeristencia.ObtenerPersistenciaUsuario();

            string rol = ddFiltroRol.SelectedValue;

            List<Usuario> lista = new List<Usuario>();
            lista = persistenciaUsuario.buscarRol(rol);

            if (lista.Count == 0)
            {
                mensajeBusqueda.Text = ("No se encontraron resultados para esa busqueda");
                gridUsers.DataSource = lista;
                gridUsers.DataBind();
            }
            else
            {
                gridUsers.DataSource = lista;
                gridUsers.DataBind();
            }
        }

        protected void btnCompletar_Click(object sender, EventArgs e)
        {
            string doc = txtNroDoc.Text;

            PersistenciaUsuario persistenciaUsuario = Sistema.fabricaPeristencia.ObtenerPersistenciaUsuario();
            Usuario u = new Usuario();

            u = persistenciaUsuario.buscar(doc);

            if (u == null)
            {
                mensaje.ForeColor = Color.Red;
                mensaje.Text = "El usuario con este documento no existe, verifique si ingreso el documento correctamente";
                return;
            }
            else
            {
                mensaje.ForeColor = Color.Green;
                mensaje.Text = "Usuario encontrado!";
                txtNombre.Text = u.nombre;
                txtApellido.Text = u.apellido;
                txtCorreo.Text = u.correo;
                txtUser.Text = u.nombreUsuario;
                CheckBoxAdmin.Checked = u.rol;
            }
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtNombre.Text = "";
            txtApellido.Text = "";
            txtNroDoc.Text = "";
            txtCorreo.Text = "";
            txtUser.Text = "";
            txtPass.Text = "";
            CheckBoxAdmin.Checked = false;
        }
    }
}