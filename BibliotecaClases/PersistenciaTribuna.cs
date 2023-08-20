using Biblioteca;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObligatiorioProg3_Estadio
{
   public interface PersistenciaTribuna
    {
       Tribuna buscar(String id);
        int guardar(Tribuna trib);
        //   void eliminar(int id);
        int eliminar(String id); //cambiamos a int para devolver un valor que se pueda checkear si elimino o no ya que ahora es int;
        List<Tribuna> lista();
    }
}
