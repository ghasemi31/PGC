$(document).mousemove(function () {

    if (!$('.detailtbl').attr('data-isBind')) {
        if ($('.detailtbl input[type="text"]').size() > 0) {
            Init();

            $('.detailtbl input[type="checkbox"]').click(function () {
                ToggleText(this);
                if ($(this).attr('checked')) {
                    UpdatePrice($($($(this).parents('tr')[0]).find('input[type="text"]')));
                }
                UpdateTotalPrice();
            });

            $('.detailtbl input[type="text"]').keyup(function () { UpdatePrice(this); UpdateTotalPrice(); });

            $('.detailtbl input[type="text"]').keyup(function () {
                UpdatePrice(this);
                UpdateTotalPrice();
            });

            $('.detailtbl input[type="text"]').ForceNumericOnly();

            $('#detail input[type="submit"]').click(function () {
                UpdateOrderTitleList();
                //orderTitleList
            })
        }
        $('.detailtbl').attr('data-isBind', 'isBind');

    }
});


function UpdateOrderTitleList() {
    $('.detailtbl input[type="checkbox"]').each(function () {
        if ($(this).attr('checked')) {
            var id = $(this).attr('id');
            id = id.split('chk-')[1];

            var quantity = $($($(this).parents('tr')[0]).find('input[type="text"]')).val();

            $('#orderTitleList').val($('#orderTitleList').val() + ',' + id + '=' + quantity);
        }
    });   
};


function Init() {
//    $('.detailtbl span[id*="lbl"]').text('0');
//    $($('.footerRow td')[1]).text('0');
    $('.detailtbl input[type="text"]').each(function () {
        UpdatePrice(this);
        if (!$($($(this).parents('tr')[0]).find('input[type="checkbox"]')).attr('checked'))
            $(this).attr('disabled', 'disabled');
    });
    UpdateTotalPrice();  
}

function ToggleText(obj) {
    var textBox = $($($(obj).parents('tr')[0]).find('input[type="text"]'));
    var labelForPrice = $($($(obj).parents('tr')[0]).find('span[id*="lbl"]'));

    if ($(obj).attr('checked') == 'checked') {
        textBox.removeAttr('disabled');        
    }
    else {
        textBox.attr('disabled', 'disabled');
        labelForPrice.text('0');
    }
}

function UpdatePrice(obj) {
    var quantity = parseInt($(obj).val().replace(',', ''));
    var fee = parseInt($($($(obj).parents('tr')[0]).find('td')[1]).text().replace(',', '').replace(',', '').replace(',', '').replace(',', ''));
    var labelForPrice = $($($(obj).parents('tr')[0]).find('span[id*="lbl"]'));

    if ($(obj).val() == "")
        labelForPrice.text('0');
    else {
        var totalprice = (quantity * fee).toString();
        labelForPrice.text(totalprice.replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,"));
    } 
}

function UpdateTotalPrice() {
    var total = 0;
    for (var i = 0; i < $('.detailtbl span[id*="lbl"]').size(); i++) {
        var singleTotalPrice = $($('.detailtbl span[id*="lbl"]')[i]).text().toString();
        singleTotalPrice = singleTotalPrice.replace(',', '').replace(',', '').replace(',', '').replace(',', '').replace(' ریال', '');
        total += parseInt(singleTotalPrice);
    }
    $($('.footerRow td')[1]).text(total.toString() + " ریال");
    $($('.footerRow td')[1]).text($($('.footerRow td')[1]).text().replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,"));
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
                key == 46 ||
                (key >= 37 && key <= 40) ||
                (key >= 48 && key <= 57) ||
                (key >= 96 && key <= 105));
        });
    });
};