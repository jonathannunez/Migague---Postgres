using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaEntidades;
using CapaDatos;
using System.Data;
using Npgsql;

namespace CapaLogicaNegocio
{
    public class UsuarioLN
    {
        #region "PATRON SINGLETON"
        private static UsuarioLN usuario = null;
        private UsuarioLN() { }
        public static UsuarioLN getInstance()
        {
            if(usuario == null)
            {
                usuario = new UsuarioLN();
            }
            return usuario;
        }
        #endregion

        #region "ABM"
        public List<Usuario> listaUsuarios(string schema)
        {
            try
            {
                return UsuarioDAO.getInstance().listaUsuarios(schema);
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public string nuevoUsuario(string nombre, string password, string nombre_rol, string schema)
        {
            try
            {
                return UsuarioDAO.getInstance().nuevoUsuario(nombre, password, nombre_rol, schema);
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public bool eliminarUsuario(int id_usuario)
        {
            try
            {
                return UsuarioDAO.getInstance().eliminarUsuario(id_usuario);
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public bool updateUsuario(int iduser, string nombre, string password, string nombre_rol, string schema)
        {
            try
            {
                return UsuarioDAO.getInstance().updateUsuario(iduser, nombre, password, nombre_rol, schema);
            }
            catch (Exception e)
            {

                throw e;
            }
        }
        #endregion

        public Usuario login(String empresa,String user, String password)
        {
            try
            {
                return UsuarioDAO.getInstance().login(empresa,user, password);
            }
            catch(Exception e)
            {

                throw e;
            }
            
        }

        public DataTable getMenuItems(int id_rol)
        {
            try
            {
                return UsuarioDAO.getInstance().getMenuItems(id_rol);
            }
            catch (Exception e)
            {

                throw e;
            }

        }

        public Usuario setSucursalPorUsuario(Usuario user, string schema)
        {
            try
            {
                return UsuarioDAO.getInstance().setSucursalPorUsuario(user, schema);
            }
            catch (Exception e)
            {

                throw e;
            }
        }


        public bool updateSucursalUsuario(int id_usuario, int id_sucursal, string schema)
        {
            try
            {
                return UsuarioDAO.getInstance().updateSucursalUsuario(id_usuario, id_sucursal, schema);
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public bool eliminarSucursalUsuario(int id_usuario, int id_sucursal, string schema)
        {
            try
            {
                return UsuarioDAO.getInstance().eliminarSucursalUsuario(id_usuario, id_sucursal, schema);
            }
            catch (Exception e)
            {

                throw e;
            }
        }

    }

    
}
