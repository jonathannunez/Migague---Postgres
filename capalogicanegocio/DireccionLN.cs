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
    public class DireccionLN
    {
        // getInstance
        #region "PATRON SINGLETON"
        private static DireccionLN direccion = null;
        private DireccionLN() { }
        public static DireccionLN getInstance()
        {
            if (direccion == null)
            {
                direccion = new DireccionLN();
            }
            return direccion;
        }
        #endregion 


        #region "ABM"
        public List<Direccion> listaDireccionesCliente(int id_cliente, string schema, NpgsqlConnection conexion)
        {
            try
            {
                return DireccionDAO.getInstance().listaDireccionesCliente(id_cliente, schema, conexion);
            }
            catch (Exception e)
            {

                throw e;
            }
        }
        
        public bool nuevaDireccion(int id_cliente, Direccion direccion, string schema, NpgsqlConnection conexion)
        {
            try
            {
                return DireccionDAO.getInstance().nuevaDireccion(id_cliente, direccion, schema, conexion);
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public bool updateDireccion(int id, int id_cliente, string calle, int altura, string descripcion, int id_localidad,
            DateTime fecha, string schema)
        {
            try
            {
                return DireccionDAO.getInstance().updateDireccion(id,id_cliente, calle, altura, descripcion, id_localidad, 
                    fecha, schema);
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public bool eliminarDireccion(int id, string schema, NpgsqlConnection conexion)
        {
            try
            {
                return DireccionDAO.getInstance().eliminarDireccion(id, schema, conexion);
            }
            catch (Exception e)
            {

                throw e;
            }
        }
        
        #endregion

      

    }


}
