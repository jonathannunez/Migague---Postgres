<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TransferirStock.aspx.cs" Inherits="Migague.Views.ABM.TransferirStock" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <br /><br />
    
    <asp:Label ID="lblSucursalSalida" runat="server" Text="<%$ Resources:Stock, lblSucursalSalida%>"></asp:Label>
    <asp:DropDownList ID="ddlSucursalSalida" runat="server" AutoPostBack="true"></asp:DropDownList><br />

    <asp:GridView ID="gridArticulosSalida" runat="server" AutoGenerateColumns="False" AllowPaging="true" AllowSorting="true">
            <Columns>

                <asp:TemplateField HeaderText="<%$ Resources:Stock, gridArticulosSalidaID%>" Visible="false">
                    <ItemTemplate>
                        <asp:TextBox runat="server" ID="txtIDArticuloSalida" Text='<%# Eval("articulo.id")%>' Visible="false"/>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox runat="server" ID="txtEditIDArticuloSalida" Text='<%# Eval("articulo.id")%>' Visible="false"/>
                    </EditItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="<%$ Resources:Stock, gridArticulosSalidaNombre%>" Visible="true">
                    <ItemTemplate>
                        <asp:TextBox runat="server" ID="txtNombreArticuloSalida" Text='<%# Eval("articulo.modelo.nombre")%>' Visible="false"/>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox runat="server" ID="txtEditNombreArticuloSalida" Text='<%# Eval("articulo.modelo.nombre")%>' Visible="false"/>
                    </EditItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="<%$ Resources:Stock, gridArticulosSalidaColor%>" >
                    <ItemTemplate>
                        <asp:TextBox runat="server" ID="txtColorArticuloSalida" Text='<%# Eval("articulo.color.nombre")%>' />
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox runat="server" ID="txtEditColorArticuloSalida" Text='<%# Eval("articulo.color.nombre")%>' />
                    </EditItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="<%$ Resources:Stock, gridArticulosSalidaTalle%>" >
                    <ItemTemplate>
                        <asp:TextBox runat="server" ID="txtTalleArticuloSalida" Text='<%# Eval("articulo.talle.nombre")%>' />
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox runat="server" ID="txtEditTalleArticuloSalida" Text='<%# Eval("articulo.talle.nombre")%>' />
                    </EditItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="<%$ Resources:Stock, gridArticulosSalidaCantidad%>" Visible="true">
                    <ItemTemplate>
                        <asp:TextBox runat="server" ID="txtCantidadArticuloSalida" Text='<%# Eval("cantidad_neta")%>' Visible="true"/>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox runat="server" ID="txtEditCantidadArticuloSalida" Text='<%# Eval("cantidad_neta")%>' Visible="true"/>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:CommandField ButtonType="Link" ShowEditButton="True" ShowCancelButton="true" />
            </Columns>
        </asp:GridView>

   
</asp:Content>
