<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AdminNuevoProveedor.aspx.cs" Inherits="Migague.Views.ABM.AdminNuevoProveedor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <br /><br />
    <asp:Label ID="lblNombre" runat="server" Text="<%$ Resources:AdminProveedores, lblNuevoProveedorNombre%>"></asp:Label>
    <asp:TextBox ID="txtNombre" runat="server" placeholder="<%$ Resources:AdminProveedores, phNuevoProveedor%>"></asp:TextBox><br />
    <asp:Button ID="BtnAdd" runat="server" Text="<%$ Resources:AdminProveedores, BtnAdd%>" OnClick="BtnAdd_Click"/>
</asp:Content>
