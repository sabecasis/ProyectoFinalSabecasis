<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Admin/admin.Master" CodeBehind="AdministrarArticuloSoporte.aspx.vb" Inherits="TFI.AdministrarArticuloSoporte" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        window.onload = function () {
            obtenerArticulos();
        }

        function guardar() {
            function onSuccess(result) {
                limpiarForm();
                obtenerArticulos();
            }
            function onFailure(result) { }
           
            var nombre = document.getElementById('articulo').value;
            var novedad = document.getElementById('contenido').value;
            var objetoNovedad = { id: 0, articulo: nombre, contenido: novedad };
            PageMethods.guardar(objetoNovedad, onSuccess, onFailure);
        }

        function cargarInfo() {
            document.getElementById('id').value = this.id;
            document.getElementById('articulo').value = this.name;
        }

        function obtenerArticulos() {
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
                        opt.name = result[i].articulo;
                        opt.type = 'radio';
                        opt.onclick = cargarInfo
                        var span = document.createElement('span');
                        span.appendChild(document.createTextNode(result[i].articulo));
                        div.appendChild(opt);
                        div.appendChild(span);
                        opcionesNews.appendChild(div);
                    }

                }
            }
            function onFailure(result) { }
            PageMethods.obtenerTodosLosArticulos(onSuccess, onFailure);
        }

        function eliminar() {
            function onSuccess(result) {
                limpiarForm();
                obtenerArticulos();
            }
            function onFailure(result) { }
            var idnovedad = document.getElementById('id').value;
            PageMethods.eliminar(idnovedad, onSuccess, onFailure);
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <aside id="lateral"></aside>
    <section id="contenedor">
         <input type="hidden" id="accion" name="accion" />
        <input type="hidden" id="usuarioId" name="usuarioId"  runat="server" />   
        <span id="spid">Id</span><input type="text" id="id" disabled="disabled"/>
        <p />
        <span id="sparticulo">Articulo</span><input type="text" id="articulo" />
         <p />
         <span id="spcontenidoarticulo">Contenido</span>
        <textarea id="contenido" rows="15" cols="20">
        </textarea>
        <p/>
        <input type="button" id="guardarBtn" value="Guardar" onclick="guardar()"/>
        <input type="button" id="eliminatBtn" value="Eliminar" onclick="eliminar()"/>
        <p />
        <div id="opciones">

        </div>
    </section>
</asp:Content>
