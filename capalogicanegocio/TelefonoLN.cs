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
    public class TelefonoLN
    {
        // getInstance
        #region "PATRON SINGLETON"
        private static TelefonoLN telefono = null;
        private TelefonoLN() { }
        public static TelefonoLN getInstance()
        {
            if (telefono == null)
            {
                telefono = new TelefonoLN();
            }
            return telefono;
        }
        #endregion 

        #region "ABM"
        public List<Telefono> listaTelefonos(int id_cliente, string schema, NpgsqlConnection conexion)
        {
            try
            {
                return TelefonoDAO.getInstance().listaTelefonos(id_cliente, schema, conexion);
            }
            catch (Exception e)
            {

                throw e;
            }
        }
        
        public bool nuevoTelefono(int id_cliente, string telefono, string descripcion, DateTime fecha, string schema, NpgsqlConnection conexion)
        {
            try
            {
                return TelefonoDAO.getInstance().nuevoTelefono(id_cliente, telefono, descripcion, fecha, schema, conexion);
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        /*
        public bool updateTelefono(int id, int id_cliente, string telefono, string descripcion, DateTime fecha,
            string schema)
        {
            try
            {
                return TelefonoDAO.getInstance().updateTelefono(id, id_cliente, telefono, descripcion, fecha, schema);
            }
            catch (Exception e)
            {

                throw e;
            }
        }
        */
        public bool eliminarTelefono(int id, string schema, NpgsqlConnection conexion)
        {
            try
            {
                return TelefonoDAO.getInstance().eliminarTelefono(id, schema, conexion);
            }
            catch (Exception e)
            {

                throw e;
            }
        }
        
        #endregion

      

    }


}
