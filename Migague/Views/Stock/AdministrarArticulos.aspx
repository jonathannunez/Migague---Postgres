<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AdministrarArticulos.aspx.cs" Inherits="Migague.Views.ABM.AdministrarArticulos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        <asp:Literal runat="server" Text="<%$ Resources:MenuResource, submenuAdminArticulos%>"></asp:Literal>
    </h2>

    <div class="form-group" style="text-align: left">
        <asp:GridView ID="gridArticulos" runat="server" AutoGenerateColumns="False" AllowPaging="true"
            AllowSorting="true" OnRowUpdating="gridArticulos_RowUpdating" OnRowEditing="gridArticulos_RowEditing"
            OnRowCancelingEdit="gridArticulos_RowCancelingEdit" OnRowDataBound="gridArticulos_RowDataBound">
            <Columns>

                <asp:TemplateField HeaderText="<%$ Resources:AdminArticulos, gridArticulosID%>" Visible="false">
                    <ItemTemplate>
                        <asp:TextBox runat="server" ID="txtId" Text='<%# Eval("id")%>' Visible="false"/>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox runat="server" ID="txtEditId" Text='<%# Eval("id")%>' Visible="false"/>
                    </EditItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="<%$ Resources:AdminArticulos, gridArticulosModelo%>" >
                    <ItemTemplate>
                        <asp:TextBox runat="server" ID="txtModelo" Text='<%# Eval("modelo.nombre")%>' />
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:Label ID="lblModelo" runat="server" Text='<%# Eval("modelo.nombre")%>' Visible = "false"></asp:Label>
                        <asp:DropDownList ID="ddlModelos" runat="server"></asp:DropDownList>
                    </EditItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="<%$ Resources:AdminArticulos, gridArticulosTalle%>" >
                    <ItemTemplate>
                        <asp:TextBox runat="server" ID="txtTalle" Text='<%# Eval("talle.nombre")%>' />
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:Label ID="lblTalle" runat="server" Text='<%# Eval("talle.nombre")%>' Visible = "false"></asp:Label>
                        <asp:DropDownList ID="ddlTalles" runat="server"></asp:DropDownList>
                    </EditItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="<%$ Resources:AdminArticulos, gridArticulosColor%>" >
                    <ItemTemplate>
                        <asp:TextBox runat="server" ID="txtColor" Text='<%# Eval("color.nombre")%>' />
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:Label ID="lblColor" runat="server" Text='<%# Eval("color.nombre")%>' Visible = "false"></asp:Label>
                        <asp:DropDownList ID="ddlColores" runat="server"></asp:DropDownList>
                    </EditItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="<%$ Resources:AdminArticulos, gridArticulosPreciomay%>" >
                    <ItemTemplate>
                        <asp:TextBox runat="server" ID="txtPreciomay" Text='<%# Eval("precio_may")%>' />
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox runat="server" ID="txtEditPreciomay" Text='<%# Eval("precio_may")%>' />
                    </EditItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="<%$ Resources:AdminArticulos, gridArticulosPreciomin%>" >
                    <ItemTemplate>
                        <asp:TextBox runat="server" ID="txtPreciomin" Text='<%# Eval("precio_min")%>' />
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox runat="server" ID="txtEditPreciomin" Text='<%# Eval("precio_min")%>' />
                    </EditItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="<%$ Resources:AdminArticulos, gridArticulosCodbarra%>" >
                    <ItemTemplate>
                        <asp:TextBox runat="server" ID="txtCodbarra" Text='<%# Eval("cod_barra")%>' />
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox runat="server" ID="txtEditCodbarra" Text='<%# Eval("cod_barra")%>' />
                    </EditItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="<%$ Resources:AdminArticulos, gridArticulosActivo%>" Visible="false">
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
        <asp:Button ID="BtnNew" runat="server" Text="<%$ Resources:AdminArticulos, BtnNuevo%>" OnClick="BtnAddNew_Click"/>
        <asp:Button ID="BtnDelete" runat="server" Text="<%$ Resources:AdminArticulos, BtnEliminar%>" OnClick="BtnDelete_Click"/>
        </div>
</asp:Content>
