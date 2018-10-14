<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/ConsumidorFinal.Master" CodeBehind="Ayuda.aspx.vb" Inherits="TFI.Ayuda" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        window.onload = function () {
            obtenerArticulos(); 
        }
        
        function obtenerArticulos() {
            function onSuccess(result) {
                var opcionesNews = document.getElementById('opciones');
                while (opcionesNews.firstChild) {
                    opcionesNews.removeChild(opcionesNews.firstChild);
                }
                var defaultcontent = '<div class="post-preview"><a href="#articuloxxx_modal" data-target="#articuloxxx_modal" data-toggle="collapse"><h3 class="post-title">??</h3></a></div><div class="collapse" id="articuloxxx_modal">ttt</div>';
                if (result != null) {
                    var completeContent = "";
                    var intermediateContent = "";
                    var opcionesNews = document.getElementById('opciones');
                    for (i = 0; i < result.length; i++) {
                        intermediateContent = defaultcontent.replace("xxx", result[i].id).replace("xxx", result[i].id).replace("xxx", result[i].id);
                        intermediateContent = intermediateContent.replace("??", result[i].articulo);
                        intermediateContent = intermediateContent.replace("ttt", result[i].contenido);
                        completeContent = completeContent.concat(intermediateContent);
                        intermediateContent = "";
                    }
                    opcionesNews.innerHTML = completeContent;
                   
                }
            }
            function onFailure(result) { }
            PageMethods.obtenerTodosLosArticulos(onSuccess, onFailure);
        }
    </script>
   <link href="../static/shop-homepage.css" rel="stylesheet" />
    <script src="../scripts/jquery.js"></script>
    <script src="../scripts/bootstrap.min.js"></script>
    <link href="../static/clean-blog.css" rel="stylesheet" />

        <!-- Bootstrap Core CSS -->
    <link href="../static/font-awesome/css/bootstrap.min.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
      <nav class="navbar navbar-inverse navbar-fixed-top" role="navigation">
        <div class="container">

            <div class="navbar-header page-scroll">
                <a class="navbar-brand" href="/Cliente/Inicio.aspx">
                    <img class="dooba-img" src="../static/logotipo-trans.png" id="logo"/></a>
            </div>

            <div class="collapse navbar-collapse" >
                <ul class="nav navbar-nav navbar-right" id="menuHorizontal">
                    <li class="hidden">
                        <a href="#page-top"></a>
                    </li>
                    <li>
                        <a href="/Cliente/IniciarSesion.aspx" id ="alogin">Iniciar sesión</a>
                    </li>
                    <li>
                        <a href="/Cliente/Inicio.aspx">Inicio</a>
                    </li>
                    <li>
                        <a href="#" data-target="#idioma" data-toggle="modal" id="acambiaridioma">Cambiar idioma</a>
                    </li>
                     <li>
                        <a href="/Cliente/Catalogo.aspx" id="acatalogo" >Catálogo</a>
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
      
    </nav>
   <header class="intro-header" style="background-image: url('../static/Preguntas.jpg')">
        <div class="container">
            <div class="row">
                <div class="col-lg-8 col-lg-offset-2 col-md-10 col-md-offset-1">
                    <div class="site-heading">
                        <h1 id="hayudaenlinea">Ayuda en línea</h1>
                    </div>
                </div>
            </div>
        </div>
    </header>
    <section class="busqueda-formulario">
        <asp:Label ID="LblMensaje" runat="server" Text="Label"></asp:Label>
        <input type="hidden" id="idUsuario" runat="server" />
        <div class="row">
             <span id="spbuscarformulario">¿No sabés dónde ir? Buscá la página acá </span><input type="text" id="buscarForm"/><input type="button" id="buscarBtn" value="Buscar"  onclick="window.location.href = '/Cliente/BusquedaFormulario.aspx?busqueda=' + document.getElementById('buscarForm').value;" />
        </div >
    </section>
    <section class="busqueda-formulario">
        <a href="/Cliente/ChatDeSoporteAlCliente.aspx" id="achatesoporte">Chat de soporte</a>
    </section>
    <section class="busqueda-formulario">
        <div class="container">
            <div  class="row"> 
                 <div class="col-lg-8 col-lg-offset-2 col-md-10 col-md-offset-1" id="opciones">
                  
                 </div>
            </div>
        </div>
    </section>
     <div id="idioma" class="modal fade" role="dialog">
        <div id="Div2" class="modal-dialog modal-lg modal-content modal-dooba modal-idioma">
             <button type="button" class="btn btn-default btn-modal-close btn-circle pull-right" data-dismiss="modal"><i class="fa fa-times"></i></button>
             <div class="col-md-3"><span id="sidioma">Idioma: </span></div><select id="idiomaSeleccionado" onchange="seleccionarIdioma();"></select>
        </div>
    </div>
</asp:Content>
