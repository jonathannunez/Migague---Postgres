<%@ Page Title="ADMIN PAGE" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AdministrarUsuarios.aspx.cs" Inherits="Migague.Views.ABM.AdministrarUsuarios" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        <asp:Literal runat="server" Text="<%$ Resources:MenuResource, submenuAdminUsers%>"></asp:Literal>
    </h2>
    <div class="form-group" style="text-align: left; width: 354px;">
        <asp:GridView ID="gridUsuarios" runat="server" AutoGenerateColumns="False" AllowPaging="True" 
            AllowSorting="True" OnRowUpdating="gridUsuarios_RowUpdating" 
            OnRowEditing="gridUsuarios_RowEditing" OnRowCancelingEdit="gridUsuarios_RowCancelingEdit"
            OnRowDataBound ="gridUsuarios_RowDataBound">
            <Columns>
                <asp:TemplateField HeaderText="<%$ Resources:AdminUsuarios, gridUsuariosID%>" Visible="false">
                    <ItemTemplate>
                        <asp:TextBox runat="server" ID="txtIdUser" Text='<%# Eval("id")%>' Visible="false"/>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox runat="server" ID="txtEditIdUser" Text='<%# Eval("id")%>' Visible="false"/>
                    </EditItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="<%$ Resources:AdminUsuarios, gridUsuariosNombre%>">
                    <ItemTemplate>
                        <asp:TextBox runat="server" ID="txtNombreUser" Text='<%# Eval("nombre")%>' />
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox runat="server" ID="txtEditNombreUser" Text='<%# Eval("nombre")%>' />
                    </EditItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="<%$ Resources:AdminUsuarios, gridUsuariosPassword%>">
                    <ItemTemplate>
                         <asp:TextBox runat="server" ID="txtPassword" Text='<%# Eval("password")%>' />
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox runat="server" ID="txtEditPassword" Text='<%# Eval("password")%>' />
                    </EditItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="<%$ Resources:AdminUsuarios, gridUsuariosNombreRol%>">
                    <ItemTemplate>
                         <asp:TextBox runat="server" ID="txtRol" Text='<%# Eval("rol.nombre")%>' />
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:Label ID="lblRol" runat="server" Text='<%# Eval("rol.nombre")%>' Visible = "false"></asp:Label>
                        <asp:DropDownList runat="server" id="ddlEditRol" ></asp:DropDownList>
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
        <asp:Button ID="BtnAddNewUser" runat="server" Text="<%$ Resources:AdminUsuarios, BtnNuevoUsuario%>" OnClick="BtnAddNewUser_Click"/>
        <asp:Button ID="BtnAdminSucursales" runat="server" Text="<%$ Resources:AdminUsuarios, BtnAdminSucursales%>" OnClick="BtnAdminSucursales_Click"/>
        <asp:Button ID="BtnEliminarUsuario" runat="server" Text="<%$ Resources:AdminUsuarios, BtnEliminar%>" OnClick="BtnEliminarUsuario_Click"/>
        </div>
     
</asp:Content>
