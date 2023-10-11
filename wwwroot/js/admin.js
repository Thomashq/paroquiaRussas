$(document).ready(function () {
    validateEqualityOfPasswordFields();
});

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