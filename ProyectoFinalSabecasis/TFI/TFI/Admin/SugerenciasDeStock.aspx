<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Admin/admin.Master" CodeBehind="SugerenciasDeStock.aspx.vb" Inherits="TFI.SugerenciasDeStock" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
         <link href="../static/bootstrap.min.css" rel="stylesheet" />
    <script src="../scripts/jquery.js"></script>
    <script src="../scripts/bootstrap.min.js"></script>
    <link href="../static/font-awesome/css/font-awesome.min.css" rel="stylesheet" />
    <link href="../static/shop-homepage.css" rel="stylesheet" />
    <link href="../static/sidebar.css" rel="stylesheet" />
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
            function onFailure(result) {
                alert(result._message);
            }
            PageMethods.cargarProductos(onSuccess, onFailure);
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
        <span id="spproducto">Producto:</span><select id="producto" name="producto" ></select>
        <p />
        <span id="spproductosugerido">Producto sugerido: </span><select id="sugerido" name="sugerido" ></select>
        <p />
        <input type="submit" id="eliminarBtn" value="Eliminar" onclick="setAccion(this.id)" />
        <input type="submit" id="modificarBtn" value="Guardar" onclick="setAccion(this.id)" />
        <input type="button" id="limpiarBtn" value="Limpiar" onclick="limpiarForm()" />
    </section>
    <div id="idioma" class="modal fade" role="dialog">
        <div id="Div2" class="modal-dialog modal-lg modal-content modal-dooba modal-idioma">
            <button type="button" class="btn btn-default btn-modal-close btn-circle pull-right" data-dismiss="modal"><i class="fa fa-times"></i></button>
            <div class="col-md-3"><span id="sidioma">Idioma: </span></div>
            <select id="idiomaSeleccionado" onchange="seleccionarIdioma();"></select>
        </div>
    </div>
</asp:Content>
