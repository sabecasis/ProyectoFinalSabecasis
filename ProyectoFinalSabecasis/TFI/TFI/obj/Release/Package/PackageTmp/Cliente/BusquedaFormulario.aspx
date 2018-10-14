<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/ConsumidorFinal.Master" CodeBehind="BusquedaFormulario.aspx.vb" Inherits="TFI.BusquedaFormulario" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        window.onload = function () {
            var loc = document.location.href;
            var getString = loc.split('?')[1];
            var criterio = getString.split('=')[1];
            function onSuccess(result) {
                if (result != null) {
                    var cont = document.getElementById('resultados');
                    for (i = 0; i < result.length; i++) {
                        var a = document.createElement('a');
                        a.id = i;
                        a.href = result[i];
                        a.appendChild(document.createTextNode(result[i]));
                        cont.appendChild(a);
                    }
                }
            }
            function onFailure(result) { }
            PageMethods.buscar(criterio, onSuccess, onFailure);
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div id="background">
        <img src="../static/fondo%205.jpg" />
    </div>
    <div id="resultados">

    </div>
</asp:Content>
