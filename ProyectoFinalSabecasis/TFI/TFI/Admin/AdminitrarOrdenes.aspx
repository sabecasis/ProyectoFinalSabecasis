<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Admin/admin.Master" CodeBehind="AdminitrarOrdenes.aspx.vb" Inherits="TFI.AdminitrarOrdenes" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
          <link href="../static/bootstrap.min.css" rel="stylesheet" />
     <script src="../scripts/jquery.js"></script>
    <script src="../scripts/bootstrap.min.js"></script>
    <link href="../static/font-awesome/css/font-awesome.min.css" rel="stylesheet" />
    <link href="../static/shop-homepage.css" rel="stylesheet" />
    <link href="../static/sidebar.css" rel="stylesheet" />
    <script src="../scripts/jquery-2.1.4.min.js"></script>
    <script src="../scripts/abmscripts.js"></script>
    <script src="../scripts/jquery-ui.min.js"></script>
    <link href="../static/jquery-ui.css" rel="stylesheet" />
    <script>
       

        $(function () {
            $("#ContentPlaceHolder1_fecha_desde").datepicker({
                dateFormat: "yy-mm-dd"
            });
        });


        $(function () {
            $("#ContentPlaceHolder1_fecha_hasta").datepicker({
                dateFormat: "yy-mm-dd"
            });
        });

   

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
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
        <input type="hidden" id="accion" name="accion" />
        <div class="row">
            <div class="mensaje-respuesta-contacto" id="LblMensaje" runat="server"></div>
        </div>
        <span id="spnrodeorden">Nro. De Orden:</span><input type="text" id="nrodeorden" runat="server"/>
        <p />
        <span id="spusuario">Usuario:</span><input type="text" id="nombreusuario" runat="server"/>
        <p />
        <span id="spfechadesde">fecha desde</span><input type="text" id="fecha_desde" name="fecha_desde" runat="server"/>
        <p/>
        <span id="spfechahasta">fecha hasta</span><input type="text" id="fecha_hasta" name="fecha_hasta" runat="server"/>
        <p/>
        <span id="spnrodefactura">Nro. De Factura</span><input type="text" id="nrodefactura" name="nrodefactura" runat="server"/>
        <p/>
        <input type="submit" id="buscarBtn" value="Buscar"  onclick="setAccion(this.id);" />
         <input type="submit" id="limpiarBtn" value="Limpiar"  onclick="setAccion(this.id);" />
        <p />
        <div>
        <div id="contenedorElementos">
        </div>
            <asp:GridView ID="ordenes" runat="server" AutoGenerateColumns="False" CellPadding="3" GridLines="Vertical" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" onrowcommand="ordenes_RowCommand">
                <AlternatingRowStyle BackColor="#DCDCDC" />
                <Columns>
                    <asp:HyperLinkField DataNavigateUrlFields="nroDeOrden" DataTextField="nroDeOrden" HeaderText="Nro. De Orden" NavigateUrl="/Admin/DetalleDeOrden.aspx" Target="_blank" DataNavigateUrlFormatString="/Admin/DetalleDeOrden.aspx?nroDeOrden={0}" />
                    <asp:ButtonField CommandName="descargarBtn"  DataTextField="nroFactura" HeaderText="Nro. De Factura" />
                    <asp:HyperLinkField DataNavigateUrlFields="nroEgreso" DataNavigateUrlFormatString="/Admin/EgresoDeStock.aspx?nroEgreso={0}" DataTextField="nroEgreso" HeaderText="Nro. De Egreso" NavigateUrl="/Admin/EgresoDeStock.aspx" Target="_blank" />
                    <asp:BoundField DataField="usuario" HeaderText="Usuario" />
                    <asp:BoundField DataField="fechaInicio" HeaderText="Fecha De Creación" />
                </Columns>
                <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
                <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                <SortedAscendingHeaderStyle BackColor="#0000A9" />
                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                <SortedDescendingHeaderStyle BackColor="#000065" />
            </asp:GridView>
        </div>
    </section>
       <div id="idioma" class="modal fade" role="dialog">
        <div id="Div2" class="modal-dialog modal-lg modal-content modal-dooba modal-idioma">
            <button type="button" class="btn btn-default btn-modal-close btn-circle pull-right" data-dismiss="modal"><i class="fa fa-times"></i></button>
            <div class="col-md-3"><span id="sidioma">Idioma: </span></div>
            <select id="idiomaSeleccionado" onchange="seleccionarIdioma();"></select>
        </div>
    </div>
</asp:Content>
