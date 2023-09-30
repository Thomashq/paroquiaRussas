$(document).ready(function () {
    changeTextToNavbarCards();
    createImageInBase64();
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
    });
};

function createImageInBase64() {
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