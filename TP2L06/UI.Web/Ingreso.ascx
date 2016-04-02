<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Ingreso.ascx.cs" Inherits="UI.Web.Login1" %>
<link href="Styles/Site.css" rel="stylesheet" type="text/css" />
<asp:Label ID=errorLabel runat=server CssClass=error></asp:Label>
<br />
<br />
<table class=login>
    <tr>
        <td></td>
        <td></td>
        <td></td>
        <td></td>
    </tr>
    <tr>
        <td></td>
        <td align=right>
            <asp:Label ID="usuarioLabel" runat="server" Text="Nombre de Usuario: "></asp:Label>
        </td>
        <td align=center>
            <asp:TextBox ID="usuarioTextBox" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="UsuarioRequerido" runat="server" ControlToValidate="usuarioTextBox" 
            EnableClientScript="true" ErrorMessage="Ingrese un nombre de usuario" ForeColor="Red" SetFocusOnError="true" 
            Text="*"></asp:RequiredFieldValidator>
        </td>
        <td></td>
    </tr>
    <tr>
        <td></td>
        <td align=right>
            <asp:Label ID="claveLabel" runat="server" Text="Clave: "></asp:Label>
        </td>
        <td align=center>
            <asp:TextBox ID="claveTextBox" runat="server" TextMode="Password"></asp:TextBox>
            <asp:RequiredFieldValidator ID="ClaveRequerida" runat="server" ControlToValidate="claveTextBox" EnableClientScript="true"
            ErrorMessage="Ingrese una clave" ForeColor="Red" SetFocusOnError="true" Text="*"></asp:RequiredFieldValidator>
        </td>
        <td></td>
    </tr>
    <tr>
        <td></td>
        <td></td>
        <td align=center>
            <asp:Button ID="ingresarButton" runat="server" onclick="ingresarButton_Click" Text="Ingresar" />
        </td>
        <td></td>
    </tr>
    <tr>
        <td></td>
        <td></td>
        <td></td>
        <td></td>
    </tr>
</table>