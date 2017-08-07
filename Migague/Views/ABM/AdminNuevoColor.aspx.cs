using CapaLogicaNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Migague.Views.ABM
{
    public partial class AdminNuevoColor : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                TienePermiso();
            }
        }

        protected void BtnAdd_Click(object sender, EventArgs e)
        {
            
            string retorno = ColorLN.getInstance().nuevoColor(txtNombre.Text.Trim(),
                Session["schema"].ToString());
            txtNombre.Text = "";
            Response.Write(@"<script language='javascript'>alert('" + retorno + " .');</script>");
            
        }
    }
}