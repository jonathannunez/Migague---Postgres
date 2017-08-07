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
    public class ModeloDAO
    {
        // getInstance
        #region "PATRON SINGLETON"
        private static ModeloDAO modeloDao = null;
        private ModeloDAO() { }
        public static ModeloDAO getInstance()
        {
            if (modeloDao == null)
            {
                modeloDao = new ModeloDAO();
            }
            return modeloDao;
        }
        #endregion

        #region "ABMS"
        public List<Modelo> listaModelos(string schema)
        {
            List<Modelo> listaModelos = new List<Modelo>();
            NpgsqlConnection conexion = null;
            NpgsqlCommand cmd = null;
            NpgsqlTransaction tran = null;
            NpgsqlDataReader dr = null;

            try
            {
                conexion = Conexion.getInstance().ConexionDB();
                cmd = new NpgsqlCommand("logueo.spgetmodelos", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("parm_schema", schema);
                conexion.Open();
                tran = conexion.BeginTransaction();
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Modelo modelo = new Modelo();
                    modelo.id = Convert.ToInt32(dr["ID"].ToString());
                    modelo.codigo = dr["CODIGO"].ToString();
                    modelo.nombre = dr["DESCRIPCION"].ToString();
                    modelo.id_proveedor = Convert.ToInt32(dr["IDPROVEEDOR"].ToString());
                    modelo.id_temporada = Convert.ToInt32(dr["IDTEMPORADA"].ToString());
                    modelo.fecha = Convert.ToDateTime(dr["FECHA"].ToString());
                    modelo.id_tipo_producto = Convert.ToInt32(dr["IDTIPOPRODUCTO"].ToString());
                    modelo.es_activo = Convert.ToBoolean(dr["ESACTIVO"].ToString());
                    listaModelos.Add(modelo);
                }
                dr.Close();
            }
            catch (Exception e)
            {
                listaModelos = null;
                throw (e);
            }
            finally
            {
                tran.Commit();
                conexion.Close();
                // proveedor 
                List<Proveedor> listaProveedores = ProveedorDAO.getInstance().listaProveedores(schema);
                List<Temporada> listaTemporadas = TemporadaDAO.getInstance().listaTemporadas(schema);
                List<TipoProducto> listaTiposProductos = TipoProductoDAO.getInstance().listaTiposProductos(schema);
                foreach(Modelo modelo in listaModelos)
                {
                    foreach(Proveedor proveedor in listaProveedores)
                    {
                        if (modelo.id_proveedor == proveedor.id)
                        {
                            modelo.proveedor = proveedor;
                            break;
                        }
                    }
                    foreach(Temporada temporada in listaTemporadas)
                    {
                        if(modelo.id_temporada == temporada.id)
                        {
                            modelo.temporada = temporada;
                            break;
                        }
                    }
                    foreach(TipoProducto tipoProducto in listaTiposProductos)
                    {
                        if(modelo.id_tipo_producto == tipoProducto.id)
                        {
                            modelo.tipoProducto = tipoProducto;
                        }
                    }                    
                }

                
            }
            return listaModelos;
        }
        
        public string nuevoModelo(string codigo, string nombre, int idproveedor, int idtemporada,
             int idtipoproducto, DateTime fecha, string schema)
        {
            string retorno = null;
            List<Modelo> listModelos = listaModelos(schema);
            bool existe = validarObjetoExistente(listModelos, nombre,codigo);
            if (existe)
            {
                retorno = "El modelo ya existe";
                return retorno;
            }
            NpgsqlConnection conexion = null;
            NpgsqlCommand cmd = null;
            try
            {
                conexion = Conexion.getInstance().ConexionDB();
                cmd = new NpgsqlCommand("logueo.spnuevomodelo", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("parm_codigo", codigo);
                cmd.Parameters.AddWithValue("parm_nombre", nombre);
                cmd.Parameters.AddWithValue("parm_idproveedor", idproveedor);
                cmd.Parameters.AddWithValue("parm_idtemporada", idtemporada);
                cmd.Parameters.AddWithValue("parm_fecha", fecha);
                cmd.Parameters.AddWithValue("parm_idtipoproducto", idtipoproducto);
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
            retorno = "Nuevo modelo añadido correctamente";
            return retorno;
        }

        public bool updateModelo(int id, string codigo, string nombre, int idproveedor, int idtemporada,
             int idtipoproducto, DateTime fecha, string schema)
        {
            NpgsqlConnection conexion = null;
            NpgsqlCommand cmd = null;
            List<Modelo> listModelos = listaModelos(schema);
            bool existe = validarObjetoExistente(listModelos, nombre,codigo);
            if (existe)
            {
                return false;
            }
            try
            {
                conexion = Conexion.getInstance().ConexionDB();
                cmd = new NpgsqlCommand("logueo.spupdatemodelo", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("parm_id", id);
                cmd.Parameters.AddWithValue("parm_codigo", codigo);
                cmd.Parameters.AddWithValue("parm_nombre", nombre);
                cmd.Parameters.AddWithValue("parm_idproveedor", idproveedor);
                cmd.Parameters.AddWithValue("parm_idtemporada", idtemporada);
                cmd.Parameters.AddWithValue("parm_fecha", fecha);
                cmd.Parameters.AddWithValue("parm_idtipoproducto", idtipoproducto);
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

        public bool eliminarModelo(int id, string schema)
        {
            bool retorno = false;
            NpgsqlConnection conexion = null;
            NpgsqlCommand cmd = null;
            try
            {
                conexion = Conexion.getInstance().ConexionDB();
                cmd = new NpgsqlCommand("logueo.speliminarmodelo", conexion);
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

        public bool validarObjetoExistente(List<Modelo> lista, string nombre, string codigo)
        {
            bool existe = false;
            foreach (Modelo modelo in lista)
            {
                if ((modelo.nombre.ToUpper().Trim() == nombre.ToUpper().Trim()) &
                    (modelo.codigo.ToUpper().Trim() == codigo.ToUpper().Trim()))
                {
                    existe = true;
                    break;
                }
            }
            return existe;
        }

    
    }
}
