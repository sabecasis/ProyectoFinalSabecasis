<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Admin/admin.Master" CodeBehind="AdministrarOfertasPersonales.aspx.vb" Inherits="TFI.AdministrarOfertasPersonales" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        window.onload = function () {
            obtenerOfertasPersonales();
        }
        function obtenerOfertasPersonales() {
            function onSuccess(result) {
                var opcionesNews = document.getElementById('opciones');
                while (opcionesNews.firstChild) {
                    opcionesNews.removeChild(opcionesNews.firstChild);
                }
                for (i = 0; i < result.length; i++) {
                    var div = document.createElement('div');
                    var opt = document.createElement('input');
                    opt.value = result[i].oferta;
                    opt.id = result[i].id;
                    opt.name = result[i].oferta;
                    opt.type = 'checkbox';
                    opt.onclick=cargarInfo
                    var span = document.createElement('span');
                    span.appendChild(document.createTextNode(result[i].oferta));
                    div.appendChild(opt);
                    div.appendChild(span);
                    opcionesNews.appendChild(div);
                }
                
            }

            function onFailure(result) { }
            PageMethods.obtenerOfertasPersonales(onSuccess, onFailure)
        }

        function guardar() {
            function onSuccess(result) {
                obtenerOfertasPersonales();
                limpiarForm();
            }
            function onFailure(result) {
                alert(result);
            }
            var oferta = { id: (document.getElementById('id').value=='')?0:document.getElementById('id').value, oferta: document.getElementById('nombre').value }
            PageMethods.guardar(oferta, onSuccess, onFailure);
        }

        function eliminar() {
            function onSuccess(result) {
                obtenerOfertasPersonales();
                limpiarForm();
            }
            function onFailure(result) {
                alert(result);
            }
            var idOferta = (document.getElementById('id').value == '') ? 0 : document.getElementById('id').value;
            PageMethods.eliminar(idOferta, onSuccess, onFailure);
        }

        function cargarInfo() {
            document.getElementById('id').value = this.id;
            document.getElementById('nombre').value = this.name;
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
        <input type="button" value="Guardar" id="guardarBtn" onclick="guardar()"/>
         <input type="button" value="Eliminar" id="eliminarBtn" onclick="eliminar()"/>
        <input type="button" value="Limpiar" id="limpiarBtn" onclick="limpiarForm()"/>
        <p />
        <div id="opciones">

        </div>
    </section>
</asp:Content>
