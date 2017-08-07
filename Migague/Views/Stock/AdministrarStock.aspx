<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AdministrarStock.aspx.cs" Inherits="Migague.Views.Stock.AdministrarStock" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        <asp:Literal runat="server" Text="<%$ Resources:MenuResource, submenuAdminStock%>"></asp:Literal>
    </h2>

    <div class="form-group" style="text-align: left">
        <asp:GridView ID="gridStock" runat="server" AutoGenerateColumns="False" AllowPaging="true"
            AllowSorting="true" OnRowUpdating="gridStock_RowUpdating" OnRowEditing="gridStock_RowEditing"
            OnRowCancelingEdit="gridStock_RowCancelingEdit" OnRowDataBound="gridStock_RowDataBound">
            <Columns>

                <asp:TemplateField HeaderText="<%$ Resources:Stock, gridStockID%>" Visible="false">
                    <ItemTemplate>
                        <asp:TextBox runat="server" ID="txtId" Text='<%# Eval("id")%>' Visible="false"/>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox runat="server" ID="txtEditId" Text='<%# Eval("id")%>' Visible="false"/>
                    </EditItemTemplate>
                </asp:TemplateField>

                 <asp:TemplateField HeaderText="<%$ Resources:Stock, gridStockCantidadNeta%>" >
                    <ItemTemplate>
                        <asp:TextBox runat="server" ID="txtCantidadNeta" Text='<%# Eval("cantidad_neta")%>' />
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox runat="server" ID="txtEditCantidadNeta" Text='<%# Eval("cantidad_neta")%>' Visible="false"/>
                    </EditItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="<%$ Resources:Stock, gridStockInOut%>" >
                    <ItemTemplate>
                        <asp:TextBox runat="server" ID="txtInOut" Text='<%# Eval("in_out")%>' />
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox runat="server" ID="txtEditInOut" Text='<%# Eval("in_out")%>' Visible="false"/>
                    </EditItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="<%$ Resources:Stock, gridStockCantidad%>" >
                    <ItemTemplate>
                        <asp:TextBox runat="server" ID="txtCantidad" Text='<%# Eval("cantidad")%>' />
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox runat="server" ID="txtEditCantidad" Text='<%# Eval("cantidad")%>' Visible="false"/>
                    </EditItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="<%$ Resources:Stock, gridStockFecha%>" >
                    <ItemTemplate>
                        <asp:TextBox runat="server" ID="txtFecha" Text='<%# Eval("fecha")%>' />
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox runat="server" ID="txtEditFecha" Text='<%# Eval("fecha")%>' Visible="false"/>
                    </EditItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="<%$ Resources:Stock, gridStockArticulo%>" >
                    <ItemTemplate>
                        <asp:TextBox runat="server" ID="txtArticulo" Text='<%# Eval("articulo")%>' />
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox runat="server" ID="txtEditArticulo" Text='<%# Eval("articulo")%>' Visible="false"/>
                    </EditItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="<%$ Resources:Stock, gridStockArticuloColor%>" >
                    <ItemTemplate>
                        <asp:TextBox runat="server" ID="txtArticuloColor" Text='<%# Eval("color")%>' />
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox runat="server" ID="txtEditArticuloColor" Text='<%# Eval("color")%>' Visible="false"/>
                    </EditItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="<%$ Resources:Stock, gridStockActivo%>" Visible="false">
                    <ItemTemplate>
                        <asp:TextBox runat="server" ID="txtActivo" Text='<%# Eval("esactivo")%>' Visible="false"/>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox runat="server" ID="txtEditActivo" Text='<%# Eval("esactivo")%>' Visible="false"/>
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
        <asp:Button ID="BtnNew" runat="server" Text="<%$ Resources:Stock, BtnNuevo%>" OnClick="BtnAddNew_Click"/>
        <asp:Button ID="BtnDelete" runat="server" Text="<%$ Resources:Stock, BtnEliminar%>" OnClick="BtnDelete_Click"/>
        </div>
</asp:Content>
