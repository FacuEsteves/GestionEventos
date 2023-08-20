using ObligatiorioProg3_Estadio;
using ObligatorioProg3_Estadio;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolkit.Core.DataTypes.Semantic.Types;

namespace ObligatiorioProg3_Estadio
{
    public class PersistenciaCompraSQL : PersistenciaCompra
    {
        private static SqlConnection conectar()
        {
            String server = @"sql.bsite.net\MSSQL2016";
            String cadena = "Server=" + server + ";Database=fefabees_ESTADIOURUGUAY;User Id=fefabees_ESTADIOURUGUAY;Password=estadioUruguay;";
            SqlConnection conexion = new SqlConnection(cadena);
            conexion.Open();
            return conexion;
        }
        public Compra buscar(int id)
        {
            SqlConnection conexion = conectar();
            SqlCommand comando = new SqlCommand("select* from COMPRA (nolock) where idcompra=@idcompra", conexion);
            comando.Parameters.AddWithValue("@idcompra", id);
            SqlDataReader reader;
            Compra c = null;

            reader = comando.ExecuteReader();

            if (reader.Read())
            {
                c = new Compra();
                c.idcompra = id;
                c.evento = reader.GetInt32(reader.GetOrdinal("idevento"));
                c.cliente= reader.GetString(reader.GetOrdinal("idcliente"));
                c.puerta = reader.GetInt32(reader.GetOrdinal("idpuerta"));
            }
            return c;
        }

        public Compra buscarCliente(String idcliente)
        {
            SqlConnection conexion = conectar();
            SqlCommand comando = new SqlCommand("select* from COMPRA (nolock) where idcliente=@idcliente", conexion);
            comando.Parameters.AddWithValue("@idcliente", idcliente);
            SqlDataReader reader;
            Compra c = null;

            reader = comando.ExecuteReader();

            while (reader.Read())
            {
                c = new Compra();
                c.cliente = idcliente;
                c.idcompra = reader.GetInt32(reader.GetOrdinal("idcompra"));
            }
            return c;
  
            
        }

        public int eliminar(int id)
        {
            SqlConnection conexion = conectar();
            SqlCommand comando = new SqlCommand("delete from COMPRA where idevento=" + id, conexion);
            int cont = comando.ExecuteNonQuery();
            return cont;
        }

        public int guardar(Compra c)
        {
            int cont = 0;
            SqlTransaction transaccion = null;

            try
            {
              SqlConnection conexion = conectar();
                transaccion = conexion.BeginTransaction();

                SqlCommand comando = new SqlCommand("update COMPRA set idevento=@idevento,idpuerta=@idpuerta,idcliente=@idcliente where idcliente=@idcliente AND idevento=@idevento", conexion, transaccion);
                comando.Parameters.AddWithValue("@idevento", c.evento);
                comando.Parameters.AddWithValue("@idpuerta", c.puerta);
                comando.Parameters.AddWithValue("@idcliente", c.cliente);

                cont = comando.ExecuteNonQuery();

                if (cont == 0)
                {

                    comando = new SqlCommand("insert into COMPRA (idevento,idcliente,idpuerta) values (@idevento, @idcliente, @idpuerta)", conexion,transaccion);
                    comando.Parameters.AddWithValue("@idevento", c.evento);
                    comando.Parameters.AddWithValue("@idpuerta", c.puerta);
                    comando.Parameters.AddWithValue("@idcliente", c.cliente);
                    cont = comando.ExecuteNonQuery();
                   }
                //confirmar transaccion en la base de datos
                transaccion.Commit();
            }
            catch (Exception ex)
            {
                if (transaccion == null)
                {
                    transaccion.Rollback();
                }
            }

            return cont;
        }

        public List<Compra> lista()
        {
            List<Compra> lista = new List<Compra>();
            Compra c = null;
            SqlConnection conexion = conectar();
            SqlCommand sentencia = new SqlCommand("select * from COMPRA (nolock)", conexion);
            SqlDataReader reader = sentencia.ExecuteReader();

            while (reader.Read())
            {
                c = new Compra();
                c.idcompra = (int)reader["idcompra"];
                /*c.idevento = (int)reader["idevento"];
                c.idcliente = (int)reader["idcliente"];
                c.idtribuna = (int)reader["idtribuna"];
                c.idpuerta = (int)reader["idpuerta"];*/

                lista.Add(c);
            }
            return lista;
        }
    }
}
