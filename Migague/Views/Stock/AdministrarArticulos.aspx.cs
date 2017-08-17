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
    public partial class AdministrarArticulos : BasePage
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
            List<Articulo> listaArticulos = ArticuloLN.getInstance().listaArticulos(Session["schema"].ToString());
            gridArticulos.DataSource = listaArticulos;
            gridArticulos.DataBind();
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
            CheckBox chkSelectAll = (CheckBox)gridArticulos.HeaderRow.FindControl("chkDeleteAll");

            foreach (GridViewRow row in gridArticulos.Rows)
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
        protected void gridArticulos_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = gridArticulos.Rows[e.RowIndex];
            TextBox txtId = (TextBox)row.FindControl("txtEditId");
            TextBox txtPreciomay = (TextBox)row.FindControl("txtEditPreciomay");
            TextBox txtPreciomin = (TextBox)row.FindControl("txtEditPreciomin");
            int idmodelo = Convert.ToInt32((gridArticulos.Rows[e.RowIndex].FindControl("ddlModelos") as DropDownList).SelectedItem.Value);
            string modelo = (gridArticulos.Rows[e.RowIndex].FindControl("ddlModelos") as DropDownList).SelectedItem.Text;  
            int idtalle = Convert.ToInt32((gridArticulos.Rows[e.RowIndex].FindControl("ddlTalles") as DropDownList).SelectedItem.Value);
            string color = (gridArticulos.Rows[e.RowIndex].FindControl("ddlColores") as DropDownList).SelectedItem.Text;
            int idcolor = Convert.ToInt32((gridArticulos.Rows[e.RowIndex].FindControl("ddlColores") as DropDownList).SelectedItem.Value);
            string talle = (gridArticulos.Rows[e.RowIndex].FindControl("ddlTalles") as DropDownList).SelectedItem.Text;
            int id = Convert.ToInt32(txtId.Text.Trim());
            int preciomay = Convert.ToInt32(txtPreciomay.Text.Trim());
            int preciomin = Convert.ToInt32(txtPreciomin.Text.Trim());
            
            bool retorno = ArticuloLN.getInstance().updateArticulo(id, idmodelo, modelo, idtalle, talle, idcolor, color,
                preciomay, preciomin, "", Session["schema"].ToString());
            if (retorno)
            {
                Response.Write(@"<script language='javascript'>alert('Articulo actualizado correctamente.');</script>");
                Response.Redirect(Request.Url.AbsoluteUri);
            }
            
        }

        // Poner en modo edición
        protected void gridArticulos_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gridArticulos.EditIndex = e.NewEditIndex;
            BindData();
        }

        // Cancelar el modo edición
        protected void gridArticulos_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            e.Cancel = true;
            gridArticulos.EditIndex = -1;
            BindData();
        }

        // Agregar nuevo objeto
        protected void BtnAddNew_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Views/Stock/AdminNuevoArticulo.aspx");
        }

        // Eliminar seleccionados
        protected void BtnDelete_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow row in gridArticulos.Rows)
            {
                CheckBox chk = (CheckBox)row.FindControl("chkDelete");
                if (chk.Checked)
                {
                    TextBox txtId = (TextBox)row.FindControl("txtId");
                    int id = Convert.ToInt32(txtId.Text.Trim());
                    bool eliminados = ArticuloLN.getInstance().eliminarArticulo(id,
                        Session["schema"].ToString());
                    if (!eliminados)
                    {
                        Response.Write(@"<script language='javascript'>alert('Error al eliminar el Articulo.');</script>");
                    }
                }

            }
            Response.Write(@"<script language='javascript'>alert('Eliminados');</script>");
            Response.Redirect(Request.Url.AbsoluteUri);
        }

        // cargar ddls en modo edicion
        protected void gridArticulos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow && gridArticulos.EditIndex == e.Row.RowIndex)
            {
                #region "cargar lista modelos"
                DropDownList ddlModelos = (DropDownList)e.Row.FindControl("ddlModelos");
                List<Modelo> listaModelos = ModeloLN.getInstance().listaModelos(Session["schema"].ToString());
                foreach (Modelo modelo in listaModelos)
                {
                    ListItem newItem = new ListItem(modelo.nombre, modelo.id.ToString(), true);
                    ddlModelos.Items.Add(newItem);
                }
                ddlModelos.DataBind();
                ddlModelos.Items.FindByText((e.Row.FindControl("lblModelo") as Label).Text).Selected = true;
                #endregion

                #region "cargar lista talles"
                DropDownList ddlTalles = (DropDownList)e.Row.FindControl("ddlTalles");
                List<Talle> listaTalles = TalleLN.getInstance().listaTalles(Session["schema"].ToString());
                foreach (Talle talle in listaTalles)
                {
                    ListItem newItem = new ListItem(talle.nombre, talle.id.ToString(), true);
                    ddlTalles.Items.Add(newItem);
                }
                ddlTalles.DataBind();
                ddlTalles.Items.FindByText((e.Row.FindControl("lblTalle") as Label).Text).Selected = true;
                #endregion

                
                #region "cargar lista colores"
                DropDownList ddlColores = (DropDownList)e.Row.FindControl("ddlColores");
                List<Color> listaColores = ColorLN.getInstance().listaColores(Session["schema"].ToString());
                foreach (Color color in listaColores)
                {
                    ListItem newItem = new ListItem(color.nombre, color.id.ToString(), true);
                    ddlColores.Items.Add(newItem);
                }
                ddlColores.DataBind();
                ddlColores.Items.FindByText((e.Row.FindControl("lblColor") as Label).Text).Selected = true;
                #endregion
    

            }
        }
        
    }
}