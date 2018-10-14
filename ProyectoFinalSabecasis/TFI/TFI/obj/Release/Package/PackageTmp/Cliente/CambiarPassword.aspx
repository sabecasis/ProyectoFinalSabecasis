<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/ConsumidorFinal.Master" CodeBehind="CambiarPassword.aspx.vb" Inherits="TFI.CambiarPassword" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
    <link href="../static/bootstrap.min.css" rel="stylesheet" />  
    <script src="../scripts/jquery.js"></script>
    <script src="../scripts/bootstrap.min.js"></script>
    <link href="../static/font-awesome/css/font-awesome.min.css" rel="stylesheet" />
    <script>
        window.onload = function () {
            document.getElementById('ContentPlaceHolder1_urlanterior').value = document.referrer;
            obtenerUsuarioDeUrl();
            $("#ContentPlaceHolder1_mansajeEnPantalla").fadeOut(6000, function () {
            });
        }



        function obtenerUsuarioDeUrl() {
            var params = location.search.split("?");
            usuario = '';
            if (params.length > 1) {
                var partes = params[1].split("=");
                usuario = partes[1];
            }
            if (usuario != '') {
                document.getElementById('ContentPlaceHolder1_nombreusuario').value = usuario;
            }
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
                </ul>
            </div>
        </div>  
    </nav>
    <section id="contenedor">
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
