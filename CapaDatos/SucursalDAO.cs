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
    public class SucursalDAO
    {
        // getInstance
        #region "PATRON SINGLETON"
        private static SucursalDAO sucursalDao = null;
        private SucursalDAO() { }
        public static SucursalDAO getInstance()
        {
            if (sucursalDao == null)
            {
                sucursalDao = new SucursalDAO();
            }
            return sucursalDao;
        }
        #endregion

        #region "ABMS"
        public List<Sucursal> listaSucursales(string schema)
        {
            List<Sucursal> listaSucursales = new List<Sucursal>();
            NpgsqlConnection conexion = null;
            NpgsqlCommand cmd = null;
            NpgsqlTransaction tran = null;
            NpgsqlDataReader dr = null;

            try
            {
                conexion = Conexion.getInstance().ConexionDB();
                cmd = new NpgsqlCommand("logueo.spgetsucursales", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("parm_schema", schema);
                conexion.Open();
                tran = conexion.BeginTransaction();
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    
                    Sucursal sucursal = new Sucursal();
                    sucursal.id = Convert.ToInt32(dr["ID"].ToString());
                    sucursal.nombre = dr["NOMBRE"].ToString();
                    sucursal.calle = dr["CALLE"].ToString();
                    sucursal.altura = Convert.ToInt32(dr["ALTURA"].ToString());
                    sucursal.fecha = Convert.ToDateTime(dr["FECHA"].ToString());
                    sucursal.id_localidad = Convert.ToInt32(dr["LOCALIDAD"].ToString());
                    sucursal.es_activo = Convert.ToBoolean(dr["ESACTIVO"].ToString());
                    listaSucursales.Add(sucursal);
                }
                dr.Close();
            }
            catch (Exception e)
            {
                listaSucursales = null;
                throw (e);
            }
            finally
            {
                tran.Commit();
                conexion.Close();
                // localidad
                List<Localidad> listaLocalidades = LocalidadDAO.getInstance().listaLocalidades(schema);
                foreach(Sucursal sucursal in listaSucursales)
                {
                    foreach (Localidad localidad in listaLocalidades)
                    {
                        if (localidad.id == sucursal.id_localidad)
                        {
                            sucursal.localidad = localidad;
                            break;
                        }
                    }
                }
                
            }
            return listaSucursales;
        }
        
        public string nuevaSucursal(string nombre, string calle, int altura, DateTime fecha, int id_localidad, string schema)
        {
            string retorno = null;
            List<Sucursal> listSucursales = listaSucursales(schema);
            bool existe = validarObjetoExistente(listSucursales, nombre);
            if (existe)
            {
                retorno = "La sucursal ya existe";
                return retorno;
            }
            NpgsqlConnection conexion = null;
            NpgsqlCommand cmd = null;
            try
            {
                conexion = Conexion.getInstance().ConexionDB();
                cmd = new NpgsqlCommand("logueo.spnuevasucursal", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("parm_nombre", nombre);
                cmd.Parameters.AddWithValue("parm_calle", calle);
                cmd.Parameters.AddWithValue("parm_altura", altura);
                cmd.Parameters.AddWithValue("parm_fecha", fecha);
                cmd.Parameters.AddWithValue("parm_idlocalidad", id_localidad);
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
            retorno = "Nueva sucursal añadido correctamente";
            return retorno;
        }


        public bool updateSucursal(int id, string nombre, string calle, int altura, DateTime fecha, int id_localidad,
            string schema)
        {
            NpgsqlConnection conexion = null;
            NpgsqlCommand cmd = null;
            List<Sucursal> listSucursales = listaSucursales(schema);
            bool existe = validarObjetoExistente(listSucursales, nombre);
            if (existe)
            {
                return false;
            }
            try
            {
                conexion = Conexion.getInstance().ConexionDB();
                cmd = new NpgsqlCommand("logueo.spupdatesucursal", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("parm_id", id);
                cmd.Parameters.AddWithValue("parm_nombre", nombre);
                cmd.Parameters.AddWithValue("parm_calle", calle);
                cmd.Parameters.AddWithValue("parm_altura", altura);
                cmd.Parameters.AddWithValue("parm_fecha", fecha);
                cmd.Parameters.AddWithValue("parm_idlocalidad", id_localidad);
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

        public bool eliminarSucursal(int id, string schema)
        {
            bool retorno = false;
            NpgsqlConnection conexion = null;
            NpgsqlCommand cmd = null;
            try
            {
                conexion = Conexion.getInstance().ConexionDB();
                cmd = new NpgsqlCommand("logueo.speliminarsucursal", conexion);
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

        public bool validarObjetoExistente(List<Sucursal> lista, string nombre)
        {
            bool existe = false;
            foreach (Sucursal sucursal in lista)
            {
                if (sucursal.nombre.ToUpper().Trim() == nombre.ToUpper().Trim())
                {
                    existe = true;
                    break;
                }
            }
            return existe;
        }

    
    }
}
