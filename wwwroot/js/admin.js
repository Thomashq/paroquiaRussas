$(document).ready(function () {
    validateEqualityOfPasswordFields();
    deleteNews();
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

//NEWS
function deleteNews() {
    $(".news-delete").click(function () {
        var newsId = $(this).data("id");
        $.ajax({
            url: "/api/News/DeleteNews/" + newsId,
            method: "DELETE",
            success: function (data) {
                if (data.message) {
                    createPopupSuccess(data.message);
                    location.reload();
                }
                else if (data.error)
                    createPopupError(data.error);
            },
            error: function (data) {
                createPopupError(data.message);
            }
        });

        return false;
    });
}
