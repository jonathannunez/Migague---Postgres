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
    public partial class AdministrarSucursales : BasePage
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
            List<Sucursal> listaSucursales = SucursalLN.getInstance().listaSucursales(Session["schema"].ToString());
            gridSucursales.DataSource = listaSucursales;
            gridSucursales.DataBind();
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
            CheckBox chkSelectAll = (CheckBox)gridSucursales.HeaderRow.FindControl("chkDeleteAll");

            foreach (GridViewRow row in gridSucursales.Rows)
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
        protected void gridSucursales_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = gridSucursales.Rows[e.RowIndex];
            TextBox txtId = (TextBox)row.FindControl("txtEditId");
            TextBox txtNombre = (TextBox)row.FindControl("txtEditNombre");
            TextBox txtCalle = (TextBox)row.FindControl("txtEditCalle");
            TextBox txtAltura = (TextBox)row.FindControl("txtEditAltura");
            TextBox txtFecha = (TextBox)row.FindControl("txtEditFecha");
            int idlocalidad = Convert.ToInt32((gridSucursales.Rows[e.RowIndex].FindControl("ddlLocalidades") as DropDownList).SelectedItem.Value);
            int id = Convert.ToInt32(txtId.Text.Trim());
            int altura = Convert.ToInt32(txtAltura.Text.Trim());
            string nombre = txtNombre.Text.Trim();
            string calle = txtCalle.Text.Trim();
            DateTime fecha = Convert.ToDateTime(txtFecha.Text.Trim());

            bool retorno = SucursalLN.getInstance().updateSucursal(id, nombre, calle, altura ,fecha, idlocalidad,
                Session["schema"].ToString());
            if (retorno)
            {
                Response.Write(@"<script language='javascript'>alert('Sucursal actualizado correctamente.');</script>");
                Response.Redirect(Request.Url.AbsoluteUri);
            }
            else
            {
                Response.Write(@"<script language='javascript'>alert('Ya existe la sucursal.');</script>");
                Response.Redirect(Request.Url.AbsoluteUri);
            }
        }

        // Poner en modo edición
        protected void gridSucursales_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gridSucursales.EditIndex = e.NewEditIndex;
            BindData();
        }

        // Cancelar el modo edición
        protected void gridSucursales_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            e.Cancel = true;
            gridSucursales.EditIndex = -1;
            BindData();
        }

        // Agregar nuevo objeto
        protected void BtnAddNew_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Views/ABM/AdminNuevaSucursal.aspx");
        }

        // Eliminar seleccionados
        protected void BtnDelete_Click(object sender, EventArgs e)
        {
            int count = 0;
            foreach (GridViewRow row in gridSucursales.Rows)
            {
                CheckBox chk = (CheckBox)row.FindControl("chkDelete");
                if (chk.Checked)
                {
                    TextBox txtId = (TextBox)row.FindControl("txtId");
                    TextBox txtNombre = (TextBox)row.FindControl("txtNombre");
                    TextBox txtFecha = (TextBox)row.FindControl("txtFecha");
                    int id = Convert.ToInt32(txtId.Text.Trim());
                    string nombre = txtNombre.Text.Trim();
                    DateTime fecha = Convert.ToDateTime(txtFecha.Text.Trim());
                    bool eliminados = SucursalLN.getInstance().eliminarSucursal(id,
                        Session["schema"].ToString());
                    if (!eliminados)
                    {
                        Response.Write(@"<script language='javascript'>alert('Error al eliminar la Sucursal "
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

        protected void gridSucursales_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow && gridSucursales.EditIndex == e.Row.RowIndex)
            {
                #region "cargar lista sucursales"
                DropDownList ddlLocalidades = (DropDownList)e.Row.FindControl("ddlLocalidades");
                List<Localidad> listLocalidades = LocalidadLN.getInstance().listaLocalidades(Session["schema"].ToString());
                foreach (Localidad localidad in listLocalidades)
                {
                    ListItem newItem = new ListItem(localidad.nombre, localidad.id.ToString(), true);
                    ddlLocalidades.Items.Add(newItem);
                }
                ddlLocalidades.DataBind();
                ddlLocalidades.Items.FindByText((e.Row.FindControl("lblLocalidad") as Label).Text).Selected = true;
                #endregion
            }
        }
    }
}