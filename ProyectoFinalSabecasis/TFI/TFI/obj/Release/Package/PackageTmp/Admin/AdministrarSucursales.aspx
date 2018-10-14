<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Admin/admin.Master" CodeBehind="AdministrarSucursales.aspx.vb" Inherits="TFI.AdministrarSucursales" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        window.onload = function () {
            function onSuccessEstado(result) {
                var selectEstado = document.getElementById('estado')
                var option;
                if (result != null) {
                    for (i = 0; i < result.length; i++) {
                        option = document.createElement('OPTION');
                        option.value = result[i].id;
                        option.text = result[i].estado;
                        selectEstado.appendChild(option);
                    }
                }
            }
            function onFailure(result) {
            }
            PageMethods.obtenerEstadosDeSucursal(onSuccessEstado, onFailure);
            cargarPaises();
            cargarTipsosTel();
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
                if (result != null && result.length > 0) {
                    var paises = document.getElementById('pais');
                    paises.innerHTML = "<select id=\"pais\" onchange=\"cargarProvincias();\"><option value=\"0\">Seleccionar</option></select>";
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
            }
            PageMethods.obtenerTodosLosPaises(onSuccess, onFailure);
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

        function crear() {
            function onSuccess(result) {
                if (result != null) {
                    document.getElementById('id').value = result;
                }
            }
            function onFailure(result) { }
            PageMethods.obtenerId(onSuccess, onFailure);
        }

        function buscar() {
            function onSuccess(result){
                if (resulta != null) {
                    document.getElementById('id').value=result.nroSucursal;
                    document.getElementById('nombre').value=result.nombre;
                    document.getElementById('calle').value=result.contacto.calle;
                    document.getElementById('piso').value=result.contacto.piso;
                    document.getElementById('depto').value=result.contacto.departamento;
                    document.getElementById('nro').value=result.contacto.numero;
                    document.getElementById('email').value=result.contacto.email;
                    document.getElementById('telefono').value = result.contacto.telefono;
                    var tiposTel = document.getElementById('tipoTel');
                    for (i = 0; i < tiposTel.length; i++) {
                        if (result.telefono.tipo.id == tiposTel.value) {
                            tiposTel.selectedIndex = i;
                        }
                    }
                    var paises = document.getElementById('pais');
                    for (i = 0; i < paises.length; i++) {
                        if (result.localidad.provincia.pais.id == paises.value) {
                            paises.selectedIndex = i;
                        }
                    }
                    cargarProvincias(result.localidad.provincia.pais.id, result.localidad.provincia.id);
                    cargarLocalidades(result.localidad.provincia.id, result.localidad.id);
                    var estados = document.getElementById('estado');
                    for (i = 0; i < estados.length; i++) {
                        if (result.estado.id == estados.value) {
                            estados.selectedIndex = i;
                        }
                    }
                }
            }
            function onFailure(result) { }
            PageMethods.buscar({ nroSucursal: document.getElementById('id').value, nombre: document.getElementById('nombre').value });
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <aside id="lateral"></aside>
    <section id="contenedor">
        <div id="2barra"></div>
        <input type="hidden" id="accion" name="accion" />
        <span id="spid">Id:</span><input type="text" id="id" name="id" />
        <p />
        <span id="spnombre">Nombre:</span><input type="text" id="nombre" name="nombre"  />
        <p />
          <span id="spcalle">Calle: </span><input type="text" id="calle" name="calle" />
        <p/>
         <span id="spnro">Nro.: </span><input type="text" id="nro" name="nro" />
        <p/>
         <span id="sppiso">Piso: </span><input type="text" id="piso" name="piso"/>
        <p/>
         <span id="spdepto">Departamento: </span><input type="text" id="depto" name="depto"/>
        <p/>
         <span id="sppais">Pais: </span><select id="pais" onchange="cargarProvincias(0);" name="pais"></select>
        <p/>
         <span id="spprovincia">Provincia: </span><select id="provincia" onchange="cargarLocalidades(0);" name="provincia"></select>
        <p/>
         <span id="splocalidad">Localidad: </span><select id="localidad" name="localidad" ></select>
        <p/>
         <span id="spemail">E-mail: </span><input type="text" id="email" name="email" />
        <p/>
         <span id="sptelefono">Telefono: </span><input type="text" id="telefono" name="telefono"/>
        <p/>
         <span id="sptipotel">Tipo de Telefono: </span><select id="tipoTel" name="tipoTel" ></select>
        <p/>
          <span id="spestado">Estado:</span><select id="estado" name="estado" name="estado"></select>
        <p />
        <input type="button" id="eliminarBtn" value="Eliminar" onclick="setAccion(this.id)" />
        <input type="submit" id="modificarBtn" value="Guardar" onclick="setAccion(this.id)"/>
        <input type="button" id="crearBtn" value="Crear" onclick="crear()" />
         <input type="button" id="limpiarBtn" value="Limpiar" onclick="limpiarForm()"/>
        <input type="button" id="buscarBtn" value="Buscar" onclick="buscar()" />
        <p />
    </section>
</asp:Content>
