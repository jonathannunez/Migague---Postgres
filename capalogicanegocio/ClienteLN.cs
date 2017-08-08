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
    public class ClienteLN
    {
        // getInstance
        #region "PATRON SINGLETON"
        private static ClienteLN cliente = null;
        private ClienteLN() { }
        public static ClienteLN getInstance()
        {
            if (cliente == null)
            {
                cliente = new ClienteLN();
            }
            return cliente;
        }
        #endregion 

        #region "ABM"
        public List<Cliente> listaClientes(string schema)
        {
            try
            {
                return ClienteDAO.getInstance().listaClientes(schema);
            }
            catch (Exception e)
            {

                throw e;
            }
        }
        
        public string nuevoCliente(Cliente cliente, string schema)
        {
            try
            {
                return ClienteDAO.getInstance().nuevoCliente(cliente, schema);
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public bool updateCliente(int id, string razonsocial, string nombre, string cuit, DateTime fecha, string email,
            int cattributaria, int catlistaprecio, string schema)
        {
            try
            {
                return ClienteDAO.getInstance().updateCliente(id, razonsocial, nombre, cuit, fecha, email, cattributaria,
                    catlistaprecio, schema);
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public bool eliminarCliente(Cliente cliente, string schema)
        {
            try
            {
                return ClienteDAO.getInstance().eliminarCliente(cliente, schema);
            }
            catch (Exception e)
            {

                throw e;
            }
        }
        
        #endregion

      

    }


}
