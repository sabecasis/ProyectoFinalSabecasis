<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Admin/admin.Master" CodeBehind="EstadisticasDeGanancias.aspx.vb" Inherits="TFI.EstadisticasDeGanancias" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="background">
        <img src="../static/fondo.jpg" />
    </div>
    <aside id="lateral"></aside>
    <section id="contenedor">
         <span id="spsucursal">Sucursal</span><asp:DropDownList ID="CMBSucursal" runat="server" onselectedindexchanged="CMBsusursal_SelectedIndexChanged" Width="224px"
        AutoPostBack="True" AppendDataBoundItems="true"></asp:DropDownList>
        <h1 id="hgananciatotal">Ganancias anuales totales</h1>
         <asp:Chart ID="GraficaAnual" runat="server">
            <Series>
                <asp:Series Name="Series1"></asp:Series>
            </Series>
            <ChartAreas>
                <asp:ChartArea Name="ChartArea1"></asp:ChartArea>
            </ChartAreas>
        </asp:Chart>
     <h1 id="hgananciamensualtotal">Ganancias mensuales totales</h1>
        <span id="spanio">Año</span><asp:DropDownList ID="CMBAnios" runat="server" onselectedindexchanged="CMBAnios_SelectedIndexChanged" Width="224px"
        AutoPostBack="True" AppendDataBoundItems="true"></asp:DropDownList>
         <asp:Chart ID="GraficaMensual" runat="server" Width="400px">
            <Series>
                <asp:Series Name="Series2"></asp:Series>
            </Series>
            <ChartAreas>
                <asp:ChartArea Name="ChartArea2"></asp:ChartArea>
            </ChartAreas>
        </asp:Chart>

    <h1 id="hgananciadiariatotal">Ganancias diarias totales</h1>
        <span id="spmes">Mes</span><asp:DropDownList ID="CMBMeses" runat="server" onselectedindexchanged="CMBMeses_SelectedIndexChanged" Width="224px"
        AutoPostBack="True" AppendDataBoundItems="true"></asp:DropDownList>
         <asp:Chart ID="GraficoDiario" runat="server" Width="400px">
            <Series>
                <asp:Series Name="Series3"></asp:Series>
            </Series>
            <ChartAreas>
                <asp:ChartArea Name="ChartArea3"></asp:ChartArea>
            </ChartAreas>
        </asp:Chart>
   
    </section>   
</asp:Content>
