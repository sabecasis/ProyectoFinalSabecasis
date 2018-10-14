<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/ConsumidorFinal.Master" CodeBehind="EstadoDeCuenta.aspx.vb" Inherits="TFI.EstadoDeCuenta" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
       <link href="../static/bootstrap.min.css" rel="stylesheet" />
    <script src="../scripts/jquery.js"></script>
    <script src="../scripts/bootstrap.min.js"></script>
    <link href="../static/font-awesome/css/font-awesome.min.css" rel="stylesheet" />
    <link href="../static/shop-homepage.css" rel="stylesheet" />
    <link href="../static/sidebar-front.css" rel="stylesheet" />
     <link href="../static/font-awesome/css/bootstrap.min.css" rel="stylesheet" />
     <script>
        window.onload = function () {
           
            obtenerEstadoDeCuenta();
        }
        function obtenerEstadoDeCuenta() {
            function onSuccess(result) {
                if (result != null) {
                    var cont = document.getElementById('contenedor');
                    var sp1 = document.createElement('h2');
                    sp1.appendChild(document.createTextNode('Totales'))
                    cont.appendChild(sp1);
                    var tabla1 = document.createElement('TABLE');
                    tabla1.className = 'table con-borde';
                    var tr = document.createElement('tr');
                    var td1 = document.createElement('th');
                    td1.className = 'con-borde';
                    td1.appendChild(document.createTextNode('Total en débitos'));
                    var td2 = document.createElement('th');
                    td2.className = 'con-borde';
                    td2.appendChild(document.createTextNode('Total en créditos'));
                    var td3 = document.createElement('th');
                    td3.className = 'con-borde';
                    td3.appendChild(document.createTextNode('Total facturado'));
                    tr.appendChild(td1);
                    tr.appendChild(td2);
                    tr.appendChild(td3);
                    tabla1.appendChild(tr);
                    cont.appendChild(tabla1);

                    var tr = document.createElement('tr');
                    var td2 = document.createElement('td');
                    td2.className = 'con-borde';
                    td2.appendChild(document.createTextNode(result.totalEnDebito));
                    var td4 = document.createElement('td');
                    td4.className = 'con-borde';
                    td4.appendChild(document.createTextNode(result.totalEnCredito));
                    var td6 = document.createElement('td');
                    td6.className = 'con-borde';
                    td6.appendChild(document.createTextNode(result.totalFacturado));
                    tr.appendChild(td2);
                    tr.appendChild(td4);
                    tr.appendChild(td6);
                    tabla1.appendChild(tr);

                    if (result.notasDeCredito != null && result.notasDeCredito.length > 0) {
                        var sp1 = document.createElement('h2');
                        sp1.appendChild(document.createTextNode('Notas de crédito'))
                        cont.appendChild(sp1);
                        var tabla = document.createElement('TABLE');
                        tabla.className="table con-borde"
                        cont.appendChild(tabla);
                        var tr = document.createElement('tr');
                        var td1 = document.createElement('th');
                        td1.className = 'con-borde';
                        td1.appendChild(document.createTextNode('Nro. de nota de crédito'));
                        var td2 = document.createElement('th');
                        td2.className = 'con-borde';
                        td2.appendChild(document.createTextNode('Descripción'));
                        var td3 = document.createElement('th');
                        td3.className = 'con-borde';
                        td3.appendChild(document.createTextNode('Nro. Factura'));
                        var td4 = document.createElement('th');
                        td4.className = 'con-borde';
                        td4.appendChild(document.createTextNode('Monto'));
                        var td5 = document.createElement('th');
                        td5.className = 'con-borde';
                        td5.appendChild(document.createTextNode('Activa'));
                        tr.appendChild(td1);
                        tr.appendChild(td2);
                        tr.appendChild(td3);
                        tr.appendChild(td4);
                        tr.appendChild(td5);

                        tabla.appendChild(tr);
                        for (i = 0; i < result.notasDeCredito.length; i++) {
                            
                            var tr = document.createElement('tr');
                            var td2 = document.createElement('td');
                            td2.className = 'con-borde';
                            td2.appendChild(document.createTextNode(result.notasDeCredito[i].nroNotaDeCredito));
                            var td4 = document.createElement('td');
                            td4.className = 'con-borde';
                            td4.appendChild(document.createTextNode(result.notasDeCredito[i].descripcion));
                            var td5 = document.createElement('td');
                            td5.className = 'con-borde';
                            td5.appendChild(document.createTextNode(result.notasDeCredito[i].factura.nroFactura));
                            var td6 = document.createElement('td');
                            td6.className = 'con-borde';
                            td6.appendChild(document.createTextNode(result.notasDeCredito[i].monto));
                            var td7 = document.createElement('td');
                            td7.className = 'con-borde';
                            td7.appendChild(document.createTextNode((result.notasDeCredito[i].estaActiva)?'Si':'No'));
                            tr.appendChild(td2);
                            tr.appendChild(td4);
                            tr.appendChild(td5);
                            tr.appendChild(td6);
                            tr.appendChild(td7);
                            tabla.appendChild(tr)
                        }
                    }
                }

                if (result.facturas != null && result.facturas.length > 0) {
                    var sp1 = document.createElement('h2');
                    sp1.appendChild(document.createTextNode('Facturas'))
                    cont.appendChild(sp1);
                    var tabla = document.createElement('TABLE');
                    tabla.className = "table con-borde"
                    cont.appendChild(tabla);
                    var tr = document.createElement('tr');
                    var td1 = document.createElement('th');
                    td1.className = 'con-borde';
                    td1.appendChild(document.createTextNode('Nro. de Factura'));
                    var td2 = document.createElement('th');
                    td2.className = 'con-borde';
                    td2.appendChild(document.createTextNode('Nro de nota de crédito aplicada'));
                    var td3 = document.createElement('th');
                    td3.className = 'con-borde';
                    td3.appendChild(document.createTextNode('Monto de venta'));
                    var td4 = document.createElement('th');
                    td4.className = 'con-borde';
                    td4.appendChild(document.createTextNode('Monto de cobro'));
                    var td5 = document.createElement('th');
                    td5.className = 'con-borde';
                    td5.appendChild(document.createTextNode('Fecha de cobro'));
                    tr.appendChild(td1);
                    tr.appendChild(td2);
                    tr.appendChild(td3);
                    tr.appendChild(td4);
                    tr.appendChild(td5);
                    tabla.appendChild(tr);

                    for (i = 0; i < result.facturas.length; i++) {

                        var tr = document.createElement('tr');
                        var td2 = document.createElement('td');
                        td2.className = 'con-borde';
                        td2.appendChild(document.createTextNode(result.facturas[i].nroFactura));
                        var td4 = document.createElement('td');
                        td4.className = 'con-borde';
                        td4.appendChild(document.createTextNode((result.facturas[i].notaDeCreditoAplicada!=null)?result.facturas[i].notaDeCreditoAplicada.nroNotaDeCredito:''));
                        var td5 = document.createElement('td');
                        td5.className = 'con-borde';
                        td5.appendChild(document.createTextNode(result.facturas[i].montoDeVenta));
                        var td6 = document.createElement('td');
                        td6.className = 'con-borde';
                        td6.appendChild(document.createTextNode(result.facturas[i].montoDeCobro));
                        var td7 = document.createElement('td');
                        td7.className = 'con-borde';
                        td7.appendChild(document.createTextNode(result.facturas[i].fechaDeCobro.toLocaleDateString()));
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
                alert(result._message);
            }
            nombreUsuario = document.getElementById('ContentPlaceHolder1_nombreUsuario').value;
            PageMethods.obtenerEstadoDeCuenta(nombreUsuario, onSuccess, onFailure);
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <nav class="navbar navbar-inverse navbar-fixed-top" role="navigation">
        <div class="container">

            <div class="navbar-header page-scroll">
                <a class="navbar-brand" href="/Cliente/Inicio.aspx">
                    <img class="dooba-img" src="../static/logotipo-trans.png" id="logo" /></a>
            </div>

            <div class="collapse navbar-collapse">
                <ul class="nav navbar-nav navbar-right" id="menuHorizontal">
                    <li class="hidden">
                        <a href="#page-top"></a>
                    </li>
                    <li>
                        <a href="/Cliente/IniciarSesion.aspx" id="alogin">Iniciar sesión</a>
                    </li>
                    <li>
                        <a href="/Cliente/Inicio.aspx">Inicio</a>
                    </li>
                    <li>
                        <a href="#" data-target="#idioma" data-toggle="modal" id="acambiaridioma">Cambiar idioma</a>
                    </li>
                    <li>
                        <a href="/Cliente/Catalogo.aspx" id="acatalogo">Catálogo</a>
                    </li>
                    <li>
                        <a href="/Cliente/Ayuda.aspx" id="aayuda">Ayuda</a>
                    </li>
                     <li>
                        <a href="/Cliente/NovedadesPublicas.aspx" id="aNovedadesPublicas">Novedades</a>
                    </li>
                </ul>
                 <div class="enlinea espaciado-izquierdo-breve espaciado-suerior" style="visibility:hidden"  id="user_tag"><i class="glyphicon glyphicon-user logo-usuario"></i><a href="/Cliente/Perfil.aspx" id="aPerfil" runat="server" class="usuario espaciado-izquierdo-breve"></a> </div>
            </div>
        </div>
        <div id="breadcrums" runat="server" class="breadcrums">
        </div>
        <div class="collapse navbar-collapse navbar-ex1-collapse">
            <ul class="nav navbar-nav side-nav">
                <li>
                    <a href="/Cliente/Perfil.aspx">Perfil</a>
                </li>
                <li>
                    <a href="/Cliente/ModuloDeCliente.aspx">Mis compras</a>
                </li>
                <li  class="active">
                    <a href="#">Estado de cuenta</a>
                </li>
                <li>
                    <a href="/Cliente/RecuperarPassword.aspx">Recuperar contraseña</a>
                </li>
                <li>
                    <a href="/Cliente/ChatDeSoporteAlCliente.aspx">Soporte al cliente</a>
                </li>
                <li>
                    <a href="/Cliente/Novedades.aspx">Subscripción a newsletter</a>
                </li>
            </ul>
        </div>
    </nav>
    <section id="contenedor">
        <input type="hidden" id="referrer" runat="server" />
        <input type="hidden" runat="server" id="nombreUsuario" />
    </section>

     <div id="idioma" class="modal fade" role="dialog">
        <div id="Div2" class="modal-dialog modal-lg modal-content modal-dooba modal-idioma">
            <button type="button" class="btn btn-default btn-modal-close btn-circle pull-right" data-dismiss="modal"><i class="fa fa-times"></i></button>
            <div class="col-md-3"><span id="sidioma">Idioma: </span></div>
            <select id="idiomaSeleccionado" onchange="seleccionarIdioma();"></select>
        </div>
    </div>
</asp:Content>
