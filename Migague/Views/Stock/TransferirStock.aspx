<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TransferirStock.aspx.cs" Inherits="Migague.Views.ABM.TransferirStock" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <br /><br />
    
    <asp:Label ID="lblSucursalSalida" runat="server" Text="<%$ Resources:Stock, lblSucursalSalida%>"></asp:Label>
    <asp:DropDownList ID="ddlSucursalSalida" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlSucursalSalida_SelectedIndexChanged"></asp:DropDownList><br />

    <asp:GridView ID="gridArticulosSalida" runat="server" AutoGenerateColumns="False" AllowPaging="True" AllowSorting="True"
        OnRowDataBound="gridArticulosSalida_DataBound" OnSelectedIndexChanged="gridArticulosSalida_SelectedIndexChanged">
            <Columns>

                <asp:TemplateField HeaderText="<%$ Resources:Stock, gridArticulosSalidaID%>">
                    <ItemTemplate>
                        <asp:TextBox runat="server" ID="txtIDArticuloSalida" Text='<%# Eval("articulo.id")%>'/>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox runat="server" ID="txtEditIDArticuloSalida" Text='<%# Eval("articulo.id")%>'/>
                    </EditItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="<%$ Resources:Stock, gridArticulosSalidaNombre%>">
                    <ItemTemplate>
                        <asp:TextBox runat="server" ID="txtArticuloID" Text='<%# Eval("articulo.modelo.id")%>' />
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox runat="server" ID="txtEditArticuloID" Text='<%# Eval("articulo.modelo.id")%>'/>
                    </EditItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="<%$ Resources:Stock, gridArticulosSalidaNombre%>">
                    <ItemTemplate>
                        <asp:TextBox runat="server" ID="txtNombreArticuloSalida" Text='<%# Eval("articulo.modelo.nombre")%>'/>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox runat="server" ID="txtEditNombreArticuloSalida" Text='<%# Eval("articulo.modelo.nombre")%>'/>
                    </EditItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="<%$ Resources:Stock, gridArticulosSalidaNombre%>">
                    <ItemTemplate>
                        <asp:TextBox runat="server" ID="txtColorID" Text='<%# Eval("articulo.color.id")%>'/>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox runat="server" ID="txtEditColorID" Text='<%# Eval("articulo.color.id")%>'/>
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

                <asp:TemplateField HeaderText="<%$ Resources:Stock, gridArticulosSalidaNombre%>">
                    <ItemTemplate>
                        <asp:TextBox runat="server" ID="txtTalleID" Text='<%# Eval("articulo.talle.id")%>'/>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox runat="server" ID="txtEditTalleID" Text='<%# Eval("articulo.talle.id")%>'/>
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

                <asp:TemplateField HeaderText="<%$ Resources:Stock, gridArticulosSalidaCantidad%>">
                    <ItemTemplate>
                        <asp:TextBox runat="server" ID="txtCantidadArticuloSalida" Text='<%# Eval("cantidad_neta")%>' Visible="true"/>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox runat="server" ID="txtEditCantidadArticuloSalida" Text='<%# Eval("cantidad_neta")%>' Visible="true"/>
                    </EditItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="<%$ Resources:Stock, gridArticulosSalidaCantidad%>">
                    <ItemTemplate>
                        <asp:TextBox runat="server" ID="txtCantidadIngresada" Visible="true"/>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox runat="server" ID="txtEditCantidadIngresada" Visible="true"/>
                    </EditItemTemplate>
                </asp:TemplateField>

                <asp:CommandField ButtonType="Link" ShowCancelButton="true" AccessibleHeaderText="Seleccionar" ShowSelectButton="True" />
            </Columns>
        </asp:GridView>
    
    <br /> 
    <asp:Label ID="lblSucursalEntrada" runat="server" Text="<%$ Resources:Stock, lblSucursalSalida%>"></asp:Label>
    <asp:DropDownList ID="ddlSucursalEntrada" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlSucursalEntrada_SelectedIndexChanged"></asp:DropDownList><br />
    <br />
    <asp:Label ID="cantidadIngresada" runat="server" Text="Cantidad"></asp:Label>
    <asp:TextBox ID="txtCantIngresada" runat="server"></asp:TextBox>
    <br />
    <asp:Button ID="btnTransferir" runat="server" Text="transferir" OnClick="btnTransferir_Click"/>
</asp:Content>
