
$(document).on("callback", function () {
    $('.state').on("click", function () {
        if ($(this).val() != 1) {
            $('.all-branch').css('visibility', 'hidden ');
            $('.min-count').css('visibility', 'hidden');
        }
        else {
            $('.all-branch').css('visibility', 'initial');
            if ($('input[type = "checkbox"]').is(':checked')) {
                $('.min-count').css('visibility', 'initial');
            }
            else {
                $('.min-count').css('visibility', 'hidden');
            }
        }
    })
    $(function () {
        $('input[type="checkbox"]').bind('click', function () {
            if ($(this).is(':checked')) {
                $('.min-count').css('visibility', 'initial');
            }
            else {
                $('.min-count').css('visibility', 'hidden');
            }
        });
    });
})

