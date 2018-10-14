<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Admin/admin.Master" CodeBehind="AdministrarIdiomas.aspx.vb" Inherits="TFI.AdministrarIdiomas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../scripts/abmscripts.js"></script>
    <script type="text/javascript">
           
        function guardar() {
            var idIdioma=document.getElementById('id').value;
            var elementos = new Array();
            var textos = document.getElementsByName('elementos');
            var idiomas =document.getElementById('idiomas');
            for (i = 0; i < textos.length; i++) {
                elementos[i] = {
                    idioma: {
                        id: idIdioma,
                        descripcion: '',
                        nombre:'',
                        elementos: {}
                    },
                    texto: textos[i].value.replace(/\n/, '<br/>'),
                    elemento: {
                        id: textos[i].id,
                        nombre:''
                    }
                }
            }
            var idioma = {
                id: idIdioma,
                nombre: idiomas.options[idiomas.selectedIndex].text,
                descripcion: document.getElementById('descripcion').value,
                elementos: elementos
            }
            function onSuccess(result) {
                alert(result);
            }
            function onFailure(result) {
                alert(result);
            }
            PageMethods.guardar(idioma, onSuccess, onFailure);
        }

        function eliminar() {
            PageMethods.eliminar(document.getElementById('id').value, onSuccess, onFailure);
            function onSuccess(result) {
                alert(result);
            }
            function onFailure(result) {
                alert(result);
            }
        }
       
        window.onload = function () {
            cargarIdiomas();
            document.getElementById('id').value = 0;
            document.getElementById('id').disabled = 'disabled';
            crearPaginado();
        }

        function cambiarInputATextArea(id) {
            var currentId = id.srcElement!=null?this.id.substring(1, this.id.length):id;
            var elemento = document.getElementById(currentId);
            var td = elemento.parentElement;
            td.removeChild(elemento);
            var nuevoElemento = document.createElement('textarea');
            nuevoElemento.name = 'elementos';
            nuevoElemento.id = currentId;
            td.appendChild(nuevoElemento);
            var currentElement = document.getElementById('c' + currentId);
            var padre = currentElement.parentElement;
            padre.removeChild(currentElement);
        }

        function cargarElementos(ident) {
            var hasta = 20;
            if (ident==null || ident != 1) {
                var hasta = this.id * 20;
            }
            var desde = hasta - 19;
            if (total < 20) {
                desde = 1;
            }

            function onSuccess(result) {
                if (result != null && result.length > 0) {
                    var contenedor = document.getElementById('contenedorElementos');
                    contenedor.innerHTML = "<div id=\"contenedorElementos\" ></div>";
                    var tabla = document.createElement('TABLE');
                    for (i = 0; i < result.length; i++) {
                        var tr = document.createElement('TR');
                        var td1 = document.createElement('TD');
                        var texto = document.createTextNode(result[i].nombre);
                        td1.appendChild(texto);
                        var input = document.createElement('INPUT');
                        input.type = 'text';
                        input.id = result[i].id;
                        input.name = 'elementos';
                        var td2 = document.createElement('TD');
                        td2.appendChild(input);
                        var td3 = document.createElement('TD');
                        var cambiar = document.createElement('SPAN');
                        cambiar.id = 'c' + result[i].id;
                        cambiar.onclick = cambiarInputATextArea;
                        var texto2 = document.createTextNode('+');
                        cambiar.appendChild(texto2);
                        td3.appendChild(cambiar);
                        tr.appendChild(td1);
                        tr.appendChild(td2);
                        tr.appendChild(td3);
                        tabla.appendChild(tr);
                    }
                    contenedor.appendChild(tabla);
                    cargarElementosDeIdioma();
                }
                
            }
            function onFailure(result) {

            }

            PageMethods.obtenerTodosLosElementosPaginados(desde, hasta, onSuccess, onFailure);
        }

        function crearPaginado() {
            function onSuccess(result) {

                cantidad = result / 20;
                total = result;
                cantidad = ~~(cantidad);
                var resto = result % 20;
                if (result < 20) {
                    resto = 0;
                    cantidad = 1;
                }
                if (resto != 0) {
                    cantidad += 1;
                }
                var cuenta = 1;
                for (i = 0; i < cantidad; i++) {
                    var link = document.createElement('SPAN');
                    link.onclick = cargarElementos;
                    link.id = i + 1;
                    texto = document.createTextNode(i+1);
                    link.appendChild(texto);
                    document.getElementById('contenedorPaginado').appendChild(link);
                }

                cargarElementos(1);
            }
            function onFailure(result) { }
            PageMethods.obtenerCantidadDeElementos(onSuccess, onFailure);
        }

        function cargarElementosDeIdioma() {
            var idiomaId = document.getElementById('idiomas').value == '' ? 0 : document.getElementById('idiomas').value;
            function onSuccess(resultado) {
                if (resultado != null && resultado.length > 0) {
                    var inputs = document.getElementsByName('elementos');
                    for (j = 0; j < inputs.length; j++) {
                        for (i = 0; i < resultado.length; i++) {
                            if (inputs[j].id == resultado[i].elemento.id) {
                                if (resultado[i].texto.length > 100) {
                                    var id = inputs[j].id;
                                    cambiarInputATextArea(inputs[j].id);
                                    document.getElementById(id).value = resultado[i].texto;
                                } else {
                                    inputs[j].value = resultado[i].texto;
                                }
                                break;
                            }
                        }
                    }
                } 
            }

            function onFailure(result) {
                alert(result)
            }
            PageMethods.obtenerElementosDeIdioma(idiomaId, onSuccess, onFailure);
        }

        function cargarIdiomas(){
            function onSuccess(result) {
                var select = document.getElementById('idiomas');
                for (i = 0; i < result.length; i++) {
                    var option = document.createElement('OPTION');
                    option.value = result[i].id;
                    var texto = document.createTextNode(result[i].nombre);
                    option.appendChild(texto);
                    select.appendChild(option);
                }
            }
            function onFailure(result) {}
            PageMethods.obtenerListaIdiomas(onSuccess, onFailure);
        }


        function buscarIdioma(id) {
            function onSuccess(result) {
                if (result != null) {
                    document.getElementById('descripcion').value = result.descripcion;
                } else {
                    var elementos = document.getElementsByName('elementos');
                    for (i = 0; i < elementos.length; i++) {
                        elementos[i].value = '';
                    }
                    document.getElementById('descripcion').value = '';
                }
                cargarElementosDeIdioma();
            }
            function onFailure(result) {
                alert(result);
            }
            document.getElementById('id').value = id;
            PageMethods.obtenerIdioma(id, onSuccess, onFailure);
          
        }

    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <aside id="lateral"></aside>
    <section id="contenedor">
        <div id="2barra"></div>
        <span id="spid">Id:</span><input type="text" id="id" />
        <p />
        <span id="spidioma">Idioma:</span><select id="idiomas" onchange="buscarIdioma(this.value);"></select>
        <p />
        <span id="spdescripcion">Descripción</span><input type="text" id="descripcion" />
        <p/>
        <input type="button" id="eliminarBtn" value="Eliminar" onclick="eliminar()" />
        <input type="button" id="modificarBtn" value="Guardar"  onclick="guardar()" />
        <p />
        <div id="3barra"></div>
        <div>
        <div id="contenedorElementos">

        </div>
            <p/>
        <div id="contenedorPaginado">

        </div>
        </div>
    </section>
</asp:Content>
