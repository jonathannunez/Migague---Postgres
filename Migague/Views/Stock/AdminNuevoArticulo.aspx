<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AdminNuevoArticulo.aspx.cs" Inherits="Migague.Views.ABM.AdminNuevoArticulo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <br /><br />
    
    <asp:Label ID="lblModelo" runat="server" Text="<%$ Resources:AdminArticulos, lblNuevoArticuloModelo%>"></asp:Label>
    <asp:DropDownList ID="ddlModelo" runat="server"></asp:DropDownList><br />

    <asp:Label ID="lblTalle" runat="server" Text="<%$ Resources:AdminArticulos, lblNuevoArticuloTalle%>"></asp:Label>
    <asp:DropDownList ID="ddlTalle" runat="server"></asp:DropDownList><br />

    <asp:Label ID="lblColor" runat="server" Text="<%$ Resources:AdminArticulos, lblNuevoArticuloColor%>"></asp:Label>
    <asp:DropDownList ID="ddlColor" runat="server"></asp:DropDownList><br />

    <asp:Label ID="lblPreciomay" runat="server" Text="<%$ Resources:AdminArticulos, lblNuevoArticuloPreciomay%>"></asp:Label>
    <asp:TextBox ID="txtPreciomay" runat="server" placeholder="<%$ Resources:AdminArticulos, phNuevoArticuloPreciomay%>"></asp:TextBox><br />
    
    <asp:Label ID="lblPreciomin" runat="server" Text="<%$ Resources:AdminArticulos, lblNuevoArticuloPreciomin%>"></asp:Label>
    <asp:TextBox ID="txtPreciomin" runat="server" placeholder="<%$ Resources:AdminArticulos, phNuevoArticuloPreciomin%>"></asp:TextBox><br />  

     <asp:Label ID="lblStock" runat="server" Text="<%$ Resources:AdminArticulos, lblNuevoArticuloStock%>"></asp:Label>
    <asp:TextBox ID="txtStock" runat="server" placeholder="<%$ Resources:AdminArticulos, phNuevoArticuloStock%>"></asp:TextBox><br />  

    <asp:Button ID="BtnAdd" runat="server" Text="<%$ Resources:AdminArticulos, BtnAdd%>" OnClick="BtnAdd_Click"/>
</asp:Content>
