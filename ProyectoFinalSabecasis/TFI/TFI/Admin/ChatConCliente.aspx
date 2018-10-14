<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Admin/admin.Master" CodeBehind="ChatConCliente.aspx.vb" Inherits="TFI.ChatConCliente" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <link href="../static/bootstrap.min.css" rel="stylesheet" />
    <script src="../scripts/jquery.js"></script>
    <script src="../scripts/bootstrap.min.js"></script>
    <link href="../static/font-awesome/css/font-awesome.min.css" rel="stylesheet" />
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
                //alert(result._message);
            }
            var nroDeConsulta = document.getElementById('ContentPlaceHolder1_nroconsulta').innerText;
            PageMethods.obtenerMensajes(nroDeConsulta, onSuccess, onFailure);
        }

        function buscarRespuestaDeSesion() {
            function onSuccess(result){
                if (result!=null) {
                        clearInterval(intervalo);
                        document.getElementById('mensaje').disabled = '';
                        document.getElementById('enviarBtn').disabled = '';
                        document.getElementById('spnombreagenteatencion').innerText = result.sesion.usuario.nombre;
                        document.getElementById('ContentPlaceHolder1_nroconsulta').innerText=result.sesion.id;
                        esconder();
                        intervalo2 = setInterval(obtenerMensajes, 500);
                }
            }

            function onFailure(result) {
                //alert(result._message);
            }
            var nroDeConsulta = document.getElementById('ContentPlaceHolder1_idUsuario').value;
            PageMethods.buscarRespuestaEnCola(nroDeConsulta, onSuccess, onFailure);
        }

        function finalizar() {
            clearInterval(intervalo2);
            intervalo = setInterval(buscarRespuestaDeSesion, 1000);
            function onSuccess(result) {
                mostrar();
                document.getElementById('spnombreagenteatencion').innerText="";
                document.getElementById('ContentPlaceHolder1_nroconsulta').innerText = "";
                fechaUltimoComentario = 0;
            }
            function onFailure(result) {

                alert(result._message);
            }
            var idUsuario = document.getElementById('ContentPlaceHolder1_idUsuario').value;
            var nroDeConsulta = document.getElementById('ContentPlaceHolder1_nroconsulta').innerText;
            PageMethods.finalizarSesion(nroDeConsulta, idUsuario, onSuccess, onFailure);
        }

        function crearComentario() {
            function onSuccess(result) {
                document.getElementById('mensaje').value = '';
            }
            function onFailure(result) {
                //alert(result._message);
            }
            var comentario = document.getElementById('mensaje').value;
            var idUsuario = document.getElementById('ContentPlaceHolder1_idUsuario').value;
            var idSesion = document.getElementById('ContentPlaceHolder1_nroconsulta').innerText;
            PageMethods.enviarComentario(idUsuario, comentario, idSesion, onSuccess, onFailure);
        }

        function mostrar() {
            document.getElementById('ajaxloader').style.display = 'block';
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
    <section id="contenedor">
           <div id="contacto">
               <input type="text" id="idUsuario" runat="server" style="visibility:hidden" />
                <table>
                    <tr>
                        <td><span id="spnrodeticketchat">Nro. de consulta</span></td>
                        <td><asp:Label ID="nroconsulta" runat="server" Text=""></asp:Label></td>
                    </tr>
                    <tr>
                        <td><span id="spusuarioatencionchat">Su cliente es</span></td>
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
                   <tr>
                        <td><input type="button"  id="finalizarBtn" name="finalizarBtn" value="Finalizar" onclick="finalizar()"/></td>
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
