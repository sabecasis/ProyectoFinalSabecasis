﻿document.onreadystatechange = function () {
    if (document.readyState == "complete") {
        var cookies = document.cookie.split(';');
        var usuario = '';
        for (i = 0; i < cookies.length; i++) {
            if (cookies[i].indexOf("usuario") > -1) {
                usuario = cookies[i].split('=')[1];
            }
        }

        verificarSesion();
        mostrarLogin();
       // cargarComboIdiomas();
        //cargarMenuHorizontal(usuario);
        //cargarMenuVertical(usuario);
       // obtenerLeyendasDePantalla();
    }
}


function mostrarLogin() {
    var divlogin = document.getElementById('logincontent');
    if (divlogin != null) {
        $("#logincontent").load('/Cliente/IniciarSesion.aspx');
    }
}

function setAccion(id) {
    document.getElementById('accion').value = id;
}



function cargarMenuVertical(usuario) {
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
                var menu = document.getElementById('menuVertical');
                var barraLateral = document.getElementById('lateral');
                if (menu != null && result != null && result.length > 0) {
                    barraLateral.style.visibility = 'visible';
                    while (menu.firstChild) {
                        menu.removeChild(menu.firstChild);
                    }

                    var ul = document.createElement('UL');
                    for (i = 0; i < result.length; i++) {
                        var li = document.createElement('LI');
                        var a = document.createElement('A');
                        a.href = result[i].url;
                        a.id = result[i].elemento.nombre;
                        var texto = document.createTextNode(result[i].elemento.leyendaPorDefecto);
                        a.appendChild(texto);
                        li.appendChild(a);
                        ul.appendChild(li);
                    }
                    menu.appendChild(ul);
                }
                obtenerLeyendasDePantalla();
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
    xmlhttp.send(JSON.stringify({ dto: { url: window.location.pathname, usuario: usuario } }));
}



function cargarMenuHorizontal(usuario) {
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
                var menu = document.getElementById('menuNavegacion');
                for (i = 0; i < result.length; i++) {
                    if (result[i].permisoPadre == null) {
                        var li = document.createElement('LI');
                        var a = document.createElement('A');
                        a.href = result[i].url;
                        a.id = result[i].elemento.nombre;
                        var texto = document.createTextNode(result[i].elemento.leyendaPorDefecto);
                        a.appendChild(texto);
                        li.appendChild(a);
                        menu.appendChild(li);
                    }
                }
                obtenerLeyendasDePantalla();
            }
            else if (xmlhttp.status == 400) {
                alert('There was an error 400');
            }
            else {
                alert('something else other than 200 was returned');
            }
        }
    }
    xmlhttp.open("POST", "/CommonService.asmx/obtenerPermisos", true);
    xmlhttp.setRequestHeader("Content-type", "application/json");
    xmlhttp.send(JSON.stringify({usuario:usuario}));
}


function verificarSesion() {
    var cookies = document.cookie.split(';');
    var resultado = false;
    for (i = 0; i < cookies.length; i++) {
        if (cookies[i].indexOf("usuario") > -1) {
            resultado=true;
            break;
        }
    }
    var menuHorizontal = document.getElementById('menuHorizontal');
    if (menuHorizontal != null) {
        if (resultado) {
            if (document.getElementById('alogin') != null) {
                menuHorizontal.children[0].removeChild(document.getElementById('alogin'));
            }
            var logout = document.createElement('A');
            logout.href = "/Cliente/CerrarSesion.aspx"
            logout.id = "alogout";
            var texto = document.createTextNode('Cerrar Sesión');
            logout.appendChild(texto);
            menuHorizontal.children[0].appendChild(logout);
                 
            var li = document.createElement('li');
            var checkout = document.createElement('A');
            checkout.href = "/Cliente/Checkout.aspx"
            checkout.id = "acheckout";
            var texton2 = document.createTextNode('Checkout');
            checkout.appendChild(texton2);
            li.appendChild(checkout);
            menuHorizontal.appendChild(li);
        } else {
            if (document.getElementById('alogout') != null) {
                menuHorizontal.children[0].removeChild(document.getElementById('alogout'));
            }
            var logout = document.createElement('A');
            logout.href = "#";
            logout.id = "alogin";
            logout.setAttribute("data-target", "#divlogin");
            logout.setAttribute("data-toggle", "modal");
            var texto = document.createTextNode('Iniciar Sesión');
            logout.appendChild(texto);
            if (document.getElementById('alogin')==null) {
                menuHorizontal.children[0].appendChild(logout);
            }

            if (document.getElementById('acheckout') != null) {
                removeElement('acheckout');
            }
        }
    }
        
}


function removeElement(id) {
    return (removeElement.elem=document.getElementById(id)).parentNode.removeChild(elem);
}

