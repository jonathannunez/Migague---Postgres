<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="NuevoStock.aspx.cs" Inherits="Migague.Views.Stock.NuevoStock" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <br /><br />
    <asp:DropDownList ID="ddlArticulos" runat="server" AutoPostBack="false" OnSelectedIndexChanged="ddlArticulos_SelectedIndexChanged"></asp:DropDownList><br />
    <asp:DropDownList ID="ddlColores" runat="server" AutoPostBack="false" OnSelectedIndexChanged="ddlColores_SelectedIndexChanged"></asp:DropDownList> <br />
    <asp:Label ID="lblCantidad" runat="server" Text="<%$ Resources:Stock, lblNuevoStockCantidad%>"></asp:Label>
    <asp:TextBox ID="txtCantidad" runat="server" placeholder="<%$ Resources:Stock, phNuevoStockCantidad%>"></asp:TextBox><br />
    <asp:Button ID="BtnAdd" runat="server" Text="<%$ Resources:Stock, BtnAdd%>" OnClick="BtnAdd_Click"/>
</asp:Content>
