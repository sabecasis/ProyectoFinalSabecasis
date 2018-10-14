<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Admin/admin.Master" CodeBehind="AdministrarRoles.aspx.vb" Inherits="TFI.AdministrarRoles" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <link href="../static/bootstrap.min.css" rel="stylesheet" />
    <script src="../scripts/jquery.js"></script>
    <script src="../scripts/bootstrap.min.js"></script>
    <link href="../static/font-awesome/css/font-awesome.min.css" rel="stylesheet" />
    <link href="../static/shop-homepage.css" rel="stylesheet" />
    <link href="../static/sidebar.css" rel="stylesheet" />
    <script src="../scripts/abmscripts.js"></script>
    <script type="text/javascript">
        function limpiar() {
            limpiarForm();
            //cargarPermisos();
            deshabilitar();
            if (document.getElementById('ContentPlaceHolder1_id').value != '0' || document.getElementById('ContentPlaceHolder1_id').value != '') {
                document.getElementById('ContentPlaceHolder1_id').disabled = 'disabled';
                document.getElementById('modificarBtn').disabled = '';
            }
        }

        function guardar() {
            var arrayPermisos = document.getElementsByName('permisos');
            var permisosFiltrados = new Array();
            var i = 0
            var j = 0;
            while (document.getElementById('ContentPlaceHolder1_chkPermisos_' + i) != null) {
                if (document.getElementById('ContentPlaceHolder1_chkPermisos_' + i).checked == true) {
                    permisosFiltrados[j] = { id: document.getElementById('ContentPlaceHolder1_chkPermisos_' + i).value, nombre: '' };
                    j++;
                }
                i++;
            }
            
            var rol = { id: document.getElementById('ContentPlaceHolder1_id').value, nombre: document.getElementById('ContentPlaceHolder1_nombre').value, permisos: permisosFiltrados, iniciaEnAdmin: document.getElementById('ContentPlaceHolder1_iniciaadmin').checked }
            PageMethods.guardar(rol, onSuccess, onFailure);
            function onSuccess(result) {
                alert(result);
                document.getElementById('modificarBtn').disabled = 'disabled';
            }
            function onFailure(result) {
                alert(result._message);
            }
            document.getElementById('ContentPlaceHolder1_id').disabled = 'disabled';
        }

        function eliminar() {
            PageMethods.eliminar(document.getElementById('ContentPlaceHolder1_id').value, onSuccess, onFailure);
            function onSuccess(result) {
                alert(result);
                document.getElementById('modificarBtn').disabled = 'disabled';
            }
            function onFailure(result) {
                alert(result._message);
            }
        }

        window.onload = function () {
            if (document.getElementById('ContentPlaceHolder1_id').value != '0' || document.getElementById('ContentPlaceHolder1_id').value != '') {
                document.getElementById('ContentPlaceHolder1_id').disabled = 'disabled';
                document.getElementById('modificarBtn').disabled = '';
            }
        }

        function crear() {
            function onSuccess(result) {
                document.getElementById('ContentPlaceHolder1_id').value = result;
            }
            function onFailure(result) {
                alert(result._message);
            }
            PageMethods.obtenerProximoId(onSuccess, onFailure);
            document.getElementById('ContentPlaceHolder1_id').disabled = 'disabled';
            document.getElementById('modificarBtn').disabled = '';           
        }

        function deshabilitar() {
            document.getElementById('modificarBtn').disabled = 'disabled';
            document.getElementById('ContentPlaceHolder1_id').disabled = '';
        }

        function buscar() {
            PageMethods.buscar(document.getElementById('ContentPlaceHolder1_nombre').value, onSuccess, onFailure);
            function onSuccess(result) {
                var rol = result
                if (rol != null) {
                    document.getElementById('ContentPlaceHolder1_id').value = rol.id;
                    document.getElementById('ContentPlaceHolder1_nombre').value = rol.nombre;
                    document.getElementById('ContentPlaceHolder1_iniciaadmin').checked = rol.iniciaEnAdmin;
                    document.getElementById('modificarBtn').disabled = '';

                    var i = 0;
                    var j = 0;
                    while (document.getElementById('ContentPlaceHolder1_chkPermisos_' + i) != null && j < rol.permisos.length) {
                        if (document.getElementById('ContentPlaceHolder1_chkPermisos_' + i).value == rol.permisos[j].id) {
                            document.getElementById('ContentPlaceHolder1_chkPermisos_' + i).checked = true;
                            j++;
                        }
                        i++;
                    }
                }
            }
            function onFailure(result) {
                alert(result._message);
            }
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
        <div class="row">
            <div class="mensaje-respuesta-contacto" id="LblMensaje" runat="server"></div>
        </div>
        <div class="row">
            <div class="col-lg-3"><span id="spid">Id:</span></div ><div class="col-lg-3"><input type="text" id="id" runat="server"/></div>
        </div>
        <div class="row">
            <div class="col-lg-3"><span id="spnombre">Nombre:</span></div ><div class="col-lg-3"><input type="text" id="nombre" runat="server"/></div >
        </div>
         <div class="row">
            <div class="col-lg-4"><span id="spiniciaenadmin">Inicia como admin:</span></div ><div class="col-lg-3"><input type="checkbox" id="iniciaadmin" runat="server"/></div >
        </div>
        <div class="row">
        <div class="col-lg-3"><input type="button" id="crearBtn" value="Crear" onclick="crear()" /></div >
       <div class="col-lg-3"> <input type="button" id="eliminarBtn" value="Eliminar" onclick="eliminar()" /></div >
        <div class="col-lg-3"><input type="button" id="modificarBtn" value="Guardar" disabled="disabled" onclick="guardar()" /></div >
            </div>
        <div class="row">
        <div class="col-lg-3"><input type="button" id="limpiarBtn" value="Limpiar" onclick="limpiar()" /></div >
        <div class="col-lg-3"><input type="button" id="buscarBtn" value="Buscar" onclick="buscar()" /></div >
        </div>
        <div id="conetenedorAsignacion" class="row">
            <div class="col-lg-7">
                <h3 id="htitulopermisos">Permisos</h3>
                <div id="contenedorPermisos">

                </div>
                <asp:CheckBoxList ID="chkPermisos" runat="server"></asp:CheckBoxList>
            </div>
            <div class="col-lg-5">
                <h3 id="htitulorolesexistentes">Roles existentes</h3>
                <asp:ListBox ID="LstRoles" runat="server" AutoPostBack="True"></asp:ListBox>
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

