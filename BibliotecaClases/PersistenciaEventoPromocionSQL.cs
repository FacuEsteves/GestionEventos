using BibliotecaClases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using ObligatorioProg3_Estadio;
namespace ObligatiorioProg3_Estadio
{
    public class PersistenciaEventoPromocionSQL : PersistenciaEventoPromocion
    {
        private static SqlConnection conectar()
        {
            String server = @"sql.bsite.net\MSSQL2016";
            String cadena = "Server=" + server + ";Database=fefabees_ESTADIOURUGUAY;User Id=fefabees_ESTADIOURUGUAY;Password=estadioUruguay;";
            SqlConnection conexion = new SqlConnection(cadena);
            conexion.Open();
            return conexion;
        }
        public EventoPromocion buscar(int idev)
        {
            SqlConnection conexion = conectar();
            SqlCommand comando = new SqlCommand("SELECT [idevento],[idpromocion],[Descuento] FROM [fefabees_ESTADIOURUGUAY].[dbo].[EventoPromocion] (nolock) where [idevento] = " + idev, conexion);
            SqlDataReader reader;
            EventoPromocion ep = null;

            reader = comando.ExecuteReader();

            if (reader.Read())
            {
                ep = new EventoPromocion();
               // ep.evento = idevento;
                ep.promocion = reader.GetInt32(reader.GetOrdinal("idpromocion"));
                ep.Descuento = reader.GetDouble(reader.GetOrdinal("Descuento"));
            }

            return ep;
        }

        public int guardar(EventoPromocion eventoPromocion)
        {
            int cont = 0;
            SqlTransaction transaccion = null;

            try
            {
                SqlConnection conexion = conectar();
                transaccion = conexion.BeginTransaction();

                SqlCommand comando = new SqlCommand("UPDATE [dbo].[EventoPromocion] SET [idpromocion] = @idpromocion,[Descuento] = @Descuento WHERE [idevento] = @idevento", conexion, transaccion);
                comando.Parameters.AddWithValue("@idevento", eventoPromocion.evento);
                comando.Parameters.AddWithValue("@idpromocion", eventoPromocion.promocion);
                comando.Parameters.AddWithValue("@Descuento", eventoPromocion.Descuento);
                cont = comando.ExecuteNonQuery();

                if (cont == 0)
                {
                    comando = new SqlCommand("INSERT INTO [dbo].[EventoPromocion] ([idevento],[idpromocion],[Descuento]) VALUES (@idevento,@idpromocion,@Descuento)", conexion, transaccion);
                    comando.Parameters.AddWithValue("@idevento", eventoPromocion.evento);
                    comando.Parameters.AddWithValue("@idpromocion", eventoPromocion.promocion);
                    comando.Parameters.AddWithValue("@Descuento", eventoPromocion.Descuento);
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
    }
}
