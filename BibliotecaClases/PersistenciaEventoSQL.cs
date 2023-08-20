using ObligatorioProg3_Estadio;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObligatiorioProg3_Estadio
{
    public class PersistenciaEventoSQL : PersistenciaEvento
    {
        private static SqlConnection conectar()
        {
            String server = @"sql.bsite.net\MSSQL2016";
            String cadena = "Server=" + server + ";Database=fefabees_ESTADIOURUGUAY;User Id=fefabees_ESTADIOURUGUAY;Password=estadioUruguay;";
            SqlConnection conexion = new SqlConnection(cadena);
            conexion.Open();
            return conexion;
        }
        public Evento buscar(int id)
        {
            SqlConnection conexion = conectar();
            SqlCommand comando = new SqlCommand("select [nombre_evento],[fecha_hora],[fecha_hora_fin] from EVENTO (nolock) where idevento=" + id, conexion);
            SqlDataReader reader;
            Evento e = null;

            reader = comando.ExecuteReader();

            if (reader.Read())
            {
                e = new Evento();
                e.idevento = id;
                e.nombre= reader.GetString(reader.GetOrdinal("nombre_evento"));
                e.fecha_hora_inicio = reader.GetDateTime(reader.GetOrdinal("fecha_hora"));
                e.fecha_hora_fin = reader.GetDateTime(reader.GetOrdinal("fecha_hora_fin"));
            }
            return e;
        }

        public Evento buscarNombre(int id)
        {
            SqlConnection conexion = conectar();
            SqlCommand comando = new SqlCommand("select nombre_evento from EVENTO (nolock) where idevento=" + id, conexion);
            SqlDataReader reader;
            Evento e = null;

            reader = comando.ExecuteReader();

            if (reader.Read())
            {
                e = new Evento();
             
                e.nombre = reader.GetString(reader.GetOrdinal("nombre_evento"));
            
            }
            return e;
        }

        public int eliminar(int id)
        {
            SqlConnection conexion = conectar();
            SqlCommand comando = new SqlCommand("delete from EVENTO where idevento=" + id, conexion);
            int cont = comando.ExecuteNonQuery();
            return cont;
        }

        public int guardar(Evento e)
        {
            int cont = 0;
            SqlTransaction transaccion = null;

            try
            {
                SqlConnection conexion = conectar();
                transaccion = conexion.BeginTransaction();
                SqlCommand comando = null;


                comando = new SqlCommand("update EVENTO set fecha_hora=@fecha_hora_inicio, fecha_hora_fin=@fecha_hora_fin where nombre_evento=@nombre_evento", conexion, transaccion);
                comando.Parameters.AddWithValue("@nombre_evento", e.nombre);
                comando.Parameters.AddWithValue("@fecha_hora_inicio", e.fecha_hora_inicio);
                comando.Parameters.AddWithValue("@fecha_hora_fin", e.fecha_hora_fin);


                cont = comando.ExecuteNonQuery();

                if (cont == 0)
                {
                    comando = new SqlCommand("insert into EVENTO (nombre_evento,fecha_hora, fecha_hora_fin) values (@nombre_evento,@fecha_hora_inicio,@fecha_hora_fin)", conexion, transaccion);
                    comando.Parameters.AddWithValue("@nombre_evento", e.nombre);
                    comando.Parameters.AddWithValue("@fecha_hora_inicio", e.fecha_hora_inicio);
                    comando.Parameters.AddWithValue("@fecha_hora_fin", e.fecha_hora_fin);
                    cont = comando.ExecuteNonQuery();
                }
                //confirmar transaccion en la base de datos
                transaccion.Commit();
            }
            catch (Exception ex)
            {
                if (transaccion != null)
                {
                    transaccion.Rollback();
                }
            }

            return cont;
        }

        public List<Evento> lista()
        {
            List<Evento> lista = new List<Evento>();
            Evento evento = null;
            SqlConnection conexion = conectar();
            SqlCommand sentencia = new SqlCommand("SELECT idevento,nombre_evento,fecha_hora,fecha_hora_fin FROM EVENTO (nolock)", conexion);
            SqlDataReader reader = sentencia.ExecuteReader();

            while (reader.Read())
            {
                evento = new Evento();
                evento.idevento = reader.GetInt32(reader.GetOrdinal("idevento"));
                evento.nombre = reader.GetString(reader.GetOrdinal("nombre_evento"));
                evento.fecha_hora_inicio = reader.GetDateTime(reader.GetOrdinal("fecha_hora"));
                evento.fecha_hora_fin = reader.GetDateTime(reader.GetOrdinal("fecha_hora_fin"));
                lista.Add(evento);
            }
            return lista;
        }

        public int UltimoId()
        {
            SqlConnection conexion = conectar();
            SqlCommand comando = new SqlCommand("SELECT MAX(idevento) AS 'UltimoId' from EVENTO", conexion);
            int resultado =(int)comando.ExecuteScalar();

            return resultado;
        }
    }
}
