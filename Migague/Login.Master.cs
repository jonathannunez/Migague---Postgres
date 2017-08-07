using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Threading;
using System.Globalization;

namespace Migague
{
    public partial class LoginMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

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

    }
}