<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Admin/admin.Master" CodeBehind="AdministrarPermisos.aspx.vb" Inherits="TFI.AdministrarPermisos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../static/bootstrap.min.css" rel="stylesheet" />
    <script src="../scripts/jquery.js"></script>
    <script src="../scripts/bootstrap.min.js"></script>
    <link href="../static/font-awesome/css/font-awesome.min.css" rel="stylesheet" />
    <link href="../static/shop-homepage.css" rel="stylesheet" />
    <link href="../static/sidebar.css" rel="stylesheet" />
    <script src="../scripts/abmscripts.js"></script>
    <script type="text/javascript">
        function limpiar() {
            limpiarForm();
            //deshabilitar();
        }

        function guardar() {

            var permiso = { id: document.getElementById('ContentPlaceHolder1_id').value, nombre: document.getElementById('ContentPlaceHolder1_nombre').value, url: document.getElementById('ContentPlaceHolder1_url').value, elemento: { id: document.getElementById('ContentPlaceHolder1_LstElemento').value, nombre: '', leyendaPorDefecto: '' } }
            PageMethods.guardar(permiso, onSuccess, onFailure);
            function onSuccess(result) {
                alert(result);
                document.getElementById('modificarBtn').disabled = 'disabled';
            }
            function onFailure(result) {
                alert(result._message);
            }
            document.getElementById('ContentPlaceHolder1_id').disabled = 'disabled';
        }

        function eliminar() {
            PageMethods.eliminar(document.getElementById('ContentPlaceHolder1_id').value, onSuccess, onFailure);
            function onSuccess(result) {
                alert(result);
                document.getElementById('modificarBtn').disabled = 'disabled';
            }
            function onFailure(result) {
                alert(result._message);
            }
        }


        function crear() {
            function onSuccess(result) {
                document.getElementById('ContentPlaceHolder1_id').value = result;
            }
            function onFailure(result) {
                alert(result._message);
            }
            PageMethods.obtenerProximoId(onSuccess, onFailure);
            document.getElementById('ContentPlaceHolder1_id').disabled = 'disabled';
            document.getElementById('modificarBtn').disabled = '';
        }

        function buscar() {
            PageMethods.buscar(document.getElementById('ContentPlaceHolder1_nombre').value, onSuccess, onFailure);
            function onSuccess(result) {
                if (result != null) {
                    var permiso = result
                    document.getElementById('ContentPlaceHolder1_id').value = permiso.id;
                    document.getElementById('ContentPlaceHolder1_nombre').value = permiso.nombre;
                    document.getElementById('ContentPlaceHolder1_url').value = permiso.url;
                    document.getElementById('modificarBtn').disabled = '';
                    document.getElementById('ContentPlaceHolder1_LstElemento').value = permiso.elemento.id;
                }

            }
            function onFailure(result) {
                alert(result._message);
            }
        }




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
            <div class="row">
             <div class="mensaje-respuesta-contacto" id="LblMensaje" runat="server"></div>
            </div>
            <div class="row">
                <div class="col-lg-2"><span id="spid">Id:</span></div><div class="col-lg-2"><input type="text" disabled="disabled" id="id" runat="server"/></div>
            </div>
            <div class="row">
                <div class="col-lg-2"><span id="spnombre">Nombre:</span></div><div class="col-lg-2"><input type="text" id="nombre" runat="server"/></div>
            </div>
            <div class="row">
                <div class="col-lg-2"><span id="spurl">url:</span></div><div class="col-lg-2"><input type="text" id="url" runat="server"/></div>
            </div>
            <div class="row">
                <div class="col-lg-2"><span id="spidelementodeidioma">Elemento de idioma:</span></div><div class="col-lg-2"><asp:ListBox ID="LstElemento" runat="server"></asp:ListBox></div>
            </div>
            <p />
            <div class="row">
                <div class="col-lg-1"><input type="button" id="crearBtn" value="Crear" onclick="crear()" /></div>
                <div class="col-lg-1"><input type="button" id="eliminarBtn" value="Eliminar" onclick="eliminar()" /></div>
                <div class="col-lg-1"><input type="button" id="modificarBtn" value="Guardar"  onclick="guardar()" /></div>
                <div class="col-lg-1"><input type="button" id="limpiarBtn" value="Limpiar" onclick="limpiar()" /></div>
                <div class="col-lg-1"><input type="button" id="buscarBtn" value="Buscar" onclick="buscar()" /></div>
            </div>
            <div class="row">
                <h3 id="hpermisosexistentes">Permisos existentes</h3>
                <div class="col-lg-1">
                    <asp:ListBox ID="LstPermisos" runat="server" AutoPostBack="True"></asp:ListBox>
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
