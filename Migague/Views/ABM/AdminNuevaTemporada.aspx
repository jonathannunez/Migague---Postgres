<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AdminNuevaTemporada.aspx.cs" Inherits="Migague.Views.ABM.AdminNuevaTemporada" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <br /><br />
    <asp:Label ID="lblAno" runat="server" Text="<%$ Resources:AdminTemporadas, lblNuevoTemporadaAno%>"></asp:Label>
    <asp:TextBox ID="txtAno" runat="server" placeholder="<%$ Resources:AdminTemporadas, phNuevoTemporadaAno%>"></asp:TextBox><br />
    <asp:Label ID="lblEstacion" runat="server" Text="<%$ Resources:AdminTemporadas, lblNuevoTemporadaEstacion%>"></asp:Label>
    <asp:TextBox ID="txtEstacion" runat="server" placeholder="<%$ Resources:AdminTemporadas, phNuevoTemporadaEstacion%>"></asp:TextBox><br />
    <asp:Button ID="BtnAdd" runat="server" Text="<%$ Resources:AdminTemporadas, BtnAdd%>" OnClick="BtnAdd_Click"/>
</asp:Content>