function cargarComboIdiomas() {
    var xmlhttp;
    if (window.XMLHttpRequest) {
        // IE7+, Firefox, Chrome, Opera, Safari
        xmlhttp = new XMLHttpRequest();
    } else {
        // IE6, IE5
        xmlhttp = new ActiveXObject("Microsoft.XMLHTTP");
    }

    xmlhttp.onreadystatechange = function () {
        if (xmlhttp.readyState == XMLHttpRequest.DONE) {
            if (xmlhttp.status == 200) {
                var result = JSON.parse(xmlhttp.responseText).d;
                var select = document.getElementById('idiomaSeleccionado');
                for (i = 0; i < result.length; i++) {
                    var option = document.createElement('OPTION');
                    option.value = result[i].id;
                    var texto = document.createTextNode(result[i].nombre);
                    option.appendChild(texto);
                    select.appendChild(option);
                }
                select.value = obtenerIdIdioma();
            }
            else if (xmlhttp.status == 400) {
                alert('There was an error 400');
            }
            else {
                alert('something else other than 200 was returned');
            }
        }
    }
    xmlhttp.open("POST", "/CommonService.asmx/obtenerIdiomas", true);
    xmlhttp.setRequestHeader("Content-type", "application/json");
    xmlhttp.send();
}

function obtenerIdIdioma(){
    var cookies = document.cookie.split(';');
    var idioma = 0;
    for (i = 0; i < cookies.length; i++) {
        if (cookies[i].indexOf("idioma") > -1) {
            idioma = cookies[i].split('=')[1];// el valor de la cookie
            break;
        }
    }
    return idioma;
}

function seleccionarIdioma() {
    document.cookie = "idioma=" + document.getElementById('idiomaSeleccionado').value;
    obtenerLeyendasDePantalla();
}


function obtenerLeyendasDePantalla() {
    var xmlhttp;
    var idioma = obtenerIdIdioma();
    var spanTraducibles = document.getElementsByTagName('SPAN');
    var inputTraducibles = document.getElementsByTagName('INPUT');
    var thTraducibles = document.getElementsByTagName('TH');
    var aTraducibles = document.getElementsByTagName('A');
    var articleTraducibles = document.getElementsByTagName('ARTICLE');
    var h1Traducibles = document.getElementsByTagName('H1');
    var h2Traducibles = document.getElementsByTagName('H2');
    var leyendas = new Array();
    j = 0;
    for (i = 0; i < spanTraducibles.length; i++) {
        leyendas[j] = spanTraducibles[i].id;
        j++;
    }
    for (i = 0; i < inputTraducibles.length; i++) {
        if (inputTraducibles[i].type == 'button') {
            leyendas[j] = inputTraducibles[i].id;
            j++;
        }
    }
    for (i = 0; i < thTraducibles.length; i++) {
            leyendas[j] = thTraducibles[i].id;
            j++;  
    }
    for (i = 0; i < aTraducibles.length; i++) {
            leyendas[j] = aTraducibles[i].id;
            j++;
      
    }
    for (i = 0; i < articleTraducibles.length; i++) {
            leyendas[j] = articleTraducibles[i].id;
            j++;
    }
    for (i = 0; i < h1Traducibles.length; i++) {
        leyendas[j] = h1Traducibles[i].id;
        j++;
    }
    for (i = 0; i < h2Traducibles.length; i++) {
        leyendas[j] = h2Traducibles[i].id;
        j++;
    }
    if (window.XMLHttpRequest) {
        // IE7+, Firefox, Chrome, Opera, Safari
        xmlhttp = new XMLHttpRequest();
    } else {
        // IE6, IE5
        xmlhttp = new ActiveXObject("Microsoft.XMLHTTP");
    }

    xmlhttp.onreadystatechange = function () {
        if (xmlhttp.readyState == XMLHttpRequest.DONE) {
            if (xmlhttp.status == 200) {
                var elementos = JSON.parse(xmlhttp.response).d;
                if (elementos != null) {
                    for (i = 0; i < elementos.length; i++) {
                        var domElem = document.getElementById(elementos[i].elemento.nombre)
                        if (domElem != null) {
                            var texto = document.createTextNode(elementos[i].texto);
                            if (domElem.type == 'button') {
                                domElem.setAttribute('value', elementos[i].texto);
                            } else if (domElem.tagName.toLowerCase() == 'article') {
                                domElem.innerHTML=elementos[i].texto;
                            }
                            else {
                                if (domElem.firstChild != null) {
                                    domElem.removeChild(domElem.firstChild);
                                }
                                domElem.appendChild(texto);
                            }
                        }
                    }
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
    xmlhttp.open("POST", "/CommonService.asmx/traducir", true);
    xmlhttp.setRequestHeader("Content-type", "application/json");
    xmlhttp.send(JSON.stringify({ dto: { leyendas: leyendas, idioma: idioma } }));
}