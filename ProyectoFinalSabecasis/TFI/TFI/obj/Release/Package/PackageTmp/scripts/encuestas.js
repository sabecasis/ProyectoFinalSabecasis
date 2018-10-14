function enviarEncuesta() {
    function onSuccess(result) { }
    function onFailure(result) { }

    for (i = 0; i < encuestas.length; i++) {
        for (j = 0; j < encuestas[i].preguntas.length; j++) {
            for (h = 0; h < encuestas[i].preguntas[j].respuestas.length; h++) {
                var elem = document.getElementById(encuestas[i].preguntas[j].respuestas[h].id);
                if (elem.checked == true) {
                    PageMethods.actualizarOpcion(encuestas[i].preguntas[j].respuestas[h], onSuccess, onFailure);
                }
            }
        }
    }
    alert('Gracias!');
}

function obtenerEncuestas() {
    function onSuccess(result) {
        encuestas = result;
        var seccionEncuestas = document.getElementById('encuestas');
        var botones = document.createElement('DIV');
        botones.id = 'seccionBotones';
        botones.className = 'row text-center encuesta';
        if (result != null && result.length > 0) {
            for (i = 0; i < result.length; i++) {
                if (result[i].preguntas.length > 0) {
                    for (j = 0; j < result[i].preguntas.length; j++) {
                        var separador = document.createElement('DIV');
                        separador.id = 'sep' + result[i].preguntas[j].id;
                        separador.className = 'container';
                        var pregunta = document.createElement('DIV');
                        pregunta.className = 'row text-left encuesta bg-success';
                        pregunta.id = 'pr' + result[i].preguntas[j].id;
                        pregunta.appendChild(document.createTextNode(result[i].preguntas[j].pregunta));
                        separador.appendChild(pregunta);
                        var tr = document.createElement('DIV');
                        tr.className = 'row text-left encuesta  bg-warning';
                        for (h = 0; h < result[i].preguntas[j].respuestas.length; h++) {
                            var opt = document.createElement('INPUT');
                            opt.type = 'RADIO';
                            opt.value = result[i].preguntas[j].respuestas[h].id;
                            opt.name = result[i].preguntas[j].id;
                            opt.id = result[i].preguntas[j].respuestas[h].id;
                            var td = document.createElement('DIV');
                            td.appendChild(document.createTextNode(result[i].preguntas[j].respuestas[h].opcion));
                            td.className = 'col-md-1';
                            tr.appendChild(td);
                            var td2 = document.createElement('DIV');
                            td2.appendChild(opt);
                            td2.className = 'col-md-1';
                            tr.appendChild(td2);
                        }
                        separador.appendChild(tr);
                        seccionEncuestas.appendChild(separador);
                    }
                    var botonEnviar = document.createElement('a');
                    botonEnviar.id = 'encuestaBtn';
                    botonEnviar.href = '#opinion';
                    botonEnviar.appendChild(document.createTextNode('Enviar encuesta'));
                    botonEnviar.className = 'btn btn-success';
                    botonEnviar.onclick = enviarEncuesta;
                    botones.appendChild(botonEnviar);
                } else {
                    seccionEncuestas.appendChild(document.createTextNode('Por el momento no tenemos preguntas para hacerte, pero siempre puedes enviarnos tu opinión a través del formulario de contacto.'));
                }

            }
        } else {
            
            seccionEncuestas.appendChild(document.createTextNode('Por el momento no tenemos preguntas para hacerte, pero siempre puedes enviarnos tu opinión a través del formulario de contacto.'));
        }
       
        var botonConsultar = document.createElement('a');
        botonConsultar.id = 'consultarEncuestaBtn';
        botonConsultar.appendChild(document.createTextNode('Ver resultados de encuestas'));
        botonConsultar.className = 'btn btn-info';
        botonConsultar.href = '#opinion';
        botonConsultar.setAttribute('data-target', '#divencuesta');
        botonConsultar.setAttribute('data-toggle', 'modal');
        botones.appendChild(botonConsultar);
        seccionEncuestas.appendChild(botones);
    }
    function onFailure(result) {
        
    }
    PageMethods.obtenerEncuestasPorTipo(1, onSuccess, onFailure);
}