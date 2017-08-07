using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;
using Npgsql;

//CONEXION A LA BASE DE DATOS
namespace CapaDatos
{
    public class Conexion
    {
        #region "PATRON SINGLETON"
        private static Conexion conexion = null;
        private Conexion() { }
        public static Conexion getInstance()
        {
            if(conexion == null)
            {
                conexion = new Conexion();
            }
            return conexion;
        }
        #endregion 

        public NpgsqlConnection ConexionDB()
        {
            NpgsqlConnection conexion = new NpgsqlConnection();
            String connectionString = ConfigurationManager.ConnectionStrings["migague-postgres"].ConnectionString;
            conexion.ConnectionString = connectionString;
            return conexion;
        }
    }
}
