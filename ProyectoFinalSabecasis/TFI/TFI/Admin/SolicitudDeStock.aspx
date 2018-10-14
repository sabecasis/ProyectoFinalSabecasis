<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Admin/admin.Master" CodeBehind="SolicitudDeStock.aspx.vb" Inherits="TFI.SolicitudDeStock" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../static/bootstrap.min.css" rel="stylesheet" />
    <script src="../scripts/jquery.js"></script>
    <script src="../scripts/jquery-2.1.4.min.js"></script>
    <script src="../scripts/abmscripts.js"></script>
    <script src="../scripts/jquery-ui.min.js"></script>
    <link href="../static/jquery-ui.css" rel="stylesheet" />
    <script src="../scripts/bootstrap.min.js"></script>
    <link href="../static/font-awesome/css/font-awesome.min.css" rel="stylesheet" />
    <link href="../static/shop-homepage.css" rel="stylesheet" />
    <link href="../static/sidebar.css" rel="stylesheet" />
    <script>
        $(function () {
            $("#fecha").datepicker({
                dateFormat: "yy-mm-dd"
            });
        });

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
            }
            function onFailure(result) {
                alert(result._message);
            }
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
                }
            }
            function onFailure(result) {
                alert(result._message);
            }
            PageMethods.cargarProductos(onSuccess, onFailure);
        }

        function crear() {
            function onSuccess(result) {
                if (result != null) {
                    document.getElementById('id').value = result;
                }
            }
            function onFailure(result) {
                alert(result._message);
            }
            PageMethods.obtenerProximoId(onSuccess, onFailure);
        }

        window.onload = function () {
            cargarSucursales();
            cargarProductos();
        }

        function buscar() {
            function onSuccess(result) {
                if (result != null) {
                    document.getElementById('id').value = result.nroPedido;
                    document.getElementById('cantidad').value = result.cantidad;
                    document.getElementById('fecha').value = result.fecha;
                    document.getElementById('nombreusuario').value = result.usuario.nombre;
                    if (result.estaIngersado == true) {
                        document.getElementById('completado').checked = true;
                    } else {
                        document.getElementById('completado').checked = false;
                    }
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
                }
            }
            function onFailure(result) {
                alert(result._message);
            }
            PageMethods.buscar(document.getElementById('id').value, onSuccess, onFailure)
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
                <ul class="nav navbar-nav navbar-right">
                    <li>
                        <a href="#" class="dropdown-toggle" data-toggle="dropdown" id="aPerfil" runat="server"><b class="caret"></b></a>
                        <ul class="dropdown-menu">
                            <li>
                                <a href="/Cliente/CerrarSesion.aspx" id="alogout">Cerrar Sesión</a>
                            </li>
                              <li>
                                <a  href="#" data-target="#idioma" data-toggle="modal" id="acambiaridioma">Cambiar Idioma</a>
                            </li>
                        </ul>
                    </li>
                </ul>
            </div>
        </div>
        <div id="breadcrums" runat="server" class="breadcrums">
        </div>
        <div class="collapse navbar-collapse navbar-ex1-collapse">
            <ul class="nav navbar-nav side-nav" id="menuVertical">
            </ul>
        </div>
    </nav>
    <section id="contenedor" class="container">
        <input type="hidden" id="accion" name="accion" />
        <div class="row">
           
            <div class="mensaje-respuesta-contacto" id="LblMensaje" runat="server"></div>
        </div>
        <div class="row">
            <div class="col-lg-3"><span id="spid">Id:</span></div>
            <div class="col-lg-3"><input type="text" id="id" name="id" /></div>
        </div>
        <div class="row">
           <div class="col-lg-3"> <span id="spcantidad">Cantidad: </span></div>
          <div class="col-lg-3">  <input type="text" id="cantidad" name="cantidad" /></div>
        </div>
        <div class="row">
         <div class="col-lg-3">   <span id="spsucursal">Sucursal: </span></div>
          <div class="col-lg-3">  <select id="sucursal" name="sucursal"></select></div>
        </div>
        <div class="row">
          <div class="col-lg-3">  <span id="spproducto">Producto: </span></div>
         <div class="col-lg-3">   <select id="producto" name="producto"></select></div>
        </div>
        <div class="row">
          <div class="col-lg-3">  <span id="spfecha">Fecha</span></div>
           <div class="col-lg-3"><input type="text" id="fecha" name="fecha" /></div>
        </div>
        <div class="row">
           <div class="col-lg-3"> <span id="spcompletado">Completado:</span></div>
           <div class="col-lg-3"><input type="checkbox" id="completado" name="completado" /></div>
        </div>
        <div class="row">
            <div class="col-lg-3"><span id="spusuario">Nombre de usuario:</span></div>
            <div class="col-lg-3"><input type="text" disabled="disabled" id="nombreusuario" name="usuario" /></div>
        </div>
        <div class="row">
            <div class="col-lg-7"><input type="submit" id="descargarComprobanteBtn" value="Descargar Comprobante" onclick="setAccion(this.id)" /></div>
            <div class="col-lg-4"><input type="button" id="eliminarBtn" value="Eliminar" onclick="setAccion(this.id)" /></div>
           </div>
        <div class="row">
            <div class="col-lg-7"> <input type="submit" id="modificarBtn" value="Guardar" onclick="setAccion(this.id)" /></div>
             <div class="col-lg-4"><input type="button" id="crearBtn" value="Crear" onclick="crear()" /></div>
           </div>
            <div class="row">
             <div class="col-lg-7"><input type="button" id="limpiarBtn" value="Limpiar" onclick="limpiarForm()" /></div>
           <div class="col-lg-4"> <input type="button" id="buscarBtn" value="Buscar" onclick="buscar()" /></div>
        </div>
    </section>
    <div id="idioma" class="modal fade" role="dialog">
        <div id="Div2" class="modal-dialog modal-lg modal-content modal-dooba modal-idioma">
            <button type="button" class="btn btn-default btn-modal-close btn-circle pull-right" data-dismiss="modal"><i class="fa fa-times"></i></button>
            <div class="col-md-3"><span id="sidioma">Idioma: </span></div>
            <select id="idiomaSeleccionado" onchange="seleccionarIdioma();"></select>
        </div>
    </div>
</asp:Content>
