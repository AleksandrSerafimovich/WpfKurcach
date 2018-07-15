    function imgurUpload(file) {
                var headers = new Headers({
        'authorization': 'Client-ID 434c51b8fa4df14'
});

var form = new FormData();
form.append('image', file);

                return fetch('https://api.imgur.com/3/image', {
                    method: 'post',
            headers: headers,
            body: form
                }).then(function (response) {
                    return response.json();
                }).then(function (result) {
                    if (result.success) {
                        return result.data.link;
}

throw 'Upload error';
});
}

            var dragdropOptions = {
        allowedTypes: ['image/jpeg', 'image/png'],
                handleFile: function (file, createPlaceholder) {
                    var placeholder = createPlaceholder();
                    imgurUpload(file).then(function (url) {
        // Replace the placeholder with the image HTML
        placeholder.insert('<img src=\'' + url + '\' />');
    }).catch(function () {
        // Error so remove the placeholder
        placeholder.cancel();

    alert('Problem uploading image to imgur.');
});
}
};

var textarea = document.getElementById('demo');
    sceditor.create(textarea, {
    plugins: 'dragdrop',
        format: 'xhtml',
        resizeEnabled: false,
        height: 550,
    icons: 'monocons',
    dragdrop: dragdropOptions,
    autofocus: true,
    emoticonsRoot: '/',
    width: '100%',
    style: 'https://cdn.jsdelivr.net/npm/sceditor@2/minified/themes/content/default.min.css'
});

sceditor.instance(textarea).css('img {max - width: 50%; }');