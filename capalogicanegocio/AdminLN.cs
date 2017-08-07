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
    public class AdminLN
    {
        // getInstance
        #region "PATRON SINGLETON"
        private static AdminLN admin = null;
        private AdminLN() { }
        public static AdminLN getInstance()
        {
            if (admin == null)
            {
                admin = new AdminLN();
            }
            return admin;
        }
        #endregion

        //empresas
        #region "EMPRESAS"
        public List<Empresa> listaEmpresas()
        {
            try
            {
                return AdminDAO.getInstance().listaEmpresas();
            }
            catch (Exception e)
            {

                throw e;
            }
        }
        #endregion

        // categorias tributarias
        #region "CATEGORIAS TRIBUTARIAS"
        public List<CategoriaTributaria> listaCategoriasTributarias(string schema)
        {
            try
            {
                return AdminDAO.getInstance().listaCategoriasTributarias(schema);
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public List<CategoriaTributaria> listaCategoriasTributarias(string schema, NpgsqlConnection conexion)
        {
            try
            {
                return AdminDAO.getInstance().listaCategoriasTributarias(schema, conexion);
            }
            catch (Exception e)
            {

                throw e;
            }
        }
        #endregion

        // formas de pago
        #region "FORMAS DE PAGO"
        public List<FormaPago> listaFormasPago()
        {
            try
            {
                return AdminDAO.getInstance().listaFormasPago();
            }
            catch (Exception e)
            {

                throw e;
            }
        }
        #endregion

        // funcxrol
        #region "FUNCXROL"
        public void updatefuncxrol(int idrol, int id_funcionalidad, string schema)
        {
            try
            {
                AdminDAO.getInstance().updatefuncxrol(idrol, id_funcionalidad, schema);
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
                AdminDAO.getInstance().deletefuncxrol(idrol, id_funcionalidad, schema);
            }
            catch (Exception e)
            {

                throw e;
            }
        }
        #endregion


    }


}
