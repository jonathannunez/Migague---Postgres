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
    public partial class AdministrarModelos : BasePage
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
            List<Modelo> listaModelos = ModeloLN.getInstance().listaModelos(Session["schema"].ToString());
            gridModelos.DataSource = listaModelos;
            gridModelos.DataBind();
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
            CheckBox chkSelectAll = (CheckBox)gridModelos.HeaderRow.FindControl("chkDeleteAll");

            foreach (GridViewRow row in gridModelos.Rows)
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
        protected void gridModelos_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = gridModelos.Rows[e.RowIndex];
            TextBox txtId = (TextBox)row.FindControl("txtEditId");
            TextBox txtCodigo = (TextBox)row.FindControl("txtEditCodigo");
            TextBox txtNombre = (TextBox)row.FindControl("txtEditNombre");
            TextBox txtFecha = (TextBox)row.FindControl("txtEditFecha");
            int idproveedor = Convert.ToInt32((gridModelos.Rows[e.RowIndex].FindControl("ddlProveedores") as DropDownList).SelectedItem.Value);
            int idtemporada = Convert.ToInt32((gridModelos.Rows[e.RowIndex].FindControl("ddlTemporadas") as DropDownList).SelectedItem.Value);
            int idtipoproducto = Convert.ToInt32((gridModelos.Rows[e.RowIndex].FindControl("ddlTiposProductos") as DropDownList).SelectedItem.Value);
            int id = Convert.ToInt32(txtId.Text.Trim());
            string codigo = txtCodigo.Text.Trim();
            string nombre = txtNombre.Text.Trim();
            DateTime fecha = Convert.ToDateTime(txtFecha.Text.Trim());
            
            bool retorno = ModeloLN.getInstance().updateModelo(id,codigo,nombre,idproveedor,idtemporada,idtipoproducto,
                fecha, Session["schema"].ToString());
            if (retorno)
            {
                Response.Write(@"<script language='javascript'>alert('Modelo actualizado correctamente.');</script>");
                Response.Redirect(Request.Url.AbsoluteUri);
            }
            
        }

        // Poner en modo edición
        protected void gridModelos_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gridModelos.EditIndex = e.NewEditIndex;
            BindData();
        }

        // Cancelar el modo edición
        protected void gridModelos_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            e.Cancel = true;
            gridModelos.EditIndex = -1;
            BindData();
        }

        // Agregar nuevo objeto
        protected void BtnAddNew_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Views/ABM/AdminNuevoModelo.aspx");
        }

        // Eliminar seleccionados
        protected void BtnDelete_Click(object sender, EventArgs e)
        {
            int count = 0;
            foreach (GridViewRow row in gridModelos.Rows)
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
                    bool eliminados = ModeloLN.getInstance().eliminarModelo(id,
                        Session["schema"].ToString());
                    if (!eliminados)
                    {
                        Response.Write(@"<script language='javascript'>alert('Error al eliminar el Modelo "
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

        // cargar ddls en modo edicion
        protected void gridModelos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow && gridModelos.EditIndex == e.Row.RowIndex)
            {
                #region "cargar lista proveedores"
                DropDownList ddlproveedores = (DropDownList)e.Row.FindControl("ddlProveedores");
                List<Proveedor> listaProveedores = ProveedorLN.getInstance().listaProveedores(Session["schema"].ToString());
                foreach (Proveedor proveedor in listaProveedores)
                {
                    ListItem newItem = new ListItem(proveedor.nombre, proveedor.id.ToString(), true);
                    ddlproveedores.Items.Add(newItem);
                }
                ddlproveedores.DataBind();
                ddlproveedores.Items.FindByText((e.Row.FindControl("lblProveedor") as Label).Text).Selected = true;
                #endregion

                #region "cargar lista temporadas"
                DropDownList ddlTemporadas = (DropDownList)e.Row.FindControl("ddlTemporadas");
                List<Temporada> listaTemporadas = TemporadaLN.getInstance().listaTemporadas(Session["schema"].ToString());
                foreach (Temporada temporada in listaTemporadas)
                {
                    ListItem newItem = new ListItem(temporada.nombre, temporada.id.ToString(), true);
                    ddlTemporadas.Items.Add(newItem);
                }
                ddlTemporadas.DataBind();
                ddlTemporadas.Items.FindByText((e.Row.FindControl("lblTemporada") as Label).Text).Selected = true;
                #endregion

                #region "cargar lista tipos productos"
                DropDownList ddlTiposProductos = (DropDownList)e.Row.FindControl("ddlTiposProductos");
                List<TipoProducto> listaTiposProductos = TipoProductoLN.getInstance().listaTiposProductos(Session["schema"].ToString());
                foreach (TipoProducto tipoProducto in listaTiposProductos)
                {
                    ListItem newItem = new ListItem(tipoProducto.nombre, tipoProducto.id.ToString(), true);
                    ddlTiposProductos.Items.Add(newItem);
                }
                ddlTiposProductos.DataBind();
                ddlTiposProductos.Items.FindByText((e.Row.FindControl("lblTipoProducto") as Label).Text).Selected = true;
                #endregion

            }
        }
        
    }
}