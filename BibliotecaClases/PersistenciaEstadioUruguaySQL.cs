using BibliotecaClases;
using ObligatorioProg3_Estadio;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace ObligatiorioProg3_Estadio
{
    public class PersistenciaEstadioUruguaySQL:PersistenciaEstadioUruguay
    {
        private static SqlConnection conectar()
        {
            String server = @"sql.bsite.net\MSSQL2016";
            String cadena = "Server=" + server + ";Database=fefabees_ESTADIOURUGUAY;User Id=fefabees_ESTADIOURUGUAY;Password=estadioUruguay;";
            SqlConnection conexion = new SqlConnection(cadena);
            conexion.Open();
            return conexion;
        }

        public List<Evento> EventosPorFecha(DateTime fecha, string tipo)
        {
            List<Evento > events = new List<Evento > ();
            Evento evento = null;
            SqlConnection conexion = conectar();
            SqlCommand comando = new SqlCommand("BuscarEventosPorFecha", conexion);
            SqlDataReader reader;

            comando.CommandType = CommandType.StoredProcedure;

            SqlParameter p1 = new SqlParameter();
            p1.Value = fecha.ToString("yyyy-MM-dd");
            p1.ParameterName = "@fecha";
            p1.SqlDbType = SqlDbType.DateTime;
            p1.Direction = ParameterDirection.Input;
            comando.Parameters.Add(p1);

            SqlParameter p2 = new SqlParameter();
            p2.Value = tipo;
            p2.ParameterName = "@tipoBusqueda";
            p2.SqlDbType = SqlDbType.VarChar;
            p2.Direction = ParameterDirection.Input;
            comando.Parameters.Add(p2);

            reader = comando.ExecuteReader();
            while (reader.Read())
            {
                evento = new Evento();
                evento.idevento = reader.GetInt32(reader.GetOrdinal("idevento"));
                evento.nombre = reader.GetString(reader.GetOrdinal("nombre_evento"));
                evento.fecha_hora_inicio = reader.GetDateTime(reader.GetOrdinal("fecha_hora"));
                evento.fecha_hora_fin = reader.GetDateTime(reader.GetOrdinal("fecha_hora_fin"));
                events.Add(evento);
            }
            return events;

        }

        public List<Evento> FiltroEvento(string nombre_evento, DateTime fecha_inicio, DateTime fecha_fin)
        {
            List<Evento> events = new List<Evento>();
            Evento evento = null;
            SqlConnection conexion = conectar();
            SqlCommand comando = new SqlCommand("FiltroEventos", conexion);
            SqlDataReader reader;

            comando.CommandType = CommandType.StoredProcedure;

            SqlParameter p1 = new SqlParameter();
            p1.Value = nombre_evento;
            p1.ParameterName = "@nombre_evento";
            p1.SqlDbType = SqlDbType.VarChar;
            p1.Direction = ParameterDirection.Input;
            comando.Parameters.Add(p1);

            SqlParameter p2 = new SqlParameter();
            p2.Value = fecha_inicio.ToString("yyyy-MM-dd");
            p2.ParameterName = "@fecha_hora";
            p2.SqlDbType = SqlDbType.DateTime;
            p2.Direction = ParameterDirection.Input;
            comando.Parameters.Add(p2);

            SqlParameter p3 = new SqlParameter();
            p3.Value = fecha_fin.ToString("yyyy-MM-dd");
            p3.ParameterName = "@fecha_hora_fin";
            p3.SqlDbType = SqlDbType.DateTime;
            p3.Direction = ParameterDirection.Input;
            comando.Parameters.Add(p3);

            reader = comando.ExecuteReader();
            while (reader.Read())
            {
                evento = new Evento();
                evento.idevento = reader.GetInt32(reader.GetOrdinal("idevento"));
                evento.nombre = reader.GetString(reader.GetOrdinal("nombre_evento"));
                evento.fecha_hora_inicio = reader.GetDateTime(reader.GetOrdinal("fecha_hora"));
                evento.fecha_hora_fin = reader.GetDateTime(reader.GetOrdinal("fecha_hora_fin"));
                events.Add(evento);
            }
            return events;
        }

        public List<RecaudacionEvento> RecaudacionEventos(int idevento)
        {
            int cont = 0;
            List<RecaudacionEvento> recaudacion = new List<RecaudacionEvento>();
            RecaudacionEvento re = null;
            SqlConnection conexion = conectar();
            SqlCommand sentencia = new SqlCommand("select * from V_RECAUDACIONEVENTOS (nolock) where idevento="+idevento, conexion);
            cont = sentencia.ExecuteNonQuery();
            SqlDataReader reader = sentencia.ExecuteReader();

            if (cont == 0)
            {
                recaudacion = null;
            }
            else
            {
                while (reader.Read())
                {
                    re = new RecaudacionEvento();
                    re.evento = (string)reader["Evento"];
                    re.puerta = (int)reader["Puerta"];
                    re.vendidas = (int)reader["Vendidas"];
                    re.recaudacion = (double)reader["Recaudacion"];
                    re.lugaresLibres = (int)reader["Lugares Libres"];
                    re.inicio = (DateTime)reader["Fecha Inicio"];
                    re.fin = (DateTime)reader["Fecha Fin"];

                    recaudacion.Add(re);
                }
            }
            return recaudacion;
        }

        public List<RecaudacionEvento> RecaudacionEventosFecha(DateTime inicio, DateTime fin)
        {
            int cont = 0;
            List<RecaudacionEvento> recaudacion = new List<RecaudacionEvento>();
            RecaudacionEvento re = null;
            SqlConnection conexion = conectar();
            SqlCommand sentencia = new SqlCommand("select * from V_RECAUDACIONEVENTOS (nolock) where [Fecha Inicio]>=@inicio and [Fecha Fin]<=@fin", conexion);
            sentencia.Parameters.AddWithValue("@inicio", inicio);
            sentencia.Parameters.AddWithValue("@fin", fin);
            cont = sentencia.ExecuteNonQuery();
            SqlDataReader reader = sentencia.ExecuteReader();

            if (cont == 0)
            {
                recaudacion = null;
            }
            else
            {
                while (reader.Read())
                {
                    re = new RecaudacionEvento();
                    re.evento = (string)reader["Evento"];
                    re.puerta = (int)reader["Puerta"];
                    re.vendidas = (int)reader["Vendidas"];
                    re.recaudacion = (double)reader["Recaudacion"];
                    re.lugaresLibres = (int)reader["Lugares Libres"];
                    re.inicio = (DateTime)reader["Fecha Inicio"];
                    re.fin = (DateTime)reader["Fecha Fin"];

                    recaudacion.Add(re);
                }
            }
            return recaudacion;
        }

        public List<RecaudacionEvento> ResumenRecaudacion(int idevento)
        {
            List<RecaudacionEvento> recaudacion = new List<RecaudacionEvento>();
            RecaudacionEvento re = null;
            SqlConnection conexion = conectar();
            SqlCommand sentencia = new SqlCommand("select Sum(Vendidas) as TotalVendidas, Sum(Recaudacion) as Recaudacion,Sum([Lugares Libres]) as Libres from V_RECAUDACIONEVENTOS (nolock) where idevento=" + idevento, conexion);
            SqlDataReader reader = sentencia.ExecuteReader();

            try
            {
                while (reader.Read())
                {
                    re = new RecaudacionEvento();
                    re.vendidas = (int)reader["TotalVendidas"];
                    re.recaudacion = (double)reader["Recaudacion"];
                    re.lugaresLibres = (int)reader["Libres"];

                    recaudacion.Add(re);
                }
            }
            catch (Exception ex)
            {
                recaudacion = null;
            }
            return recaudacion;
        }

        public List<RecaudacionEvento> ResumenRecaudacionFecha(DateTime inicio, DateTime fin)
        {
            List<RecaudacionEvento> recaudacion = new List<RecaudacionEvento>();
            RecaudacionEvento re = null;
            SqlConnection conexion = conectar();
            SqlCommand sentencia = new SqlCommand("select Sum(Vendidas) as TotalVendidas, Sum(Recaudacion) as Recaudacion,Sum([Lugares Libres]) as Libres from V_RECAUDACIONEVENTOS (nolock) where [Fecha Inicio]>=@inicio and [Fecha Fin]<=@fin", conexion);
            sentencia.Parameters.AddWithValue("@inicio", inicio);
            sentencia.Parameters.AddWithValue("@fin", fin);
            SqlDataReader reader = sentencia.ExecuteReader();

            try
            {
                while (reader.Read())
                {
                    re = new RecaudacionEvento();
                    re.vendidas = (int)reader["TotalVendidas"];
                    re.recaudacion = (double)reader["Recaudacion"];
                    re.lugaresLibres = (int)reader["Libres"];

                    recaudacion.Add(re);
                }
            }
            catch (Exception ex)
            {
                recaudacion = null;
            }
            return recaudacion;
        }

        public List<ViewDisponibilidad> V_DATOSEVENTO(int idevento)
        {
            List<ViewDisponibilidad> viewDisponibilidads = new List<ViewDisponibilidad>();
            ViewDisponibilidad disponibilidad = null;
            SqlConnection conexion = conectar();
            SqlCommand sentencia = new SqlCommand("SELECT [idevento],[idpuerta],[disponible] FROM [fefabees_ESTADIOURUGUAY].[dbo].[V_DATOSEVENTO] (nolock) WHERE idevento =" + idevento, conexion);
            SqlDataReader reader = sentencia.ExecuteReader();

            try
            {
                while (reader.Read())
                {
                    disponibilidad = new ViewDisponibilidad();
                    disponibilidad.idevento = reader.GetInt32(reader.GetOrdinal("idevento"));
                    disponibilidad.idpuerta = reader.GetInt32(reader.GetOrdinal("idpuerta"));
                    disponibilidad.disponible = reader.GetInt32(reader.GetOrdinal("disponible"));
                    viewDisponibilidads.Add(disponibilidad);
                }
            }
            catch (Exception ex)
            {
                viewDisponibilidads = null;
            }
            return viewDisponibilidads;
        }
    }
}
