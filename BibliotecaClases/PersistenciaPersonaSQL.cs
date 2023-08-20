using ObligatiorioProg3_Estadio;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObligatiorioProg3_Estadio
{
    public class PersistenciaPersonaSQL : PersistenciaPersona
    {
        private static SqlConnection conectar()
        {
            String server = @"sql.bsite.net\MSSQL2016";
            String cadena = "Server=" + server + ";Database=fefabees_ESTADIOURUGUAY;User Id=fefabees_ESTADIOURUGUAY;Password=estadioUruguay;";
            SqlConnection conexion = new SqlConnection(cadena);
            conexion.Open();
            return conexion;
        }
        public Persona buscar(string nrodoc)
        {
            SqlConnection conexion = conectar();
            SqlCommand comando = new SqlCommand("SELECT nombre,apellido,correo FROM PERSONA (nolock) where nrodoc=" + nrodoc, conexion);
            SqlDataReader reader;
            Persona p = null;

            reader = comando.ExecuteReader();

            if (reader.Read())
            {
                p = new Persona();
                p.nrodoc = nrodoc;
                p.nombre = reader.GetString(reader.GetOrdinal("nombre"));
                p.apellido= reader.GetString(reader.GetOrdinal("apellido"));
                p.correo= reader.GetString(reader.GetOrdinal("correo"));
            }

            return p;
        }

        public int eliminar(string nrodoc)
        {
            SqlConnection conexion = conectar();
            SqlCommand comando = new SqlCommand("DELETE FROM PERSONA WHERE nrodoc=" + nrodoc, conexion);
            int cont = comando.ExecuteNonQuery();
            return cont;
        }

        public int guardar(Persona p)
        {
            int cont = 0;
            SqlTransaction transaccion = null;

            try
            {
                SqlConnection conexion = conectar();
                transaccion = conexion.BeginTransaction();

                SqlCommand comando = new SqlCommand("UPDATE PERSONA SET nombre = @nombre, apellido = @apellido, correo = @correo  WHERE nrodoc = @nrodoc", conexion, transaccion);
                comando.Parameters.AddWithValue("@nrodoc", p.nrodoc);
                comando.Parameters.AddWithValue("@nombre", p.nombre);
                comando.Parameters.AddWithValue("@apellido", p.apellido);
                comando.Parameters.AddWithValue("@correo", p.correo);
                cont = comando.ExecuteNonQuery();

                if (cont == 0)
                {
                    comando = new SqlCommand("INSERT INTO PERSONA (nrodoc,nombre,apellido,correo) VALUES (@nrodoc ,@nombre,@apellido,@correo)", conexion, transaccion);
                    comando.Parameters.AddWithValue("@nrodoc", p.nrodoc);
                    comando.Parameters.AddWithValue("@nombre", p.nombre);
                    comando.Parameters.AddWithValue("@apellido", p.apellido);
                    comando.Parameters.AddWithValue("@correo", p.correo);
                    cont = comando.ExecuteNonQuery();
                }

                //confirmar la transaccion en la base de datos
                transaccion.Commit();

            }
            catch (Exception ex)
            {
                if (transaccion == null)
                {
                    transaccion.Rollback();
                    cont = 0;
                }
            }

            return cont;
        }

        public List<Persona> lista()
        {
            SqlConnection conexion = conectar();
            SqlCommand comando = new SqlCommand("select * from PERSONA (nolock)", conexion);
            SqlDataReader reader;
            Persona p = null;
            List<Persona> resultado = new List<Persona>();

            reader = comando.ExecuteReader();

            while (reader.Read())
            {
                p = new Persona();
                p.nrodoc = reader.GetString(reader.GetOrdinal("nrodoc"));
                p.nombre = reader.GetString(reader.GetOrdinal("nombre"));
                p.apellido = reader.GetString(reader.GetOrdinal("apellido"));
                p.correo = reader.GetString(reader.GetOrdinal("correo"));
                resultado.Add(p);
            }

            return resultado;
        }
    }
}
