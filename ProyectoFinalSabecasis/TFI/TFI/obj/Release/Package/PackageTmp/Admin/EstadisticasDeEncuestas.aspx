<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Admin/admin.Master" CodeBehind="EstadisticasDeEncuestas.aspx.vb" Inherits="TFI.EstadisticasDeEncuestas" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="background">
        <img src="../static/fondo.jpg" />
    </div>
    <aside id="lateral"></aside>
    <section id="contenedor">
        <asp:DropDownList ID="CMBPreguntas" runat="server" onselectedindexchanged="CMBPreguntas_SelectedIndexChanged" Width="224px"
        AutoPostBack="True" AppendDataBoundItems="true"></asp:DropDownList>
        <asp:Chart ID="Grafica" runat="server">
            <Series>
                <asp:Series Name="Series1"></asp:Series>
            </Series>
            <ChartAreas>
                <asp:ChartArea Name="ChartArea1"></asp:ChartArea>
            </ChartAreas>
        </asp:Chart>
        
    </section>   
</asp:Content>

