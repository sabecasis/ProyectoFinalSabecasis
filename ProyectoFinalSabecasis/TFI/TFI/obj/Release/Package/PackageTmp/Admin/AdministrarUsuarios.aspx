<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Admin/admin.Master" CodeBehind="AdministrarUsuarios.aspx.vb" Inherits="TFI.AdministrarUsuarios" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../scripts/abmscripts.js"></script>
    <script type="text/javascript" >
        function limpiar() {
            limpiarForm();
            cargarRoles();
            deshabilitar();
        }

        function guardar() {
            var usuario;
            var arrayRoles = document.getElementsByName('roles');
            var arrayRolesAGuardar = new Array();
            var j = 0;
            for (i = 0; i < arrayRoles.length; i++) {
                if (arrayRoles[i].checked == true) {
                    arrayRolesAGuardar[j] = { id: arrayRoles[i].value, nombre: '' };
                    j++;
                }
            }
            var telefono = { id:0, telefono:document.getElementById('telefono').value, tipo: {id: document.getElementById('tipoTel').value, tipo:''}}
            var oContacto = {
                id: 0,
                calle: document.getElementById('calle').value,
                numero: document.getElementById('nro').value,
                piso: document.getElementById('piso').value == '' ? 0 : document.getElementById('piso').value,
                departamento: document.getElementById('depto').value,
                email: document.getElementById('email').value,
                telefonos:[telefono],
                localidad:{id:document.getElementById('localidad').value, cp:'', localidad:''}
            }
            var oPersona = {
                id:0,
                nombre: document.getElementById('nombre').value,
                apellido: document.getElementById('apellido').value,
                documento: document.getElementById('documento').value,
                tipoDeDocumento:{
                    id: document.getElementById('tipoDoc').value,
                    tipoDeDocumento:''
                },
                contacto: oContacto
            }

            usuario = {
                id: document.getElementById('id').value,
                nombre: document.getElementById('nombreusuario').value,
                password: document.getElementById('contrasenia').value,
                persona: oPersona,
                roles: arrayRolesAGuardar,
                bloqueado: document.getElementById('bloqueado').checked
            }
            function onSuccess(result) {
                alert(result);
                document.getElementById('modificarBtn').disabled = 'disabled';
            }
            function onFailure(result) {
                alert(result);
            }
            
            PageMethods.guardar(usuario, onSuccess, onFailure);
            
            document.getElementById('id').disabled = 'disabled';
        }

        function eliminar() {
            PageMethods.eliminar(document.getElementById('id').value, onSuccess, onFailure);
            function onSuccess(result) {
                alert(result);
                document.getElementById('modificarBtn').disabled = 'disabled';
            }
            function onFailure(result) {
                alert(result);
            }
        }

        function cargarRoles() {
            
            function onSuccessRoles(result) {
                var contenedor = document.getElementById('contenedorRoles');
                while (contenedor.firstChild) {
                    contenedor.removeChild(contenedor.firstChild);
                }
                var ul = document.createElement('UL');
                contenedor.appendChild(ul);
                if (result != null && result.length > 0) {
                    for (i = 0; i < result.length; i++) {
                        var li = document.createElement('LI');
                        var elemento = document.createElement('INPUT');
                        elemento.type = 'CHECKBOX';
                        elemento.value = result[i].id;
                        elemento.name = 'roles';
                        var texto = document.createElement('SPAN');
                        texto.appendChild(document.createTextNode(result[i].nombre));
                        li.appendChild(texto);
                        li.appendChild(elemento);
                        ul.appendChild(li);
                    }
                }
            }

            function onFailure(result) { }
            PageMethods.obtenerTodosLosRoles(onSuccessRoles, onFailure)
        }


        function cargarTipsosTel() {

            function onSuccess(result) {
                if (result != null && result.length > 0) {
                    var paises = document.getElementById('tipoTel');
                    paises.innerHTML = "<select id=\"tipoTel\"><option value=\"0\">Seleccionar</option></select>";
                    for (i = 0; i < result.length; i++) {
                        var opcion = document.createElement('OPTION');
                        opcion.value = result[i].id;
                        var texto = document.createTextNode(result[i].tipo);
                        opcion.appendChild(texto);
                        paises.appendChild(opcion);
                    }
                }
            }

            function onFailure(result) {
            }
            PageMethods.obtenerTodosLosTiposDeTelefono(onSuccess, onFailure);
        }


        function cargarTipsoDoc() {

            function onSuccess(result) {
                if (result != null && result.length > 0) {
                    var paises = document.getElementById('tipoDoc');
                    paises.innerHTML = "<select id=\"tipoDoc\"><option value=\"0\">Seleccionar</option></select>";
                    for (i = 0; i < result.length; i++) {
                        var opcion = document.createElement('OPTION');
                        opcion.value = result[i].id;
                        var texto = document.createTextNode(result[i].tipo);
                        opcion.appendChild(texto);
                        paises.appendChild(opcion);
                    }
                }
            }

            function onFailure(result) {
            }
            PageMethods.obtenerTodosLosTiposDeDocumento(onSuccess, onFailure);
        }


        function cargarLocalidades(idProvincia, valorSeleccionado) {

            function onSuccess(result) {
                if (result != null && result.length > 0) {
                    var paises = document.getElementById('localidad');
                    paises.innerHTML = "<select id=\"localidad\"><option value=\"0\">Seleccionar</option></select>";
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
            }
            var prov = idProvincia;
            if (prov == 0) {
                prov = document.getElementById('provincia').value
            }
            PageMethods.obtenerTodasLasLocalidadesPorProvincia(prov, onSuccess, onFailure);
        }

        function cargarProvincias(idPais, valorSeleccionado) {

            function onSuccess(result) {
                if (result != null && result.length > 0) {
                    var paises = document.getElementById('provincia');
                    paises.innerHTML = "<select id=\"provincia\" onchange=\"cargarLocalidades()\"><option value=\"0\">Seleccionar</option></select>";
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
            }
            var pais = idPais;
            if (pais == 0) {
                pais = document.getElementById('pais').value
            }
            PageMethods.obtenerTodasLasProvinciasPorPais(pais, onSuccess, onFailure);
        }


        function cargarPaises() {

            function onSuccess(result) {
                if (result!=null &&  result.length > 0) {
                    var paises = document.getElementById('pais');
                    paises.innerHTML = "<select id=\"pais\" onchange=\"cargarProvincias();\"><option value=\"0\">Seleccionar</option></select>";
                    for(i=0;i<result.length;i++){
                        var opcion = document.createElement('OPTION');
                        opcion.value=result[i].id;
                        var texto = document.createTextNode(result[i].pais);
                        opcion.appendChild(texto);
                        paises.appendChild(opcion);
                    }
                }
            }

            function onFailure(result) {
            }
            PageMethods.obtenerTodosLosPaises(onSuccess, onFailure);
        }

        window.onload = function () {
            cargarRoles();
            cargarPaises();
            cargarTipsoDoc();
            cargarTipsosTel();
        }

        function crear() {
            function onSuccess(result) {
                document.getElementById('id').value = result;
            }
            function onFailure(result) {
                alert(result.get_message);
            }
            PageMethods.obtenerProximoId(onSuccess, onFailure);
            document.getElementById('id').disabled = 'disabled';
            document.getElementById('modificarBtn').disabled = '';           
        }

        function buscar() {
            PageMethods.buscar(document.getElementById('nombreusuario').value, onSuccess, onFailure);
            function onSuccess(result) {
               var usuario=result

               document.getElementById('id').value = usuario.id;
               document.getElementById('nombreusuario').value = usuario.nombre;
               document.getElementById('contrasenia').value = '';
               document.getElementById('contrasenia2').value = '';
               document.getElementById('nombre').value = usuario.persona.nombre;
               document.getElementById('apellido').value = usuario.persona.apellido;
               document.getElementById('documento').value = usuario.persona.documento;
               document.getElementById('calle').value = usuario.persona.contacto.calle;
               document.getElementById('nro').value = usuario.persona.contacto.numero;
               document.getElementById('piso').value = usuario.persona.contacto.piso;
               document.getElementById('depto').value = usuario.persona.contacto.departamento;
               document.getElementById('email').value = usuario.persona.contacto.email;
               document.getElementById('telefono').value = usuario.persona.contacto.telefonos[0].telefono;
               document.getElementById('tipoTel').value = usuario.persona.contacto.telefonos[0].tipo.id;
               document.getElementById('tipoDoc').value = usuario.persona.tipoDeDocumento.id;
               document.getElementById('pais').value = usuario.persona.contacto.localidad.provincia.pais.id;
               cargarProvincias(usuario.persona.contacto.localidad.provincia.pais.id, usuario.persona.contacto.localidad.provincia.id);
               cargarLocalidades(usuario.persona.contacto.localidad.provincia.id, usuario.persona.contacto.localidad.id);
               document.getElementById('bloqueado').checked = usuario.bloqueado;
                document.getElementById('modificarBtn').disabled = '';

                var roles = document.getElementsByName('roles');
                for (j = 0; j < roles.length; j++) {
                    for (i = 0; i < usuario.roles.length; i++) {
                        if (roles[j].value == usuario.roles[i].id) {
                            roles[j].checked = true;
                        }
                    }

                }
            }
            function onFailure(result) {
                alert(result);
            }
        }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <aside id="lateral"></aside>
    <section id="contenedor">
        <div id="2barra"></div>
        <span>Id:</span><input type="text" id="id" />
        <p />
        <span id="spusuario">Nombre de usuario:</span><input type="text" id="nombreusuario" />
        <p />
        <span id="spcontrasenia">Contraseña: </span><input type="password" id="contrasenia" />
        <p/>
         <span id="spcontrasenia2">Repetir contraseña: </span><input type="password" id="contrasenia2" />
        <p/>
         <span id="spbloqueado">Bloqueado: </span><input type="checkbox" id="bloqueado" />
        <p/>
        <span id="spnombre">Nombre: </span><input type="text" id="nombre" />
        <p/>
        <span id="spapellido">Apellido: </span><input type="text" id="apellido" />
        <p/>
         <span id="spdocumento">Documento: </span><input type="text" id="documento" />
        <p/>
          <span id="sptipodoc">Tipo de documento: </span><select id="tipoDoc" ></select>
        <p/>
         <span id="spcalle">Calle: </span><input type="text" id="calle" />
        <p/>
         <span id="spnro">Nro.: </span><input type="text" id="nro" />
        <p/>
         <span id="sppiso">Piso: </span><input type="text" id="piso" />
        <p/>
         <span id="spdepto">Departamento: </span><input type="text" id="depto" />
        <p/>
         <span id="sppais">Pais: </span><select id="pais" onchange="cargarProvincias(0);"></select>
        <p/>
         <span id="spprovincia">Provincia: </span><select id="provincia" onchange="cargarLocalidades(0);"></select>
        <p/>
         <span id="splocalidad">Localidad: </span><select id="localidad" ></select>
        <p/>
         <span id="spemail">E-mail: </span><input type="text" id="email" />
        <p/>
         <span id="sptelefono">Telefono: </span><input type="text" id="telefono" />
        <p/>
         <span id="sptipotel">Tipo de Telefono: </span><select id="tipoTel" ></select>
        <p/>
        <input type="button" id="crearBtn" value="Crear" onclick="crear()" />
        <input type="button" id="eliminarBtn" value="Eliminar" onclick="eliminar()" />
        <input type="button" id="modificarBtn" value="Guardar" disabled="disabled" onclick="guardar()" />
        <input type="button" id="limpiarBtn" value="Limpiar" onclick="limpiar()" />
        <input type="button" id="buscarBtn" value="Buscar" onclick="buscar()" />
        <p />
        <div id="3barra"></div>
        <div id="conetenedorAsignacion">
            <span>Roles</span>
            <div id="contenedorRoles">

            </div>
        </div>
    </section>
</asp:Content>
