using CapaLogicaNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Migague.Views.ABM
{
    public partial class AdminNuevoBanco : BasePage
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
            DateTime dateTime = DateTime.UtcNow.Date;
            string retorno = BancoLN.getInstance().nuevoBanco(txtNombre.Text.Trim(), dateTime,
                Session["schema"].ToString());
            txtNombre.Text = "";
            Response.Write(@"<script language='javascript'>alert('" + retorno + " .');</script>");
        }
    }
}