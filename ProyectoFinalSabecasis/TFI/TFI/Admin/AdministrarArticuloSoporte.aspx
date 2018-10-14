<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Admin/admin.Master" CodeBehind="AdministrarArticuloSoporte.aspx.vb" Inherits="TFI.AdministrarArticuloSoporte" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
         <link href="../static/bootstrap.min.css" rel="stylesheet" />
    <script src="../scripts/jquery.js"></script>
    <script src="../scripts/bootstrap.min.js"></script>
    <link href="../static/font-awesome/css/font-awesome.min.css" rel="stylesheet" />
    <link href="../static/shop-homepage.css" rel="stylesheet" />
    <link href="../static/sidebar-front.css" rel="stylesheet" />
    <script src="../scripts/abmscripts.js"></script>
    <link href="../static/font-awesome/css/bootstrap.min.css" rel="stylesheet" />
    <script>
        window.onload = function () {
            obtenerArticulos();
        }

        function guardar() {
            function onSuccess(result) {
                limpiarForm();
                obtenerArticulos();
            }
            function onFailure(result) {
                alert(result._message);
            }
           
            var nombre = document.getElementById('articulo').value;
            var novedad = document.getElementById('contenido').value;
            var objetoNovedad = { id: 0, articulo: nombre, contenido: novedad };
            PageMethods.guardar(objetoNovedad, onSuccess, onFailure);
        }

        function cargarInfo() {
            document.getElementById('id').value = this.id;
            document.getElementById('articulo').value = this.name;
        }

        function obtenerArticulos() {
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
                        opt.name = result[i].articulo;
                        opt.type = 'radio';
                        opt.onclick = cargarInfo
                        var span = document.createElement('span');
                        span.appendChild(document.createTextNode(result[i].articulo));
                        div.appendChild(opt);
                        div.appendChild(span);
                        opcionesNews.appendChild(div);
                    }

                }
            }
            function onFailure(result) {
                alert(result._message);
            }
            PageMethods.obtenerTodosLosArticulos(onSuccess, onFailure);
        }

        function eliminar() {
            function onSuccess(result) {
                limpiarForm();
                obtenerArticulos();
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
                         <a href="#" id="aPerfil" runat="server" data-toggle="dropdown" class="dropdown-toggle"></a>
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
         <input type="hidden" id="referrer" runat="server" />
         <input type="hidden" id="accion" name="accion" />
        <input type="hidden" id="usuarioId" name="usuarioId"  runat="server" />   
        <div class="container">
       <div class="row">
             <div class="col-lg-1"><span id="spid">Id</span></div><div class="col-lg-1"><input type="text" id="id" disabled="disabled"/></div>
       </div>
             <div class="row">
        <div class="col-lg-1"><span id="sparticulo">Articulo</span></div><div class="col-lg-1"><input type="text" id="articulo" /></div>
          </div>
                 <div class="row">
         <div class="col-lg-1"><span id="spcontenidoarticulo">Contenido</span></div>
        <div class="col-lg-1"><textarea id="contenido" rows="15" cols="50"></textarea></div>
              </div>
         <div class="row">
        <div class="col-lg-1"><input type="button" id="guardarBtn" value="Guardar" onclick="guardar()"/></div>
        <div class="col-lg-1"><input type="button" id="eliminatBtn" value="Eliminar" onclick="eliminar()"/></div>
             </div>
         <div class="row">
        <div id="opciones" class="row">

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
