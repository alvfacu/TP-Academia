<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Fecha.ascx.cs" Inherits="UI.Web.Fecha" %>
<asp:TextBox ID="fechaTextBox" runat="server" ReadOnly="True"></asp:TextBox>
<asp:Calendar ID="calendario" runat=server 
    onselectionchanged="calendario_SelectionChanged"></asp:Calendar>

