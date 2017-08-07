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
    public class CategoriasPreciosDAO
    {
        // getInstance
        #region "PATRON SINGLETON"
        private static CategoriasPreciosDAO categoriaPreciosDao = null;
        private CategoriasPreciosDAO() { }
        public static CategoriasPreciosDAO getInstance()
        {
            if (categoriaPreciosDao == null)
            {
                categoriaPreciosDao = new CategoriasPreciosDAO();
            }
            return categoriaPreciosDao;
        }
        #endregion

        #region "ABMS"
        public List<CategoriaPrecios> listaCategoriasPrecios(string schema)
        {
            List<CategoriaPrecios> listaCategoriasPrecios = new List<CategoriaPrecios>();
            NpgsqlConnection conexion = null;
            NpgsqlCommand cmd = null;
            NpgsqlTransaction tran = null;
            NpgsqlDataReader dr = null;

            try
            {
                conexion = Conexion.getInstance().ConexionDB();
                cmd = new NpgsqlCommand("logueo.spgetcategoriasprecios", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("parm_schema", schema);
                conexion.Open();
                tran = conexion.BeginTransaction();
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    CategoriaPrecios categoriaPrecio = new CategoriaPrecios();
                    categoriaPrecio.id = Convert.ToInt32(dr["ID"].ToString());
                    categoriaPrecio.nombre = dr["DESCRIPCION"].ToString();
                    categoriaPrecio.es_activo = Convert.ToBoolean(dr["ESACTIVO"].ToString());
                    listaCategoriasPrecios.Add(categoriaPrecio);
                }
                dr.Close();
            }
            catch (Exception e)
            {
                listaCategoriasPrecios = null;
                throw (e);
            }
            finally
            {
                tran.Commit();
                conexion.Close();
            }
            return listaCategoriasPrecios;
        }

        public List<CategoriaPrecios> listaCategoriasPrecios(string schema, NpgsqlConnection conexion)
        {
            List<CategoriaPrecios> listaCategoriasPrecios = new List<CategoriaPrecios>();
            NpgsqlCommand cmd = null;
            NpgsqlDataReader dr = null;

            try
            {
                cmd = new NpgsqlCommand("logueo.spgetcategoriasprecios", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("parm_schema", schema);
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    CategoriaPrecios categoriaPrecio = new CategoriaPrecios();
                    categoriaPrecio.id = Convert.ToInt32(dr["ID"].ToString());
                    categoriaPrecio.nombre = dr["DESCRIPCION"].ToString();
                    categoriaPrecio.es_activo = Convert.ToBoolean(dr["ESACTIVO"].ToString());
                    listaCategoriasPrecios.Add(categoriaPrecio);
                }
                dr.Close();
            }
            catch (Exception e)
            {
                listaCategoriasPrecios = null;
                throw (e);
            }
            finally
            {
            }
            return listaCategoriasPrecios;
        }

        public string nuevaCategoriaPrecio(string nombre, string schema)
        {
            string retorno = null;
            List<CategoriaPrecios> listCategoriasPrecios = listaCategoriasPrecios(schema);
            bool existe = validarObjetoExistente(listCategoriasPrecios, nombre);
            if (existe)
            {
                retorno = "La categoria de lista de precios ya existe";
                return retorno;
            }
            NpgsqlConnection conexion = null;
            NpgsqlCommand cmd = null;
            try
            {
                conexion = Conexion.getInstance().ConexionDB();
                cmd = new NpgsqlCommand("logueo.spnuevacategoriaprecio", conexion);
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
            retorno = "Nueva categoria lista de precios añadida correctamente";
            return retorno;
        }

        public bool updateCategoriaPrecio(int id, string nombre, string schema)
        {
            NpgsqlConnection conexion = null;
            NpgsqlCommand cmd = null;
            try
            {
                conexion = Conexion.getInstance().ConexionDB();
                cmd = new NpgsqlCommand("logueo.spupdatecategoriaprecio", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("parm_id_color", id);
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

        public bool eliminarCategoriaPrecio(int id, string schema)
        {
            bool retorno = false;
            NpgsqlConnection conexion = null;
            NpgsqlCommand cmd = null;
            try
            {
                conexion = Conexion.getInstance().ConexionDB();
                cmd = new NpgsqlCommand("logueo.speliminarcategoriaprecio", conexion);
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

        public bool validarObjetoExistente(List<CategoriaPrecios> lista, string nombre)
        {
            bool existe = false;
            foreach (CategoriaPrecios categoriaPrecio in lista)
            {
                if (categoriaPrecio.nombre.ToUpper().Trim() == nombre.ToUpper().Trim())
                {
                    existe = true;
                    break;
                }
            }
            return existe;
        }

    
    }
}
