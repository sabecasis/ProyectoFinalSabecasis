<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Admin/admin.Master" CodeBehind="PreguntaDeEncuesta.aspx.vb" Inherits="TFI.PreguntaDeEncuesta" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        function obtenerId() {
            var loc = document.location.href;
            var param = loc.split('?')[1];
            var valor = param.split('=')[1];
            document.getElementById('encuesta').value = valor;
        }
        window.onload = function () {
            listaOpciones = new Array();
            obtenerId();
            obtenerOpciones();
        }

        function obtenerOpciones() {
            var contenido = document.getElementById('opp');
            contenido.innerHTML = '<div id=\'opp\'></div>';
            for (i = 0; i < listaOpciones.length; i++) {
                var opcion = document.createElement('DIV');
                opcion.innerHTML = listaOpciones[i].opcion;
                contenido.appendChild(opcion);
            }
        }

        function agregar() {
            listaOpciones[listaOpciones.length] = {id:0, opcion: document.getElementById('opcion').value };
            obtenerOpciones();
            document.getElementById('opcion').value = '';
            
        }
        function crear() {
            function onSuccess(result) {
                if (result != null) {
                    document.getElementById('id').value = result;
                }
            }
            function onFailure(result) { }
            PageMethods.obtenerProximoId(onSuccess, onFailure);
        }

        function guardar() {
            function onSuccess(result) {
                alert(result);
                limpiarForm();
                obtenerId();
                var contenido = document.getElementById('opp');
                contenido.innerHTML = '<div id=\'opp\'></div>';
                listaOpciones = new Array();
            }
            function onFailure(result) {
                var resultado = result;
            }
            var pregunta = { id: document.getElementById('id').value, pregunta: document.getElementById('pregunta').value, respuestas: listaOpciones, activa: document.getElementById('activa').checked }
            var encuesta = { id: document.getElementById('encuesta').value, preguntas: [pregunta], nombre:''}
            PageMethods.guardar(encuesta, onSuccess, onFailure);
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
      <aside id="lateral">
         
      </aside>
    <section id="contenedor">
        <div id="2barra"></div>
        <input type="hidden" id="accion" name="accion" />
        <span id="spid">Id:</span><input type="text" id="id" name="id" />
        <p />
        <span id="sppregunta">Pregunta:</span><input type="text" id="pregunta" name="pregunta" />
        <p />
        <span id="spencuesta">Encuesta: </span><input type="text" id="encuesta"  name="encuesta" disabled="disabled"/>
        <p />
        <span id="spactiva">Activa: </span><input type="checkbox" id="activa" name="activa"/>
        <p />
        <input type="button" id="modificarBtn" value="Guardar" onclick="guardar()"/>
        <input type="button" id="crearBtn" value="Crear" onclick="crear()" />
         <input type="button" id="limpiarBtn" value="Limpiar" onclick="limpiarForm()"/>
        <input type="button" id="buscarBtn" value="Buscar" onclick="buscar()" />
        <p />
         <div id="3barra"></div>
        <span id="spopcion">Opcion:</span><input type="text" id="opcion" name="opcion" />
        <p />
        <input type="button" id="agregarBtn" value="Agregar" onclick="agregar()" />
        <p />
         <h1 id="hopciones">Opciones</h1>
          <div id="opp">

          </div>
    </section>
</asp:Content>
