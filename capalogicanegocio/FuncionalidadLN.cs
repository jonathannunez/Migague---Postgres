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
    public class FuncionalidadLN
    {
        // getInstance
        #region "PATRON SINGLETON"
        private static FuncionalidadLN funcionalidad = null;
        private FuncionalidadLN() { }
        public static FuncionalidadLN getInstance()
        {
            if (funcionalidad == null)
            {
                funcionalidad = new FuncionalidadLN();
            }
            return funcionalidad;
        }
        #endregion 

        #region "ABM"
        public List<Funcionalidad> listaFuncionalidades(string schema)
        {
            try
            {
                return FuncionalidadDAO.getInstance().listaFuncionalidades(schema);
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public List<Funcionalidad> listaFuncionalidadesRol(int id_rol, string schema)
        {
            try
            {
                return FuncionalidadDAO.getInstance().listaFuncionalidadesRol(id_rol, schema);
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public void updatefuncxrol(int idrol, int id_funcionalidad, string schema)
        {
            try
            {
                FuncionalidadDAO.getInstance().updatefuncxrol(idrol,id_funcionalidad, schema);
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public void deletefuncxrol(int idrol, int id_funcionalidad, string schema)
        {
            try
            {
                FuncionalidadDAO.getInstance().deletefuncxrol(idrol, id_funcionalidad, schema);
            }
            catch (Exception e)
            {

                throw e;
            }
        }
        
        #endregion

      

    }


}
