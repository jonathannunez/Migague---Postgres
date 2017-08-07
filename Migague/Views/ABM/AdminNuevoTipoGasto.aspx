<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AdminNuevoTipoGasto.aspx.cs" Inherits="Migague.Views.ABM.AdminNuevoTipoGasto" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <br /><br />
    <asp:Label ID="lblNombre" runat="server" Text="<%$ Resources:AdminTiposGastos, lblNuevoTipoGastoNombre%>"></asp:Label>
    <asp:TextBox ID="txtNombre" runat="server" placeholder="<%$ Resources:AdminTiposGastos, phNuevoTipoGasto%>"></asp:TextBox><br />
    <asp:Button ID="BtnAdd" runat="server" Text="<%$ Resources:AdminTiposGastos, BtnAdd%>" OnClick="BtnAdd_Click"/>
</asp:Content>
