$(document).ready(function () {
    $('#sm-icon').mouseenter(function () {
        $('#sm').fadeIn('slow');
    });
    $('#sm').mouseleave(function () {
        $('#sm').fadeOut('slow');
    });
});