using CapaLogicaNegocio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaEntidades;

namespace Migague.Views.ABM
{
    public partial class TransferirStock : BasePage
    {
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

        protected void gridArticulosSalida_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = gridArticulosSalida.SelectedRow;
            TextBox txtId = (TextBox)row.FindControl("txtEditId");
        }
    }
}