$(document).ready(function () {
    $('#inputTitle').empty();
    $('#inputText').empty();
});

$('#addNews').click(function () {     
    $('body,html').animate({
        scrollTop: 0                   
    }, 500);
});


$('#collapsedNews').hide();
$('#addNews').on('click', function () {
    $('#collapsedNews').toggle("slow");
});

var title;
var text;

$('#addForm').on('click propertychange', function () {
    $('#collapsedNews').hide();
    title = $('#inputTitle').val();
    title = markdown.toHTML(title);
    $('#inputTitle').val(title);
    text = $('#inputText').val();
    text = markdown.toHTML(text);
    $('#inputText').val(text);

});


