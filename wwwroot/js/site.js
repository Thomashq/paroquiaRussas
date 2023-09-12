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