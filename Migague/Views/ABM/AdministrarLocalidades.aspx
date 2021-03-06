﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AdministrarLocalidades.aspx.cs" Inherits="Migague.Views.ABM.AdministrarLocalidades" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        <asp:Literal runat="server" Text="<%$ Resources:MenuResource, submenuAdminLocalidades%>"></asp:Literal>
    </h2>

    <div class="form-group" style="text-align: left">
        <asp:Label ID="lblPais" runat="server" Text="<%$ Resources:AdminLocalidades, lblPais%>"></asp:Label>
        <asp:DropDownList ID="ddlPaises" runat="server" OnSelectedIndexChanged="ddlPaises_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
        <br />
        <asp:Label ID="lblProvincia" runat="server" Text="<%$ Resources:AdminLocalidades, lblProvincia%>"></asp:Label>
        <asp:DropDownList ID="ddlProvincias" runat="server" OnSelectedIndexChanged="ddlProvincias_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
        <br />
        <br />
        <asp:GridView ID="gridLocalidades" runat="server" AutoGenerateColumns="False" AllowPaging="true"
            AllowSorting="true" OnRowUpdating="gridLocalidades_RowUpdating" OnRowEditing="gridLocalidades_RowEditing"
            OnRowCancelingEdit="gridLocalidades_RowCancelingEdit">
            <Columns>
                <asp:TemplateField HeaderText="<%$ Resources:AdminLocalidades, gridLocalidadesIDProvincia%>" Visible="false">
                    <ItemTemplate>
                        <asp:TextBox runat="server" ID="txtIdProvincia" Text='<%# Eval("provincia.id")%>' Visible="false"/>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox runat="server" ID="txtEditIdProvincia" Text='<%# Eval("provincia.id")%>' Visible="false"/>
                    </EditItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="<%$ Resources:AdminLocalidades, gridLocalidadesID%>" Visible="false">
                    <ItemTemplate>
                        <asp:TextBox runat="server" ID="txtId" Text='<%# Eval("id")%>' Visible="false"/>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox runat="server" ID="txtEditId" Text='<%# Eval("id")%>' Visible="false"/>
                    </EditItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="<%$ Resources:AdminLocalidades, gridLocalidadesNombre%>" >
                    <ItemTemplate>
                        <asp:TextBox runat="server" ID="txtNombre" Text='<%# Eval("nombre")%>' />
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox runat="server" ID="txtEditNombre" Text='<%# Eval("nombre")%>' />
                    </EditItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="<%$ Resources:AdminLocalidades, gridLocalidadesFecha%>" >
                    <ItemTemplate>
                        <asp:TextBox runat="server" ID="txtFecha" Text='<%# Eval("fecha")%>' />
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox runat="server" ID="txtEditFecha" Text='<%# Eval("fecha")%>' />
                    </EditItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="<%$ Resources:AdminLocalidades, gridLocalidadesActivo%>" Visible="true">
                    <ItemTemplate>
                        <asp:TextBox runat="server" ID="txtActivo" Text='<%# Eval("es_activo")%>' Visible="true"/>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox runat="server" ID="txtEditActivo" Text='<%# Eval("es_activo")%>' Visible="true"/>
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
        <asp:Button ID="BtnNew" runat="server" Text="<%$ Resources:AdminLocalidades, BtnNuevo%>" OnClick="BtnAddNew_Click"/>
        <asp:Button ID="BtnDelete" runat="server" Text="<%$ Resources:AdminLocalidades, BtnEliminar%>" OnClick="BtnDelete_Click"/>
        </div>
</asp:Content>
