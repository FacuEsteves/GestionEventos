using ObligatiorioProg3_Estadio;
using ObligatorioProg3_Estadio;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ObligatiorioProg3_Estadio
{
    public class PersistenciaClienteSQL : PersistenciaCliente
    {
        private static SqlConnection conectar()
        {
            String server = @"sql.bsite.net\MSSQL2016";
            String cadena = "Server=" + server + ";Database=fefabees_ESTADIOURUGUAY;User Id=fefabees_ESTADIOURUGUAY;Password=estadioUruguay;";
            SqlConnection conexion = new SqlConnection(cadena);
            conexion.Open();
            return conexion;
        }
        public Cliente buscar(string nrodoc)
        {
            SqlConnection conexion = conectar();
            SqlCommand comando = new SqlCommand("SELECT p.nombre, p.apellido, p.correo, c.socio FROM PERSONA as p, CLIENTE as c (nolock) where c.nrodoc=p.nrodoc AND c.nrodoc=" + nrodoc, conexion);
            SqlDataReader reader;
            Cliente c = null;

            reader = comando.ExecuteReader();

            if (reader.Read())
            {
                c = new Cliente();
                c.nrodoc = nrodoc;
                c.nombre = reader.GetString(reader.GetOrdinal("nombre"));
                c.apellido = reader.GetString(reader.GetOrdinal("apellido"));
                c.correo = reader.GetString(reader.GetOrdinal("correo"));
                 c.socio = reader.GetBoolean(reader.GetOrdinal("socio"));

            }

            return c;
        }

        public int eliminar(string nrodoc)
        {
            SqlConnection conexion = conectar();
            SqlCommand comando = new SqlCommand("DELETE FROM CLIENTE WHERE nrodoc=" + nrodoc, conexion);
            int cont = comando.ExecuteNonQuery();
            return cont;
        }

        public int guardar(Cliente c)
        {
            int cont = 0;
            SqlTransaction transaccion = null;

            try
            {
                SqlConnection conexion = conectar();
                transaccion = conexion.BeginTransaction();

                SqlCommand comando = new SqlCommand("UPDATE CLIENTE SET socio = @socio WHERE nrodoc = @nrodoc", conexion, transaccion);
                comando.Parameters.AddWithValue("@socio", c.socio);
                comando.Parameters.AddWithValue("@nrodoc", c.nrodoc);
                cont = comando.ExecuteNonQuery();

                if (cont == 0)
                {
                    comando = new SqlCommand("INSERT INTO CLIENTE (nrodoc,socio) VALUES (@nrodoc , @socio)", conexion, transaccion);
                    comando.Parameters.AddWithValue("@nrodoc", c.nrodoc);;
                    comando.Parameters.AddWithValue("@socio", c.socio);
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

        public List<Cliente> lista()
        {
            SqlConnection conexion = conectar();
            SqlCommand comando = new SqlCommand("SELECT c.nrodoc, p.nombre, p.apellido, p.correo, c.socio FROM PERSONA as p, CLIENTE as c (nolock)", conexion);
            SqlDataReader reader;
            Cliente c = null;
            List<Cliente> resultado = new List<Cliente>();

            reader = comando.ExecuteReader();

            while (reader.Read())
            {
                c = new Cliente();
                c.nrodoc = reader.GetString(reader.GetOrdinal("nrodoc"));
                c.nombre = reader.GetString(reader.GetOrdinal("nombre"));
                c.apellido = reader.GetString(reader.GetOrdinal("apellido"));
                c.correo = reader.GetString(reader.GetOrdinal("correo"));
                c.socio = reader.GetBoolean(reader.GetOrdinal("socio"));
                resultado.Add(c);
            }

            return resultado;
        }
    }
}
