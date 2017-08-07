<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AdminNuevoUsuario.aspx.cs" Inherits="Migague.Views.ABM.AdminNuevoUsuario" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <br /><br />
    <asp:Label ID="lblNombreUsuario" runat="server" Text="<%$ Resources:AdminUsuarios, txtNuevoUsuarioNombre%>">
    </asp:Label><asp:TextBox ID="txtNombreUser" runat="server" placeholder="<%$ Resources:AdminUsuarios, phNuevoUsuarioNombre%>"></asp:TextBox><br />
    <asp:Label ID="lblPasswordUsuario" runat="server" Text="<%$ Resources:AdminUsuarios, txtNuevoUsuarioContraseña%>"> </asp:Label>
    <asp:TextBox ID="txtPasswordUser" runat="server" placeholder="<%$ Resources:AdminUsuarios, phNuevoUsuarioPassword%>"></asp:TextBox><br />
    <asp:Label ID="lblRol" runat="server" Text="<%$ Resources:AdminUsuarios, ddlNuevoUsuarioRoles%>"> </asp:Label>
    <asp:DropDownList ID="ddlRoles" runat="server"></asp:DropDownList><br />
    <asp:Button ID="btnAddUser" runat="server" Text="<%$ Resources:AdminUsuarios, BtnAddUser%>" OnClick="btnAddUser_Click" />
</asp:Content>
