<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/ConsumidorFinal.Master" CodeBehind="OfertasPersonalizadas.aspx.vb" Inherits="TFI.OfertasPersonalizadas" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        window.onload = function () {
            obtenerOfertasPersonales();
        }
        function obtenerOfertasPersonales() {
            function onSuccess(result) {
                var opcionesNews = document.getElementById('contenedor');
                for (i = 0; i < result.length; i++) {
                    var div = document.createElement('div');
                    var opt = document.createElement('input');
                    opt.value = result[i].id;
                    opt.id = result[i].id;
                    opt.name = 'ofertas';
                    opt.type = 'checkbox';
                    var span = document.createElement('span');
                    span.appendChild(document.createTextNode(result[i].oferta));
                    div.appendChild(opt);
                    div.appendChild(span);
                    opcionesNews.appendChild(div);
                }
                obtenerOfertasDeUsuario();
            }
            
            function onFailure(result) { }
            PageMethods.obtenerOfertasPersonales(onSuccess, onFailure)
        }

        function obtenerOfertasDeUsuario() {
            function onSuccess(result) {
                if (result != null) {
                    for (i = 0; i < result.length; i++) {
                        document.getElementById(result[i].id).checked = true;
                    }
                }
            }
            function onFailure(result) { }
            var id = document.getElementById('ContentPlaceHolder1_idUsuario').value;
            PageMethods.obtenerOfertasPersonalesDeUsuario(id, onSuccess, onFailure);
        }

        function guardar() {
            var check = document.getElementsByName('ofertas');
            var news = new Array()
            for (i = 0; i < check.length; i++) {
                if (check[i].checked == true) {
                    news[news.length] = check[i].id;
                }
            }
            var idUsuario = document.getElementById('ContentPlaceHolder1_idUsuario').value;
            function onSuccess(result) {
                alert(result)
            }
            function onFailure(result) { }
            PageMethods.guardar(news, idUsuario, onSuccess, onFailure);
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
        <input type="button" value="Guardar" id="guardarBtn" onclick="guardar()"/>
    </section>
</asp:Content>
