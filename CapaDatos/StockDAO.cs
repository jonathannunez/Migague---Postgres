using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using CapaEntidades;
using Npgsql;

namespace CapaDatos
{
    public class StockDAO
    {
        // getInstance
        #region "PATRON SINGLETON"
        private static StockDAO stockDao = null;
        private StockDAO() { }
        public static StockDAO getInstance()
        {
            if (stockDao == null)
            {
                stockDao = new StockDAO();
            }
            return stockDao;
        }
        #endregion

        public bool validarObjetoExistente(DataTable lista, string nombre)
        {
            bool existe = false;
            foreach (DataRow row in lista.Rows)
            {
                if ((row["nombre"].ToString().Trim() == nombre))
                {
                    existe = true;
                    break;
                }
            }
            return existe;
        }


        // stock
        #region "STOCK"
        public DataTable listaStock(int sucursal,string schema)
        {
            NpgsqlConnection conexion = null;
            NpgsqlCommand cmd = null;
            DataTable dt = new DataTable();
            NpgsqlTransaction tran = null;
            NpgsqlDataReader dr = null;

            try
            {
                conexion = Conexion.getInstance().ConexionDB();
                cmd = new NpgsqlCommand("logueo.spgetstock2", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("parm_idsucursal", sucursal);
                cmd.Parameters.AddWithValue("parm_schema", schema);
                conexion.Open();
                tran = conexion.BeginTransaction();
                dr = cmd.ExecuteReader();
                dt.Columns.Add("id");
                dt.Columns.Add("cantidad_neta");
                dt.Columns.Add("in_out");
                dt.Columns.Add("cantidad");
                dt.Columns.Add("fecha");
                dt.Columns.Add("articulo");
                dt.Columns.Add("color");
                dt.Columns.Add("esactivo");
                while (dr.Read())
                {
                    DataRow row = dt.NewRow();
                    row["id"] = Convert.ToInt32(dr["ID"].ToString());
                    row["cantidad_neta"] = Convert.ToInt32(dr["CANTIDAD_NETA"].ToString());
                    row["in_out"] = dr["IN_OUT"].ToString();
                    row["cantidad"] = Convert.ToInt32(dr["CANTIDAD"].ToString());
                    row["fecha"] = dr["FECHA"].ToString();
                    row["articulo"] = dr["ARTICULO"].ToString();
                    row["color"] = dr["COLOR"].ToString();
                    row["esactivo"] = Convert.ToBoolean(dr["ESACTIVO"].ToString());
                    dt.Rows.Add(row);
                }
                dr.Close();
            }
            catch (Exception e)
            {
                dt = null;
                throw (e);
            }
            finally
            {
                tran.Commit();
                conexion.Close();
            }
            return dt;
        } 

        public List<Stockcs> listaStock2(int id_sucursal, string schema)
        {
            NpgsqlConnection conexion = null;
            NpgsqlCommand cmd = null;
            List<Stockcs> listaStock = new List<Stockcs>();
            NpgsqlTransaction tran = null;
            NpgsqlDataReader dr = null;

            try
            {
                conexion = Conexion.getInstance().ConexionDB();
                cmd = new NpgsqlCommand("logueo.spgetstock2", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("parm_idsucursal", id_sucursal);
                cmd.Parameters.AddWithValue("parm_schema", schema);
                conexion.Open();
                tran = conexion.BeginTransaction();
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Stockcs stock = new Stockcs();
                    stock.id = Convert.ToInt32(dr["ID"].ToString());
                    stock.cantidad_neta = Convert.ToInt32(dr["CANTIDAD_NETA"].ToString());
                    stock.in_out = dr["IN_OUT"].ToString();
                    stock.cantidad = Convert.ToInt32(dr["CANTIDAD"].ToString());
                    stock.fecha = Convert.ToDateTime(dr["FECHA"].ToString());
                    stock.es_activo = Convert.ToBoolean(dr["ESACTIVO"].ToString());

                    Modelo modelo = new Modelo();
                    modelo.id = Convert.ToInt32(dr["IDMODELO"].ToString());
                    modelo.nombre = dr["ARTICULO"].ToString();

                    Color color = new Color();
                    color.id = Convert.ToInt32(dr["IDCOLOR"].ToString());
                    color.nombre = dr["COLOR"].ToString();

                    Talle talle = new Talle();
                    talle.id = Convert.ToInt32(dr["IDTALLE"].ToString());
                    talle.nombre = dr["TALLE"].ToString();

                    Articulo articulo = new Articulo();
                    articulo.modelo = modelo;
                    articulo.color = color;
                    articulo.talle = talle;

                    stock.articulo = articulo;
                    listaStock.Add(stock);
                }
                dr.Close();
            }
            catch (Exception)
            {
                tran.Rollback();
                listaStock = null;
            }
            tran.Commit();
            conexion.Close();
            return listaStock;
        }

        public bool checkStockDisponible(Stockcs stock, Sucursal sucursal, string schema)
        {
            bool retorno = false;
            NpgsqlConnection conexion = null;
            NpgsqlCommand cmd = null;
            NpgsqlTransaction tran = null;

            try
            {
                conexion = Conexion.getInstance().ConexionDB();
                cmd = new NpgsqlCommand("logueo.spcheckstockdisponible", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("parm_idsucursal", sucursal.id);
                cmd.Parameters.AddWithValue("parm_idarticulo", stock.articulo.id);
                cmd.Parameters.AddWithValue("parm_cantidad", stock.cantidad);
                cmd.Parameters.AddWithValue("parm_schema", schema);
                conexion.Open();
                Object retornoSQL = cmd.ExecuteScalar();
                retorno = Convert.ToBoolean(retornoSQL);
            }
            catch (Exception e)
            {
                throw (e);
            }
            tran.Commit();
            conexion.Close();
            return retorno;
        }

        public bool nuevoStock(Stockcs stock, string schema, NpgsqlConnection conexion)
        {
            NpgsqlCommand cmd = null;
            try
            {
                cmd = new NpgsqlCommand("logueo.spnuevostock", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("parm_cantidad", stock.cantidad);
                cmd.Parameters.AddWithValue("parm_fecha", stock.fecha);
                cmd.Parameters.AddWithValue("parm_idarticulo", stock.articulo.id);
                cmd.Parameters.AddWithValue("parm_idsucursal", stock.sucursal.id);
                cmd.Parameters.AddWithValue("parm_schema", schema);
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

        public bool decrementarStock(Stockcs stock, Sucursal sucursal, string schema)
        {
            bool retorno = false;
            bool hayStock = checkStockDisponible(stock, sucursal, schema);
            if (hayStock)
            {
                // si hay stock disponible lo reservo
                NpgsqlConnection conexion = null;
                NpgsqlCommand cmd = null;
                NpgsqlTransaction tran = null;
                try
                {
                    conexion = Conexion.getInstance().ConexionDB();
                    cmd = new NpgsqlCommand("logueo.spdecrementarstock", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("parm_sucursal", sucursal.id);
                    cmd.Parameters.AddWithValue("parm_idarticulo", stock.articulo.id);
                    cmd.Parameters.AddWithValue("parm_cantidad", stock.cantidad);
                    cmd.Parameters.AddWithValue("parm_schema", schema);
                    conexion.Open();
                    tran = conexion.BeginTransaction();
                    cmd.ExecuteNonQuery();
                    retorno = true;
                }
                catch (Exception e)
                {
                    tran.Rollback();
                    retorno = false;
                }
                tran.Commit();
                conexion.Close();
            }
            else
            {
                retorno = hayStock;
            }
            return retorno;
        }

        public bool decrementarStock(Stockcs stock, Sucursal sucursal, string schema, NpgsqlConnection conexion)
        {
            bool retorno = false;
            bool hayStock = checkStockDisponible(stock, sucursal, schema);
            if (hayStock)
            {
                // si hay stock disponible lo reservo
                NpgsqlCommand cmd = null;
                try
                {
                    cmd = new NpgsqlCommand("logueo.spdecrementarstock", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("parm_sucursal", sucursal.id);
                    cmd.Parameters.AddWithValue("parm_idarticulo", stock.articulo.id);
                    cmd.Parameters.AddWithValue("parm_cantidad", stock.cantidad);
                    cmd.Parameters.AddWithValue("parm_schema", schema);
                    cmd.ExecuteNonQuery();
                    retorno = true;
                }
                catch (Exception e)
                {
                    retorno = false;
                }
            }
            else
            {
                retorno = hayStock;
            }
            return retorno;
        }

        public bool incrementarStock(Stockcs stock, Sucursal sucursal, string schema)
        {
            bool retorno = false;
            NpgsqlConnection conexion = null;
            NpgsqlCommand cmd = null;
            NpgsqlTransaction tran = null;
            try
            {
                conexion = Conexion.getInstance().ConexionDB();
                cmd = new NpgsqlCommand("logueo.spincrementarstock", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("parm_sucursal", sucursal.id);
                cmd.Parameters.AddWithValue("parm_idarticulo", stock.articulo.id);
                cmd.Parameters.AddWithValue("parm_cantidad", stock.cantidad);
                cmd.Parameters.AddWithValue("parm_schema", schema);
                conexion.Open();
                tran = conexion.BeginTransaction();
                cmd.ExecuteNonQuery();
                retorno = true;
            }
            catch (Exception e)
            {
                tran.Rollback();
                retorno = false;
            }
            tran.Commit();
            conexion.Close();
            return retorno;
        }

        public bool incrementarStock(Stockcs stock, Sucursal sucursal, string schema, NpgsqlConnection conexion)
        {
            bool retorno = false;
            NpgsqlCommand cmd = null;
            try
            {
                cmd = new NpgsqlCommand("logueo.spincrementarstock", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("parm_sucursal", sucursal.id);
                cmd.Parameters.AddWithValue("parm_idarticulo", stock.articulo.id);
                cmd.Parameters.AddWithValue("parm_cantidad", stock.cantidad);
                cmd.Parameters.AddWithValue("parm_schema", schema);
                cmd.ExecuteNonQuery();
                retorno = true;
            }
            catch (Exception e)
            {
                retorno = false;
            }
            return retorno;
        }

        public bool registrarStockPerdido(Stockcs stock, Sucursal sucursal, string descripcion, string schema)
        {
            bool retorno = false;
            NpgsqlConnection conexion = null;
            NpgsqlCommand cmd = null;
            NpgsqlTransaction tran = null;
            try
            {
                conexion = Conexion.getInstance().ConexionDB();
                cmd = new NpgsqlCommand("logueo.spregistrarstockperdido", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("parm_sucursal", sucursal.id);
                cmd.Parameters.AddWithValue("parm_idarticulo", stock.articulo.id);
                cmd.Parameters.AddWithValue("parm_schema", schema);
                conexion.Open();
                tran = conexion.BeginTransaction();
                bool decremento = decrementarStock(stock, sucursal, schema, conexion);
                if (decremento)
                {
                    cmd.ExecuteNonQuery();
                    retorno = true;
                }
                else
                {
                    tran.Rollback();
                }
            }
            catch (Exception)
            {
                tran.Rollback();
                conexion.Close();
                return retorno;
            }
            tran.Commit();
            conexion.Close();
            return retorno;
        }

        public bool registrarStockFallado(Stockcs stock, Sucursal sucursal, string schema)
        {
            bool retorno = false;
            NpgsqlConnection conexion = null;
            NpgsqlCommand cmd = null;
            NpgsqlTransaction tran = null;
            try
            {
                conexion = Conexion.getInstance().ConexionDB();
                cmd = new NpgsqlCommand("logueo.spregistrarperdido", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("parm_sucursal", sucursal.id);
                cmd.Parameters.AddWithValue("parm_idarticulo", stock.articulo.id);
                cmd.Parameters.AddWithValue("parm_schema", schema);
                conexion.Open();
                tran = conexion.BeginTransaction();
                bool decremento = decrementarStock(stock, sucursal, schema, conexion);
                if (decremento)
                {
                    cmd.ExecuteNonQuery();
                    retorno = true;
                }
                else
                {
                    tran.Rollback();
                }
            }
            catch (Exception)
            {
                tran.Rollback();
                conexion.Close();
                return retorno;
            }
            tran.Commit();
            conexion.Close();
            return retorno;
        }

        public bool cambioStock(Stockcs stockEntrada, Stockcs stockSalida, Sucursal sucursal, string schema)
        {
            bool retorno = false;
            NpgsqlConnection conexion = null;
            NpgsqlTransaction tran = null;
            try
            {
                conexion = Conexion.getInstance().ConexionDB();
                conexion.Open();
                tran = conexion.BeginTransaction();
                bool incremento = incrementarStock(stockEntrada, sucursal, schema, conexion);
                if (incremento)
                {
                    bool decremento = decrementarStock(stockSalida, sucursal, schema, conexion);
                    if (decremento)
                    {
                        retorno = true;
                    }
                    else
                    {
                        tran.Rollback();
                    }
                }
                else
                {
                    tran.Rollback();
                }
            }
            catch (Exception)
            {
                tran.Rollback();
                conexion.Close();
                return retorno;
            }
            tran.Commit();
            conexion.Close();
            return retorno;
        }

        public bool transferenciaStock(Stockcs stock, Sucursal sucursalSalida, Sucursal sucursalEntrada, string schema)
        {
            bool retorno = false;
            NpgsqlConnection conexion = null;
            NpgsqlTransaction tran = null;
            try
            {
                conexion = Conexion.getInstance().ConexionDB();
                conexion.Open();
                tran = conexion.BeginTransaction();
                bool decremento = decrementarStock(stock, sucursalSalida, schema, conexion);
                if (decremento)
                {
                    bool incremento = incrementarStock(stock, sucursalEntrada, schema, conexion);
                    if (incremento)
                    {
                        retorno = true;
                    }
                    else
                    {
                        tran.Rollback();
                    }
                }
                else
                {
                    tran.Rollback();
                }
            }
            catch (Exception)
            {
                tran.Rollback();
                conexion.Close();
                return retorno;
            }
            tran.Commit();
            conexion.Close();
            return retorno;
        }


        #endregion
    }
}
