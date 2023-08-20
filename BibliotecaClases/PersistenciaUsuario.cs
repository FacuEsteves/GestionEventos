using ObligatorioProg3_Estadio;
using System;
using System.Collections.Generic;

namespace ObligatiorioProg3_Estadio
{
    public interface PersistenciaUsuario
    {
        Usuario buscar(string doc);
        Usuario buscarUserName(string userN,string pass);
        int guardar(Usuario u);
        int eliminar(string nrodoc);
        List<Usuario> lista();
        List<Usuario> buscarRol(string rol);
        List<Usuario> buscarQuery(string query);
    }
}
