<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Admin/admin.Master" CodeBehind="AdministrarElementos.aspx.vb" Inherits="TFI.AdministrarElementos" %>

<%@ OutputCache Location="None" VaryByParam="None" %>
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
            deshabilitar();
        }

        function deshabilitar() {
            document.getElementById('modificarBtn').disabled = 'disabled';

            document.getElementById('ContentPlaceHolder1_id').disabled = '';
        }
        function guardar() {
            var elemento = {
                id: document.getElementById('ContentPlaceHolder1_id').value,
                nombre: document.getElementById('ContentPlaceHolder1_nombre').value,
                leyendaPorDefecto: document.getElementById('ContentPlaceHolder1_leyenda').value
            }
            PageMethods.guardar(elemento, onSuccess, onFailure);
            function onSuccess(result) {
                alert(result);
                limpiar();
            }
            function onFailure(result) {
                alert(response._message);
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
                alert(response._message);
            }
        }

        function crear() {
            document.getElementById('ContentPlaceHolder1_id').disabled = 'disabled';
            document.getElementById('modificarBtn').disabled = '';
            PageMethods.obtenerId(onSuccess, onFailure);
            function onSuccess(result) {
                document.getElementById('ContentPlaceHolder1_id').value = result;
            }
            function onFailure(result) {
                alert(response._message);
            }
        }

        function buscar() {
            function onSuccess(response) {
                var elemento = response
                if (elemento != null) {
                    document.getElementById('ContentPlaceHolder1_id').value = elemento.id;
                    document.getElementById('ContentPlaceHolder1_nombre').value = elemento.nombre;
                    document.getElementById('ContentPlaceHolder1_leyenda').value = elemento.leyendaPorDefecto;
                    document.getElementById('modificarBtn').disabled = '';
                }
            }
            function onFailure(response) {
                alert(response._message());
            }
            var nombre = document.getElementById('ContentPlaceHolder1_nombre').value;
            if (nombre != '') {
                PageMethods.buscar(document.getElementById('ContentPlaceHolder1_nombre').value, onSuccess, onFailure);
            }
        }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <nav class="navbar navbar-inverse navbar-fixed-top" role="navigation">
        <div class="container">
            <div class="mensaje-respuesta-contacto" id="LblMensaje" runat="server"></div>
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
        <span id="spid">Id:</span><input type="text" id="id" runat="server" />
        <p />
        <span id="spnombre">Nombre:</span><input type="text" id="nombre" runat="server" />
        <p />
        <span id="spleyendaDefecto">Leyenda por defecto: </span><input type="text" id="leyenda" runat="server" />
        <p />
        <input type="button" id="crearBtn" value="Crear" onclick="crear()" />
        <input type="button" id="eliminarBtn" value="Eliminar" onclick="eliminar()" />
        <input type="button" id="modificarBtn" value="Guardar" disabled="disabled" onclick="guardar()" />
        <input type="button" id="limpiarBtn" value="Limpiar" onclick="limpiar()" />
        <input type="button" id="buscarBtn" value="Buscar" onclick="buscar()" />
        <p />
     <asp:ListBox ID="LstElementos" runat="server" AutoPostBack="True"></asp:ListBox>
    </section>
     
    <div id="idioma" class="modal fade" role="dialog">
        <div id="Div2" class="modal-dialog modal-lg modal-content modal-dooba modal-idioma">
            <button type="button" class="btn btn-default btn-modal-close btn-circle pull-right" data-dismiss="modal"><i class="fa fa-times"></i></button>
            <div class="col-md-3"><span id="sidioma">Idioma: </span></div>
            <select id="idiomaSeleccionado" onchange="seleccionarIdioma();"></select>
        </div>
    </div>
</asp:Content>
