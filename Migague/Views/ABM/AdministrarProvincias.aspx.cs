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
    public partial class AdministrarProvincias : BasePage
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
            List<Pais> listPaises = PaisLN.getInstance().listaPaises(Session["schema"].ToString());
            foreach(Pais pais in listPaises)
            {
                ListItem newItem = new ListItem(pais.nombre, pais.id.ToString(), true);
                ddlPaises.Items.Add(newItem);
            }
            
            reloadData();
        }

        protected void reloadData()
        {
            List<Provincia> listaProvincias = ProvinciaLN.getInstance().listaProvincias(Session["schema"].ToString());
            int id_pais = Convert.ToInt32(ddlPaises.SelectedValue.ToString());
            List<Provincia> listaFiltrada = listaProvincias.Where(item => item.pais.id == id_pais).ToList();
            gridProvincias.DataSource = listaFiltrada;
            gridProvincias.DataBind();
            //int id_pais = Convert.ToInt32(ddlPaises.SelectedValue.ToString());
            /*List<Provincia> listaProvincias = ProvinciaLN.getInstance().listaProvincias(id_pais, Session["schema"].ToString());
            gridProvincias.DataSource = listaProvincias;
            gridProvincias.DataBind();*/
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
            CheckBox chkSelectAll = (CheckBox)gridProvincias.HeaderRow.FindControl("chkDeleteAll");

            foreach (GridViewRow row in gridProvincias.Rows)
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
        protected void gridProvincias_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = gridProvincias.Rows[e.RowIndex];
            TextBox txtId = (TextBox)row.FindControl("txtEditId");
            TextBox txtNombre = (TextBox)row.FindControl("txtEditNombre");
            TextBox txtFecha =  (TextBox)row.FindControl("txtEditFecha");
            int id = Convert.ToInt32(txtId.Text.Trim());
            string nombre = txtNombre.Text.Trim();
            DateTime fecha = Convert.ToDateTime(txtFecha.Text.Trim());

            bool retorno = ProvinciaLN.getInstance().updateProvincia(id, nombre, fecha, Session["schema"].ToString());
            if (retorno)
            {
                Response.Write(@"<script language='javascript'>alert('Provincia actualizada correctamente.');</script>");
                Response.Redirect(Request.Url.AbsoluteUri);
            }
        }

        // Poner en modo edición
        protected void gridProvincias_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gridProvincias.EditIndex = e.NewEditIndex;
            reloadData();
        }

        // Cancelar el modo edición
        protected void gridProvincias_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            e.Cancel = true;
            gridProvincias.EditIndex = -1;
            reloadData();
        }

        // Agregar nuevo objeto
        protected void BtnAddNew_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Views/ABM/AdminNuevaProvincia.aspx");
        }

        // Eliminar seleccionados
        protected void BtnDelete_Click(object sender, EventArgs e)
        {
            int count = 0;
            foreach (GridViewRow row in gridProvincias.Rows)
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
                    bool eliminados = ProvinciaLN.getInstance().eliminarProvincia(id,
                        Session["schema"].ToString());
                    if (!eliminados)
                    {
                        Response.Write(@"<script language='javascript'>alert('Error al eliminar la provincia "
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

        protected void ddlProvincias_SelectedIndexChanged(object sender, EventArgs e)
        {
            reloadData();
        }
    }
}