<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Admin/admin.Master" CodeBehind="AdministrarUsuarios.aspx.vb" Inherits="TFI.AdministrarUsuarios" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../static/bootstrap.min.css" rel="stylesheet" />
    <script src="../scripts/jquery.js"></script>
    <script src="../scripts/bootstrap.min.js"></script>
    <link href="../static/font-awesome/css/font-awesome.min.css" rel="stylesheet" />
    <link href="../static/shop-homepage.css" rel="stylesheet" />
    <link href="../static/sidebar.css" rel="stylesheet" />
    <script src="../scripts/abmscripts.js"></script>
    <script type="text/javascript">
        window.onload = function () {
            if (document.getElementById('ContentPlaceHolder1_id').value != '' || document.getElementById('ContentPlaceHolder1_id').value != '0') {
                document.getElementById('modificarBtn').disabled = '';
                document.getElementById('ContentPlaceHolder1_id').disabled = 'disabled';
            }
            if (document.getElementById('ContentPlaceHolder1_pais').value == 'Seleccionar' || document.getElementById('ContentPlaceHolder1_id').value == '') {
                cargarPaises()
            }
           
        }

        function limpiar() {
            limpiarForm();
            deshabilitar();
        }

        function guardar() {
            var usuario;
            var i = 0
            var j = 0;
            var arrayRolesAGuardar = new Array();
            while (document.getElementById('ContentPlaceHolder1_chkRoles_' + i) != null) {
                if (document.getElementById('ContentPlaceHolder1_chkRoles_' + i).checked == true) {
                    arrayRolesAGuardar[j] = { id: document.getElementById('ContentPlaceHolder1_chkRoles_' + i).value, nombre: '' };
                    j++;
                }
                i++;
            }
          
            var telefono = { id: 0, telefono: document.getElementById('ContentPlaceHolder1_telefono').value, tipo: { id: document.getElementById('ContentPlaceHolder1_tipoTel').value, tipo: '' } }
            var oContacto = {
                id: 0,
                calle: document.getElementById('ContentPlaceHolder1_calle').value,
                numero: document.getElementById('ContentPlaceHolder1_nro').value,
                piso: document.getElementById('ContentPlaceHolder1_piso').value == '' ? 0 : document.getElementById('ContentPlaceHolder1_piso').value,
                departamento: document.getElementById('ContentPlaceHolder1_depto').value,
                email: document.getElementById('ContentPlaceHolder1_email').value,
                telefonos: [telefono],
                localidad: { id: document.getElementById('ContentPlaceHolder1_localidad').value, cp: '', localidad: '' }
            }
            var oPersona = {
                id: 0,
                nombre: document.getElementById('ContentPlaceHolder1_nombre').value,
                apellido: document.getElementById('ContentPlaceHolder1_apellido').value,
                documento: document.getElementById('ContentPlaceHolder1_documento').value,
                tipoDeDocumento: {
                    id: document.getElementById('ContentPlaceHolder1_tipoDoc').value,
                    tipoDeDocumento: ''
                },
                contacto: oContacto
            }

            usuario = {
                id: document.getElementById('ContentPlaceHolder1_id').value,
                nombre: document.getElementById('ContentPlaceHolder1_nombreusuario').value,
                password: document.getElementById('contrasenia').value,
                persona: oPersona,
                roles: arrayRolesAGuardar,
                bloqueado: document.getElementById('ContentPlaceHolder1_bloqueado').checked
            }
            function onSuccess(result) {
                alert(result);
                document.getElementById('modificarBtn').disabled = 'disabled';
            }
            function onFailure(result) {
                alert(result._message);
            }

            PageMethods.guardar(usuario, onSuccess, onFailure);

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

        function deshabilitar() {
            document.getElementById('modificarBtn').disabled = 'disabled';
            document.getElementById('ContentPlaceHolder1_id').disabled = '';
        }

     

        function cargarLocalidades(idProvincia, valorSeleccionado) {

            function onSuccess(result) {
                if (result != null && result.length > 0) {
                    var paises = document.getElementById('ContentPlaceHolder1_localidad');
                    paises.innerHTML = "<select id=\"localidad\"><option value=\"0\"></option></select>";
                    for (i = 0; i < result.length; i++) {
                        var opcion = document.createElement('OPTION');
                        opcion.value = result[i].id;
                        var texto = document.createTextNode(result[i].localidad);
                        opcion.appendChild(texto);
                        paises.appendChild(opcion);
                    }
                    paises.value = valorSeleccionado;
                }
            }

            function onFailure(result) {
                alert(result._message);
            }
            var prov = idProvincia;
            if (prov == 0) {
                prov = document.getElementById('ContentPlaceHolder1_provincia').value
            }
            PageMethods.obtenerTodasLasLocalidadesPorProvincia(prov, onSuccess, onFailure);
        }

        function cargarProvincias(idPais, valorSeleccionado) {

            function onSuccess(result) {
                if (result != null && result.length > 0) {
                    var paises = document.getElementById('ContentPlaceHolder1_provincia');
                    paises.innerHTML = "<select id=\"provincia\" onchange=\"cargarLocalidades()\"><option value=\"0\"></option></select>";
                    for (i = 0; i < result.length; i++) {
                        var opcion = document.createElement('OPTION');
                        opcion.value = result[i].id;
                        var texto = document.createTextNode(result[i].provincia);
                        opcion.appendChild(texto);
                        paises.appendChild(opcion);
                    }
                    paises.value = valorSeleccionado;
                }
            }

            function onFailure(result) {
                alert(result._message);
            }
            var pais = idPais;
            if (pais == 0) {
                pais = document.getElementById('ContentPlaceHolder1_pais').value
            }
            PageMethods.obtenerTodasLasProvinciasPorPais(pais, onSuccess, onFailure);
        }


        function cargarPaises() {

            function onSuccess(result) {
                if (result != null && result.length > 0) {
                    var paises = document.getElementById('ContentPlaceHolder1_pais');
                    paises.innerHTML = "<select id=\"pais\" onchange=\"cargarProvincias();\"><option value=\"0\"></option></select>";
                    for (i = 0; i < result.length; i++) {
                        var opcion = document.createElement('OPTION');
                        opcion.value = result[i].id;
                        var texto = document.createTextNode(result[i].pais);
                        opcion.appendChild(texto);
                        paises.appendChild(opcion);
                    }
                }
            }

            function onFailure(result) {
                alert(result._message);
            }
            PageMethods.obtenerTodosLosPaises(onSuccess, onFailure);
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

        function buscar() {
            PageMethods.buscar(document.getElementById('ContentPlaceHolder1_nombreusuario').value, onSuccess, onFailure);
            function onSuccess(result) {
                usuario = result;
                if (result != null) {
                    document.getElementById('ContentPlaceHolder1_id').value = usuario.id;
                    document.getElementById('ContentPlaceHolder1_nombreusuario').value = usuario.nombre;
                    document.getElementById('ContentPlaceHolder1_nombre').value = usuario.persona.nombre;
                    document.getElementById('ContentPlaceHolder1_apellido').value = usuario.persona.apellido;
                    document.getElementById('ContentPlaceHolder1_documento').value = usuario.persona.documento;
                    document.getElementById('ContentPlaceHolder1_calle').value = usuario.persona.contacto.calle;
                    document.getElementById('ContentPlaceHolder1_nro').value = usuario.persona.contacto.numero;
                    document.getElementById('ContentPlaceHolder1_piso').value = usuario.persona.contacto.piso;
                    document.getElementById('ContentPlaceHolder1_depto').value = usuario.persona.contacto.departamento;
                    document.getElementById('ContentPlaceHolder1_email').value = usuario.persona.contacto.email;
                    document.getElementById('ContentPlaceHolder1_telefono').value = usuario.persona.contacto.telefonos[0].telefono;
                    document.getElementById('ContentPlaceHolder1_tipoTel').value = usuario.persona.contacto.telefonos[0].tipo.id;
                    document.getElementById('ContentPlaceHolder1_tipoDoc').value = usuario.persona.tipoDeDocumento.id;
                    document.getElementById('ContentPlaceHolder1_pais').value = usuario.persona.contacto.localidad.provincia.pais.id;
                    cargarProvincias(usuario.persona.contacto.localidad.provincia.pais.id, usuario.persona.contacto.localidad.provincia.id);
                    cargarLocalidades(usuario.persona.contacto.localidad.provincia.id, usuario.persona.contacto.localidad.id);
                    document.getElementById('ContentPlaceHolder1_bloqueado').checked = usuario.bloqueado;
                    document.getElementById('modificarBtn').disabled = '';

                    var i = 0;
                    var j = 0;
                    while (document.getElementById('ContentPlaceHolder1_chkRoles_' + i) != null && j < usuario.roles.length) {
                        if (document.getElementById('ContentPlaceHolder1_chkRoles_' + i).value == usuario.roles[j].id) {
                            document.getElementById('ContentPlaceHolder1_chkRoles_' + i).checked = true;
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
    <section id="contenedor">
        <div class="row">
            <div class="col-lg-3"><span>Id:</span></div>
            <div class="col-lg-3">
                <input type="text" id="id" runat="server" /></div>
        </div>
        <div class="row">
            <div class="col-lg-3"><span id="spusuario">Nombre de usuario:</span></div>
            <div class="col-lg-3">
                <input type="text" id="nombreusuario" runat="server" /></div>
        </div>
        <div class="row">
            <div class="col-lg-3"><span id="spcontrasenia">Contraseña: </span></div>
            <div class="col-lg-3">
                <input type="password" id="contrasenia" /></div>
        </div>
        <div class="row">
            <div class="col-lg-3"><span id="spcontrasenia2">Repetir contraseña: </span></div>
            <div class="col-lg-3">
                <input type="password" id="contrasenia2" /></div>
        </div>
        <div class="row">
            <div class="col-lg-3"><span id="spbloqueado">Bloqueado: </span></div>
            <div class="col-lg-3">
                <input type="checkbox" id="bloqueado" runat="server"  /></div>
        </div>
        <div class="row">
            <div class="col-lg-3"><span id="spnombre">Nombre: </span></div>
            <div class="col-lg-3">
                <input type="text" id="nombre" runat="server" /></div>
        </div>
        <div class="row">
            <div class="col-lg-3"><span id="spapellido">Apellido: </span></div>
            <div class="col-lg-3">
                <input type="text" id="apellido" runat="server" /></div>
        </div>
        <div class="row">
            <div class="col-lg-3"><span id="spdocumento">Documento: </span></div>
            <div class="col-lg-3">
                <input type="text" id="documento" runat="server" /></div>
        </div>
        <div class="row">
            <div class="col-lg-3"><span id="sptipodoc">Tipo de documento: </span></div>
            <div class="col-lg-3">
                <select id="tipoDoc" runat="server" ></select></div>
        </div>
        <div class="row">
            <div class="col-lg-3"><span id="spcalle">Calle: </span></div>
            <div class="col-lg-3">
                <input type="text" id="calle" runat="server"  /></div>
        </div>
        <div class="row">
            <div class="col-lg-3"><span id="spnro">Nro.: </span></div>
            <div class="col-lg-3">
                <input type="text" id="nro" runat="server"  value="0"/></div>
        </div>
        <div class="row">
            <div class="col-lg-3"><span id="sppiso">Piso: </span></div>
            <div class="col-lg-3">
                <input type="text" id="piso" runat="server" value="0"/></div>
        </div>
        <div class="row">
            <div class="col-lg-3"><span id="spdepto">Departamento: </span></div>
            <div class="col-lg-3">
                <input type="text" id="depto" runat="server" /></div>
        </div>
        <div class="row">
            <div class="col-lg-3"><span id="sppais">Pais: </span></div>
            <div class="col-lg-3">
                <select id="pais" onchange="cargarProvincias(0);" runat="server" ><option value="0"></option></select></div>
        </div>
        <div class="row">
            <div class="col-lg-3"><span id="spprovincia">Provincia: </span></div>
            <div class="col-lg-3">
                <select id="provincia" onchange="cargarLocalidades(0);" runat="server" ><option value="0"></option></select></div>
        </div>
        <div class="row">
            <div class="col-lg-3"><span id="splocalidad" >Localidad: </span></div>
            <div class="col-lg-3">
                <select id="localidad" runat="server" ><option value="0"></option></select></div>
        </div>
        <div class="row">
            <div class="col-lg-3"><span id="spemail">E-mail: </span></div>
            <div class="col-lg-3">
                <input type="text" id="email" runat="server" /></div>
        </div>
        <div class="row">
            <div class="col-lg-3"><span id="sptelefono">Telefono: </span></div>
            <div class="col-lg-3">
                <input type="text" id="telefono" runat="server" /></div>
        </div>
        <div class="row">
            <div class="col-lg-3"><span id="sptipotel" >Tipo de Telefono: </span></div>
            <div class="col-lg-3">
                <select id="tipoTel" runat="server" ></select></div>
        </div>
        <div class="row">
            <div class="col-lg-3">
                <input type="button" id="crearBtn" value="Crear" onclick="crear()" /></div>
            <div class="col-lg-3">
                <input type="button" id="eliminarBtn" value="Eliminar" onclick="eliminar()" /></div>
            <div class="col-lg-3">
                <input type="button" id="modificarBtn" value="Guardar" disabled="disabled" onclick="guardar()" /></div>
        </div>
        <div class="row">
            <div class="col-lg-3">
                <input type="button" id="limpiarBtn" value="Limpiar" onclick="limpiar()" /></div>
            <div class="col-lg-3">
                <input type="button" id="buscarBtn" value="Buscar" onclick="buscar()" /></div>
        </div>
        <p />
        <div id="conetenedorAsignacion" class="row">
            <h3 id="hindicadorroles">Roles</h3>
            <div id="contenedorRoles">
            </div>
            <asp:CheckBoxList ID="chkRoles"  runat="server"></asp:CheckBoxList>
        </div>
        <div class ="row">
            <h3 id="hindicadorusuariosexistentes">Usuarios existentes</h3>
            <asp:ListBox ID="LstUsuarios" runat="server" AutoPostBack="True"></asp:ListBox>
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
