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
    public class LocalidadDAO
    {
        // getInstance
        #region "PATRON SINGLETON"
        private static LocalidadDAO localidadDao = null;
        private LocalidadDAO() { }
        public static LocalidadDAO getInstance()
        {
            if (localidadDao == null)
            {
                localidadDao = new LocalidadDAO();
            }
            return localidadDao;
        }
        #endregion

        #region "ABMS"
        public List<Localidad> listaLocalidades(string schema)
        {
            List<Localidad> listaLocalidades = new List<Localidad>();
            NpgsqlConnection conexion = null;
            NpgsqlCommand cmd = null;
            NpgsqlTransaction tran = null;
            NpgsqlDataReader dr = null;

            try
            {
                conexion = Conexion.getInstance().ConexionDB();
                cmd = new NpgsqlCommand("logueo.getlocalidades", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
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

                    // provincia a la que pertenece la localidad
                    Provincia provincia = new Provincia();
                    provincia.id = Convert.ToInt32(dr["IDPROVINCIA"].ToString());
                    provincia.nombre = dr["NOMBREPROVINCIA"].ToString();
                    provincia.pais = pais;
                    provincia.fecha = Convert.ToDateTime(dr["FECHAPROVINCIA"].ToString());
                    provincia.es_activo = Convert.ToBoolean(dr["ESACTIVOPROVINCIA"].ToString());

                    // localidad en si misma
                    Localidad localidad = new Localidad();
                    localidad.id = Convert.ToInt32(dr["ID"].ToString()); ;
                    localidad.nombre = dr["DESCRIPCION"].ToString(); ;
                    localidad.provincia = provincia;
                    localidad.fecha = Convert.ToDateTime(dr["FECHA"].ToString()); ;
                    localidad.es_activo = Convert.ToBoolean(dr["ESACTIVO"].ToString());
                    listaLocalidades.Add(localidad);
                }
                dr.Close();
            }
            catch (Exception e)
            {
                listaLocalidades = null;
                throw (e);
            }
            finally
            {
                tran.Commit();
                conexion.Close();
            }
            return listaLocalidades;
        }

        public List<Localidad> listaLocalidades(string schema, NpgsqlConnection conexion)
        {
            List<Localidad> listaLocalidades = new List<Localidad>();
            NpgsqlCommand cmd = null;
            NpgsqlDataReader dr = null;

            try
            {
                cmd = new NpgsqlCommand("logueo.getlocalidades", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("parm_schema", schema);
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    // pais al que pertenece la provincia
                    Pais pais = new Pais();
                    pais.id = Convert.ToInt32(dr["IDPAIS"].ToString());
                    pais.nombre = dr["NOMBREPAIS"].ToString();
                    pais.fecha = Convert.ToDateTime(dr["FECHAPAIS"].ToString());
                    pais.es_activo = Convert.ToBoolean(dr["ESACTIVOPAIS"].ToString());

                    // provincia a la que pertenece la localidad
                    Provincia provincia = new Provincia();
                    provincia.id = Convert.ToInt32(dr["IDPROVINCIA"].ToString());
                    provincia.nombre = dr["NOMBREPROVINCIA"].ToString();
                    provincia.pais = pais;
                    provincia.fecha = Convert.ToDateTime(dr["FECHAPROVINCIA"].ToString());
                    provincia.es_activo = Convert.ToBoolean(dr["ESACTIVOPROVINCIA"].ToString());

                    // localidad en si misma
                    Localidad localidad = new Localidad();
                    localidad.id = Convert.ToInt32(dr["ID"].ToString()); ;
                    localidad.nombre = dr["DESCRIPCION"].ToString(); ;
                    localidad.provincia = provincia;
                    localidad.fecha = Convert.ToDateTime(dr["FECHA"].ToString()); ;
                    localidad.es_activo = Convert.ToBoolean(dr["ESACTIVO"].ToString());
                    listaLocalidades.Add(localidad);
                }
                dr.Close();
            }
            catch (Exception e)
            {
                listaLocalidades = null;
                throw (e);
            }
            finally
            {
            }
            return listaLocalidades;
        }

        public string nuevaLocalidad(int id_provincia, string nombre_provincia, string nombre, DateTime fecha, string schema)
        {
            string retorno = null;
            List<Localidad> listLocalidades = listaLocalidades(schema);
            bool existe = validarObjetoExistente(listLocalidades, nombre, nombre_provincia);
            if (existe)
            {
                retorno = "La localidad ya existe en ese país";
                return retorno;
            }
            NpgsqlConnection conexion = null;
            NpgsqlCommand cmd = null;
            try
            {
                conexion = Conexion.getInstance().ConexionDB();
                cmd = new NpgsqlCommand("logueo.spnuevalocalidad", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("parm_idprovincia", id_provincia);
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

        public bool updateLocalidad(int id, string nombre, DateTime fecha, string schema)
        {
            NpgsqlConnection conexion = null;
            NpgsqlCommand cmd = null;
            try
            {
                conexion = Conexion.getInstance().ConexionDB();
                cmd = new NpgsqlCommand("logueo.spupdatelocalidad", conexion);
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

        public bool eliminarLocalidad(int id, string schema)
        {
            bool retorno = false;
            NpgsqlConnection conexion = null;
            NpgsqlCommand cmd = null;
            try
            {
                conexion = Conexion.getInstance().ConexionDB();
                cmd = new NpgsqlCommand("logueo.speliminarlocalidad", conexion);
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

        public bool validarObjetoExistente(List<Localidad> lista, string nombre, string nombre_pronvicia)
        {
            bool existe = false;
            foreach (Localidad localidad in lista)
            {
                if ((localidad.nombre.ToUpper().Trim() == nombre.ToUpper().Trim()) &
                    (localidad.provincia.nombre.ToUpper().Trim() == nombre_pronvicia.ToUpper().Trim()))
                {
                    existe = true;
                    break;
                }
            }
            return existe;
        }
    
    }
}
