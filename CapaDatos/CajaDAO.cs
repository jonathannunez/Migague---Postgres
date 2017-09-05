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
    public class CajaDAO
    {
        // getInstance
        #region "PATRON SINGLETON"
        private static CajaDAO cajaDao = null;
        private CajaDAO() { }
        public static CajaDAO getInstance()
        {
            if (cajaDao == null)
            {
                cajaDao = new CajaDAO();
            }
            return cajaDao;
        }
        #endregion

        #region "ABMS"
        public List<Caja_Movimientos> listaMovimientosCaja(string schema)
        {
            List<Caja_Movimientos> listaMovimientos = new List<Caja_Movimientos>();
            NpgsqlConnection conexion = null;
            NpgsqlCommand cmd = null;
            NpgsqlTransaction tran = null;
            NpgsqlDataReader dr = null;

            try
            {
                conexion = Conexion.getInstance().ConexionDB();
                cmd = new NpgsqlCommand("logueo.spgetmovimientoscaja", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("parm_schema", schema);
                conexion.Open();
                tran = conexion.BeginTransaction();
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Caja_Movimientos movimiento = new Caja_Movimientos();
                    movimiento.id = Convert.ToInt32(dr["ID"].ToString());
                    movimiento.fecha_movimiento = Convert.ToDateTime(dr["FECHA_MOVIMIENTO"].ToString());
                    movimiento.monto = Convert.ToDouble(dr["MONTO"].ToString());
                    movimiento.in_out = dr["IN_OUT"].ToString();
                    listaMovimientos.Add(movimiento);
                }
                
                dr.Close();
            }
            catch (Exception e)
            {
                listaMovimientos = null;
                tran.Rollback();
                conexion.Close();
            }
            tran.Commit();
            conexion.Close();
            return listaMovimientos;
        }

        public bool nuevoMovimientoCaja(Caja_Movimientos movimiento, string schema)
        {
            NpgsqlTransaction tran = null;
            NpgsqlConnection conexion = null;
            NpgsqlCommand cmd = null;
            try
            {
                conexion = Conexion.getInstance().ConexionDB();
                cmd = new NpgsqlCommand("logueo.spnuevomovimientocaja", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("parm_monto", movimiento.monto);
                cmd.Parameters.AddWithValue("parm_fecha_movimiento", movimiento.fecha_movimiento);
                cmd.Parameters.AddWithValue("parm_in_out", movimiento.in_out);
                cmd.Parameters.AddWithValue("parm_schema", schema);
                conexion.Open();
                tran = conexion.BeginTransaction();
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                tran.Rollback();
                conexion.Close();
                return false;
            }
            tran.Commit();
            conexion.Close();
            return true;
        }

        public bool nuevoMovimientoCaja(Caja_Movimientos movimiento, string schema, NpgsqlConnection conexion)
        {
            NpgsqlCommand cmd = null;
            try
            {
                conexion = Conexion.getInstance().ConexionDB();
                cmd = new NpgsqlCommand("logueo.spnuevomovimientocaja", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("parm_monto", movimiento.monto);
                cmd.Parameters.AddWithValue("parm_fecha_movimiento", movimiento.fecha_movimiento);
                cmd.Parameters.AddWithValue("parm_in_out", movimiento.in_out);
                cmd.Parameters.AddWithValue("parm_schema", schema);
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

        public bool cerrarCaja(Caja_Movimientos movimiento, Caja_Cierre cierre_caja,string schema)
        {
            NpgsqlTransaction tran = null;
            NpgsqlConnection conexion = null;
            NpgsqlCommand cmd = null;
            try
            {
                conexion = Conexion.getInstance().ConexionDB();
                cmd = new NpgsqlCommand("logueo.spcierrecaja", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("parm_fecha_desde", cierre_caja.fecha_desde);
                cmd.Parameters.AddWithValue("parm_fecha_hasta", cierre_caja.fecha_hasta);
                cmd.Parameters.AddWithValue("parm_idusuario", cierre_caja.id_usuario);
                cmd.Parameters.AddWithValue("parm_schema", schema);
                conexion.Open();
                tran = conexion.BeginTransaction();
                if(nuevoMovimientoCaja(movimiento, schema, conexion))
                {
                    cmd.ExecuteNonQuery();
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                tran.Rollback();
                conexion.Close();
                return false;
            }
            tran.Commit();
            conexion.Close();
            return true;
        }

        public List<Caja_Cierre> listaCierreCaja(string schema)
        {
            List<Caja_Cierre> listaCierre = new List<Caja_Cierre>();
            NpgsqlConnection conexion = null;
            NpgsqlCommand cmd = null;
            NpgsqlTransaction tran = null;
            NpgsqlDataReader dr = null;

            try
            {
                conexion = Conexion.getInstance().ConexionDB();
                cmd = new NpgsqlCommand("logueo.spgetcierrecaja", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("parm_schema", schema);
                conexion.Open();
                tran = conexion.BeginTransaction();
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Caja_Movimientos movimiento = new Caja_Movimientos();
                    movimiento.id = Convert.ToInt32(dr["ID"].ToString());
                    movimiento.fecha_movimiento = Convert.ToDateTime(dr["FECHA_MOVIMIENTO"].ToString());
                    movimiento.monto = Convert.ToDouble(dr["MONTO"].ToString());
                    movimiento.in_out = dr["IN_OUT"].ToString();
                    //listaMovimientos.Add(movimiento);
                }

                dr.Close();
            }
            catch (Exception e)
            {
                listaMovimientos = null;
                tran.Rollback();
                conexion.Close();
            }
            tran.Commit();
            conexion.Close();
            return listaMovimientos;
        }

        #endregion

        /*
        public bool validarObjetoExistente(List<Cliente> lista, string nombre, string cuit)
        {
            bool existe = false;
            foreach (Cliente cliente in lista)
            {
                if ((cliente.nombre.ToUpper().Trim() == nombre.ToUpper().Trim()) &
                    (cliente.cuit.ToUpper().Trim() == cuit.ToUpper().Trim()))
                {
                    existe = true;
                    break;
                }
            }
            return existe;
        }
        */

    }
}
