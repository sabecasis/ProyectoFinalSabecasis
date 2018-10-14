<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Admin/admin.Master" CodeBehind="AdministrarTiposDeGarantia.aspx.vb" Inherits="TFI.AdministrarTiposDeGarantia" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        function crear() {
                  
            PageMethods.obtenerId(onSuccess, onFailure);
            function onSuccess(result) {
                document.getElementById('id').value = result;
            }
            function onFailure(result) {
                alert(response.get_message());
            }
        }

        function buscar() {
            function onSuccess(result) {
                if (result != null) {
                    document.getElementById('id').value = result.id;
                    document.getElementById('dias').value = result.dias;
                    document.getElementById('descripcion').value = result.descripcion;
                }
            }
            function onFailure(result) {

            }
            var tipo =              
                    {
                        id: (document.getElementById('id').value=='')?0:document.getElementById('id').value,
                        dias: (document.getElementById('dias').value=='')?0:document.getElementById('dias').value,
                        descripcion: document.getElementById('descripcion').value 
                    } 
                
            PageMethods.buscar(tipo, onSuccess, onFailure);
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
        <span id="spdiasvigencia">Dias de vigencia:</span><input type="text" id="dias" name="dias"  />
        <p />
        <span id="spdescripcion">Descripción:</span><input type="text" id="descripcion" name="descripcion"/>
        <p/>
        <input type="submit" id="eliminarBtn" value="Eliminar" onclick="setAccion(this.id)" />
        <input type="button" id="crearBtn" value="Crear" onclick="crear()" />
        <input type="submit" id="modificarBtn" value="Guardar"  onclick="setAccion(this.id)"/>
         <input type="button" id="limpiarBtn" value="Limpiar" onclick="limpiarForm()"/>
        <input type="button" id="buscarBtn" value="Buscar" onclick="buscar()" />
        <p />
    </section>
</asp:Content>
