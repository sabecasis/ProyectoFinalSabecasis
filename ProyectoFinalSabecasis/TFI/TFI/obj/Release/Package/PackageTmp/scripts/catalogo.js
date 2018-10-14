function limpiarComentarios() {
    document.getElementById('comentarios').innerHTML = '';
}

function eliminarComentario() {
    var comentId = this.id.substring(6);
    PageMethods.eliminarComentario(comentId, onSuccess, onFailure);
    function onSuccess(result) {
        obtenerComentarios(nroProd);
    }
    function onFailure(result) { }
}

function obtenerComentarios(nroProducto) {
    nroProd = 0;
    if (nroProducto != null && !isNaN(nroProducto)) {
        nroProd = nroProducto;
    } else {
       nroProd= this.id.substring(this.id.indexOf('vercoment') + 9);
    }
    PageMethods.obtenerComentariosDeProducto(nroProd, onSuccess, onFailure);
    function onSuccess(result) {
        limpiarComentarios();
        var comentarios = document.getElementById('comentarios');
        if (document.getElementById('ContentPlaceHolder1_usuarioId').value!=0) {
            var divagregar = document.createElement('DIV');
            divagregar.className = 'modal-dialog text-center';
            comentarios.appendChild(divagregar);
            var divtextarea = document.createElement('DIV');
            divtextarea.className = 'row';
            var textarea = document.createElement('textarea');
            textarea.rows = 4;
            textarea.cols = 50;
            textarea.id = 'nuevoComentario';
            textarea.className = 'form-control';
            divtextarea.appendChild(textarea);
            divagregar.appendChild(divtextarea);
            var divBtn = document.createElement('DIV');
            divBtn.className = 'row';
            var botonagregar = document.createElement('input');
            botonagregar.id = 'agregarComentarioBtn';
            botonagregar.value = 'agregar comentario';
            botonagregar.onclick = crearComentario;
            botonagregar.type = 'button';
            botonagregar.className = 'btn btn-info btn-block';
            divBtn.appendChild(botonagregar);
            divagregar.appendChild(divBtn);
        }
        if (result != null) {
            for (i = 0; i < result.length; i++) {
                var div = document.createElement('DIV');
                div.id = 'comentario' + result[i].id;
                div.className = 'modal-dialog modal-content';
                var barraInfoComentario = document.createElement('div');
                barraInfoComentario.className = 'row informacion-comentarios-producto';
                var fecha = document.createElement('DIV');
                fecha.appendChild(document.createTextNode(result[i].fecha.toLocaleDateString()));
                fecha.className = 'col-md-4';
                barraInfoComentario.appendChild(fecha);
                var nomUsuario = document.createElement('div');
                nomUsuario.className = 'col-md-4';
                nomUsuario.appendChild(document.createTextNode(result[i].usuario.nombre));
                barraInfoComentario.appendChild(nomUsuario);
                if (document.getElementById('ContentPlaceHolder1_usuarioId').value == result[i].usuario.id) {
                    var eliminar = document.createElement('a');
                    eliminar.appendChild(document.createTextNode('X'));
                    eliminar.id = 'coment' + result[i].id;
                    eliminar.onclick = eliminarComentario;
                    eliminar.className = 'col-md-4';
                    eliminar.href = '#comentarios';
                    barraInfoComentario.appendChild(eliminar);
                }
                var comentario = document.createElement('DIV');
                comentario.innerHTML = result[i].comentario;
                div.appendChild(barraInfoComentario);
                div.appendChild(comentario);
                comentarios.appendChild(div);
            }
        }
    }
    function onFailure(result) { }
}


function limpiarFiltro() {
    obtenerCatalogo(idCatalogo, 0, false);
}

function filtrar() {
    var precio = document.getElementById('precio').value;
    var conDescuento = document.getElementById('filtrodescuento').checked;
    obtenerCatalogo(idCatalogo, precio, conDescuento);
}

