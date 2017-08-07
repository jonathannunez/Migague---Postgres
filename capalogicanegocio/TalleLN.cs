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
    public class TalleLN
    {
        // getInstance
        #region "PATRON SINGLETON"
        private static TalleLN talle = null;
        private TalleLN() { }
        public static TalleLN getInstance()
        {
            if (talle == null)
            {
                talle = new TalleLN();
            }
            return talle;
        }
        #endregion 

        #region "ABM"
        public List<Talle> listaTalles(string schema)
        {
            try
            {
                return TalleDAO.getInstance().listaTalles(schema);
            }
            catch (Exception e)
            {

                throw e;
            }
        }
        
        public string nuevoTalle(string nombre, string schema)
        {
            try
            {
                return TalleDAO.getInstance().nuevoTalle(nombre, schema);
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public bool updateTalle(int id, string nombre, string schema)
        {
            try
            {
                return TalleDAO.getInstance().updateTalle(id, nombre, schema);
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public bool eliminarTalle(int id, string nombre, string schema)
        {
            try
            {
                return TalleDAO.getInstance().eliminarTalle(id, nombre, schema);
            }
            catch (Exception e)
            {

                throw e;
            }
        }
        
        #endregion

      

    }


}
