<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AdminNuevoTipoProducto.aspx.cs" Inherits="Migague.Views.ABM.AdminNuevoTipoProducto" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <br /><br />
    <asp:Label ID="lblNombre" runat="server" Text="<%$ Resources:AdminTiposProductos, lblNuevoTipoProductoNombre%>"></asp:Label>
    <asp:TextBox ID="txtNombre" runat="server" placeholder="<%$ Resources:AdminTiposProductos, phNuevoTipoProducto%>"></asp:TextBox><br />
    <asp:Button ID="BtnAdd" runat="server" Text="<%$ Resources:AdminTiposProductos, BtnAdd%>" OnClick="BtnAdd_Click"/>
</asp:Content>
