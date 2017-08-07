<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AdminNuevoTalle.aspx.cs" Inherits="Migague.Views.ABM.AdminNuevoTalle" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <br /><br />
    <asp:Label ID="lblNombre" runat="server" Text="<%$ Resources:AdminTalles, lblNuevoTalleNombre%>"></asp:Label>
    <asp:TextBox ID="txtNombre" runat="server" placeholder="<%$ Resources:AdminTalles, phNuevoTalle%>"></asp:TextBox><br />
    <asp:Button ID="BtnAdd" runat="server" Text="<%$ Resources:AdminTalles, BtnAdd%>" OnClick="BtnAdd_Click"/>
</asp:Content>
