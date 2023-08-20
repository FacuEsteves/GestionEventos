using ObligatorioProg3_Estadio;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObligatiorioProg3_Estadio
{
    public class PersistenciaPromocionSQL : PersistenciaPromocion
    {
        private static SqlConnection conectar()
        {
            String server = @"sql.bsite.net\MSSQL2016";
            String cadena = "Server=" + server + ";Database=fefabees_ESTADIOURUGUAY;User Id=fefabees_ESTADIOURUGUAY;Password=estadioUruguay;";
            SqlConnection conexion = new SqlConnection(cadena);
            conexion.Open();
            return conexion;
        }
        public Promocion buscar(int idpromo)
        {
            SqlConnection conexion = conectar();
            SqlCommand comando = new SqlCommand("SELECT [idpromocion],[descripcion] FROM [PROMOCION] where idpromocion =" + idpromo, conexion);
            SqlDataReader reader;
            Promocion p = null;

            reader = comando.ExecuteReader();

            if (reader.Read())
            {
                p = new Promocion();
                p.idpromocion = reader.GetInt32(reader.GetOrdinal("idpromocion"));
                p.descripcion = reader.GetString(reader.GetOrdinal("descripcion"));
            }

            return p;
        }

        public int eliminar(string nrodoc)
        {
            throw new NotImplementedException();
        }

        public int guardar(Persona p)
        {
            throw new NotImplementedException();
        }

        public List<Persona> lista()
        {
            throw new NotImplementedException();
        }
    }
}
