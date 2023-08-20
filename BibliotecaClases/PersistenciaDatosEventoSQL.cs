using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ObligatiorioProg3_Estadio;
using ObligatorioProg3_Estadio;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Toolkit.Core.DataTypes.Semantic.Types;

namespace ObligatiorioProg3_Estadio
{
    public class PersistenciaDatosEventoSQL : PersistenciaDatosEvento
    {
        private static SqlConnection conectar()
        {
            String server = @"sql.bsite.net\MSSQL2016";
            String cadena = "Server=" + server + ";Database=fefabees_ESTADIOURUGUAY;User Id=fefabees_ESTADIOURUGUAY;Password=estadioUruguay;";
            SqlConnection conexion = new SqlConnection(cadena);
            conexion.Open();
            return conexion;
        }

        public DatosEvento buscarPrecio(int idev, int idp)
        {
            SqlConnection conexion = conectar();
            SqlCommand comando = new SqlCommand("select costo from DATOSEVENTO (nolock) where idevento=@idev and idpuerta=@idp", conexion);
            comando.Parameters.AddWithValue("@idev", idev);
            comando.Parameters.AddWithValue("@idp",idp );
            SqlDataReader reader;
           DatosEvento de = null;

            reader = comando.ExecuteReader();

            if (reader.Read())
            {
                de = new DatosEvento();
              
                de.costo = reader.GetDouble(reader.GetOrdinal("costo"));
      
            }
            return de;
        }

        public int eliminar(int idevento)
        {
            throw new NotImplementedException();
        }

        public int guardar(DatosEvento datos)
        {
            int cont = 0;
            SqlTransaction transaccion = null;

            try
            {
                SqlConnection conexion = conectar();
                transaccion = conexion.BeginTransaction();

                SqlCommand comando = new SqlCommand("UPDATE DATOSEVENTO SET capacidad = @capacidad, costo = @costo WHERE idevento = @idevento and idpuerta=@idpuerta", conexion, transaccion);
                comando.Parameters.AddWithValue("@idevento", datos.evento);
                comando.Parameters.AddWithValue("@idpuerta", datos.puerta);
                comando.Parameters.AddWithValue("@capacidad", datos.capacidad);
                comando.Parameters.AddWithValue("@costo", datos.costo);

                cont = comando.ExecuteNonQuery();

                if (cont == 0)
                {

                    comando = new SqlCommand("INSERT INTO DATOSEVENTO(idevento,idpuerta,capacidad,costo) VALUES (@idevento,@idpuerta,@capacidad,@costo)", conexion, transaccion);
                    comando.Parameters.AddWithValue("@idevento", datos.evento);
                    comando.Parameters.AddWithValue("@idpuerta", datos.puerta);
                    comando.Parameters.AddWithValue("@capacidad", datos.capacidad);
                    comando.Parameters.AddWithValue("@costo", datos.costo);

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

        public List<DatosEvento> lista()
        {
            throw new NotImplementedException();
        }

        public List<DatosEvento> listaBuscar(int idevento)
        {
            List<DatosEvento> lista = new List<DatosEvento>();
            DatosEvento e = null;
            SqlConnection conexion = conectar();
            SqlCommand comando = new SqlCommand("select [idpuerta],[capacidad],[costo] from DATOSEVENTO (nolock) where idevento="+idevento, conexion);
            SqlDataReader reader = comando.ExecuteReader();
            
            while (reader.Read())
            {
                e = new DatosEvento();
                e.puerta = reader.GetInt32(reader.GetOrdinal("idpuerta"));
                e.capacidad = reader.GetInt32(reader.GetOrdinal("capacidad"));
                e.costo = reader.GetDouble(reader.GetOrdinal("costo"));
                lista.Add(e);
            }
            return lista;
        }

        public List<DatosEvento> listainserta(string id)
        {
            List<DatosEvento> lista = new List<DatosEvento>();
            DatosEvento e = null;
            SqlConnection conexion = conectar();
            SqlCommand comando = new SqlCommand("SELECT idpuerta FROM PUERTA WHERE idtribuna = @idtribuna", conexion);
            comando.Parameters.AddWithValue("@idtribuna", id);
            SqlDataReader reader = comando.ExecuteReader();

            while (reader.Read())
            {
                e = new DatosEvento();
                e.puerta = reader.GetInt32(reader.GetOrdinal("idpuerta"));
                e.capacidad = 0;
                e.costo = 0;
                lista.Add(e);
            }
            return lista;
        }
    }
}
