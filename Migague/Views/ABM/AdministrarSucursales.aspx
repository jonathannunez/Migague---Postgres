<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AdministrarSucursales.aspx.cs" Inherits="Migague.Views.ABM.AdministrarSucursales" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        <asp:Literal runat="server" Text="<%$ Resources:MenuResource, submenuAdminSucursales%>"></asp:Literal>
    </h2>

    <div class="form-group" style="text-align: left">
        <asp:GridView ID="gridSucursales" runat="server" AutoGenerateColumns="False" AllowPaging="true"
            AllowSorting="true" OnRowUpdating="gridSucursales_RowUpdating" OnRowEditing="gridSucursales_RowEditing"
            OnRowCancelingEdit="gridSucursales_RowCancelingEdit" OnRowDataBound="gridSucursales_RowDataBound">
            <Columns>
                <asp:TemplateField HeaderText="<%$ Resources:AdminSucursales, gridSucursalesID%>" Visible="false">
                    <ItemTemplate>
                        <asp:TextBox runat="server" ID="txtId" Text='<%# Eval("id")%>' Visible="false"/>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox runat="server" ID="txtEditId" Text='<%# Eval("id")%>' Visible="false"/>
                    </EditItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="<%$ Resources:AdminSucursales, gridSucursalesNombre%>" >
                    <ItemTemplate>
                        <asp:TextBox runat="server" ID="txtNombre" Text='<%# Eval("nombre")%>' />
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox runat="server" ID="txtEditNombre" Text='<%# Eval("nombre")%>' />
                    </EditItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="<%$ Resources:AdminSucursales, gridSucursalesCalle%>" >
                    <ItemTemplate>
                        <asp:TextBox runat="server" ID="txtCalle" Text='<%# Eval("calle")%>' />
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox runat="server" ID="txtEditCalle" Text='<%# Eval("calle")%>' />
                    </EditItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="<%$ Resources:AdminSucursales, gridSucursalesAltura%>" >
                    <ItemTemplate>
                        <asp:TextBox runat="server" ID="txtAltura" Text='<%# Eval("altura")%>' />
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox runat="server" ID="txtEditAltura" Text='<%# Eval("altura")%>' />
                    </EditItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="<%$ Resources:AdminSucursales, gridSucursalesFecha%>" >
                    <ItemTemplate>
                        <asp:TextBox runat="server" ID="txtFecha" Text='<%# Eval("fecha")%>' />
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox runat="server" ID="txtEditFecha" Text='<%# Eval("fecha")%>' />
                    </EditItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="<%$ Resources:AdminSucursales, gridSucursalesLocalidad%>" >
                    <ItemTemplate>
                        <asp:TextBox runat="server" ID="txtLocalidad" Text='<%# Eval("localidad")%>' />
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:Label ID="lblLocalidad" runat="server" Text='<%# Eval("localidad")%>' Visible = "false"></asp:Label>
                        <asp:DropDownList ID="ddlLocalidades" runat="server"></asp:DropDownList>
                    </EditItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="<%$ Resources:AdminSucursales, gridSucursalesActivo%>" Visible="true">
                    <ItemTemplate>
                        <asp:TextBox runat="server" ID="txtActivo" Text='<%# Eval("es_activo")%>' Visible="true"/>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox runat="server" ID="txtEditActivo" Text='<%# Eval("es_activo")%>' Visible="true"/>
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
        <asp:Button ID="BtnNew" runat="server" Text="<%$ Resources:AdminSucursales, BtnNuevo%>" OnClick="BtnAddNew_Click"/>
        <asp:Button ID="BtnDelete" runat="server" Text="<%$ Resources:AdminSucursales, BtnEliminar%>" OnClick="BtnDelete_Click"/>
        </div>
</asp:Content>
