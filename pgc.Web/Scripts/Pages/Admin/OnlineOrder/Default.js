$(document).ready(function () {

    $('input[id*=chbAllow]').each(function () {
        changeChecked(this);
        $(this).click(function () { changeChecked(this); })
    });

    if ($('input[type=radio][checked=checked]').val() == 'false')
        HideItems();
    else
        ShowItems(true);

    $('input[type=radio]').click(function () {
        if ($(this).val() == 'false')
            HideItems();
        else
            ShowItems(false);
    });

});

function HideItems() {
    $('input[type=checkbox]').attr('disabled', 'disabled');
    $('select[id*=timeTo],select[id*=timeFrom]').attr('disabled', 'disabled');
    $('tr.msg').fadeIn(500);
}
function ShowItems(isFirstTime) {
    
    $('input[type=checkbox]').removeAttr('disabled');
    $('select[id*=timeTo],select[id*=timeFrom]').removeAttr('disabled');
    $('input[id*=chbAllow]').each(function () { changeChecked(this); });

    if (!isFirstTime) {
        $('tr.msg').fadeOut(500);
        $('tr.msg').animate({ height: '0px' }, 500);
    }
    else {
        $('tr.msg').toggle();
    }
}
function changeChecked(obj) {
    var item = $(obj);
    var _Disabled = (item.attr('checked')) ? false : true;
    var timeFrom = $($(item.parent()).find('select[id*=timeFrom]')).attr('disabled', _Disabled);
    var timeTo = $($(item.parent()).find('select[id*=timeTo]')).attr('disabled', _Disabled);
}
