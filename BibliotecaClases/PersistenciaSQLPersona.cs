using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObligatiorioProg3_Estadio
{
    public class PersistenciaSQLPersona : PersistenciaPersona
    {
        private SqlConnection crearConexion()
        {
            SqlConnection conexion = new SqlConnection("Server=sql.bsite.net\\MSSQL2016; Database=practico_1; User ID=fefabees_ESTADIOURUGUAY;Password=estadioUruguay");
            conexion.Open();
            return conexion;
        }

        public Persona buscar(String nrodoc)
        {
            SqlConnection conexion = crearConexion();
            SqlCommand comando = new SqlCommand("SELECT nrodoc ,nombre FROM PERSONA (nolock) where nrodoc=" + nrodoc, conexion);
            SqlDataReader reader;
            Persona p = null;

            reader = comando.ExecuteReader();

            if (reader.Read())
            {
                p = new Persona();
                p.nrodoc = nrodoc;
                p.nombre = reader.GetString(reader.GetOrdinal("nombre"));
            }

            return p;
        }

        public int guardar(Persona p)
        {
            int cont = 0;
            SqlTransaction transaccion = null;

            try
            {
                SqlConnection conexion = crearConexion();
                transaccion = conexion.BeginTransaction();

                SqlCommand comando = new SqlCommand("UPDATE PERSONA SET nombre = @nombre  WHERE nrodoc = @nrodoc", conexion, transaccion);
                comando.Parameters.AddWithValue("@nombre", p.nombre);
                comando.Parameters.AddWithValue("@nrodoc", p.nrodoc);
                cont = comando.ExecuteNonQuery();

                if (cont == 0)
                {
                    comando = new SqlCommand("INSERT INTO PERSONA (nrodoc,nombre) VALUES (@nrodoc ,@nombre)", conexion, transaccion);
                    comando.Parameters.AddWithValue("@nrodoc", p.nrodoc);
                    comando.Parameters.AddWithValue("@nombre", p.nombre);
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

        public int eliminar(string ci)
        {
            SqlConnection conexion = crearConexion();
            SqlCommand comando = new SqlCommand("DELETE FROM PERSONA WHERE nrodoc=" + ci, conexion);
            int cont = comando.ExecuteNonQuery();
            return cont;
        }

        public List<Persona> lista()
        {
            SqlConnection conexion = crearConexion();
            SqlCommand comando = new SqlCommand("select * from DEPARTAMENTO (nolock)", conexion);
            SqlDataReader reader;
            Persona p = null;
            List<Persona> resultado = new List<Persona>();

            reader = comando.ExecuteReader();

            while (reader.Read())
            {
                p = new Persona();
                p.nrodoc = reader.GetString(reader.GetOrdinal("nrodoc"));
                p.nombre = reader.GetString(reader.GetOrdinal("nombre"));
                resultado.Add(p);
            }

            return resultado;
        }
    }
}
