<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Admin/admin.Master" CodeBehind="EstadisticasDeFichas.aspx.vb" Inherits="TFI.EstadisticasDeFichas" %>
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
