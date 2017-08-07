<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AdminNuevaLocalidad.aspx.cs" Inherits="Migague.Views.ABM.AdminNuevaLocalidad" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <br /><br />
    <asp:DropDownList ID="ddlPaises" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlPaises_SelectedIndexChanged"></asp:DropDownList>
    <br />
    <asp:DropDownList ID="ddlProvincias" runat="server" AutoPostBack="false"></asp:DropDownList>
    <br />
    <asp:Label ID="lblNombre" runat="server" Text="<%$ Resources:AdminLocalidades, lblNuevaLocalidadNombre%>"></asp:Label>
    <asp:TextBox ID="txtNombre" runat="server" placeholder="<%$ Resources:AdminLocalidades, phNuevaLocalidad%>"></asp:TextBox><br />
    <asp:Button ID="BtnAdd" runat="server" Text="<%$ Resources:AdminLocalidades, BtnAdd%>" OnClick="BtnAdd_Click"/>
</asp:Content>
