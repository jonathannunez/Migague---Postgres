using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Threading;
using System.Globalization;
using System.Data;
using CapaEntidades;
using CapaLogicaNegocio;
using System.Resources;
using Resources;

namespace Migague
{
    public partial class SiteMaster : MasterPage
    {


        protected void Page_Load(object sender, EventArgs e)
        {
            ResourceManager rm = Resource.ResourceManager;
            //Render Menu
            if (!IsPostBack)
            {
                int id_rol = Convert.ToInt32(Session["userRol"].ToString());
                DataTable menudata = UsuarioLN.getInstance().getMenuItems(id_rol);
                AddTopMenuItems(menudata);
                List<Sucursal> sucursales = (List<Sucursal>)Session["sucursales"];
                foreach (Sucursal sucursal in sucursales)
                {
                    ListItem newItem = new ListItem(sucursal.nombre, Convert.ToString(sucursal.id), true);
                    ddlSucursales.Items.Add(newItem);
                }
            }
            int idsucursal = Convert.ToInt32(ddlSucursales.SelectedItem.Value.ToString());
            Session["userSucursal"] = idsucursal;
            //Seteo nombre de usuario
            lb_userName.Text = rm.GetString("LblBienvenido") + " " + Session["userName"].ToString();  
        }

        //CONTROL DE IDIOMAS
        protected void btnEspañol_Click(object sender, EventArgs e)
        {
            Session["Lang"] = "es-ES";
            Response.Redirect(Request.Url.AbsoluteUri);
        }

        protected void btnIngles_Click(object sender, EventArgs e)
        {
            Session["Lang"] = "en-US";
            Response.Redirect(Request.Url.AbsoluteUri);
        }

        protected void btnCoreano_Click(object sender, EventArgs e)
        {
            Session["Lang"] = "ko-KR";
            Response.Redirect(Request.Url.AbsoluteUri);
        }

        //BOTON DE ADMINISTRAR USUARIOS
        protected void btnAdministracion_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/AdministrarUsuarios.aspx");
        }

        protected void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Response.Redirect("~/Views/Login/Login.aspx");
        }

        //RENDER MENU
        private void AddTopMenuItems(DataTable menuData)
        {
            DataView view = null;
            try
            {
                view = new DataView(menuData);
                view.RowFilter = "parentid IS NULL";
                ResourceManager rm = MenuResource.ResourceManager;
                foreach (DataRowView row in view)
                {
                    //Adding the menu item
                    string nombreMenu = row["GR"].ToString();
                    string nombreMenuResource = rm.GetString(nombreMenu);
                    MenuItem newMenuItem = new MenuItem(nombreMenuResource, row["ID"].ToString());
                    
                    menuBar.Items.Add(newMenuItem);
                    //Calling the function to add the child menu items
                    AddChildMenuItems(menuData, newMenuItem);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                view = null;
            }
        }

        private void AddChildMenuItems(DataTable menuData, MenuItem parentMenuItem)
        {
            DataView view = null;
            try
            {
                view = new DataView(menuData);
                view.RowFilter = "parentid=" + parentMenuItem.Value;
                ResourceManager rm = MenuResource.ResourceManager;
                foreach (DataRowView row in view)
                {
                    string nombreMenu = row["GR"].ToString();
                    string nombreMenuResource = rm.GetString(nombreMenu);
                    MenuItem newMenuItem = new MenuItem(nombreMenuResource, row["ID"].ToString());
                    newMenuItem.NavigateUrl = row["URL"].ToString();
                    parentMenuItem.ChildItems.Add(newMenuItem);
                    // This code is used to recursively add child menu items filtering by ParentID
                    AddChildMenuItems(menuData, newMenuItem);
                }
            }
            catch (Exception ex)
            {
                //Show the error massage here
            }
            finally
            {
                view = null;
            }
        }

        protected void ddlSucursales_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idsucursal = Convert.ToInt32(ddlSucursales.SelectedItem.Value.ToString());
            Session["userSucursal"] = idsucursal;
        }
    }

    
}