function cargarOpcionesDeCatalogo() {
    var xmlhttp;
    if (window.XMLHttpRequest) {
        // code for IE7+, Firefox, Chrome, Opera, Safari
        xmlhttp = new XMLHttpRequest();
    } else {
        // code for IE6, IE5
        xmlhttp = new ActiveXObject("Microsoft.XMLHTTP");
    }

    xmlhttp.onreadystatechange = function () {
        if (xmlhttp.readyState == XMLHttpRequest.DONE) {
            if (xmlhttp.status == 200) {
                var result = JSON.parse(xmlhttp.responseText).d;
                var menu = document.getElementById('categorias');
                if (menu != null && result != null && result.length > 0) {
                    while (menu.firstChild) {
                        menu.removeChild(menu.firstChild);
                    }

                    var ul = document.createElement('UL');
                    for (i = 0; i < result.length; i++) {
                        var li = document.createElement('LI');
                        var a = document.createElement('A');
                        a.href = result[i].url;
                        a.className = 'list-group-item';
                        a.id = result[i].elemento.nombre;
                        var texto = document.createTextNode(result[i].elemento.leyendaPorDefecto);
                        a.appendChild(texto);
                        li.appendChild(a);
                        ul.appendChild(li);
                    }
                    menu.appendChild(ul);
                }
            }
            else if (xmlhttp.status == 400) {
                alert('There was an error 400');
            }
            else {
                alert('something else other than 200 was returned');
            }
        }
    }
    xmlhttp.open("POST", "/CommonService.asmx/obtenerPermisosHijos", true);
    xmlhttp.setRequestHeader("Content-type", "application/json");
    xmlhttp.send(JSON.stringify({ dto: { url: window.location.pathname, usuario: null } }));
}

function imageNotFound(){
    this.onerror=null;this.src='../static/product.jpg';
}


