using ObligatorioProg3_Estadio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObligatiorioProg3_Estadio
{
    public interface PersistenciaCompra
    {
        Compra buscar(int id);
        int guardar(Compra compra);
        int eliminar(int id);
       List<Compra> lista();

        Compra buscarCliente(String idcliente);
    }
}
