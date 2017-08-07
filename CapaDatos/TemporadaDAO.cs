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
    public class TemporadaDAO
    {
        // getInstance
        #region "PATRON SINGLETON"
        private static TemporadaDAO temporadaDao = null;
        private TemporadaDAO() { }
        public static TemporadaDAO getInstance()
        {
            if (temporadaDao == null)
            {
                temporadaDao = new TemporadaDAO();
            }
            return temporadaDao;
        }
        #endregion

        #region "ABMS"
        public List<Temporada> listaTemporadas(string schema)
        {
            List<Temporada> listaTemporadas = new List<Temporada>();
            NpgsqlConnection conexion = null;
            NpgsqlCommand cmd = null;
            NpgsqlTransaction tran = null;
            NpgsqlDataReader dr = null;

            try
            {
                conexion = Conexion.getInstance().ConexionDB();
                cmd = new NpgsqlCommand("logueo.spgettemporadas", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("parm_schema", schema);
                conexion.Open();
                tran = conexion.BeginTransaction();
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Temporada temporada = new Temporada();
                    temporada.id = Convert.ToInt32(dr["ID"].ToString());
                    temporada.año = Convert.ToInt32(dr["ANO"].ToString());
                    temporada.estacion = dr["ESTACION"].ToString();
                    temporada.nombre = temporada.año.ToString() + temporada.estacion;
                    temporada.es_activo = Convert.ToBoolean(dr["ESACTIVO"].ToString());
                    listaTemporadas.Add(temporada);
                }
                dr.Close();
            }
            catch (Exception e)
            {
                listaTemporadas = null;
                throw (e);
            }
            finally
            {
                tran.Commit();
                conexion.Close();
            }
            return listaTemporadas;
        }
        
        public string nuevaTemporada(string año, string estacion, string schema)
        {
            string retorno = null;
            List<Temporada> listTemporadas = listaTemporadas(schema);
            bool existe = validarObjetoExistente(listTemporadas, año, estacion);
            if (existe)
            {
                retorno = "La temporada ya existe";
                return retorno;
            }
            NpgsqlConnection conexion = null;
            NpgsqlCommand cmd = null;
            try
            {
                conexion = Conexion.getInstance().ConexionDB();
                cmd = new NpgsqlCommand("logueo.spnuevatemporada", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                int año2 = Convert.ToInt32(año.ToString());
                cmd.Parameters.AddWithValue("parm_ano", año2);
                cmd.Parameters.AddWithValue("parm_estacion", estacion);
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
            retorno = "Nueva temporada añadida correctamente";
            return retorno;
        }

        public bool updateTemporada(int id, string año, string estacion, string schema)
        {
            NpgsqlConnection conexion = null;
            NpgsqlCommand cmd = null;
            List<Temporada> listTemporadas = listaTemporadas(schema);
            bool existe = validarObjetoExistente(listTemporadas, año, estacion);
            if (existe)
            { 
                return false;
            }
            try
            {
                conexion = Conexion.getInstance().ConexionDB();
                cmd = new NpgsqlCommand("logueo.spupdatetemporada", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                int ano2 = Convert.ToInt32(año);
                cmd.Parameters.AddWithValue("parm_ano", ano2);
                cmd.Parameters.AddWithValue("parm_temporada", estacion);
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

        public bool eliminarTemporada(int id, string schema)
        {
            bool retorno = false;
            NpgsqlConnection conexion = null;
            NpgsqlCommand cmd = null;
            try
            {
                conexion = Conexion.getInstance().ConexionDB();
                cmd = new NpgsqlCommand("logueo.speliminartemporada", conexion);
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

        public bool validarObjetoExistente(List<Temporada> lista, string año, string estacion)
        {
            bool existe = false;
            foreach (Temporada temporada in lista)
            {
                if ((temporada.año.ToString().ToUpper().Trim() == año.ToUpper().Trim()) &
                    (temporada.estacion.ToUpper().Trim() == estacion.ToUpper().Trim()))
                {
                    existe = true;
                    break;
                }
            }
            return existe;
        }

    
    }
}
