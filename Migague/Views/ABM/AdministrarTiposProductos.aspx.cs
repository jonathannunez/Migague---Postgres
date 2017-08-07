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
    public partial class AdministrarTiposProductos : BasePage
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
            List<TipoProducto> listaTiposProductos = TipoProductoLN.getInstance().listaTiposProductos(Session["schema"].ToString());
            gridTiposProductos.DataSource = listaTiposProductos;
            gridTiposProductos.DataBind();
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
            CheckBox chkSelectAll = (CheckBox)gridTiposProductos.HeaderRow.FindControl("chkDeleteAll");

            foreach (GridViewRow row in gridTiposProductos.Rows)
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
        protected void gridTiposProductos_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = gridTiposProductos.Rows[e.RowIndex];
            TextBox txtId = (TextBox)row.FindControl("txtEditId");
            TextBox txtNombre = (TextBox)row.FindControl("txtEditNombre");
            int idRol = Convert.ToInt32(txtId.Text.Trim());
            string nombreRol = txtNombre.Text.Trim();

            bool retorno = TipoProductoLN.getInstance().updateTipoProducto(idRol, nombreRol, Session["schema"].ToString());
            if (retorno)
            {
                Response.Write(@"<script language='javascript'>alert('TipoProducto actualizado correctamente.');</script>");
                Response.Redirect(Request.Url.AbsoluteUri);
            }
            else
            {
                Response.Write(@"<script language='javascript'>alert('Ya existe el tipo de producto.');</script>");
                Response.Redirect(Request.Url.AbsoluteUri);
            }
        }

        // Poner en modo edición
        protected void gridTiposProductos_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gridTiposProductos.EditIndex = e.NewEditIndex;
            BindData();
        }

        // Cancelar el modo edición
        protected void gridTiposProductos_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            e.Cancel = true;
            gridTiposProductos.EditIndex = -1;
            BindData();
        }

        // Agregar nuevo objeto
        protected void BtnAddNew_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Views/ABM/AdminNuevoTipoProducto.aspx");
        }

        // Eliminar seleccionados
        protected void BtnDelete_Click(object sender, EventArgs e)
        {
            int count = 0;
            foreach (GridViewRow row in gridTiposProductos.Rows)
            {
                CheckBox chk = (CheckBox)row.FindControl("chkDelete");
                if (chk.Checked)
                {
                    TextBox txtId = (TextBox)row.FindControl("txtId");
                    TextBox txtNombre = (TextBox)row.FindControl("txtNombre");
                    int id = Convert.ToInt32(txtId.Text.Trim());
                    string nombre = txtNombre.Text.Trim();
                    bool eliminados = TipoProductoLN.getInstance().eliminarTipoProducto(id,
                        Session["schema"].ToString());
                    if (!eliminados)
                    {
                        Response.Write(@"<script language='javascript'>alert('Error al eliminar el rol "
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