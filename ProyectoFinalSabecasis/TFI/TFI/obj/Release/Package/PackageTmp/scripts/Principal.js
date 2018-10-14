/*!
 * Start Bootstrap - Agency Bootstrap Theme (http://startbootstrap.com)
 * Code licensed under the Apache License v2.0.
 * For details, see http://www.apache.org/licenses/LICENSE-2.0.
 */

// jQuery for page scrolling feature - requires jQuery Easing plugin
$(function() {
    $('a.page-scroll').bind('click', function(event) {
        var $anchor = $(this);
        $('html, body').stop().animate({
            scrollTop: $($anchor.attr('href')).offset().top
        }, 1500, 'easeInOutExpo');
        event.preventDefault();
    });
});

// Highlight the top nav as scrolling occurs
$('body').scrollspy({
    target: '.navbar-fixed-top'
})

// Closes the Responsive Menu on Menu Item Click
$('.navbar-collapse ul li a').click(function() {
    $('.navbar-toggle:visible').click();
});

function crearGrafico() {
    var id = document.getElementById('seleccionpreguntas').selectedIndex;

    if (id != null && preguntas != null && preguntas.length > 0) {
        document.getElementById('morris-bar-chart').innerHTML = '';
        Morris.Bar({
            element: 'morris-bar-chart',
            data: preguntas[id].respuestas,
            xkey: 'opcion',
            ykeys: ['cantidadDeSelecciones'],
            labels: ['cantidad'],
            barRatio: 0.4,
            xLabelAngle: 0,
            hideHover: 'auto',
            resize: true
        });
    }
}
function obtenerInformacionDePreguntas() {
    PageMethods.obtenerPreguntasDeEncuesta(onSuccess, onFailure);
    function onSuccess(result) {
        preguntas = result;
        if (result != null) {
            var select = document.getElementById('seleccionpreguntas');
            for (i = 0; i < result.length; i++) {
                var opt = document.createElement('OPTION');
                opt.appendChild(document.createTextNode(result[i].pregunta));
                opt.value = result[i].id;
                select.appendChild(opt);
            }
            Morris.Bar({
                element: 'morris-bar-chart',
                data: preguntas[0].respuestas,
                xkey: 'opcion',
                ykeys: ['cantidadDeSelecciones'],
                labels: ['cantidad'],
                barRatio: 0.4,
                xLabelAngle: 0,
                hideHover: 'always',
                resize: true
            });
        }
    }
    function onFailure(result) { }
}