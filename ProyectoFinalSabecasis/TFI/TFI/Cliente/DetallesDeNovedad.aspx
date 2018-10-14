<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/ConsumidorFinal.Master" CodeBehind="DetallesDeNovedad.aspx.vb" Inherits="TFI.DetallesDeNovedad" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
  
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <meta name="description" content="" />
    <meta name="author" content="" />
  

  
    <link href="../static/shop-homepage.css" rel="stylesheet" />
    <script src="../scripts/jquery.js"></script>
    <script src="../scripts/bootstrap.min.js"></script>
     <link href="../static/font-awesome/css/font-awesome.min.css" rel="stylesheet" type="text/css" />
      <link href="../static/bootstrap.min.css" rel="stylesheet" />
    <link href="../static/clean-blog.css" rel="stylesheet"/>
     <link href="../static/font-awesome/css/bootstrap.min.css" rel="stylesheet" />
   
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <input type="hidden" id="usuarioId" runat="server" />  
    <nav class="navbar navbar-inverse navbar-fixed-top" role="navigation">
        <div class="container">

          <div class="collapse navbar-collapse" >
                 <a class="navbar-brand" href="/Cliente/Inicio.aspx"><img id="logo" src="../static/logotipo-trans.png" /></a>
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
                        <a href="/Cliente/Ayuda.aspx"  id="aayuda">Ayuda</a>
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
      <header class="intro-header" style="background-image: url('/static/fondo11.jpg')" id="cabecera" runat="server">
        <div class="container">
            <div class="mensaje-respuesta-contacto" id="LblMensaje" runat="server"></div> 
            <div class="row">
                <div class="col-lg-8 col-lg-offset-2 col-md-10 col-md-offset-1">
                    <div class="post-heading" id="titulo" runat="server">
                        <h1>Novedades!</h1>
                        <h2 class="subheading">Las novedades serán mostradas aquí</h2>
                    </div>
                </div>
            </div>
        </div>
    </header>

    <!-- Post Content -->
    <article>
        <div class="container">
            <div class="row">
                <div class="col-lg-8 col-lg-offset-2 col-md-10 col-md-offset-1" id="contenido" runat="server">
                 
                </div>
            </div>
        </div>
    </article>

    <hr>
   
     <input type="hidden" id="urlanterior" name="urlanterior" runat="server"/> 

    <div id="idioma" class="modal fade" role="dialog">
        <div id="Div2" class="modal-dialog modal-lg modal-content modal-dooba modal-idioma">
             <button type="button" class="btn btn-default btn-modal-close btn-circle pull-right" data-dismiss="modal"><i class="fa fa-times"></i></button>
             <div class="col-md-3"><span id="sidioma">Idioma: </span></div><select id="idiomaSeleccionado" onchange="seleccionarIdioma();"></select>
        </div>
    </div>
     
</asp:Content>
