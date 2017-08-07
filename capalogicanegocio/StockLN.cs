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
    public class StockLN
    {
        // getInstance
        #region "PATRON SINGLETON"
        private static StockLN stock = null;
        private StockLN() { }
        public static StockLN getInstance()
        {
            if (stock == null)
            {
                stock = new StockLN();
            }
            return stock;
        }
        #endregion 

        // stock
        #region "STOCK"
        public DataTable listaStock(int id_sucursal,string schema)
        {
            try
            {
                return StockDAO.getInstance().listaStock(id_sucursal,schema);
            }
            catch (Exception e)
            {

                throw e;
            }
        }
        /*
        public string nuevoBanco(string nombre, DateTime fecha, string schema)
        {
            try
            {
                return AdminDAO.getInstance().nuevoBanco(nombre, fecha, schema);
            }
            catch (Exception e)
            {

                throw e;
            }
        }
        public bool updateBanco(int id, string nombre, DateTime fecha, string schema)
        {
            try
            {
                return AdminDAO.getInstance().updateBanco(id, nombre, fecha, schema);
            }
            catch (Exception e)
            {

                throw e;
            }
        }
        public bool eliminarBanco(int id, string nombre, string schema)
        {
            try
            {
                return AdminDAO.getInstance().eliminarBanco(id, nombre, schema);
            }
            catch (Exception e)
            {

                throw e;
            }
        }
        */
        #endregion

      

    }


}
