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
    public class FuncionalidadDAO
    {
        // getInstance
        #region "PATRON SINGLETON"
        private static FuncionalidadDAO funcionalidadDao = null;
        private FuncionalidadDAO() { }
        public static FuncionalidadDAO getInstance()
        {
            if (funcionalidadDao == null)
            {
                funcionalidadDao = new FuncionalidadDAO();
            }
            return funcionalidadDao;
        }
        #endregion

        #region "ABMS"
        public List<Funcionalidad> listaFuncionalidades(string schema)
        {
            List<Funcionalidad> listaFuncionalidades = new List<Funcionalidad>();
            NpgsqlConnection conexion = null;
            NpgsqlCommand cmd = null;
            NpgsqlTransaction tran = null;
            NpgsqlDataReader dr = null;

            try
            {
                conexion = Conexion.getInstance().ConexionDB();
                cmd = new NpgsqlCommand("logueo.spgetallfuncionalidades", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("parm_schema", schema);
                conexion.Open();
                tran = conexion.BeginTransaction();
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Funcionalidad funcionalidad = new Funcionalidad();
                    funcionalidad.id = Convert.ToInt32(dr["ID"].ToString());
                    funcionalidad.nombre = dr["DESCRIPCION"].ToString();
                    listaFuncionalidades.Add(funcionalidad);
                }
                dr.Close();
            }
            catch (Exception e)
            {
                listaFuncionalidades = null;
                throw (e);
            }
            finally
            {
                tran.Commit();
                conexion.Close();
            }
            return listaFuncionalidades;
        }

        public List<Funcionalidad> listaFuncionalidadesRol(int id_rol, string schema)
        {
            List<Funcionalidad> listaFuncionalidades = new List<Funcionalidad>();
            NpgsqlConnection conexion = null;
            NpgsqlCommand cmd = null;
            NpgsqlTransaction tran = null;
            NpgsqlDataReader dr = null;

            try
            {
                conexion = Conexion.getInstance().ConexionDB();
                cmd = new NpgsqlCommand("logueo.spgetfuncionalidadesrol", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("parm_idrol", id_rol);
                cmd.Parameters.AddWithValue("parm_schema", schema);
                conexion.Open();
                tran = conexion.BeginTransaction();
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Funcionalidad funcionalidad = new Funcionalidad();
                    funcionalidad.id = Convert.ToInt32(dr["ID"].ToString());
                    funcionalidad.nombre = dr["DESCRIPCION"].ToString();
                    listaFuncionalidades.Add(funcionalidad);
                }
                dr.Close();
            }
            catch (Exception e)
            {
                listaFuncionalidades = null;
                throw (e);
            }
            finally
            {
                tran.Commit();
                conexion.Close();
            }
            return listaFuncionalidades;
        }

        public void updatefuncxrol(int idrol, int id_funcionalidad, string schema)
        {
            NpgsqlConnection conexion = null;
            NpgsqlCommand cmd = null;
            try
            {
                conexion = Conexion.getInstance().ConexionDB();
                cmd = new NpgsqlCommand("logueo.spupdatefuncxrol", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("parm_id_rol", idrol);
                cmd.Parameters.AddWithValue("parm_id_funcionalidad", id_funcionalidad);
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
        }
        public void deletefuncxrol(int idrol, int id_funcionalidad, string schema)
        {
            NpgsqlConnection conexion = null;
            NpgsqlCommand cmd = null;
            try
            {
                conexion = Conexion.getInstance().ConexionDB();
                cmd = new NpgsqlCommand("logueo.spdeletefuncxrol", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("parm_id_rol", idrol);
                cmd.Parameters.AddWithValue("parm_id_funcionalidad", id_funcionalidad);
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
        }
        #endregion

        public bool validarObjetoExistente(List<Color> lista, string nombre)
        {
            bool existe = false;
            foreach (Color color in lista)
            {
                if (color.nombre.ToUpper().Trim() == nombre.ToUpper().Trim())
                {
                    existe = true;
                    break;
                }
            }
            return existe;
        }

    
    }
}
