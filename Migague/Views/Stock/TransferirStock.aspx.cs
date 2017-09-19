using CapaLogicaNegocio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaEntidades;
using System.Drawing;

namespace Migague.Views.ABM
{
    public partial class TransferirStock : BasePage
    {
        static Articulo articulo = new Articulo();
        static Stockcs stock = new Stockcs();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                TienePermiso();
                BindData();
            }

        }

        // bind data
        protected void BindData()
        {
            cargarSucursales();
            reloadData(Convert.ToInt32(ddlSucursalSalida.SelectedItem.Value));
            cargarSucursalesEntrada();
        }

        // cargar sucursales
        protected void cargarSucursales()
        {
            List<Sucursal> list = Session["sucursales"] as List<Sucursal>;
            foreach(Sucursal sucursal in list)
            {
                ListItem newItem = new ListItem(sucursal.nombre, sucursal.id.ToString(), true);
                ddlSucursalSalida.Items.Add(newItem);
                //ddlSucursalEntrada.Items.Add(newItem);
            }
        }

        protected void cargarSucursalesEntrada()
        {
            List<Sucursal> list = Session["sucursales"] as List<Sucursal>;
            foreach (Sucursal sucursal in list)
            {
                ListItem newItem = new ListItem(sucursal.nombre, sucursal.id.ToString(), true);
                if(sucursal.id != Convert.ToInt32(ddlSucursalSalida.SelectedItem.Value))
                {
                    ddlSucursalEntrada.Items.Add(newItem);
                }
            }
        }

        protected void reloadData(int id_sucursal)
        {
            List<Stockcs> listaStockSalida = StockLN.getInstance().listaStock2(id_sucursal, Session["schema"].ToString());
            gridArticulosSalida.DataSource = listaStockSalida;
            gridArticulosSalida.DataBind();
        }

        protected void ddlSucursalSalida_SelectedIndexChanged(object sender, EventArgs e)
        {
            reloadData(Convert.ToInt32(ddlSucursalSalida.SelectedItem.Value));
        }

        // cuando la fila esta seleccionada
        protected void gridArticulosSalida_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (GridViewRow row in gridArticulosSalida.Rows)
            {
                if (row.RowIndex == gridArticulosSalida.SelectedIndex)
                {
                    row.BackColor = ColorTranslator.FromHtml("#A1DCF2");
                    row.ToolTip = string.Empty;

                    #region Articulo
                    
                    TextBox txtvalues = (TextBox)row.FindControl("txtIDArticuloSalida");
                    articulo.id = Convert.ToInt32(txtvalues.Text.Trim());
                    txtvalues = (TextBox)row.FindControl("txtArticuloID");
                    articulo.modelo.id = Convert.ToInt32(txtvalues.Text.Trim());
                    txtvalues = (TextBox)row.FindControl("txtNombreArticuloSalida");
                    articulo.modelo.nombre = txtvalues.Text.Trim();
                    txtvalues = (TextBox)row.FindControl("txtColorID");
                    articulo.color.id = Convert.ToInt32(txtvalues.Text.Trim());
                    txtvalues = (TextBox)row.FindControl("txtColorArticuloSalida");
                    articulo.color.nombre = txtvalues.Text.Trim();
                    txtvalues = (TextBox)row.FindControl("txtTalleID");
                    articulo.talle.id = Convert.ToInt32(txtvalues.Text.Trim());
                    txtvalues = (TextBox)row.FindControl("txtTalleArticuloSalida");
                    articulo.talle.nombre = txtvalues.Text.Trim();
                    #endregion

                    
                    stock.articulo = articulo;
                    txtvalues = (TextBox)row.FindControl("txtCantidadArticuloSalida");
                    stock.cantidad_neta = Convert.ToInt32(txtvalues.Text.Trim());
                    txtvalues = (TextBox)row.FindControl("txtCantidadIngresada");
                    if(txtvalues.Text != "")
                    {
                        stock.cantidad = Convert.ToInt32(txtvalues.Text.Trim());
                    }
                    else
                    {
                        if(txtCantIngresada.Text.Trim()!= "")
                        {
                            stock.cantidad = Convert.ToInt32(txtCantIngresada.Text.Trim());
                        }
                    }
                }
                else
                {
                    row.BackColor = ColorTranslator.FromHtml("#FFFFFF");
                    row.ToolTip = "Click to select this row.";
                }
            }
        }

        // seleccionar una fila
        protected void gridArticulosSalida_DataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(gridArticulosSalida, "Select$" + e.Row.RowIndex);
                e.Row.ToolTip = "Click to select this row.";
            }
        }

        protected void ddlSucursalEntrada_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btnTransferir_Click(object sender, EventArgs e)
        {
            Sucursal sucursalEntrada = new Sucursal();
            sucursalEntrada.id = Convert.ToInt32(ddlSucursalEntrada.SelectedItem.Value);
            sucursalEntrada.nombre = ddlSucursalEntrada.SelectedItem.Text.Trim();

            Sucursal sucursalSalida = new Sucursal();
            sucursalSalida.id = Convert.ToInt32(ddlSucursalSalida.SelectedItem.Value);
            sucursalSalida.nombre = ddlSucursalSalida.SelectedItem.Text.Trim();

            stock.sucursal = sucursalSalida;
            StockLN.getInstance().transferenciaStock(stock, sucursalSalida, sucursalEntrada, Session["schema"].ToString());
        }
    }
}