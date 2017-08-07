using CapaLogicaNegocio;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaEntidades;

namespace Migague.Views.ABM
{
    public partial class AdministrarFuncionalidades : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        { 
            if (!IsPostBack)
            {
                TienePermiso();
                List<Rol> listaRoles = RolLN.getInstance().listaRoles(Session["schema"].ToString());
                foreach (Rol rol in listaRoles)
                {
                    ListItem newItem = new ListItem(rol.nombre, rol.id.ToString(), true);
                    ddlRoles.Items.Add(newItem);
                }

                List<Funcionalidad> listFuncionalidades = FuncionalidadLN.getInstance().listaFuncionalidades(Session["schema"].ToString());
                foreach (Funcionalidad funcionalidad in listFuncionalidades)
                {
                    ListItem newItem = new ListItem(funcionalidad.nombre,funcionalidad.id.ToString(), true);
                    chListFuncionalidades.Items.Add(newItem);
                }

                setFuncxRol();
            }
        }

        //cuando cambia el selector de rol
        protected void ddlRoles_SelectedIndexChanged(object sender, EventArgs e)
        {
            setFuncxRol();
        }
        
        //funcion para setear los checkbox por rol según las funcionalidades que tenga
        protected void setFuncxRol()
        {
            //desselecciono todos los items
            foreach (ListItem item in chListFuncionalidades.Items)
            {
                item.Selected = false;
            }

            //busco las funcionalidades x rol 
            int id_rol = Convert.ToInt32(ddlRoles.SelectedValue.ToString());
            List<Rol> listaRoles = RolLN.getInstance().listaRoles(Session["schema"].ToString());
            List<Rol> listRolSeleccionado = listaRoles.Where(item => item.id == id_rol).ToList();
            Rol rolSeleccionado = new Rol();
            rolSeleccionado = listRolSeleccionado[0];

            //recorro todos los items buscando si es igual a alguna de las funcionalidades entonces lo pongo true
            foreach (ListItem item in chListFuncionalidades.Items)
            {
                foreach (Funcionalidad funcionalidad in rolSeleccionado.funcionalidades)
                {
                    if (funcionalidad.id.ToString() == item.Value.ToString())
                    {
                        item.Selected = true;
                        break;
                    }
                    else
                    {
                        item.Selected = false;
                    }
                }
            }
        }

        protected void BtnUpdateFunxRol_Click(object sender, EventArgs e)
        {
            int id_rol = Convert.ToInt32(ddlRoles.SelectedValue.ToString());
            List<Rol> listaRoles = RolLN.getInstance().listaRoles(Session["schema"].ToString());
            List<Rol> listRolSeleccionado = listaRoles.Where(item => item.id == id_rol).ToList();
            Rol rolSeleccionado = new Rol();
            rolSeleccionado = listRolSeleccionado[0];

            //recorro todos los items buscando si es igual a alguna de las funcionalidades entonces lo pongo true
            foreach (ListItem item in chListFuncionalidades.Items)
            {
                bool existeRelacion = false;
                foreach (Funcionalidad funcionalidad in rolSeleccionado.funcionalidades)
                {
                    if ((funcionalidad.id.ToString() == item.Value.ToString())
                        && (!item.Selected))
                    {
                        AdminLN.getInstance().deletefuncxrol(id_rol, funcionalidad.id, Session["schema"].ToString());
                        existeRelacion = true;
                    } 
                }
                if(!existeRelacion && item.Selected)
                {
                    int id_funcionalidad = Convert.ToInt32(item.Value.ToString());
                    AdminLN.getInstance().updatefuncxrol(id_rol, id_funcionalidad, Session["schema"].ToString());
                }
            }


            //recorro todos los items para saber cual esta seleccionado y lo guardo en una lista
        }

    }
}