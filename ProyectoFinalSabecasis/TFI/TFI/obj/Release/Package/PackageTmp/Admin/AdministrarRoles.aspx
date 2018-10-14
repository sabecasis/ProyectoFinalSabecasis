<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Admin/admin.Master" CodeBehind="AdministrarRoles.aspx.vb" Inherits="TFI.AdministrarRoles" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../scripts/abmscripts.js"></script>
    <script type="text/javascript">
        function limpiar() {
            limpiarForm();
            cargarPermisos();
            deshabilitar();
        }

        function guardar() {
            var arrayPermisos = document.getElementsByName('permisos');
            var permisosFiltrados = new Array();
            var j = 0;
            for (i = 0; i < arrayPermisos.length; i++) {
                if (arrayPermisos[i].checked == true) {
                    permisosFiltrados[j] = { id: arrayPermisos[i].value, nombre: '', elemento: { id: 0, nombre: '' } };
                    j++;
                }
            }
            
            var rol = { id: document.getElementById('id').value, nombre: document.getElementById('nombre').value, permisos: permisosFiltrados }
            PageMethods.guardar(rol, onSuccess, onFailure);
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

        function cargarPermisos() {
            function onSuccessPermisos(result) {
                var contenedor = document.getElementById('contenedorPermisos');
                while (contenedor.firstChild) {
                    contenedor.removeChild(contenedor.firstChild);
                }
                var ul = document.createElement('UL');
                contenedor.appendChild(ul);
                if (result != null && result.length > 0) {
                    for (i = 0; i < result.length; i++) {
                        var li = document.createElement('LI');
                        var elemento = document.createElement('INPUT');
                        elemento.type = 'CHECKBOX';
                        elemento.value = result[i].id;
                        elemento.name = 'permisos';
                        var texto = document.createElement('SPAN');
                        texto.appendChild(document.createTextNode(result[i].nombre));
                        li.appendChild(texto);
                        li.appendChild(elemento);
                        ul.appendChild(li);
                    }
                }
            }

             function onFailure(result) { }
            PageMethods.obtenerTodosLosPermisos(onSuccessPermisos, onFailure);
        }

        window.onload = function () {
            cargarPermisos();
        }

        function crear() {
            function onSuccess(result) {
                document.getElementById('id').value = result;
            }
            function onFailure(result) {
                alert(result.get_message);
            }
            PageMethods.obtenerProximoId(onSuccess, onFailure);
            document.getElementById('id').disabled = 'disabled';
            document.getElementById('modificarBtn').disabled = '';           
        }

        function buscar() {
            PageMethods.buscar(document.getElementById('nombre').value, onSuccess, onFailure);
            function onSuccess(result) {
                var rol = result
                document.getElementById('id').value = rol.id;
                document.getElementById('nombre').value = rol.nombre;
                document.getElementById('modificarBtn').disabled = '';

                var permisos = document.getElementsByName('permisos');
                
                for(j=0;j<rol.permisos.length;j++){
                    for (i = 0; i < permisos.length; i++) {
                        if (permisos[i].value == rol.permisos[j].id) {
                            permisos[i].checked = true;
                        }
                    }
                }
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
        <input type="button" id="crearBtn" value="Crear" onclick="crear()" />
        <input type="button" id="eliminarBtn" value="Eliminar" onclick="eliminar()" />
        <input type="button" id="modificarBtn" value="Guardar" disabled="disabled" onclick="guardar()" />
        <input type="button" id="limpiarBtn" value="Limpiar" onclick="limpiar()" />
        <input type="button" id="buscarBtn" value="Buscar" onclick="buscar()" />
        <p />
        <div id="3barra"></div>
        <div id="conetenedorAsignacion">
            <span>Permisos</span>
            <div id="contenedorPermisos">

            </div>
        </div>
    </section>
</asp:Content>
