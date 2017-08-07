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
    public class TipoProductoLN
    {
        // getInstance
        #region "PATRON SINGLETON"
        private static TipoProductoLN tipoProducto = null;
        private TipoProductoLN() { }
        public static TipoProductoLN getInstance()
        {
            if (tipoProducto == null)
            {
                tipoProducto = new TipoProductoLN();
            }
            return tipoProducto;
        }
        #endregion 

        #region "ABM"
        public List<TipoProducto> listaTiposProductos(string schema)
        {
            try
            {
                return TipoProductoDAO.getInstance().listaTiposProductos(schema);
            }
            catch (Exception e)
            {

                throw e;
            }
        }
        
        public string nuevoTipoProducto(string nombre, string schema)
        {
            try
            {
                return TipoProductoDAO.getInstance().nuevoTipoProducto(nombre, schema);
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public bool updateTipoProducto(int id, string nombre, string schema)
        {
            try
            {
                return TipoProductoDAO.getInstance().updateTipoProducto(id, nombre, schema);
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public bool eliminarTipoProducto(int id, string schema)
        {
            try
            {
                return TipoProductoDAO.getInstance().eliminarTipoProducto(id, schema);
            }
            catch (Exception e)
            {

                throw e;
            }
        }
        
        #endregion

      

    }


}
