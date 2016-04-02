<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="UI.Web.Login" %>
<%@ Register src="Ingreso.ascx" TagName="Ingreso" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="bodyContentPlaceHolder" runat="server">
<div class=paginas>
<asp:Panel ID="headerLogin" runat="server">
    <br />
    <h2>Ingresar</h2>
    <p><em><strong>Por favor ingresa tu nombre de usuario y tu clave.</strong></em></p>
</asp:Panel>
<center>
<asp:ValidationSummary ID="ResumenValidaciones" ForeColor=Red Font-Bold=true runat="server" 
        HeaderText="Los siguientes errores ocurrieron:" ShowMessageBox="false" 
        DisplayMode="BulletList" ShowSummary="true" style="margin:auto" 
        Width="333px" Height="61px" />
<uc1:Ingreso ID=Ingreso runat=server />
</center>
</div>
</asp:Content>
