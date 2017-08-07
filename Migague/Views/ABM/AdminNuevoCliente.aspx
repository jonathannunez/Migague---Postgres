<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AdminNuevoCliente.aspx.cs" Inherits="Migague.Views.ABM.AdminNuevoCliente" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <br /><br />
    <asp:Label ID="lblRazonSocial" runat="server" Text="<%$ Resources:AdminClientes, lblNuevoClienteRazonSocial%>"></asp:Label>
    <asp:TextBox ID="txtRazonSocial" runat="server" placeholder="<%$ Resources:AdminClientes, phNuevoClienteRazonSocial%>"></asp:TextBox><br />

    <asp:Label ID="lblNombre" runat="server" Text="<%$ Resources:AdminClientes, lblNuevoClienteNombre%>"></asp:Label>
    <asp:TextBox ID="txtNombre" runat="server" placeholder="<%$ Resources:AdminClientes, phNuevoClienteNombre%>"></asp:TextBox><br />

    <asp:Label ID="lblCuit" runat="server" Text="<%$ Resources:AdminClientes, lblNuevoClienteCuit%>"></asp:Label>
    <asp:TextBox ID="txtCuit" runat="server" placeholder="<%$ Resources:AdminClientes, phNuevoClienteCuit%>"></asp:TextBox><br />

    <asp:Label ID="lblEmail" runat="server" Text="<%$ Resources:AdminClientes, lblNuevoClienteEmail%>"></asp:Label>
    <asp:TextBox ID="txtEmail" runat="server" placeholder="<%$ Resources:AdminClientes, phNuevoClienteEmail%>"></asp:TextBox><br />

    <asp:Label ID="lblCatTribut" runat="server" Text="<%$ Resources:AdminClientes, lblNuevoClienteCatTributaria%>"></asp:Label>
    <asp:DropDownList ID="ddlCategoriasTributarias" runat="server" AutoPostBack="false"></asp:DropDownList>
    <br />
    <asp:Label ID="lblCatListaPrecio" runat="server" Text="<%$ Resources:AdminClientes, lblNuevoClienteCatListaPrecio%>"></asp:Label>
    <asp:DropDownList ID="ddlCategoriasListaPrecios" runat="server" AutoPostBack="false"></asp:DropDownList>
    <br />
    
    <asp:Label ID="lblDirecciones" runat="server" Text="<%$ Resources:AdminClientes, lblDirecciones%>" Font-Bold="true"></asp:Label><br />
    <asp:PlaceHolder ID="placeholderDirecciones" runat="server"></asp:PlaceHolder> <br /> 
    <asp:Button ID="btnAddDireccion" runat="server" Text="<%$ Resources:AdminClientes, BtnAddDireccion%>" OnClick="btnAddDireccion_Click"/><br /><br />

    <asp:Label ID="lblTelefonos" runat="server" Text="<%$ Resources:AdminClientes, lblTelefonos%>" Font-Bold="true"></asp:Label><br />
    <asp:PlaceHolder ID="placeholderTelefonos" runat="server"></asp:PlaceHolder> <br /> 
    <asp:Button ID="BtnAddTelefono" runat="server" Text="<%$ Resources:AdminClientes, BtnAddTelefono%>" OnClick="BtnAddTelefono_Click"/><br /><br />

    <asp:Label ID="lblTransportes" runat="server" Text="<%$ Resources:AdminClientes, lblTransportes%>" Font-Bold="true"></asp:Label><br />
    <asp:PlaceHolder ID="placeholderTransportes" runat="server"></asp:PlaceHolder> <br />
    <asp:Button ID="btnAddTransporte" runat="server" Text="<%$ Resources:AdminClientes, btnAddTransporte%>" OnClick="btnAddTransporte_Click"/><br /><br />
    
    <asp:Button ID="BtnAdd" runat="server" Text="<%$ Resources:AdminClientes, BtnAdd%>" OnClick="BtnAdd_Click"/>

    
</asp:Content>
