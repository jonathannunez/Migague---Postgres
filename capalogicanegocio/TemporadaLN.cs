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
    public class TemporadaLN
    {
        // getInstance
        #region "PATRON SINGLETON"
        private static TemporadaLN temporada = null;
        private TemporadaLN() { }
        public static TemporadaLN getInstance()
        {
            if (temporada == null)
            {
                temporada = new TemporadaLN();
            }
            return temporada;
        }
        #endregion 


        #region "ABM"
        public List<Temporada> listaTemporadas(string schema)
        {
            try
            {
                return TemporadaDAO.getInstance().listaTemporadas(schema);
            }
            catch (Exception e)
            {

                throw e;
            }
        }
        
        public string nuevaTemporada(string año, string estacion, string schema)
        {
            try
            {
                return TemporadaDAO.getInstance().nuevaTemporada(año, estacion, schema);
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public bool updateTemporada(int id, string año, string estacion, string schema)
        {
            try
            {
                return TemporadaDAO.getInstance().updateTemporada(id, año, estacion, schema);
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public bool eliminarTemporada(int id, string schema)
        {
            try
            {
                return TemporadaDAO.getInstance().eliminarTemporada(id, schema);
            }
            catch (Exception e)
            {

                throw e;
            }
        }
        
        #endregion

      

    }


}
