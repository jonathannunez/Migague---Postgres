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
    public class BancoDAO
    {
        // getInstance
        #region "PATRON SINGLETON"
        private static BancoDAO bancoDao = null;
        private BancoDAO() { }
        public static BancoDAO getInstance()
        {
            if (bancoDao == null)
            {
                bancoDao = new BancoDAO();
            }
            return bancoDao;
        }
        #endregion


        #region "ABMS"
        public List<Banco> listaBancos(string schema)
        {
            List<Banco> listaBancos = new List<Banco>();
            NpgsqlConnection conexion = null;
            NpgsqlCommand cmd = null;
            NpgsqlTransaction tran = null;
            NpgsqlDataReader dr = null;

            try
            {
                conexion = Conexion.getInstance().ConexionDB();
                cmd = new NpgsqlCommand("logueo.spgetbancos", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("parm_schema", schema);
                conexion.Open();
                tran = conexion.BeginTransaction();
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Banco banco = new Banco();
                    banco.id = Convert.ToInt32(dr["ID"].ToString());
                    banco.nombre = dr["DESCRIPCION"].ToString();
                    banco.fecha = Convert.ToDateTime(dr["FECHA"].ToString());
                    banco.es_activo = Convert.ToBoolean(dr["ESACTIVO"].ToString());
                    listaBancos.Add(banco);
                }
                dr.Close();
            }
            catch (Exception e)
            {
                listaBancos = null;
                throw (e);
            }
            finally
            {
                tran.Commit();
                conexion.Close();
            }
            return listaBancos;
        }
        
        public string nuevoBanco(string nombre, DateTime fecha, string schema)
        {
            string retorno = null;
            List<Banco> listBancos = listaBancos(schema);
            bool existe = validarObjetoExistente(listBancos, nombre);
            if (existe)
            {
                retorno = "El banco ya existe";
                return retorno;
            }
            NpgsqlConnection conexion = null;
            NpgsqlCommand cmd = null;
            try
            {
                conexion = Conexion.getInstance().ConexionDB();
                cmd = new NpgsqlCommand("logueo.spnuevobanco", conexion);
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
            retorno = "Nuevo banco añadido correctamente";
            return retorno;
        }

        public bool updateBanco(int id, string nombre, DateTime fecha, string schema)
        {
            NpgsqlConnection conexion = null;
            NpgsqlCommand cmd = null;
            List<Banco> listBancos = listaBancos(schema);
            bool existe = validarObjetoExistente(listBancos, nombre);
            if (existe)
            {
                return false;
            }
            try
            {
                conexion = Conexion.getInstance().ConexionDB();
                cmd = new NpgsqlCommand("logueo.spupdatebanco", conexion);
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

        public bool eliminarBanco(int id, string nombre, string schema)
        {
            bool retorno = false;
            NpgsqlConnection conexion = null;
            NpgsqlCommand cmd = null;
            try
            {
                conexion = Conexion.getInstance().ConexionDB();
                cmd = new NpgsqlCommand("logueo.speliminarbanco", conexion);
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

        public bool validarObjetoExistente(List<Banco> lista, string nombre)
        {
            bool existe = false;
            foreach (Banco banco in lista)
            {
                if (banco.nombre.ToUpper().Trim() == nombre.ToUpper().Trim())
                {
                    existe = true;
                    break;
                }
            }
            return existe;
        }

    
    }
}
