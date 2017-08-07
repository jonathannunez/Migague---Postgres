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
    public class TipoProductoDAO
    {
        // getInstance
        #region "PATRON SINGLETON"
        private static TipoProductoDAO tipoProductoDao = null;
        private TipoProductoDAO() { }
        public static TipoProductoDAO getInstance()
        {
            if (tipoProductoDao == null)
            {
                tipoProductoDao = new TipoProductoDAO();
            }
            return tipoProductoDao;
        }
        #endregion

        // colores
        #region "ABMS"
        public List<TipoProducto> listaTiposProductos(string schema)
        {
            List<TipoProducto> listaTiposProductos = new List<TipoProducto>();
            NpgsqlConnection conexion = null;
            NpgsqlCommand cmd = null;
            NpgsqlTransaction tran = null;
            NpgsqlDataReader dr = null;

            try
            {
                conexion = Conexion.getInstance().ConexionDB();
                cmd = new NpgsqlCommand("logueo.spgettiposproductos", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("parm_schema", schema);
                conexion.Open();
                tran = conexion.BeginTransaction();
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    TipoProducto tipoProducto = new TipoProducto();
                    tipoProducto.id = Convert.ToInt32(dr["ID"].ToString());
                    tipoProducto.nombre = dr["DESCRIPCION"].ToString();
                    tipoProducto.es_activo = Convert.ToBoolean(dr["ESACTIVO"].ToString());
                    listaTiposProductos.Add(tipoProducto);
                }
                dr.Close();
            }
            catch (Exception e)
            {
                listaTiposProductos = null;
                throw (e);
            }
            finally
            {
                tran.Commit();
                conexion.Close();
            }
            return listaTiposProductos;
        }
        
        public string nuevoTipoProducto(string nombre, string schema)
        {
            string retorno = null;
            List<TipoProducto> listTiposProductos = listaTiposProductos(schema);
            bool existe = validarObjetoExistente(listTiposProductos, nombre);
            if (existe)
            {
                retorno = "El tipo de producto ya existe";
                return retorno;
            }
            NpgsqlConnection conexion = null;
            NpgsqlCommand cmd = null;
            try
            {
                conexion = Conexion.getInstance().ConexionDB();
                cmd = new NpgsqlCommand("logueo.spnuevotipoproducto", conexion);
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
            retorno = "Nuevo tipo de producto añadido correctamente";
            return retorno;
        }

        public bool updateTipoProducto(int id, string nombre, string schema)
        {
            NpgsqlConnection conexion = null;
            NpgsqlCommand cmd = null;
            List<TipoProducto> listTiposProductos = listaTiposProductos(schema);
            bool existe = validarObjetoExistente(listTiposProductos, nombre);
            if (existe)
            {
                return false;
            }
            try
            {
                conexion = Conexion.getInstance().ConexionDB();
                cmd = new NpgsqlCommand("logueo.spupdatetipoproducto", conexion);
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

        public bool eliminarTipoProducto(int id, string schema)
        {
            bool retorno = false;
            NpgsqlConnection conexion = null;
            NpgsqlCommand cmd = null;
            try
            {
                conexion = Conexion.getInstance().ConexionDB();
                cmd = new NpgsqlCommand("logueo.speliminartipoproducto", conexion);
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

        public bool validarObjetoExistente(List<TipoProducto> lista, string nombre)
        {
            bool existe = false;
            foreach (TipoProducto tipoProducto in lista)
            {
                if (tipoProducto.nombre.ToUpper().Trim() == nombre.ToUpper().Trim())
                {
                    existe = true;
                    break;
                }
            }
            return existe;
        }

    
    }
}
