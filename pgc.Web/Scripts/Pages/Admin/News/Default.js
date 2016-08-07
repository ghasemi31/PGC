$(document).on("callback", function () {
    if ($('.dispGallery input[type="checkbox"]').is(':checked')) {
        $('.picGallery').css('visibility', 'initial');
    }
    else {
        $('.picGallery').css('visibility', 'hidden');
    }
    $(function () {
        $('.dispGallery input[type="checkbox"]').bind('click', function () {
            if ($(this).is(':checked')) {
                $('.picGallery').css('visibility', 'initial');
            }
            else {
                $('.picGallery').css('visibility', 'hidden');
            }
        });
    });
})