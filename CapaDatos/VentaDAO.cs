using CapaEntidades;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    class VentaDAO
    {
        // getInstance
        #region "PATRON SINGLETON"
        private static VentaDAO ventaDao = null;
        private VentaDAO() { }
        public static VentaDAO getInstance()
        {
            if (ventaDao == null)
            {
                ventaDao = new VentaDAO();
            }
            return ventaDao;
        }
        #endregion

        #region ABM
        public List<Venta> getVentasCliente(Cliente cliente,string schema)
        {
            List<Venta> listaVentas = new List<Venta>();
            NpgsqlConnection conexion = null;
            NpgsqlCommand cmd = null;
            NpgsqlTransaction tran = null;
            NpgsqlDataReader dr = null;

            try
            {
                conexion = Conexion.getInstance().ConexionDB();
                cmd = new NpgsqlCommand("logueo.spgetventascliente", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("parm_schema", schema);
                cmd.Parameters.AddWithValue("parm_idcliente", cliente.id);
                conexion.Open();
                tran = conexion.BeginTransaction();
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Venta venta = new Venta();
                    venta.id_venta = Convert.ToInt32(dr["ID"].ToString());
                    venta.cliente = cliente;
                    venta.fecha = Convert.ToDateTime(dr["FECHA"].ToString());
                    venta.monto = Convert.ToDouble(dr["MONTO"].ToString());
                    venta.id_caja = Convert.ToInt32(dr["ID_CAJA"].ToString());
                    listaVentas.Add(venta);
                }
                dr.Close();
            }
            catch (Exception e)
            {
                listaVentas = null;
                tran.Rollback();
                conexion.Close();
                return listaVentas;
            }
            tran.Commit();
            conexion.Close();
            return listaVentas;
        }

        public bool nuevaVenta(Venta venta, string schema)
        {
            return true;
        }
        #endregion

    }
}
