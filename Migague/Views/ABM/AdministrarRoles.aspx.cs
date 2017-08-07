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
    public partial class AdministrarRoles : BasePage
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
            List<Rol> listaUsuarios = RolLN.getInstance().listaRoles(Session["schema"].ToString());
            gridRoles.DataSource = listaUsuarios;
            gridRoles.DataBind();
        }

        //Para chequear una fila
        protected void chkDelete_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chkStatus = (CheckBox)sender;
            GridViewRow row = (GridViewRow)chkStatus.NamingContainer;
        }

        //Para chequear todas las filas
        protected void chkDeleteAll_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chkSelectAll = (CheckBox)gridRoles.HeaderRow.FindControl("chkDeleteAll");

            foreach (GridViewRow row in gridRoles.Rows)
            {
                CheckBox chkRow = (CheckBox)row.FindControl("chkDelete");

                if (chkSelectAll.Checked == true)
                {
                    chkRow.Checked = true;
                }
                else
                {
                    chkRow.Checked = false;
                }
            }
        }

        //Aceptar actualizar datos
        protected void gridRoles_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = gridRoles.Rows[e.RowIndex];
            TextBox txtIdRol = (TextBox)row.FindControl("txtEditIdRol");
            TextBox txtNombreRol = (TextBox)row.FindControl("txtEditNombre");
            int idRol = Convert.ToInt32(txtIdRol.Text.Trim());
            string nombreRol = txtNombreRol.Text.Trim();

            bool retorno = RolLN.getInstance().updateRol(idRol, nombreRol, Session["schema"].ToString());
            if (retorno)
            {
                Response.Write(@"<script language='javascript'>alert('Rol actualizado correctamente.');</script>");
                Response.Redirect(Request.Url.AbsoluteUri);
            }
            else
            {
                Response.Write(@"<script language='javascript'>alert('El Rol ya existe.');</script>");
                Response.Redirect(Request.Url.AbsoluteUri);
            }
        }

        //Poner en modo edición
        protected void gridRoles_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gridRoles.EditIndex = e.NewEditIndex;
            BindData();
        }

        //Cancelar el modo edición
        protected void gridRoles_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            e.Cancel = true;
            gridRoles.EditIndex = -1;
            BindData();
        }

        protected void BtnAddNewRol_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Views/ABM/AdminNuevoRol.aspx");
        }

        protected void BtnDeleteRol_Click(object sender, EventArgs e)
        {
            int count = 0;
            foreach (GridViewRow row in gridRoles.Rows)
            {
                CheckBox chk = (CheckBox)row.FindControl("chkDelete");
                if (chk.Checked)
                {
                    TextBox txtIdRol = (TextBox)row.FindControl("txtIdRol");
                    TextBox txtNombreRol = (TextBox)row.FindControl("txtNombre");
                    int idRol = Convert.ToInt32(txtIdRol.Text.Trim());
                    bool eliminados = RolLN.getInstance().eliminarRol(idRol,
                        Session["schema"].ToString());
                    if (!eliminados)
                    {
                        Response.Write(@"<script language='javascript'>alert('Error al eliminar el rol ');</script>");
                    }
                    else
                    {
                        count = count + 1;
                    }
                }

            }
            Response.Write(@"<script language='javascript'>alert('Eliminados: " + count + " .');</script>");
            Response.Redirect(Request.Url.AbsoluteUri);
        }
    }
}