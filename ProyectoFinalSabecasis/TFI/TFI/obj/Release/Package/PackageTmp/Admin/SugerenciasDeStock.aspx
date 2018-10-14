<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Admin/admin.Master" CodeBehind="SugerenciasDeStock.aspx.vb" Inherits="TFI.SugerenciasDeStock" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        window.onload = function () {
            cargarProductos();
        }
        function cargarProductos() {
            function onSuccess(result) {
                if (result != null) {
                    var select = document.getElementById('producto');
                    var select2 = document.getElementById('sugerido');
                    for (i = 0; i < result.length; i++) {
                        var opt = document.createElement('OPTION');
                        opt.text = result[i].nombre;
                        opt.value = result[i].nroDeProducto;
                        select.appendChild(opt);
                       
                    }

                    for (i = 0; i < result.length; i++) {
                        var opt = document.createElement('OPTION');
                        opt.text = result[i].nombre;
                        opt.value = result[i].nroDeProducto;
                        select2.appendChild(opt);

                    }
                }
            }
            function onFailure(result) { }
            PageMethods.cargarProductos(onSuccess, onFailure);
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <aside id="lateral"></aside>
    <section id="contenedor">
         <input type="hidden" id="accion" name="accion" />
        <span id="spproducto">Producto:</span><select id="producto" name="producto" ></select>
        <p />
        <span id="spproductosugerido">Producto sugerido: </span><select id="sugerido" name="sugerido" ></select>
        <p />
        <input type="submit" id="eliminarBtn" value="Eliminar" onclick="setAccion(this.id)" />
        <input type="submit" id="modificarBtn" value="Guardar" onclick="setAccion(this.id)" />
        <input type="button" id="limpiarBtn" value="Limpiar" onclick="limpiarForm()" />
    </section>
</asp:Content>
