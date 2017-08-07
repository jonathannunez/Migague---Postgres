using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Globalization;
using System.Data;
using CapaEntidades;
using Npgsql;

namespace CapaDatos
{
    public class UsuarioDAO : LoginDAO
    {
        #region "PATRON SINGLETON"
        private static UsuarioDAO daoUsuario = null;

        private UsuarioDAO() { }
        public static UsuarioDAO getInstance()
        {
            if (daoUsuario == null)
            {
                daoUsuario = new UsuarioDAO();
            }
            return daoUsuario;
        }
        #endregion

        #region "ABM"
        public List<Usuario> listaUsuarios(string schema)
        {
            NpgsqlConnection conexion = null;
            NpgsqlCommand cmd = null;
            NpgsqlTransaction tran = null;
            List<Usuario> listaUsuarios = new List<Usuario>();
            List<Usuario> listaFinalUsuarios = new List<Usuario>();
            NpgsqlDataReader dr = null;

            try
            {
                conexion = Conexion.getInstance().ConexionDB();
                cmd = new NpgsqlCommand("logueo.spgetusuarios", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("parm_schema", schema);
                conexion.Open();
                tran = conexion.BeginTransaction();
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Usuario usuario = new Usuario();
                    usuario.id = Convert.ToInt32(dr["ID"].ToString());
                    usuario.nombre = dr["NOMBRE"].ToString();
                    usuario.password = Decrypt(dr["PASSWORD"].ToString());
                    usuario.id_rol = Convert.ToInt32(dr["IDROL"].ToString());
                    usuario.id_empresa = Convert.ToInt32(dr["IDEMPRESA"].ToString());
                    listaUsuarios.Add(usuario);
                }
                dr.Close();
            }
            catch (Exception e)
            {
                listaFinalUsuarios = null;
                throw (e);
            }
            finally
            {
                tran.Commit();
                conexion.Close();
                foreach(Usuario usuario in listaUsuarios)
                {
                    #region "ATRIBUTOS EMPRESA, ROL, FUNCIONALIDADES" // rol usuario
                    // empresa
                    Usuario nuevoUsuario = new Usuario();
                    nuevoUsuario = usuario;
                    nuevoUsuario = getEmpresaUsuarioSchema(nuevoUsuario, schema);

                    nuevoUsuario = getFuncionalidadesRol(nuevoUsuario);
                    listaFinalUsuarios.Add(nuevoUsuario);
                    #endregion
                }
               
            }
            return listaFinalUsuarios;
        }

