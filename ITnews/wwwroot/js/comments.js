$('.comments').hide();
$('.openComments').on('click', function () {
    var Id = $(this).attr('data-id');
    $(".comments." +Id).load('../../News/GetComments', { Id: Id} );
    $(".comments." +Id).toggle();
});

$('.comments').on('click', '.sendComments', function () {
    var Id = $(this).attr('data-id');
    var text = $('.textareaComment.' + Id).val();
    var author = $(this).attr('data-author');
    $(".comments." + Id).load('../../News/PostComments', { Id: Id, text: text, author: author },
        function (response, status, xhr)
        {
            if (status != "error") {
                hubConnection.invoke("Send", Id);
            }
        });
});

$('.comments').on('click', '.heartComment', function () {
    var Id = $(this).attr('data-idNews');
    var IdComment = $(this).attr('data-id');
    var user = $(this).attr('data-user');
    var likes = $(this).attr('data-likes');
        var res = parseInt(likes);
        res = res + 1;
    $(".comments." + Id).load('../../News/SetLike', { IdComment: IdComment, user: user, likes: res, Id: Id });
});

let hubUrl = 'https://localhost:44384/comment';
const hubConnection = new signalR.HubConnectionBuilder()
    .withUrl(hubUrl)
    .configureLogging(signalR.LogLevel.Information)
    .build();

hubConnection.on("Send", function (data) {
    if (data != null) {
      $(".comments." + data).load('../../News/GetComments', { Id: data });
    }
});

hubConnection.start();

var count = $(".my-rating-" + 0).attr("data-count");

for (var i = 0; i < count; i++) {
    var initrat = $(".my-rating-" + i).attr("data-rating");
        $(".my-rating-" + i).starRating({
            initialRating: initrat,
            totalStars: 5,
            starShape: 'rounded',
            starSize: 40,
            useFullStars: true,
            emptyColor: 'lightgray',
            hoverColor: 'salmon',
            activeColor: 'crimson',
            useGradient: false,
            callback: function (currentRating, $el) {
                var id = $el.attr("data-id");
                var user = $el.attr("data-user");

                 $.ajax({
                    url: "../../News/SetStar",
                    type: "POST",
                    data:
                        {
                           currentRating: currentRating,
                           id: id,
                           user: user
                        },
                        dataType: 'json'
                      });
            }    
        });
};

