<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AdministrarRoles.aspx.cs" Inherits="Migague.Views.ABM.AdministrarRoles" %>
<asp:Content ID="bodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        <asp:Literal runat="server" Text="<%$ Resources:MenuResource, submenuAdminRoles%>"></asp:Literal>
    </h2>

    <div class="form-group" style="text-align: left">
        <asp:GridView ID="gridRoles" runat="server" AutoGenerateColumns="False" AllowPaging="true"
            AllowSorting="true" OnRowUpdating="gridRoles_RowUpdating" OnRowEditing="gridRoles_RowEditing"
            OnRowCancelingEdit="gridRoles_RowCancelingEdit">
            <Columns>
                <asp:TemplateField HeaderText="<%$ Resources:AdminRoles, gridRolesID%>" Visible="false">
                    <ItemTemplate>
                        <asp:TextBox runat="server" ID="txtIdRol" Text='<%# Eval("id")%>' Visible="false"/>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox runat="server" ID="txtEditIdRol" Text='<%# Eval("id")%>' Visible="false"/>
                    </EditItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="<%$ Resources:AdminRoles, gridRolesNombre%>" >
                    <ItemTemplate>
                        <asp:TextBox runat="server" ID="txtNombre" Text='<%# Eval("nombre")%>' />
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox runat="server" ID="txtEditNombre" Text='<%# Eval("nombre")%>' />
                    </EditItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="<%$ Resources:AdminRoles, gridRolesActivo%>" >
                    <ItemTemplate>
                        <asp:TextBox runat="server" ID="txtActivo" Text='<%# Eval("es_activo")%>' />
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox runat="server" ID="txtEditActivo" Text='<%# Eval("es_activo")%>' />
                    </EditItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField>
                    <HeaderTemplate>
                        <asp:CheckBox ID="chkDeleteAll" runat="server" OnCheckedChanged="chkDeleteAll_CheckedChanged" AutoPostBack="true"/>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:CheckBox ID="chkDelete" runat="server" OnCheckedChanged="chkDelete_CheckedChanged" AutoPostBack="true"/>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:CommandField ButtonType="Link" ShowEditButton="True" ShowCancelButton="true" />
            </Columns>
        </asp:GridView>
        <br />
        <asp:Button ID="BtnAddNewRol" runat="server" Text="<%$ Resources:AdminRoles, BtnNuevoRol%>" OnClick="BtnAddNewRol_Click"/>
        <asp:Button ID="BtnDeleteRol" runat="server" Text="<%$ Resources:AdminRoles, BtnEliminar%>" OnClick="BtnDeleteRol_Click"/>
        </div>

</asp:Content>
