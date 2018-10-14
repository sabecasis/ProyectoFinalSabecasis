<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Admin/admin.Master" CodeBehind="PreguntaDeEncuesta.aspx.vb" Inherits="TFI.PreguntaDeEncuesta" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../static/bootstrap.min.css" rel="stylesheet" />
    <script src="../scripts/jquery.js"></script>
    <script src="../scripts/abmscripts.js"></script>
    <script src="../scripts/bootstrap.min.js"></script>
    <link href="../static/font-awesome/css/font-awesome.min.css" rel="stylesheet" />
    <link href="../static/shop-homepage.css" rel="stylesheet" />
    <link href="../static/sidebar.css" rel="stylesheet" />
    <script>
        function obtenerId() {
            try {
                var loc = document.location.href;
                var param = loc.split('?')[1];
                var valor = param.split('=')[1];
                document.getElementById('encuesta').value = valor;
            } catch (err) {
            }
        }
        window.onload = function () {
            listaOpciones = new Array();
            obtenerId();
            obtenerOpciones();
        }

        function buscar() {
            function onSuccess(result) {
                if (result != null) {
                    document.getElementById('id').value = result.id;
                    document.getElementById('pregunta').value = result.pregunta;
                    document.getElementById('encuesta').value = result.encuesta.id;
                    document.getElementById('activa').checked = result.activa;
                    document.getElementById('agregarBtn').disabled = 'disabled';
                    listaOpciones = result.respuestas;
                    obtenerOpciones();
                }
            }
            function onFailure(result) {
                alert(result._message);
            }
            PageMethods.buscar(document.getElementById('id').value, onSuccess, onFailure);
        }

        function limpiar() {
            limpiarForm();
            document.getElementById('agregarBtn').disabled = '';
            obtenerId();
            document.getElementById('opp').innerHTML = '';
        }

        function obtenerOpciones() {
            var contenido = document.getElementById('opp');
            contenido.innerHTML = '<div id=\'opp\'></div>';
            for (i = 0; i < listaOpciones.length; i++) {
                var opcion = document.createElement('DIV');
                opcion.innerHTML = listaOpciones[i].opcion;
                contenido.appendChild(opcion);
            }
        }

        function agregar() {
            listaOpciones[listaOpciones.length] = { id: 0, opcion: document.getElementById('opcion').value };
            obtenerOpciones();
            document.getElementById('opcion').value = '';

        }
        function crear() {
            function onSuccess(result) {
                if (result != null) {
                    document.getElementById('id').value = result;
                }
            }
            function onFailure(result) {
                alert(result._message);
            }
            PageMethods.obtenerProximoId(onSuccess, onFailure);
        }

        function guardar() {
            function onSuccess(result) {
                alert(result);
                limpiarForm();
                obtenerId();
                var contenido = document.getElementById('opp');
                contenido.innerHTML = '<div id=\'opp\'></div>';
                listaOpciones = new Array();
            }
            function onFailure(result) {
                alert(result._message);
            }
            var pregunta = { id: document.getElementById('id').value, pregunta: document.getElementById('pregunta').value, respuestas: listaOpciones, activa: document.getElementById('activa').checked }
            var encuesta = { id: document.getElementById('encuesta').value, preguntas: [pregunta], nombre: '' }
            PageMethods.guardar(encuesta, onSuccess, onFailure);
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
                                <a href="#" data-target="#idioma" data-toggle="modal" id="acambiaridioma">Cambiar Idioma</a>
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
    <section id="contenedor" class="container">
        <input type="hidden" id="accion" name="accion" />
         <div class="row">
             <div class="mensaje-respuesta-contacto" id="LblMensaje" runat="server"></div>
            </div>
        <div class="row">
            <div class="col-lg-3"><span id="spid">Id:</span></div>
            <div class="col-lg-3">
                <input type="text" id="id" name="id" /></div>
        </div>
        <div class="row">
            <div class="col-lg-3"><span id="sppregunta">Pregunta:</span></div>
            <div class="col-lg-3">
                <input type="text" id="pregunta" name="pregunta" /></div>
        </div>
        <div class="row">
            <div class="col-lg-3"><span id="spencuesta">Encuesta: </span></div>
            <div class="col-lg-3">
                <input type="text" id="encuesta" name="encuesta" disabled="disabled" /></div>
        </div>
        <div class="row">
            <div class="col-lg-3"><span id="spactiva">Activa: </span></div>
            <div class="col-lg-3">
                <input type="checkbox" id="activa" name="activa" /></div>
        </div>
        <div class="row">
            <div class="col-lg-2">
                <input type="button" id="modificarBtn" value="Guardar" onclick="guardar()" /></div>
            <div class="col-lg-2">
                <input type="button" id="crearBtn" value="Crear" onclick="crear()" /></div>
            <div class="col-lg-2">
                <input type="button" id="limpiarBtn" value="Limpiar" onclick="limpiar()" /></div>
            <div class="col-lg-2">
                <input type="button" id="buscarBtn" value="Buscar" onclick="buscar()" /></div>
        </div>
        <div class="row">

            <div class="col-lg-3">
                <span id="spopcion">Opcion:</span></div><div class="col-lg-3">
                    <input type="text" id="opcion" name="opcion" /></div>
            </div>
            <div class="row">
                <div class="col-lg-3">
                    <input type="button" id="agregarBtn" value="Agregar" onclick="agregar()" /></div>
            </div>
            <div class="row">
                <div class="row">
                    <h1 id="hopciones">Opciones</h1>
                </div>
                <div class="row">
                    <div id="opp">
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
