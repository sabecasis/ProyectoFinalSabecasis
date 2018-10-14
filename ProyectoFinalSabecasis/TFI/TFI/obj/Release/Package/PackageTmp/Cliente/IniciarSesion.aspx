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
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div id="background">
        <img src="../static/fondo%205.jpg"  />
    </div>
    <input type="hidden" id="usuarioId" runat="server" />  
    <nav class="navbar navbar-inverse navbar-fixed-top" role="navigation">
        <div class="container">

            <div class="navbar-header page-scroll">
                <button type="button" class="navbar-toggle" data-toggle="collapse" data-target="#bs-example-navbar-collapse-1">
                    <span class="sr-only">Toggle navigation</span>
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                </button>
                <a class="navbar-brand" href="/Cliente/Inicio.aspx">
                    <img class="dooba-img" src="../static/logotipo-trans.png" /></a>
            </div>

            <div class="collapse navbar-collapse" >
                <ul class="nav navbar-nav navbar-right" id="menuHorizontal">
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
                </ul>
            </div>
        </div>  
    </nav>
    <section>
    <div class="container loginmodal-container">
        <div class="row">
            <asp:Label ID="LblMensaje" runat="server" Text="" CssClass="mensaje-respuesta-contacto"></asp:Label>
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
              $("#ContentPlaceHolder1_LblMensaje").fadeOut(6000, function () {
              });
          }

    </script>
</asp:Content>
