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
    public partial class AdminNuevaLocalidad : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                TienePermiso();
                BindData();
            }
        }

        protected void CargarPaises()
        {
            List<Pais> listaPaises = PaisLN.getInstance().listaPaises(Session["schema"].ToString());
            foreach (Pais pais in listaPaises)
            {
                ListItem newItem = new ListItem(pais.nombre, pais.id.ToString(), true);
                ddlPaises.Items.Add(newItem);
            }
        }

        protected void CargarProvincias()
        {
            ddlProvincias.Items.Clear();
            List<Provincia> listaProvincias = ProvinciaLN.getInstance().listaProvincias(Session["schema"].ToString());
            int id_pais = Convert.ToInt32(ddlPaises.SelectedValue.ToString());
            List<Provincia> listaFiltrada = listaProvincias.Where(item => item.pais.id == id_pais).ToList();
            foreach (Provincia provincia in listaFiltrada)
            {
                ListItem newItem = new ListItem(provincia.nombre, provincia.id.ToString(), true);
                ddlProvincias.Items.Add(newItem);
            }
        }

        protected void BindData()
        {
            CargarPaises();
            CargarProvincias();
        }

        protected void BtnAdd_Click(object sender, EventArgs e)
        {
            DateTime dateTime = DateTime.UtcNow.Date;
            int id_provincia = Convert.ToInt32(ddlProvincias.SelectedValue.ToString());
            string nombre_provincia = ddlProvincias.SelectedItem.Text.ToString();
            string retorno = LocalidadLN.getInstance().nuevaLocalidad(id_provincia,nombre_provincia,
                txtNombre.Text.Trim(), dateTime,
                Session["schema"].ToString());
            txtNombre.Text = "";
            Response.Write(@"<script language='javascript'>alert('" + retorno + " .');</script>");
        }

        protected void ddlPaises_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarProvincias();
        }
    }
}