<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Admin/admin.Master" CodeBehind="AdministrarPermisos.aspx.vb" Inherits="TFI.AdministrarPermisos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../scripts/abmscripts.js"></script>
    <script type="text/javascript">
        function limpiar() {
            limpiarForm();
            deshabilitar();
        }

        function guardar() {
       
            var permiso = { id: document.getElementById('id').value, nombre: document.getElementById('nombre').value, url: document.getElementById('url').value, elemento: {id:document.getElementById('elemento').value, nombre:'', leyendaPorDefecto:''}}
                PageMethods.guardar(permiso, onSuccess, onFailure);
                function onSuccess(result) {
                    alert(result);
                    document.getElementById('modificarBtn').disabled = 'disabled';
                }
                function onFailure(result) {
                    alert(result);
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
                alert(result);
            }
        }

     
        function crear() {
            function onSuccess(result){
                document.getElementById('id').value=result;
            }
            function onFailure(result){
                alert(result.get_message);
            }
            PageMethods.obtenerProximoId(onSuccess, onFailure);
            document.getElementById('id').disabled = 'disabled';
            document.getElementById('modificarBtn').disabled = '';
        }

        function buscar() {
            PageMethods.buscar(document.getElementById('nombre').value, onSuccess, onFailure);
            function onSuccess(result) {
                var permiso = result
                document.getElementById('id').value = permiso.id;
                document.getElementById('nombre').value = permiso.nombre;
                document.getElementById('url').value = permiso.url;
                document.getElementById('modificarBtn').disabled = '';
                document.getElementById('elemento').value = permiso.elemento.id;
               
            }
            function onFailure(result) {
                alert(result);
            }
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
         <p />
        <span id="spurl">url:</span><input type="text" id="url" />
        <p />
          <span id="spidelemento">Id elemento:</span><input type="text" id="elemento" />
        <p />
        <input type="button" id="crearBtn" value="Crear" onclick="crear()" />
        <input type="button" id="eliminarBtn" value="Eliminar" onclick="eliminar()" />
        <input type="button" id="modificarBtn" value="Guardar" disabled="disabled" onclick="guardar()" />
        <input type="button" id="limpiarBtn" value="Limpiar" onclick="limpiar()" />
        <input type="button" id="buscarBtn" value="Buscar" onclick="buscar()" />
   

    </section>
</asp:Content>