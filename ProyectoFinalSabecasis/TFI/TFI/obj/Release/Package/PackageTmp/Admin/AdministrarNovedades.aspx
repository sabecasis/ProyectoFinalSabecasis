<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Admin/admin.Master" CodeBehind="AdministrarNovedades.aspx.vb" Inherits="TFI.AdministrarNovedades" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        window.onload = function () {
            obtenerTodosLosNewsletter();
            obtenerNovedades();
        }

        function obtenerTodosLosNewsletter() {
            function onSuccess(result) {
                if (result != null) {
                    var opcionesNews = document.getElementById('newsletters');
                    for (i = 0; i < result.length; i++) {
                        var div = document.createElement('div');
                        var opt = document.createElement('input');
                        opt.value = result[i].id;
                        opt.id = result[i].id;
                        opt.name = 'newsletters';
                        opt.type = 'radio';
                        var span = document.createElement('span');
                        span.appendChild(document.createTextNode(result[i].descripcion));
                        div.appendChild(opt);
                        div.appendChild(span);
                        opcionesNews.appendChild(div);
                    }
                    
                }
            }
            function onFailure(result) { }
            PageMethods.obtenerTodosLosNewsletter(onSuccess, onFailure);
        }

        function guardar() {
            function onSuccess(result) {
                limpiarForm();
                obtenerNovedades();
            }
            function onFailure(result) { }
            var news = document.getElementsByName('newsletters');
            var newsletterEncontrado;
            for (i = 0; i < news.length; i++) {
                newsletterEncontrado = news[0].id;
                if (news[i].checked == true) {
                    newsletterEncontrado = news[i].id;
                    break;
                }
            }
            var nombre = document.getElementById('nombre').value;
            var novedad = document.getElementById('novedad').value;
            var objetoNovedad = { id: 0, nombre: nombre, novedad: novedad, newsletter: { id: newsletterEncontrado } };
            PageMethods.guardar(objetoNovedad,onSuccess, onFailure);
        }

        function cargarInfo() {
            document.getElementById('id').value = this.id;
            document.getElementById('nombre').value = this.name;
        }

        function obtenerNovedades() {
            function onSuccess(result) {
                var opcionesNews = document.getElementById('opciones');
                while (opcionesNews.firstChild) {
                    opcionesNews.removeChild(opcionesNews.firstChild);
                }
                if (result != null) {
                    var opcionesNews = document.getElementById('opciones');
                    for (i = 0; i < result.length; i++) {
                        var div = document.createElement('div');
                        var opt = document.createElement('input');
                        opt.value = result[i].id;
                        opt.id = result[i].id;
                        opt.name = result[i].nombre;
                        opt.type = 'radio';
                        opt.onclick=cargarInfo
                        var span = document.createElement('span');
                        span.appendChild(document.createTextNode(result[i].nombre));
                        div.appendChild(opt);
                        div.appendChild(span);
                        opcionesNews.appendChild(div);
                    }
                    
                }
            }
            function onFailure(result) { }
            PageMethods.obtenerTodasLasNovedades(onSuccess, onFailure);
        }

        function eliminar() {
            function onSuccess(result) {
                limpiarForm();
                obtenerNovedades();
            }
            function onFailure(result) { }
            var idnovedad = document.getElementById('id').value;
            PageMethods.eliminar(idnovedad, onSuccess, onFailure);
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div id="background">
         <img src="../static/fondo%2010.jpg" />
    </div> 
    <aside id="lateral">
        <div id="menuVertical"></div>
    </aside>
    <section id="contenedor">
        <input type="hidden" id="idUsuario" runat="server" />
        <span id="spid">Id:</span><input type="text" id="id" disabled="disabled"/>
        <p />
        <span id="spnombre">Nombre:</span><input type="text" id="nombre" />
        <p />
        <div id="newsletters"></div>
        <p />
        <span id="spnovedad">Novedad</span>
        <p />
        <textarea id="novedad" rows="15", cols="15"></textarea>
        <p />
        <input type="button" value="Guardar" id="guardarBtn" onclick="guardar()"/>
         <input type="button" value="Eliminar" id="eliminarBtn" onclick="eliminar()"/>
        <input type="button" value="Limpiar" id="limpiarBtn" onclick="limpiarForm()"/>
        <p />
        <div id="opciones">

        </div>
    </section>
</asp:Content>
