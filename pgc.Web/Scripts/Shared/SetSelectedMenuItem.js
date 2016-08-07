$(document).ready(function () {

    var item = $('#SelectedMenuItem').val();

    $('div.sub a').each(function () {
        if ($(this).attr('href') == item) {

            $($(this).parent()).css('display', 'block');
            $($(this).parent().parent()).attr('class', $($(this).parent().parent()).attr('class') + ' cat-open');
            //$($(this).parent()).text($(this).text());
            $(this).attr('class', $(this).attr('class').replace('star', 'arrow'));
            
        }
        
    });
});