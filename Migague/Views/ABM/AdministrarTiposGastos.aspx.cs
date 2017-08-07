using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaLogicaNegocio;
using CapaEntidades;

namespace Migague.Views.ABM
{
    public partial class AdministrarTiposGastos : BasePage
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
            List<TipoGasto> listaTiposGastos = TipoGastoLN.getInstance().listaTiposGastos(Session["schema"].ToString());
            gridTiposGastos.DataSource = listaTiposGastos;
            gridTiposGastos.DataBind();
        }

        // Para chequear una fila
        protected void chkDelete_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chkStatus = (CheckBox)sender;
            GridViewRow row = (GridViewRow)chkStatus.NamingContainer;
        }

        // Para chequear todas las filas
        protected void chkDeleteAll_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chkSelectAll = (CheckBox)gridTiposGastos.HeaderRow.FindControl("chkDeleteAll");

            foreach (GridViewRow row in gridTiposGastos.Rows)
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

        // Aceptar actualizar datos
        protected void gridTiposGastos_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = gridTiposGastos.Rows[e.RowIndex];
            TextBox txtId = (TextBox)row.FindControl("txtEditId");
            TextBox txtNombre = (TextBox)row.FindControl("txtEditNombre");
            int idRol = Convert.ToInt32(txtId.Text.Trim());
            string nombreRol = txtNombre.Text.Trim();

            bool retorno = TipoGastoLN.getInstance().updateTipoGasto(idRol, nombreRol, Session["schema"].ToString());
            if (retorno)
            {
                Response.Write(@"<script language='javascript'>alert('Tipo de gasto actualizado correctamente.');</script>");
                Response.Redirect(Request.Url.AbsoluteUri);
            }
            else
            {
                Response.Write(@"<script language='javascript'>alert('El tipo de gasto ya existe.');</script>");
                Response.Redirect(Request.Url.AbsoluteUri);
            }
        }

        // Poner en modo edición
        protected void gridTiposGastos_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gridTiposGastos.EditIndex = e.NewEditIndex;
            BindData();
        }

        // Cancelar el modo edición
        protected void gridTiposGastos_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            e.Cancel = true;
            gridTiposGastos.EditIndex = -1;
            BindData();
        }

        // Agregar nuevo objeto
        protected void BtnAddNew_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Views/ABM/AdminNuevoTipoGasto.aspx");
        }

        // Eliminar seleccionados
        protected void BtnDelete_Click(object sender, EventArgs e)
        {
            int count = 0;
            foreach (GridViewRow row in gridTiposGastos.Rows)
            {
                CheckBox chk = (CheckBox)row.FindControl("chkDelete");
                if (chk.Checked)
                {
                    TextBox txtId = (TextBox)row.FindControl("txtId");
                    TextBox txtNombre = (TextBox)row.FindControl("txtNombre");
                    int id = Convert.ToInt32(txtId.Text.Trim());
                    string nombre = txtNombre.Text.Trim();
                    bool eliminados = TipoGastoLN.getInstance().eliminarTipoGasto(id,
                        Session["schema"].ToString());
                    if (!eliminados)
                    {
                        Response.Write(@"<script language='javascript'>alert('Error al eliminar la categoría de precio "
                                            + nombre + " .');</script>");
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