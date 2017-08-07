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
    public partial class AdministrarClientes : BasePage
    {
        static List<Cliente> listaClientes = new List<Cliente>();
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
            listaClientes = ClienteLN.getInstance().listaClientes(Session["schema"].ToString());
            gridClientes.DataSource = listaClientes;
            gridClientes.DataBind();
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
            CheckBox chkSelectAll = (CheckBox)gridClientes.HeaderRow.FindControl("chkDeleteAll");

            foreach (GridViewRow row in gridClientes.Rows)
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
        protected void gridClientes_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = gridClientes.Rows[e.RowIndex];
            TextBox txtId = (TextBox)row.FindControl("txtEditId");
            TextBox txtRazonSocial = (TextBox)row.FindControl("txtEditRazonSocial"); 
            TextBox txtNombre = (TextBox)row.FindControl("txtEditNombre");
            TextBox txtCuit = (TextBox)row.FindControl("txtEditCuit");
            TextBox txtMail = (TextBox)row.FindControl("txtEditMail");
            TextBox txtFecha = (TextBox)row.FindControl("txtEditFecha");
            int idcattributaria = Convert.ToInt32((gridClientes.Rows[e.RowIndex].FindControl("ddlCatTributaria") as DropDownList).SelectedItem.Value);
            int idcatlistaprecio = Convert.ToInt32((gridClientes.Rows[e.RowIndex].FindControl("ddlCatListaPrecio") as DropDownList).SelectedItem.Value);
            int id = Convert.ToInt32(txtId.Text.Trim());
            string cuit = txtCuit.Text.Trim();
            string razonsocial = txtRazonSocial.Text.Trim();
            string mail = txtMail.Text.Trim();
            string nombre = txtNombre.Text.Trim();
            string calle = txtRazonSocial.Text.Trim();
            DateTime fecha = Convert.ToDateTime(txtFecha.Text.Trim());

            bool retorno = ClienteLN.getInstance().updateCliente(id, razonsocial,nombre,cuit,fecha,mail,idcattributaria,
                idcatlistaprecio, Session["schema"].ToString());
            if (retorno)
            {
                Response.Write(@"<script language='javascript'>alert('Cliente actualizado correctamente.');</script>");
                Response.Redirect(Request.Url.AbsoluteUri);
            }
        }

        // Poner en modo edición
        protected void gridClientes_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gridClientes.EditIndex = e.NewEditIndex;
            BindData();
        }

        // Cancelar el modo edición
        protected void gridClientes_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            e.Cancel = true;
            gridClientes.EditIndex = -1;
            BindData();
        }

        // Agregar nuevo objeto
        protected void BtnAddNew_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Views/ABM/AdminNuevoCliente.aspx");
        }

        // Eliminar seleccionados
        protected void BtnDelete_Click(object sender, EventArgs e)
        {
            int count = 0;
            foreach (GridViewRow row in gridClientes.Rows)
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
                    bool eliminados = ClienteLN.getInstance().eliminarCliente(id,
                        Session["schema"].ToString());
                    if (!eliminados)
                    {
                        Response.Write(@"<script language='javascript'>alert('Error al eliminar la Cliente "
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

        protected void gridClientes_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow && gridClientes.EditIndex == e.Row.RowIndex)
            {
                #region "cargar lista categorias tributarias"
                DropDownList ddlCatTributaria = (DropDownList)e.Row.FindControl("ddlCatTributaria");
                List<CategoriaTributaria> listaCategoriasTributarias = AdminLN.getInstance().listaCategoriasTributarias(Session["schema"].ToString());
                foreach (CategoriaTributaria categoriaTributaria in listaCategoriasTributarias)
                {
                    ListItem newItem = new ListItem(categoriaTributaria.nombre, categoriaTributaria.id.ToString(), true);
                    ddlCatTributaria.Items.Add(newItem);
                }
                ddlCatTributaria.DataBind();
                ddlCatTributaria.Items.FindByText((e.Row.FindControl("lblCatTributaria") as Label).Text).Selected = true;
                #endregion

                #region "cargar lista categorias listas precios"
                DropDownList ddlCatListaPrecios = (DropDownList)e.Row.FindControl("ddlCatListaPrecio");
                List<CategoriaPrecios> listaCategoriaPrecios = CategoriaPreciosLN.getInstance().listaCategoriasPrecios(Session["schema"].ToString());
                foreach (CategoriaPrecios categoriaPrecios in listaCategoriaPrecios)
                {
                    ListItem newItem = new ListItem(categoriaPrecios.nombre, categoriaPrecios.id.ToString(), true);
                    ddlCatListaPrecios.Items.Add(newItem);
                }
                ddlCatListaPrecios.DataBind();
                ddlCatListaPrecios.Items.FindByText((e.Row.FindControl("lblCatListaPrecio") as Label).Text).Selected = true;
                #endregion
            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int id_cliente = Convert.ToInt32(gridClientes.DataKeys[e.Row.RowIndex].Value.ToString());
                List<Cliente> listaFiltrada = listaClientes.Where(item => item.id == id_cliente).ToList();
                Cliente clienteSeleccionado = listaFiltrada[0];

                GridView gvTelefonos = e.Row.FindControl("gvTelefonos") as GridView;
                gvTelefonos.DataSource = clienteSeleccionado.telefonos;
                gvTelefonos.DataBind();

                GridView gvDirecciones = e.Row.FindControl("gvDirecciones") as GridView;
                gvDirecciones.DataSource = clienteSeleccionado.direcciones;
                gvDirecciones.DataBind();

                GridView gvTransportes = e.Row.FindControl("gvTransportes") as GridView;
                gvTransportes.DataSource = clienteSeleccionado.transportes;
                gvTransportes.DataBind();
            }
        }
    }
}