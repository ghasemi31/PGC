function pageLoad() {

    hide();

}

function hide() {
    $('ul.fup.insert li').children().css('display', 'none');

    $($('ul.fup.insert li:first').children()[0]).show();
    $($('ul.fup.insert li:nth-child(2)').children()[0]).show();
    //$('ul.fup.insert li iframe').contents().find('.preview')

    $('ul.fup.insert li iframe').each(function () {
        if ($(this).contents().find('.preview').size() == 1)
            $($(this).parent()).show();
    });

    $('ul.fup.insert li a').click(function () {

        $("html, body").animate({ scrollTop: $(this).offset().top - 400}, { duration: 800 });

        $($(this).parent().next().children()[0]).show();        
        $($(this).parent().children()).show();        
        $(this).hide();
        
        return false;
    });
};

