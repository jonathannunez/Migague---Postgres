<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AdminNuevaSucursal.aspx.cs" Inherits="Migague.Views.ABM.AdminNuevaSucursal" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <br /><br />
    
    <asp:Label ID="lblNombre" runat="server" Text="<%$ Resources:AdminSucursales, lblNuevaSucursalNombre%>"></asp:Label>
    <asp:TextBox ID="txtNombre" runat="server" placeholder="<%$ Resources:AdminSucursales, phNuevaSucursalNombre%>"></asp:TextBox><br />

    <asp:Label ID="lblCalle" runat="server" Text="<%$ Resources:AdminSucursales, lblNuevaSucursalCalle%>"></asp:Label>
    <asp:TextBox ID="txtCalle" runat="server" placeholder="<%$ Resources:AdminSucursales, phNuevaSucursalCalle%>"></asp:TextBox><br />

    <asp:Label ID="lblAltura" runat="server" Text="<%$ Resources:AdminSucursales, lblNuevaSucursalAltura%>"></asp:Label>
    <asp:TextBox ID="txtAltura" runat="server" placeholder="<%$ Resources:AdminSucursales, phNuevaSucursalAltura%>"></asp:TextBox><br />

    <asp:Label ID="lblLocalidad" runat="server" Text="<%$ Resources:AdminSucursales, lblNuevaSucursalLocalidad%>"></asp:Label>
    <asp:DropDownList ID="ddlLocalidad" runat="server"></asp:DropDownList><br />

    <asp:Button ID="BtnAdd" runat="server" Text="<%$ Resources:AdminSucursales, BtnAdd%>" OnClick="BtnAdd_Click"/>
</asp:Content>
