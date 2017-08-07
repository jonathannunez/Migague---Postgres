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
    public class CategoriaPreciosLN
    {
        // getInstance
        #region "PATRON SINGLETON"
        private static CategoriaPreciosLN categoriaPrecio = null;
        private CategoriaPreciosLN() { }
        public static CategoriaPreciosLN getInstance()
        {
            if (categoriaPrecio == null)
            {
                categoriaPrecio = new CategoriaPreciosLN();
            }
            return categoriaPrecio;
        }
        #endregion 
        

        #region "ABM"
        public List<CategoriaPrecios> listaCategoriasPrecios(string schema)
        {
            try
            {
                return CategoriasPreciosDAO.getInstance().listaCategoriasPrecios(schema);
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public List<CategoriaPrecios> listaCategoriasPrecios(string schema, NpgsqlConnection conexion)
        {
            try
            {
                return CategoriasPreciosDAO.getInstance().listaCategoriasPrecios(schema, conexion);
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public string nuevaCategoriaPrecio(string nombre, string schema)
        {
            try
            {
                return CategoriasPreciosDAO.getInstance().nuevaCategoriaPrecio(nombre, schema);
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public bool updateCategoriaPrecio(int id, string nombre, string schema)
        {
            try
            {
                return CategoriasPreciosDAO.getInstance().updateCategoriaPrecio(id, nombre, schema);
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public bool eliminarCategoriaPrecio(int id, string schema)
        {
            try
            {
                return CategoriasPreciosDAO.getInstance().eliminarCategoriaPrecio(id, schema);
            }
            catch (Exception e)
            {

                throw e;
            }
        }
        
        #endregion

      

    }


}
