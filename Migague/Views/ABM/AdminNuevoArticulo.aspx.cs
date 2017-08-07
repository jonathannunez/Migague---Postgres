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
            //cargarColores();
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
            int preciomay = Convert.ToInt32(txtPreciomay.Text.Trim());
            int preciomin = Convert.ToInt32(txtPreciomin.Text.Trim());
            int idmodelo = Convert.ToInt32(ddlModelo.SelectedItem.Value.ToString());
            string modelo = ddlModelo.SelectedItem.Text.ToString();
            int idtalle = Convert.ToInt32(ddlTalle.SelectedItem.Value.ToString());
            string talle = ddlTalle.SelectedItem.Text.ToString();
            int idcolor = Convert.ToInt32(ddlColor.SelectedItem.Value.ToString());
            string color = ddlColor.SelectedItem.Text.ToString();
            
            string retorno = ArticuloLN.getInstance().nuevoArticulo(idmodelo, modelo, idtalle, talle, idcolor, color, 
                preciomay, preciomin, "", Session["schema"].ToString());
            txtPreciomay.Text = "";
            txtPreciomin.Text = "";
            Response.Write(@"<script language='javascript'>alert('" + retorno + " .');</script>");
            
        }
    }
}