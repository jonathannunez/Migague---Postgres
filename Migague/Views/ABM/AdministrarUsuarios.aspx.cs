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
    public partial class AdministrarUsuarios : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                TienePermiso();
                bindData();
            }
        }

        protected void bindData()
        {
            List<Usuario> listaUsuarios = UsuarioLN.getInstance().listaUsuarios(Session["schema"].ToString());
            gridUsuarios.DataSource = listaUsuarios;
            gridUsuarios.DataBind();
        }
        protected void BtnAddNewUser_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Views/ABM/AdminNuevoUsuario.aspx");
        }

        protected void BtnEliminarUsuario_Click(object sender, EventArgs e)
        {
            int count = 0;
            foreach (GridViewRow row in gridUsuarios.Rows)
            {
                CheckBox chk = (CheckBox)row.FindControl("chkDelete");
                if (chk.Checked)
                {
                    TextBox txtID = (TextBox)row.FindControl("txtIdUser");
                    TextBox txtNombreUser = (TextBox)row.FindControl("txtNombreUser");
                    TextBox txtPassUser = (TextBox)row.FindControl("txtPassword");
                    TextBox txtRol = (TextBox)row.FindControl("txtRol");
                    int id_usuario = Convert.ToInt32(txtID.Text.Trim());
                    string nombreUsuario = txtNombreUser.Text.Trim();
                    string passUsuario = txtPassUser.Text.Trim();
                    string nombreRolUser = txtRol.Text.Trim();
                    bool eliminados = UsuarioLN.getInstance().eliminarUsuario(id_usuario);
                    if (!eliminados)
                    {
                        Response.Write(@"<script language='javascript'>alert('Error al eliminar el usuario " 
                                            + nombreUsuario + " .');</script>");
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

        //Para chequear una fila
        protected void chkDelete_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chkStatus = (CheckBox)sender;
            GridViewRow row = (GridViewRow)chkStatus.NamingContainer;
        }

        //Para chequear todas las filas
        protected void chkDeleteAll_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chkSelectAll = (CheckBox)gridUsuarios.HeaderRow.FindControl("chkDeleteAll");

            foreach(GridViewRow row in gridUsuarios.Rows)
            {
                CheckBox chkRow = (CheckBox)row.FindControl("chkDelete");

                if(chkSelectAll.Checked == true)
                {
                    chkRow.Checked = true;
                }
                else
                {
                    chkRow.Checked = false;
                }
            }
        }

        //Poner en modo edición
        protected void gridUsuarios_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gridUsuarios.EditIndex = e.NewEditIndex;
            bindData();
        }

        //Cancelar el modo edición
        protected void gridUsuarios_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            e.Cancel = true;
            gridUsuarios.EditIndex = -1;
            bindData();
        }

        //Confirmar actualizar datos
        protected void gridUsuarios_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = gridUsuarios.Rows[e.RowIndex];
            TextBox txtIdUser = (TextBox)row.FindControl("txtEditIdUser");
            TextBox txtNombreUser = (TextBox)row.FindControl("txtEditNombreUser");
            TextBox txtPassUser = (TextBox)row.FindControl("txtEditPassword");
            int idUser = Convert.ToInt32(txtIdUser.Text.Trim());
            string nombreRolUser = (gridUsuarios.Rows[e.RowIndex].FindControl("ddlEditRol") as DropDownList).SelectedItem.Value;
            string nombreUser = txtNombreUser.Text.Trim();
            string passUser = txtPassUser.Text.Trim();

            bool retorno = UsuarioLN.getInstance().updateUsuario(idUser,nombreUser, passUser, 
                nombreRolUser, Session["schema"].ToString());
            if (retorno)
            {
                Response.Write(@"<script language='javascript'>alert('Usuario actualizado correctamente.');</script>");
                Response.Redirect(Request.Url.AbsoluteUri);
            }
        }

        //Cargar el DropDownList de Roles en modo edición
        protected void gridUsuarios_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow && gridUsuarios.EditIndex == e.Row.RowIndex)
            {
                DropDownList ddlRoles = (DropDownList)e.Row.FindControl("ddlEditRol");
                List<Rol> listaRoles = RolLN.getInstance().listaRoles(Session["schema"].ToString());
                foreach(Rol rol in listaRoles)
                {
                    ListItem newItem = new ListItem(rol.nombre, rol.id.ToString(), true);
                    ddlRoles.Items.Add(newItem);
                }
                ddlRoles.DataBind();
                ddlRoles.Items.FindByValue((e.Row.FindControl("lblRol") as Label).Text).Selected = true;
            }
        }

        protected void BtnAdminSucursales_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Views/ABM/AdminUsuarioSucursal.aspx");
        }
    }
}