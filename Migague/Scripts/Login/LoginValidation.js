function ValidateLogin() {
    try {
        var nombreUsuario = document.getElementById('<%=txtPassword.ClientID %>').value;
    }
    catch (err) {
        alert(err);
    }
    alert("Hicimmo el primer var");
    var passwordUsuario = document.getElementById('<%=txtPassword.ClientID %>').value;
    if (nombreUsuario == "" && passwordUsuario == "") {
        alert('<%=HttpContext.GetGlobalResourceObject("LoginResource","Subtitulo") %>');
        return false;
    } else if (nombreUsuario == "") {
        alert("Ingrese usuario");
        return false;
    } else if (passwordUsuario == "") {
        alert("Ingrese password");
        return false;
    }
    return true;
}



