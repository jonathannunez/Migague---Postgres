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
    public partial class AdministrarTransportes : BasePage
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
            List<Transporte> listaTransportes = TransporteLN.getInstance().listaTransportes(Session["schema"].ToString());
            gridTransportes.DataSource = listaTransportes;
            gridTransportes.DataBind();
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
            CheckBox chkSelectAll = (CheckBox)gridTransportes.HeaderRow.FindControl("chkDeleteAll");

            foreach (GridViewRow row in gridTransportes.Rows)
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
        protected void gridTransportes_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = gridTransportes.Rows[e.RowIndex];
            TextBox txtId = (TextBox)row.FindControl("txtEditId");
            TextBox txtNombre = (TextBox)row.FindControl("txtEditNombre");
            TextBox txtFecha =  (TextBox)row.FindControl("txtEditFecha");
            int id = Convert.ToInt32(txtId.Text.Trim());
            string nombre = txtNombre.Text.Trim();
            DateTime fecha = Convert.ToDateTime(txtFecha.Text.Trim());

            bool retorno = TransporteLN.getInstance().updateTransporte(id, nombre, fecha, Session["schema"].ToString());
            if (retorno)
            {
                Response.Write(@"<script language='javascript'>alert('Transporte actualizado correctamente.');</script>");
                Response.Redirect(Request.Url.AbsoluteUri);
            }
            else
            {
                Response.Write(@"<script language='javascript'>alert('El transporte ya existe.');</script>");
                Response.Redirect(Request.Url.AbsoluteUri);
            }
        }

        // Poner en modo edición
        protected void gridTransportes_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gridTransportes.EditIndex = e.NewEditIndex;
            BindData();
        }

        // Cancelar el modo edición
        protected void gridTransportes_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            e.Cancel = true;
            gridTransportes.EditIndex = -1;
            BindData();
        }

        // Agregar nuevo objeto
        protected void BtnAddNew_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Views/ABM/AdminNuevoTransporte.aspx");
        }

        // Eliminar seleccionados
        protected void BtnDelete_Click(object sender, EventArgs e)
        {
            int count = 0;
            foreach (GridViewRow row in gridTransportes.Rows)
            {
                CheckBox chk = (CheckBox)row.FindControl("chkDelete");
                if (chk.Checked)
                {
                    TextBox txtId = (TextBox)row.FindControl("txtId");
                    TextBox txtNombre = (TextBox)row.FindControl("txtNombre");
                    TextBox txtFecha = (TextBox)row.FindControl("txtFecha");
                    int id = Convert.ToInt32(txtId.Text.Trim());
                    DateTime fecha = Convert.ToDateTime(txtFecha.Text.Trim());
                    bool eliminados = TransporteLN.getInstance().eliminarTransporte(id,
                        Session["schema"].ToString());
                    if (!eliminados)
                    {
                        Response.Write(@"<script language='javascript'>alert('Error al eliminar el Transporte .');</script>");
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