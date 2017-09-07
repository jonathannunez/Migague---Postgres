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

        public bool elimimnarCtaCte(Cliente cliente, string schema)
        {
            NpgsqlTransaction tran = null;
            NpgsqlConnection conexion = null;
            NpgsqlCommand cmd = null;
            try
            {
                conexion = Conexion.getInstance().ConexionDB();
                cmd = new NpgsqlCommand("logueo.speliminarctacte", conexion);
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
