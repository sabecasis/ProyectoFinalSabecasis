<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/ConsumidorFinal.Master" CodeBehind="IniciarSesion.aspx.vb" Inherits="TFI.IniciarSesion" %>
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
     <link href="../static/font-awesome/css/bootstrap.min.css" rel="stylesheet" />
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div id="background">
        <img src="../static/fondo%205.jpg"  />
    </div>
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
                        <a href="#" id ="alogin">Iniciar sesión</a>
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
    <section>
    <div class="container loginmodal-container">
        <div class="row">
           
            <div class="mensaje-respuesta-contacto" id="LblMensaje" runat="server"></div>
        </div>
         <div class="row">
            <div class="col-md-4"><span id="spusuario">Usuario:</span></div><div class="col-md-4"><input type="text" id="nombreusuario" name="nombreusuario" runat="server"/></div>
        </div>
        <div class="row">
        <div class="col-md-4"> <span id="spcontrasenia">Contraseña:</span></div><div class="col-md-4"><input type="password" id="contrasenia" runat="server" name="contrasenia" /></div>
        </div>
        <div class="row">
            <input type="submit" id="loginBtn" name="loginBtn" value="Iniciar Sesión"  runat="server"/>
        </div>
        <div class="row">
            <div class="col-md-9"> <a href="/Cliente/RecuperarPassword.aspx" id="aCambPass">Olvidé mi contraseña</a></div>
            <div class="col-md-5"><a href="/Cliente/Registrarse.aspx" id="aregistro">Registrarse</a></div>
        </div>
    </div>
     <input type="hidden" id="urlanterior" name="urlanterior" runat="server"/>
    </section>   

    <div id="idioma" class="modal fade" role="dialog">
        <div id="Div2" class="modal-dialog modal-lg modal-content modal-dooba modal-idioma">
             <button type="button" class="btn btn-default btn-modal-close btn-circle pull-right" data-dismiss="modal"><i class="fa fa-times"></i></button>
             <div class="col-md-3"><span id="sidioma">Idioma: </span></div><select id="idiomaSeleccionado" onchange="seleccionarIdioma();"></select>
        </div>
    </div>
      <script>
          window.onload = function () {
              document.getElementById('ContentPlaceHolder1_urlanterior').value = document.referrer;
              //$("#ContentPlaceHolder1_LblMensaje").fadeOut(6000, function () {
              //});
          }

    </script>
</asp:Content>
