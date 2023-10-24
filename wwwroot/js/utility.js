$(document).ready(function () {
    changeTextToNavbarCards();
    createImageInBase64ForNews();
    createImageInBase64ForEvents();
    changePageToNavbarCards();
    createTableToAdmin();
});

function formatDate(dataString) {
    const data = new Date(dataString + "T00:00:00");
    const dia = String(data.getDate()).padStart(2, '0');
    const mes = String(data.getMonth() + 1).padStart(2, '0');
    const ano = data.getFullYear();
    return `${dia}/${mes}/${ano}`;
}

function createPopupError(message) {
    var errorMessageDiv = $("#error-message-div");

    $("#error-message").text(message);
    errorMessageDiv.show();

    setTimeout(function () {
        errorMessageDiv.fadeOut();
    }, 4000);
}

function createPopupSuccess(message) {
    var successMessageDiv = $("#success-message-div");

    $("#success-message").text(message);
    successMessageDiv.show();

    setTimeout(function () {
        successMessageDiv.fadeOut();
    }, 4000);
}

$(".close-alert").click(function () {
    $('.alert').hide('hide');
});

//NAVBAR CARDS (LITURGIA E ADMIN)
function changeTextToNavbarCards() {
    $(".nav-link").click(function () {
        $(".card-body").hide();
        $(".nav-link").removeClass("active");

        var selectedTab = $(this).attr("data-nav");

        $(".conteudo-" + selectedTab).show();
        $(this).addClass("active");

        clearAllFieldsFromAdmin(selectedTab);
    });
};

//NAVBAR CARDS ADMIN (CRIAR E EDITAR)
function changePageToNavbarCards() {
    $(".nav-link-admin-page[data-nav-page='editar']").click(function () {
        $(".pagina-admin-editar").show();
        $(".pagina-admin-criar").hide();
    });

    $(".nav-link-admin-page[data-nav-page='criar']").click(function () {
        $(".pagina-admin-editar").hide();
        $(".pagina-admin-criar").show();
    });
};

function createImageInBase64ForNews() {
    $('#formFileSm').change(function () {
        var input = this;
        var file = input.files[0];

        if (file) {
            var reader = new FileReader();

            reader.onload = function (e) {
                var base64Value = file.name + ',' + e.target.result;
                $('#newsImageBase64').val(base64Value);
            };

            reader.readAsDataURL(file);
        }
    });
}

function createImageInBase64ForEvents() {
    $('#formEventsFileSm').change(function () {
        var input = this;
        var file = input.files[0];

        if (file) {
            var reader = new FileReader();

            reader.onload = function (e) {
                var base64Value = file.name + ',' + e.target.result;
                $('#eventsImageBase64').val(base64Value);
            };

            reader.readAsDataURL(file);
        }
    });
}

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