$(document).mousemove(function () {
    if (!$('#list').attr('data-tooltip')) {
        $('.tooltip').each(function () {
            $(this).easyTooltip();
        });
        $('#list').attr('data-tooltip', 'isbind');
    }
});