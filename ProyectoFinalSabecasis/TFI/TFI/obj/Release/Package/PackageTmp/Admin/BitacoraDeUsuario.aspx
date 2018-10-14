<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Admin/admin.Master" CodeBehind="BitacoraDeUsuario.aspx.vb" Inherits="TFI.BitacoraDeUsuario" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../scripts/jquery-2.1.4.min.js"></script>
    <script src="../scripts/abmscripts.js"></script>
    <script src="../scripts/jquery-ui.min.js"></script>
    <link href="../static/jquery-ui.css" rel="stylesheet" />
    <script>
        function buscar() {
            var bitacora = {
                usuario: { id: 0, nombre: document.getElementById('nombreusuario').value},
                evento:{id:document.getElementById('eventos').value, evento:''},
                fecha: document.getElementById('fecha').value == '' ? '0001-01-01' : document.getElementById('fecha').value,
                hora:''
            }

            PageMethods.buscar(bitacora, onSuccess, onFailure)
        }

        $(function () {
            $("#fecha").datepicker({
                dateFormat: "yy-mm-dd"
            });
        });

        window.onload = function () {
            obtenerEventos();
            limpiar();
        }

        function obtenerEventos() {
            function onSuccesEvento(result) {
                var select = document.getElementById('eventos');
                var option = document.createElement('OPTION');
                option.id = 0;
                option.value = 0;
                var texto = document.createTextNode('seleccionar');
                option.appendChild(texto);
                select.appendChild(option);
                if (result != null && result.length > 0) {
                    for (i = 0; i < result.length; i++) {
                        var option = document.createElement('OPTION');
                        option.id = result[i].id;
                        option.value = result[i].id;
                        var texto = document.createTextNode(result[i].evento);
                        option.appendChild(texto);
                        select.appendChild(option);
                    }
                }
            }
            function onFailureEvento(result) {
            }
            PageMethods.obtenerEventos(onSuccesEvento, onFailureEvento);
        }


        function onSuccess(result) {
            var div = document.getElementById('contenedorElementos');
            while (div.firstChild) {
                div.removeChild(div.firstChild);
            }
            var tabla = document.createElement('TABLE');
            var cabecera = document.createElement('TR');
            var cevento = document.createElement('TH');
            cevento.appendChild(document.createTextNode('Evento'));
            cevento.id = 'thevento';
            cabecera.appendChild(cevento);
            var cusuario = document.createElement('TH');
            cusuario.appendChild(document.createTextNode('Usuario'));
            cusuario.id = 'thusuario';
            cabecera.appendChild(cusuario);
            var cfecha = document.createElement('TH');
            cfecha.appendChild(document.createTextNode('Fecha'));
            cfecha.id = 'thfecha';
            cabecera.appendChild(cfecha);
            var chora = document.createElement('TH');
            chora.appendChild(document.createTextNode('Hora'));
            chora.id = 'thhora';
            cabecera.appendChild(chora);
            tabla.appendChild(cabecera);
            div.appendChild(tabla);
            if (result != null && result.length > 0) {

                for (i = 0; i < result.length; i++) {
                    var tr = document.createElement('TR');
                    var td1 = document.createElement('TD');
                    var texto1 = document.createTextNode(result[i].evento.evento);
                    td1.appendChild(texto1);
                    tr.appendChild(td1);
                    var td2 = document.createElement('TD');
                    var texto2 = document.createTextNode(result[i].usuario.nombre);
                    td2.appendChild(texto2);
                    tr.appendChild(td2);
                    var td3 = document.createElement('TD');
                    var texto3 = document.createTextNode(result[i].fecha);
                    td3.appendChild(texto3);
                    tr.appendChild(td3);
                    var td4 = document.createElement('TD');
                    var texto4 = document.createTextNode(result[i].hora);
                    td4.appendChild(texto4);
                    tr.appendChild(td4);
                    tabla.appendChild(tr);
                }
                
            }
        }
        function onFailure(result) {
            alert(result);
        }

        function limpiar() {
            limpiarForm();
            var bitacora = { usuario: { id: 0, nombre: '' }, evento: { id: 0, evento: '' }, fecha: '0001-01-01', hora: ''}
            PageMethods.buscar(bitacora, onSuccess, onFailure);
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <aside id="lateral"></aside>
    <section id="contenedor">
        <div id="2barra"></div>
        <span id="spevento">Evento:</span><select id="eventos"></select>
        <p />
        <span id="spusuario">Usuario:</span><input type="text" id="nombreusuario" />
        <p />
        <span id="spfecha">fecha</span><input type="text" id="fecha" name="fecha" />
        <p/>
        <input type="button" id="buscarBtn" value="Buscar"  onclick="buscar();" />
         <input type="button" id="limpiarBtn" value="Limpiar"  onclick="limpiar();" />
        <p />
        <div id="3barra"></div>
        <div>
        <div id="contenedorElementos">
        </div>
        </div>
    </section>
</asp:Content>
