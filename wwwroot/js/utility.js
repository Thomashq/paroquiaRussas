$(document).ready(function () {
    changeTextToNavbarCards();
    createImageInBase64ForNews();
    createImageInBase64ForEvents();
    changePageToNavbarCards();
    openModalNews();
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

//MODAL NOTICIAS
function openModalNews() {
    $(".link-news").click(function () {
        var newsId = $(this).data("id");
        $.ajax({
            url: "/api/News/View/" + newsId,
            method: "GET",
            success: function (data) {
                if (data.error) {
                    createPopupError(data.error)
                } else {
                    $("#modal").modal('show');
                    fillsModalFields(data.data);
                }
            },
            error: function () {
                createPopupError()
            }
        });

        return false;
    });
}