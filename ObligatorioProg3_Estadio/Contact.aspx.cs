using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ObligatorioProg3_Estadio.ServicioConsulta;

namespace ObligatorioProg3_Estadio
{
    public partial class Contact : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            int idevento = 1;
            int idpuerta = 1;
            ConsultaEstadioWebServiceSoapClient consult = new ConsultaEstadioWebServiceSoapClient();
            Label1.Text = consult.Disponibilidad(idevento,idpuerta).ToString();
        }
    }
}