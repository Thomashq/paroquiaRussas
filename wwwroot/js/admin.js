$(document).ready(function () {
    validateEqualityOfPasswordFields();
    createTableToAdmin();
    openModalNewsToUpdate();

    deleteEntity("news", "News");
    deleteEntity("events", "Event");
    deleteEntity("person", "Person");
});

function createTableToAdmin() {
    $('.call-data-table').DataTable({
        "ordering": true,
        "paging": true,
        "searching": true,
        "oLanguage": {
            "sEmptyTable": "Nenhum registro encontrado na tabela",
            "sInfo": "Mostrar _START_ até _END_ de _TOTAL_ registros",
            "sInfoEmpty": "Mostrar 0 até 0 de 0 Registros",
            "sInfoFiltered": "(Filtrar de _MAX_ total registros)",
            "sInfoPostFix": "",
            "sInfoThousands": ".",
            "sLengthMenu": "Mostrar _MENU_ registros por pagina",
            "sLoadingRecords": "Carregando...",
            "sProcessing": "Processando...",
            "sZeroRecords": "Nenhum registro encontrado",
            "sSearch": "Pesquisar",
            "oPaginate": {
                "sNext": "Proximo",
                "sPrevious": "Anterior",
                "sFirst": "Primeiro",
                "sLast": "Ultimo"
            },
            "oAria": {
                "sSortAscending": ": Ordenar colunas de forma ascendente",
                "sSortDescending": ": Ordenar colunas de forma descendente"
            }
        }
    });
}


function clearAllFieldsFromAdmin(conteudo) {
    if (conteudo === "usuarios" || conteudo === "eventos" || conteudo === "noticias") {
        $("#contentInput").val("");
        $("#headlineInput").val("");
        $("#titleInput").val("");
        $("#newsImageBase64").val("");
        $("#formFileSm").val("");

        $("#nameInput").val("");
        $("#descriptionInput").val("");
        $("#addressInput").val("");
        $("#dateInput").val("");
        $("#timeInput").val("");
        $("#formEventsFileSm").val("");
        $("#eventsImageBase64").val("");

        $("#userNameInput").val("");
        $("#pwdInput").val("");
        $("#pwdRepeatInput").val("");
    }
}

function validateEqualityOfPasswordFields() {
    $('.personForms').submit(function (e) {
        var password = $('#pwdInput').val();
        var passwordRepeated = $('#pwdRepeatInput').val();

        if (password !== passwordRepeated) {
            createPopupError('As senhas não coincidem.');
            e.preventDefault();
        }
    });
}

function deleteEntity(entityType, entityDelete) {
    $("." + entityType + "-delete").click(function () {
        var entityId = $(this).data("id");
        $.ajax({
            url: "/api/" + entityType + "/Delete" + entityDelete + "/" + entityId,
            method: "DELETE",
            success: function (data) {
                if (data.message) {
                    createPopupSuccess(data.message);
                    location.reload();
                } else if (data.error) {
                    createPopupError(data.error);
                }
            },
            error: function (data) {
                createPopupError(data.message);
            }
        });

        return false;
    });
}

//MODAL NOTICIAS
function openModalNewsToUpdate() {
    $(".news-edit").click(function () {
        var newsId = $(this).data("id");
        $.ajax({
            url: "/api/News/View/" + newsId,
            method: "GET",
            success: function (data) {
                if (data.error) {
                    createPopupError(data.error)
                } else {
                    fillNewsFormsModal(data);

                    $("#news-forms-modal").modal('show');
                }
            },
            error: function () {
                createPopupError()
            }
        });

        return false;
    });
}

function fillNewsFormsModal(data) {
    $("#titleInputModal").val(data.data.newsTitle);
    $("#headlineInputModal").val(data.data.headline);
    $("#contentInputModal").val(data.data.newsContent);
    $("#newsImageBase64Modal").val(data.data.newsImage);
    $("#idInputModal").val(data.data.id);
}