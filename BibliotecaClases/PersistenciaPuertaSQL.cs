using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolkit.Core.Extensions;
using ObligatorioProg3_Estadio;

namespace ObligatiorioProg3_Estadio
{
    public class PersistenciaPuertaSQL : PersistenciaPuerta
    {
        private static SqlConnection conectar()
        {
            String server = @"sql.bsite.net\MSSQL2016";
            String cadena = "Server=" + server + ";Database=fefabees_ESTADIOURUGUAY;User Id=fefabees_ESTADIOURUGUAY;Password=estadioUruguay;";
            SqlConnection conexion = new SqlConnection(cadena);
            conexion.Open();
            return conexion;
        }

        public Puerta buscar(int idpuerta)
        {
            SqlConnection conexion = conectar();
            SqlCommand comando = new SqlCommand("select*  from PUERTA (nolock) where idpuerta=" + idpuerta, conexion);
            SqlDataReader reader;
             Puerta p = null;

            reader = comando.ExecuteReader();

            if (reader.Read())
            {
                p = new Puerta();
                p.tribuna= reader.GetString(reader.GetOrdinal("idtribuna"));
                p.nombre = reader.GetString(reader.GetOrdinal("nombre_puerta"));
                p.capacidad_total = reader.GetInt32(reader.GetOrdinal("capacidad_total"));
                p.capacidad_parcial = reader.GetInt32(reader.GetOrdinal("capacidad_parcial"));
            }
            return p; //devolvemos puerta seleccionada
        }

        public int eliminar(int id)
        {
            throw new NotImplementedException();
        }

        public int guardar(Puerta p)
        {
            throw new NotImplementedException();
        }

        public List<Puerta> lista()
        {
            List<Puerta> lista = new List<Puerta>();
            Puerta p = null;
            SqlConnection conexion = conectar();
            SqlCommand comando = new SqlCommand("SELECT idpuerta,nombre_puerta,capacidad_total,capacidad_parcial,idtribuna FROM PUERTA", conexion);
            SqlDataReader reader = comando.ExecuteReader();

            while (reader.Read())
            {
                p = new Puerta();
                p.idpuerta = reader.GetInt32(reader.GetOrdinal("idpuerta"));
                p.nombre = reader.GetString(reader.GetOrdinal("nombre_puerta"));
                p.capacidad_total = reader.GetInt32(reader.GetOrdinal("capacidad_total"));
                p.capacidad_parcial = reader.GetInt32(reader.GetOrdinal("capacidad_parcial"));
                p.tribuna = reader.GetString(reader.GetOrdinal("idtribuna"));
                lista.Add(p);
            }
            return lista;
        }
    }
}   
