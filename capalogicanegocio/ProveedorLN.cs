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
    public class ProveedorLN
    {
        // getInstance
        #region "PATRON SINGLETON"
        private static ProveedorLN proveedor = null;
        private ProveedorLN() { }
        public static ProveedorLN getInstance()
        {
            if (proveedor == null)
            {
                proveedor = new ProveedorLN();
            }
            return proveedor;
        }
        #endregion 

        #region "ABM"
        public List<Proveedor> listaProveedores(string schema)
        {
            try
            {
                return ProveedorDAO.getInstance().listaProveedores(schema);
            }
            catch (Exception e)
            {

                throw e;
            }
        }
        
        public string nuevoProveedor(string nombre, DateTime fecha, string schema)
        {
            try
            {
                return ProveedorDAO.getInstance().nuevoProveedor(nombre, fecha, schema);
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public bool updateProveedor(int id, string nombre, DateTime fecha, string schema)
        {
            try
            {
                return ProveedorDAO.getInstance().updateProveedor(id, nombre, fecha, schema);
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public bool eliminarProveedor(int id, string nombre, string schema)
        {
            try
            {
                return ProveedorDAO.getInstance().eliminarProveedor(id, schema);
            }
            catch (Exception e)
            {

                throw e;
            }
        }
        
        #endregion

      

    }


}
