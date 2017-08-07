<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AdministrarFuncionalidades.aspx.cs" Inherits="Migague.Views.ABM.AdministrarFuncionalidades" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <br /><br />
    <div class="form-group" style="text-align: left; width: 354px;">
        <asp:DropDownList ID="ddlRoles" runat="server" OnSelectedIndexChanged="ddlRoles_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
    </div>
    <div class="form-group" style="text-align: left; width: 354px;">
        <asp:CheckBoxList ID="chListFuncionalidades" runat="server"></asp:CheckBoxList>
    </div>
    <div class="form-group" style="text-align: left; width: 354px;">
        <asp:Button ID="BtnUpdateFunxRol" runat="server" Text="Update" OnClick="BtnUpdateFunxRol_Click"/>
    </div>
</asp:Content>
