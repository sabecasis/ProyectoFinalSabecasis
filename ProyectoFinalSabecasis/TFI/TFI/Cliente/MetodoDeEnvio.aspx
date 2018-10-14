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
     <link href="../static/font-awesome/css/bootstrap.min.css" rel="stylesheet" />
     <link href="../static/font-awesome/css/bootstrap.min.css" rel="stylesheet" />
      <script>

        function obtenerTiposDeEnvio() {
            function onSuccess(result) {
                var cont = document.getElementById('contenido');
                if (result != null) {
                    var tabla = document.createElement('select');
                    tabla.id = 'tipoDeEnvio';
                    tabla.name = 'tipoDeEnvio';
                    tabla.onchange = determinarAccionSegunTipoDeEnvio;
                    tabla.onload = determinarAccionSegunTipoDeEnvio;
                    cont.appendChild(tabla);
                    for (i = 0; i < result.length; i++) {
                        var td1 = document.createElement('option');
                        td1.appendChild(document.createTextNode(result[i].tipo));
                        td1.value = result[i].id;
                        tabla.appendChild(td1);
                    }
                }
            }
            function onFailure(result) {
                alert(result._message);
            }
            PageMethods.obtenerTiposDeEnvio(onSuccess, onFailure);
        }

        window.onload = function () {
            obtenerTiposDeEnvio();
            cargarPaises();
            cargarTipsosTel();
            obtenerIdCheckout();
        }

        function obtenerIdCheckout() {
            var loc = document.location.href;
            var getString = loc.split('?')[1];
            idCheckout = getString.split('=')[1];
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
            function onFailure(result) {
                alert(result._message);
            }
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
                    document.getElementById('localidad').onchange();
                }
            }

            function onFailure(result) {
                alert(result._message);
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
                alert(result._message);
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
                alert(result._message);
            }
            PageMethods.obtenerTodosLosPaises(onSuccess, onFailure);
        }

        function determinarAccionSegunTipoDeEnvio() {
            var tipo = document.getElementById('tipoDeEnvio');
            if (tipo.value == 1) {
                obtenerPrecio();
                document.getElementById('contenedorocasucursal').style.visibility = 'hidden';
                document.getElementById('continuarBtn').style.visibility = 'visible';
                document.getElementById('contenedordireccion').style.visibility = 'hidden';
            } else if (tipo.value == 2) {
                document.getElementById('continuarBtn').style.visibility = 'hidden';
                document.getElementById('contenedorocasucursal').style.visibility = 'visible';
                document.getElementById('contenedordireccion').style.visibility = 'hidden';
            } else if (tipo.value == 3) {
                document.getElementById('continuarBtn').style.visibility = 'hidden';
                document.getElementById('contenedordireccion').style.visibility = 'visible';
                document.getElementById('contenedorocasucursal').style.visibility = 'hidden';
            }
        }

        function buscarSucursales() {
            function onSuccess(result) {
                if (result != null && result.length > 0) {
                    var select = document.getElementById('sucursaloca');
                    var largo = select.length;
                    for (i = 0; i <largo; i++) {
                        select.remove(i);
                    }
                    for (j = 0; j < result.length; j++) {
                        var opt = document.createElement('option');
                        opt.value = result[j].Key;
                        opt.appendChild(document.createTextNode(result[j].Value));
                        select.appendChild(opt);
                    }
                    select.onchange();
                }
                
            }
            function onFailure(result) {
                alert(result._message);
            }
            PageMethods.obtenerSucursalesDeOca(document.getElementById('codigopostal').value, onSuccess, onFailure)
        }

        function obtenerPrecio() {
            function onSuccess(result) {
                document.getElementById('precio').value = result;
                document.getElementById('continuarBtn').style.visibility = 'visible';
            }
            function onFailure(result) {
                alert(result._message);
            }
            var cp = null;
            var tipoEnvio = document.getElementById('tipoDeEnvio').value;
            if (tipoEnvio == 1) {
                document.getElementById('precio').value = 0;
            } else if (tipoEnvio == 2 ) {
                cp = document.getElementById('codigopostal').value;
                PageMethods.obtenerPrecioEnvioPorCp(cp, idCheckout, onSuccess, onFailure);
            } else if (tipoEnvio == 3) {
                var idLocalidad = document.getElementById('localidad').value;
                PageMethods.obtenerPrecioEnvioPorLocalidad(idLocalidad, idCheckout, onSuccess, onFailure);
            }
             
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
                alert(result._message);
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
                <a class="navbar-brand" href="/Cliente/Inicio.aspx">
                    <img class="dooba-img" src="../static/logotipo-trans.png" id="logo" /></a>
            </div>

            <div class="collapse navbar-collapse" >
                <ul class="nav navbar-nav navbar-right" id="menuHorizontal">
                     <li class="hidden">
                        <a href="#page-top"></a>
                    </li>
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
                     <li>
                        <a href="/Cliente/NovedadesPublicas.aspx" id="aNovedadesPublicas">Novedades</a>
                    </li>
                </ul>
                 <div class="enlinea espaciado-izquierdo-breve espaciado-suerior" style="visibility:hidden"  id="user_tag"><i class="glyphicon glyphicon-user logo-usuario"></i><a href="/Cliente/Perfil.aspx" id="aPerfil" runat="server" class="usuario espaciado-izquierdo-breve"></a> </div>
            </div>
        </div>
          <div id="breadcrums" runat="server" class="breadcrums">

        </div>   
    </nav>
    <section >
        <input type="hidden" id="accion" name="accion" />
        <input type="hidden" id="idContacto" name="idConctacto" runat="server" />
        <div id="contenedor">
        <div class="mensaje-respuesta-contacto" id="LblMensaje" runat="server"></div>
            <p />
            <div id="contenido">

        </div>
        <span id="spprecio">Precio</span><input type="text" readonly="readonly" id="precio" value="0"  name="precio" />
          <p />
        <input type="submit" value="Continuar" id="continuarBtn" name="continuarBtn" onclick="setAccion(this.id)"/>
            <p />

            <div id="contenedorocasucursal" style="visibility:hidden;">
                <div id="indicaciones" >
                    Ingrese el código postal y luego seleccione la sucursal de OCA donde desea retirar el producto.
                </div>
                <span id="spcp">Código postal: </span><input id="codigopostal" name="codigopostal"/><input type="button" onclick="buscarSucursales()" value="Buscar"/>
                <p />
                <span id="spsucursaloca">Sucursal OCA: </span> <select id="sucursaloca" name="sucursaloca" onchange="obtenerPrecio()"></select>
                <p />
           </div>
        <div id="contenedordireccion" style="visibility:hidden;">
             <p />
            <input type="checkbox" id="infoPerfil" name="infoPerfil" onclick="obtenerInformacionDeContacto()" /><span id="spusarinfo">Usar información de contacto de mi perfil</span>
            <p />
            <span id="spcalle">Calle: </span><input type="text" id="calle" name="calle"/>
        <p/>
         <span id="spnro">Nro.: </span><input type="text" id="nro"  name="nro" />
        <p/>
         <span id="sppiso">Piso: </span><input type="text" id="piso"  name="piso"/>
        <p/>
         <span id="spdepto">Departamento: </span><input type="text"  id="depto" name="depto"/>
        <p/>
         <span id="sppais">Pais: </span><select id="pais" onchange="cargarProvincias(0);"  name="pais"></select>
        <p/>
         <span id="spprovincia">Provincia: </span><select id="provincia"  onchange="cargarLocalidades(0);" name="provincia"></select>
        <p/>
         <span id="splocalidad">Localidad: </span><select id="localidad"  name="localidad" onchange="obtenerPrecio()" ></select>
        <p/>
         <span id="spemail">E-mail: </span><input type="text" id="email"  name="email"/>
        <p/>
         <span id="sptelefono">Telefono: </span><input type="text"  id="telefono" name="telefono"/>
        <p/>
         <span id="sptipotel">Tipo de Telefono: </span><select id="tipoTel"  name="tipoTel" ></select>
        <p/>
        </div>
       
        <p />
         </div>
    </section>
</asp:Content>
