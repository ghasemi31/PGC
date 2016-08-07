$(document).mousemove(function () {

    if (!$('.txtPrice').attr('data-isBind')) {

        GetComma($('.txtPrice'));

        $('.txtPrice').ForceNumericOnly();

        $('.txtPrice').keyup(function () { GetComma(this); });

    }
});


function GetComma(obj) {
    var textNum = $(obj).val().replace(',', '').replace(',', '').replace(',', '').replace(',', '');
    $(obj).val(textNum.replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,"));
}

jQuery.fn.ForceNumericOnly =
function () {
    return this.each(function () {
        $(this).keydown(function (e) {
            var key = e.charCode || e.keyCode || 0;
            // allow backspace, tab, delete, arrows, numbers and keypad numbers ONLY
            return (
                key == 8 ||
                key == 9 ||
                key == 189 || 
                key == 173 ||
                key == 46 ||
                (key >= 37 && key <= 40) ||
                (key >= 48 && key <= 57) ||
                (key >= 96 && key <= 105));
        });
    });
};