<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AdministrarClientes.aspx.cs" Inherits="Migague.Views.ABM.AdministrarClientes" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        <asp:Literal runat="server" Text="<%$ Resources:MenuResource, submenuAdminClientes%>"></asp:Literal>
    </h2>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript">
        $("[src*=plus]").live("click", function () {
            $(this).closest("tr").after("<tr><td></td><td colspan = '999'>" + $(this).next().html() + "</td></tr>")
            $(this).attr("src", "images/minus.png");
        });
        $("[src*=minus]").live("click", function () {
            $(this).attr("src", "images/plus.png");
            $(this).closest("tr").next().remove();
        });
    </script>
    <div class="form-group" style="text-align: left">
        <asp:GridView ID="gridClientes" runat="server" AutoGenerateColumns="False" AllowPaging="true"
            AllowSorting="true" OnRowUpdating="gridClientes_RowUpdating" OnRowEditing="gridClientes_RowEditing"
            OnRowCancelingEdit="gridClientes_RowCancelingEdit" OnRowDataBound="gridClientes_RowDataBound" 
            DataKeyNames="id">
            
            <Columns>

                <asp:TemplateField>
                    <ItemTemplate>
                        <img alt = "" style="cursor: pointer" src="../../Imagenes/plus.png" / height="42" width="42">
                        <asp:Panel ID="pnlOrders" runat="server" Style="display: none">
                            <asp:GridView ID="gvTelefonos" runat="server" AutoGenerateColumns="false" CssClass = "ChildGrid" Caption="TELEFONOS">
                                <Columns>
                                    <asp:TemplateField HeaderText="<%$ Resources:AdminClientes, gridClientesID%>" Visible="false">
                                        <ItemTemplate>
                                            <asp:TextBox runat="server" ID="txtIdTelefono" Text='<%# Eval("id")%>' Visible="false"/>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox runat="server" ID="txtEditIdTelefono" Text='<%# Eval("id")%>' Visible="false"/>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField ItemStyle-Width="150px" DataField="telefono" HeaderText="Numero" />
                                    <asp:BoundField ItemStyle-Width="150px" DataField="descripcion" HeaderText="Descripcion" />
                                    <asp:BoundField ItemStyle-Width="150px" DataField="fecha" HeaderText="Fecha" />
                                    <asp:BoundField ItemStyle-Width="150px" DataField="es_activo" HeaderText="Activo" />
                                    <asp:TemplateField>
                                        
                                        <FooterTemplate>
                                        <asp:Button ID="btnAdd" runat="server" Text="Add" />
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>


                            <asp:GridView ID="gvDirecciones" runat="server" AutoGenerateColumns="false" CssClass = "ChildGrid" Caption="DIRECCIONES">
                                <Columns>
                                    <asp:BoundField ItemStyle-Width="150px" DataField="id" HeaderText="IdDireccion" />
                                    <asp:BoundField ItemStyle-Width="150px" DataField="calle" HeaderText="Calle" />
                                    <asp:BoundField ItemStyle-Width="150px" DataField="altura" HeaderText="Altura" />
                                    <asp:BoundField ItemStyle-Width="150px" DataField="descripcion" HeaderText="Descripcion" />
                                    <asp:BoundField ItemStyle-Width="150px" DataField="localidad.id" HeaderText="IdLocalidad" />
                                    <asp:BoundField ItemStyle-Width="150px" DataField="localidad.nombre" HeaderText="Localidad" />
                                    <asp:BoundField ItemStyle-Width="150px" DataField="fecha" HeaderText="Fecha" />
                                    <asp:BoundField ItemStyle-Width="150px" DataField="es_activo" HeaderText="Activo" />
                                </Columns>
                            </asp:GridView>

                            <asp:GridView ID="gvTransportes" runat="server" AutoGenerateColumns="false" CssClass = "ChildGrid" Caption="TRANSPORTES">
                                <Columns>
                                    <asp:BoundField ItemStyle-Width="150px" DataField="id" HeaderText="IdTransporte" />
                                    <asp:BoundField ItemStyle-Width="150px" DataField="descripcion" HeaderText="Descripcion" />
                                    <asp:BoundField ItemStyle-Width="150px" DataField="fecha" HeaderText="Fecha" />
                                    <asp:BoundField ItemStyle-Width="150px" DataField="es_activo" HeaderText="Activo" />
                                </Columns>
                            </asp:GridView>

                        </asp:Panel>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="<%$ Resources:AdminClientes, gridClientesID%>" Visible="false">
                    <ItemTemplate>
                        <asp:TextBox runat="server" ID="txtId" Text='<%# Eval("id")%>' Visible="false"/>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox runat="server" ID="txtEditId" Text='<%# Eval("id")%>' Visible="false"/>
                    </EditItemTemplate>
                </asp:TemplateField>

                 <asp:TemplateField HeaderText="<%$ Resources:AdminClientes, gridClientesRazonSocial%>" >
                    <ItemTemplate>
                        <asp:TextBox runat="server" ID="txtRazonSocial" Text='<%# Eval("razon_social")%>' />
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox runat="server" ID="txtEditRazonSocial" Text='<%# Eval("razon_social")%>' />
                    </EditItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="<%$ Resources:AdminClientes, gridClientesNombre%>" >
                    <ItemTemplate>
                        <asp:TextBox runat="server" ID="txtNombre" Text='<%# Eval("nombre")%>' />
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox runat="server" ID="txtEditNombre" Text='<%# Eval("nombre")%>' />
                    </EditItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="<%$ Resources:AdminClientes, gridClientesCuit%>" >
                    <ItemTemplate>
                        <asp:TextBox runat="server" ID="txtCuit" Text='<%# Eval("cuit")%>' />
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox runat="server" ID="txtEditCuit" Text='<%# Eval("cuit")%>' />
                    </EditItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="<%$ Resources:AdminClientes, gridClientesFecha%>" >
                    <ItemTemplate>
                        <asp:TextBox runat="server" ID="txtFecha" Text='<%# Eval("fecha_ingreso")%>' />
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox runat="server" ID="txtEditFecha" Text='<%# Eval("fecha_ingreso")%>' />
                    </EditItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="<%$ Resources:AdminClientes, gridClientesMail%>" >
                    <ItemTemplate>
                        <asp:TextBox runat="server" ID="txtMail" Text='<%# Eval("email")%>' />
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox runat="server" ID="txtEditMail" Text='<%# Eval("email")%>' />
                    </EditItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="<%$ Resources:AdminClientes, gridClientesCatTributaria%>" >
                    <ItemTemplate>
                        <asp:TextBox runat="server" ID="txtCatTributaria" Text='<%# Eval("categoriaTributaria.nombre")%>' />
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:Label ID="lblCatTributaria" runat="server" Text='<%# Eval("categoriaTributaria.nombre")%>' Visible = "false"></asp:Label>
                        <asp:DropDownList ID="ddlCatTributaria" runat="server"></asp:DropDownList>
                    </EditItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="<%$ Resources:AdminClientes, gridClientesCatListaPrecio%>" >
                    <ItemTemplate>
                        <asp:TextBox runat="server" ID="txtCatListaPrecios" Text='<%# Eval("categoriaPrecios.nombre")%>' />
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:Label ID="lblCatListaPrecio" runat="server" Text='<%# Eval("categoriaPrecios.nombre")%>' Visible = "false"></asp:Label>
                        <asp:DropDownList ID="ddlCatListaPrecio" runat="server"></asp:DropDownList>
                    </EditItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="<%$ Resources:AdminClientes, gridClientesActivo%>" Visible="false">
                    <ItemTemplate>
                        <asp:TextBox runat="server" ID="txtActivo" Text='<%# Eval("es_activo")%>' Visible="false"/>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox runat="server" ID="txtEditActivo" Text='<%# Eval("es_activo")%>' Visible="false"/>
                    </EditItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField>
                    <HeaderTemplate>
                        <asp:CheckBox ID="chkDeleteAll" runat="server" OnCheckedChanged="chkDeleteAll_CheckedChanged" AutoPostBack="true"/>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:CheckBox ID="chkDelete" runat="server" OnCheckedChanged="chkDelete_CheckedChanged" AutoPostBack="true"/>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:CommandField ButtonType="Link" ShowEditButton="True" ShowCancelButton="true" />
            </Columns>
        </asp:GridView>
        <br />
        <asp:Button ID="BtnNew" runat="server" Text="<%$ Resources:AdminClientes, BtnNuevo%>" OnClick="BtnAddNew_Click"/>
        <asp:Button ID="BtnDelete" runat="server" Text="<%$ Resources:AdminClientes, BtnEliminar%>" OnClick="BtnDelete_Click"/>
        </div>
</asp:Content>
