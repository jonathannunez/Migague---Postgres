using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaEntidades;
using CapaDatos;
using System.Data;

namespace CapaLogicaNegocio
{
    public class RolLN
    {
        // getInstance
        #region "PATRON SINGLETON"
        private static RolLN rol = null;
        private RolLN() { }
        public static RolLN getInstance()
        {
            if (rol == null)
            {
                rol = new RolLN();
            }
            return rol;
        }
        #endregion 

        #region "ABM"
        public List<Rol> listaRoles(string schema)
        {
            try
            {
                return RolDAO.getInstance().listaRoles(schema);
            }
            catch (Exception e)
            {

                throw e;
            }
        }
        
        public string nuevoRol(string nombre, string schema)
        {
            try
            {
                return RolDAO.getInstance().nuevoRol(nombre, schema);
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public bool updateRol(int id, string nombre, string schema)
        {
            try
            {
                return RolDAO.getInstance().updateRol(id, nombre, schema);
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public bool eliminarRol(int id, string schema)
        {
            try
            {
                return RolDAO.getInstance().eliminarRol(id, schema);
            }
            catch (Exception e)
            {

                throw e;
            }
        }
        
        #endregion

      

    }


}
