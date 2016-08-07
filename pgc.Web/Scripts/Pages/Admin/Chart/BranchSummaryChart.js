$(document).ready(function () {
    LimitPrice();
    
})
$(document).on("callback", function () {
    LimitPrice();
})

var LimitPrice = function () {
    $('.chbMinValue input').click(function () {
        if ($(this).is(':checked')) {
            $('.txtprice').css('visibility', 'initial');
        }
        else {
            $('.txtprice').css('visibility', 'hidden');
        }
    })
}