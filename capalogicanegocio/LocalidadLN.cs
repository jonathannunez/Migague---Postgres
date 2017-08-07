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
    public class LocalidadLN
    {
        // getInstance
        #region "PATRON SINGLETON"
        private static LocalidadLN localidad = null;
        private LocalidadLN() { }
        public static LocalidadLN getInstance()
        {
            if (localidad == null)
            {
                localidad = new LocalidadLN();
            }
            return localidad;
        }
        #endregion 


        #region "ABM"
        public List<Localidad> listaLocalidades(string schema)
        {
            try
            {
                return LocalidadDAO.getInstance().listaLocalidades(schema);
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public List<Localidad> listaLocalidades(string schema, NpgsqlConnection conexion)
        {
            try
            {
                return LocalidadDAO.getInstance().listaLocalidades(schema, conexion);
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public string nuevaLocalidad(int id_provincia, string nombre_provincia, string nombre, DateTime fecha, string schema)
        {
            try
            {
                return LocalidadDAO.getInstance().nuevaLocalidad(id_provincia, nombre_provincia, nombre, fecha, schema);
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public bool updateLocalidad(int id, string nombre, DateTime fecha, string schema)
        {
            try
            {
                return LocalidadDAO.getInstance().updateLocalidad(id, nombre, fecha, schema);
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public bool eliminarLocalidad(int id, string schema)
        {
            try
            {
                return LocalidadDAO.getInstance().eliminarLocalidad(id, schema);
            }
            catch (Exception e)
            {

                throw e;
            }
        }
        
        #endregion

      

    }


}
