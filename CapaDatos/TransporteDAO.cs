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
    public class TransporteDAO
    {
        // getInstance
        #region "PATRON SINGLETON"
        private static TransporteDAO transporteDao = null;
        private TransporteDAO() { }
        public static TransporteDAO getInstance()
        {
            if (transporteDao == null)
            {
                transporteDao = new TransporteDAO();
            }
            return transporteDao;
        }
        #endregion


        #region "ABMS"
        public List<Transporte> listaTransportes(string schema)
        {
            List<Transporte> listaTransportes = new List<Transporte>();
            NpgsqlConnection conexion = null;
            NpgsqlCommand cmd = null;
            NpgsqlTransaction tran = null;
            NpgsqlDataReader dr = null;

            try
            {
                conexion = Conexion.getInstance().ConexionDB();
                cmd = new NpgsqlCommand("logueo.spgettransportes", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("parm_schema", schema);
                conexion.Open();
                tran = conexion.BeginTransaction();
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Transporte transporte = new Transporte();
                    transporte.id = Convert.ToInt32(dr["ID"].ToString());
                    transporte.nombre = dr["DESCRIPCION"].ToString();
                    transporte.fecha = Convert.ToDateTime(dr["FECHA"].ToString());
                    transporte.es_activo = Convert.ToBoolean(dr["ESACTIVO"].ToString());
                    listaTransportes.Add(transporte);
                }
                dr.Close();
            }
            catch (Exception e)
            {
                listaTransportes = null;
                throw (e);
            }
            finally
            {
                tran.Commit();
                conexion.Close();
            }
            return listaTransportes;
        }

        public List<Transporte> listaTransportesCliente(int id_cliente, string schema, NpgsqlConnection conexion)
        {
            List<Transporte> listaTransportes = new List<Transporte>();
            NpgsqlCommand cmd = null;
            NpgsqlDataReader dr = null;

            try
            {
                cmd = new NpgsqlCommand("logueo.spgettransportesclientes", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("parm_idcliente", id_cliente);
                cmd.Parameters.AddWithValue("parm_schema", schema);
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Transporte transporte = new Transporte();
                    transporte.id = Convert.ToInt32(dr["ID"].ToString());
                    transporte.nombre = dr["NOMBRE"].ToString();
                    transporte.descripcion = dr["DESCRIPCION"].ToString();
                    transporte.fecha = Convert.ToDateTime(dr["FECHA"].ToString());
                    transporte.es_activo = Convert.ToBoolean(dr["ESACTIVO"].ToString());
                    listaTransportes.Add(transporte);
                }
                dr.Close();
            }
            catch (Exception e)
            {
                listaTransportes = null;
                throw (e);
            }
            finally
            {
            }
            return listaTransportes;
        }

        public string nuevoTransporte(string nombre, DateTime fecha, string schema)
        {
            string retorno = null;
            List<Transporte> listTransportes = listaTransportes(schema);
            bool existe = validarObjetoExistente(listTransportes, nombre);
            if (existe)
            {
                retorno = "El transporte ya existe";
                return retorno;
            }
            NpgsqlConnection conexion = null;
            NpgsqlCommand cmd = null;
            try
            {
                conexion = Conexion.getInstance().ConexionDB();
                cmd = new NpgsqlCommand("logueo.spnuevotransportecliente", conexion);
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
            retorno = "Nuevo transporte añadido correctamente";
            return retorno;
        }

        public bool nuevoTransporteCliente(int id_cliente, Transporte transporte, string schema, NpgsqlConnection conexion)
        {
            bool retorno = false;
            List<Transporte> listTransportes = listaTransportesCliente(id_cliente, schema, conexion);
            bool existe = validarObjetoExistente(listTransportes, transporte.nombre);
            if (existe)
            {
                retorno = false;
                return retorno;
            }
            //NpgsqlConnection conexion = null;
            NpgsqlCommand cmd = null;
            try
            {
                conexion = Conexion.getInstance().ConexionDB();
                cmd = new NpgsqlCommand("logueo.spnuevotransportecliente", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("parm_idtransporte", transporte.id);
                cmd.Parameters.AddWithValue("parm_idcliente", id_cliente);
                cmd.Parameters.AddWithValue("parm_descripcion", transporte.descripcion);
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

        public bool updateTransporte(int id, string nombre, DateTime fecha, string schema)
        {
            NpgsqlConnection conexion = null;
            NpgsqlCommand cmd = null;
            List<Transporte> listTransportes = listaTransportes(schema);
            bool existe = validarObjetoExistente(listTransportes, nombre);
            if (existe)
            {
                return false;
            }
            try
            {
                conexion = Conexion.getInstance().ConexionDB();
                cmd = new NpgsqlCommand("logueo.spupdatetransporte", conexion);
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

        public bool eliminarTransporte(int id, string schema)
        {
            bool retorno = false;
            NpgsqlConnection conexion = null;
            NpgsqlCommand cmd = null;
            try
            {
                conexion = Conexion.getInstance().ConexionDB();
                cmd = new NpgsqlCommand("logueo.speliminartransporte", conexion);
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

        public bool validarObjetoExistente(List<Transporte> lista, string nombre)
        {
            bool existe = false;
            foreach (Transporte transporte in lista)
            {
                if (transporte.nombre.ToUpper().Trim() == nombre.ToUpper().Trim())
                {
                    existe = true;
                    break;
                }
            }
            return existe;
        }

    }
}
