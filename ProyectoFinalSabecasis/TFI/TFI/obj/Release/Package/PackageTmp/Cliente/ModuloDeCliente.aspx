<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/ConsumidorFinal.Master" CodeBehind="ModuloDeCliente.aspx.vb" Inherits="TFI.ModuloDeCliente" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        window.onload = function () {
            obtenerProductos();    
        }

       function obtenerProductos(){
            function onSuccess(result){
                if (result != null && result.length > 0) {
                    var cont = document.getElementById('contenedor');
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
                    td5.appendChild(document.createTextNode('Opción'));
                    tr.appendChild(td1);
                    tr.appendChild(td2);
                    tr.appendChild(td3);
                    tr.appendChild(td4);
                    tr.appendChild(td5);
                    tabla.appendChild(tr);
                    for (i = 0; i < result.length; i++) {
                        var tr = document.createElement('tr');
                        var td2 = document.createElement('td');
                        td2.appendChild(document.createTextNode(result[i].nroDeSerie));
                        var td4 = document.createElement('td');
                        td4.appendChild(document.createTextNode(result[i].producto.nombre));
                        var td5 = document.createElement('td');
                        td5.appendChild(document.createTextNode(result[i].garantia.nroGarantia));
                        var td6 = document.createElement('td');
                        td6.appendChild(document.createTextNode(result[i].garantia.fechaFin.toLocaleDateString()));
                        var a = document.createElement('a');
                        a.id = result[i].nroDeSerie;
                        a.name = 'averdetalles';
                        a.href = "/Cliente/DetalleDeProducto.aspx?id=" + result[i].nroDeSerie;
                        a.appendChild(document.createTextNode('Ver detalles'));
                        var td7 = document.createElement('td');
                        td7.appendChild(a);
                        tr.appendChild(td2);
                        tr.appendChild(td4);
                        tr.appendChild(td5);
                        tr.appendChild(td6);
                        tr.appendChild(td7);
                        tabla.appendChild(tr)
                    }
                }
            }
            function onFailure(result) {
                alert(result);
            }
            PageMethods.obtenerProductosAdquiridos(document.getElementById('ContentPlaceHolder1_idUsuario').value, onSuccess, onFailure);
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="background">
         <img src="../static/fondo%2010.jpg" />
    </div> 
    <aside id="lateral">
        <div id="menuVertical"></div>
    </aside>
    <section id="contenedor">
    <input type="hidden" id="idUsuario" runat="server" />   
        <asp:Table ID="tabla" runat="server"></asp:Table>
    </section>
</asp:Content>
