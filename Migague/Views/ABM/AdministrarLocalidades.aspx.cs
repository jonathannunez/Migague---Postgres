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
    public partial class AdministrarLocalidades : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                TienePermiso();
                BindData();
            }
        }

        protected void CargarPaises()
        {
            List<Pais> listaPaises = PaisLN.getInstance().listaPaises(Session["schema"].ToString());
            foreach (Pais pais in listaPaises)
            {
                ListItem newItem = new ListItem(pais.nombre, pais.id.ToString(), true);
                ddlPaises.Items.Add(newItem);
            }
        }

        protected void CargarProvincias()
        {
            ddlProvincias.Items.Clear();
            List<Provincia> listaProvincias = ProvinciaLN.getInstance().listaProvincias(Session["schema"].ToString());
            int id_pais = Convert.ToInt32(ddlPaises.SelectedValue.ToString());
            List<Provincia> listaFiltrada = listaProvincias.Where(item => item.pais.id == id_pais).ToList();
            foreach (Provincia provincia in listaFiltrada)
            {
                ListItem newItem = new ListItem(provincia.nombre, provincia.id.ToString(), true);
                ddlProvincias.Items.Add(newItem);
            }
        }

        protected void BindData()
        {
            CargarPaises();
            CargarProvincias();
            reloadData();
        }

        protected void reloadData()
        {
            List<Localidad> listLocalidades = LocalidadLN.getInstance().listaLocalidades(Session["schema"].ToString());
            int id_provincia = Convert.ToInt32(ddlProvincias.SelectedValue.ToString());
            List<Localidad> listaFiltrada = listLocalidades.Where(item => item.provincia.id == id_provincia).ToList();
            gridLocalidades.DataSource = listaFiltrada;
            gridLocalidades.DataBind();
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
            CheckBox chkSelectAll = (CheckBox)gridLocalidades.HeaderRow.FindControl("chkDeleteAll");

            foreach (GridViewRow row in gridLocalidades.Rows)
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
        protected void gridLocalidades_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = gridLocalidades.Rows[e.RowIndex];
            TextBox txtId = (TextBox)row.FindControl("txtEditId");
            TextBox txtNombre = (TextBox)row.FindControl("txtEditNombre");
            TextBox txtFecha =  (TextBox)row.FindControl("txtEditFecha");
            int id = Convert.ToInt32(txtId.Text.Trim());
            string nombre = txtNombre.Text.Trim();
            DateTime fecha = Convert.ToDateTime(txtFecha.Text.Trim());

            bool retorno = LocalidadLN.getInstance().updateLocalidad(id, nombre, fecha, Session["schema"].ToString());
            if (retorno)
            {
                Response.Write(@"<script language='javascript'>alert('Localidad actualizada correctamente.');</script>");
                Response.Redirect(Request.Url.AbsoluteUri);
            }
        }

        // Poner en modo edición
        protected void gridLocalidades_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gridLocalidades.EditIndex = e.NewEditIndex;
            reloadData();
        }

        // Cancelar el modo edición
        protected void gridLocalidades_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            e.Cancel = true;
            gridLocalidades.EditIndex = -1;
            reloadData();
        }

        // Agregar nuevo objeto
        protected void BtnAddNew_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Views/ABM/AdminNuevaLocalidad.aspx");
        }

        // Eliminar seleccionados
        protected void BtnDelete_Click(object sender, EventArgs e)
        {
            int count = 0;
            foreach (GridViewRow row in gridLocalidades.Rows)
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
                    bool eliminados = LocalidadLN.getInstance().eliminarLocalidad(id,
                        Session["schema"].ToString());
                    if (!eliminados)
                    {
                        Response.Write(@"<script language='javascript'>alert('Error al eliminar la Localidad "
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

        protected void ddlLocalidades_SelectedIndexChanged(object sender, EventArgs e)
        {
            reloadData();
        }

        protected void ddlPaises_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarProvincias();
            reloadData();
        }

        protected void ddlProvincias_SelectedIndexChanged(object sender, EventArgs e)
        {
            reloadData();
        }
    }
}