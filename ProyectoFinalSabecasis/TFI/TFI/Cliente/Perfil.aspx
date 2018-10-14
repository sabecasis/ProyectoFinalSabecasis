<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/ConsumidorFinal.Master" CodeBehind="Perfil.aspx.vb" Inherits="TFI.Perfil" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../static/bootstrap.min.css" rel="stylesheet" />
    <script src="../scripts/jquery.js"></script>
    <script src="../scripts/bootstrap.min.js"></script>
    <link href="../static/font-awesome/css/font-awesome.min.css" rel="stylesheet" />
    <link href="../static/shop-homepage.css" rel="stylesheet" />
    <link href="../static/sidebar-front.css" rel="stylesheet" />
     <link href="../static/font-awesome/css/bootstrap.min.css" rel="stylesheet" />
    <script src="../scripts/GestionarUsuarios.js"></script>
    <script>
        window.onload = function () {
           
            cargarPaises();
            cargarTipsoDoc();
            cargarTipsosTel();
            document.getElementById('nombreusuario').value = document.getElementById('ContentPlaceHolder1_unombre').value;
            document.getElementById('id').value = document.getElementById('ContentPlaceHolder1_usuarioId').value;
            buscar();
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
                <ul class="nav navbar-nav navbar-right" id="menuHorizontal">
                    <li class="hidden">
                        <a href="#page-top"></a>
                    </li>
                    <li>
                        <a href="/Cliente/IniciarSesion.aspx" id="alogin">Iniciar sesión</a>
                    </li>
                    <li>
                        <a href="/Cliente/Inicio.aspx">Inicio</a>
                    </li>
                    <li>
                        <a href="#" data-target="#idioma" data-toggle="modal" id="acambiaridioma">Cambiar idioma</a>
                    </li>
                    <li>
                        <a href="/Cliente/Catalogo.aspx" id="acatalogo">Catálogo</a>
                    </li>
                    <li>
                        <a href="/Cliente/Ayuda.aspx" id="aayuda">Ayuda</a>
                    </li>
                     <li>
                        <a href="/Cliente/NovedadesPublicas.aspx" id="aNovedadesPublicas">Novedades</a>
                    </li>
                </ul>
                <div class="enlinea espaciado-izquierdo-breve espaciado-suerior" style="visibility:hidden"  id="user_tag"><i class="glyphicon glyphicon-user logo-usuario"></i><a href="/Cliente/Perfil.aspx" id="aPerfil" runat="server" class="usuario espaciado-izquierdo-breve"></a> </div>
            </div>
        </div>
        <div id="breadcrums" runat="server" class="breadcrums">
        </div>
        <div class="collapse navbar-collapse navbar-ex1-collapse">
            <ul class="nav navbar-nav side-nav">
                <li class="active">
                    <a href="#">Perfil</a>
                </li>
                <li>
                    <a href="/Cliente/ModuloDeCliente.aspx">Mis compras</a>
                </li>
                <li>
                    <a href="/Cliente/EstadoDeCuenta.aspx">Estado de cuenta</a>
                </li>
                <li>
                    <a href="/Cliente/RecuperarPassword.aspx">Recuperar contraseña</a>
                </li>
                <li>
                    <a href="/Cliente/ChatDeSoporteAlCliente.aspx">Soporte al cliente</a>
                </li>
                <li>
                    <a href="/Cliente/Novedades.aspx">Subscripción a newsletter</a>
                </li>
            </ul>
        </div>
    </nav>
    <section class="contenedor-soft">

        <div class="container">
            <div class="row">
                <div class="col-lg-2"><span id="spusuario">Nombre de usuario:</span></div>
                <div class="col-lg-2">
                    <input type="text" id="nombreusuario" /></div>
            </div>
            <div class="row">
                <div class="col-lg-2"><span id="spnombre">Nombre: </span></div>
                <div class="col-lg-2">
                    <input type="text" id="nombre" /></div>
            </div>
            <div class="row">
                <div class="col-lg-2"><span id="spapellido">Apellido: </span></div>
                <div class="col-lg-2">
                    <input type="text" id="apellido" /></div>
            </div>
            <div class="row">
                <div class="col-lg-2"><span id="spdocumento">Documento: </span></div>
                <div class="col-lg-2">
                    <input type="text" id="documento" /></div>
            </div>
            <div class="row">
                <div class="col-lg-2"><span id="sptipodoc">Tipo de documento: </span></div>
                <div class="col-lg-2">
                    <select id="tipoDoc" onchange="this.value=usuario.persona.tipoDeDocumento.id;"></select></div>
            </div>
            <div class="row">
                <div class="col-lg-2"><span id="spcalle">Calle: </span></div>
                <div class="col-lg-2">
                    <input type="text" id="calle" /></div>
            </div>
            <div class="row">
                <div class="col-lg-2"><span id="spnro">Nro.: </span></div>
                <div class="col-lg-2">
                    <input type="text" id="nro" /></div>
            </div>
            <div class="row">
                <div class="col-lg-2"><span id="sppiso">Piso: </span></div>
                <div class="col-lg-2">
                    <input type="text" id="piso" /></div>
            </div>
            <div class="row">
                <div class="col-lg-2"><span id="spdepto">Departamento: </span></div>
                <div class="col-lg-2">
                    <input type="text" id="depto" /></div>
            </div>
            <div class="row">
                <div class="col-lg-2"><span id="sppais">Pais: </span></div>
                <div class="col-lg-2">
                    <select id="pais" onchange="cargarProvincias(0);"></select></div>
            </div>
            <div class="row">
                <div class="col-lg-2"><span id="spprovincia">Provincia: </span></div>
                <div class="col-lg-2">
                    <select id="provincia" onchange="cargarLocalidades(0);"></select></div>
            </div>
            <div class="row">
                <div class="col-lg-2"><span id="splocalidad">Localidad: </span></div>
                <div class="col-lg-2">
                    <select id="localidad"></select></div>
            </div>
            <div class="row">
                <div class="col-lg-2"><span id="spemail">E-mail: </span></div>
                <div class="col-lg-2">
                    <input type="text" id="email" /></div>
            </div>
            <div class="row">
                <div class="col-lg-2"><span id="sptelefono">Telefono: </span></div>
                <div class="col-lg-2">
                    <input type="text" id="telefono" /></div>
            </div>
            <div class="row">
                <div class="col-lg-2"><span id="sptipotel">Tipo de Telefono: </span></div>
                <div class="col-lg-2">
                    <select id="tipoTel" onchange="this.value=usuario.persona.contacto.telefonos[0].tipo.id"></select></div>
            </div>
            <div class="row">
                <input type="password" id="contrasenia" style="visibility: hidden"/>
                </div>
                <div class="row">
                <input type="password" id="contrasenia2" style="visibility: hidden"/>
        </div>
             <div class="row">
                <input type="checkbox" id="bloqueado" style="visibility: hidden" />
            </div>
            <div class="row">
                <input type="hidden" id="usuarioId" runat="server" />
            </div>
            <div class="row">
                <input type="hidden" id="referrer" runat="server" />
            </div>
            <div class="row">
                <input type="hidden" id="unombre" runat="server" />
            </div>
            <div class="row">
                <input type="hidden" id="accion" name="accion" />
            </div>
            <div class="row">
                <input type="text" id="id" readonly="readonly" style="visibility: hidden" />
            </div>
            <div class="row">
                <input type="button" id="modificarBtn" value="Guardar" disabled="disabled" onclick="guardar()" />
            </div>
            <div class="row">
                <a href="/Cliente/BajaDeCliente.aspx" id="abajacliente">Darse de baja</a>
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

