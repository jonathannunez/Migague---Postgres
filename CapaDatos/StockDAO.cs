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
        private static StockDAO daoAdmin = null;
        private StockDAO() { }
        public static StockDAO getInstance()
        {
            if (daoAdmin == null)
            {
                daoAdmin = new StockDAO();
            }
            return daoAdmin;
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
                cmd = new NpgsqlCommand("logueo.spgetstock", conexion);
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

        public bool checkStockPreVenta(int id_sucursal, int id_modelo, int id_color, int id_talle, int cantidad, 
            string schema)
        {
            bool retorno = false;
            NpgsqlConnection conexion = null;
            NpgsqlCommand cmd = null;
            NpgsqlTransaction tran = null;
            NpgsqlDataReader dr = null;

            try
            {
                conexion = Conexion.getInstance().ConexionDB();
                cmd = new NpgsqlCommand("logueo.checkStockPreVenta", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("parm_idsucursal", id_modelo);
                cmd.Parameters.AddWithValue("parm_idmodelo", id_modelo);
                cmd.Parameters.AddWithValue("parm_idcolor", id_color);
                cmd.Parameters.AddWithValue("parm_idtalle", id_talle);
                cmd.Parameters.AddWithValue("parm_schema", schema);
                conexion.Open();
                tran = conexion.BeginTransaction();
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    if (Convert.ToInt32(dr["CANTIDAD_NETA"].ToString()) >= cantidad)
                    {
                        retorno = true;
                    }
                }
                dr.Close();
            }
            catch (Exception e)
            {
                retorno = false;
                throw (e);
            }
            finally
            {
                tran.Commit();
                conexion.Close();
            }
            return retorno;
        }

        public bool reservaStockPreVenta(int id_modelo, int id_color, int id_talle, int cantidad, string schema)
        {
            bool retorno = false;
            //bool hayStock = checkStockPreVenta(id_modelo, id_color, id_talle, cantidad, schema);
            bool hayStock = false;
            if (hayStock)
            {
                // si hay stock disponible lo reservo
                NpgsqlConnection conexion = null;
                NpgsqlCommand cmd = null;
                try
                {
                    conexion = Conexion.getInstance().ConexionDB();
                    cmd = new NpgsqlCommand("logueo..reservarStock", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("parm_idmodelo", id_modelo);
                    cmd.Parameters.AddWithValue("parm_idcolor", id_color);
                    cmd.Parameters.AddWithValue("parm_idtalle", id_talle);
                    cmd.Parameters.AddWithValue("parm_schema", schema);
                    conexion.Open();
                    cmd.ExecuteNonQuery();
                    retorno = true;
                }
                catch (Exception e)
                {
                    throw e;
                }
                finally
                {
                    conexion.Close();
                }
            }
            return retorno;
        }
        #endregion

    }
}
