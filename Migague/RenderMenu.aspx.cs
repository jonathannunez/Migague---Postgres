using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaEntidades;
using CapaLogicaNegocio;

namespace Migague
{
    public partial class RenderMenu : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int id_rol = Convert.ToInt32(Session["userRol"].ToString());
            DataTable probando = UsuarioLN.getInstance().getMenuItems(id_rol);
            AddTopMenuItems(probando);
        }

        private void AddTopMenuItems(DataTable menuData)
        {
            DataView view = null;
            try
            {
                view = new DataView(menuData);
                view.RowFilter = "parentid IS NULL";
                foreach (DataRowView row in view)
                {
                    //Adding the menu item
                    MenuItem newMenuItem = new MenuItem(row["GR"].ToString(), row["ID"].ToString());
                    
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
                foreach (DataRowView row in view)
                {
                    MenuItem newMenuItem = new MenuItem(row["GR"].ToString(), row["ID"].ToString());
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
    }
}