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
    public class TelefonoDAO
    {
        // getInstance
        #region "PATRON SINGLETON"
        private static TelefonoDAO telefonoDao = null;
        private TelefonoDAO() { }
        public static TelefonoDAO getInstance()
        {
            if (telefonoDao == null)
            {
                telefonoDao = new TelefonoDAO();
            }
            return telefonoDao;
        }
        #endregion

        #region "ABMS"
        public List<Telefono> listaTelefonos(int id_cliente, string schema, NpgsqlConnection conexion)
        {
            List<Telefono> listaTelefonos = new List<Telefono>();
            NpgsqlCommand cmd = null;
            NpgsqlDataReader dr = null;
            try
            {
                cmd = new NpgsqlCommand("logueo.spgettelefonosclientes", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("parm_idcliente", id_cliente);
                cmd.Parameters.AddWithValue("parm_schema", schema);
                //tran = conexion.BeginTransaction();
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Telefono telefono = new Telefono();
                    telefono.id = Convert.ToInt32(dr["ID"].ToString());
                    telefono.id_cliente = Convert.ToInt32(dr["IDCLIENTE"].ToString());
                    telefono.telefono = dr["TELEFONO"].ToString();
                    telefono.descripcion = dr["DESCRIPCION"].ToString();
                    telefono.fecha = Convert.ToDateTime(dr["FECHA"].ToString());
                    telefono.es_activo = Convert.ToBoolean(dr["ESACTIVO"].ToString());
                    listaTelefonos.Add(telefono);
                }
                dr.Close();
            }
            catch (Exception e)
            {
                listaTelefonos = null;
                throw (e);
            }
            finally
            {
            }
            return listaTelefonos;
        }
        
        public bool nuevoTelefono(int id_cliente, string telefono, string descripcion, DateTime fecha, string schema, NpgsqlConnection conexion)
        {
            bool retorno = false;
            List<Telefono> listTelefonos = listaTelefonos(id_cliente, schema, conexion);
            bool existe = validarObjetoExistente(listTelefonos, telefono);
            if (existe)
            {
                return retorno;
            }
            NpgsqlCommand cmd = null;
            try
            {
                cmd = new NpgsqlCommand("logueo.spnuevotelefonocliente", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("parm_idcliente", id_cliente);
                cmd.Parameters.AddWithValue("parm_telefono", telefono);
                cmd.Parameters.AddWithValue("parm_descripcion", descripcion);
                cmd.Parameters.AddWithValue("parm_fecha", fecha);
                cmd.Parameters.AddWithValue("parm_schema", schema);
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                //conexion.Close();
            }
            retorno = true;
            return retorno;
        }

        /*
        public bool updateTelefono(int id, int id_cliente, string telefono, string descripcion, DateTime fecha,
            string schema)
        {
            NpgsqlConnection conexion = null;
            NpgsqlCommand cmd = null;
            List<Telefono> listTelefonos = listaTelefonos(id_cliente, schema);
            bool existe = validarObjetoExistente(listTelefonos, telefono);
            if (existe)
            {
                return false;
            }
            try
            {
                conexion = Conexion.getInstance().ConexionDB();
                cmd = new NpgsqlCommand("logueo.spupdatetelefono", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("parm_idtelefono", id);
                cmd.Parameters.AddWithValue("parm_idcliente", id_cliente);
                cmd.Parameters.AddWithValue("parm_telefono", telefono);
                cmd.Parameters.AddWithValue("parm_descripcion", descripcion);
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
        */
        public bool eliminarTelefono(int id, string schema, NpgsqlConnection conexion)
        {
            bool retorno = false;
            NpgsqlCommand cmd = null;
            try
            {
                cmd = new NpgsqlCommand("logueo.speliminartelefonocliente", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("parm_id", id);
                cmd.Parameters.AddWithValue("parm_schema", schema);
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                return false;
                throw e;
            }
            finally
            {
            }
            retorno = true;
            return retorno;
        }
        #endregion

        public bool validarObjetoExistente(List<Telefono> lista, string numero)
        {
            bool existe = false;
            foreach (Telefono telefono in lista)
            {
                if (telefono.telefono.ToUpper().Trim() == numero.ToUpper().Trim())
                {
                    existe = true;
                    break;
                }
            }
            return existe;
        }

    
    }
}
