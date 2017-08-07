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
    public class PaisLN
    {
        // getInstance
        #region "PATRON SINGLETON"
        private static PaisLN pais = null;
        private PaisLN() { }
        public static PaisLN getInstance()
        {
            if (pais == null)
            {
                pais = new PaisLN();
            }
            return pais;
        }
        #endregion 


        #region "ABM"
        public List<Pais> listaPaises(string schema)
        {
            try
            {
                return PaisDAO.getInstance().listaPaises(schema);
            }
            catch (Exception e)
            {

                throw e;
            }
        }
        
        public string nuevoPais(string nombre, DateTime fecha, string schema)
        {
            try
            {
                return PaisDAO.getInstance().nuevoPais(nombre, fecha, schema);
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public bool updatePais(int id, string nombre, DateTime fecha, string schema)
        {
            try
            {
                return PaisDAO.getInstance().updatePais(id, nombre, fecha, schema);
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public bool eliminarPais(int id, string nombre, string schema)
        {
            try
            {
                return PaisDAO.getInstance().eliminarPais(id, nombre, schema);
            }
            catch (Exception e)
            {

                throw e;
            }
        }
        
        #endregion

      

    }


}
