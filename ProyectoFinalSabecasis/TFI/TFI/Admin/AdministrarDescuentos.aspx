<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Admin/admin.Master" CodeBehind="AdministrarDescuentos.aspx.vb" Inherits="TFI.AdministrarDescuentos" %>

<%@ OutputCache Location="None" VaryByParam="None" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../static/bootstrap.min.css" rel="stylesheet" />
    <script src="../scripts/jquery.js"></script>
    <script src="../scripts/bootstrap.min.js"></script>
    <link href="../static/font-awesome/css/font-awesome.min.css" rel="stylesheet" />
    <link href="../static/shop-homepage.css" rel="stylesheet" />
    <link href="../static/sidebar.css" rel="stylesheet" />
    <script src="../scripts/jquery-2.1.4.min.js"></script>
    <script src="../scripts/abmscripts.js"></script>
    <script src="../scripts/jquery-ui.min.js"></script>
    <link href="../static/jquery-ui.css" rel="stylesheet" />
    <script type="text/javascript">
        function crear() {
            function onSuccess(result) {
                document.getElementById('ContentPlaceHolder1_id').value = result;
            }
            function onFailure(result) {
                alert(result._message);
            }
            PageMethods.obtenerProximoId(onSuccess, onFailure);
        }

        $(function () {
            $("#ContentPlaceHolder1_fechainicio").datepicker({
                dateFormat: "yy-mm-dd"
            });
        });


        $(function () {
            $("#ContentPlaceHolder1_fechafin").datepicker({
                dateFormat: "yy-mm-dd"
            });
        });

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
         <div class="mensaje-respuesta-contacto" id="LblMensaje" runat="server"></div>
        <input type="hidden" id="accion" name="accion" />
        <div class="row">
       <div class="col-lg-3"> <span id="spid">Id:</span></div><div class="col-lg-3"><input type="text" id="id" runat="server" /></div>
        </div>
        <div class="row">
        <div class="col-lg-3"><span id="spnombre">Nombre:</span></div><div class="col-lg-3"><input type="text" id="nombre" runat="server" /></div>
            </div>
        <div class="row">
        <div class="col-lg-3"><span id="spmonto">Monto: </span></div><div class="col-lg-3"><input type="text" id="monto" runat="server" /></div>
            </div>
        <div class="row">
         <div class="col-lg-3"><span id="spporcentaje">Porcentaje: </span></div><div class="col-lg-3"><input type="text" id="porcentaje" runat="server" /></div>
            </div>
        <div class="row">
         <div class="col-lg-3"><span id="spdescripcion">Descripción: </span></div><div class="col-lg-3"><input type="text" id="descripcion" runat="server" /></div>
            </div>
        <div class="row">
          <div class="col-lg-3"><span id="spfechainicio">Fecha Inicio: </span></div><div class="col-lg-3"><input type="text" id="fechainicio" runat="server" /></div>
            </div>
       <div class="row">
         <div class="col-lg-3"><span id="spfechafin">Fecha Fin: </span></div><div class="col-lg-3"><input type="text" id="fechafin" runat="server" /></div>
           </div>
        <div class="row">
       <div class="col-lg-2"> <input type="button" id="crearBtn" value="Crear" onclick="crear()" /></div>
        <div class="col-lg-2"><input type="submit" id="modificarBtn" value="Guardar"  onclick="setAccion(this.id)" /></div>
        <div class="col-lg-2"><input type="button" id="limpiarBtn" value="Limpiar" onclick="limpiarForm()" /></div>
        <div class="col-lg-2"><input type="submit" id="buscarBtn" value="Buscar" onclick="setAccion(this.id)" /></div>
            </div>
        <div class="row">
            <span id="spdescuentoscreados">Descuentos creados</span>
         </div>
        <div class="row">
     <asp:ListBox ID="LstDescuentos" runat="server" AutoPostBack="True"></asp:ListBox>
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