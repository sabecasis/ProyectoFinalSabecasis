<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Admin/admin.Master" CodeBehind="AdministrarNovedades.aspx.vb" Inherits="TFI.AdministrarNovedades" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <link href="../static/bootstrap.min.css" rel="stylesheet" />
    <script src="../scripts/jquery.js"></script>
    <script src="../scripts/bootstrap.min.js"></script>
    <link href="../static/font-awesome/css/font-awesome.min.css" rel="stylesheet" />
    <link href="../static/shop-homepage.css" rel="stylesheet" />
    <link href="../static/sidebar.css" rel="stylesheet" />
     <script src="../scripts/abmscripts.js"></script>
    <script>
        window.onload = function () {
            obtenerTodosLosNewsletter();
            obtenerNovedades();
        }

        function obtenerTodosLosNewsletter() {
            function onSuccess(result) {
                if (result != null) {
                    var opcionesNews = document.getElementById('newsletters');
                    var span = document.createElement('span');
                    span.id = 'spnewsletter';
                    span.appendChild(document.createTextNode('Newsletter'));
                    opcionesNews.appendChild(span);
                    var div = document.createElement('select');
                    div.name = 'newsletterscombo';
                    div.id = 'newsletterscombo';                  
                    for (i = 0; i < result.length; i++) {
                        var opt = document.createElement('option');
                        opt.value = result[i].id;
                        opt.appendChild(document.createTextNode(result[i].descripcion))
                        div.appendChild(opt);
                        
                    }
                    opcionesNews.appendChild(div);
                    
                    
                }
            }
            function onFailure(result) {
                alert(result._message);
            }
            PageMethods.obtenerTodosLosNewsletter(onSuccess, onFailure);
        }

        function guardar() {
            function onSuccess(result) {
                limpiarForm();
                obtenerNovedades();
            }
            function onFailure(result) {
                alert(result._message);
            }
            var news = document.getElementById('newsletterscombo').value;
            
            var nombre = document.getElementById('nombre').value;
            var novedad = document.getElementById('novedad').value;
            var titulo = document.getElementById('titulo').value;
            var objetoNovedad = { id: 0, nombre: nombre, novedad: novedad, newsletter: { id: news }, titulo: titulo };
            PageMethods.guardar(objetoNovedad,onSuccess, onFailure);
        }

        function cargarInfo() {
            function onSuccess(result) {
                if (result != null) {
                    document.getElementById('id').value = result.id;
                    document.getElementById('nombre').value = result.nombre;
                    document.getElementById('novedad').value = result.novedad;
                    document.getElementById('titulo').value = result.titulo;
                    document.getElementById('newsletters').value = result.newsletter.id;
                }
            }
            function onFailure(result) {
                alert(result._message);
            }
            document.getElementById('id').value = this.id;
            document.getElementById('nombre').value = this.name;
            PageMethods.obtenerNovedadPorNombre(this.name, onSuccess, onFailure);
            
        }

        function obtenerNovedades() {
            function onSuccess(result) {
                var opcionesNews = document.getElementById('opciones');
                while (opcionesNews.firstChild) {
                    opcionesNews.removeChild(opcionesNews.firstChild);
                }
                if (result != null) {
                    var opcionesNews = document.getElementById('opciones');
                    for (i = 0; i < result.length; i++) {
                        var div = document.createElement('div');
                        var opt = document.createElement('input');
                        opt.value = result[i].id;
                        opt.id = result[i].id;
                        opt.name = result[i].nombre;
                        opt.type = 'radio';
                        opt.onclick=cargarInfo
                        var span = document.createElement('span');
                        span.appendChild(document.createTextNode(result[i].nombre));
                        div.appendChild(opt);
                        div.appendChild(span);
                        opcionesNews.appendChild(div);
                    }
                    
                }
            }
            function onFailure(result) {
                alert(result._message);
            }
            PageMethods.obtenerTodasLasNovedades(onSuccess, onFailure);
        }

        function eliminar() {
            function onSuccess(result) {
                limpiarForm();
                obtenerNovedades();
            }
            function onFailure(result) {
                alert(result._message);
            }
            var idnovedad = document.getElementById('id').value;
            PageMethods.eliminar(idnovedad, onSuccess, onFailure);
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
    <input type="hidden" id="accion" name="accion"/>
    <section id="contenedor">
         <div class="row">
           
            <div class="mensaje-respuesta-contacto" id="LblMensaje" runat="server"></div>
        </div>
        <input type="hidden" id="idUsuario" runat="server" />
        <span id="spid">Id:</span><input type="text" id="id" name="id" readonly="readonly"/>
        <p />
        <span id="spnombre">Nombre:</span><input type="text" name="nombre" id="nombre" />
        <p />
        <div id="newsletters"></div>
        <p />
          <span id="sptitulon">Titulo</span><input type="text" name="titulo" id="titulo" />
        <p />
        <span id="spnovedad">Novedad</span>
        <p />
        <textarea id="novedad" rows="10", cols="50"></textarea>
        <p />

        <span id="spimagenadd">Imagen Add: </span><asp:FileUpload ID="ImagenNovedad" runat="server" />
        <p />
          <span id="spimagenprincipaladd">Imagen Principal Novedad: </span><asp:FileUpload ID="ImagenPrincipal" runat="server" />
        <p />
        <input type="submit" value="Guardar" id="modificarBtn" name="modificarBtn" onclick="guardar(); setAccion(this.id);"/>
         <input type="button" value="Eliminar" id="eliminarBtn" onclick="eliminar()"/>
        <input type="button" value="Limpiar" id="limpiarBtn" onclick="limpiarForm()"/>
        <p />
        <div id="opciones">

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