        public string nuevoUsuario(string nombre, string password, string nombre_rol, string schema)
        {
            string retorno = null;
            List<Usuario> listUsuarios = listaUsuarios(schema);
            bool existe = validarObjetoExistente(listUsuarios, nombre);
            if (existe)
            {
                retorno = "El usuario ya existe";
                return retorno;
            }
            NpgsqlConnection conexion = null;
            NpgsqlCommand cmd = null;
            password = Encrypt(password);

            try
            {
                conexion = Conexion.getInstance().ConexionDB();
                cmd = new NpgsqlCommand("logueo.spNuevoUsuario", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("parm_nombre", nombre);
                cmd.Parameters.AddWithValue("parm_password", password);
                cmd.Parameters.AddWithValue("parm_nombre_rol", nombre_rol);
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
            retorno = "Nuevo usuario añadido correctamente";
            return retorno;
        }

        public bool eliminarUsuario(int id_usuario)
        {
            bool retorno = false;
            NpgsqlConnection conexion = null;
            NpgsqlCommand cmd = null;
            try
            {
                conexion = Conexion.getInstance().ConexionDB();
                cmd = new NpgsqlCommand("logueo.speliminarusuario", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("parm_idusuario", id_usuario);
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

        public bool updateUsuario(int iduser, string nombre, string password, string nombre_rol, string schema)
        {
            NpgsqlConnection conexion = null;
            NpgsqlCommand cmd = null;
            password = Encrypt(password);
            try
            {
                conexion = Conexion.getInstance().ConexionDB();
                cmd = new NpgsqlCommand("logueo.spupdateusuario", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("parm_id", iduser);
                cmd.Parameters.AddWithValue("parm_nombre", nombre);
                cmd.Parameters.AddWithValue("parm_password", password);
                cmd.Parameters.AddWithValue("parm_nombre_rol", nombre_rol);
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
        #endregion

        public Usuario login(String empresa, String user, String password)
        {
            NpgsqlConnection conexion = null;
            NpgsqlCommand cmd = null;
            Usuario usuario = null;
            NpgsqlTransaction tran = null;
            NpgsqlDataReader dr = null;
            password = Encrypt(password);
            try
            {
                conexion = Conexion.getInstance().ConexionDB();
                conexion.Open();
                tran = conexion.BeginTransaction();
                cmd = new NpgsqlCommand("logueo.spaccesosistema", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@empresa", empresa);
                cmd.Parameters.AddWithValue("@nombreuser", user);
                cmd.Parameters.AddWithValue("@passuser", password);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    usuario = new Usuario();
                    usuario.id = Convert.ToInt32(dr["id"].ToString());
                    usuario.nombre = dr["nombre"].ToString();
                    usuario.password = password;
                    usuario.id_rol = Convert.ToInt32(dr["id_rol"].ToString());
                }
                dr.Close();
                if (usuario != null)
                {
                    // sucursales usuario
                    List<Sucursal> listaSucursales = getSucursalesUsuario(usuario, conexion);
                    usuario.sucursales = listaSucursales;
                }
            }
            catch (Exception e)
            {
                usuario = null;
                throw e;
            }
            finally
            {
                tran.Commit();
                conexion.Close();
                #region "ATRIBUTOS EMPRESA, ROL, FUNCIONALIDADES"
                if (usuario != null)
                {


                    // empresa
                    usuario = getEmpresaUsuario(usuario, empresa);

                    // rol usuario
                    usuario = getFuncionalidadesRol(usuario);
                    #endregion
                }
            }
            return usuario;
        }


        public List<Sucursal> getSucursalesUsuario(Usuario user, NpgsqlConnection conexion)
        {
            List<Sucursal> listaSucursales = new List<Sucursal>();
            try
            {
                NpgsqlCommand cmd = new NpgsqlCommand("logueo.spgetsucursalesusuario", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("parm_idusuario", user.id);
                NpgsqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Sucursal sucursal = new Sucursal();
                    sucursal.id = Convert.ToInt32(dr["ID"].ToString());
                    sucursal.nombre = dr["NOMBRE"].ToString();
                    listaSucursales.Add(sucursal);
                }
            }
            catch (Exception e)
            {
                listaSucursales = null;
                throw e;
            }
            return listaSucursales;
        }

        public Usuario getEmpresaUsuario(Usuario usuario, string empresa)
        {
            List<Empresa> listaEmpresas = AdminDAO.getInstance().listaEmpresas();
            foreach (Empresa emp in listaEmpresas)
            {
                if (emp.nombre == empresa)
                {
                    usuario.empresa = emp;
                    break;
                }
            }
            return usuario;

        }

        public Usuario getEmpresaUsuarioSchema(Usuario usuario, string schema)
        {
            List<Empresa> listaEmpresas = AdminDAO.getInstance().listaEmpresas();
            foreach (Empresa emp in listaEmpresas)
            {
                if (emp.schema == schema)
                {
                    usuario.empresa = emp;
                    break;
                }
            }
            return usuario;

        }

        public Usuario getFuncionalidadesRol(Usuario usuario)
        {
            List<Rol> listaRoles = RolDAO.getInstance().listaRoles(usuario.empresa.schema);
            foreach (Rol rol in listaRoles)
            {
                if (rol.id == usuario.id_rol)
                {
                    usuario.rol = rol;
                    break;
                }
            }
            return usuario;
        }

        public DataTable getMenuItems(int id_rol)
        {
            NpgsqlConnection conexion = null;
            NpgsqlCommand cmd = null;
            DataTable dtMenuItems = new DataTable();
            NpgsqlTransaction tran = null;
            NpgsqlDataAdapter sda = null;

            try
            {
                conexion = Conexion.getInstance().ConexionDB();
                conexion.Open();
                tran = conexion.BeginTransaction();
                cmd = new NpgsqlCommand("logueo.spgetmenuitem", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("parm_idrol", id_rol);
                sda = new NpgsqlDataAdapter(cmd);
                sda.Fill(dtMenuItems);
                cmd.Dispose();
                sda.Dispose();
            }
            catch (Exception e)
            {
                dtMenuItems = null;
                throw e;
            }
            finally
            {
                tran.Commit();
                conexion.Close();
            }
            return dtMenuItems;
        }

        public Usuario setSucursalPorUsuario(Usuario user, string schema)
        {
            List<Sucursal> listaSucursales = new List<Sucursal>();
            NpgsqlConnection conexion = null;
            NpgsqlCommand cmd = null;
            NpgsqlTransaction tran = null;
            NpgsqlDataReader dr = null;

            try
            {
                conexion = Conexion.getInstance().ConexionDB();
                cmd = new NpgsqlCommand("logueo.spgetsucursalesusuario", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("parm_idusuario", user.id);
                conexion.Open();
                tran = conexion.BeginTransaction();
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Sucursal sucursal = new Sucursal();
                    sucursal.id = Convert.ToInt32(dr["ID"].ToString());
                    sucursal.nombre = dr["NOMBRE"].ToString();
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
                foreach (Sucursal sucursal in listaSucursales)
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
                user.sucursales = listaSucursales;

            }
            return user;
        }

       

        public bool updateSucursalUsuario(int id_usuario, int id_sucursal, string schema)
        {
            NpgsqlConnection conexion = null;
            NpgsqlCommand cmd = null;
            try
            {
                conexion = Conexion.getInstance().ConexionDB();
                cmd = new NpgsqlCommand("logueo.spupdatesucxuser", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("parm_id", id_usuario);
                cmd.Parameters.AddWithValue("parm_id_sucursal", id_sucursal);
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

        public bool eliminarSucursalUsuario(int id_usuario, int id_sucursal, string schema)
        {
            bool retorno = false;
            NpgsqlConnection conexion = null;
            NpgsqlCommand cmd = null;
            try
            {
                conexion = Conexion.getInstance().ConexionDB();
                cmd = new NpgsqlCommand("logueo.spdeletesucxuser", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("parm_id_usuario", id_usuario);
                cmd.Parameters.AddWithValue("parm_id_sucursal", id_sucursal);
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

        public bool validarObjetoExistente(List<Usuario> lista, string nombre)
        {
            bool existe = false;
            foreach (Usuario usuario in lista)
            {
                if ((usuario.nombre.ToUpper().Trim() == nombre.ToUpper().Trim()))
                {
                    existe = true;
                    break;
                }
            }
            return existe;
        }
    }
}
