using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaEntidades;
using CapaLogicaNegocio;
using System.Data;

namespace Migague.Views.ABM
{
    public partial class AdminNuevoUsuario : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                TienePermiso();
                List<Rol> listaRoles = RolLN.getInstance().listaRoles(Session["schema"].ToString());
                foreach(Rol rol in listaRoles)
                {
                    ListItem newItem = new ListItem(rol.nombre, rol.id.ToString(), true);
                    ddlRoles.Items.Add(newItem);
                }
            }
        }

        protected void btnAddUser_Click(object sender, EventArgs e)
        {
            string retorno = UsuarioLN.getInstance().nuevoUsuario(txtNombreUser.Text.Trim(),
            txtPasswordUser.Text.Trim(), ddlRoles.SelectedValue.ToString().Trim(), Session["schema"].ToString());
            txtNombreUser.Text = "";
            txtPasswordUser.Text = "";
            Response.Write(@"<script language='javascript'>alert('" + retorno + " .');</script>");
        }
    }
}