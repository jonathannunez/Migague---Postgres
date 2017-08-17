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
    public class ArticuloLN
    {
        // getInstance
        #region "PATRON SINGLETON"
        private static ArticuloLN articulo = null;
        private ArticuloLN() { }
        public static ArticuloLN getInstance()
        {
            if (articulo == null)
            {
                articulo = new ArticuloLN();
            }
            return articulo;
        }
        #endregion 

        #region "ABM"
        public List<Articulo> listaArticulos(string schema)
        {
            try
            {
                return ArticuloDAO.getInstance().listaArticulos(schema);
            }
            catch (Exception e)
            {

                throw e;
            }
        }
        
        public string nuevoArticulo(Articulo articulo, Stockcs stock, string schema)
        {
            try
            {
                return ArticuloDAO.getInstance().nuevoArticulo(articulo, stock, schema);
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public bool updateArticulo(int id, int idmodelo, string modelo, int idtalle, string talle, int idcolor,
            string color, int preciomay, int preciomin, string codbarra, string schema)
        {
            try
            {
                return ArticuloDAO.getInstance().updateArticulo(id, idmodelo, modelo, idtalle, talle, idcolor, color,
                    preciomay, preciomin, codbarra, schema);
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public bool eliminarArticulo(int id, string schema)
        {
            try
            {
                return ArticuloDAO.getInstance().eliminarArticulo(id, schema);
            }
            catch (Exception e)
            {

                throw e;
            }
        }
        
        #endregion

      

    }


}
