<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Admin/admin.Master" CodeBehind="BitacoraDeUsuario.aspx.vb" Inherits="TFI.BitacoraDeUsuario" %>
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
    <script>
        function buscar() {
            var bitacora = {
                usuario: { id: 0, nombre: document.getElementById('nombreusuario').value},
                evento:{id:document.getElementById('eventos').value, evento:''},
                fechaDesde: document.getElementById('fecha').value == '' ? '0001-01-01' : document.getElementById('fecha').value,
                fechaHasta:document.getElementById('fechaHasta').value == '' ? '0001-01-01' : document.getElementById('fechaHasta').value,
                hora:''
            }

            PageMethods.buscar(bitacora, onSuccess, onFailure)
        }

        $(function () {
            $("#fecha").datepicker({
                dateFormat: "yy-mm-dd"
            });
        });


        $(function () {
            $("#fechaHasta").datepicker({
                dateFormat: "yy-mm-dd"
            });
        });

        window.onload = function () {
            obtenerEventos();
            limpiar();
        }

        function obtenerEventos() {
            function onSuccesEvento(result) {
                var select = document.getElementById('eventos');
                var option = document.createElement('OPTION');
                option.id = 0;
                option.value = 0;
                var texto = document.createTextNode('seleccionar');
                option.appendChild(texto);
                select.appendChild(option);
                if (result != null && result.length > 0) {
                    for (i = 0; i < result.length; i++) {
                        var option = document.createElement('OPTION');
                        option.id = result[i].id;
                        option.value = result[i].id;
                        var texto = document.createTextNode(result[i].evento);
                        option.appendChild(texto);
                        select.appendChild(option);
                    }
                }
            }
            function onFailureEvento(result) {
                alert(result._message);
            }
            PageMethods.obtenerEventos(onSuccesEvento, onFailureEvento);
        }


        function onSuccess(result) {
            var div = document.getElementById('contenedorElementos');
            while (div.firstChild) {
                div.removeChild(div.firstChild);
            }
            var tabla = document.createElement('TABLE');
            tabla.className = 'table con-borde';
            var cabecera = document.createElement('TR');
            var cevento = document.createElement('TH');
            cevento.appendChild(document.createTextNode('Evento'));
            cevento.id = 'thevento';
            cevento.className = 'con-borde';
            cabecera.appendChild(cevento);
            var cusuario = document.createElement('TH');
            cusuario.appendChild(document.createTextNode('Usuario'));
            cusuario.id = 'thusuario';
            cusuario.className = 'con-borde';
            cabecera.appendChild(cusuario);
            var cfecha = document.createElement('TH');
            cfecha.appendChild(document.createTextNode('Fecha'));
            cfecha.id = 'thfecha';
            cfecha.className = 'con-borde';
            cabecera.appendChild(cfecha);
            var chora = document.createElement('TH');
            chora.appendChild(document.createTextNode('Hora'));
            chora.id = 'thhora';
            chora.className = 'con-borde';
            cabecera.appendChild(chora);
            tabla.appendChild(cabecera);
            div.appendChild(tabla);
            if (result != null && result.length > 0) {

                for (i = 0; i < result.length; i++) {
                    var tr = document.createElement('TR');
                    var td1 = document.createElement('TD');
                    var texto1 = document.createTextNode(result[i].evento.evento);
                    td1.appendChild(texto1);
                    tr.appendChild(td1);
                    td1.className = 'con-borde';
                    var td2 = document.createElement('TD');
                    var texto2 = document.createTextNode(result[i].usuario.nombre);
                    td2.appendChild(texto2);
                    td2.className = 'con-borde';
                    tr.appendChild(td2);
                    var td3 = document.createElement('TD');
                    var texto3 = document.createTextNode(result[i].fechaDesde);
                    td3.appendChild(texto3);
                    td3.className = 'con-borde';
                    tr.appendChild(td3);
                    var td4 = document.createElement('TD');
                    var texto4 = document.createTextNode(result[i].hora);
                    td4.appendChild(texto4);
                    td4.className = 'con-borde';
                    tr.appendChild(td4);
                    tabla.appendChild(tr);
                }
                
            }
        }
        function onFailure(result) {
            alert(result._message);
        }

        function limpiar() {
            limpiarForm();
            var bitacora = { usuario: { id: 0, nombre: '' }, evento: { id: 0, evento: '' }, fechaDesde: '0001-01-01', hora: '', fechaHasta: '0001-01-01' }
            PageMethods.buscar(bitacora, onSuccess, onFailure);
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
        <span id="spevento">Evento:</span><select id="eventos"></select>
        <p />
        <span id="spusuario">Usuario:</span><input type="text" id="nombreusuario" />
        <p />
        <span id="spfechadesde">fecha desde</span><input type="text" id="fecha" name="fecha" />
        <p/>
        <span id="spfechahasta">fecha hasta</span><input type="text" id="fechaHasta" name="fechaHasta" />
        <p/>
        <input type="button" id="buscarBtn" value="Buscar"  onclick="buscar();" />
         <input type="button" id="limpiarBtn" value="Limpiar"  onclick="limpiar();" />
        <p />
        <div>
        <div id="contenedorElementos">
        </div>
            <asp:GridView ID="bitacora" runat="server"></asp:GridView>
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
