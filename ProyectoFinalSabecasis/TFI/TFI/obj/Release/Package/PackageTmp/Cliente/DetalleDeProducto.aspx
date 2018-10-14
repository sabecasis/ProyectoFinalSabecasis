<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/ConsumidorFinal.Master" CodeBehind="DetalleDeProducto.aspx.vb" Inherits="TFI.DetalleDeProducto" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        window.onload = function () {
            obtenerInformacionDePRoducto();
        }

        function obteneEgresoDeStock() {
            function onSuccess(result) {
                if (result != null) {
                    var cont = document.getElementById('detallesdeenvio');
                    var tabla = document.createElement('TABLE');
                    cont.appendChild(tabla);
                    var tr = document.createElement('tr');
                    var td1 = document.createElement('th');
                    td1.appendChild(document.createTextNode('Nro. de envío'));
                    var td2 = document.createElement('th');
                    td2.appendChild(document.createTextNode('estado'));
                    var td3 = document.createElement('th');
                    td3.appendChild(document.createTextNode('Monto'));
                    var td4 = document.createElement('th');
                    td4.appendChild(document.createTextNode('Tipo de envío'));
                   
                    tr.appendChild(td1);
                    tr.appendChild(td2);
                    tr.appendChild(td3);
                    tr.appendChild(td4);
                    

                    tabla.appendChild(tr);
                    var tr = document.createElement('tr');
                    var td2 = document.createElement('td');
                    td2.appendChild(document.createTextNode(result.nroEnvio));
                    var td4 = document.createElement('td');
                    td4.appendChild(document.createTextNode(result.estado.estado));
                    var td5 = document.createElement('td');
                    td5.appendChild(document.createTextNode(result.monto));
                    var td6 = document.createElement('td');
                    td6.appendChild(document.createTextNode(result.tipo.tipo));
                   
                    tr.appendChild(td2);
                    tr.appendChild(td4);
                    tr.appendChild(td5);
                    tr.appendChild(td6);
                    

                    tabla.appendChild(tr)

                }
            }
            function onFailure(result) {

            }
            PageMethods.obtenerEnvioDeProducto(nroDeSerie, onSuccess, onFailure);
        }

        function obtenerInformacionDeFacturacion() {
            function onSuccess(result) {
                if (result != null) {
                    var cont = document.getElementById('detallesdefacturacion');
                    var tabla = document.createElement('TABLE');
                    cont.appendChild(tabla);
                    var tr = document.createElement('tr');
                    var td1 = document.createElement('th');
                    td1.appendChild(document.createTextNode('Nro. de factura'));
                    var td2 = document.createElement('th');
                    td2.appendChild(document.createTextNode('Fecha de cobro'));
                    var td3 = document.createElement('th');
                    td3.appendChild(document.createTextNode('Monto'));
                    var td4 = document.createElement('th');
                    td4.appendChild(document.createTextNode('Nro. de orden'));
                   
                    tr.appendChild(td1);
                    tr.appendChild(td2);
                    tr.appendChild(td3);
                    tr.appendChild(td4);


                    tabla.appendChild(tr);
                    var tr = document.createElement('tr');
                    var td2 = document.createElement('td');
                    td2.appendChild(document.createTextNode(result.nroFactura));
                    var td4 = document.createElement('td');
                    td4.appendChild(document.createTextNode(result.fechaDeCobro.toLocaleDateString()));
                    var td5 = document.createElement('td');
                    td5.appendChild(document.createTextNode(result.montoDeCobro));
                    var td6 = document.createElement('td');
                    td6.appendChild(document.createTextNode(result.orden.nroDeOrden));

                    nroDeFactura = result.nroFactura;
                    tr.appendChild(td2);
                    tr.appendChild(td4);
                    tr.appendChild(td5);
                    tr.appendChild(td6);
                    tabla.appendChild(tr)
                }
            }
            function onFailure(result) { }
            PageMethods.obtenerInformacionDeFactura(nroDeSerie, onSuccess, onFailure);
        }

        function obtenerInformacionDePRoducto() {
            function onSuccess(result) {
                if (result != null) {
                    var cont = document.getElementById('detallesdeproducto');
                    var tabla = document.createElement('TABLE');
                    cont.appendChild(tabla);
                    var tr = document.createElement('tr');
                    var td1 = document.createElement('th');
                    td1.appendChild(document.createTextNode('Nro. de serie'));
                    var td2 = document.createElement('th');
                    td2.appendChild(document.createTextNode('Producto'));
                    var td3 = document.createElement('th');
                    td3.appendChild(document.createTextNode('Nro. de garantía'));
                    var td4 = document.createElement('th');
                    td4.appendChild(document.createTextNode('Fecha finalización garantía'));
                    var td5 = document.createElement('th');
                    td5.appendChild(document.createTextNode('Estado de producto'));

                    tr.appendChild(td1);
                    tr.appendChild(td2);
                    tr.appendChild(td3);
                    tr.appendChild(td4);
                    tr.appendChild(td5);

                    tabla.appendChild(tr);
                    var tr = document.createElement('tr');
                        var td2 = document.createElement('td');
                        td2.appendChild(document.createTextNode(result.nroDeSerie));
                        var td4 = document.createElement('td');
                        td4.appendChild(document.createTextNode(result.producto.nombre));
                        var td5 = document.createElement('td');
                        td5.appendChild(document.createTextNode(result.garantia.nroGarantia));
                        var td6 = document.createElement('td');
                        td6.appendChild(document.createTextNode(result.garantia.fechaFin.toLocaleDateString()));
                        var td7 = document.createElement('td');
                        if (result.estado.id == 4) {
                            td7.appendChild(document.createTextNode(result.estado.estado));
                            document.getElementById('cambiarBtn').disabled = 'disabled';
                        } else {
                            td7.appendChild(document.createTextNode(''));
                        }
                        tr.appendChild(td2);
                        tr.appendChild(td4);
                        tr.appendChild(td5);
                        tr.appendChild(td6);
                        tr.appendChild(td7);
                        
                        tabla.appendChild(tr)
                        obteneEgresoDeStock();
                        obtenerInformacionDeFacturacion();
                    
                }
            }
            function onFailure(result) {

            }
            var param = window.location.search.replace("?", "");
            if (param != null) {
                nroDeSerie = param.split("=")[1];
                PageMethods.obtenerInformacionDeProducto(nroDeSerie, onSuccess, onFailure);
            }
        }
        function cambiarProducto() {
            function onSuccess(result) {
                if (result != null) {
                    var cont = document.getElementById('detallesdefacturacion');
                    cont.appendChild(document.createTextNode('Nro. De Nota de crédito ' + result));
                    document.getElementById('cambiarBtn').disabled = 'disabled';
                }
            }
            function onFailure(result) { }
            PageMethods.cambiarProducto(nroDeSerie, nroDeFactura, onSuccess, onFailure);
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div id="background">
        <img src="../static/fondo11.jpg" />
    </div>
    <section id="contenedor">
       <div id="detallesdeproducto"></div>
        <div id="detallesdeenvio"></div>
        <div id="detallesdefacturacion"></div>
        <input type="button" id="cambiarBtn" value="Cambiar producto" onclick="cambiarProducto()"/>
    </section>   
</asp:Content>
