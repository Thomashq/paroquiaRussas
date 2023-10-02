$(document).ready(function () {
    changeTextToNavbarCards();
    openModalNews();
});

//filtros
$(document).ready(function () {
    $("#filtro").on("keyup", function () {
        var filtroTexto = $("#filtro").val().toLowerCase();
        var nenhumEventoEncontrado = true;


        $(".evento").each(function () {
            var eventoTexto = $(this).text().toLowerCase();

            if (eventoTexto.indexOf(filtroTexto) === -1) {
                $(this).hide();
            } else {
                $(this).show();
                nenhumEventoEncontrado = false;
            }
        });

        if (nenhumEventoEncontrado) {
            $("#nenhum-evento-encontrado").show();
        } else {
            $("#nenhum-evento-encontrado").hide();
        }
    });
});

$(document).ready(function () {
    $("#filtro-noticias").on("keyup", function () {
        var filtroTexto = $("#filtro-noticias").val().toLowerCase();
        var nenhumaNoticiaEncontrada = true;


        $(".noticia").each(function () {
            var eventoTexto = $(this).text().toLowerCase();

            if (eventoTexto.indexOf(filtroTexto) === -1) {
                $(this).hide();
            } else {
                $(this).show();
                nenhumaNoticiaEncontrada = false;
            }
        });

        if (nenhumaNoticiaEncontrada) {
            $("#nenhuma-noticia-encontrada").show();
        } else {
            $("#nenhuma-noticia-encontrada").hide();
        }
    });
});

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

function fillsModalFields(data) {
    var content = data.newsContent.replace(/\\n\\n/g, "<br>");
    var contentSplited = content.split("<br>");

    $(".modal-title").text(data.newsTitle);
    $(".modal-headline").text(data.headline);

    $("#image-modal").attr("src", data.newsImage);
    $("#image-modal").attr("alt", data.headline);

    $(".modal-date").text(formatDate(data.creationDate));

    $(".modal-content-news").empty();

    contentSplited.forEach(function (item) {
        var element = $("<p>").text(item);
        $(".modal-content-news").append(element);
    });
}