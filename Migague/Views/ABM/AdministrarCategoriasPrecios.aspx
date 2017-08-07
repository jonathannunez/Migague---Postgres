<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AdministrarCategoriasPrecios.aspx.cs" Inherits="Migague.Views.ABM.AdministrarCategoriasPrecios" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        <asp:Literal runat="server" Text="<%$ Resources:MenuResource, submenuAdminCategoriasPrecios%>"></asp:Literal>
    </h2>

    <div class="form-group" style="text-align: left">
        <asp:GridView ID="gridCategoriasPrecios" runat="server" AutoGenerateColumns="False" AllowPaging="true"
            AllowSorting="true" OnRowUpdating="gridCategoriasPrecios_RowUpdating" OnRowEditing="gridCategoriasPrecios_RowEditing"
            OnRowCancelingEdit="gridCategoriasPrecios_RowCancelingEdit">
            <Columns>
                <asp:TemplateField HeaderText="<%$ Resources:AdminCategoriasPrecios, gridCategoriasPreciosID%>" Visible="false">
                    <ItemTemplate>
                        <asp:TextBox runat="server" ID="txtId" Text='<%# Eval("id")%>' Visible="false"/>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox runat="server" ID="txtEditId" Text='<%# Eval("id")%>' Visible="false"/>
                    </EditItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="<%$ Resources:AdminCategoriasPrecios, gridCategoriasPreciosNombre%>" >
                    <ItemTemplate>
                        <asp:TextBox runat="server" ID="txtNombre" Text='<%# Eval("nombre")%>' />
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox runat="server" ID="txtEditNombre" Text='<%# Eval("nombre")%>' />
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$ Resources:AdminCategoriasPrecios, gridCategoriasPreciosActivo%>" Visible="false">
                    <ItemTemplate>
                        <asp:TextBox runat="server" ID="txtActivo" Text='<%# Eval("es_activo")%>' Visible="false"/>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox runat="server" ID="txtEditActivo" Text='<%# Eval("es_activo")%>' Visible="false"/>
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
        <asp:Button ID="BtnNew" runat="server" Text="<%$ Resources:AdminCategoriasPrecios, BtnNuevo%>" OnClick="BtnAddNew_Click"/>
        <asp:Button ID="BtnDelete" runat="server" Text="<%$ Resources:AdminCategoriasPrecios, BtnEliminar%>" OnClick="BtnDelete_Click"/>
        </div>
</asp:Content>