function otenerDetalleProducto() {
    var prodId = this.id.substring(2);
    var producto = { nroDeProducto: prodId, nombre:'' };
    PageMethods.obtenerProducto(producto, onSuccess, onFailure);
    function onSuccess(result) {
        if (result != null) {
            document.getElementById('divdescripcionprod').innerHTML = result.descripcion;
            document.getElementById('imagendescripcionprod').src = result.urlImagen;
            document.getElementById('hpreciodescripcion').innerText = '$' + result.precioVenta;
            document.getElementById('hnombreproddescripcion').innerText = result.nombre;
        }
    }
    function onFailure(result) {
    }
}
function obtenerCatalogo(idCatalogo, precio, conDescuento) {
    PageMethods.obtenerProductosPorCatalogo(idCatalogo, precio, conDescuento, onSuccess, onFailure);
    function onSuccess(result) {
        var contenido = document.getElementById('contenido');
        var divs = document.getElementsByName('producto');
        while (divs.length > 0) {
            contenido.removeChild(divs[0]);
        }
        if (result != null && result.length > 0) {
            productosDeCatalogo = result;
            for (i = 0; i < result.length; i++) {
                //El div que contiene el producto
                var prod = document.createElement('div');
                prod.className = 'col-sm-4 col-lg-4 col-md-4 thumbnail';
                prod.id = 'prodcont' + result[i].nroDeProducto;
                prod.setAttribute('name','producto');
               //El div que contiene la imagen e info del producto
                var thumbnaildiv = document.createElement('DIV');
                prod.appendChild(thumbnaildiv);
                //La imagen del producto
                var img = document.createElement('IMG');
                img.src = result[i].urlImagen;
                img.id = 'imgprod' + result[i].nroDeProducto;
                img.onerror = imageNotFound;
                thumbnaildiv.appendChild(img);

                //El div que contiene la información específica del producto debajo de la imagen
                var divcaption = document.createElement('DIV');
                divcaption.className = 'caption';
                thumbnaildiv.appendChild(divcaption);

                //El nombre del producto
                var nombre = document.createElement('a');
                nombre.appendChild(document.createTextNode(result[i].nombre));
                nombre.id = 'sp' + result[i].nroDeProducto;
                nombre.href = "#";
                nombre.setAttribute('data-target', '#detalleProducto');
                nombre.setAttribute('data-toggle', 'modal');
                nombre.onclick = otenerDetalleProducto;
                var contenedorNombre = document.createElement('h4');
                contenedorNombre.appendChild(nombre);
                divcaption.appendChild(contenedorNombre);


                //El precio
                var pprecio = document.createElement('p');
                var contenedorPrecio = document.createElement('h4');
                var precio = document.createElement('SPAN');
                var texto = "$ " + result[i].precioVenta;
                precio.appendChild(document.createTextNode(texto));
                contenedorPrecio.appendChild(precio);
                pprecio.appendChild(contenedorPrecio);
                divcaption.appendChild(pprecio);

               
                //aca ponemos el checkbox que nos permite seleccionar el producto para agregar al checkout
                var pseleccion = document.createElement('p');
                var labelseleccion = document.createElement('span');
                labelseleccion.appendChild(document.createTextNode('Seleccionar '));
                labelseleccion.name = 'spseleccionar';
                labelseleccion.className = 'datos-producto';
                var checkbox = document.createElement('INPUT');
                checkbox.id = 'prod' + result[i].nroDeProducto;
                checkbox.type = 'CHECKBOX';
                checkbox.name = 'productos';
                checkbox.value = result[i].nroDeProducto;
                pseleccion.appendChild(labelseleccion);
                pseleccion.appendChild(checkbox);
                divcaption.appendChild(pseleccion);

                var contenedorcantidad = document.createElement('p');
                var labelcantidad = document.createElement('span');
                labelcantidad.appendChild(document.createTextNode('Igresar cantidad '));
                labelcantidad.name = 'spingresarcantidad';
                labelcantidad.className = 'datos-producto';
                contenedorcantidad.appendChild(labelcantidad);
                var cantBox = document.createElement('INPUT');
                cantBox.type = 'TEXT';
                cantBox.name = 'cantidad' + result[i].nroDeProducto;
                cantBox.id = 'cantidad' + result[i].nroDeProducto;
                cantBox.value = 1;
                contenedorcantidad.appendChild(cantBox);
                divcaption.appendChild(contenedorcantidad);
                

                //agregamos las estrellitas de raiting
                var divestrellitas = document.createElement('div');
                var estre1 = document.createElement('span');
                estre1.className = 'fa star-o';
                estre1.id = result[i].nroDeProducto + '-' + 1;
                divestrellitas.appendChild(estre1);
                var estre2 = document.createElement('span');
                estre2.className = 'glyphicon glyphicon-star-empty';
                estre2.id = result[i].nroDeProducto + '-' + 2;
                divestrellitas.appendChild(estre2);
                var estre3 = document.createElement('span');
                estre3.className = 'glyphicon glyphicon-star-empty';
                estre3.id = result[i].nroDeProducto + '-' + 3;
                divestrellitas.appendChild(estre3);
                var estre4 = document.createElement('span');
                estre4.className = 'glyphicon glyphicon-star-empty';
                estre4.id = result[i].nroDeProducto + '-' + 4;
                divestrellitas.appendChild(estre4);
                var estre5 = document.createElement('span');
                estre5.className = 'glyphicon glyphicon-star-empty';
                estre5.id = result[i].nroDeProducto + '-' + 5;
                divestrellitas.appendChild(estre5);
                prod.appendChild(divestrellitas);

                //con este link podemos ver comentarios de productos
                var linkvercoment = document.createElement('a');
                linkvercoment.href = '#comentarios';
                linkvercoment.onclick = obtenerComentarios;
                linkvercoment.id = 'vercoment' + result[i].nroDeProducto;
                linkvercoment.name = 'avercomentario';
                linkvercoment.appendChild(document.createTextNode('Ver comentarios'));
                linkvercoment.setAttribute('data-target', '#comentarios');
                linkvercoment.setAttribute('data-toggle', 'collapse');
                linkvercoment.className = 'link-ver-comentarios';
                prod.appendChild(linkvercoment);

                //agregamos todo al div general de productos
                contenido.appendChild(prod);
            }

        }
    }

    function onFailure() { }
}

function cargarCatalogo() {
    var params = location.search.split("?");
    idCatalogo = 1;
    if (params.length > 1) {
        var partes = params[1].split("=");
        idCatalogo = partes[1].replace('#', '');
    }
    if (idCatalogo != 0) {
        //document.getElementById('busqueda').style.display = 'block';
        obtenerCatalogo(idCatalogo, 0, false);
    }

}

function crearComentario() {
    var comentario = {
        id: 0,
        comentario: document.getElementById('nuevoComentario').value,
        producto: { nroDeProducto: nroProd },
        usuario: { id: document.getElementById('ContentPlaceHolder1_usuarioId').value }
    }

    function onSuccess(result) {
        limpiarComentarios();
        obtenerComentarios(nroProd);
    }
    function onFailure(result) { }
    PageMethods.crearComentarioDeProducto(comentario, onSuccess, onFailure);
}