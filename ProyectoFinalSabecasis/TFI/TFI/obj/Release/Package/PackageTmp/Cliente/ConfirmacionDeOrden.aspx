<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/ConsumidorFinal.Master" CodeBehind="ConfirmacionDeOrden.aspx.vb" Inherits="TFI.ConfirmacionDeOrden" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        window.onload = function () {
            var loc = document.location.href;
            var getString = loc.split('?')[1];
            var idOrden = getString.split('=')[1];
            var texto = document.getElementById('spconfirmacion').innerHTML;
            document.getElementById('spconfirmacion').innerHTML = texto.replace('?', idOrden);

            obtenerEncuestas();
        }

       
        function enviarEncuesta() {
            function onSuccess(result) { }
            function onFailure(result){}

            for (i = 0; i < encuestas.length; i++) {
                for (j = 0; j < encuestas[i].preguntas.length; j++) {
                    for (h = 0; h < encuestas[i].preguntas[j].respuestas.length; h++) {
                        var elem = document.getElementById(encuestas[i].preguntas[j].respuestas[h].id);
                        if (elem.checked == true) {
                            PageMethods.actualizarOpcion(encuestas[i].preguntas[j].respuestas[h], onSuccess, onFailure);
                        }
                    }
                }
            }
            alert('Gracias!');
        }

        function obtenerEncuestas() {
            function onSuccess(result) {
                encuestas = result;
                if (result != null) {
                    var seccionEncuestas = document.getElementById('encuestas');
                    for (i = 0; i < result.length; i++) {
                        for (j = 0; j < result[i].preguntas.length; j++) {
                            var separador = document.createElement('DIV');
                            separador.id = 'sep' + result[i].preguntas[j].id;
                            var pregunta = document.createElement('DIV');
                            pregunta.id = 'pr' + result[i].preguntas[j].id;
                            pregunta.appendChild(document.createTextNode(result[i].preguntas[j].pregunta));
                            separador.appendChild(pregunta);
                            var respuestas = document.createElement('div');
                            var tabla = document.createElement('TABLE');
                            var tr = document.createElement('TR');
                            tabla.appendChild(tr);
                            respuestas.appendChild(tabla);
                            for (h = 0; h < result[i].preguntas[j].respuestas.length; h++) {
                                var opt = document.createElement('INPUT');
                                opt.type = 'RADIO';
                                opt.value = result[i].preguntas[j].respuestas[h].id;
                                opt.name = result[i].preguntas[j].id;
                                opt.id = result[i].preguntas[j].respuestas[h].id;
                                var td = document.createElement('TD');
                                td.appendChild(document.createTextNode(result[i].preguntas[j].respuestas[h].opcion));
                                tr.appendChild(td);
                                var td2 = document.createElement('TD');
                                td2.appendChild(opt);
                                tr.appendChild(td2);
                            }
                            separador.appendChild(respuestas);
                            seccionEncuestas.appendChild(separador);
                        }
                        
                    }
                }
            }
            function onFailure(result) { }
            PageMethods.obtenerEncuestasPorTipo(2, onSuccess, onFailure);
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div id="background">
        <img src="../static/fondo%204.jpg" />
    </div> 
     <aside id="lateral"></aside>
    <section id="contenedor">
        <div id="2barra"></div>
        <input type="hidden" id="accion" name="accion" />
        <div id="contenido" runat="server">
            <span id="spconfirmacion"> Su orden fue creada con el número de orden: ?.  Usted recibirá un emaild e confirmación dentro de las próximas 24 horas</span>
        </div>
         <div id="3barra"></div>
        <div id="encuestas">

        </div>
        <p><input type="button" value="Enviar encuesta" id="encuestaBtn" name="encuestaBtn" onclick="enviarEncuesta()" /></p>
    </section>
</asp:Content>
