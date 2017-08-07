using CapaLogicaNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Migague.Views.ABM
{
    public partial class AdminNuevaTemporada : BasePage
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
            string ano = txtAno.Text.ToString().Trim();
            string retorno = TemporadaLN.getInstance().nuevaTemporada(ano, txtEstacion.Text.Trim(),
                Session["schema"].ToString());
            txtAno.Text = "";
            txtEstacion.Text = "";
            Response.Write(@"<script language='javascript'>alert('" + retorno + " .');</script>");
        }
    }
}