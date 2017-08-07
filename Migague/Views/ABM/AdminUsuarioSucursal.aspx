<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AdminUsuarioSucursal.aspx.cs" Inherits="Migague.Views.ABM.AdminUsuarioSucursal" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <br /><br />
    <div class="form-group" style="text-align: left; width: 354px;">
        <asp:DropDownList ID="ddlUsuarios" runat="server" OnSelectedIndexChanged="ddlUsuarios_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
    </div>
    <div class="form-group" style="text-align: left; width: 354px;">
        <asp:CheckBoxList ID="chListSucursales" runat="server"></asp:CheckBoxList>
    </div>
    <div class="form-group" style="text-align: left; width: 354px;">
        <asp:Button ID="BtnUpdateSucxUsuario" runat="server" Text="Update" OnClick="BtnUpdateSucxUsuario_Click"/>
    </div>
</asp:Content>
