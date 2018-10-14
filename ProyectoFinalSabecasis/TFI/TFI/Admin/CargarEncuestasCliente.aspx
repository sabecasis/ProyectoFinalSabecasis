<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Admin/admin.Master" CodeBehind="CargarEncuestasCliente.aspx.vb" Inherits="TFI.CargarEncuestasCliente" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../static/bootstrap.min.css" rel="stylesheet" />
    <script src="../scripts/jquery.js"></script>
    <script src="../scripts/bootstrap.min.js"></script>
      <script src="../scripts/jquery-2.1.4.min.js"></script>
    <script src="../scripts/abmscripts.js"></script>
    <script src="../scripts/jquery-ui.min.js"></script>
    <link href="../static/jquery-ui.css" rel="stylesheet" />
    <link href="../static/font-awesome/css/font-awesome.min.css" rel="stylesheet" />
    <link href="../static/shop-homepage.css" rel="stylesheet" />
    <link href="../static/sidebar.css" rel="stylesheet" />
    <script>
        
         function crear() {
             document.getElementById('ContentPlaceHolder1_aagregarPreguntas').href = "#";
            function onSuccess(result) {
                if (result != null) {
                    document.getElementById('ContentPlaceHolder1_id').value = result
                }
            }
            function onFailure(result) {
                alert(result._message);
            }
            PageMethods.obtenerProximoId(onSuccess, onFailure);
         }

         $(function () {
             $("#ContentPlaceHolder1_fechaDesde").datepicker({
                 dateFormat: "yy-mm-dd"
             });
         });

         $(function () {
             $("#ContentPlaceHolder1_fechaHasta").datepicker({
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
    <section id="contenedor" class="container">
        <div class="row">
            <div class="mensaje-respuesta-contacto" id="LblMensaje" runat="server"></div>
        </div>
        <input type="hidden" id="accion" name="accion" />
        <div class="row">
            <div class="col-lg-3"><span id="spid">Id:</span></div>
            <div class="col-lg-3">
                <input type="text" id="id" name="id" runat="server"/></div>
        </div>
        <div class="row">
            <div class="col-lg-3"><span id="spnombre">Nombre:</span></div>
            <div class="col-lg-3">
                <input type="text" id="nombre" name="nombre" runat="server"/></div>
        </div>
        <div class="row">
            <div class="col-lg-3"><span id="sptipoencuesta">Tipo de encuesta:</span></div>
            <div class="col-lg-3">
                <select id="tipo" name="tipo" runat="server"></select></div>
        </div>
        <div class="row">
            <div class="col-lg-3"><span id="spdescripcion">Descripcion: </span></div>
            <div class="col-lg-3">
                <input type="text" id="descripcion" name="descripcion" runat="server"/></div>
        </div>
        <div class="row">
            <div class="col-lg-3"><span id="spfechadesde">Fecha desde: </span></div>
            <div class="col-lg-3">
                <input type="text" id="fechaDesde" name="fechaDesde" runat="server"/></div>
        </div>
        <div class="row">
            <div class="col-lg-3"><span id="spfechahasta">Fecha hasta: </span></div>
            <div class="col-lg-3">
                <input type="text" id="fechaHasta" name="fechaHasta" runat="server"/></div>
        </div>
        <div class="row">
            <div class="col-lg-3">
                <input type="submit" id="modificarBtn" value="Guardar" onclick="setAccion(this.id)" /></div>
            <div class="col-lg-3">
                <input type="button" id="crearBtn" value="Crear" onclick="crear()" /></div>
            <div class="col-lg-3">
                <input type="button" id="limpiarBtn" value="Limpiar" onclick="limpiarForm()" /></div>
            <div class="col-lg-3">
                <input type="submit" id="buscarBtn" value="Buscar" onclick="setAccion(this.id)" /></div>
        </div>
        <div class="row">
            <div class="col-lg-6"><a href="#" id="aagregarPreguntas" runat="server">Agregar preguntas</a> </div>
        </div>
         <div class="row">
            <div class="row">
                <span id="sppreguntasdeencuesta">Preguntas de encuesta</span>
            </div>
            <div class="row">
             <asp:GridView ID="LstPreguntas" runat="server">
                </asp:GridView>
            </div>
        </div>
        <div class="row">
            <div>
                <span id="spencuestascreadas">Encuestas creadas</span>
            </div>
            <div>
            <asp:ListBox ID="LstEncuestas" runat="server" AutoPostBack="True"></asp:ListBox>
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
