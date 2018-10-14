<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/ConsumidorFinal.Master" CodeBehind="EstadoDeCuenta.aspx.vb" Inherits="TFI.EstadoDeCuenta" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        window.onload = function () {
            obtenerEstadoDeCuenta();
        }
        function obtenerEstadoDeCuenta() {
            function onSuccess(result) {
                if (result != null) {
                    var cont = document.getElementById('contenedor');
                    var tabla1 = document.createElement('TABLE');
                    var tr = document.createElement('tr');
                    var td1 = document.createElement('th');
                    td1.appendChild(document.createTextNode('Total en débitos'));
                    var td2 = document.createElement('th');
                    td2.appendChild(document.createTextNode('Total en créditos'));
                    var td3 = document.createElement('th');
                    td3.appendChild(document.createTextNode('Total facturado'));
                    tr.appendChild(td1);
                    tr.appendChild(td2);
                    tr.appendChild(td3);
                    tabla1.appendChild(tr);
                    cont.appendChild(tabla1);

                    var tr = document.createElement('tr');
                    var td2 = document.createElement('td');
                    td2.appendChild(document.createTextNode(result.totalEnDebito));
                    var td4 = document.createElement('td');
                    td4.appendChild(document.createTextNode(result.totalEnCredito));
                    var td6 = document.createElement('td');
                    td6.appendChild(document.createTextNode(result.totalFacturado));
                    tr.appendChild(td2);
                    tr.appendChild(td4);
                    tr.appendChild(td6);
                    tabla1.appendChild(tr);

                    var tabla = document.createElement('TABLE');
                    cont.appendChild(tabla);
                    for (i = 0; i < result.notasDeCredito.length; i++) {
                        cont.appendChild(tabla);
                        var tr = document.createElement('tr');
                        var td1 = document.createElement('th');
                        td1.appendChild(document.createTextNode('Nro. de nota de crédito'));
                        var td2 = document.createElement('th');
                        td2.appendChild(document.createTextNode('descripcion'));
                        var td3 = document.createElement('th');
                        td3.appendChild(document.createTextNode('Nro. Factura'));
                        var td4 = document.createElement('th');
                        td4.appendChild(document.createTextNode('Monto'));
                        tr.appendChild(td1);
                        tr.appendChild(td2);
                        tr.appendChild(td3);
                        tr.appendChild(td4);

                        tabla.appendChild(tr);
                        var tr = document.createElement('tr');
                        var td2 = document.createElement('td');
                        td2.appendChild(document.createTextNode(result.notasDeCredito[i].nroNotaDeCredito));
                        var td4 = document.createElement('td');
                        td4.appendChild(document.createTextNode(result.notasDeCredito[i].descripcion));
                        var td5 = document.createElement('td');
                        td5.appendChild(document.createTextNode(result.notasDeCredito[i].factura.nroFactura));
                        var td6 = document.createElement('td');
                        td6.appendChild(document.createTextNode(result.notasDeCredito[i].monto));
                        tr.appendChild(td2);
                        tr.appendChild(td4);
                        tr.appendChild(td5);
                        tr.appendChild(td6);
                        tabla.appendChild(tr)
                    }
                }
            }
            function onFailure(result) { }
            idUsuario = document.getElementById('ContentPlaceHolder1_idUsuario').value;
            PageMethods.obtenerEstadoDeCuenta(idUsuario, onSuccess, onFailure);
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div id="background">
         <img src="../static/fondo%2012.png" />
    </div> 
    <aside id="lateral">
        <div id="menuVertical"></div>
    </aside>
    <section id="contenedor">
        <input type="hidden" runat="server" id="idUsuario" />
    </section>
</asp:Content>
