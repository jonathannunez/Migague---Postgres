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
    public partial class AdminUsuarioSucursal : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        { 
            if (!IsPostBack)
            {
                TienePermiso();
                List<Usuario> listaUsuarios = UsuarioLN.getInstance().listaUsuarios(Session["schema"].ToString());
                foreach (Usuario usuario in listaUsuarios)
                {
                    ListItem newItem = new ListItem(usuario.nombre, usuario.id.ToString(), true);
                    ddlUsuarios.Items.Add(newItem);
                }

                List<Sucursal> listaSucursales = SucursalLN.getInstance().listaSucursales(Session["schema"].ToString());
                foreach (Sucursal sucursal in listaSucursales)
                {
                    ListItem newItem = new ListItem(sucursal.nombre, sucursal.id.ToString(), true);
                    chListSucursales.Items.Add(newItem);
                }

                setSucxUser();
            }
        }
        
        //funcion para setear los checkbox por rol según las funcionalidades que tenga
        protected void setSucxUser()
        {
            //desselecciono todos los items
            foreach (ListItem item in chListSucursales.Items)
            {
                item.Selected = false;
            }

            //busco las sucursales x usuarios
            int id_usuario = Convert.ToInt32(ddlUsuarios.SelectedValue.ToString());
            Usuario usuario = new Usuario();
            usuario.id = id_usuario;
            usuario = UsuarioLN.getInstance().setSucursalPorUsuario(usuario, Session["schema"].ToString());
    

            //recorro todos los items buscando si es igual a alguna de las funcionalidades entonces lo pongo true
            foreach (ListItem item in chListSucursales.Items)
            {
                foreach (Sucursal sucursal in usuario.sucursales)
                {
                    if (sucursal.id.ToString() == item.Value.ToString())
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


        protected void ddlUsuarios_SelectedIndexChanged(object sender, EventArgs e)
        {
            setSucxUser();
        }

        protected void BtnUpdateSucxUsuario_Click(object sender, EventArgs e)
        {
            int id_usuario = Convert.ToInt32(ddlUsuarios.SelectedValue.ToString());
            Usuario usuario = new Usuario();
            usuario.id = id_usuario;
            usuario = UsuarioLN.getInstance().setSucursalPorUsuario(usuario, Session["schema"].ToString());
            //DataTable sucxuser = AdminLN.getInstance().listaSucxUser(id_usuario, Session["schema"].ToString());

            //recorro todos los items buscando si es igual a alguna de las funcionalidades entonces lo pongo true
            foreach (ListItem item in chListSucursales.Items)
            {
                bool existeRelacion = false;
                foreach (Sucursal sucursal in usuario.sucursales)
                {
                    if ((sucursal.id.ToString() == item.Value.ToString())
                        && (!item.Selected))
                    {
                        int id_sucursal = Convert.ToInt32(sucursal.id.ToString());
                        UsuarioLN.getInstance().eliminarSucursalUsuario(id_usuario, id_sucursal, Session["schema"].ToString());
                        existeRelacion = true;
                    }
                }
                if (!existeRelacion && item.Selected)
                {
                    int id_sucursal = Convert.ToInt32(item.Value.ToString());
                    UsuarioLN.getInstance().updateSucursalUsuario(id_usuario, id_sucursal, Session["schema"].ToString());
                }
            }

        }
    }
}