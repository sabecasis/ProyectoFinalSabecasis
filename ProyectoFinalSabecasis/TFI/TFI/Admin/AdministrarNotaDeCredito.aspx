<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Admin/admin.Master" CodeBehind="AdministrarNotaDeCredito.aspx.vb" Inherits="TFI.AdministrarNotaDeCredito" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../static/bootstrap.min.css" rel="stylesheet" />
    <script src="../scripts/jquery.js"></script>
    <script src="../scripts/bootstrap.min.js"></script>
    <link href="../static/font-awesome/css/font-awesome.min.css" rel="stylesheet" />
    <link href="../static/shop-homepage.css" rel="stylesheet" />
    <link href="../static/sidebar.css" rel="stylesheet" />
    <script src="../scripts/abmscripts.js"></script>
    <script src="../scripts/jquery-2.1.4.min.js"></script>
     <script src="../scripts/jquery-ui.min.js"></script>
    <link href="../static/jquery-ui.css" rel="stylesheet" />
    <script type="text/javascript">
        function limpiar() {
            limpiarForm();

        }     
        function crear() {
            function onSuccess(result) {
                document.getElementById('ContentPlaceHolder1_id').value = result;
            }
            function onFailure(result) {
                alert(result.get_message);
            }
            PageMethods.obtenerProximoId(onSuccess, onFailure);
            document.getElementById('ContentPlaceHolder1_id').disabled = 'disabled';
            document.getElementById('modificarBtn').disabled = '';
        }

        $(function () {
            $("#ContentPlaceHolder1_fecha").datepicker({
                dateFormat: "yy-mm-dd"
            });
        });

        $(function () {
            $("#ContentPlaceHolder1_fechacaducidad").datepicker({
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
    <section class="contenedor-soft">
        <div class="container">
            <input type="hidden" id="accion"  name="accion"/>
           <div class="row">
                <div class="col-lg-5"> <div class="mensaje-respuesta-contacto" id="LblMensaje" runat="server"></div></div>
            </div>
            
            <div class="row">
                <div class="col-lg-2"><span id="spid">Id:</span></div><div class="col-lg-2"><input type="text" id="id" runat="server"/></div>
            </div>
            <div class="row">
                <div class="col-lg-2"><span id="spnrodevolucion">Nro. de devolución:</span></div><div class="col-lg-2"><input type="text" id="nrodevolucion" runat="server"/></div>
            </div>
            <div class="row">
                <div class="col-lg-2"><span id="spnrodefactura">Numero de factura:</span></div><div class="col-lg-2"><input type="text" id="nroFactura" runat="server"/></div>
            </div>
            <div class="row">
                <div class="col-lg-2"><span id="spdescripcion">Descripción:</span></div><div class="col-lg-2"><input type="text" id="descripcion" runat="server"/></div>
            </div>
            <div class="row">
                <div class="col-lg-2"><span id="spmonto">Monto:</span></div><div class="col-lg-2"><input type="text" id="monto" runat="server"/></div>
            </div>
            <div class="row">
                <div class="col-lg-2"><span id="spfecha">Fecha:</span></div><div class="col-lg-2"><input type="text" id="fecha" runat="server"/></div>
            </div>
             <div class="row">
                <div class="col-lg-2"><span id="spfechacaducidad">Fecha caducidad:</span></div><div class="col-lg-2"><input type="text" id="fechacaducidad" runat="server"/></div>
            </div>
            <p />
            <div class="row">
                <div class="col-lg-1"><input type="submit" id="modificarBtn" value="Guardar"  onclick="setAccion(this.id)"  /></div>
                <div class="col-lg-1"><input type="submit" id="buscarBtn" value="Buscar"  onclick="setAccion(this.id)"/></div>
                <div class="col-lg-1"><input type="button" id="limpiarBtn" value="Limpiar" onclick="limpiar()" /></div>
                <input type="submit" id="descargarComprobanteBtn" value="Descargar Comprobante" onclick="setAccion(this.id)" />
            </div>
            <div class="row">
                <h2>Notas de crédito creadas</h2>
                <div class="col-lg-3">
                    <asp:ListBox ID="LstNotas" runat="server" AutoPostBack="True" Width="300"></asp:ListBox>
                </div>
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

