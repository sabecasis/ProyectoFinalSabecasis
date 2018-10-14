<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/ConsumidorFinal.Master" CodeBehind="ModuloDeCliente.aspx.vb" Inherits="TFI.ModuloDeCliente" %>
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
           
            obtenerProductos();    
        }

       function obtenerProductos(){
            function onSuccess(result){
                if (result != null && result.length > 0) {
                    var cont = document.getElementById('contenedor');
                    var tabla = document.createElement('TABLE');
                    tabla.className = 'table con-borde';
                    tabla.id = 'tablaproductos';
                    var tr = document.createElement('tr');
                    var td = document.createElement('th');
                    td.className = 'con-borde';
                    td.appendChild(document.createTextNode('Nro. de serie'));
                    var td2 = document.createElement('th');
                    td2.className = 'con-borde';
                    td2.appendChild(document.createTextNode('Producto'));
                    var td4 = document.createElement('th');
                    td4.className = 'con-borde';
                    td4.appendChild(document.createTextNode('Fecha finalización garantía'));
                    var td5 = document.createElement('th');
                    td5.className = 'con-borde';
                    td5.appendChild(document.createTextNode('Opción'));
                    tr.appendChild(td2);
                    tr.appendChild(td4);
                    tr.appendChild(td5);
                    tabla.appendChild(tr);
                    for (i = 0; i < result.length; i++) {
                        var tr = document.createElement('tr');
                        var td20 = document.createElement('td');
                        td20.className = 'con-borde';
                        td20.appendChild(document.createTextNode(result[i].nroDeSerie));
                        var td4 = document.createElement('td');
                        td4.className = 'con-borde';
                        td4.appendChild(document.createTextNode(result[i].producto.nombre));
                        var td6 = document.createElement('td');
                        td6.appendChild(document.createTextNode(result[i].garantia.fechaFin.toLocaleDateString()));
                        td6.className = 'con-borde';
                        var a = document.createElement('a');
                        a.id = result[i].nroDeSerie;
                        a.name = 'averdetalles';
                        a.href = "/Cliente/DetalleDeProducto.aspx?id=" + result[i].nroDeSerie;
                        a.appendChild(document.createTextNode('Ver detalles'));
                        var td7 = document.createElement('td');
                        td7.className = 'con-borde';
                        td7.appendChild(a);
                        tr.appendChild(td4);
                        tr.appendChild(td6);
                        tr.appendChild(td7);
                        tabla.appendChild(tr)
                    }
                    cont.appendChild(tabla);
                }
            }
            function onFailure(result) {
                alert(result._message);
            }
            PageMethods.obtenerProductosAdquiridos(document.getElementById('ContentPlaceHolder1_idUsuario').value, onSuccess, onFailure);
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
                <li >
                    <a href="/Cliente/Perfil.aspx">Perfil</a>
                </li>
                <li class="active">
                    <a href="#">Mis compras</a>
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
    <section id="contenedor">
        <input type="hidden" id="idUsuario" runat="server" />   
        <asp:Table ID="tabla" runat="server"></asp:Table>
        <input type="hidden" id="referrer" runat="server" />
    </section>

     <div id="idioma" class="modal fade" role="dialog">
        <div id="Div2" class="modal-dialog modal-lg modal-content modal-dooba modal-idioma">
            <button type="button" class="btn btn-default btn-modal-close btn-circle pull-right" data-dismiss="modal"><i class="fa fa-times"></i></button>
            <div class="col-md-3"><span id="sidioma">Idioma: </span></div>
            <select id="idiomaSeleccionado" onchange="seleccionarIdioma();"></select>
        </div>
    </div> 
</asp:Content>
