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
    public class BancoLN
    {
        // getInstance
        #region "PATRON SINGLETON"
        private static BancoLN banco = null;
        private BancoLN() { }
        public static BancoLN getInstance()
        {
            if (banco == null)
            {
                banco = new BancoLN();
            }
            return banco;
        }
        #endregion 

        #region "ABM"
        public List<Banco> listaBancos(string schema)
        {
            try
            {
                return BancoDAO.getInstance().listaBancos(schema);
            }
            catch (Exception e)
            {

                throw e;
            }
        }
        
        public string nuevoBanco(string nombre, DateTime fecha, string schema)
        {
            try
            {
                return BancoDAO.getInstance().nuevoBanco(nombre, fecha, schema);
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public bool updateBanco(int id, string nombre, DateTime fecha, string schema)
        {
            try
            {
                return BancoDAO.getInstance().updateBanco(id, nombre, fecha, schema);
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public bool eliminarBanco(int id, string nombre, string schema)
        {
            try
            {
                return BancoDAO.getInstance().eliminarBanco(id, nombre, schema);
            }
            catch (Exception e)
            {

                throw e;
            }
        }
        
        #endregion

      

    }


}
