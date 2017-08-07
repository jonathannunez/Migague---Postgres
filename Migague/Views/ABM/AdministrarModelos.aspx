<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AdministrarModelos.aspx.cs" Inherits="Migague.Views.ABM.AdministrarModelos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        <asp:Literal runat="server" Text="<%$ Resources:MenuResource, submenuAdminModelos%>"></asp:Literal>
    </h2>

    <div class="form-group" style="text-align: left">
        <asp:GridView ID="gridModelos" runat="server" AutoGenerateColumns="False" AllowPaging="true"
            AllowSorting="true" OnRowUpdating="gridModelos_RowUpdating" OnRowEditing="gridModelos_RowEditing"
            OnRowCancelingEdit="gridModelos_RowCancelingEdit" OnRowDataBound="gridModelos_RowDataBound">
            <Columns>

                <asp:TemplateField HeaderText="<%$ Resources:AdminModelos, gridModelosID%>" Visible="false">
                    <ItemTemplate>
                        <asp:TextBox runat="server" ID="txtId" Text='<%# Eval("id")%>' Visible="false"/>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox runat="server" ID="txtEditId" Text='<%# Eval("id")%>' Visible="false"/>
                    </EditItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="<%$ Resources:AdminModelos, gridModelosCodigo%>" >
                    <ItemTemplate>
                        <asp:TextBox runat="server" ID="txtCodigo" Text='<%# Eval("codigo")%>' />
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox runat="server" ID="txtEditCodigo" Text='<%# Eval("codigo")%>' />
                    </EditItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="<%$ Resources:AdminModelos, gridModelosNombre%>" >
                    <ItemTemplate>
                        <asp:TextBox runat="server" ID="txtNombre" Text='<%# Eval("nombre")%>' />
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox runat="server" ID="txtEditNombre" Text='<%# Eval("nombre")%>' />
                    </EditItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="<%$ Resources:AdminModelos, gridModelosProveedor%>" >
                    <ItemTemplate>
                        <asp:TextBox runat="server" ID="txtProveedor" Text='<%# Eval("proveedor.nombre")%>' />
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:Label ID="lblProveedor" runat="server" Text='<%# Eval("proveedor.nombre")%>' Visible = "false"></asp:Label>
                        <asp:DropDownList ID="ddlProveedores" runat="server"></asp:DropDownList>
                    </EditItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="<%$ Resources:AdminModelos, gridModelosTemporada%>" >
                    <ItemTemplate>
                        <asp:TextBox runat="server" ID="txtTemporada" Text='<%# Eval("temporada.nombre")%>' />
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:Label ID="lblTemporada" runat="server" Text='<%# Eval("temporada.nombre")%>' Visible = "false"></asp:Label>
                        <asp:DropDownList ID="ddlTemporadas" runat="server"></asp:DropDownList>
                    </EditItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="<%$ Resources:AdminModelos, gridModelosFecha%>" >
                    <ItemTemplate>
                        <asp:TextBox runat="server" ID="txtFecha" Text='<%# Eval("fecha")%>' />
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox runat="server" ID="txtEditFecha" Text='<%# Eval("fecha")%>' />
                    </EditItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="<%$ Resources:AdminModelos, gridModelosTipoProducto%>" >
                    <ItemTemplate>
                        <asp:TextBox runat="server" ID="txtTipoProducto" Text='<%# Eval("tipoProducto.nombre")%>' />
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:Label ID="lblTipoProducto" runat="server" Text='<%# Eval("tipoProducto.nombre")%>' Visible = "false"></asp:Label>
                        <asp:DropDownList ID="ddlTiposProductos" runat="server"></asp:DropDownList>
                    </EditItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="<%$ Resources:AdminModelos, gridModelosActivo%>" Visible="false">
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
        <asp:Button ID="BtnNew" runat="server" Text="<%$ Resources:AdminModelos, BtnNuevo%>" OnClick="BtnAddNew_Click"/>
        <asp:Button ID="BtnDelete" runat="server" Text="<%$ Resources:AdminModelos, BtnEliminar%>" OnClick="BtnDelete_Click"/>
        </div>
</asp:Content>
