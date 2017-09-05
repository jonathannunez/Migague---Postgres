using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaEntidades;
using CapaLogicaNegocio;
using System.Threading;
using System.Globalization;
using System.Security.Cryptography;
using System.IO;
using System.Text;
using System.Data;

namespace Migague.Views.Login
{
    public partial class Login : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            txtEmpresa.Focus();
            this.Form.DefaultButton = this.BtnLogin.UniqueID;

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            Usuario usuario = UsuarioLN.getInstance().login(txtEmpresa.Text.Trim(),txtUsuario.Text.Trim(), txtPassword.Text.Trim());
            if (usuario != null)
            {
                //SET SESSION USER
                Session["userId"] = usuario.id;
                Session["userName"] = usuario.nombre;
                Session["userRol"] = usuario.rol.id;
                Session["schema"] = usuario.empresa.schema;
                Session["sucursales"] = usuario.sucursales;
                Session["funcionalidades"] = usuario.rol.funcionalidades;
                Session["userSucursal"] = Convert.ToInt32(usuario.sucursales[0].id.ToString());
                Response.Redirect("~/Default.aspx");
                //Response.Redirect("~/RenderMenu.aspx");

            }
            else
            {
                txtEmpresa.Text = "";
                txtUsuario.Text = "";
                txtPassword.Text = "";
                txtEmpresa.Focus();
            }
        }
    }
}