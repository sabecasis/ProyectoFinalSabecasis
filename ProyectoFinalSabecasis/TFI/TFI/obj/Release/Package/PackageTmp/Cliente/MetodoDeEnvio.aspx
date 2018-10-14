<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/ConsumidorFinal.Master" CodeBehind="MetodoDeEnvio.aspx.vb" Inherits="TFI.MetodoDeEnvio" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
      <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <meta name="description" content="" />
    <meta name="author" content="" />
  

  
    <link href="../static/shop-homepage.css" rel="stylesheet" />
    <script src="../scripts/jquery.js"></script>
    <script src="../scripts/bootstrap.min.js"></script>
     <link href="../static/font-awesome/css/font-awesome.min.css" rel="stylesheet" type="text/css" />
      <link href="../static/bootstrap.min.css" rel="stylesheet" />
      <script>

        function obtenerPrecio() {
            function onSuccess(result) {
                document.getElementById('precio').value = result;
            }

            function onFailure(result) { }
            var id = this.id;
           //TODO, discriminar calculo de costo segun tipo de envio
            PageMethods.obtenerPrecioDeEnvio(id, onSuccess, onFailure);
        }

        function obtenerTiposDeEnvio() {
            function onSuccess(result) {
                var cont = document.getElementById('contenido');
                if (result != null) {
                    var tabla = document.createElement('TABLE');
                    tabla.className = 'table table-bordered table-hover';
                    cont.appendChild(tabla);
                    for (i = 0; i < result.length; i++) {
                        var tr = document.createElement('TR');
                        var td1 = document.createElement('TD');
                        td1.appendChild(document.createTextNode(result[i].tipo));
                        tr.appendChild(td1);
                        var td2 = document.createElement('TD');
                        var radio = document.createElement('input');
                        radio.type = 'RADIO';
                        radio.id = result[i].id;
                        radio.onclick = obtenerPrecio;
                        radio.name = 'tipoDeEnvio';
                        radio.value = result[i].id;
                        td2.appendChild(radio);
                        tr.appendChild(td2);
                        tabla.appendChild(tr);
                    }
                }
            }
            function onFailure(result) { }
            PageMethods.obtenerTiposDeEnvio(onSuccess, onFailure);
        }

        window.onload = function () {
            obtenerTiposDeEnvio();
            cargarPaises();
            cargarTipsosTel();
        }

        function obtenerInformacionDeContacto() {
            function onSuccess(result) {
                if (result != null) {
                    document.getElementById('calle').value = result.calle;
                    document.getElementById('nro').value = result.numero;
                    document.getElementById('piso').value = result.piso;
                    document.getElementById('depto').value = result.departamento;
                    document.getElementById('email').value = result.email;
                    document.getElementById('telefono').value = result.telefonos[0].telefono;
                    document.getElementById('tipoTel').value = result.telefonos[0].tipo.id;
                    document.getElementById('pais').value = result.localidad.provincia.pais.id;
                    cargarProvincias(result.localidad.provincia.pais.id, result.localidad.provincia.id);
                    cargarLocalidades(result.localidad.provincia.id, result.localidad.id);
                }
            }
            function onFailure(result) { }
            PageMethods.obtenerInformacionDeContacto(document.getElementById('ContentPlaceHolder1_idContacto').value, onSuccess, onFailure);
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
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div id="background">
        <img src="../static/fondo11.jpg" />
    </div>
      <nav class="navbar navbar-inverse navbar-fixed-top" role="navigation">
        <div class="container">
            <div class="navbar-header page-scroll">
                <button type="button" class="navbar-toggle" data-toggle="collapse" data-target="#bs-example-navbar-collapse-1">
                    <span class="sr-only">Toggle navigation</span>
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                </button>
                <a class="navbar-brand" href="/Cliente/Inicio.aspx">
                    <img class="dooba-img" src="../static/logotipo-trans.png" /></a>
            </div>

            <div class="collapse navbar-collapse" >
                <ul class="nav navbar-nav navbar-right" id="menuHorizontal">
                    <li>
                        <a href="/Cliente/IniciarSesion.aspx" id ="alogin">Iniciar sesión</a>
                    </li>
                    <li>
                        <a href="/Cliente/Inicio.aspx">Inicio</a>
                    </li>
                    <li>
                        <a href="#" data-target="#idioma" data-toggle="modal" id="acambiaridioma">Cambiar idioma</a>
                    </li>
                    <li>
                        <a href="/Cliente/Catalogo.aspx" id="acatalogo" >Catálogo</a>
                    </li>
                    <li>
                        <a href="/Cliente/Ayuda.aspx"  id="aayuda">Ayuda</a>
                    </li>
                </ul>
            </div>
        </div>  
    </nav>
    <section >
        <input type="hidden" id="accion" name="accion" />
        <input type="hidden" id="idContacto" name="idConctacto" runat="server" />
        <div id="contenedor">
        <div id="contenido">

        </div>
        <span id="spprecio">Precio</span><input type="text" disabled="disabled" id="precio"  name="precio" />
        <input type="checkbox" id="infoPerfil" name="infoPerfil" onclick="obtenerInformacionDeContacto()" /><span id="spusarinfo">Usar información de contacto de mi perfil</span>
        <p />
        <div>
            <span id="spcalle">Calle: </span><input type="text" id="calle" name="calle"/>
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
         <span id="spemail">E-mail: </span><input type="text" id="email" name="email"/>
        <p/>
         <span id="sptelefono">Telefono: </span><input type="text" id="telefono" name="telefono"/>
        <p/>
         <span id="sptipotel">Tipo de Telefono: </span><select id="tipoTel" name="tipoTel" ></select>
        <p/>
        </div>
         <p />
        <input type="submit" value="Seleccionar" id="seleccionarBtn" name="seleccionarBtn" onclick="setAccion(this.id)"/>
        <p />
         </div>
    </section>
</asp:Content>
