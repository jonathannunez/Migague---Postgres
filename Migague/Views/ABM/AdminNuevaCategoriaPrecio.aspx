<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AdminNuevaCategoriaPrecio.aspx.cs" Inherits="Migague.Views.ABM.AdminNuevaCategoriaPrecio" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <br /><br />
    <asp:Label ID="lblNombre" runat="server" Text="<%$ Resources:AdminCategoriasPrecios, lblNuevoCategoriaPrecioNombre%>"></asp:Label>
    <asp:TextBox ID="txtNombre" runat="server" placeholder="<%$ Resources:AdminCategoriasPrecios, phNuevoCategoriaPrecio%>"></asp:TextBox><br />
    <asp:Button ID="BtnAdd" runat="server" Text="<%$ Resources:AdminCategoriasPrecios, BtnAdd%>" OnClick="BtnAdd_Click"/>
</asp:Content>
