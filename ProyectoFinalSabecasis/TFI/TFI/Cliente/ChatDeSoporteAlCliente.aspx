<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/ConsumidorFinal.Master" CodeBehind="ChatDeSoporteAlCliente.aspx.vb" Inherits="TFI.ChatDeSoporteAlCliente" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
   <link href="../static/bootstrap.min.css" rel="stylesheet" />
    <script src="../scripts/jquery.js"></script>
    <script src="../scripts/bootstrap.min.js"></script>
    <link href="../static/font-awesome/css/font-awesome.min.css" rel="stylesheet" />
     <link href="../static/font-awesome/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../static/shop-homepage.css" rel="stylesheet" />
    <link href="../static/sidebar.css" rel="stylesheet" />
     <script>
        window.onload = function () {
           
            intervalo = setInterval(buscarRespuestaDeSesion, 1000);
            fechaUltimoComentario = 0;
            document.getElementById('mensaje').disabled = 'disabled';
            document.getElementById('enviarBtn').disabled = 'disabled';

        }

        function obtenerMensajes() {
            function onSuccess(result) {
                if (result != null) {
                    if (result.fecha != fechaUltimoComentario) {
                        fechaUltimoComentario = result.fecha;
                        document.getElementById('ventana').value = document.getElementById('ventana').value + '\n \n' + result.usuario.nombre + ': \n' +  result.comentario;
                    }
                }
            }
            function onFailure(result) {
            }
            var nroDeConsulta = document.getElementById('ContentPlaceHolder1_nroconsulta').innerText;
            PageMethods.obtenerMensajes(nroDeConsulta, onSuccess, onFailure);
        }

        function buscarRespuestaDeSesion() {
            function onSuccess(result){
                if (result!=null) {
                    if (result.estado.id == 2) {
                        clearInterval(intervalo);
                        document.getElementById('mensaje').disabled = '';
                        document.getElementById('enviarBtn').disabled = '';
                        document.getElementById('spnombreagenteatencion').innerText=result.sesion.asesor.nombre;
                        esconder();
                        intervalo2 = setInterval(obtenerMensajes, 500);
                    }
                }
            }

            function onFailure(result) { }
            var nroDeConsulta = document.getElementById('ContentPlaceHolder1_nroconsulta').innerText;
            PageMethods.buscarRespuestaEnCola(nroDeConsulta, onSuccess, onFailure);
        }

        function crearComentario() {
            function onSuccess(result) {
                document.getElementById('mensaje').value = '';
            }
            function onFailure(result) {
                alert(result);
            }
            var comentario = document.getElementById('mensaje').value;
            var idUsuario = document.getElementById('ContentPlaceHolder1_idUsuario').value;
            var idSesion = document.getElementById('ContentPlaceHolder1_nroconsulta').innerText;
            PageMethods.enviarComentario(idUsuario, comentario, idSesion, onSuccess, onFailure);
        }

        function esconder() {
            document.getElementById('ajaxloader').style.display = 'none';
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
                <li class="active">
                    <a href="#" >Soporte al cliente</a>
                </li>
                <li>
                    <a href="/Cliente/Novedades.aspx">Subscripción a newsletter</a>
                </li>
            </ul>
        </div>
    </nav>
    
    <section class="contenedor-soft">
        <div class="bg-info">
        <span id="spdescripcionchat">Usted se encuentra en el móulo de atención al cliente por chat. Por favor aguarde, un representante tomará la comunicación en breve.</span>
    </div>
         <input type="hidden" id="referrer" runat="server" />
           <div id="contacto">
               <div class="mensaje-respuesta-contacto" id="LblMensaje" runat="server"></div>
               <input type="hidden" id="idUsuario" runat="server" />
                <table class="table">
                    <tr>
                        <td><span id="spnrodeticketchat">Nro. de consulta</span></td>
                        <td><asp:Label ID="nroconsulta" runat="server" Text=""></asp:Label></td>
                    </tr>
                    <tr>
                        <td><span id="spagenteatencionchat">Su agente es</span></td>
                        <td><span id="spnombreagenteatencion"></span> <div id="ajaxloader">
                   <img src="../static/ajax-loader.gif" width="50" height="50"/>
               </div></td>
                    </tr>
                </table>
               <table>
                    <tr>
                        <td><textarea rows="15" cols="50"  id="ventana" name="ventana"></textarea></td>
                    </tr>
                      <tr>
                        <td><textarea rows="5" cols="50"  id="mensaje" name="mensaje"></textarea></td>
                    </tr>
                     <tr>
                        <td><input type="button"  id="enviarBtn" name="enviarBtn" value="Enviar" onclick="crearComentario()"/></td>
                    </tr>
                </table>
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
