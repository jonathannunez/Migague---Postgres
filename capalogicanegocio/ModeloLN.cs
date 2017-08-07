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
    public class ModeloLN
    {
        // getInstance
        #region "PATRON SINGLETON"
        private static ModeloLN modelo = null;
        private ModeloLN() { }
        public static ModeloLN getInstance()
        {
            if (modelo == null)
            {
                modelo = new ModeloLN();
            }
            return modelo;
        }
        #endregion 

        #region "ABM"
        public List<Modelo> listaModelos(string schema)
        {
            try
            {
                return ModeloDAO.getInstance().listaModelos(schema);
            }
            catch (Exception e)
            {

                throw e;
            }
        }
        
        public string nuevoModelo(string codigo, string nombre, int idproveedor, int idtemporada,
             int idtipoproducto, DateTime fecha, string schema)
        {
            try
            {
                return ModeloDAO.getInstance().nuevoModelo(codigo, nombre, idproveedor, idtemporada, idtipoproducto, 
                    fecha, schema);
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public bool updateModelo(int id, string codigo, string nombre, int idproveedor, int idtemporada,
             int idtipoproducto, DateTime fecha, string schema)
        {
            try
            {
                return ModeloDAO.getInstance().updateModelo(id, codigo, nombre, idproveedor, idtemporada, idtipoproducto,
                    fecha, schema);
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public bool eliminarModelo(int id, string schema)
        {
            try
            {
                return ModeloDAO.getInstance().eliminarModelo(id, schema);
            }
            catch (Exception e)
            {

                throw e;
            }
        }
        
        #endregion

      

    }


}
