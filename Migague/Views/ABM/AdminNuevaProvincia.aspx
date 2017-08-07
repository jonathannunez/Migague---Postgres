<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AdminNuevaProvincia.aspx.cs" Inherits="Migague.Views.ABM.AdminNuevaProvincia" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <br /><br />
    <asp:DropDownList ID="ddlPaises" runat="server" AutoPostBack="false" OnSelectedIndexChanged="ddlPaises_SelectedIndexChanged"></asp:DropDownList>
    <br />
    <asp:Label ID="lblNombre" runat="server" Text="<%$ Resources:AdminPaises, lblNuevoPaisNombre%>"></asp:Label>
    <asp:TextBox ID="txtNombre" runat="server" placeholder="<%$ Resources:AdminPaises, phNuevoPais%>"></asp:TextBox><br />
    <asp:Button ID="BtnAdd" runat="server" Text="<%$ Resources:AdminPaises, BtnAdd%>" OnClick="BtnAdd_Click"/>
</asp:Content>
