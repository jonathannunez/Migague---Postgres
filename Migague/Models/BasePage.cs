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
using System.IO;

namespace Migague
{
    public class BasePage:Page
    {
       public static int id_sucursal;

        protected override void InitializeCulture()
        {
            string language = Session["Lang"].ToString();
            if (language != null && language != "")
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo(language);
                Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture(language);
            }

            base.InitializeCulture();
        }

        protected  void TienePermiso()
        {
            List<Funcionalidad> funcionalidadesUser = (List<Funcionalidad>)Session["funcionalidades"];
            if (funcionalidadesUser == null)
            {
                Session.Abandon();
                Response.Write(@"<script language='javascript'>alert(' Debe estar logueado .');</script>");
                Response.Redirect("~/Views/Login/Login.aspx");
            }
            bool permiso = false;
            string pageName = Path.GetFileNameWithoutExtension(Page.AppRelativeVirtualPath);
            foreach(Funcionalidad funcionalidad in funcionalidadesUser)
            {
                if (funcionalidad.nombre.Trim() == pageName)
                {
                    permiso = true;
                }
            }
            if (!permiso)
            {
                Session.Abandon();
                Response.Write(@"<script language='javascript'>alert(' Debe estar logueado .');</script>");
                Response.Redirect("~/Views/Login/Login.aspx");
            } 
        }
    }
}