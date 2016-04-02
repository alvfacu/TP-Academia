<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Cursos.aspx.cs" Inherits="UI.Web.Cursos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="bodyContentPlaceHolder" runat="server">
<div class=paginas>
    <br />
<br />
<h1 class=titulo>Cursos</h1>
<asp:Panel ID="adminPanel" runat=server>
<asp:Panel ID="gridPanel" runat="server">
    <br />
    <asp:GridView ID="gridView" runat="server" AutoGenerateColumns="False" HorizontalAlign=Center
        SelectedRowStyle-BackColor=Orange
        SelectedRowStyle-ForeColor=Black
        DataKeyNames="ID" onselectedindexchanged="gridView_SelectedIndexChanged">
        <Columns>
            <asp:BoundField HeaderText="Descripción" DataField="Descripcion" />
            <asp:BoundField HeaderText="Año Calendario" DataField="AnioCalendario" />
            <asp:BoundField HeaderText="Cupo" DataField="Cupo" />
            <asp:BoundField HeaderText="Comision" DataField="Desc_Comision" />
            <asp:BoundField HeaderText="Materia" DataField="Desc_Materia" />
            <asp:CommandField SelectText="Seleccionar" ShowSelectButton="True"/>
        </Columns>
        <SelectedRowStyle BackColor="Orange" ForeColor="Black" />
    </asp:GridView>
</asp:Panel>
<br />
<asp:Panel ID="gridActionPanel" runat=server>
    <asp:LinkButton ID="nuevoLinkButton" runat=server CausesValidation=false
        onclick="nuevoLinkButton_Click" Text="Nuevo"></asp:LinkButton>
    &nbsp;&nbsp;
    <asp:LinkButton ID="editarLinkButton" runat=server CausesValidation=false Text="Editar"
        onclick="editarLinkButton_Click"></asp:LinkButton>
    &nbsp;&nbsp;
    <asp:LinkButton ID="eliminarLinkButton" runat=server CausesValidation="False" 
        onclick="eliminarLinkButton_Click" Text="Eliminar"></asp:LinkButton>   
    &nbsp;&nbsp;
    <asp:LinkButton ID="reporteLinkButton" runat=server CausesValidation=false
        onclick="reporteLinkButton_Click" Text="Generar reporte"></asp:LinkButton>
</asp:Panel>
</asp:Panel>
<br />

<div class=error>
<center>
<asp:ValidationSummary ID="ResumenValidaciones" ForeColor=Red runat="server" 
        HeaderText="Los siguientes errores ocurrieron:" ShowMessageBox="false" 
        DisplayMode="BulletList" ShowSummary="true" style="margin:auto" 
        Width="467px" />
<asp:Panel ID="errorPanel" runat=server Visible=false>
<asp:Label ID="mensajeError" runat=server ForeColor=Red></asp:Label>
    <br />
    <br />
</asp:Panel>
</center>
</div>

<asp:Panel ID="usuarioPanel" runat=server Visible=false>
<asp:Panel ID="formPanel" runat="server">
<div class=formulario>
<br />
    <asp:Label ID="descripcionLabel" runat="server" Text="Descripción: "></asp:Label>
    <asp:TextBox ID="descripcionTextBox" runat=server></asp:TextBox>
    <asp:RequiredFieldValidator ID="DescripcionRequerida" runat="server" ControlToValidate="descripcionTextBox"  
       ErrorMessage='Ingrese descripción' EnableClientScript="true" SetFocusOnError="true" Text="*" ForeColor=Red></asp:RequiredFieldValidator>
    <br />
    <asp:Label ID="anioLabel" runat="server" Text="Año Calendario: "></asp:Label>
    <asp:TextBox ID="anioTextBox" runat=server Width="88px"></asp:TextBox>
    <asp:RequiredFieldValidator ID="AnioRequerido" runat="server" ControlToValidate="anioTextBox"  
       ErrorMessage='Ingresa Año Calendario' EnableClientScript="true" SetFocusOnError="true" Text="*" ForeColor=Red></asp:RequiredFieldValidator>
    <asp:RegularExpressionValidator runat="server" id="AnioCorrecto" controltovalidate="anioTextBox" ValidationExpression="\d+"
       ErrorMessage='El año debe ser un número entero' EnableClientScript="true" SetFocusOnError="true" Text="*" ForeColor=Red />
    <br />
    <asp:Label ID="cupoLabel" runat="server" Text="Cupo: "></asp:Label>
    <asp:TextBox ID="cupoTextBox" runat=server></asp:TextBox>
    <asp:RequiredFieldValidator ID="CupoRequerido" runat="server" ControlToValidate="descripcionTextBox"  
       ErrorMessage='Ingrese cupo' EnableClientScript="true" SetFocusOnError="true" Text="*" ForeColor=Red></asp:RequiredFieldValidator>
    <asp:RegularExpressionValidator runat="server" id="CupoCorrecto" controltovalidate="cupoTextBox" ValidationExpression="\d+"
       ErrorMessage='El cupo debe ser un número entero' EnableClientScript="true" SetFocusOnError="true" Text="*" ForeColor=Red/>
    <br />
    <asp:Label ID="materiaLabel" runat="server" Text="Materia: "></asp:Label>
    <asp:DropDownList ID="materiasList" runat=server Height="18px" Width="220px" 
        AutoPostBack="True" 
        onselectedindexchanged="materiasList_SelectedIndexChanged"></asp:DropDownList>  
    <br />
    <asp:Label ID="comisionLabel" runat="server" Text="Comision: "></asp:Label>
    <asp:DropDownList ID="comisionesList" runat=server Height="17px" Width="91px"></asp:DropDownList>    
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
</asp:Panel>
</div>
</asp:Content>

