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
            function onFailure(result) { }
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
            function onFailure(result){}
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
            function onFailure(result) { }
            var id = document.getElementById('ContentPlaceHolder1_usuarioId').value;
            PageMethods.obtenerNewsDeUsuario(id, onSuccess, onFailure);
        }

        window.onload = function () {
            obtenerTodosLosNewsletter();
            obtenerNovedades();
        }

        function obtenerNovedades() {
            function onSuccess(result) {
                if (result != null) {
                    var opcionesNews = document.getElementById('novedades');
                    for (i = 0; i < result.length; i++) {
                        var div = document.createElement('div');
                        div.id = 'novedad' + result[i].id;
                        var span = document.createElement('span');
                        span.appendChild(document.createTextNode(result[i].novedad));
                        div.appendChild(span);
                        opcionesNews.appendChild(div);
                    }
                    
                }
            }
            function onFailure(result) { }
            var idUsuario = document.getElementById('ContentPlaceHolder1_usuarioId').value;
            PageMethods.obtenerTodasLasNovedadesPorUsuario(idUsuario, onSuccess, onFailure);
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <aside id="lateral"></aside>
    <section id="contenedor">
         <input type="hidden" id="accion" name="accion" />
        <input type="hidden" id="usuarioId" name="usuarioId"  runat="server" />   
        <div id="newsletter">
         <div> Newletter: </div>

         <div id="opcionesNews">

         </div>
           <input type="submit" id="modificarBtn" value="Guardar" onclick="guardar()"/>
        </div>
        <div id="2barra"></div>
        <div id="novedades">

        </div>
    </section>
</asp:Content>
