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
    public class ProvinciaLN
    {
        // getInstance
        #region "PATRON SINGLETON"
        private static ProvinciaLN provincia = null;
        private ProvinciaLN() { }
        public static ProvinciaLN getInstance()
        {
            if (provincia == null)
            {
                provincia = new ProvinciaLN();
            }
            return provincia;
        }
        #endregion 


        #region "ABM"
        public List<Provincia> listaProvincias(string schema)//int id_pais, string schema)
        {
            try
            {
                return ProvinciaDAO.getInstance().listaProvincias(schema);//id_pais, schema);
            }
            catch (Exception e)
            {

                throw e;
            }
        }
        
        public string nuevaProvincia(int id_pais, string nombre_pais, string nombre, DateTime fecha, string schema)
        {
            try
            {
                return ProvinciaDAO.getInstance().nuevaProvincia(id_pais, nombre_pais, nombre, fecha, schema);
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public bool updateProvincia(int id, string nombre, DateTime fecha, string schema)
        {
            try
            {
                return ProvinciaDAO.getInstance().updateProvincia(id, nombre, fecha, schema);
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public bool eliminarProvincia(int id, string schema)
        {
            try
            {
                return ProvinciaDAO.getInstance().eliminarProvincia(id, schema);
            }
            catch (Exception e)
            {

                throw e;
            }
        }
        
        #endregion

      

    }


}
