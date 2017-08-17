using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaEntidades;
using CapaDatos;
using System.Data;
using Npgsql;

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

        public List<Stockcs> listaStock2(int id_sucursal, string schema)
        {
            try
            {
                return StockDAO.getInstance().listaStock2(id_sucursal, schema);
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public bool nuevoStock(Stockcs stock, string schema, NpgsqlConnection conexion)
        {
            try
            {
                return StockDAO.getInstance().nuevoStock(stock, schema, conexion);
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public bool decrementarStock(Stockcs stock, Sucursal sucursal, string schema)
        {
            try
            {
                return StockDAO.getInstance().decrementarStock(stock, sucursal, schema);
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public bool incrementarStock(Stockcs stock, Sucursal sucursal, string schema)
        {
            try
            {
                return StockDAO.getInstance().incrementarStock(stock, sucursal, schema);
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public bool registrarStockPerdido(Stockcs stock, Sucursal sucursal, string descripcion, string schema)
        {
            try
            {
                return StockDAO.getInstance().registrarStockPerdido(stock, sucursal, descripcion, schema);
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public bool registrarStockFallado(Stockcs stock, Sucursal sucursal, string schema)
        {
            try
            {
                return StockDAO.getInstance().registrarStockFallado(stock, sucursal, schema);
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public bool cambioStock(Stockcs stockEntrada, Stockcs stockSalida, Sucursal sucursal, string schema)
        {
            try
            {
                return StockDAO.getInstance().cambioStock(stockEntrada, stockSalida, sucursal, schema);
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public bool transferenciaStock(Stockcs stock, Sucursal sucursalSalida, Sucursal sucursalEntrada, string schema)
        {
            try
            {
                return StockDAO.getInstance().transferenciaStock(stock, sucursalSalida, sucursalEntrada, schema);
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        #endregion



    }


}
