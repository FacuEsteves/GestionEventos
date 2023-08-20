using ObligatiorioProg3_Estadio;
using ObligatorioProg3_Estadio;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ObligatiorioProg3_Estadio
{
    public class PersistenciaUsuarioSQL : PersistenciaUsuario
    {
        private static SqlConnection conectar()
        {
            String server = @"sql.bsite.net\MSSQL2016";
            String cadena = "Server=" + server + ";Database=fefabees_ESTADIOURUGUAY;User Id=fefabees_ESTADIOURUGUAY;Password=estadioUruguay;";
            SqlConnection conexion = new SqlConnection(cadena);
            conexion.Open();
            return conexion;
        }
        public Usuario buscar(string doc)
        {
            SqlConnection conexion = conectar();
            SqlCommand comando = new SqlCommand("SELECT P.nombre, P.apellido, P.correo, U.nrodoc, U.usuario, U.contrasena, U.rol FROM PERSONA AS P INNER JOIN USUARIO AS U ON U.nrodoc = P.nrodoc WHERE U.nrodoc =@nrodoc", conexion);
            comando.Parameters.AddWithValue("@nrodoc", doc);
            SqlDataReader reader;
            Usuario u = null;

            reader = comando.ExecuteReader();

            if (reader.Read())
            {
                u = new Usuario();
                u.nrodoc = doc;
                u.nombre = reader.GetString(reader.GetOrdinal("nombre"));
                u.apellido = reader.GetString(reader.GetOrdinal("apellido"));
                u.correo = reader.GetString(reader.GetOrdinal("correo"));
                u.nombreUsuario = reader.GetString(reader.GetOrdinal("usuario"));
                u.contrasenia = reader.GetString(reader.GetOrdinal("contrasena"));
                u.rol = reader.GetBoolean(reader.GetOrdinal("rol"));

            }

            return u;
        }

        public Usuario buscarUserName(string userN,string pass)
        {
            SqlConnection conexion = conectar();
            SqlCommand comando = new SqlCommand("SELECT P.nombre, P.apellido, P.correo, U.nrodoc, U.usuario, U.contrasena, U.rol FROM PERSONA AS P INNER JOIN USUARIO AS U ON U.nrodoc = P.nrodoc WHERE U.usuario =@usuario and U.contrasena=@pass", conexion);
            comando.Parameters.AddWithValue("@usuario", userN);
            comando.Parameters.AddWithValue("@pass", pass);
            SqlDataReader reader;
            Usuario u = null;

            reader = comando.ExecuteReader();

            if (reader.Read())
            {
                u = new Usuario();
                u.nombreUsuario = userN;
                u.nrodoc = reader.GetString(reader.GetOrdinal("nrodoc"));
                u.nombre = reader.GetString(reader.GetOrdinal("nombre"));
                u.apellido = reader.GetString(reader.GetOrdinal("apellido"));
                u.correo = reader.GetString(reader.GetOrdinal("correo"));
                u.contrasenia = reader.GetString(reader.GetOrdinal("contrasena"));
                u.rol = reader.GetBoolean(reader.GetOrdinal("rol"));

            }

            return u;
        }

        public int eliminar(string nrodoc)
        {
            SqlConnection conexion = conectar();
            SqlCommand comando = new SqlCommand("DELETE FROM USUARIO WHERE nrodoc=" + nrodoc, conexion);
            int cont = comando.ExecuteNonQuery();
            return cont;
        }

        public int guardar(Usuario u)
        {
            int cont = 0;
            SqlTransaction transaccion = null;

            try
            {
                SqlConnection conexion = conectar();
                transaccion = conexion.BeginTransaction();

                SqlCommand comando = new SqlCommand("UPDATE USUARIO SET usuario = @usuario , contrasena = @contrasena, rol = @rol WHERE nrodoc = @nrodoc", conexion, transaccion);
                comando.Parameters.AddWithValue("@usuario", u.nombreUsuario);
                comando.Parameters.AddWithValue("@contrasena", u.contrasenia);
                comando.Parameters.AddWithValue("@rol", u.rol);
                comando.Parameters.AddWithValue("@nrodoc", u.nrodoc);
                cont = comando.ExecuteNonQuery();

                if (cont == 0)
                {
                    comando = new SqlCommand("INSERT INTO USUARIO (nrodoc,usuario,contrasena,rol) VALUES (@nrodoc ,@usuario, @contrasena, @rol)", conexion, transaccion);
                    comando.Parameters.AddWithValue("@nrodoc", u.nrodoc);
                    comando.Parameters.AddWithValue("@usuario", u.nombreUsuario);
                    comando.Parameters.AddWithValue("@contrasena", u.contrasenia);
                    comando.Parameters.AddWithValue("@rol", u.rol);
                    cont = comando.ExecuteNonQuery();
                }

                //confirmar la transaccion en la base de datos
                transaccion.Commit();

            }
            catch (Exception ex)
            {
                if (transaccion != null)
                {
                    transaccion.Rollback();
                    cont = 0;
                }
            }

            return cont;
        }

        public List<Usuario> lista()
        {
            SqlConnection conexion = conectar();
            SqlCommand comando = new SqlCommand("SELECT u.nrodoc, PERSONA.nombre,PERSONA.apellido,PERSONA.correo, u.usuario, u.rol FROM USUARIO as u INNER JOIN PERSONA on PERSONA.nrodoc=u.nrodoc", conexion);
            SqlDataReader reader;
            Usuario u = null;
            List<Usuario> resultado = new List<Usuario>();

            reader = comando.ExecuteReader();

            while (reader.Read())
            {
                u = new Usuario();
                u.nrodoc = reader.GetString(reader.GetOrdinal("nrodoc"));
                u.nombre = reader.GetString(reader.GetOrdinal("nombre"));
                u.apellido = reader.GetString(reader.GetOrdinal("apellido"));
                u.nombreUsuario = reader.GetString(reader.GetOrdinal("usuario"));
                u.rol = reader.GetBoolean(reader.GetOrdinal("rol"));
                u.correo = reader.GetString(reader.GetOrdinal("correo"));
                resultado.Add(u);
            }

            return resultado;
        }
        public List<Usuario> buscarRol(string rol)
        {
            List<Usuario> resultado = new List<Usuario>();

            if (rol == "Todos")
            {
                SqlConnection conexion = conectar();
                SqlCommand comando = new SqlCommand("SELECT u.nrodoc, PERSONA.nombre,PERSONA.apellido,PERSONA.correo, u.usuario, u.rol FROM USUARIO as u INNER JOIN PERSONA on PERSONA.nrodoc=u.nrodoc", conexion);
                SqlDataReader reader;
                Usuario u = null;

                reader = comando.ExecuteReader();

                while (reader.Read())
                {
                    u = new Usuario();
                    u.nrodoc = reader.GetString(reader.GetOrdinal("nrodoc"));
                    u.nombre = reader.GetString(reader.GetOrdinal("nombre"));
                    u.apellido = reader.GetString(reader.GetOrdinal("apellido"));
                    u.nombreUsuario = reader.GetString(reader.GetOrdinal("usuario"));
                    u.rol = reader.GetBoolean(reader.GetOrdinal("rol"));
                    u.correo = reader.GetString(reader.GetOrdinal("correo"));
                    resultado.Add(u);
                }
            }
            if (rol!="Todos")
            {
                SqlConnection conexion = conectar();
                SqlCommand comando = new SqlCommand("SELECT u.nrodoc, PERSONA.nombre,PERSONA.apellido,PERSONA.correo, u.usuario, u.rol FROM USUARIO as u INNER JOIN PERSONA on PERSONA.nrodoc=u.nrodoc where u.rol="+rol, conexion);
                SqlDataReader reader;
                Usuario u = null;

                reader = comando.ExecuteReader();

                while (reader.Read())
                {
                    u = new Usuario();
                    u.nrodoc = reader.GetString(reader.GetOrdinal("nrodoc"));
                    u.nombre = reader.GetString(reader.GetOrdinal("nombre"));
                    u.apellido = reader.GetString(reader.GetOrdinal("apellido"));
                    u.nombreUsuario = reader.GetString(reader.GetOrdinal("usuario"));
                    u.rol = reader.GetBoolean(reader.GetOrdinal("rol"));
                    u.correo = reader.GetString(reader.GetOrdinal("correo"));
                    resultado.Add(u);
                }
            }
            return resultado;
        }

        public List<Usuario> buscarQuery(string query)
        {
            List<Usuario> resultado = new List<Usuario>();

            SqlConnection conexion = conectar();
            SqlCommand comando = new SqlCommand("SELECT u.nrodoc, PERSONA.nombre,PERSONA.apellido,PERSONA.correo, u.usuario, u.rol FROM USUARIO as u INNER JOIN PERSONA on PERSONA.nrodoc=u.nrodoc where u.nrodoc like('%"+query+ "%') or PERSONA.nombre like('%" + query+ "%') or PERSONA.apellido like('%" + query+ "%') or PERSONA.correo like('%" + query+ "%') or u.usuario like('%" + query+"%')", conexion);
            SqlDataReader reader;
            Usuario u = null;
    
            reader = comando.ExecuteReader();

            while (reader.Read())
            {        
                u = new Usuario();
                        
                u.nrodoc = reader.GetString(reader.GetOrdinal("nrodoc"));
                u.nombre = reader.GetString(reader.GetOrdinal("nombre"));
                u.apellido = reader.GetString(reader.GetOrdinal("apellido"));
                u.nombreUsuario = reader.GetString(reader.GetOrdinal("usuario"));
                u.rol = reader.GetBoolean(reader.GetOrdinal("rol"));
                u.correo = reader.GetString(reader.GetOrdinal("correo"));
                resultado.Add(u);
            }
            return resultado;
        }
    }
}
