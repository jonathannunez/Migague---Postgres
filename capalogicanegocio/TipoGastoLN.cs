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
    public class TipoGastoLN
    {
        // getInstance
        #region "PATRON SINGLETON"
        private static TipoGastoLN tipoGasto = null;
        private TipoGastoLN() { }
        public static TipoGastoLN getInstance()
        {
            if (tipoGasto == null)
            {
                tipoGasto = new TipoGastoLN();
            }
            return tipoGasto;
        }
        #endregion 

        #region "ABM"
        public List<TipoGasto> listaTiposGastos(string schema)
        {
            try
            {
                return TipoGastoDAO.getInstance().listaTiposGastos(schema);
            }
            catch (Exception e)
            {

                throw e;
            }
        }
        
        public string nuevoTipoGasto(string nombre, string schema)
        {
            try
            {
                return TipoGastoDAO.getInstance().nuevoTipoGasto(nombre, schema);
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public bool updateTipoGasto(int id, string nombre, string schema)
        {
            try
            {
                return TipoGastoDAO.getInstance().updateTipoGasto(id, nombre, schema);
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public bool eliminarTipoGasto(int id, string schema)
        {
            try
            {
                return TipoGastoDAO.getInstance().eliminarTipoGasto(id, schema);
            }
            catch (Exception e)
            {

                throw e;
            }
        }
        
        #endregion

      

    }


}
