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
    public class ProveedorDAO
    {
        // getInstance
        #region "PATRON SINGLETON"
        private static ProveedorDAO proveedorDao = null;
        private ProveedorDAO() { }
        public static ProveedorDAO getInstance()
        {
            if (proveedorDao == null)
            {
                proveedorDao = new ProveedorDAO();
            }
            return proveedorDao;
        }
        #endregion


        #region "ABMS"
        public List<Proveedor> listaProveedores(string schema)
        {
            List<Proveedor> listaProveedores = new List<Proveedor>();
            NpgsqlConnection conexion = null;
            NpgsqlCommand cmd = null;
            NpgsqlTransaction tran = null;
            NpgsqlDataReader dr = null;

            try
            {
                conexion = Conexion.getInstance().ConexionDB();
                cmd = new NpgsqlCommand("logueo.spgetproveedores", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("parm_schema", schema);
                conexion.Open();
                tran = conexion.BeginTransaction();
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Proveedor proveedor = new Proveedor();
                    proveedor.id = Convert.ToInt32(dr["ID"].ToString());
                    proveedor.nombre = dr["DESCRIPCION"].ToString();
                    proveedor.fecha = Convert.ToDateTime(dr["FECHA"].ToString());
                    proveedor.es_activo = Convert.ToBoolean(dr["ESACTIVO"].ToString());
                    listaProveedores.Add(proveedor);
                }
                dr.Close();
            }
            catch (Exception e)
            {
                listaProveedores = null;
                throw (e);
            }
            finally
            {
                tran.Commit();
                conexion.Close();
            }
            return listaProveedores;
        }
        
        public string nuevoProveedor(string nombre, DateTime fecha, string schema)
        {
            string retorno = null;
            List<Proveedor> listProveedores = listaProveedores(schema);
            bool existe = validarObjetoExistente(listProveedores, nombre);
            if (existe)
            {
                retorno = "El proveedor ya existe";
                return retorno;
            }
            NpgsqlConnection conexion = null;
            NpgsqlCommand cmd = null;
            try
            {
                conexion = Conexion.getInstance().ConexionDB();
                cmd = new NpgsqlCommand("logueo.spnuevoproveedor", conexion);
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
            retorno = "Nuevo proveedor añadido correctamente";
            return retorno;
        }

        public bool updateProveedor(int id, string nombre, DateTime fecha, string schema)
        {
            NpgsqlConnection conexion = null;
            NpgsqlCommand cmd = null;
            List<Proveedor> listProveedores = listaProveedores(schema);
            bool existe = validarObjetoExistente(listProveedores, nombre);
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

        public bool eliminarProveedor(int id, string schema)
        {
            bool retorno = false;
            NpgsqlConnection conexion = null;
            NpgsqlCommand cmd = null;
            try
            {
                conexion = Conexion.getInstance().ConexionDB();
                cmd = new NpgsqlCommand("logueo.speliminarproveedor", conexion);
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

        public bool validarObjetoExistente(List<Proveedor> lista, string nombre)
        {
            bool existe = false;
            foreach (Proveedor proveedor in lista)
            {
                if (proveedor.nombre.ToUpper().Trim() == nombre.ToUpper().Trim())
                {
                    existe = true;
                    break;
                }
            }
            return existe;
        }

    
    }
}
