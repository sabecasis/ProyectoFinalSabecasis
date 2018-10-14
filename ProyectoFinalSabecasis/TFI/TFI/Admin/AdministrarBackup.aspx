<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Admin/admin.Master" CodeBehind="AdministrarBackup.aspx.vb" Inherits="TFI.AdministrarBackup" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        window.onload=function(){
            obtenerListBackup();
        }

        function restaurarBackup() {
            var url = this.id;
            function onSuccess(result) {
                alert(result);
            }
            function onFailure(result) {
                alert(result._message);
            }
            PageMethods.restaurarBackup(url, onSuccess, onFailure);
        }

        function obtenerListBackup() {
            function onSuccess(result) {
                if (result != null) {
                    var cont = document.getElementById('listadoBackups');
                    var tabla = document.createElement('TABLE');
                    cont.appendChild(tabla);
                    var trheader = document.createElement('tr');
                    var tdd = document.createElement('td');
                    tdd.appendChild(document.createTextNode('Id'));
                    trheader.appendChild(tdd);
                    tabla.appendChild(trheader);
                    var tdd2 = document.createElement('td');
                    tdd2.appendChild(document.createTextNode('Backup'));
                    trheader.appendChild(tdd2);
                    for (i = 0; i < result.length; i++) {
                        var tr = document.createElement('TR');
                        var td = document.createElement('td');
                        var a = document.createElement('a');
                        a.href = "#";
                        a.onclick = restaurarBackup;
                        a.id = result[i].urlEnServidor;
                        a.appendChild(document.createTextNode(result[i].urlEnServidor));
                        td.appendChild(a);
                        var td1 = document.createElement('td');
                        td1.appendChild(document.createTextNode(result[i].id));
                        tr.appendChild(td1);
                        tr.appendChild(td);
                        tabla.appendChild(tr);
                    }
                }
                
            }
            function onFailure(result) {
                alert(result._message);
            }

            PageMethods.obtenerBackups(onSuccess, onFailure);
        }
    </script>
          <link href="../static/bootstrap.min.css" rel="stylesheet" />
    <script src="../scripts/jquery.js"></script>
    <script src="../scripts/bootstrap.min.js"></script>
    <link href="../static/font-awesome/css/font-awesome.min.css" rel="stylesheet" />
    <link href="../static/shop-homepage.css" rel="stylesheet" />
    <link href="../static/sidebar.css" rel="stylesheet" />
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
        <p/>
        <input type="hidden" id="accion" name="accion" />
        <input type="submit" id="backupBtn" value="Crear Backup"  onclick="setAccion(this.id)" />
        <div id="3barra"></div>
        <div id="listadoBackups">

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
