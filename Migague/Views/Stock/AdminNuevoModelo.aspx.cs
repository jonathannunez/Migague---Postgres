using CapaLogicaNegocio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaEntidades;

namespace Migague.Views.ABM
{
    public partial class AdminNuevoModelo : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                TienePermiso();
                BindData();
            }

        }

        // bind data
        protected void BindData()
        {
            cargarProveedores();
            cargarTemporadas();
            cargarTiposProductos();
        }

        // cargar proveedores
        protected void cargarProveedores()
        {
            List<Proveedor> list = ProveedorLN.getInstance().listaProveedores(Session["schema"].ToString());
            foreach (Proveedor proveedor in list)
            {
                ListItem newItem = new ListItem(proveedor.nombre, proveedor.id.ToString(), true);
                ddlProveedor.Items.Add(newItem);
            }
        }

        // cargar temporadas
        protected void cargarTemporadas()
        {
            List<Temporada> list = TemporadaLN.getInstance().listaTemporadas(Session["schema"].ToString());
            foreach (Temporada temporada in list)
            {
                ListItem newItem = new ListItem(temporada.nombre, temporada.id.ToString(), true);
                ddlTemporada.Items.Add(newItem);
            }
        }

        // cargar tipos de productos
        protected void cargarTiposProductos()
        {
            List<TipoProducto> list = TipoProductoLN.getInstance().listaTiposProductos(Session["schema"].ToString());
            foreach (TipoProducto tipoProducto in list)
            {
                ListItem newItem = new ListItem(tipoProducto.nombre, tipoProducto.id.ToString(), true);
                ddlTipoProducto.Items.Add(newItem);
            }
        }

        protected void BtnAdd_Click(object sender, EventArgs e)
        {
            DateTime dateTime = DateTime.UtcNow.Date;
            string codigo = txtCodigo.Text.Trim();
            string nombre = txtNombre.Text.Trim();
            int idproveedor = Convert.ToInt32(ddlProveedor.SelectedItem.Value.ToString());
            int idtemporada = Convert.ToInt32(ddlTemporada.SelectedItem.Value.ToString());
            int idtipoproducto = Convert.ToInt32(ddlTipoProducto.SelectedItem.Value.ToString());
            
            string retorno = ModeloLN.getInstance().nuevoModelo(codigo,nombre,idproveedor,idtemporada, idtipoproducto,
                dateTime, Session["schema"].ToString());
            txtNombre.Text = "";
            Response.Write(@"<script language='javascript'>alert('" + retorno + " .');</script>");
            
        }
    }
}