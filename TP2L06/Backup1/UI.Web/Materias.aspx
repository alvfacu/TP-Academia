﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Materias.aspx.cs" Inherits="UI.Web.Materias" %>

<asp:Content ID="Content1" ContentPlaceHolderID="bodyContentPlaceHolder" runat="server">
<div class=paginas>
    <br />
<br />
<h1 class=titulo>Materias</h1>
<asp:Panel ID="adminPanel" runat=server>
<asp:Panel ID="gridPanel" runat="server">
<br />
    <asp:GridView ID="gridView" runat="server" AutoGenerateColumns="False" HorizontalAlign=Center
        SelectedRowStyle-BackColor=Orange
        SelectedRowStyle-ForeColor=Black
        DataKeyNames="ID" onselectedindexchanged="gridView_SelectedIndexChanged">
        <Columns>
            <asp:BoundField HeaderText="Descripcion" DataField="Descripcion" />
            <asp:BoundField HeaderText="Hs Semanales" DataField="HSSemanales" />
            <asp:BoundField HeaderText="Hs Totales" DataField="HSTotales" />
            <asp:BoundField HeaderText="Plan" DataField="Desc_Plan" />
            <asp:CommandField SelectText="Seleccionar" ShowSelectButton="True" />
        </Columns>
        <SelectedRowStyle BackColor="Orange" ForeColor="Black" />
    </asp:GridView>
</asp:Panel>
<br />
<asp:Panel ID="gridActionPanel" runat=server>
    <asp:LinkButton ID="editarLinkButton" runat=server CausesValidation=false 
        onclick="editarLinkButton_Click">Editar</asp:LinkButton>
    &nbsp;&nbsp;
    <asp:LinkButton ID="eliminarLinkButton" runat=server CausesValidation="False"
        onclick="eliminarLinkButton_Click">Eliminar</asp:LinkButton>
    &nbsp;&nbsp;
    <asp:LinkButton ID="nuevoLinkButton" runat=server CausesValidation=false 
        onclick="nuevoLinkButton_Click">Nuevo</asp:LinkButton>
</asp:Panel>
</asp:Panel>
<br />

<div class="error">
<center>
<asp:ValidationSummary ID="ResumenValidaciones" ForeColor=Red runat="server" 
        HeaderText="Los siguientes errores ocurrieron:" ShowMessageBox="false" 
        DisplayMode="BulletList" ShowSummary="true" Width="405px" />
<asp:Panel ID="errorPanel" runat=server Visible=false>
<asp:Label ID="mensajeError" runat=server ForeColor=Red></asp:Label>
    <br />
</asp:Panel>
</center>
</div>

<asp:Panel ID="usuarioPanel" runat=server Visible=false>
<asp:Panel ID="formPanel" runat="server">
<div class=formulario>
<br />
    <asp:Label ID="descripcionLabel" runat="server" Text="Descripcion: "></asp:Label>
    <asp:TextBox ID="descripcionTextBox" runat=server Width="307px"></asp:TextBox>
    <asp:RequiredFieldValidator ID="DescripcionRequerida" runat="server" ControlToValidate="descripcionTextBox"  
       ErrorMessage='Especifique descripción' EnableClientScript="true" SetFocusOnError="true" Text="*" ForeColor=Red></asp:RequiredFieldValidator>
    <br />
    <asp:Label ID="semanalesLabel" runat="server" Text="Hs Semanales: "></asp:Label>
    <asp:TextBox ID="semanalesTextBox" runat=server></asp:TextBox>
    <asp:RequiredFieldValidator ID="HorasSemanalesRequeridas" runat="server" ControlToValidate="semanalesTextBox"  
       ErrorMessage='Especifique cantidad de horas semanales' EnableClientScript="true" SetFocusOnError="true" Text="*" ForeColor=Red></asp:RequiredFieldValidator>
    <asp:RegularExpressionValidator runat="server" ValidationExpression="\d+" ControlToValidate="semanalesTextBox" 
        ErrorMessage='La cantidad de Hs Semanales debe ser un número entero' EnableClientScript="true" SetFocusOnError="true" Text="*" ForeColor=Red></asp:RegularExpressionValidator>
    <br />
    <asp:Label ID="totalesLabel" runat="server" Text="Hs Totales: "></asp:Label>
    <asp:TextBox ID="totalesTextBox" runat=server></asp:TextBox>
    <asp:RequiredFieldValidator ID="HorasTotalesRequeridas" runat="server" ControlToValidate="totalesTextBox"  
       ErrorMessage='Especifique cantidad de horas semanales' EnableClientScript="true" SetFocusOnError="true" Text="*" ForeColor=Red></asp:RequiredFieldValidator>
    <asp:RegularExpressionValidator ID="ValidarCantidad" runat="server" ValidationExpression="\d+" ControlToValidate="totalesTextBox" 
        ErrorMessage='La cantidad de Hs Totales debe ser un número entero' EnableClientScript="true" SetFocusOnError="true" Text="*" ForeColor=Red></asp:RegularExpressionValidator>
    <br />
    <asp:Label ID="planLabel" runat="server" Text="Plan: "></asp:Label>
    <asp:DropDownList ID="planesList" runat="server"></asp:DropDownList>
    <br />
    <br />
</div>
</asp:Panel>

<asp:Panel ID="formActionsPanel" runat=server>
    <asp:LinkButton ID="aceptarLinkButton" runat=server
        onclick="aceptarLinkButton_Click">Aceptar</asp:LinkButton>
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:LinkButton ID="cancelarLinkButton" runat=server CausesValidation="False"
        onclick="cancelarLinkButton_Click">Cancelar</asp:LinkButton>
</asp:Panel>
<br />
<br />

</asp:Panel>
</div>

</asp:Content>
