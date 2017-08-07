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
    public class RolDAO
    {
        // getInstance
        #region "PATRON SINGLETON"
        private static RolDAO rolDao = null;
        private RolDAO() { }
        public static RolDAO getInstance()
        {
            if (rolDao == null)
            {
                rolDao = new RolDAO();
            }
            return rolDao;
        }
        #endregion

        #region "ABMS"
        public List<Rol> listaRoles(string schema)
        {
            List<Rol> listaRoles = new List<Rol>();
            NpgsqlConnection conexion = null;
            NpgsqlCommand cmd = null;
            NpgsqlTransaction tran = null;
            NpgsqlDataReader dr = null;

            try
            {
                conexion = Conexion.getInstance().ConexionDB();
                cmd = new NpgsqlCommand("logueo.spgetroles", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("parm_schema", schema);
                conexion.Open();
                tran = conexion.BeginTransaction();
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Rol rol = new Rol();
                    rol.id = Convert.ToInt32(dr["ID"].ToString());
                    rol.nombre = dr["ROL"].ToString();
                    rol.es_activo = Convert.ToBoolean(dr["ESACTIVO"].ToString());
                    listaRoles.Add(rol);
                }
                dr.Close();
            }
            catch (Exception e)
            {
                listaRoles = null;
                throw (e);
            }
            finally
            {
                tran.Commit();
                conexion.Close();
            }
            foreach(Rol rol in listaRoles)
            {
                List<Funcionalidad> listaFuncionalidades = FuncionalidadDAO.getInstance().listaFuncionalidadesRol(rol.id,schema);
                rol.funcionalidades = listaFuncionalidades;
            }
            return listaRoles;
        }
        
        
        public string nuevoRol(string nombre, string schema)
        {
            string retorno = null;
            List<Rol> listRoles = listaRoles(schema);
            bool existe = validarObjetoExistente(listRoles, nombre);
            if (existe)
            {
                retorno = "El rol ya existe";
                return retorno;
            }
            NpgsqlConnection conexion = null;
            NpgsqlCommand cmd = null;
            try
            {
                conexion = Conexion.getInstance().ConexionDB();
                cmd = new NpgsqlCommand("logueo.spnuevorol", conexion);
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
            retorno = "Nuevo rol añadido correctamente";
            return retorno;
        }

        public bool updateRol(int id, string nombre, string schema)
        {
            NpgsqlConnection conexion = null;
            NpgsqlCommand cmd = null;
            List<Rol> listRoles = listaRoles(schema);
            bool existe = validarObjetoExistente(listRoles, nombre);
            if (existe)
            {
                return false;
            }
            try
            {
                conexion = Conexion.getInstance().ConexionDB();
                cmd = new NpgsqlCommand("logueo.spupdaterol", conexion);
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

        public bool eliminarRol(int id, string schema)
        {
            bool retorno = false;
            NpgsqlConnection conexion = null;
            NpgsqlCommand cmd = null;
            try
            {
                conexion = Conexion.getInstance().ConexionDB();
                cmd = new NpgsqlCommand("logueo.speliminarrol", conexion);
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

        public bool validarObjetoExistente(List<Rol> lista, string nombre)
        {
            bool existe = false;
            foreach (Rol rol in lista)
            {
                if (rol.nombre.ToUpper().Trim() == nombre.ToUpper().Trim())
                {
                    existe = true;
                    break;
                }
            }
            return existe;
        }

    
    }
}
