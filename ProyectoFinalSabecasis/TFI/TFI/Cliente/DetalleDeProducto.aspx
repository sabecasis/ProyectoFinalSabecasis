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
                    tabla.className = 'table con-borde';
                    cont.appendChild(tabla);
                    var tr = document.createElement('tr');
                    var td1 = document.createElement('th');
                    td1.className = 'con-borde';
                    td1.appendChild(document.createTextNode('Nro. de envío'));
                    var td2 = document.createElement('th');
                    td2.className = 'con-borde';
                    td2.appendChild(document.createTextNode('estado'));
                    var td3 = document.createElement('th');
                    td3.className = 'con-borde';
                    td3.appendChild(document.createTextNode('Monto'));
                    var td4 = document.createElement('th');
                    td4.className = 'con-borde';
                    td4.appendChild(document.createTextNode('Tipo de envío'));
                   
                    tr.appendChild(td1);
                    tr.appendChild(td2);
                    tr.appendChild(td3);
                    tr.appendChild(td4);
                    

                    tabla.appendChild(tr);
                    var tr = document.createElement('tr');
                    var td2 = document.createElement('td');
                    td2.className = 'con-borde';
                    td2.appendChild(document.createTextNode(result.nroEnvio));
                    var td4 = document.createElement('td');
                    td4.className = 'con-borde';
                    td4.appendChild(document.createTextNode(result.estado.estado));
                    var td5 = document.createElement('td');
                    td5.className = 'con-borde';
                    td5.appendChild(document.createTextNode(result.monto));
                    var td6 = document.createElement('td');
                    td6.className = 'con-borde';
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

        function submit() {
            document.getElementById('nroFactura').value = this.id;
            document.forms['form1'].submit();
        }

        function obtenerInformacionDeFacturacion() {
            function onSuccess(result) {
                if (result != null) {
                    var cont = document.getElementById('detallesdefacturacion');
                    var tabla = document.createElement('TABLE');
                    tabla.className = 'table con-borde';
                    cont.appendChild(tabla);
                    var tr = document.createElement('tr');
                    var td1 = document.createElement('th');
                    td1.className = 'con-borde';
                    td1.appendChild(document.createTextNode('Nro. de factura'));
                    var td2 = document.createElement('th');
                    td2.className = 'con-borde';
                    td2.appendChild(document.createTextNode('Fecha de cobro'));
                    var td3 = document.createElement('th');
                    td3.className = 'con-borde';
                    td3.appendChild(document.createTextNode('Monto'));
                    var td4 = document.createElement('th');
                    td4.className = 'con-borde';
                    td4.appendChild(document.createTextNode('Nro. de orden'));
                    var td5 = document.createElement('th');
                    td5.className = 'con-borde';
                    td5.appendChild(document.createTextNode('Link descarga'));
                   
                    tr.appendChild(td1);
                    tr.appendChild(td2);
                    tr.appendChild(td3);
                    tr.appendChild(td4);
                    tr.appendChild(td5);

                    tabla.appendChild(tr);
                    var tr = document.createElement('tr');
                    var td2 = document.createElement('td');
                    td2.className = 'con-borde';
                    td2.appendChild(document.createTextNode(result.nroFactura));
                    var td4 = document.createElement('td');
                    td4.className = 'con-borde';
                    td4.appendChild(document.createTextNode(result.fechaDeCobro.toLocaleDateString()));
                    var td5 = document.createElement('td');
                    td5.className = 'con-borde';
                    td5.appendChild(document.createTextNode(result.montoDeCobro));
                    var td6 = document.createElement('td');
                    td6.className = 'con-borde';
                    td6.appendChild(document.createTextNode(result.orden.nroDeOrden));
                    var td7 = document.createElement('td');
                    td7.className = 'con-borde';
                    var a = document.createElement('a');
                    a.id = nroDeSerie
                    a.onclick = submit
                    a.appendChild(document.createTextNode("download"))
                    td7.appendChild(a);

                    nroDeFactura = result.nroFactura;
                    tr.appendChild(td2);
                    tr.appendChild(td4);
                    tr.appendChild(td5);
                    tr.appendChild(td6);
                    tr.appendChild(td7);
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
                    tabla.className = 'table con-borde';
                    cont.appendChild(tabla);
                    var tr = document.createElement('tr');
                    var td1 = document.createElement('th');
                    td1.className = 'con-borde';
                    td1.appendChild(document.createTextNode('Nro. de serie'));
                    var td2 = document.createElement('th');
                    td2.className = 'con-borde';
                    td2.appendChild(document.createTextNode('Producto'));
                    var td3 = document.createElement('th');
                    td3.className = 'con-borde';
                    td3.appendChild(document.createTextNode('Nro. de garantía'));
                    var td4 = document.createElement('th');
                    td4.className = 'con-borde';
                    td4.appendChild(document.createTextNode('Fecha finalización garantía'));
                    var td5 = document.createElement('th');
                    td5.className = 'con-borde';
                    td5.appendChild(document.createTextNode('Estado de producto'));

                    tr.appendChild(td1);
                    tr.appendChild(td2);
                    tr.appendChild(td3);
                    tr.appendChild(td4);
                    tr.appendChild(td5);

                    tabla.appendChild(tr);
                    var tr = document.createElement('tr');
                    var td2 = document.createElement('td');
                    td2.className = 'con-borde';
                        td2.appendChild(document.createTextNode(result.nroDeSerie));
                        var td4 = document.createElement('td');
                        td4.className = 'con-borde';
                        td4.appendChild(document.createTextNode(result.producto.nombre));
                        var td5 = document.createElement('td');
                        td5.className = 'con-borde';
                        td5.appendChild(document.createTextNode(result.garantia.nroGarantia));
                        var td6 = document.createElement('td');
                        td6.className = 'con-borde';
                        td6.appendChild(document.createTextNode(result.garantia.fechaFin.toLocaleDateString()));
                        var td7 = document.createElement('td');
                        if (result.estado.id == 4) {
                            td7.appendChild(document.createTextNode(result.estado.estado));
                            document.getElementById('cambiarBtn').disabled = 'disabled';
                        } else {
                            td7.appendChild(document.createTextNode(result.estado.estado));
                        }
                        td7.className = 'con-borde';
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
                    cont.appendChild(document.createTextNode(result));
                    document.getElementById('cambiarBtn').disabled = 'disabled';
                }
            }
            function onFailure(result) { }
            PageMethods.cambiarProducto(nroDeSerie, nroDeFactura, onSuccess, onFailure);
        }

    </script>
    <link href="../static/bootstrap.min.css" rel="stylesheet" />
    <script src="../scripts/jquery.js"></script>
    <script src="../scripts/bootstrap.min.js"></script>
    <link href="../static/font-awesome/css/font-awesome.min.css" rel="stylesheet" />
    <link href="../static/shop-homepage.css" rel="stylesheet" />
    <link href="../static/sidebar-front.css" rel="stylesheet" />
     <link href="../static/font-awesome/css/bootstrap.min.css" rel="stylesheet" />
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
                <li class="active">
                    <a href="/Cliente/ModuloDeCliente.aspx">Mis compras</a>
                </li>
                <li>
                    <a href="/Cliente/EstadoDeCuenta.aspx">Estado de cuenta</a>
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
    <section class="contenedor-soft">
         <div class="row">
            <div class="mensaje-respuesta-contacto" id="LblMensaje" runat="server"></div>
        </div>
        <input type="hidden" name="nroFactura" id="nroFactura" />
       <div id="detallesdeproducto"></div>
        <div id="detallesdeenvio"></div>
        <div id="detallesdefacturacion"></div>
         <input type="hidden" id="referrer" runat="server" />
        <input type="button" id="cambiarBtn" value="Cambiar producto" onclick="cambiarProducto()"/>
    </section> 
    <div id="idioma" class="modal fade" role="dialog">
        <div id="Div2" class="modal-dialog modal-lg modal-content modal-dooba modal-idioma">
            <button type="button" class="btn btn-default btn-modal-close btn-circle pull-right" data-dismiss="modal"><i class="fa fa-times"></i></button>
            <div class="col-md-3"><span id="sidioma">Idioma: </span></div>
            <select id="idiomaSeleccionado" onchange="seleccionarIdioma();"></select>
        </div>
    </div>  
</asp:Content>
