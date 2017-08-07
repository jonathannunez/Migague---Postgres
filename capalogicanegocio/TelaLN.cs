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
    public class TelaLN
    {
        // getInstance
        #region "PATRON SINGLETON"
        private static TelaLN tela = null;
        private TelaLN() { }
        public static TelaLN getInstance()
        {
            if (tela == null)
            {
                tela = new TelaLN();
            }
            return tela;
        }
        #endregion 

        #region "ABM"
        public List<Tela> listaTelas(string schema)
        {
            try
            {
                return TelaDAO.getInstance().listaTelas(schema);
            }
            catch (Exception e)
            {

                throw e;
            }
        }
        
        public string nuevaTela(string nombre, string schema)
        {
            try
            {
                return TelaDAO.getInstance().nuevaTela(nombre, schema);
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public bool updateTela(int id, string nombre, string schema)
        {
            try
            {
                return TelaDAO.getInstance().updateTela(id, nombre, schema);
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public bool eliminarTela(int id, string nombre, string schema)
        {
            try
            {
                return TelaDAO.getInstance().eliminarTela(id, nombre, schema);
            }
            catch (Exception e)
            {

                throw e;
            }
        }
        
        #endregion

      

    }


}
