using Biblioteca;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObligatiorioProg3_Estadio
{
    public class PersistenciaTribunaSQL : PersistenciaTribuna

    {
        private static SqlConnection crearConexion()
        {
            String server = @"sql.bsite.net\MSSQL2016";
            String cadena = "Server=" + server + ";Database=fefabees_ESTADIOURUGUAY;User Id=fefabees_ESTADIOURUGUAY;Password=estadioUruguay;";
            SqlConnection conexion = new SqlConnection(cadena);
            conexion.Open();
            return conexion;
        }
        public Tribuna buscar(String id)
        {
            SqlConnection conexion = crearConexion();
            SqlCommand comando = new SqlCommand("select idtribuna,nombre_tribuna from TRIBUNA (nolock) where idtribuna=" + id, conexion);
            SqlDataReader reader;
            Tribuna trib = null;

            reader = comando.ExecuteReader();

            if (reader.Read())
            {  //avanzamos al primer registro y comprobamos si hay datos
                trib = new Tribuna();
               trib.id_tribuna = id;
               trib.nombre_tribuna= reader.GetString(reader.GetOrdinal("nombre_tribuna"));
            }

            return trib;
        }
    
        public int eliminar(String id)
        {
            SqlConnection conexion = crearConexion();
            SqlCommand comando = new SqlCommand("delete from Tribuna where idtribuna=" + id, conexion);
            int cont = comando.ExecuteNonQuery(); // en este caso marcaria 1 porque se afecto una fila
            return cont;
        }

        public int guardar(Tribuna trib)
        {
            int cont = 0;
            SqlTransaction transaccion = null;
            try
            {
                SqlConnection conexion = crearConexion();
                transaccion = conexion.BeginTransaction();
                SqlCommand comando = new SqlCommand("Update Tribuna SET nombre_tribuna= @NOM where idtribuna= @ID", conexion, transaccion);
                comando.Parameters.AddWithValue("@NOM", trib.nombre_tribuna);
                comando.Parameters.AddWithValue("@ID", trib.id_tribuna);
                cont = comando.ExecuteNonQuery();
                if (cont == 0) //NO ENCONTRO REGISTRO PARA ACTUALIZAR, LO GUARDAMOS;
                {

                    comando = new SqlCommand("insert into Tribuna (nombre_tribuna , idtribuna) values (@NOM, @ID)", conexion, transaccion);
                    comando.Parameters.AddWithValue("@NOM", trib.nombre_tribuna);
                    comando.Parameters.AddWithValue("@ID", trib.id_tribuna);
                    cont = comando.ExecuteNonQuery();
                }
                //confirmar transaccion en la base de datos
                transaccion.Commit();

            }
            catch (Exception ex)
            {
                if (transaccion != null)
                    transaccion.Rollback();

            }
            return cont;
        }
    


        public List<Tribuna> lista()
        {
            SqlConnection conexion = crearConexion();
            SqlCommand comando = new SqlCommand("select idtribuna,nombre_tribuna from Tribuna (nolock)", conexion);
            SqlDataReader reader;
             Tribuna trib = null;
            List<Tribuna> resultado = new List<Tribuna>();

            reader = comando.ExecuteReader();

            while (reader.Read())
            {
               trib = new Tribuna();
                trib.id_tribuna = reader.GetString(reader.GetOrdinal("idtribuna"));
                trib.nombre_tribuna = reader.GetString(reader.GetOrdinal("nombre_tribuna"));
                resultado.Add(trib);
            }

            return resultado;
        }
    }
}

