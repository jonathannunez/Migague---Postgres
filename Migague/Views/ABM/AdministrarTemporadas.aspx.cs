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
    public partial class AdministrarTemporadas : BasePage
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
            List<Temporada> listaTemporadas = TemporadaLN.getInstance().listaTemporadas(Session["schema"].ToString());
            gridTemporadas.DataSource = listaTemporadas;
            gridTemporadas.DataBind();
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
            CheckBox chkSelectAll = (CheckBox)gridTemporadas.HeaderRow.FindControl("chkDeleteAll");

            foreach (GridViewRow row in gridTemporadas.Rows)
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
        protected void gridTemporadas_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = gridTemporadas.Rows[e.RowIndex];
            TextBox txtId = (TextBox)row.FindControl("txtEditId");
            TextBox txtAno = (TextBox)row.FindControl("txtEditAno");
            TextBox txtEstacion = (TextBox)row.FindControl("txtEditEstacion");
            int id = Convert.ToInt32(txtId.Text.Trim());
            string ano = txtAno.Text.Trim();
            string estacion = txtEstacion.Text.Trim();

            bool retorno = TemporadaLN.getInstance().updateTemporada(id, ano,estacion , Session["schema"].ToString());
            if (retorno)
            {
                Response.Write(@"<script language='javascript'>alert('Temporada actualizada correctamente.');</script>");
                Response.Redirect(Request.Url.AbsoluteUri);
            }
            else
            {
                Response.Write(@"<script language='javascript'>alert('Ya existe esa temporada correctamente.');</script>");
                Response.Redirect(Request.Url.AbsoluteUri);
            }
        }

        // Poner en modo edición
        protected void gridTemporadas_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gridTemporadas.EditIndex = e.NewEditIndex;
            BindData();
        }

        // Cancelar el modo edición
        protected void gridTemporadas_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            e.Cancel = true;
            gridTemporadas.EditIndex = -1;
            BindData();
        }

        // Agregar nuevo objeto
        protected void BtnAddNew_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Views/ABM/AdminNuevaTemporada.aspx");
        }

        // Eliminar seleccionados
        protected void BtnDelete_Click(object sender, EventArgs e)
        {
            int count = 0;
            foreach (GridViewRow row in gridTemporadas.Rows)
            {
                CheckBox chk = (CheckBox)row.FindControl("chkDelete");
                if (chk.Checked)
                {
                    TextBox txtId = (TextBox)row.FindControl("txtId");
                    TextBox txtAno = (TextBox)row.FindControl("txtAno");
                    TextBox txtEstacion = (TextBox)row.FindControl("txtEstacion");
                    int id = Convert.ToInt32(txtId.Text.Trim());
                    string ano = txtAno.Text.Trim();
                    string estacion = txtEstacion.Text.Trim();
                    bool eliminados = TemporadaLN.getInstance().eliminarTemporada(id,
                        Session["schema"].ToString());
                    if (!eliminados)
                    {
                        Response.Write(@"<script language='javascript'>alert('Error al eliminar la temporada "
                                            + ano + " " + estacion +  " .');</script>");
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