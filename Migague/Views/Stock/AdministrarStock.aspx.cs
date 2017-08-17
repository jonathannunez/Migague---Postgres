using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaLogicaNegocio;

namespace Migague.Views.Stock
{
    public partial class AdministrarStock : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                TienePermiso();
                
            }
            BindData();
        }

        protected void BindData()
        {
            int id_sucursal = Convert.ToInt32(Session["userSucursal"].ToString());
            DataTable listaStock = StockLN.getInstance().listaStock(Convert.ToInt32(Session["userSucursal"].ToString()),
                Session["schema"].ToString());
            
            gridStock.DataSource = listaStock;
            gridStock.DataBind();
        }

        protected void CargarSucursales()
        {
            /* DataTable listaSucursales = UsuarioLN.getInstance().(Session["schema"].ToString());
             foreach (DataRow row in listaRoles.Rows)
             {
                 ListItem newItem = new ListItem(row["nombre"].ToString(), row["id"].ToString(), true);
                 ddlRoles.Items.Add(newItem);
             }*/
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
            CheckBox chkSelectAll = (CheckBox)gridStock.HeaderRow.FindControl("chkDeleteAll");

            foreach (GridViewRow row in gridStock.Rows)
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
        protected void gridStock_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = gridStock.Rows[e.RowIndex];
            TextBox txtId = (TextBox)row.FindControl("txtEditId");
            TextBox txtPreciomay = (TextBox)row.FindControl("txtEditPreciomay");
            TextBox txtPreciomin = (TextBox)row.FindControl("txtEditPreciomin");
            int idmodelo = Convert.ToInt32((gridStock.Rows[e.RowIndex].FindControl("ddlModelos") as DropDownList).SelectedItem.Value);
            int idtalle = Convert.ToInt32((gridStock.Rows[e.RowIndex].FindControl("ddlTalles") as DropDownList).SelectedItem.Value);
            int idcolor = Convert.ToInt32((gridStock.Rows[e.RowIndex].FindControl("ddlColores") as DropDownList).SelectedItem.Value);
            int id = Convert.ToInt32(txtId.Text.Trim());
            int preciomay = Convert.ToInt32(txtPreciomay.Text.Trim());
            int preciomin = Convert.ToInt32(txtPreciomin.Text.Trim());
            
            /*
            bool retorno = AdminLN.getInstance().updateStock(id, idmodelo, idtalle, idcolor, preciomay, preciomin,
                "", Session["schema"].ToString());
            if (retorno)
            {
                Response.Write(@"<script language='javascript'>alert('Stock actualizado correctamente.');</script>");
                Response.Redirect(Request.Url.AbsoluteUri);
            }
            */
        }

        // Poner en modo edición
        protected void gridStock_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gridStock.EditIndex = e.NewEditIndex;
            BindData();
        }

        // Cancelar el modo edición
        protected void gridStock_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            e.Cancel = true;
            gridStock.EditIndex = -1;
            BindData();
        }

        // Agregar nuevo objeto
        protected void BtnAddNew_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Views/Stock/NuevoStock.aspx");
        }

        // Eliminar seleccionados
        protected void BtnDelete_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow row in gridStock.Rows)
            {
                CheckBox chk = (CheckBox)row.FindControl("chkDelete");
                if (chk.Checked)
                {
                    TextBox txtId = (TextBox)row.FindControl("txtId");
                    int id = Convert.ToInt32(txtId.Text.Trim());
                    /*
                    bool eliminados = AdminLN.getInstance().eliminarStock(id,
                        Session["schema"].ToString());
                    if (!eliminados)
                    {
                        Response.Write(@"<script language='javascript'>alert('Error al eliminar el Stock.');</script>");
                    }
                    */
                }

            }
            Response.Write(@"<script language='javascript'>alert('Eliminados');</script>");
            Response.Redirect(Request.Url.AbsoluteUri);
        }

        // cargar ddls en modo edicion
        protected void gridStock_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            /*
            if (e.Row.RowType == DataControlRowType.DataRow && gridStock.EditIndex == e.Row.RowIndex)
            {
                #region "cargar lista modelos"
                DropDownList ddlModelos = (DropDownList)e.Row.FindControl("ddlModelos");
                DataTable listaModelos = AdminLN.getInstance().listaModelos(Session["schema"].ToString());
                foreach (DataRow row in listaModelos.Rows)
                {
                    ListItem newItem = new ListItem(row["nombre"].ToString(), row["id"].ToString(), true);
                    ddlModelos.Items.Add(newItem);
                }
                ddlModelos.DataBind();
                ddlModelos.Items.FindByText((e.Row.FindControl("lblModelo") as Label).Text).Selected = true;
                #endregion

                #region "cargar lista talles"
                DropDownList ddlTalles = (DropDownList)e.Row.FindControl("ddlTalles");
                DataTable listaTalles = AdminLN.getInstance().listaTalles(Session["schema"].ToString());
                foreach (DataRow row in listaTalles.Rows)
                {
                    ListItem newItem = new ListItem(row["nombre"].ToString(), row["id"].ToString(), true);
                    ddlTalles.Items.Add(newItem);
                }
                ddlTalles.DataBind();
                ddlTalles.Items.FindByText((e.Row.FindControl("lblTalle") as Label).Text).Selected = true;
                #endregion

                #region "cargar lista colores"
                DropDownList ddlColores = (DropDownList)e.Row.FindControl("ddlColores");
                DataTable listaColores = AdminLN.getInstance().listaColores(Session["schema"].ToString());
                foreach (DataRow row in listaColores.Rows)
                {
                    ListItem newItem = new ListItem(row["nombre"].ToString(), row["id"].ToString(), true);
                    ddlColores.Items.Add(newItem);
                }
                ddlColores.DataBind();
                ddlColores.Items.FindByText((e.Row.FindControl("lblColor") as Label).Text).Selected = true;
                #endregion
               
            } */
        }

        protected void ddlSucursales_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}