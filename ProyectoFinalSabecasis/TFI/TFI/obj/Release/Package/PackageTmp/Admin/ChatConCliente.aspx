﻿<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Admin/admin.Master" CodeBehind="ChatConCliente.aspx.vb" Inherits="TFI.ChatConCliente" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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
                        clearInterval(intervalo);
                        document.getElementById('mensaje').disabled = '';
                        document.getElementById('enviarBtn').disabled = '';
                        document.getElementById('spnombreagenteatencion').appendChild(document.createTextNode(result.sesion.usuario.nombre));
                        document.getElementById('ContentPlaceHolder1_nroconsulta').appendChild(document.createTextNode(result.sesion.id));
                        esconder();
                        intervalo2 = setInterval(obtenerMensajes, 500);
                }
            }

            function onFailure(result) { }
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
            function onFailure(result) { }
            var nroDeConsulta = document.getElementById('ContentPlaceHolder1_idUsuario').value;
            var idUsuario = document.getElementById('ContentPlaceHolder1_nroconsulta').value;
            PageMethods.finalizarSesion(nroDeConsulta, idUsuario, onSuccess, onFailure);
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

        function mostrar() {
            document.getElementById('ajaxloader').style.display = 'block';
        }
        function esconder() {
            document.getElementById('ajaxloader').style.display = 'none';
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="background">
         <img src="../static/fondo%202.jpg" />
    </div> 
    <aside id="lateral"></aside>
    <section id="contenedor">
           <div id="contacto">
               <input type="hidden" id="idUsuario" runat="server" />
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
</asp:Content>
