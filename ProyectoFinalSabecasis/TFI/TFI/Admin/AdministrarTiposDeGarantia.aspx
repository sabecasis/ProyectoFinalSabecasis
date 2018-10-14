<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Admin/admin.Master" CodeBehind="AdministrarTiposDeGarantia.aspx.vb" Inherits="TFI.AdministrarTiposDeGarantia" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <link href="../static/bootstrap.min.css" rel="stylesheet" />
    <script src="../scripts/jquery.js"></script>
    <script src="../scripts/bootstrap.min.js"></script>
    <link href="../static/font-awesome/css/font-awesome.min.css" rel="stylesheet" />
    <link href="../static/shop-homepage.css" rel="stylesheet" />
    <link href="../static/sidebar.css" rel="stylesheet" />
      <script>
        function crear() {
                  
            PageMethods.obtenerId(onSuccess, onFailure);
            function onSuccess(result) {
                document.getElementById('id').value = result;
            }
            function onFailure(result) {
                alert(result._message);
            }
        }

        function buscar() {
            function onSuccess(result) {
                if (result != null) {
                    document.getElementById('id').value = result.id;
                    document.getElementById('dias').value = result.dias;
                    document.getElementById('descripcion').value = result.descripcion;
                }
            }
            function onFailure(result) {
                alert(result._message);
                
            }
            var tipo =              
                    {
                        id: (document.getElementById('id').value=='')?0:document.getElementById('id').value,
                        dias: (document.getElementById('dias').value=='')?0:document.getElementById('dias').value,
                        descripcion: document.getElementById('descripcion').value 
                    } 
                
            PageMethods.buscar(tipo, onSuccess, onFailure);
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
        <input type="hidden" id="accion" name="accion" />
        
        <p />
         <span id="spid">Id:</span><input type="text" id="id" name="id"/>
        <p />
        <span id="spdiasvigencia">Dias de vigencia:</span><input type="text" id="dias" name="dias"  />
        <p />
        <span id="spdescripcion">Descripción:</span><input type="text" id="descripcion" name="descripcion"/>
        <p/>
        <input type="submit" id="eliminarBtn" value="Eliminar" onclick="setAccion(this.id)" />
        <input type="button" id="crearBtn" value="Crear" onclick="crear()" />
        <input type="submit" id="modificarBtn" value="Guardar"  onclick="setAccion(this.id)"/>
         <input type="button" id="limpiarBtn" value="Limpiar" onclick="limpiarForm()"/>
        <input type="button" id="buscarBtn" value="Buscar" onclick="buscar()" />
        <p />
    </section>

      <div id="idioma" class="modal fade" role="dialog">
        <div id="Div2" class="modal-dialog modal-lg modal-content modal-dooba modal-idioma">
            <button type="button" class="btn btn-default btn-modal-close btn-circle pull-right" data-dismiss="modal"><i class="fa fa-times"></i></button>
            <div class="col-md-3"><span id="sidioma">Idioma: </span></div>
            <select id="idiomaSeleccionado" onchange="seleccionarIdioma();"></select>
        </div>
    </div>
</asp:Content>
