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
    public class SucursalLN
    {
        // getInstance
        #region "PATRON SINGLETON"
        private static SucursalLN sucursal = null;
        private SucursalLN() { }
        public static SucursalLN getInstance()
        {
            if (sucursal == null)
            {
                sucursal = new SucursalLN();
            }
            return sucursal;
        }
        #endregion 

        #region "ABM"
        public List<Sucursal> listaSucursales(string schema)
        {
            try
            {
                return SucursalDAO.getInstance().listaSucursales(schema);
            }
            catch (Exception e)
            {

                throw e;
            }
        }
        
        public string nuevaSucursal(string nombre, string calle, int altura, DateTime fecha, int id_localidad, string schema)
        {
            try
            {
                return SucursalDAO.getInstance().nuevaSucursal(nombre, calle, altura, fecha, id_localidad, schema);
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public bool updateSucursal(int id, string nombre, string calle, int altura, DateTime fecha, int id_localidad,
            string schema)
        {
            try
            {
                return SucursalDAO.getInstance().updateSucursal(id, nombre, calle, altura, fecha, id_localidad, schema);
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public bool eliminarSucursal(int id, string schema)
        {
            try
            {
                return SucursalDAO.getInstance().eliminarSucursal(id, schema);
            }
            catch (Exception e)
            {

                throw e;
            }
        }
        
        #endregion

      

    }


}
