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
    public partial class AdminNuevoArticulo : BasePage
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
            cargarModelos();
            cargarTalles();
            cargarColores();
        }

        // cargar modelos
        protected void cargarModelos()
        {
            List<Modelo> list = ModeloLN.getInstance().listaModelos(Session["schema"].ToString());
            foreach (Modelo modelo in list)
            {
                ListItem newItem = new ListItem(modelo.nombre, modelo.id.ToString(), true);
                ddlModelo.Items.Add(newItem);
            }
        }

        // cargar talles
        protected void cargarTalles()
        {
            List<Talle> list = TalleLN.getInstance().listaTalles(Session["schema"].ToString());
            foreach (Talle talle in list)
            {
                ListItem newItem = new ListItem(talle.nombre, talle.id.ToString(), true);
                ddlTalle.Items.Add(newItem);
            }
        }

        // cargar colores
        protected void cargarColores()
        {
            List<Color> list = ColorLN.getInstance().listaColores(Session["schema"].ToString());
            foreach (Color color in list)
            {
                ListItem newItem = new ListItem(color.nombre, color.id.ToString(), true);
                ddlColor.Items.Add(newItem);
            }
        }
        
        protected void BtnAdd_Click(object sender, EventArgs e)
        {
            Modelo modelo = new Modelo();
            modelo.nombre = ddlModelo.SelectedItem.Text.ToString();
            modelo.id = Convert.ToInt32(ddlModelo.SelectedItem.Value.ToString());

            Talle talle = new Talle();
            talle.id = Convert.ToInt32(ddlTalle.SelectedItem.Value.ToString());
            talle.nombre = ddlTalle.SelectedItem.Text.ToString();

            Color color = new Color();
            color.id = Convert.ToInt32(ddlColor.SelectedItem.Value.ToString());
            color.nombre = ddlColor.SelectedItem.Text.ToString();

            Articulo articulo = new Articulo();
            articulo.precio_may = Convert.ToInt32(txtPreciomay.Text.Trim());
            articulo.precio_min = Convert.ToInt32(txtPreciomin.Text.Trim());
            articulo.modelo = modelo;
            articulo.talle = talle;
            articulo.color = color;
            articulo.cod_barra = "";

            Sucursal sucursal = new Sucursal();
            sucursal.id = Convert.ToInt32(Session["userSucursal"].ToString());

            Stockcs stock = new Stockcs();
            stock.articulo = articulo;
            stock.cantidad = Convert.ToInt32(txtStock.Text.Trim());
            stock.sucursal = sucursal;
            
            
            string retorno = ArticuloLN.getInstance().nuevoArticulo(articulo, stock, Session["schema"].ToString());


            txtPreciomay.Text = "";
            txtPreciomin.Text = "";
            Response.Write(@"<script language='javascript'>alert('" + retorno + " .');</script>");
            
        }
    }
}