<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AdminNuevoRol.aspx.cs" Inherits="Migague.Views.ABM.AdminNuevoRol" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <br /><br />
    <asp:Label ID="lblNombreRol" runat="server" Text="<%$ Resources:AdminRoles, lblNuevoRolNombre%>"></asp:Label>
    <asp:TextBox ID="txtNombreRol" runat="server" placeholder="<%$ Resources:AdminRoles, phNuevoRolNombre%>"></asp:TextBox><br />
    <asp:Button ID="BtnAddRol" runat="server" Text="<%$ Resources:AdminRoles, BtnAddRol%>" OnClick="BtnAddRol_Click"/>
</asp:Content>
