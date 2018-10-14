<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Admin/admin.Master" CodeBehind="EstadisticasDeGanancias.aspx.vb" Inherits="TFI.EstadisticasDeGanancias" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
   <link href="../static/bootstrap.min.css" rel="stylesheet" />
    <script src="../scripts/jquery.js"></script>
    <script src="../scripts/bootstrap.min.js"></script>
    <link href="../static/font-awesome/css/font-awesome.min.css" rel="stylesheet" />
    <link href="../static/shop-homepage.css" rel="stylesheet" />
    <link href="../static/sidebar.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
      <div id="background">
        <img src="../static/usuario.png"  />
    </div>
    <nav class="navbar navbar-inverse navbar-fixed-top" role="navigation">
        <div class="container">

            <div class="navbar-header page-scroll">
                <a class="navbar-brand" href="/Cliente/Inicio.aspx">
                    <img class="dooba-img" src="../static/logotipo-trans.png" id="logo" /></a>
                </div>
                <div class="collapse navbar-collapse"> 
                    <ul class="nav navbar-nav navbar-right">
                       <li>
                          <a href="#" class="dropdown-toggle" data-toggle="dropdown" id="aPerfil" runat="server"><b class="caret"></b></a>
                        <ul class="dropdown-menu">
                            <li>
                                <a href="/Cliente/CerrarSesion.aspx" id="alogout">Cerrar Sesión</a>
                            </li>
                              <li>
                                <a  href="#" data-target="#idioma" data-toggle="modal" id="acambiaridioma">Cambiar Idioma</a>
                            </li>
                        </ul>
                           </li>
                      </ul>
                 </div>
            </div>
        <div id="breadcrums" runat="server" class="breadcrums">
        </div>
        <div class="collapse navbar-collapse navbar-ex1-collapse">
            <ul class="nav navbar-nav side-nav" id="menuVertical">
              
            </ul>
        </div>
    </nav>
    <section id="contenedor">
        <div class="row">
           
            <div class="mensaje-respuesta-contacto" id="LblMensaje" runat="server"></div>
        </div>
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
     <div id="idioma" class="modal fade" role="dialog">
        <div id="Div2" class="modal-dialog modal-lg modal-content modal-dooba modal-idioma">
            <button type="button" class="btn btn-default btn-modal-close btn-circle pull-right" data-dismiss="modal"><i class="fa fa-times"></i></button>
            <div class="col-md-3"><span id="sidioma">Idioma: </span></div>
            <select id="idiomaSeleccionado" onchange="seleccionarIdioma();"></select>
        </div>
    </div> 
</asp:Content>
