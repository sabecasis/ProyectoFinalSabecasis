<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Admin/admin.Master" CodeBehind="AdministrarElementos.aspx.vb" Inherits="TFI.AdministrarElementos" %>

<%@ OutputCache Location="None" VaryByParam="None" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../scripts/abmscripts.js"></script>
    <script type="text/javascript">

        function limpiar() {
            limpiarForm();
            deshabilitar();
        }

        function guardar() {
            var elemento = {
                id: document.getElementById('id').value,
                nombre: document.getElementById('nombre').value,
                leyendaPorDefecto: document.getElementById('leyenda').value
            }
            PageMethods.guardar(elemento, onSuccess, onFailure);
            function onSuccess(result) {
                alert(result);
                limpiar();
            }
            function onFailure(result) {
                alert(response.get_message());
            }
            document.getElementById('id').disabled = 'disabled';
        }

        function eliminar() {
            PageMethods.eliminar(document.getElementById('id').value, onSuccess, onFailure);
            function onSuccess(result) {
                alert(result);
                document.getElementById('modificarBtn').disabled = 'disabled';
            }
            function onFailure(result) {
                alert(response.get_message());
            }
        }

        function crear() {
            document.getElementById('id').disabled = 'disabled';
            document.getElementById('modificarBtn').disabled = '';
            PageMethods.obtenerId(onSuccess, onFailure);
            function onSuccess(result) {
                document.getElementById('id').value = result;
            }
            function onFailure(result) {
                alert(response.get_message());
            }
        }

        function buscar() {
            function onSuccess(response) {
                var elemento = response

                document.getElementById('id').value = elemento.id;
                document.getElementById('nombre').value = elemento.nombre;
                document.getElementById('leyenda').value = elemento.leyendaPorDefecto;
                document.getElementById('modificarBtn').disabled = '';
            }
            function onFailure(response) {
                alert(response.get_message());
            }
            PageMethods.buscar(document.getElementById('nombre').value, onSuccess, onFailure);
        }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   
    <aside id="lateral"></aside>
    <section id="contenedor">
        <div id="2barra"></div>
        <span id="spid">Id:</span><input type="text" id="id" />
        <p />
        <span id="spnombre">Nombre:</span><input type="text" id="nombre" />
        <p />
        <span id="spleyendaDefecto">Leyenda por defecto: </span><input type="text" id="leyenda" />
        <p />
        <input type="button" id="crearBtn" value="Crear" onclick="crear()" />
        <input type="button" id="eliminarBtn" value="Eliminar" onclick="eliminar()" />
        <input type="button" id="modificarBtn" value="Guardar" disabled="disabled" onclick="guardar()" />
        <input type="button" id="limpiarBtn" value="Limpiar" onclick="limpiar()" />
        <input type="button" id="buscarBtn" value="Buscar" onclick="buscar()" />
    </section>
  
</asp:Content>
