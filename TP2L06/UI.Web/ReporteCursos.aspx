<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ReporteCursos.aspx.cs" Inherits="UI.Web.ReporteCursos" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="bodyContentPlaceHolder" runat="server">
<div class=paginas>
    <br />
<br />
</div>

<center>
    <rsweb:ReportViewer ID="ReportViewer1"  runat="server" Font-Names="Verdana" 
    Font-Size="8pt" InteractiveDeviceInfos="(Collection)" 
    WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Width="962px">
    <LocalReport ReportPath="ReporteCursos.rdlc">
        <DataSources>
            <rsweb:ReportDataSource DataSourceId="ObjectDataSource1" Name="DataSet1" />
        </DataSources>
    </LocalReport>
    </rsweb:ReportViewer>
    <asp:ScriptManager id="ScriptManager1" runat="server"/>
    
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="reportesCurso" TypeName="Negocio.CursoLogic"></asp:ObjectDataSource>
</center>
</asp:Content>
