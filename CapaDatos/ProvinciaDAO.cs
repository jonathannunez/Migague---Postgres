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
    public class ProvinciaDAO
    {
        // getInstance
        #region "PATRON SINGLETON"
        private static ProvinciaDAO paisDao = null;
        private ProvinciaDAO() { }
        public static ProvinciaDAO getInstance()
        {
            if (paisDao == null)
            {
                paisDao = new ProvinciaDAO();
            }
            return paisDao;
        }
        #endregion

        #region "ABMS"
        public List<Provincia> listaProvincias(string schema)//int id_pais, string schema)
        {
            List<Provincia> listaProvincias = new List<Provincia>();
            NpgsqlConnection conexion = null;
            NpgsqlCommand cmd = null;
            NpgsqlTransaction tran = null;
            NpgsqlDataReader dr = null;

            try
            {
                conexion = Conexion.getInstance().ConexionDB();
                cmd = new NpgsqlCommand("logueo.getprovincias", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.Parameters.AddWithValue("parm_id_pais", id_pais);
                cmd.Parameters.AddWithValue("parm_schema", schema);
                conexion.Open();
                tran = conexion.BeginTransaction();
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    // pais al que pertenece la provincia
                    Pais pais = new Pais();
                    pais.id = Convert.ToInt32(dr["IDPAIS"].ToString());
                    pais.nombre = dr["NOMBREPAIS"].ToString();
                    pais.fecha = Convert.ToDateTime(dr["FECHAPAIS"].ToString());
                    pais.es_activo = Convert.ToBoolean(dr["ESACTIVOPAIS"].ToString());

                    // provincia
                    Provincia provincia = new Provincia();
                    provincia.id = Convert.ToInt32(dr["ID"].ToString());
                    provincia.nombre = dr["DESCRIPCION"].ToString();
                    provincia.pais = pais;
                    provincia.fecha = Convert.ToDateTime(dr["FECHA"].ToString());
                    provincia.es_activo = Convert.ToBoolean(dr["ESACTIVO"].ToString());
                    listaProvincias.Add(provincia);
                }
                dr.Close();
            }
            catch (Exception e)
            {
                listaProvincias = null;
                throw (e);
            }
            finally
            {
                tran.Commit();
                conexion.Close();
            }
            return listaProvincias;
        }
        
        public string nuevaProvincia(int id_pais, string nombre_pais, string nombre, DateTime fecha, string schema)
        {
            string retorno = null;
            List<Provincia> listProvincias = listaProvincias(schema);
            bool existe = validarObjetoExistente(listProvincias, nombre, nombre_pais);
            if (existe)
            {
                retorno = "La provincia ya existe en ese país";
                return retorno;
            }
            NpgsqlConnection conexion = null;
            NpgsqlCommand cmd = null;
            try
            {
                conexion = Conexion.getInstance().ConexionDB();
                cmd = new NpgsqlCommand("logueo.spnuevaprovincia", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("parm_id_pais", id_pais);
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
            retorno = "Nueva provincia añadida correctamente";
            return retorno;
        }

        public bool updateProvincia(int id, string nombre, DateTime fecha, string schema)
        {
            NpgsqlConnection conexion = null;
            NpgsqlCommand cmd = null;
            try
            {
                conexion = Conexion.getInstance().ConexionDB();
                cmd = new NpgsqlCommand("logueo.spupdateprovincia", conexion);
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

        public bool eliminarProvincia(int id, string schema)
        {
            bool retorno = false;
            NpgsqlConnection conexion = null;
            NpgsqlCommand cmd = null;
            try
            {
                conexion = Conexion.getInstance().ConexionDB();
                cmd = new NpgsqlCommand("logueo.speliminarprovincia", conexion);
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

        public bool validarObjetoExistente(List<Provincia> lista, string nombre, string nombre_pais)
        {
            bool existe = false;
            foreach (Provincia provincia in lista)
            {
                if ((provincia.nombre.ToUpper().Trim() == nombre.ToUpper().Trim()) &
                    (provincia.pais.nombre.ToUpper().Trim() == nombre_pais.ToUpper().Trim()))
                {
                    existe = true;
                    break;
                }
            }
            return existe;
        }
    
    }
}
