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
    public partial class AdministrarPaises : BasePage
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
            gridPaises.DataSource = listaPaises;
            gridPaises.DataBind();
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
            CheckBox chkSelectAll = (CheckBox)gridPaises.HeaderRow.FindControl("chkDeleteAll");

            foreach (GridViewRow row in gridPaises.Rows)
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
        protected void gridPaises_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = gridPaises.Rows[e.RowIndex];
            TextBox txtId = (TextBox)row.FindControl("txtEditId");
            TextBox txtNombre = (TextBox)row.FindControl("txtEditNombre");
            TextBox txtFecha =  (TextBox)row.FindControl("txtEditFecha");
            int id = Convert.ToInt32(txtId.Text.Trim());
            string nombre = txtNombre.Text.Trim();
            DateTime fecha = Convert.ToDateTime(txtFecha.Text.Trim());

            bool retorno = PaisLN.getInstance().updatePais(id, nombre, fecha, Session["schema"].ToString());
            if (retorno)
            {
                Response.Write(@"<script language='javascript'>alert('Pais actualizado correctamente.');</script>");
                Response.Redirect(Request.Url.AbsoluteUri);
            }
        }

        // Poner en modo edición
        protected void gridPaises_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gridPaises.EditIndex = e.NewEditIndex;
            BindData();
        }

        // Cancelar el modo edición
        protected void gridPaises_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            e.Cancel = true;
            gridPaises.EditIndex = -1;
            BindData();
        }

        // Agregar nuevo objeto
        protected void BtnAddNew_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Views/ABM/AdminNuevoPais.aspx");
        }

        // Eliminar seleccionados
        protected void BtnDelete_Click(object sender, EventArgs e)
        {
            int count = 0;
            foreach (GridViewRow row in gridPaises.Rows)
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
                    bool eliminados = PaisLN.getInstance().eliminarPais(id, nombre,
                        Session["schema"].ToString());
                    if (!eliminados)
                    {
                        Response.Write(@"<script language='javascript'>alert('Error al eliminar el pais "
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