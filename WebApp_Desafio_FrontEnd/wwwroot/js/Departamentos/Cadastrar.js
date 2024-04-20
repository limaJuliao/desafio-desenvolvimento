﻿
$('#btnSalvar').click(() => {
    let form = $('#form');

    if (!form.valid()) {
        console.log('Formulario Invalido');
        return;
    }

    let data = SerielizeForm($('#form'))

    $.ajax({
        type: 'POST',
        url: form.attr('action'),
        data,
        success: result => {

            Swal.fire({
                type: result.Type,
                title: result.Title,
                text: result.Message,
            }).then(() => {
                window.location.href = `${config.contextPath}${result.Controller}/${result.Action}`;
            });

        },
        error: result => {

            Swal.fire({
                text: result.responseText,
                confirmButtonText: 'Ok',
                icon: 'error'
            });

        },
    })
})