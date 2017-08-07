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
    public partial class AdminNuevaSucursal : BasePage
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
            cargarLocalidades();
        }

        // cargar modelos
        protected void cargarLocalidades()
        {
            List<Localidad> listaLocalidades = LocalidadLN.getInstance().listaLocalidades(Session["schema"].ToString());
            foreach (Localidad localidad in listaLocalidades)
            {
                ListItem newItem = new ListItem(localidad.nombre, localidad.id.ToString(), true);
                ddlLocalidad.Items.Add(newItem);
            }
        }


        protected void BtnAdd_Click(object sender, EventArgs e)
        {
            string nombre = txtNombre.Text.Trim();
            string calle = txtCalle.Text.Trim();
            int altura = Convert.ToInt32(txtAltura.Text.Trim());
            int idlocalidad = Convert.ToInt32(ddlLocalidad.SelectedItem.Value.ToString());
            DateTime dateTime = DateTime.UtcNow.Date;

            string retorno = SucursalLN.getInstance().nuevaSucursal(nombre,calle,altura,dateTime,idlocalidad, Session["schema"].ToString());
            txtNombre.Text = "";
            txtCalle.Text = "";
            txtAltura.Text = "";
            Response.Write(@"<script language='javascript'>alert('" + retorno + " .');</script>");
            
        }
    }
}