<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Admin/admin.Master" CodeBehind="DetalleDeOrden.aspx.vb" Inherits="TFI.DetalleDeOrden" %>
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
    <section class="contenedor-soft">
        <input type="hidden" id="accion" name="accion" />
        <div class="row">
            <div class="mensaje-respuesta-contacto" id="LblMensaje" runat="server"></div>
        </div>
        <div class="container">
            <div class="row"><div class="col-lg-3"><span id="spusuario">Usuario:</span></div><div class="col-lg-3"><input type="text" readonly="readonly" id="nombreusuario" runat="server" /></div></div>
       <div class="row">
           <h4 id="hinformacionorden">Información de orden</h4>
            <asp:GridView ID="orden" runat="server" AutoGenerateColumns="False" CellPadding="3" GridLines="Vertical" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" Width="582px">
                <AlternatingRowStyle BackColor="#DCDCDC" />
                <Columns>
                    <asp:HyperLinkField DataNavigateUrlFields="nroDeOrden" DataTextField="nroDeOrden" HeaderText="Nro. De Orden" NavigateUrl="/Admin/DetalleDeOrden.aspx" Target="_blank" DataNavigateUrlFormatString="/Admin/DetalleDeOrden.aspx?nroDeOrden={0}" />
                    <asp:BoundField DataField="fechaInicio" HeaderText="Fecha De Creación" />
                    <asp:BoundField DataField="totalAPagar" HeaderText="Total Cobrado" />
                    <asp:BoundField DataField="cuotas" HeaderText="Cuotas" />
                    <asp:BoundField DataField="recargoPorTarjeta" HeaderText="Recargo por tarjeta" />
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
            <div class="row">
            <h4 id="hdetalledeorden">Detalle de orden</h4>
            <asp:GridView ID="detall" runat="server" AutoGenerateColumns="False" CellPadding="3" GridLines="Vertical" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" Width="567px">
                <AlternatingRowStyle BackColor="#DCDCDC" />
                <Columns>
                    <asp:BoundField DataField="nombreProducto" HeaderText="Producto" />
                    <asp:BoundField DataField="cantidad" HeaderText="Cantidad" />
                    <asp:BoundField DataField="monto" HeaderText="Precio" />
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
            <div class="row">
                <h4 id="hinformacionpago">Información de pago</h4>
              <asp:GridView ID="informacionPago" runat="server" AutoGenerateColumns="False" CellPadding="3" GridLines="Vertical" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" Width="566px">
                <AlternatingRowStyle BackColor="#DCDCDC" />
                  <Columns>
                      <asp:BoundField DataField="titular" HeaderText="Titular de tarjeta" />
                      <asp:BoundField DataField="metodo" HeaderText="Método de pago" />
                      <asp:BoundField DataField="nroDeTarjeta" HeaderText="Nro. De Tarjeta" />
                      <asp:BoundField DataField="cvv" HeaderText="CVV" />
                      <asp:BoundField DataField="mesVencimiento" HeaderText="Mes de vencimieto" />
                      <asp:BoundField DataField="anioVencimiento" HeaderText="Año de vencimiento" />
                      <asp:BoundField DataField="nroNotaDeCredito" HeaderText="Nro. De Nota De Crédito" />
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
            <div class="row">
                <h4 id="hinformacionenvio">Información de envío</h4>
              <asp:GridView ID="envio" runat="server" AutoGenerateColumns="False" CellPadding="3" GridLines="Vertical" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" Width="560px">
                <AlternatingRowStyle BackColor="#DCDCDC" />
                <Columns>
                    <asp:BoundField DataField="nroEnvio" HeaderText="Nro. De Envío" />
                    <asp:BoundField DataField="fecha" HeaderText="Fecha" />
                    <asp:BoundField DataField="monto" HeaderText="Costo" />
                    <asp:BoundField DataField="tipo" HeaderText="Tipo De Envío" />
                    <asp:BoundField DataField="estado" HeaderText="Estado" />
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
