<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Admin/admin.Master" CodeBehind="EgresoDeStock.aspx.vb" Inherits="TFI.EgresoDeStock" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <link href="../static/bootstrap.min.css" rel="stylesheet" />
    <script src="../scripts/jquery.js"></script>
    <script src="../scripts/bootstrap.min.js"></script>
    <link href="../static/font-awesome/css/font-awesome.min.css" rel="stylesheet" />
    <link href="../static/shop-homepage.css" rel="stylesheet" />
    <link href="../static/sidebar.css" rel="stylesheet" />
     <script src="../scripts/jquery.js"></script>
    <script src="../scripts/jquery-2.1.4.min.js"></script>
    <script src="../scripts/abmscripts.js"></script>
    <script src="../scripts/jquery-ui.min.js"></script>
    <link href="../static/jquery-ui.css" rel="stylesheet" />
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
            obtenerTodosLosEstados();
            obtenerEgresoPorParametroId();
        }

        function obtenerTodosLosEstados() {
            function onSuccess(result) {
                if (result != null) {
                    var select = document.getElementById('estado');
                    for (i = 0; i < result.length; i++) {
                        var opt = document.createElement('OPTION');
                        opt.text = result[i].estado;
                        opt.value = result[i].id;
                        select.appendChild(opt);
                    }
                }
            }
            function onFailure() {
                alert(result._message);
            }
            PageMethods.obtenerEstados(onSuccess, onFailure);
        }

        function obtenerEgresoPorParametroId() {
            var loc = document.location.href;
            var getString = loc.split('?');
            if (getString.length > 1) {
                var nombreParametro = getString[1].split('=')[0];
                if (nombreParametro == 'nroEgreso') {
                    document.getElementById('id').value = getString[1].split('=')[1];
                    buscar();
                }
            }

        }

        function buscar() {
            function onSuccess(result) {
                if (result != null) {
                    document.getElementById('id').value = result.nroEgreso;
                   
                    document.getElementById('fecha').value = result.fecha;
                    document.getElementById('nombreusuario').value = result.usuario.nombre;
                    document.getElementById('motivo').value = result.motivo;
                    document.getElementById('estado').value = result.estado.id;

                    
                    var sucs = document.getElementById('sucursal');
                    for (i = 0; i < sucs.length; i++) {
                        if (sucs[i].value == result.sucursal.nroSucursal) {
                            sucs.selectedIndex = sucs[i].index;
                        }
                    }

                    if (result.productosEspecificosEnStock != null) {
                        document.getElementById('cantidad').value = result.productosEspecificosEnStock.length;
                        var cont = document.getElementById('instancias');
                        cont.innerHTML = '';
                        var tabla = document.createElement('TABLE');
                        tabla.className = 'table';
                        cont.appendChild(tabla);
                        for (i = 0; i < result.productosEspecificosEnStock.length; i++) {
                            var tr = document.createElement('TR');
                            var td = document.createElement('TD');
                            td.className = 'con-borde';
                            td.appendChild(document.createTextNode(result.productosEspecificosEnStock[i].nroDeSerie));
                            var td2 = document.createElement('TD');
                            var input = document.createElement('INPUT');
                            input.type = 'TEXT';
                            input.name = 'productosEspecificos';
                            input.hidden='hidden'
                            input.value = result.productosEspecificosEnStock[i].nroDeSerie;
                            td2.appendChild(input);
                            tr.appendChild(td);
                            tr.appendChild(td2);
                            tabla.appendChild(tr);
                        }
                    }
                
                }
                }
            
            function onFailure(result) {
                alert(result._message);
            }
            PageMethods.buscar(document.getElementById('id').value, onSuccess, onFailure)
        }

        function obtenerIntanciasEspecificas() {
            function onSuccess(result) {
                if (result != null) {
                    var cont = document.getElementById('instancias');
                    cont.innerHTML = '';
                    var tabla = document.createElement('TABLE');
                    tabla.className = 'table';
                    cont.appendChild(tabla);
                    for (i = 0; i < result.length; i++) {
                        var tr = document.createElement('TR');
                        var td = document.createElement('TD');
                        td.className = 'con-borde';
                        td.appendChild(document.createTextNode(result[i].nroDeSerie));
                        var td2 = document.createElement('TD');
                        var input = document.createElement('INPUT');
                        input.type = 'TEXT';
                        input.name = 'productosEspecificos';
                        input.hidden='hidden'
                        input.value = result[i].nroDeSerie;
                        td2.appendChild(input);
                        tr.appendChild(td);
                        tr.appendChild(td2);
                        tabla.appendChild(tr);
                    }
                
                }
            }
            function onFailure(result) {
                alert(result._message);
            }
                var nroProducto = document.getElementById('producto').value;
                var nroSucursal = document.getElementById('sucursal').value;
                var cantidad = (document.getElementById('cantidad').value != null) ? document.getElementById('cantidad').value : 0
                PageMethods.obtenerInstaciasEspecificas(nroSucursal, nroProducto, cantidad, onSuccess, onFailure);

        }

        function limpiar() {
            limpiarForm();
            document.getElementById('instancias').innerHtml='';
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
     <section id="contenedor">
         <div class="row">
           
            <div class="mensaje-respuesta-contacto" id="LblMensaje" runat="server"></div>
        </div>
        <input type="hidden" id="accion" name="accion" />
        <span id="spid">Id:</span><input type="text" id="id" name="id" />
        <p />
          <span id="spcantidad">Cantidad: </span><input type="text" onchange="obtenerIntanciasEspecificas()" id="cantidad" name="cantidad" />
        <p/>
         <span id="spsucursal">Sucursal: </span><select id="sucursal" onchange="obtenerIntanciasEspecificas()"  name="sucursal"></select>
        <p/>
         <span id="spproducto">Producto: </span><select id="producto" onchange="obtenerIntanciasEspecificas()" name="producto" ></select>
        <p/>
           <span id="spestado">Estado: </span><select id="estado" name="estado" ></select>
        <p/>
         <span id="spfecha">Fecha</span><input type="text" id="fecha" name="fecha" />
        <p/>
          <span id="spmotivo">Motivo:</span><input type="text" id="motivo" name="motivo" />
        <p />
          <span id="spusuario">Nombre de usuario:</span><input type="text" disabled="disabled" id="nombreusuario" name="usuario" />
        <p />
        <input type="submit" id="descargarComprobanteBtn" value="Descargar Comprobante" onclick="setAccion(this.id)" />
        <input type="submit" id="eliminarBtn" value="Eliminar" onclick="setAccion(this.id)" />
        <input type="submit" id="modificarBtn" value="Guardar" onclick="setAccion(this.id)"/>
        <input type="button" id="crearBtn" value="Crear" onclick="crear()" />
         <input type="button" id="limpiarBtn" value="Limpiar" onclick="limpiar()"/>
        <input type="button" id="buscarBtn" value="Buscar" onclick="buscar()" />
        <p />
         <h3 id="hinstanciasprod">Productos específicos elegidos</h3>
         <div id="instancias">

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
