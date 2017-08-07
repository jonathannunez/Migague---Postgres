<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AdminNuevoColor.aspx.cs" Inherits="Migague.Views.ABM.AdminNuevoColor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <br /><br />
    <asp:Label ID="lblNombre" runat="server" Text="<%$ Resources:AdminColores, lblNuevoColorNombre%>"></asp:Label>
    <asp:TextBox ID="txtNombre" runat="server" placeholder="<%$ Resources:AdminColores, phNuevoColor%>"></asp:TextBox><br />
    <asp:Button ID="BtnAdd" runat="server" Text="<%$ Resources:AdminColores, BtnAdd%>" OnClick="BtnAdd_Click"/>
</asp:Content>
