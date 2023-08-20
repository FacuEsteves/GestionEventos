using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObligatiorioProg3_Estadio
{
    public interface PersistenciaDetalleCompra
    {
        DetalleCompra buscar(int id);
        int guardar(DetalleCompra detalleCompra);
        int eliminar(int id);
        List<DetalleCompra> lista();
    }
}
