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