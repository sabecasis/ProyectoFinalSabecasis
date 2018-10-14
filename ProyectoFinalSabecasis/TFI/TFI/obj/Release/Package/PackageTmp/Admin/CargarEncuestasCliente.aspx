<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Admin/admin.Master" CodeBehind="CargarEncuestasCliente.aspx.vb" Inherits="TFI.CargarEncuestasCliente" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        function obtenerTiposDeEncuesta() {
            function onSuccess(result) {
                if (result != null) {
                    var select = document.getElementById('tipo');
                    for (i = 0; i < result.length; i++) {
                        var opt = document.createElement('OPTION');
                        opt.text = result[i].tipo;
                        opt.value = result[i].id;
                        select.appendChild(opt);
                    }
                }
            }
            function onFailure(result) {
            }
            PageMethods.obtenerTodosLosTiposDeEncuesta(onSuccess, onFailure);
        }

        window.onload = function () {
            obtenerTiposDeEncuesta();
        }

        function crear() {
            document.getElementById('aagregarPreguntas').href = "#";
            function onSuccess(result) {
                if (result != null) {
                   document.getElementById('id').value=result
                }
            }
            function onFailure(result) { }
            PageMethods.obtenerProximoId(onSuccess, onFailure);
        }

        function buscar() {
            function onSuccess(result) {
                if (result != null) {
                    document.getElementById('id').value = result.id;
                    document.getElementById('nombre').value = result.nombre;
                    document.getElementById('descripcion').value = result.descripcion;
                    var select = document.getElementById('tipo');
                    for (i = 0; i < select.length; i++) {
                        if (select[i].value == result.tipo.id) {
                            select.selectedIndex = select[i].index;
                        }
                    }
                    if (result.activa == true) {
                        document.getElementById('activa').checked = true;
                    }
                    document.getElementById('aagregarPreguntas').href = "/Admin/PreguntaDeEncuesta.aspx?encuestaid=" + result.id;
                }
            }
            function onFailure(result) { }
            var id = document.getElementById('id').value;
            var nombre = document.getElementById('nombre').value;
            PageMethods.buscar(id, nombre, onSuccess, onFailure)
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <aside id="lateral"></aside>
    <section id="contenedor">
        <div id="2barra"></div>
        <input type="hidden" id="accion" name="accion" />
        <span id="spid">Id:</span><input type="text" id="id" name="id" />
        <p />
        <span id="spnombre">Nombre:</span><input type="text" id="nombre" name="nombre" />
        <p />
        <span id="sptipoencuesta">Tipo de encuesta:</span><select id="tipo" name="tipo" ></select>
        <p />
        <span id="spdescripcion">Descripcion: </span><input type="text" id="descripcion"  name="descripcion"/>
        <p />
        <span id="spactiva">Activa: </span><input type="checkbox" id="activa" name="activa"/>
        <p />
        <input type="submit" id="modificarBtn" value="Guardar" onclick="setAccion(this.id)"/>
        <input type="button" id="crearBtn" value="Crear" onclick="crear()" />
         <input type="button" id="limpiarBtn" value="Limpiar" onclick="limpiarForm()"/>
        <input type="button" id="buscarBtn" value="Buscar" onclick="buscar()" />
        <a href="#" id="aagregarPreguntas" >Agregar preguntas</a> 
    </section>
</asp:Content>
