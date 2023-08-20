using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ObligatorioProg3_Estadio;
namespace ObligatiorioProg3_Estadio
{
    public interface PersistenciaDatosEvento
    {
        int guardar(DatosEvento datos);
        int eliminar(int idevento);
        List<DatosEvento> lista();
        List<DatosEvento> listainserta(String id);
        List<DatosEvento> listaBuscar(int idevento);
        DatosEvento buscarPrecio(int idev, int idp);

    }
}
