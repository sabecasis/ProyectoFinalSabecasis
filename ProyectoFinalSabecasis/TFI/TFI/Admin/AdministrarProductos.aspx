<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Admin/admin.Master" CodeBehind="AdministrarProductos.aspx.vb" Inherits="TFI.AdministrarProductos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../static/bootstrap.min.css" rel="stylesheet" />
    <script src="../scripts/jquery.js"></script>
    <script src="../scripts/bootstrap.min.js"></script>
    <script src="../scripts/abmscripts.js"></script>

    <link href="../static/font-awesome/css/font-awesome.min.css" rel="stylesheet" />
    <link href="../static/shop-homepage.css" rel="stylesheet" />
    <link href="../static/sidebar.css" rel="stylesheet" />
    <script>
        window.onload = function () {

        }

        function crear() {
            function onSuccess(result) {
                if (result != null) {
                    document.getElementById('ContentPlaceHolder1_id').value = result;
                }
            }
            function onFailure(result) {
                alert(result._message);
            }
            PageMethods.obtenerId(onSuccess, onFailure);
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
                                <a href="#" data-target="#idioma" data-toggle="modal" id="acambiaridioma">Cambiar Idioma</a>
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
        <div class="mensaje-respuesta-contacto" id="LblMensaje" runat="server"></div>
        <input type="hidden" id="accion" name="accion" />
        <div class="row">
           <div class="col-lg-5"><span id="spid">Id:</span></div><div class="col-lg-3"><input type="text" id="id" name="id" value="0" runat="server"/></div>
        </div>
        <div class="row">
            <div class="col-lg-5"><span id="spnombre">Nombre:</span></div><div class="col-lg-3"><input type="text" id="nombre" name="nombre" runat="server"/></div>
        </div>
        <div class="row">
            <div class="col-lg-5"><span id="spdescripcion">Descripción:</span></div><div class="col-lg-3"><input type="text" id="descripcion" name="descripcion" runat="server"/></div>
        </div>
        <div class="row">
            <div class="col-lg-5"><span id="spprecioventa">Precio de venta:</span></div><div class="col-lg-3"><input type="text" id="precioventa" name="precioventa" value="0" runat="server"/></div>
        </div>
        <div class="row">
            <div class="col-lg-5"><span id="spporcentaje">Porcentaje de ganancia:</span></div><div class="col-lg-3"><input type="text" id="porcentajeganancia" name="porcentajeganancia" value="0" runat="server"/></div>
        </div>
        <div class="row">
           <div class="col-lg-5"> <span id="spcostoposesion">Costo de posesión:</span></div><div class="col-lg-3"><input type="text" id="costoposesion" name="costoposesion" value="0" runat="server"/></div>
        </div>
        <div class="row">
            <div class="col-lg-5"><span id="spcostofinanciero">Costo financiero:</span></div><div class="col-lg-3"><input type="text" id="costofinanciero" name="costofinanciero" value="0" runat="server"/></div>
        </div>
        <div class="row">
            <div class="col-lg-5"><span id="spcostoestandar">Costo estándar:</span></div><div class="col-lg-3"><input type="text" id="costoestandar" name="costoestandar" value="0" runat="server"/></div>
        </div>
        <div class="row">
            <div class="col-lg-5"><span id="sppuntominimo">Punto mínimo de reposición:</span></div><div class="col-lg-3"><input type="text" id="puntominimo" name="puntominimo" value="0" runat="server"/></div>
        </div>
        <div class="row">
           <div class="col-lg-5"> <span id="sppuntomaximo">Punto máximo de reposición:</span></div><div class="col-lg-3"><input type="text" id="puntomaximo" name="puntomaximo" value="0" runat="server"/></div>
        </div>
        <div class="row">
            <div class="col-lg-5"><span id="spaltopaquete">Alto de paquete:</span></div><div class="col-lg-3"><input type="text" id="altopaquete" name="altopaquete" value="0" runat="server"/></div>
        </div>
        <div class="row">
            <div class="col-lg-5"><span id="apanchopaquete">Ancho de paquete:</span></div><div class="col-lg-3"><input type="text" id="anchopaquete" name="anchopaquete" value="0" runat="server"/></div>
        </div>
        <div class="row">
            <div class="col-lg-5"><span id="splargopaquete">Largo de paquete:</span></div><div class="col-lg-3"><input type="text" id="largopaquete" name="largopaquete" value="0" runat="server"/></div>
        </div>
        <div class="row">
            <div class="col-lg-5"><span id="spciclo">Ciclo:</span></div><div class="col-lg-3"><input type="text" id="ciclo" name="ciclo" value="0" runat="server" /></div>
        </div>
        <div class="row">
            <div class="col-lg-5"><span id="spcantidadenstock">Cantidad en stock:</span></div><div class="col-lg-3"><input type="text" id="cantidad" name="cantidad" value="0" disabled="disabled" runat="server"/></div>
        </div>
        <div class="row">
            <div class="col-lg-5"><span id="spgarantia">Garantía:</span></div><div class="col-lg-3"><select id="garantia" name="garantia" runat="server"></select></div>
        </div>
        <div class="row">
           <div class="col-lg-5"> <span id="spestado">Estado:</span></div><div class="col-lg-3"><select id="estado" name="estado" runat="server">
            </select></div>
        </div>
        <div class="row">
            <div class="col-lg-5"><span id="spfamiliaproducto">Familia:</span></div><div class="col-lg-3"><select id="familia" name="familia" runat="server">
            </select></div>
        </div>
        <div class="row">
            <div class="col-lg-5"><span id="spmetodoreposicion">Método de reposición:</span></div><div class="col-lg-3"><select id="metodorep" name="metodorep" runat="server">
            </select></div>
        </div>
        <div class="row">
           <div class="col-lg-5"> <span id="spmetodovaloracion">Método de valoración:</span></div><div class="col-lg-3"><select id="metodoval" name="metodoval" runat="server">
            </select></div>
        </div>
        <div class="row">
           <div class="col-lg-5"> <span id="sptipodeproducto">Tipo de producto:</span></div><div class="col-lg-3"><select id="tipoproducto" name="tipoproducto" runat="server">
            </select></div>
        </div>
        <div class="row">
            <div class="col-lg-12"><span id="spayudacarcateristicasprod" class="bg-danger con-borde">Los saltos de línea de las características deben ser ingresados como etiquetas HTML </span></div>
        </div>
        <div class="row">
           <div class="col-lg-3"> <span id="spcaracteristicasproducto">Caraterísticas: </span></div>
            <div class="col-lg-5"><textarea id="caracteristicas" name="caracteristicas" rows="18" cols="50" runat="server"></textarea></div>
        </div>
        <div class="row">
           <div class="col-lg-3"> <span id="spimagen">Imagen:</span></div><div class="col-lg-3"><asp:FileUpload ID="imagen" runat="server" /></div>
        </div>
        <div class="row">
            <div id="conetenedorAsignacion" class="col-lg-6">
                <div class="row"><span id="spcatalogos">Catálogos</span></div>
                <div class="row"><asp:CheckBoxList ID="chCatalogos" runat="server" AutoPostBack="False"></asp:CheckBoxList></div>
            </div>
             <div class="col-lg-6">
            <div class="row"><span id="spdescuentosaplicables">Descuentos a aplicar</span></div>
            <div class="row">
                 <asp:CheckBoxList ID="chDescuentos" runat="server" AutoPostBack="False"></asp:CheckBoxList>
            </div>
        </div>
        </div>
        <div class="row">
           <!-- <div class="col-lg-2"><input type="submit" id="eliminarBtn" value="Eliminar" onclick="setAccion(this.id)" /></div>-->
            <div class="col-lg-2"><input type="submit" id="modificarBtn" value="Guardar" onclick="setAccion(this.id)" /></div>
            <div class="col-lg-2"><input type="button" id="crearBtn" value="Crear" onclick="crear()" /></div>
            <div class="col-lg-2"><input type="button" id="limpiarBtn" value="Limpiar" onclick="limpiarForm()" /></div>
            <div class="col-lg-2"><input type="submit" id="buscarBtn" value="Buscar" onclick="setAccion(this.id)" /></div>
        </div>
        <div class="row">
            <div class="row"><span id="spproductosexistentes">Productos creados</span></div>
            <div class="row">
                <asp:ListBox ID="ltsProductos" runat="server" AutoPostBack="True"></asp:ListBox>
            </div>
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
