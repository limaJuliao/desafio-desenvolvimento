
$(document).ready(function () {

    var table = $('#dataTables-Departamentos').DataTable({
        paging: false,
        ordering: false,
        info: false,
        searching: false,
        processing: true,
        serverSide: true,
        ajax: config.contextPath + 'Departamentos/Datatable',
        columns: [
            { data: 'ID' },
            { data: 'Descricao', title: 'Descrição' },
        ],
    });

    $('#dataTables-Departamentos tbody').
        on('click', 'tr', function () {
            if ($(this).hasClass('selected')) {
                $(this).removeClass('selected');
            } else {
                table.$('tr.selected').removeClass('selected');
                $(this).addClass('selected');
            }
        })
        .on('dblclick', 'tr', function () {
            let data = table.row(this).data();
            window.location.href = `${config.contextPath}Departamentos/Editar/${+ data.ID}`;
        });

    $('#btnEditar').click(function () {
        var data = table.row('.selected').data();
        window.location.href = config.contextPath + 'Departamentos/Editar/' + data.ID;
    });

    $('#btnExcluir').click(function () {
        console.log('Excluindo departamento');

        let data = table.row('.selected').data();
        let idRegistro = data.ID;

        if (!idRegistro || idRegistro <= 0)
            return;


        let url = `${config.contextPath}Departamentos/Excluir/${idRegistro}`;

        if (idRegistro) {
            Swal.fire({
                text: `Tem certeza de que deseja excluir ${data.Descricao} ?`,
                type: "warning",
                showCancelButton: true,
            }).then(function (result) {

                if (result.value) {
                    $.ajax({
                        url,
                        type: 'DELETE',
                        contentType: 'application/json',
                        error: function (result) {

                            Swal.fire({
                                text: result,
                                confirmButtonText: 'Ok',
                                icon: 'error'
                            });

                        },
                        success: function (result) {

                            Swal.fire({
                                type: result.Type,
                                tittle: result.Title,
                                text: result.Message,
                            }).then(function () {
                                table.draw();
                            })
                        }
                    })
                } else {
                    console.log('Cancelou a exclusão');
                }
            })
        }
    })
});