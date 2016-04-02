<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Comisiones.aspx.cs" Inherits="UI.Web.Comisiones" %>

<asp:Content ID="Content1" ContentPlaceHolderID="bodyContentPlaceHolder" runat="server">
<div class=paginas>
    <br />
<br />
<h1 class=titulo>Comisiones</h1>
<asp:Panel ID="adminPanel" runat=server>
<asp:Panel ID="gridPanel" runat="server">
    <asp:GridView ID="gridView" runat="server" AutoGenerateColumns="False" SelectedRowStyle-BackColor=Orange
        SelectedRowStyle-ForeColor=Black HorizontalAlign=Center
        DataKeyNames="ID" onselectedindexchanged="gridView_SelectedIndexChanged" 
        Width="433px">
        <Columns>
            <asp:BoundField HeaderText="Comisión" DataField="Descripcion" />
            <asp:BoundField HeaderText="Año" DataField="AnioEspecialidad" />
            <asp:BoundField HeaderText="Plan                " DataField="DescPlan" />
            <asp:CommandField SelectText="Seleccionar" ShowSelectButton="True" 
                SelectImageUrl="~/Images/facultad-de-derecho_664535.jpg"/>
        </Columns>
    </asp:GridView>
</asp:Panel>

<br />

<asp:Panel ID="gridActionPanel" runat=server>
    <asp:LinkButton ID="editarLinkButton" runat=server CausesValidation=false Text="Editar"
        onclick="editarLinkButton_Click"></asp:LinkButton>
    &nbsp;&nbsp;
    <asp:LinkButton ID="eliminarLinkButton" runat=server CausesValidation="False" 
        onclick="eliminarLinkButton_Click" Text="Eliminar"></asp:LinkButton>
    &nbsp;&nbsp;
    <asp:LinkButton ID="nuevoLinkButton" runat=server CausesValidation=false
        onclick="nuevoLinkButton_Click" Text="Nuevo"></asp:LinkButton>
</asp:Panel>
</asp:Panel>
<br />
<div class=error>
<center>
<asp:ValidationSummary ID="ResumenValidaciones" CssClass=error runat="server" 
        HeaderText="Los siguientes errores ocurrieron:" ShowMessageBox="false" 
        DisplayMode="BulletList" ShowSummary="true" Width="365px" />
<asp:Panel ID="errorPanel" runat=server Visible=false>
<asp:Label ID="mensajeError" runat=server CssClass=error></asp:Label>
    <br />
    <br />
</asp:Panel>
</center>
</div>

<asp:Panel ID="usuarioPanel" runat=server Visible=false>
<asp:Panel ID="formPanel" runat="server">
<div class=formulario>
    <br />
    <asp:Label ID="anioLabel" runat="server" Text="Año Especialidad: "></asp:Label>
    <asp:TextBox ID="anioTextBox" runat=server></asp:TextBox>
    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="anioTextBox" ValidationExpression="\d+"
       ErrorMessage='El año debe ser un número entero' EnableClientScript="true" SetFocusOnError="true" Text="*" ForeColor=Red></asp:RegularExpressionValidator>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="anioTextBox"  
       ErrorMessage='Especifique año' EnableClientScript="true" SetFocusOnError="true" Text="*" ForeColor=Red></asp:RequiredFieldValidator>
    <br />
    <asp:Label ID="descripcionLabel" runat="server" Text="Descripción: "></asp:Label>
    <asp:TextBox ID="descripcionTextBox" runat=server></asp:TextBox>
    <asp:RequiredFieldValidator ID="DescripcionRequerida" runat="server" ControlToValidate="descripcionTextBox"  
       ErrorMessage='Se requerie alguna descripción' EnableClientScript="true" SetFocusOnError="true" Text="*" ForeColor=Red></asp:RequiredFieldValidator>
    <br />
    <asp:Label ID="planLabel" runat=server Text="Plan: "></asp:Label>
    <asp:DropDownList ID="planesList" runat=server></asp:DropDownList>
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
