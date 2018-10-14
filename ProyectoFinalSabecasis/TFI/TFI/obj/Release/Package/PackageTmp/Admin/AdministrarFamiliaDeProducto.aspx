<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Admin/admin.Master" CodeBehind="AdministrarFamiliaDeProducto.aspx.vb" Inherits="TFI.AdministrarFamiliaDeProducto" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        function buscar() {
            function onSuccess(result) {
                if (result != null) {
                    document.getElementById('id').value = result.id;
                    document.getElementById('familia').value = result.familia;
                }
            }
            function onFailure(result) {
            }
            var familia = { id: document.getElementById('id').value, familia: document.getElementById('familia').value };
            PageMethods.buscar(familia, onSuccess, onFailure);
        }

        function crear() {
            function onSuccess(result) {
                if (result != null) {
                    document.getElementById('id').value = result;
                }
            }
            function onFailure(result) { }
            PageMethods.obtenerId(onSuccess, onFailure);
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <aside id="lateral"></aside>
    <section id="contenedor">
        <div id="2barra"></div>
        <input type="hidden" id="accion" name="accion" />
        <p />
         <span id="spid">Id:</span><input type="text" id="id" name="id"/>
        <p />
        <span id="spfamiliaproducto">Familia:</span><input type="text" id="familia" name="familia"  />
        <p />
        <input type="submit" id="eliminarBtn" value="Eliminar" onclick="setAccion(this.id)" />
        <input type="button" id="crearBtn" value="Crear" onclick="crear()" />
        <input type="submit" id="modificarBtn" value="Guardar"  onclick="setAccion(this.id)"/>
         <input type="button" id="limpiarBtn" value="Limpiar" onclick="limpiarForm()"/>
        <input type="button" id="buscarBtn" value="Buscar" onclick="buscar()" />
        <p />
    </section>
</asp:Content>
