<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AdminNuevoModelo.aspx.cs" Inherits="Migague.Views.ABM.AdminNuevoModelo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <br /><br />
    
    <asp:Label ID="lblCodigo" runat="server" Text="<%$ Resources:AdminModelos, lblNuevoModeloCodigo%>"></asp:Label>
    <asp:TextBox ID="txtCodigo" runat="server" placeholder="<%$ Resources:AdminModelos, phNuevoModeloCodigo%>"></asp:TextBox><br />

    <asp:Label ID="lblNombre" runat="server" Text="<%$ Resources:AdminModelos, lblNuevoModeloNombre%>"></asp:Label>
    <asp:TextBox ID="txtNombre" runat="server" placeholder="<%$ Resources:AdminModelos, phNuevoModelo%>"></asp:TextBox><br />
    
    <asp:Label ID="lblProveedor" runat="server" Text="<%$ Resources:AdminModelos, lblNuevoModeloProveedor%>"></asp:Label>
    <asp:DropDownList ID="ddlProveedor" runat="server"></asp:DropDownList><br />

    <asp:Label ID="lblTemporada" runat="server" Text="<%$ Resources:AdminModelos, lblNuevoModeloTemporada%>"></asp:Label>
    <asp:DropDownList ID="ddlTemporada" runat="server"></asp:DropDownList><br />

    <asp:Label ID="lblTipoProducto" runat="server" Text="<%$ Resources:AdminModelos, lblNuevoModeloTipoProducto%>"></asp:Label>
    <asp:DropDownList ID="ddlTipoProducto" runat="server"></asp:DropDownList><br />

    <asp:Button ID="BtnAdd" runat="server" Text="<%$ Resources:AdminModelos, BtnAdd%>" OnClick="BtnAdd_Click"/>
</asp:Content>
