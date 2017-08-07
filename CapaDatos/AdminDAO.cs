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
    public class AdminDAO : LoginDAO
    {
        // getInstance
        #region "PATRON SINGLETON"
        private static AdminDAO daoAdmin = null;
        private AdminDAO() { }
        public static AdminDAO getInstance()
        {
            if(daoAdmin == null)
            {
                daoAdmin = new AdminDAO();
            }
            return daoAdmin;  
        }
        #endregion

        public bool validarObjetoExistente(DataTable lista, string nombre)
        {
            bool existe = false;
            foreach (DataRow row in lista.Rows)
            {
                if ((row["nombre"].ToString().Trim() == nombre))
                {
                    existe = true;
                    break;
                }
            }
            return existe;
        }

        //empresas 
        #region "EMPRESAS"
        public List<Empresa> listaEmpresas()
        {
            List<Empresa> listaEmpresas = new List<Empresa>();
            NpgsqlConnection conexion = null;
            NpgsqlCommand cmd = null;
            NpgsqlTransaction tran = null;
            NpgsqlDataReader dr = null;

            try
            {
                conexion = Conexion.getInstance().ConexionDB();
                cmd = new NpgsqlCommand("logueo.spgetempresas", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                tran = conexion.BeginTransaction();
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Empresa empresa = new Empresa();
                    empresa.id = Convert.ToInt32(dr["ID"].ToString());
                    empresa.nombre = dr["DESCRIPCION"].ToString();
                    empresa.schema = dr["SCHEMA"].ToString();
                    listaEmpresas.Add(empresa);
                }
                dr.Close();
            }
            catch (Exception e)
            {
                listaEmpresas = null;
                throw (e);
            }
            finally
            {
                tran.Commit();
                conexion.Close();
            }
            return listaEmpresas;
        }
        #endregion

        // categorias tributarias
        #region "CATEGORIAS TRIBUTARIAS"
        public List<CategoriaTributaria> listaCategoriasTributarias(string schema)
        {
            NpgsqlConnection conexion = null;
            NpgsqlCommand cmd = null;
            List<CategoriaTributaria> listaCategoriasTributarias = new List<CategoriaTributaria>();
            NpgsqlTransaction tran = null;
            NpgsqlDataReader dr = null;

            try
            {
                conexion = Conexion.getInstance().ConexionDB();
                cmd = new NpgsqlCommand("logueo.spgetcategoriastributarias", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("parm_schema", schema);
                conexion.Open();
                tran = conexion.BeginTransaction();
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    CategoriaTributaria categoriaTributaria = new CategoriaTributaria();
                    categoriaTributaria.id = Convert.ToInt32(dr["ID"].ToString());
                    categoriaTributaria.nombre = dr["DESCRIPCION"].ToString();
                    categoriaTributaria.fecha = Convert.ToDateTime(dr["FECHA"].ToString());
                    categoriaTributaria.es_activo = Convert.ToBoolean(dr["ESACTIVO"].ToString());
                    listaCategoriasTributarias.Add(categoriaTributaria);
                }
                dr.Close();
            }
            catch (Exception e)
            {
                listaCategoriasTributarias = null;
                throw (e);
            }
            finally
            {
                tran.Commit();
                conexion.Close();
            }
            return listaCategoriasTributarias;
        }

        public List<CategoriaTributaria> listaCategoriasTributarias(string schema, NpgsqlConnection conexion)
        {
            NpgsqlCommand cmd = null;
            List<CategoriaTributaria> listaCategoriasTributarias = new List<CategoriaTributaria>();
            NpgsqlDataReader dr = null;

            try
            {
                cmd = new NpgsqlCommand("logueo.spgetcategoriastributarias", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("parm_schema", schema);
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    CategoriaTributaria categoriaTributaria = new CategoriaTributaria();
                    categoriaTributaria.id = Convert.ToInt32(dr["ID"].ToString());
                    categoriaTributaria.nombre = dr["DESCRIPCION"].ToString();
                    categoriaTributaria.fecha = Convert.ToDateTime(dr["FECHA"].ToString());
                    categoriaTributaria.es_activo = Convert.ToBoolean(dr["ESACTIVO"].ToString());
                    listaCategoriasTributarias.Add(categoriaTributaria);
                }
                dr.Close();
            }
            catch (Exception e)
            {
                listaCategoriasTributarias = null;
                throw (e);
            }
            finally
            {
            }
            return listaCategoriasTributarias;
        }

        #endregion

        // formas de pago
        #region "FORMAS DE PAGO"
        public List<FormaPago> listaFormasPago()
        {
            NpgsqlConnection conexion = null;
            NpgsqlCommand cmd = null;
            NpgsqlTransaction tran = null;
            NpgsqlDataReader dr = null;
            List<FormaPago> listaFormasPago = new List<FormaPago>();

            try
            {
                conexion = Conexion.getInstance().ConexionDB();
                cmd = new NpgsqlCommand("logueo.spgetformasdepago", conexion);
                cmd.CommandType = CommandType.StoredProcedure;;
                conexion.Open();
                tran = conexion.BeginTransaction();
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    FormaPago formaPago = new FormaPago();
                    formaPago.id = Convert.ToInt32(dr["ID"].ToString());
                    formaPago.nombre = dr["DESCRIPCION"].ToString();
                    formaPago.fecha = Convert.ToDateTime(dr["FECHA"].ToString());
                    formaPago.es_activo = Convert.ToBoolean(dr["ESACTIVO"].ToString());
                    listaFormasPago.Add(formaPago);
                }
                dr.Close();
            }
            catch (Exception e)
            {
                listaFormasPago = null;
                throw (e);
            }
            finally
            {
                tran.Commit();
                conexion.Close();
            }
            return listaFormasPago;
        }
        #endregion

        // funcxrol
        #region "FUNCXROL"
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

    }
}
