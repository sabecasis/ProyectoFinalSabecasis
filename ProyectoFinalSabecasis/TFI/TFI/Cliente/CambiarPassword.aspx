<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/ConsumidorFinal.Master" CodeBehind="CambiarPassword.aspx.vb" Inherits="TFI.CambiarPassword" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
    <link href="../static/bootstrap.min.css" rel="stylesheet" />  
    <script src="../scripts/jquery.js"></script>
      <link href="../static/shop-homepage.css" rel="stylesheet" />  
    <script src="../scripts/bootstrap.min.js"></script>
    <link href="../static/font-awesome/css/font-awesome.min.css" rel="stylesheet" />
     <link href="../static/font-awesome/css/bootstrap.min.css" rel="stylesheet" />
    <script>
        window.onload = function () {
            document.getElementById('ContentPlaceHolder1_urlanterior').value = document.referrer;
            $("#ContentPlaceHolder1_mansajeEnPantalla").fadeOut(6000, function () {
            });
        }


    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <div id="background">
            <img src="../static/fondo%205.jpg"  />
        </div>
        <nav class="navbar navbar-inverse navbar-fixed-top" role="navigation">
        <div class="container">
            <div class="navbar-header page-scroll">
              
                <a class="navbar-brand" href="/Cliente/Inicio.aspx">
                    <img class="dooba-img" src="../static/logotipo-trans.png" id="logo" /></a>
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
    <section id="contenedor">
         <div class="mensaje-respuesta-contacto" id="LblMensaje" runat="server"></div>
        <p />
        <input type="hidden" id="urlanterior" name="urlanterior" runat="server"/>
        <span id="mansajeEnPantalla" name="mansajeEnPantalla" runat="server"></span>
        <div class="col-md-5"><span id="spusuario"> Usuario: </span></div><div class="col-md-5"><input type="text" disabled="disabled" id="nombreusuario" name="nombreusuario" runat="server" /></div>
       <div class="col-md-5"> <span id="spcontrasenia">Contraseña:</span></div><div class="col-md-5"><input type="password" id="contrasenia" name="contrasenia" runat="server"/></div>
       <div class="col-md-5"> <span id="spcontrasenia2">Repetir Contraseña:</span></div><div class="col-md-5"><input type="password" id="contrasenia2" name="contrasenia2" runat="server"/></div>
       <div class="col-md-5"> <input type="submit" id="loginBtn" name="cambiarPassBtn" value="Cambiar Contraseña" onclick="cambiar();"  runat="server"/></div>
    </section>


     <div id="idioma" class="modal fade" role="dialog">
        <div id="Div2" class="modal-dialog modal-lg modal-content modal-dooba modal-idioma">
             <button type="button" class="btn btn-default btn-modal-close btn-circle pull-right" data-dismiss="modal"><i class="fa fa-times"></i></button>
             <div class="col-md-3"><span id="sidioma">Idioma: </span></div><select id="idiomaSeleccionado" onchange="seleccionarIdioma();"></select>
        </div>
    </div>

     
</asp:Content>
