$(function () {
    $("#tags").autocomplete({
        source: $("#tags").attr("data-autocomplete-source")
    });
});


