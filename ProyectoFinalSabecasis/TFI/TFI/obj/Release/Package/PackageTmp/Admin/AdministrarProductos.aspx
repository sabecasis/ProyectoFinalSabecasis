<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Admin/admin.Master" CodeBehind="AdministrarProductos.aspx.vb" Inherits="TFI.AdministrarProductos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        window.onload = function () {
            function onSuccessCatalogos(result) {
                var contenedor = document.getElementById('contenedorCatalogos');
                    var ul = document.createElement('UL');
                    contenedor.appendChild(ul);
                    if (result != null && result.length > 0) {
                        for (i = 0; i < result.length; i++) {
                            var li = document.createElement('LI');
                            var elemento = document.createElement('INPUT');
                            elemento.type = 'CHECKBOX';
                            elemento.value = result[i].id;
                            elemento.name = 'catalogos';
                            var texto = document.createElement('SPAN');
                            texto.appendChild(document.createTextNode(result[i].catalogo));
                            li.appendChild(texto);
                            li.appendChild(elemento);
                            ul.appendChild(li);
                        }
                    }
            }
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
            function onSuccessMetodoVal(result){
                var selectMetodoVal = document.getElementById('metodoval');
                var option;
                if (result != null) {
                    for (i = 0; i < result.length; i++) {
                        option = document.createElement('OPTION');
                        option.value = result[i].id;
                        option.text = result[i].metodo;
                        selectMetodoVal.appendChild(option);
                    }
                }
            }
            function onSuccessMetodoRep(result) {
                var selectMetodoVal = document.getElementById('metodorep');
                var option;
                if (result != null) {
                    for (i = 0; i < result.length; i++) {
                        option = document.createElement('OPTION');
                        option.value = result[i].id;
                        option.text = result[i].metodo;
                        selectMetodoVal.appendChild(option);
                    }
                }
            }

            function onSuccessTipo(result) {
                var selectMetodoVal = document.getElementById('tipoproducto');
                var option;
                if (result != null) {
                    for (i = 0; i < result.length; i++) {
                        option = document.createElement('OPTION');
                        option.value = result[i].id;
                        option.text = result[i].tipo;
                        selectMetodoVal.appendChild(option);
                    }
                }
            }

            function onSuccessFamilia(result) {
                var selectMetodoVal = document.getElementById('familia');
                var option;
                if (result != null) {
                    for (i = 0; i < result.length; i++) {
                        option = document.createElement('OPTION');
                        option.value = result[i].id;
                        option.text = result[i].familia;
                        selectMetodoVal.appendChild(option);
                    }
                }
            }


            function onSuccessGarantia(result) {
                var selectMetodoVal = document.getElementById('garantia');
                var option;
                if (result != null) {
                    for (i = 0; i < result.length; i++) {
                        option = document.createElement('OPTION');
                        option.value = result[i].id;
                        option.text = result[i].descripcion;
                        selectMetodoVal.appendChild(option);
                    }
                }
            }


            PageMethods.obtenerEstadosDeProducto(onSuccessEstado, onFailure);
            PageMethods.obtenerMetodosDeValoracion(onSuccessMetodoVal, onFailure);
            PageMethods.obtenerMetodosDeReposicion(onSuccessMetodoRep, onFailure);
            PageMethods.obtenerTiposDeProducto(onSuccessTipo, onFailure);
            PageMethods.obtenerFamiliasDeProducto(onSuccessFamilia, onFailure);
            PageMethods.obtenerTiposDeGarantia(onSuccessGarantia, onFailure);
            PageMethods.obtenerTodosLosCatalogos(onSuccessCatalogos, onFailure);
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
            function onSuccess(result) {
                if (result != null) {
                    document.getElementById('id').value = result.nroDeProducto;
                    document.getElementById('nombre').value = result.nombre;
                    document.getElementById('descripcion').value = result.descripcion;
                    document.getElementById('precioventa').value = result.precioVenta;
                    document.getElementById('porcentajeganancia') = result.porcentajeDeGanancia;
                    document.getElementById('costoposesion') = result.costoDePosesion;
                    document.getElementById('costofinanciero') = result.costoFinanciero;
                    document.getElementById('costoestandar')=result.costoEstandar
                    document.getElementById('puntominimo') = result.puntoMinimoDeReposicion;
                    document.getElementById('puntomaximo') = result.puntoMaximoDeReposicion;
                    document.getElementById('ciclo') = result.ciclo;
                    document.getElementById('cantidad') = result.cantidad;
                    var selectGarantia = document.getElementById('garantia');
                    for (i = 0; i < selectGarantia.length; i++) {
                        if (selectGarantia[i].value == result.garantia.id) {
                            selectGarantia.selectedIndex = i;
                            break;
                        }
                    }
                    var selectEstado = document.getElementById('estado');
                    for (i = 0; i < selectEstado.length; i++) {
                        if (selectEstado[i].value == result.estado.id) {
                            selectEstado.selectedIndex = i;
                            break;
                        }
                    }
                    var selectFamilia = document.getElementById('familia');
                    for (i = 0; i < selectFamilia.length; i++) {
                        if (selectFamilia[i].value == result.familiaDeProducto.id) {
                            selectFamilia.selectedIndex = i;
                            break;
                        }
                    }

                    var selectMetodoRepo = document.getElementById('metodorep');
                    for (i = 0; i < selectMetodoRepo.length; i++) {
                        if (selectMetodoRepo[i].value == result.metodoDeReposicion.id) {
                            selectMetodoRepo.selectedIndex = i;
                            break;
                        }
                    }
                    var selectMetodoVal = document.getElementById('metodoval');
                    for (i = 0; i < selectMetodoVal.length; i++) {
                        if (selectMetodoVal[i].value == result.metodoDeValoracion.id) {
                            selectMetodoVal.selectedIndex = i;
                            break;
                        }
                    }
                    var selectTipoProd = document.getElementById('tipoproducto');
                    for (i = 0; i < selectTipoProd.length; i++) {
                        if (selectTipoProd[i].value == result.tipoDeProducto.id) {
                            selectTipoProd.selectedIndex = i;
                            break;
                        }
                    }
                }
            }
            function onFailure(result) { }
            PageMethods.buscar({ nroDeProducto: document.getElementById('id').value, nombre: document.getElementById('nombre').value }, onSuccess, onFailure);
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
        <span id="spdescripcion">Descripción:</span><input type="text" id="descripcion" name="descripcion"/>
        <p/>
         <span id="spprecioventa">Precio de venta:</span><input type="text" id="precioventa" name="precioventa"/>
        <p />
         <span id="spporcentaje">Porcentaje de ganancia:</span><input type="text" id="porcentajeganancia" name="porcentajeganancia"/>
        <p />
         <span id="spcostoposesion">Costo de posesión:</span><input type="text" id="costoposesion" name="costoposesion"/>
        <p />
         <span id="spcostofinanciero">Costo financiero:</span><input type="text" id="costofinanciero" name="costofinanciero"/>
        <p />
         <span id="spcostoestandar">Costo estándar:</span><input type="text" id="costoestandar" name="costoestandar"/>
        <p />
         <span id="sppuntominimo">Punto mínimo de reposición:</span><input type="text" id="puntominimo" name="puntominimo"/>
        <p />
         <span id="sppuntomaximo">Punto máximo de reposición:</span><input type="text" id="puntomaximo" name="puntomaximo"/>
        <p />
         <span id="spciclo">Ciclo:</span><input type="text" id="ciclo" name="ciclo"/>
        <p />
         <span id="spcantidadenstock">Cantidad en stock:</span><input type="text" id="cantidad" name="cantidad"/>
        <p />
         <span id="spgarantia">Garantía:</span><select id="garantia" name="garantia"></select>
        <p />
         <span id="spestado">Estado:</span><select id="estado" name="estado"></select>
        <p />
         <span id="spfamiliaproducto">Familia:</span><select id="familia" name="familia"></select>
        <p />
         <span id="spmetodoreposicion">Método de reposición:</span><select id="metodorep" name="metodorep" ></select>
        <p />
         <span id="spmetodovaloracion">Método de valoración:</span><select id="metodoval" name="metodoval"></select>
        <p />
         <span id="sptipodeproducto">Tipo de producto:</span><select id="tipoproducto" name="tipoproducto"></select>
        <p />
        <span id="spimagen">Imagen:</span><asp:FileUpload ID="imagen" runat="server" />
        <p />
        <div id="3barra"></div>
        <div id="conetenedorAsignacion">
            <span id="spcatalogos">Catálogos</span>
            <div id="contenedorCatalogos">
                
            </div>
        </div>
        <input type="button" id="eliminarBtn" value="Eliminar" onclick="setAccion(this.id)" />
        <input type="submit" id="modificarBtn" value="Guardar" onclick="setAccion(this.id)"/>
        <input type="button" id="crearBtn" value="Crear" onclick="crear()" />
         <input type="button" id="limpiarBtn" value="Limpiar" onclick="limpiarForm()"/>
        <input type="button" id="buscarBtn" value="Buscar" onclick="buscar()" />
        <p />
    </section>
</asp:Content>
