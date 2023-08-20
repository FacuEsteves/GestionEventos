using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObligatiorioProg3_Estadio
{
    public class PersistenciaDetalleCompraSQL : PersistenciaDetalleCompra
    {
        private static SqlConnection conectar()
        {
            String server = @"sql.bsite.net\MSSQL2016";
            String cadena = "Server=" + server + ";Database=fefabees_ESTADIOURUGUAY;User Id=fefabees_ESTADIOURUGUAY;Password=estadioUruguay;";
            SqlConnection conexion = new SqlConnection(cadena);
            conexion.Open();
            return conexion;
        }
        public DetalleCompra buscar(int id)
        {
            SqlConnection conexion = conectar();
            SqlCommand comando = new SqlCommand("select* from DETALLECOMPRA (nolock) where idcompra=@idcompra", conexion);
            comando.Parameters.AddWithValue("@idcompra", id);
            SqlDataReader reader;
            DetalleCompra detalleC = null;

            reader = comando.ExecuteReader();

            if (reader.Read())
            {
                detalleC = new DetalleCompra();
                detalleC.compra = id;
                detalleC.precio = reader.GetDouble(reader.GetOrdinal("precio"));
                detalleC.acceso = Convert.ToInt32(reader.GetOrdinal("acceso"));
            }
            return detalleC;
        }

        public int eliminar(int id)
        {
            SqlConnection conexion = conectar();
            SqlCommand comando = new SqlCommand("delete from DETALLECOMPRA where idevento=" + id, conexion);
            int cont = comando.ExecuteNonQuery();
            return cont;
        }

        public int guardar(DetalleCompra detalleCompra)
        {
            int cont = 0;
            SqlTransaction transaccion = null;

            try
            {

                SqlConnection conexion = conectar();
                transaccion = conexion.BeginTransaction();

                SqlCommand comando = new SqlCommand("UPDATE DETALLECOMPRA SET idcompra = @idcompra, precio = @precio, acceso = @acceso  WHERE idcompra = @idcompra", conexion, transaccion);
                comando.Parameters.AddWithValue("@idcompra", detalleCompra.compra);
                comando.Parameters.AddWithValue("@precio", detalleCompra.precio);
                comando.Parameters.AddWithValue("@acceso", detalleCompra.acceso);
                
                cont = comando.ExecuteNonQuery();

                if (cont == 0) {
                 comando = new SqlCommand("insert into DETALLECOMPRA (idcompra,precio,acceso) values (@idcompra,@precio,@acceso)", conexion, transaccion);
                 comando.Parameters.AddWithValue("@idcompra", detalleCompra.compra);
                 comando.Parameters.AddWithValue("@precio", detalleCompra.precio);
                comando.Parameters.AddWithValue("@acceso", detalleCompra.acceso);
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

        public List<DetalleCompra> lista()
        {
            List<DetalleCompra> lista = new List<DetalleCompra>();
            DetalleCompra detalleC = null;
            SqlConnection conexion = conectar();
            SqlCommand sentencia = new SqlCommand("select * from DETALLECOMPRA (nolock)", conexion);
            SqlDataReader reader = sentencia.ExecuteReader();

            while (reader.Read())
            {
                detalleC = new DetalleCompra();
                /*detalleC.iddetallecompra = (int)reader["iddetallecompra"];
                detalleC.idcompra = (int)reader["idcompra"];
                detalleC.precio = (float)reader["precio"];*/

                lista.Add(detalleC);
            }
            return lista;
        }
    }
}
