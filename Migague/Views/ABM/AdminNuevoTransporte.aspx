﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AdminNuevoTransporte.aspx.cs" Inherits="Migague.Views.ABM.AdminNuevoTransporte" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <br /><br />
    <asp:Label ID="lblNombre" runat="server" Text="<%$ Resources:AdminTransportes, lblNuevoTransporteNombre%>"></asp:Label>
    <asp:TextBox ID="txtNombre" runat="server" placeholder="<%$ Resources:AdminTransportes, phNuevoTransporte%>"></asp:TextBox><br />
    <asp:Button ID="BtnAdd" runat="server" Text="<%$ Resources:AdminTransportes, BtnAdd%>" OnClick="BtnAdd_Click"/>
</asp:Content>
