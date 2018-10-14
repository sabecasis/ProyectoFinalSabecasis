<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/ConsumidorFinal.Master" CodeBehind="Novedades.aspx.vb" Inherits="TFI.Novedades" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        function obtenerTodosLosNewsletter() {
            function onSuccess(result) {
                if (result != null) {
                    var opcionesNews = document.getElementById('opcionesNews');
                    for (i = 0; i < result.length; i++) {
                        var div = document.createElement('div');
                        var opt = document.createElement('input');
                        opt.value = result[i].id;
                        opt.id = result[i].id;
                        opt.name = 'newsletters';
                        opt.type = 'checkbox';
                        var span = document.createElement('span');
                        span.appendChild(document.createTextNode(result[i].descripcion));
                        div.appendChild(opt);
                        div.appendChild(span);
                        opcionesNews.appendChild(div);
                    }
                    obtenerNewsDeUsuario();
                }
            }
            function onFailure(result) {
                alert(result._message);
            }
            PageMethods.obtenerTodosLosNewsletter(onSuccess, onFailure);
        }

        function guardar() {
            var check = document.getElementsByName('newsletters');
            var news = new Array()
            for (i = 0; i < check.length; i++) {
                if (check[i].checked == true) {
                    news[news.length] = check[i].id;
                }
            }
            var idUsuario = document.getElementById('ContentPlaceHolder1_usuarioId').value;
            function onSuccess(result) {
                alert(result)
            }
            function onFailure(result) {
                alert(result._message);
            }
            PageMethods.guardar(idUsuario, news, onSuccess, onFailure);
        }

        function obtenerNewsDeUsuario() {
            function onSuccess(result) {
                if (result != null) {
                    for (i = 0; i < result.length; i++) {
                        document.getElementById(result[i].id).checked = true;
                    }
                }
            }
            function onFailure(result) {
                alert(result._message);
            }
            var id = document.getElementById('ContentPlaceHolder1_usuarioId').value;
            PageMethods.obtenerNewsDeUsuario(id, onSuccess, onFailure);
        }

        window.onload = function () {
           
            obtenerTodosLosNewsletter();

        }

       
    </script>
     <link href="../static/bootstrap.min.css" rel="stylesheet" />
    <script src="../scripts/jquery.js"></script>
    <script src="../scripts/bootstrap.min.js"></script>
    <link href="../static/font-awesome/css/font-awesome.min.css" rel="stylesheet" />
    <link href="../static/shop-homepage.css" rel="stylesheet" />
    <link href="../static/sidebar-front.css" rel="stylesheet" />
     <link href="../static/font-awesome/css/bootstrap.min.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     
    <nav class="navbar navbar-inverse navbar-fixed-top" role="navigation">
        <div class="container">

            <div class="navbar-header page-scroll">
                <a class="navbar-brand" href="/Cliente/Inicio.aspx">
                    <img class="dooba-img" src="../static/logotipo-trans.png" id="logo" /></a>
            </div>

            <div class="collapse navbar-collapse">
                <ul class="nav navbar-nav navbar-right" id="menuHorizontal">
                    <li class="hidden">
                        <a href="#page-top"></a>
                    </li>
                    <li>
                        <a href="/Cliente/IniciarSesion.aspx" id="alogin">Iniciar sesión</a>
                    </li>
                    <li>
                        <a href="/Cliente/Inicio.aspx">Inicio</a>
                    </li>
                    <li>
                        <a href="#" data-target="#idioma" data-toggle="modal" id="acambiaridioma">Cambiar idioma</a>
                    </li>
                    <li>
                        <a href="/Cliente/Catalogo.aspx" id="acatalogo">Catálogo</a>
                    </li>
                    <li>
                        <a href="/Cliente/Ayuda.aspx" id="aayuda">Ayuda</a>
                    </li>
                     <li>
                        <a href="/Cliente/NovedadesPublicas.aspx" id="aNovedadesPublicas">Novedades</a>
                    </li>
                </ul>
                 <div class="enlinea espaciado-izquierdo-breve espaciado-suerior" style="visibility:hidden"  id="user_tag"><i class="glyphicon glyphicon-user logo-usuario"></i><a href="/Cliente/Perfil.aspx" id="aPerfil" runat="server" class="usuario espaciado-izquierdo-breve"></a> </div>
            </div>
        </div>
        <div id="breadcrums" runat="server" class="breadcrums">
        </div>
        <div class="collapse navbar-collapse navbar-ex1-collapse">
            <ul class="nav navbar-nav side-nav">
                <li >
                    <a href="/Cliente/Perfil.aspx">Perfil</a>
                </li>
                <li>
                    <a href="/Cliente/ModuloDeCliente.aspx">Mis compras</a>
                </li>
                <li>
                    <a href="/Cliente/EstadoDeCuenta.aspx">Estado de cuenta</a>
                </li>
                <li>
                    <a href="/Cliente/RecuperarPassword.aspx">Recuperar contraseña</a>
                </li>
                <li>
                    <a href="/Cliente/ChatDeSoporteAlCliente.aspx">Soporte al cliente</a>
                </li>
                <li class="active">
                    <a href="#">Subscripción a newsletter</a>
                </li>
            </ul>
        </div>
    </nav>
    <section id="contenedor">
        <div class="mensaje-respuesta-contacto" id="LblMensaje" runat="server"></div>
        <input type="hidden" id="referrer" runat="server" />
         <input type="hidden" id="accion" name="accion" />
        <input type="hidden" id="usuarioId" name="usuarioId"  runat="server" />   
        <div id="newsletter">
         <h1 id="hnewsletter"> News letter </h1>

         <div id="opcionesNews">

         </div>
           <input type="submit" id="modificarBtn" value="Guardar" onclick="guardar();setAccion(this.id)"/>
        </div>
    </section>
</asp:Content>
