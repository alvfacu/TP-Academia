<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DocentesCursos.aspx.cs" Inherits="UI.Web.DocentesCursos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="bodyContentPlaceHolder" runat="server">
<div class=paginas>
    <br />
<br />
<h1 class=titulo>Dictados</h1>
<asp:Panel ID="adminPanel" runat=server>
<asp:Panel ID="gridPanel" runat="server">
    <br />
    <asp:GridView ID="gridView" runat="server" AutoGenerateColumns="False" HorizontalAlign=Center
        SelectedRowStyle-BackColor=Orange
        SelectedRowStyle-ForeColor=Black
        DataKeyNames="ID" onselectedindexchanged="gridView_SelectedIndexChanged">
        <Columns>
            <asp:BoundField HeaderText="Curso" DataField="Curso" />
            <asp:BoundField HeaderText="Docente" DataField="Docente" />
            <asp:BoundField HeaderText="Cargo" DataField="Cargo" />
            <asp:CommandField SelectText="Seleccionar" ShowSelectButton="True" />
        </Columns>
        <SelectedRowStyle BackColor="Orange" ForeColor="Black" />
    </asp:GridView>
</asp:Panel>
<br />
<asp:Panel ID="gridActionPanel" runat=server>
    <asp:LinkButton ID="editarLinkButton" runat=server CausesValidation=false
        onclick="editarLinkButton_Click" Text="Editar"></asp:LinkButton>
    &nbsp;&nbsp;
    <asp:LinkButton ID="eliminarLinkButton" runat=server CausesValidation="False"
        onclick="eliminarLinkButton_Click" Text="Eliminar"></asp:LinkButton>
    &nbsp;&nbsp;
    <asp:LinkButton ID="nuevoLinkButton" runat=server CausesValidation=false 
        onclick="nuevoLinkButton_Click" Text="Nuevo"></asp:LinkButton>
</asp:Panel>
</asp:Panel>
<br />

<div class=error><center>
<asp:ValidationSummary ID="ResumenValidaciones" ForeColor=Red runat="server" 
        HeaderText="Los siguientes errores ocurrieron:" ShowMessageBox="false" 
        DisplayMode="BulletList" ShowSummary="true" Width="448px" />
<asp:Panel ID="errorPanel" runat=server Visible=false>
<asp:Label ID="mensajeError" runat=server ForeColor=Red></asp:Label>
    <br />
    <br />
</asp:Panel>
</center></div>

<asp:Panel ID="usuarioPanel" runat=server Visible=false>
<asp:Panel ID="formPanel" runat="server">
<div class=formulario>
<br />
    <asp:Label ID="docenteLabel" runat="server" Text="Nombre y Apellido Docente: "></asp:Label>
    <asp:DropDownList ID="docentesList" runat=server AutoPostBack="True" onselectedindexchanged="docentesList_SelectedIndexChanged"></asp:DropDownList>
    <br />
    <asp:Label ID="cursosLabel" runat="server" Text="Curso: "></asp:Label>
    <asp:DropDownList ID="cursoList" runat=server></asp:DropDownList>
    <br />    
    <asp:Label ID="cargoLabel" runat="server" Text="Cargo: "></asp:Label>
    <asp:DropDownList ID="cargosList" runat=server></asp:DropDownList>
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
<br />
<br />
</asp:Panel>
</asp:Panel>
</div>
</asp:Content>
