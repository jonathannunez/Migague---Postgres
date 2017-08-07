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
    public class ColorLN
    {
        // getInstance
        #region "PATRON SINGLETON"
        private static ColorLN color = null;
        private ColorLN() { }
        public static ColorLN getInstance()
        {
            if (color == null)
            {
                color = new ColorLN();
            }
            return color;
        }
        #endregion 

        // color
        #region "ABM"
        public List<Color> listaColores(string schema)
        {
            try
            {
                return ColorDAO.getInstance().listaColores(schema);
            }
            catch (Exception e)
            {

                throw e;
            }
        }
        
        public string nuevoColor(string nombre, string schema)
        {
            try
            {
                return ColorDAO.getInstance().nuevoColor(nombre, schema);
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public bool updateColor(int id, string nombre, string schema)
        {
            try
            {
                return ColorDAO.getInstance().updateColor(id, nombre, schema);
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public bool eliminarColor(int id, string nombre, string schema)
        {
            try
            {
                return ColorDAO.getInstance().eliminarColor(id, nombre, schema);
            }
            catch (Exception e)
            {

                throw e;
            }
        }
        
        #endregion

      

    }


}
