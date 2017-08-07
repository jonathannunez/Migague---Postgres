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
    public partial class AdminNuevoCliente : BasePage
    {
        static List<String> controlTelefonos = new List<String>(); // para guardar los objetos de telefono (textbox,etc)
        static List<String> controlDirecciones = new List<String>(); // para guardar los objetos de direcciones
        static List<String> controlTransportes = new List<String>(); // para guardar los objetos de transportes
        static int cantTelefonos = 0; // es el id que va a tener cada uno de los textbox agregados
        static int cantDirecciones = 0; // es el id que va a tener cada uno de los textbox agregados
        static int cantTransportes = 0; // es el id que va a tener cada uno de los textbox agregados

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                TienePermiso();
                BindData();
            }
            else
            {
                #region "TELEFONOS"
                if (cantTelefonos > 0)
                {
                    for (int i = 1; i <= cantTelefonos; i++)
                    {
                        addTelefono(i, "reload");
                    }
                }
                else
                {
                    controlTelefonos = new List<String>();
                }
                #endregion


                #region "DIRECCIONES"
                if (controlDirecciones != null)
                {
                    for (int j = 1; j <= cantDirecciones; j++)
                    {
                        addDireccion(j, "reload");
                    }
                }
                else
                {
                    controlDirecciones = new List<String>();
                }
                #endregion


                #region "TRANSPORTES"
                if (controlTransportes != null)
                {
                    for (int k = 1; k <= cantTransportes; k++)
                    {
                        addTransporte(k, "reload");
                    }
                }
                else
                {
                    controlTransportes = new List<String>();
                }
                #endregion

            }
        }

        protected void CargarCategoriasTribuarias()
        {
            List<CategoriaTributaria> listaCategoriasTributarias = AdminLN.getInstance().listaCategoriasTributarias(Session["schema"].ToString());
            foreach (CategoriaTributaria categoria_tributaria in listaCategoriasTributarias)
            {
                ListItem newItem = new ListItem(categoria_tributaria.nombre, categoria_tributaria.id.ToString(), true);
                ddlCategoriasTributarias.Items.Add(newItem);
            }
        }

        protected void CargarCategoriasListaPrecios()
        {
            List<CategoriaPrecios> listaCategoriasListasPrecios = CategoriaPreciosLN.getInstance().listaCategoriasPrecios(Session["schema"].ToString());
            foreach (CategoriaPrecios categoria_lista_precio in listaCategoriasListasPrecios)
            {
                ListItem newItem = new ListItem(categoria_lista_precio.nombre, categoria_lista_precio.id.ToString(), true);
                ddlCategoriasListaPrecios.Items.Add(newItem);
            }
        }

        protected void BindData()
        {
            CargarCategoriasTribuarias();
            CargarCategoriasListaPrecios();
        }

        protected void BtnAdd_Click(object sender, EventArgs e)
        {
            Cliente cliente = new Cliente();
            DateTime dateTime = DateTime.UtcNow.Date;
            List<Telefono> listaTelefonos = new List<Telefono>();
            List<Direccion> listaDirecciones = new List<Direccion>();
            List<Transporte> listaTransportes = new List<Transporte>();
            cliente.razon_social = txtRazonSocial.Text.Trim();
            cliente.nombre = txtNombre.Text.Trim();
            cliente.cuit = txtCuit.Text.Trim();
            cliente.id_categoriaTributaria= Convert.ToInt32(ddlCategoriasTributarias.SelectedItem.Value.ToString());
            cliente.id_categoriaPrecios = Convert.ToInt32(ddlCategoriasListaPrecios.SelectedItem.Value.ToString());

            // obtenemos telefonos
            #region "TELEFONOS"
            for (int i = 1; i <= cantTelefonos; i++)
            {
                Telefono telefono = new Telefono();
                foreach (String control in controlTelefonos)
                {
                    if (control.Contains("numero." + i))
                    {
                        telefono.telefono = (placeholderTelefonos.FindControl(control) as TextBox).Text;
                    }
                    else if (control.Contains("descripcion." + i))
                    {
                        telefono.descripcion = (placeholderTelefonos.FindControl(control) as TextBox).Text;
                    }
                }
                listaTelefonos.Add(telefono);
            }
            cliente.telefonos = listaTelefonos;
            #endregion


            // obtenemos direcciones
            #region "DIRECCIONES"
            for (int i = 1; i <= cantDirecciones; i++)
            {
                Direccion direccion = new Direccion();
                foreach (String control in controlDirecciones)
                {
                    if (control.Contains("calle." + i))
                    {
                        direccion.calle = (placeholderTelefonos.FindControl(control) as TextBox).Text;
                    }
                    else if (control.Contains("altura." + i))
                    {
                        direccion.altura = Convert.ToInt32((placeholderTelefonos.FindControl(control) as TextBox).Text);
                    }
                    else if (control.Contains("descripciones." + i))
                    {
                        DropDownList ddl = (DropDownList)placeholderDirecciones.FindControl(control);
                        direccion.descripcion = ddl.SelectedItem.Text.Trim();
                    }
                    else if (control.Contains("localidades." + i))
                    {
                        DropDownList ddl2 = (DropDownList)placeholderDirecciones.FindControl(control);
                        direccion.id_localidad = Convert.ToInt32(ddl2.SelectedItem.Value);
                    }
                }
                listaDirecciones.Add(direccion);
            }
            cliente.direcciones = listaDirecciones;
            #endregion


            // obtenemos los transportes
            #region "TRANSPORTES"
            for (int i = 1; i <= cantTransportes; i++)
            {
                Transporte transporte = new Transporte();
                foreach (String control in controlTransportes)
                {
                    if (control.Contains("descripcion." + i))
                    {
                        transporte.descripcion = (placeholderTelefonos.FindControl(control) as TextBox).Text;
                    }
                    else if (control.Contains("transporte." + i))
                    {
                        DropDownList ddl = (DropDownList)placeholderDirecciones.FindControl(control);
                        transporte.nombre = ddl.SelectedItem.Text.Trim();
                        transporte.id = Convert.ToInt32(ddl.SelectedItem.Value.ToString());
                    }
                }
                listaTransportes.Add(transporte);
            }
            cliente.transportes = listaTransportes;
            #endregion

            string retorno = ClienteLN.getInstance().nuevoCliente(cliente, Session["schema"].ToString());
            txtNombre.Text = "";
            Response.Write(@"<script language='javascript'>alert('" + retorno + " .');</script>");
            
        }

        protected void BtnAddTelefono_Click(object sender, EventArgs e)
        {
            cantTelefonos++;
            addTelefono(cantTelefonos,"new");
            
        }

        protected void btnAddTransporte_Click(object sender, EventArgs e)
        {
            cantTransportes++;
            addTransporte(cantTransportes, "new");
        }

        protected void btnAddDireccion_Click(object sender, EventArgs e)
        {
            cantDirecciones++;
            addDireccion(cantDirecciones, "new");
        }

        public void addTelefono(int contador, string reload)
        {
            TextBox tbNumero = new TextBox();
            TextBox tbDescripcion = new TextBox();
            Label lblNumero = new Label();
            Label lblDescripcion = new Label();
            lblNumero.ID = "tel.lblNumero" + contador.ToString();
            lblNumero.Text = "Ingrese numero";
            lblDescripcion.ID = "tel.lblDescripcion" + contador.ToString();
            lblDescripcion.Text = "Ingrese descripción";
            tbNumero.ID = "tel.numero." + contador.ToString();
            tbNumero.Attributes.Add("placeholder", "Ingrese número de teléfono");
            tbDescripcion.ID = "tel.descripcion." + contador.ToString();
            tbDescripcion.Attributes.Add("placeholder", "Ingrese descripción de teléfono");
            LiteralControl lineBreak = new LiteralControl("<br />");
            placeholderTelefonos.Controls.Add(lblNumero);
            placeholderTelefonos.Controls.Add(tbNumero);
            placeholderTelefonos.Controls.Add(lblDescripcion);
            placeholderTelefonos.Controls.Add(tbDescripcion);
            placeholderTelefonos.Controls.Add(lineBreak);
            if (reload == "new")
            {
                controlTelefonos.Add(tbNumero.ID);
                controlTelefonos.Add(tbDescripcion.ID);
            }
            //ViewState.Add("controlTelefonos", controlTelefonos);  // guardamos el estado y lo volvemos a actualizar
        }

        public void addDireccion(int contador, string reload)
        {
            // labels
            Label lblCalle = new Label();
            Label lblAltura = new Label();
            Label lblDescripcion = new Label();
            Label lblLocalidad = new Label();
            lblCalle.ID = "dir.lblCalle" + contador.ToString();
            lblCalle.Text = "Ingrese calle";
            lblAltura.ID = "dir.lblAltura" + contador.ToString();
            lblAltura.Text = "Ingrese altura";
            lblDescripcion.ID = "dir.lblDescripcion" + contador.ToString();
            lblDescripcion.Text = "Seleccione descripcion";
            lblLocalidad.ID = "dir.lblLocalidad" + contador.ToString();
            lblLocalidad.Text = "Seleccione localidad";

            // textbox
            TextBox tbCalle = new TextBox();
            TextBox tbAltura = new TextBox();
            tbCalle.ID = "dir.calle." + contador.ToString();
            tbCalle.Attributes.Add("placeholder", "Ingrese calle");
            tbAltura.ID = "dir.altura." + contador.ToString();
            tbAltura.Attributes.Add("placeholder", "Ingrese altura");

            // dropdownlist descripciones
            DropDownList ddlDescripciones = new DropDownList();
            List<ListItem> items = new List<ListItem>();
            items.Add(new ListItem("Direccion Real","1"));
            items.Add(new ListItem("Direccion Envio","2"));
            items.Add(new ListItem("Otra direccion","3"));
            ddlDescripciones.ID = "dir.descripciones." + contador.ToString();
            ddlDescripciones.Items.AddRange(items.ToArray());

            // dropdownlist localidades
            DropDownList ddlLocalidades = new DropDownList();
            ddlLocalidades.ID = "dir.localidades." + contador.ToString();
            List<Localidad> listaLocalidades = LocalidadLN.getInstance().listaLocalidades(Session["schema"].ToString());
            foreach (Localidad localidad in listaLocalidades)
            {
                ListItem newItem = new ListItem(localidad.nombre, localidad.id.ToString(), true);
                ddlLocalidades.Items.Add(newItem);
            }

            // agregamos todos los botones al placeholder de direcciones
            LiteralControl lineBreak = new LiteralControl("<br />");
            placeholderDirecciones.Controls.Add(lblCalle);
            placeholderDirecciones.Controls.Add(tbCalle);
            placeholderDirecciones.Controls.Add(lblAltura);
            placeholderDirecciones.Controls.Add(tbAltura);
            placeholderDirecciones.Controls.Add(lblDescripcion);
            placeholderDirecciones.Controls.Add(ddlDescripciones);
            placeholderDirecciones.Controls.Add(lblLocalidad);
            placeholderDirecciones.Controls.Add(ddlLocalidades);
            placeholderDirecciones.Controls.Add(lineBreak);

            if (reload == "new")
            {
                controlDirecciones.Add(tbCalle.ID);
                controlDirecciones.Add(tbAltura.ID);
                controlDirecciones.Add(ddlDescripciones.ID);
                controlDirecciones.Add(ddlLocalidades.ID);
            }
            //ViewState.Add("controlTelefonos", controlTelefonos);  // guardamos el estado y lo volvemos a actualizar
        }

        public void addTransporte(int contador, string reload)
        {
            // labels
            Label lblTransporte = new Label();
            Label lblDescripcion = new Label();
            lblTransporte.ID = "trans.lblTransporte" + contador.ToString();
            lblTransporte.Text = "Seleccione transporte";
            lblTransporte.ID = "trans.lblDescripcion" + contador.ToString();
            lblTransporte.Text = "Ingrese descripcion";

            // descripcion
            TextBox tbDescripcion = new TextBox();
            tbDescripcion.ID = "trans.descripcion." + contador.ToString();
            tbDescripcion.Attributes.Add("placeholder", "Ingrese descripcion");

            // dropdownlist transporte
            DropDownList ddlTransportes = new DropDownList();
            ddlTransportes.ID = "trans.transporte." + contador.ToString();
            List<Transporte> listaTransportes = TransporteLN.getInstance().listaTransportes(Session["schema"].ToString());
            foreach (Transporte transporte in listaTransportes)
            {
                ListItem newItem = new ListItem(transporte.nombre, transporte.id.ToString(), true);
                ddlTransportes.Items.Add(newItem);
            }

            // agregamos botones
            LiteralControl lineBreak = new LiteralControl("<br />");
            placeholderTransportes.Controls.Add(lblTransporte);
            placeholderTransportes.Controls.Add(ddlTransportes);
            placeholderTransportes.Controls.Add(lblDescripcion);
            placeholderTransportes.Controls.Add(tbDescripcion);
            placeholderTransportes.Controls.Add(lineBreak);
            if (reload == "new")
            {
                controlTransportes.Add(ddlTransportes.ID);
                controlTransportes.Add(tbDescripcion.ID);
            }
        }


       
    }
}