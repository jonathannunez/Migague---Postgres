<%@ Page Title="Login" Language="C#" MasterPageFile="~/Login.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Migague.Views.Login.Login" Culture="auto:es-ES" UICulture="auto"%>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContentLog" runat="server">

    <h2><asp:Literal runat="server" Text="<%$ Resources:LoginResource, Titulo%>" />.</h2>

    <p> <asp:Literal runat="server" Text="<%$ Resources:LoginResource, Subtitulo%>" /> </p>

    <script type="text/javascript">
        function ValidateLogin() {
            var nombreUsuario = document.getElementById('<%=txtPassword.ClientID %>').value;
            var passwordUsuario = document.getElementById('<%=txtPassword.ClientID %>').value;
        if (nombreUsuario == "" && passwordUsuario == "") {
            alert('<%=HttpContext.GetGlobalResourceObject("LoginResource","CamposVacios") %>');
            return false;
        } else if (nombreUsuario == "") {
            alert('<%=HttpContext.GetGlobalResourceObject("LoginResource","CamposVacios") %>');
            return false;
        } else if (passwordUsuario == "") {
            alert('<%=HttpContext.GetGlobalResourceObject("LoginResource","CamposVacios") %>');
            return false;
        }
        return true;
        }
    </script>

        <div class="form-group" style="text-align: left">
            <asp:TextBox ID="txtEmpresa" name="txtEmpresa" runat="server" placeholder="<%$ Resources:LoginResource, TxtEmpresaPH%>"></asp:TextBox>
        </div>
        <div class="form-group" style="text-align: left">
            <asp:TextBox ID="txtUsuario" name="txtUsuario" runat="server" placeholder="<%$ Resources:LoginResource, TxtUserPH%>"></asp:TextBox>
            <%-- <asp:RequiredFieldValidator ID="TxtUserValidation" runat="server" ErrorMessage="<%$ Resources:LoginResource, TxtUserValidation%>" ControlToValidate="txtUsuario" ForeColor="Red"></asp:RequiredFieldValidator> --%>
        </div>
        <div class="form-group" style="text-align: left">
            <asp:TextBox ID="txtPassword" runat="server" placeholder="<%$ Resources:LoginResource, TxtPassPH%>" TextMode="Password"></asp:TextBox>
            <%-- <asp:RequiredFieldValidator ID="TxtPassValidation" runat="server" ErrorMessage="<%$ Resources:LoginResource, TxtPassValidation%>" ControlToValidate="txtPassword" ForeColor="Red"></asp:RequiredFieldValidator> --%>
        </div>
     
    <asp:Button ID="BtnLogin" runat="server" text="<%$ Resources:LoginResource, TxtUsuario%>"  OnClick="btnLogin_Click" OnClientClick="return ValidateLogin()" />
    

</asp:Content>
