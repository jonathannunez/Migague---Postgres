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
    public class Cuenta_CorrienteDAO
    {
        // getInstance
        #region "PATRON Cuenta_CorrienteDAO"
        private static Cuenta_CorrienteDAO cta_cteDao = null;
        private Cuenta_CorrienteDAO() { }
        public static Cuenta_CorrienteDAO getInstance()
        {
            if (cta_cteDao == null)
            {
                cta_cteDao = new Cuenta_CorrienteDAO();
            }
            return cta_cteDao;
        }
        #endregion

        #region "ABMS"
        public List<Cuenta_Corriente> listaCtaCteCliente(Cliente cliente,string schema)
        {
            List<Cuenta_Corriente> listaCtaCte = new List<Cuenta_Corriente>();
            NpgsqlConnection conexion = null;
            NpgsqlCommand cmd = null;
            NpgsqlTransaction tran = null;
            NpgsqlDataReader dr = null;

            try
            {
                conexion = Conexion.getInstance().ConexionDB();
                cmd = new NpgsqlCommand("logueo.spgetctactecliente", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("parm_idcliente", cliente.id);
                cmd.Parameters.AddWithValue("parm_schema", schema);
                conexion.Open();
                tran = conexion.BeginTransaction();
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Cuenta_Corriente cta_cte = new Cuenta_Corriente();
                    cta_cte.id = Convert.ToInt32(dr["ID"].ToString());
                    cta_cte.id_cliente = cliente.id;
                    cta_cte.id_venta = Convert.ToInt32(dr["ID_VENTA"].ToString());
                    cta_cte.saldo = Convert.ToDouble(dr["SALDO"].ToString());
                    cta_cte.fecha_cancelacion = Convert.ToDateTime(dr["FECHA_CANCELACION"].ToString());
                    cta_cte.id_caja = Convert.ToInt32(dr["ID_CAJA"].ToString());

                    listaCtaCte.Add(cta_cte);
                }
                
                dr.Close();
            }
            catch (Exception e)
            {
                listaCtaCte = null;
                tran.Rollback();
                conexion.Close();
            }
            tran.Commit();
            conexion.Close();
            return listaCtaCte;
        }

        public bool modificarCtaCte(Cliente cliente, string schema)
        {
            NpgsqlTransaction tran = null;
            NpgsqlConnection conexion = null;
            NpgsqlCommand cmd = null;
            try
            {
                conexion = Conexion.getInstance().ConexionDB();
                cmd = new NpgsqlCommand("logueo.spmodificarctacte", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("parm_idctacte", cliente.cta_cte.id);
                cmd.Parameters.AddWithValue("parm_idcliente", cliente.id);
                cmd.Parameters.AddWithValue("parm_idventa", cliente.cta_cte.id_venta);
                cmd.Parameters.AddWithValue("parm_saldo", cliente.cta_cte.saldo);
                cmd.Parameters.AddWithValue("parm_fecha_cancelacion", cliente.cta_cte.fecha_cancelacion);
                cmd.Parameters.AddWithValue("parm_idcaja", cliente.cta_cte.id_caja);
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

        public bool cerrarCaja(Caja_Cierre cierre_caja,string schema)
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
                if(nuevoMovimientoCaja(cierre_caja.movimiento, schema, conexion))
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
                    movimiento.id = Convert.ToInt32(dr["ID_MOVIMIENTO"].ToString());
                    movimiento.fecha_movimiento = Convert.ToDateTime(dr["FECHA_MOVIMIENTO"].ToString());
                    movimiento.monto = Convert.ToDouble(dr["MONTO"].ToString());
                    movimiento.in_out = dr["IN_OUT"].ToString();

                    Caja_Cierre cierre_caja = new Caja_Cierre();
                    cierre_caja.id = Convert.ToInt32(dr["ID_CIERRE"].ToString());
                    cierre_caja.fecha_desde = Convert.ToDateTime(dr["FECHA_DESDE"].ToString());
                    cierre_caja.fecha_hasta = Convert.ToDateTime(dr["FECHA_HASTA"].ToString());
                    cierre_caja.id_usuario = Convert.ToInt32(dr["ID_USUARIO"].ToString());
                    cierre_caja.movimiento = movimiento;
                    
                    listaCierre.Add(cierre_caja);
                }

                dr.Close();
            }
            catch (Exception e)
            {
                listaCierre = null;
                tran.Rollback();
                conexion.Close();
            }
            tran.Commit();
            conexion.Close();
            return listaCierre;
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
