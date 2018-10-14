function limpiarForm() {
    var inputs = document.getElementsByTagName('INPUT');
    for (i = 0; i < inputs.length; i++) {
        if (inputs[i].type == 'text') {
            inputs[i].value = '';
        }
        if (inputs[i].type == 'checkbox') {
            inputs[i].checked = false;
        }
    }
    var textareas = document.getElementsByTagName('textarea');
    for (i = 0; i < textareas.length; i++) {
       
        textareas[i].innerText = '';
        
    }


    var selects = document.getElementsByTagName('SELECT');
    for (i = 0; i < selects.length; i++) {
        if (selects[i].id!="idiomaSeleccionado") {
            selects[i].selectedIndex = 0;
        }
    }
}

function deshabilitar() {
    document.getElementById('modificarBtn').disabled = 'disabled';
    document.getElementById('id').disabled = '';
}