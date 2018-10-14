<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Admin/admin.Master" CodeBehind="IngresoDeStock.aspx.vb" Inherits="TFI.IngresoDeStock" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        function cargarSucursales() {
            function onSuccess(result) {
                if (result != null) {
                    var select = document.getElementById('sucursal');
                    for (i = 0; i < result.length; i++) {
                        var opt = document.createElement('OPTION');
                        opt.text = result[i].nombre;
                        opt.value = result[i].nroSucursal;
                        select.appendChild(opt);
                    }
                }
                cargarProductos();
            }
            function onFailure(result) { }
            PageMethods.cargarSucursales(onSuccess, onFailure);
        }

        function cargarProductos() {
            function onSuccess(result) {
                if (result != null) {
                    var select = document.getElementById('producto');
                    for (i = 0; i < result.length; i++) {
                        var opt = document.createElement('OPTION');
                        opt.text = result[i].nombre;
                        opt.value = result[i].nroDeProducto;
                        select.appendChild(opt);
                    }
                    obtenerSolicitudesActivas();
                }
            }
            function onFailure(result) { }
            PageMethods.cargarProductos(onSuccess, onFailure);
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

        window.onload = function () {
            cargarSucursales();
            
        }

        function buscar() {
            function onSuccess(result) {
                if (result != null) {
                    document.getElementById('id').value = result.nroPedido;
                    document.getElementById('cantidad').value = result.cantidad;
                    document.getElementById('fecha').value = result.fecha;
                    document.getElementById('usuario').value = result.usuario.nombre;
                   
                    var prods = document.getElementById('producto');
                    for (i = 0; i < prods.length; i++) {
                        if (prods[i].value == result.producto.nroDeProducto) {
                            prods.selectedIndex = prods[i].index;
                        }
                    }

                    var sucs = document.getElementById('sucursal');
                    for (i = 0; i < sucs.length; i++) {
                        if (sucs[i].value == result.sucursal.nroSucursal) {
                            sucs.selectedIndex = sucs[i].index;
                        }
                    }
                    var sols = document.getElementById('solicitud');
                    for (i = 0; i < sols.length; i++) {
                        if (sols[i].value == result.solicitudDeStock.nroPedido) {
                            sols.selectedIndex = sols[i].index;
                        }
                    }
                }
            }
            function onFailure(result) { }
            PageMethods.buscar(document.getElementById('id').value, onSuccess, onFailure)
        }

        function obtenerSolicitudesActivas() {
            function onSuccess(result) {
                if (result != null) {
                    var select = document.getElementById('solicitud');
                    for (i = 0; i < result.length; i++) {
                        var opt = document.createElement('OPTION');
                        opt.text = result[i].nroPedido;
                        opt.value = result[i].nroPedido;
                        select.appendChild(opt);
                    }
                }
            }
            function onFailure(result) { }
            var nroSucursal = document.getElementById('sucursal').value;
            var nroProducto = document.getElementById('producto').value;
            PageMethods.obtenerSolicitudesActivas(nroSucursal, nroProducto, onSuccess, onFailure);
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
        <span id="spfecha">fecha:</span><input type="text" id="fecha" name="fecha"  />
        <p />
          <span id="spusuario">Nombre de usuario: </span><input disabled="disabled" type="text" id="usuario" name="usuario" />
        <p/>
         <span id="spcantidad">Cantidad: </span><input type="text" id="cantidad" name="cantidad" />
        <p/>
           <span id="spsucursal">Sucursal: </span><select id="sucursal"  name="sucursal" onchange="obtenerSolicitudesActivas()"></select>
        <p/>
         <span id="spproducto">Producto: </span><select id="producto" name="producto" onchange="obtenerSolicitudesActivas()" ></select>
        <p/>
         <span id="sppreciocompra">Precio de compra: </span><input type="text" id="preciocompra" name="preciocompra" />
        <p/>
           <span id="spnrosolicitud">Nro de solicitud de stock: </span><select id="solicitud" name="solicitud" ></select>
        <p/>
        <input type="button" id="eliminarBtn" value="Eliminar" onclick="setAccion(this.id)" />
        <input type="submit" id="modificarBtn" value="Guardar" onclick="setAccion(this.id)"/>
        <input type="button" id="crearBtn" value="Crear" onclick="crear()" />
         <input type="button" id="limpiarBtn" value="Limpiar" onclick="limpiarForm()"/>
        <input type="button" id="buscarBtn" value="Buscar" onclick="buscar()" />
        <p />
    </section>
</asp:Content>
