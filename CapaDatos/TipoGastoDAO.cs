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
    public class TipoGastoDAO
    {
        // getInstance
        #region "PATRON SINGLETON"
        private static TipoGastoDAO tipoGastoDao = null;
        private TipoGastoDAO() { }
        public static TipoGastoDAO getInstance()
        {
            if (tipoGastoDao == null)
            {
                tipoGastoDao = new TipoGastoDAO();
            }
            return tipoGastoDao;
        }
        #endregion

        #region "ABMS"
        public List<TipoGasto> listaTiposGastos(string schema)
        {
            List<TipoGasto> listaTiposGastos = new List<TipoGasto>();
            NpgsqlConnection conexion = null;
            NpgsqlCommand cmd = null;
            NpgsqlTransaction tran = null;
            NpgsqlDataReader dr = null;

            try
            {
                conexion = Conexion.getInstance().ConexionDB();
                cmd = new NpgsqlCommand("logueo.spgettiposgastos", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("parm_schema", schema);
                conexion.Open();
                tran = conexion.BeginTransaction();
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    TipoGasto tipoGasto = new TipoGasto();
                    tipoGasto.id = Convert.ToInt32(dr["ID"].ToString());
                    tipoGasto.nombre = dr["DESCRIPCION"].ToString();
                    tipoGasto.es_activo = Convert.ToBoolean(dr["ESACTIVO"].ToString());
                    listaTiposGastos.Add(tipoGasto);
                }
                dr.Close();
            }
            catch (Exception e)
            {
                listaTiposGastos = null;
                throw (e);
            }
            finally
            {
                tran.Commit();
                conexion.Close();
            }
            return listaTiposGastos;
        }
        
        public string nuevoTipoGasto(string nombre, string schema)
        {
            string retorno = null;
            List<TipoGasto> listTiposGastos = listaTiposGastos(schema);
            bool existe = validarObjetoExistente(listTiposGastos, nombre);
            if (existe)
            {
                retorno = "El tipo de gasto ya existe";
                return retorno;
            }
            NpgsqlConnection conexion = null;
            NpgsqlCommand cmd = null;
            try
            {
                conexion = Conexion.getInstance().ConexionDB();
                cmd = new NpgsqlCommand("logueo.spnuevotipogasto", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
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
            retorno = "Nuevo tipo de gasto añadido correctamente";
            return retorno;
        }

        public bool updateTipoGasto(int id, string nombre, string schema)
        {
            NpgsqlConnection conexion = null;
            NpgsqlCommand cmd = null;
            List<TipoGasto> listTiposGastos = listaTiposGastos(schema);
            bool existe = validarObjetoExistente(listTiposGastos, nombre);
            if (existe)
            {
                return false;
            }
            try
            {
                conexion = Conexion.getInstance().ConexionDB();
                cmd = new NpgsqlCommand("logueo.spupdatetipogasto", conexion);
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
            return true;
        }

        public bool eliminarTipoGasto(int id, string schema)
        {
            bool retorno = false;
            NpgsqlConnection conexion = null;
            NpgsqlCommand cmd = null;
            try
            {
                conexion = Conexion.getInstance().ConexionDB();
                cmd = new NpgsqlCommand("logueo.speliminartipogasto", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("parm_id", id);
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

        public bool validarObjetoExistente(List<TipoGasto> lista, string nombre)
        {
            bool existe = false;
            foreach (TipoGasto tipoGasto in lista)
            {
                if (tipoGasto.nombre.ToUpper().Trim() == nombre.ToUpper().Trim())
                {
                    existe = true;
                    break;
                }
            }
            return existe;
        }

    
    }
}
