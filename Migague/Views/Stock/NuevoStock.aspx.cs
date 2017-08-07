using CapaLogicaNegocio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaEntidades;

namespace Migague.Views.Stock
{
    public partial class NuevoStock : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                TienePermiso();
                BindData();
            }
        }

        protected void BindData()
        {
            
            List<Articulo> listArticulos = ArticuloLN.getInstance().listaArticulos(Session["schema"].ToString());
            foreach (Articulo articulo in listArticulos)
            {
                ListItem newItem = new ListItem(articulo.modelo.nombre, articulo.id.ToString(), true);

                if (ddlArticulos.Items.FindByText(newItem.Text.ToString()) == null)
                {
                    ddlArticulos.Items.Add(newItem);
                }

            }
            
        }

        protected void BtnAdd_Click(object sender, EventArgs e)
        {
            DateTime dateTime = DateTime.UtcNow.Date;
            /*
            int id_pais = Convert.ToInt32(ddlPaises.SelectedValue.ToString());
            string retorno = AdminLN.getInstance().nuevaProvincia(id_pais,txtNombre.Text.Trim(), dateTime,
                Session["schema"].ToString());
            txtNombre.Text = "";
            
            Response.Write(@"<script language='javascript'>alert('" + retorno + " .');</script>");
            */
        }

        protected void ddlPaises_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        protected void ddlArticulos_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlColores_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}