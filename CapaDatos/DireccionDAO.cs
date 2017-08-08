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
    public class DireccionDAO
    {
        // getInstance
        #region "PATRON SINGLETON"
        private static DireccionDAO direccionDao = null;
        private DireccionDAO() { }
        public static DireccionDAO getInstance()
        {
            if (direccionDao == null)
            {
                direccionDao = new DireccionDAO();
            }
            return direccionDao;
        }
        #endregion

        #region "ABMS"
        public List<Direccion> listaDireccionesCliente(int id_cliente, string schema, NpgsqlConnection conexion)
        {
            DataTable dt = new DataTable();
            List<Direccion> listaDirecciones = new List<Direccion>();
            NpgsqlCommand cmd = null;
            NpgsqlDataReader dr = null;

            try
            {
                cmd = new NpgsqlCommand("logueo.spgetdireccionescliente", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("parm_idcliente", id_cliente);
                cmd.Parameters.AddWithValue("parm_schema", schema);
                dr = cmd.ExecuteReader();
                if( dr != null)
                {
                    dt.Load(dr);
                    dr.Close();
                }
                foreach(DataRow row in dt.Rows)
                {
                    Direccion direccion = new Direccion();
                    direccion.id = Convert.ToInt32(row.ItemArray[0]);
                    direccion.id_cliente = Convert.ToInt32(row.ItemArray[1].ToString());
                    direccion.calle = row.ItemArray[2].ToString();
                    direccion.altura = Convert.ToInt32(row.ItemArray[3].ToString());
                    direccion.descripcion = row.ItemArray[4].ToString();
                    direccion.id_localidad = Convert.ToInt32(row.ItemArray[5].ToString());
                    direccion.fecha = Convert.ToDateTime(row.ItemArray[6].ToString());
                    direccion.es_activo = Convert.ToBoolean(row.ItemArray[7].ToString());
                    listaDirecciones.Add(direccion);

                    List<Localidad> listaLocalidades = LocalidadDAO.getInstance().listaLocalidades(schema, conexion);
                    foreach (Localidad localidad in listaLocalidades)
                    {
                        if (direccion.id_localidad == localidad.id)
                        {
                            direccion.localidad = localidad;
                            break;
                        }
                    }

                }
                dr.Close();
            }
            catch (Exception e)
            {
                listaDirecciones = null;
                throw (e);
            }
            finally
            {
               
            }
            return listaDirecciones;
        }
        
        public bool nuevaDireccion(int id_cliente, Direccion direccion, string schema, NpgsqlConnection conexion)
        {
            bool retorno = false;
            List<Direccion> listDirecciones = listaDireccionesCliente(id_cliente,schema, conexion);
            bool existe = validarObjetoExistente(listDirecciones, direccion.calle, direccion.altura, id_cliente);
            if (existe)
            {
                return retorno;
            }
            NpgsqlCommand cmd = null;
            try
            {
                cmd = new NpgsqlCommand("logueo.spnuevadireccioncliente", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("parm_idcliente", id_cliente);
                cmd.Parameters.AddWithValue("parm_calle", direccion.calle);
                cmd.Parameters.AddWithValue("parm_altura", direccion.altura);
                cmd.Parameters.AddWithValue("parm_descripcion", direccion.descripcion);
                cmd.Parameters.AddWithValue("parm_idlocalidad", direccion.id_localidad);
                cmd.Parameters.AddWithValue("parm_fecha", direccion.fecha);
                cmd.Parameters.AddWithValue("parm_schema", schema);
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {

            }
            retorno = true;
            return retorno;
        }

        public bool updateDireccion(int id, int id_cliente, string calle, int altura, string descripcion, int id_localidad,
            DateTime fecha, string schema)
        {
            NpgsqlConnection conexion = null;
            NpgsqlCommand cmd = null;
            try
            {
                conexion = Conexion.getInstance().ConexionDB();
                cmd = new NpgsqlCommand("logueo.spupdatedireccion", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("parm_id", id);
                cmd.Parameters.AddWithValue("parm_idcliente", id_cliente);
                cmd.Parameters.AddWithValue("parm_calle", calle);
                cmd.Parameters.AddWithValue("parm_altura", altura);
                cmd.Parameters.AddWithValue("parm_descripcion", descripcion);
                cmd.Parameters.AddWithValue("parm_idlocalidad", id_localidad);
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

        public bool eliminarDireccion(int id, string schema, NpgsqlConnection conexion)
        {
            bool retorno = false;
            NpgsqlCommand cmd = null;
            try
            {
                cmd = new NpgsqlCommand("logueo.speliminardireccion", conexion);
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

        public bool validarObjetoExistente(List<Direccion> lista, string calle, int altura, int id_cliente)
        {
            bool existe = false;
            foreach (Direccion direccion in lista)
            {
                if ((direccion.calle.ToUpper().Trim() == calle.ToUpper().Trim()) &
                    (direccion.altura == altura) &
                    (direccion.id_cliente == id_cliente))
                {
                    existe = true;
                    break;
                }
            }
            return existe;
        }
    
    }
}
