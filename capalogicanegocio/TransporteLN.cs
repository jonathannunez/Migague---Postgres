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
    public class TransporteLN
    {
        // getInstance
        #region "PATRON SINGLETON"
        private static TransporteLN transporte = null;
        private TransporteLN() { }
        public static TransporteLN getInstance()
        {
            if (transporte == null)
            {
                transporte = new TransporteLN();
            }
            return transporte;
        }
        #endregion 

        #region "ABM"
        public List<Transporte> listaTransportes(string schema)
        {
            try
            {
                return TransporteDAO.getInstance().listaTransportes(schema);
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public List<Transporte> listaTransportesCliente(int id_cliente, string schema, NpgsqlConnection conexion)
        {
            try
            {
                return TransporteDAO.getInstance().listaTransportesCliente(id_cliente, schema, conexion);
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public string nuevoTransporte(string nombre, DateTime fecha, string schema)
        {
            try
            {
                return TransporteDAO.getInstance().nuevoTransporte(nombre, fecha, schema);
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public bool nuevoTransporteCliente(int id_cliente, Transporte transporte, string schema, NpgsqlConnection conexion)
        {
            try
            {
                return TransporteDAO.getInstance().nuevoTransporteCliente(id_cliente, transporte, schema, conexion);
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public bool updateTransporte(int id, string nombre, DateTime fecha, string schema)
        {
            try
            {
                return TransporteDAO.getInstance().updateTransporte(id, nombre, fecha, schema);
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public bool eliminarTransporte(int id, string schema)
        {
            try
            {
                return TransporteDAO.getInstance().eliminarTransporte(id, schema);
            }
            catch (Exception e)
            {

                throw e;
            }
        }
        
        #endregion

      

    }


}
