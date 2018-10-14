<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/ConsumidorFinal.Master" CodeBehind="Ayuda.aspx.vb" Inherits="TFI.Ayuda" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        window.onload = function () {
            obtenerArticulos();
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
                        div.id = 'articulo' + result[i].id;
                        var br1 = document.createElement('BR');
                        var span = document.createElement('span');
                        span.appendChild(document.createTextNode(result[i].articulo));
                        var br = document.createElement('BR');
                        var span2 = document.createElement('span');
                        span2.appendChild(document.createTextNode(result[i].contenido));
                        div.appendChild(br1);
                        div.appendChild(span);
                        div.appendChild(br);
                        div.appendChild(span2);
                        opcionesNews.appendChild(div);
                    }

                }
            }
            function onFailure(result) { }
            PageMethods.obtenerTodosLosArticulos(onSuccess, onFailure);
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
        <div id="opciones">

        </div>
    </section>
</asp:Content>
