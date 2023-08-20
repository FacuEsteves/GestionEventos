using ObligatiorioProg3_Estadio;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ObligatorioProg3_Estadio
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            mensaje.Text = "";
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            Usuario u = new Usuario();
            Encriptador enc = new Encriptador();

            string userN = txtUser.Text;
            string pass = enc.ToSHA256(txtPass.Text);

            PersistenciaUsuario persistenciaUsuario = Sistema.fabricaPeristencia.ObtenerPersistenciaUsuario();

            u=persistenciaUsuario.buscarUserName(userN,pass);

            if (u==null)
            {
                mensaje.Text = ("Usuario o contraseña incorrectos intetelo de nuevo");
            }
            else
            {
                if(u.rol==false)
                {
                    txtUser.Text = "";
                    txtPass.Text = "";
                    Session["UsuarioActual"] = u;
                    Response.Redirect("ListaEventos.aspx");
                }
                if (u.rol == true)
                {
                    txtUser.Text = "";
                    txtPass.Text = "";
                    Session["UsuarioActual"] = u;
                    Response.Redirect("ListaEventosA.aspx");
                }
            }
        }
    }
}