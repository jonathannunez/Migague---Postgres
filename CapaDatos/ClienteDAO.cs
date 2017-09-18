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
    public class ClienteDAO
    {
        // getInstance
        #region "PATRON SINGLETON"
        private static ClienteDAO clienteDao = null;
        private ClienteDAO() { }
        public static ClienteDAO getInstance()
        {
            if (clienteDao == null)
            {
                clienteDao = new ClienteDAO();
            }
            return clienteDao;
        }
        #endregion

        #region "ABMS"
        public List<Cliente> listaClientes(string schema)
        {
            DataTable dt = new DataTable();
            List<Cliente> listaClientes = new List<Cliente>();
            NpgsqlConnection conexion = null;
            NpgsqlCommand cmd = null;
            NpgsqlTransaction tran = null;
            NpgsqlDataReader dr = null;

            try
            {
                conexion = Conexion.getInstance().ConexionDB();
                cmd = new NpgsqlCommand("logueo.spgetclientes", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("parm_schema", schema);
                conexion.Open();
                tran = conexion.BeginTransaction();
                dr = cmd.ExecuteReader();
                if(dr != null)
                {
                    dt.Load(dr);
                    dr.Close();
                }
                foreach(DataRow row in dt.Rows)
                {
                    Cliente cliente = new Cliente();
                    cliente.id = Convert.ToInt32(row.ItemArray[0]);
                    cliente.razon_social = row.ItemArray[1].ToString();
                    cliente.nombre = row.ItemArray[2].ToString();
                    cliente.cuit = row.ItemArray[3].ToString();
                    cliente.fecha_ingreso = Convert.ToDateTime(row.ItemArray[4].ToString());
                    cliente.email = row.ItemArray[5].ToString();
                    cliente.id_categoriaTributaria = Convert.ToInt32(row.ItemArray[6].ToString());
                    cliente.id_categoriaPrecios = Convert.ToInt32(row.ItemArray[7].ToString());
                    cliente.es_activo = Convert.ToBoolean(row.ItemArray[8].ToString());

                    List<Telefono> listaTelefonos = TelefonoDAO.getInstance().listaTelefonos(cliente.id, schema, conexion);
                    cliente.telefonos = listaTelefonos;

                    List<Transporte> listaTransportes = TransporteDAO.getInstance().listaTransportesCliente(cliente.id, schema, conexion);
                    cliente.transportes = listaTransportes;

                    List<Direccion> listaDirecciones = DireccionDAO.getInstance().listaDireccionesCliente(cliente.id, schema, conexion);
                    cliente.direcciones = listaDirecciones;

                    List<CategoriaTributaria> listaCategoriasTributarias = AdminDAO.getInstance().listaCategoriasTributarias(schema, conexion);
                    foreach (CategoriaTributaria categoriaTributaria in listaCategoriasTributarias)
                    {
                        if (cliente.id_categoriaTributaria == categoriaTributaria.id)
                        {
                            cliente.categoriaTributaria = categoriaTributaria;
                            break;
                        }
                    }


                    List<CategoriaPrecios> listaCategoriasPrecios = CategoriasPreciosDAO.getInstance().listaCategoriasPrecios(schema, conexion);
                    foreach (CategoriaPrecios categoriaPrecios in listaCategoriasPrecios)
                    {
                        if (cliente.id_categoriaPrecios == categoriaPrecios.id)
                        {
                            cliente.categoriaPrecios = categoriaPrecios;
                            break;
                        }
                    }

                    listaClientes.Add(cliente);
                }
                dr.Close();
            }
            catch (Exception e)
            {
                listaClientes = null;
                tran.Rollback();
                conexion.Close();
                throw (e);
            }
            finally
            {
                
            }
            tran.Commit();
            conexion.Close();
            return listaClientes;
        }

        public string nuevoCliente(Cliente cliente, string schema)
        {
            string retorno = null;
            NpgsqlTransaction tran = null;
            List<Cliente> list = listaClientes(schema);
            bool existe = validarObjetoExistente(list, cliente.nombre, cliente.cuit);
            if (existe)
            {
                retorno = "El cliente ya existe";
                return retorno;
            }

            NpgsqlConnection conexion = null;
            NpgsqlCommand cmd = null;
            try
            {
                conexion = Conexion.getInstance().ConexionDB();
                cmd = new NpgsqlCommand("logueo.spnuevocliente", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("parm_razonsocial", cliente.razon_social);
                cmd.Parameters.AddWithValue("parm_nombre", cliente.nombre);
                cmd.Parameters.AddWithValue("parm_cuit", cliente.cuit);
                cmd.Parameters.AddWithValue("parm_fecha", cliente.fecha_ingreso);
                cmd.Parameters.AddWithValue("parm_email", cliente.email);
                cmd.Parameters.AddWithValue("parm_categoriatributaria", cliente.id_categoriaTributaria);
                cmd.Parameters.AddWithValue("parm_catlistaprecio", cliente.id_categoriaPrecios);
                cmd.Parameters.AddWithValue("parm_schema", schema);
                conexion.Open();
                tran = conexion.BeginTransaction();
                Object id_cliente = cmd.ExecuteScalar();
                cliente.id =  Convert.ToInt32(id_cliente);
                foreach (Telefono telefono in cliente.telefonos)
                {
                    bool retTel = false;
                    retTel = TelefonoDAO.getInstance().nuevoTelefono(cliente.id, telefono.telefono, telefono.descripcion,
                        cliente.fecha_ingreso, schema, conexion);
                    if (!retTel)
                    {
                        retorno = "El teléfono " + telefono.telefono + " ya existe para este cliente";
                        return retorno;
                    }
                }

                foreach(Transporte transporte in cliente.transportes)
                {
                    bool retTrans = false;
                    retTrans = TransporteDAO.getInstance().nuevoTransporteCliente(cliente.id, transporte, schema, conexion);
                    if (!retTrans)
                    {
                        retorno = "El transporte " + transporte.nombre + " ya existe para este cliente";
                        return retorno;
                    }
                }

                foreach(Direccion direccion in cliente.direcciones)
                {
                    bool retDir = false;
                    retDir = DireccionDAO.getInstance().nuevaDireccion(cliente.id, direccion, schema, conexion);
                    if (!retDir)
                    {
                        retorno = "La direccion " + direccion.calle + " " + direccion.altura + " ya existe para este cliente";
                        return retorno;
                    }
                }

                
            }
            catch (Exception e)
            {
                tran.Rollback();
                throw e;
            }
            finally
            {
                tran.Commit();
                conexion.Close();
            }
            retorno = "Nuevo cliente añadido correctamente";
            return retorno;
        }

        public bool updateCliente(int id, string razonsocial, string nombre, string cuit, DateTime fecha, string email,
            int cattributaria, int catlistaprecio, string schema)
        {
            NpgsqlConnection conexion = null;
            NpgsqlCommand cmd = null;
            List<Cliente> list = listaClientes(schema);
            bool existe = validarObjetoExistente(list, nombre, cuit);
            if (existe)
            {
                return false;
            }
            try
            {
                conexion = Conexion.getInstance().ConexionDB();
                cmd = new NpgsqlCommand("logueo.spupdatecliente", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("parm_id", id);
                cmd.Parameters.AddWithValue("parm_razonsocial", razonsocial);
                cmd.Parameters.AddWithValue("parm_nombre", nombre);
                cmd.Parameters.AddWithValue("parm_cuit", cuit);
                cmd.Parameters.AddWithValue("parm_fecha", fecha);
                cmd.Parameters.AddWithValue("parm_email", email);
                cmd.Parameters.AddWithValue("parm_categoriatributaria", cattributaria);
                cmd.Parameters.AddWithValue("parm_catlistaprecio", catlistaprecio);
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

        public bool eliminarCliente(Cliente cliente, string schema)
        {
            bool retorno = false;
            NpgsqlConnection conexion = null;
            NpgsqlCommand cmd = null;
            NpgsqlTransaction tran = null;
            try
            {
                conexion = Conexion.getInstance().ConexionDB();
                cmd = new NpgsqlCommand("logueo.speliminarcliente", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("parm_id", cliente.id);
                cmd.Parameters.AddWithValue("parm_schema", schema);
                conexion.Open();
                tran = conexion.BeginTransaction();
                cmd.ExecuteNonQuery();

                foreach (Telefono telefono in cliente.telefonos)
                {
                    bool retTel = false;
                    retTel = TelefonoDAO.getInstance().eliminarTelefono(telefono.id, schema, conexion);
                    if (!retTel)
                    {
                        retorno = false;
                        return retorno;
                    }
                }

                foreach (Transporte transporte in cliente.transportes)
                {
                    bool retTel = false;
                    retTel = TransporteDAO.getInstance().eliminarTransporteCliente(transporte.id, cliente.id,schema, conexion);
                    if (!retTel)
                    {
                        retorno = false;
                        return retorno;
                    }
                }

                foreach (Direccion direccion in cliente.direcciones)
                {
                    bool retTel = false;
                    retTel = DireccionDAO.getInstance().eliminarDireccion(direccion.id, schema, conexion);
                    if (!retTel)
                    {
                        retorno = false;
                        return retorno;
                    }
                }


            }
            catch (Exception e)
            {
                tran.Rollback();
                throw e;

            }
            finally
            {
                
            }
            tran.Commit();
            conexion.Close();
            retorno = true;
            return retorno;
        }

        #endregion

        public CategoriaPrecios getCategoriaCliente(Cliente cliente,string schema)
        {
            CategoriaPrecios categoria = new CategoriaPrecios();
            NpgsqlConnection conexion = null;
            NpgsqlCommand cmd = null;
            NpgsqlTransaction tran = null;
            NpgsqlDataReader dr = null;

            try
            {
                conexion = Conexion.getInstance().ConexionDB();
                cmd = new NpgsqlCommand("logueo.spgetcategoriacliente", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("parm_schema", schema);
                cmd.Parameters.AddWithValue("parm_idcliente", cliente.id);
                conexion.Open();
                tran = conexion.BeginTransaction();
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    categoria.id = Convert.ToInt32(dr["ID"].ToString());
                    categoria.nombre = dr["DESCRIPCION"].ToString();
                    categoria.es_activo = Convert.ToBoolean(dr["ESACTIVO"].ToString());
                }
                dr.Close();
            }
            catch (Exception e)
            {
                categoria = null;
                tran.Rollback();
                conexion.Close();
                return categoria;
            }
            tran.Commit();
            conexion.Close();
            return categoria;
        }

        public bool validarObjetoExistente(List<Cliente> lista, string nombre, string cuit)
        {
            bool existe = false;
            foreach (Cliente cliente in lista)
            {
                if ((cliente.nombre.ToUpper().Trim() == nombre.ToUpper().Trim()) &
                    (cliente.cuit.ToUpper().Trim() == cuit.ToUpper().Trim()))
                {
                    existe = true;
                    break;
                }
            }
            return existe;
        }

    
    }
}
