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
    public partial class AdminNuevaProvincia : BasePage
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
            List<Pais> listaPaises = PaisLN.getInstance().listaPaises(Session["schema"].ToString());
            foreach (Pais pais in listaPaises)
            {
                ListItem newItem = new ListItem(pais.nombre, pais.id.ToString(), true);
                ddlPaises.Items.Add(newItem);
            }
        }

        protected void BtnAdd_Click(object sender, EventArgs e)
        {
            DateTime dateTime = DateTime.UtcNow.Date;
            int id_pais = Convert.ToInt32(ddlPaises.SelectedValue.ToString());
            string nombre_pais = ddlPaises.SelectedItem.Text.ToString();
            string retorno = ProvinciaLN.getInstance().nuevaProvincia(id_pais,nombre_pais,txtNombre.Text.Trim(), dateTime,
                Session["schema"].ToString());
            txtNombre.Text = "";
            Response.Write(@"<script language='javascript'>alert('" + retorno + " .');</script>");
        }

        protected void ddlPaises_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }
    }
}