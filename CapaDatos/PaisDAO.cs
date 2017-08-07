using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using CapaEntidades;
using Npgsql;

namespace CapaDatos
{
    public class PaisDAO
    {
        // getInstance
        #region "PATRON SINGLETON"
        private static PaisDAO paisDao = null;
        private PaisDAO() { }
        public static PaisDAO getInstance()
        {
            if (paisDao == null)
            {
                paisDao = new PaisDAO();
            }
            return paisDao;
        }
        #endregion

        #region "ABMS"
        public List<Pais> listaPaises(string schema)
        {
            List<Pais> listaPaises = new List<Pais>();
            NpgsqlConnection conexion = null;
            NpgsqlCommand cmd = null;
            NpgsqlTransaction tran = null;
            NpgsqlDataReader dr = null;

            try
            {
                conexion = Conexion.getInstance().ConexionDB();
                cmd = new NpgsqlCommand("logueo.spgetPaises", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("parm_schema", schema);
                conexion.Open();
                tran = conexion.BeginTransaction();
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Pais pais = new Pais();
                    pais.id = Convert.ToInt32(dr["ID"].ToString());
                    pais.nombre = dr["DESCRIPCION"].ToString();
                    pais.fecha = Convert.ToDateTime(dr["FECHA"].ToString());
                    pais.es_activo = Convert.ToBoolean(dr["ESACTIVO"].ToString());
                    listaPaises.Add(pais);
                }
                dr.Close();
            }
            catch (Exception e)
            {
                listaPaises = null;
                throw (e);
            }
            finally
            {
                tran.Commit();
                conexion.Close();
            }
            return listaPaises;
        }
        
        public string nuevoPais(string nombre, DateTime fecha, string schema)
        {
            string retorno = null;
            List<Pais> listPaises = listaPaises(schema);
            bool existe = validarObjetoExistente(listPaises, nombre);
            if (existe)
            {
                retorno = "El pais ya existe";
                return retorno;
            }
            NpgsqlConnection conexion = null;
            NpgsqlCommand cmd = null;
            try
            {
                conexion = Conexion.getInstance().ConexionDB();
                cmd = new NpgsqlCommand("logueo.spnuevopais", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("parm_nombre", nombre);
                cmd.Parameters.AddWithValue("parm_fecha", fecha);
                cmd.Parameters.AddWithValue("parm_schema", schema);
                conexion.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                conexion.Close();
            }
            retorno = "Nuevo pais añadido correctamente";
            return retorno;
        }

        public bool updatePais(int id, string nombre, DateTime fecha, string schema)
        {
            NpgsqlConnection conexion = null;
            NpgsqlCommand cmd = null;
            try
            {
                conexion = Conexion.getInstance().ConexionDB();
                cmd = new NpgsqlCommand("logueo.spupdatepais", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("parm_id", id);
                cmd.Parameters.AddWithValue("parm_nombre", nombre);
                cmd.Parameters.AddWithValue("parm_fecha", fecha);
                cmd.Parameters.AddWithValue("parm_schema", schema);
                conexion.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                conexion.Close();
            }
            return true;
        }

        public bool eliminarPais(int id, string nombre, string schema)
        {
            bool retorno = false;
            NpgsqlConnection conexion = null;
            NpgsqlCommand cmd = null;
            try
            {
                conexion = Conexion.getInstance().ConexionDB();
                cmd = new NpgsqlCommand("logueo.speliminarpais", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("parm_id", id);
                cmd.Parameters.AddWithValue("parm_nombre", nombre);
                cmd.Parameters.AddWithValue("parm_schema", schema);
                conexion.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                conexion.Close();
            }
            retorno = true;
            return retorno;
        }
        #endregion

        public bool validarObjetoExistente(List<Pais> lista, string nombre)
        {
            bool existe = false;
            foreach (Pais pais in lista)
            {
                if (pais.nombre.ToUpper().Trim() == nombre.ToUpper().Trim())
                {
                    existe = true;
                    break;
                }
            }
            return existe;
        }

    
    }
}
