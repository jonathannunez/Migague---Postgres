using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using CapaEntidades;
using Npgsql;

namespace CapaDatos
{
    public class ArticuloDAO
    {
        // getInstance
        #region "PATRON SINGLETON"
        private static ArticuloDAO articuloDao = null;
        private ArticuloDAO() { }
        public static ArticuloDAO getInstance()
        {
            if (articuloDao == null)
            {
                articuloDao = new ArticuloDAO();
            }
            return articuloDao;
        }
        #endregion

        #region "ABMS"
        public List<Articulo> listaArticulos(string schema)
        {
            List<Articulo> listaArticulos = new List<Articulo>();
            NpgsqlConnection conexion = null;
            NpgsqlCommand cmd = null;
            NpgsqlTransaction tran = null;
            NpgsqlDataReader dr = null;

            try
            {
                conexion = Conexion.getInstance().ConexionDB();
                cmd = new NpgsqlCommand("logueo.spgetarticulos", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("parm_schema", schema);
                conexion.Open();
                tran = conexion.BeginTransaction();
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Articulo articulo = new Articulo();
                    articulo.id = Convert.ToInt32(dr["ID"].ToString());
                    articulo.id_modelo = Convert.ToInt32(dr["IDMODELO"].ToString());
                    articulo.id_talle = Convert.ToInt32(dr["IDTALLE"].ToString());
                    articulo.id_color = Convert.ToInt32(dr["IDCOLOR"].ToString());
                    articulo.precio_may = Convert.ToInt32(dr["PRECIOMAY"].ToString());
                    articulo.precio_min = Convert.ToInt32(dr["PRECIOMIN"].ToString());
                    articulo.cod_barra = dr["CODBARRA"].ToString();
                    articulo.es_activo = Convert.ToBoolean(dr["ESACTIVO"].ToString());
                    listaArticulos.Add(articulo);
                }
                dr.Close();
            }
            catch (Exception e)
            {
                listaArticulos = null;
                throw (e);
            }
            finally
            {
                tran.Commit();
                conexion.Close();
                // proveedor 
                List<Modelo> listaModelos = ModeloDAO.getInstance().listaModelos(schema);
                List<Talle> listaTalles = TalleDAO.getInstance().listaTalles(schema);
                List<Color> listaColores = ColorDAO.getInstance().listaColores(schema);
                foreach(Articulo articulo in listaArticulos)
                {
                    foreach(Modelo modelo in listaModelos)
                    {
                        if (articulo.id_modelo == modelo.id)
                        {
                            articulo.modelo = modelo;
                            break;
                        }
                    }
                    foreach(Talle talle in listaTalles)
                    {
                        if(articulo.id_talle == talle.id)
                        {
                            articulo.talle = talle;
                            break;
                        }
                    }
                    foreach(Color color in listaColores)
                    {
                        if(articulo.id_color == color.id)
                        {
                            articulo.color = color;
                        }
                    }                    
                }

                
            }
            return listaArticulos;
        }
        
        public string nuevoArticulo(Articulo articulo , Stockcs stock, string schema)
        {
            string retorno = null;
            List<Articulo> listArticulos = listaArticulos(schema);
            bool existe = validarObjetoExistente(listArticulos, articulo.modelo.nombre, articulo.color.nombre, articulo.talle.nombre);
            if (existe)
            {
                retorno = "El articulo ya existe";
                return retorno;
            }
            NpgsqlConnection conexion = null;
            NpgsqlCommand cmd = null;
            NpgsqlTransaction tran = null;
            try
            {
                conexion = Conexion.getInstance().ConexionDB();
                cmd = new NpgsqlCommand("logueo.spnuevoarticulo", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("parm_idmodelo", articulo.modelo.id);
                cmd.Parameters.AddWithValue("parm_idtalle", articulo.talle.id);
                cmd.Parameters.AddWithValue("parm_idcolor", articulo.color.id);
                cmd.Parameters.AddWithValue("parm_preciomay", articulo.precio_may);
                cmd.Parameters.AddWithValue("parm_preciomin", articulo.precio_min);
                cmd.Parameters.AddWithValue("parm_codbarra", articulo.cod_barra);
                cmd.Parameters.AddWithValue("parm_schema", schema);
                conexion.Open();
                tran = conexion.BeginTransaction();
                Object id_articulo = cmd.ExecuteScalar();
                articulo.id = Convert.ToInt32(id_articulo);

                if (!StockDAO.getInstance().nuevoStock(stock, schema, conexion))
                {
                    tran.Rollback();
                    return "Error al crear stock";
                }
            }
            catch (Exception e)
            {
                tran.Rollback();
            }
            finally
            {
                
            }
            tran.Commit();
            conexion.Close();
            retorno = "Nuevo articulo añadido correctamente";
            return retorno;
        }

        public bool updateArticulo(int id, int idmodelo, string modelo, int idtalle, string talle, int idcolor, 
            string color, int preciomay, int preciomin, string codbarra, string schema)
        {
            NpgsqlConnection conexion = null;
            NpgsqlCommand cmd = null;
            List<Articulo> listArticulos = listaArticulos(schema);
            bool existe = validarObjetoExistente(listArticulos, modelo, color, talle);
            if (existe)
            {
                return false;
            }
            try
            {
                conexion = Conexion.getInstance().ConexionDB();
                cmd = new NpgsqlCommand("logueo.spupdatearticulo", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("parm_id", id);
                cmd.Parameters.AddWithValue("parm_idmodelo", idmodelo);
                cmd.Parameters.AddWithValue("parm_idtalle", idtalle);
                cmd.Parameters.AddWithValue("parm_idcolor", idcolor);
                cmd.Parameters.AddWithValue("parm_preciomay", preciomay);
                cmd.Parameters.AddWithValue("parm_preciomin", preciomin);
                cmd.Parameters.AddWithValue("parm_codbarra", codbarra);
                cmd.Parameters.AddWithValue("parm_schema", schema);
                conexion.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                conexion.Close();
            }
            return true;
        }

        public bool eliminarArticulo(int id, string schema)
        {
            bool retorno = false;
            NpgsqlConnection conexion = null;
            NpgsqlCommand cmd = null;
            try
            {
                conexion = Conexion.getInstance().ConexionDB();
                cmd = new NpgsqlCommand("logueo.speliminararticulo", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("parm_id", id);
                cmd.Parameters.AddWithValue("parm_schema", schema);
                conexion.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                conexion.Close();
            }
            retorno = true;
            return retorno;
        }
        #endregion

        public bool validarObjetoExistente(List<Articulo> lista, string modelo, string color, string talle)
        {
            bool existe = false;
            foreach (Articulo articulo in lista)
            {
                if ((articulo.modelo.nombre.ToUpper().Trim() == modelo.ToUpper().Trim()) &
                    (articulo.color.nombre.ToUpper().Trim() == color.ToUpper().Trim()) &
                    (articulo.talle.nombre.ToUpper().Trim() == talle.ToUpper().Trim()))
                {
                    existe = true;
                    break;
                }
            }
            return existe;
        }

    
    }
}
